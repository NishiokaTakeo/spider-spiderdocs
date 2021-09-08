using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.Net;
using System.Net.Mime;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Spider.IO;
using System.Text.RegularExpressions;


//---------------------------------------------------------------------------------
namespace Spider.Net
{
//---------------------------------------------------------------------------------
	public class SMTPSettings
	{
		public string ServerAddress;
		public int Port = 25;
		public int Timeout = 20000;
		public string User;
		public string Password;
		public bool SSL = false;
        //public DeliveryMethod  DeliveryMethod  = SmtpDeliveryMethod.Network
	}

	public class EmailInlineImages
	{
		///<summary>
		///Image name which appears in body like &lt;img src="cid:name" /&gt;<br />
		///</summary>
		public string name;

		///<summary>
		///Full local path for an image file.<br />
		///</summary>
		public string path;

		public EmailInlineImages(string Name, string Path)
		{
			name = Name;
			path = Path;
		}
	}

//---------------------------------------------------------------------------------
	/// <summary>
	/// <para>Generic email class which allows you to send emails easily.</para>
	/// </summary>
	public class Email
	{
		readonly string dummyEmail = "dummy@dummy.com";

		SMTPSettings server;

		///<summary>
		///Set true if body is not formatted in HTML. It automatically convert current body to HTML format.<br />
		///</summary>
		public bool IsBodyHtml = true;

		///<summary>
		///Do not use this parameter as it is obsolete.<br />
		///</summary>
		public bool PlainText
		{
			get { return IsBodyHtml; }
			set { IsBodyHtml = value; }
		}

		///<summary>
		///Set full local paths of attachments which will be sent with an email.<br />
		///</summary>
		public List<string> attachments = new List<string>();

		///<summary>
		///Set inline images which will be embedded into an email.<br />
		///</summary>
		public List<EmailInlineImages> InlineImages = new List<EmailInlineImages>();

		///<summary>
		///If true, all files in the "attachments" property will be deleted after sending mail.<br />
		///Please be careful to use this property.
		///</summary>
		public bool DeleteAttachements = false;

		///<summary>
		///If true, this does not wait for the email is sent. You can set a call back function to OnEmailSent to get result.<br />
		///</summary>
		public bool MultiThread = true;
		
		///<summary>
		///Set an instance of call back function which will be called when email is transfered from the server.<br />
		///</summary>
		public EmailSent OnEmailSent;
		public delegate void EmailSent(string err);

		public string subject { get; set; }

		///<summary>
		///body should be HTML otherwise set IsConvertBodyToHTML = true.<br />
		///</summary>
		public string body { get; set; }

		public List<string> to = new List<string>();
		public List<string> cc = new List<string>();
		public List<string> bcc = new List<string>();
        public string ReplayTo { get; set; }

		/// <summary>
		/// <para>Set a from email address. User name of SMTP server credential is used if this is not set.</para>
		/// </summary>
		public string from
		{
			get
			{
				if(String.IsNullOrEmpty(_from) && (server != null))
					return server.User;
				else
					return _from;
			}

			set
			{
				_from = value;
			}
		}
		string _from;

		/// <summary>
		/// <para>Set name which appears in recipient's email client instead of the the from email address.</para>
		/// </summary>
		public string from_name { get; set; }

		/// <summary>
		/// <para>Set an email address to reply.</para>
		/// </summary>
		public string reply { get; set; }

		string SiteRootLocalPath;
		string SiteRootRemoteUrl;

//---------------------------------------------------------------------------------
		/// <summary>
		/// <para>Use another constructor if you will send emails to Outlook.com or hotmail.com with inline images!!</para>
		/// </summary>
		public Email(SMTPSettings SMTPServerCredential)
		{
			server = SMTPServerCredential;
		}

		/// <summary>
		/// <para>You need to use this constructor if you will send emails to Outlook.com or hotmail.com with inline images.</para>
		/// </summary>
		/// <param name="SiteRootLocalPath">The root local path of a website which can host the inline images.</param>
		/// /// <param name="SiteRootRemoteUrl">The root remote URL of a website which can host the inline images.</param>
		public Email(SMTPSettings SMTPServerCredential, string SiteRootLocalPath, string SiteRootRemoteUrl)
		{
			server = SMTPServerCredential;
			this.SiteRootLocalPath = SiteRootLocalPath;
			this.SiteRootRemoteUrl = SiteRootRemoteUrl;
		}

