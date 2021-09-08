using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using SpiderDocsForms;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;
using NLog;

namespace SpiderDocs
{
	public partial class frmGroupUser : Form
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

		int idGroup;
		DTS_Group DA_Group = new DTS_Group();

		public frmGroupUser()
		{
			InitializeComponent();

			//populate grid groups
			groupBindingSource.DataSource = DA_Group.GetDataTable();
		}

		private void frmGroupUser_Load(object sender, EventArgs e)
		{
			try
			{
				//populate grid groups
				populateUserList();

				lblGroups.Text  = "Groups\\Departments (" + dtgGroups.RowCount.ToString() + ")";
				lblGroups.Refresh();
                dtgGroups.ReadOnly = false;


            }
			catch(Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default_open_form, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
			}
		}

		private void populateUserList()
		{
			try
			{
				List<User> users = UserController.GetUser(true, false);
				List<int> ids = GroupController.GetUserIdInGroup(true, getIdGroup());

				User[] group_users = users.Where(a => ids.Contains(a.id)).ToArray();
				User[] not_group_users = users.Where(a => !ids.Contains(a.id)).ToArray();

				dtgUsers.Rows.Clear();

				foreach(User user in group_users)
					dtgUsers.Rows.Add(imageList1.Images[1], user.id, user.name, true);

				foreach(User user in not_group_users)
					dtgUsers.Rows.Add(imageList1.Images[1], user.id, user.name, false);

				lblUsers.Text = "Users (" + dtgUsers.RowCount.ToString() + ")";

			}
			catch(Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			foreach(DataGridViewRow linha in dtgGroups.Rows)
				linha.Cells[0].Value = imageList1.Images[0];
		}

		private void dtgGroups_SelectionChanged(object sender, EventArgs e)
		{
			idGroup = Convert.ToInt32(dtgGroups.Rows[dtgGroups.CurrentRow.Index].Cells[1].Value);
			populateUserList();          
		}

		private int getIdGroup()
		{
			int data = 0;
			if(dtgGroups.RowCount > 0)
			{
				int Id = Convert.ToInt32(dtgGroups.Rows[dtgGroups.CurrentRow.Index].Cells[1].Value);
				data = Id;
			}

			if(data == 1)
				dtgUsers.Enabled = false;
			else
				dtgUsers.Enabled = true;

			return data;
		}

		private void dtgPermission_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if(e.ColumnIndex == 3)
				{
					int idUser = Convert.ToInt32(dtgUsers.Rows[e.RowIndex].Cells[1].Value);
					dtgUsers.EndEdit();

					if((bool)(dtgUsers[3, e.RowIndex].Value) == false)
						GroupController.DeleteUserGroup(idGroup, idUser);
					else
						GroupController.AssignGroup(idGroup, idUser);
				}
			}
			catch(Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void pbUncheckAll_Click(object sender, EventArgs e)
		{
			if(idGroup == 1)
				return;

			dtgUsers.EndEdit();
			foreach(DataGridViewRow row in dtgUsers.Rows)
			{
				row.Cells[3].Value = false;
				GroupController.DeleteUserGroup(idGroup, Convert.ToInt32(row.Cells[1].Value));
			}
		}

		private void pbCheckAll_Click(object sender, EventArgs e)
		{
			if(idGroup == 1)
				return;

			dtgUsers.EndEdit();
			foreach(DataGridViewRow row in dtgUsers.Rows)
			{
				if((bool)(row.Cells[3].Value) == false)
				{
					row.Cells[3].Value = true;
					GroupController.AssignGroup(idGroup, Convert.ToInt32(row.Cells[1].Value));
				}
			}
		}
	}
}
