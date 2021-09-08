using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Configuration;
using Spider.IO;

namespace WebSpiderDocs
{
	public enum GetRootUriMode
	{
		FullUri,
		CurrentPage,
		CurrentDir,
		Root
	}

	public class WebUtilities : IDisposable
	{
        string _tempFolder = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tempFolder">System path or relative path for temp folder</param>
        public WebUtilities(string tempFolder = "temp")
        {
            _tempFolder = tempFolder;
        }

        public static string GetCurrentUri(GetRootUriMode mode, string additonal_path = "")
		{
			string ans = HttpContext.Current.Request.Url.AbsoluteUri;
			
			switch(mode)
			{
			case GetRootUriMode.Root:
                //ans = ans.Replace(HttpContext.Current.Request.Url.AbsolutePath, "") + HttpContext.Current.Request.ApplicationPath;
                ans = ans.Replace(HttpContext.Current.Request.Url.PathAndQuery, "") + HttpContext.Current.Request.ApplicationPath;
                break;

			case GetRootUriMode.CurrentPage:
				ans = ans.Replace(HttpContext.Current.Request.Url.Query, "");
				break;

			case GetRootUriMode.CurrentDir:
				ans = ans.Replace(HttpContext.Current.Request.Url.Query, "")
						 .Replace(HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Count() - 1], "");
				break;
			}

			if(!String.IsNullOrEmpty(additonal_path))
			{
				additonal_path = additonal_path.Replace(HttpContext.Current.Server.MapPath("~/"), "/");
				additonal_path = additonal_path.Replace("\\", "/");
				ans += additonal_path;
			}

			return ans;
		}
        public string GetTempFolder()
        {
            string filepath = "";
            if (System.IO.DriveInfo.GetDrives().ToList().Exists(drive => _tempFolder.Contains(drive.Name)))
                filepath = _tempFolder;
            else
                filepath = HttpContext.Current.Server.MapPath("~/") + _tempFolder;

            return filepath;

        }
        public string CreateTempFolder(string postfix = "")
		{
			string name = Path.GetRandomFileName();
			
			if(!String.IsNullOrEmpty(postfix))
				name += ("_" + postfix);

            string filepath = "";
            if ( System.IO.DriveInfo.GetDrives().ToList().Exists( drive => _tempFolder.Contains(drive.Name)) )
                filepath = _tempFolder + "\\" + name + "\\";
            else             
                filepath = HttpContext.Current.Server.MapPath("~/") + _tempFolder + "\\" + name + "\\";

			Directory.CreateDirectory(filepath);

			return filepath;
		}

		public void DeleteOldTempFiles(DateTime before = new DateTime())
		{
			if(before == new DateTime())
				before = DateTime.Now.AddHours(-12);

            string filepath = "";
            if (System.IO.DriveInfo.GetDrives().ToList().Exists(drive => _tempFolder.Contains(drive.Name)))
                filepath = _tempFolder + "\\";
            else
                filepath = HttpContext.Current.Server.MapPath("~/") + _tempFolder + "\\";

			FileFolder.DeleteFilesInPeriod(filepath, To: before);
		}

		public static T GetAppSettingsVal<T>(string key)
		{
			T ans = default(T);

			if(ConfigurationManager.AppSettings[key] != null)
			{
				try
				{
					ans = (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));

				}catch {}
			}

			return ans;
		}

		public static string ToWebURL(string drivePath, string domainAppPath, string applicationPath, Uri uri)
		{
			string webHome = ToWebPath(drivePath.Replace(domainAppPath, ""))
				   , authroty = uri.Authority;
			
			return string.Format(uri.Scheme + "://{0}/{1}/{2}", authroty, applicationPath.Replace("/",""),webHome);
		}

		public static string ToWebPath(string path)
		{
			return (new System.Text.RegularExpressions.Regex(@"(/|\\)+")).Replace(path, "/");
		}

		~WebUtilities()
        {
            Console.WriteLine("Finalizer is called");
        }
        public void Dispose()
        {
            this.DeleteOldTempFiles(before: DateTime.Now.AddHours(-WebSettings.ExportDocKeepHours));
        }
    }
}
