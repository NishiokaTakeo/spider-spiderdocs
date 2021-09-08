using System;
using System.Linq;
using System.Collections.Generic;
using Spider.Data;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public enum en_FolderPermissionMode
	{
		User = 0,
		Group
	}

	public enum en_MenuPermissionMode
	{
		Main = 0,
		Sub
	}

	public enum en_FolderPermission
	{
		Deny = -1,
		NoSetting,
		Allow,
		Both // Both should be treated same as Deny.
	}

    public enum en_NGroup
    {
        None = 0,
        User,
        Group
    }


    //---------------------------------------------------------------------------------
    public class PermissionController
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

		public static readonly int ADMIN_ID = 1;

		#region SpiderDocsForm

		// public static List<Folder> GetAssignedFolderToUser(bool only_edit_permitted_folders = false)
        // {
        //     return GetAssignedFolderToUser(SpiderDocsApplication.CurrentUserId, only_edit_permitted_folders).ToList();
        // }

        #endregion


        //---------------------------------------------------------------------------------
        // Permission for folders ---------------------------------------------------------
        //---------------------------------------------------------------------------------
        public static Dictionary<en_Actions, string> GetFolderPermissionTitles()
		{
			Dictionary<en_Actions, string> ans = new Dictionary<en_Actions, string>();

			SqlOperation sql = new SqlOperation("permission", SqlOperationMode.Select);
			sql.Fields("id", "permission");
			sql.OrderBy("sort", SqlOperation.en_order_by.Ascent);
			sql.Commit();

			while(sql.Read())
				ans.Add((en_Actions)sql.Result<int>("id"), sql.Result("permission"));

			return ans;
		}

        //---------------------------------------------------------------------------------
        public static bool CheckPermission(int id_folder, en_Actions action, int user_id = 0)
		{
            //Dictionary<en_Actions, en_FolderPermission> permissions = PermissionController.GetFolderPermissions(id_folder, user_id);
			if( user_id == 0) user_id = SpiderDocsApplication.CurrentUserId;

            //FolderPermission permissions = new Cache(user_id).GetFoldersPermissions(action).FirstOrDefault(x => x.FolderId == id_folder) ?? new FolderPermission(id_folder);
            Dictionary<en_Actions, en_FolderPermission> permissions = PermissionController.GetFolderPermissions(id_folder, user_id);

            if ( false == (permissions.ContainsKey(action) && permissions[action] == en_FolderPermission.Allow)  )
                logger.Warn("{0} doesn't have permission for {1} on {2}",user_id,action,id_folder);

            return permissions.ContainsKey(action) && permissions[action] == en_FolderPermission.Allow ;
        }

        /// <summary>
        /// Get an user or a group permissions
        /// </summary>
        /// <param name="id_folder"></param>
        /// <param name="id_groupOrUser"></param>
        /// <param name="mode"></param>
        /// <param name="GetBoth"></param>
        /// <returns></returns>
        public static Dictionary<en_Actions, en_FolderPermission> GetFolderPermission(int id_folder, int id_groupOrUser, en_FolderPermissionMode mode, bool GetBoth = false)
		{
			Dictionary<en_Actions, en_FolderPermission> ans = new Dictionary<en_Actions, en_FolderPermission>();

			if(id_groupOrUser == 0) return ans;

			string[] TableField = GetFunctionField(mode);

			SqlOperation sql = new SqlOperation("permission", SqlOperationMode.Function);
			sql.LeftJoin(string.Format("{0}(@id_folder) as fp", TableField[0]), "fp.id_permission = permission.id");
			sql.Fields("id_permission", "allow", "[deny]");

            //sql.Where("id_folder", id_folder);
            sql.Where(TableField[1], id_groupOrUser);
            sql.SetCommandParameter("id_folder", id_folder);
            sql.Distinct = true;

			sql.Commit();



            while (sql.Read())
			{
				en_FolderPermission vPer = PermissionByAllowDeny(sql.Result<bool>("allow"),sql.Result<bool>("deny"), GetBoth);

				ans.Add((en_Actions)sql.Result<int>("id_permission"), vPer);
			}

			return ans;
		}

        /// <summary>
        /// Get Folder's permissions
        /// </summary>
        /// <param name="id_folder">Folder ID that searches</param>
        /// <returns></returns>
        public static bool HasFolderPermissions(int id_folder)
        {
            SqlOperation sql = new SqlOperation("fnGetFolderPermissions(@id_folder)", SqlOperationMode.Function);            
            sql.Fields("id_folder");

            sql.SetCommandParameter("id_folder", id_folder);
            
            sql.Commit();

            while (sql.Read())
            {
                return true;
            }

            return false;
        }

        //---------------------------------------------------------------------------------
        // Edit permission for folders ----------------------------------------------------
        //---------------------------------------------------------------------------------
        /// <summary>
        /// Add Permission. the permission means you can do such as check-in/out, read, send-email and so on in particular folder.</summary>
        /// <param name=""></param>
        /// <seealso cref="String">
        /// </seealso>
        public static void AddPermission(int id_folder, int id_GroupOrUser, en_FolderPermissionMode mode, Dictionary<en_Actions, en_FolderPermission> permissions)
		{
			string[] TableField = GetTableField(mode);

            DeletePermission(mode, id_folder,id_GroupOrUser);

            //List<int> FolderUserOrGroupIds = GetFolderLinkId(mode, id_folder, id);

            //int id_folder_group = 0;
            //int id_folder_user = 0;
   //         if (mode == en_FolderPermissionMode.User)
			//{
			//	id_folder_user = FolderUserOrGroupIds[0];
			//	DeletePermission(mode, id_folder_user);

			//}else
			//{
			//	id_folder_group = FolderUserOrGroupIds[0];
			//	DeletePermission(mode, id_folder_group);
			//}

			foreach(KeyValuePair<en_Actions, en_FolderPermission> permission in permissions)
			{
				bool allow = false;
				bool deny = false;

				switch(permission.Value)
				{
					case en_FolderPermission.Both:
						allow = true;
						deny = true;
						break;

					case en_FolderPermission.Allow:
						allow = true;
						break;

					case en_FolderPermission.Deny:
						deny = true;
						break;
				}

				string[] fields = new string[]
				{
                    "id_folder",
                    TableField[1],
					"id_permission",
					"allow",
					"[deny]"
				};

				object[] vals = new object[]
				{
                    id_folder,
                    id_GroupOrUser,
					(int)permission.Key,
					allow ? 1 : 0,
					deny ? 1 : 0
				};

				SqlOperation sql = new SqlOperation(TableField[0], SqlOperationMode.Insert);
				sql.Fields(fields, vals);
				sql.Commit();
			}

        }

        ///// <summary>
        ///// Grant Full Permission to folder
        ///// </summary>
        ///// <param name="folder_id"></param>
        public static void GrantFullPermission(int folder_id)
        {
            //PermissionController.AssignFolder(en_FolderPermissionMode.Group, folder_id, Group.ALL);

            en_FolderPermissionMode mode = en_FolderPermissionMode.Group;

            Dictionary<en_Actions, en_FolderPermission> permissions = new Dictionary<en_Actions, en_FolderPermission>();

            foreach (en_Actions actn in Enum.GetValues(typeof(en_Actions)))
            {
                permissions.Add((en_Actions)actn, en_FolderPermission.Allow);
            }

            PermissionController.AddPermission(folder_id, Group.ALL, mode, permissions);

             Cache.RemoveUsersCache(Cache.en_UKeys.DB_GetAssignedFolderToUser);
        }

        //      //---------------------------------------------------------------------------------
        //      public static void UpdatePermission(int id, int check, int alowOrDeny)
        //{
        //	SqlOperation sql = new SqlOperation("document_permission", SqlOperationMode.Update);

        //	if(alowOrDeny == 1) sql.Field("allow", check);
        //	if(alowOrDeny == 2) sql.Field("[deny]", check);

        //	sql.Where("id", id);
        //	sql.Commit();
        //}

        /// <summary>
        /// Delete permissions
        /// </summary>
        /// <param name="mode">User or Group</param>
        /// <param name="id_GroupOrUser">id of an user or a group</param>
        static void DeletePermission(en_FolderPermissionMode mode, int idFolder, int id_GroupOrUser)
		{
            if (idFolder <= 0) return;

            string[] TableField = GetTableField(mode);

            SqlOperation sql = new SqlOperation(TableField[0], SqlOperationMode.Delete);

			sql.Where(TableField[1], id_GroupOrUser);
            sql.Where("id_folder", idFolder);

			sql.Commit();
		}

        /// <summary>
        /// Delete all permissions on a folder.
        /// </summary>
        /// <param name="idFolder"></param>
        public static void DeleteAllPermission(int idFolder)
        {
            if (idFolder <= 0) return;

            string[] TableField = GetTableField(en_FolderPermissionMode.Group);

            SqlOperation sql = new SqlOperation(TableField[0], SqlOperationMode.Delete);

            sql.Where("id_folder", idFolder);

            sql.Commit();



            TableField = GetTableField(en_FolderPermissionMode.User);

            sql = new SqlOperation(TableField[0], SqlOperationMode.Delete);

            sql.Where("id_folder", idFolder);

            sql.Commit();
        }

        /// <summary>
        /// Delete all permissions on a folder.
        /// </summary>
        /// <param name="idFolder"></param>
        public static void CopyPermissions(int fromFolderId, int toFolderId)
        {
            SqlOperation sql = new SqlOperation("copyPermissions", SqlOperationMode.StoredProcedure);
            sql.SetCommandParameter("fromFolderId", fromFolderId);
            sql.SetCommandParameter("toFolderId", toFolderId);

            sql.Commit();
        }

        //---------------------------------------------------------------------------------
        // Folder assignment to users and groups ------------------------------------------
        //---------------------------------------------------------------------------------
        public static List<int> GetAssignedFolderToGroup(List<int> id_group)
		{
			string[] TableField = GetTableField(en_FolderPermissionMode.Group);
			List<int> ans = new List<int>();

			SqlOperation sql = new SqlOperation(TableField[0], SqlOperationMode.Select);
			sql.Fields("id_folder");
			sql.Where_In(TableField[1], id_group.ToArray());

			sql.Commit();

			while(sql.Read())
				ans.Add(Convert.ToInt32(sql.Result_Obj("id_folder")));

			return ans;
		}

