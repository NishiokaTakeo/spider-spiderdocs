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
	public class DragDropTypeController
    {
		static Logger logger = LogManager.GetCurrentClassLogger();

        static readonly string[] tb_dragdrop_attribute = new string[]
		{
            "id",
            "id_folder",
            "id_type"
        };
        
        //---------------------------------------------------------------------------------
        public static DragDropType GetBy(int id_folder)
		{            
            DragDropType ans = new DragDropType();

            SqlOperation sql = new SqlOperation("dragDropType", SqlOperationMode.StoredProcedure);
            sql.SetCommandParameter("id_folder", id_folder);

            sql.Fields("id", "id_folder", "id_type");
            sql.Commit();


            while (sql.Read())
            {
                ans = new DragDropType
                {
                    id = sql.Result<int>("id"),
                    id_folder = sql.Result<int>("id_folder"),
                    id_type = sql.Result<int>("id_type")
                };

				return ans;
            }            
            
            return null;
        }

//---------------------------------------------------------------------------------
	}
}
