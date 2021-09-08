using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Spider.Data;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class DTS_Base
	{
//---------------------------------------------------------------------------------
		protected DataTable table = new DataTable();

		protected string table_name;
		protected List<string> fields = new List<string>();
		protected List<object> fields_def = new List<object>();

//---------------------------------------------------------------------------------
		protected void AddField(string field, object def)
		{
			fields.Add(field);
			fields_def.Add(def);
		}

//---------------------------------------------------------------------------------
		public DataTable GetDataTable(string sort = "")
		{
			if(!String.IsNullOrEmpty(sort))
			{
				table.DefaultView.Sort = sort;
				table = table.DefaultView.ToTable();
			}
			
			return table;
		}

//---------------------------------------------------------------------------------
		public virtual void Select()
		{
			Select("", null);
		}
		
		public virtual void Select(string where, params object[] val)
		{
			Select(where, (Array)val);
		}

		public virtual void Select(string where, Array val)
		{
			SqlOperation sql = new SqlOperation(table_name, SqlOperationMode.Select_Table);

			sql.SetOutputTable(table);
			sql.Fields(fields.ToArray());

			if(!String.IsNullOrEmpty(where) && (val != null) && (0 < val.Length))
				sql.Where_In(where, val);

			sql.Commit();
			//table = sql.table;

			int i = 0;
			foreach(string wrk in fields)
			{
				string field = wrk.Replace("[", "").Replace("]", "");

				if(fields.Count == fields_def.Count)
					table.Columns[field].DefaultValue = fields_def[i];
				else
					table.Columns[field].DefaultValue = DBNull.Value;

				i++;
			}
		}

//---------------------------------------------------------------------------------
		public virtual void Delete(int idx)
		{
			SqlOperation sql = new SqlOperation(table_name, SqlOperationMode.Delete);

			sql.Where("id", idx);

			sql.Commit();
			Select();			
		}

//---------------------------------------------------------------------------------
		public virtual void Insert(DataRowView data)
		{
			SqlOperation sql = new SqlOperation(table_name, SqlOperationMode.Insert);

			SetFields(sql, data);

			sql.Commit();
			Select();			
		}

		public virtual void Insert(string data)
		{
			SqlOperation sql = new SqlOperation(table_name, SqlOperationMode.Insert);

			sql.Field(fields[1], data); // fields[0] is always ID. fields[1] is data field.

			sql.Commit();
			Select();			
		}

//---------------------------------------------------------------------------------
		public virtual void Update(DataRowView data)
		{
			SqlOperation sql = new SqlOperation(table_name, SqlOperationMode.Update);
			
			SetFields(sql, data);
			sql.Where("id", data["id"]);

			sql.Commit();
			Select();
		}

		public virtual void Update(int idx, string data)
		{
			SqlOperation sql = new SqlOperation(table_name, SqlOperationMode.Update);
			
			sql.Field(fields[1], data); // fields[0] is always ID. fields[1] is data field.
			sql.Where("id", idx);

			sql.Commit();
			Select();
		}

//---------------------------------------------------------------------------------
		void SetFields(SqlOperation sql, DataRowView data)
		{
			int cnt = fields.Count();
			int start = 1;
			
			for(int i = start; i < cnt; i++)
				sql.Field(fields[i], data[fields[i]]);
		}

//---------------------------------------------------------------------------------
	}
}
