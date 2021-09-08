using System;
using System.Linq;
using System.Collections.Generic;
using Spider.Data;
using Spider.Net;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class DocumentTypeController : BaseController
	{
		static TableInformation table = new TableInformation(
			"document_type",
			new string[]
			{
				"type"
			}
		);

		public static DocumentType DocumentType(int id)
		{
			DocumentType ans = null;

			List<DocumentType> wrk = DocumentType(false, new int[] { id });
			if(0 < wrk.Count)
				ans = wrk[0];

			return ans;
		}

		public static List<DocumentType> DocumentType(bool sort = false, params int[] ids)
		{
			List<DocumentType> ans = new List<DocumentType>();
			SqlOperation sql;

			sql = Get(table, ids);
			while(sql.Read())
			{
				DocumentType wrk = new DocumentType();

				wrk.id = sql.Result<int>(table.tb_id_field);
				wrk.type = sql.Result("type");

				ans.Add(wrk);
			}

			if(sort)
				ans = ans.OrderBy(a => a.type).ToList();

			return ans;
		}

//---------------------------------------------------------------------------------
		public static void AssignAttributeToDocType(int id_atb, int id_doc_type,bool duplicate_chk = false)
		{
			SqlOperation sql = new SqlOperation("attribute_doc_type", SqlOperationMode.Insert);

			sql.Field("id_attribute", id_atb);
			sql.Field("id_doc_type", id_doc_type);
			sql.Field("position", 99);
            sql.Field("duplicate_chk", duplicate_chk);

            sql.Commit();
		}

//---------------------------------------------------------------------------------
		public static void UpdateAttributeInDocType(List<int> id_atbs, int id_doc_type)
		{
			SqlOperation sql;

			int i = 0;
			foreach(int id_atb in id_atbs)
			{
				sql = new SqlOperation("attribute_doc_type", SqlOperationMode.Update);

				sql.Field("position", i);
				sql.Where("id_attribute", id_atb);
				sql.Where("id_doc_type", id_doc_type);

				sql.Commit();
				i++;
			}
		}

//---------------------------------------------------------------------------------
		public static void RemoveAttributeFromDocType(int id_atb = 0, int id_doc_type = 0)
		{
			SqlOperation sql = new SqlOperation("attribute_doc_type", SqlOperationMode.Delete);

			if(0 < id_atb)
				sql.Where("id_attribute", id_atb);

			if(0 < id_doc_type)
				sql.Where("id_doc_type", id_doc_type);

			sql.Commit();
		}

//---------------------------------------------------------------------------------
		public static bool IsDocTypeUsed(int id_type)
		{
			SqlOperation sql = new SqlOperation("document", SqlOperationMode.Select_Scalar);
			sql.Where("id_type", id_type);

			return (0 < sql.GetCountId());
		}

//---------------------------------------------------------------------------------
		public static bool IsDocTypeExists(string docType_Name, int idDocType)
		{
			SqlOperation sql = new SqlOperation("document_type", SqlOperationMode.Select_Scalar);
			sql.Where("type", docType_Name);
			sql.Where("id <> " + idDocType);

			return (0 < sql.GetCountId());
		}

//---------------------------------------------------------------------------------
		public static bool IsAttributeValueExists(int id_atb = 0, int id_doc_type = 0)
		{
			SqlOperation sql = new SqlOperation("attribute_doc_type", SqlOperationMode.Select_Scalar);

			if(0 < id_atb)
				sql.Where("id_attribute", id_atb);

			if(0 < id_doc_type)
				sql.Where("id_doc_type", id_doc_type);

			return (0 < sql.GetCountId());
		}

//---------------------------------------------------------------------------------
	}
}
