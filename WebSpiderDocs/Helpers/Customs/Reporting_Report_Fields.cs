using System;

namespace ReportBuilder.Models.Report
{
    public class Reporting_Report_Fields
    {
        public int Id { get; set; }
        public int Report_Id { get; set; }
        public int Field_Id { get; set; }
        public int Sort { get; set; }
        public string Display_Name { get; set; }
        public Reporting_Fields Field { get; set; }
    }
}