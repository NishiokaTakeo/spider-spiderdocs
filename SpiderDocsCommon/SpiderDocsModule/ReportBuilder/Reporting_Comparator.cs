
using System;
using System.Collections.Generic;

namespace ReportBuilder.Models.Report
{
    public class Reporting_Comparator
    {
        public int Id { get; set; }
        public string SQL_Value { get; set; }
        public string Display_Value { get; set; }
        public DateTime Created_Date { get; set; }
        public bool Active { get; set; }
        public List<int> Field_Types { get; set; }
		public int field_Id {get;set;}
    }
}