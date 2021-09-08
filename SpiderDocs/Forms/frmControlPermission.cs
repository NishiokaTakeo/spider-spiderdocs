using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;
using SpiderDocsForms;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;
using NLog;

namespace SpiderDocs
{
	public partial class frmControlPermission : Form
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

		DTS_System_permission_level DA_System_permission_level = new DTS_System_permission_level();
		DTS_User DA_User = new DTS_User(PermissionController.ADMIN_ID);

		bool FormLoaded = false;
		bool bPopulating = false;

		public frmControlPermission()
		{
			InitializeComponent();

			system_permission_levelBindingSource.DataSource = DA_System_permission_level.GetDataTable();
			userBindingSource.DataSource = DA_User.GetDataTable();
		}

		private void frmControlPermission_Load(object sender, EventArgs e)
		{
			try
			{
				dtgLevelPermission.SelectionChanged += new System.EventHandler(dtgLevelPermission_SelectionChanged);
				if(system_permission_levelBindingSource.Count == 0) { btnSave.Enabled = false; btnDelete.Enabled = false; }

				FormLoaded = true;
				TreeViewPermission();

			}
			catch(Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default_open_form, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
			}
		}

		private int GetIdPermission()
		{
			int data = 0;

			if(FormLoaded && (dtgLevelPermission.RowCount > 0))
			{
				int Id = Convert.ToInt32(dtgLevelPermission.Rows[dtgLevelPermission.CurrentRow.Index].Cells[0].Value);
				data = Id;
			}

			return data;
		}

