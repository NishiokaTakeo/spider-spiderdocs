using System;
using System.Data;
using Spider.Data;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class DTS_DocumentDeleted : DTS_Base
	{
//---------------------------------------------------------------------------------
		public DTS_DocumentDeleted()
		{
			table.Columns.Add("id_doc", typeof(Int32));
			table.Columns.Add("title", typeof(string));
			table.Columns.Add("type", typeof(string));
			table.Columns.Add("whocreated", typeof(string));
			table.Columns.Add("who_deleted", typeof(string));
			table.Columns.Add("date", typeof(DateTime));
			table.Columns.Add("reason", typeof(string));

			Select();
		}

//---------------------------------------------------------------------------------
		override public void Select()
		{
			string[] fiedls = new string[]
			{
				"dd.id_doc",
				"d.title",
				"dt.type",
				"u1.name as 'whocreated'",
				"u.name as 'who_deleted'",
				"dd.date",
				"dd.reason"
			};
			
			SqlOperation sql = new SqlOperation("document_deleted dd", SqlOperationMode.Select);
			sql.Fields(fiedls);
			sql.InnerJoin("[user] u", "dd.id_user = u.id");
			sql.InnerJoin("document d", "dd.id_doc = d.id");
			sql.InnerJoin("[user] u1", "d.id_user = u1.id");
			sql.InnerJoin("document_type dt", "d.id_type = dt.id");
			sql.Commit();

			while(sql.Read())
			{
				table.Rows.Add(
					Convert.ToInt32(sql.Result_Obj("id_doc")),
					sql.Result("title"),
					sql.Result("type"),
					sql.Result("whocreated"),
					sql.Result("who_deleted"),
					Convert.ToDateTime(sql.Result_Obj("date")),
					sql.Result("reason")
				);
			}
		}

//---------------------------------------------------------------------------------
	}
}
