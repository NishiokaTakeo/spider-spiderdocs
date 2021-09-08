using System;
using System.Collections.Generic;
using System.Linq;
using Spider.Data;

namespace SpiderDocsModule
{
	public class ViewNotificationAmendedController
	{
		public static readonly int ALL_USERS_ID = 1;

		static readonly string[] tb_Fields = new string[]
		{
			"id_user",
			"name",
			"email",
			"id_doc",
			"title",
			"version",
			"id_amendedBy",
			"amendedBy",
            "amendedDate",
			"reason"/*,
			"id_notification_group",
			"group_name" */
		};

		//public static List<ViewNotificationAmended> Get(int id_doc, int version)
		//{
		//	List<ViewNotificationAmended> ans = new List<ViewNotificationAmended>();

		//	SqlOperation sql = new SqlOperation("view_notification_amended", SqlOperationMode.Select);
		//	sql.Fields(tb_Fields);


		//	sql.Where("id_doc", id_doc);
		//	sql.Where("version", version);

		//	sql.Commit();

		//	while(sql.Read())
		//	{
		//              ViewNotificationAmended wrk = new ViewNotificationAmended();

		//              wrk.id_user = sql.Result_Int("id_user");
		//              wrk.name = sql.Result("name");
		//              wrk.email = sql.Result("email");
		//              wrk.id_doc = sql.Result_Int("id_doc");
		//              wrk.title = sql.Result("title");
		//              wrk.version = sql.Result_Int("version");
		//              wrk.id_amendedBy = sql.Result_Int("id_amendedBy");
		//              wrk.amendedBy = sql.Result("amendedBy");
		//              wrk.amendedDate = sql.Result("amendedDate");                
		//              wrk.reason = sql.Result("reason");
		//              /*wrk.id_notification_group = sql.Result_Int("id_notification_group");
		//              wrk.group_name = sql.Result("group_name");*/

		//		ans.Add(wrk);
		//	}


		//	return ans;
		//}

		public static List<ViewNotificationAmended> Get(int id_doc, int version)
		{
			List<ViewNotificationAmended> ans = new List<ViewNotificationAmended>();

			SqlOperation sql = new SqlOperation("GetNotificationInfo", SqlOperationMode.StoredProcedure);
			sql.Fields(tb_Fields);

			sql.SetCommandParameter("id_doc", id_doc);
			sql.SetCommandParameter("version", version);

			sql.Commit();


			while (sql.Read())
			{
				ViewNotificationAmended wrk = new ViewNotificationAmended();

				wrk.id_user = sql.Result_Int("id_user");
				wrk.name = sql.Result("name");
				wrk.email = sql.Result("email");
				wrk.id_doc = sql.Result_Int("id_doc");
				wrk.title = sql.Result("title");
				wrk.version = sql.Result_Int("version");
				wrk.id_amendedBy = sql.Result_Int("id_amendedBy");
				wrk.amendedBy = sql.Result("amendedBy");
				wrk.amendedDate = sql.Result<DateTime>("amendedDate");
				wrk.reason = sql.Result("reason");
				/*wrk.id_notification_group = sql.Result_Int("id_notification_group");
                wrk.group_name = sql.Result("group_name");*/

				ans.Add(wrk);
			}


			return ans;
		}

		
	}
}
