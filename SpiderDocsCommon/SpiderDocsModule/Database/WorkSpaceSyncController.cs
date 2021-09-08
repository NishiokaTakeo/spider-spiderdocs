using System;
using System.Linq;
using System.Collections.Generic;
using Spider.Data;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{

    //---------------------------------------------------------------------------------
    public class WorkSpaceSyncController
    {
        static Logger logger = LogManager.GetCurrentClassLogger();



        /// <summary>
        /// Transfer a document to user workspace
        /// </summary>
        /// <param name="id_doc">documentId that transfers</param>
        /// <param name="id_user">What persion transfers for</param>
        public static int TransferToUserWorkSpace(int id_version, string hash,int id_user)
        {
            int id = 0;
            try
            { 
                SqlOperation sql = new SqlOperation("transferToUserWorkSpace", SqlOperationMode.StoredProcedure);
                sql.SetCommandParameter("id_version", id_version);
                sql.SetCommandParameter("id_user", id_user);
                sql.SetCommandParameter("hash", hash);

                sql.Field("id");

                sql.Commit();
                while (sql.Read())
                {
                    id = sql.Result_Int("id");
                }

                if (id <= 0)
                    logger.Error("[Sync] LinkID is -1. id_version:{0}, hash:{1}, id_user:{2}", id_version,hash,id_user);
            }
            catch(Exception ex)
            {
                logger.Error(ex);

            }
            return id;
        }


        /// <summary>
        /// Transfer a document to user workspace
        /// </summary>
        /// <param name="id_doc">documentId that transfers</param>
        /// <param name="id_user">What persion transfers for</param>
        public static void TransferToUserWorkSpaceBack(int id, SqlOperation sql = null)
        {
            sql = new SqlOperation("transferToUserWorkSpaceBack", SqlOperationMode.StoredProcedure) ;
            sql.SetCommandParameter("id", id);

            sql.Commit();
        }


        /// <summary>
        /// Save a document to user workspace
        /// </summary>
        /// <param name="wkspace"></param>
        public static int CreateUserWorkSpace(UserWorkSpace wkspace)
        {
            int insertedID = 0;
            try
            { 
			    SqlOperation sql = new SqlOperation("user_workspace", SqlOperationMode.Insert);

                sql.Field("id_user", wkspace.id_user);
                sql.Field("id_version", wkspace.id_version);
                sql.Field("filename", wkspace.filename);
                sql.Field("filedata", wkspace.filedata);
                sql.Field("filehash", wkspace.filehash);
                sql.Field("created_date", ToDbDateTime(wkspace.created_date));

                if( wkspace.lastaccess_date > DateTime.MinValue)
                    sql.Field("lastaccess_date", ToDbDateTime(wkspace.lastaccess_date));

                if( wkspace.lastmodified_date > DateTime.MinValue)
                    sql.Field("lastmodified_date", ToDbDateTime(wkspace.lastmodified_date));

                insertedID = Convert.ToInt32(sql.Commit());
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }
            return insertedID;

        }

        /// <summary>
        /// Records last file access date
        /// </summary>
        /// <param name="id"></param>
        public static void UpdateLastAccess(int id, DateTime utc )
        {
            SqlOperation sql = new SqlOperation("user_workspace", SqlOperationMode.Update);
            sql.Field("lastaccess_date", ToDbDateTime(utc));

            sql.Where("id", id);

            sql.Commit();
        }

        /// <summary>
        /// Records last file access date
        /// </summary>
        /// <param name="id"></param>
        public static void UpdateLastModified(int id, DateTime utc)
        {
            SqlOperation sql = new SqlOperation("user_workspace", SqlOperationMode.Update);
            sql.Field("lastmodified_date", ToDbDateTime(utc));

            sql.Where("id", id);

            sql.Commit();
        }

        public static bool Delete(int id)
        {

            SqlOperation sql = new SqlOperation("user_workspace", SqlOperationMode.Delete);
            sql.Where("id", id);
            try
            {
                sql.Commit();
                return true;
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                return false;
            }

        }

        public static UserWorkSpace GetBy(int id, bool filedata = false  )
        {
            UserWorkSpace ans = new UserWorkSpace();
            SqlOperation sql = new SqlOperation("user_workspace", SqlOperationMode.Select);
            sql.Fields("id","id_user","id_version","filename","filehash","created_date", "lastaccess_date", "lastmodified_date");

            if( filedata) sql.Field("filedata");

            sql.Where("id", id);

            sql.Commit();

            while(sql.Read())
            {
                ans.id = sql.Result<int>("id");
                ans.id_user = sql.Result<int>("id_user");
                ans.id_version = sql.Result<int>("id_version");
                ans.filename = sql.Result<string>("filename");
                //ans.filedata = sql.Result<byte[]>("filedata");
                ans.filehash = sql.Result<string>("filehash");
                ans.created_date = sql.Result<DateTime>("created_date");
                ans.lastaccess_date = sql.Result<DateTime>("lastaccess_date");
                ans.lastmodified_date = sql.Result<DateTime>("lastmodified_date");

                if (filedata)
                    ans.filedata = (byte[])sql.Result_Obj("filedata");
            }

            return ans;
        }


        async public static  System.Threading.Tasks.Task<UserWorkSpace> GetByAsync(int id, bool filedata = false  )
        {
            return await System.Threading.Tasks.Task.Run(() => WorkSpaceSyncController.GetBy(id, filedata));
        }

        public static List<UserWorkSpace> SearchBy(int id_user, bool filedata = false)
        {
            List<UserWorkSpace> ans = new List<UserWorkSpace>();
            SqlOperation sql = new SqlOperation("user_workspace", SqlOperationMode.Select);
            sql.Fields("id", "id_user", "id_version", "filename", "filehash", "created_date", "lastaccess_date", "lastmodified_date");

            if (filedata) sql.Field("filedata");

            sql.Where("id_user", id_user);
			sql.Where("filename", SqlOperationOperator.NOT_EQUAL , "");

			sql.OrderBy("lastmodified_date", Spider.Data.Sql.SqlOperation.en_order_by.Descent);

            sql.Commit();

            while (sql.Read())
            {
                var userWorkSpace = new UserWorkSpace();
                userWorkSpace.id = sql.Result<int>("id");
                userWorkSpace.id_user = sql.Result<int>("id_user");
                userWorkSpace.id_version = sql.Result<int>("id_version");
                userWorkSpace.filename = sql.Result<string>("filename");
                userWorkSpace.filehash = sql.Result<string>("filehash");
                userWorkSpace.created_date = sql.Result<DateTime>("created_date");
                userWorkSpace.lastaccess_date = sql.Result<DateTime>("lastaccess_date");
                userWorkSpace.lastmodified_date = sql.Result<DateTime>("lastmodified_date");

                if (filedata)
                    userWorkSpace.filedata = (byte[])sql.Result_Obj("filedata");

                ans.Add(userWorkSpace);
            }

            return ans;
        }

        public static void SyncUp(int id, byte[] filedate, string filehash, SqlOperation sql = null)
        {
            sql = new SqlOperation("user_workspace", SqlOperationMode.Update);

            sql.Field("filedata", filedate);
            sql.Field("filehash", filehash);
            sql.Field("lastmodified_date", ToDbDateTime(DateTime.UtcNow));

            sql.Where("id", id);

            sql.Commit();
            //sql.Commit();//
        }

        public static void Rename(int id, string reName, DateTime lastmodified_date)
        {
            var sql = new SqlOperation("user_workspace", SqlOperationMode.Update);

            sql.Field("filename", reName);
            sql.Field("lastmodified_date",ToDbDateTime(lastmodified_date));

            sql.Where("id", id);

            sql.Commit();
        }

        public static int BackUp(UserWorkSpace backup)
        {
            try { 

			    SqlOperation sql = new SqlOperation("user_workspace_deleted", SqlOperationMode.Insert);

                sql.Field("id", backup.id);
                sql.Field("id_user", backup.id_user == 0 ? SpiderDocsApplication.CurrentUserId: backup.id_user);
                sql.Field("id_version", backup.id_version);
                sql.Field("filename", backup.filename);
                sql.Field("filedata", backup.filedata ?? new byte[] { });
                sql.Field("filehash", backup.filehash);
                sql.Field("created_date", ToDbDateTime(backup.created_date));

                if(backup.lastaccess_date > DateTime.MinValue)
                    sql.Field("lastaccess_date", ToDbDateTime(backup.lastaccess_date));

                if (backup.lastmodified_date > DateTime.MinValue)
                    sql.Field("lastmodified_date", ToDbDateTime(backup.lastmodified_date));

                int id = Convert.ToInt32(sql.Commit());

                return id;
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }
            
            return 0;
        }

        async public static System.Threading.Tasks.Task<int> BackUpAsync(UserWorkSpace backup)
        {
            return await System.Threading.Tasks.Task.Run(() => BackUp(backup));
        }

        /// <summary>
        /// There are duplicated the date with id_user = 0 when you syncup;
        /// </summary>
        public static void RemoveKnowissued()
        {
            SqlOperation sql = new SqlOperation("user_workspace_deleted", SqlOperationMode.Delete);

            sql.Where("id_user", 0);

            sql.Commit();
        }


        public static string ToDbDateTime(DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd hh:mm:ss");
        }

	}
}
