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

	public class OptionsController : Controller
	{
		// GET: WorkSpace
		[Attributes.CustomAuthorize]
		public ActionResult Preferences()
        {
			return View("Preferences");
        }

		// [Attributes.CustomAuthorize]
		// public ActionResult Group()
		// {
		// 	return View("Group");
		// }

		// [Attributes.CustomAuthorize]
		// public ActionResult Folder()
		// {
		// 	return View("Folder");
		// }
	}
}