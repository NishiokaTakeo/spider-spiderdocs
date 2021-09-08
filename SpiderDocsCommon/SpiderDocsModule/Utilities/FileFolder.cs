using System;
using System.Linq;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{

	public class FileFolder : Spider.IO.FileFolder
	{
        static Logger logger = LogManager.GetCurrentClassLogger();
        //static protected string AppPath { get { return System.Reflection.Assembly.GetExecutingAssembly().Location; } }
        //protected static readonly string user_path = workspace_path + SpiderDocsApplication.CurrentUserName + "\\";

        static protected string workspace_path
        {
            get
            {

                string work = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if ( string.IsNullOrEmpty(work)) work = AppDomain.CurrentDomain.BaseDirectory;

                work += "\\SpiderDocs\\";

                return work;
            }
        }

        public static string SendToPath
        {
            get { return app_path + "SendTo\\"; }
        }

        public static string SendToDonePath
        {
            get { return SendToPath + "Done\\"; }
        }

        public static string SendToPendingPath
        {
            get { return SendToPath + "Pending\\"; }
        }

        public static string SendToOpeningPath
        {
            get { return SendToPath + "Opening\\"; }
        }

        //static protected readonly string workspace_path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SpiderDocs\\";


		public static readonly string TempPath = Environment.GetEnvironmentVariable("temp") + "\\SpiderDocs\\";
        static readonly string app_path = workspace_path + "__App__\\";

        public static readonly string OCR = GetExecutePath() + "\\OCR\\";
        public static readonly string OCR_DATA = OCR + "data\\";

        public static System.Collections.Generic.List<string> extensionsForScan
		{
				get {
						System.Collections.Generic.List<string> ans = new System.Collections.Generic.List<string>();

						ans.Add(".pdf");
						ans.AddRange(extensionsForImg);

						return ans;
					}
		}

		public static System.Collections.Generic.List<string> extensionsForImg
		{
			get
			{
				return new System.Collections.Generic.List<string>()
											{
												".png",
												".gif",
												".jpg",
												".jpeg",
												".bmp",
												".tif"
											};
			}
		}

        //---------------------------------------------------------------------------------
        // working folders ----------------------------------------------------------------
        //---------------------------------------------------------------------------------
        //public static string GetUserFolder()
        //{
        //    return user_path;
        //}

        /// <summary>
        /// System working folder
        /// </summary>
        /// <returns>System Path</returns>
        /// 

        public static void HideFolder(string path)
        {
            if (System.IO.File.Exists(path))
            {
                path = System.IO.Path.GetDirectoryName(path);
            }

            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);

                //DirectoryInfo di = Directory.CreateDirectory(path);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;

                foreach (var file in di.GetFiles("*.*", SearchOption.AllDirectories))
                    File.SetAttributes(file.FullName, (File.GetAttributes(file.FullName) | FileAttributes.Hidden));

            }
        }

        public static void UnHideFolder(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                //DirectoryInfo di = Directory.CreateDirectory(path);
                di.Attributes = FileAttributes.Directory & ~FileAttributes.Hidden;

                foreach (var file in di.GetFiles("*.*", SearchOption.AllDirectories))
                    File.SetAttributes(file.FullName, (File.GetAttributes(file.FullName) & ~FileAttributes.Hidden));

            }
        }

        public static string GetAppFolder()
		{
			if( !System.IO.Directory.Exists(app_path) )
				FileFolder.CreateFolder(app_path);

			return app_path;
		}



        public static string CreateAppFolder()
        {
            string apppath = GetAppFolder();

            // SendTo
            if( !System.IO.Directory.Exists(SendToPath) )
                FileFolder.CreateFolder(SendToPath,true);

                FileFolder.CreateFolder(SendToDonePath, true);

                FileFolder.CreateFolder(SendToPendingPath, true);

            if( !System.IO.Directory.Exists(SendToOpeningPath) )
			    FileFolder.CreateFolder(SendToOpeningPath, true);

            return apppath;

        }

		public static string GetWorkSpaceFolder()
		{
			if( !System.IO.Directory.Exists(workspace_path) )
				FileFolder.CreateFolder(workspace_path);

			return workspace_path;
		}


		public static bool CreateTempFolder()
		{
			return FileFolder.CreateFolder(TempPath, true);
		}

