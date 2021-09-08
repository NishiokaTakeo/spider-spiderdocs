using System;
using System.Collections.Generic;

namespace SpiderDocsModule
{
	//public class Email :  Spider.Net.Email
	//{
        
 //   }

	public static class EmailExtension
	{
		public static void OpenNewEmail(this Spider.Net.Email mail, string subject, string body, List<string> Attachements)
		{
			mail.subject = subject;
			mail.body = body;

			foreach(string FileLocation in Attachements)
				mail.attachments.Add(FileLocation);

			mail.OpenNewEmail();
		}
	}
}
