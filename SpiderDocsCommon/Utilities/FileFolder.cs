using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using File = System.IO.File;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using IWshRuntimeLibrary;
using Spider.Types;
using System.Security.AccessControl;
using System.Security.Principal;

//---------------------------------------------------------------------------------
namespace Spider.IO
{
	public enum en_OfficeType
	{
		Word,
		Excel,
		PowerPoint,
		NotOffice,

		Max
	}

	public enum en_FilterType
	{
		Images,
		PDF,
		PDFAndJpg,
		Excel,
		Word,
		PowerPoint,

		Max
	}

//---------------------------------------------------------------------------------
	/// <summary>
	/// <para>Provide common functions to deal with files.</para>
	/// </summary>
	public class FileFolder
	{
		/// <summary>
		/// <para>String table of extensions of Word files.</para>
		/// </summary>
		public static readonly List<string> extensionsForWord = new List<string>()
		{
			".doc",
			".docx"
		};

		/// <summary>
		/// <para>String table of extensions of Excel files.</para>
		/// </summary>
		public static readonly List<string> extensionsForExcel = new List<string>()
		{
			".xls",
			".xlsx"
		};

		/// <summary>
		/// <para>String table of extensions of PowerPoint files.</para>
		/// </summary>
		public static readonly List<string> extensionsForPowerPoint = new List<string>()
		{
			".ppt",
			".pptx"
		};

		/// <summary>
		/// <para>String table of extensions of image files.</para>
		/// </summary>
		public static readonly List<string> extensionsForImage = new List<string>()
		{
			".png",
			".gif",
			".jpg",
			".jpeg",
			".bmp",
			".tif"
		};

		/// <summary>
		/// <para>String table of extensions of PDF and Jpeg files.</para>
		/// </summary>
		public static readonly List<string> extensionsForPDFAndJpg = new List<string>()
		{
			".pdf",
			".jpg",
			".jpeg"
		};

		/// <summary>
		/// <para>String table of extensions of PDF file.</para>
		/// </summary>
		public static readonly List<string> extensionsForPDF = new List<string>()
		{
			".pdf",
		};

        /// <summary>
        /// Check if path's file is image
        /// </summary>
        /// <param name="path"></param>
        /// <returns>True if an argument is image file</returns>
        public static bool Has4Image(string path)
        {
            return extensionsForImage.Exists(f => path.ToLower().Contains(f.ToLower()));
        }

        /// <summary>
        /// Check if path's file is pdf
        /// </summary>
        /// <param name="path"></param>
        /// <returns>True if an argument is pdf file</returns>
        public static bool Has4PDF(string path)
        {
            return extensionsForPDF.Exists(f => path.ToLower().Contains(f.ToLower()));
        }
		/// <summary>
		/// Check if path can be ocr
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static bool CanOCR(string path)
		{
			return  Has4PDF( path ) || Has4Image(path);
		}

        //---------------------------------------------------------------------------------
        public static bool IsFileLocked(string filePath)
		{
			try
			{
                FileInfo fileInfo = new FileInfo(filePath);
                fileInfo.IsReadOnly = false;

                using (File.Open(filePath, FileMode.Open)){}
			}
			catch (IOException e)
			{
				var errorCode = Marshal.GetHRForException(e) & ((1 << 16) - 1);

				return (errorCode == 32 || errorCode == 33);
			}

			return false;
		}

//---------------------------------------------------------------------------------
		/// <summary>
		/// <para>Gives filter string for the file save dialog.</para>
		/// </summary>
		/// <param name="AllFiles">Set true if allow uses to select any file types.</param>
		public static string GetExtensionFilterString(en_FilterType mode, bool AllFiles = false)
		{
			return GetExtensionFilterString(mode, null, AllFiles);
		}

