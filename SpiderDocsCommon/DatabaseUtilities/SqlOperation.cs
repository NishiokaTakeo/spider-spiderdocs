// This class a home made O/R mapper.
// Only tested in Microsoft SQL server but this potentially works in MySQL and SQLite as well.

// --- Tuning up points ---
// INSERT can deal with multiple lines
// Bulk transactions function is a good idea to speed up.

using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using NLog;
using System.Timers;
using System.Data.SqlClient;
using System.Threading.Tasks;


//---------------------------------------------------------------------------------
namespace Spider.Data.Sql
{
    abstract public class SqlOperation
    {
        private static Logger logger = LogManager.GetLogger("SQL");
        private static Timer aTimer;

        //---------------------------------------------------------------------------------
        SqlOperationMode m_mode;
        string m_table;
        int m_max = 0;
        int ReadIdx = -1;
        public bool Distinct = false;
        bool transaction = false;
        string sql = ""; // for debug
        DbConnection m_sqlConn;
        DbCommand m_cmd;
        readonly int timeout = 600;
        List<string> m_group_by = new List<string>();
        bool _inprocess = false;


        //---------------------------------------------------------------------------------
        class ValueParameters
        {
            public ValueParameters(string param, object val)
            {
                this.m_params = param;
                this.m_params_val = val;
                //logger.Debug("SQL Stored-Parameters: {0} , {1}", param,val);
            }

            public string m_params;

            object _m_params_val;
            public object m_params_val
            {
                set
                {
                    if (value.GetType() == typeof(DateTime))
                        _m_params_val = ((DateTime)value).ToString(ConstData.DB_DATE_TIME);
                    else
                        _m_params_val = value;
                }
                get { return _m_params_val; }
            }
        }

        List<ValueParameters> m_params = new List<ValueParameters>();

        //---------------------------------------------------------------------------------
        class TargetFields
        {
            string _field;
            public string field
            {
                get { return _field; }
                set { _field = Spider.Data.Utilities.SqlConvert(value); }
            }

            public object val;
        }

        List<TargetFields> m_field = new List<TargetFields>();
        List<TargetFields> m_stparam = new List<TargetFields>();

        //---------------------------------------------------------------------------------
        // variables for results
        //---------------------------------------------------------------------------------
        DataTable _table;
        public DataTable table { get { return _table; } }

        class QueryResult : List<List<object>>
        {
            public List<object> Rows(int index)
            {
                if (index < this.Count)
                    return this[index];
                else
                    return null;
            }
        }

        QueryResult m_result = new QueryResult();
        public int ResultCount { get { return m_result.Count; } }

        //---------------------------------------------------------------------------------
        class Criteria
        {
            public List<SqlOperationCriteriaCollections> val = new List<SqlOperationCriteriaCollections>();
            public List<string> raw = new List<string>();

            public void Clear()
            {
                val.Clear();
                raw.Clear();
            }

            public void Add(string field, SqlOperationOperator operation, object val)
            {
                bool added = false;

                if ((operation == SqlOperationOperator.IN) || (operation == SqlOperationOperator.NOT_IN))
                {
                    if (val.GetType() == typeof(List<int>))
                    {
                        val = ((List<int>)val).ToArray();
                        Add(field, operation, (Array)val);
                        added = true;

                    } else if (val.GetType() == typeof(List<string>))
                    {
                        val = ((List<string>)val).ToArray();
                        Add(field, operation, (Array)val);
                        added = true;
                    }
                }

                if (!added)
                    Add(field, operation, new object[] { val });
            }

            public void Add(string field, SqlOperationOperator operation, Array vals)
            {
                if (((operation == SqlOperationOperator.IN) || (operation == SqlOperationOperator.NOT_IN))
                && (vals.Length <= 0))
                {
                    return;
                }

                SqlOperationCriteriaCollection criterias = new SqlOperationCriteriaCollection();
                criterias.m_criterias.Add(new SqlOperationCriteria(field, operation, vals));
                this.val.Add(new SqlOperationCriteriaCollections(criterias));
            }

            public void Add(SqlOperationCriteriaCollection criterias)
            {
                if (0 < criterias.m_criterias.Count)
                    this.val.Add(new SqlOperationCriteriaCollections(criterias));
            }

            public void Add(SqlOperationCriteriaCollections collections)
            {
                if (0 < collections.m_collections.Count)
                    this.val.Add(collections);
            }
        }

        Criteria m_where = new Criteria();
        Criteria m_having = new Criteria();

        //---------------------------------------------------------------------------------
        class Join
        {
            public List<SqlOperationJoinParameters> val = new List<SqlOperationJoinParameters>();
            public List<string[]> raw = new List<string[]>();

            public void Clear()
            {
                val.Clear();
                raw.Clear();
            }
        }

        Join m_inner_join = new Join();
        Join m_left_join = new Join();

        //---------------------------------------------------------------------------------
        class Order
        {
            string _field;
            public string field { get { return _field; } }

            en_order_by _direction;
            public en_order_by direction { get { return _direction; } }

            public Order(string Field, en_order_by Direction)
            {
                this._field = Spider.Data.Utilities.SqlConvert(Field);
                this._direction = Direction;
            }
        }

        public enum en_order_by
        {
            Ascent = 0,
            Descent,

            Max
        }

        List<Order> m_order = new List<Order>();
        string m_option = "";


        //---------------------------------------------------------------------------------
        abstract protected DbConnection GetSqlConnection();
        abstract protected DbCommand GetSqlCommand(string sql, DbConnection sqlConn);
        abstract protected DbDataAdapter GetSqlAdapter();

        public bool KeepConnection { get; set; } = false;

        //---------------------------------------------------------------------------------
        public SqlOperation()
        {
        }

