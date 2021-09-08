using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SpiderDocsForms;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;
using NLog;
//---------------------------------------------------------------------------------
namespace SpiderDocs
{
	public partial class frmFolderPermissions : Form
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

		List<Group> AssignedGroups = new List<Group>();
		List<User> AssignedUsers = new List<User>();

		bool execRoutine = false;
		const int ColIdx_ChkAllow = 1;
		const int ColIdx_ChkDeny = 2;
		const int ColIdx_PermissionId = 3;

		SpiderDocsForms.DatagridViewCheckBoxHeaderCell cbHeaderAllow;
		SpiderDocsForms.DatagridViewCheckBoxHeaderCell cbHeaderDeny;

		//DTS_Folder DA_Folder = new DTS_Folder(false, false);

//---------------------------------------------------------------------------------
		int lvGroup_id
		{
			get
			{
				if(0 < lvGroup.SelectedItems.Count)
					return ((Group)lvGroup.SelectedItems[0].Tag).id;
				else
					return -1;
			}
		}

		int lvUsersOfGroup_id
		{
			get
			{
				if(0 < lvUsersOfGroup.SelectedItems.Count)
					return ((User)lvUsersOfGroup.SelectedItems[0].Tag).id;
				else
					return -1;
			}
		}

//---------------------------------------------------------------------------------
		public frmFolderPermissions()
		{
			InitializeComponent();
			dtgPermission.AutoGenerateColumns = false;
            cboFolder.UseDataSource4AssignedMe(permission: en_Actions.None);
            if(cboFolder.Items.Count > 0) cboFolder.SelectedValue = (cboFolder.Items[0] as Folder).id;

			cbHeaderAllow = new SpiderDocsForms.DatagridViewCheckBoxHeaderCell(true, false);
			cbHeaderDeny = new SpiderDocsForms.DatagridViewCheckBoxHeaderCell(true, false);
			cbHeaderAllow.Value = "Allow";
			cbHeaderDeny.Value = "Deny";

			cbHeaderAllow.Enabled = false;
			cbHeaderDeny.Enabled = false;

			cbHeaderAllow.Align = HorizontalAlignment.Right;
			cbHeaderDeny.Align = HorizontalAlignment.Right;

			cbHeaderAllow.OnCheckBoxClicked += new SpiderDocsForms.CheckBoxClickedHandler(cbAllAllow_Click);
			cbHeaderDeny.OnCheckBoxClicked += new SpiderDocsForms.CheckBoxClickedHandler(cbAllDeny_Click);

			dtgPermission.Columns[ColIdx_ChkAllow].HeaderCell = cbHeaderAllow;
			dtgPermission.Columns[ColIdx_ChkDeny].HeaderCell = cbHeaderDeny;

			((DataGridViewCheckBoxColumn)dtgPermission.Columns[ColIdx_ChkAllow]).TrueValue = true;
			((DataGridViewCheckBoxColumn)dtgPermission.Columns[ColIdx_ChkAllow]).FalseValue = false;

			((DataGridViewCheckBoxColumn)dtgPermission.Columns[ColIdx_ChkDeny]).TrueValue = true;
			((DataGridViewCheckBoxColumn)dtgPermission.Columns[ColIdx_ChkDeny]).FalseValue = false;
		}

//---------------------------------------------------------------------------------
		private void frmFolderPermissions_Load(object sender, EventArgs e)
		{
			try
			{
				populateComboFolder();
				populateGroupList();

				cbHeaderAllow.Checked = false;
				cbHeaderDeny.Checked = false;

				cbHeaderAllow.Enabled = false;
				cbHeaderDeny.Enabled = false;

				execRoutine = true;

                lblInheritanceDescription.Text = "";


				cboFolder_SelectionChangeCommitted();
			}
			catch(Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default_open_form, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
			}
		}

//---------------------------------------------------------------------------------
		private void cboFolder_SelectionChangeCommitted(object sender, EventArgs e)
		{
			cboFolder_SelectionChangeCommitted();
		}

