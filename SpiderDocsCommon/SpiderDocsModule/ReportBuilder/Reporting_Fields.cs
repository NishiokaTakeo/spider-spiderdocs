
using System;

namespace ReportBuilder.Models.Report
{
    public class Reporting_Fields
    {
        public int Id { get; set; }
        public int Category_Id { get; set; }
        public string SQL_Field_Name { get; set; }
        public string Display_Name { get; set; }
        public int Field_Type_Id { get; set; }
        public DateTime Created_Date { get; set; }
        public bool Active { get; set; }
    }
}