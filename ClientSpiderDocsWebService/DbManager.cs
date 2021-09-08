// 09/05/2014 Mori
// This code is not compatible with other then MS SQL server and just work around.
// Need to update this code when we need compatibility with other database such as My SQL.
// In this time, should link code from main client project instead of COPY/PASTE code for clear version control!!!

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

//---------------------------------------------------------------------------------
namespace SpiderDocs
{
//---------------------------------------------------------------------------------
	public static class DbManager
	{
		public static string strConn;

		public static en_sql_mode m_sql_mode = en_sql_mode.ms_sql;
		public enum en_sql_mode
		{
			ms_sql = 0,
			my_sql,

			Max
		}

		static string[] tb_sql_mode = new string[(int)en_sql_mode.Max]
		{
			"mssql",
			"mysql"
		};

//---------------------------------------------------------------------------------		       
		public static en_sql_mode GetServerModeVal(string src)
		{
			en_sql_mode ans = en_sql_mode.ms_sql;
			
			switch(src)
			{
			case "mssql":
			default:
				ans = en_sql_mode.ms_sql;
				break;
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public static string GetServerModeStr()
		{
			return tb_sql_mode[(int)m_sql_mode];
		}

//---------------------------------------------------------------------------------
		public static string GetServerModeStr(en_sql_mode idx)
		{
			return tb_sql_mode[(int)idx];
		}
		  
//---------------------------------------------------------------------------------		       
		public static string getConnectionDetails()
		{
			return strConn;
		}   
	   
//---------------------------------------------------------------------------------
		public static bool saveConn(string Conn)
		{

			strConn = Conn;
			return true;
		}
	   
//---------------------------------------------------------------------------------
		public static DbConnection GetSqlConnection()
		{
			return GetSqlConnection("");
		}

//---------------------------------------------------------------------------------  
		public static DbConnection GetSqlConnection(string connstr)
		{
			return GetSqlConnection(connstr, m_sql_mode);
		}

//---------------------------------------------------------------------------------  
		public static DbConnection GetSqlConnection(string connstr, en_sql_mode mode)
		{
			DbConnection sqlConn = null;
			string ActualStr = strConn;

			if(connstr != "")
				ActualStr = connstr;

			// MS SQL
			sqlConn = new SqlConnection(ActualStr);
			sqlConn.Open();

			return sqlConn;
		}
	   
//---------------------------------------------------------------------------------
		public static DbCommand GetSqlCommand(string sql, DbConnection sqlConn) 
		{
			DbCommand sqlCom;

			sqlCom = sqlConn.CreateCommand();
			sql = SqlConvert(sql);
			
			sqlCom.CommandText = sql;
			sqlCom.CommandTimeout = 180;
			
			return sqlCom;
		}

//---------------------------------------------------------------------------------
		public static string SqlConvert(string src) 
		{
			Match word;

			// Search sub SQL sentence
			word = Regex.Match(src, @"\(\s*select", RegexOptions.IgnoreCase);
			if(word.Success)
			{
				int i;
				int cnt = 1;	// = 1 is for the beginning of '('

				// Search closing of the bracket ')'
				for(i = word.Index + 1; i < src.Length; i++)	// + 1 is for the beginning of '('
				{
					if(src[i] == '(')
						cnt++;
					else if(src[i] == ')')
						cnt--;

					if(cnt <= 0)
						break;
				}

				// Recurs for each sub queries
				string sub = SqlConvert(src.Substring(word.Index + 1, i - word.Index - 1));	// Extract string without brackets

				// Join each queries
				src = src.Remove(word.Index + 1, i - word.Index - 1);	// Remove the sub query excepr brackets
				src = src.Insert(word.Index + 1, sub);	// Insert the converted sub query
			}

			return src;
		}

//---------------------------------------------------------------------------------
		public static DbDataAdapter GetSqlAdapter() 
		{
			DbDataAdapter sqlAdapter;

			// MS SQL
			sqlAdapter = new SqlDataAdapter();

			return sqlAdapter;
		}

//---------------------------------------------------------------------------------
		public static void setSqlParameter(
			DbCommand cmd,
			string name,
			DbType type,
			int size,
			ParameterDirection direction,
			byte precision,
			byte scale,
			string sourceColumn,
			DataRowVersion sourceVersion,
			bool sourceColumnNullMapping,
			Object val,
			string xmlSchemaCollectionDatabase,
			string xmlSchemaCollectionOwningSchema,
			string xmlSchemaCollectionName)
		{
			DbParameter parameter = cmd.CreateParameter();
			setSqlParameter(name, type, val, parameter);
			parameter.Size = size;
			parameter.Direction = direction;
			parameter.SourceColumn = sourceColumn;
			parameter.SourceVersion = sourceVersion;
			parameter.SourceColumnNullMapping = sourceColumnNullMapping;
			cmd.Parameters.Add(parameter);
		}

//---------------------------------------------------------------------------------
		public static void setSqlParameter(string name, DbType type, int size, object val, DbCommand cmd) 
		{
			DbParameter parameter = cmd.CreateParameter();
			setSqlParameter(name, type, val, parameter);
			parameter.Size = size;
			cmd.Parameters.Add(parameter);
		}

//---------------------------------------------------------------------------------		
		public static void setSqlParameter(string name, DbType type, object val, DbCommand cmd)
		{
			DbParameter parameter = cmd.CreateParameter();
			setSqlParameter(name, type, val, parameter);
			cmd.Parameters.Add(parameter);
		}

//---------------------------------------------------------------------------------
		static void setSqlParameter(string name, DbType type, object val, DbParameter parameter) 
		{
			parameter.ParameterName = name;
			parameter.DbType = type;
			parameter.Value = val;
		}

//---------------------------------------------------------------------------------
		public static string[] GetDbInfo()
		{
            SpiderDocsModule.SqlOperation sql = null;
			string[] ans = new string[2];
			
			DbConnection sqlConn = DbManager.GetSqlConnection();
			string dbname = sqlConn.Database.ToString();
			sqlConn.Close();
            
			switch(DbManager.m_sql_mode)
			{
			case en_sql_mode.ms_sql:
				sql = new SpiderDocsModule.SqlOperation("information_schema.TABLES", Spider.Data.SqlOperationMode.Select);
				sql.Fields("table_schema AS basename", "(sum(data_length + index_length) / 1048576) AS size");
				sql.Where("table_schema", dbname);
				sql.GroupBy("table_schema");
				break;

			case en_sql_mode.my_sql:
				break;
			}

			if(sql != null)
			{
				sql.Commit();
				sql.Read();
				ans[0] = sql.Result("basename");
				ans[1] = sql.Result("size") + " MB";
			}

			return ans;
		}

//---------------------------------------------------------------------------------
	}
}
