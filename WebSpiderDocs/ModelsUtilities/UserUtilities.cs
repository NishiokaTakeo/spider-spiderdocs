using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SpiderDocsModule;
using WebSpiderDocs.Models;

namespace WebSpiderDocs
{
	public class UserUtilities
	{
		public static List<User> GetAll()
		{
			List<User> ans = UserController.GetUser(true, false);
			ans = ans.OrderBy(a => a.name).ToList(); 

			return ans;
		}

		public static SelectList GetSelectList(IList<User> src = null, int? id = null)
		{
			if(src == null)
				src = GetAll();

			return new SelectList(src, "id", "name", id);
		}
	}
}
