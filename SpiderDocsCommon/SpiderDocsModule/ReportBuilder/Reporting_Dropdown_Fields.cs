
using System;
using System.Collections.Generic;

namespace ReportBuilder.Models.Report
{
    public class Reporting_Dropdown_Fields
    {
        public int Field_Id { get; set; }
        public string Source_Table { get; set; }
        public string Value_Field { get; set; }
        public string Text_Field { get; set; }
        public DateTime Created_Date { get; set; }
        public bool Active { get; set; }

        public List<Tuple<string, string>> List { get; set; }
    }
}