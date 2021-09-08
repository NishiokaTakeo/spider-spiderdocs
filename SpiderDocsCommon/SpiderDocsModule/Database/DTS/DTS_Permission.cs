using System;
using System.Data;
using Spider.Data;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
//---------------------------------------------------------------------------------
	public class DTS_Permission : DTS_Base
	{
//---------------------------------------------------------------------------------
		public void Select(int id_folder_group = -1, int id_folder_user = -1)
		{
			SqlOperation sql;			
			
			table.Clear();

			sql = new SqlOperation("permission p", SqlOperationMode.Select_Table);
			sql.Fields(
				"dp.id as id",
				"id_folder_group",
				"id_folder_user",
				"id_permission",
				"allow",
				"[deny]",
				"permission",
				"p.id AS id_p",
				"sort"
			);

			sql.LeftJoin("document_permission dp", "dp.id_permission = p.id");
			
			if(0 < id_folder_group)
				sql.Where("id_folder_group", id_folder_group);

			if(0 < id_folder_user)
				sql.Where("id_folder_user", id_folder_user);

			sql.Commit();
			table.Merge(sql.table);


			sql = new SqlOperation("permission", SqlOperationMode.Select);
			sql.Fields(
				"permission",
				"id",
				"sort"
			);

			if(0 < table.Rows.Count)
			{
				string where = "id NOT IN(";

				foreach(DataRow row in table.Rows)
					where += row["id_p"].ToString() + ", ";

				where += ")";
				where = where.Replace(", )", ")");
				sql.Where(where);
			}

			sql.Commit();

			while(sql.Read())
			{
				table.Rows.Add(
					0,	// dp.id
					0,	// id_folder_group
					0,	// id_folder_user
					0,	// id_permission
					0,	// allow
					0,	// [deny]
					sql.Result("permission"),
					Int32.Parse(sql.Result("id")),
					Int32.Parse(sql.Result("sort"))
				);
			}

			table.DefaultView.Sort = "sort";

			table.Columns["id"].DefaultValue = DBNull.Value;
			table.Columns["id_folder_group"].DefaultValue = DBNull.Value;
			table.Columns["id_folder_user"].DefaultValue =  DBNull.Value;
			table.Columns["id_permission"].DefaultValue =  DBNull.Value;
			table.Columns["allow"].DefaultValue =  DBNull.Value;
			table.Columns["deny"].DefaultValue =  DBNull.Value;
			table.Columns["permission"].DefaultValue =  DBNull.Value;
			table.Columns["id_p"].DefaultValue =  DBNull.Value;
			table.Columns["sort"].DefaultValue =  DBNull.Value;
		}

//---------------------------------------------------------------------------------
	}
}
