using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
//using System.Windows.Forms;
using Microsoft.Win32;
using Spider.IO;
using SpiderDocsModule;
using NLog;
namespace SpiderDocsForms
{

//---------------------------------------------------------------------------------
	public class FileFolder : SpiderDocsModule.FileFolder
	{
		static Logger logger = LogManager.GetCurrentClassLogger();

		static readonly string user_path = workspace_path + SpiderDocsApplication.CurrentUserName + "\\";

//---------------------------------------------------------------------------------


//---------------------------------------------------------------------------------
		public static bool CreateUserFolder()
		{
			return FileFolder.CreateFolder(user_path);
		}

        //---------------------------------------------------------------------------------

        public static string GetUserFolder()
        {
            return user_path;
        }
        //---------------------------------------------------------------------------------
        public static string GetInstalledExePath()
		{
			string ans = "";

			using(RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(SpiderDocsApplication.RegistryPath))
			{
				if(registryKey != null)
				{
					try{ ans = registryKey.GetValue("SpiderDocsPath").ToString(); } catch{}
					registryKey.Close();
				}
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public static string GetInstalledPath()
		{
			return FileFolder.GetPath(GetInstalledExePath());
		}

//---------------------------------------------------------------------------------
// Office extensions --------------------------------------------------------------
//---------------------------------------------------------------------------------
		public static en_OfficeType OfficeCheck(string ext)
		{
			en_OfficeType ans = en_OfficeType.NotOffice;
			string extL = ext.ToLower();

			switch(extL)
			{
			case ".doc":
			case ".docx":
				if(SpiderDocsApplication.IsWord)
					ans = en_OfficeType.Word;
				break;

			case ".xls":
			case ".xlsx":
			case ".xlsm":
				if(SpiderDocsApplication.IsExcel)
					ans = en_OfficeType.Excel;
				break;

			case ".ppt":
			case ".pptx":
			case ".pps":
				if(SpiderDocsApplication.IsPowPnt)
					ans = en_OfficeType.PowerPoint;
				break;
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public static FileInfo[] GetAllFilesFromWorkSpace(bool only_hidden_files = false)
		{
			//get all files from workSpace directory
			DirectoryInfo pathFiles = new DirectoryInfo(FileFolder.GetUserFolder());

			FileInfo[] files = pathFiles.GetFiles("*.*", SearchOption.AllDirectories)
									.Where(a =>
										{
											if(a.Name.Contains(".user_workspace.sd")) return false;

											if(only_hidden_files)
												return ((a.Attributes & FileAttributes.Hidden) != 0);
											else
												return ((a.Attributes & FileAttributes.Hidden) == 0);
										})
									.ToArray();

			// Filter Office's back up files which starts from '~'
			files = files.Where(a => a.Name[0] != '~').ToArray();

			return files;
		}

//---------------------------------------------------------------------------------
		public static void DeleteHiddenFilesInWorkSpace()
		{
			FileInfo[] files = GetAllFilesFromWorkSpace(true);

			foreach(FileInfo file in files)
			{
				string path = FileFolder.GetPath(file.FullName);
				if(path != FileFolder.GetUserFolder())
					FileFolder.DeleteFilesAndThisDir(path);
				else
					FileFolder.DeleteFile(file.FullName);
			}
		}

//---------------------------------------------------------------------------------
		public static void ConvertOldWorkSpaceFormat()
		{
			FileInfo[] files = GetAllFilesFromWorkSpace();

			foreach(FileInfo file in files)
			{
				string path = file.FullName;
				string fileName = Path.GetFileName(path.Trim());

				if((5 <= fileName.Length)
				&& (fileName.IndexOf("~$", 0, 2) == -1)		//check if is temp file
				&& (fileName.IndexOf("DMS", 0, 3) != -1)	//check if file is from ckeck out
				&& (fileName.IndexOf("V", 4) != -1)
				&& (fileName.IndexOf("-", fileName.IndexOf("V", 5)) != -1))
				{
					int doc_id = Convert.ToInt32(fileName.Substring(3, fileName.IndexOf("V", 4) - 3));
					int doc_version = Convert.ToInt32(fileName.Substring(fileName.IndexOf("V", 4) + 1, (fileName.IndexOf("-") - fileName.IndexOf("V", 4)) - 1));
                    SpiderDocsForms.Document doc = DocumentController<SpiderDocsForms.Document>.GetDocument(doc_id, version: doc_version);

					if(doc != null && fileName.Contains(doc.title))
					{
						string new_path = FileFolder.GetPath(path) + doc.id_version + "\\";
						Directory.CreateDirectory(new_path);
						new_path = new_path + doc.title;

						File.Copy(path, new_path, true);

						if(File.Exists(new_path))
							FileFolder.DeleteFile(path);
					}
				}
			}
		}

//---------------------------------------------------------------------------------
		public static SpiderDocsForms.Document GetDocumentFromWorkSpace(string path)
		{
            SpiderDocsForms.Document doc = null;

			if(path.Contains(FileFolder.GetTempFolder()) || path.Contains(FileFolder.GetUserFolder()))
			{
				int version_id;
				string folder_name = FileFolder.GetContainedFolderName(path);

				if(!String.IsNullOrEmpty(folder_name) && int.TryParse(FileFolder.GetContainedFolderName(path), out version_id))
				{
					Dictionary<int,int> DocumentId = DocumentController.GetDocumentId(version_id);

					if(DocumentId.ContainsKey(version_id))
					{
						int doc_id = DocumentId[version_id];
						doc = DocumentController<SpiderDocsForms.Document>.GetDocument(doc_id, version_id);

                        if (doc == null)
                        {
							// null means possibily removed.

							logger.Warn("A file might be removed after checked out:{0}",path);

							return doc;
                        }

                        doc.path = path;
					}
				}
			}

			return doc;
		}

//---------------------------------------------------------------------------------
	}
}
