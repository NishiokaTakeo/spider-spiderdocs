using System;
using System.Data.SqlClient;
using NLog;

namespace ReportBuilder.Models
{

        public abstract class TableBase
        {
            protected static Logger logger = LogManager.GetCurrentClassLogger();

            SqlDataReader _dataReader;
            public TableBase()
            {
            }

            public TableBase(SqlDataReader v)
            {
                _dataReader = v;
            }


            public abstract string TableName { get; }
            public abstract string DBKey { get; }
            public abstract string[] GetColumns();

            protected T GetFieldValue<T>(string fieldName)
            {

                if (IsColumnExists(fieldName))
                {
                    return GetFieldValue<T>(this._dataReader.GetOrdinal(fieldName));
                }

                return GetDefault<T>();
            }

            protected T GetFieldValue<T>(int ordinalID)
            {
                string name = _dataReader.GetName(ordinalID);

                if (typeof(int) == typeof(T))
                {

                    return _dataReader[name] == DBNull.Value ?
                            GetDefault<T>()
                            : (T)Convert.ChangeType(_dataReader[ordinalID].ToString(), typeof(T));

                    /*
                    return _dataReader[name] == DBNull.Value ?
                        (T)Convert.ChangeType(0, typeof(T))
                        : (T)Convert.ChangeType(_dataReader.GetFieldValue<int>(ordinalID), typeof(T));
                    */
                }
                else if (typeof(double) == typeof(T))
                {

                    return _dataReader[name] == DBNull.Value ?
                                GetDefault<T>()
                            : (T)Convert.ChangeType(_dataReader[ordinalID].ToString(), typeof(T));

                    /*
                    return _dataReader[name] == DBNull.Value ?
                        (T)Convert.ChangeType(0, typeof(T))
                        : (T)Convert.ChangeType(_dataReader.GetFieldValue<int>(ordinalID), typeof(T));
                    */
                }
                else if (typeof(string) == typeof(T))
                {
                    return _dataReader[name] == DBNull.Value ?
                        GetDefault<T>()
                        : (T)Convert.ChangeType(_dataReader[ordinalID].ToString(), typeof(T));

                    /* 
                    return _dataReader[name] == DBNull.Value ?
                        (T)Convert.ChangeType("", typeof(T))
                        : (T)Convert.ChangeType(_dataReader.GetFieldValue<string>(ordinalID), typeof(T));
                    */
                }
                else if (typeof(DateTime) == typeof(T))
                {

                    return _dataReader[name] == DBNull.Value ?
                        GetDefault<T>()
                        : (T)Convert.ChangeType(_dataReader.GetFieldValue<DateTime>(ordinalID), typeof(T));
                }
                else if (typeof(DateTime?) == typeof(T))
                {
                    var t = Nullable.GetUnderlyingType(typeof(T));

                    return _dataReader[name] == DBNull.Value ?
                        GetDefault<T>()
                        : (T)Convert.ChangeType(_dataReader.GetFieldValue<DateTime?>(ordinalID), t);
                }
                else if (typeof(bool?) == typeof(T))
                {
                    var t = Nullable.GetUnderlyingType(typeof(T));

                    return _dataReader[name] == DBNull.Value ?
                        GetDefault<T>()
                        : (T)Convert.ChangeType(_dataReader.GetFieldValue<bool?>(ordinalID), t);
                }
                else if (typeof(bool) == typeof(T))
                {
                    return _dataReader[name] == DBNull.Value ?
                        GetDefault<T>()
                        : (T)Convert.ChangeType(_dataReader.GetFieldValue<bool>(ordinalID), typeof(T));
                }
                else if (typeof(decimal?) == typeof(T))
                {
                    var t = Nullable.GetUnderlyingType(typeof(T));

                    return _dataReader[name] == DBNull.Value ?
                        GetDefault<T>()
                        : (T)Convert.ChangeType(_dataReader.GetFieldValue<decimal?>(ordinalID),t);
                }
                else if (typeof(decimal) == typeof(T))
                {
                    return _dataReader[name] == DBNull.Value ?
                        GetDefault<T>()
                        : (T)Convert.ChangeType(_dataReader.GetFieldValue<decimal>(ordinalID), typeof(T));
                }
                else if (typeof(byte?) == typeof(T))
                {
                    var t = Nullable.GetUnderlyingType(typeof(T));

                    return _dataReader[name] == DBNull.Value ?
                        GetDefault<T>()
                        : (T)Convert.ChangeType(_dataReader.GetFieldValue<byte?>(ordinalID), t);
                }
                else if (typeof(byte) == typeof(T))
                {
                    return _dataReader[name] == DBNull.Value ?
                        GetDefault<T>()
                        : (T)Convert.ChangeType(_dataReader.GetFieldValue<byte>(ordinalID), typeof(T));
                }
                else
                {
                    throw new Exception("Need to implement");
                    //return default(T);
                }
            }

            protected T GetDefault<T>()
            {
                if (typeof(int?) == typeof(T))
                {
                    return default(T);
                }
                else if (typeof(int) == typeof(T))
                {
                    return (T)Convert.ChangeType(0, typeof(T));
                }
                else if (typeof(double) == typeof(T))
                {

                    return (T)Convert.ChangeType(0, typeof(T));
                }
                else if (typeof(string) == typeof(T))
                {
                    return (T)Convert.ChangeType("", typeof(T));
                }
                else if (typeof(DateTime) == typeof(T))
                {

                    //return (T)Convert.ChangeType(DateTime.MinValue, typeof(T));
                    return (T)Convert.ChangeType("1975-01-01", typeof(T));
                }
                else if (typeof(DateTime?) == typeof(T))
                {
                    return default(T);
                }
                else if (typeof(bool?) == typeof(T))
                {
                    return default(T);
                }
                else if (typeof(bool) == typeof(T))
                {
                    return (T)Convert.ChangeType(false, typeof(T));
                }
                else if (typeof(decimal?) == typeof(T))
                {
                    return default(T);
                }
                else if (typeof(decimal) == typeof(T))
                {
                    return (T)Convert.ChangeType(0, typeof(T));
                }
                else if (typeof(byte?) == typeof(T))
                {
                    return default(T);
                }
                else if (typeof(byte) == typeof(T))
                {
                    return (T)Convert.ChangeType(0, typeof(T));
                }
                else
                {
                    throw new Exception("Need to implement");
                    //return default(T);
                }
            }

            /// <summary>
            /// return JSON object
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(this);
            }

            public bool IsColumnExists(string columnName)
            {
                var reader = this._dataReader;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return true;
                    }
                }

                return false;
            }
    }
}