        //---------------------------------------------------------------------------------
        public SqlOperation(string table, SqlOperationMode mode)
        {
            m_table = Spider.Data.Utilities.SqlConvert(table);
            m_mode = mode;
        }

        //---------------------------------------------------------------------------------
        public void Reload(string table, SqlOperationMode mode)
        {
            Init();

            m_table = Spider.Data.Utilities.SqlConvert(table);
            m_mode = mode;
        }
        void SetupAutoCloseConnection()
        {
            if (KeepConnection) {
                int goodByAt = 60 * 1000 * 5;   // 5 minutes

                aTimer = new System.Timers.Timer(goodByAt);

                //aTimer.Interval = 2000;
                aTimer.AutoReset = false;

                aTimer.Enabled = true;
                aTimer.Elapsed += ATimer_Elapsed;
            }

        }

        private void ATimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_inprocess) return;

            this.CloseConnection();
        }

        //---------------------------------------------------------------------------------
        void Init()
        {
            m_table = "";
            m_mode = SqlOperationMode.None;

            m_max = 0;
            m_field.Clear();

            ReadIdx = -1;
            m_result.Clear();
            _table = null;

            m_where.Clear();
            m_having.Clear();

            m_inner_join.Clear();
            m_left_join.Clear();

            m_group_by.Clear();
            m_order.Clear();

            m_params.Clear();

            if (!KeepConnection)
            {
                if (!transaction)
                    m_sqlConn = null;
            }

            m_cmd = null;

            sql = "";
        }

        //---------------------------------------------------------------------------------
        public bool Read()
        {
            if ((ReadIdx + 1) < m_result.Count)
            {
                ReadIdx++;
                return true;

            } else
                return false;
        }

        //---------------------------------------------------------------------------------
        public string this[string idx]
        {
            get { return Result(idx); }
        }

        public string Result(string field)
        {
            return Result<string>(field);
        }

        public int Result_Int(string field, int DefaultValue = -1)
        {
            return Result<int>(field, DefaultValue);
        }

        public T Result<T>(string field, object DefaultValue = null)
        {
            return Spider.Data.Utilities.ConvertFromObject<T>(Result_Obj(field), DefaultValue);
        }

        public object Result_Obj(string field)
        {
            if (ReadIdx < 0)
                return null;

            object ans = null;
            int idx;

            idx = m_field.FindIndex(a => PureName(a.field) == PureName(field));

            if (idx < 0)
                idx = m_field.FindIndex(a => a.field.Contains(field));

            if (idx < m_result.Rows(ReadIdx).Count)
            {
                object result = m_result.Rows(ReadIdx)[idx];
                if (result != DBNull.Value)
                    ans = result;
            }

            return ans;
        }

        /// <summary>
        /// Trim [] in field name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string PureName(string name)
        {
            name = name.Replace("[", "").Replace("]", "");

            return name;
        }

//---------------------------------------------------------------------------------
		// For SELECT
		public void Field(string field)
		{
			if((m_mode == SqlOperationMode.Select)
			|| (m_mode == SqlOperationMode.Select_Scalar)
            || (m_mode == SqlOperationMode.StoredProcedure)
            || (m_mode == SqlOperationMode.Function)
            || (m_mode == SqlOperationMode.Select_Table))
			{
				SetField(field);
			}
		}

		// For SELECT
		public void Fields(params string[] fields)
		{
			if((m_mode == SqlOperationMode.Select)
			|| (m_mode == SqlOperationMode.Select_Scalar)
            || (m_mode == SqlOperationMode.StoredProcedure)
            || (m_mode == SqlOperationMode.Function)
            || (m_mode == SqlOperationMode.Select_Table))
			{
				foreach(string field in fields)
					SetField(field);
			}
		}

		// For UPDATE, INSERT
		public void Fields(string[] fields, object[] vals)
		{
			if(((m_mode == SqlOperationMode.Insert) || (m_mode == SqlOperationMode.Update) || (m_mode == SqlOperationMode.StoredProcedure) || (m_mode == SqlOperationMode.Function))
			&&  (fields.Length == vals.Length))
			{
				int i = 0;
				foreach(string field in fields)
					SetField(field, vals[i++]);
			}
		}

		// For UPDATE, INSERT
		public void Field(string field, object val)
		{
			if((m_mode == SqlOperationMode.Insert) || (m_mode == SqlOperationMode.Update) || (m_mode == SqlOperationMode.StoredProcedure) || (m_mode == SqlOperationMode.Function))
				SetField(field, val);
		}
        public void SetCommandParameter(string field, object val )
        {
            TargetFields wrk = new TargetFields();

            wrk.field = field;
            wrk.val = val;

            m_stparam.Add(wrk);
        }

		void SetField(string field, object val = null)
		{
			TargetFields wrk = new TargetFields();

			wrk.field = field;
			wrk.val = val;

			m_field.Add(wrk);
		}

//---------------------------------------------------------------------------------
		public void InnerJoin(SqlOperationJoinParameters parameters) { m_inner_join.val.Add(parameters); }
		public void InnerJoin(string join, string on) { m_inner_join.raw.Add(new string[] { join, on }); }
		public void InnerJoin(string BaseTable, string TargetTable, string Field) { m_inner_join.val.Add(new SqlOperationJoinParameters(BaseTable, TargetTable, Field, Field)); }
		public void InnerJoin(string BaseTable, string TargetTable, string BaseFiel, string TargetField) { m_inner_join.val.Add(new SqlOperationJoinParameters(BaseTable, TargetTable, BaseFiel, TargetField));	}