//---------------------------------------------------------------------------------
		///// <summary>
		///// Get Folder's ids I can read
		///// </summary>
		///// <param name="user_id"></param>
		///// <param name="only_edit_permitted_folders"></param>
		///// <returns></returns>
		//public static List<Folder> GetAssignedFolderToUserCache(int user_id, bool only_edit_permitted_folders = false)
		//{
		//	List<Folder> cached = new Cache(user_id).GetAssignedFolderToUser();

		//	if(only_edit_permitted_folders)
		//		cached = FilterByPermission(user_id,cached,en_Actions.CheckIn_Out);

		//	return cached;
		//}


// 		/// <summary>
// 		/// Get Folder's ids I can read
// 		/// </summary>
// 		/// <param name="user_id"></param>
// 		/// <param name="only_edit_permitted_folders"></param>
// 		/// <returns></returns>
// 		public static List<Folder> GetAssignedFolderToUser(int user_id/*, bool only_edit_permitted_folders = false*/)
// 		{
// 			List<Folder> folders = new List<Folder>();
// 			List<int> folder_ids = new List<int>();
// 			SqlOperation sql;

// 			sql = new SqlOperation("view_folder", SqlOperationMode.Select);

// 			sql.Fields("id");
// 			sql.Fields("document_folder");
// 			sql.Fields("id_parent");
// 			sql.Where("id_user", user_id);

