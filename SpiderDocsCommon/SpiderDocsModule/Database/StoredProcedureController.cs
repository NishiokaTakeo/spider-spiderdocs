using System;
using System.Linq;
using System.Collections.Generic;
using Spider.Data;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{

//---------------------------------------------------------------------------------
	public class StoredProcedureController
    {
		public static readonly int ADMIN_ID = 1;

        public static int hasDuplicate(string title, int id_type, string id_atbs,string atb_vals = "",int exclude_id_doc =0 )
        {
            List<Folder> folders = new List<Folder>();

            SqlOperation sql = new SqlOperation("hasDuplicate", SqlOperationMode.StoredProcedure);

            sql.SetCommandParameter("title", title);
            sql.SetCommandParameter("id_type", id_type);
            sql.SetCommandParameter("id_atbs", id_atbs);
            sql.SetCommandParameter("val_atbs", atb_vals);
            sql.SetCommandParameter("excld_id_doc", exclude_id_doc);            

            sql.Field("ans");
            sql.Commit();

            int ans = 0;
            while (sql.Read())
            {
                ans = sql.Result_Int("ans");
            }

            return ans;
        }


        //public static int hasWarnDuplicate(string title, int id_folder, int exclude_id_doc = 0)
        //{
        //    List<Folder> folders = new List<Folder>();

        //    SqlOperation sql = new SqlOperation("hasWarnDuplicate", SqlOperationMode.StoredProcedure);

        //    sql.SetCommandParameter("title", title);
        //    sql.SetCommandParameter("id_folder", id_folder);
        //    sql.SetCommandParameter("excld_id_doc", exclude_id_doc);

        //    sql.Field("ans");
        //    sql.Commit();

        //    int ans = 0;
        //    while (sql.Read())
        //    {
        //        ans = sql.Result_Int("ans");
        //    }

        //    return ans;
        //}


		public static int canUnCheckDuplicate(string title, int id_type, string id_atbs)
		{
			List<Folder> folders = new List<Folder>();

			SqlOperation sql = new SqlOperation("canUnCheckDuplicate", SqlOperationMode.StoredProcedure);

			sql.SetCommandParameter("title", title);
			sql.SetCommandParameter("id_type", id_type);
			sql.SetCommandParameter("id_atbs", id_atbs);

			sql.Field("ans");
			sql.Commit();

			int ans = 0;
			while (sql.Read())
			{
				ans = sql.Result_Int("ans");
			}

			return ans;
		}

        //public static int warnUnCheckDuplicate()
        //{
        //    List<Folder> folders = new List<Folder>();

        //    SqlOperation sql = new SqlOperation("warnUnCheckDuplicate", SqlOperationMode.StoredProcedure);

        //    sql.Field("ans");
        //    sql.Commit();

        //    int ans = 0;
        //    while (sql.Read())
        //    {
        //        ans = sql.Result_Int("ans");
        //    }

        //    return ans;
        //}
    }
}
