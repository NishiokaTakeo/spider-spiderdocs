using System;
using System.Linq;
using Microsoft.Win32;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	[Serializable]
    [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
    public class UserSettings : JSONSettings<UserSettings>
	{
		const string run_path = @"Software\\Microsoft\\Windows\\CurrentVersion\\Run";

        // Private settings from registry (Per PC)
        [Newtonsoft.Json.JsonProperty]
        public string userName;
        [Newtonsoft.Json.JsonProperty]
        public string pass;
        [Newtonsoft.Json.JsonProperty]
        public bool autoConnect = true;
        [Newtonsoft.Json.JsonProperty]
        public bool autoLogin = true;
        [Newtonsoft.Json.JsonProperty]
        public bool offline = false;
        [Newtonsoft.Json.JsonProperty]
        public bool ocr = true;
        [Newtonsoft.Json.JsonProperty]
        public bool save_pass = true;
        //[Newtonsoft.Json.JsonProperty]
        //public string current_path = !string.IsNullOrEmpty(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)) ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : SpiderDocsModule.FileFolder.GetWorkSpaceFolder();
        [Newtonsoft.Json.JsonProperty]
        public bool debug_always_logged_in = false;

		static string _configFile = SpiderDocsModule.FileFolder.GetWorkSpaceFolder() + "settings.json";

        public bool AutoStartup
		{
			get
			{
				object strValue = null;

				using (RegistryKey key = Registry.CurrentUser.OpenSubKey(run_path))
				{
					if(key != null)
					{
						strValue = key.GetValue("SpiderDocs");
						key.Close();
					}
				}

				return (strValue != null);
			}
			set
			{
				RegistryKey key = Registry.CurrentUser.OpenSubKey(run_path, true);

				if(value)
				{
					string exe_path = FileFolder.GetExecuteFileName();
					key.SetValue("SpiderDocs", exe_path + " " + "minimized");
					key.Close();

				}else
				{
					try{ key.DeleteValue("SpiderDocs", true); } catch{}
				}
			}
		}

        public UserSettings() : base(_configFile)
        {
            logger.Debug("settings.json is located '{0}'", _configFile);

            if (!SpiderDocsModule.FileFolder.IsFileOrDirectoryExists(_configFile))
            {
				logger.Info("Start migration for UserSettings.");

				Migrate();

                logger.Info("End migration for UserSettings.");
            }

            LoadFromJson();

            if (!CheckMigrationResult())
            {
                logger.Info("UserSettings has differences.");

                LoadFromReg();
            }

        }

        public void LoadFromReg()
        {

            // From registry
            using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(SpiderDocsApplication.RegistryPath))
            {
                if (registryKey != null)
                {


                    try { userName = registryKey.GetValue("userName").ToString(); } catch { }
                    try { pass = registryKey.GetValue("pass").ToString(); } catch { }
                    try { autoConnect = Convert.ToBoolean(registryKey.GetValue("autoConnect")); } catch { }
                    try { autoLogin = Convert.ToBoolean(registryKey.GetValue("autoLogin")); } catch { }
                    try { offline = Convert.ToBoolean(registryKey.GetValue("offline")); } catch { }
                    try { ocr = Convert.ToBoolean(registryKey.GetValue("ocr")); } catch { }
                    try { save_pass = Convert.ToBoolean(registryKey.GetValue("save_pass")); }
                    catch { }
                    //try { current_path = !string.IsNullOrEmpty(registryKey.GetValue("current_path")?.ToString()) ? registryKey.GetValue("current_path")?.ToString() : current_path; } catch { }
                    try { debug_always_logged_in = Convert.ToBoolean(registryKey.GetValue("debug_always_logged_in")); } catch { }
                    registryKey.Close();

                    logger.Debug("Settings have been loaded '{0},{1},{2},{3},{4},{5},{6},{7}' from {8}", userName, pass, autoConnect, autoLogin, offline, ocr, save_pass, debug_always_logged_in, SpiderDocsApplication.RegistryPath);
                }
            }
        }

		bool CheckMigrationResult()
		{
			bool hasDif = false;

			using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(SpiderDocsApplication.RegistryPath))
            {
                if (registryKey != null)
                {

                    if (userName != registryKey.GetValue("userName")?.ToString()) { hasDif = true; /*logger.Error("Migration failed. userName:{0}", userName); */};
                    if (pass != registryKey.GetValue("pass")?.ToString()) { hasDif = true; /*logger.Error("Migration failed. pass:{0}", pass);*/ };
                    if (autoConnect != Convert.ToBoolean(registryKey.GetValue("autoConnect"))) { hasDif = true; /*logger.Error("Migration failed. autoConnect:{0}", autoConnect);*/ };
                    if (autoLogin != Convert.ToBoolean(registryKey.GetValue("autoLogin"))) { hasDif = true; /*logger.Error("Migration failed. autoLogin:{0}", autoLogin);*/ };
                    if (offline != Convert.ToBoolean(registryKey.GetValue("offline"))) { hasDif = true; /*logger.Error("Migration failed. offline:{0}", offline);*/ };
                    if (ocr != Convert.ToBoolean(registryKey.GetValue("ocr"))) { hasDif = true; /*logger.Error("Migration failed. ocr:{0}", ocr); */};
                    if (save_pass != Convert.ToBoolean(registryKey.GetValue("save_pass"))) { hasDif = true; /*logger.Error("Migration failed. save_pass:{0}", save_pass);*/ };
                    //if (current_path != registryKey.GetValue("current_path")?.ToString()) { hasDif = true; logger.Error("Migration failed. current_path:{0}", current_path); };
                    if (debug_always_logged_in != Convert.ToBoolean(registryKey.GetValue("debug_always_logged_in"))) { hasDif = true; /*logger.Error("Migration failed. debug_always_logged_in:{0}", debug_always_logged_in);*/ };

                }
            }

			return !hasDif;
		}



        //---------------------------------------------------------------------------------
        public override void Load()
		{
            LoadFromJson();
		}

