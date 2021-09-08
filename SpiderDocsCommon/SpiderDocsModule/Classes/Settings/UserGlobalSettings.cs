using System;
using Spider.Data;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public enum en_DoubleClickBehavior
	{
		OpenToRead = 0,
		CheckOut,
		CheckOutFooter,

		Max
	}

//---------------------------------------------------------------------------------
// Private settings from database (Per user) --------------------------------------
//---------------------------------------------------------------------------------
	public class UserGlobalSettings
	{
		public int userId;
		public en_DoubleClickBehavior double_click = en_DoubleClickBehavior.OpenToRead;
		public bool show_import_dialog_new_mail = false;
        public bool exclude_archive = false;
		public bool ocr = true;
		public bool default_ocr_import = true;
        //public bool pdf_merge = false;
        public bool default_pdf_merge = false;
        public bool enable_folder_creation_by_user = false;
        static public bool IsDevelopper {
			get {
				return UserController.GetUser(true, SpiderDocsApplication.CurrentUserId)?.login.ToLower() == "administrator" ;
			}
		}
        public bool IsActiveOCR { get { return this.ocr && this.default_ocr_import; } }
//---------------------------------------------------------------------------------
		public UserGlobalSettings(int userId)
		{
			this.userId = userId;
			//Load();
		}

//---------------------------------------------------------------------------------
		public void Load()
		{
			UserGlobalSettings setting = new Cache(userId).UserGlobalSetting_Load();

			this.double_click = setting.double_click;
			this.show_import_dialog_new_mail = setting.show_import_dialog_new_mail;
            this.default_ocr_import = setting.default_ocr_import;            
            this.ocr = setting.ocr;
            //this.pdf_merge = setting.pdf_merge;
            this.default_pdf_merge = setting.default_pdf_merge;
            this.enable_folder_creation_by_user = setting.enable_folder_creation_by_user;
        }

//---------------------------------------------------------------------------------
		public void Save()
		{
			UserGlobalSettingController.Save(this);
		}
	}

	public class UserGlobalSettingController
	{
		static string[] tb_user_preferences =
		{
			"double_click",
			"show_import_dialog_new_mail",
            //"exclude_archive"
			"default_ocr_import",
			"ocr",
            //"pdf_merge",
            "default_pdf_merge",
            "enable_folder_creation_by_user"
        };

		public static UserGlobalSettings Load(int userId)
		{
			UserGlobalSettings setting = new UserGlobalSettings(userId);

			SqlOperation sql = new SqlOperation("user_preferences", SqlOperationMode.Select);
			sql.Fields(tb_user_preferences);
			sql.Where("id_user", userId);
			sql.Commit();

			if(sql.Read())
			{
				setting.double_click = (en_DoubleClickBehavior)Convert.ToInt32(sql.Result_Obj("double_click"));
				setting.show_import_dialog_new_mail = sql.Result<bool>("show_import_dialog_new_mail");
                //exclude_archive = sql.Result<bool>("exclude_archive");
				setting.default_ocr_import = sql.Result<bool>("default_ocr_import");
				setting.ocr = sql.Result<bool>("ocr");
                //setting.pdf_merge = sql.Result<bool>("pdf_merge");
                setting.default_pdf_merge = sql.Result<bool>("default_pdf_merge");
                setting.enable_folder_creation_by_user = sql.Result<bool>("enable_folder_creation_by_user");
            }

			return setting;
		}

		public static void Save(UserGlobalSettings setting)
		{
			if(setting.userId <= 0)
				return;

			object[] vals = new object[]
			{
				(int)setting.double_click,
				setting.show_import_dialog_new_mail,
                //exclude_archive
				setting.default_ocr_import,
				setting.ocr,
                //setting.pdf_merge,
                setting.default_pdf_merge,
                setting.enable_folder_creation_by_user
            };

			SqlOperation sql;

			sql = new SqlOperation("user_preferences", SqlOperationMode.Select_Scalar);
			sql.Where("id_user", setting.userId);

			if(sql.GetCount("id_user") <= 0)
			{
				sql = new SqlOperation("user_preferences", SqlOperationMode.Insert);
				sql.Field("id_user", setting.userId);

			}else
			{
				sql = new SqlOperation("user_preferences", SqlOperationMode.Update);
				sql.Where("id_user", setting.userId);
			}

			sql.Fields(tb_user_preferences, vals);
			sql.Commit();
		}
	}
//---------------------------------------------------------------------------------
}
