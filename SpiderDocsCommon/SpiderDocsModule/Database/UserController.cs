using System;
using System.Linq;
using System.Collections.Generic;
using Spider.Data;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public enum en_UserEvents
	{
		Login = 1,
		Logout,
		LoginFaile,
		PasswordCahnge,
		PasswordChangeFail,

		Max
	}

	public class UserController
	{
		public static readonly string[] tb_Fields = new string[]
		{
			"login",
			"name",
			"id_permission",
			"email",
			"reviewer",
			"active",
			"password",
            "name_computer",
        };

		public static string[] tb_EventStr = new string[(int)en_UserEvents.Max]
		{
			"", // 1 base
			"Login",
			"Logout",
			"Login Failed",
			"Change Password Succeeded",
			"Change Password Not succeeded"
		};

		static readonly string[] tb_user_recent_document =
		{
			"id_user",
			"id_doc",
			"date"
		};

//---------------------------------------------------------------------------------
// user ---------------------------------------------------------------------------
//---------------------------------------------------------------------------------
		public static User GetUser(bool only_active, int id_user)
		{
            IEnumerable<User> uses = Cache.GetUser();

            uses = uses.Where(u => u.id == id_user);

            if (only_active)
                uses = uses.Where(u => u.active == only_active);

            return uses.FirstOrDefault();

            //return GetUser(only_active, false, id_user: new int[] { id_user }, username: null).FirstOrDefault();
        }

		public static User GetUser(bool only_active, string username)
		{
            IEnumerable<User> uses = Cache.GetUser();            

            if( only_active )
            {
                uses = uses.Where(u => u.active == only_active);
            }

            if (!string.IsNullOrEmpty(username))
            {
                uses = uses.Where(u => u.login.ToLower() == username.ToLower());
            }

            return uses.FirstOrDefault() ?? new User();

            //return GetUser(only_active, false, id_user: null, username: new string[] { username }).FirstOrDefault();
        }

		public static List<User> GetUser(bool only_active, bool only_reviewer, params int[] id_user)
		{
            IEnumerable<User> uses = Cache.GetUser();

            if (only_active)
            {
                uses = uses.Where(u => u.active == only_active);
            }

            if (only_reviewer)
            {
                uses = uses.Where(u => u.reviewer == only_reviewer);
            }

            if (id_user != null && id_user.Length > 0)
            {
                uses = uses.Where(u => id_user.Contains(u.id));
            }

            return uses.ToList();

            //return GetUser(only_active, only_reviewer, id_user: id_user, username: null);
        }

        /*
		public static List<User> GetUser(bool only_active, bool only_reviewer, params int[] id_user)
		{
			return GetUser(only_active, only_reviewer, id_user: id_user, username: null);
		}
        */
		public static List<User> GetUsers()
		{
			return _GetUser(false,false,null);
		}

		static List<User> _GetUser(bool only_active, bool only_reviewer, int[] id_user = null, string[] username = null)
		{
			List<User> ans = new List<User>();
			SqlOperation sql = new SqlOperation("user", SqlOperationMode.Select);
			sql.Field("id");
			sql.Fields(tb_Fields);

			if(only_active)
				sql.Where("active", 1);

			if(only_reviewer)
				sql.Where("reviewer", 1);

			if(id_user != null && (0 < id_user.Length))
				sql.Where_In("id", id_user);

			if(username != null && (0 < username.Length))
				sql.Where_In("login", username);

            sql.OrderBy("name", Spider.Data.Sql.SqlOperation.en_order_by.Ascent);

			sql.Commit();

			while(sql.Read())
			{
				User wrk = new User();

				wrk.id = Convert.ToInt32(sql.Result_Obj("id"));
				wrk.login = sql.Result("login");
				wrk.name = sql.Result("name");
				wrk.id_permission = Convert.ToInt32(sql.Result_Obj("id_permission"));
				wrk.email = sql.Result("email");
				wrk.reviewer = (Convert.ToInt32(sql.Result_Obj("reviewer")) == 0 ? false : true);
				wrk.active = sql.Result<bool>("active");
				wrk.password = sql.Result("password");

				ans.Add(wrk);
			}
            /*
			if(sort)
				ans = ans.OrderBy(a => a.name).ToList();
            */
			return ans;
		}

//---------------------------------------------------------------------------------
		public static void UpdatetUser(User user)
		{
			UpdateOrInsertUser(user, false);
		}

		public static void InsertUser(User user)
		{
			UpdateOrInsertUser(user, true);
		}

		static void UpdateOrInsertUser(User user, bool insert)
		{
			SqlOperation sql;
			
			if(insert)
			{
				sql = new SqlOperation("user", SqlOperationMode.Insert);

			}else
			{
				sql = new SqlOperation("user", SqlOperationMode.Update);
				sql.Where("id", user.id);
			}

			object[] vals = 
			{
				user.login,
				user.name,
				user.id_permission,
				user.email,
				user.reviewer,
				user.active,
				user.password,
                user.name_computer
			};

			sql.Fields(tb_Fields, vals);
			sql.Commit();

			if(insert)
			{
				sql = new SqlOperation("user", SqlOperationMode.Select_Scalar);
				GroupController.AssignGroup(1, sql.GetMaxId());
				NotificationGroupController.AssignGroup(1, sql.GetMaxId(),en_NGroup.User);
			}
		}
		
//---------------------------------------------------------------------------------
		public static void ChangePassword(int id_user, string newPassword)
		{
			SqlOperation sql = new SqlOperation("user", SqlOperationMode.Update);
			
			sql.Field("password", newPassword);
			sql.Where("id", id_user);
			sql.Commit();
		}

//---------------------------------------------------------------------------------
		public static bool IsUserAlreadyExists(string login)
		{
			bool ans = false;

			SqlOperation sql = new SqlOperation("user", SqlOperationMode.Select_Scalar);
			sql.Where("login", login);
			
			if(0 < sql.GetCountId())
				ans = true;

			return ans;
		}

//---------------------------------------------------------------------------------
// Login --------------------------------------------------------------------------
//---------------------------------------------------------------------------------
		public static int Login(string userName, string pass)
		{
			int IdData = 0;

			SqlOperation sql = new SqlOperation("user", SqlOperationMode.Select);
			sql.Fields("id");
			sql.Where("login", userName);
			sql.Where("password", pass);
			sql.Commit();

			if(sql.Read())
				IdData = sql.Result_Int("id");

			return IdData;
		}

		public static int LoginActive(string userName, string pass)
		{
			User user = Cache.GetUser().Where( u => u.login.Trim().ToLower() == userName.Trim().ToLower() && u.password == pass && u.active)?.FirstOrDefault();
			int userId = 0;
			if ( user != null ) userId = user.id;

			return userId;
		}
        
        public static User LoginByMD5(string UserName, string Password_Md5)
        {
            User user = GetUser(true, UserName);

            if (user.id == 0) return new User();

            byte[] buf;
            Crypt crypt = new Crypt();

            buf = System.Text.Encoding.ASCII.GetBytes(crypt.Decrypt(user.password));
            buf = System.Security.Cryptography.MD5.Create().ComputeHash(buf);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < buf.Length; i++)
                sb.Append(buf[i].ToString("X2"));

            if (Password_Md5.ToLower() != sb.ToString().ToLower())
                user = new User();

            return user;
        }


        //---------------------------------------------------------------------------------
        // recent -------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        public static void SaveDocumentRecent(SqlOperation sql, int userId, int id_doc)
		{
			object[] vals = new object[]
			{
				userId,
				id_doc,
				DateTime.Now
			};		
			
			// Delete existing records
			sql = SqlOperation.GetSqlOperation(sql, "user_recent_document", SqlOperationMode.Delete);
			sql.Where("id_doc", id_doc);
			sql.Where("id_user", userId);
			sql.Commit();

			// Insert new records
			sql.Reload("user_recent_document", SqlOperationMode.Insert);
			sql.Fields(tb_user_recent_document, vals);
			sql.Commit();
		}

//---------------------------------------------------------------------------------
// log ----------------------------------------------------------------------------
//---------------------------------------------------------------------------------
		public static void registerLog(int userId, en_UserEvents id_event, string obs)
		{
			string[] fields = new string[]
			{
				"id_user",
				"id_event",
				"obs",
				"date"
			};

			object[] vals = new object[]
			{
				userId,
				(int)id_event,
				obs,
				DateTime.Now
			};
			
			SqlOperation sql = new SqlOperation("user_log", SqlOperationMode.Insert);
			sql.Fields(fields, vals);

			new System.Threading.Thread(() => { sql.Commit(); }){Priority = System.Threading.ThreadPriority.Lowest}.Start();

			//sql.Commit();
		}


        #region  SpiderDocsForms.UserController

        public static void registerLog(en_UserEvents id_event, string obs)
        {
            registerLog(SpiderDocsApplication.CurrentUserId, id_event, obs);
        }

        #endregion


        //---------------------------------------------------------------------------------
	}
}
