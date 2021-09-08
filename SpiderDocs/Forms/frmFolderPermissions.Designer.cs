namespace SpiderDocs
{
    partial class frmFolderPermissions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFolderPermissions));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lvGroup = new DragNDrop.DragAndDropListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lvUsersOfGroup = new DragNDrop.DragAndDropListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lvGroupAndUser = new DragNDrop.DragAndDropListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cboFolder = new SpiderDocsForms.FolderComboBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnToggleInheritance = new System.Windows.Forms.Button();
            this.pbAddFolder = new System.Windows.Forms.PictureBox();
            this.lblInheritanceDescription = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dtgPermission = new System.Windows.Forms.DataGridView();
            this.permissionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.allowDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.denyDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.permission_id_DataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnExcluir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPermission)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.splitContainer1.Location = new System.Drawing.Point(6, 58);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.lvGroupAndUser);
            this.splitContainer1.Size = new System.Drawing.Size(628, 550);
            this.splitContainer1.SplitterDistance = 322;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 22;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.pictureBox2);
            this.splitContainer2.Panel1.Controls.Add(this.lvGroup);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pictureBox3);
            this.splitContainer2.Panel2.Controls.Add(this.lvUsersOfGroup);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Size = new System.Drawing.Size(322, 550);
            this.splitContainer2.SplitterDistance = 302;
            this.splitContainer2.TabIndex = 21;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(278, 141);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(41, 42);
            this.pictureBox2.TabIndex = 30;
            this.pictureBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox2, "Add groups");
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // lvGroup
            // 
            this.lvGroup.AllowReorder = true;
            this.lvGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvGroup.BackColor = System.Drawing.Color.FloralWhite;
            this.lvGroup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8});
            this.lvGroup.Corinthians = false;
            this.lvGroup.FullRowSelect = true;
            this.lvGroup.HideSelection = false;
            this.lvGroup.LineColor = System.Drawing.Color.Gainsboro;
            this.lvGroup.Location = new System.Drawing.Point(3, 21);
            this.lvGroup.MultiSelect = false;
            this.lvGroup.Name = "lvGroup";
            this.lvGroup.Reorder = false;
            this.lvGroup.Size = new System.Drawing.Size(272, 294);
            this.lvGroup.SmallImageList = this.imageList1;
            this.lvGroup.TabIndex = 23;
            this.lvGroup.UseCompatibleStateImageBehavior = false;
            this.lvGroup.View = System.Windows.Forms.View.Details;
            this.lvGroup.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvGroup_ItemSelectionChanged);
            this.lvGroup.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvGroup_MouseDoubleClick);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "";
            this.columnHeader7.Width = 25;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Group names";
            this.columnHeader8.Width = 220;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "group.png");
            this.imageList1.Images.SetKeyName(1, "Preppy-icon.png");
            this.imageList1.Images.SetKeyName(2, "icon_delete_user.png");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Groups";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.InitialImage")));
            this.pictureBox3.Location = new System.Drawing.Point(281, 82);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(38, 38);
            this.pictureBox3.TabIndex = 31;
            this.pictureBox3.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox3, "Add users");
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // lvUsersOfGroup
            // 
            this.lvUsersOfGroup.AllowReorder = true;
            this.lvUsersOfGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvUsersOfGroup.BackColor = System.Drawing.Color.FloralWhite;
            this.lvUsersOfGroup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10});
            this.lvUsersOfGroup.Corinthians = false;
            this.lvUsersOfGroup.FullRowSelect = true;
            this.lvUsersOfGroup.HideSelection = false;
            this.lvUsersOfGroup.LineColor = System.Drawing.Color.Gainsboro;
            this.lvUsersOfGroup.Location = new System.Drawing.Point(5, 20);
            this.lvUsersOfGroup.Name = "lvUsersOfGroup";
            this.lvUsersOfGroup.Reorder = false;
            this.lvUsersOfGroup.Size = new System.Drawing.Size(270, 222);
            this.lvUsersOfGroup.SmallImageList = this.imageList1;
            this.lvUsersOfGroup.TabIndex = 24;
            this.lvUsersOfGroup.UseCompatibleStateImageBehavior = false;
            this.lvUsersOfGroup.View = System.Windows.Forms.View.Details;
            this.lvUsersOfGroup.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvUsersOfGroup_ItemSelectionChanged);
            this.lvUsersOfGroup.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvUsersOfGroup_MouseDoubleClick);
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "";
            this.columnHeader9.Width = 25;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "User names";
            this.columnHeader10.Width = 220;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(6, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Members of group";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(3, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Allowed";
            // 
            // lvGroupAndUser
            // 
            this.lvGroupAndUser.AllowDrop = true;
            this.lvGroupAndUser.AllowReorder = true;
            this.lvGroupAndUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvGroupAndUser.BackColor = System.Drawing.Color.FloralWhite;
            this.lvGroupAndUser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.lvGroupAndUser.Corinthians = true;
            this.lvGroupAndUser.FullRowSelect = true;
            this.lvGroupAndUser.HideSelection = false;
            this.lvGroupAndUser.LineColor = System.Drawing.Color.Gainsboro;
            this.lvGroupAndUser.Location = new System.Drawing.Point(3, 22);
            this.lvGroupAndUser.MultiSelect = false;
            this.lvGroupAndUser.Name = "lvGroupAndUser";
            this.lvGroupAndUser.Reorder = false;
            this.lvGroupAndUser.Size = new System.Drawing.Size(299, 525);
            this.lvGroupAndUser.SmallImageList = this.imageList1;
            this.lvGroupAndUser.TabIndex = 24;
            this.lvGroupAndUser.UseCompatibleStateImageBehavior = false;
            this.lvGroupAndUser.View = System.Windows.Forms.View.Details;
            this.lvGroupAndUser.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvGroupAndUser_ItemSelectionChanged);
            this.lvGroupAndUser.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvGroupAndUser_DragDrop);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "";
            this.columnHeader3.Width = 25;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Group/User Names";
            this.columnHeader4.Width = 250;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(10, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = "Allowed";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 25;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 212;
            // 
            // cboFolder
            // 
            this.cboFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboFolder.BackColor = System.Drawing.Color.Ivory;
            this.cboFolder.DisplayMember = "document_folder";
            this.cboFolder.DropDownHeight = 1;
            this.cboFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFolder.FormattingEnabled = true;
            this.cboFolder.IntegralHeight = false;
            this.cboFolder.Location = new System.Drawing.Point(52, 25);
            this.cboFolder.Name = "cboFolder";
            this.cboFolder.Size = new System.Drawing.Size(229, 21);
            this.cboFolder.TabIndex = 25;
            this.cboFolder.ValueMember = "id";
            this.cboFolder.SelectionChangeCommitted += new System.EventHandler(this.cboFolder_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(52, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 15);
            this.label3.TabIndex = 26;
            this.label3.Text = "Folder to setup permission:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnToggleInheritance);
            this.panel1.Controls.Add(this.pbAddFolder);
            this.panel1.Controls.Add(this.lblInheritanceDescription);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(-5, -13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 63);
            this.panel1.TabIndex = 28;
            // 
            // btnToggleInheritance
            // 
            this.btnToggleInheritance.Enabled = false;
            this.btnToggleInheritance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToggleInheritance.Location = new System.Drawing.Point(775, 25);
            this.btnToggleInheritance.Name = "btnToggleInheritance";
            this.btnToggleInheritance.Size = new System.Drawing.Size(148, 30);
            this.btnToggleInheritance.TabIndex = 32;
            this.btnToggleInheritance.Text = "Disable inheritance";
            this.btnToggleInheritance.UseVisualStyleBackColor = true;
            this.btnToggleInheritance.Click += new System.EventHandler(this.btnToggleInheritance_Click);
            // 
            // pbAddFolder
            // 
            this.pbAddFolder.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pbAddFolder.Image = global::SpiderDocs.Properties.Resources.folder_add;
            this.pbAddFolder.Location = new System.Drawing.Point(292, 23);
            this.pbAddFolder.Name = "pbAddFolder";
            this.pbAddFolder.Size = new System.Drawing.Size(37, 35);
            this.pbAddFolder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAddFolder.TabIndex = 31;
            this.pbAddFolder.TabStop = false;
            this.toolTip1.SetToolTip(this.pbAddFolder, "Add Folders");
            this.pbAddFolder.Click += new System.EventHandler(this.pbAddFolder_Click);
            // 
            // lblInheritanceDescription
            // 
            this.lblInheritanceDescription.AutoSize = true;
            this.lblInheritanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInheritanceDescription.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblInheritanceDescription.Location = new System.Drawing.Point(360, 34);
            this.lblInheritanceDescription.Name = "lblInheritanceDescription";
            this.lblInheritanceDescription.Size = new System.Drawing.Size(43, 18);
            this.lblInheritanceDescription.TabIndex = 26;
            this.lblInheritanceDescription.Text = "xxxxx";
            this.lblInheritanceDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBox1.Image = global::SpiderDocs.Properties.Resources.folder;
            this.pictureBox1.Location = new System.Drawing.Point(13, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 38);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // dtgPermission
            // 
            this.dtgPermission.AllowUserToAddRows = false;
            this.dtgPermission.AllowUserToDeleteRows = false;
            this.dtgPermission.AllowUserToResizeColumns = false;
            this.dtgPermission.AllowUserToResizeRows = false;
            this.dtgPermission.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dtgPermission.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dtgPermission.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dtgPermission.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgPermission.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.permissionDataGridViewTextBoxColumn,
            this.allowDataGridViewCheckBoxColumn,
            this.denyDataGridViewCheckBoxColumn,
            this.permission_id_DataGridViewCheckBoxColumn});
            this.dtgPermission.Location = new System.Drawing.Point(640, 80);
            this.dtgPermission.MultiSelect = false;
            this.dtgPermission.Name = "dtgPermission";
            this.dtgPermission.RowHeadersVisible = false;
            this.dtgPermission.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Silver;
            this.dtgPermission.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgPermission.Size = new System.Drawing.Size(280, 526);
            this.dtgPermission.TabIndex = 20;
            this.dtgPermission.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtgPermission_CellMouseUp);
            // 
            // permissionDataGridViewTextBoxColumn
            // 
            this.permissionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.permissionDataGridViewTextBoxColumn.DataPropertyName = "permission";
            this.permissionDataGridViewTextBoxColumn.HeaderText = "Permission";
            this.permissionDataGridViewTextBoxColumn.Name = "permissionDataGridViewTextBoxColumn";
            this.permissionDataGridViewTextBoxColumn.ReadOnly = true;
            this.permissionDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // allowDataGridViewCheckBoxColumn
            // 
            this.allowDataGridViewCheckBoxColumn.DataPropertyName = "allow";
            this.allowDataGridViewCheckBoxColumn.FalseValue = "";
            this.allowDataGridViewCheckBoxColumn.HeaderText = "Allow";
            this.allowDataGridViewCheckBoxColumn.Name = "allowDataGridViewCheckBoxColumn";
            this.allowDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.allowDataGridViewCheckBoxColumn.TrueValue = "";
            this.allowDataGridViewCheckBoxColumn.Width = 50;
            // 
            // denyDataGridViewCheckBoxColumn
            // 
            this.denyDataGridViewCheckBoxColumn.DataPropertyName = "deny";
            this.denyDataGridViewCheckBoxColumn.FalseValue = "";
            this.denyDataGridViewCheckBoxColumn.HeaderText = "Deny";
            this.denyDataGridViewCheckBoxColumn.Name = "denyDataGridViewCheckBoxColumn";
            this.denyDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.denyDataGridViewCheckBoxColumn.TrueValue = "";
            this.denyDataGridViewCheckBoxColumn.Width = 50;
            // 
            // permission_id_DataGridViewCheckBoxColumn
            // 
            this.permission_id_DataGridViewCheckBoxColumn.HeaderText = "permission_id";
            this.permission_id_DataGridViewCheckBoxColumn.Name = "permission_id_DataGridViewCheckBoxColumn";
            this.permission_id_DataGridViewCheckBoxColumn.ReadOnly = true;
            this.permission_id_DataGridViewCheckBoxColumn.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(640, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Permissions";
            // 
            // btnExcluir
            // 
            this.btnExcluir.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnExcluir.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnExcluir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSteelBlue;
            this.btnExcluir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcluir.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnExcluir.ImageKey = "icon_delete_user.png";
            this.btnExcluir.ImageList = this.imageList1;
            this.btnExcluir.Location = new System.Drawing.Point(557, 55);
            this.btnExcluir.Margin = new System.Windows.Forms.Padding(1);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnExcluir.Size = new System.Drawing.Size(73, 24);
            this.btnExcluir.TabIndex = 59;
            this.btnExcluir.Text = "Delete";
            this.btnExcluir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExcluir.UseVisualStyleBackColor = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // frmFolderPermissions
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(931, 613);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtgPermission);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboFolder);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(854, 500);
            this.Name = "frmFolderPermissions";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Folder Permissions";
            this.Load += new System.EventHandler(this.frmFolderPermissions_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPermission)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private SpiderDocsForms.FolderComboBox cboFolder;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DataGridView dtgPermission;
		private System.Windows.Forms.Label label5;
        private DragNDrop.DragAndDropListView lvGroup;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private DragNDrop.DragAndDropListView lvUsersOfGroup;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.Label label6;
        private DragNDrop.DragAndDropListView lvGroupAndUser;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        internal System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label lblInheritanceDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn permissionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn allowDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn denyDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn permission_id_DataGridViewCheckBoxColumn;
        private System.Windows.Forms.PictureBox pbAddFolder;
        private System.Windows.Forms.Button btnToggleInheritance;
    }
}