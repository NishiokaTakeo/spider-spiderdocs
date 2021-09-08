using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using SpiderDocsForms;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;
using NLog;
using System.Threading;
using System.Linq;

//---------------------------------------------------------------------------------
namespace SpiderDocs
{
    static class Program
	{
		static List<string> pathFileWindows = new List<string>();
        static Logger logger = LogManager.GetCurrentClassLogger();

		[STAThread]
		static void Main(string[] args)
		{
            logger.Debug("Launch SD with parameters:{0} ", args == null ? "" : string.Join(",", args));

            try
            {
                SpiderDocsApplication.LoadAllSettings();
                logger.Trace("Done SpiderDocsApplication.LoadAllSettings");

                Setup(args);

                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-AU");
                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                MessageBox.Show(lib.msg_error_default + lib.msg_error_description + ex.ToString(), lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AppDomain currentDomain = AppDomain.CurrentDomain;
			currentDomain.UnhandledException += new UnhandledExceptionEventHandler(GlobalErrorCatchHandler);

			Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

                if (0 < args.Length)
				{
					switch(args[0])
					{
                        // Send to database ( Send To )
					    case "savefile":
						    ParsePaths(args);
						    if(0 < pathFileWindows.Count)
						    {
							    saveFileDialog();

								MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.SendTo);
						    }
						    break;

                        // Send to workspace ( Send To )
					    case "workspace":
						    ParsePaths(args);
						    if(0 < pathFileWindows.Count)
						    {
                                workspace();
							    MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.WorkSpaceUpdateCount);
						    }
						    break;

					    case "minimized":
						    minimized();
						    break;

					    case "dmsfile":
						    ParsePaths(args);
						    if(0 < pathFileWindows.Count)
						    {
							    OpenDmsFile();
						    }
						    break;

					    case "restart":
						    if(args.Length == 2)
							    Restart(args[1]);
						    else
							    Restart("");
						    break;

					    case "database":
						    if(args.Length == 5)
						    {
								frmSplashScreen frm = new frmSplashScreen();
								frm.UpdateServerSetting(new ServerSettings{server = args[1],port = Convert.ToInt32(args[2]),localDb = false});
								frm.UpdateUserSettings(new UserSettings{userName = args[3],pass = new Crypt().Encrypt(args[4])});

							    Application.Run(frm);
						    }
						    break;

					    case "localdb":
						    if(args.Length == 1)
						    {
							    SpiderDocsApplication.CurrentServerSettings.localDb = true;

							    Application.Run(new frmSplashScreen());
						    }
						    break;
					}

				}else if(!SpiderDocsApplication.IsAnotherInstance())
				{
                    logger.Trace("Done SpiderDocsApplication.IsAnotherInstance");

                    //lunch Spider docs
                    Application.Run(new frmSplashScreen());
				}
			}
			catch(Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default + lib.msg_error_description + error.ToString(), lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

            logger.Trace("[End]");
		}

        private static void Setup(string[] args)
        {
            logger.Trace("Begin");

			if(args.Length > 0 && args[0] == "restart")
			{
				Cache.RemoveAll();
				return;
			}

			// Pre load
			SpiderDocsApplication.CurrentServerSettings.LoadFromRegistory();

			ApplicationSettings setting = new ApplicationSettings();

			var enabledMultiAddress = setting.PreviousMultiAddress;
			if (false == enabledMultiAddress)
			{
				SpiderDocsApplication.CurrentServerSettings.server = setting.UpdateServer;
				SpiderDocsApplication.CurrentServerSettings.port = setting.UpdateServerPort;
				SpiderDocsApplication.CurrentUserSettings.offline = setting.offline;
				SpiderDocsApplication.CurrentUserSettings.autoConnect = setting.autoConnect;
				SpiderDocsApplication.CurrentServerSettings.Save();
			}
			else
			{
				// load as usuall
				SpiderDocsApplication.CurrentServerSettings.server = setting.SelectedServer;
				SpiderDocsApplication.CurrentServerSettings.port = setting.SelectedPort;
				SpiderDocsApplication.CurrentUserSettings.offline = setting.SelectedOffline;
				SpiderDocsApplication.CurrentUserSettings.autoConnect = setting.SelectedAutoConnect;
				SpiderDocsApplication.CurrentServerSettings.Save();
			}

			SpiderDocsApplication.CurrentUserSettings.Load();

            if(!String.IsNullOrEmpty(SpiderDocsApplication.CurrentServerSettings.server)
				  && (0 < SpiderDocsApplication.CurrentServerSettings.port))
			{
				if(SpiderDocsApplication.CurrentServerSettings.localDb || !SpiderDocsApplication.CurrentUserSettings.offline)
				{
					SpiderDocsServer server = new SpiderDocsServer(SpiderDocsApplication.CurrentServerSettings);
					server.onConnected += onConnectedServer;
					server.onConnectionErr += ()=> {};
					server.Connect();
				}
			}

			FileFolder.CreateAppFolder();

            string TempPath = FileFolder.GetTempFolder();

            if (!Directory.Exists(TempPath))
                Directory.CreateDirectory(TempPath);

            logger.Trace("End");
        }

