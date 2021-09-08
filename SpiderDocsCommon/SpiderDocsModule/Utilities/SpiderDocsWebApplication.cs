using System;
using Microsoft.Win32;
using System.IO;
using NLog;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpiderDocsModule
{

	public class SpiderDocsWebApplication
	{

 		static Logger logger = LogManager.GetCurrentClassLogger();


		// Need to be loaded at first. Contains server address and so on.
		public static ServerSettings CurrentServerSettings = new ServerSettings();

		// Second load. Loaded when server connection is succeeded.
		public static PublicSettings CurrentPublicSettings;
		public static MailSettingss CurrentMailSettings;

		// Fourth load after user login is succeeded.
		public static UserGlobalSettings CurrentUserGlobalSettings;

//---------------------------------------------------------------------------------
		// This function will work only when a user already logged in.
		public static void LoadAllSettings()
		{
			SqlOperation.MethodToGetDbManager = new Func<DbManager>(() =>
			{
				return new DbManager(SpiderDocsWebApplication.CurrentServerSettings.conn, SpiderDocsWebApplication.CurrentServerSettings.svmode);
			});
		}

	}
}
