using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SpiderDocsModule;
using System.Threading.Tasks;
using ZetaLongPaths;

/*
    Import Foldes and belongs files to Spider Docs where established in Win Registry on running PC.
*/

namespace ImportFolders
{
    public interface IConfig
    {
        bool Test();
        string ImportRoot();
        int OperatorID();

        bool EmptyImport();
        string FolderSort();
        string Server();
        string JobNoFromPath(string path);

        List<DocumentAttribute> SaveAttr(string path);

        List<string> ListOnly();

        // Import after
        string ListOnlyAfter();
    }


    class Program : IConfig
    {
        static Logger logger = LogManager.GetCurrentClassLogger();
        static Logger loggerComp = LogManager.GetLogger("CMP");

        static Folder CurrentFolder { get; set; } = new Folder();

        static IConfig iConfig;
        static long totalsize = 0;
        static string resume = "";
        static void Main(string[] args)
        {
            Data.Init();

            iConfig = new Program();
            resume = iConfig.ListOnlyAfter();

            logger.Info("Task is started. Root:{0}, Test:{1}, EmptyImport:{2}", iConfig.ImportRoot(),iConfig.Test(),iConfig.EmptyImport());
            
            /*
             This section must change depend on destination.
             */
            SqlOperation.MethodToGetDbManager = new Func<DbManager>(() =>
            {
                return new DbManager(SpiderDocsApplication.CurrentServerSettings.conn, SpiderDocsApplication.CurrentServerSettings.svmode);
            });

            SpiderDocsApplication.CurrentServerSettings = new ServerSettings();
            SpiderDocsApplication.CurrentServerSettings.server = iConfig.Server();

            SpiderDocsServer server = new SpiderDocsServer(SpiderDocsApplication.CurrentServerSettings);
            server.onConnected += new Action<ServerSettings, bool>((RetServerSettings, ConnectionChk) =>
            {
                if (ConnectionChk)
                {
                    SpiderDocsApplication.CurrentServerSettings = RetServerSettings;

                    SqlOperation.MethodToGetDbManager = new Func<DbManager>(() =>
                    {
                        return new DbManager(SpiderDocsApplication.CurrentServerSettings.conn, SpiderDocsApplication.CurrentServerSettings.svmode);
                    });

                    SpiderDocsApplication.CurrentPublicSettings = new PublicSettings();
                    SpiderDocsApplication.CurrentPublicSettings.Load();
                }
            });

            server.Connect();

            // Add Folder
            Folder root = new Folder() {id=19,document_folder= "E Drive", id_parent = 0 }; //CreateFolderAndSetCurrent(DateTime.Now.ToString("yyyyMMddHHmm"), new Folder());
            try
            { 
                ImportStartAt(iConfig.ImportRoot(), root);

                logger.Info("Task is finished successfully : Total Size {0} MB.", ((double)totalsize / 1024 / 1024).ToString("N3"));
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }
        }

        static void ImportStartAt(string fileOrDir,Folder parent)
        {
            // Filter
            var orderbyFile = EntyiesBy(fileOrDir)/*.Where( x => !Exclude(x)).ToList()*/;
            
            foreach (string path in orderbyFile)
            {
                logger.Debug("Work at : {0}", path);

                if (iConfig.ListOnly().Count > 0 && !iConfig.ListOnly().Exists(x => path.Contains(x))) continue;

               _SeekAndImport(path,parent);
            }
        }

