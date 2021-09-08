using System;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Outlook;
using lib = SpiderDocsModule.Library;
using SpiderDocsForms;
using NLog;
using SpiderDocsModule;

namespace AddInOutlookAttachment2013
{
	[ComVisible(true)]
	public class RibbonAttachment : IRibbonExtensibility
	{
		private IRibbonUI ribbon;
		bool PrevLoginState = false;
        static Logger logger = LogManager.GetCurrentClassLogger();

		public void Ribbon_Load(IRibbonUI ribbonUI)
		{
			this.ribbon = ribbonUI;
		}
                
		public void saveTo_SpiderDocs(IRibbonControl control)
		{
            try
            {
			    if(SpiderDocsApplication.IsLoggedIn)
			    {
				    if(!PrevLoginState)
					    SpiderDocsApplication.LoadAllSettings();

				    //get selected attachment
				    AttachmentSelection attachmentSelection = control.Context as AttachmentSelection;

				    //get an available name
				    string filePath = AddInModules.FileFolder.GetAvailableFileName(AddInModules.FileFolder.GetTempFolder() + attachmentSelection[1].FileName);

				    //save doc in hard disk (temp folder)
				    attachmentSelection[1].SaveAsFile(filePath);

				    //show form
				    frmSaveDocExternal frm;
				    frm = new frmSaveDocExternal(filePath, DocumentSaveButtons_FormMode.AddIn);
				    frm.ShowDialog();

				    PrevLoginState = true;

			    }else
			    {
				    MessageBox.Show(lib.msg_error_SpiderDoc_not_open, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				    PrevLoginState = false;
			    }
            }
            catch (System.Exception e)
            {
                logger.Error(e);
            }
		}

		public string GetCustomUI(string ribbonID)
		{
			return GetResourceText("AddInOutlookAttachment2013.RibbonAttachment.xml");
		}

		private static string GetResourceText(string resourceName)
		{
			Assembly asm = Assembly.GetExecutingAssembly();
			string[] resourceNames = asm.GetManifestResourceNames();

			for(int i = 0; i < resourceNames.Length; ++i)
			{
				if(string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
				{
					using(StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
					{
						if(resourceReader != null)
							return resourceReader.ReadToEnd();
					}
				}
			}

			return null;
		}

		public Bitmap GetImage(IRibbonControl control)
	    {
			return Properties.Resources.menu.ToBitmap();
		}

	}
}
