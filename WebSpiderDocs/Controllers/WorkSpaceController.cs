using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using WebSpiderDocs.Models;
using SpiderDocsModule;
using NLog;
using System.Threading.Tasks;

namespace WebSpiderDocs.Controllers
{

	public class WorkSpaceController : Controller
	{
		// GET: WorkSpace
		[Attributes.CustomAuthorize]
		public ActionResult Index(int id = 0)
        {
			return View();
        }

		[Attributes.CustomAuthorize]
		public ActionResult Local()
		{
			return View("Local");
		}

    }

	//public class CustomAuthorize : System.Web.Mvc.AuthorizeAttribute
	//{
	//	protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
	//	{
	//		if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
	//		{
	//			filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new
	//				System.Web.Routing.RouteValueDictionary(new { action="Login", controller = "Users" }));

	//		}

	//		//if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
	//		//{
	//		//	filterContext.Result = new System.Web.Mvc.HttpUnauthorizedResult();
	//		//}
	//		//else
	//		//{
	//		//	filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new
	//		//		System.Web.Routing.RouteValueDictionary(new { controller = "AccessDenied" }));
	//		//}
	//	}
	//}
}