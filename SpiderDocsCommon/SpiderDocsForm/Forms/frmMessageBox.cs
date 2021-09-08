using System;
using System.Windows.Forms;
using Spider.Common;
using SpiderDocsModule;

namespace SpiderDocsForms
{
	public partial class frmMessageBox : Spider.Forms.FormBase
	{
		Document doc;
			 
		public frmMessageBox(Document target)
		{
			InitializeComponent();

			doc = target;
		}

		private void frmMessageBox_Load(object sender, EventArgs e)
		{
			History log = HistoryController.GetLatestHistory(doc.id_version);
			
			lblUser.Text = UserController.GetUser(true, log.id_user).name;
			lblDate.Text = log.date.ToString(ConstData.DATE_TIME);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if(ckNotify.AutoCheck)
				DocumentController.AddDocumentTracked(doc.id, SpiderDocsApplication.CurrentUserId);

			Close();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			if(ckNotify.AutoCheck)
				DocumentController.AddDocumentTracked(doc.id, SpiderDocsApplication.CurrentUserId);

			doc.Open();

			Close();
		}
	}
}
