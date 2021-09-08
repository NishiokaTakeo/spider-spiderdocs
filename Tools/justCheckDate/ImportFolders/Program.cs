using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandLine;
using SpiderDocsModule;
using System.Threading.Tasks;

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
        string Server();

        List<string> ListFirstLayer();

        string ListFirstLayerAfter();
    }


    class Program : IConfig
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        static Folder CurrentFolder { get; set; } = new Folder();

        static IConfig iConfig;
        static long totalsize = 0;
        static string resume = "";
        static void Main(string[] args)
        {

            iConfig = new Program();
            resume = iConfig.ListFirstLayerAfter();

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
            Folder root = new Folder() {id=196,document_folder= "M Drive", id_parent = 0 }; //CreateFolderAndSetCurrent(DateTime.Now.ToString("yyyyMMddHHmm"), new Folder());
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
            var orderbyFile = EntyiesBy(fileOrDir).Where( x => !Exclude(x)).ToList();
            
            foreach (string path in orderbyFile)
            {
                logger.Debug("Work at : {0}", path);

                if (path.IndexOf("PDF_Reports") > -1 || path.IndexOf("SpiderDocsDownloadedFiles") > -1 || path.IndexOf("_2019") > -1) continue;
                
               _SeekAndImport(path,parent);
                //try
                //{
                //    Archive(path);
                //}catch(Exception ex)
                //{
                //    logger.Error(ex);
                //}
            }
        }

        static void Archive(string path)
        {
            string to = path.Replace("M:\\", "M:\\00000_SpiderTemp\\");

            logger.Info("Done. Moved {0} to {1}", path,to);

            if (Path.GetFileName(path).IndexOf(".") == 0) return;

            FileAttributes attr = File.GetAttributes(path);
            if (attr.HasFlag(FileAttributes.Directory))
            {
                if(System.IO.Directory.Exists(to) )
                    to += "_"+DateTime.Now.ToString("yyyyMMddHmmss");

                System.IO.Directory.Move(path, to);
            }
            else
            {
                System.IO.File.Move(path, to);
                
            }

        }

        static bool Exclude(string path)
        {
            if (iConfig.ListFirstLayer().Count() == 0) return false;

            FileAttributes attr = File.GetAttributes(path);
            if (attr.HasFlag(FileAttributes.Directory))
            {
                if (iConfig.ListFirstLayer().Exists(x => x == System.IO.Path.GetFileNameWithoutExtension(path))) return false ;
            }

            return true;
        }

        static void SeekAndImport(string fileOrDir,Folder parent)
        {
            //Folder parentFolder = parent;

            var orderbyFile = EntyiesBy(fileOrDir).ToList();

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
            var orderbyFile = Directory.GetFileSystemEntries(fileOrDir)
                                .Where(f => !new FileInfo(f).Attributes.HasFlag(FileAttributes.Hidden))
                                //.OrderBy(f => new FileInfo(f).Attributes.HasFlag(FileAttributes.Directory))
                                .OrderByDescending(x => new FileInfo(x).LastWriteTime).ToList();

            return orderbyFile;
        }

        static void _SeekAndImport(string path,Folder parent)
        {
            if (Path.GetFileName(path).IndexOf(".") == 0) return;
            
            FileAttributes attr = File.GetAttributes(path);
            if (attr.HasFlag(FileAttributes.Directory))
            {
                logger.Debug("Seek files in {0}",path);

                //Create Folder 
                Folder nxtFld = CreateFolderAndSetCurrent(Path.GetFileName(path), parent);

                SeekAndImport(path, nxtFld);
                return;
            }

            if (string.IsNullOrWhiteSpace(resume) )
            {
                Import(path,parent);
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
                
                Task.Run(() => AddPermission(exists.id));
                return new Folder { id = exists.id, document_folder = name, id_parent = parent.id };
            }
            else
            {

            }
            
            Folder fld = new Folder() { document_folder = name, id_parent = parent.id };
            fld.id = FolderController.Save(fld); 

            Task.Run(() => AddPermission(fld.id));

            //CurrentFolder = fld;
            logger.Debug("A folder is created: {0}, {1}", fld.id, fld.document_folder);

            // Clear Cache
            new Cache(iConfig.OperatorID()).RemoveMyCache(Cache.en_UKeys.DB_GetAssignedFolderToUser);

            return fld;
        }
        static DateTime CreateOrMofiedDate(Document doc)
        {
            var history = HistoryController.GetLatestHistory(doc.id_latest_version, new en_Events[] { en_Events.Created, en_Events.SaveNewVer, en_Events.NewVer });

            if (history == null) return DateTime.MinValue;

            return history.date > doc.date ? history.date : doc.date;
        }
        static void Import(string fullpath,Folder parent)
        {
            string
                err = string.Empty, 

                filename = Path.GetFileName(fullpath),

                extention = Path.GetExtension(fullpath);

            totalsize += new System.IO.FileInfo(fullpath).Length;

            Document dbInDoc = SearchBy(parent.id, filename);
            DateTime createOrModified = CreateOrMofiedDate(dbInDoc);

            DateTime lastModified = System.IO.File.GetLastWriteTime(fullpath);
            
            //Skip if nothing changed.
            if (dbInDoc.id > 0 && lastModified <= createOrModified)
            {
                return;
            }
            
            //Add as New
            if (dbInDoc.id <= 0)
            {
                logger.Error("File not found: {0}",fullpath);
                return;
                //throw new Exception("Error");
            }

            dbInDoc = LastSavedIDs(dbInDoc);

            DateTime latest;
            // update if a doc has been updated since it is created
            if (lastModified > createOrModified)
            {
                latest = lastModified;
                //
            }
            else
            {
                latest = createOrModified;
            }

            logger.Info("{0}, {1}, {2}", dbInDoc.id, dbInDoc.id_latest_version,latest.ToString("yyyy/MM/dd HH:mm:ss"));
        }

        static SpiderDocsModule.Document LastSavedIDs(Document doc)
        {
            DTS_Document table = new DTS_Document(iConfig.OperatorID(), 100, SpiderDocsApplication.CurrentServerSettings.localDb);
            table.Criteria.Add(new SearchCriteria()
            {
                FolderIds = new List<int> { doc.id_folder },
                Titles = new List<string> { doc.title },
            });
            table.Select();
            List<SpiderDocsModule.Document> _search = table.GetDocuments<SpiderDocsModule.Document>();

            return _search.FirstOrDefault();
        }

        static SpiderDocsModule.Document SearchBy(int folderid, string title)
        {
            DTS_Document table = new DTS_Document(iConfig.OperatorID(), 1, SpiderDocsApplication.CurrentServerSettings.localDb);
            table.Criteria.Add(new SearchCriteria()
            {
                FolderIds = new List<int> { folderid },
                Titles = new List<string> { title },
            });
            table.Select();
            List<SpiderDocsModule.Document> _search = table.GetDocuments<SpiderDocsModule.Document>();
            return _search.FirstOrDefault() ?? new Document();
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
        
        
        /// <summary>
        /// Grant Full Permission to folder
        /// </summary>
        /// <param name="folder_id"></param>
        static void AddPermission(int folder_id)
        {
            PermissionController.AssignFolder(en_FolderPermissionMode.Group, folder_id, Group.ALL);

            en_FolderPermissionMode mode = en_FolderPermissionMode.Group;

            Dictionary<en_Actions, en_FolderPermission> permissions = new Dictionary<en_Actions, en_FolderPermission>();

            foreach (en_Actions actn in Enum.GetValues(typeof(en_Actions)))
            {
                if ((int)actn > 10) break;

                permissions.Add((en_Actions)actn, en_FolderPermission.Allow);
            }

            PermissionController.AddPermission(folder_id, Group.ALL, mode, permissions);

        }

        bool IConfig.Test()
        {
            return File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/.test");
        }

        string IConfig.ImportRoot()
        {
            
            return System.Configuration.ConfigurationManager.AppSettings["root"]; //@"C:\Users\spider\Dropbox";
        }

        int IConfig.OperatorID()
        {
            return 1;
        }

        bool IConfig.EmptyImport()
        {
            return File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/.emptyimport");
        }
        string IConfig.Server()
        {
            return System.Configuration.ConfigurationManager.AppSettings["server"]; //@"C:\Users\spider\Dropbox";
        }

        List<string> IConfig.ListFirstLayer()
        {          
            string camna = System.Configuration.ConfigurationManager.AppSettings["include.firstlayer"];

            if( string.IsNullOrWhiteSpace(camna)) return new List<string>();

            return camna.Split(',').ToList(); //@"C:\Users\spider\Dropbox";
        }

        string IConfig.ListFirstLayerAfter()
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["include.firstlayer.after"];

            return path ?? "";
        }
    }
}
