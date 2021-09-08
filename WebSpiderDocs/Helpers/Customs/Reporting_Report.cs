using System;

namespace ReportBuilder.Models.Report
{
    public class Reporting_Report
    {
        public int Id { get; set; }
        public string Report_Name { get; set; }
        public int User_Id { get; set; }
        public DateTime Created_Date { get; set; }
        public bool Active { get; set; }
    }
}