//---------------------------------------------------------------------------------
		public void LeftJoin(SqlOperationJoinParameters parameters) { m_left_join.val.Add(parameters); }
		public void LeftJoin(string join, string on) { m_left_join.raw.Add(new string[] { join, on }); }
		public void LeftJoin(string BaseTable, string TargetTable, string Field) { m_left_join.val.Add(new SqlOperationJoinParameters(BaseTable, TargetTable, Field, Field)); }
		public void LeftJoin(string BaseTable, string TargetTable, string BaseFiel, string TargetField) { m_left_join.val.Add(new SqlOperationJoinParameters(BaseTable, TargetTable, BaseFiel, TargetField));	}

//---------------------------------------------------------------------------------
		public void Where(SqlOperationCriteriaCollections collections) { m_where.Add(collections); }
		public void Where(SqlOperationCriteriaCollection criterias)	{ m_where.Add(criterias); }
		public void Where(string where, object val) { m_where.Add(where, SqlOperationOperator.EQUAL, val); }
		public void Where(string where, SqlOperationOperator operation, object val) { m_where.Add(where, operation, val); }
		public void Where_In(string where, Array vals) { m_where.Add(where, SqlOperationOperator.IN, vals);	}
		public void Where_In(string where, object val) { m_where.Add(where, SqlOperationOperator.IN, val); }
		public void Where_In(string where, params object[] vals) { m_where.Add(where, SqlOperationOperator.IN, (Array)vals); }
		public void Where_Not_In(string where, Array vals) { m_where.Add(where, SqlOperationOperator.NOT_IN, vals);	}
		public void Where_Not_In(string where, object val) { m_where.Add(where, SqlOperationOperator.NOT_IN, val); }
		public void Where_Not_In(string where, params object[] vals) { m_where.Add(where, SqlOperationOperator.NOT_IN, (Array)vals); }
		public void Where(string where) { m_where.raw.Add(where); }

		public void Having(SqlOperationCriteriaCollections collections)	{ m_having.Add(collections); }
		public void Having(SqlOperationCriteriaCollection criterias) { m_having.Add(criterias); }
		public void Having(string having, object val) { m_having.Add(having, SqlOperationOperator.EQUAL, val); }
		public void Having(string having, SqlOperationOperator operation, object val) { m_having.Add(having, operation, val); }
		public void Having_In(string having, Array vals) { m_having.Add(having, SqlOperationOperator.IN, vals);	}
		public void Having_In(string having, object val) { m_having.Add(having, SqlOperationOperator.IN, val); }
		public void Having_In(string having, params object[] vals) { m_having.Add(having, SqlOperationOperator.IN, (Array)vals); }
		public void Having_Not_In(string having, Array vals) { m_having.Add(having, SqlOperationOperator.NOT_IN, vals);	}
		public void Having_Not_In(string having, object val) { m_having.Add(having, SqlOperationOperator.NOT_IN, val); }
		public void Having_Not_In(string having, params object[] vals) { m_having.Add(having, SqlOperationOperator.NOT_IN, (Array)vals); }
		public void Having(string having) { m_having.raw.Add(having); }

//---------------------------------------------------------------------------------
		public void GroupBy(string field)
		{
			m_group_by.Add(Spider.Data.Utilities.SqlConvert(field));
		}

//---------------------------------------------------------------------------------
		public void OrderBy(string field, en_order_by direction)
		{
			m_order.Add(new Order(field, direction));
		}

		public void Option(string opt)
		{
			this.m_option = opt;
		}
//---------------------------------------------------------------------------------
		public void SetMaxResult(int val)
		{
			m_max = val;
		}

