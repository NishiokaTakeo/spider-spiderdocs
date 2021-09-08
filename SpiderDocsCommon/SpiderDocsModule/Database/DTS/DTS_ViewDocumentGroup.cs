using System;
using System.Data;
using Spider.Data;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class DTS_ViewDocumentGroup : DTS_Base
	{
//---------------------------------------------------------------------------------
		public DTS_ViewDocumentGroup()
		{
            table.Columns.Add("c_img", typeof(System.Drawing.Image));
            table.Columns.Add("id_doc", typeof(Int32));
			table.Columns.Add("title", typeof(string));
			table.Columns.Add("date", typeof(DateTime));
			table.Columns.Add("version", typeof(string));
			table.Columns.Add("name", typeof(string));
			table.Columns.Add("reason", typeof(string));
			table.Columns.Add("group_name", typeof(string));
            table.Columns.Add("id_sp_status", typeof(int));
            table.Columns.Add("extension", typeof(string));
        }

//---------------------------------------------------------------------------------
		public void Select(int id_notification_group = -1)
		{
            int id_event = EventIdController.GetEventId(en_Events.SaveNewVer);

            table.Clear();
            
			SqlOperation sql = new SqlOperation("view_document_group", SqlOperationMode.Select);
			sql.Fields("id_doc", "title", "date", "version", "name", "reason", "group_name", "id_sp_status", "extension");
            //sql.Where("id_event", id_event);
            sql.Where("id_notification_group", id_notification_group);

            sql.OrderBy("version", SqlOperation.en_order_by.Descent);
            sql.OrderBy("date", SqlOperation.en_order_by.Descent);
			sql.Commit();

			while(sql.Read())
			{
                var row = table.NewRow();
                
				row["id_doc"] = sql.Result<int>("id_doc");
                row["title"] = sql.Result("title");
                row["date"] = sql.Result<DateTime>("date");
                row["version"] = "V" + sql.Result("version") ;
                row["name"] = sql.Result("name");
                row["reason"] = sql.Result("reason");
                row["group_name"] = sql.Result("group_name");
                row["id_sp_status"] = sql.Result<int>("id_sp_status");
				row["extension"] = sql.Result("extension");
				
				loadIconsDtgBase(ref row);

                table.Rows.Add(row);
			}
		}

		private void loadIconsDtgBase(ref System.Data.DataRow row)
        {
            Spider.Drawing.IconManager icon = new Spider.Drawing.IconManager();


            en_file_Sp_Status status = en_file_Sp_Status.normal;

			if (row["id_sp_status"] != DBNull.Value)
				status = (en_file_Sp_Status)Convert.ToInt32(row["id_sp_status"]);
            /*
			if (status == en_file_Sp_Status.review_overdue)
				row["c_img"] = Properties.Resources.exclamation;
			else*/
				row["c_img"] = icon.GetSmallIcon(Convert.ToString(row["extension"]));
		
		}


//---------------------------------------------------------------------------------
	}
}