		void cboFolder_SelectionChangeCommitted()
		{
			try
			{
                if(Convert.ToInt32(cboFolder.SelectedValue) == 0)
                {
                    AssignedGroups.Clear();
                    AssignedUsers.Clear();
                    lvGroupAndUser.Items.Clear();
                    return;
                }

                lvGroup.SelectedItems.Clear();
				lvUsersOfGroup.SelectedItems.Clear();
				clearDataGrid();

                int id_folder = Convert.ToInt32(cboFolder.SelectedValue);

                List<int> AssignedGroupsId = PermissionController.GetAssignedUserOrGroupToFolder(en_FolderPermissionMode.Group, id_folder);
				if(0 < AssignedGroupsId.Count)
					AssignedGroups = GroupController.GetGroups(AssignedGroupsId.ToArray());
				else
					AssignedGroups.Clear();

				List<int> AssignedUserId = PermissionController.GetAssignedUserOrGroupToFolder(en_FolderPermissionMode.User, id_folder);
				if(0 < AssignedUserId.Count)
					AssignedUsers = UserController.GetUser(false, false, AssignedUserId.ToArray());
				else
					AssignedUsers.Clear();

				populatePermissionList();

                // Let user knows this folder permission has been inherited or not.
                ShowInheritenceInfo(id_folder);
            }
			catch(Exception error)
			{
				logger.Error(error);
			}
		}

        /// <summary>
        /// Show permission inheritance information to UI.
        /// If the permission has upper parents then it is inheritance. The form provides enable/disable permission inheritance from parents.
        /// </summary>
        /// <param name="curFolderId">FolderID that current selected folder</param>
        void ShowInheritenceInfo(int curFolderId)
        {
            var curFolder = FolderController.GetFolder(curFolderId);
            if (curFolder.id == 0 || curFolder.id_parent == 0)
            {
                // Do nothing if selected folder is not found or root folder.

                EnableEditableCtrs();

                lblInheritanceDescription.Text = string.Empty;
                btnToggleInheritance.Enabled = false ;

                return;
            }

            btnToggleInheritance.Enabled = true;

            var inheritedFolder = PermissionController.GetInheritanceFolder(curFolderId);
            if (inheritedFolder.id == curFolderId)
            {
                // Selected folder is not inherited

                // Let user knows this folder is NOT inherited.
                lblInheritanceDescription.Text = string.Format("Permission has not inherited from any folders.");
                lblInheritanceDescription.BackColor = System.Drawing.Color.Transparent;
                lblInheritanceDescription.ForeColor = System.Drawing.Color.Black;
                btnToggleInheritance.Text = string.Format("Enable inheritance.");

                EnableEditableCtrs();
            }
            else
            {
                // Selected folder is inherited

                // Let user knows this folder is inherited.
                lblInheritanceDescription.Text = string.Format("Permission has inherited from a Folder ({0})", inheritedFolder.document_folder);
                lblInheritanceDescription.BackColor = System.Drawing.Color.MediumVioletRed;
                lblInheritanceDescription.ForeColor = System.Drawing.Color.White;
                btnToggleInheritance.Text = string.Format("Disable inheritance. (Copy)");

                DisableEditableCtrs();
            }
        }


        /// <summary>
        /// Disable controls for editing.
        /// Inherited folders' permission should be non-editable.
        /// </summary>
        void DisableEditableCtrs()
        {
            
            //lvGroupAndUser.Enabled = false;
            dtgPermission.Enabled = false;
            btnExcluir.Enabled = false;
            pictureBox2.Enabled = false;
            pictureBox3.Enabled = false;
            lvGroup.Enabled = false;
            lvUsersOfGroup.Enabled = false;
        }

        /// <summary>
        /// Enable controls for editing.
        /// Non inherited folders' permission should be editable.
        /// </summary>
        void EnableEditableCtrs()
        {
            
            //lvGroupAndUser.Enabled = true;
            dtgPermission.Enabled = true;
            btnExcluir.Enabled = true;
            pictureBox2.Enabled = true;
            pictureBox3.Enabled = true;
            lvGroup.Enabled = true;
            lvUsersOfGroup.Enabled = true;
        }

