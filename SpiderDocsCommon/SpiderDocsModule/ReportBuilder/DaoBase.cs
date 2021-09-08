using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ReportBuilder.Models;
using System.Text.RegularExpressions;
using System.Data;
using System.Reflection;

namespace ReportBuilder.Controllers
{
    public class DaoBase : IDisposable
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        Logger backuplogger = LogManager.GetLogger("SQLBackup");
        SqlConnection _connection;
        string _constr = string.Empty;
        protected SqlConnection DBConnection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        public DaoBase(string connection)
        {
            _constr = connection;

            //DBConnection = ConnectionPool.Connect(_constr);


            DBConnection = new SqlConnection(connection);
            DBConnection.Open();


        }

        public bool ExecuteRaw(string sql, Dictionary<string, object> p = null)
        {
            if (p == null) p = new Dictionary<string, object>();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(sql, DBConnection))
                {
                    sqlCommand.Parameters.Clear();

                    p.ToList().ForEach(x =>
                    {
                        sqlCommand.Parameters.AddWithValue(x.Key, x.Value);
                    });

                    sqlCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "DB Error: {0}", Regex.Replace(sql, @"\t|\n|\r", ""));

                return false;
            }
            finally
            {
                logger.Trace("RAW EXECUTE : {0}", Regex.Replace(sql, @"\t|\n|\r", ""));
            }

            return true;

        }

        public T SelectById<T>(int Id) where T : TableBase
        {
            var table = (T)Activator.CreateInstance(typeof(T));

            string sql = string.Format("SELECT * FROM {0} WHERE {1} = {2} ",
                                        table.TableName,
                                        table.DBKey,
                                        Id.ToString());

            return this.ExecuteSQL<T>(sql).FirstOrDefault();
        }

        public IEnumerable<T> ExecuteSQL<T>(string sql, Dictionary<string, object> p = null)
        {
            if (p == null) p = new Dictionary<string, object>();

            List<T> list = new List<T>();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(sql, DBConnection))
                {
                    sqlCommand.Parameters.Clear();

                    p.ToList().ForEach(x =>
                    {
                        sqlCommand.Parameters.AddWithValue(x.Key, x.Value);
                    });

                    sqlCommand.ExecuteNonQuery();

                    using (SqlDataReader dr = sqlCommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add((T)Activator.CreateInstance(typeof(T), dr));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "DB Error : {0}", Regex.Replace(sql, @"\t|\n|\r", ""));
            }
            finally
            {
                logger.Trace("ExecuteSQL : {0} - {1}", Regex.Replace(sql, @"\t|\n|\r", ""), Newtonsoft.Json.JsonConvert.SerializeObject(p));
            }

            return list;
        }

        public IEnumerable<T> ExecuteProcedure<T>(string rpcName, Dictionary<string, object> p = null)
        {
            if (p == null) p = new Dictionary<string, object>();

            List<T> list = new List<T>();

            try
            {
                using (SqlCommand cmd = new SqlCommand(rpcName, DBConnection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Clear();

                    p.ToList().ForEach(x =>
                    {
                        cmd.Parameters.AddWithValue(x.Key, x.Value);
                    });

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add((T)Activator.CreateInstance(typeof(T), dr));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "DB Error : {0}", rpcName);
            }
            finally
            {
                logger.Trace("EXECUTE PROCEDURE : {0} - {1}", rpcName, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            }

            return list;

        }

        public object ExecuteProcedureScalr(string rpcName, Dictionary<string, object> p = null)
        {
            if (p == null) p = new Dictionary<string, object>();

            try
            {
                using (SqlCommand cmd = new SqlCommand(rpcName, DBConnection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Clear();

                    p.ToList().ForEach(x =>
                    {
                        cmd.Parameters.AddWithValue(x.Key, x.Value);
                    });

                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "DB Error : {0}", rpcName);
            }
            finally
            {
                logger.Trace("EXECUTE ROCEDURE : {0} - {1}", rpcName, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            }
            return null;
        }

        /// <summary>
        /// Return List of Passed type using passed table paramter. Value should be other than default. 
        /// It is prototype. Do NOT use if you get any error.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteSQLDynamic<T>(T table) where T : TableBase
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            string where = " ( 1 = 1 ) ";   // default is all

            //String sql = "SELECT * FROM [tblContact] WHERE [StudentId] = @id ORDER BY ContactId DESC";
            //string sql = "SELECT * FROM [{0}] WHERE {1} ORDER BY {2} DESC";
            string sql = "SELECT * FROM {0} WHERE {1} ORDER BY {2} DESC";

            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                table.GetColumns().ToList().ForEach(x =>
                {
                    if (x.Replace(" ", "") == prop.Name)
                    {
                        var value = prop.GetValue(table, null);

                        // set where if type is int or string and filled other than default.
                        if (
                            (prop.PropertyType == typeof(int) && (int)value > 0)
                            || (prop.PropertyType == typeof(string) && !string.IsNullOrWhiteSpace((string)value))
                        )
                        {
                            where += " AND [" + x + "] = @" + prop.Name + " ";
                            dic.Add("@" + prop.Name, value);
                        }
                    }
                });
            }

            //sql = sql.Replace("#1", where);
            sql = string.Format(sql, table.TableName, where, table.DBKey);

            IEnumerable<T> tbls = this.ExecuteSQL<T>(sql, dic);

            return tbls;
        }

        public bool ExecuteUpdateSQL<T>(T table) where T : TableBase
        {
            string sChanges = table.TableName + " detail has changed. ";

            var tableId = table.GetType().GetProperties().Where(x => x.Name == table.DBKey).FirstOrDefault().GetValue(table).ToString();

            T oldTable = SelectById<T>(int.Parse(tableId));

            Dictionary<string, object> p = new Dictionary<string, object>();

            var keyvalue = ((T)table).GetType().GetProperty(table.DBKey).GetValue(((T)table), null);

            string sql = "UPDATE #0 SET #1 WHERE #2".Replace("#2", string.Format("{0} = {1};", table.DBKey, keyvalue));

            string v1 = "";
            int i = 0;

            table.GetColumns().Where(x => x != table.DBKey).ToList()
                .ForEach(x =>
                {
                    ++i;
                    x = x.Replace(" ", "");

                    string pn = "@P" + (i).ToString();

                    //var pv = ((T)table).GetType().GetProperty(x).GetValue(((T)table), null);
                    var all = ((T)table).GetType().GetProperties().Where(pt => pt.Name == x);
                    var pv = (all.FirstOrDefault(xx => xx.DeclaringType == typeof(T)) ?? all.First()).GetValue(((T)table), null);

                    if (pv == null) return;

                    if (!p.ContainsKey(pn)) p.Add(pn, pv);

                    v1 += (i == 1) ? "[" + x + "]" : ",[" + x + "]";
                    v1 += " = " + pn;

                    oldTable.GetColumns().Where(old => old != table.DBKey).ToList()
                   .ForEach(oldField =>
                   {
                       oldField = oldField.Replace(" ", "");

                       if (x == oldField)
                       {
                           var oldValueAux = oldTable.GetType().GetProperties().Where(w => w.Name == oldField).FirstOrDefault().GetValue(oldTable);

                           string oldValue = oldValueAux == null ? "" : oldValueAux.ToString();

                           if (pv.ToString() != oldValue)
                           {
                               sChanges += string.Format("[Field({0}) Old Value: {1} | New Value: {2}] ", x, oldValue, pv.ToString());
                           }
                       }
                   });
                });

            // Marge columns name and its values to main SQL
            sql = sql.Replace("#0", table.TableName).Replace("#1", v1);

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(sql, DBConnection))
                {
                    sqlCommand.Parameters.Clear();

                    p.ToList().ForEach(x =>
                    {
                        sqlCommand.Parameters.AddWithValue(x.Key, x.Value);
                    });

                    sqlCommand.ExecuteScalar();

                    
                }
            }
            catch (Exception ex)
            {
                backuplogger.Debug("FAILED UPDATE EXECUTE : {0} - {1} - {2}", Regex.Replace(sql, @"\t|\n|\r", ""), Newtonsoft.Json.JsonConvert.SerializeObject(p), Newtonsoft.Json.JsonConvert.SerializeObject(table));
                logger.Error(ex, "DB Error: {0}", Regex.Replace(sql, @"\t|\n|\r", ""));
                return false;
            }

            return true;

        }

        public int ExecuteInsertSQL<T>(T table) where T : TableBase
        {
            Dictionary<string, object> p = new Dictionary<string, object>();

            string sql = "INSERT INTO #0 (#1) VALUES (#2);SELECT SCOPE_IDENTITY();";
            string sqlLog = sql;
            string v1 = "";
            string v2 = "";
            string v2Log = "";
            int i = 0;
            int lastID = 0;

            table.GetColumns().Where(x => x != table.DBKey).ToList()
                .ForEach(x =>
                {
                    ++i;
                    x = x.Replace(" ", "");

                    string pn = "@P" + (i).ToString();

                    //var pv = ((T)table).GetType().GetProperty(x).GetValue(((T)table), null);
                    var all = ((T)table).GetType().GetProperties().Where(pt => pt.Name == x);
                    var pv = (all.FirstOrDefault(xx => xx.DeclaringType == typeof(T)) ?? all.First()).GetValue(((T)table), null);

                    if (pv == null) return;

                    if (pv.GetType() == typeof(DateTime) || pv.GetType() == typeof(DateTime?))
                    {
                        string date = ((DateTime)pv).ToString("yyyy-MM-dd HH':'mm':'ss");

                        if (!p.ContainsKey(pn)) p.Add(pn, date);
                        v2Log += (i == 1) ? date : "," + date;
                    }
                    else
                    {
                        if (!p.ContainsKey(pn)) p.Add(pn, pv);
                        v2Log += (i == 1) ? pv : "," + pv;
                    }

                    v1 += (i == 1) ? "[" + x + "]" : ",[" + x + "]";
                    v2 += (i == 1) ? pn : "," + pn;
                });

            // Marge columns name and its values to main SQL
            sql = sql.Replace("#0", table.TableName).Replace("#1", v1).Replace("#2", v2);
            sqlLog = sqlLog.Replace("#0", table.TableName).Replace("#1", v1).Replace("#2", v2Log);

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(sql, DBConnection))
                {
                    sqlCommand.Parameters.Clear();

                    p.ToList().ForEach(x =>
                    {
                        sqlCommand.Parameters.AddWithValue(x.Key, x.Value);
                    });

                    lastID = Convert.ToInt32(sqlCommand.ExecuteScalar());


                }
            }
            catch (Exception ex)
            {
                backuplogger.Debug("FAILED INSERT EXECUTE : {0} - {1} - {2}", Regex.Replace(sql, @"\t|\n|\r", ""), Newtonsoft.Json.JsonConvert.SerializeObject(p), Newtonsoft.Json.JsonConvert.SerializeObject(table));
                logger.Error(ex, "DB Error: {0}", Regex.Replace(sql, @"\t|\n|\r", ""));
            }

            return lastID;

        }

        public bool ExecuteDeleteSQL<T>(int id) where T : TableBase
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            var table = (T)Activator.CreateInstance(typeof(T));

            p.Add("@id", id);

            string sql = "DELETE FROM #0 WHERE #1".Replace("#0", table.TableName).Replace("#1", string.Format("{0} = @id;", table.DBKey));
            string sqlLog = "DELETE FROM #0 WHERE #1".Replace("#0", table.TableName).Replace("#1", string.Format("{0} = {1};", table.DBKey, id));

            try
            {
                // Backup records before remove.
                if (!BackupRecods<T>(p)) return true;

                using (SqlCommand sqlCommand = new SqlCommand(sql, DBConnection))
                {
                    sqlCommand.Parameters.Clear();

                    p.ToList().ForEach(x =>
                    {
                        sqlCommand.Parameters.AddWithValue(x.Key, x.Value);
                    });

                    sqlCommand.ExecuteScalar();


                }
            }
            catch (Exception ex)
            {
                backuplogger.Debug("FAILED DELETE EXECUTE : {0} - {1}", Regex.Replace(sql, @"\t|\n|\r", ""), Newtonsoft.Json.JsonConvert.SerializeObject(p));
                logger.Error(ex, "DB Error: {0}", Regex.Replace(sql, @"\t|\n|\r", ""));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Create Table by Table. 
        /// Default value is fixed value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool ExecuteCreateSQL<T>(T table) where T : TableBase
        {
            string sql = "CREATE TABLE {0} ({1}) ";
            string rows = "";

            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                table.GetColumns().ToList().ForEach(x =>
                {

                    if (x.Replace(" ", "") == prop.Name)
                    {
                        string row = "", rowtmp = "";
                        var value = prop.GetValue(table, null);

                        //string nullable = ValueTypeHelper.IsNullable(value) ? "" : "NOT NULL";

                        if (table.DBKey == prop.Name)
                            rowtmp = "[" + prop.Name + "] {0} identity(1,1)";
                        else
                            rowtmp = "[" + prop.Name + "] {0} default {1}";


                        if (prop.PropertyType == typeof(int))
                            row = string.Format(rowtmp, "int", 0);

                        else if (prop.PropertyType == typeof(string))
                            row = string.Format(rowtmp, "nvarchar(" + ((string)value).Length + ")", "''");

                        else if (prop.PropertyType == typeof(bool))
                            row = string.Format(rowtmp, "bit", ((bool)value) ? "1" : "0");

                        else if (prop.PropertyType == typeof(DateTime))
                            row = string.Format(rowtmp, "datetime", "getdate()");

                        if (rows != "") rows += ", ";

                        rows += row;
                    }
                });
            }

            sql = string.Format(sql, table.TableName, rows);

            bool success = this.ExecuteRaw(sql);

            return success;
        }

        /// <summary>
        /// table check
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exists(string table)
        {
            bool exists;
            string sql = "";
            try
            {
                // ANSI SQL way.  Works in PostgreSQL, MSSQL, MySQL.  
                sql = "select case when exists((select * from information_schema.tables where table_name = '" + table + "')) then 1 else 0 end";
                using (SqlCommand sqlCommand = new SqlCommand(sql, DBConnection))
                {
                    exists = (int)sqlCommand.ExecuteScalar() == 1;
                }

            }
            catch
            {
                try
                {
                    // Other RDBMS.  Graceful degradation
                    exists = true;
                    sql = "select 1 from " + table + " where 1 = 0";
                    using (SqlCommand sqlCommand = new SqlCommand(sql, DBConnection))
                    {
                        sqlCommand.ExecuteNonQuery();
                    }

                }
                catch
                {
                    exists = false;
                }
            }

            return exists;
        }

        /// <summary>
        /// Backup records before remove it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns>true if the target is at least one</returns>
        public bool BackupRecods<T>(Dictionary<string, object> dic) where T : TableBase
        {
            //Logger backuplogger = LogManager.GetLogger("SQLBackup");

            var table = (T)Activator.CreateInstance(typeof(T));
            string slc = "SELECT * FROM #0 WHERE #1".Replace("#0", table.TableName).Replace("#1", string.Format("{0} = @id;", table.DBKey));
            var records = this.ExecuteSQL<T>(slc, dic);

            if (records.Count() > 0) backuplogger.Debug("REMOVED : {0} - {1} - {2}", slc, Newtonsoft.Json.JsonConvert.SerializeObject(dic), Newtonsoft.Json.JsonConvert.SerializeObject(records));

            return (records.Count() > 0);
        }

        static class ValueTypeHelper
        {
            public static bool IsNullable<T>(T t) { return false; }
            public static bool IsNullable<T>(T? t) where T : struct { return true; }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    DBConnection.Close();
                    //ConnectionPool.Disconnect(_constr);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DaoEmailTemplate() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }

    public static class ConnectionPool
    {
        static Dictionary<string, SqlConnection> _cons = new Dictionary<string, SqlConnection>();
        static Dictionary<string, int> _accounts = new Dictionary<string, int>();
        static private Object thisLock = new Object();

        public static SqlConnection Connect(string connection)
        {
            lock (thisLock)
            {

                int active = 0;

                _accounts.TryGetValue(connection, out active);

                if (active == 0)
                {
                    _cons[connection] = new SqlConnection(connection);
                    _cons[connection].Open();
                    Note(connection);
                }
            }

            return _cons[connection];
        }

        public static void Disconnect(string connection)
        {
            lock (thisLock)
            {
                if (NoOtherUsed(connection))
                {
                    DeNote(connection);
                    GetCon(connection).Close();
                }
            }
        }

        static void Note(string key)
        {
            int actives = 0;

            _accounts.TryGetValue(key, out actives);

            _accounts[key] = ++actives;
        }

        static void DeNote(string key)
        {
            _accounts[key]--;
        }

        static bool NoOtherUsed(string connection)
        {
            return _accounts[connection] == 1;
        }

        static SqlConnection GetCon(string connection)
        {
            return _cons[connection];
        }


    }

}