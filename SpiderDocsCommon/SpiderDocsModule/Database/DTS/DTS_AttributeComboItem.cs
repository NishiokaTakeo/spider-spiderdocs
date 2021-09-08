using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Spider.Data;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
//---------------------------------------------------------------------------------
	public class DTS_AttributeComboItem : DTS_Base
	{
		bool TopBlank = false;
		
		readonly string[] tb_fields = new string[]
		{
			"id",
			"id_atb",
			"[value]"
		};

//---------------------------------------------------------------------------------
		public DTS_AttributeComboItem(int id_atb, bool TopBlank = true)
		{
			table_name = "attribute_combo_item";
			fields = tb_fields.ToList();

			this.TopBlank = TopBlank;
			Select(id_atb);
		}

//---------------------------------------------------------------------------------
		public void Select(int id_atb)
		{
			base.Select("id_atb", id_atb);

			table.DefaultView.Sort = "value";
			table = table.DefaultView.ToTable();

			if(TopBlank)
			{
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
		public override void Delete(int idx)
		{
			DocumentAttributeController.DeleteComboItems(ids: new int[] { idx });
		}

//---------------------------------------------------------------------------------
		public void DeleteAll(int id_atb)
		{
			DocumentAttributeController.DeleteComboItems(attr_ids: new int[] { id_atb });
		}

//---------------------------------------------------------------------------------
		public void Insert(int id_atb, string value)
		{
			DocumentAttributeCombo combo = new DocumentAttributeCombo();
			combo.text = value;
			DocumentAttributeController.InsertOrUpdateComboItem(combo, id_atb, true);
		}

//---------------------------------------------------------------------------------
		public override void Update(int id, string value)
		{
			DocumentAttributeCombo combo = DocumentAttributeController.GetComboItems(ids: new int[] { id }).FirstOrDefault();
			combo.text = value;
			DocumentAttributeController.InsertOrUpdateComboItem(combo, 0);
		}

//---------------------------------------------------------------------------------
	}
}