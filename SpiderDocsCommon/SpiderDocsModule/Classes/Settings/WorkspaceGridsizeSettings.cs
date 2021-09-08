using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Spider.Data;
using SpiderDocsModule;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
    public class cl_WorkspaceGridsize:ICloneable
    {
        // Workspace grid size
        static readonly string[] tb_user_grid_size =
        {
            "db_grid_full",
            "local_grid_full",
            "splitDistance"
        };

        //---------------------------------------------------------------------------------
        public bool db_grid_full = false;
        public bool local_grid_full = false;
        public int splitDistance = 48;

        //---------------------------------------------------------------------------------
        FieldInfo[] properties { get { return typeof(cl_WorkspaceGridsize).GetFields(); } }
        public int userId { get; set; }

        //---------------------------------------------------------------------------------
        public cl_WorkspaceGridsize(int userId)
        {
            this.userId = userId;
            //Load();
        }

        //---------------------------------------------------------------------------------
        public void Load()
        {
            cl_WorkspaceGridsize setting = new Cache(this.userId).cl_WorkspaceGridsize_Load();
			this. db_grid_full = setting.db_grid_full;
			this. local_grid_full = setting.local_grid_full;
			this.splitDistance = setting.splitDistance;
			this.userId = setting.userId;

            // SqlOperation sql = new SqlOperation("user_grid_size", SqlOperationMode.Select);
            // sql.Fields(tb_user_grid_size);
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
            cl_WorkspaceGridsizeController.Save(this);
            // SqlOperation sql;
            // List<object> vals = new List<object>();

            // sql = new SqlOperation("user_grid_size", SqlOperationMode.Delete);
            // sql.Where("id_user", userId);
            // sql.Commit();

            // foreach(FieldInfo field in properties)
            // {
            // 	if(field.FieldType == typeof(OpenedPanel))
            // 		vals.Add((int)field.GetValue(this));
            // 	else
            // 		vals.Add(field.GetValue(this));
            // }

            // sql = new SqlOperation("user_grid_size", SqlOperationMode.Insert);
            // sql.Field("id_user", userId);
            // sql.Fields(tb_user_grid_size, vals.ToArray());
            // sql.Commit();
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        //---------------------------------------------------------------------------------
    }

    public class cl_WorkspaceGridsizeController
    {
        static FieldInfo[] properties { get { return typeof(cl_WorkspaceGridsize).GetFields(); } }

        // Workspace grid size
        static readonly string[] tb_user_grid_size =
        {
            "db_grid_full",
            "local_grid_full",
            "splitDistance"
        };

        public static cl_WorkspaceGridsize Load(cl_WorkspaceGridsize setting)
        {
            FieldInfo[] properties = typeof(cl_WorkspaceGridsize).GetFields();

            SqlOperation sql = new SqlOperation("user_grid_size", SqlOperationMode.Select);
            sql.Fields(tb_user_grid_size);
            sql.Where("id_user", setting.userId);
            sql.Commit();

            if (sql.Read())
            {
                foreach (FieldInfo field in properties)
                {
                    string name = field.Name;
                    if (name == "c_folder_width")
                        name = "c_folder_with"; // field name in the database is registered with this typo...

                    if (field.FieldType == typeof(bool))
                    {
                        field.SetValue(setting, sql.Result<bool>(name));

                    }
                    else if (field.FieldType == typeof(int))
                    {
                        int val = sql.Result<int>(name);

                        if (0 < val)
                            field.SetValue(setting, sql.Result<int>(name));

                    }
                    else if (field.FieldType == typeof(OpenedPanel))
                    {
                        field.SetValue(setting, (OpenedPanel)sql.Result<int>(name));
                    }
                }
            }

            return setting;
        }

        public static void Save(cl_WorkspaceGridsize setting)
        {
            SqlOperation sql;
            List<object> vals = new List<object>();

            sql = new SqlOperation("user_grid_size", SqlOperationMode.Delete);
            sql.Where("id_user", setting.userId);
            sql.Commit();

            foreach (FieldInfo field in properties)
            {
                if (field.FieldType == typeof(OpenedPanel))
                    vals.Add((int)field.GetValue(setting));
                else
                    vals.Add(field.GetValue(setting));
            }

            sql = new SqlOperation("user_grid_size", SqlOperationMode.Insert);
            sql.Field("id_user", setting.userId);
            sql.Fields(tb_user_grid_size, vals.ToArray());
            sql.Commit();

        }
    }
}