		public Email()
		{
		}

//---------------------------------------------------------------------------------
		public void SetSMTPSettings(SMTPSettings server)
		{
			this.server = server;
		}

//---------------------------------------------------------------------------------
		public void Send(string FullPathToSaveEml = "")
		{
			if(server == null)
				return;
            
            MailMessage ms = GetMailMessage();
            
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

			SmtpClient smtp = new SmtpClient
			{
				Host = server.ServerAddress,
				Port = server.Port,
				EnableSsl = server.SSL,
				Credentials = new NetworkCredential(server.User, server.Password),
				Timeout = server.Timeout
			};

			// If MultiThread is true, send this email in different thread not to stop mail thread.
			if(MultiThread)
			{
				if(SynchronizationContext.Current == null)
					SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

				System.Threading.Tasks.Task task = new System.Threading.Tasks.Task(() => Task_Send(smtp, ms, FullPathToSaveEml));
				task.ContinueWith((a) =>
				{
					if(OnEmailSent != null)
					{
						string ans = "";

						if(a.Exception != null)
							ans = a.Exception.Message;

						OnEmailSent(ans); // Callback
					}

				}, TaskScheduler.FromCurrentSynchronizationContext());

				task.Start();

			}else
			{
				Send(smtp, ms, FullPathToSaveEml);
			}
		}

//---------------------------------------------------------------------------------
		void Task_Send(SmtpClient smtp, MailMessage ms, string FullPathToSaveEml)
		{
			Send(smtp, ms, FullPathToSaveEml);
		}

//---------------------------------------------------------------------------------
		void Send(SmtpClient smtp, MailMessage ms, string FullPathToSaveEml)
		{           
            smtp.Send(ms);

			if(!String.IsNullOrEmpty(FullPathToSaveEml))
				SaveEmlFile(ms, FullPathToSaveEml, false);

			smtp.Dispose();
			ms.Dispose();

			// Delete attachment files in local file system.
			if(DeleteAttachements && (attachments != null) && (0 < attachments.Count) && FileFolder.DeleteFiles(attachments))
			{
                /* no need to remove directory.
				string path = FileFolder.GetPath(attachments[0]);
                
				if(!FileFolder.IsFileOrDirectoryExistsIn(path))
					Directory.Delete(path, true);
                */
			}
		}

//---------------------------------------------------------------------------------
		public void OpenNewEmail()
		{
			string path = Path.GetTempFileName() + ".eml";

			MailMessage ms = GetMailMessage();
			ms.From = new MailAddress(dummyEmail);
			ms.To.Add(new MailAddress(dummyEmail));
			ms.Headers.Add("X-Unsent", "1");

			SaveEmlFile(ms, path, true);
			Process.Start(path);
		}

//---------------------------------------------------------------------------------
		void SaveEmlFile(MailMessage ms, string FullPathToSave, bool RemoveFromAndTo)
		{
			string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			Directory.CreateDirectory(path);

			SmtpClient smtp = new SmtpClient();
			smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
			smtp.PickupDirectoryLocation = path;
			smtp.Send(ms);

			path = Directory.GetFiles(path).Single();

			using(var sr = new StreamReader(path))
			{
				using(var sw = new StreamWriter(FullPathToSave))
				{
					if(RemoveFromAndTo)
					{
						string line;

						while((line = sr.ReadLine()) != null)
						{
							if(!line.StartsWith("X-Sender:")
							&& !line.StartsWith("From:")
							&& !line.StartsWith("X-Receiver: " + dummyEmail)
							&& !line.StartsWith("To: " + dummyEmail))
							{
								sw.WriteLine(line);
							}
						}

					}else
					{
						sw.Write(sr.ReadToEnd());
					}

					sw.Close();
				}

				sr.Close();
			}
		}

//---------------------------------------------------------------------------------
		MailMessage GetMailMessage()
		{
			MailMessage ms = new MailMessage();
			bool MSAccount = false;
			string wrk_body = this.body;

			// --- Filter duplicated and incorrect format email and add them into lists. ---
				// 1. Check BCC at first. Since BCC is to hide email addresses from recipients, this list should have the highest priority to be added into the list.
				bcc = bcc.Distinct().ToList();
				bcc.RemoveAll(a => !a.Contains("@"));
				foreach(string wrk in bcc)
					ms.Bcc.Add(new MailAddress(wrk));

				// 2. To is 2nd priority.
				to = to.Distinct().ToList();
				to.RemoveAll(a => !a.Contains("@"));
				to.RemoveAll(a => bcc.Contains(a)); // Removing emails which exist in BCC already.
				foreach(string wrk in to)
					ms.To.Add(new MailAddress(wrk));

				// 3. Finally, add to CC
				cc = cc.Distinct().ToList();
				cc.RemoveAll(a => !a.Contains("@"));
				cc.RemoveAll(a => to.Contains(a)); // Removing emails which exist in To already.
				cc.RemoveAll(a => bcc.Contains(a)); // Removing emails which exist in BCC already.
				foreach(string wrk in cc)
					ms.CC.Add(new MailAddress(wrk));
			// --- End of adding emails. ---

			if(!String.IsNullOrEmpty(from))
			{
				if(!String.IsNullOrEmpty(from_name))
					ms.From = new MailAddress(from, from_name);
				else
					ms.From = new MailAddress(from);

			}

			if(!String.IsNullOrEmpty(this.reply))
				ms.ReplyToList.Add(reply);

			ms.Subject = this.subject;

			foreach(string path in attachments)
			{
				Attachment item = new Attachment(path);
				ms.Attachments.Add(item);
			}
            
            // If body is not in HTML, convert it to HTML.
            ms.IsBodyHtml = IsBodyHtml;
            if (IsBodyHtml)
            {
                if (wrk_body.ToLower().IndexOf("<html") == -1)
                    wrk_body = "<HTML><BODY>" + wrk_body.Replace("\n", "<br />") + "</BODY></HTML>";

                // Check if there is a recipient who has Outlook.com account as it needs special treatment for inline images.
                if (this.to.Exists(a => a.ToLower().Contains("@hotmail.com") || a.ToLower().Contains("@outlook.com")))
                    MSAccount = true;

                // If there is no emails to Outlook.com or hotmail.com, attach inline images normally.
                if (!MSAccount)
                {
                    AlternateView view = AlternateView.CreateAlternateViewFromString(wrk_body, null, "text/html");

                    foreach (EmailInlineImages InlineImage in InlineImages)
                    {
                        LinkedResource wrk = new LinkedResource(InlineImage.path);
                        wrk.ContentId = InlineImage.name;
                        wrk.ContentType = new ContentType(Spider.NET.MIME.GetMimeType(Path.GetExtension(InlineImage.path)));
                        view.LinkedResources.Add(wrk);
                    }

                    ms.AlternateViews.Add(view);

                    // *** Workaround for inline images for Outlook.com ***
                    // LikedResource for inline images does not work on Outlook.com.
                    // This code replaces embedding images to linked images on the server.
                    // This code is not needed once Outlook.com fixes this issue.
                    // This code is only for web application.
                }
                else if (!String.IsNullOrEmpty(SiteRootLocalPath) && !String.IsNullOrEmpty(SiteRootRemoteUrl))
                {
                    foreach (EmailInlineImages InlineImage in InlineImages)
                    {
                        string image_url = InlineImage.path.Replace(SiteRootLocalPath, "");
                        image_url = SiteRootRemoteUrl + image_url.Replace('\\', '/');

                        wrk_body = wrk_body.Replace("cid:" + InlineImage.name, image_url);
                    }

                    ms.Body = wrk_body;
                }
            }
            else
            {
                ms.Body = wrk_body;
            }

            //ms.BodyEncoding = System.Text.Encoding.UTF8;

            return ms;
		}

        string GetPlainTextFromHtml(string htmlString)
        {
            string htmlTagPattern = @"<.*?>";
            var regexCss = new Regex(@"(\<script(.+?)\)|(\<style(.+?)\)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            htmlString = regexCss.Replace(htmlString, string.Empty);
            htmlString = Regex.Replace(htmlString, htmlTagPattern, string.Empty);
            htmlString = Regex.Replace(htmlString, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
            htmlString = htmlString.Replace(" ", string.Empty);

            return htmlString;
        }

        //---------------------------------------------------------------------------------
    }
}
