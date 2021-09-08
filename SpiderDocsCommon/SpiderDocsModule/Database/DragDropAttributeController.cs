using System;
using System.Linq;
using System.Data.Common;
using System.Collections.Generic;
using System.Reflection;
using Spider.Data;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class DragDropAttributeController 
    {
		static Logger logger = LogManager.GetCurrentClassLogger();

        static readonly string[] tb_dragdrop_attribute = new string[]
		{
            "id",
            "id_folder",
            "id_atb",
            "value_from",
            "value_taken"
        };

//---------------------------------------------------------------------------------
		public static List<DocumentAttribute> GetDragDropAttribute(int id_folder)
		{            
            List<DragDropAttribute> ddAttrs = new List<DragDropAttribute>();

            SqlOperation sql = new SqlOperation("dragDropAttribute", SqlOperationMode.StoredProcedure);
            sql.SetCommandParameter("id_folder", id_folder);

            sql.Fields("id_folder", "id_atb", "value_from", "value_taken");
            sql.Commit();


            while (sql.Read())
            {
                ddAttrs.Add(new DragDropAttribute
                {
                    id_folder = sql.Result<int>("id_folder"),
                    id_atb = sql.Result<int>("id_atb"),
                    value_from = sql.Result<string>("value_from"),
                    value_taken = sql.Result<string>("value_taken")
                });
            }
            
            List<DocumentAttribute> cachedAttrs = DocumentAttributeController.GetAttributesCache(true);

            List<DocumentAttribute> ans = ddAttrs.Select(dda =>
            {
                var atr = cachedAttrs.Find(a => a.id == dda.id_atb);
                atr.atbValue = dda.value_taken;
                return atr;
            }).ToList();
            
            return ans;
        }

//---------------------------------------------------------------------------------
	}
}
