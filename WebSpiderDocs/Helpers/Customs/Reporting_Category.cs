using System;

namespace ReportBuilder.Models.Report
{
    public class Reporting_Category
    {
        public int Id { get; set; }
        public string Table_Name { get; set; }
        public string Display_Name { get; set; }
        public DateTime Created_Date { get; set; }
        public bool Active { get; set; }
    }
}