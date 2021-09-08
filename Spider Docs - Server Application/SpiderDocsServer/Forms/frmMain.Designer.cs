namespace SpiderDocsServer
{
    partial class frmMain
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
			System.Windows.Forms.Label portLabel;
			System.Windows.Forms.Label serverLabel;
			System.Windows.Forms.Label email_accountLabel;
			System.Windows.Forms.Label passwordLabel;
			System.Windows.Forms.Label max_docsLabel;
			System.Windows.Forms.Label max_recentsLabel;
			System.Windows.Forms.Label label29;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.registerUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.updatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.emailServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.tb3 = new System.Windows.Forms.TabPage();
			this.gp_ServerMode = new System.Windows.Forms.GroupBox();
			this.rb_mode1 = new System.Windows.Forms.RadioButton();
			this.rb_mode2 = new System.Windows.Forms.RadioButton();
			this.EmailPanel = new System.Windows.Forms.Panel();
			this.btnTestEmailServer = new System.Windows.Forms.Button();
			this.btnSaveEmailsServer = new System.Windows.Forms.Button();
			this.txtEmailServerPort = new System.Windows.Forms.TextBox();
			this.ckSSL = new System.Windows.Forms.CheckBox();
			this.txtEmailServer = new System.Windows.Forms.TextBox();
			this.ckSend = new System.Windows.Forms.CheckBox();
			this.txtEmailPassword = new System.Windows.Forms.TextBox();
			this.txtEmailAccount = new System.Windows.Forms.TextBox();
			this.btnSaveHostPort = new System.Windows.Forms.Button();
			this.label39 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.lblMsgEmail = new System.Windows.Forms.Label();
			this.pBoxloadingEmailServer = new System.Windows.Forms.PictureBox();
			this.label38 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lblMsg = new System.Windows.Forms.Label();
			this.pBoxLoading = new System.Windows.Forms.PictureBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnTestConn = new System.Windows.Forms.Button();
			this.txtDbName = new System.Windows.Forms.TextBox();
			this.txtUser = new System.Windows.Forms.TextBox();
			this.txtPass = new System.Windows.Forms.TextBox();
			this.txtDbServer = new System.Windows.Forms.TextBox();
			this.label32 = new System.Windows.Forms.Label();
			this.label33 = new System.Windows.Forms.Label();
			this.label34 = new System.Windows.Forms.Label();
			this.label35 = new System.Windows.Forms.Label();
			this.label37 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblMsgHost = new System.Windows.Forms.Label();
			this.pBoxloadinbgHostPort = new System.Windows.Forms.PictureBox();
			this.btnTestHostPort = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.btnSocket = new System.Windows.Forms.Button();
			this.lblServiceStatus = new System.Windows.Forms.Label();
			this.lblIp = new System.Windows.Forms.Label();
			this.lblHostName = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.txtHostPort = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.tb2 = new System.Windows.Forms.TabPage();
			this.pBoxLoadinbgUpdate = new System.Windows.Forms.PictureBox();
			this.panel6 = new System.Windows.Forms.Panel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.btnInstallUpdate = new System.Windows.Forms.Button();
			this.lblMsgUpdate = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.lblProgress = new System.Windows.Forms.Label();
			this.prgDownload = new System.Windows.Forms.ProgressBar();
			this.btnCheckUpdates = new System.Windows.Forms.Button();
			this.label25 = new System.Windows.Forms.Label();
			this.lblLastTimeChecked = new System.Windows.Forms.Label();
			this.label40 = new System.Windows.Forms.Label();
			this.lblCurrentVersion = new System.Windows.Forms.Label();
			this.tbControl = new System.Windows.Forms.TabControl();
			this.tb1 = new System.Windows.Forms.TabPage();
			this.panelActive = new System.Windows.Forms.Panel();
			this.maskProduct_key = new System.Windows.Forms.MaskedTextBox();
			this.label26 = new System.Windows.Forms.Label();
			this.lblActivation = new System.Windows.Forms.Label();
			this.btnActiveProduct = new System.Windows.Forms.Button();
			this.txtClientId = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.lblMsgServiceStatus = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tb6 = new System.Windows.Forms.TabPage();
			this.GrpFooterMenu = new System.Windows.Forms.GroupBox();
			this.rbFooter_With = new System.Windows.Forms.RadioButton();
			this.rbFooter_Option = new System.Windows.Forms.RadioButton();
			this.ckFooter = new System.Windows.Forms.CheckBox();
			this.ckAllowDuplicatedFilesNames = new System.Windows.Forms.CheckBox();
			this.txtWebServiceAddress = new System.Windows.Forms.TextBox();
			this.ckAllowWorkSpace = new System.Windows.Forms.CheckBox();
			this.ckReasonNewVersion = new System.Windows.Forms.CheckBox();
			this.label23 = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.txtMax_docs = new System.Windows.Forms.TextBox();
			this.txtMax_recents = new System.Windows.Forms.TextBox();
			this.ckShow_watermarks = new System.Windows.Forms.CheckBox();
			this.btnSaveOptions = new System.Windows.Forms.Button();
			this.tb7 = new System.Windows.Forms.TabPage();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label17 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.tsVersion = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openDMSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			portLabel = new System.Windows.Forms.Label();
			serverLabel = new System.Windows.Forms.Label();
			email_accountLabel = new System.Windows.Forms.Label();
			passwordLabel = new System.Windows.Forms.Label();
			max_docsLabel = new System.Windows.Forms.Label();
			max_recentsLabel = new System.Windows.Forms.Label();
			label29 = new System.Windows.Forms.Label();
			this.tb3.SuspendLayout();
			this.gp_ServerMode.SuspendLayout();
			this.EmailPanel.SuspendLayout();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pBoxloadingEmailServer)).BeginInit();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pBoxLoading)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pBoxloadinbgHostPort)).BeginInit();
			this.tb2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pBoxLoadinbgUpdate)).BeginInit();
			this.tbControl.SuspendLayout();
			this.tb1.SuspendLayout();
			this.panelActive.SuspendLayout();
			this.tb6.SuspendLayout();
			this.GrpFooterMenu.SuspendLayout();
			this.tb7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.statusStrip.SuspendLayout();
			this.MenuStrip.SuspendLayout();
			this.SuspendLayout();
			//
			// portLabel
			//
			portLabel.AutoSize = true;
			portLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			portLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			portLabel.Location = new System.Drawing.Point(369, 39);
			portLabel.Name = "portLabel";
			portLabel.Size = new System.Drawing.Size(29, 13);
			portLabel.TabIndex = 68;
			portLabel.Text = "Port:";
			//
			// serverLabel
			//
			serverLabel.AutoSize = true;
			serverLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			serverLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			serverLabel.Location = new System.Drawing.Point(7, 16);
			serverLabel.Name = "serverLabel";
			serverLabel.Size = new System.Drawing.Size(41, 13);
			serverLabel.TabIndex = 57;
			serverLabel.Text = "Server:";
			//
			// email_accountLabel
			//
			email_accountLabel.AutoSize = true;
			email_accountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			email_accountLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			email_accountLabel.Location = new System.Drawing.Point(350, 14);
			email_accountLabel.Name = "email_accountLabel";
			email_accountLabel.Size = new System.Drawing.Size(50, 13);
			email_accountLabel.TabIndex = 59;
			email_accountLabel.Text = "Account:";
			//
			// passwordLabel
			//
			passwordLabel.AutoSize = true;
			passwordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			passwordLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			passwordLabel.Location = new System.Drawing.Point(7, 38);
			passwordLabel.Name = "passwordLabel";
			passwordLabel.Size = new System.Drawing.Size(56, 13);
			passwordLabel.TabIndex = 61;
			passwordLabel.Text = "Password:";
			//
			// max_docsLabel
			//
			max_docsLabel.AutoSize = true;
			max_docsLabel.Location = new System.Drawing.Point(8, 278);
			max_docsLabel.Name = "max_docsLabel";
			max_docsLabel.Size = new System.Drawing.Size(104, 13);
			max_docsLabel.TabIndex = 0;
			max_docsLabel.Text = "Max files per search:";
			//
			// max_recentsLabel
			//
			max_recentsLabel.AutoSize = true;
			max_recentsLabel.Location = new System.Drawing.Point(8, 305);
			max_recentsLabel.Name = "max_recentsLabel";
			max_recentsLabel.Size = new System.Drawing.Size(84, 13);
			max_recentsLabel.TabIndex = 2;
			max_recentsLabel.Text = "Max recent files:";
			//
			// label29
			//
			label29.AutoSize = true;
			label29.Location = new System.Drawing.Point(8, 332);
			label29.Name = "label29";
			label29.Size = new System.Drawing.Size(110, 13);
			label29.TabIndex = 78;
			label29.Text = "WebService Address:";
			//
			// registerUserToolStripMenuItem
			//
			this.registerUserToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
			this.registerUserToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.registerUserToolStripMenuItem.Name = "registerUserToolStripMenuItem";
			this.registerUserToolStripMenuItem.Size = new System.Drawing.Size(59, 23);
			this.registerUserToolStripMenuItem.Text = "Status";
			this.registerUserToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			//
			// updatesToolStripMenuItem
			//
			this.updatesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
			this.updatesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.updatesToolStripMenuItem.Name = "updatesToolStripMenuItem";
			this.updatesToolStripMenuItem.Size = new System.Drawing.Size(72, 23);
			this.updatesToolStripMenuItem.Text = "Updates";
			this.updatesToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			//
			// settingsToolStripMenuItem
			//
			this.settingsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(70, 23);
			this.settingsToolStripMenuItem.Text = "Settings";
			this.settingsToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			//
			// emailServerToolStripMenuItem
			//
			this.emailServerToolStripMenuItem.Name = "emailServerToolStripMenuItem";
			this.emailServerToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
			this.emailServerToolStripMenuItem.Text = "E-mail Server";
			//
			// clientsToolStripMenuItem
			//
			this.clientsToolStripMenuItem.Name = "clientsToolStripMenuItem";
			this.clientsToolStripMenuItem.Size = new System.Drawing.Size(62, 23);
			this.clientsToolStripMenuItem.Text = "Clients";
			//
			// logToolStripMenuItem
			//
			this.logToolStripMenuItem.Name = "logToolStripMenuItem";
			this.logToolStripMenuItem.Size = new System.Drawing.Size(44, 23);
			this.logToolStripMenuItem.Text = "Log";
			//
			// testToolStripMenuItem
			//
			this.testToolStripMenuItem.Name = "testToolStripMenuItem";
			this.testToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
			this.testToolStripMenuItem.Text = "test";
			//
			// imageList
			//
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "Status.png");
			this.imageList.Images.SetKeyName(1, "Client.png");
			this.imageList.Images.SetKeyName(2, "Log.png");
			this.imageList.Images.SetKeyName(3, "Settings.png");
			this.imageList.Images.SetKeyName(4, "Update.png");
			this.imageList.Images.SetKeyName(5, "preferences.png");
			this.imageList.Images.SetKeyName(6, "icon3d.ico");
			//
			// tb3
			//
			this.tb3.Controls.Add(this.gp_ServerMode);
			this.tb3.Controls.Add(this.EmailPanel);
			this.tb3.Controls.Add(this.btnSaveHostPort);
			this.tb3.Controls.Add(this.label39);
			this.tb3.Controls.Add(this.panel3);
			this.tb3.Controls.Add(this.label38);
			this.tb3.Controls.Add(this.panel2);
			this.tb3.Controls.Add(this.btnSave);
			this.tb3.Controls.Add(this.btnTestConn);
			this.tb3.Controls.Add(this.txtDbName);
			this.tb3.Controls.Add(this.txtUser);
			this.tb3.Controls.Add(this.txtPass);
			this.tb3.Controls.Add(this.txtDbServer);
			this.tb3.Controls.Add(this.label32);
			this.tb3.Controls.Add(this.label33);
			this.tb3.Controls.Add(this.label34);
			this.tb3.Controls.Add(this.label35);
			this.tb3.Controls.Add(this.label37);
			this.tb3.Controls.Add(this.panel1);
			this.tb3.Controls.Add(this.btnTestHostPort);
			this.tb3.Controls.Add(this.label3);
			this.tb3.Controls.Add(this.btnSocket);
			this.tb3.Controls.Add(this.lblServiceStatus);
			this.tb3.Controls.Add(this.lblIp);
			this.tb3.Controls.Add(this.lblHostName);
			this.tb3.Controls.Add(this.label14);
			this.tb3.Controls.Add(this.label13);
			this.tb3.Controls.Add(this.txtHostPort);
			this.tb3.Controls.Add(this.label12);
			this.tb3.ImageKey = "Settings.png";
			this.tb3.Location = new System.Drawing.Point(4, 39);
			this.tb3.Name = "tb3";
			this.tb3.Padding = new System.Windows.Forms.Padding(3);
			this.tb3.Size = new System.Drawing.Size(676, 493);
			this.tb3.TabIndex = 2;
			this.tb3.Text = " SETTINGS ";
			this.tb3.UseVisualStyleBackColor = true;
			//
			// gp_ServerMode
			//
			this.gp_ServerMode.Controls.Add(this.rb_mode1);
			this.gp_ServerMode.Controls.Add(this.rb_mode2);
			this.gp_ServerMode.Location = new System.Drawing.Point(40, 219);
			this.gp_ServerMode.Name = "gp_ServerMode";
			this.gp_ServerMode.Size = new System.Drawing.Size(217, 66);
			this.gp_ServerMode.TabIndex = 72;
			this.gp_ServerMode.TabStop = false;
			this.gp_ServerMode.Text = "Server Mode";
			//
			// rb_mode1
			//
			this.rb_mode1.AutoSize = true;
			this.rb_mode1.Checked = true;
			this.rb_mode1.Location = new System.Drawing.Point(7, 19);
			this.rb_mode1.Name = "rb_mode1";
			this.rb_mode1.Size = new System.Drawing.Size(126, 17);
			this.rb_mode1.TabIndex = 70;
			this.rb_mode1.TabStop = true;
			this.rb_mode1.Text = "Microsoft SQL Server";
			this.rb_mode1.UseVisualStyleBackColor = true;
			this.rb_mode1.CheckedChanged += new System.EventHandler(this.rb_mode_CheckedChanged);
			//
			// rb_mode2
			//
			this.rb_mode2.AutoSize = true;
			this.rb_mode2.Location = new System.Drawing.Point(7, 42);
			this.rb_mode2.Name = "rb_mode2";
			this.rb_mode2.Size = new System.Drawing.Size(60, 17);
			this.rb_mode2.TabIndex = 71;
			this.rb_mode2.Text = "MySQL";
			this.rb_mode2.UseVisualStyleBackColor = true;
			this.rb_mode2.CheckedChanged += new System.EventHandler(this.rb_mode_CheckedChanged);
			//
			// EmailPanel
			//
			this.EmailPanel.Controls.Add(this.btnTestEmailServer);
			this.EmailPanel.Controls.Add(portLabel);
			this.EmailPanel.Controls.Add(this.btnSaveEmailsServer);
			this.EmailPanel.Controls.Add(this.txtEmailServerPort);
			this.EmailPanel.Controls.Add(serverLabel);
			this.EmailPanel.Controls.Add(this.ckSSL);
			this.EmailPanel.Controls.Add(this.txtEmailServer);
			this.EmailPanel.Controls.Add(this.ckSend);
			this.EmailPanel.Controls.Add(email_accountLabel);
			this.EmailPanel.Controls.Add(this.txtEmailPassword);
			this.EmailPanel.Controls.Add(this.txtEmailAccount);
			this.EmailPanel.Controls.Add(passwordLabel);
			this.EmailPanel.Location = new System.Drawing.Point(0, 359);
			this.EmailPanel.Name = "EmailPanel";
			this.EmailPanel.Size = new System.Drawing.Size(663, 89);
			this.EmailPanel.TabIndex = 8;
			//
			// btnTestEmailServer
			//
			this.btnTestEmailServer.Location = new System.Drawing.Point(457, 63);
			this.btnTestEmailServer.Name = "btnTestEmailServer";
			this.btnTestEmailServer.Size = new System.Drawing.Size(125, 23);
			this.btnTestEmailServer.TabIndex = 29;
			this.btnTestEmailServer.Text = "Sending Test";
			this.btnTestEmailServer.UseVisualStyleBackColor = true;
			this.btnTestEmailServer.Click += new System.EventHandler(this.btnTestEmailServer_Click);
			//
			// btnSaveEmailsServer
			//
			this.btnSaveEmailsServer.Location = new System.Drawing.Point(588, 63);
			this.btnSaveEmailsServer.Name = "btnSaveEmailsServer";
			this.btnSaveEmailsServer.Size = new System.Drawing.Size(75, 23);
			this.btnSaveEmailsServer.TabIndex = 28;
			this.btnSaveEmailsServer.Text = "Save";
			this.btnSaveEmailsServer.UseVisualStyleBackColor = true;
			this.btnSaveEmailsServer.Click += new System.EventHandler(this.btnSaveEmailsServer_Click);
			//
			// txtEmailServerPort
			//
			this.txtEmailServerPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtEmailServerPort.Location = new System.Drawing.Point(413, 33);
			this.txtEmailServerPort.MaxLength = 5;
			this.txtEmailServerPort.Name = "txtEmailServerPort";
			this.txtEmailServerPort.Size = new System.Drawing.Size(65, 20);
			this.txtEmailServerPort.TabIndex = 64;
			this.txtEmailServerPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOnlyDigit_KeyPress);
			//
			// ckSSL
			//
			this.ckSSL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ckSSL.ForeColor = System.Drawing.SystemColors.ControlText;
			this.ckSSL.Location = new System.Drawing.Point(104, 54);
			this.ckSSL.Name = "ckSSL";
			this.ckSSL.Size = new System.Drawing.Size(61, 24);
			this.ckSSL.TabIndex = 65;
			this.ckSSL.Text = "SSL";
			this.ckSSL.UseVisualStyleBackColor = true;
			//
			// txtEmailServer
			//
			this.txtEmailServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtEmailServer.Location = new System.Drawing.Point(104, 9);
			this.txtEmailServer.MaxLength = 50;
			this.txtEmailServer.Name = "txtEmailServer";
			this.txtEmailServer.Size = new System.Drawing.Size(212, 20);
			this.txtEmailServer.TabIndex = 58;
			//
			// ckSend
			//
			this.ckSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ckSend.ForeColor = System.Drawing.SystemColors.ControlText;
			this.ckSend.Location = new System.Drawing.Point(170, 54);
			this.ckSend.Name = "ckSend";
			this.ckSend.Size = new System.Drawing.Size(71, 24);
			this.ckSend.TabIndex = 66;
			this.ckSend.Text = "Send";
			this.ckSend.UseVisualStyleBackColor = true;
			//
			// txtEmailPassword
			//
			this.txtEmailPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtEmailPassword.Location = new System.Drawing.Point(104, 33);
			this.txtEmailPassword.MaxLength = 50;
			this.txtEmailPassword.Name = "txtEmailPassword";
			this.txtEmailPassword.PasswordChar = '*';
			this.txtEmailPassword.Size = new System.Drawing.Size(212, 20);
			this.txtEmailPassword.TabIndex = 62;
			//
			// txtEmailAccount
			//
			this.txtEmailAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtEmailAccount.Location = new System.Drawing.Point(413, 9);
			this.txtEmailAccount.MaxLength = 50;
			this.txtEmailAccount.Name = "txtEmailAccount";
			this.txtEmailAccount.Size = new System.Drawing.Size(212, 20);
			this.txtEmailAccount.TabIndex = 60;
			//
			// btnSaveHostPort
			//
			this.btnSaveHostPort.Enabled = false;
			this.btnSaveHostPort.Location = new System.Drawing.Point(593, 72);
			this.btnSaveHostPort.Name = "btnSaveHostPort";
			this.btnSaveHostPort.Size = new System.Drawing.Size(75, 23);
			this.btnSaveHostPort.TabIndex = 69;
			this.btnSaveHostPort.Text = "Save";
			this.btnSaveHostPort.UseVisualStyleBackColor = true;
			this.btnSaveHostPort.Click += new System.EventHandler(this.btnSaveHostPort_Click);
			//
			// label39
			//
			this.label39.AutoSize = true;
			this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label39.Location = new System.Drawing.Point(10, 339);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(93, 17);
			this.label39.TabIndex = 56;
			this.label39.Text = "E-mail Server";
			//
			// panel3
			//
			this.panel3.BackColor = System.Drawing.Color.White;
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.lblMsgEmail);
			this.panel3.Controls.Add(this.pBoxloadingEmailServer);
			this.panel3.Location = new System.Drawing.Point(0, 451);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(701, 35);
			this.panel3.TabIndex = 55;
			//
			// lblMsgEmail
			//
			this.lblMsgEmail.AutoSize = true;
			this.lblMsgEmail.Location = new System.Drawing.Point(56, 7);
			this.lblMsgEmail.Name = "lblMsgEmail";
			this.lblMsgEmail.Size = new System.Drawing.Size(62, 13);
			this.lblMsgEmail.TabIndex = 7;
			this.lblMsgEmail.Text = "lblMsgEmail";
			this.lblMsgEmail.Visible = false;
			//
			// pBoxloadingEmailServer
			//
			this.pBoxloadingEmailServer.Image = ((System.Drawing.Image)(resources.GetObject("pBoxloadingEmailServer.Image")));
			this.pBoxloadingEmailServer.Location = new System.Drawing.Point(27, 3);
			this.pBoxloadingEmailServer.Name = "pBoxloadingEmailServer";
			this.pBoxloadingEmailServer.Size = new System.Drawing.Size(24, 24);
			this.pBoxloadingEmailServer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pBoxloadingEmailServer.TabIndex = 6;
			this.pBoxloadingEmailServer.TabStop = false;
			this.pBoxloadingEmailServer.Visible = false;
			//
			// label38
			//
			this.label38.AutoSize = true;
			this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label38.Location = new System.Drawing.Point(10, 145);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(69, 17);
			this.label38.TabIndex = 54;
			this.label38.Text = "Database";
			//
			// panel2
			//
			this.panel2.BackColor = System.Drawing.Color.White;
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.lblMsg);
			this.panel2.Controls.Add(this.pBoxLoading);
			this.panel2.Location = new System.Drawing.Point(-4, 291);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(696, 35);
			this.panel2.TabIndex = 53;
			//
			// lblMsg
			//
			this.lblMsg.AutoSize = true;
			this.lblMsg.Location = new System.Drawing.Point(56, 7);
			this.lblMsg.Name = "lblMsg";
			this.lblMsg.Size = new System.Drawing.Size(37, 13);
			this.lblMsg.TabIndex = 7;
			this.lblMsg.Text = "lblMsg";
			this.lblMsg.Visible = false;
			//
			// pBoxLoading
			//
			this.pBoxLoading.Image = ((System.Drawing.Image)(resources.GetObject("pBoxLoading.Image")));
			this.pBoxLoading.Location = new System.Drawing.Point(22, 4);
			this.pBoxLoading.Name = "pBoxLoading";
			this.pBoxLoading.Size = new System.Drawing.Size(24, 24);
			this.pBoxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pBoxLoading.TabIndex = 6;
			this.pBoxLoading.TabStop = false;
			this.pBoxLoading.Visible = false;
			//
			// btnSave
			//
			this.btnSave.Enabled = false;
			this.btnSave.Location = new System.Drawing.Point(593, 262);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 52;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			//
			// btnTestConn
			//
			this.btnTestConn.Location = new System.Drawing.Point(462, 262);
			this.btnTestConn.Name = "btnTestConn";
			this.btnTestConn.Size = new System.Drawing.Size(125, 23);
			this.btnTestConn.TabIndex = 51;
			this.btnTestConn.Text = "Test Connection";
			this.btnTestConn.UseVisualStyleBackColor = true;
			this.btnTestConn.Click += new System.EventHandler(this.btnTestConn_Click);
			//
			// txtDbName
			//
			this.txtDbName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtDbName.Location = new System.Drawing.Point(109, 193);
			this.txtDbName.MaxLength = 100;
			this.txtDbName.Name = "txtDbName";
			this.txtDbName.Size = new System.Drawing.Size(217, 20);
			this.txtDbName.TabIndex = 44;
			this.txtDbName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDbControls_KeyUp);
			//
			// txtUser
			//
			this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtUser.Location = new System.Drawing.Point(418, 169);
			this.txtUser.MaxLength = 100;
			this.txtUser.Name = "txtUser";
			this.txtUser.Size = new System.Drawing.Size(212, 20);
			this.txtUser.TabIndex = 46;
			this.txtUser.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDbControls_KeyUp);
			//
			// txtPass
			//
			this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPass.Location = new System.Drawing.Point(418, 193);
			this.txtPass.MaxLength = 100;
			this.txtPass.Name = "txtPass";
			this.txtPass.PasswordChar = '*';
			this.txtPass.Size = new System.Drawing.Size(212, 20);
			this.txtPass.TabIndex = 48;
			this.txtPass.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDbControls_KeyUp);
			//
			// txtDbServer
			//
			this.txtDbServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtDbServer.Location = new System.Drawing.Point(109, 169);
			this.txtDbServer.MaxLength = 100;
			this.txtDbServer.Name = "txtDbServer";
			this.txtDbServer.Size = new System.Drawing.Size(217, 20);
			this.txtDbServer.TabIndex = 43;
			this.txtDbServer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDbControls_KeyUp);
			//
			// label32
			//
			this.label32.AutoSize = true;
			this.label32.Location = new System.Drawing.Point(9, 196);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(87, 13);
			this.label32.TabIndex = 50;
			this.label32.Text = "Database Name:";
			//
			// label33
			//
			this.label33.AutoSize = true;
			this.label33.Location = new System.Drawing.Point(356, 176);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(32, 13);
			this.label33.TabIndex = 49;
			this.label33.Text = "User:";
			//
			// label34
			//
			this.label34.AutoSize = true;
			this.label34.Location = new System.Drawing.Point(356, 200);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(56, 13);
			this.label34.TabIndex = 47;
			this.label34.Text = "Password:";
			//
			// label35
			//
			this.label35.AutoSize = true;
			this.label35.Location = new System.Drawing.Point(9, 172);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(90, 13);
			this.label35.TabIndex = 45;
			this.label35.Text = "Database Server:";
			//
			// label37
			//
			this.label37.AutoSize = true;
			this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label37.Location = new System.Drawing.Point(10, 10);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(76, 17);
			this.label37.TabIndex = 42;
			this.label37.Text = "Host name";
			//
			// panel1
			//
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.lblMsgHost);
			this.panel1.Controls.Add(this.pBoxloadinbgHostPort);
			this.panel1.Location = new System.Drawing.Point(-4, 100);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(696, 35);
			this.panel1.TabIndex = 41;
			//
			// lblMsgHost
			//
			this.lblMsgHost.AutoSize = true;
			this.lblMsgHost.Location = new System.Drawing.Point(56, 7);
			this.lblMsgHost.Name = "lblMsgHost";
			this.lblMsgHost.Size = new System.Drawing.Size(59, 13);
			this.lblMsgHost.TabIndex = 7;
			this.lblMsgHost.Text = "lblMsgHost";
			this.lblMsgHost.Visible = false;
			//
			// pBoxloadinbgHostPort
			//
			this.pBoxloadinbgHostPort.Image = ((System.Drawing.Image)(resources.GetObject("pBoxloadinbgHostPort.Image")));
			this.pBoxloadinbgHostPort.Location = new System.Drawing.Point(22, 4);
			this.pBoxloadinbgHostPort.Name = "pBoxloadinbgHostPort";
			this.pBoxloadinbgHostPort.Size = new System.Drawing.Size(24, 24);
			this.pBoxloadinbgHostPort.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pBoxloadinbgHostPort.TabIndex = 6;
			this.pBoxloadinbgHostPort.TabStop = false;
			this.pBoxloadinbgHostPort.Visible = false;
			//
			// btnTestHostPort
			//
			this.btnTestHostPort.Location = new System.Drawing.Point(462, 72);
			this.btnTestHostPort.Name = "btnTestHostPort";
			this.btnTestHostPort.Size = new System.Drawing.Size(125, 23);
			this.btnTestHostPort.TabIndex = 40;
			this.btnTestHostPort.Text = "Test Port Availability";
			this.btnTestHostPort.UseVisualStyleBackColor = true;
			this.btnTestHostPort.Click += new System.EventHandler(this.btnTestHostPort_Click);
			//
			// label3
			//
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(259, 50);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(52, 17);
			this.label3.TabIndex = 38;
			this.label3.Text = "Status:";
			//
			// btnSocket
			//
			this.btnSocket.Location = new System.Drawing.Point(259, 71);
			this.btnSocket.Name = "btnSocket";
			this.btnSocket.Size = new System.Drawing.Size(110, 25);
			this.btnSocket.TabIndex = 36;
			this.btnSocket.Text = "Start Service";
			this.btnSocket.UseVisualStyleBackColor = true;
			this.btnSocket.Click += new System.EventHandler(this.btnSocket_Click);
			//
			// lblServiceStatus
			//
			this.lblServiceStatus.AutoSize = true;
			this.lblServiceStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblServiceStatus.ForeColor = System.Drawing.Color.RoyalBlue;
			this.lblServiceStatus.Location = new System.Drawing.Point(308, 50);
			this.lblServiceStatus.Name = "lblServiceStatus";
			this.lblServiceStatus.Size = new System.Drawing.Size(61, 17);
			this.lblServiceStatus.TabIndex = 37;
			this.lblServiceStatus.Text = "Stopped";
			//
			// lblIp
			//
			this.lblIp.AutoSize = true;
			this.lblIp.ForeColor = System.Drawing.Color.RoyalBlue;
			this.lblIp.Location = new System.Drawing.Point(106, 58);
			this.lblIp.Name = "lblIp";
			this.lblIp.Size = new System.Drawing.Size(26, 13);
			this.lblIp.TabIndex = 35;
			this.lblIp.Text = "lblIp";
			//
			// lblHostName
			//
			this.lblHostName.AutoSize = true;
			this.lblHostName.ForeColor = System.Drawing.Color.RoyalBlue;
			this.lblHostName.Location = new System.Drawing.Point(106, 39);
			this.lblHostName.Name = "lblHostName";
			this.lblHostName.Size = new System.Drawing.Size(67, 13);
			this.lblHostName.TabIndex = 34;
			this.lblHostName.Text = "lblHostName";
			//
			// label14
			//
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(12, 83);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(29, 13);
			this.label14.TabIndex = 33;
			this.label14.Text = "Port:";
			//
			// label13
			//
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(12, 38);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(61, 13);
			this.label13.TabIndex = 32;
			this.label13.Text = "Host name:";
			//
			// txtHostPort
			//
			this.txtHostPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtHostPort.Location = new System.Drawing.Point(109, 76);
			this.txtHostPort.MaxLength = 8;
			this.txtHostPort.Name = "txtHostPort";
			this.txtHostPort.Size = new System.Drawing.Size(74, 20);
			this.txtHostPort.TabIndex = 31;
			this.txtHostPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOnlyDigit_KeyPress);
			this.txtHostPort.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtHostPort_KeyUp);
			//
			// label12
			//
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(12, 62);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(20, 13);
			this.label12.TabIndex = 30;
			this.label12.Text = "IP:";
			//
			// tb2
			//
			this.tb2.Controls.Add(this.pBoxLoadinbgUpdate);
			this.tb2.Controls.Add(this.panel6);
			this.tb2.Controls.Add(this.panel5);
			this.tb2.Controls.Add(this.btnInstallUpdate);
			this.tb2.Controls.Add(this.lblMsgUpdate);
			this.tb2.Controls.Add(this.label16);
			this.tb2.Controls.Add(this.label11);
			this.tb2.Controls.Add(this.lblProgress);
			this.tb2.Controls.Add(this.prgDownload);
			this.tb2.Controls.Add(this.btnCheckUpdates);
			this.tb2.Controls.Add(this.label25);
			this.tb2.Controls.Add(this.lblLastTimeChecked);
			this.tb2.Controls.Add(this.label40);
			this.tb2.Controls.Add(this.lblCurrentVersion);
			this.tb2.ImageKey = "Update.png";
			this.tb2.Location = new System.Drawing.Point(4, 39);
			this.tb2.Name = "tb2";
			this.tb2.Padding = new System.Windows.Forms.Padding(3);
			this.tb2.Size = new System.Drawing.Size(676, 493);
			this.tb2.TabIndex = 1;
			this.tb2.Text = " UPDATE   ";
			this.tb2.UseVisualStyleBackColor = true;
			//
			// pBoxLoadinbgUpdate
			//
			this.pBoxLoadinbgUpdate.Image = ((System.Drawing.Image)(resources.GetObject("pBoxLoadinbgUpdate.Image")));
			this.pBoxLoadinbgUpdate.Location = new System.Drawing.Point(311, 178);
			this.pBoxLoadinbgUpdate.Name = "pBoxLoadinbgUpdate";
			this.pBoxLoadinbgUpdate.Size = new System.Drawing.Size(24, 24);
			this.pBoxLoadinbgUpdate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pBoxLoadinbgUpdate.TabIndex = 77;
			this.pBoxLoadinbgUpdate.TabStop = false;
			this.pBoxLoadinbgUpdate.Visible = false;
			//
			// panel6
			//
			this.panel6.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.panel6.Location = new System.Drawing.Point(11, 126);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(406, 1);
			this.panel6.TabIndex = 75;
			//
			// panel5
			//
			this.panel5.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.panel5.Location = new System.Drawing.Point(9, 26);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(406, 1);
			this.panel5.TabIndex = 74;
			//
			// btnInstallUpdate
			//
			this.btnInstallUpdate.Enabled = false;
			this.btnInstallUpdate.Location = new System.Drawing.Point(212, 178);
			this.btnInstallUpdate.Name = "btnInstallUpdate";
			this.btnInstallUpdate.Size = new System.Drawing.Size(93, 23);
			this.btnInstallUpdate.TabIndex = 34;
			this.btnInstallUpdate.Text = "Apply";
			this.btnInstallUpdate.UseVisualStyleBackColor = true;
			this.btnInstallUpdate.Click += new System.EventHandler(this.btnInstallUpdate_Click);
			//
			// lblMsgUpdate
			//
			this.lblMsgUpdate.AutoSize = true;
			this.lblMsgUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMsgUpdate.Location = new System.Drawing.Point(16, 216);
			this.lblMsgUpdate.Name = "lblMsgUpdate";
			this.lblMsgUpdate.Size = new System.Drawing.Size(90, 15);
			this.lblMsgUpdate.TabIndex = 33;
			this.lblMsgUpdate.Text = "lblMsgUpdates";
			this.lblMsgUpdate.Visible = false;
			//
			// label16
			//
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label16.Location = new System.Drawing.Point(6, 104);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(132, 18);
			this.label16.TabIndex = 32;
			this.label16.Text = "Updates Availables";
			//
			// label11
			//
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(6, 7);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(53, 18);
			this.label11.TabIndex = 31;
			this.label11.Text = "Details";
			//
			// lblProgress
			//
			this.lblProgress.AutoSize = true;
			this.lblProgress.Enabled = false;
			this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblProgress.Location = new System.Drawing.Point(14, 134);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(114, 15);
			this.lblProgress.TabIndex = 30;
			this.lblProgress.Text = "Download progress";
			//
			// prgDownload
			//
			this.prgDownload.Enabled = false;
			this.prgDownload.Location = new System.Drawing.Point(16, 152);
			this.prgDownload.Name = "prgDownload";
			this.prgDownload.Size = new System.Drawing.Size(401, 20);
			this.prgDownload.TabIndex = 29;
			//
			// btnCheckUpdates
			//
			this.btnCheckUpdates.Enabled = false;
			this.btnCheckUpdates.Location = new System.Drawing.Point(16, 178);
			this.btnCheckUpdates.Name = "btnCheckUpdates";
			this.btnCheckUpdates.Size = new System.Drawing.Size(190, 23);
			this.btnCheckUpdates.TabIndex = 28;
			this.btnCheckUpdates.Text = "Check/Download Updates";
			this.btnCheckUpdates.UseVisualStyleBackColor = true;
			this.btnCheckUpdates.Click += new System.EventHandler(this.btnCheckUpdates_Click);
			//
			// label25
			//
			this.label25.AutoSize = true;
			this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label25.Location = new System.Drawing.Point(16, 61);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(109, 15);
			this.label25.TabIndex = 25;
			this.label25.Text = "Last time checked:";
			//
			// lblLastTimeChecked
			//
			this.lblLastTimeChecked.AutoSize = true;
			this.lblLastTimeChecked.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLastTimeChecked.ForeColor = System.Drawing.Color.RoyalBlue;
			this.lblLastTimeChecked.Location = new System.Drawing.Point(130, 61);
			this.lblLastTimeChecked.Name = "lblLastTimeChecked";
			this.lblLastTimeChecked.Size = new System.Drawing.Size(19, 15);
			this.lblLastTimeChecked.TabIndex = 24;
			this.lblLastTimeChecked.Text = "---";
			//
			// label40
			//
			this.label40.AutoSize = true;
			this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label40.Location = new System.Drawing.Point(16, 39);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(94, 15);
			this.label40.TabIndex = 23;
			this.label40.Text = "Current Version:";
			//
			// lblCurrentVersion
			//
			this.lblCurrentVersion.AutoSize = true;
			this.lblCurrentVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCurrentVersion.ForeColor = System.Drawing.Color.RoyalBlue;
			this.lblCurrentVersion.Location = new System.Drawing.Point(130, 39);
			this.lblCurrentVersion.Name = "lblCurrentVersion";
			this.lblCurrentVersion.Size = new System.Drawing.Size(19, 15);
			this.lblCurrentVersion.TabIndex = 22;
			this.lblCurrentVersion.Text = "---";
			//
			// tbControl
			//
			this.tbControl.Controls.Add(this.tb1);
			this.tbControl.Controls.Add(this.tb3);
			this.tbControl.Controls.Add(this.tb6);
			this.tbControl.Controls.Add(this.tb2);
			this.tbControl.Controls.Add(this.tb7);
			this.tbControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbControl.ImageList = this.imageList;
			this.tbControl.Location = new System.Drawing.Point(0, 0);
			this.tbControl.Name = "tbControl";
			this.tbControl.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tbControl.SelectedIndex = 0;
			this.tbControl.Size = new System.Drawing.Size(684, 536);
			this.tbControl.TabIndex = 1;
			//
			// tb1
			//
			this.tb1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tb1.Controls.Add(this.panelActive);
			this.tb1.Controls.Add(this.label30);
			this.tb1.Controls.Add(this.lblMsgServiceStatus);
			this.tb1.Controls.Add(this.label1);
			this.tb1.Controls.Add(this.label2);
			this.tb1.ImageIndex = 0;
			this.tb1.Location = new System.Drawing.Point(4, 39);
			this.tb1.Name = "tb1";
			this.tb1.Padding = new System.Windows.Forms.Padding(3);
			this.tb1.Size = new System.Drawing.Size(676, 493);
			this.tb1.TabIndex = 0;
			this.tb1.Text = " STATUS    ";
			//
			// panelActive
			//
			this.panelActive.BackColor = System.Drawing.Color.LightCoral;
			this.panelActive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelActive.Controls.Add(this.maskProduct_key);
			this.panelActive.Controls.Add(this.label26);
			this.panelActive.Controls.Add(this.lblActivation);
			this.panelActive.Controls.Add(this.btnActiveProduct);
			this.panelActive.Controls.Add(this.txtClientId);
			this.panelActive.Controls.Add(this.label19);
			this.panelActive.Location = new System.Drawing.Point(3, 6);
			this.panelActive.Name = "panelActive";
			this.panelActive.Size = new System.Drawing.Size(670, 40);
			this.panelActive.TabIndex = 71;
			//
			// maskProduct_key
			//
			this.maskProduct_key.Location = new System.Drawing.Point(206, 7);
			this.maskProduct_key.Mask = "9999-9999";
			this.maskProduct_key.Name = "maskProduct_key";
			this.maskProduct_key.Size = new System.Drawing.Size(60, 20);
			this.maskProduct_key.TabIndex = 1;
			//
			// label26
			//
			this.label26.AutoSize = true;
			this.label26.BackColor = System.Drawing.Color.Transparent;
			this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label26.Location = new System.Drawing.Point(131, 10);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(73, 15);
			this.label26.TabIndex = 40;
			this.label26.Text = "Product key:";
			//
			// lblActivation
			//
			this.lblActivation.AutoSize = true;
			this.lblActivation.BackColor = System.Drawing.Color.Transparent;
			this.lblActivation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblActivation.Location = new System.Drawing.Point(352, 8);
			this.lblActivation.Name = "lblActivation";
			this.lblActivation.Size = new System.Drawing.Size(0, 17);
			this.lblActivation.TabIndex = 39;
			//
			// btnActiveProduct
			//
			this.btnActiveProduct.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.btnActiveProduct.Location = new System.Drawing.Point(278, 7);
			this.btnActiveProduct.Name = "btnActiveProduct";
			this.btnActiveProduct.Size = new System.Drawing.Size(70, 21);
			this.btnActiveProduct.TabIndex = 2;
			this.btnActiveProduct.Text = "Activation";
			this.btnActiveProduct.UseVisualStyleBackColor = false;
			this.btnActiveProduct.Click += new System.EventHandler(this.btnActiveProduct_Click);
			//
			// txtClientId
			//
			this.txtClientId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtClientId.Location = new System.Drawing.Point(61, 7);
			this.txtClientId.MaxLength = 8;
			this.txtClientId.Name = "txtClientId";
			this.txtClientId.Size = new System.Drawing.Size(64, 20);
			this.txtClientId.TabIndex = 0;
			//
			// label19
			//
			this.label19.AutoSize = true;
			this.label19.BackColor = System.Drawing.Color.Transparent;
			this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label19.Location = new System.Drawing.Point(3, 10);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(56, 15);
			this.label19.TabIndex = 7;
			this.label19.Text = "Client ID:";
			//
			// label30
			//
			this.label30.AutoSize = true;
			this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label30.Location = new System.Drawing.Point(9, 80);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(44, 15);
			this.label30.TabIndex = 30;
			this.label30.Text = "Status:";
			//
			// lblMsgServiceStatus
			//
			this.lblMsgServiceStatus.AutoSize = true;
			this.lblMsgServiceStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMsgServiceStatus.ForeColor = System.Drawing.Color.RoyalBlue;
			this.lblMsgServiceStatus.Location = new System.Drawing.Point(120, 80);
			this.lblMsgServiceStatus.Name = "lblMsgServiceStatus";
			this.lblMsgServiceStatus.Size = new System.Drawing.Size(32, 15);
			this.lblMsgServiceStatus.TabIndex = 25;
			this.lblMsgServiceStatus.Text = "Stop";
			//
			// label1
			//
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(6, 50);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "Service";
			//
			// label2
			//
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.DarkGray;
			this.label2.Location = new System.Drawing.Point(6, 60);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(664, 20);
			this.label2.TabIndex = 5;
			this.label2.Text = "---------------------------------------------------------------------------------" +
    "--------------------------------------------------";
			//
			// tb6
			//
			this.tb6.AutoScroll = true;
			this.tb6.Controls.Add(this.GrpFooterMenu);
			this.tb6.Controls.Add(this.ckFooter);
			this.tb6.Controls.Add(this.ckAllowDuplicatedFilesNames);
			this.tb6.Controls.Add(this.txtWebServiceAddress);
			this.tb6.Controls.Add(label29);
			this.tb6.Controls.Add(this.ckAllowWorkSpace);
			this.tb6.Controls.Add(this.ckReasonNewVersion);
			this.tb6.Controls.Add(this.label23);
			this.tb6.Controls.Add(this.label27);
			this.tb6.Controls.Add(this.txtMax_docs);
			this.tb6.Controls.Add(this.txtMax_recents);
			this.tb6.Controls.Add(this.ckShow_watermarks);
			this.tb6.Controls.Add(this.btnSaveOptions);
			this.tb6.Controls.Add(max_docsLabel);
			this.tb6.Controls.Add(max_recentsLabel);
			this.tb6.ImageKey = "preferences.png";
			this.tb6.Location = new System.Drawing.Point(4, 39);
			this.tb6.Name = "tb6";
			this.tb6.Size = new System.Drawing.Size(676, 493);
			this.tb6.TabIndex = 5;
			this.tb6.Text = " OPTIONS  ";
			this.tb6.UseVisualStyleBackColor = true;
			//
			// GrpFooterMenu
			//
			this.GrpFooterMenu.Controls.Add(this.rbFooter_With);
			this.GrpFooterMenu.Controls.Add(this.rbFooter_Option);
			this.GrpFooterMenu.Location = new System.Drawing.Point(24, 189);
			this.GrpFooterMenu.Name = "GrpFooterMenu";
			this.GrpFooterMenu.Size = new System.Drawing.Size(215, 66);
			this.GrpFooterMenu.TabIndex = 1015;
			this.GrpFooterMenu.TabStop = false;
			this.GrpFooterMenu.Text = "Footer Menu";
			//
			// rbFooter_With
			//
			this.rbFooter_With.AutoSize = true;
			this.rbFooter_With.Location = new System.Drawing.Point(14, 42);
			this.rbFooter_With.Name = "rbFooter_With";
			this.rbFooter_With.Size = new System.Drawing.Size(170, 17);
			this.rbFooter_With.TabIndex = 1;
			this.rbFooter_With.TabStop = true;
			this.rbFooter_With.Text = "Always check out with a footer";
			this.rbFooter_With.UseVisualStyleBackColor = true;
			//
			// rbFooter_Option
			//
			this.rbFooter_Option.AutoSize = true;
			this.rbFooter_Option.Location = new System.Drawing.Point(14, 19);
			this.rbFooter_Option.Name = "rbFooter_Option";
			this.rbFooter_Option.Size = new System.Drawing.Size(86, 17);
			this.rbFooter_Option.TabIndex = 0;
			this.rbFooter_Option.TabStop = true;
			this.rbFooter_Option.Text = "Show Option";
			this.rbFooter_Option.UseVisualStyleBackColor = true;
			//
			// ckFooter
			//
			this.ckFooter.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.ckFooter.Location = new System.Drawing.Point(6, 164);
			this.ckFooter.Name = "ckFooter";
			this.ckFooter.Size = new System.Drawing.Size(220, 24);
			this.ckFooter.TabIndex = 1014;
			this.ckFooter.Text = "Add document information as a footer:";
			this.ckFooter.UseVisualStyleBackColor = true;
			this.ckFooter.CheckedChanged += new System.EventHandler(this.ckFooter_CheckedChanged);
			//
			// ckAllowDuplicatedFilesNames
			//
			this.ckAllowDuplicatedFilesNames.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.ckAllowDuplicatedFilesNames.Location = new System.Drawing.Point(6, 134);
			this.ckAllowDuplicatedFilesNames.Name = "ckAllowDuplicatedFilesNames";
			this.ckAllowDuplicatedFilesNames.Size = new System.Drawing.Size(220, 24);
			this.ckAllowDuplicatedFilesNames.TabIndex = 80;
			this.ckAllowDuplicatedFilesNames.Text = "Allow users to save files with same name:";
			this.ckAllowDuplicatedFilesNames.UseVisualStyleBackColor = true;
			//
			// txtWebServiceAddress
			//
			this.txtWebServiceAddress.Location = new System.Drawing.Point(211, 329);
			this.txtWebServiceAddress.MaxLength = 300;
			this.txtWebServiceAddress.Name = "txtWebServiceAddress";
			this.txtWebServiceAddress.Size = new System.Drawing.Size(400, 20);
			this.txtWebServiceAddress.TabIndex = 79;
			//
			// ckAllowWorkSpace
			//
			this.ckAllowWorkSpace.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.ckAllowWorkSpace.Location = new System.Drawing.Point(6, 104);
			this.ckAllowWorkSpace.Name = "ckAllowWorkSpace";
			this.ckAllowWorkSpace.Size = new System.Drawing.Size(220, 24);
			this.ckAllowWorkSpace.TabIndex = 77;
			this.ckAllowWorkSpace.Text = "Allow users to import files to workspace:";
			this.ckAllowWorkSpace.UseVisualStyleBackColor = true;
			//
			// ckReasonNewVersion
			//
			this.ckReasonNewVersion.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.ckReasonNewVersion.Location = new System.Drawing.Point(6, 74);
			this.ckReasonNewVersion.Name = "ckReasonNewVersion";
			this.ckReasonNewVersion.Size = new System.Drawing.Size(220, 24);
			this.ckReasonNewVersion.TabIndex = 76;
			this.ckReasonNewVersion.Text = "Require reason to save new version:";
			this.ckReasonNewVersion.UseVisualStyleBackColor = true;
			//
			// label23
			//
			this.label23.AutoSize = true;
			this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label23.Location = new System.Drawing.Point(8, 12);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(116, 18);
			this.label23.TabIndex = 32;
			this.label23.Text = "General Options";
			//
			// label27
			//
			this.label27.AutoSize = true;
			this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label27.ForeColor = System.Drawing.Color.DarkGray;
			this.label27.Location = new System.Drawing.Point(4, 23);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(664, 20);
			this.label27.TabIndex = 74;
			this.label27.Text = "---------------------------------------------------------------------------------" +
    "--------------------------------------------------";
			//
			// txtMax_docs
			//
			this.txtMax_docs.Location = new System.Drawing.Point(211, 275);
			this.txtMax_docs.MaxLength = 4;
			this.txtMax_docs.Name = "txtMax_docs";
			this.txtMax_docs.Size = new System.Drawing.Size(70, 20);
			this.txtMax_docs.TabIndex = 9;
			this.txtMax_docs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOnlyDigit_KeyPress);
			//
			// txtMax_recents
			//
			this.txtMax_recents.Location = new System.Drawing.Point(211, 302);
			this.txtMax_recents.MaxLength = 4;
			this.txtMax_recents.Name = "txtMax_recents";
			this.txtMax_recents.Size = new System.Drawing.Size(70, 20);
			this.txtMax_recents.TabIndex = 11;
			this.txtMax_recents.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOnlyDigit_KeyPress);
			//
			// ckShow_watermarks
			//
			this.ckShow_watermarks.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.ckShow_watermarks.Location = new System.Drawing.Point(6, 44);
			this.ckShow_watermarks.Name = "ckShow_watermarks";
			this.ckShow_watermarks.Size = new System.Drawing.Size(220, 24);
			this.ckShow_watermarks.TabIndex = 13;
			this.ckShow_watermarks.Text = "Show watermarks at old versions:";
			this.ckShow_watermarks.UseVisualStyleBackColor = true;
			//
			// btnSaveOptions
			//
			this.btnSaveOptions.Enabled = false;
			this.btnSaveOptions.Location = new System.Drawing.Point(584, 394);
			this.btnSaveOptions.Name = "btnSaveOptions";
			this.btnSaveOptions.Size = new System.Drawing.Size(75, 23);
			this.btnSaveOptions.TabIndex = 4;
			this.btnSaveOptions.Text = "Save";
			this.btnSaveOptions.UseVisualStyleBackColor = true;
			this.btnSaveOptions.Click += new System.EventHandler(this.btnSaveOptions_Click);
			//
			// tb7
			//
			this.tb7.Controls.Add(this.linkLabel1);
			this.tb7.Controls.Add(this.label17);
			this.tb7.Controls.Add(this.label21);
			this.tb7.Controls.Add(this.label28);
			this.tb7.Controls.Add(this.lblVersion);
			this.tb7.Controls.Add(this.pictureBox1);
			this.tb7.ImageKey = "icon3d.ico";
			this.tb7.Location = new System.Drawing.Point(4, 39);
			this.tb7.Name = "tb7";
			this.tb7.Padding = new System.Windows.Forms.Padding(3);
			this.tb7.Size = new System.Drawing.Size(676, 493);
			this.tb7.TabIndex = 6;
			this.tb7.Text = "ABOUT";
			this.tb7.UseVisualStyleBackColor = true;
			//
			// linkLabel1
			//
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point(73, 203);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(169, 13);
			this.linkLabel1.TabIndex = 63;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = " www.spiderdevelopments.com.au";
			//
			// label17
			//
			this.label17.AutoSize = true;
			this.label17.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label17.Location = new System.Drawing.Point(27, 204);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(49, 13);
			this.label17.TabIndex = 62;
			this.label17.Text = "Website:";
			//
			// label21
			//
			this.label21.AutoSize = true;
			this.label21.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label21.Location = new System.Drawing.Point(27, 180);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(204, 13);
			this.label21.TabIndex = 61;
			this.label21.Text = "E-mail: web@spiderdevelopments.com.au";
			//
			// label28
			//
			this.label28.AutoSize = true;
			this.label28.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label28.Location = new System.Drawing.Point(27, 155);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(137, 13);
			this.label28.TabIndex = 60;
			this.label28.Text = "Support: +61 08 9328 7199";
			//
			// lblVersion
			//
			this.lblVersion.AutoSize = true;
			this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblVersion.Location = new System.Drawing.Point(27, 231);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(198, 13);
			this.lblVersion.TabIndex = 59;
			this.lblVersion.Text = "Document Management System - Server";
			//
			// pictureBox1
			//
			this.pictureBox1.Image = global::SpiderDocsServer.Properties.Resources.logospider_about;
			this.pictureBox1.Location = new System.Drawing.Point(18, 19);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(357, 120);
			this.pictureBox1.TabIndex = 58;
			this.pictureBox1.TabStop = false;
			//
			// statusStrip
			//
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsVersion,
            this.toolStripStatusLabel2,
            this.statusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 514);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.statusStrip.Size = new System.Drawing.Size(684, 22);
			this.statusStrip.TabIndex = 22;
			this.statusStrip.Text = "StatusStrip";
			//
			// tsVersion
			//
			this.tsVersion.Name = "tsVersion";
			this.tsVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsVersion.Size = new System.Drawing.Size(92, 17);
			this.tsVersion.Text = "System version: ";
			//
			// toolStripStatusLabel2
			//
			this.toolStripStatusLabel2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabel2.Image")));
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(381, 17);
			this.toolStripStatusLabel2.Spring = true;
			this.toolStripStatusLabel2.Text = "Encrypted";
			//
			// statusLabel
			//
			this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.statusLabel.Image = global::SpiderDocsServer.Properties.Resources.database;
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(196, 17);
			this.statusLabel.Text = "Database Status: Not Connected.";
			//
			// notifyIcon
			//
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Tag = "";
			this.notifyIcon.Text = "Spider Docs";
			this.notifyIcon.Visible = true;
			this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
			this.notifyIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseMove);
			//
			// MenuStrip
			//
			this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDMSToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.MenuStrip.Name = "contextMenuStrip1";
			this.MenuStrip.Size = new System.Drawing.Size(104, 48);
			//
			// openDMSToolStripMenuItem
			//
			this.openDMSToolStripMenuItem.Name = "openDMSToolStripMenuItem";
			this.openDMSToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.openDMSToolStripMenuItem.Text = "Show";
			//
			// exitToolStripMenuItem
			//
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			//
			// frmMain
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoScroll = true;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(684, 536);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.tbControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(700, 575);
			this.MinimumSize = new System.Drawing.Size(700, 575);
			this.Name = "frmMain";
			this.Text = "Spider Docs - Server";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.Resize += new System.EventHandler(this.frmMain_Resize);
			this.tb3.ResumeLayout(false);
			this.tb3.PerformLayout();
			this.gp_ServerMode.ResumeLayout(false);
			this.gp_ServerMode.PerformLayout();
			this.EmailPanel.ResumeLayout(false);
			this.EmailPanel.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pBoxloadingEmailServer)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pBoxLoading)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pBoxloadinbgHostPort)).EndInit();
			this.tb2.ResumeLayout(false);
			this.tb2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pBoxLoadinbgUpdate)).EndInit();
			this.tbControl.ResumeLayout(false);
			this.tb1.ResumeLayout(false);
			this.tb1.PerformLayout();
			this.panelActive.ResumeLayout(false);
			this.panelActive.PerformLayout();
			this.tb6.ResumeLayout(false);
			this.tb6.PerformLayout();
			this.GrpFooterMenu.ResumeLayout(false);
			this.GrpFooterMenu.PerformLayout();
			this.tb7.ResumeLayout(false);
			this.tb7.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.MenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem registerUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emailServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.TabPage tb3;
		private System.Windows.Forms.TabPage tb2;
        private System.Windows.Forms.TabControl tbControl;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsVersion;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblMsgEmail;
        private System.Windows.Forms.PictureBox pBoxloadingEmailServer;
        private System.Windows.Forms.Button btnSaveEmailsServer;
        private System.Windows.Forms.Button btnTestEmailServer;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.PictureBox pBoxLoading;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnTestConn;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtDbServer;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMsgHost;
        private System.Windows.Forms.PictureBox pBoxloadinbgHostPort;
        private System.Windows.Forms.Button btnTestHostPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSocket;
        private System.Windows.Forms.Label lblServiceStatus;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.Label lblHostName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtHostPort;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtEmailServerPort;
        private System.Windows.Forms.TextBox txtEmailServer;
        private System.Windows.Forms.TextBox txtEmailAccount;
        private System.Windows.Forms.TextBox txtEmailPassword;
        private System.Windows.Forms.CheckBox ckSend;
        private System.Windows.Forms.CheckBox ckSSL;
		private System.Windows.Forms.Button btnSaveHostPort;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lblLastTimeChecked;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label lblCurrentVersion;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ProgressBar prgDownload;
        private System.Windows.Forms.Button btnCheckUpdates;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnInstallUpdate;
        private System.Windows.Forms.Label lblMsgUpdate;
        private System.Windows.Forms.TabPage tb6;
        private System.Windows.Forms.Button btnSaveOptions;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.TextBox txtMax_docs;
        private System.Windows.Forms.TextBox txtMax_recents;
        private System.Windows.Forms.CheckBox ckShow_watermarks;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openDMSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label27;
		private System.Windows.Forms.Panel EmailPanel;
        private System.Windows.Forms.CheckBox ckReasonNewVersion;
        private System.Windows.Forms.CheckBox ckAllowWorkSpace;
        private System.Windows.Forms.PictureBox pBoxLoadinbgUpdate;
        private System.Windows.Forms.TabPage tb7;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtWebServiceAddress;
        private System.Windows.Forms.CheckBox ckAllowDuplicatedFilesNames;
		private System.Windows.Forms.GroupBox gp_ServerMode;
		private System.Windows.Forms.RadioButton rb_mode1;
		private System.Windows.Forms.RadioButton rb_mode2;
		private System.Windows.Forms.CheckBox ckFooter;
		private System.Windows.Forms.GroupBox GrpFooterMenu;
		private System.Windows.Forms.RadioButton rbFooter_With;
		private System.Windows.Forms.RadioButton rbFooter_Option;
		private System.Windows.Forms.TabPage tb1;
		private System.Windows.Forms.Panel panelActive;
		private System.Windows.Forms.MaskedTextBox maskProduct_key;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label lblActivation;
		private System.Windows.Forms.Button btnActiveProduct;
		private System.Windows.Forms.TextBox txtClientId;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label lblMsgServiceStatus;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
    }
}

