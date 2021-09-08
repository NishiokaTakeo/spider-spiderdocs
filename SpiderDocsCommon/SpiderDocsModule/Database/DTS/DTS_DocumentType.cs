using System;
using System.Data;
using System.Collections.Generic;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
//---------------------------------------------------------------------------------
	public class DTS_DocumentType : DTS_Base
	{
		bool f_combo = false;

//---------------------------------------------------------------------------------
		public DTS_DocumentType(bool combo)
		{
			f_combo = combo;

			table_name = "document_type";
			fields = new List<string> { "id", "type" };

			Select();
		}

//---------------------------------------------------------------------------------
		public override void Select()
		{
			base.Select();

			if(f_combo)
			{
				this.table.Columns.Add("position", typeof(int));
				foreach(DataRow row in table.Rows)
					row["position"] = 1;

				DataRow wrk = this.table.NewRow();
				for(int i = 0; i < this.table.Columns.Count; i++)
				{
					if(this.table.Columns[i].DataType == typeof(string))
						wrk[i] = "";
					else
						wrk[i] = 0;
				}

				this.table.Rows.InsertAt(wrk, 0);
			}
		}

//---------------------------------------------------------------------------------
	}
}
