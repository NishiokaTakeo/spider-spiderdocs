using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SpiderDocsModule;
using SpiderDocsForms;
using lib = SpiderDocsModule.Library;
using NLog;
namespace SpiderDocs
{
	public partial class frmFolder : Form
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

		bool deleteAction;
		DTS_Folder DataAdapter = new DTS_Folder(false, false);

		public frmFolder()
		{
			InitializeComponent();
			document_folderBindingSource.DataSource = DataAdapter.GetDataTable();
		}

		private void frmFolder_Load(object sender, EventArgs e)
		{
			if(document_folderBindingSource.Count == 0) { btnSave.Enabled = false; btnDelete.Enabled = false; }
		}

		private void document_folderDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			foreach(DataGridViewRow linha in dtgFolder.Rows)
				linha.Cells[0].Value = imageList1.Images[0];
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			txtFolderName.Focus();
			btnAdd.Enabled = false;
			btnDelete.Enabled = true;
			btnSave.Enabled = true;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if(txtFolderName.Text == "")
				{
					txtFolderName.BackColor = Color.LavenderBlush;
					txtFolderName.Focus();
					MessageBox.Show(lib.msg_required_folder_name, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				else
				{
					txtFolderName.BackColor = Color.White;
				}

				int idx = Utilities.ConvDataRowId(((DataRowView)document_folderBindingSource.Current).Row.ItemArray[0]);

				//check unique folder name
				if((idx <= 0) && !FolderController.IsUniqueFolderName(txtFolderName.Text))
				{
					MessageBox.Show(lib.msg_existing_folder, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				Validate();
				document_folderBindingSource.EndEdit();
				if(0 < idx)
					DataAdapter.Update(idx, txtFolderName.Text);
				else
					DataAdapter.Insert(txtFolderName.Text);

				btnAdd.Enabled = true;

				Utilities.refreshAllComboFolders();
			}
			catch(Exception error)
			{
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				logger.Error(error);
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				deleteAction = true;

				if(((DataRowView)document_folderBindingSource.Current).IsNew == false)
				{
					int id_folder = Convert.ToInt32(dtgFolder.Rows[dtgFolder.CurrentRow.Index].Cells[1].Value);

					if(0 < FolderController.GetDocIds(id_folder).Count)
					{
						MessageBox.Show(lib.msg_folder_used, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

					}else if(MessageBox.Show(lib.msg_ask_delete_record, lib.msg_messabox_title, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
					{
						deleteFolder(id_folder);
						Utilities.refreshAllComboFolders();
					}

				}else
				{
					deleteFolder(-1);
				}
			}
			catch(Exception error)
			{
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				logger.Error(error);
			}
		}

		private void deleteFolder(int idx)
		{
			document_folderBindingSource.RemoveCurrent();
			Validate();
			document_folderBindingSource.EndEdit();

			if(0 < idx)
				DataAdapter.Delete(idx);

			dtgFolder.Enabled = true;

			if(dtgFolder.RowCount == 0)
			{
				btnSave.Enabled = false;
				btnDelete.Enabled = false;
			}
		}

		private void txtFolderName_Validating(object sender, CancelEventArgs e)
		{
			if(txtFolderName.Text == "")
			{
				if((DataRowView)document_folderBindingSource.Current != null)
				{
					int idx = Utilities.ConvDataRowId(((DataRowView)document_folderBindingSource.Current).Row.ItemArray[0]);              
					if(idx > 0)
					{
						txtFolderName.Focus();
						txtFolderName.BackColor = Color.LavenderBlush;
						MessageBox.Show(lib.msg_required_folder_name, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}

				document_folderBindingSource.CancelEdit();
				return;
			}
			else
			{
				//check unique folder name
				if(deleteAction == false)
				{
					if((DataRowView)document_folderBindingSource.Current != null)
					{
						int idx = Utilities.ConvDataRowId(((DataRowView)document_folderBindingSource.Current).Row.ItemArray[0]);
						if((idx <= 0) && !FolderController.IsUniqueFolderName(txtFolderName.Text))
						{
							MessageBox.Show(lib.msg_existing_folder, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
							txtFolderName.BackColor = Color.LavenderBlush;
							document_folderBindingSource.CancelEdit();
							e.Cancel = true;
							return;
						}
					}
				}           
			}

			deleteAction = false;
			txtFolderName.BackColor = Color.White;
		}

		private void dtgFolder_SelectionChanged(object sender, EventArgs e)
		{
			if(btnAdd.Enabled == false)
				btnAdd.Enabled = true;
		}
	}
}
