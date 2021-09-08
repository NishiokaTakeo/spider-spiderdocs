// Database structure is messy.
// The parameters not be expanded as fields but should be records with parameter ids.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Spider.Data;
using SpiderDocsModule;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public enum OpenedPanel
	{
		Explorer,
		Search,
        Archived
    }

    //---------------------------------------------------------------------------------
    public class cl_WorkspaceCustomize
    {
        //static readonly string[] tb_user_workspace_customize =
        //{
        //	"f_id",
        //	"f_keyword",
        //	"f_name",
        //	"f_folder",
        //	"f_date",
        //	"f_author",
        //	"f_extension",
        //	"f_docType",
        //	"c_id",
        //	"c_id_width",
        //	"c_id_position",
        //	"c_name",
        //	"c_name_width",
        //	"c_name_position",
        //	"c_folder",
        //	"c_folder_with",
        //	"c_folder_position",
        //	"c_docType",
        //	"c_docType_width",
        //	"c_docType_position",
        //	"c_author",
        //	"c_author_width",
        //	"c_author_position",
        //	"c_version",
        //	"c_version_width",
        //	"c_version_position",
        //	"c_date",
        //	"c_date_width",
        //	"c_date_position",
        //	"c_atb_id",
        //	"c_atb_width",
        //	"c_atb_position",
        //	"cboFolder_width",
        //	"cboAuthor_width",
        //	"cboExtension_width",
        //	"cboDocType_width",
        //	"f_Review",
        //	"OpenedPanel",

        //	"c_mail_from",
        //	"c_mail_from_width",
        //	"c_mail_from_position",

        //	"c_mail_to",
        //	"c_mail_to_width",
        //	"c_mail_to_position",

        //	"c_mail_time",
        //	"c_mail_time_width",
        //	"c_mail_time_position",

        //	"c_mail_in_out_prefix"
        //};

        //---------------------------------------------------------------------------------
        // order should be same as tabel fields
        public bool f_id = true;
        public bool f_keyword = true;
        public bool f_name = true;
        public bool f_folder = true;
        public bool f_date = true;
        public bool f_author = true;
        public bool f_extension = true;
        public bool f_docType = true;

        public bool c_id = true;
        public int c_id_width = 42;
        public int c_id_position = 1;

        public bool c_name = true;
        public int c_name_width = 289;
        public int c_name_position = 3;

        public bool c_folder = true;
        public int c_folder_width = 84;
        public int c_folder_position = 7;

        public bool c_docType = true;
        public int c_docType_width = 85;
        public int c_docType_position = 8;

        public bool c_author = true;
        public int c_author_width = 59;
        public int c_author_position = 9;

        public bool c_version = true;
        public int c_version_width = 59;
        public int c_version_position = 10;

        public bool c_date = true;
        public int c_date_width = 11;
        public int c_date_position = 12;

        public int c_atb_id = 0;
        public int c_atb_width = 0;
        public int c_atb_position = 0;

        public int cboFolder_width = 171;
        public int cboAuthor_width = 150;
        public int cboExtension_width = 150;
        public int cboDocType_width = 150;

        public bool f_Review = true;

        public OpenedPanel OpenedPanel = OpenedPanel.Explorer;

        public bool c_mail_from = false;
        public int c_mail_from_width = 70;
        public int c_mail_from_position = 4;

        public bool c_mail_to = false;
        public int c_mail_to_width = 70;
        public int c_mail_to_position = 5;

        public bool c_mail_time = false;
        public int c_mail_time_width = 70;
        public int c_mail_time_position = 6;

        public bool c_mail_in_out_prefix = false;

        //---------------------------------------------------------------------------------
        FieldInfo[] properties { get { return typeof(cl_WorkspaceCustomize).GetFields(); } }
        public int userId { get; set; }

        //---------------------------------------------------------------------------------
        public cl_WorkspaceCustomize(int userId)
        {
            this.userId = userId;
            //Load();
        }

        //---------------------------------------------------------------------------------
        public void Load()
        {
            cl_WorkspaceCustomize setting = new Cache(this.userId).cl_WorkspaceCustomize_Load();

			this.f_id = setting.f_id ;
			this.f_keyword = setting.f_keyword ;
			this.f_name = setting.f_name ;
			this.f_folder = setting.f_folder ;
			this.f_date = setting.f_date ;
			this.f_author = setting.f_author ;
			this.f_extension = setting.f_extension ;
			this.f_docType = setting.f_docType ;
			this.c_id = setting.c_id ;
			this.c_id_width = setting.c_id_width ;
			this.c_id_position = setting.c_id_position ;
			this.c_name = setting.c_name ;
			this.c_name_width = setting.c_name_width ;
			this.c_name_position = setting.c_name_position ;
			this.c_folder = setting.c_folder ;
			this.c_folder_width = setting.c_folder_width ;
			this.c_folder_position = setting.c_folder_position ;
			this.c_docType = setting.c_docType ;
			this.c_docType_width = setting.c_docType_width ;
			this.c_docType_position = setting.c_docType_position ;
			this.c_author = setting.c_author ;
			this.c_author_width = setting.c_author_width ;
			this.c_author_position = setting.c_author_position ;
			this.c_version = setting.c_version ;
			this.c_version_width = setting.c_version_width ;
			this.c_version_position = setting.c_version_position ;
			this.c_date = setting.c_date ;
			this.c_date_width = setting.c_date_width ;
			this.c_date_position = setting.c_date_position ;
			this.c_atb_id = setting.c_atb_id ;
			this.c_atb_width = setting.c_atb_width ;
			this.c_atb_position = setting.c_atb_position ;
			this.cboFolder_width = setting.cboFolder_width ;
			this.cboAuthor_width = setting.cboAuthor_width ;
			this.cboExtension_width = setting.cboExtension_width ;
			this.cboDocType_width = setting.cboDocType_width ;
			this.f_Review = setting.f_Review ;
			this.OpenedPanel = setting.OpenedPanel ;
			this.c_mail_from = setting.c_mail_from ;
			this.c_mail_from_width = setting.c_mail_from_width ;
			this.c_mail_from_position = setting.c_mail_from_position ;
			this.c_mail_to = setting.c_mail_to ;
			this.c_mail_to_width = setting.c_mail_to_width ;
			this.c_mail_to_position = setting.c_mail_to_position ;
			this.c_mail_time = setting.c_mail_time ;
			this.c_mail_time_width = setting.c_mail_time_width ;
			this.c_mail_time_position = setting.c_mail_time_position ;
			this.c_mail_in_out_prefix = setting.c_mail_in_out_prefix ;
			this.userId = setting.userId ;


            // FieldInfo[] properties = typeof(cl_WorkspaceCustomize).GetFields();

            // SqlOperation sql = new SqlOperation("user_workspace_customize", SqlOperationMode.Select);
            // sql.Fields(tb_user_workspace_customize);
            // sql.Where("id_user", userId);
            // sql.Commit();

            // if(sql.Read())
            // {
            // 	foreach(FieldInfo field in properties)
            // 	{
            // 		string name = field.Name;
            // 		if(name == "c_folder_width")
            // 			name = "c_folder_with"; // field name in the database is registered with this typo...

            // 		if(field.FieldType == typeof(bool))
            // 		{
            // 			field.SetValue(this, sql.Result<bool>(name));

            // 		}else if(field.FieldType == typeof(int))
            // 		{
            // 			int val = sql.Result<int>(name);

            // 			if(0 < val)
            // 				field.SetValue(this, sql.Result<int>(name));

            // 		}else if(field.FieldType == typeof(OpenedPanel))
            // 		{
            // 			field.SetValue(this, (OpenedPanel)sql.Result<int>(name));
            // 		}
            // 	}
            // }
        }

        //---------------------------------------------------------------------------------
        public void Save()
        {
            cl_WorkspaceCustomizeController.Save(this);

            // FieldInfo[] properties = typeof(cl_WorkspaceCustomize).GetFields();
            // List<object> vals = new List<object>();

            // SqlOperation sql = new SqlOperation("user_workspace_customize", SqlOperationMode.Delete);
            // sql.Where("id_user", userId);
            // sql.Commit();

            // foreach(FieldInfo field in properties)
            // {
            // 	if(field.FieldType == typeof(OpenedPanel))
            // 		vals.Add((int)field.GetValue(this));
            // 	else
            // 		vals.Add(field.GetValue(this));
            // }

            // sql = new SqlOperation("user_workspace_customize", SqlOperationMode.Insert);
            // sql.Field("id_user", userId);
            // sql.Fields(tb_user_workspace_customize, vals.ToArray());
            // sql.Commit();
        }

        public static void CopyPropertiesTo<T, TU>(T source, TU dest)
        {
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TU).GetProperties()
                    .Where(x => x.CanWrite)
                    .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite) { // check if the property can be set or no.
                        p.SetValue(dest, sourceProp.GetValue(source, null), null);
                    }
                }

            }
        }
    }



