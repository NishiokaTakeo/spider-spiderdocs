using System;
using Microsoft.Win32;
using System.IO;
using NLog;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpiderDocsModule
{
	public enum CurrentVersionState
	{
		Latest,
		Older,
		Newer
	}
	public class SpiderDocsApplication
	{
		public static readonly string RegistryPath = @"SOFTWARE\SpiderDocs";
		public static readonly string ServiceClientDetailsRegistryPath = @"SOFTWARE\SpiderDocs\clientDetails";
		public static readonly string ServicePortDbRegistryPath = @"SOFTWARE\SpiderDocs\PortDb";

 		static Logger logger = LogManager.GetCurrentClassLogger();

		// The following settings are static as only one user and one server can be usable in SpiderDocsForms layer.

		// Need to be loaded at first. Contains server address and so on.
		public static ServerSettings CurrentServerSettings = new ServerSettings();

		// Second load. Loaded when server connection is succeeded.
		public static PublicSettings CurrentPublicSettings;
		public static MailSettingss CurrentMailSettings;

        // Third load. Loads Registry values for user account info when available. If not, a user must enter from the dialog.
        public static UserSettings CurrentUserSettings = SpiderDocsModule.Factory.Instance4UserSettins();
        public static int CurrentUserId { get { return MMF.ReadData<int>(MMF_Items.UserId); } }
		public static string CurrentUserName { get { return CurrentUserSettings.userName; } }

		// Fourth load after user login is succeeded.
		public static UserGlobalSettings CurrentUserGlobalSettings;
		public static cl_WorkspaceGridsize WorkspaceGridsizeSettings;
		public static cl_WorkspaceCustomize WorkspaceCustomize;

//---------------------------------------------------------------------------------
		// This function will work only when a user already logged in.
		public static void LoadAllSettings()
		{
			SqlOperation.MethodToGetDbManager = new Func<DbManager>(() =>
			{
				return new DbManager(SpiderDocsApplication.CurrentServerSettings.conn, SpiderDocsApplication.CurrentServerSettings.svmode);
			});

			if(SpiderDocsApplication.IsLoggedIn)
			{
				SpiderDocsApplication.CurrentServerSettings = new ServerSettings();
				SpiderDocsApplication.CurrentServerSettings.LoadFromRegistory();

				SpiderDocsApplication.CurrentPublicSettings = new PublicSettings();
				SpiderDocsApplication.CurrentPublicSettings.Load();

				SpiderDocsApplication.CurrentMailSettings = new MailSettingss();
				SpiderDocsApplication.CurrentMailSettings.Load();

                SpiderDocsApplication.CurrentUserSettings = SpiderDocsModule.Factory.Instance4UserSettins();// new UserSettings();

				int user_id = MMF.ReadData<int>(MMF_Items.UserId);
				SpiderDocsApplication.CurrentUserGlobalSettings = new UserGlobalSettings(user_id);
				SpiderDocsApplication.CurrentUserGlobalSettings.Load();
				SpiderDocsApplication.WorkspaceGridsizeSettings = new cl_WorkspaceGridsize(user_id);
                SpiderDocsApplication.WorkspaceGridsizeSettings.Load();
                SpiderDocsApplication.WorkspaceCustomize = new cl_WorkspaceCustomize(user_id);
                SpiderDocsApplication.WorkspaceCustomize.Load();
            }
		}

//---------------------------------------------------------------------------------
		// This function will work only when a user already logged in.
		public static void SaveAllSettings()
		{
			logger.Trace("Begin");

			if(SpiderDocsApplication.IsLoggedIn)
			{
				if(SpiderDocsApplication.CurrentServerSettings != null)
					SpiderDocsApplication.CurrentServerSettings.Save();

				// Public settings should not be saved from client side.

				// Mail settings should not be saved from client side.

				if(SpiderDocsApplication.CurrentUserSettings != null)
					SpiderDocsApplication.CurrentUserSettings.Save();

				if(SpiderDocsApplication.CurrentUserGlobalSettings != null)
					SpiderDocsApplication.CurrentUserGlobalSettings.Save();

				if(SpiderDocsApplication.WorkspaceGridsizeSettings != null)
					SpiderDocsApplication.WorkspaceGridsizeSettings.Save();

				if(SpiderDocsApplication.WorkspaceCustomize != null)
					SpiderDocsApplication.WorkspaceCustomize.Save();
			}
		}

//---------------------------------------------------------------------------------
		public static bool IsLoggedIn
		{
			get
			{
				//----- @@Mori: This is workaround to skip login check below.
				// In some PCs, this IsLoggedIn does not work properly somehow.
				// In this case, the Office add-ins cannot recognize if a current user is logged-in or not and
				// it always considers "not logged-in".
				// To avoid this problem, add a DWORD value named "debug_always_logged_in" in HKCU and set 1.
				// It allows to skip login check below and return always true (menas always logged in).
				if((CurrentUserSettings != null) && CurrentUserSettings.debug_always_logged_in)
					return true;
				//----- until here

				if(0 < MMF.ReadData<int>(MMF_Items.UserId))
					return true;
				else
					return false;
			}
		}

//---------------------------------------------------------------------------------
		public static bool IsAnotherInstance()
		{
			logger.Trace("Begin");

			bool ans = false;

			try
			{
				ans = SpiderDocsModule.Utilities.IsAnotherInstance((IntPtr)MMF.ReadData<int>(MMF_Items.WindowHandle));

			}catch(Exception error)
			{
				logger.Error(error);
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public static string GetAppVersion()
		{
			logger.Trace("Begin");

			string ans = "";

			using(RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(SpiderDocsApplication.RegistryPath))
			{
				if(registryKey != null)
				{
					try{ ans = registryKey.GetValue("Version").ToString(); } catch{}
					registryKey.Close();
				}
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public static CurrentVersionState CheckCurrentVersionState()
		{
			logger.Trace("Begin");

			CurrentVersionState ans = CurrentVersionState.Latest;

			string[] sversion = Cache.getSystemVersion().Split('.');
			string[] cversion = GetAppVersion().Split('.');

			for(int i = 0 ; i < 3; i++){

				if( Convert.ToInt32(sversion[i]) < Convert.ToInt32(cversion[i]))
				{
					ans = CurrentVersionState.Newer;
					break;
				}
				else if( Convert.ToInt32(sversion[i]) > Convert.ToInt32(cversion[i]))
				{
					ans = CurrentVersionState.Older;
					break;
				}
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public static bool IsExcel { get { return (Type.GetTypeFromProgID("Excel.Application") != null); } }
		public static bool IsWord { get { return (Type.GetTypeFromProgID("Word.Application") != null); } }
		public static bool IsOutlook { get { return (Type.GetTypeFromProgID("Outlook.Application") != null); } }
		public static bool IsPowPnt { get { return (Type.GetTypeFromProgID("PowerPoint.Application") != null); } }

        //---------------------------------------------------------------------------------
        public static RegistryKey get32BitRegKey()
        {
            return RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);
        }

        //---------------------------------------------------------------------------------
        public static RegistryKey get64BitRegKey()
        {
            return RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
        }
        //public static RegistryKey get32BitRegCUKey()
        //{
        //    return RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Registry32);
        //}
//
        ////---------------------------------------------------------------------------------
        //public static RegistryKey get64BitRegCUKey()
        //{
        //    return RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Registry64);
        //}

        public static void AddInstallationInfo(string version_no)
        {
            RegistryKey regkey;

            regkey = get32BitRegKey();
            AddInstallationInfo(regkey, version_no);

            regkey = get64BitRegKey();
            AddInstallationInfo(regkey, version_no);
        }

        //---------------------------------------------------------------------------------
        static void AddInstallationInfo(RegistryKey regkey, string version_no)
        {
			logger.Trace("Begin");

            string program_files_folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) + "\\Spider Docs\\";
            try
            {
                regkey = regkey.CreateSubKey(@"Software\SpiderDocs\");

                regkey.SetValue("SpiderDocsPath", program_files_folder + "SpiderDocs.exe", RegistryValueKind.String);
                regkey.SetValue("Version", version_no, RegistryValueKind.String);

                regkey.Close();
            }
            catch { }
        }


	}

    public class ApplicationSettings
    {
		public static string CONFIG_FILENAME = "setting.json";

		public string SpiderDocsPath = string.Empty;

        public bool localDb { get; set; } = false;

        // Server 1
        public bool UpdateServerChecked = true;
        public string UpdateServer = string.Empty;
        public int UpdateServerPort = 5322;
        public bool offline { get; set; } = false;
        public bool autoConnect { get; set; } = false;

        // Server 2
        public bool UpdateServer2Checked = false;
        public string UpdateServer2 { get; set; } = "";
        public int UpdateServerPort2 { get; set; } = 5322;
        public bool offline2 { get; set; } = false;
        public bool autoConnect2 { get; set; } = false;

		public bool PreviousMultiAddress {get;set;} = false;

		public string SelectedServer
		{
			get
			{
				return UpdateServerChecked ? UpdateServer : UpdateServer2;
			}
		}

		public int SelectedPort
		{
			get
			{
				try
				{
					return Convert.ToInt32(UpdateServerChecked ? UpdateServerPort : UpdateServerPort2);
				}
				catch
				{
					var def = new ApplicationSettings();
					return UpdateServerChecked ? def.UpdateServerPort : def.UpdateServerPort2;
				}

			}
		}

		public bool SelectedOffline
		{
			get
			{
				return UpdateServerChecked ? offline : offline2;
			}
		}

		public bool SelectedAutoConnect
		{
			get
			{
				return UpdateServerChecked ? autoConnect : autoConnect2;
			}
		}

		static Logger logger = LogManager.GetCurrentClassLogger();

        public ApplicationSettings()
		{
            if (!FileFolder.IsFileOrDirectoryExists(ConfigPath()))
            {
                SaveAsJsonIfNotExist();
            }
            LoadFromJson();
        }

        /// <summary>
        /// Get Install Path
        /// </summary>
        /// <returns></returns>
        public static string GetInstallPath()
		{
			string ans = "C:\\Program Files (x86)\\Spider Docs\\Spider Docs.exe";

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

        public string SaveAsJson()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(this, Formatting.Indented);

            File.WriteAllText(ConfigPath(), json);

            return json;
        }

        void LoadFromJson()
        {

            try
            //DEBUG:BTPERTS02, SSBIM026, SSBIM027 are thorughing an error here.
            {

                string stringly = File.ReadAllText(ConfigPath());

                if (string.IsNullOrWhiteSpace(stringly) || !FileFolder.IsJson(stringly))
                {
                    stringly = SaveAsJsonIfNotExist();
                }

                JObject json = Newtonsoft.Json.Linq.JObject.Parse(stringly);

                this.SpiderDocsPath = (string) json["SpiderDocsPath"];
                this.localDb = (bool) (json["localDb"] ?? SpiderDocsApplication.CurrentServerSettings.localDb);

				// Server 1
				this.UpdateServerChecked = (bool) (json["UpdateServerChecked"] ?? this.UpdateServerChecked);
                this.UpdateServer = (string) (json["UpdateServer"] ?? SpiderDocsApplication.CurrentServerSettings.server);
			    this.UpdateServerPort = int.Parse((string)(json["UpdateServerPort"] ?? this.UpdateServerPort));
                this.offline = (bool) (json["offline"] ?? SpiderDocsApplication.CurrentUserSettings.offline);
                this.autoConnect = (bool) (json["autoConnect"] ?? SpiderDocsApplication.CurrentUserSettings.autoConnect);
                // Server 2
                this.UpdateServer2Checked = (bool) (json["UpdateServer2Checked"] ?? this.UpdateServer2Checked);
                this.UpdateServer2 = (string) (json["UpdateServer2"] ?? this.UpdateServer2);
                this.UpdateServerPort2 = (int) (json["UpdateServerPort2"] ?? this.UpdateServerPort2);
                this.offline2 = (bool) (json["offline2"] ?? this.offline2);
                this.autoConnect2 = (bool) (json["autoConnect2"] ?? this.autoConnect2);

				this.PreviousMultiAddress = (bool) (json["PreviousMultiAddress"] ?? this.PreviousMultiAddress);


            }
            catch (Exception ex)
            {
                logger.Error(ex,"Error:{0}, {1}", ConfigPath(), File.ReadAllText(ConfigPath(), System.Text.Encoding.ASCII).Replace("\n","").Replace("\r",""));
                throw ex;
            }
        }

        /// <summary>
        /// Get Root folder that involved spiderdocs.exe
        /// </summary>
        /// <returns></returns>
		static string GetRootDir()
        {

            string exe = GetInstallPath();
            return FileFolder.GetPath(exe);
        }

        /// <summary>
        /// Get configration path
        /// </summary>
        /// <returns></returns>
        public static string ConfigPath()
        {
            return GetRootDir() + CONFIG_FILENAME;
        }

		/// <summary>
		/// create setting.json if it isn't existing.
		/// </summary>
		string SaveAsJsonIfNotExist()
		{
			PublicSettings settingPublic = new PublicSettings();

			ServerSettings setting = new ServerSettings();
			setting.LoadFromRegistory();

            var userSettings = SpiderDocsModule.Factory.Instance4UserSettins();

            this.SpiderDocsPath = GetInstallPath(); //ConfigPath();
            this.localDb = setting.localDb;

			this.UpdateServer = setting.server.Trim();
			this.UpdateServerPort = setting.port;
            this.offline = userSettings.offline;
            this.autoConnect = userSettings.autoConnect;

			this.PreviousMultiAddress = settingPublic.feature_multiaddress;

			//
			// please write update if you add new property
			//

			// Server2 and Port2 (remote servers) support only new way.

			// EnabledMultiaddress use default.

			return SaveAsJson();
		}

        /// <summary>
        /// copy values from registory to setting.json
        /// </summary>
		public void MigrateIfDefault()
        {
            if (this.UpdateServer.Trim() == "_noset_")
            {
                SaveAsJsonIfNotExist();
            }
        }

		public bool Valid()
		{
			if (this.UpdateServer.Trim() == "_noset_") return false;

			return true;
		}
    }





}
