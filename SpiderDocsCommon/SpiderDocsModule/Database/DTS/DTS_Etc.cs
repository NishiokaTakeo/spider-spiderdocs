using System;
using System.Data;
using System.Collections.Generic;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
//---------------------------------------------------------------------------------
	public class DTS_System_permission_level : DTS_Base
	{
		public DTS_System_permission_level()
		{
			table_name = "system_permission_level";
			fields = new List<string> { "id", "permission", "obs" };

			Select();
		}
	}

//---------------------------------------------------------------------------------
	public class DTS_Group : DTS_Base
	{
		public DTS_Group()
		{
			table_name = "group";
			fields = new List<string> { "id", "group_name", "obs" };

			Select();
		}

		public override void Select()
		{
			base.Select();
			this.table = this.table.AsEnumerable().OrderBy(a => a["group_name"]).CopyToDataTable();		
		}
	}

    public class DTS_NotificationGroup : DTS_Base
    {
        public DTS_NotificationGroup()
        {
            table_name = "notification_group";
            fields = new List<string> { "id", "group_name"};

            Select();
        }

        public override void Select()
        {
            base.Select();

            var dt = this.table.Clone();

            var rows = this.table.AsEnumerable().OrderBy(a => a["group_name"]);
            foreach (var row in rows)
                dt.ImportRow(row);

            this.table = dt;
        }

        public void Select(int id)
        {
            base.Select();

            var dt = this.table.Clone();

            var rows = this.table.AsEnumerable().Where(a => int.Parse(a["id"].ToString()) == id).OrderBy(a => a["group_name"]);
            foreach (var row in rows)
                dt.ImportRow(row);

            this.table = dt;
        }
    }
    //---------------------------------------------------------------------------------
}