        //---------------------------------------------------------------------------------
        private void lvGroup_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if(!execRoutine)
				return;

			execRoutine = false;
			try
			{
				lvUsersOfGroup.SelectedItems.Clear();

				lvGroupAndUser.AllowReorder = true;
				Group item = (Group)e.Item.Tag;

				//check if user or group is already in the list
				foreach(ListViewItem eachItem in this.lvGroupAndUser.Items)
				{
					if((typeof(Group) == eachItem.Tag.GetType())
					&&	(item.id == ((Group)eachItem.Tag).id))
					{
						lvGroupAndUser.AllowReorder = false;
						break;
					}
				}

				lvUsersOfGroup.Items.Clear();
				populateUserOfGroupList(item.id);
			}
			catch(Exception error)
			{
				logger.Error(error);
			}
			execRoutine = true;
		}

//---------------------------------------------------------------------------------
		private void lvGroup_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			addGroupPermission();
			populatePermissionList();
		}

//---------------------------------------------------------------------------------
		private void lvUsersOfGroup_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if(!execRoutine)
				return;

			execRoutine = false;
			try
			{
				lvGroup.SelectedItems.Clear();

				lvGroupAndUser.AllowReorder = true;
				User item = (User)e.Item.Tag;

				foreach(ListViewItem eachItem in this.lvGroupAndUser.Items)
				{
					if((typeof(User) == eachItem.Tag.GetType())
					&&	(item.id == ((User)eachItem.Tag).id))
					{
						lvGroupAndUser.AllowReorder = false;
						break;
					}
				}
			}
			catch(Exception error)
			{
				logger.Error(error);
			}
			execRoutine = true;
		}

//---------------------------------------------------------------------------------
		private void lvUsersOfGroup_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			addGroupPermission();
			populatePermissionList();
		}

//---------------------------------------------------------------------------------
		private void lvGroupAndUser_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			try
			{
				if(0 < lvGroupAndUser.SelectedItems.Count)
					populatelvGroupAndUser(e.Item.Tag);
				else
				{
					dtgPermission.Rows.Clear();

					cbHeaderAllow.Checked = false;
					cbHeaderDeny.Checked = false;

					cbHeaderAllow.Enabled = false;
					cbHeaderDeny.Enabled = false;
				}
			}
			catch(Exception error)
			{
				logger.Error(error);
			}
		}

//---------------------------------------------------------------------------------
		private void lvGroupAndUser_DragDrop(object sender, DragEventArgs e)
		{
			addGroupPermission();
		}

//---------------------------------------------------------------------------------
		private void dtgPermission_CellMouseUp(object sender,DataGridViewCellMouseEventArgs e)
		{
			dtgPermission.EndEdit();

			UpdateCellData();
			cbHeaderAllow.ChkAllTick();
			cbHeaderDeny.ChkAllTick();
		}

		private void cbAllAllow_Click(bool check)
		{
			ChgAllTickPermission(ColIdx_ChkAllow, check);
		}

		private void cbAllDeny_Click(bool check)
		{
			ChgAllTickPermission(ColIdx_ChkDeny, check);
		}

		private void ChgAllTickPermission(int col, bool check)
		{
			int cnt = dtgPermission.Rows.Count;

			for(int i = 0; i < cnt; i++)
				dtgPermission.Rows[i].Cells[col].Value = check;

			UpdateCellData();
		}

        /// <summary>
        /// Update users permision for particular documents.</summary>
        /// <param name=""></param>
        /// <seealso cref="String">
        /// Tag represents id_user. </seealso>
		private void UpdateCellData()
		{
			try
			{
				en_FolderPermissionMode mode = en_FolderPermissionMode.Group;
				int id = -1;

				if(lvGroupAndUser.SelectedItems[0].Tag.GetType() == typeof(Group))
				{
					mode = en_FolderPermissionMode.Group;
					id = ((Group)lvGroupAndUser.SelectedItems[0].Tag).id;

				}else if(lvGroupAndUser.SelectedItems[0].Tag.GetType() == typeof(User))
				{
					mode = en_FolderPermissionMode.User;
					id = ((User)lvGroupAndUser.SelectedItems[0].Tag).id;
				}

                // true if id_user is .
				if(0 < id)
				{
					Dictionary<en_Actions, en_FolderPermission> permissions = new Dictionary<en_Actions, en_FolderPermission>();

                    // for each rows in the users.It will be all rows even if you choose one row on the GUI.
					foreach(DataGridViewRow row in dtgPermission.Rows)
					{
                        int action = int.Parse((string)row.Cells[ColIdx_PermissionId].Value);
                        en_FolderPermission permission = en_FolderPermission.NoSetting;

                        bool allow = (bool)row.Cells[ColIdx_ChkAllow].Value;
                        bool deny = (bool)row.Cells[ColIdx_ChkDeny].Value;

						if(allow && deny)
							permission = en_FolderPermission.Both;
						else if(deny)
							permission = en_FolderPermission.Deny;
						else if(allow)
							permission = en_FolderPermission.Allow;

                        permissions.Add((en_Actions)action, permission);
					}

					PermissionController.AddPermission(Convert.ToInt32(cboFolder.SelectedValue), id, mode, permissions);
				}
			}
			catch(Exception error)
			{
				 logger.Error(error);
			}
			finally
			{
                new Cache(SpiderDocsApplication.CurrentUserId).Remove(Cache.en_UKeys.DB_GetAssignedFolderToUser);
			}
		}

