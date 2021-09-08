namespace SpiderDocs
{
    partial class frmMDI
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMDI));
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.tsUser = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsGroup = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsVersion = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsServer = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.menuStripFake = new System.Windows.Forms.MenuStrip();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.Menu_Document = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_Scan = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_Import = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_WorkSpace = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_Logout = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_Registration = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_Users = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_Folders = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_DocumentAttributes = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_DocumentTypes = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_Groups = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_Favourite = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_NotificationGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_Permissions = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_GroupsOfUsers = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_MenuAcess = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_Permissions_Folders = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_Reports = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_UserLog = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_DeletedDocs = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_ReportBuilder = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_Options = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_ChangePassword = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_Preferences = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_FooterSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_Windows = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_Sync = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_About = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_Help = new System.Windows.Forms.ToolStripMenuItem();
			this.SubMenu_About = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip
			// 
			this.statusStrip.BackColor = System.Drawing.Color.WhiteSmoke;
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsUser,
            this.tsGroup,
            this.tsVersion,
            this.tsServer,
            this.tsStatus});
			this.statusStrip.Location = new System.Drawing.Point(0, 649);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.statusStrip.Size = new System.Drawing.Size(836, 24);
			this.statusStrip.TabIndex = 2;
			this.statusStrip.Text = "StatusStrip";
			// 
			// tsUser
			// 
			this.tsUser.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
			this.tsUser.Name = "tsUser";
			this.tsUser.Size = new System.Drawing.Size(37, 19);
			this.tsUser.Text = "User:";
			// 
			// tsGroup
			// 
			this.tsGroup.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
			this.tsGroup.Name = "tsGroup";
			this.tsGroup.Size = new System.Drawing.Size(47, 19);
			this.tsGroup.Text = "Group:";
			// 
			// tsVersion
			// 
			this.tsVersion.Name = "tsVersion";
			this.tsVersion.Size = new System.Drawing.Size(92, 19);
			this.tsVersion.Text = "System version: ";
			// 
			// tsServer
			// 
			this.tsServer.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
			this.tsServer.Name = "tsServer";
			this.tsServer.Size = new System.Drawing.Size(46, 19);
			this.tsServer.Text = "Server:";
			// 
			// tsStatus
			// 
			this.tsStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
			this.tsStatus.Name = "tsStatus";
			this.tsStatus.Size = new System.Drawing.Size(49, 19);
			this.tsStatus.Text = "Status: ";
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "excel2010.png");
			this.imageList.Images.SetKeyName(1, "excel2003.png");
			this.imageList.Images.SetKeyName(2, "Word2010.png");
			this.imageList.Images.SetKeyName(3, "Word2003.png");
			this.imageList.Images.SetKeyName(4, "PowerPoint2010.png");
			this.imageList.Images.SetKeyName(5, "PowerPoint2003.gif");
			this.imageList.Images.SetKeyName(6, "Windows_Photo_Viewer.png");
			this.imageList.Images.SetKeyName(7, "pdf.jpg");
			this.imageList.Images.SetKeyName(8, "notepad.gif");
			this.imageList.Images.SetKeyName(9, "Outlook2010.png");
			this.imageList.Images.SetKeyName(10, "folder_256.png");
			this.imageList.Images.SetKeyName(11, "notFound.png");
			// 
			// menuStripFake
			// 
			this.menuStripFake.AllowItemReorder = true;
			this.menuStripFake.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.menuStripFake.BackColor = System.Drawing.Color.WhiteSmoke;
			this.menuStripFake.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStripFake.GripMargin = new System.Windows.Forms.Padding(2, 0, 2, 2);
			this.menuStripFake.Location = new System.Drawing.Point(625, 9);
			this.menuStripFake.Name = "menuStripFake";
			this.menuStripFake.Size = new System.Drawing.Size(202, 24);
			this.menuStripFake.TabIndex = 65;
			// 
			// menuStrip
			// 
			this.menuStrip.AllowItemReorder = true;
			this.menuStrip.AllowMerge = false;
			this.menuStrip.BackColor = System.Drawing.Color.WhiteSmoke;
			this.menuStrip.GripMargin = new System.Windows.Forms.Padding(2, 0, 2, 2);
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Document,
            this.Menu_Registration,
            this.Menu_Permissions,
            this.Menu_Reports,
            this.Menu_Options,
            this.Menu_Windows,
            this.Menu_Sync,
            this.Menu_About});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(836, 40);
			this.menuStrip.TabIndex = 63;
			// 
			// Menu_Document
			// 
			this.Menu_Document.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenu_Scan,
            this.SubMenu_Import,
            this.SubMenu_WorkSpace,
            this.SubMenu_Logout,
            this.SubMenu_Exit});
			this.Menu_Document.Enabled = false;
			this.Menu_Document.Image = global::SpiderDocs.Properties.Resources.page;
			this.Menu_Document.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Menu_Document.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.Menu_Document.Name = "Menu_Document";
			this.Menu_Document.Size = new System.Drawing.Size(69, 36);
			this.Menu_Document.Text = "File";
			// 
			// SubMenu_Scan
			// 
			this.SubMenu_Scan.Enabled = false;
			this.SubMenu_Scan.Image = global::SpiderDocs.Properties.Resources.scanner;
			this.SubMenu_Scan.Name = "SubMenu_Scan";
			this.SubMenu_Scan.Size = new System.Drawing.Size(136, 22);
			this.SubMenu_Scan.Text = "Scan";
			this.SubMenu_Scan.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			// 
			// SubMenu_Import
			// 
			this.SubMenu_Import.Enabled = false;
			this.SubMenu_Import.Image = global::SpiderDocs.Properties.Resources.import;
			this.SubMenu_Import.Name = "SubMenu_Import";
			this.SubMenu_Import.Size = new System.Drawing.Size(136, 22);
			this.SubMenu_Import.Text = "Import";
			this.SubMenu_Import.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			// 
			// SubMenu_WorkSpace
			// 
			this.SubMenu_WorkSpace.Enabled = false;
			this.SubMenu_WorkSpace.Image = ((System.Drawing.Image)(resources.GetObject("SubMenu_WorkSpace.Image")));
			this.SubMenu_WorkSpace.Name = "SubMenu_WorkSpace";
			this.SubMenu_WorkSpace.Size = new System.Drawing.Size(136, 22);
			this.SubMenu_WorkSpace.Text = "Work Space";
			this.SubMenu_WorkSpace.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			// 
			// SubMenu_Logout
			// 
			this.SubMenu_Logout.Enabled = false;
			this.SubMenu_Logout.Image = ((System.Drawing.Image)(resources.GetObject("SubMenu_Logout.Image")));
			this.SubMenu_Logout.Name = "SubMenu_Logout";
			this.SubMenu_Logout.Size = new System.Drawing.Size(136, 22);
			this.SubMenu_Logout.Text = "Logout";
			this.SubMenu_Logout.Click += new System.EventHandler(this.SubMenu_Logout_Click);
			// 
			// SubMenu_Exit
			// 
			this.SubMenu_Exit.Enabled = false;
			this.SubMenu_Exit.Image = global::SpiderDocs.Properties.Resources.menu_exit;
			this.SubMenu_Exit.Name = "SubMenu_Exit";
			this.SubMenu_Exit.Size = new System.Drawing.Size(136, 22);
			this.SubMenu_Exit.Text = "Exit";
			this.SubMenu_Exit.Visible = false;
			this.SubMenu_Exit.Click += new System.EventHandler(this.SubMenu_Exit_Click);
			// 
			// Menu_Registration
			// 
			this.Menu_Registration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenu_Users,
            this.SubMenu_Folders,
            this.SubMenu_DocumentAttributes,
            this.SubMenu_DocumentTypes,
            this.SubMenu_Groups,
            this.SubMenu_Favourite,
            this.SubMenu_NotificationGroup});
			this.Menu_Registration.Enabled = false;
			this.Menu_Registration.Image = global::SpiderDocs.Properties.Resources.register;
			this.Menu_Registration.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.Menu_Registration.Name = "Menu_Registration";
			this.Menu_Registration.Size = new System.Drawing.Size(114, 36);
			this.Menu_Registration.Text = "Registration";
			// 
			// SubMenu_Users
			// 
			this.SubMenu_Users.Enabled = false;
			this.SubMenu_Users.Image = global::SpiderDocs.Properties.Resources.icon_user;
			this.SubMenu_Users.Name = "SubMenu_Users";
			this.SubMenu_Users.Size = new System.Drawing.Size(185, 22);
			this.SubMenu_Users.Text = "Users";
			this.SubMenu_Users.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			// 
			// SubMenu_Folders
			// 
			this.SubMenu_Folders.Enabled = false;
			this.SubMenu_Folders.Image = global::SpiderDocs.Properties.Resources.folder;
			this.SubMenu_Folders.Name = "SubMenu_Folders";
			this.SubMenu_Folders.Size = new System.Drawing.Size(185, 22);
			this.SubMenu_Folders.Text = "Folders";
			this.SubMenu_Folders.Visible = false;
			this.SubMenu_Folders.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			// 
			// SubMenu_DocumentAttributes
			// 
			this.SubMenu_DocumentAttributes.Enabled = false;
			this.SubMenu_DocumentAttributes.Image = global::SpiderDocs.Properties.Resources.add_custom_field;
			this.SubMenu_DocumentAttributes.Name = "SubMenu_DocumentAttributes";
			this.SubMenu_DocumentAttributes.Size = new System.Drawing.Size(185, 22);
			this.SubMenu_DocumentAttributes.Text = "Attributes";
			this.SubMenu_DocumentAttributes.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			// 
			// SubMenu_DocumentTypes
			// 
			this.SubMenu_DocumentTypes.Enabled = false;
			this.SubMenu_DocumentTypes.Image = ((System.Drawing.Image)(resources.GetObject("SubMenu_DocumentTypes.Image")));
			this.SubMenu_DocumentTypes.Name = "SubMenu_DocumentTypes";
			this.SubMenu_DocumentTypes.Size = new System.Drawing.Size(185, 22);
			this.SubMenu_DocumentTypes.Text = "Document Types";
			this.SubMenu_DocumentTypes.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			// 
			// SubMenu_Groups
			// 
			this.SubMenu_Groups.Enabled = false;
			this.SubMenu_Groups.Image = global::SpiderDocs.Properties.Resources.group;
			this.SubMenu_Groups.Name = "SubMenu_Groups";
			this.SubMenu_Groups.Size = new System.Drawing.Size(185, 22);
			this.SubMenu_Groups.Text = "Groups\\Departments";
			this.SubMenu_Groups.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			// 
			// SubMenu_Favourite
			// 
			this.SubMenu_Favourite.Image = global::SpiderDocs.Properties.Resources.favourite;
			this.SubMenu_Favourite.Name = "SubMenu_Favourite";
			this.SubMenu_Favourite.Size = new System.Drawing.Size(185, 22);
			this.SubMenu_Favourite.Text = "Favourite";
			this.SubMenu_Favourite.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			// 
			// SubMenu_NotificationGroup
			// 
			this.SubMenu_NotificationGroup.Enabled = false;
			this.SubMenu_NotificationGroup.Image = global::SpiderDocs.Properties.Resources.notification;
			this.SubMenu_NotificationGroup.Name = "SubMenu_NotificationGroup";
			this.SubMenu_NotificationGroup.Size = new System.Drawing.Size(185, 22);
			this.SubMenu_NotificationGroup.Text = "Notification Group";
			this.SubMenu_NotificationGroup.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			// 
			// Menu_Permissions
			// 
			this.Menu_Permissions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenu_GroupsOfUsers,
            this.SubMenu_MenuAcess,
            this.SubMenu_Permissions_Folders});
			this.Menu_Permissions.Enabled = false;
			this.Menu_Permissions.Image = global::SpiderDocs.Properties.Resources.icon_Lock;
			this.Menu_Permissions.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.Menu_Permissions.Name = "Menu_Permissions";
			this.Menu_Permissions.Size = new System.Drawing.Size(114, 36);
			this.Menu_Permissions.Text = "Permissions";
			// 
			// SubMenu_GroupsOfUsers
			// 
			this.SubMenu_GroupsOfUsers.Enabled = false;
			this.SubMenu_GroupsOfUsers.Image = global::SpiderDocs.Properties.Resources.group;
			this.SubMenu_GroupsOfUsers.Name = "SubMenu_GroupsOfUsers";
			this.SubMenu_GroupsOfUsers.Size = new System.Drawing.Size(187, 22);
			this.SubMenu_GroupsOfUsers.Text = "Members of Groups";
			this.SubMenu_GroupsOfUsers.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			// 
			// SubMenu_MenuAcess
			// 
			this.SubMenu_MenuAcess.Enabled = false;
			this.SubMenu_MenuAcess.Image = global::SpiderDocs.Properties.Resources.menu_menu;
			this.SubMenu_MenuAcess.Name = "SubMenu_MenuAcess";
			this.SubMenu_MenuAcess.Size = new System.Drawing.Size(187, 22);
			this.SubMenu_MenuAcess.Text = "Menu Access Control";
			this.SubMenu_MenuAcess.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			// 
			// SubMenu_Permissions_Folders
			// 
			this.SubMenu_Permissions_Folders.Enabled = false;
			this.SubMenu_Permissions_Folders.Image = global::SpiderDocs.Properties.Resources.folder;
			this.SubMenu_Permissions_Folders.Name = "SubMenu_Permissions_Folders";
			this.SubMenu_Permissions_Folders.Size = new System.Drawing.Size(187, 22);
			this.SubMenu_Permissions_Folders.Text = "Folders";
			this.SubMenu_Permissions_Folders.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			// 
			// Menu_Reports
			// 
			this.Menu_Reports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenu_UserLog,
            this.SubMenu_DeletedDocs,
            this.SubMenu_ReportBuilder});
			this.Menu_Reports.Enabled = false;
			this.Menu_Reports.Image = global::SpiderDocs.Properties.Resources.reports;
			this.Menu_Reports.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.Menu_Reports.Name = "Menu_Reports";
			this.Menu_Reports.Size = new System.Drawing.Size(91, 36);
			this.Menu_Reports.Text = "Reports";
			// 
			// SubMenu_UserLog
			// 
			this.SubMenu_UserLog.Enabled = false;
			this.SubMenu_UserLog.Image = global::SpiderDocs.Properties.Resources.menu_log;
			this.SubMenu_UserLog.Name = "SubMenu_UserLog";
			this.SubMenu_UserLog.Size = new System.Drawing.Size(180, 22);
			this.SubMenu_UserLog.Text = "User logs";
			this.SubMenu_UserLog.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			// 
			// SubMenu_DeletedDocs
			// 
			this.SubMenu_DeletedDocs.Enabled = false;
			this.SubMenu_DeletedDocs.Image = global::SpiderDocs.Properties.Resources.bin;
			this.SubMenu_DeletedDocs.Name = "SubMenu_DeletedDocs";
			this.SubMenu_DeletedDocs.Size = new System.Drawing.Size(180, 22);
			this.SubMenu_DeletedDocs.Text = "Deleted documents";
			this.SubMenu_DeletedDocs.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			// 
			// SubMenu_ReportBuilder
			// 
			this.SubMenu_ReportBuilder.Image = global::SpiderDocs.Properties.Resources.report_builder;
			this.SubMenu_ReportBuilder.Name = "SubMenu_ReportBuilder";
			this.SubMenu_ReportBuilder.Size = new System.Drawing.Size(180, 22);
			this.SubMenu_ReportBuilder.Text = "Report Builder";
			this.SubMenu_ReportBuilder.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			// 
			// Menu_Options
			// 
			this.Menu_Options.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenu_ChangePassword,
            this.SubMenu_Preferences,
            this.SubMenu_FooterSettings});
			this.Menu_Options.Enabled = false;
			this.Menu_Options.Image = global::SpiderDocs.Properties.Resources.option;
			this.Menu_Options.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.Menu_Options.Name = "Menu_Options";
			this.Menu_Options.Size = new System.Drawing.Size(93, 36);
			this.Menu_Options.Text = "Options";
			// 
			// SubMenu_ChangePassword
			// 
			this.SubMenu_ChangePassword.Enabled = false;
			this.SubMenu_ChangePassword.Image = global::SpiderDocs.Properties.Resources.icon_key;
			this.SubMenu_ChangePassword.Name = "SubMenu_ChangePassword";
			this.SubMenu_ChangePassword.Size = new System.Drawing.Size(180, 22);
			this.SubMenu_ChangePassword.Text = "Change Password";
			this.SubMenu_ChangePassword.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			// 
			// SubMenu_Preferences
			// 
			this.SubMenu_Preferences.Enabled = false;
			this.SubMenu_Preferences.Image = global::SpiderDocs.Properties.Resources.checkbox;
			this.SubMenu_Preferences.Name = "SubMenu_Preferences";
			this.SubMenu_Preferences.Size = new System.Drawing.Size(180, 22);
			this.SubMenu_Preferences.Text = "Preferences";
			this.SubMenu_Preferences.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			// 
			// SubMenu_FooterSettings
			// 
			this.SubMenu_FooterSettings.Enabled = false;
			this.SubMenu_FooterSettings.Image = global::SpiderDocs.Properties.Resources.editing;
			this.SubMenu_FooterSettings.Name = "SubMenu_FooterSettings";
			this.SubMenu_FooterSettings.Size = new System.Drawing.Size(180, 22);
			this.SubMenu_FooterSettings.Text = "Footer Settings";
			this.SubMenu_FooterSettings.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			// 
			// Menu_Windows
			// 
			this.Menu_Windows.Image = global::SpiderDocs.Properties.Resources.order;
			this.Menu_Windows.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.Menu_Windows.Name = "Menu_Windows";
			this.Menu_Windows.Size = new System.Drawing.Size(100, 36);
			this.Menu_Windows.Text = "Windows";
			this.Menu_Windows.ToolTipText = "Layout cascade";
			this.Menu_Windows.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
			// 
			// Menu_Sync
			// 
			this.Menu_Sync.Image = ((System.Drawing.Image)(resources.GetObject("Menu_Sync.Image")));
			this.Menu_Sync.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.Menu_Sync.Name = "Menu_Sync";
			this.Menu_Sync.Size = new System.Drawing.Size(76, 36);
			this.Menu_Sync.Text = "Sync";
			this.Menu_Sync.ToolTipText = "Layout cascade";
			this.Menu_Sync.Click += new System.EventHandler(this.Menu_Sync_Click);
			// 
			// Menu_About
			// 
			this.Menu_About.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenu_Help,
            this.SubMenu_About});
			this.Menu_About.Image = ((System.Drawing.Image)(resources.GetObject("Menu_About.Image")));
			this.Menu_About.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.Menu_About.Name = "Menu_About";
			this.Menu_About.Size = new System.Drawing.Size(84, 36);
			this.Menu_About.Text = "About";
			this.Menu_About.ToolTipText = "Layout cascade";
			// 
			// SubMenu_Help
			// 
			this.SubMenu_Help.Name = "SubMenu_Help";
			this.SubMenu_Help.Size = new System.Drawing.Size(107, 22);
			this.SubMenu_Help.Text = "Help";
			this.SubMenu_Help.Click += new System.EventHandler(this.SubMenu_Help_Click);
			// 
			// SubMenu_About
			// 
			this.SubMenu_About.Name = "SubMenu_About";
			this.SubMenu_About.Size = new System.Drawing.Size(107, 22);
			this.SubMenu_About.Text = "About";
			this.SubMenu_About.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			// 
			// frmMDI
			// 
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(836, 673);
			this.Controls.Add(this.menuStripFake);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.menuStrip);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.MainMenuStrip = this.menuStripFake;
			this.MinimumSize = new System.Drawing.Size(800, 700);
			this.Name = "frmMDI";
			this.Text = "Spider Docs - Document Management System";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMDI_FormClosing);
			this.Load += new System.EventHandler(this.frmMDI_Load);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel tsUser;
        private System.Windows.Forms.ToolStripStatusLabel tsGroup;
		private System.Windows.Forms.ToolStripStatusLabel tsVersion;
        public System.Windows.Forms.ImageList imageList;
        public System.Windows.Forms.MenuStrip menuStripFake;
		private System.Windows.Forms.ToolStripMenuItem Menu_Document;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_Scan;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_Import;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_WorkSpace;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_Exit;
		private System.Windows.Forms.ToolStripMenuItem Menu_Registration;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_Users;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_Folders;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_DocumentAttributes;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_DocumentTypes;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_Groups;
		private System.Windows.Forms.ToolStripMenuItem Menu_Permissions;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_GroupsOfUsers;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_MenuAcess;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_Permissions_Folders;
		private System.Windows.Forms.ToolStripMenuItem Menu_Reports;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_UserLog;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_DeletedDocs;
		private System.Windows.Forms.ToolStripMenuItem Menu_Options;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_ChangePassword;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_Preferences;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_FooterSettings;
		private System.Windows.Forms.ToolStripMenuItem Menu_Windows;
		private System.Windows.Forms.ToolStripMenuItem Menu_About;
		public System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_Help;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_About;
		private System.Windows.Forms.ToolStripMenuItem SubMenu_Logout;
		private System.Windows.Forms.ToolStripMenuItem Menu_Sync;
		private System.Windows.Forms.ToolStripStatusLabel tsStatus;
        private System.Windows.Forms.ToolStripMenuItem SubMenu_Favourite;
        private System.Windows.Forms.ToolStripMenuItem SubMenu_NotificationGroup;
        private System.Windows.Forms.ToolStripStatusLabel tsServer;
        private System.Windows.Forms.ToolStripMenuItem SubMenu_ReportBuilder;
    }
}



