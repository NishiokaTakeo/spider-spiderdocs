namespace SpiderDocs
{
    partial class frmSplashScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplashScreen));
			this.timerOpacity = new System.Windows.Forms.Timer(this.components);
			this.pbLock = new System.Windows.Forms.PictureBox();
			this.lblUserName = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.lblPass = new System.Windows.Forms.Label();
			this.btnLogin = new System.Windows.Forms.Button();
			this.ckSavepass = new System.Windows.Forms.CheckBox();
			this.pbar = new System.Windows.Forms.ProgressBar();
			this.btnClose = new System.Windows.Forms.Button();
			this.panelLogin = new System.Windows.Forms.Panel();
			this.lblLogin = new System.Windows.Forms.Label();
			this.txtUser = new System.Windows.Forms.TextBox();
			this.pbChangeServer = new System.Windows.Forms.PictureBox();
			this.toolTipats = new System.Windows.Forms.ToolTip(this.components);
			this.lblAction = new System.Windows.Forms.Label();
			this.panelError = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.lblMsgError = new System.Windows.Forms.Label();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.lblVersion = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pbLock)).BeginInit();
			this.panelLogin.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbChangeServer)).BeginInit();
			this.panelError.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			this.SuspendLayout();
			//
			// timerOpacity
			//
			this.timerOpacity.Tick += new System.EventHandler(this.timerOpacity_Tick);
			//
			// pbLock
			//
			this.pbLock.BackColor = System.Drawing.Color.Transparent;
			this.pbLock.Image = global::SpiderDocs.Properties.Resources.login;
			this.pbLock.Location = new System.Drawing.Point(4, 10);
			this.pbLock.Name = "pbLock";
			this.pbLock.Size = new System.Drawing.Size(46, 41);
			this.pbLock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbLock.TabIndex = 7;
			this.pbLock.TabStop = false;
			//
			// lblUserName
			//
			this.lblUserName.AutoSize = true;
			this.lblUserName.BackColor = System.Drawing.Color.Transparent;
			this.lblUserName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.lblUserName.Location = new System.Drawing.Point(56, 7);
			this.lblUserName.Name = "lblUserName";
			this.lblUserName.Size = new System.Drawing.Size(63, 13);
			this.lblUserName.TabIndex = 2;
			this.lblUserName.Text = "User Name:";
			//
			// txtPassword
			//
			this.txtPassword.BackColor = System.Drawing.Color.WhiteSmoke;
			this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPassword.Location = new System.Drawing.Point(224, 23);
			this.txtPassword.MaxLength = 50;
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = 'x';
			this.txtPassword.Size = new System.Drawing.Size(122, 20);
			this.txtPassword.TabIndex = 1;
			//
			// lblPass
			//
			this.lblPass.AutoSize = true;
			this.lblPass.BackColor = System.Drawing.Color.Transparent;
			this.lblPass.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.lblPass.Location = new System.Drawing.Point(221, 7);
			this.lblPass.Name = "lblPass";
			this.lblPass.Size = new System.Drawing.Size(56, 13);
			this.lblPass.TabIndex = 3;
			this.lblPass.Text = "Password:";
			//
			// btnLogin
			//
			this.btnLogin.AutoSize = true;
			this.btnLogin.Location = new System.Drawing.Point(352, 20);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(67, 23);
			this.btnLogin.TabIndex = 3;
			this.btnLogin.Text = "Login";
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			//
			// ckSavepass
			//
			this.ckSavepass.AutoSize = true;
			this.ckSavepass.BackColor = System.Drawing.Color.Transparent;
			this.ckSavepass.Checked = true;
			this.ckSavepass.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckSavepass.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ckSavepass.Location = new System.Drawing.Point(352, 49);
			this.ckSavepass.Name = "ckSavepass";
			this.ckSavepass.Size = new System.Drawing.Size(51, 17);
			this.ckSavepass.TabIndex = 2;
			this.ckSavepass.Text = "Save";
			this.ckSavepass.UseVisualStyleBackColor = false;
			//
			// pbar
			//
			this.pbar.Location = new System.Drawing.Point(457, 187);
			this.pbar.MarqueeAnimationSpeed = 77;
			this.pbar.Name = "pbar";
			this.pbar.Size = new System.Drawing.Size(230, 19);
			this.pbar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.pbar.TabIndex = 13;
			//
			// btnClose
			//
			this.btnClose.BackColor = System.Drawing.Color.Transparent;
			this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnClose.FlatAppearance.BorderSize = 0;
			this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnClose.ForeColor = System.Drawing.Color.DimGray;
			this.btnClose.Location = new System.Drawing.Point(661, 28);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(23, 26);
			this.btnClose.TabIndex = 8;
			this.btnClose.Text = "X";
			this.toolTipats.SetToolTip(this.btnClose, "Close application");
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			//
			// panelLogin
			//
			this.panelLogin.BackColor = System.Drawing.Color.Transparent;
			this.panelLogin.Controls.Add(this.txtPassword);
			this.panelLogin.Controls.Add(this.txtUser);
			this.panelLogin.Controls.Add(this.lblLogin);
			this.panelLogin.Controls.Add(this.pbLock);
			this.panelLogin.Controls.Add(this.lblPass);
			this.panelLogin.Controls.Add(this.btnLogin);
			this.panelLogin.Controls.Add(this.ckSavepass);
			this.panelLogin.Controls.Add(this.lblUserName);
			this.panelLogin.Location = new System.Drawing.Point(12, 12);
			this.panelLogin.Name = "panelLogin";
			this.panelLogin.Size = new System.Drawing.Size(428, 70);
			this.panelLogin.TabIndex = 15;
			this.panelLogin.Visible = false;
			//
			// lblLogin
			//
			this.lblLogin.AutoSize = true;
			this.lblLogin.BackColor = System.Drawing.Color.Transparent;
			this.lblLogin.ForeColor = System.Drawing.Color.Maroon;
			this.lblLogin.Location = new System.Drawing.Point(56, 51);
			this.lblLogin.Name = "lblLogin";
			this.lblLogin.Size = new System.Drawing.Size(43, 13);
			this.lblLogin.TabIndex = 18;
			this.lblLogin.Text = "lblLogin";
			this.lblLogin.Visible = false;
			//
			// txtUser
			//
			this.txtUser.BackColor = System.Drawing.Color.WhiteSmoke;
			this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtUser.Location = new System.Drawing.Point(59, 23);
			this.txtUser.MaxLength = 50;
			this.txtUser.Name = "txtUser";
			this.txtUser.Size = new System.Drawing.Size(139, 20);
			this.txtUser.TabIndex = 0;
			//
			// pbChangeServer
			//
			this.pbChangeServer.Image = ((System.Drawing.Image)(resources.GetObject("pbChangeServer.Image")));
			this.pbChangeServer.Location = new System.Drawing.Point(643, 33);
			this.pbChangeServer.Name = "pbChangeServer";
			this.pbChangeServer.Size = new System.Drawing.Size(16, 16);
			this.pbChangeServer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbChangeServer.TabIndex = 18;
			this.pbChangeServer.TabStop = false;
			this.toolTipats.SetToolTip(this.pbChangeServer, "Change Server");
			this.pbChangeServer.Click += new System.EventHandler(this.pbChangeServer_Click);
			//
			// toolTipats
			//
			this.toolTipats.IsBalloon = true;
			this.toolTipats.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			//
			// lblAction
			//
			this.lblAction.BackColor = System.Drawing.Color.Transparent;
			this.lblAction.ForeColor = System.Drawing.Color.RoyalBlue;
			this.lblAction.Location = new System.Drawing.Point(457, 210);
			this.lblAction.Name = "lblAction";
			this.lblAction.Size = new System.Drawing.Size(232, 14);
			this.lblAction.TabIndex = 19;
			this.lblAction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			//
			// panelError
			//
			this.panelError.BackColor = System.Drawing.Color.Transparent;
			this.panelError.Controls.Add(this.label3);
			this.panelError.Controls.Add(this.lblMsgError);
			this.panelError.Controls.Add(this.pictureBox3);
			this.panelError.Location = new System.Drawing.Point(13, 161);
			this.panelError.Name = "panelError";
			this.panelError.Size = new System.Drawing.Size(676, 70);
			this.panelError.TabIndex = 20;
			this.panelError.Visible = false;
			//
			// label3
			//
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.Brown;
			this.label3.Location = new System.Drawing.Point(86, 38);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(277, 21);
			this.label3.TabIndex = 21;
			this.label3.Text = "Contact suport team or try again in few minutes.";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			//
			// lblMsgError
			//
			this.lblMsgError.BackColor = System.Drawing.Color.Transparent;
			this.lblMsgError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMsgError.ForeColor = System.Drawing.Color.Brown;
			this.lblMsgError.Location = new System.Drawing.Point(73, 23);
			this.lblMsgError.Name = "lblMsgError";
			this.lblMsgError.Size = new System.Drawing.Size(131, 21);
			this.lblMsgError.TabIndex = 20;
			this.lblMsgError.Text = "Database error.";
			this.lblMsgError.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			//
			// pictureBox3
			//
			this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox3.Image = global::SpiderDocs.Properties.Resources.database;
			this.pictureBox3.Location = new System.Drawing.Point(3, 13);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(46, 46);
			this.pictureBox3.TabIndex = 7;
			this.pictureBox3.TabStop = false;
			//
			// lblVersion
			//
			this.lblVersion.AutoSize = true;
			this.lblVersion.BackColor = System.Drawing.Color.Transparent;
			this.lblVersion.ForeColor = System.Drawing.Color.Maroon;
			this.lblVersion.Location = new System.Drawing.Point(600, 9);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(72, 13);
			this.lblVersion.TabIndex = 21;
			this.lblVersion.Text = "Version: 0.0.0";
			//
			// frmSplashScreen
			//
			this.BackgroundImage = global::SpiderDocs.Properties.Resources.loginScreen;
			this.ClientSize = new System.Drawing.Size(701, 240);
			this.ControlBox = false;
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.panelLogin);
			this.Controls.Add(this.pbChangeServer);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.pbar);
			this.Controls.Add(this.panelError);
			this.Controls.Add(this.lblAction);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmSplashScreen";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmSplashScreen";
			this.Load += new System.EventHandler(this.frmSplashScreen_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSplashScreen_KeyDown);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			((System.ComponentModel.ISupportInitialize)(this.pbLock)).EndInit();
			this.panelLogin.ResumeLayout(false);
			this.panelLogin.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbChangeServer)).EndInit();
			this.panelError.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerOpacity;
        private System.Windows.Forms.PictureBox pbLock;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.CheckBox ckSavepass;
        private System.Windows.Forms.ProgressBar pbar;
        private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.PictureBox pbChangeServer;
        private System.Windows.Forms.ToolTip toolTipats;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.Panel panelError;
		private System.Windows.Forms.Label lblMsgError;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblLogin;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.PictureBox pictureBox3;
    }
}