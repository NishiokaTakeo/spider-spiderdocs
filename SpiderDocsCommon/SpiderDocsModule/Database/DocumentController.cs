using System;
using System.Linq;
using System.Data.Common;
using System.Collections.Generic;
using System.Reflection;
using Spider.Data;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{

	public enum en_GetDocumentInfoMode
	{
		Document = 0,
		DocumentAndVersion,
		DocumentAndData,

		Max
	}

    //---------------------------------------------------------------------------------
    internal class DocumentController : DocumentController<Document>
    {
    }

    public class DocumentController<Document> where Document : SpiderDocsModule.Document, new()
    {
		static Logger logger = LogManager.GetCurrentClassLogger();

        static readonly string[] tb_view_document = new string[]
		{
			"id",
			"id_version",
			"id_user",
			"id_folder",
			"title",
			"extension",
			"name",
			"document_folder",
			"id_status",
			"version",
			"id_type",
			"type",
			"date",
			"id_review",
			"id_checkout_user",
			"CheckOutUser",
			"id_sp_status",
			"created_date"/*,
            "id_notification_group"*/
		};

        static readonly string[] tb_document = new string[]
		{
			"title",
			"extension",
			"id_status",
			"id_sp_status",
			"id_user",
			"id_folder",
			"id_type",
			"created_date",
			"id_latest_version",
			"id_review",
			"id_checkout_user"
		};

		static readonly string tb_document_version_id = "id_doc";
		static readonly string[] tb_document_version = new string[]
		{
			"filedata",
			"filesize",
			"version",
			"extension",
			"reason"
		};


        static readonly string[] tb_document_notification_group = new string[]
        {
            "id",
            "id_doc",
            "id_notification_group"
        };

        static readonly string[] tb_document_deleted = new string[]
		{
			"id_doc",
			"reason",
			"id_user",
			"date"
		};

		static readonly string[] tb_txt = new string[]
		{
			"id_doc",
			"id",
			"filetext"
		};

//---------------------------------------------------------------------------------
// Methods for getting document details -------------------------------------------
//---------------------------------------------------------------------------------
		public static List<Document> GetBy(int id_folder)
		{
			List<Document> docs = GetDocument(id_folder:new int[] { id_folder }, mode:en_GetDocumentInfoMode.Document);

			return docs;
		}


		// Return document and document version details
		public static Document GetDocument(int id_doc, int id_version = 0, int version = 0, bool data = false)
		{

			Document doc = null;
			en_GetDocumentInfoMode mode = en_GetDocumentInfoMode.DocumentAndVersion;

			if((0 < id_version) ||(0 < version))
				mode = en_GetDocumentInfoMode.Document;
			else if(data)
				mode = en_GetDocumentInfoMode.DocumentAndData;

			List<Document> docs = GetDocument(id_doc:new int[] { id_doc }, mode:mode);

			if(0 < docs.Count)
			{
				doc = docs[0];

				if(mode == en_GetDocumentInfoMode.Document)
				{
					if(0 < id_version)
						doc.id_version = id_version;
					else if(0 < version)
						doc.version = id_version;

					doc = (Document)GetDocumentVersionInfo((Document)doc, data);
				}
			}

			return doc;
		}

		// Return multiple documents details
		// Return all documents details in the document table when the argument is nothing
		public static List<Document> GetDocument(int[] id_doc = null, int[] id_folder = null, en_GetDocumentInfoMode mode = en_GetDocumentInfoMode.DocumentAndVersion)
		{
			List<Document> ans = new List<Document>();

			if((id_doc == null && id_folder == null) || (0 == id_doc?.Length  && 0 == id_folder?.Length)) return ans;


				SqlOperation sql = new SqlOperation("view_document", SqlOperationMode.Select);
				sql.Field("id");
				sql.Fields(tb_view_document);

				if ((id_doc != null) && (0 < id_doc.Length) ) sql.Where_In("id", id_doc);
				if ((id_folder != null) && (0 < id_folder.Length) ) sql.Where_In("id_folder", id_folder);

				sql.Commit();

				while(sql.Read())
				{
					Document wrk = new Document();

					wrk = DistributeDocumentInfo(sql, wrk);
					DocumentAttributeController.SetToDocument(wrk);

					switch(mode)
					{
					case en_GetDocumentInfoMode.DocumentAndVersion:
						wrk = GetDocumentVersionInfo(wrk, false);
						break;

					case en_GetDocumentInfoMode.DocumentAndData:
						wrk = GetDocumentVersionInfo(wrk, true);
						break;
					}

					ans.Add((Document)wrk);
				}

			return ans;
		}


        // Access to document_version table
        public static List<int> GetVersionIds(int id_doc)
		{
			List<int> versions = new List<int>();

			SqlOperation sql = new SqlOperation("document_version", SqlOperationMode.Select);
			sql.Fields(new string[]{"id","version"});

			sql.Where("id_doc", id_doc);
			sql.OrderBy("version", SqlOperation.en_order_by.Ascent);

			sql.Commit();

			while(sql.Read())
			{
                versions.Add(int.Parse(sql.Result("id")));
			}

			return versions;
		}


//---------------------------------------------------------------------------------
		// Access to document_version table
		static Document GetDocumentVersionInfo(Document doc, bool data)
		{
			List<string> fields = tb_document_version.ToList();

			SqlOperation sql = new SqlOperation("document_version", SqlOperationMode.Select);

			sql.Fields(data ? fields.ToArray() :  fields.Where(x => x != "filedata").ToArray());

			if(0 < doc.id_version)
			{
				sql.Where("id", doc.id_version);

			}else if(0 < doc.version)
			{
				sql.Where("id_doc", doc.id);
				sql.Where("version", doc.id_version);
			}

			sql.Commit();

			while(sql.Read())
			{
				doc.version = int.Parse(sql.Result("version"));
				doc.extension = sql.Result("extension");
				doc.reason = sql.Result("reason");
				doc.size = sql.Result<long>("filesize");

				if(data)
					doc.filedata = (byte[])sql.Result_Obj("filedata");
			}

			return doc;
		}

        static public bool IsUniqInFolder(int id_doc,string name,int id_folder)
        {
            return !GetBy(id_folder).Exists(x => x.title == name && x.id != id_doc);
        }

//---------------------------------------------------------------------------------
		// Distribute populated fields
		static Document DistributeDocumentInfo(SqlOperation sql, Document wrk)
		{
			wrk.id = Convert.ToInt32(sql.Result_Obj("id"));
			wrk.extension = sql.Result("extension");
			wrk.id_version = Convert.ToInt32(sql.Result_Obj("id_version"));
			wrk.id_docType = Convert.ToInt32(sql.Result_Obj("id_type"));
			wrk.title = sql.Result("title");
			wrk.id_folder = Convert.ToInt32(sql.Result_Obj("id_folder"));
			wrk.id_status = (en_file_Status)Convert.ToInt32(sql.Result_Obj("id_status"));
			wrk.id_sp_status = (sql.Result_Obj("id_sp_status") != DBNull.Value ? (en_file_Sp_Status)Convert.ToInt32(sql.Result_Obj("id_sp_status")) : en_file_Sp_Status.normal);
			wrk.id_user = Convert.ToInt32(sql.Result_Obj("id_user"));
			wrk.id_review = (sql.Result_Obj("id_review") != DBNull.Value ? Convert.ToInt32(sql.Result_Obj("id_review")) : -1);
			wrk.id_checkout_user = (sql.Result_Obj("id_checkout_user") != DBNull.Value ? Convert.ToInt32(sql.Result_Obj("id_checkout_user")) : -1);
			wrk.id_latest_version = sql.Result_Int("id_version");
			wrk.date = (sql.Result_Obj("created_date") != DBNull.Value ? sql.Result<DateTime>("created_date") : new DateTime());
            //wrk.id_notification_group = Convert.ToInt32(sql.Result_Obj("id_notification_group"));
            //if(sql.Result_Obj("CheckOutUser") != DBNull.Value) wrk.id_checkout_user = Convert.ToInt32(sql.Result_Obj("CheckOutUser"));
            //if(sql.Result_Obj("id_version") != DBNull.Value) wrk.id_latest_version = Convert.ToInt32(sql.Result_Obj("id_version"));

            return wrk;
		}

//---------------------------------------------------------------------------------
// Get and Save binary data of a document -----------------------------------------
//---------------------------------------------------------------------------------
		public static void LoadBinaryData(Document src)
		{
			src.filedata = GetBinaryData(id_doc: src.id, id_version: src.id_version, version: src.version);
		}

		static byte[] GetBinaryData(SqlOperation sql = null, int id_doc = 0, int id_version = 0, int version = 0)
		{
			sql = SqlOperation.GetSqlOperation(sql, "document_version", SqlOperationMode.Select);
			sql.Fields("filedata");

			if(0 < id_version)
			{
				sql.Where("id", id_version);

			}else if((0 < id_doc) && (0 < version))
			{
				sql.Where("id_doc", id_doc);
				sql.Where("version", version);
			}

			sql.Commit();
			sql.Read();

			return (byte[])sql.Result_Obj("filedata");
		}

//---------------------------------------------------------------------------------
// Text extraction for standalone mode --------------------------------------------
//---------------------------------------------------------------------------------
		public static List<Document> GetAllDocumentTxtInfo()
		{
			List<Document> ans = new List<Document>();

			SqlOperation sql = new SqlOperation("document_txt", SqlOperationMode.Select);
			sql.Fields(tb_txt);
			sql.Commit();

			while(sql.Read())
			{
				Document wrk = new Document();

				wrk.id = Convert.ToInt32(sql.Result_Obj("id_doc"));

				if(sql.Result_Obj("id") == DBNull.Value)
					wrk.id_version = 0;
				else
					wrk.id_version = Convert.ToInt32(sql.Result_Obj("id"));

				ans.Add(wrk);
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public static void UpdateDocumentText(Document obj, string text, SqlOperation sql = null)
		{
			if(sql == null)
				sql = new SqlOperation("document_txt", SqlOperationMode.Update);
			else
				sql.Reload("document_txt", SqlOperationMode.Update);

			sql.Field("filetext", text);
			sql.Field("id", obj.id_version);

			sql.Where("id_doc", obj.id);

			sql.Commit();
		}

//---------------------------------------------------------------------------------
		public static Document MergeMissingProperties(Document doc)
		{
            return MergeMissingProperties(new List<Document> { doc }).FirstOrDefault();
		}

        /// <summary>
        /// copy values from database if value isn't filled.
        /// </summary>
        /// <param name="docs"></param>
        /// <returns></returns>
		public static List<Document> MergeMissingProperties(List<Document> docs)
		{
			PropertyInfo[] pinfo = typeof(Document).GetProperties();
			List<Document> wrks = GetDocument(id_doc:docs.Select(a => a.id).ToArray());

            if (wrks == null || wrks.Count == 0) return docs;

			foreach(Document src in docs)
			{
				Document wrk = wrks.Find(a => a.id == src.id);

				foreach(PropertyInfo info in pinfo)
				{
					object src_val = info.GetValue(src, null);

					bool int_val = false;
					try
					{
						int tmp = (int)src_val;

						if(tmp <= 0)
							int_val = true;

					}catch{}

					if((src_val == null)
					|| int_val
					|| ((src_val.GetType() == typeof(List<DocumentAttribute>)) && (((List<DocumentAttribute>)src_val).Count <= 0))
					|| ((src_val.GetType() == typeof(DateTime)) && (((DateTime)src_val) == new DateTime())))
					{
						info.SetValue(src, info.GetValue(wrk, null), null);
					}
				}
			}

			return docs;
		}

//---------------------------------------------------------------------------------
		public static Dictionary<int,int> GetDocumentId(params int[] id_versions)
		{
			Dictionary<int,int> ans = new Dictionary<int,int>();

			if(0 < id_versions.Length)
			{
				SqlOperation sql = new SqlOperation("document_version", SqlOperationMode.Select);
				sql.Fields("id", "id_doc");
				sql.Where_In("id", id_versions);
                sql.OrderBy("id", Spider.Data.Sql.SqlOperation.en_order_by.Ascent);
				sql.Commit();

				while(sql.Read())
					ans.Add(sql.Result<int>("id"), sql.Result<int>("id_doc"));
			}

			return ans;
		}
        public static DocumentNotificationGroup GetDocNotificationGroup(int id_doc)
        {
            DocumentNotificationGroup ans = new DocumentNotificationGroup();
            SqlOperation sql = new SqlOperation("document_notification_group", SqlOperationMode.Select);

            sql.Fields(tb_document_notification_group);
            sql.Where("id_doc", id_doc);

            sql.Commit(); ;

            while (sql.Read())
            {
                DocumentNotificationGroup wrk = new DocumentNotificationGroup();

                wrk.id = sql.Result<int>("id");
                wrk.id_doc = sql.Result<int>("id_doc");
                wrk.id_notification_group = sql.Result<int>("id_notification_group");

                return wrk;
            }

            return ans;
        }
        internal static int ScheduleNotification(int userId, bool localDb, SpiderDocsModule.Document document)
        {
			return ScheduleNotificationAmendedController.Insert(document.id,document.version);
        }

        //---------------------------------------------------------------------------------
        // Methods for accesing database
        //---------------------------------------------------------------------------------
        internal static Document AddDocument(SqlOperation sql, bool localDb, Document objDoc)
		{
			sql = new SqlOperation("document", SqlOperationMode.Insert);

            objDoc = InsertOrUpdateDocument(sql, objDoc, true);

			// Just add record at this stage. Texts will be updated by background thread as it takes a long time.
			if(localDb)
			{
				object[] vals_txt = new object[]
				{
					objDoc.id,
					null,
					null
				};

				sql.Reload("document_txt", SqlOperationMode.Insert);
				sql.Fields(tb_txt, vals_txt);
				sql.Commit();
			}

			return objDoc;
		}

//---------------------------------------------------------------------------------
		internal static int AddVersion(SqlOperation sql, int userId, bool localDb, Document objDoc)
		{
			objDoc.reason = (objDoc.reason == null ? "" : objDoc.reason);

			object[] vals = new object[]
			{
				objDoc.filedata,
				objDoc.size,
				objDoc.version,
				objDoc.extension,
				objDoc.reason
			};

			sql = SqlOperation.GetSqlOperation(sql, "document_version", SqlOperationMode.Insert);
			sql.Field(tb_document_version_id, objDoc.id);
			sql.Fields(tb_document_version, vals);
			//sql.Commit();
            int insertedID = Convert.ToInt32(sql.Commit());

			//objDoc.id_version = GetLatestDocVerId(sql);
			//objDoc.id_latest_version = GetLatestDocVerId(sql);
            objDoc.id_version = insertedID;
            objDoc.id_latest_version = insertedID;
            objDoc.id_status = en_file_Status.checked_in;

			int id_hist = HistoryController.SaveDocumentHistoric(sql, userId, objDoc);
			sql.Reload("document_version", SqlOperationMode.Update);
			sql.Field("id_historic", id_hist);
			sql.Where("id", objDoc.id_version);
			sql.Commit();

			InsertOrUpdateDocument(sql, objDoc, false);

            if(localDb)
				TextExtraction.UpdateDocument(objDoc, sql);

			UserController.SaveDocumentRecent(sql, userId, objDoc.id);

			return id_hist;
		}


        internal static int UpdateVersion(SqlOperation sql, int userId, bool localDb, Document objDoc)
        {
            objDoc.reason = (objDoc.reason == null ? "" : objDoc.reason);

            object[] vals = new object[]
            {
                objDoc.filedata,
                objDoc.size,
                objDoc.version,
                objDoc.extension,
                objDoc.reason
            };

            int id_hist = HistoryController.SaveDocumentHistoric(sql, userId, objDoc);

            sql = SqlOperation.GetSqlOperation(sql, "document_version", SqlOperationMode.Update);
            sql.Field(tb_document_version_id, objDoc.id);
            sql.Fields(tb_document_version, vals);
            sql.Field("id_historic", id_hist);

            int insertedID = Convert.ToInt32(sql.Commit());

            sql.Where("id", objDoc.id_version);
            sql.Commit();

            return insertedID;
        }


        /* No longer used sience Convert.ToInt32(sql.Commit())
		static int GetLatestDocVerId(SqlOperation sql = null)
		{
			sql = SqlOperation.GetSqlOperation(sql, "document_version", SqlOperationMode.Select_Scalar);
			return sql.GetMaxId();
		}
        */
        //---------------------------------------------------------------------------------
        // Rollback to old document version
        public static string RollBackVersion(int userId, bool localDb, int id, int id_version, int CurrentVersionNo, string reason)
		{
			string ans = "";
			SqlOperation sql = null;

			Document objDoc = DocumentController<Document>.GetDocument(id, id_version: id_version, data: true);
			objDoc.id_event = EventIdController.GetEventId(en_Events.Rollback);
			objDoc.id_user = userId;
			objDoc.date = DateTime.Now;
			objDoc.reason = reason;
			objDoc.comments = "(From Version:" + objDoc.version + ")";
			objDoc.version = CurrentVersionNo + 1;

			try
			{
				sql = new SqlOperation();

				sql.BeginTran();
				AddVersion(sql, userId, localDb, objDoc);
				sql.CommitTran();
			}
			catch(Exception error)
			{
				if(sql != null)
					sql.RollBack();

				ans = error.Message;
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public static bool DeleteDocument(int userId, int id_doc, string reason)
		{
			bool ans = true;

            try
			{
				Document doc = GetDocument(id_doc);

                ans = DeleteDocument(userId, doc, reason);

            }
			catch(Exception ex)
			{
				ans = false;
                logger.Error(ex);

            }

			return ans;
		}

        public static bool DeleteDocument(int userId, Document doc, string reason)
        {
            bool ans = true;

            SqlOperation sql = new SqlOperation();
            sql.BeginTran();

            try
            {
                // Change document status
                doc.id_status = en_file_Status.deleted;
                InsertOrUpdateDocument(sql, doc, false);

                // Add document to document_deleted table
                object[] vals = new object[]
                {
                    doc.id,
                    reason,
                    userId,
                    DateTime.Now
                };

                sql.Reload("document_deleted", SqlOperationMode.Insert);
                sql.Fields(tb_document_deleted, vals);
                sql.Commit();

                sql.CommitTran();

            }
            catch(Exception ex)
            {
                sql.RollBack();
                ans = false;
                logger.Error(ex);
            }

            return ans;
        }

        public static bool DeleteBy(int id_folder, int userId, string reason)
		{
			bool ok = false;
			try
			{
				List<Document> docs = GetBy(id_folder);

				docs.ForEach(doc => DeleteDocument(userId,doc.id,reason));

				ok = true;
			}
			catch(Exception ex)
			{
				logger.Error(ex);
			}

			return ok;
		}

//---------------------------------------------------------------------------------
// Methods to insert ot update tables
//---------------------------------------------------------------------------------
		public enum DocumentSaveMode
		{
			Insert,
			Update
		}

		public static Document InsertOrUpdateDocument(SqlOperation sql, Document objDoc, bool insert)
		{
			object[] vals = new object[]
			{
				objDoc.title,
				objDoc.extension,
				(int)objDoc.id_status,
				(int)objDoc.id_sp_status,
				objDoc.id_user,
				objDoc.id_folder,
				objDoc.id_docType,
				( objDoc.date == DateTime.MinValue ? DateTime.Now : objDoc.date ),
				objDoc.id_latest_version,
				objDoc.id_review,
				objDoc.id_checkout_user
			};

			SqlOperationMode sql_mode = SqlOperationMode.Update;
			if(insert)
				sql_mode = SqlOperationMode.Insert;

			sql = SqlOperation.GetSqlOperation(sql, "document", sql_mode);

			if(!insert)
				sql.Where("id", objDoc.id);

			sql.Fields(tb_document, vals);
			//sql.Commit();
            int insertedID = Convert.ToInt32(sql.Commit());

            if (insert) objDoc.id = insertedID;

            /*
			if(insert)
				objDoc.id = GetLatestDocId(sql);
            */
			DocumentAttributeController<Document>.SaveAttribute(sql, objDoc);

            return objDoc;
		}

//---------------------------------------------------------------------------------
        /*
		static int GetLatestDocId(SqlOperation sql = null)
		{
			sql = SqlOperation.GetSqlOperation(sql, "document", SqlOperationMode.Select_Scalar);
			return sql.GetMaxId();
		}
        */
//---------------------------------------------------------------------------------
// Document Tracking --------------------------------------------------------------
//---------------------------------------------------------------------------------
		public static List<string> GetTrackingUserEmail(int id_doc)
		{
			List<int> UserIds = new List<int>();
			List<string> Emails = new List<string>();

			SqlOperation sql;

			// Get all user ids who are waiting this document
			sql = new SqlOperation("document_tracked", SqlOperationMode.Select);
			sql.Field("id_user");
			sql.Where("id_doc", id_doc);
			sql.Where("fired", 0);
			sql.Commit();

			while(sql.Read())
				UserIds.Add(Convert.ToInt32(sql.Result_Obj("id_user")));

			// Get their emails
			if(0 < UserIds.Count)
			{
				sql = new SqlOperation("user", SqlOperationMode.Select);
				sql.Field("email");
				sql.Where_In("id", UserIds.ToArray());
				sql.Commit();

				while(sql.Read())
					Emails.Add(sql.Result("email"));
			}

			return Emails;
		}

//---------------------------------------------------------------------------------
		public static void AddDocumentTracked(int id_doc, int id_user)
		{
			SqlOperation sql = new SqlOperation("document_tracked", SqlOperationMode.Insert);

			sql.Field("id_doc", id_doc);
			sql.Field("id_user",id_user);
			sql.Field("fired", 0);

			sql.Commit();
		}

//---------------------------------------------------------------------------------
		public static void RemoveDocumentTracked(int id_doc)
		{
			SqlOperation sql = new SqlOperation("document_tracked", SqlOperationMode.Update);
			sql.Field("fired", 1);
			sql.Where("id_doc", id_doc);
			sql.Commit();
		}

//---------------------------------------------------------------------------------
// Generic Methods ----------------------------------------------------------------
//---------------------------------------------------------------------------------
		public static bool IsDocumentExists(int id_folder = 0, string title = "")
		{
			SqlOperation sql = new SqlOperation("document", SqlOperationMode.Select_Scalar);

			if(0 < id_folder)
				sql.Where("id_folder", id_folder);

			if(!String.IsNullOrEmpty(title))
				sql.Where("title", title);

			sql.Where("id_status <> " + (int)en_file_Status.deleted);

			return (0 < sql.GetCountId());
		}

//---------------------------------------------------------------------------------
		public static string GetNameAvailable(int id_folder, string title, string extension)
		{
			string nameFile = title + '.' + extension;

			nameFile = FileFolder.GetAvailableFileName(nameFile, ((string a) => {
							SqlOperation sql = new SqlOperation("document", SqlOperationMode.Select_Scalar);
							sql.Where("id_folder", id_folder);
							sql.Where("title", a);
							return (sql.GetCountId() <= 0);
						}));

			return nameFile;
		}

//---------------------------------------------------------------------------------
		public static List<string> GetExistingExtensions()
		{
			List<string> ans = new List<string>();

			SqlOperation sql = new SqlOperation("document", SqlOperationMode.Select);
			sql.Field("extension");
			sql.Distinct = true;
			sql.Commit();

			while(sql.Read())
			{
				string wrk = sql.Result("extension");

				if(!String.IsNullOrEmpty(wrk))
					ans.Add(wrk);
			}

			ans.Sort();

			return ans;
		}

		/// <summary>
		/// Gets active documents by version ID
		/// </summary>
		/// <param name="veresionIds"></param>
		/// <returns></returns>
		async public static System.Threading.Tasks.Task<List<Tuple<int,int>>> ExistAsync(int[] veresionIds)
		{
			return await System.Threading.Tasks.Task.Run(() => Exist(veresionIds));
		}

		/// <summary>
		/// Gets active documents by version ID
		/// </summary>
		/// <param name="veresionIds"></param>
		/// <returns></returns>
		public static List<Tuple<int, int>> Exist(int[] veresionIds)
		{
			var ans = new List<Tuple<int, int>>();

			SqlOperation sql = new SqlOperation("view_document_exist", SqlOperationMode.Select);
			sql.Fields("id", "id_doc");

			sql.Where_In("id", veresionIds);
			sql.Commit();


			while (sql.Read())
			{
				ans.Add(new Tuple<int, int>(sql.Result_Int("id"), sql.Result_Int("id_doc")));
			}

			return ans;
		}


		//---------------------------------------------------------------------------------
	}
}
