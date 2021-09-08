using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

//---------------------------------------------------------------------------------
namespace Spider.Data
{
    public class ConstData
    {
        public const string DB_DATE_TIME = "yyyy-MM-dd HH:mm";
        public const string DB_DATE = "yyyy-MM-dd 00:00";
        public const string DATE_TIME = "dd/MM/yyyy HH:mm";
        public const string DATE_TIME_12 = "dd/MM/yyyy hh:mm tt";
        public const string DATE = "dd/MM/yyyy";
    }

    public enum SqlOperationMode
    {
        None = 0,
        Select,
        Select_Scalar,
        Select_Table,
        Insert,
        Update,
        Delete,
        StoredProcedure,
        Function,

        Max
    };

    public enum SqlOperationAndOr
    {
        AND = 0,
        OR
    };

    public enum SqlOperationOperator
    {
        EQUAL = 0,
        NOT_EQUAL,
        GREATER_THAN,
        LESS_THAN,
        GREATER_THAN_OR_EQUAL_TO,
        LESS_THAN_OR_EQUAL_TO,
        IN,
        NOT_IN,
        BETWEEN,
        LIKE_BEGINNING,
        LIKE_END,
        LIKE_BOTH,
        CONTAINS,

        RAW
    };

    //---------------------------------------------------------------------------------
    public class SqlOperationCriteria
    {
        /// <param name="m_where">target field name</param>
        public string m_where;

        /// <param name="m_operator">operator for where criteria for the target field</param>
        public SqlOperationOperator m_operator = SqlOperationOperator.EQUAL;

        /// <param name="m_operator">evaluate value to compare with the target field</param>
        public Array m_where_val;
        public string EscapeLike = "";

        /// <param name="where">field name</param>
        /// <param name="vals">value which is compared to the field name</param>
        public SqlOperationCriteria(string where, Array vals)
        {
            AddParams(where, SqlOperationOperator.EQUAL, vals);
        }

        /// <param name="where">field name</param>
        /// <param name="vals">value which is compared to specified field value</param>
        public SqlOperationCriteria(string where, params object[] vals)
        {
            AddParams(where, SqlOperationOperator.EQUAL, (Array)vals);
        }

        /// <param name="where">field name</param>
        /// <param name="Operator">operator to specify how to compare to specified field value</param>
        /// <param name="vals">value which is compared to the field name</param>
        public SqlOperationCriteria(string where, SqlOperationOperator Operator, Array vals)
        {
            AddParams(where, Operator, vals);
        }

        /// <param name="where">field name</param>
        /// <param name="Operator">operator to specify how to compare to specified field value</param>
        /// <param name="vals">value which is compared to the field name</param>
        public SqlOperationCriteria(string where, SqlOperationOperator Operator, params object[] vals)
        {
            AddParams(where, Operator, (Array)vals);
        }

        public string Escape4Like(string sql)
        {
            if (!string.IsNullOrWhiteSpace(EscapeLike))
            {
                sql = sql
                        .Replace("[", EscapeLike + "[")
                        .Replace("]", EscapeLike + "]")
                        .Replace("%", EscapeLike + "%")
                        .Replace("_", EscapeLike + "_")
                        .Replace("^", EscapeLike + "^");
            }

            return sql;
        }

        void AddParams(string where, SqlOperationOperator Operator, Array vals)
        {
            m_where = Spider.Data.Utilities.SqlConvert(where);
            m_operator = Operator;

            if (vals != null)
                m_where_val = vals;
            else
                m_where_val = null;
        }
    }

    //---------------------------------------------------------------------------------
    public class SqlOperationCriteriaCollection
    {
        public List<SqlOperationCriteria> m_criterias = new List<SqlOperationCriteria>();
        public SqlOperationCriteriaCollection m_child;
        public SqlOperationAndOr m_AndOr = SqlOperationAndOr.AND;

        public void Add(SqlOperationCriteria src)
        {
            m_criterias.Add(src);
        }
    }

    public class SqlOperationCriteriaCollections
    {
        public List<SqlOperationCriteriaCollection> m_collections = new List<SqlOperationCriteriaCollection>();
        public SqlOperationAndOr m_AndOr = SqlOperationAndOr.AND;

        public SqlOperationCriteriaCollections()
        {
        }

        public SqlOperationCriteriaCollections(SqlOperationCriteriaCollection src)
        {
            m_collections.Add(src);
        }

        public void Add(SqlOperationCriteriaCollection src)
        {
            m_collections.Add(src);
        }
    }

    public class SqlOperationJoinParameters
    {
        public class Relationship
        {
            public string BaseTable;
            public string BaseField;
            public string TargetField;

            public Relationship(string base_table = "", string base_field = "", string target_field = "")
            {
                this.BaseTable = Spider.Data.Utilities.SqlConvert(base_table);
                this.BaseField = base_field;
                this.TargetField = target_field;
            }
        }

        string _TargetTable;
        public string TargetTable { get { return _TargetTable; } }
        public List<Relationship> FieldRelationship = new List<Relationship>();

        public SqlOperationJoinParameters(string TargetTable)
        {
            this._TargetTable = Spider.Data.Utilities.SqlConvert(TargetTable);
        }

        public void AddRelationship(string BaseTable, string BaseField, string TargetField)
        {
            FieldRelationship.Add(new Relationship(BaseTable, BaseField, TargetField));
        }

        public SqlOperationJoinParameters(string BaseTable, string TargetTable, string Field)
        {
            this._TargetTable = Spider.Data.Utilities.SqlConvert(TargetTable);
            FieldRelationship.Add(new Relationship(BaseTable, Field, Field));
        }

        public SqlOperationJoinParameters(string BaseTable, string TargetTable, string BaseField, string TargetField)
        {
            this._TargetTable = Spider.Data.Utilities.SqlConvert(TargetTable);
            FieldRelationship.Add(new Relationship(BaseTable, BaseField, TargetField));
        }




    }
}
