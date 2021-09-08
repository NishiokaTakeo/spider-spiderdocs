using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSpiderDocs.Attributes
{
	public class Attributes
	{
	}

	public class CustomAuthorize : System.Web.Mvc.AuthorizeAttribute
	{
		protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
		{
			if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
			{
				filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new
					System.Web.Routing.RouteValueDictionary(new { action = "Login", controller = "Users" }));

			}

			//if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
			//{
			//	filterContext.Result = new System.Web.Mvc.HttpUnauthorizedResult();
			//}
			//else
			//{
			//	filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new
			//		System.Web.Routing.RouteValueDictionary(new { controller = "AccessDenied" }));
			//}
		}
	}

}