
using System;
using System.Collections.Generic;
using System.Linq;
using Spider.Data;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class NotificationGroupController
	{
//---------------------------------------------------------------------------------
		public static readonly int ALL_USERS_ID = 1;

		static readonly string[] tb_Fields = new string[]
		{
			"id",
			"group_name",
		};

		static readonly string[] tb_user_notification_group = new string[]
		{
            "id",
            "id_key",
            "key_type",
			"id_notification_group"
		};


//---------------------------------------------------------------------------------
		public static List<NotificationGroup> GetGroups(params int[] id_group)
		{
			SqlOperation sql;
			List<NotificationGroup> ans = new List<NotificationGroup>();

			sql = new SqlOperation("notification_group", SqlOperationMode.Select);
			sql.Fields(tb_Fields);

			if(0 < id_group.Length)
				sql.Where_In("id", id_group);
			
			sql.Commit();

			while(sql.Read())
			{
                NotificationGroup wrk = new NotificationGroup();

				wrk.id = sql.Result_Int("id");
				wrk.group_name = sql.Result("group_name");

				ans.Add(wrk);
			}

			if(0 < ans.Count)
			{
				sql = new SqlOperation("user_notification_group", SqlOperationMode.Select);
				sql.Fields("id_notification_group", "COUNT(id)");
				sql.GroupBy("id_notification_group");
				sql.Where_In("id_notification_group", ans.Select(a => a.id).ToArray());
				sql.Commit();

				while(sql.Read())
					ans.Find(a => a.id == sql.Result_Int("id_notification_group")).NoOfUsers = sql.Result_Int("COUNT(id)");
			}

			return ans.OrderBy(a => a.group_name).ToList();
		}

        public static void DeleteGroup(int id_group)
        {
            SqlOperation sql = new SqlOperation("notification_group", SqlOperationMode.Delete);

            sql.Where("id", id_group);

            sql.Commit();
        }

        public static void SaveGroup(NotificationGroup group)
        {
            SqlOperation sql;
            if (group.id > 0 )
            {
                sql = new SqlOperation("notification_group", SqlOperationMode.Update);
                sql.Field("group_name", group.group_name);
                sql.Where("id", group.id);
            }
            else
            {
                sql = new SqlOperation("notification_group", SqlOperationMode.Insert);
                sql.Field("group_name", group.group_name);
            }

            sql.Commit();
        }

        //---------------------------------------------------------------------------------
        public static void AssignGroup(int id_ngroup, int key, en_NGroup key_type)
		{
			object[] vals =
			{
				key,
                (int)key_type,
				id_ngroup
			};

			SqlOperation sql = new SqlOperation("user_notification_group", SqlOperationMode.Insert);
			sql.Fields(new string[]{ "id_key", "key_type", "id_notification_group"}, vals);
			sql.Commit();

            

        }
        public static List<UserNotificationGroup> GetUserInGroup(int[] id_group)
        {
            List<UserNotificationGroup> ans = new List<UserNotificationGroup>();

            SqlOperation sql = new SqlOperation("user_notification_group", SqlOperationMode.Select);
            sql.Fields(tb_user_notification_group);

            sql.Where_In("id_notification_group", id_group);
            sql.Commit();
            sql.OrderBy("key_type", Spider.Data.Sql.SqlOperation.en_order_by.Ascent);
            while (sql.Read())
            {
                ans.Add(new UserNotificationGroup()
                {
                    id = sql.Result_Int("id"),
                    id_key = sql.Result_Int("id_key"),
                    key_type = (en_NGroup)sql.Result_Int("key_type"),
                    id_notification_group = sql.Result_Int("id_notification_group"),
                });
            }
                

            return ans;
        }
        //---------------------------------------------------------------------------------
        public static List<UserNotificationGroup> GetUserIdInGroup(bool only_active, params int[] id_group)
		{
			List<UserNotificationGroup> users = new List<UserNotificationGroup>();
			
			SqlOperation sql = new SqlOperation("user_notification_group", SqlOperationMode.Select);
			sql.Fields(new string[] { "id_key","key_type", "id_notification_group" });
			sql.Where_In("id_notification_group", id_group);			
			sql.Commit();
			
			while (sql.Read())
			{
				users.Add(new UserNotificationGroup()
				{
					id_key = sql.Result_Int("id_key"),
					key_type = (en_NGroup)sql.Result_Int("key_type"),
					id_notification_group = sql.Result_Int("id_notification_group")					
				});
			}
				
			return users;
		}

//---------------------------------------------------------------------------------
		public static List<int> GetGroupId(int id_user)
		{
			List<int> ans = new List<int>();
			SqlOperation sql = new SqlOperation("user_notification_group", SqlOperationMode.Select);

			sql.Fields("id_notification_group");
			sql.Where("id_user", id_user);

			sql.Commit();

			while(sql.Read())
				ans.Add(Convert.ToInt32(sql.Result_Obj("id_notification_group")));

			return ans;
		}

//---------------------------------------------------------------------------------
		public static void DeleteUserGroup(int id_group)
		{
			SqlOperation sql = new SqlOperation("user_notification_group", SqlOperationMode.Delete);

            if (id_group > 0)
			    sql.Where("id_notification_group", id_group);

            sql.Commit();
		}

        public static void DeleteUserGroupBykey(int id_ngroup, int id_key, en_NGroup key_type)
        {

            SqlOperation sql = new SqlOperation("user_notification_group", SqlOperationMode.Delete);

            
            sql.Where("id_notification_group", id_ngroup);
            sql.Where("id_key", id_key);
            sql.Where("key_type", (int)key_type);


            sql.Commit();
        }

        public static void DeleteUserGroupBy(int id)
        {
            SqlOperation sql = new SqlOperation("user_notification_group", SqlOperationMode.Delete);

            sql.Where("id", id);

            sql.Commit();
        }


        //---------------------------------------------------------------------------------
        public static bool IsGroupUsed(int id_group)
		{
			bool ans = false;

			SqlOperation sql = new SqlOperation("user_notification_group", SqlOperationMode.Select);
			sql.Where("id_notification_group", id_group);
			
			if(0 < sql.GetCountId())
				ans = true;

			return ans;
		}


//---------------------------------------------------------------------------------
	}
}
