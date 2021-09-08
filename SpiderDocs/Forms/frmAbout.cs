using System;
using System.Windows.Forms;
using System.Reflection;

namespace SpiderDocs
{
	public partial class frmAbout : Spider.Forms.FormBase
	{
		public frmAbout()
		{
			InitializeComponent();
		}

		private void About_Load(object sender, EventArgs e)
		{
			lblVersion.Text = "Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(0, 5);
			lblComputerName.Text = "Computer Name: " + System.Environment.MachineName;
		}
	}
}
