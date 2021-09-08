using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SpiderDocsForms;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;
using NLog;

namespace SpiderDocs
{
	public partial class frmGroup : Form
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

		DTS_Group DA_Group = new DTS_Group();
		
		public frmGroup()
		{
			InitializeComponent();

			groupBindingSource.DataSource = DA_Group.GetDataTable();
		}

		private void frmGroup_Load(object sender, EventArgs e)
		{
			if(groupBindingSource.Count == 0)
			{
				btnSave.Enabled = false;
				btnDelete.Enabled = false;
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			txtGroupName.Focus();
			btnAdd.Enabled = false;
			btnDelete.Enabled = true;
			btnSave.Enabled = true;
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if(!btnAdd.Enabled)
				{
					groupBindingSource.CancelEdit();

					btnAdd.Enabled = true;
					btnDelete.Enabled = false;
					btnSave.Enabled = false;
					return;
				}

				int id_group = Convert.ToInt32(dtgGroup.Rows[dtgGroup.CurrentRow.Index].Cells[0].Value);

				if (((DataRowView)groupBindingSource.Current).IsNew == false)
				{
					if (0 < GroupController.GetUserIdInGroup(false, id_group).Count)
					{
						MessageBox.Show("This group already has registered users. \n First you must remove all users.", "Spider Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);

					}
					else
					{
						if (MessageBox.Show("Are you sure you want to delete the current record?", "Spider Docs", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
							deleteGroup();
					}

				}
				else
				{
					deleteGroup();
				}

			}
			catch (Exception error)
			{
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				logger.Error(error);
			}
			finally {

			}
		}

		private void deleteGroup()
		{
			int idx = Utilities.ConvDataRowId(((DataRowView)groupBindingSource.Current).Row.ItemArray[0]);

			groupBindingSource.RemoveCurrent();
			Validate();
			groupBindingSource.EndEdit();
			DA_Group.Delete(idx);
			btnAdd.Enabled = true;

			if(dtgGroup.RowCount == 0)
			{
				btnSave.Enabled = false;
				btnDelete.Enabled = false;
			}
		}

		private bool validation()
		{
			if(txtGroupName.Text == "")
			{
				txtGroupName.BackColor = Color.LavenderBlush;
				txtGroupName.Focus();
				MessageBox.Show(lib.msg_required_name, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;

			}else
			{
				txtGroupName.BackColor = Color.White;
			}

			if (txtGroupComments.Text == "")
			{
				txtGroupComments.BackColor = Color.LavenderBlush;
				txtGroupComments.Focus();
				MessageBox.Show(lib.msg_required_name, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;

			}
			else
			{
				txtGroupComments.BackColor = Color.White;
			}

			return true;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if(!validation())
				{
					//groupBindingSource.CancelEdit();
					groupBindingSource.EndEdit();
					return;
				}

				Validate();
				groupBindingSource.EndEdit();

				int idx = Utilities.ConvDataRowId(((DataRowView)groupBindingSource.Current).Row.ItemArray[0]);
				if(0 < idx)
				{
					DA_Group.Update((DataRowView)groupBindingSource.Current);

				}else
				{
					DA_Group.Insert((DataRowView)groupBindingSource.Current);
					groupBindingSource.MoveFirst();
				}
				
				groupBindingSource.DataSource = DA_Group.GetDataTable();
				btnAdd.Enabled = true;
			}
			catch(Exception error)
			{
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				logger.Error(error);
			}
		}

		private void dtgGroup_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			foreach(DataGridViewRow rows in dtgGroup.Rows)
			{
				if((rows.Cells[0].Value != DBNull.Value) && (Convert.ToInt32(rows.Cells[0].Value) == 1))
				{
					rows.DefaultCellStyle.BackColor = Color.WhiteSmoke;
					rows.DefaultCellStyle.ForeColor = Color.Black;
				}
			}
		}

		private void dtgGroup_SelectionChanged(object sender, EventArgs e)
		{
			if(((DataRowView)groupBindingSource.Current != null))
			{
				if(Utilities.ConvDataRowId(((DataRowView)groupBindingSource.Current).Row.ItemArray[0]) == 1)
				{
					btnDelete.Enabled = false;
					btnSave.Enabled = false;

				}else
				{
					btnDelete.Enabled = true;
					btnSave.Enabled = true;
				}
			}

			//remove pending chances
			if(((DataRowView)groupBindingSource.Current != null))
			{
				if(((DataRowView)groupBindingSource.Current).IsNew == false)
				{
					foreach(DataRowView rowItem in groupBindingSource)
					{
						if((rowItem.Row.ItemArray[0] == DBNull.Value) || (Convert.ToInt32(rowItem.Row.ItemArray[0]) < 0))
						{
							rowItem.Delete();
							btnAdd.Enabled = true;
						}
					}
				}
			}
		}

		private void txtGroupName_Validating(object sender, CancelEventArgs e)
		{
			if(((DataRowView)groupBindingSource.Current != null) 
			&& (Utilities.ConvDataRowId(((DataRowView)groupBindingSource.Current).Row.ItemArray[0]) > 0))
			{
				if(validation() == false)
				{
					groupBindingSource.CancelEdit();
					return;
				}
			}
		}
	}
}
