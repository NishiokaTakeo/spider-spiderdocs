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
using System.Reflection;

namespace WebSpiderDocs.Controllers
{
	public class UsersController : BaseController
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

		public UsersController() : base()
		{
		}

        public ActionResult Login()
		{
			//return null;

			return View();
		}

		[HttpPost]
		public ActionResult LoginByForm(UserViewModel model)
		{
			User user = UserController.LoginByMD5(model.Login, model.PasswordMD5);

			if (user.id == 0) logger.Info("Login was attempted, but failed. : {0}", model.Login);

			if (user.id > 0)
			{
				FormsAuthentication.SetAuthCookie(string.Format("{0}_{1}", user.id, model.Login), false);

				//FormsAuthentication.RedirectFromLoginPage(model.Login, false);

				return RedirectToAction("Index", "WorkSpace");
			}

			ModelState.AddModelError("Not Found", "The username or password is not correct.");

			return View("Login", model);
		}


		[HttpPost]
		public ActionResult Login(string UserName, string Password_Md5)
		{
            User user = UserController.LoginByMD5(UserName, Password_Md5);

            user.password = ""; // for sucurity

            if (user.id == 0) logger.Info("Login was attempted, but failed. : {0}", UserName);

            return Json(new { User = user });
		}

		[HttpPost]
		public void Logout()
		{
			Logout(false);
		}

		public void Logout(bool RedirectToLoginPage)
		{
			FormsAuthentication.SignOut();

			if(RedirectToLoginPage)
				FormsAuthentication.RedirectToLoginPage("");
		}
	}
}
