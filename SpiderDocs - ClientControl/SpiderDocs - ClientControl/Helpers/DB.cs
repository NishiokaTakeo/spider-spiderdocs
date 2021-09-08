using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpiderDocs_ClientControl.Helpers
{
    public class DbManager
    {
        public static DbCommand GetSqlCommand(string sql, DbConnection  con )
        {
            DbCommand cmd = con.CreateCommand();
            cmd.CommandText = sql;

            return cmd;
        }

        internal static DbConnection GetSqlConnection(string conn)
        {
            SqlConnection sqlConn = new SqlConnection(conn);
            return sqlConn;
        }
    }
}