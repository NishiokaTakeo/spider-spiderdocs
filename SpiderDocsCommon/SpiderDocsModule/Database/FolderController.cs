using System;
using System.Linq;
using System.Collections.Generic;
using Spider.Data;
using NLog;
//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class FolderController
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

		public readonly static string table_name = "document_folder";
		public readonly static string[] fields =
		{
			"id",
			"document_folder",
			"id_parent",
            "archived"
        };

        readonly static string[] _fieldsArchived =
        {
            "id",
            "document_folder",
            "id_parent"
        };

        /// <summary>
        /// Get Folder by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Folder GetFolder(int id)
		{
			return GetFolders(id).FirstOrDefault();
		}

		public static List<Folder> GetFolders(params int[] ids)
		{
			List<Folder> ans = new List<Folder>();
			SqlOperation sql = new SqlOperation(table_name, SqlOperationMode.Select);
			sql.Fields(fields);

			if(0 < ids.Length)
				sql.Where_In("id", ids);

            sql.OrderBy("id_parent", Spider.Data.Sql.SqlOperation.en_order_by.Ascent);
            sql.OrderBy("document_folder", Spider.Data.Sql.SqlOperation.en_order_by.Ascent);

            sql.Commit();

			while(sql.Read())
			{
				Folder wrk = new Folder();

				wrk.id = sql.Result_Int("id");
				wrk.document_folder = sql.Result("document_folder");
				wrk.id_parent = sql.Result_Int("id_parent");
                wrk.archived = sql.Result<bool>("archived");
                ans.Add(wrk);
			}

			return ans;			
		}


        public static int CountFolders()
        {
            SqlOperation sql = new SqlOperation(table_name, SqlOperationMode.Select_Scalar);

            return sql.GetCountId();
        }


        //---------------------------------------------------------------------------------
        public static bool IsUniqueFolderName(string folderName, int id_parent)
		{
            return (FindBy(folderName, id_parent)?.id <= 0);

   //         //bool ans = false;
   //         SqlOperation sql = new SqlOperation("document_folder", SqlOperationMode.Select_Scalar);
			//sql.Where("document_folder", folderName);
   //         sql.Where("id_parent",id_parent);

   //         return (sql.GetCountId() <= 0);
            
   //         /*
			//if(0 < sql.GetCountId())
			//	ans = true;

			//return ans;
   //         */
		}

        public static Folder FindBy(string name, int id_parent)
        {
            SqlOperation sql = new SqlOperation(table_name, SqlOperationMode.Select);
            sql.Fields(fields);

            sql.Where("document_folder", name?.Trim());
            sql.Where("id_parent", id_parent);

            sql.Commit();

            Folder wrk = new Folder();
            while (sql.Read())
            {               
                wrk.id = sql.Result_Int("id");
                wrk.document_folder = sql.Result("document_folder");
                wrk.id_parent = sql.Result_Int("id_parent");
                wrk.archived = sql.Result<bool>("archived");
            }

            return wrk;
        }

        /// <summary>
        /// Search Folders by name
        /// </summary>
        /// <param name="name">will be replaced to '%name%' in query</param>
        /// <returns></returns>
        public static List<Folder> SearchBy(string name, int length = 0)
        {
            SqlOperation sql = new SqlOperation(table_name, SqlOperationMode.Select);
            sql.Fields("document_folder");
            
            sql.Where("document_folder", SqlOperationOperator.LIKE_END, name.Trim());
            sql.Where("archived", 0);
            if(length > 0 ) sql.Where(" LEN(document_folder) <= " + length.ToString());

            sql.Distinct = true;
            sql.Commit();

            List<Folder> ans = new List<Folder>();
            
            while (sql.Read())
            {
                Folder wrk = new Folder();
                //wrk.id = sql.Result_Int("id");
                wrk.document_folder = sql.Result("document_folder");

                ans.Add(wrk);
            }

            return ans;
        }


        /// <summary>
        /// Update Parent Folder
        /// </summary>
        /// <param name="id_chld"></param>
        /// <param name="id_prnt"></param>
        public static void UpdateParent(int id_chld , int id_prnt )
		{

			Folder chld = GetFolder(id_chld);

			if( chld == null ) return;

			SqlOperation sql = new SqlOperation("document_folder", SqlOperationMode.Update);

			sql.Field("id_parent", id_prnt);
			sql.Where("id", chld.id);

			sql.Commit();
		}

        /// <summary>
        /// Save or Update or nothing if exists
        /// </summary>
        /// <param name="fld">A folder which will be conducted</param>
        public static int Save(Folder fld)
        {
            SqlOperation sql;
            bool isNew = fld.id == 0;
            if (isNew)
                sql = new SqlOperation("document_folder", SqlOperationMode.Insert);
            else
            {
                sql = new SqlOperation("document_folder", SqlOperationMode.Update);
                sql.Where("id", fld.id);    
            }

            sql.Field("document_folder", fld.document_folder);
            sql.Field("id_parent", fld.id_parent);
            sql.Field("archived", fld.archived);

            if (isNew) { 
                fld.id = Convert.ToInt32(sql.Commit());
            }
            else
            {
                sql.Commit();                
            }

            return fld.id;
        }

        /// <summary>
        /// Archive or un Archive folder
        /// </summary>
        /// <param name="fld">A folder which will be conducted</param>
        public static void ChangeArchiveStatus(int id, bool archive)
        {
            SqlOperation sql;
            sql = new SqlOperation("document_folder", SqlOperationMode.Update);
            sql.Where("id", id);

            sql.Field("archived", archive);

            sql.Commit();
        }


        /// <summary>
        /// Delete Folder
        /// </summary>
        /// <param name="id"></param>
        public static bool Delete(int id)
		{			

            bool ans = true;
            Folder folder = new Folder();

            SqlOperation sql = new SqlOperation("document_folder", SqlOperationMode.Select);
            try
            {

                sql.Fields(fields);
                sql.Where("id", id);
                sql.Commit();
               
                
                while (sql.Read())
                {
                    folder.id = Convert.ToInt32(sql.Result_Obj("id"));
                    folder.document_folder = sql.Result_Obj("document_folder").ToString();
                    folder.id_parent = Convert.ToInt32(sql.Result_Obj("id_parent"));                    
                }
                
                if( folder.id == 0  ) return true;

                logger.Info("A folder is rmeoved :{0}", folder.id);
                if(logger.IsDebugEnabled) logger.Debug("{0}", Newtonsoft.Json.JsonConvert.SerializeObject(folder));

                object[] vals = new object[]
                {
                    folder.id,
                    folder.document_folder,
                    folder.id_parent
                };

                sql = new SqlOperation();

                sql.BeginTran();

                sql = SqlOperation.GetSqlOperation(sql,"document_folder_deleted", SqlOperationMode.Insert);
                
                sql.Fields(_fieldsArchived, vals);                    
                sql.Commit();

                sql = SqlOperation.GetSqlOperation(sql,"document_folder", SqlOperationMode.Delete);
                sql.Where("id", id);

                sql.Commit();
          
                sql.CommitTran();

                ans = true;

            }
            catch(Exception ex)
            {
                sql.RollBack();
                logger.Error(ex);
                ans = false;                
            }

            return ans;

        }

        /// <summary>
        /// Get children folders by 
        /// </summary>
        /// <param name="id_folder"></param>
        /// <returns></returns>
        public static List<Folder> DrillDownFoldersBy(int id_folder)
        {
            List<Folder> folders = new List<Folder>();

            SqlOperation sql = new SqlOperation("drillDownFoldersBy", SqlOperationMode.StoredProcedure);
            sql.SetCommandParameter("id_parent", id_folder);

            sql.Fields("id","document_folder","id_parent");
            sql.Commit();
                        
            while (sql.Read())
            {
                folders.Add(new Folder{
                    id = sql.Result<int>("id"),
                    document_folder = sql.Result<string>("document_folder"),
                    id_parent = sql.Result<int>("id_parent")
                });                
            }

            return folders;
        }
        
        public static bool isArhived(int id_folder)
        {
            SqlOperation sql = new SqlOperation("document_folder_archived", SqlOperationMode.Select_Scalar);
            
            sql.Where("id", id_folder);

            return (0 < sql.GetCountId());           
        }

        //---------------------------------------------------------------------------------
    }
}
