using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using lib = SpiderDocsModule.Library;
using SpiderDocsModule;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsForms
{
	public class Utilities : SpiderDocsModule.Utilities
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

//---------------------------------------------------------------------------------
		[DllImport("kernel32.dll")]
		public static extern uint GetTickCount();

//---------------------------------------------------------------------------------
		public static bool CheckSavePath(string SavePath)
		{
			bool ans = false;

			if((SavePath == "") || !Directory.Exists(SavePath))
				MessageBox.Show(lib.msg_required_location, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			else
				ans = true;

			return ans;
		}

//---------------------------------------------------------------------------------
		public static System.Threading.Tasks.Task<string> CopyToWorkSpace(string origPath, bool delete_original_file)
		{

            System.Threading.Tasks.Task<string> task = null;

            try
			{
				var wFile = new WorkSpaceFile(origPath, FileFolder.GetUserFolder());
				string shortHash = wFile.GetVerZeroFolderName();

				//check file name
				string workSpacePath = FileFolder.GetAvailableFileName(FileFolder.GetUserFolder() + shortHash + "\\"+ Path.GetFileName(origPath));

				if(delete_original_file)
					File.Move(origPath, workSpacePath);
				else
					File.Copy(origPath, workSpacePath);

                task = System.Threading.Tasks.Task.Run(() =>
                {

                    new Cache(SpiderDocsApplication.CurrentUserId).GetSyncMgr(FileFolder.GetUserFolder()).InitSync(new WorkSpaceFile(workSpacePath, FileFolder.GetUserFolder()));

                    return workSpacePath;
                });

                MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.WorkSpaceUpdateCount);

			}
			catch(IOException error)
			{
				logger.Error(error);
			}

            return task;

        }

//---------------------------------------------------------------------------------
		static public bool IsOfficeProcessExists()
		{
			bool ans = false;

			foreach(var process in Process.GetProcesses())
			{
				if((process.ProcessName.ToLower() == "winword")
				|| (process.ProcessName.ToLower() == "excel")
				|| (process.ProcessName.ToLower() == "powerpnt")
				|| (process.ProcessName.ToLower() == "outlook"))
				{
					ans = true;
					break;
				}
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		static public bool IsSpiderDocsProcessExists()
		{
			bool ans = false;

			foreach(var process in Process.GetProcesses())
			{
				if(process.ProcessName.ToLower() == "spiderdocs")
				{
					ans = true;
					break;
				}
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		static public string CreateExternalLink(int id_doc)
		{
			return CreateExternalLink(SpiderDocsApplication.CurrentPublicSettings.webService_address, id_doc);
		}

//---------------------------------------------------------------------------------
	}
}