		/// <summary>
		/// <para>Gives filter string for the file save dialog.</para>
		/// </summary>
		/// <param name="AllFiles">Set true if allow uses to select any file types.</param>
		public static string GetExtensionFilterString(List<string> Extensions, bool AllFiles = false)
		{
			return GetExtensionFilterString(en_FilterType.Max, Extensions, AllFiles);
		}

		/// <summary>
		/// <para>Gives filter string for the file save dialog.</para>
		/// </summary>
		/// <param name="AllFiles">Set true if allow uses to select any file types.</param>
		public static string GetExtensionFilterString(string Extension, bool AllFiles = false)
		{
			List<string> Extensions = new List<string>();
			Extensions.Add(Extension);

			return GetExtensionFilterString(en_FilterType.Max, Extensions, AllFiles);
		}

		static string GetExtensionFilterString(en_FilterType mode, List<string> file_extension, bool AllFiles)
		{
			List<string> extensionsList = new List<string>();
			string extTitle = "";

			if((file_extension != null) && (0 < file_extension.Count))
			{
				extTitle = "FILES";
				file_extension = file_extension.Select(a => a.ToLower()).ToList();
				extensionsList.AddRange(file_extension);
			}
			else if(mode == en_FilterType.Images)
			{
				extTitle = "Image files";
				extensionsList = extensionsForImage;
			}
			else if(mode == en_FilterType.PDF)
			{
				extTitle = "PDF files";
				extensionsList = extensionsForPDF;
			}
			else if(mode == en_FilterType.PDFAndJpg)
			{
				extTitle = "PDF/JPG files";
				extensionsList = extensionsForPDFAndJpg;
			}
			else if(mode == en_FilterType.Word)
			{
				extTitle = "Microsoft Word files";
				extensionsList = extensionsForWord;
			}
			else if(mode == en_FilterType.Excel)
			{
				extTitle = "Microsoft Excel files";
				extensionsList = extensionsForExcel;
			}
			else if(mode == en_FilterType.PowerPoint)
			{
				extTitle = "Microsoft PowerPoint files";
				extensionsList = extensionsForExcel;
			}

			string extensionString = "";
			string extStr1 = "";
			string extStr2 = "";

			if(extensionsList.Count > 0)
			{
				foreach(string ext in extensionsList)
				{
					if(!string.IsNullOrEmpty(extStr1))
					{
						extStr1 += ", ";
					}
					if(!string.IsNullOrEmpty(extStr2))
					{
						extStr2 += ";";
					}
					extStr1 += ext.ToUpper();
					extStr2 += string.Format("*{0}", ext);
				}
				extensionString = extTitle;
				extensionString += " (";
				extensionString += extStr1;
				extensionString += ")|";
				extensionString += extStr2;
			}

			if(AllFiles)
				extensionString += "|All files (*.*)|*.*";

			return extensionString;
		}

//---------------------------------------------------------------------------------
		/// <summary>
		/// <para>Delete the specified file.</para>
		/// </summary>
		public static bool DeleteFile(string fullpath)
		{
			return DeleteFiles(new List<string>() { fullpath });
		}

		/// <summary>
		/// <para>Delete the specified files.</para>
		/// </summary>
		public static bool DeleteFiles(List<string> fullpaths)
		{
			bool ans = true;

			foreach(string path in fullpaths)
			{
				if(!DeleteFiles(GetPath(path), Path.GetFileName(path)))
					ans = false;
			}

			return ans;
		}

		/// <summary>
		/// <para>Delete the specified folder including contained files and folders.</para>
		/// </summary>
		public static bool DeleteFilesAndThisDir(string path)
		{
			return DeleteFiles(path, include_root_folder: true);
		}

		/// <summary>
		/// <para>Delete files which have been modified in specified period in the specified folder.</para>
		/// </summary>
		public static bool DeleteFilesInPeriod(string path, DateTime From = new DateTime(), DateTime To = new DateTime())
		{
			Period period = new Period(From, To);

			return DeleteFiles(path, period: period);
		}

