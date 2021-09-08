using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using lib = SpiderDocsModule.Library;
using DocumentAttribute = SpiderDocsModule.DocumentAttribute;
using SystemAttributes = SpiderDocsModule.SystemAttributes;
using en_AttrType = SpiderDocsModule.en_AttrType;
using en_AttrCheckState = SpiderDocsModule.en_AttrCheckState;
//using SpiderDocsForms;
using FileFolder = SpiderDocsForms.FileFolder;
using Document = SpiderDocsForms.Document;
using Utilities = SpiderDocsForms.Utilities;
using AddInModules;
using NLog;
using SpiderDocsModule;

//---------------------------------------------------------------------------------
namespace AddInOutlook2013
{
	public class SpiderDocsOutlookRibbon : SpiderDocsRibbon
	{
		protected MailItem CurrentMail;
		protected string SavedPath;
		protected bool IsOpenedByMailWindow = false;

//---------------------------------------------------------------------------------
		public SpiderDocsOutlookRibbon() : base(Globals.Factory.GetRibbonFactory())
		{
		}

//---------------------------------------------------------------------------------
		override public void SetMenuEnabled()
		{
			// dummy
		}

//---------------------------------------------------------------------------------
		public void ThisIsOpenedByMailWindow(MailItem mail, string path)
		{
			CurrentMail = mail;
			SavedPath = path;
			IsOpenedByMailWindow = true;
		}

//---------------------------------------------------------------------------------
        /// <summary>
        /// Save doc to database
        /// </summary>
        /// <param name="Context"></param>
        /// <param name="SaveToWorkspace"></param>
        /// <param name="composed"></param>
		public void SaveToSpiderDocs(object Context, bool composed)
		{
			string strDocPath = SaveTempFile(Context);
            try
            {
			    if(!String.IsNullOrEmpty(strDocPath))
			    {

					Document doc;
                    bool ans = AddInModule.SaveDocument(strDocPath, CurrentMail, OutlookSaveMethods, out doc);
					
					if(ans)
					{
						CurrentMail.Categories = "Checked In at Spiderdocs";
						
						doc.Attrs.RemoveAll(a =>
									    a.SystemAttributeType == SystemAttributes.MailSubject
									|| a.SystemAttributeType == SystemAttributes.MailFrom
									|| a.SystemAttributeType == SystemAttributes.MailIsComposed
									|| a.SystemAttributeType == SystemAttributes.MailTime
									|| a.SystemAttributeType == SystemAttributes.MailTo);

						DocumentAttribute attr;

						attr = new DocumentAttribute();
						attr.SystemAttributeType = SystemAttributes.MailIsComposed;
						attr.id_type = en_AttrType.ChkBox;
						attr.atbValue = (composed ? en_AttrCheckState.True : en_AttrCheckState.False);
						doc.Attrs.Add(attr);

						string subject = "";
						if(!String.IsNullOrEmpty(CurrentMail.Subject))
							subject = CurrentMail.Subject;

						attr = new DocumentAttribute();
						attr.SystemAttributeType = SystemAttributes.MailSubject;
						attr.id_type = en_AttrType.Label;
						attr.atbValue = subject;
						doc.Attrs.Add(attr);

						string sender = "";
						if(CurrentMail.Sender != null)
							sender = GetSenderSMTPAddress(CurrentMail);

						attr = new DocumentAttribute();
						attr.SystemAttributeType = SystemAttributes.MailFrom;
						attr.id_type = en_AttrType.Label;
						attr.atbValue = sender;
						doc.Attrs.Add(attr);

						string recipient = "";
						if(0 < CurrentMail.Recipients.Count)
							recipient = String.Join(", ", GetSMTPAddressForRecipients(CurrentMail));

						attr = new DocumentAttribute();
						attr.SystemAttributeType = SystemAttributes.MailTo;
						attr.id_type = en_AttrType.Label;
						attr.atbValue = recipient;
						doc.Attrs.Add(attr);

						attr = new DocumentAttribute();
						attr.SystemAttributeType = SystemAttributes.MailTime;
						attr.id_type = en_AttrType.DateTime;
						attr.atbValue = CurrentMail.ReceivedTime;
						doc.Attrs.Add(attr);

						doc.UpdateProperty(false);
						MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.GridUpdateCount);

                        CurrentMail.Save();
					}
			    }
            }
            catch (System.Exception e)
            {
                logger.Error(e);
            }
		}

