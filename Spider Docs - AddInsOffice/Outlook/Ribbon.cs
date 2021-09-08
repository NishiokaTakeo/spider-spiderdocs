using System;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Tools.Ribbon;
using NLog;

//---------------------------------------------------------------------------------
namespace AddInOutlook2013
{
	public partial class Ribbon : SpiderDocsOutlookRibbon
	{
//---------------------------------------------------------------------------------
		public Ribbon()
		{
			InitializeComponent();
			initialize(menuSaveSpiderRead, buttonSaveWorkspaceRead);
		}

//---------------------------------------------------------------------------------
		override public void SetMenuEnabled()
		{
            try
            {
                string subject = "";
                Explorer expolere = Globals.ThisAddIn.Application.ActiveExplorer();

                if(expolere != null)
                {
                    Selection selection = null;
                    try { selection = expolere.Selection; } catch {}

                    if((selection != null) && (selection.Count > 0))
                    {
                        object selectedItem = selection[1]; // Index is one-based.
                        MailItem mailItem = selectedItem as MailItem;

                        if(mailItem != null)
                            subject = mailItem.Subject;
                    }
                }

                SetMenuEnabled(subject, menuSaveSpider, btnSaveWorkSpace);

            }catch(System.Exception ex)
            {
                logger.Error(ex);
            }
		}

//---------------------------------------------------------------------------------
		private void btnSaveToDatabse_Click(object sender, RibbonControlEventArgs e)
		{
			SaveToSpiderDocs(e.Control.Context, false);
		}

//---------------------------------------------------------------------------------
		private void btnSaveWorkSpace_Click(object sender, RibbonControlEventArgs e)
		{
            SaveToWorkSpace(e.Control.Context);
		}

//---------------------------------------------------------------------------------
	}
}