        static void SeekAndImport(string fileOrDir,Folder parent)
        {
            var orderbyFile = EntyiesBy(fileOrDir);

            foreach (string path in orderbyFile)
            {
               _SeekAndImport(path,parent);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileOrDir"></param>
        /// <returns>File paths</returns>
        static List<string> EntyiesBy(string fileOrDir)
        {
            var orderbyFile = GetFileSystemEntries(fileOrDir, OrderAsc());

            return orderbyFile;
        }
        static bool OrderAsc()
        {
            return iConfig.FolderSort() != "desc";
        }
        
        /// <summary>
        /// Get All file path with last write time
        /// </summary>
        /// <param name="fileOrDir"></param>
        /// <param name="asc"></param>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        static List<string> GetFileSystemEntries(string fileOrDir, bool asc = true, string searchPattern = "*")
        {
            Dictionary<string, DateTime> tmp = new Dictionary<string, DateTime>();

            //if (fileOrDir.Length >= 260)
            //{
            //    logger.Warn("The specified path, file name, or both are too long: {0}", fileOrDir);
            //}
            
            var dir = new ZlpDirectoryInfo(fileOrDir);
            
            // list the files
            try
            {
                foreach (var f in dir.GetFiles(searchPattern))
                {

                    if (!f.Attributes.HasFlag(ZetaLongPaths.Native.FileAttributes.Hidden)) tmp.Add(f.FullName,f.LastWriteTime);
                }
            }
            catch
            {
                Console.WriteLine("Directory {0}  \n could not be accessed!!!!", dir.FullName);
            }

            // process each directory
            // If I have been able to see the files in the directory I should also be able 
            // to look at its directories so I dont think I should place this in a try catch block
            foreach (var d in dir.GetDirectories())
            {                
                if (!d.Attributes.HasFlag(ZetaLongPaths.Native.FileAttributes.Hidden)) tmp.Add(d.FullName, d.LastWriteTime);
                //dirs.Add(d);
            }

            List<string> filesOrDir = asc ?
                                        tmp.ToList().OrderBy(x => x.Value).Select(x => x.Key).ToList()
                                        :
                                        tmp.ToList().OrderByDescending(x => x.Value).Select(x => x.Key).ToList();
            
            return filesOrDir;
        }

        static void _SeekAndImport(string path,Folder parent)
        {
            if (Path.GetFileName(path).IndexOf(".") == 0) return;
            //if (path.Length >= 260)
            //{
            //    logger.Warn("The specified path, file name, or both are too long: {0}",path);
            //    return;
            //}


            ZetaLongPaths.Native.FileAttributes attr = ZlpFileInfo.FromString(path).Attributes;// File.GetAttributes(path);
            if (attr.HasFlag(ZetaLongPaths.Native.FileAttributes.Directory))
            {
                logger.Debug("Seek files in {0}",path);

                //Create Folder 
                Folder nxtFld = CreateFolderAndSetCurrent(Path.GetFileName(path), parent);

                SeekAndImport(path, nxtFld);
                return;
            }
            return;
            if (!Data.PATHS.Exists(x => x.ToLower().Trim() == path.ToLower().Trim())) return;

            if (string.IsNullOrWhiteSpace(resume) )
            {
                int id_doc = Import(path,parent);
                if( id_doc > 0)
                {
                    // update CET
                    loggerComp.Info("UPDATE [tbl] SET [fld] = {0} WHERE [fld2] = '{1}'", id_doc, path);
                }
            }
            else
            {
                logger.Debug("NOT Imported : {0}", path);
            }

            if ( !string.IsNullOrWhiteSpace(resume) && resume == path ) resume = "";
        }

        static Folder CreateFolderAndSetCurrent(string name,Folder parent)
        {

            // nothing if exists
            Folder exists = FolderController.FindBy(name, parent.id);
            if (exists.id > 0)
            {
                logger.Debug("A folder exists already: {0}, {1}", exists.id, name);
                
                //Un archived
                if( exists.archived)
                    FolderController.ChangeArchiveStatus(exists.id, false);

                //Task.Run(() => AddPermission(exists.id));

                //Task.Run(() => AddPermission(exists.id));
                return new Folder { id = exists.id, document_folder = name, id_parent = parent.id };
            }

            Folder fld = new Folder() { document_folder = name, id_parent = parent.id };
            fld.id = FolderController.Save(fld); 

            //Task.Run(() => AddPermission(fld.id));

            //CurrentFolder = fld;
            logger.Debug("A folder is created: {0}, {1}", fld.id, fld.document_folder);

            // Clear Cache
            new Cache(iConfig.OperatorID()).RemoveMyCache(Cache.en_UKeys.DB_GetAssignedFolderToUser);

            return fld;
        }

        static DateTime CreateOrMofiedDate(Document doc)
        {
            var history = HistoryController.GetLatestHistory(doc.id_latest_version, new en_Events[] { en_Events.Created, en_Events.SaveNewVer,en_Events.Scan, en_Events.NewVer, en_Events.Import });

            if (history == null) return DateTime.MinValue;
            
            //if (history.date < doc.date)
            //    throw new Exception("Created date is nothing:{"+ history.id_doc + "}:{"+ doc.id_latest_version + "}");

            return history.date.AddSeconds(1);            // Millisecond is cut when a file is saved, so put 1 sec here.
        }

        static int Import(string fullpath,Folder parent)
        {
            if(! IsNotBeingUsed(fullpath))
            {
                logger.Warn("A file is being used by another process:{0}", fullpath);
                return 0;
            }

            string
                err = string.Empty,

                filename = Path.GetFileName(fullpath),

                extention = Path.GetExtension(fullpath);

            ZlpFileInfo fileinfo = new ZlpFileInfo(fullpath);

            Document doc;

            totalsize += fileinfo.Length;

            Document dbInDoc = SearchBy(parent.id, filename);
            DateTime createOrModified = CreateOrMofiedDate(dbInDoc);

            DateTime lastModified = fileinfo.LastWriteTime;

            //DateTime lastModified = System.IO.File.GetLastWriteTime(fullpath);

            //Skip if nothing changed.
            if (dbInDoc.id > 0 && lastModified < createOrModified)
            {
                return dbInDoc.id;
            }

            bool newOrVer = false;

            //Add as New
            if (dbInDoc.id <= 0)
            {
                if( Path.GetExtension(filename).Length > 8 )
                {
                    logger.Info("Extension Len is exceeded: {0}", fullpath);
                    return 0;
                }

                doc = new SpiderDocsModule.Document
                {
                    id_docType = 0,
                    id_folder = parent.id,
                    extension = extention,// file.Split(new Char[] { '.' }).Last(),
                    title = filename,
                    //Attrs = iConfig.SaveAttr(fullpath),
                    date = lastModified

                };

                doc.path = FilePath(fullpath);
                logger.Debug("Imported as New File: '{0}'", fullpath);

                if (iConfig.Test()) return 0;

                err = doc.AddDocument(iConfig.OperatorID(), false, lastModified);
                newOrVer = true;
            }

            // update if a doc has been updated since it is created
            else if (lastModified > createOrModified)
            {
                if (Path.GetExtension(filename).Length > 8)
                {
                    logger.Info("Extension Len is exceeded: {0}", fullpath);
                    return 0;
                }

                doc = dbInDoc;
                doc.path = FilePath(fullpath);

                logger.Debug("Imported as Ver Up, {2}, DB:'{0}', A file:'{1}'", createOrModified.ToString("dd/MM/yy HH:mm:ss"), lastModified.ToString("dd/MM/yy HH:mm:ss"), fullpath);

                if (iConfig.Test()) return 0;

                err = doc.AddVersion(iConfig.OperatorID(), false, lastModified);

                newOrVer = false;
            }
            else { doc = new Document(); }

            if (string.IsNullOrWhiteSpace(err))
            {
                doc = LastSavedIDs(doc);

                logger.Info("SAVED({0}:{1}) from : '{2}'", newOrVer ? "NEW":"VER", doc?.id, fullpath);

                return doc.id;
            }
            else
            {
                throw new Exception(string.Format("At {0}:{1} {2}", parent.document_folder, parent.id, fullpath));
            }
        }

        static SpiderDocsModule.Document LastSavedIDs(Document doc)
        {

            DTS_Document table = new DTS_Document(iConfig.OperatorID(), 10, SpiderDocsApplication.CurrentServerSettings.localDb);
            table.Criteria.Add(new SearchCriteria()
            {
                FolderIds = new List<int> { doc.id_folder },
                Titles = new List<string> { doc.title },
            });
            table.Select();
            List<SpiderDocsModule.Document> _search = table.GetDocuments<SpiderDocsModule.Document>();

            return _search.OrderByDescending(x => x.date).FirstOrDefault();
        }

        static SpiderDocsModule.Document SearchBy(int folderid, string title)
        {
            if ( title.IndexOf(".msg") > -1 || title.IndexOf(".eml") > -1)
            {
                title = System.Text.RegularExpressions.Regex.Replace(title, "\\.msg", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                title = System.Text.RegularExpressions.Regex.Replace(title, "\\.eml", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                title += " ";// .msg and .eml add date start with space. so add one more space.
            }

            var criteria = new SearchCriteria()
            {
                FolderIds = new List<int> { folderid },
                Titles = new List<string> { title },
            };

            var ext = System.IO.Path.GetExtension(title);
            if (ext != string.Empty)
                criteria.Extensions = new List<string> { ext };

            DTS_Document table = new DTS_Document(iConfig.OperatorID(), 1, SpiderDocsApplication.CurrentServerSettings.localDb);
            table.Criteria.Add(criteria);

            table.Select();
            List<SpiderDocsModule.Document> _search = table.GetDocuments<SpiderDocsModule.Document>();
            return _search.OrderByDescending(x => x.date).FirstOrDefault() ?? new Document();
        }

        static bool HasUpdatedSince(string import, int folderid, string title,out Document dbInDoc)
        {
            dbInDoc = SearchBy(folderid, title);

            DateTime lastModified = System.IO.File.GetLastWriteTime(import);

            if( lastModified > dbInDoc.date)
            {
                logger.Info("A file is modified DB:'{0}', A file:'{1}'", lastModified.ToString("dd/MM/yy HH:mm:ss"), dbInDoc.date.ToString("dd/MM/yy HH:mm:ss"));
                return true;
            }
            else
            {
                return false;
            }
        }
        
        static string MyTemp()
        {           
            string  tmp  = Path.GetTempPath() + "SDIMPORT_"+DateTime.Now.ToString("yyyyMMddhhmm")+"/" ;
            if( !Directory.Exists(tmp) )
            {
                Directory.CreateDirectory(tmp);
            }

            return tmp;
        }

        static string FilePath(string original)
        {
            if( !iConfig.EmptyImport() )
            {
                return original;
            }

            string zero = MyTemp() + Path.GetFileName(original);

            using (File.Create(zero)) 

            return zero;
        }
        
        
        ///// <summary>
        ///// Grant Full Permission to folder
        ///// </summary>
        ///// <param name="folder_id"></param>
        //static void AddPermission(int folder_id)
        //{
        //    PermissionController.AssignFolder(en_FolderPermissionMode.Group, folder_id, SpiderDocsModule.Group.ALL);

        //    en_FolderPermissionMode mode = en_FolderPermissionMode.Group;

        //    Dictionary<en_Actions, en_FolderPermission> permissions = new Dictionary<en_Actions, en_FolderPermission>();

        //    permissions.Add(en_Actions.CheckIn_Out, en_FolderPermission.Allow);
        //    permissions.Add(en_Actions.ImportNewVer, en_FolderPermission.Allow);
        //    permissions.Add(en_Actions.OpenRead, en_FolderPermission.Allow);
        //    permissions.Add(en_Actions.Delete, en_FolderPermission.Allow);
        //    permissions.Add(en_Actions.Export, en_FolderPermission.Allow);
        //    permissions.Add(en_Actions.Archive, en_FolderPermission.Allow);
        //    permissions.Add(en_Actions.SendByEmail, en_FolderPermission.Allow);
        //    permissions.Add(en_Actions.Rollback, en_FolderPermission.Allow);
        //    permissions.Add(en_Actions.Properties, en_FolderPermission.Allow);

        //    PermissionController.AddPermission(folder_id, SpiderDocsModule.Group.ALL, mode, permissions);

        //}

        static bool IsNotBeingUsed(string path)
        {
            bool ok = true;
            try
            { 
                using (var fileHandle = ZetaLongPaths.ZlpIOHelper.CreateFileHandle(
                                path,
                                ZetaLongPaths.Native.CreationDisposition.OpenAlways,
                                ZetaLongPaths.Native.FileAccess.GenericAll,
                                ZetaLongPaths.Native.FileShare.Read))
                using (FileStream fs = new System.IO.FileStream(fileHandle, System.IO.FileAccess.Read))
                {
                    BinaryReader br = new BinaryReader(fs);

                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    br.Close();
                    fs.Close();

                
                }
            }            
            catch
            {
                ok = false;
            }

            return ok;
        }


        public bool Test()
        {
            return File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/test");
        }

        public string ImportRoot()
        {
            
            return System.Configuration.ConfigurationManager.AppSettings["root"]; //@"C:\Users\spider\Dropbox";
        }

        public int OperatorID()
        {
            return 7;
        }

        public bool EmptyImport()
        {
            return File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/.emptyimport");
        }
        public string Server()
        {
            return System.Configuration.ConfigurationManager.AppSettings["server"]; //@"C:\Users\spider\Dropbox";
        }

        public List<string> ListOnly()
        {          
            string camna = System.Configuration.ConfigurationManager.AppSettings["include.only"];

            if( string.IsNullOrWhiteSpace(camna)) return new List<string>();

            return camna.Split(',').ToList(); //@"C:\Users\spider\Dropbox";
        }

        public string ListOnlyAfter()
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["include.only.after"];

            return path ?? "";
        }

        public string JobNoFromPath(string path)
        {

            string root = ImportRoot();
            path = path.Replace(root, "");

            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^(\/|\\)+", System.Text.RegularExpressions.RegexOptions.None);
            path = reg.Replace(path,"");

            reg = new System.Text.RegularExpressions.Regex(@"(\/|\\).*");
            path = reg.Replace(path, "");

            return path;
        }

        public List<DocumentAttribute> SaveAttr(string path)
        {
            string job = JobNoFromPath(path);
            var attrs = new List<DocumentAttribute>() { new DocumentAttribute() { id = 3, atbValue = job } };

            return attrs;
        }

        public string FolderSort()
        {
            string srt = System.Configuration.ConfigurationManager.AppSettings["folder.sort"];

            return srt;
        }
    }
}

/*
 select d.id as id_doc ,d.title,dh.id_user,dh.date from document as d
inner join document_historic as dh on d.id_latest_version = dh.id_version
inner join document_folder as f on f.id = d.id_folder
where d.id_folder not in (6,9,3480,3487,3492,3522,3524,3538,3545,3554,3556,3566,3596,3619,3623,3620,3621,3622,3597,3598,3603,3604,3609,3614,3618,3615,3616,3617,3610,3611,3612,3613,3605,3606,3607,3608,3599,3600,3601,3602,3567,3568,3573,3574,3575,3577,3579,3583,3584,3585,3586,3587,3588,3589,3590,3591,3592,3593,3594,3595,3580,3581,3582,3578,3576,3569,3570,3572,3571,3557,3558,3560,3562,3564,3565,3563,3561,3559,3555,3546,3548,3549,3550,3551,3553,3552,3547,3539,3540,3541,3542,3543,3544,3525,3528,3531,3536,3537,3532,3533,3534,3535,3529,3530,3526,3527,3523,3493,3494,3495,3496,3497,3498,3499,3500,3501,3503,3504,3505,3506,3507,3508,3509,3510,3511,3512,3513,3514,3516,3517,3518,3519,3520,3521,3515,3502,3488,3489,3490,3491,3481,3483,3484,3485,3486,3482,321792,321754,321753,21,2696,11885,321794,321844,1,2,3,4,5,6,7,8,10,11,12,13,14,15,16,17,18,19,20,22,23,24,25,26,27,28,29,30,31,32,33,35,37,40,41,42,44,45,46,48,196,3479,59998,59999,102618,321600)
and f.archived = 0
and dh.id_user = 1
order by date 
     */
