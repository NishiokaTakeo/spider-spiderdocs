using System;
using System.Data;
using Spider.Data;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
//---------------------------------------------------------------------------------
	public class DTS_DocumentVersion : DTS_Base
	{
//---------------------------------------------------------------------------------
		public DTS_DocumentVersion()
		{
			table.Columns.Add("id_doc", typeof(Int32));
			table.Columns.Add("id", typeof(Int32));
			table.Columns.Add("name", typeof(string));
			table.Columns.Add("date", typeof(DateTime));
			table.Columns.Add("event", typeof(string));
			table.Columns.Add("version", typeof(Int32));
			table.Columns.Add("reason", typeof(string));
			table.Columns.Add("versionb", typeof(string));
		}

//---------------------------------------------------------------------------------
		public void Select(int id_file)
		{
			table.Clear();
			
			SqlOperation sql;
			sql = new SqlOperation("document_version AS dv", SqlOperationMode.Select);

			sql.Fields("dv.id_doc", "dv.id", "dv.version", "u.name", "dh.date", "dv.reason", "dh.comments", "de.event");
			sql.InnerJoin("document_historic AS dh", "dv.id_historic = dh.id");
			sql.InnerJoin("document_event AS de", "de.id = dh.id_event");
			sql.InnerJoin("[user] AS u", "dh.id_user = u.id");

			sql.Where("id_doc", id_file);
			sql.OrderBy("version", SqlOperation.en_order_by.Descent);

			sql.Commit();

			while(sql.Read())
			{
				table.Rows.Add(
					sql.Result_Int("dv.id_doc"),
					sql.Result_Int("dv.id"),
					sql.Result("u.name"),
					(DateTime)sql.Result_Obj("dh.date"),
					sql.Result("event") + " " + sql.Result("dh.comments"),
					sql.Result_Int("dv.version"),
					sql.Result("dv.reason"),
					"V" + sql.Result("dv.version")
				);
			}
		}

//---------------------------------------------------------------------------------
	}
}


