using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSpiderDocs
{
	public static class SpiderHtmlHelper
	{
		public static ClientSideValidationDisabler BeginDisableClientSideValidation(this HtmlHelper html)
		{
			return new ClientSideValidationDisabler(html);
		}
	}

	public class ClientSideValidationDisabler : IDisposable
	{
		private HtmlHelper _html;

		public ClientSideValidationDisabler(HtmlHelper html)
		{
			_html = html;
			_html.EnableClientValidation(false);
		}

		public void Dispose()
		{
			_html.EnableClientValidation(true);
			_html = null;
		}
	}
}
