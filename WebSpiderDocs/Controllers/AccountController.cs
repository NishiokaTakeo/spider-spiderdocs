using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSpiderDocs.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

		[Authorize]
		public ActionResult Logout()
		{
			System.Web.Security.FormsAuthentication.SignOut();

			return RedirectToAction("Login", "Users");
		}
    }
}