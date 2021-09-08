using NLog;
using System;
using SpiderDocsModule;
using System.Text.RegularExpressions;
using System.Web;
using System.Linq;

public static partial class ConfigurationFactory
{


    #region Spider Web Client Helper
    public class SpiderDocsConf : SpiderDocsWebAPIs.SpiderDocHelper.IConfiguration
    {

        /*
        private string HomeDir{get;set;}=string.Empty;
        private string Home()
        {   
            if( !string.IsNullOrWhiteSpace(HomeDir)) return HomeDir;

            string home = Guid.NewGuid().ToString().Substring(0, 5)
                    ,path = System.Web.HttpRuntime.AppDomainAppPath + "\\Content\\spiderdocs-temporary" + "\\" + home;

            Fn.MkDir(path);

            HomeDir = path;
            return HomeDir;
        }
        */
        public string GetWebTempPath()
        {
            string path = System.Web.HttpRuntime.AppDomainAppPath + "temp\\spiderdocs-temporary";
            SpiderDocsWebAPIs.Fn.MkDir(path);
            return path;
        }

        public string GetWebTempURL()
        {
            string webHome = SpiderDocsWebAPIs.Fn.ToWebPath(GetWebTempPath().Replace(System.Web.HttpRuntime.AppDomainAppPath, ""))
                    , authroty = HttpContext.Current.Request.Url.Authority;

            return string.Format("http://{0}/{1}", authroty, webHome);
        }


    }
    #endregion


    #region Spider Web Client

    //public class SpiderWebClientConf : SpiderDocsWebAPIs.SpiderDoscWebClient.IConfiguration
    //{
    //    Logger logger;

    //    public string GetServerURL()
    //    {
    //        return System.Configuration.ConfigurationManager.AppSettings["SpiderDocs.WebServerURL"];
    //    }

    //    public Logger GetLogger()
    //    {
    //        logger = NLog.LogManager.GetCurrentClassLogger();
    //        return logger;
    //    }

    //    public Func<DbManager> GetDbManager()
    //    {
    //        Match match = Regex.Match(GetServerURL(), @"https?:\/\/([a-zA-Z0-9\.\-]+):?([0-9]*)", RegexOptions.IgnoreCase);
    //        string server_address = match.Groups[1].Value;
    //        string port = match.Groups[2].Value == "" ? "80" : match.Groups[2].Value;

    //        SpiderDocsApplication.CurrentServerSettings = new ServerSettings();
    //        //SpiderDocsApplication.CurrentServerSettings.Load();
    //        SpiderDocsApplication.CurrentServerSettings.server = server_address;
    //        SpiderDocsApplication.CurrentServerSettings.port = int.Parse(port);

    //        /*
    //        SpiderDocsApplication.CurrentServerSettings = new ServerSettings();
    //        SpiderDocsApplication.CurrentServerSettings.server = "CETJNSQL01";
    //        SpiderDocsApplication.CurrentServerSettings.port = 5322;
    //        */
            
    //        SpiderDocsServer server = new SpiderDocsServer(SpiderDocsApplication.CurrentServerSettings);
    //        server.Connect();

    //        return SpiderDocsModule.SqlOperation.MethodToGetDbManager = new Func<DbManager>(() =>
    //        {
    //            return new DbManager(SpiderDocsApplication.CurrentServerSettings.conn, SpiderDocsApplication.CurrentServerSettings.svmode);
    //        });
    //    }

    //    public string LoginID()
    //    {
    //        return System.Configuration.ConfigurationManager.AppSettings["SpiderDocs.LoginID"];
    //    }

    //    public string LoginPassword()
    //    {
    //        return System.Configuration.ConfigurationManager.AppSettings["SpiderDocs.Password"];
    //    }
    //}
    #endregion
}

namespace SpiderDocsWebAPIs
{
    internal class Fn
    {
        /// <summary>
        /// Remove folder recurcy
        /// </summary>
        /// <param name="path">folder path</param>
        /// <param name="after">remove filder if folder is updated after this paramter day. will include this parameter days.the value should be absolute number</param>
        /// <returns></returns>
        public static void RmDir(string path, int after = 7)
        {
            string[] dirs = System.IO.Directory.GetDirectories(path);
            foreach (string dOrf in dirs)
            {
                System.IO.FileAttributes attr = System.IO.File.GetAttributes(dOrf);

                // When File
                if (!attr.HasFlag(System.IO.FileAttributes.Directory)) continue;

                // When Folder
                System.IO.DirectoryInfo d = new System.IO.DirectoryInfo(dOrf);
                if (d.LastWriteTime < DateTime.Now.AddDays(after * -1))
                {
                    d.Delete(true);
                    continue;
                }
                // Find meeting requirement folder to under this folder.
                RmDir(dOrf, after);
            }
        }

        public static bool MkDir(string FilePath)
        {
            if (!System.IO.Directory.Exists(FilePath))
            {
                System.IO.Directory.CreateDirectory(FilePath);
                return true;
            }
            return false;
        }

        public static string ToWebPath(string path)
        {
            return (new Regex(@"(/|\\)+")).Replace(path, "/");
        }

        /// <summary>
        /// aaa.jpg to aaa
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>filename without extension</returns>
        public static string Filename2Name(string filename)
        {
            var array = filename.Split(new char[] { '.' });

            return string.Join(".", array.Take(array.Count() - 1));
        }

        public static string Guid()
        {
            return System.Guid.NewGuid().ToString().Substring(0, 7);
        }

        public static string Extention(string path)
        {
            return Path2Filename(path).Split('.').LastOrDefault();
        }

        /// <summary>
        /// c:dev/aaa/bbb/ccc.jpg to ccc.jpg
        /// </summary>
        /// <param name="fullpath"></param>
        /// <returns>filename</returns>
        public static string Path2Filename(string fullpath)
        {
            string filename = fullpath
                                .Replace("/+", "/")
                                .Replace("\\+", "\\")
                                .Split(new char[] { '/', '\\' }).LastOrDefault();

            return filename;
        }
    }
}