//---------------------------------------------------------------------------------
		public override void Save()
		{
			SaveAsJson();

			RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(SpiderDocsApplication.RegistryPath);

			registryKey.SetValue("userName", userName, RegistryValueKind.String);
			registryKey.SetValue("pass", pass, RegistryValueKind.String);
			registryKey.SetValue("autoConnect", (autoConnect == true ? 1 : 0), RegistryValueKind.DWord);
			registryKey.SetValue("autoLogin", (autoLogin == true ? 1 : 0), RegistryValueKind.DWord);
			registryKey.SetValue("offline", (offline == true ? 1 : 0), RegistryValueKind.DWord);
			registryKey.SetValue("ocr", (ocr == true ? 1 : 0), RegistryValueKind.DWord);
            registryKey.SetValue("save_pass", (save_pass == true ? 1 : 0), RegistryValueKind.DWord);
			//registryKey.SetValue("current_path", current_path, RegistryValueKind.String);

			registryKey.Close();


        }

        protected override void Migrate()
        {
            LoadFromReg();

            SaveAsJson();
        }

        public override void LoadFromJson()
        {
            try
            {
                Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(System.IO.File.ReadAllText(ConfigPath()));

                userName = (string)json["userName"];
                pass = (string)json["pass"];
                autoConnect = Convert.ToBoolean((string)json["autoConnect"]);
                autoLogin = Convert.ToBoolean((string)json["autoLogin"]);
                offline = Convert.ToBoolean((string)json["offline"]);
                ocr = Convert.ToBoolean((string)json["ocr"]);
                save_pass = Convert.ToBoolean((string)json["save_pass"]);
                //current_path = (string)json["current_path"];
                debug_always_logged_in = Convert.ToBoolean((string)json["debug_always_logged_in"]);

                logger.Debug("Settings have been loaded '{0},{1},{2},{3},{4},{5},{6},{7}' from {8}", userName, pass, autoConnect, autoLogin, offline, ocr, save_pass, debug_always_logged_in, ConfigPath());
            }
            catch(Exception ex)
            {
                logger.Error(ex, System.IO.File.ReadAllText(ConfigPath(), System.Text.Encoding.ASCII).Replace("\r","").Replace("\n","") );
            }
        }
        //---------------------------------------------------------------------------------
    }
}

    [Serializable]
    [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
    public abstract class JSONSettings<T>  where T : new()
    {
		static protected Logger logger = LogManager.GetCurrentClassLogger();
		protected string ConfigRootPath {get;set;} = string.Empty;

        public JSONSettings()
        {

        }

        public JSONSettings(string path)
		{
			ConfigRootPath = path;
		}

        protected abstract void Migrate();
        public abstract void Load();
        public abstract void Save();
        public abstract void LoadFromJson();


        public void SaveAsJson()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);

			System.IO.File.WriteAllText(ConfigPath(), json);
        }

        /// <summary>
        /// Get configration path
        /// </summary>
        /// <returns></returns>
        public string ConfigPath()
        {
            return ConfigRootPath;
		}


        public static void CopyPropertiesTo(T source, JSONSettings<T> dest)
		{
			var sourceProps = typeof (T).GetProperties().Where(x => x.CanRead).ToList();
			var destProps = typeof(JSONSettings<T>).GetProperties()
					.Where(x => x.CanWrite)
					.ToList();

			foreach (var sourceProp in sourceProps)
			{
				if (destProps.Any(x => x.Name == sourceProp.Name))
				{
					var p = destProps.First(x => x.Name == sourceProp.Name);
					if(p.CanWrite) { // check if the property can be set or no.
						p.SetValue(dest, sourceProp.GetValue(source, null), null);
					}
				}

			}

		}

    }
