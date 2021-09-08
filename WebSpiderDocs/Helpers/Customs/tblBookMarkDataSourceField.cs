using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ReportBuilder.Models
{
    public class tblBookMarkDataSourceField : TableBase
    {
        public override string TableName
        {
            get
            {
                return "tblBookMarkDataSourceField";
            }
        }

        public override string DBKey
        {
            get
            {
                return "DataSourceID";
            }
        }

        private string[] columns = new string[]{
            "DataSourceID"
            ,"Name"
            ,"DisplayName"
        };
               

        public int DataSourceID { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        

        public override string[] GetColumns()
        {
            return columns;
        }

        public tblBookMarkDataSourceField()
        {

        }

        public tblBookMarkDataSourceField(SqlDataReader dataReader):base(dataReader)
        {
            this.DataSourceID = base.GetFieldValue<int>("DataSourceID");
            this.Name = base.GetFieldValue<string>("Name");
            this.DisplayName = base.GetFieldValue<string>("DisplayName");
        }
    }
}