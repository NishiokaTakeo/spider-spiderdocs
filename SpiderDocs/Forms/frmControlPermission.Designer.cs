namespace SpiderDocs
{
    partial class frmControlPermission
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
			System.Windows.Forms.Label permissionLabel;
			System.Windows.Forms.Label obsLabel;
			System.Windows.Forms.Label label1;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmControlPermission));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.system_permission_levelBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
			this.btnAdd = new System.Windows.Forms.ToolStripButton();
			this.system_permission_levelBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.btnDelete = new System.Windows.Forms.ToolStripButton();
			this.btnSave = new System.Windows.Forms.ToolStripButton();
			this.txtLevelName = new System.Windows.Forms.TextBox();
			this.txtLevelComments = new System.Windows.Forms.TextBox();
			this.dtgLevelPermission = new System.Windows.Forms.DataGridView();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.treeView3 = new System.Windows.Forms.TreeView();
			this.treeView4 = new System.Windows.Forms.TreeView();
			this.treeView5 = new System.Windows.Forms.TreeView();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.userDataGridView = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.userBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.treeView2 = new System.Windows.Forms.TreeView();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			permissionLabel = new System.Windows.Forms.Label();
			obsLabel = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.system_permission_levelBindingNavigator)).BeginInit();
			this.system_permission_levelBindingNavigator.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.system_permission_levelBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtgLevelPermission)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.userDataGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.userBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.SuspendLayout();
			// 
			// permissionLabel
			// 
			permissionLabel.AutoSize = true;
			permissionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			permissionLabel.Location = new System.Drawing.Point(34, 40);
			permissionLabel.Name = "permissionLabel";
			permissionLabel.Size = new System.Drawing.Size(36, 13);
			permissionLabel.TabIndex = 12;
			permissionLabel.Text = "Level:";
			// 
			// obsLabel
			// 
			obsLabel.AutoSize = true;
			obsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			obsLabel.Location = new System.Drawing.Point(15, 59);
			obsLabel.Name = "obsLabel";
			obsLabel.Size = new System.Drawing.Size(59, 13);
			obsLabel.TabIndex = 14;
			obsLabel.Text = "Comments:";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.BackColor = System.Drawing.Color.SteelBlue;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			label1.ForeColor = System.Drawing.Color.White;
			label1.Location = new System.Drawing.Point(15, 9);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(227, 18);
			label1.TabIndex = 13;
			label1.Text = "Menu/SubMenu - Access Control";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
			this.groupBox1.Controls.Add(obsLabel);
			this.groupBox1.Controls.Add(this.system_permission_levelBindingNavigator);
			this.groupBox1.Controls.Add(this.txtLevelName);
			this.groupBox1.Controls.Add(this.txtLevelComments);
			this.groupBox1.Controls.Add(permissionLabel);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.groupBox1.Location = new System.Drawing.Point(5, 34);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(421, 232);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "                       Level";
			// 
			// system_permission_levelBindingNavigator
			// 
			this.system_permission_levelBindingNavigator.AddNewItem = this.btnAdd;
			this.system_permission_levelBindingNavigator.BackColor = System.Drawing.SystemColors.Control;
			this.system_permission_levelBindingNavigator.BindingSource = this.system_permission_levelBindingSource;
			this.system_permission_levelBindingNavigator.CountItem = null;
			this.system_permission_levelBindingNavigator.DeleteItem = null;
			this.system_permission_levelBindingNavigator.Dock = System.Windows.Forms.DockStyle.None;
			this.system_permission_levelBindingNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.system_permission_levelBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnDelete,
            this.btnSave});
			this.system_permission_levelBindingNavigator.Location = new System.Drawing.Point(324, 59);
			this.system_permission_levelBindingNavigator.MoveFirstItem = null;
			this.system_permission_levelBindingNavigator.MoveLastItem = null;
			this.system_permission_levelBindingNavigator.MoveNextItem = null;
			this.system_permission_levelBindingNavigator.MovePreviousItem = null;
			this.system_permission_levelBindingNavigator.Name = "system_permission_levelBindingNavigator";
			this.system_permission_levelBindingNavigator.PositionItem = null;
			this.system_permission_levelBindingNavigator.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.system_permission_levelBindingNavigator.Size = new System.Drawing.Size(72, 25);
			this.system_permission_levelBindingNavigator.TabIndex = 7;
			this.system_permission_levelBindingNavigator.Text = "bindingNavigator1";
			// 
			// btnAdd
			// 
			this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.RightToLeftAutoMirrorImage = true;
			this.btnAdd.Size = new System.Drawing.Size(23, 22);
			this.btnAdd.Text = "Add new";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// system_permission_levelBindingSource
			// 
			this.system_permission_levelBindingSource.DataMember = "system_permission_level";
			// 
			// btnDelete
			// 
			this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.RightToLeftAutoMirrorImage = true;
			this.btnDelete.Size = new System.Drawing.Size(23, 22);
			this.btnDelete.Text = "Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnSave
			// 
			this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(23, 22);
			this.btnSave.Text = "Save Data";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// txtLevelName
			// 
			this.txtLevelName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.system_permission_levelBindingSource, "permission", true));
			this.txtLevelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLevelName.Location = new System.Drawing.Point(76, 34);
			this.txtLevelName.MaxLength = 50;
			this.txtLevelName.Name = "txtLevelName";
			this.txtLevelName.Size = new System.Drawing.Size(240, 20);
			this.txtLevelName.TabIndex = 13;
			this.txtLevelName.Validating += new System.ComponentModel.CancelEventHandler(this.txtLevelName_Validating);
			// 
			// txtLevelComments
			// 
			this.txtLevelComments.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.system_permission_levelBindingSource, "obs", true));
			this.txtLevelComments.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLevelComments.Location = new System.Drawing.Point(76, 57);
			this.txtLevelComments.MaxLength = 200;
			this.txtLevelComments.Name = "txtLevelComments";
			this.txtLevelComments.Size = new System.Drawing.Size(240, 20);
			this.txtLevelComments.TabIndex = 15;
			// 
			// dtgLevelPermission
			// 
			this.dtgLevelPermission.AllowUserToAddRows = false;
			this.dtgLevelPermission.AllowUserToDeleteRows = false;
			this.dtgLevelPermission.AllowUserToOrderColumns = true;
			this.dtgLevelPermission.AllowUserToResizeRows = false;
			this.dtgLevelPermission.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dtgLevelPermission.AutoGenerateColumns = false;
			this.dtgLevelPermission.BackgroundColor = System.Drawing.Color.FloralWhite;
			this.dtgLevelPermission.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dtgLevelPermission.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dtgLevelPermission.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
			this.dtgLevelPermission.DataSource = this.system_permission_levelBindingSource;
			this.dtgLevelPermission.Location = new System.Drawing.Point(8, 117);
			this.dtgLevelPermission.MultiSelect = false;
			this.dtgLevelPermission.Name = "dtgLevelPermission";
			this.dtgLevelPermission.ReadOnly = true;
			this.dtgLevelPermission.RowHeadersVisible = false;
			this.dtgLevelPermission.RowHeadersWidth = 20;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtgLevelPermission.RowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dtgLevelPermission.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtgLevelPermission.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dtgLevelPermission.Size = new System.Drawing.Size(415, 149);
			this.dtgLevelPermission.TabIndex = 0;
			this.dtgLevelPermission.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dtgLevelPermission_DataBindingComplete);
			// 
			// treeView1
			// 
			this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.treeView1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.treeView1.Location = new System.Drawing.Point(10, 35);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(166, 232);
			this.treeView1.TabIndex = 2;
			this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCheck);
			// 
			// treeView3
			// 
			this.treeView3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.treeView3.BackColor = System.Drawing.Color.WhiteSmoke;
			this.treeView3.Location = new System.Drawing.Point(354, 35);
			this.treeView3.Name = "treeView3";
			this.treeView3.Size = new System.Drawing.Size(166, 232);
			this.treeView3.TabIndex = 4;
			this.treeView3.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCheck);
			// 
			// treeView4
			// 
			this.treeView4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.treeView4.BackColor = System.Drawing.Color.WhiteSmoke;
			this.treeView4.Location = new System.Drawing.Point(525, 35);
			this.treeView4.Name = "treeView4";
			this.treeView4.Size = new System.Drawing.Size(166, 232);
			this.treeView4.TabIndex = 5;
			this.treeView4.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCheck);
			// 
			// treeView5
			// 
			this.treeView5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.treeView5.BackColor = System.Drawing.Color.WhiteSmoke;
			this.treeView5.Location = new System.Drawing.Point(696, 35);
			this.treeView5.Name = "treeView5";
			this.treeView5.Size = new System.Drawing.Size(166, 232);
			this.treeView5.TabIndex = 6;
			this.treeView5.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCheck);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.groupBox2.Location = new System.Drawing.Point(7, 35);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(418, 231);
			this.groupBox2.TabIndex = 8;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "               Members of Level";
			// 
			// userDataGridView
			// 
			this.userDataGridView.AllowUserToAddRows = false;
			this.userDataGridView.AllowUserToDeleteRows = false;
			this.userDataGridView.AllowUserToOrderColumns = true;
			this.userDataGridView.AllowUserToResizeRows = false;
			this.userDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.userDataGridView.AutoGenerateColumns = false;
			this.userDataGridView.BackgroundColor = System.Drawing.Color.FloralWhite;
			this.userDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.userDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn8});
			this.userDataGridView.DataSource = this.userBindingSource;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.userDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.userDataGridView.Enabled = true;
			this.userDataGridView.Location = new System.Drawing.Point(9, 65);
			this.userDataGridView.Name = "userDataGridView";
			this.userDataGridView.ReadOnly = true;
			this.userDataGridView.RowHeadersVisible = false;
			this.userDataGridView.RowHeadersWidth = 20;
			this.userDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.userDataGridView.Size = new System.Drawing.Size(414, 201);
			this.userDataGridView.TabIndex = 0;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.DataPropertyName = "id";
			this.dataGridViewTextBoxColumn4.HeaderText = "id";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.Visible = false;
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.DataPropertyName = "login";
			this.dataGridViewTextBoxColumn5.HeaderText = "Login";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			this.dataGridViewTextBoxColumn5.Visible = false;
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn6.DataPropertyName = "name";
			this.dataGridViewTextBoxColumn6.HeaderText = "Name";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn7
			// 
			this.dataGridViewTextBoxColumn7.DataPropertyName = "password";
			this.dataGridViewTextBoxColumn7.HeaderText = "password";
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			this.dataGridViewTextBoxColumn7.Visible = false;
			// 
			// dataGridViewCheckBoxColumn1
			// 
			this.dataGridViewCheckBoxColumn1.DataPropertyName = "active";
			this.dataGridViewCheckBoxColumn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.dataGridViewCheckBoxColumn1.HeaderText = "Active";
			this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
			this.dataGridViewCheckBoxColumn1.ReadOnly = true;
			this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn1.Width = 50;
			// 
			// dataGridViewTextBoxColumn8
			// 
			this.dataGridViewTextBoxColumn8.DataPropertyName = "id_permission";
			this.dataGridViewTextBoxColumn8.HeaderText = "id_permission";
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			this.dataGridViewTextBoxColumn8.ReadOnly = true;
			this.dataGridViewTextBoxColumn8.Visible = false;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::SpiderDocs.Properties.Resources.login;
			this.pictureBox1.Location = new System.Drawing.Point(16, 7);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(64, 63);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 9;
			this.pictureBox1.TabStop = false;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = global::SpiderDocs.Properties.Resources.big_groups;
			this.pictureBox2.Location = new System.Drawing.Point(19, 22);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(38, 36);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox2.TabIndex = 10;
			this.pictureBox2.TabStop = false;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.dtgLevelPermission);
			this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
			this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.pictureBox2);
			this.splitContainer1.Panel2.Controls.Add(this.userDataGridView);
			this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
			this.splitContainer1.Size = new System.Drawing.Size(872, 277);
			this.splitContainer1.SplitterDistance = 436;
			this.splitContainer1.TabIndex = 14;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.Color.SteelBlue;
			this.panel1.Location = new System.Drawing.Point(-1, 5);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(888, 26);
			this.panel1.TabIndex = 15;
			// 
			// treeView2
			// 
			this.treeView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.treeView2.BackColor = System.Drawing.Color.WhiteSmoke;
			this.treeView2.Location = new System.Drawing.Point(180, 35);
			this.treeView2.Name = "treeView2";
			this.treeView2.Size = new System.Drawing.Size(170, 232);
			this.treeView2.TabIndex = 16;
			this.treeView2.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCheck);
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
			this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(label1);
			this.splitContainer2.Panel2.Controls.Add(this.treeView2);
			this.splitContainer2.Panel2.Controls.Add(this.panel1);
			this.splitContainer2.Panel2.Controls.Add(this.treeView1);
			this.splitContainer2.Panel2.Controls.Add(this.treeView5);
			this.splitContainer2.Panel2.Controls.Add(this.treeView4);
			this.splitContainer2.Panel2.Controls.Add(this.treeView3);
			this.splitContainer2.Size = new System.Drawing.Size(878, 559);
			this.splitContainer2.SplitterDistance = 283;
			this.splitContainer2.TabIndex = 17;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
			this.dataGridViewTextBoxColumn1.HeaderText = "id";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.Visible = false;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.DataPropertyName = "permission";
			this.dataGridViewTextBoxColumn2.HeaderText = "Menu Access Level";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.Width = 150;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn3.DataPropertyName = "obs";
			this.dataGridViewTextBoxColumn3.HeaderText = "Comments";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			// 
			// frmControlPermission
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(878, 559);
			this.Controls.Add(this.splitContainer2);
			this.MinimumSize = new System.Drawing.Size(886, 492);
			this.Name = "frmControlPermission";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Menu Access Level";
			this.Load += new System.EventHandler(this.frmControlPermission_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.system_permission_levelBindingNavigator)).EndInit();
			this.system_permission_levelBindingNavigator.ResumeLayout(false);
			this.system_permission_levelBindingNavigator.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.system_permission_levelBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtgLevelPermission)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.userDataGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.userBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TreeView treeView3;
        private System.Windows.Forms.TreeView treeView4;
        private System.Windows.Forms.TreeView treeView5;
        private System.Windows.Forms.BindingSource system_permission_levelBindingSource;
        private System.Windows.Forms.BindingNavigator system_permission_levelBindingNavigator;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.DataGridView dtgLevelPermission;
        private System.Windows.Forms.TextBox txtLevelName;
        private System.Windows.Forms.TextBox txtLevelComments;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.BindingSource userBindingSource;
		private System.Windows.Forms.DataGridView userDataGridView;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}