		/// <summary>
		/// <para>Delete folders and files which are in the specified folder.</para>
		/// </summary>
		/// <param name="filter">Set string which is part of file or folder name to be deleted.</param>
		/// <param name="include_root_folder">Set true to delete the specified folder (the root folder) as well.</param>
		/// <param name="period">Set period of modified time of files or folders to be deleted.</param>
		public static bool DeleteFiles(
			string path,
			string filter = "",
			bool include_root_folder = false,
			Period period = null)
		{
			bool ans = false;

			if(!Directory.Exists(path) && !File.Exists(path))
				return true;

			try
			{
				DirectoryInfo pathFiles = new DirectoryInfo(path);

				FileInfo[] files;
				DirectoryInfo[] dirs;

				if(filter != "")
				{
					files = pathFiles.GetFiles("*" + filter + "*.*");
					dirs = pathFiles.GetDirectories("*" + filter + "*");

				}else
				{
					files = pathFiles.GetFiles("*.*");
					dirs = pathFiles.GetDirectories("*");
				}

				foreach(FileInfo fileinfo in files)
				{
					bool bk_IsReadOnly = fileinfo.IsReadOnly;

					try
					{
						fileinfo.IsReadOnly = false;

						if((period == null) || period.IsInThisPeriod(File.GetLastWriteTime(fileinfo.FullName)))
							File.Delete(fileinfo.FullName);

					}catch
					{
						fileinfo.IsReadOnly = bk_IsReadOnly;
					}
				}

				foreach(DirectoryInfo dir in dirs)
				{
					if((period == null) || period.IsInThisPeriod(Directory.GetLastWriteTime(dir.FullName)))
						Directory.Delete(dir.FullName, true);
				}

				if(include_root_folder)
					Directory.Delete(path, true);

				ans = true;

			}catch {}

			return ans;
		}

//---------------------------------------------------------------------------------
		/// <summary>
		/// <para>Get path excluding file name.</para>
		/// </summary>
		public static string GetPath(string path)
		{
			string name = Path.GetFileName(path);

			if(!String.IsNullOrEmpty(name))
				path = path.Replace(name, "");

			return path;
		}

//---------------------------------------------------------------------------------
		/// <summary>
		/// <para>Get the closest folder name.</para>
		/// <para>E.g. "C:\abc\def\ghi.txt" -> "def"</para>
		/// </summary>
		public static string GetContainedFolderName(string path)
		{
			string ans = "";

			if(path.Contains('\\'))
			{
				path = path.Replace(Path.GetFileName(path), "");
				path = path.Remove(path.Length - 1);
				int idx = path.LastIndexOf("\\");

				ans = path.Substring(idx + 1);
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public static string GetExecutePath()
		{
			return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\";
		}

//---------------------------------------------------------------------------------
		public static string GetExecuteFileName()
		{
			return Process.GetCurrentProcess().MainModule.FileName;
		}

//---------------------------------------------------------------------------------
		[DllImport("kernel32", EntryPoint = "GetShortPathName", CharSet = CharSet.Auto, SetLastError = true)]
		static extern int GetShortPathName(String pathName, StringBuilder shortName, int cbShortName);
		public static string GetShortPathName(string path)
		{
			StringBuilder sb = new StringBuilder(255);
			int n = GetShortPathName(path, sb, 255);

			if(n == 0) // check for errors
				return "";
			else
				return sb.ToString();
		}

//---------------------------------------------------------------------------------
		/// <summary>
		/// <para>Returns true if specified string is usable for file name.</para>
		/// </summary>
		public static bool IsValidFileName(string fileName)
		{
			bool ans = false;

			try
			{
			  new System.IO.FileInfo(fileName);
			  ans = true;
			}
			catch(ArgumentException)
			{
			}
			catch(System.IO.PathTooLongException)
			{
			}
			catch(NotSupportedException)
			{
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		/// <summary>
		/// <para>Checks if same file exists or not.
		/// <para> If exists, adds an index number which increases until it becomes in-existing file name into the end of file name and returns it.</para>
		/// </summary>
		public static string GetAvailableFileName(string FullPath)
		{
			string path = GetPath(FullPath);
			string name = Path.GetFileName(FullPath);

			if(!Directory.Exists(path))
				Directory.CreateDirectory(path);

			List<string> ExistingFilesNames = Directory.GetFiles(path).ToList();

			string AvailableFileName = GetAvailableFileName
										(
											name,
											((string a) =>
											{
												return !ExistingFilesNames.Exists(b => b.ToLower().Contains(a.ToLower()));
											})
										);

			return path + AvailableFileName;
		}

		/// <summary>
		/// <para>Checks if specified file name meets the specified condition.
		/// <para> If does not meet, adds an index number which increases until it meets the condition and returns it.</para>
		/// </summary>
		/// <param name="condition">File name check function.</param>
		public static string GetAvailableFileName(string FullPath, Func<string, bool> condition)
		{
			string ans = FullPath;

			// put index up to 10000
			for(int i = 1; i <= 10000; i++)
			{
				if(condition(FullPath))
				{
					ans = FullPath;
					break;

				}else
				{
					string extension = Path.GetExtension(FullPath);
                    if(extension == null || string.IsNullOrWhiteSpace(extension) )
                        FullPath = FullPath+" (" + i + ")" + extension; // extension null cause exception if try to replace from null.
                    else 
					    FullPath = FullPath.Replace(extension, "") + " (" + i + ")" + extension;
				}
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public static void MoveFile(string origPath, string destPath)
		{
			if(File.Exists(destPath))
			{
				FileInfo fileinfo = new FileInfo(destPath);
				fileinfo.IsReadOnly = false;
				File.Delete(destPath);
			}

			File.Move(origPath, destPath);
		}

//---------------------------------------------------------------------------------
		/// <summary>
		/// <para>Rename the specified file and returns full path to the renamed file.</para>
		/// </summary>
		public static string RenameFile(string path, string newName)
		{
			string ans = "";

			if(File.Exists(path))
			{

				FileInfo fileinfo = new FileInfo(path);
				string tempName = path.Replace(fileinfo.Name, "") + newName;

				fileinfo.IsReadOnly = false;

				try
				{
					File.Move(path, tempName);
					ans = tempName;

				}catch {}
			}

			return ans;
		}

        //---------------------------------------------------------------------------------
        /// <param name="CleanUp">Set true to delete all contained files when specified folder already exists.</param>
        public static bool CreateFolder(string path, bool CleanUp = false)
		{
			bool ans = true;

			try
			{
                Directory.CreateDirectory(path);

                GrantAccess(path);

                if (CleanUp)
                {
                    DeleteFiles(path);
                }

            }
            catch { ans = false; }

			return ans;
		}

        private static void GrantAccess(string fullPath)
        {
            try
            {
                GetDirectoryAccessControll(fullPath);

                DirectoryInfo dInfo = new DirectoryInfo(fullPath);
                DirectorySecurity dSecurity = dInfo.GetAccessControl();
                grant_full_controll(dInfo);
            }
            catch { }
        }
        private static DirectorySecurity GetDirectoryAccessControll(string path, string user = "Everyone")
        {
            DirectoryInfo dInfo = new DirectoryInfo(path);

            IdentityReference everybodyIdentity = new SecurityIdentifier(WellKnownSidType.WorldSid, null);

            FileSystemAccessRule rule = new FileSystemAccessRule(
                everybodyIdentity,
                FileSystemRights.FullControl,
                InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                PropagationFlags.None,
                AccessControlType.Allow);
            DirectorySecurity ds = Directory.GetAccessControl(path);
            ds.AddAccessRule(rule);
            Directory.SetAccessControl(path, ds);

            return ds;
        }

        static void grant_full_controll(DirectoryInfo dInfo)
        {
            // Copy the DirectorySecurity to the current directory
            //dInfo.SetAccessControl(dSecurity);

            foreach (FileInfo fi in dInfo.GetFiles())
            {
                // Get the file's FileSecurity
                var ac = fi.GetAccessControl();

                // inherit from the directory
                ac.SetAccessRuleProtection(false, false);

                ac.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, AccessControlType.Allow));

                // apply change
                fi.SetAccessControl(ac);
            }

            // Recurse into Directories
            foreach (var d in dInfo.GetDirectories())
            {
                /*
                DirectorySecurity dSecurity = d.GetAccessControl();
                dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                dInfo.SetAccessControl(dSecurity);
                */

                GetDirectoryAccessControll(d.FullName);

                grant_full_controll(d);
            }
        }

        //---------------------------------------------------------------------------------
        /// <summary>
        /// <para>Returns true if a file of a folder exists in the specified path.</para>
        /// </summary>
        /// <param name="path">Full path for a file or a folder.</param>
        public static bool IsFileOrDirectoryExists(string path)
		{
			return(File.Exists(path) || Directory.Exists(path));
		}

//---------------------------------------------------------------------------------
		/// <summary>
		/// <para>Returns true if a file of a folder exists in the specified folder.</para>
		/// </summary>
		/// <param name="path">Full path for a folder.</param>
		public static bool IsFileOrDirectoryExistsIn(string path)
		{
			bool ans = false;

			string[] files = Directory.GetFiles(path);
			string[] dirs = Directory.GetDirectories(path);

			if((0 < files.Count()) || (0 < dirs.Count()))
				ans = true;

			return ans;
		}

//---------------------------------------------------------------------------------
		public static string GetFileSizeInKB(string path)
		{
			FileInfo vFile = new FileInfo(path);
			int Len = ((int)vFile.Length / 1024);
			string size = Len.ToString();

			return size;
		}

//---------------------------------------------------------------------------------
		/// <summary>
		/// <para>Create a shortcut file.</para>
		/// </summary>
		/// <param name="save_path">Path to save.</param>
		/// <param name="target">Path to assign to this short cut file.</param>
		/// <param name="args">Arguments which are passed to the target when this short cut is opened.</param>
		/// <param name="description">Description which is saved into this short cut file.</param>
		public static void CreateShortcut(string save_path, string target, string args = "", string description = "")
		{
			WshShell shell = new WshShell();

			IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(save_path);
			shortcut.TargetPath = target;
			shortcut.WorkingDirectory = target.Replace(Path.GetFileName(target), "");
			shortcut.Arguments = args;
			shortcut.Description = description;
			shortcut.Save();
		}

//---------------------------------------------------------------------------------
		/// <summary>
		/// <para>Create Internet shortcut file.</para>
		/// </summary>
		public static void CreateInternetShortcut(string path, string name, string url)
		{
			using(StreamWriter writer = new StreamWriter(path + name + ".url"))
			{
				writer.WriteLine("[InternetShortcut]");
				writer.WriteLine("URL=" + url);
				writer.Flush();
			}
		}

        /// <summary>
        /// Get Files from paths
        /// </summary>
        /// <param name="paths">return files full path</param>
        public static List<string> GetFilesByPath(string[] paths)
        {
            string[] args = paths;
            List<string> pathFileWindows = new List<string>();

            for (int i = 0; i < args.Length; i++)
            {
                if (Directory.Exists(args[i].ToString())) // directory?
                {
                    //get all files from the directory
                    DirectoryInfo pathFiles = new DirectoryInfo(args[i]);
                    FileInfo[] files = pathFiles.GetFiles("*.*", SearchOption.AllDirectories);

                    foreach (FileInfo info in files)
                        pathFileWindows.Add(info.FullName);

                }
                else // file
                {
                    pathFileWindows.Add(args[i].ToString());
                }
            }
            return pathFileWindows;
        }

        //---------------------------------------------------------------------------------
    }
}
