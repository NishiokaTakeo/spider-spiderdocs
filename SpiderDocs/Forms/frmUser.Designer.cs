namespace SpiderDocs
{
    partial class frmUser
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
            System.Windows.Forms.Label loginLabel;
            System.Windows.Forms.Label nameLabel;
            System.Windows.Forms.Label passwordLabel;
            System.Windows.Forms.Label activeLabel;
            System.Windows.Forms.Label id_permissionLabel;
            System.Windows.Forms.Label emailLabel;
            System.Windows.Forms.Label lblReviewer;
            this.dtgUser = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loginDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_permission = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.passwordDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.reviewerDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.system_permission_levelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.activeCheckBox = new System.Windows.Forms.CheckBox();
            this.emailTextBox1 = new System.Windows.Forms.TextBox();
            this.cboPermission = new System.Windows.Forms.ComboBox();
            this.txtDescPass = new System.Windows.Forms.TextBox();
            this.reviewerCheckBox = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.lbNewUser = new System.Windows.Forms.ToolStripLabel();
            loginLabel = new System.Windows.Forms.Label();
            nameLabel = new System.Windows.Forms.Label();
            passwordLabel = new System.Windows.Forms.Label();
            activeLabel = new System.Windows.Forms.Label();
            id_permissionLabel = new System.Windows.Forms.Label();
            emailLabel = new System.Windows.Forms.Label();
            lblReviewer = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.system_permission_levelBindingSource)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // loginLabel
            // 
            loginLabel.AutoSize = true;
            loginLabel.Location = new System.Drawing.Point(9, 67);
            loginLabel.Name = "loginLabel";
            loginLabel.Size = new System.Drawing.Size(36, 13);
            loginLabel.TabIndex = 4;
            loginLabel.Text = "Login:";
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new System.Drawing.Point(9, 38);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new System.Drawing.Size(38, 13);
            nameLabel.TabIndex = 6;
            nameLabel.Text = "Name:";
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new System.Drawing.Point(268, 38);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new System.Drawing.Size(56, 13);
            passwordLabel.TabIndex = 8;
            passwordLabel.Text = "Password:";
            // 
            // activeLabel
            // 
            activeLabel.AutoSize = true;
            activeLabel.Location = new System.Drawing.Point(606, 66);
            activeLabel.Name = "activeLabel";
            activeLabel.Size = new System.Drawing.Size(40, 13);
            activeLabel.TabIndex = 10;
            activeLabel.Text = "Active:";
            // 
            // id_permissionLabel
            // 
            id_permissionLabel.AutoSize = true;
            id_permissionLabel.Location = new System.Drawing.Point(221, 66);
            id_permissionLabel.Name = "id_permissionLabel";
            id_permissionLabel.Size = new System.Drawing.Size(104, 13);
            id_permissionLabel.TabIndex = 22;
            id_permissionLabel.Text = "Menu Access Level:";
            // 
            // emailLabel
            // 
            emailLabel.AutoSize = true;
            emailLabel.Location = new System.Drawing.Point(520, 37);
            emailLabel.Name = "emailLabel";
            emailLabel.Size = new System.Drawing.Size(38, 13);
            emailLabel.TabIndex = 22;
            emailLabel.Text = "E-mail:";
            // 
            // lblReviewer
            // 
            lblReviewer.AutoSize = true;
            lblReviewer.Location = new System.Drawing.Point(520, 66);
            lblReviewer.Name = "lblReviewer";
            lblReviewer.Size = new System.Drawing.Size(55, 13);
            lblReviewer.TabIndex = 100;
            lblReviewer.Text = "Reviewer:";
            // 
            // dtgUser
            // 
            this.dtgUser.AllowUserToAddRows = false;
            this.dtgUser.AllowUserToDeleteRows = false;
            this.dtgUser.AllowUserToResizeRows = false;
            this.dtgUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgUser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.loginDataGridViewTextBoxColumn,
            this.id_permission,
            this.passwordDataGridViewTextBoxColumn,
            this.email,
            this.activeDataGridViewCheckBoxColumn,
            this.reviewerDataGridViewCheckBoxColumn});
            this.dtgUser.Location = new System.Drawing.Point(3, 93);
            this.dtgUser.MultiSelect = false;
            this.dtgUser.Name = "dtgUser";
            this.dtgUser.ReadOnly = true;
            this.dtgUser.RowHeadersVisible = false;
            this.dtgUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgUser.Size = new System.Drawing.Size(806, 305);
            this.dtgUser.TabIndex = 1;
            this.dtgUser.SelectionChanged += new System.EventHandler(this.userDataGridView_SelectionChanged);
            this.dtgUser.Click += new System.EventHandler(this.userDataGridView_Click);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.MinimumWidth = 150;
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // loginDataGridViewTextBoxColumn
            // 
            this.loginDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.loginDataGridViewTextBoxColumn.DataPropertyName = "login";
            this.loginDataGridViewTextBoxColumn.FillWeight = 1F;
            this.loginDataGridViewTextBoxColumn.HeaderText = "Login";
            this.loginDataGridViewTextBoxColumn.MinimumWidth = 170;
            this.loginDataGridViewTextBoxColumn.Name = "loginDataGridViewTextBoxColumn";
            this.loginDataGridViewTextBoxColumn.ReadOnly = true;
            this.loginDataGridViewTextBoxColumn.Width = 170;
            // 
            // id_permission
            // 
            this.id_permission.DataPropertyName = "permission_str";
            this.id_permission.HeaderText = "Level permission";
            this.id_permission.Name = "id_permission";
            this.id_permission.ReadOnly = true;
            // 
            // passwordDataGridViewTextBoxColumn
            // 
            this.passwordDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.passwordDataGridViewTextBoxColumn.DataPropertyName = "password";
            this.passwordDataGridViewTextBoxColumn.FillWeight = 1F;
            this.passwordDataGridViewTextBoxColumn.HeaderText = "Password";
            this.passwordDataGridViewTextBoxColumn.Name = "passwordDataGridViewTextBoxColumn";
            this.passwordDataGridViewTextBoxColumn.ReadOnly = true;
            this.passwordDataGridViewTextBoxColumn.Visible = false;
            this.passwordDataGridViewTextBoxColumn.Width = 90;
            // 
            // email
            // 
            this.email.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.email.DataPropertyName = "email";
            this.email.FillWeight = 1F;
            this.email.HeaderText = "E-mail";
            this.email.MinimumWidth = 180;
            this.email.Name = "email";
            this.email.ReadOnly = true;
            this.email.Width = 180;
            // 
            // activeDataGridViewCheckBoxColumn
            // 
            this.activeDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.activeDataGridViewCheckBoxColumn.DataPropertyName = "active";
            this.activeDataGridViewCheckBoxColumn.FillWeight = 1F;
            this.activeDataGridViewCheckBoxColumn.HeaderText = "Active";
            this.activeDataGridViewCheckBoxColumn.MinimumWidth = 50;
            this.activeDataGridViewCheckBoxColumn.Name = "activeDataGridViewCheckBoxColumn";
            this.activeDataGridViewCheckBoxColumn.ReadOnly = true;
            this.activeDataGridViewCheckBoxColumn.Width = 50;
            // 
            // reviewerDataGridViewCheckBoxColumn
            // 
            this.reviewerDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.reviewerDataGridViewCheckBoxColumn.DataPropertyName = "reviewer";
            this.reviewerDataGridViewCheckBoxColumn.FillWeight = 1F;
            this.reviewerDataGridViewCheckBoxColumn.HeaderText = "Reviewer";
            this.reviewerDataGridViewCheckBoxColumn.MinimumWidth = 55;
            this.reviewerDataGridViewCheckBoxColumn.Name = "reviewerDataGridViewCheckBoxColumn";
            this.reviewerDataGridViewCheckBoxColumn.ReadOnly = true;
            this.reviewerDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.reviewerDataGridViewCheckBoxColumn.Width = 55;
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(50, 62);
            this.txtLogin.MaxLength = 50;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(160, 20);
            this.txtLogin.TabIndex = 33;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(50, 37);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(160, 20);
            this.txtName.TabIndex = 30;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Enabled = false;
            this.passwordTextBox.Location = new System.Drawing.Point(438, 121);
            this.passwordTextBox.MaxLength = 20;
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.ReadOnly = true;
            this.passwordTextBox.Size = new System.Drawing.Size(43, 20);
            this.passwordTextBox.TabIndex = 99;
            // 
            // activeCheckBox
            // 
            this.activeCheckBox.Checked = true;
            this.activeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.activeCheckBox.Location = new System.Drawing.Point(648, 62);
            this.activeCheckBox.Name = "activeCheckBox";
            this.activeCheckBox.Size = new System.Drawing.Size(25, 24);
            this.activeCheckBox.TabIndex = 35;
            this.activeCheckBox.UseVisualStyleBackColor = true;
            // 
            // emailTextBox1
            // 
            this.emailTextBox1.Location = new System.Drawing.Point(560, 34);
            this.emailTextBox1.MaxLength = 80;
            this.emailTextBox1.Name = "emailTextBox1";
            this.emailTextBox1.Size = new System.Drawing.Size(223, 20);
            this.emailTextBox1.TabIndex = 32;
            // 
            // cboPermission
            // 
            this.cboPermission.DataSource = this.system_permission_levelBindingSource;
            this.cboPermission.DisplayMember = "permission";
            this.cboPermission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPermission.FormattingEnabled = true;
            this.cboPermission.Location = new System.Drawing.Point(330, 62);
            this.cboPermission.Name = "cboPermission";
            this.cboPermission.Size = new System.Drawing.Size(160, 21);
            this.cboPermission.TabIndex = 34;
            this.cboPermission.ValueMember = "id";
            // 
            // txtDescPass
            // 
            this.txtDescPass.Location = new System.Drawing.Point(330, 35);
            this.txtDescPass.MaxLength = 20;
            this.txtDescPass.Name = "txtDescPass";
            this.txtDescPass.PasswordChar = '*';
            this.txtDescPass.Size = new System.Drawing.Size(160, 20);
            this.txtDescPass.TabIndex = 31;
            // 
            // reviewerCheckBox
            // 
            this.reviewerCheckBox.Checked = true;
            this.reviewerCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.reviewerCheckBox.Location = new System.Drawing.Point(577, 62);
            this.reviewerCheckBox.Name = "reviewerCheckBox";
            this.reviewerCheckBox.Size = new System.Drawing.Size(25, 24);
            this.reviewerCheckBox.TabIndex = 101;
            this.reviewerCheckBox.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnSave,
            this.lbNewUser});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(820, 25);
            this.toolStrip1.TabIndex = 102;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAdd
            // 
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAdd.Image = global::SpiderDocs.Properties.Resources.add2;
            this.btnAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(23, 22);
            this.btnAdd.Text = "toolStripButton1";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::SpiderDocs.Properties.Resources.salvar;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "toolStripButton2";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lbNewUser
            // 
            this.lbNewUser.ForeColor = System.Drawing.Color.Red;
            this.lbNewUser.Name = "lbNewUser";
            this.lbNewUser.Size = new System.Drawing.Size(93, 22);
            this.lbNewUser.Text = "*** New User ***";
            this.lbNewUser.Visible = false;
            // 
            // frmUser
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(820, 413);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.reviewerCheckBox);
            this.Controls.Add(lblReviewer);
            this.Controls.Add(this.txtDescPass);
            this.Controls.Add(this.cboPermission);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.activeCheckBox);
            this.Controls.Add(this.emailTextBox1);
            this.Controls.Add(emailLabel);
            this.Controls.Add(id_permissionLabel);
            this.Controls.Add(loginLabel);
            this.Controls.Add(nameLabel);
            this.Controls.Add(passwordLabel);
            this.Controls.Add(activeLabel);
            this.Controls.Add(this.dtgUser);
            this.Controls.Add(this.passwordTextBox);
            this.MinimumSize = new System.Drawing.Size(828, 440);
            this.Name = "frmUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Users";
            this.Load += new System.EventHandler(this.frmUser_Load);
            this.Resize += new System.EventHandler(this.frmUser_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dtgUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.system_permission_levelBindingSource)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.DataGridView dtgUser;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.CheckBox activeCheckBox;
        private System.Windows.Forms.TextBox emailTextBox1;
		private System.Windows.Forms.ComboBox cboPermission;
		private System.Windows.Forms.BindingSource system_permission_levelBindingSource;
		private System.Windows.Forms.TextBox txtDescPass;
		private System.Windows.Forms.CheckBox reviewerCheckBox;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnAdd;
		private System.Windows.Forms.ToolStripButton btnSave;
		private System.Windows.Forms.ToolStripLabel lbNewUser;
		private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn loginDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn id_permission;
		private System.Windows.Forms.DataGridViewTextBoxColumn passwordDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn email;
		private System.Windows.Forms.DataGridViewCheckBoxColumn activeDataGridViewCheckBoxColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn reviewerDataGridViewCheckBoxColumn;
    }
}