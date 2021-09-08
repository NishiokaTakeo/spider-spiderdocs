using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using InspectorWrapperExplained;
using lib = SpiderDocsModule.Library;
//using SpiderDocsForms;
using Spider.Task;
using SpiderDocsModule;

//---------------------------------------------------------------------------------
namespace AddInOutlook2013
{
	public partial class ThisAddIn
	{
		private Inspectors _inspectors;
		private Dictionary<Guid, InspectorWrapper> _wrappedInspectors;
		private Dictionary<Guid, OpenedMail> OpenedMails;

//---------------------------------------------------------------------------------
		private void ThisAddIn_Startup(object sender, System.EventArgs e)
		{
			_wrappedInspectors = new Dictionary<Guid, InspectorWrapper>();
			OpenedMails = new Dictionary<Guid, OpenedMail>();

			_inspectors = Globals.ThisAddIn.Application.Inspectors;
			_inspectors.NewInspector += new InspectorsEvents_NewInspectorEventHandler(WrapInspector);

			Globals.ThisAddIn.Application.ItemSend += new ApplicationEvents_11_ItemSendEventHandler(ItemSend);
			
			// Handle also already existing Inspectors
			// (e.g. Double clicking a .msg file)
			foreach(Inspector inspector in _inspectors)
				WrapInspector(inspector);
		}

//---------------------------------------------------------------------------------
		void ItemSend(object Item, ref bool Cancel)
		{
			if(SpiderDocsApplication.IsLoggedIn && SpiderDocsApplication.CurrentUserGlobalSettings.show_import_dialog_new_mail)
			{
				DialogResult result = MessageBox.Show(lib.msg_file_save_db, lib.msg_messabox_title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

				if(result == DialogResult.Yes)
				{
					OpenedMail SendMail = null;
			
					foreach(KeyValuePair<Guid, OpenedMail> wrk in OpenedMails)
					{
						if((MailItem)((OpenedMail)wrk.Value).mail == (MailItem)Item)
						{
							SendMail = (OpenedMail)wrk.Value;
							break;
						}
					}

					if(SendMail != null)
						SendMail.ribbon.SaveToSpiderDocs(SendMail.ribbon.Context, true);

				}else if(result == DialogResult.Cancel)
				{
					Cancel = true;
				}
			}
		}

//---------------------------------------------------------------------------------
		void WrapInspector(Inspector inspector)
		{
			InspectorWrapper wrapper = InspectorWrapper.GetWrapperFor(inspector);

			if(wrapper != null)
			{
				// register for the closed event
				wrapper.Closed += new InspectorWrapperClosedEventHandler(wrapper_Closed);

				// remember the inspector in memory
				_wrappedInspectors[wrapper.Id] = wrapper;

				if(wrapper is MailItemWrapper)
					OpenedMails[wrapper.Id] = new OpenedMail((MailItemWrapper)wrapper);
			}
		}

//---------------------------------------------------------------------------------
		 void wrapper_Closed(Guid id)
		 {
			OpenedMails[id].Dispose();
			OpenedMails.Remove(id);

			_wrappedInspectors.Remove(id);

			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

//---------------------------------------------------------------------------------
		private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
		{
			_wrappedInspectors.Clear();
			_inspectors.NewInspector -= new InspectorsEvents_NewInspectorEventHandler(WrapInspector); 
			_inspectors = null;

			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

//---------------------------------------------------------------------------------
		class OpenedMail : IDisposable
		{
			MailItemWrapper wrapper;
			Guid id { get { return wrapper.Id; } } // just for debug
			
			MailItem _mail;
			public MailItem mail { get { return _mail; } }

			SpiderDocsOutlookRibbon _ribbon;
			public SpiderDocsOutlookRibbon ribbon { get { return _ribbon; } }

			bool IsDisposed = false;

//---------------------------------------------------------------------------------
			public OpenedMail(MailItemWrapper Wrapper)
			{
				wrapper = Wrapper;
				_mail = wrapper.Item;
				((InspectorEvents_10_Event)((MailItemWrapper)wrapper).Inspector).Activate += new InspectorEvents_10_ActivateEventHandler(this.ActivateEventHandler);
			}

//---------------------------------------------------------------------------------
			public virtual void Dispose()
			{
				if(!IsDisposed)
				{
					wrapper = null;

					if(_mail != null)
					{
                        try { ((Microsoft.Office.Interop.Outlook._MailItem)_mail).Close(OlInspectorClose.olDiscard); } catch { }
						Marshal.ReleaseComObject(_mail);
						_mail = null;

						if(_ribbon != null)
							_ribbon.Dispose();
					}

					GC.Collect();
					GC.WaitForPendingFinalizers();

					IsDisposed = true;
				}
			}

			~OpenedMail()
			{
				Dispose();
			}

//---------------------------------------------------------------------------------
			void ActivateEventHandler()
			{
				if(!IsDisposed)
					new SpiderThread(wait, callback, 500).Start(wrapper);
			}

//---------------------------------------------------------------------------------
			object wait(object arg)
			{
				object ans = null;

				while(wrapper == null);

				ThisRibbonCollection ribbonCollection = Globals.Ribbons[wrapper.Inspector];
				
				while(ans == null)
				{
					if(ribbonCollection.ribbon != null)
						ans = ribbonCollection.ribbon;
					else if(ribbonCollection.RibbonCompose != null)
						ans = ribbonCollection.RibbonCompose;
				}

				return ans;
			}

//---------------------------------------------------------------------------------
			void callback(object arg)
			{
				string SavedPath = "";

				_ribbon = (SpiderDocsOutlookRibbon)arg;

				_ribbon.ThisIsOpenedByMailWindow(_mail, SavedPath);
				_ribbon.SetMenuEnabled(SavedPath);
			}
		}

//---------------------------------------------------------------------------------
		#region VSTO generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InternalStartup()
		{
			this.Startup += new System.EventHandler(ThisAddIn_Startup);
			this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
		}
		
		#endregion
	}
}
