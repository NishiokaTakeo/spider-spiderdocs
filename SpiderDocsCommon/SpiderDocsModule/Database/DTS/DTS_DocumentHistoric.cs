using System;
using System.Data;
using Spider.Data;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class DTS_DocumentHistoric : DTS_Base
	{
//---------------------------------------------------------------------------------
		public DTS_DocumentHistoric()
		{
			table.Columns.Add("id_user", typeof(Int32));
			table.Columns.Add("name", typeof(string));
			table.Columns.Add("date", typeof(DateTime));
			table.Columns.Add("event", typeof(string));
			table.Columns.Add("id_doc", typeof(Int32));
			table.Columns.Add("version", typeof(Int32));
			table.Columns.Add("versionb", typeof(string));
		}

//---------------------------------------------------------------------------------
		public void Select(int id_version = -1, int id_doc = -1)
		{
			table.Clear();

			SqlOperation sql = new SqlOperation("document_historic as dh", SqlOperationMode.Select);
			sql.Fields("u.id AS id_user", "name", "date", "event", "comments", "id_doc", "version");
			sql.InnerJoin("document_version AS dv", "dh.id_version = dv.id");
			sql.InnerJoin("document_event AS de", "de.id = dh.id_event");
			sql.LeftJoin("[user] AS u", "dh.id_user = u.id");

			if((0 < id_version) || (0 < id_doc))
			{
				SqlOperationCriteriaCollection collection = new SqlOperationCriteriaCollection();
				collection.m_AndOr = SqlOperationAndOr.OR;

				if(0 < id_version)
					collection.Add(new SqlOperationCriteria("id_version", id_version));

				if(0 < id_doc)
					collection.Add(new SqlOperationCriteria("id_doc", id_doc));

				sql.Where(collection);
			}

			sql.OrderBy("date", SqlOperation.en_order_by.Descent);
			sql.OrderBy("version", SqlOperation.en_order_by.Descent);
			sql.Commit();

			while(sql.Read())
			{
				table.Rows.Add(
					sql.Result<int>("id_user"),
					sql.Result("name"),
					sql.Result<DateTime>("date"),
					sql.Result("event") + " " + sql.Result("comments"),
					sql.Result<int>("id_doc"),
					sql.Result<int>("version"),
					"V" + sql.Result("version")
				);
			}
		}

//---------------------------------------------------------------------------------
	}
}

