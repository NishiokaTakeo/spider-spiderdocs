using System;
using System.Linq;
using System.Collections.Generic;
using Spider.Data;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class HistoryController
	{
//---------------------------------------------------------------------------------
		static readonly string[] tb_document_historic = new string[]
		{
			"id_version",
			"id_event",
			"id_user",
			"date",
			"comments"
		};

		// structure of view_histric
		static readonly string[] tb_view_historic = new string[]
		{
            "id",
			"id_doc",
			"id_version",
			"title",
			"version",
			"date",
			"id_user",
			"name",
            "id_event",
			"event",
			"rollback_to",
			"type",
			"comments",
			"reason",
		};

//---------------------------------------------------------------------------------
		public static int SaveDocumentHistoric(SqlOperation sql, int userId, Document objDoc)
		{
			object[] vals = new object[]
			{
				objDoc.id_version,
				objDoc.id_event,
				userId,
				DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
				objDoc.comments ?? ""
			};

			sql = SqlOperation.GetSqlOperation(sql, "document_historic", SqlOperationMode.Insert);
			sql.Fields(tb_document_historic, vals);
			int lastID = sql.Commit<int>();

            return lastID;
		}

        public static void UpdateHistricTime(SqlOperation sql, int id_historic, DateTime newdat)
        {
            if (id_historic == 0 || newdat == DateTime.MinValue ) return;

            sql = SqlOperation.GetSqlOperation(sql, "document_historic", SqlOperationMode.Update);

            sql.Where("id", id_historic);
            sql.Field("date", newdat.ToString("yyyy-MM-dd HH:mm:ss"));
            sql.Commit();
        }

        //---------------------------------------------------------------------------------
        public static History GetLatestHistory(int id_version, params en_Events[] Event)
		{
			History ans = null;

			SqlOperation sql = new SqlOperation("document_historic", SqlOperationMode.Select);

			sql.Fields(tb_document_historic);
			sql.Where("id_version", id_version);

			int[] EventIds = Event.Where(a => a != en_Events.INVALID).Select(a => EventIdController.GetEventId(a)).ToArray();
			if(0 < EventIds.Length)
				sql.Where_In("id_event", EventIds);

			sql.SetMaxResult(1);
			sql.OrderBy("date", SqlOperation.en_order_by.Descent);
			sql.Commit();

			while(sql.Read())
			{
				ans = new History();

				ans.id_version = sql.Result_Int("id_version");
				ans.Event = EventIdController.GetEvent(sql.Result_Int("id_event"));
				ans.id_user = sql.Result_Int("id_user");
				ans.date = Convert.ToDateTime(sql.Result_Obj("date"));
				ans.comments = sql.Result("comments");
			}

			return ans;
		}

		/// <summary>
		/// Get histories records
		/// </summary>
		/// <param name="id_version"></param>
		/// <param name="Event"></param>
		/// <returns>view_historic records</returns>
		public static List<History> GetHistories(SearchCriteria criteria)
		{
            int[] id_docs = criteria.DocIds.ToArray();

			id_docs = id_docs == null ? new int[]{} : id_docs;

			List<History> ans = new List<History>();

			SqlOperation sql = new SqlOperation("view_historic", SqlOperationMode.Select);

			sql.Fields(tb_view_historic);

			if( id_docs.Count() > 0)
				sql.Where_In("id_doc", id_docs);

			//sql.SetMaxResult(100);
			sql.OrderBy("id_doc", SqlOperation.en_order_by.Descent);
            sql.OrderBy("version", SqlOperation.en_order_by.Descent);
            sql.OrderBy("date", SqlOperation.en_order_by.Descent);

			sql.Commit();

			while(sql.Read())
			{
				History history = new History();
				history.id_doc = sql.Result_Int("id_doc");
				history.title = sql.Result("title");
				history.version = sql.Result_Int("version");
				history.type = sql.Result("type");
				history.rollback_to = sql.Result_Int("rollback_to");
                history.id_event = sql.Result_Int("id_event");
                history.id_version = sql.Result_Int("id_version");
                history.Event = EventIdController.GetEvent(sql.Result_Int("id_event"));
                history.event_name = sql.Result("event");
                history.id_user = sql.Result_Int("id_user");
				history.date = Convert.ToDateTime(sql.Result_Obj("date"));
				history.comments = sql.Result("comments");
				history.reason = sql.Result("reason");
                history.name = sql.Result("name");
                //history.id_notification_group = sql.Result("id_notification_group");
                //history.notification_group_name = sql.Result("notification_group_name");
                ans.Add(history);
			}

			return ans;
        }

        #region SpiderDocsForm
        static public int SaveDocumentHistoric(SqlOperation sql, Document objDoc)
        {
            return SaveDocumentHistoric(sql, SpiderDocsApplication.CurrentUserId, objDoc);
        }
        #endregion

    }
}
