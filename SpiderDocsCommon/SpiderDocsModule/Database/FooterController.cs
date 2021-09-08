using System;
using System.Collections.Generic;
using Spider.Data;

namespace SpiderDocsModule
{
	public class FooterController<T> : BaseController where T : Footer, new()
	{
		static TableInformation table = new TableInformation(
			"system_footer",
			new string[]
			{
				"field_id",
				"field_type"
			});

		public static List<T> GetFooter(params int[] ids)
		{
			List<T> ans = new List<T>();
			SqlOperation sql = Get(table, ids);

			while(sql.Read())
			{
				T wrk = new T();

				wrk.id = sql.Result_Int("id");
				wrk.field_id = sql.Result_Int("field_id");
				wrk.field_type = (en_FooterType)sql.Result_Int("field_type");

				ans.Add(wrk);
			}

			return ans;	
		}

		public static void AddFooter(T src)
		{
			AddOrUpdateFooter(src, SqlOperationMode.Insert);
		}

		public static void UpdateFooter(T src)
		{
			AddOrUpdateFooter(src, SqlOperationMode.Update);
		}

		static void AddOrUpdateFooter(T src, SqlOperationMode mode)
		{
			object[] vals =
			{
				src.field_id,
				(int)src.field_type
			};

			AddOrUpdate(table, src.id, vals, mode);
		}

		public static void DeleteFooter(params int[] ids)
		{
			Delete(table, ids);
		}

		public static bool IsAttributeUsedByFooter(int id_atb)
		{
			SqlOperation sql = new SqlOperation("system_footer", SqlOperationMode.Select_Scalar);
			sql.Where("field_type", (int)en_FooterType.Attr);
			sql.Where("field_id", id_atb);

			return (0 < sql.GetCountId());
		}
	}
}
