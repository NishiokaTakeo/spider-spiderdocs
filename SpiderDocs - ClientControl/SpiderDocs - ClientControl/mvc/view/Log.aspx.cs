using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using NLog;
namespace SpiderDocs_ClientControl
{
    public partial class Log : System.Web.UI.Page
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            logger.Trace("Begin {0} {1}", P("machinename"), P("callsite"));

            if (Request.QueryString.Count == 0 && Request.Form.Count == 0) return;

            if (isSpam(Request["comments"]))
            {
                logger.Info("Detected as Spam :\"{0}\"", Request["comments"]);

                Response.StatusCode = 401;
                Response.End();

                return;
            }
            try
            {
                switch ( P("level").ToLower().Trim() )
                {
                    case "error":
                        logger.Error("{0} {1} {2} {3}", P("machinename"), P("callsite"), P("message"), P("StackTrace"));
                        break;
                    case "warn":
                        logger.Warn("{0} {1} {2} {3}", P("machinename"), P("callsite"), P("message"), P("StackTrace"));
                        break;
                    case "info":
                        logger.Info("{0} {1} {2}", P("machinename"), P("callsite"), P("message"));
                        break;
                    case "debug":
                        logger.Debug("{0} {1} {2}", P("machinename"), P("callsite"), P("message"));
                        break;
                    case "trace":
                        logger.Trace("{0} {1} {2}", P("machinename"), P("callsite"), P("message"));
                        break;
                    default:
                        logger.Error("{0} {1} {2} {3}", P("machinename"), P("callsite"), P("message"), P("StackTrace"));
                        break;
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

        }

        string P(string name)
        {
            return Request.QueryString[name];
        }

        public class NLogFormat
        {
            public string level { get; set; } = string.Empty;
            public string callsite { get; set; } = string.Empty;
            public string message { get; set; } = string.Empty;
            public string StackTrace { get; set; } = string.Empty;
        }


        public bool isSpam(string textInput)
        {
            if (string.IsNullOrWhiteSpace(textInput)) textInput = string.Empty;

            string pattern = string.Empty;

            pattern = @"[АаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯяѲѳѢѣѴѵѮѯЅѕѰѱѠѡѪѫѦѧѬѭѨѩ]";
            if (Regex.Matches(textInput, pattern).Count > 10)
            {
                return true;
            }

            pattern = @"https?\:\/\/";
            if (Regex.Matches(textInput, pattern).Count > 0)
            {
                return true;
            }


            return false;
        }


    }
}
