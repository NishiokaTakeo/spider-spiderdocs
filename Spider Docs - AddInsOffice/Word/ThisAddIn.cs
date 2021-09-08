using System;
using Microsoft.Office.Interop.Word;

namespace AddInWord2013
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            Application.WindowActivate += new ApplicationEvents4_WindowActivateEventHandler(Application_WindowActivate);      
			Application.DocumentBeforeClose += new ApplicationEvents4_DocumentBeforeCloseEventHandler(Application_DocumentBeforeClose);   

            Globals.Ribbons.Ribbon.timerCheckSystemStatus.Start();
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        void Application_WindowActivate(Document Doc, Window Wn)
        {
			Globals.Ribbons.Ribbon.SetMenuEnabled();
		}

        void Application_DocumentBeforeClose(Document Doc, ref bool Cancel)
        {
		}
		
        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            //this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
