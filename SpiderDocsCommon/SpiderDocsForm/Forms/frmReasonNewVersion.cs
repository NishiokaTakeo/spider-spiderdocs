using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Spider.Drawing;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;

namespace SpiderDocsForms
{
	public partial class frmReasonNewVersion : Spider.Forms.FormBase
	{
		public string reason = "";
		public bool result = false;
		IconManager icon = new IconManager();

		public frmReasonNewVersion(Document doc)
		{
			build(new List<Document> { doc });
		}

		public frmReasonNewVersion(List<Document> docs)
		{
			build(docs);
		}

		void build(List<Document> docs)
		{
			InitializeComponent();

			if(docs.Count == 1)
			{
				this.lblId.Text = docs[0].id.ToString();
				this.lblName.Text = docs[0].title;
				this.lblVersion.Text = (docs[0].version + 1).ToString();

				icon.GetLargeIcon(docs[0].extension);
				this.pboxAppIcon.Image = icon.GetLargeIcon(docs[0].extension);

			}else
			{
				this.lblNameTitle.Text = String.Format(lib.msg_multiple_files_checkin, docs.Count.ToString());
					
				this.lblId.Visible = false;
				this.lblIdTitle.Visible = false;

				this.lblName.Visible = false;
						
				this.lblVersion.Visible = false;
				this.lblVersionTitle.Visible = false; 

				this.pboxAppIcon.Visible = false;
			}
		}

//---------------------------------------------------------------------------------
		private void btnSave_Click(object sender, EventArgs e)
		{
			if((txtReason.TextLength < 10) && (txtReason.Enabled == true))
			{
				txtReason.BackColor = Color.LavenderBlush;
				txtReason.Focus();
				MessageBox.Show("Please, enter the reason.", "Spider Docs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

			}else
			{
				reason = this.txtReason.Text;
				result = true;
				Close();
			}
		}

//---------------------------------------------------------------------------------
		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

//---------------------------------------------------------------------------------
	}
}