//---------------------------------------------------------------------------------
	

	public class cl_WorkspaceCustomizeController
	{
		static readonly string[] tb_user_workspace_customize =
		{
			"f_id",
			"f_keyword",
			"f_name",
			"f_folder",
			"f_date",
			"f_author",
			"f_extension",
			"f_docType",
			"c_id",
			"c_id_width",
			"c_id_position",
			"c_name",
			"c_name_width",
			"c_name_position",
			"c_folder",
			"c_folder_with",
			"c_folder_position",
			"c_docType",
			"c_docType_width",
			"c_docType_position",
			"c_author",
			"c_author_width",
			"c_author_position",
			"c_version",
			"c_version_width",
			"c_version_position",
			"c_date",
			"c_date_width",
			"c_date_position",
			"c_atb_id",
			"c_atb_width",
			"c_atb_position",
			"cboFolder_width",
			"cboAuthor_width",
			"cboExtension_width",
			"cboDocType_width",
			"f_Review",
			"OpenedPanel",

			"c_mail_from",
			"c_mail_from_width",
			"c_mail_from_position",

			"c_mail_to",
			"c_mail_to_width",
			"c_mail_to_position",

			"c_mail_time",
			"c_mail_time_width",
			"c_mail_time_position",

			"c_mail_in_out_prefix"
		};

		public static cl_WorkspaceCustomize Load(cl_WorkspaceCustomize setting)
		{
			FieldInfo[] properties = typeof(cl_WorkspaceCustomize).GetFields();

			SqlOperation sql = new SqlOperation("user_workspace_customize", SqlOperationMode.Select);
			sql.Fields(tb_user_workspace_customize);
			sql.Where("id_user", setting.userId);
			sql.Commit();

			if(sql.Read())
			{
				foreach(FieldInfo field in properties)
				{
					string name = field.Name;
					if(name == "c_folder_width")
						name = "c_folder_with"; // field name in the database is registered with this typo...

					if(field.FieldType == typeof(bool))
					{
						field.SetValue(setting, sql.Result<bool>(name));

					}else if(field.FieldType == typeof(int))
					{
						int val = sql.Result<int>(name);

						if(0 < val)
							field.SetValue(setting, sql.Result<int>(name));

					}else if(field.FieldType == typeof(OpenedPanel))
					{
						field.SetValue(setting, (OpenedPanel)sql.Result<int>(name));
					}
				}
			}

			return setting;
		}

		public static void Save(cl_WorkspaceCustomize setting)
		{
			FieldInfo[] properties = typeof(cl_WorkspaceCustomize).GetFields();
			List<object> vals = new List<object>();

			SqlOperation sql = new SqlOperation("user_workspace_customize", SqlOperationMode.Delete);
			sql.Where("id_user", setting.userId);
			sql.Commit();

			foreach(FieldInfo field in properties)
			{
				if(field.FieldType == typeof(OpenedPanel))
					vals.Add((int)field.GetValue(setting));
				else
					vals.Add(field.GetValue(setting));
			}

			sql = new SqlOperation("user_workspace_customize", SqlOperationMode.Insert);
			sql.Field("id_user", setting.userId);
			sql.Fields(tb_user_workspace_customize, vals.ToArray());
			sql.Commit();
		}
	}

}