//---------------------------------------------------------------------------------
		public object Commit()
		{
			return Commit<object>();
		}

        void SqlOperationModeSelect()
        {
            sql = Select();
            BindParams(m_mode);

            m_cmd = GetSqlCommand(sql, m_sqlConn);
            SetSQLTimeout();

            int i = 0;
            for (i = 0; i < m_params.Count; i++)
                setSqlParameter(m_params[i].m_params, TypeToDbType(m_params[i].m_params_val.GetType()), m_params[i].m_params_val, m_cmd);

            ClearResult();

            Commit_Select();

        }

        T SqlOperationModeSelect_Scalar<T>()
        {
            var ans = default(T);

            sql = Select();
            BindParams(m_mode);


            m_cmd = GetSqlCommand(sql, m_sqlConn);
            SetSQLTimeout();

            int i = 0;
            for (i = 0; i < m_params.Count; i++)
                setSqlParameter(m_params[i].m_params, TypeToDbType(m_params[i].m_params_val.GetType()), m_params[i].m_params_val, m_cmd);

            ClearResult();

            object wrk = Commit_Select_Scalar();

            if ((wrk != null) & (wrk != DBNull.Value))
                ans =(T)Convert.ChangeType(wrk, typeof(T));

            return ans;

        }

        void SqlOperationModeSelect_Table()
        {
            sql = Select();
            BindParams(m_mode);

            m_cmd = GetSqlCommand(sql, m_sqlConn);
            SetSQLTimeout();

            int i = 0;
            for (i = 0; i < m_params.Count; i++)
                setSqlParameter(m_params[i].m_params, TypeToDbType(m_params[i].m_params_val.GetType()), m_params[i].m_params_val, m_cmd);

            ClearResult();

            Commit_Select_Table();
        }

        T SqlOperationModeInsert<T>()
        {
            var ans = default(T);


            sql = Insert();

            BindParams(m_mode);

            m_cmd = GetSqlCommand(sql, m_sqlConn);
            SetSQLTimeout();

            int i = 0;
            for (i = 0; i < m_params.Count; i++)
                setSqlParameter(m_params[i].m_params, TypeToDbType(m_params[i].m_params_val.GetType()), m_params[i].m_params_val, m_cmd);

            ClearResult();

            object wrk = Commit_Select_Scalar();

            if ((wrk != null) & (wrk != DBNull.Value))
                ans = (T)Convert.ChangeType(wrk, typeof(T));

            return ans;

        }

        void SqlOperationModeUpdate()
        {
            sql = Update();


            BindParams(m_mode);

            m_cmd = GetSqlCommand(sql, m_sqlConn);
            SetSQLTimeout();


            int i = 0;
            for (i = 0; i < m_params.Count; i++)
                setSqlParameter(m_params[i].m_params, TypeToDbType(m_params[i].m_params_val.GetType()), m_params[i].m_params_val, m_cmd);

            ClearResult();
            //logger.Trace("SQL Execution(BF) : {0}", m_cmd.CommandText);
            m_cmd.ExecuteNonQuery();
        }

        void SqlOperationModeDelete()
        {
            sql = Delete();

            BindParams(m_mode);

            m_cmd = GetSqlCommand(sql, m_sqlConn);

            int i = 0;
            for (i = 0; i < m_params.Count; i++)
                setSqlParameter(m_params[i].m_params, TypeToDbType(m_params[i].m_params_val.GetType()), m_params[i].m_params_val, m_cmd);

            ClearResult();
            //logger.Trace("SQL Execution(BF) : {0}", m_cmd.CommandText);
            m_cmd.ExecuteNonQuery();
        }

        void SqlOperationModeStoredProcedure()
        {
            sql = m_table;


            BindParams(m_mode);

            //m_cmd = GetSqlCommand(sql, m_sqlConn);
            var con = m_sqlConn as SqlConnection;
            m_cmd = new SqlCommand(m_table, con);
            m_cmd.CommandType = CommandType.StoredProcedure;
            m_cmd.Parameters.Clear();

            SetSQLTimeout();
            string logText = "";
            for (int i = 0; i < m_stparam.Count(); i++)
            {
                var f = m_stparam[i];

                logText = $"name:@{f.field}, value:{f.val}, ";

                (m_cmd as SqlCommand).Parameters.AddWithValue("@" + f.field, f.val);
            }

            logger.Debug("[Parameters] : {0}", logText.TrimEnd(new char[]{',', ' '}));

            ClearResult();

            Commit_Select();

        }

        /// <summary>
        /// Run SQL Query for Table Value Function
        /// </summary>
        void SqlOperationModeFunction()
        {

            sql = Select();
            BindParams(m_mode);

            m_cmd = GetSqlCommand(sql, m_sqlConn);
            SetSQLTimeout();

            for (int i = 0; i < m_params.Count; i++)
                setSqlParameter(m_params[i].m_params, TypeToDbType(m_params[i].m_params_val.GetType()), m_params[i].m_params_val, m_cmd);

            // Add Function parameters
            string logText = "";
            for (int i = 0; i < m_stparam.Count(); i++)
            {
                var f = m_stparam[i];

                logText = $"name:@{f.field}, value:{f.val}, ";

                (m_cmd as SqlCommand).Parameters.AddWithValue("@" + f.field, f.val);
            }

            logger.Debug("[Parameters] : {0}", logText.TrimEnd(new char[]{',', ' '}));

            ClearResult();

            Commit_Select();
        }

        void SetSQLTimeout()
        {
            m_cmd.CommandTimeout = timeout;
        }

        void ClearResult()
        {
            ReadIdx = -1;
            m_result.Clear();
        }

        public T Commit<T>()
        {
            //logger.Trace("========== Begining of Commit Method ==========");
            DateTime start = DateTime.Now;

            _inprocess = true;

            T ans = default(T);

            if (m_sqlConn == null)
            {
                m_sqlConn = GetSqlConnection();
            }

            if (m_sqlConn.State == ConnectionState.Closed)
                m_sqlConn.Open();

            try
            {

                switch (m_mode)
                {
                    case SqlOperationMode.Select:
                        SqlOperationModeSelect();

                        break;
                    case SqlOperationMode.Select_Scalar:
                        ans = SqlOperationModeSelect_Scalar<T>();

                        break;
                    case SqlOperationMode.Select_Table:
                        SqlOperationModeSelect_Table();

                        break;

                    case SqlOperationMode.Insert:
                        ans = SqlOperationModeInsert<T>();

                        break;

                    case SqlOperationMode.Update:
                        SqlOperationModeUpdate();

                        break;

                    case SqlOperationMode.Delete:
                        SqlOperationModeDelete();

                        break;
                    case SqlOperationMode.StoredProcedure:
                        SqlOperationModeStoredProcedure();

                        break;
                    case SqlOperationMode.Function:
                        SqlOperationModeFunction();

                        break;
                }
            }

            catch (System.Threading.ThreadAbortException){ }
            catch (Exception err)
            {
                string sqltxt = m_cmd.CommandText;

                for (int i = m_params.Count() - 1; i >= 0; i--)
                {
                    ValueParameters x = m_params[i];

                    Type t = x.m_params_val.GetType();
                    string val = t == string.Empty.GetType() ? "'" + x.m_params_val?.ToString() + "'" : x.m_params_val?.ToString();

                    sqltxt = sqltxt.Replace(x.m_params, val);
                }

                try
                {
                    if (logger.IsErrorEnabled)
                        logger.Error(err, "SQL Operation Error : {0} {1}", sqltxt, Newtonsoft.Json.JsonConvert.SerializeObject(m_params));
                }
                catch { }

                throw err;
            }
            finally
            {
                _inprocess = false;

                try
                {
                    if( m_mode == SqlOperationMode.Delete)
                        logger.Info("DELETE-SQL: {0} {1} {2}", DateTime.Now.Subtract(start).ToString(@"ss\.ff"), m_cmd.CommandText, Newtonsoft.Json.JsonConvert.SerializeObject(m_params));
                    else if (logger.IsTraceEnabled)
                        logger.Trace("SQL Execution : {0} {1} {2}", DateTime.Now.Subtract(start).ToString(@"ss\.ff"), m_cmd.CommandText, Newtonsoft.Json.JsonConvert.SerializeObject(m_params));

                }
                catch { }

                if (!KeepConnection)
                {
                    if (!transaction)
                        CloseConnection();
                }

                SetupAutoCloseConnection();

            }

            return ans;
        }



        //      public T Commit<T>()
        //{
        //          //logger.Trace("========== Begining of Commit Method ==========");
        //          _inprocess = true;

        //          T ans = default(T);

        //	if(m_sqlConn == null)
        //	{
        //		m_sqlConn = GetSqlConnection();

        //		if(m_sqlConn.State == ConnectionState.Closed)
        //			m_sqlConn.Open();
        //	}

        //	// Create SQL Query
        //	switch(m_mode)
        //	{
        //	    case SqlOperationMode.Select:
        //	    case SqlOperationMode.Select_Scalar:
        //	    case SqlOperationMode.Select_Table:
        //		    sql = Select();
        //		    break;

        //	    case SqlOperationMode.Insert:
        //		    sql = Insert();
        //		    break;

        //	    case SqlOperationMode.Update:
        //		    sql = Update();
        //		    break;

        //	    case SqlOperationMode.Delete:
        //		    sql = Delete();
        //		    break;
        //              case SqlOperationMode.StoredProcedure:
        //                      sql = m_table;
        //                  break;
        //          }



        //          //logger.Debug("SQL Base Command: {0}", sql);
        //          /*
        //          int i = 0;
        //	foreach(TargetFields field in m_field)
        //	{
        //		object wrk = field.val;

        //		if(wrk == null)
        //			wrk = DBNull.Value;

        //		m_params.Add(new ValueParameters("@param" + i, wrk));

        //		i++;
        //	}
        //          */

        //          BindParams(m_mode);

        //          m_cmd = GetSqlCommand(sql, m_sqlConn);

        //              int i = 0;
        //              for (i = 0; i < m_params.Count; i++)
        //		    setSqlParameter(m_params[i].m_params, TypeToDbType(m_params[i].m_params_val.GetType()), m_params[i].m_params_val, m_cmd);

        //          ReadIdx = -1;
        //	m_result.Clear();

        //	try
        //	{
        //              //logger.Debug("SQL Command Execute: {0}", sql);

        //		// Execute SQL Query
        //		switch(m_mode)
        //		{
        //		case SqlOperationMode.Select:
        //			Commit_Select();
        //			break;

        //		case SqlOperationMode.Select_Scalar:
        //		case SqlOperationMode.Insert:
        //			object wrk = Commit_Select_Scalar();

        //			if((wrk != null) & (wrk != DBNull.Value))
        //				ans = (T)Convert.ChangeType(wrk, typeof(T));

        //			break;

        //		case SqlOperationMode.Select_Table:
        //			Commit_Select_Table();
        //			break;

        //		case SqlOperationMode.Update:
        //		case SqlOperationMode.Delete:
        //			m_cmd.ExecuteNonQuery();
        //			break;

        //              case SqlOperationMode.StoredProcedure:
        //                      m_cmd.ExecuteNonQuery();
        //                  break;

        //              }

        //          }catch(Exception err)
        //	{
        //              string sqltxt = m_cmd.CommandText;
        //              m_params.ForEach(x =>
        //              {
        //                  Type t = x.m_params_val.GetType();
        //                  string val = t == string.Empty.GetType() ? "'" + x.m_params_val.ToString() + "'" : x.m_params_val.ToString();

        //                  sqltxt = sqltxt.Replace(x.m_params, val);
        //              });

        //              logger.Error(err, "SQL Operation Error : {0} {1}", sqltxt, Newtonsoft.Json.JsonConvert.SerializeObject(m_params));


        //              throw err;
        //	}finally
        //          {
        //              _inprocess = false;

        //              logger.Trace("SQL Execution : {0} {1}", m_cmd.CommandText, Newtonsoft.Json.JsonConvert.SerializeObject(m_params));
        //              if (!KeepConnection)
        //              {
        //                  if (!transaction)
        //                      CloseConnection();
        //              }

        //              SetupAutoCloseConnection();

        //          }

        //	return ans;
        //}

        public void BindParams(SqlOperationMode mode)
        {

            //int i = 0;
            for(int i =0; i < m_field.Count(); i++)
            {
                TargetFields field = m_field[i];

                object wrk = field.val;
                string name = (mode != SqlOperationMode.StoredProcedure) ? string.Format("@param{0}", i) : string.Format("@{0}", field.field);

                if (wrk == null)
                    wrk = DBNull.Value;

                m_params.Add(new ValueParameters(name, wrk));

            }
            /*
            foreach (TargetFields field in m_field)
            {
                object wrk = field.val;
                string name = mode != SqlOperationMode.StoredProcedure ? string.Format("@param{0}", i) : string.Format("@{0}{1}", field.field, i);

                if (wrk == null)
                    wrk = DBNull.Value;

                m_params.Add(new ValueParameters(name, wrk));


                i++;
            }
            */
        }

        public void CloseConnection()
        {
            if (m_sqlConn == null) return;

            if (m_sqlConn.State == ConnectionState.Open)
                m_sqlConn.Close();
        }
