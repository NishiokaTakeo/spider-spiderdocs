using System;
using Spider.Data;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public enum en_footer_menu
	{
		show_option,
		withFooter,

		Max
	}

//---------------------------------------------------------------------------------
// public settings from database --------------------------------------------------
//---------------------------------------------------------------------------------
	public class PublicSettings
	{
		public int maxDocs = 200;
		public int maxDocsRecents = 2000;
		public bool watermark = true;
		public bool reasonNewVersion = false;
		public bool allow_workspace = true;
		public bool allow_duplicatedName = true;
		public string webService_address;
		public bool add_footer = false;
		public en_footer_menu footer_menu = en_footer_menu.show_option;
		public int delete_reason_length = 10;
		public bool auto_update = true;

		//This setting is special only for NECA Legal added at V1.5.9.
		//All functions which are related to this setting should be moved to a NECA Legal plug-in and
		//removed from the main code in the future.
		public bool ignore_subject_prefix = false;

		public bool feature_multiaddress = false;
		public bool feature_reportbuilder = false;
//---------------------------------------------------------------------------------
		// Public settings
		// static string[] tb_system_settings =
		// {
		// 	"max_docs",
		// 	"max_recents",
		// 	"show_watermarks",
		// 	"reason_newVersion",
		// 	"allow_workspace",
		// 	"allow_duplicatedName",
		// 	"webService_address",
		// 	"add_footer",
		// 	"footer_menu",
		// 	"delete_reason_length",
		// 	"auto_update"
		// };

		// static string[] tb_system_settings_1_5_9 =
		// {
		// 	"ignore_subject_prefix"
		// };

//---------------------------------------------------------------------------------
		public void Load()
		{
			PublicSettings setting = Cache.PublicSetting_Load();

			int current_version = Cache.getIntSystemVersion();

			this.maxDocs = setting.maxDocs;
			this.maxDocsRecents = setting.maxDocsRecents;
			this.watermark = setting.watermark;
			this.reasonNewVersion = setting.reasonNewVersion;
			this.allow_workspace = setting.allow_workspace;
			this.allow_duplicatedName = setting.allow_duplicatedName;
			this.webService_address = setting.webService_address;
			this.add_footer = setting.add_footer;
			this.footer_menu = setting.footer_menu;
			this.delete_reason_length = setting.delete_reason_length;
			this.auto_update = setting.auto_update;

			if(159 <= current_version)
				this.ignore_subject_prefix = setting.ignore_subject_prefix;

			this.feature_multiaddress = setting.feature_multiaddress;

			this.feature_reportbuilder = setting.feature_reportbuilder;
			// // From database
			// SqlOperation sql = new SqlOperation("system_settings", SqlOperationMode.Select);
			// sql.Fields(tb_system_settings);

			// //int current_version = SpiderDocsCoer.getIntSystemVersion();
			// int current_version = Cache.getIntSystemVersion();

			// if(159 <= current_version)
			// 	sql.Fields(tb_system_settings_1_5_9);

			// sql.Commit();
			// sql.Read();

			// maxDocs = Convert.ToInt32(sql.Result("max_docs"));
			// maxDocsRecents = Convert.ToInt32(sql.Result("max_recents"));
			// watermark = Convert.ToBoolean(sql.Result("show_watermarks"));
			// reasonNewVersion = Convert.ToBoolean(sql.Result("reason_newVersion"));
			// allow_workspace = Convert.ToBoolean(sql.Result("allow_workspace"));
			// allow_duplicatedName = Convert.ToBoolean(sql.Result("allow_duplicatedName"));
			// webService_address = sql.Result("webService_address");
			// add_footer = Convert.ToBoolean(sql.Result("add_footer"));
			// footer_menu = (en_footer_menu)Convert.ToInt32(sql.Result("footer_menu"));
			// delete_reason_length = sql.Result<int>("delete_reason_length");
			// auto_update = sql.Result<bool>("auto_update");

			// if(159 <= current_version)
			// 	ignore_subject_prefix = sql.Result<bool>("ignore_subject_prefix");
		}

//---------------------------------------------------------------------------------
		public void Save()
		{
			PublicSettingController.Save(this);

			// object[] vals = new object[]
			// {
			// 	maxDocs,
			// 	maxDocsRecents,
			// 	watermark,
			// 	reasonNewVersion,
			// 	allow_workspace,
			// 	allow_duplicatedName,
			// 	webService_address,
			// 	add_footer,
			// 	(int)footer_menu,
			// 	delete_reason_length,
			// 	auto_update
			// };

			// SqlOperation sql = new SqlOperation("system_settings", SqlOperationMode.Update);
			// sql.Fields(tb_system_settings, vals);

			// int current_version = int.Parse(SpiderDocsCoer.getSystemVersion().Replace(".", ""));

			// if(159 <= current_version)
			// {
			// 	object[] vals_1_5_9 = new object[]
			// 	{
			// 		ignore_subject_prefix
			// 	};

			// 	sql.Fields(tb_system_settings_1_5_9, vals_1_5_9);
			// }

			// sql.Commit();
		}
	}

	public class PublicSettingController
	{
		static string[] tb_system_settings =
		{
			"max_docs",
			"max_recents",
			"show_watermarks",
			"reason_newVersion",
			"allow_workspace",
			"allow_duplicatedName",
			"webService_address",
			"add_footer",
			"footer_menu",
			"delete_reason_length",
			"auto_update",
			"feature_multiaddress",
			"feature_reportbuilder"
		};

		static string[] tb_system_settings_1_5_9 =
		{
			"ignore_subject_prefix"
		};

		public static PublicSettings Load()
		{
			PublicSettings setting = new PublicSettings();

			SqlOperation sql = new SqlOperation("system_settings", SqlOperationMode.Select);
			sql.Fields(tb_system_settings);

			//int current_version = SpiderDocsCoer.getIntSystemVersion();
			int current_version = Cache.getIntSystemVersion();

			if(159 <= current_version)
				sql.Fields(tb_system_settings_1_5_9);

			sql.Commit();
			sql.Read();

			setting.maxDocs = Convert.ToInt32(sql.Result("max_docs"));
			setting.maxDocsRecents = Convert.ToInt32(sql.Result("max_recents"));
			setting.watermark = Convert.ToBoolean(sql.Result("show_watermarks"));
			setting.reasonNewVersion = Convert.ToBoolean(sql.Result("reason_newVersion"));
			setting.allow_workspace = Convert.ToBoolean(sql.Result("allow_workspace"));
			setting.allow_duplicatedName = Convert.ToBoolean(sql.Result("allow_duplicatedName"));
			setting.webService_address = sql.Result("webService_address");
			setting.add_footer = Convert.ToBoolean(sql.Result("add_footer"));
			setting.footer_menu = (en_footer_menu)Convert.ToInt32(sql.Result("footer_menu"));
			setting.delete_reason_length = sql.Result<int>("delete_reason_length");
			setting.auto_update = sql.Result<bool>("auto_update");
			setting.feature_multiaddress = sql.Result<bool>("feature_multiaddress");
			setting.feature_reportbuilder = sql.Result<bool>("feature_reportbuilder");

			if(159 <= current_version)
				setting.ignore_subject_prefix = sql.Result<bool>("ignore_subject_prefix");

			return setting;
		}

        public static void Save(PublicSettings setting)
        {
            object[] vals = new object[]
            {
                setting.maxDocs,
                setting.maxDocsRecents,
                setting.watermark,
                setting.reasonNewVersion,
                setting.allow_workspace,
                setting.allow_duplicatedName,
                setting.webService_address,
                setting.add_footer,
                (int)setting.footer_menu,
                setting.delete_reason_length,
                setting.auto_update,
				setting.feature_multiaddress,
				setting.feature_reportbuilder
			};

            SqlOperation sql = new SqlOperation("system_settings", SqlOperationMode.Update);
            sql.Fields(tb_system_settings, vals);

            int current_version = int.Parse(SpiderDocsCoer.getSystemVersion().Replace(".", ""));

            if (159 <= current_version)
            {
                object[] vals_1_5_9 = new object[]
                {
                    setting.ignore_subject_prefix
                };

                sql.Fields(tb_system_settings_1_5_9, vals_1_5_9);
            }

            sql.Commit();
        }
	}

//---------------------------------------------------------------------------------
}