		private void TreeViewPermission()
		{
			try
			{
				bPopulating = false;

				int IdPermission = GetIdPermission();
				List<int> MainIds = PermissionController.GetMenuPermission(en_MenuPermissionMode.Main, IdPermission);
				List<int> SubIds = PermissionController.GetMenuPermission(en_MenuPermissionMode.Sub, IdPermission);
				List<TopMenuItem> menus = TopMenuItemController.GetMenu();

				TreeView[] treeviews =
				{
					treeView1,
					treeView2,
					treeView3,
					treeView4,
					treeView5
				};

				// Create columns for the items.
				int i = 0;
				foreach(TreeView tv in treeviews)
				{
					TopMenuItem menu = menus[i];

					tv.Nodes.Clear();
					tv.CheckBoxes = true;

					tv.Nodes.Add(menu.id.ToString(), menu.name);
					tv.Nodes[0].Checked = MainIds.Contains(menu.id);

					foreach(TopMenuItem SubItems in menu.SubItems)
					{
						TreeNode node = tv.Nodes[0].Nodes.Add(SubItems.id.ToString(), SubItems.name);

						if(FormLoaded)
							node.Checked = SubIds.Contains(SubItems.id);
					}

					tv.ExpandAll();
					i++;
				}

				bPopulating = true;

			}
			catch(Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
		{
			try
			{
				if(FormLoaded && bPopulating)
				{
					if(e.Node.Level == 0)
					{
						if(e.Node.Checked)
							PermissionController.AddMenuPermission(en_MenuPermissionMode.Main, GetIdPermission(), Convert.ToInt32(e.Node.Name));
						else
							PermissionController.DeleteMenuPermission(en_MenuPermissionMode.Main, GetIdPermission(), Convert.ToInt32(e.Node.Name));

					}else
					{
						if(e.Node.Checked)
							PermissionController.AddMenuPermission(en_MenuPermissionMode.Sub, GetIdPermission(), Convert.ToInt32(e.Node.Name));
						else
							PermissionController.DeleteMenuPermission(en_MenuPermissionMode.Sub, GetIdPermission(), Convert.ToInt32(e.Node.Name));
					}
				}
			}
			catch(Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			txtLevelName.Focus();
			btnAdd.Enabled = false;
			btnDelete.Enabled = true;
			btnSave.Enabled = true;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if(!validation())
				{
					system_permission_levelBindingSource.CancelEdit();
					return;
				}

				Validate();
				system_permission_levelBindingSource.EndEdit();

				int idx = Utilities.ConvDataRowId(((DataRowView)system_permission_levelBindingSource.Current).Row.ItemArray[0]);
				if(0 < idx)
					DA_System_permission_level.Update((DataRowView)system_permission_levelBindingSource.Current);
				else
					DA_System_permission_level.Insert((DataRowView)system_permission_levelBindingSource.Current);

				btnAdd.Enabled = true;

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
				if(((DataRowView)system_permission_levelBindingSource.Current).IsNew == false)
				{
					int id_permission = Convert.ToInt32(dtgLevelPermission.Rows[dtgLevelPermission.CurrentRow.Index].Cells[0].Value);
					bool hasFiles = PermissionController.IsMenuPermissionUsed(id_permission);

					if(hasFiles)
					{
						MessageBox.Show(lib.msg_permission_group_used, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}

					if(MessageBox.Show(lib.msg_ask_delete_record, lib.msg_messabox_title, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
					{
						deleteLevelPermission();
					}

				}else
				{
					deleteLevelPermission();
				}
			}
			catch(Exception error)
			{
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				logger.Error(error);
			}
		}

		private void deleteLevelPermission()
		{
			try
			{
				int idx = Utilities.ConvDataRowId(((DataRowView)system_permission_levelBindingSource.Current).Row.ItemArray[0]);

				system_permission_levelBindingSource.RemoveCurrent();
				Validate();
				system_permission_levelBindingSource.EndEdit();

				if(0 < idx)
					DA_System_permission_level.Delete(idx);

				btnAdd.Enabled = true;

				if(dtgLevelPermission.RowCount == 0)
				{
					btnSave.Enabled = false;
					btnDelete.Enabled = false;
				}
			}
			catch(Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void dtgLevelPermission_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			foreach(DataGridViewRow rows in dtgLevelPermission.Rows)
			{
				if((rows.Cells[0].Value != DBNull.Value) && (Convert.ToInt32(rows.Cells[0].Value) == 1))
				{
					rows.DefaultCellStyle.BackColor = Color.WhiteSmoke;
					rows.DefaultCellStyle.ForeColor = Color.Black;
				}
			}
		}

		private void dtgLevelPermission_SelectionChanged(object sender, EventArgs e)
		{
			try
			{
				DA_User.Select(GetIdPermission());
				TreeViewPermission();

				if(Convert.ToInt32(dtgLevelPermission.Rows[dtgLevelPermission.CurrentRow.Index].Cells[0].Value) == 1)
				{
					btnDelete.Enabled = false;
					btnSave.Enabled = false;
					treeView1.Enabled = false;
					treeView2.Enabled = false;
					treeView3.Enabled = false;
					treeView4.Enabled = false;
					treeView5.Enabled = false;

				}else
				{
					btnDelete.Enabled = true;
					btnSave.Enabled = true;
					treeView1.Enabled = true;
					treeView2.Enabled = true;
					treeView3.Enabled = true;
					treeView4.Enabled = true;
					treeView5.Enabled = true;
				}

				//remove pending chances
				if(((DataRowView)system_permission_levelBindingSource.Current != null))
				{
					if(((DataRowView)system_permission_levelBindingSource.Current).IsNew == false)
					{
						foreach(DataRowView rowItem in system_permission_levelBindingSource)
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
			catch{}
		}

		private bool validation()
		{
			if(txtLevelName.Text == "")
			{
				txtLevelName.BackColor = Color.LavenderBlush;
				txtLevelName.Focus();
				MessageBox.Show(lib.msg_required_name, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;

			}else
			{
				txtLevelName.BackColor = Color.White;
			}

			return true;
		}

		private void txtLevelName_Validating(object sender, CancelEventArgs e)
		{
			if(Utilities.ConvDataRowId(((DataRowView)system_permission_levelBindingSource.Current).Row.ItemArray[0]) > 0)
			{
				if(validation() == false)
				{
					system_permission_levelBindingSource.CancelEdit();
					return;
				}
			}
		}
	}
}