//---------------------------------------------------------------------------------
		string Select()
		{
			string sql = "select ";

			if(Distinct)
				sql += "distinct ";

			if(0 < m_max)
				sql += "top " + m_max + " ";

			if(0 < m_field.Count)
				sql += (String.Join(", ", m_field.Select(a => a.field).ToArray()));
			else
				sql += "*";

			sql += " from " + m_table;

			sql = MakeJoin(sql);

			sql = MakeWhere(sql);

			sql = MakeGroupBy(sql);
			sql = MakeHaving(sql);

			sql = MakeOrderBy(sql);

			sql = MakeOption(sql);

			return sql;
		}

//---------------------------------------------------------------------------------
	   void Commit_Select()
	   {
            //logger.Trace("SQL Execution(BF) : {0}", m_cmd.CommandText);

            DbDataReader dr = m_cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
			{
                List<object> ret = new List<object>();
                foreach (TargetFields field in m_field)
				{
                    int idx = HasColumn(dr,field.field) ? dr.GetOrdinal(field.field) : m_field.IndexOf(field);

                    ret.Add(dr[idx]);
				}

				m_result.Add(ret);
			}

			dr.Close();
        }


        bool HasColumn(DbDataReader dr, string name)
        {
            for(int i =0;i< dr.FieldCount;i++)
            {
                if( dr.GetName(i).Equals(name, StringComparison.InvariantCultureIgnoreCase)) return true;
            }

            return false;
        }

        //---------------------------------------------------------------------------------
        object Commit_Select_Scalar()
		{
            //logger.Trace("SQL Execution(BF) : {0}", m_cmd.CommandText);

            return Utilities.ObjectClone<object>(m_cmd.ExecuteScalar());
		}

