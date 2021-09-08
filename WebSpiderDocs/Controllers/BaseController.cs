using System;
using System.Web.Mvc;
using System.Web.Security;
using NLog;
using SpiderDocsModule;

namespace WebSpiderDocs.Controllers
{
    public class BaseController : Controller
	{
        Logger logger = LogManager.GetCurrentClassLogger();

		// workaround to fire ExecuteCore() before each actions are called
		protected override bool DisableAsyncSupport
		{
			get { return true; }
		}

		//UserGroups AllowedGroup = UserGroups.INVALID; // allows to everyone as a default

		//public BaseController(UserGroups AllowedGroup = UserGroups.INVALID)
		//{
		//	this.AllowedGroup = AllowedGroup;
		//}

		public BaseController()
		{

		}

		protected override void ExecuteCore()
		{
            var temp = System.Web.Configuration.WebConfigurationManager.AppSettings["Folder.Temp"];

            new WebUtilities().DeleteOldTempFiles(before: DateTime.Now.AddHours(-WebSettings.ExportDocKeepHours));
            if( !string.IsNullOrWhiteSpace(temp))
                new WebUtilities(temp).DeleteOldTempFiles(before: DateTime.Now.AddHours(-WebSettings.ExportDocKeepHours));

            string actionName = Convert.ToString(this.ControllerContext.RouteData.Values["action"]);
			string controller = Convert.ToString(this.ControllerContext.RouteData.Values["controller"]);

            //SpiderDocsModule.Cache.RemoveAll();

            //if(0 < WebSettings.CurrentUserId ) new SpiderDocsModule.Cache(WebSettings.CurrentUserId).Create();

            if ((0 < WebSettings.CurrentUserId)
			|| (controller == "Users" && actionName == "Login"))
			{
                logger.Debug("controller:{0}, action:{1}, {2}", controller, actionName, WebSettings.CurrentUserId);

                try
                {
                    MMF.WriteData<int>(WebSettings.CurrentUserId, SpiderDocsModule.MMF_Items.UserId);

                    base.ExecuteCore();
                }
                catch (Exception ex)
                {
                    logger.Error(ex,"{0} {1}, {2}", controller, actionName, WebSettings.CurrentUserId);
                }
            }
			else if(controller == "Users" && actionName == "LoginByForm")
			{
				
				base.ExecuteCore();
				
			}
            else
			{
				FormsAuthentication.RedirectToLoginPage("");
			}
		}
	}
}