// 			sql.Option("FORCE ORDER");

//             sql.Commit();

// 			while(sql.Read())
// 			{
// 				folders.Add(new Folder(int.Parse(sql["id"]), sql["document_folder"], int.Parse(sql["id_parent"])));
// 				//folder_ids.Add(int.Parse(sql["id"]));
// 			}

// 			folders = folders.OrderBy( f=> f.id_parent).ThenBy(f => f.document_folder).ToList();

// 			//if(only_edit_permitted_folders)
// 			//{
// 			//	folders = FilterByPermission(user_id,folders,en_Actions.CheckIn_Out);

// 			//	/*
//    //             var permissions = GetFoldersPermissions(user_id, en_Actions.CheckIn_Out);

//    //             for (int i = folders.Count() - 1; 0 <= i; i--)
// 			//	{
// 			//		try
// 			//		{
//    //                     var  has = permissions.First(x => x.Key == folders[i].id).Value ;

//    //                     if (has.First().Value != en_FolderPermission.Allow)
// 			//				throw new Exception();

// 			//		}catch
// 			//		{
// 			//			folders.RemoveAt(i);
// 			//		}
// 			//	}
// 			//	*/
// 			//}

// 			return folders;
// 		}


        /// <summary>
        /// Get Folder's ids I can read
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="only_edit_permitted_folders"></param>
        /// <returns></returns>
        public static List<Folder> GetAssignedFolderToUser(int user_id)
        {
            List<Folder> folders = new List<Folder>();
            SqlOperation sql;

            sql = new SqlOperation("document_folder", SqlOperationMode.Select);

            sql.Fields("id", "document_folder", "id_parent");
            sql.Where("archived", 0);

            sql.OrderBy("id_parent", Spider.Data.Sql.SqlOperation.en_order_by.Ascent);
            sql.OrderBy("document_folder", Spider.Data.Sql.SqlOperation.en_order_by.Ascent);

            //sql.Option("FORCE ORDER");

            sql.Commit();

            while (sql.Read())
            {
                folders.Add(new Folder(int.Parse(sql["id"]), sql["document_folder"], int.Parse(sql["id_parent"])));
            }

            //folders = folders.OrderBy(f => f.id_parent).ThenBy(f => f.document_folder).ToList();

            return folders;
        }

        public static List<Folder> GetAssignedFolderLevel2(int id_parent, int user_id, en_Actions id_permission = 0)
        {
            List<Folder> folders = new List<Folder>();

            SqlOperation sql = new SqlOperation("folderDrillDownL2", SqlOperationMode.StoredProcedure);

            sql.SetCommandParameter("id_parent", id_parent);
            sql.SetCommandParameter("id_user", user_id);
            sql.Fields("id", "document_folder", "id_parent");
            sql.Commit();

            while (sql.Read())
            {
                folders.Add(new Folder(int.Parse(sql["id"]), sql["document_folder"], int.Parse(sql["id_parent"])));
            }

            return folders;
        }

        public static List<Folder> GetAssignedFolderLevel1(int id_parent, int user_id, en_Actions permission = en_Actions.None, bool archived = false)
        {
            List<Folder> folders = new List<Folder>();

            SqlOperation sql = new SqlOperation("folderDrillDownL1", SqlOperationMode.StoredProcedure);

            sql.SetCommandParameter("id_parent", id_parent);
            sql.SetCommandParameter("id_user", user_id);
            sql.SetCommandParameter("archived", archived);

            if ((int)permission > 0 )
                sql.SetCommandParameter("id_permission", (int)permission);

            sql.Fields("id", "document_folder", "id_parent");
            sql.Commit();

            while (sql.Read())
            {
                folders.Add(new Folder(int.Parse(sql["id"]), sql["document_folder"], int.Parse(sql["id_parent"])));
            }

            return folders;
        }

        public static List<Folder> GetArchiveFolderLevel1(int id_parent)
        {
            List<Folder> folders = new List<Folder>();

            SqlOperation sql = new SqlOperation("archiveFolderDrillDownL1", SqlOperationMode.StoredProcedure);

            sql.SetCommandParameter("id_parent", id_parent);
            sql.Fields("id", "document_folder", "id_parent");
            sql.Commit();

            while (sql.Read())
            {
                folders.Add(new Folder(int.Parse(sql["id"]), sql["document_folder"], int.Parse(sql["id_parent"])));
            }

            return folders;
        }



        public static List<Folder> drillUpfoldersby(int id, int user_id,en_Actions permission = en_Actions.OpenRead, bool archived = false)
        {
            logger.Debug("id:{0},user_id:{1},permission:{2}", id, user_id, permission);

            List<Folder> folders = new List<Folder>();

            SqlOperation sql = new SqlOperation("drillUpfoldersby", SqlOperationMode.StoredProcedure);

            sql.SetCommandParameter("id", id);
            sql.SetCommandParameter("id_user", user_id);
            sql.SetCommandParameter("id_permission", (int)permission);
            sql.SetCommandParameter("archived", (bool)archived);

            sql.Fields("id", "document_folder", "id_parent");
            sql.Commit();

            while (sql.Read())
            {
                logger.Debug("id:{0}, document_folder:{1}, id_parent:{2}", sql["id"], sql["document_folder"], sql["id_parent"]);
                folders.Add(new Folder(int.Parse(sql["id"]), sql["document_folder"], int.Parse(sql["id_parent"])));
            }

            return folders;
        }

        //      /// <summary>
        //      ///
        //      /// </summary>
        //      /// <param name="user_id"></param>
        //      /// <param name="folders"></param>
        //      /// <param name="action"></param>
        //      /// <returns>Sorted folders you have permission.</returns>
        //public static List<Folder> FilterByPermission(int user_id, List<Folder> folders, en_Actions action)
        //{
        //          folders = folders.OrderBy(x => x.id).ToList();

        //          List<Folder> viewes = new List<Folder>();

        //          List<FolderPermission> permissions = new Cache(user_id).GetFoldersPermissions(action).OrderBy(x => x.FolderId).ToList();



        //          int _last_idx = permissions.Count-1;
        //          for (int i = folders.Count - 1; 0 <= i; i--)
        //	{
        //                  for(int j = _last_idx ; 0 <= j; j--)
        //                  {
        //                      _last_idx = j;

        //                      var per = permissions[j];

        //                      if (per.FolderId < folders[i].id) break;

        //                      var has = per.FolderId == folders[i].id ?
        //                                          per:
        //                                          new FolderPermission();


        //                      if (!has.ContainsKey(action)) continue;

        //                      if (has[action] == en_FolderPermission.Allow) {
        //                          viewes.Add(folders[i]);
        //                          break;
        //                      }
        //              }
        //          }

        //	return viewes;
        //}

        /// <summary>
        ///
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="id_folder"></param>
        /// <returns></returns>
        public static Folder GetInheritanceFolder(int id_folder)
        {
            Folder ans = new Folder();

            SqlOperation sql = new SqlOperation( "fnFindInheritedFolderFrom( @id_folder ) ihr", SqlOperationMode.Function);
            sql.Fields("f.id","f.document_folder","f.id_parent","f.archived");

            sql.InnerJoin("[document_folder] f", "f.id = ihr.id_folder");
            sql.SetCommandParameter("id_folder", id_folder);

            sql.Commit();

            while (sql.Read())
            {
                ans.id = sql.Result_Int("id");
                ans.document_folder = sql.Result<string>("document_folder");
                ans.id_parent = sql.Result_Int("id_parent");
                ans.archived = sql.Result<bool>("archived");
            }

            return ans;
        }


        //---------------------------------------------------------------------------------
        public static List<int> GetAssignedUserOrGroupToFolder(en_FolderPermissionMode mode, int id_folder)
        {
            string[] TableField = GetFunctionField(mode);
            List<int> ans = new List<int>();

            SqlOperation sql = new SqlOperation( string.Format("{0}(@id_folder)",TableField[0]), SqlOperationMode.Function);
            sql.Fields(TableField[1]);
            sql.GroupBy(TableField[1]);
            sql.SetCommandParameter("id_folder", id_folder);

            sql.Commit();

            while (sql.Read())
                ans.Add(sql.Result_Int(TableField[1]));

            return ans;
        }

        //public static List<int> GetAssignedUserOrGroupToFolder(en_FolderPermissionMode mode, int id_folder)
        //{
        //    string[] TableField = GetTableField(mode);
        //    List<int> ans = new List<int>();

        //    SqlOperation sql = new SqlOperation(TableField[0], SqlOperationMode.Select);
        //    sql.Fields(TableField[1]);
        //    sql.Where("id_folder", id_folder);

        //    sql.Commit();

        //    while (sql.Read())
        //        ans.Add(sql.Result_Int(TableField[1]));

        //    return ans;
        //}
        //---------------------------------------------------------------------------------
        public static void AssignFolder(en_FolderPermissionMode mode, int id_folder, int id_GroupOrUser)
		{
			string[] TableField = GetTableField(mode);

			SqlOperation sql = new SqlOperation(TableField[0], SqlOperationMode.Insert);
			sql.Field("id_folder", id_folder);
			sql.Field(TableField[1], id_GroupOrUser);
			sql.Commit();
		}

