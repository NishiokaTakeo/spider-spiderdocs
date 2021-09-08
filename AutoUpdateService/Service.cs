using System;
using System.IO;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using SpiderDocsModule;
using NLog;
using System.Runtime.InteropServices;

//---------------------------------------------------------------------------------
namespace AutoUpdateService
{
    public class Service : ServiceBase
	{
		System.Timers.Timer timer;
		int interval = 30000;

        //SpiderDocsApplication.Logger _logger = new SpiderDocsApplication.Logger("----- UPDATA Start -----");
        static Logger logger = LogManager.GetLogger("Updater");

        [Flags]
        enum MoveFileFlags
        {

            MOVEFILE_REPLACE_EXISTING = 0x00000001,

            MOVEFILE_COPY_ALLOWED = 0x00000002,

            MOVEFILE_DELAY_UNTIL_REBOOT = 0x00000004,

            MOVEFILE_WRITE_THROUGH = 0x00000008,

            MOVEFILE_CREATE_HARDLINK = 0x00000010,

            MOVEFILE_FAIL_IF_NOT_TRACKABLE = 0x00000020
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName, MoveFileFlags dwFlags);

        //---------------------------------------------------------------------------------
        public Service()
		{
            logger.Info("Service is started");

			SqlOperation.MethodToGetDbManager = new Func<DbManager>(() =>
			{
				return new DbManager(SpiderDocsApplication.CurrentServerSettings.conn, SpiderDocsApplication.CurrentServerSettings.svmode);
			});

			timer = new System.Timers.Timer(interval);
			timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
			timer.AutoReset = true;
		}

//---------------------------------------------------------------------------------
		protected override void OnStart(string[] args)
		{
            logger.Info("Service is started");
            timer.Enabled = true;
		}
        protected override void OnStop()
        {
            logger.Info("Service is stopped");

            base.OnStop();
        }
//---------------------------------------------------------------------------------
		void TimerElapsed(object sender, ElapsedEventArgs e)
		{
            logger.Trace("Begin");

            ApplicationSettings appsetting = null;
            ServerSettings ServerSettings = null;

            try
            {
                appsetting = new ApplicationSettings();
                ServerSettings = new ServerSettings();

                if( appsetting.Valid() )
                {

                    // override information.
                    ServerSettings.server = appsetting.UpdateServer;
                    ServerSettings.port = appsetting.UpdateServerPort;

                    SpiderDocsServer server = new SpiderDocsServer(ServerSettings);
                    server.onConnected += server_onConnected;
                    server.onConnectionErr += server_onConnectionErr;

                    if (!server.Connect())
                    {
                        logger.Info("Connection failed");
                        logger.Debug("server:{0}, port:{1}",ServerSettings.server,ServerSettings.port);

                        timer.Interval = ( timer.Interval > 1000 * 60 * 5 ) ? timer.Interval : timer.Interval * 2; // Next check will be double when it faield.
                    }
                }
                else
                {
                    appsetting.MigrateIfDefault();

                    logger.Warn("ApplicationSettings isn't valid: server:{0}, port:{1}",appsetting.UpdateServer,appsetting.UpdateServerPort);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex,"{0}",Newtonsoft.Json.JsonConvert.SerializeObject(ServerSettings));
            }

            logger.Trace("End");
        }

        void StopService()
        {
            logger.Trace("Begin");

            timer.Stop();
            timer.Enabled = false;
        }

        void StartService()
        {
            logger.Trace("Begin");

            timer.Start();
            timer.Enabled = true;
        }

        //---------------------------------------------------------------------------------
        void server_onConnectionErr()
		{
            logger.Trace("Begin");

            StartService();
        }

