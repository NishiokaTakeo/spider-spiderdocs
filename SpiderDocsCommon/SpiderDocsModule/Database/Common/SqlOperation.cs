using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using Spider.Data;

namespace SpiderDocsModule
{
	public class SqlOperation : Spider.Data.Sql.SqlOperation
	{
		/// <summary>
		/// <para>Give statement to get DbManager for current accessing server.</para>
		/// </summary>
		static public Func<DbManager> MethodToGetDbManager;

        static public string DBConnectionString
        {
            get { return MethodToGetDbManager().strConn; }
        }

        public SqlOperation()
		{
		}

		public SqlOperation(string table, SqlOperationMode mode) : base(table, mode)
		{
		}
        
        override protected DbConnection GetSqlConnection()
		{
			return MethodToGetDbManager().GetSqlConnection();
		}

		override protected DbCommand GetSqlCommand(string sql, DbConnection sqlConn) 
		{
			return MethodToGetDbManager().GetSqlCommand(sql, sqlConn);
		}

		override protected DbDataAdapter GetSqlAdapter() 
		{
			return MethodToGetDbManager().GetSqlAdapter();
		}

		public static SqlOperation GetSqlOperation(SqlOperation sql, string table, SqlOperationMode mode)
		{
			if(sql == null)
				sql = new SqlOperation(table, mode);
			else
				sql.Reload(table, mode);

			return sql;
		}
	}
}