//---------------------------------------------------------------------------------
		private void btnExcluir_Click(object sender, EventArgs e)
		{
			try
			{
				if(lvGroupAndUser.SelectedItems.Count == 0)
					return;

                int id_folder = Convert.ToInt32(cboFolder.SelectedValue);

                // The root folder must have at least one permission.
                var curFolder = FolderController.GetFolder(id_folder);
                if (curFolder.id_parent == 0 && this.lvGroupAndUser.Items.Count <= 1)
                {
                    MessageBox.Show("The root folder must have one group/user setup at least.", "Spider Docs", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                var result = (MessageBox.Show("Are you sure you want to delete this group/user?", "Spider Docs", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question));

				if(result == DialogResult.Yes)
				{
					foreach(ListViewItem eachItem in this.lvGroupAndUser.SelectedItems)
					{
						//delete from base
						if(eachItem.Tag.GetType() == typeof(Group))
							PermissionController.DeleteAssignedFolder(en_FolderPermissionMode.Group, Convert.ToInt32(cboFolder.SelectedValue), ((Group)eachItem.Tag).id);
						else
							PermissionController.DeleteAssignedFolder(en_FolderPermissionMode.User, Convert.ToInt32(cboFolder.SelectedValue), ((User)eachItem.Tag).id);

						//delete from grid
						execRoutine = false;
						lvGroupAndUser.Items.RemoveAt(eachItem.Index);
						execRoutine = true;

						clearDataGrid();
					}

                    cboFolder_SelectionChangeCommitted();

                }
            }
			catch(Exception error)
			{
				 logger.Error(error);
			}
			finally
			{
                new Cache(SpiderDocsApplication.CurrentUserId).Remove(Cache.en_UKeys.DB_GetAssignedFolderToUser);
			}
		}

//---------------------------------------------------------------------------------
		/// <summary>
		/// Add Group Picture
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox2_Click(object sender, EventArgs e)
		{
			ListViewItem wrk;

			if(0 < lvGroup_id)
			{
				addGroupPermission();

				wrk = (ListViewItem)lvGroup.SelectedItems[0].Clone();
				InsertUserToPermission(wrk);

				ChgAllTickPermission(ColIdx_ChkAllow, false);
			}
		}

//---------------------------------------------------------------------------------
		private void pictureBox3_Click(object sender, EventArgs e)
		{
			ListViewItem wrk;

			if(0 < lvUsersOfGroup_id)
			{
				addGroupPermission();

				wrk = (ListViewItem)lvUsersOfGroup.SelectedItems[0].Clone();
				InsertUserToPermission(wrk);

				ChgAllTickPermission(ColIdx_ChkAllow, false);
			}
		}

//---------------------------------------------------------------------------------
// building contents of controls --------------------------------------------------
//---------------------------------------------------------------------------------
		public void populateComboFolder()
		{
			clearDataGrid();
			populatePermissionList();
		}

//---------------------------------------------------------------------------------
		private void populateGroupList()
		{
			try
			{
				ListViewItem item;
				List<Group> groups = GroupController.GetGroups();

				foreach(Group group in groups)
				{
					item = new ListViewItem("");
					item.SubItems.Add(group.group_name + " ( " + group.NoOfUsers + " )");
					item.Tag = group;
					item.ImageIndex = 0;

					lvGroup.Items.Add(item);
				}

				lvGroup.Items[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);

			}
			catch(Exception error)
			{
				 logger.Error(error);
			}
		}

//---------------------------------------------------------------------------------
		private void populateUserOfGroupList(int id)
		{
			try
			{
				ListViewItem item;
				List<int> ids = GroupController.GetUserIdInGroup(true, id);

				if(0 < ids.Count)
				{
					List<User> users = UserController.GetUser(true, false, ids.ToArray());

					foreach(User user in users)
					{
						item = new ListViewItem("");

						item.SubItems.Add(user.name);
						item.Tag = user;
						item.ImageIndex = 1;
						lvUsersOfGroup.Items.Add(item);
					}
				}
			}
			catch(Exception error)
			{
				 logger.Error(error);
			}
		}

//---------------------------------------------------------------------------------
		private void populatePermissionList()
		{
			lvGroupAndUser.Items.Clear();

			foreach(Group wrk in AssignedGroups)
			{
				ListViewItem item = new ListViewItem("");
				item.SubItems.Add(wrk.group_name);
				item.Tag = wrk;
				item.ImageIndex = 0;
				lvGroupAndUser.Items.Add(item);
			}

			foreach(User wrk in AssignedUsers)
			{
				ListViewItem item = new ListViewItem("");
				item.SubItems.Add(wrk.name);
				item.Tag = wrk;
				item.ImageIndex = 1;
				lvGroupAndUser.Items.Add(item);
			}
		}

//---------------------------------------------------------------------------------
		void populatelvGroupAndUser(object Tag)
		{
			if(execRoutine)
			{
				execRoutine = false;

				lvGroupAndUser.AllowReorder = false;

				int foler_id = Convert.ToInt32(cboFolder.SelectedValue);

				Dictionary<en_Actions, en_FolderPermission> permissions = new Dictionary<en_Actions, en_FolderPermission>();
				if(Tag.GetType() == typeof(Group))
					permissions = PermissionController.GetFolderPermission(foler_id, ((Group)Tag).id, en_FolderPermissionMode.Group, true);
				else
					permissions = PermissionController.GetFolderPermission(foler_id, ((User)Tag).id, en_FolderPermissionMode.User, true);

				dtgPermission.Rows.Clear();

				Dictionary<en_Actions, string> titles = PermissionController.GetFolderPermissionTitles();
				foreach(KeyValuePair<en_Actions, string> wrk in titles)
				{
					bool Allow = false;
					bool Deny = false;

					if(permissions.ContainsKey(wrk.Key))
					{
						switch(permissions[wrk.Key])
						{
						case en_FolderPermission.Both:
							Allow = true;
							Deny = true;
							break;

						case en_FolderPermission.Allow:
							Allow = true;
							break;

						case en_FolderPermission.Deny:
							Deny = true;
							break;
						}
					}

					dtgPermission.Rows.Add(titles[wrk.Key], Allow, Deny, ((int)wrk.Key).ToString());
				}

				cbHeaderAllow.ChkAllTick();
				cbHeaderDeny.ChkAllTick();

				execRoutine = true;
			}
		}

//---------------------------------------------------------------------------------
		private void addGroupPermission()
		{
			try
			{
				if(0 < lvGroup_id)
					PermissionController.AssignFolder(en_FolderPermissionMode.Group, Convert.ToInt32(cboFolder.SelectedValue), lvGroup_id);
				else
					PermissionController.AssignFolder(en_FolderPermissionMode.User, Convert.ToInt32(cboFolder.SelectedValue), lvUsersOfGroup_id);

				lvGroupAndUser.AllowReorder = false;

			}
			catch(Exception error)
			{
				logger.Error(error);
			}
		}

//---------------------------------------------------------------------------------
		private void InsertUserToPermission(ListViewItem wrk)
		{
			if(0 < lvGroupAndUser.SelectedItems.Count)
			{
				int idx = lvGroupAndUser.SelectedItems[0].Index;

				lvGroupAndUser.Items.Insert(idx, wrk);
				lvGroupAndUser.Items[idx].Selected = true;

			} else
			{
				lvGroupAndUser.Items.Add(wrk);
                int idx = lvGroupAndUser.Items.IndexOf(wrk);
                lvGroupAndUser.Items[idx].Selected = true;

            }

            new Cache(SpiderDocsApplication.CurrentUserId).Remove(Cache.en_UKeys.DB_GetAssignedFolderToUser);
		}

//---------------------------------------------------------------------------------
		private void clearDataGrid()
		{
			//for(int i = 0; i < dtgPermission.Rows.Count; i++)
			//{
			//	dtgPermission.Rows.Remove(dtgPermission.Rows[0]);
			//}
            dtgPermission.Rows.Clear();

            cbHeaderAllow.ChkAllTick();
			cbHeaderDeny.ChkAllTick();
		}

        private void pbAddFolder_Click(object sender, EventArgs e)
        {
            try
            {
                int current = int.Parse(cboFolder.SelectedValue.ToString());
                frmFolderExplorer explorer = new frmFolderExplorer(current, (List<Folder>)cboFolder.DataSource,en_Actions.None);
                explorer.Editable = true;

                //explorer.DataBind();

                //explorer.Select(int.Parse(cboFolder.SelectedValue.ToString()));

                explorer.ShowButton = false;
                explorer.ShowDialog(this);

                var idFolder = (int)cboFolder.SelectedValue;

                cboFolder.UseDataSource4AssignedMe(permission: en_Actions.None);

                cboFolder.UpdateDisplay(idFolder);
                    //cboFolder.SelectedValue = idFolder;

                cboFolder_SelectionChangeCommitted();
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }

        }

        /// <summary>
        /// This action will perform to make inheritance/non-inheritance folder permission.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToggleInheritance_Click(object sender, EventArgs e)
        {
            int curFolderId = Convert.ToInt32(cboFolder.SelectedValue);

            var curFolder = FolderController.GetFolder(curFolderId);

            if (curFolderId == 0 || curFolder.id_parent == 0)
            {
                return;
            };

            var inheritedFolder = PermissionController.GetInheritanceFolder(curFolderId);
            if (inheritedFolder.id == curFolder.id)
            {
                // (Enable inheritance)

                DialogResult result = MessageBox.Show(string.Format(lib.msg_ihr_perform_inheritance, curFolder.document_folder), lib.msg_messabox_title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Delete current folder's permission
                    PermissionController.DeleteAllPermission(curFolder.id);

                    dtgPermission.Rows.Clear();
                    lvGroupAndUser.Items.Clear();

                    DisableEditableCtrs();
                }

            }
            else
            {
                // (Disable inheritance)
                DialogResult result = MessageBox.Show(string.Format(lib.msg_ihr_perform_non_inheritance, curFolder.document_folder), lib.msg_messabox_title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Copy inheriting permissions to current
                    PermissionController.CopyPermissions(inheritedFolder.id, curFolder.id);

                    dtgPermission.Rows.Clear();
                    lvGroupAndUser.Items.Clear();

                    EnableEditableCtrs();
                }
            }

            cboFolder_SelectionChangeCommitted();


        }


        /*
       private void clearGroupGrid()
       {
           for (int i = 0; i < this.lvGroup.Items.Count; i++)
               lvGroup.Items.Remove(lvGroup.Items[0]);
       }
*/

        //---------------------------------------------------------------------------------
    }
}