//---------------------------------------------------------------------------------
		void Commit_Select_Table()
		{
            try {
			    if(table == null)
				    _table = new DataTable();
			    else
			    {
				    _table.Columns.Clear();
				    _table.Clear();
			    }

			    DbDataAdapter adapter = GetSqlAdapter();
			    adapter.SelectCommand = m_cmd;
			    adapter.Fill(table);
            }catch(Exception ex)
            {
                logger.Error(ex);
            }

        }

//---------------------------------------------------------------------------------
		string Insert()
		{
			string sql = "insert into " + m_table + " (";
			sql += (String.Join(", ", m_field.Select(a => a.field).ToArray()));

			sql += ") values (";

			string[] tmp_param = new string[m_field.Count];
			for(int i = 0; i < m_field.Count; i++)
				tmp_param[i] = "@param" + i;

			sql += (String.Join(" , ", tmp_param));// this first space is important. do not remove it.

			sql += "); SELECT SCOPE_IDENTITY();";

			return sql;
		}

//---------------------------------------------------------------------------------
		string Update()
		{
			string sql = "update " + m_table + " set ";

			string[] tmp_param = new string[m_field.Count];
			for(int i = 0; i < m_field.Count; i++)
				tmp_param[i] = m_field[i].field + " = @param" + i;

			sql += (String.Join(" , ", tmp_param));

			sql = MakeWhere(sql);

			return sql;
		}

//---------------------------------------------------------------------------------
		string Delete()
		{
			string sql = "delete from " + m_table;
			sql = MakeWhere(sql);

			return sql;
		}

