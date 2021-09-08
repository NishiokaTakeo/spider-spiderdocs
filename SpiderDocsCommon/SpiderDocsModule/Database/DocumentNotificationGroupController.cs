using System.Collections.Generic;
using Spider.Data;
using System.Linq;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class DocumentNotificationGroupController : BaseController
	{
		static TableInformation table = new TableInformation(
            "document_notification_group",
			new string[]
			{
				"id"
                ,"id_doc"
                ,"id_notification_group"
            }
		);

        //public static DocumentType DocumentType(int id)
        //{
        //	DocumentType ans = null;

        //	List<DocumentType> wrk = DocumentType(false, new int[] { id });
        //	if(0 < wrk.Count)
        //		ans = wrk[0];

        //	return ans;
        //}

        public static List<DocumentNotificationGroup> Select(int id_group = 0, int id_doc = 0)
        {
            List<DocumentNotificationGroup> notificationgorups = new List<DocumentNotificationGroup>();

            SqlOperation sql = new SqlOperation("document_notification_group", SqlOperationMode.Select);
            sql.Fields( new string[] { "id","id_doc","id_notification_group" });

            if (id_group > 0)
                sql.Where_In("id_notification_group", id_group);
            if (id_doc > 0)
                sql.Where_In("id_doc", id_doc);

            sql.Commit();
            
            while (sql.Read())
                notificationgorups.Add( new DocumentNotificationGroup() { id = sql.Result_Int("id"), id_doc = sql.Result_Int("id_doc"), id_notification_group = sql.Result_Int("id_notification_group") });
            
            return notificationgorups;
        }

        public static void SaveOne(SqlOperation sql, int id_doc, int[] id_notification_group = null)
        {
            RemoveNotificationGroup(sql,id_doc);

            if (id_notification_group != null && id_notification_group.Length > 0)
                id_notification_group.ToList().ForEach(ng => AssignNotificationGroup(sql, id_doc, ng));
                			
			// remove from schedule to notify
			if(id_notification_group == null)
				ScheduleNotificationAmendedController.DeleteByDocId(id_doc);			
        }

        //---------------------------------------------------------------------------------
        public static void AssignNotificationGroup(SqlOperation sql, int id_doc, int id_notification_group)
		{            
            sql = SqlOperation.GetSqlOperation(sql, "document_notification_group", SqlOperationMode.Insert);

            sql.Field("id_doc", id_doc);
			sql.Field("id_notification_group", id_notification_group);

			sql.Commit();
		}

//---------------------------------------------------------------------------------
		//public static void UpdateAttributeInDocType(List<int> id_atbs, int id_doc_type)
		//{
		//	SqlOperation sql;

		//	int i = 0;
		//	foreach(int id_atb in id_atbs)
		//	{
		//		sql = new SqlOperation("attribute_doc_type", SqlOperationMode.Update);

		//		sql.Field("position", i);
		//		sql.Where("id_attribute", id_atb);
		//		sql.Where("id_doc_type", id_doc_type);

		//		sql.Commit();
		//		i++;
		//	}
		//}

//---------------------------------------------------------------------------------
		public static void RemoveNotificationGroup(SqlOperation sql = null, int id_doc = 0 , int[] ids_group = null)
		{
            sql = sql ?? new SqlOperation("document_notification_group", SqlOperationMode.Delete);

            sql = SqlOperation.GetSqlOperation(sql,"document_notification_group", SqlOperationMode.Delete);

			if(id_doc > 0) sql.Where("id_doc", id_doc);
			
			if (ids_group != null && ids_group.Length  > 0)
				sql.Where_In("id_notification_group", ids_group);

			sql.Commit();
        }
	}
}