//---------------------------------------------------------------------------------
		public static void DeleteAssignedFolder(en_FolderPermissionMode mode, int id_folder, int id_GroupOrUser)
		{
            string[] TableField = GetTableField(mode);

            //int wrk = GetFolderLinkId(mode, id_folder, id)[0];
            DeletePermission(mode, id_folder,id_GroupOrUser);

		}

        //---------------------------------------------------------------------------------
        // static List<int> GetFolderLinkId(en_FolderPermissionMode mode, int id_folder, params int[] ids)
        // {
        //     string[] TableField = GetTableField(mode);
        //     List<int> ans = new List<int>();

        //     SqlOperation sql = new SqlOperation(TableField[0], SqlOperationMode.Select);
        //     sql.Fields("id");
        //     sql.Where("id_folder", id_folder);

        //     if (0 < ids.Length)
        //         sql.Where_In(TableField[1], ids);

        //     sql.Commit();

        //     while (sql.Read())
        //         ans.Add(Convert.ToInt32(sql.Result_Obj("id")));

        //     return ans;
        // }

        //---------------------------------------------------------------------------------
        static string[] GetTableField(en_FolderPermissionMode mode)
        {
            string[] ans = new string[2];

            if (mode == en_FolderPermissionMode.User)
            {
                ans[0] = "permission_folder_user";
                ans[1] = "id_user";
            }
            else
            {
                ans[0] = "permission_folder_group";
                ans[1] = "id_group";
            }

            return ans;
        }

        static string[] GetFunctionField(en_FolderPermissionMode mode)
        {
            string[] ans = new string[2];

            if (mode == en_FolderPermissionMode.User)
            {
                ans[0] = "fnFolderUsersPermissions";
                ans[1] = "id_user";
            }
            else
            {
                ans[0] = "fnFolderGroupsPermissions";
                ans[1] = "id_group";
            }

            return ans;
        }
        //---------------------------------------------------------------------------------
        // Permission for menu items ------------------------------------------------------
        //---------------------------------------------------------------------------------
        public static List<int> GetMenuPermission(en_MenuPermissionMode mode, int IdPermission)
		{
			List<int> ans = new List<int>();

			string[] TableField = GetTableField(mode);
			SqlOperation sql = new SqlOperation(TableField[0], SqlOperationMode.Select);
			sql.Field(TableField[1]);
			sql.Where("id_permission", IdPermission);
			sql.Commit();

			while(sql.Read())
				ans.Add(sql.Result_Int(TableField[1]));

			return ans;
		}

