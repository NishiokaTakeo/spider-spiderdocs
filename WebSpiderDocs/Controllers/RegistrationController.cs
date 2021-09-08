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

	public class RegistrationController : Controller
	{
		// GET: WorkSpace
		[Attributes.CustomAuthorize]
		public ActionResult Index()
        {
			return View();
        }

		[Attributes.CustomAuthorize]
		public ActionResult Users()
		{
			return View("Users");
		}

		[Attributes.CustomAuthorize]
		public ActionResult Attributes()
		{
			return View("Attributes");
		}

		[Attributes.CustomAuthorize]
		public ActionResult Group()
		{
			return View("Group");
		}

		[Attributes.CustomAuthorize]
		public ActionResult DocumentType()
		{
			return View("DocumentType");
		}

	}
}