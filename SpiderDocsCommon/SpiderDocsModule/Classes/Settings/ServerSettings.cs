using System;
using Microsoft.Win32;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class ServerSettings
	{
		public string conn; // Should be encrypted
		public string svmode; // Should be encrypted

		public string server;
		public int port = 5322;
		public bool localDb = false;
        static Logger logger = LogManager.GetCurrentClassLogger();
        
		public void LoadFromRegistory()
		{
            logger.Trace("Load {0}", SpiderDocsApplication.RegistryPath);

            using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(SpiderDocsApplication.RegistryPath))
            {                
                try
                {
                    conn = registryKey.GetValue("conn").ToString();
                }
                catch(Exception ex) { logger.Error(ex);}
                try
                {
                    svmode = registryKey.GetValue("svmode").ToString();
                }
                catch(Exception ex) { logger.Error(ex);}
                try
                {
                    server = registryKey.GetValue("server").ToString();
                }
                catch(Exception ex) { logger.Error(ex);}
                try
                {
                    localDb = (localDb == true) ? localDb : Convert.ToBoolean(registryKey.GetValue("localDb"));
                }
                catch(Exception ex) { logger.Error(ex);}
            }

		}

		public void Save()
		{            
            //RegistryKey key = SpiderDocsApplication.get32BitRegKey();			
            try{
                using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(SpiderDocsApplication.RegistryPath))
                {
                    if (!String.IsNullOrEmpty(svmode)) registryKey.SetValue("svmode", svmode, RegistryValueKind.String); 
                    if (!String.IsNullOrEmpty(conn)) registryKey.SetValue("conn", conn, RegistryValueKind.String); 
                    if (!String.IsNullOrEmpty(server)) registryKey.SetValue("server", server, RegistryValueKind.String); 
                    if (!String.IsNullOrEmpty(port.ToString())) registryKey.SetValue("port", port.ToString(), RegistryValueKind.DWord); 
                    registryKey.SetValue("localDb", (localDb == true ? 1 : 0), RegistryValueKind.DWord); 
                }

                new ApplicationSettings().SaveAsJson();
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }			
        }

		static public string GenerateConnStrFromPrams(string database_address, string database_name, string user, string password)
		{
			Crypt crypt = new Crypt();

			return crypt.Encrypt(
				"Data Source=" + database_address + ";" +
				"Initial Catalog=" + database_name + ";" +
				"Persist Security Info=True;" +
				"User ID=" + user + ";" +
				"Password=" + password);
		}
	}

//---------------------------------------------------------------------------------
}
