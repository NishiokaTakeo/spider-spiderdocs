using System;

namespace SpiderDocsModule
{
	public class History
	{
		public int id_doc;
		public string title;
		public int version;
		public string name;
		public int rollback_to;
		public string type;
		public int id_version;
        public int id_event;
		public en_Events Event;
        public string event_name;
		public int id_user;
		public DateTime date;
		public string comments;
		public string reason;
		public string id_notification_group;
		public string notification_group_name;
	}
}