//---------------------------------------------------------------------------------
		public static string GetTempFolder()
		{
			if(!Directory.Exists(TempPath))
				CreateTempFolder();

			return TempPath;
		}

        /// <summary>
        /// Create Folder for Update Process
        /// </summary>
        /// <returns>full name of update folder</returns>
        public static string CreateUpdateFolder()
        {
            string path = TempPath + "Update\\";
            FileFolder.CreateFolder(path,true);
            return path;
        }

//---------------------------------------------------------------------------------
		public static bool DeleteTempFiles(string filter = "")
		{
			return FileFolder.DeleteFiles(GetTempFolder(), filter);
		}

        public static void GrantFullAccess(string fullPath)
        {
            FileInfo fi = new FileInfo(fullPath);
            // Get the file's FileSecurity
            var ac = fi.GetAccessControl();

            // inherit from the directory
            ac.SetAccessRuleProtection(false, false);

            ac.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, AccessControlType.Allow));

            // apply change
            fi.SetAccessControl(ac);
        }

        public static bool IsJson(string unknown)
        {
            bool ans = true;
            try
            {
                Newtonsoft.Json.Linq.JObject.Parse(unknown);
            }
            catch
            {
                ans = false;
            }

            return ans;
        }
//---------------------------------------------------------------------------------
        /// <summary>
        /// Get DocumentId and VersionNumber from email title.
        /// </summary>
        /// <param name="Subject">email title</param>
        /// <param name="idx_bracket_start"></param>
        /// <param name="idx_bracket_end"></param>
        /// <param name="idx_version_start"></param>
        static bool GetDocumentInfoIdxFromMail(string Subject, out int idx_bracket_start, out int idx_bracket_end, out int idx_version_start)
        {
            idx_bracket_start = -1;
            idx_bracket_end = -1;
            idx_version_start = -1;

            if (String.IsNullOrEmpty(Subject)) return false;

            int s = Subject.IndexOf("["), e = Subject.IndexOf("] - "), v = Subject.IndexOf("V");

            // return if title doesn't have folowing keywords "[", "] -" or version is out of "[ ] -" keywords.
            if (s == -1 || e == -1 || s >= v || v >= e) return false;

            idx_bracket_start = s;
            idx_bracket_end = e;
            idx_version_start = v;

            return true;

            /*
			idx_bracket_start = Subject.IndexOf("[");
			idx_bracket_end = Subject.IndexOf("] - ");

            if (String.IsNullOrEmpty(Subject)) {

				if((idx_bracket_start != -1) && (idx_bracket_end != -1))
				{
					idx_version_start = Subject.IndexOf("V");

					if((idx_bracket_start >= idx_version_start) || (idx_version_start >= idx_bracket_end))
					{
						// wrong format
						idx_bracket_start = -1;
						idx_bracket_end = -1;
						idx_version_start = -1;
					}
                }
			}
            */
		}

//---------------------------------------------------------------------------------
		public static SpiderDocsModule.Document GetDocumentFromMail(string Subject)
		{
            SpiderDocsModule.Document doc = null;
            int idx_bracket_start = -1, idx_bracket_end = -1, idx_version_start = -1;
            int doc_id,doc_version;

            if (String.IsNullOrEmpty(Subject)) return doc;

            string work = Subject.Replace("SpiderDocs:", "");

            // return null If Document information is not in title.
            if (!GetDocumentInfoIdxFromMail(work, out idx_bracket_start, out idx_bracket_end, out idx_version_start)) return doc;

            if (idx_bracket_start == -1 || idx_bracket_end == -1 || idx_version_start == -1) return doc;

            //Get documentId. return false if docmentId isn't found in the title.
            if (!int.TryParse(work.Substring(idx_bracket_start + 1, idx_version_start - idx_bracket_start - 1), out doc_id)) return doc;

            // Get versionId. return false if versionId isn't found in the title.
            if (!int.TryParse(work.Substring(idx_version_start + 1, idx_bracket_end - idx_version_start - 1), out doc_version)) return doc;

            if ((0 < doc_id) && (0 < doc_version))
                return DocumentController<SpiderDocsModule.Document>.GetDocument(doc_id, version: doc_version);

			return doc;
        }

