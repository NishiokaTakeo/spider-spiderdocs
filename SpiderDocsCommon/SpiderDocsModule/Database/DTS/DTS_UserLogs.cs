using System;
using System.Data;
using Spider.Data;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class DTS_UserLogs : DTS_Base
	{
//---------------------------------------------------------------------------------
		public DTS_UserLogs()
		{
			table.Columns.Add("id_user", typeof(Int32));
			table.Columns.Add("name", typeof(string));
			table.Columns.Add("event", typeof(string));
			table.Columns.Add("date", typeof(DateTime));
			table.Columns.Add("id_doc", typeof(string));
			table.Columns.Add("version", typeof(string));
			table.Columns.Add("obs", typeof(string));
			table.Columns.Add("frm", typeof(Int32));

			Select();
		}

//---------------------------------------------------------------------------------
		public override void Select()
		{
			SqlOperation sql;

			table.Clear();

			sql = new SqlOperation("user_log AS ul", SqlOperationMode.Select);
			sql.Fields(
				"u.id",
				"u.name",
				"ul.id_event",
				"ul.date",
				"ul.obs"
			);

			sql.InnerJoin("[user] AS u", "ul.id_user = u.id");
			sql.Commit();

			while(sql.Read())
			{
				table.Rows.Add(
					Int32.Parse(sql.Result("u.id")),	// id_user
					sql.Result("name"),					// name
					UserController.tb_EventStr[sql.Result<int>("id_event")], // event
					sql.Result("ul.date"),				// date
					"",									// id_doc
					"",									// version
					sql.Result("ul.obs"),				// obs
					1									// frm
				);
			}


			DTS_DocumentHistoric wrk = new DTS_DocumentHistoric();
			wrk.Select();
			DataTable histric = wrk.GetDataTable();

			foreach(DataRow row in histric.Rows)
			{
				table.Rows.Add(
					Convert.ToInt32(row["id_user"]),	// id_user
					row["name"],						// name
					row["event"],						// event
					Convert.ToDateTime(row["date"]),	// date
					Convert.ToInt32(row["id_doc"]),		// id_doc
					Convert.ToInt32(row["version"]),	// version
					"",									// obs
					2									// frm
				);
			}

			table.DefaultView.Sort = "date DESC";
			table = table.DefaultView.ToTable();

			table.Columns["id_user"].DefaultValue = DBNull.Value;
			table.Columns["name"].DefaultValue = DBNull.Value;
			table.Columns["event"].DefaultValue = DBNull.Value;
			table.Columns["date"].DefaultValue = DBNull.Value;
			table.Columns["id_doc"].DefaultValue = DBNull.Value;
			table.Columns["version"].DefaultValue = DBNull.Value;
			table.Columns["obs"].DefaultValue = DBNull.Value;
			table.Columns["frm"].DefaultValue = DBNull.Value;
		}

//---------------------------------------------------------------------------------
	}
}
