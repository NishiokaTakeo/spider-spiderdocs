using System;
using System.Linq;
using System.Collections.Generic;
using Spider.Data;
using Spider.Net;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class ViewController
	{
        public static string ConvertTitle(string titleWithExt)
        {
            string ans = string.Empty;

            SqlOperation sql = new SqlOperation("cnvDocumentTitle", SqlOperationMode.StoredProcedure);

            sql.SetCommandParameter("title", titleWithExt);
            sql.Fields("ans");
            sql.Commit();

            while (sql.Read())
            {
                ans = sql["ans"].ToString();
            }

            return ans;
        }
    }
}
