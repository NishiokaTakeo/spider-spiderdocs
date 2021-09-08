using System;
using System.Collections.Generic;
using System.Linq;
using Spider.Data;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class GroupController
	{
//---------------------------------------------------------------------------------
		public static readonly int ALL_USERS_ID = 1;

		static readonly string[] tb_Fields = new string[]
		{
			"id",
			"group_name",
			"obs",
			"ordination",
            "is_admin"
		};

		static readonly string[] tb_user_group = new string[]
		{
			"id_user",
			"id_group"
		};

        public enum is_admin
        {
            no_admin = 0, 
            admin = 1
        }

//---------------------------------------------------------------------------------
		public static List<Group> GetGroups(params int[] id_group)
		{
			SqlOperation sql;
			List<Group> ans = new List<Group>();

			sql = new SqlOperation("group", SqlOperationMode.Select);
			sql.Fields(tb_Fields);

			if(0 < id_group.Length)
				sql.Where_In("id", id_group);
			
			sql.Commit();

			while(sql.Read())
			{
				Group wrk = new Group();

				wrk.id = sql.Result_Int("id");
				wrk.group_name = sql.Result("group_name");
				wrk.obs = sql.Result("obs");
				wrk.ordination = sql.Result_Int("ordination");

				ans.Add(wrk);
			}

			if(0 < ans.Count)
			{
				sql = new SqlOperation("user_group", SqlOperationMode.Select);
				sql.Fields("id_group", "COUNT(id)");
				sql.GroupBy("id_group");
				sql.Where_In("id_group", ans.Select(a => a.id).ToArray());
				sql.Commit();

				while(sql.Read())
					ans.Find(a => a.id == sql.Result_Int("id_group")).NoOfUsers = sql.Result_Int("COUNT(id)");
			}

			return ans.OrderBy(a => a.ordination).OrderBy(a => a.group_name).ToList();
		}

//---------------------------------------------------------------------------------
		public static void AssignGroup(int id_group, int id_user)
		{
			object[] vals =
			{
				id_user,
				id_group
			};

			SqlOperation sql = new SqlOperation("user_group", SqlOperationMode.Insert);
			sql.Fields(tb_user_group, vals);
			sql.Commit();
		}

//---------------------------------------------------------------------------------
		public static List<int> GetUserIdInGroup(bool only_active, params int[] id_group)
		{
			List<int> id_users = new List<int>();
			
			SqlOperation sql = new SqlOperation("user_group", SqlOperationMode.Select);
			sql.Field("id_user");
			sql.Where_In("id_group", id_group);			
			sql.Commit();

			while(sql.Read())
				id_users.Add(sql.Result_Int("id_user"));
				
			return id_users;
		}

//---------------------------------------------------------------------------------
		public static List<int> GetGroupId(int id_user)
		{
			List<int> ans = new List<int>();
			SqlOperation sql = new SqlOperation("user_group", SqlOperationMode.Select);

			sql.Fields("id_group");
			sql.Where("id_user", id_user);

			sql.Commit();

			while(sql.Read())
				ans.Add(Convert.ToInt32(sql.Result_Obj("id_group")));

			return ans;
		}

//---------------------------------------------------------------------------------
		public static void DeleteUserGroup(int id_group, int id_user)
		{
			SqlOperation sql = new SqlOperation("user_group", SqlOperationMode.Delete);
			
			sql.Where("id_group", id_group);
			
			if(id_user > 0)
				sql.Where("id_user", id_user);

			sql.Commit();
		}

//---------------------------------------------------------------------------------
		public static bool IsGroupUsed(int id_group)
		{
			bool ans = false;

			SqlOperation sql = new SqlOperation("user_group", SqlOperationMode.Select);
			sql.Where("id_group", id_group);
			
			if(0 < sql.GetCountId())
				ans = true;

			return ans;
		}

        public static bool IsAdmin(int id_group)
        {
            if (179 <= Cache.getIntSystemVersion())
                return false;

            SqlOperation sql = new SqlOperation("group", SqlOperationMode.Select_Scalar);
            sql.Where("id", id_group);
            sql.Where("is_admin", ((int)is_admin.admin == 1));

            bool ok = (0 < sql.GetCount("is_admin"));
            return ok;
        }

//---------------------------------------------------------------------------------
	}
}
