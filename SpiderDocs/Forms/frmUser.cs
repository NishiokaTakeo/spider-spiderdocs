using System;
using System.Windows.Forms;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;
using Spider;
using Spider.Forms;
using NLog;
namespace SpiderDocs
{
	public partial class frmUser : Form
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

		//crypt class
		public Crypt crypt = new Crypt();

		User SelectedUser = null;
		SortableBindingList<User> users = new SortableBindingList<User>(UserController.GetUser(false, false));
		DTS_System_permission_level DA_System_permission_level = new DTS_System_permission_level();

		public frmUser()
		{
			InitializeComponent();
			dtgUser.AutoGenerateColumns = false;

			//load permission levels
			system_permission_levelBindingSource.DataSource = DA_System_permission_level.GetDataTable();

			//load users
			dtgUser.DataSource = users;
		}

		private void frmUser_Load(object sender, EventArgs e)
		{
			if(dtgUser.Rows.Count == 0)
				btnSave.Enabled = false;
		}

		private void userDataGridView_SelectionChanged(object sender, EventArgs e)
		{
			if(0 < dtgUser.SelectedRows.Count)
			{
				SelectedUser = users[dtgUser.SelectedRows[0].Index];

				if(SelectedUser != null)
				{
					txtName.Text = SelectedUser.name;
					emailTextBox1.Text = SelectedUser.email;
					txtLogin.Text = SelectedUser.login;
					cboPermission.SelectedValue = SelectedUser.id_permission;
					reviewerCheckBox.Checked = SelectedUser.reviewer;
					activeCheckBox.Checked = SelectedUser.active;
					txtDescPass.Text = crypt.Decrypt(SelectedUser.password);
				}
			}
		}

		private void userDataGridView_Click(object sender, EventArgs e)
		{
			dtgUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
		}

		private void frmUser_Resize(object sender, EventArgs e)
		{
			dtgUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			SelectedUser = null;

			txtName.Focus();
			lbNewUser.Visible = true;
			btnAdd.Enabled = false;
			btnSave.Enabled = true;
            this.txtDescPass.Text = this.txtLogin.Text = this.txtName.Text = this.emailTextBox1.Text = string.Empty;
            this.reviewerCheckBox.Checked = this.activeCheckBox.Checked = false;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
            try
            {
                bool Update = (SelectedUser != null);

                //validate field
                if (validation(Update) == false)
                    return;

                if (!Update) // new user
                    SelectedUser = new User();

                SelectedUser.name = txtName.Text;
                SelectedUser.email = emailTextBox1.Text;
                SelectedUser.login = txtLogin.Text;
                SelectedUser.id_permission = Convert.ToInt32(cboPermission.SelectedValue);
                SelectedUser.reviewer = reviewerCheckBox.Checked;
                SelectedUser.active = activeCheckBox.Checked;
                SelectedUser.password = crypt.Encrypt(txtDescPass.Text);

                if (0 < SelectedUser.id)
                    UserController.UpdatetUser(SelectedUser);
                else
                    UserController.InsertUser(SelectedUser);

                Cache.Remove(Cache.en_GKeys.DB_User);

                //enable add new button
                btnAdd.Enabled = true;
                lbNewUser.Visible = false;

                users = new SortableBindingList<User>(UserController.GetUser(false, false));
                dtgUser.DataSource = users;

            }
            catch (Exception error)
            {
                MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(error);
            }
            finally
            {
                Cache.Remove(Cache.en_GKeys.DB_User);
                Utilities.RefreshAuthor(); 
            }
		}

		bool validation(bool Update)
		{
			try
			{
				bool ans = true;

				FormUtilities.PutErrorColour(txtName, false, true);
				if(txtName.Text == "")
				{
					FormUtilities.PutErrorColour(txtName, ans);

					if(ans)
						MessageBox.Show(lib.msg_required_name, lib.msg_messabox_title ,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

					ans = false;
				}

				FormUtilities.PutErrorColour(txtDescPass, false, true);
				if(txtDescPass.Text == "")
				{
					FormUtilities.PutErrorColour(txtDescPass, ans);
					
					if(ans)
						MessageBox.Show(lib.msg_required_password, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

					ans =  false;
				}

				FormUtilities.PutErrorColour(emailTextBox1, false, true);
				if(emailTextBox1.Text == "")
				{
					FormUtilities.PutErrorColour(emailTextBox1, ans);

					if(ans)
						MessageBox.Show(lib.msg_required_email, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

					ans = false;
				}

				FormUtilities.PutErrorColour(txtLogin, false, true);
				if(txtLogin.Text == "")
				{
					FormUtilities.PutErrorColour(txtLogin, ans);
					
					if(ans)
						MessageBox.Show(lib.msg_required_login, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

					ans = false;

				}else if(!Update && UserController.IsUserAlreadyExists(txtLogin.Text))
				{
					FormUtilities.PutErrorColour(txtLogin, ans);

					if(ans)
						MessageBox.Show(lib.msg_existing_login, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

					ans = false;
				}

				FormUtilities.PutErrorColour(cboPermission, false, true);
				if(Convert.ToInt32(cboPermission.SelectedValue) <= 0)
				{
					FormUtilities.PutErrorColour(cboPermission, ans);

					if(ans)
						MessageBox.Show(lib.msg_required_levelPermission, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

					ans = false;
				}

				return ans;
			}
			catch(Exception error)
			{
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				logger.Error(error);
				return false;
			}
		}
	}
}