//---------------------------------------------------------------------------------
		string MakeWhere(string sql)
		{
			return MakeFilter(sql, m_where, "WHERE");
		}

		string MakeHaving(string sql)
		{
			return MakeFilter(sql, m_having, "HAVING");
		}

		string MakeFilter(string sql, Criteria src, string keyword)
		{
			string ans = " " + keyword + " ";
			char prefix = keyword[0];

			if((0 < src.raw.Count) || (0 < src.val.Count))
			{
				int i = 0;
				string[] StrCriterias = new string[src.val.Count + src.raw.Count];

                foreach (string str in src.raw)
                {
                    string rep = Utilities.ReplaceEOL2Space(str);

                    StrCriterias[i] = "(" + rep + ")";
                    i++;
                }

                foreach (SqlOperationCriteriaCollections collections in src.val)
				{
					string[] StrCollections = new string[collections.m_collections.Count];
					int j = 0;
					foreach(SqlOperationCriteriaCollection criterias in collections.m_collections)
					{
                        string sqlparts = MakeFilter(criterias, prefix, i, j, 0);
                        StrCollections[j] = Utilities.ReplaceEOL2Space(sqlparts);

                        j++;
					}

					StrCriterias[i] = "(" + String.Join(" " + collections.m_AndOr.ToString() + " ", StrCollections) + ")";
					i++;
				}

				ans += (String.Join(" AND ", StrCriterias));

				sql += ans;
			}

			return sql;
		}

		string MakeFilter(SqlOperationCriteriaCollection criterias, char prefix, int i, int j, int level)
		{
			string ans;

			int k = 0;
			string[] StrCriteria = new string[criterias.m_criterias.Count];
			foreach(SqlOperationCriteria criteria in criterias.m_criterias)
			{
				StrCriteria[k] = "(";

				if((criteria.m_where_val == null) || (criteria.m_where_val.GetValue(0) == null))
				{
					switch(criteria.m_operator)
					{
					case SqlOperationOperator.RAW:
						StrCriteria[k] += criteria.m_where;
						break;
					case SqlOperationOperator.EQUAL:
						StrCriteria[k] += criteria.m_where + " IS NULL";
						break;
					case SqlOperationOperator.NOT_EQUAL:
						StrCriteria[k] += criteria.m_where + " IS NOT NULL";
						break;
					}

				}else if(criteria.m_where_val.GetValue(0).GetType() == typeof(Period))
				{
					string[] StrParams = new string[2];
					Period period = (Period)criteria.m_where_val.GetValue(0);

					if(period.From != new DateTime())
					{
						StrParams[0] = "@" + prefix + "param" + i + "_" + j + "_" + level + "_" + k + "_0";

						m_params.Add(new ValueParameters(StrParams[0], period.From));
					}

					if(period.To != new DateTime())
					{
						StrParams[1] = "@" + prefix + "param" + i + "_" + j + "_" + level + "_" + k + "_1";

						m_params.Add(new ValueParameters(StrParams[1], period.To));
					}

					if(!String.IsNullOrEmpty(StrParams[0]) && !String.IsNullOrEmpty(StrParams[1]))
						StrCriteria[k] += "(" + StrParams[0] + " <= " + criteria.m_where + " AND " + StrParams[1] + " >= " + criteria.m_where + ")";
					else if(!String.IsNullOrEmpty(StrParams[0]))
						StrCriteria[k] += StrParams[0] + " <= " + criteria.m_where;
					else if(!String.IsNullOrEmpty(StrParams[1]))
						StrCriteria[k] += StrParams[1] + " >= " + criteria.m_where;

				}else if(criteria.m_operator == SqlOperationOperator.CONTAINS)
				{
					List<string> StrKeyword = new List<string>();
					foreach(object where_val in criteria.m_where_val)
						StrKeyword.Add("'\"*" + where_val.ToString() + "*\"'");

					StrCriteria[k] += "CONTAINS (" + criteria.m_where + ", " + String.Join(" OR ", StrKeyword);

				}else
				{
					int l = 0;
					string[] StrParams = new string[criteria.m_where_val.Length];

					foreach(object where_val in criteria.m_where_val)
					{
						object val = where_val;

                        StrParams[l] = "@" + prefix + "param" + i + "_" + j + "_" + level + "_" + k + "_" + l ;

                        //
                        // Like only
                        //

                        switch (criteria.m_operator)
                        {
                            case SqlOperationOperator.LIKE_BOTH:
                                val = string.Format("%{0}%", criteria.Escape4Like((string)val));
                                break;
                            case SqlOperationOperator.LIKE_BEGINNING:
                                val = string.Format("%{0}", criteria.Escape4Like((string)val));
                                break;
                            case SqlOperationOperator.LIKE_END:
                                val = string.Format("{0}%", criteria.Escape4Like((string)val));
                                break;
                        }

                        m_params.Add(new ValueParameters(StrParams[l], val));

						l++;
					}

					switch(criteria.m_operator)
					{
					case SqlOperationOperator.EQUAL:
						StrCriteria[k] += criteria.m_where + " = " + StrParams[0];
						break;
					case SqlOperationOperator.NOT_EQUAL:
						StrCriteria[k] += criteria.m_where + " <> " + StrParams[0];
						break;
					case SqlOperationOperator.GREATER_THAN:
						StrCriteria[k] += criteria.m_where + " > " + StrParams[0];
						break;
					case SqlOperationOperator.LESS_THAN:
						StrCriteria[k] += criteria.m_where + " < " + StrParams[0];
						break;
					case SqlOperationOperator.GREATER_THAN_OR_EQUAL_TO:
						StrCriteria[k] += criteria.m_where + " >= " + StrParams[0];
						break;
					case SqlOperationOperator.LESS_THAN_OR_EQUAL_TO:
						StrCriteria[k] += criteria.m_where + " <= " + StrParams[0];
						break;
					case SqlOperationOperator.IN:
						StrCriteria[k] += criteria.m_where + " IN (" + String.Join(", ", StrParams) + ")";
						break;
					case SqlOperationOperator.NOT_IN:
						StrCriteria[k] += criteria.m_where + " NOT IN (" + String.Join(", ", StrParams) + ")";
						break;
					case SqlOperationOperator.LIKE_BOTH:
					case SqlOperationOperator.LIKE_BEGINNING:
					case SqlOperationOperator.LIKE_END:

                            string escape = string.Empty;
                        if (criteria.EscapeLike != string.Empty)
                                escape = " ESCAPE '" + criteria.EscapeLike + "'";

                            StrCriteria[k] += criteria.m_where + " LIKE " + StrParams[0] + escape;
						break;
					}
				}

				StrCriteria[k] += ")";
				if(StrCriteria[k] != "()")
					k++;
			}

			ans = String.Join(" " + criterias.m_AndOr.ToString() + " ", StrCriteria);
			ans = "(" + ans + ")";

			return ans;
		}

//---------------------------------------------------------------------------------
		string MakeJoin(string sql)
		{
			sql += MakeJoin(m_inner_join, "inner");
			sql += MakeJoin(m_left_join, "left");

			return sql;
		}

		string MakeJoin(Join join, string prefix)
		{
			string sql = "";
			prefix = " " + prefix + " ";

			if(0 < join.raw.Count)
			{
				string[] tmp_param = new string[join.raw.Count];
				for(int i = 0; i < join.raw.Count; i++)
					tmp_param[i] = join.raw[i][0] + " on " + join.raw[i][1];

				sql += prefix + "join " + String.Join(prefix + "join ", tmp_param);
			}

			if(0 < join.val.Count)
			{
				int i = 0;
				string[] tmp_param = new string[join.val.Count];
				foreach(SqlOperationJoinParameters param in join.val)
				{
					tmp_param[i] = param.TargetTable + " on ";

					int j = 0;
					string[] tmp_fields = new string[param.FieldRelationship.Count];
					foreach(SqlOperationJoinParameters.Relationship relationship in param.FieldRelationship)
					{
						tmp_fields[j] = relationship.BaseTable + "." + relationship.BaseField + " = " + param.TargetTable + "." + relationship.TargetField;
						j++;
					}

					tmp_param[i] += String.Join(" AND ", tmp_fields);
					i++;
				}

				sql += prefix + "join " + String.Join(prefix + "join ", tmp_param);
			}

			return sql;
		}

//---------------------------------------------------------------------------------
		string MakeGroupBy(string sql)
		{
			if(0 < m_group_by.Count)
			{
				sql += " group by ";
				sql += (String.Join(", ", m_group_by.ToArray()));
			}

			return sql;
		}

