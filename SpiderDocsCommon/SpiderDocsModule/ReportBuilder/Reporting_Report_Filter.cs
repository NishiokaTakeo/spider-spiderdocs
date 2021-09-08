
using System;

namespace ReportBuilder.Models.Report
{
	public class Reporting_Report_Filter
	{
		public static readonly string[] COND = new[] { "AND", "OR" };

        public int Id { get; set; }
        public int Report_Id { get; set; }
        public int Field_Id { get; set; }
        public Reporting_Fields Field { get; set; }
        public int Filter_Group { get; set; }
        public int Filter_Order { get; set; }
        public int Comparator_Id { get; set; }
        public string Value_1 { get; set; }
        public string Value_2 { get; set; }
        public string Conditional { get; set; }
    }
}