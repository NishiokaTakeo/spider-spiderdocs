using System;
using Microsoft.Office.Interop.PowerPoint;

namespace AddInPowerPoint2013
{
	public partial class ThisAddIn
	{
		private void ThisAddIn_Startup(object sender, System.EventArgs e)
		{
			Application.WindowActivate += new EApplication_WindowActivateEventHandler(Application_WindowActivate);      
			Application.PresentationBeforeClose += new EApplication_PresentationBeforeCloseEventHandler(Application_PresentationBeforeClose); 
		}

		private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
		{
		}

		void Application_WindowActivate(Presentation Pres, DocumentWindow Wn)
		{
			Globals.Ribbons.Ribbon1.SetMenuEnabled();
		}

		void Application_PresentationBeforeClose(Presentation Pres, ref bool Cancel)
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
			this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
		}
		
		#endregion
	}
}
