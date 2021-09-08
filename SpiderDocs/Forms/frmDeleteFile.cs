using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SpiderDocsModule;
using SpiderDocsForms;
using Document = SpiderDocsForms.Document;
using lib = SpiderDocsModule.Library;
using NLog;

namespace SpiderDocs
{
	public partial class frmDeleteFile : Spider.Forms.FormBase
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

		class FileList
		{
			public Document doc = new Document();
			public int rowidx;
		}

		List<FileList> m_FileList = new List<FileList>();
		public Form parent;

		public frmDeleteFile()
		{
			InitializeComponent();
		}

		public void AddDeleteFile(Document doc, int rowidx)
		{
			FileList wrk = new FileList();

			wrk.doc = doc;
			wrk.rowidx = rowidx;

			m_FileList.Add(wrk);
		}

		private void frmDeleteFile_Load(object sender, EventArgs e)
		{
			// Delete single file.
			if(m_FileList.Count == 1)
			{
				lblId.Text = m_FileList[0].doc.id.ToString();
				lblName.Text = m_FileList[0].doc.title;

			// Delete multiple file.
			}else
			{
				lblId_Title.Visible = false;
				lblName_Title.Visible = false;
				lblId.Visible = false;
				lblName.Visible = false;
				lblMsg.Text = String.Format(lib.msg_ask_delete_files, m_FileList.Count.ToString());
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if(txtReason.TextLength < SpiderDocsApplication.CurrentPublicSettings.delete_reason_length)
				{
					txtReason.BackColor = Color.LavenderBlush;
					txtReason.Focus();


                    MessageBox.Show(new Form() { TopMost = true }, "Please, write the reason.", lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1 ,MessageBoxOptions.DefaultDesktopOnly);

                    //MessageBox.Show("Please, write the reason.", lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				((frmWorkSpace)parent).dtgBdFiles.SuspendLayout();	// Stop drawing during deleting.

				foreach(FileList wrk in m_FileList)
				{
					if((wrk.doc.id_sp_status == en_file_Sp_Status.review)
					|| (wrk.doc.id_sp_status == en_file_Sp_Status.review_overdue))
					{
						wrk.doc.id_event = EventIdController.GetEventId(en_Events.FinalizeReview);
						wrk.doc.id_user = SpiderDocsApplication.CurrentUserId;

						Review review = ReviewController.GetReview(wrk.doc.id);
						review.FinalizeReview();
					}

					//change file status as deleted
					DocumentController.DeleteDocument(wrk.doc.id, txtReason.Text);

					//delete raws
					((frmWorkSpace)parent).dtgBdFiles.Rows.RemoveAt(wrk.rowidx);
				}

				// Select top row.
				if(((frmWorkSpace)parent).dtgBdFiles.Rows.Count > 0)
				{
					((frmWorkSpace)parent).dtgBdFiles.ClearSelection();
					((frmWorkSpace)parent).dtgBdFiles.CurrentCell = ((frmWorkSpace)parent).dtgBdFiles.Rows[0].Cells[0];
					((frmWorkSpace)parent).dtgBdFiles.Rows[0].Selected = true;
				}

				((frmWorkSpace)parent).dtgBdFiles.ResumeLayout();	// Resume drawing.

				//message
                MessageBox.Show("The document has been deleted.", lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1 ,MessageBoxOptions.DefaultDesktopOnly);

                Close();
			}
			catch(System.Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