//---------------------------------------------------------------------------------
		public static List<string> GetPermittedMenuTitles(en_MenuPermissionMode mode, params int[] MenuIds)
		{
			string[] TableField = GetTableField(mode);
			List<string> ans = new List<string>();

			SqlOperation sql = new SqlOperation(TableField[2], SqlOperationMode.Select);
			sql.Field(TableField[3]);

			if(0 < MenuIds.Length)
				sql.Where_In("id", MenuIds);

			sql.Commit();

			while(sql.Read())
				ans.Add(sql.Result(TableField[3]));

			return ans;
		}

//---------------------------------------------------------------------------------
		public static void AddMenuPermission(en_MenuPermissionMode mode, int id_permission, int id_menu)
		{
            string[] TableField = GetTableField(mode);
            SqlOperation sql = new SqlOperation(TableField[0], SqlOperationMode.Insert);

            sql.Field("id_permission", id_permission);
            sql.Field(TableField[1], id_menu);
            sql.Commit();
        }

//---------------------------------------------------------------------------------
		public static void DeleteMenuPermission(en_MenuPermissionMode mode, int id_permission, int id_menu)
		{
            string[] TableField = GetTableField(mode);
            SqlOperation sql = new SqlOperation(TableField[0], SqlOperationMode.Delete);

            sql.Where("id_permission", id_permission);
            sql.Where(TableField[1], id_menu);
            sql.Commit();
        }