        /// <summary>
        /// Save doc to work space
        /// </summary>
        /// <param name="Context"></param>
        public void SaveToWorkSpace(object Context)
        {
            string strDocPath = SaveTempFile(Context);

            try
            {
                if (!String.IsNullOrEmpty(strDocPath))
                {                    
                    bool ans = AddInModule.SaveWorkspace(strDocPath, CurrentMail, OutlookSaveMethods);

                    if (ans) 
                    { 
                        CurrentMail.Categories = "Saved in SpiderDocs Workspace";
                        CurrentMail.Save();
                    }                        
                }
            }
            catch (System.Exception e)
            {
                logger.Error(e);
            }
        }


//---------------------------------------------------------------------------------
		protected string SaveTempFile(object Context)
		{
			string strDocPath = "";
            try{
			    //get the origin of this email
			    if(!IsOpenedByMailWindow)
				    CurrentMail = checkOriginEmail(Context);
			
			    if(CurrentMail != null)
			    {
                    SpiderDocsModule.Document doc = null;

				    if(!IsOpenedByMailWindow && !String.IsNullOrEmpty(SavedPath))
				    {
					    strDocPath = SavedPath;

				    }else
				    {
					    string subject = CurrentMail.Subject;
					    if(String.IsNullOrEmpty(subject))
						    subject = "No Subject";
					
					    //This setting is special only for NECA Legal added at V1.5.9.
					    //All functions which are related to this setting should be moved to a NECA Legal plug-in and
					    //removed from the main code in the future.
					    if(!SpiderDocsApplication.CurrentPublicSettings.ignore_subject_prefix)
						    doc = SpiderDocsModule.FileFolder.GetDocumentFromMail(subject);
					    //until here

					    string strDocName = "";
					    if(doc != null)
						    strDocName = doc.PathWithVersionIdFolder;
					    else
						    strDocName = Document.ToValidTitle(subject) + ".msg"; //check invalid chacacters

					    strDocPath = FileFolder.GetTempFolder() + strDocName;
					    string path = FileFolder.GetPath(strDocPath);

					    if(Directory.Exists(path))
						    FileFolder.DeleteFiles(path);
					    else
						    Directory.CreateDirectory(path);

					    try { CurrentMail.SaveAs(strDocPath); } catch {} //save doc in hard disk (temp folder)
				    }
			    }
            }
            catch (System.Exception e)
            {
                logger.Error(e);
            }
            return strDocPath;
		}

//---------------------------------------------------------------------------------
		private MailItem checkOriginEmail(object Context)
		{
			// Check to see if a item is select in explorer or in inspector.
			if(Context is Inspector)
			{
				Inspector inspector = (Inspector)Context;

				if(inspector.CurrentItem is MailItem)
					return inspector.CurrentItem;
			}

			if(Context is Explorer)
			{
				Explorer explorer = (Explorer)Context;
				Selection selectedItems = explorer.Selection;

				foreach(MailItem message in selectedItems)
				{
					if(message is MailItem)
						return message;
				}
			}

			MessageBox.Show("There is no email selected.", lib.msg_messabox_title);
			return null;
		}

//---------------------------------------------------------------------------------
		bool OutlookSaveMethods(string path, Document doc)
		{
			if(doc != null)
			{
				//This setting is special only for NECA Legal added at V1.5.9.
				//All functions which are related to this setting should be moved to a NECA Legal plug-in and
				//removed from the main code in the future.
				if(!SpiderDocsApplication.CurrentPublicSettings.ignore_subject_prefix)
				{
					CurrentMail.Subject = FileFolder.SetMailSubject(CurrentMail.Subject, doc.id, doc.version);
				}
				//until here
			}
			
			if(String.IsNullOrEmpty(path))
				try { CurrentMail.Save(); } catch { }
			else
				CurrentMail.SaveAs(path);

			return true;
		}

//---------------------------------------------------------------------------------
		private List<string> GetSMTPAddressForRecipients(MailItem mail) 
		{ 
			List<string> ans = new List<string>();
			
			const string PR_SMTP_ADDRESS = 
				"http://schemas.microsoft.com/mapi/proptag/0x39FE001E"; 
			Recipients recips = mail.Recipients; 
			foreach (Recipient recip in recips) 
			{ 
				PropertyAccessor pa = recip.PropertyAccessor; 
				string smtpAddress = 
					pa.GetProperty(PR_SMTP_ADDRESS).ToString(); 

				ans.Add(smtpAddress);
			} 

			return ans;
		}

//---------------------------------------------------------------------------------
		private string GetSenderSMTPAddress(MailItem mail)
		{
			string PR_SMTP_ADDRESS =
				@"http://schemas.microsoft.com/mapi/proptag/0x39FE001E";
			if (mail == null)
			{
				throw new ArgumentNullException();
			}
			if (mail.SenderEmailType == "EX")
			{
				AddressEntry sender =
					mail.Sender;
				if (sender != null)
				{
					//Now we have an AddressEntry representing the Sender
					if (sender.AddressEntryUserType ==
						OlAddressEntryUserType.
						olExchangeUserAddressEntry
						|| sender.AddressEntryUserType ==
						OlAddressEntryUserType.
						olExchangeRemoteUserAddressEntry)
					{
						//Use the ExchangeUser object PrimarySMTPAddress
						ExchangeUser exchUser =
							sender.GetExchangeUser();
						if (exchUser != null)
						{
							return exchUser.PrimarySmtpAddress;
						}
						else
						{
							return null;
						}
					}
					else
					{
						return sender.PropertyAccessor.GetProperty(
							PR_SMTP_ADDRESS) as string;
					}
				}
				else
				{
					return null;
				}
			}
			else
			{
				return mail.SenderEmailAddress;
			}
		}

//---------------------------------------------------------------------------------
	}
}