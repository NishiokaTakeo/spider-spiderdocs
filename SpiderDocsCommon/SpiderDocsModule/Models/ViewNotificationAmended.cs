using System;

namespace SpiderDocsModule
{
	public class ViewNotificationAmended
    {
        //string _amendedDate = DateTime.MinValue.ToString("yyyy-MM-dd hh:mm:ss");
		public int id_user { get; set; } = 0;
		public string name { get; set; } = string.Empty;
		public string email { get; set; } = string.Empty;
		public int id_doc { get; set; } = 0;
		public string title { get; set; } = string.Empty;
		public int version { get; set; } = 0;
		public int id_amendedBy { get; set; } = 0;
		public string amendedBy { get; set; } = string.Empty;
        public DateTime amendedDate { get; set; } = DateTime.MinValue;// { get { return Convert.ToDateTime(_amendedDate).ToString("MM/dd/yyyy hh:mm"); } set { _amendedDate = value; } }
        public string reason { get; set; } = string.Empty;
		public int id_notification_group { get; set; } = 0;
		public string group_name { get; set; } = string.Empty;

		public ViewNotificationAmended()
		{

		}
		
	}
}
