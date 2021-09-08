using System;
using System.Text.RegularExpressions;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using MySql.Data.MySqlClient;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class DbManager
	{
		Crypt _crypt = new Crypt();

		string enc_strConn;
		public string strConn {	get	{ return _crypt.Decrypt(enc_strConn); } }

		string enc_sql_mode;
		public en_sql_mode m_sql_mode { get { return DbManager.GetServerModeVal(_crypt.Decrypt(enc_sql_mode)); } }

		public enum en_sql_mode
		{
			ms_sql = 0,
			my_sql,
			LocalDb,
			
			None,
			Max
		}

		static string[] tb_sql_mode = new string[(int)en_sql_mode.Max]
		{
			"mssql",
			"mysql",
			"localdb",

			"none"
		};

//---------------------------------------------------------------------------------
		public DbManager(string EncryptedConnStr, string EncryptedSQLMode)
		{
			enc_strConn = EncryptedConnStr;
			enc_sql_mode = EncryptedSQLMode;
		}

        public DbManager(string EncryptedConnStr, en_sql_mode sql_mode)
        {
            enc_strConn = EncryptedConnStr;
            enc_sql_mode = _crypt.Encrypt(tb_sql_mode[(int)sql_mode]);            
        }

        //---------------------------------------------------------------------------------
        public bool IsConnectionCheck()
		{
			bool ans = false;
			DbConnection conn = null;

			try
			{
				conn = GetSqlConnection();
				ans = true;
			}
			catch
			{}
			finally
			{
				if(conn != null)
					conn.Close();
			}

			return ans;
		}

//---------------------------------------------------------------------------------		       
		static public en_sql_mode GetServerModeVal(string src)
		{
			en_sql_mode ans = en_sql_mode.ms_sql;
			
			switch(src)
			{
			case "mssql":
			default:
				ans = en_sql_mode.ms_sql;
				break;

			case "mysql":
				ans = en_sql_mode.my_sql;
				break;

			case "localdb":
				ans = en_sql_mode.LocalDb;
				break;
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		static public string GetServerModeStr(en_sql_mode idx)
		{
			return tb_sql_mode[(int)idx];
		}

//---------------------------------------------------------------------------------
		static public string GetLocalDBConnectionStr(string path)
		{
			return "Data Source=" + path + "SpiderDocs.db;Connection Timeout=5";
		}

//---------------------------------------------------------------------------------
		public DbConnection GetSqlConnection()
		{
			DbConnection sqlConn = null;

			try
			{
				switch(m_sql_mode)
				{
				case en_sql_mode.ms_sql:
					sqlConn = new SqlConnection(strConn);
					break;

				case en_sql_mode.my_sql:
					sqlConn = new MySqlConnection(strConn);
					break;

				case en_sql_mode.LocalDb:
					SQLiteConnection tmp = new SQLiteConnection(strConn);
					sqlConn = tmp;
					break;
				}
			}
			catch{}

			sqlConn.Open();

			return sqlConn;
		}

//---------------------------------------------------------------------------------
		public DbDataAdapter GetSqlAdapter() 
		{
			DbDataAdapter sqlAdapter;
			
			switch(m_sql_mode)
			{
			case en_sql_mode.ms_sql:
			default:
				// MS SQL
				sqlAdapter = new SqlDataAdapter();
				break;

			case en_sql_mode.my_sql:
				sqlAdapter = new MySqlDataAdapter();
				break;

			case en_sql_mode.LocalDb:
				sqlAdapter = new SQLiteDataAdapter();
				break;
			}

			return sqlAdapter;
		}
	   
//---------------------------------------------------------------------------------
		public DbCommand GetSqlCommand(string sql, DbConnection sqlConn) 
		{
			DbCommand sqlCom = sqlConn.CreateCommand();
			sql = SqlConvert(sql);
			
			sqlCom.CommandText = sql;
			sqlCom.CommandTimeout = 600;
			
			return sqlCom;
		}

//---------------------------------------------------------------------------------
		string SqlConvert(string src) 
		{
			string ans = "";
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
			
			switch(m_sql_mode)
			{
			case en_sql_mode.ms_sql:
			default:
				ans = src;
				break;

			// Translate SQL Server's syntaxes to MySql
			case en_sql_mode.my_sql:
				src = src.Replace("[", "`");
				src = src.Replace("]", "`");

				word = Regex.Match(src, "select top ([0-9]+)", RegexOptions.IgnoreCase);
				if(word.Success)
				{
					src = Regex.Replace(src, "select top [0-9]+", "select", RegexOptions.IgnoreCase);
					src = src + " LIMIT " + word.Groups[1];
				}

				src = Regex.Replace(src, "begin tran", "START TRANSACTION", RegexOptions.IgnoreCase);
				src = Regex.Replace(src, "commit tran", "COMMIT", RegexOptions.IgnoreCase);
				src = Regex.Replace(src, "rollback tran", "ROLLBACK", RegexOptions.IgnoreCase);
				src = Regex.Replace(src, @"LEN\(", "LENGTH(", RegexOptions.IgnoreCase);
				src = Regex.Replace(src, "varchar", "char", RegexOptions.IgnoreCase);

				src = Regex.Replace(src, "'true'", "1", RegexOptions.IgnoreCase);
				src = Regex.Replace(src, @"atb_value\s*=\s*1", "atb_value = 'True'", RegexOptions.IgnoreCase);

				src = Regex.Replace(src, "'false'", "0", RegexOptions.IgnoreCase);
				src = Regex.Replace(src, @"atb_value\s*=\s*0", "atb_value = 'False'", RegexOptions.IgnoreCase);

				src = Regex.Replace(src, @"Convert\(\s*datetime\s*,\s*(.+?),\s*103\s*\)", @"STR_TO_DATE($1, '%d/%m/%Y')", RegexOptions.IgnoreCase);

				break;

			case en_sql_mode.LocalDb:
				src = Regex.Replace(src, "dbo.", "", RegexOptions.IgnoreCase);

				word = Regex.Match(src, @"select\s+.*?top\s+([0-9]+)\s+.+?;", RegexOptions.IgnoreCase);
				if(word.Success)
					src = Regex.Replace(src, @"select\s+.*?top\s+[0-9]+\s+(.+?);", "select $1 LIMIT " + word.Groups[1] + ";", RegexOptions.IgnoreCase);
				
				word = Regex.Match(src, "select top ([0-9]+)", RegexOptions.IgnoreCase);
				if(word.Success)
				{
					src = Regex.Replace(src, "select top [0-9]+", "select", RegexOptions.IgnoreCase);
					src = src + " LIMIT " + word.Groups[1];
				}

				src = Regex.Replace(src, "begin tran", "BEGIN", RegexOptions.IgnoreCase);
				src = Regex.Replace(src, "commit tran", "COMMIT", RegexOptions.IgnoreCase);
				src = Regex.Replace(src, "rollback tran", "ROLLBACK", RegexOptions.IgnoreCase);

				src = Regex.Replace(src, @"contains\s*\((.+?),(.+?)\)", "$1 match $2", RegexOptions.IgnoreCase);

				src = Regex.Replace(src, @"LEN\(", "LENGTH(", RegexOptions.IgnoreCase);
				
				src = Regex.Replace(src, "'true'", "1", RegexOptions.IgnoreCase);
				src = Regex.Replace(src, "'false'", "0", RegexOptions.IgnoreCase);

				src = src.Replace("'V ' +", "'V ' ||");

				bool FoundDeclare = false;
				Regex SearchDeclare = new Regex(@"declare\s+@(.+?)\s+table", RegexOptions.IgnoreCase);

				foreach(Match match in SearchDeclare.Matches(src))
				{
					string table = match.Groups[1].Value;
					
					FoundDeclare = true;
					src = Regex.Replace(src, "@" + table, table, RegexOptions.IgnoreCase);
					src = Regex.Replace(src, @"declare\s+" + table + @"\s+table\s*(\(.+?\));", "create temp table " + table + "$1;", RegexOptions.IgnoreCase);
				}

				if(FoundDeclare)
					src = Regex.Replace(src, @"\s+\b(int)\b", " integer", RegexOptions.IgnoreCase);

				Regex SearchDateTime = new Regex(@"Convert\(\s*datetime\s*,\s*(.+?),\s*103\s*\)", RegexOptions.IgnoreCase);
				foreach(Match match in SearchDateTime.Matches(src))
				{
					string table = match.Groups[1].Value.Replace('/', '-');
					src = src.Replace(match.Groups[0].Value, table);
				}

				break;
			}

			return src;
		}

//---------------------------------------------------------------------------------
	}
}
