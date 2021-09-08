using System;
using System.Configuration;

namespace WebSpiderDocs
{
	public class APISettings
	{
		public static bool AllowDeleteDocuments { get { return 0 < WebUtilities.GetAppSettingsVal<int>("AllowDeleteDocuments"); } }
	}
}
