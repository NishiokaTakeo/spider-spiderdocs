using System;
using System.Web;

namespace WebSpiderDocs
{
	public class WebSettings
	{
		static int _userId = 0;
		public static int CurrentUserId
		{
			get
			{
				int ans = 0;

				if (_userId > 0)
					ans = _userId;
				else if (UserAuthentication)
				{
					if (!String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
						ans = int.Parse(HttpContext.Current.User.Identity.Name);

				}
				else if ((Convert.ToString(HttpContext.Current.Request.RequestContext.RouteData.Values["controller"]).ToLower() == "External".ToLower())
					 && (HttpContext.Current.Request.RequestContext.RouteData.Values.ContainsKey("id")))
				{
					ans = Convert.ToInt32(HttpContext.Current.Request.RequestContext.RouteData.Values["id"]);
				}

				return ans;
			}

			set { _userId = value; }
		}

		public static bool UserAuthentication { get { return 0 < WebUtilities.GetAppSettingsVal<int>("UserAuthentication"); } }

		public static int ExportDocKeepHours
		{
			get
			{
				int hours = WebUtilities.GetAppSettingsVal<int>("ExportDocKeepHours");

				if(hours <= 0)
					hours = 12;
					
				return hours;
			}
		}

		public static string server { get { return WebUtilities.GetAppSettingsVal<string>("server"); } }
		public static int port { get { return WebUtilities.GetAppSettingsVal<int>("port"); } }

		public static string test_user { get { return WebUtilities.GetAppSettingsVal<string>("test_user"); } }
		public static string test_pass { get { return WebUtilities.GetAppSettingsVal<string>("test_pass"); } }
	}
}