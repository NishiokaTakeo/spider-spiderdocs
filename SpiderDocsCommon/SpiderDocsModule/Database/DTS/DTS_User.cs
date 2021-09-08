using System;
using System.Data;
using System.Linq;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class DTS_User : DTS_Base
	{
		bool AddBlankOnTop = false;

		static readonly string[] tb_fields =
		{
			"id",
			"login",
			"name",
			"password",
			"active",
			"id_permission",
			"email",
			"reviewer"
		};

//---------------------------------------------------------------------------------
		public DTS_User(int id_permission = 0, bool AddBlankOnTop = false)
		{
			this.fields = tb_fields.ToList();
			
			table_name = "user";

			AddField("id", DBNull.Value);
			AddField("login", DBNull.Value);
			AddField("name", DBNull.Value);
			AddField("password", DBNull.Value);
			AddField("active", true);
			AddField("id_permission", DBNull.Value);
			AddField("email", DBNull.Value);
			AddField("reviewer", true);

			this.AddBlankOnTop = AddBlankOnTop;
			Select(id_permission);
		}

//---------------------------------------------------------------------------------
		public void Select(int id_permission = 0)
		{
			if(0 < id_permission)
				base.Select("id_permission", id_permission);
			else
				base.Select();

			if(AddBlankOnTop)
			{
				DataRow row = this.table.NewRow();
				row.ItemArray = new object[] { -1, "", "", "", false, -1, "", false };
				this.table.Rows.InsertAt(row, 0);
			}
		}

//---------------------------------------------------------------------------------
		// Insert to user table and add to 'All Users' group
		public override void Insert(DataRowView data)
		{
			base.Insert(data);

			// Return new users index
			int idx = 0;
			foreach(DataRow row in table.Rows)
			{
				int val = Convert.ToInt32(row["id"]);

				if(idx < val)
					idx = val;
			}

			GroupController.AssignGroup(GroupController.ALL_USERS_ID, idx);
		}

//---------------------------------------------------------------------------------
	}
}
