using System;
using SpiderDocsModule;

//---------------------------------------------------------------------------------
namespace SpiderDocsServerModule
{
	public class SpiderDocsApplication : SpiderDocsModule.SpiderDocsApplication
	{
		static public ConnectionSettings ConnectionSettings;

		//static public PublicSettings CurrentPublicSettings;

		static public MailSettingss MailSettingss;

		static public ServiceSettings ServiceSettings;

//---------------------------------------------------------------------------------
		public static void LoadAllSettings2() // before was in SpiderDocsServerForm
		{
			SqlOperation.MethodToGetDbManager = new Func<DbManager>(() =>
			{
				return new DbManager(ConnectionSettings.conn, ConnectionSettings._mode);
			});

			SpiderDocsApplication.ConnectionSettings = new ConnectionSettings();
			SpiderDocsApplication.ConnectionSettings.Load();

			SpiderDocsApplication.ServiceSettings = new ServiceSettings();
			SpiderDocsApplication.ServiceSettings.Load();

			if(!String.IsNullOrEmpty(SpiderDocsApplication.ConnectionSettings.conn))
			{
				SpiderDocsApplication.CurrentPublicSettings = new PublicSettings();
				SpiderDocsApplication.CurrentPublicSettings.Load();

				MailSettingss = new MailSettingss();
				MailSettingss.Load();
			}
		}

//---------------------------------------------------------------------------------
	}
}
