using System;
using System.Drawing;
using System.Windows.Forms;
using SpiderDocsModule;
using SpiderDocsForms;
using NLog;

namespace SpiderDocs
{
    public partial class frmChangePassword : Spider.Forms.FormBase
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        public Crypt crypt = new Crypt();

        public frmChangePassword()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtCurrentPass.Text == "")
                {
                    txtCurrentPass.BackColor = Color.LavenderBlush;
                    txtCurrentPass.Focus();
                    MessageBox.Show("Please, enter the current password.", "Spider Docs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    txtCurrentPass.BackColor = Color.White;
                }

                if(txtNewPassword.Text == "")
                {
                    txtNewPassword.BackColor = Color.LavenderBlush;
                    txtNewPassword.Focus();
                    MessageBox.Show("Please, enter the new password.", "Spider Docs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    txtNewPassword.BackColor = Color.White;
                }

                //checking current password
                int tryLogin = 0;
                tryLogin = UserController.Login(SpiderDocsApplication.CurrentUserName, crypt.Encrypt(txtCurrentPass.Text));

                if(tryLogin > 0)
                {
                    UserController.ChangePassword(SpiderDocsApplication.CurrentUserId, crypt.Encrypt(txtNewPassword.Text));

                    UserController.registerLog(en_UserEvents.PasswordCahnge, "");
                    MessageBox.Show("Password changed.", "Spider Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtCurrentPass.Text = "";
                    txtNewPassword.Text = "";

                }
                else
                {
                    UserController.registerLog(en_UserEvents.PasswordChangeFail, "");
                    MessageBox.Show("Current password is incorrect.", "Spider Docs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch(Exception error)
            {
                MessageBox.Show("There was an unexpected error in changing the password.", "Spider Docs", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                logger.Error(error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
