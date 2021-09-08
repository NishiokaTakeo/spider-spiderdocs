using System;
using System.Collections.Generic;
using Microsoft.Win32;
using SpiderDocsModule;
using NLog;
//---------------------------------------------------------------------------------
namespace SpiderDocsServerModule
{
	public class ConnectionSettings
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

		Crypt crypt = new Crypt();
		
		public string _database_address;
		public string _database_name;
		public string _user;
		public string _password;
		public string _mode;

		public string conn
		{
			get
			{
				if(!String.IsNullOrEmpty(_database_address)
				&& !String.IsNullOrEmpty(_database_name)
				&& !String.IsNullOrEmpty(_user)
				&& !String.IsNullOrEmpty(_password)
				&& !String.IsNullOrEmpty(_mode))
				{
					return ServerSettings.GenerateConnStrFromPrams(database_address, database_name, user, password);

				}else
				{
					return "";
				}
			}
		}
		
		public string database_address
		{
			get { return (String.IsNullOrEmpty(_database_address) ? "" : crypt.Decrypt(_database_address)); }
			set { _database_address = crypt.Encrypt(value); }
		}

		public string database_name
		{
			get { return (String.IsNullOrEmpty(_database_name) ? "" : crypt.Decrypt(_database_name)); }
			set { _database_name = crypt.Encrypt(value); }
		}
		
		public string user
		{
			get { return (String.IsNullOrEmpty(_user) ? "" : crypt.Decrypt(_user)); }
			set { _user = crypt.Encrypt(value); }
		}
		
		public string password
		{
			get { return (String.IsNullOrEmpty(_password) ? "" : crypt.Decrypt(_password)); }
			set { _password = crypt.Encrypt(value); }
		}
		
		public SpiderDocsModule.DbManager.en_sql_mode mode
		{
			get
			{
				DbManager.en_sql_mode ans = DbManager.en_sql_mode.None;

				if(!String.IsNullOrEmpty(_mode))
					ans = DbManager.GetServerModeVal(crypt.Decrypt(_mode));

				return ans;
			}
			set { _mode = crypt.Encrypt(((int)value).ToString()); }
		}

//---------------------------------------------------------------------------------
		public void Load()
		{
            logger.Trace("Begin");

            // From registry
            using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(SpiderDocsApplication.ServicePortDbRegistryPath))
			{
				if(registryKey != null)
				{
					try{ _database_address = registryKey.GetValue("db_a").ToString(); } catch{}
					try{ _database_name = registryKey.GetValue("db_b").ToString(); } catch{}
					try{ _user = registryKey.GetValue("db_c").ToString(); } catch{}
					try{ _password = registryKey.GetValue("db_d").ToString(); } catch{}
					try{ _mode = registryKey.GetValue("db_e").ToString(); } catch{}

					registryKey.Close();
				}
			}
		}

		public void Save()
		{
            logger.Trace("Begin");

            RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(SpiderDocsApplication.ServicePortDbRegistryPath);

			registryKey.SetValue("db_a", _database_address, RegistryValueKind.String);
			registryKey.SetValue("db_b", _database_name, RegistryValueKind.String);
			registryKey.SetValue("db_c", _user, RegistryValueKind.String);
			registryKey.SetValue("db_d", _password, RegistryValueKind.String);
			registryKey.SetValue("db_e", _mode, RegistryValueKind.String);

			registryKey.Close();
		}

		public void Clear()
		{
            logger.Trace("Begin");

            _database_address = "";
			_database_name = "";
			_user = "";
			_password = "";
			_mode = "";
		}
	}

//---------------------------------------------------------------------------------
	public class ServiceSettings
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

        public string ClientId;
		public string ClientName;
		public string ProductKey;
		public DateTime LastUpdateCheckedDate = new DateTime();

		public int Port;

//---------------------------------------------------------------------------------
		public void Load()
		{
            logger.Trace("Begin");

            RegistryKey registryKey;

			// From registry
			using(registryKey = Registry.LocalMachine.OpenSubKey(SpiderDocsApplication.ServiceClientDetailsRegistryPath))
			{
				if(registryKey != null)
				{

					try{ ClientId = registryKey.GetValue("clientId").ToString(); } catch{}
					try{ ClientName = registryKey.GetValue("clientName").ToString(); } catch{}
					try{ ProductKey = registryKey.GetValue("product_key").ToString(); } catch{}
					try{ LastUpdateCheckedDate = DateTime.Parse(registryKey.GetValue("dtUpdateChecked").ToString()); } catch{}

					registryKey.Close();
				}
			}

			using(registryKey = Registry.LocalMachine.OpenSubKey(SpiderDocsApplication.ServicePortDbRegistryPath))
			{
				if(registryKey != null)
				{
					try{ Port = int.Parse(registryKey.GetValue("port").ToString()); } catch{}

					registryKey.Close();
				}
			}
		}

		public void Save()
		{
            logger.Trace("Begin");
            RegistryKey registryKey;
			
			registryKey = Registry.LocalMachine.CreateSubKey(SpiderDocsApplication.ServiceClientDetailsRegistryPath);

			registryKey.SetValue("clientId", ClientId, RegistryValueKind.String);
			registryKey.SetValue("clientName", ClientName, RegistryValueKind.String);
			registryKey.SetValue("product_key", ProductKey, RegistryValueKind.String);
			registryKey.SetValue("dtUpdateChecked", LastUpdateCheckedDate.ToString(), RegistryValueKind.String);
			registryKey.Close();

			registryKey = Registry.LocalMachine.CreateSubKey(SpiderDocsApplication.ServicePortDbRegistryPath);
			registryKey.SetValue("port", Port.ToString(), RegistryValueKind.String);
			registryKey.Close();
		}

//---------------------------------------------------------------------------------
	}
}