//---------------------------------------------------------------------------------
		string MakeOrderBy(string sql)
		{
			if(0 < m_order.Count)
			{
				sql += " order by ";

				string[] tmp_param = new string[m_order.Count];
				for(int i = 0; i < m_order.Count; i++)
				{
					tmp_param[i] = m_order[i].field;

					if(m_order[i].direction == en_order_by.Descent)
						tmp_param[i] += " DESC";
				}

				sql += (String.Join(", ", tmp_param));
			}

			return sql;
		}

		string MakeOption(string sql)
		{
			if( !string.IsNullOrWhiteSpace(m_option))
			{
				sql += $" OPTION ({m_option}) ";
			}
			return sql;
		}

//---------------------------------------------------------------------------------
        /*
		void setSqlParameter(
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
        */
        /*
//---------------------------------------------------------------------------------
		void setSqlParameter(string name, DbType type, int size, object val, DbCommand cmd)
		{
			DbParameter parameter = cmd.CreateParameter();
			setSqlParameter(name, type, val, parameter);
			parameter.Size = size;
			cmd.Parameters.Add(parameter);
		}
        */
//---------------------------------------------------------------------------------
		void setSqlParameter(string name, DbType type, object val, DbCommand cmd)
		{
			if(val == DBNull.Value)
			{
				// put space is very important. if it is removed then @param1 will replace @param1 and also @param1*
				m_cmd.CommandText = m_cmd.CommandText.Replace(name + " " , "NULL ");

				//logger.Debug("SQL Actual-Parameters: {0} , {1} , {2}", name, "NULL");

			}else
			{
				DbParameter parameter = cmd.CreateParameter();
				//setSqlParameter(name, type, val, parameter);

                parameter.ParameterName = name;
                parameter.DbType = type;
                parameter.Value = val;

                cmd.Parameters.Add(parameter);
			}

		}
        void setSqlParameter(ValueParameters vparam, DbCommand cmd)
        {
            string name = vparam.m_params;
            DbType type = TypeToDbType(vparam.m_params_val.GetType());
            object val = vparam.m_params_val;
            string option = vparam.m_params;

            if (val == DBNull.Value)
            {
                // put space is very important. if it is removed then @param1 will replace @param1 and also @param1*
                m_cmd.CommandText = m_cmd.CommandText.Replace(name + " ", "NULL ");

                //logger.Debug("SQL Actual-Parameters: {0} , {1} , {2}", name, "NULL");

            }
            else
            {
                DbParameter parameter = cmd.CreateParameter();
                //setSqlParameter(name, type, val, parameter);

                parameter.ParameterName = name;
                parameter.DbType = type;
                parameter.Value = val;


                cmd.Parameters.Add(parameter);
            }

        }
        //---------------------------------------------------------------------------------
        /*
                void setSqlParameter(string name, DbType type, object val, DbParameter parameter)
                {
                    parameter.ParameterName = name;
                    parameter.DbType = type;
                    parameter.Value = val;

                    //logger.Debug("SQL Actual-Parameters: {0} , {1} , {2}", name, type.ToString(), val.ToString());
                }
        */
        //---------------------------------------------------------------------------------
        public DbConnection BeginTran()
		{
			m_sqlConn = GetSqlConnection();
			DbCommand cmd = GetSqlCommand("begin tran", m_sqlConn);
			cmd.ExecuteNonQuery();

			transaction = true;

			return m_sqlConn;
		}

//---------------------------------------------------------------------------------
		public void CommitTran()
		{
			DbCommand cmd = GetSqlCommand("commit tran", m_sqlConn);
			cmd.ExecuteNonQuery();

			transaction = false;
		}

//---------------------------------------------------------------------------------
		public void RollBack()
		{
			DbCommand cmd = GetSqlCommand("rollback tran", m_sqlConn);
			cmd.ExecuteNonQuery();

			transaction = false;
		}

//---------------------------------------------------------------------------------
		public void SetOutputTable(DataTable dst)
		{
			_table = dst;
		}

//---------------------------------------------------------------------------------
		public static DbType TypeToDbType(Type t)
		{
			DbType dbt;

			try
			{
                if (t == typeof(byte[]))
                    dbt = DbType.Binary;
                else if (t == typeof(DBNull))
                    dbt = DbType.Object;
                else if (Enum.TryParse<DbType>(t.Name, out dbt)) { }
                //dbt = (DbType)Enum.Parse(typeof(DbType), t.Name);
                else
                    dbt = DbType.Object;
            }
			catch
			{
				dbt = DbType.Object;
			}

			return dbt;
		}

//---------------------------------------------------------------------------------
		public int GetMaxId()
		{
			return GetMax("id");
		}

//---------------------------------------------------------------------------------
		public int GetMax(string field)
		{
			this.Fields("MAX(" + field + ")");
			return Convert.ToInt32(this.Commit());
		}

//---------------------------------------------------------------------------------
		public int GetCount(string field)
		{
			this.Fields("COUNT(" + field + ")");
			return Convert.ToInt32(this.Commit());
		}

//---------------------------------------------------------------------------------
		public int GetCountId()
		{
			return GetCount("id");
		}

//---------------------------------------------------------------------------------
		public void GetRecordInMaxOf(string max_of, params string[] group_by)
		{
			for(int i = 0; i < group_by.Count(); i++)
				group_by[i] = String.Format(this.m_table + ".{0} = m2.{0}", group_by[i]);

			string on = String.Join(" AND ", group_by);
			on += String.Format(" AND " + this.m_table + ".{0} < m2.{0}", max_of);

			this.LeftJoin(this.m_table + " m2", on);
			this.Where("m2." + max_of, SqlOperationOperator.EQUAL, null);

			this.Commit();
		}

        public static string ReplaceEOL2Space(string str)
        {
            string rep = System.Text.RegularExpressions.Regex.Replace(str, @"\t|\n|\r", " ");

            rep = System.Text.RegularExpressions.Regex.Replace(str, @"\s+", " ");

            return rep;
        }

        //---------------------------------------------------------------------------------
    }
}