		static void onConnectedServer(ServerSettings ServerSettings, bool ConnectionChk)
		{
            logger.Trace("Begin");

			if( !ConnectionChk ) return;
			if( SpiderDocsApplication.CheckCurrentVersionState() != CurrentVersionState.Latest) return;
			if( !SpiderDocsApplication.CurrentUserSettings.autoLogin ) return;
			UserSettings us = SpiderDocsApplication.CurrentUserSettings;

			int userId = UserController.LoginActive(us.userName,  us.pass);
			if( userId == 0 ) return;

            new SpiderDocsModule.Cache(userId).Create();
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
			logger.Error(e.Exception);
        }

        private static void GlobalErrorCatchHandler(object sender, UnhandledExceptionEventArgs e)
        {
			Exception ex = (Exception) e.ExceptionObject;
			logger.Error(ex);
		}

        //---------------------------------------------------------------------------------
        static void saveFileDialog()
		{
			logger.Trace("Begin");
			if(SpiderDocsApplication.IsLoggedIn)
				copyExternalFileToSendTo(pathFileWindows);
			else
				MessageBox.Show(lib.msg_error_SpiderDoc_not_open, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

//---------------------------------------------------------------------------------
		static void workspace()
		{
			logger.Trace("Begin");

			if(SpiderDocsApplication.IsLoggedIn)
			{
 				var sync = new Cache(SpiderDocsApplication.CurrentUserId).GetSyncMgr(FileFolder.GetUserFolder());

				sync.Stop();
                copyExternalFileToWorkSpace(pathFileWindows).Wait();
				sync.Start();
			}
			else
				MessageBox.Show(lib.msg_error_SpiderDoc_not_open, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

//---------------------------------------------------------------------------------
		static async System.Threading.Tasks.Task<string[]> copyExternalFileToWorkSpace(List<string> origPath)
		{
            var syncLogger= LogManager.GetLogger("Sync");
			logger.Trace("Begin");

            string[] paths = new string[] { } ;

			try
			{
                System.Threading.Tasks.Task<string>[] tasks = new System.Threading.Tasks.Task<string>[origPath.Count];

                for (int i = 0; i< origPath.Count; i++)
                {
                    tasks[i] = Utilities.CopyToWorkSpace(origPath[i], false);

                    syncLogger.Debug("Task is added for {0}", origPath[i]);
                }

                syncLogger.Debug("Task is waiting");

                paths = await System.Threading.Tasks.Task.WhenAll(tasks);

                syncLogger.Debug("Task is completed");
            }
			catch(IOException error)
			{
				logger.Error(error);
			}

            logger.Trace("[End]");

            return paths;
        }
		/// <summary>
		/// Copy files for sendto
		/// </summary>
		/// <param name="origPath"></param>
		static void copyExternalFileToSendTo(List<string> origPath)
		{
			logger.Trace("Begin:{0}", string.Join(",",origPath));

			try
			{
				string root = string.Format("{0}{1}\\",FileFolder.SendToPendingPath,DateTime.Now.ToString("yyyyMMddhhmmss"));

                logger.Debug("Create a folder: {0}", root);
                FileFolder.CreateFolder(root,true);

				foreach(string wrk in origPath)
				{
					string newname = FileFolder.GetAvailableFileName(root + Path.GetFileName(wrk));

                    logger.Debug("copy {0} to {1}", wrk, newname);

					File.Copy(wrk, newname,true);
				}
			}
			catch(IOException error)
			{
				logger.Error(error);
			}

            logger.Trace("End");
        }

//---------------------------------------------------------------------------------
		static void minimized()
		{
			logger.Trace("Begin");
			frmSplashScreen frmSplashScreen = new frmSplashScreen(minimized: true);
			Application.Run(frmSplashScreen);
		}

//---------------------------------------------------------------------------------
		static void OpenDmsFile()
		{
			logger.Trace("Begin");
			MMF.WriteData<string>(pathFileWindows[0], MMF_Items.DmsFilePath);

			if(!SpiderDocsApplication.IsAnotherInstance())
			{
				//lunch Spider docs
				Application.Run(new frmSplashScreen());
			}
		}

//---------------------------------------------------------------------------------
		static void Restart(string arg)
		{
			logger.Trace("Begin");
			bool logout = false;
			int cnt = 0;

			while(SpiderDocsApplication.IsAnotherInstance())
			{
				if(10 < cnt)
					Application.Exit();

				System.Threading.Thread.Sleep(500);
				cnt++;
			}

			if(arg == "logout")
				logout = true;

			//lunch Spider docs
			Application.Run(new frmSplashScreen(logout: logout));
		}

//---------------------------------------------------------------------------------
		static void ParsePaths(string[] args)
		{
			logger.Trace("Begin");

			for(int i = 1; i < args.Length; i++)
			{
				if(Directory.Exists(args[i].ToString())) // directory?
				{
					//get all files from the directory
					DirectoryInfo pathFiles = new DirectoryInfo(args[1]);
					FileInfo[] files = pathFiles.GetFiles("*.*", SearchOption.AllDirectories);

					foreach(FileInfo info in files)
						pathFileWindows.Add(info.FullName);

				}else // file
				{
					pathFileWindows.Add(args[i].ToString());
				}
			}
		}
//---------------------------------------------------------------------------------
	}
}
