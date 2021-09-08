using System;
using System.Data;
using System.Collections.Generic;
using Spider.Data;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{	
	public class DTS_Footer : DTS_Base
	{
		static readonly string[] tb_fields =
		{
			"field_id",
			"field_type"
		};

		readonly public static string[] tb_RegAttr = new string[(int)en_RegAttr.Max]
		{
			"Id",
			"Name",
			"Folder",
			"DocType",
			"Version",
			"Author",
			"Date"
		};

		readonly public static string[] tb_FooterType = new string[(int)en_FooterType.Max]
		{
			"---",
			"Attribute"
		};

//---------------------------------------------------------------------------------
		public DTS_Footer()
		{
			table_name = "system_footer";
			fields = new List<string> { "id", "field_id", "field_type" };

			Select();
		}

//---------------------------------------------------------------------------------
		public override void Select()
		{
			base.Select();

			if(0 < this.table.Rows.Count)
				this.table = this.table.AsEnumerable().OrderBy(a => a["id"]).CopyToDataTable();
		}

//---------------------------------------------------------------------------------
		public void Delete(int field_id, en_FooterType field_type)
		{
			SqlOperation sql = new SqlOperation("system_footer", SqlOperationMode.Delete);
			sql.Where("field_id", field_id);
			sql.Where("field_type", (int)field_type);
			sql.Commit();

			Select();
		}
	}

//---------------------------------------------------------------------------------
}