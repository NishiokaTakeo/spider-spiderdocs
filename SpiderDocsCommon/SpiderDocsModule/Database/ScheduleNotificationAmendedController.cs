using System;
using System.Collections.Generic;
using System.Linq;
using Spider.Data;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class ScheduleNotificationAmendedController
    {
//---------------------------------------------------------------------------------
		public static readonly int ALL_USERS_ID = 1;

		static readonly string[] tb_Fields = new string[]
		{
			"id",
			"id_doc",
			"new_version",
		};
        public static int Insert(int id_doc,int version)
        {
            
            var docgrp = DocumentController.GetDocNotificationGroup(id_doc);

            if (docgrp.id_notification_group == 0) return 0;

            object[] vals = new object[]
            {
                id_doc,
                version,
                //docgrp.id_notification_group
            };

            SqlOperation sql = new SqlOperation("schedule_notification_amended", SqlOperationMode.Insert);
            sql.Fields(new string[] { "id_doc", "new_version" }, vals);

            int insertedID = Convert.ToInt32(sql.Commit());

            return insertedID;
        }
//---------------------------------------------------------------------------------
		public static List<ScheduleNotificationAmended> List()
		{
			SqlOperation sql;
			List<ScheduleNotificationAmended> ans = new List<ScheduleNotificationAmended>();

			sql = new SqlOperation("schedule_notification_amended", SqlOperationMode.Select);
			sql.Fields(tb_Fields);

			sql.Commit();

			while(sql.Read())
			{
                ScheduleNotificationAmended wrk = new ScheduleNotificationAmended();

				wrk.id = sql.Result_Int("id");
				wrk.id_doc = sql.Result_Int("id_doc");
				wrk.new_version = sql.Result_Int("new_version");

				ans.Add(wrk);
			}

			return ans;
		}


        public static void Delete(int id)
        {
            SqlOperation sql = new SqlOperation("schedule_notification_amended", SqlOperationMode.Delete);

            sql.Where("id", id);

            sql.Commit();
        }

        public static void DeleteByDocId(int id_doc)
        {
            SqlOperation sql = new SqlOperation("schedule_notification_amended", SqlOperationMode.Delete);

            sql.Where("id_doc", id_doc);

            sql.Commit();
        }		
	}
}
