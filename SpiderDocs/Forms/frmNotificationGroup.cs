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
	public partial class frmNotificationGroup : Form
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

		int /*idGroup, */idEditNewGroup;
        DTS_NotificationGroup DA_Group = new DTS_NotificationGroup();
        DTS_ViewDocumentGroup dtSearch = new DTS_ViewDocumentGroup();

        int lvGroup_id
        {
            get
            {
                if (0 < lvGroup.SelectedItems.Count)
                    return ((NotificationGroup)lvGroup.SelectedItems[0].Tag).id;
                else
                    return -1;
            }
        }

        int lvUsersOfGroup_id
        {
            get
            {
                if (0 < lvUsersOfGroup.SelectedItems.Count)
                    return ((User)lvUsersOfGroup.SelectedItems[0].Tag).id;
                else
                    return -1;
            }
        }

        int selectedNGroup_id
        {
            get { return Utilities.ConvDataRowId(((System.Data.DataRowView)groupBindingSource.Current).Row.ItemArray[0]); }
        }

        public frmNotificationGroup()
		{
			InitializeComponent();

            //populate grid groups
            groupBindingSource.DataSource = DA_Group.GetDataTable();

            populateDocumentGrid(selectedNGroup_id);
            //this.cmbNotificationGroup.DataSource = NotificationGroupController.GetGroups();
            //foreach (var row in NotificationGroupController.GetGroups())
            //{
            //    this.cmbNotificationGroup.Items.Add(row);

            //}
        }

		private void frmGroupUser_Load(object sender, EventArgs e)
		{
			try
			{

                //populate grid groups
                //populateUserList();
                populateGroupList();
                populateUserOfGroupList();

                //lblGroups.Text  = "Notification Groups (" + cmbNotificationGroup.Items.Count.ToString() + ")";
				//lblGroups.Refresh();

                cmbNotificationGroup_SelectedIndexChanged(cmbNotificationGroup, new EventArgs());
                //dtgGroups.ReadOnly = false;

                //foreach(DataGridViewRow row in dtgGroups.Rows)
                //{
                //    row.Cells[2].ReadOnly = false;
                //}
            }
			catch(Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default_open_form, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
			}
		}

        void populateNGroup(int id_ngroup)
        {
            lvGroup.SelectedItems.Clear();
            lvUsersOfGroup.SelectedItems.Clear();
            lvAllowed.Items.Clear();

            var groupname = NotificationGroupController.GetGroups();

            var userOrGrps = NotificationGroupController.GetUserInGroup(new int[] { id_ngroup });
            foreach(var userOrGrp in userOrGrps)
            {

                if( (en_NGroup)userOrGrp.key_type == en_NGroup.User)
                {
                    var usr = UserController.GetUser(true, userOrGrp.id_key);
                    if (usr == null || usr?.id <= 0) continue;

                    ListViewItem item = new ListViewItem("");
                    item.SubItems.Add(usr.name);
                    item.Tag = userOrGrp;
                    item.ImageIndex = 1;
                    lvAllowed.Items.Add(item);


                }
                else if( (en_NGroup)userOrGrp.key_type == en_NGroup.Group)
                {
                    var g = groupname.Find( x => x.id == userOrGrp.id_key);
                    ListViewItem item = new ListViewItem("");
                    item.SubItems.Add(g.group_name);
                    item.Tag = userOrGrp;
                    item.ImageIndex = 0;
                    lvAllowed.Items.Add(item);

                }
            }
        }

        private void populateUserOfGroupList(int id = 0)
        {
            try
            {
                ListViewItem item;
                //List<int> ids = GroupController.GetUserIdInGroup(true, id);

                //if (0 < ids.Count)
                //{
                    List<User> users = UserController.GetUser(true, false);

                    foreach (User user in users)
                    {
                        item = new ListViewItem("");

                        item.SubItems.Add(user.name);
                        item.Tag = user;
                        item.ImageIndex = 1;
                        lvUsersOfGroup.Items.Add(item);
                    }
                //}
            }
            catch (Exception error)
            {
                logger.Error(error);
            }
        }

        private void populateGroupList()
        {
            try
            {
                this.lvGroup.Items.Clear();

                ListViewItem item;
                List<NotificationGroup> groups = NotificationGroupController.GetGroups();

                foreach (NotificationGroup group in groups)
                {
                    item = new ListViewItem("");
                    item.SubItems.Add(group.group_name + " ( " + group.NoOfUsers + " )");
                    item.Tag = group;
                    item.ImageIndex = 0;

                    this.lvGroup.Items.Add(item);
                }

                this.lvGroup.Items[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);

            }
            catch (Exception error)
            {
                logger.Error(error);
            }
        }


        private void lvGroup_SelectionChanged(object sender, EventArgs e)
        {

        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
			this.txtGroup.Visible = false;

			if (lvGroup_id == -1 || lvGroup_id == 0 || lvGroup_id == 1 )
            {
				MessageBox.Show(lib.msg_error_no_select, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

				this.lbNewUser.Visible = false;
                this.txtGroup.Text = string.Empty;

				return;
            }


            // TODO: check if document is assigned as this notification.

            DialogResult result = MessageBox.Show(lib.msg_ask_delete_notification_group, lib.msg_messabox_title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            DocumentNotificationGroupController.RemoveNotificationGroup(ids_group: new int[] { lvGroup_id });

            NotificationGroupController.DeleteGroup(lvGroup_id);

            NotificationGroupController.DeleteUserGroup(lvGroup_id);

            DA_Group = new DTS_NotificationGroup();
            groupBindingSource.DataSource = DA_Group.GetDataTable();

            logger.Info("Notification Group with {0} and associated tables records have been removed.", lvGroup_id);

            populateGroupList();

            cmbNotificationGroup_SelectedIndexChanged(cmbNotificationGroup, new EventArgs());

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validation())
                {
                    groupBindingSource.CancelEdit();
                    return;
                }

                Validate();
                groupBindingSource.EndEdit();

                if (0 < idEditNewGroup)
                    NotificationGroupController.SaveGroup(new NotificationGroup() { id = idEditNewGroup, group_name =this.txtGroup.Text });
                else
                    NotificationGroupController.SaveGroup(new NotificationGroup() { group_name = this.txtGroup.Text });

                DA_Group = new DTS_NotificationGroup();

                groupBindingSource.DataSource = DA_Group.GetDataTable();

                populateGroupList();

                cmbNotificationGroup_SelectedIndexChanged(cmbNotificationGroup, new EventArgs());

                this.lbNewUser.Visible = false;
				this.txtGroup.Visible = false;
            }
            catch (Exception error)
            {
                MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(error);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
			this.txtGroup.Enabled = this.txtGroup.Visible = true;
            this.txtGroup.Text = string.Empty;
            this.lbNewUser.Visible = true;

            idEditNewGroup = 0;
        }

        private void btnSaveAsPDF_Click(object sender, EventArgs e)
        {


        }

        private void tsbSaveAsPDF4User_Click(object sender, EventArgs e)
        {
            string path = GetSavePathWithDialog();

            if (string.IsNullOrWhiteSpace(path)) return;

            try {

                SavePDF4Users(path);

                MessageBox.Show(lib.msg_sucess_save_file, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.IO.IOException ex)
            {
                if( ex.Message.IndexOf("cannot access") > -1)
                    MessageBox.Show(lib.msg_file_opened, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(lib.msg_error_save_file + lib.msg_error_description + ex.Message, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbSaveAsPDF4Docs_Click(object sender, EventArgs e)
        {
            string path = GetSavePathWithDialog();

            if (string.IsNullOrWhiteSpace(path)) return;

            try
            {
                SavePDF4Docs(path);

                MessageBox.Show(lib.msg_sucess_save_file, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.IO.IOException ex)
            {
                if (ex.Message.IndexOf("cannot access") > -1)
                    MessageBox.Show(lib.msg_file_opened, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(lib.msg_error_save_file + lib.msg_error_description + ex.Message, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        void SavePDF4Users(string path)
        {
            using (var saver = new PDFWriter())
            {
				var ngroups = NotificationGroupController.GetGroups();

				saver.Open();
                saver.Title("Spider Docs - List for users belong to each group");

                foreach (var groups in ngroups)
                {
                    System.Data.DataTable table = new System.Data.DataTable();

                    saver.SectionTitle(string.Format("Group: {0}", groups.group_name));

                    table.Columns.Add("User Name/Group Name", typeof(string));

                    List<string> users = NotificationGroupController.GetUserIdInGroup(true, groups.id).Select(x => (en_NGroup)x.key_type == en_NGroup.User ? Cache.GetUser().Find(u => u.id == x.id_key)?.name : ngroups.Find( g => g.id == x.id_key)?.group_name).ToList();
                    users.ForEach(name => table.Rows.Add(name));

                    saver.Table(table);

                    saver.MakeNextPage();
                }

                saver.Save(path);
            }
        }


        void SavePDF4Docs(string path)
        {
            using (var saver = new PDFWriter())
            {
                saver.Open();
                saver.Title("Spider Docs - List for docs belong to each group");
                foreach (var groups in NotificationGroupController.GetGroups())
                {
                    saver.SectionTitle(string.Format("Group: {0}", groups.group_name));

                    var docsByNotificationGroup = DocumentNotificationGroupController.Select(groups.id);

                    dtSearch.Select(groups.id);

                    if (dtSearch.GetDataTable().Rows.Count > 0)
                    {
                        System.Data.DataTable table = new System.Data.DataTable();
                        table.Columns.Add("ID", typeof(int));
                        table.Columns.Add("Title", typeof(string));
                        table.Columns.Add("Last Amended", typeof(DateTime));
                        table.Columns.Add("Version", typeof(string));
                        table.Columns.Add("Amended by", typeof(string));

                        foreach (System.Data.DataRow row in dtSearch.GetDataTable().Rows)
                        {
                            table.Rows.Add((int)row["id_doc"], (string)row["title"], (DateTime)row["date"], (string)row["version"], (string)row["name"]);
                        }

                        //List<SpiderDocsModule.Document> docs = DocumentController<SpiderDocsModule.Document>.GetDocument(id_doc: docsByNotificationGroup.Select(x => x.id_doc).ToArray());
                        //docs.ForEach(u => table.Rows.Add(u.title));

                        saver.Table(table);
                    }
                    else
                    {
                        saver.Paragraph("No documents");
                    }

                    saver.MakeNextPage();
                }

                saver.Save(path);
            }
        }


        private string GetSavePathWithDialog()
        {
            string path = string.Empty;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                path = saveFileDialog1.FileName;

            return path;
        }


        private void lvGroup_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                lvUsersOfGroup.SelectedItems.Clear();

                Group item = (Group)e.Item.Tag;


                //lvUsersOfGroup.Items.Clear();
                //populateUserOfGroupList(item.id);
            }
            catch (Exception error)
            {
                logger.Error(error);
            }
        }

        private void cmbNotificationGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id_ngroup = Convert.ToInt32(cmbNotificationGroup.SelectedValue);

                populateNGroup(id_ngroup);

                populateDocumentGrid(id_ngroup);

            }
            catch (Exception error)
            {
                logger.Error(error);
            }
        }

        void populateDocumentGrid(int id_ngroup)
        {
            dtSearch.Select(id_ngroup);

            dtgBdFiles.DataSource = dtSearch.GetDataTable();

        }


        /// <summary>
        /// Add Group Picture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {


            if (0 < lvGroup.SelectedItems.Count)
            {
                addGroupPermission(en_NGroup.Group);
            }
        }

        //---------------------------------------------------------------------------------
        private void pictureBox3_Click(object sender, EventArgs e)
        {

            if (0 < lvUsersOfGroup.SelectedItems.Count)
            {
                addGroupPermission(en_NGroup.User);

            }
        }
        private void addGroupPermission(en_NGroup grouptype)
        {
            try
            {

                if (selectedNGroup_id == 1)
                {
                    MessageBox.Show(lib.msg_error_ng_all, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (grouptype == en_NGroup.Group && selectedNGroup_id == lvGroup_id)
                {
                    MessageBox.Show(lib.msg_existing_ng, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (grouptype == en_NGroup.Group && 0 < lvGroup.SelectedItems.Count)
                {
                    NotificationGroupController.DeleteUserGroupBykey(selectedNGroup_id,lvGroup_id, en_NGroup.Group);

                    NotificationGroupController.AssignGroup(selectedNGroup_id, lvGroup_id, en_NGroup.Group);

                }
                else if (grouptype == en_NGroup.User && 0 < lvUsersOfGroup.SelectedItems.Count)
                {
                    NotificationGroupController.DeleteUserGroupBykey(selectedNGroup_id,lvUsersOfGroup_id, en_NGroup.User);

                    NotificationGroupController.AssignGroup(selectedNGroup_id, lvUsersOfGroup_id, en_NGroup.User);
                }

                populateNGroup(selectedNGroup_id);

            }
            catch (Exception error)
            {
                logger.Error(error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvAllowed.SelectedItems.Count == 0)
                    return;

                if (selectedNGroup_id == 1)
                {
                    MessageBox.Show(lib.msg_error_ng_all, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var result = (MessageBox.Show("Are you sure you want to delete this group/user?", "Spider Docs", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question));

                if (result == DialogResult.Yes)
                {
                    foreach (ListViewItem eachItem in this.lvAllowed.SelectedItems)
                    {
                        //delete from base
                        NotificationGroupController.DeleteUserGroupBy(((UserNotificationGroup)eachItem.Tag).id);

                        //delete from grid
                        lvAllowed.Items.RemoveAt(eachItem.Index);
                    }
                }
            }
            catch (Exception error)
            {
                logger.Error(error);
            }
            finally
            {
                new Cache(SpiderDocsApplication.CurrentUserId).Remove(Cache.en_UKeys.DB_GetAssignedFolderToUser);
            }
        }

        private void lvUsersOfGroup_Click(object sender, EventArgs e)
        {

        }



        private bool validation()
        {
            if (txtGroup.Text == "")
            {
                txtGroup.BackColor = System.Drawing.Color.LavenderBlush;
                txtGroup.Focus();
                MessageBox.Show(lib.msg_required_name, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;

            }
            else
            {
                txtGroup.BackColor = System.Drawing.Color.White;
            }

            return true;
        }
    }
}