        //---------------------------------------------------------------------------------
        void server_onConnected(SpiderDocsModule.ServerSettings ServerSettings, bool ConnectionChk)
		{
            logger.Trace("Begin");
            string indvVersion = string.Empty;
            try
            {
                Cache.Remove(Cache.en_GKeys.DB_getSystemVersion);

                StopService();

                SpiderDocsApplication.CurrentServerSettings = ServerSettings;

                logger.Trace("Check version Connected:{0}, Old?:{1}, AppClosed?:{2}, OfficeClosed?:{3}", ConnectionChk, SpiderDocsApplication.CheckCurrentVersionState() == CurrentVersionState.Older, !IsAppRunning(), !IsOfficeRunning());

                // Try update if connection is okay, closed spider docs and closed office then
                if( true == ConnectionChk && false == IsAppRunning() && false == IsOfficeRunning() )
                {
                    if( SpiderDocsApplication.CheckCurrentVersionState() == CurrentVersionState.Older )
                    {
                        logger.Info("New version found at {0}:{1}", SpiderDocsApplication.CurrentServerSettings.server, SpiderDocsApplication.CurrentServerSettings.port);

                        UpdateToNewVer();
                    }

                    // Has individual version
                    else if( SpiderDocsCoer.GetIndividualUpdateIsNecesarry(Environment.MachineName) )
                    {
                        logger.Info("Individual version found at {0}:{1}:{2}", SpiderDocsApplication.CurrentServerSettings.server, SpiderDocsApplication.CurrentServerSettings.port, Environment.MachineName);

                        UpdateToNewVer();

                        IndividualUpdateDone(Environment.MachineName);
                    }
                }

			}
            catch (Exception ex)
            {
                logger.Error(ex);
            }
			finally
			{
                timer.Interval = interval;
                StartService();
            }
		}

//---------------------------------------------------------------------------------
		public void UpdateToNewVer() // null means latest
		{
            logger.Trace("Begin");

            string mUpdatePath, mInstalationLogPath;

            string update_path = FileFolder.CreateUpdateFolder();

            remove_files_now(update_path);

            //download files of new version
            Array fileData = SpiderDocsModule.SpiderDocsCoer.getUpdateFile();

            using (FileStream fs = new FileStream(update_path + "setup.zip", FileMode.OpenOrCreate, FileAccess.Write))
                fs.Write((byte[])fileData, 0, fileData.Length);

            logger.Info(string.Format("Downloaded to: \"{0}\"", update_path + "setup.zip"));

            //Debug files
			using(var zip = Ionic.Zip.ZipFile.Read(update_path + "setup.zip"))
			{
				zip.ExtractAll(update_path);
			}

            logger.Info("Extracted successfully");

			//save update log
			string serverVersion = SpiderDocsModule.SpiderDocsCoer.getSystemVersion().Replace(".", "");
			string clientVersion = SpiderDocsApplication.GetAppVersion().Replace(".", "");

            logger.Info(string.Format("Updating {0} from {1}", serverVersion, clientVersion));

			mUpdatePath = update_path + "setup.exe";
			mInstalationLogPath = FileFolder.GetShortPathName(FileFolder.GetExecutePath()) + "installationLog.log";

            start_setupexe(mUpdatePath, mInstalationLogPath);

            remove_files_later(update_path);

        }

        static void IndividualUpdateDone(string nameComputer)
        {
           SpiderDocsCoer.DeleteIndividualUpdateIsNecesarry(nameComputer);
        }

		static bool IsAppRunning()
		{
            logger.Trace("Begin");
            foreach (var process in Process.GetProcesses())
			{
				if(process.ProcessName.ToLower() == "spiderdocs")
				{
					return true;
				}
			}

			return false;
		}

		static bool IsOfficeRunning()
		{
            logger.Trace("Begin");
            foreach (var process in Process.GetProcesses())
			{
				if((process.ProcessName.ToLower() == "winword")
				|| (process.ProcessName.ToLower() == "excel")
				//|| (process.ProcessName.ToLower() == "powerpnt")
				|| (process.ProcessName.ToLower() == "outlook"))
				{
					return true;
				}
			}

			return false;
		}

        static void start_setupexe(string installer_path, string log_path)
        {
            logger.Trace("Begin");

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = installer_path;

            startInfo.Arguments = "/S /s /v/qn /v/log /v" + log_path;

            // To get administrator right
            startInfo.Verb = "runas";

            //start installation process
            process.StartInfo = startInfo;
            process.Start();

            logger.Info("Launch NSIS: {0} , {1}", startInfo.FileName, startInfo.Arguments);
        }

        static void remove_files_now(string dir)
        {
            logger.Trace("Begin");

            try
            {
                string[] fileEntries = Directory.GetFiles(dir);
                foreach (string fileName in fileEntries)
                    File.Delete(fileName);

                logger.Info("All previous installer files are removed successfully");
            }
            catch(Exception ex)
            {
                logger.Error(ex,"Fiald to remove file");
            }
        }



        static void remove_files_later(string dir)
        {
            logger.Trace("Begin");

            string[] fileEntries = Directory.GetFiles(dir);
            foreach (string fileName in fileEntries)
                MoveFileEx(fileName, null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);

            logger.Info("Schedulled that removes all files at next reboot");
        }



    }
}