//---------------------------------------------------------------------------------
		public static string SetMailSubject(string Subject, int doc_id, int version_no)
		{
			string ans = "";

            int idx_bracket_start = -1, idx_bracket_end = -1, idx_version_start = -1;

			if(!String.IsNullOrEmpty(Subject))
			{
				ans = Subject.Replace("SpiderDocs:", "");

                GetDocumentInfoIdxFromMail(ans, out idx_bracket_start, out idx_bracket_end, out idx_version_start);

				if(idx_bracket_start != -1)
				{
					ans = ans.Remove(idx_bracket_start, idx_bracket_end - idx_bracket_start + 1 + (" - ").Length);

				}else
				{
					idx_bracket_start = 0;
				}

			}else
			{
				idx_bracket_start = 0;
			}

			ans = ans.Insert(idx_bracket_start, "[" + doc_id + "V" + version_no + "] - ");

			return ans;
		}

        public static string YeildNewFileName(string path)
        {
            try
            {
                string[] devided = path.Split('.');

                string  basefile = string.Join(".", devided.Take( devided.Count() - 1) ),    // C:\Dev\test.jpg -> C:\Dev\test

                        extension = devided.LastOrDefault(),    // C:\Dev\test.jpg -> .jpg

                        prefix = DateTime.Now.ToString("ddHHmmssffff"),

                        support = new Random().Next(1, 10000).ToString(),

                        uniqstr = string.Format("{0:D4}",prefix + support);

                return string.Format("{0}_{1}.{2}", basefile,uniqstr,extension) ;
            }
            catch
            {
                return path;
            }
        }

        public static void MoveDirectory(string sourceDirectory, string destinationDirectory)
        {
            try
            {
                if( !Directory.Exists(destinationDirectory))
                    FileFolder.CreateFolder(destinationDirectory);

                var txtFiles = Directory.EnumerateFiles(sourceDirectory, "*");

                foreach (string currentFile in txtFiles)
                {
                    string fileName = currentFile.Substring(sourceDirectory.Length + 1);
                    Directory.Move(currentFile, Path.Combine(destinationDirectory, fileName));
                }

                Directory.Delete(sourceDirectory, true);
            }
            catch (Exception e)
            {
                logger.Error(e);
            }
        }

		public static byte[] ByteArrayFromPath(string file_path)
		{
            byte[] bytes = null;
            try
            {
                using (var fileHandle = ZetaLongPaths.ZlpIOHelper.CreateFileHandle(
                                            file_path,
                                            ZetaLongPaths.Native.CreationDisposition.OpenExisting,
                                            ZetaLongPaths.Native.FileAccess.GenericRead,
                                            ZetaLongPaths.Native.FileShare.Read | ZetaLongPaths.Native.FileShare.Write))
                using (FileStream fs = new System.IO.FileStream(fileHandle, System.IO.FileAccess.Read))
                {
                    BinaryReader br = new BinaryReader(fs);

                    bytes = br.ReadBytes((Int32)fs.Length);
                    br.Close();
                    fs.Close();

                    fileHandle.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return bytes;
        }

        /// <summary>
        /// Save byte[] data to the path
        /// </summary>
        /// <param name="filedata"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool SaveFile(byte[] filedata, string path)
        {

            bool ans = false;


            FileInfo fileinfo = new FileInfo(path);
            if (fileinfo.Exists)
            {
                try
                {
                    File.SetAttributes(path, FileAttributes.Normal);
                    File.Delete(path);

                }
                catch { }

            }
            else if (!Directory.Exists(FileFolder.GetPath(path)))
            {
                Directory.CreateDirectory(FileFolder.GetPath(path));
            }

            try
            {
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(filedata, 0, filedata.Length);
                fs.Flush();
                fs.Close();

                ans = true;

            }
            catch { }

            return ans;
        }



    }
}