//---------------------------------------------------------------------------------
		static string[] GetTableField(en_MenuPermissionMode mode)
		{
			string[] ans = new string[4];

			if(mode == en_MenuPermissionMode.Main)
			{
				ans[0] = "system_permission_menu";
				ans[1] = "id_menu";
				ans[2] = "system_menu";
				ans[3] = "menu_internal_name";

			}else
			{
				ans[0] = "system_permission_submenu";
				ans[1] = "id_submenu";
				ans[2] = "system_submenu";
				ans[3] = "submenu_internal_name";
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public static string GetMenuPermissionGroupName(int IdPermission)
		{
			Dictionary<int, string> ans = GetMenuPermissionGroupNames(IdPermission);

			if(0 < ans.Count)
				return ans[IdPermission];
			else
				return "";
		}

		public static Dictionary<int, string> GetMenuPermissionGroupNames(params int[] IdPermission)
		{
			Dictionary<int, string> ans = new Dictionary<int, string>();

			SqlOperation sql = new SqlOperation("system_permission_level", SqlOperationMode.Select);
			sql.Field("id");
			sql.Field("permission");

			if(0 < IdPermission.Length)
				sql.Where_In("id", IdPermission);

			sql.Commit();

			while(sql.Read())
				ans.Add(sql.Result<int>("id"), sql.Result("permission"));

			return ans;
		}

//---------------------------------------------------------------------------------
		public static bool IsMenuPermissionUsed(int id_permission)
		{
			bool ans = false;

			SqlOperation sql = new SqlOperation("user", SqlOperationMode.Select_Scalar);
			sql.Where("id_permission", id_permission);

			if(0 < sql.GetCountId())
				ans = true;

			return ans;
		}

        public static Dictionary<en_Actions, en_FolderPermission> GetFolderPermissions(int id_folder, int user_id)
        {

            Dictionary<en_Actions, en_FolderPermission> permissions = new Dictionary<en_Actions, en_FolderPermission>();

            SqlOperation sql = new SqlOperation("folderPermissionByUser", SqlOperationMode.StoredProcedure);

            sql.SetCommandParameter("id_folder", id_folder);
            sql.SetCommandParameter("id_user", user_id);
            sql.Fields("id_permission", "allow");
            sql.Commit();
            while (sql.Read())
            {
                en_FolderPermission vPer = PermissionByAllowDeny(sql.Result<bool>("allow"),false);
                permissions.Add((en_Actions)sql.Result<int>("id_permission"), vPer);

                if (sql.Result<bool>("allow") == false && logger.IsDebugEnabled) logger.Debug("[Permission Deny] id_folder:{0}, user_id:{1}, permission={2}, allow={3}", id_folder, user_id, sql.Result<int>("id_permission"), sql.Result<bool>("allow"));
            }

			FillDeny(ref permissions);

            return permissions;
        }

        public static bool hasFoldersPermissionByUser(int[] id_folder, int user_id,en_Actions permission = 0)
        {
            SqlOperation sql = new SqlOperation("hasFoldersPermissionByUser", SqlOperationMode.StoredProcedure);

            sql.SetCommandParameter("id_folders", string.Join(",",id_folder));
            sql.SetCommandParameter("id_user", user_id);
            sql.SetCommandParameter("permission", (int)permission);
            sql.Fields("ans");
            sql.Commit();
            while (sql.Read())
            {
                return sql.Result<bool>("ans");
            }

            return false;
        }
        static void FillDeny(ref Dictionary<en_Actions, en_FolderPermission> permissions)
		{
			foreach (en_Actions per in Enum.GetValues(typeof(en_Actions)))
			{
                if (per >= en_Actions.Max) continue;

				en_FolderPermission actualValue;
				if( !permissions.TryGetValue(per, out actualValue) )
				{
					permissions[per] = en_FolderPermission.Deny;
				}
			}
		}

        static public bool IsFeatureEnabled(string feature)
        {
            int IdPermission = UserController.GetUser(false, SpiderDocsApplication.CurrentUserId).id_permission;
            string name = GetMenuPermissionGroupName(IdPermission);

            if (name.Trim() == "Administrators") return true;

            List<int> SubIds = PermissionController.GetMenuPermission(en_MenuPermissionMode.Sub, IdPermission);

            if (SubIds.Count == 0) return false;

            List<string> arraySubMenus = PermissionController.GetPermittedMenuTitles(en_MenuPermissionMode.Sub, SubIds.ToArray());

            if (arraySubMenus.IndexOf(feature) > -1)
                return true;

            return false;
        }


        static en_FolderPermission PermissionByAllowDeny(bool allow, bool deny = false, bool GetBoth = false)
        {
            var allowStr = allow ? "1" : "0";
            var denyStr = deny ? "1" : "0";

            en_FolderPermission vPer = en_FolderPermission.Deny;

            switch (allowStr + denyStr)
            {
				// Allow
				case "10":
					vPer = en_FolderPermission.Allow;
					break;

				// Deny or Both
				case "11":
					if(GetBoth) // Both is only used for the permission setting form.
						vPer = en_FolderPermission.Both;
					else // Generally Both should be considered same as Deny as it is higher priority.
						vPer = en_FolderPermission.Deny;
					break;

				// Deny
				case "01":
					vPer = en_FolderPermission.Deny;
					break;

				// Not set
				default:
					vPer = en_FolderPermission.NoSetting;
					break;
            }

            return vPer;

        }

	}
}
