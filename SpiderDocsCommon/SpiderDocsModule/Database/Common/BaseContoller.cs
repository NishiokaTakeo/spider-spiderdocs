using System;
using System.Collections.Generic;
using Spider.Data;

namespace SpiderDocsModule
{
	abstract public class BaseController
	{
		protected class TableInformation
		{
			public string tb_table;
			public string tb_id_field = "id";
			public string[] tb_val_fields;

			public TableInformation(string table, string[] val_fields)
			{
				this.tb_table = table;
				this.tb_val_fields = val_fields;
			}

			public TableInformation(string table, string id_field, string[] val_fields)
			{
				this.tb_table = table;
				this.tb_id_field = id_field;
				this.tb_val_fields = val_fields;
			}
		}

		protected static SqlOperation Get(TableInformation table, params int[] ids)
		{
			SqlOperation sql = new SqlOperation(table.tb_table, SqlOperationMode.Select);
			sql.Field(table.tb_id_field);
			sql.Fields(table.tb_val_fields);

			if(0 < ids.Length)
				sql.Where_In(table.tb_id_field, ids);

			sql.Commit();

			return sql;
		}

		protected static void AddOrUpdate(TableInformation table, int id, object[] vals, SqlOperationMode mode)
		{
			SqlOperation sql = new SqlOperation(table.tb_table, mode);
			sql.Fields(table.tb_val_fields, vals);

			if(mode == SqlOperationMode.Update)
				sql.Where(table.tb_id_field, id);

			sql.Commit();
		}

		protected static void Delete(TableInformation table, params int[] ids)
		{
			SqlOperation sql = new SqlOperation(table.tb_table, SqlOperationMode.Delete);

			if(0 < ids.Length)
				sql.Where_In(table.tb_id_field, ids);

			sql.Commit();
		}
	}
}
