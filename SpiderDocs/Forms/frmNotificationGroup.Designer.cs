namespace SpiderDocs
{
    partial class frmNotificationGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNotificationGroup));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblGroups = new System.Windows.Forms.Label();
            this.document_typeBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.lbNewUser = new System.Windows.Forms.ToolStripLabel();
            this.txtGroup = new System.Windows.Forms.ToolStripTextBox();
            this.tsbSaveAsPDF4User = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveAsPDF4Docs = new System.Windows.Forms.ToolStripButton();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dtgBdFiles = new SpiderDocsForms.DocumentDataGridView();
            this.dataGridViewImageColumn3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.c_id_doc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.group_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extension = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_sp_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbNotificationGroup = new System.Windows.Forms.ComboBox();
            this.lvGroup = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lvUsersOfGroup = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvAllowed = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnExcluir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.groupBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.document_typeBindingNavigator)).BeginInit();
            this.document_typeBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgBdFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "group.png");
            this.imageList1.Images.SetKeyName(1, "Preppy-icon.png");
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(329, 120);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(41, 42);
            this.pictureBox2.TabIndex = 64;
            this.pictureBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox2, "Add groups");
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(329, 129);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 38);
            this.pictureBox1.TabIndex = 65;
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, "Add users");
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox3.Image = global::SpiderDocs.Properties.Resources.delete;
            this.pictureBox3.Location = new System.Drawing.Point(294, 8);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(20, 20);
            this.pictureBox3.TabIndex = 65;
            this.pictureBox3.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox3, "Add groups");
            this.pictureBox3.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblGroups
            // 
            this.lblGroups.AutoSize = true;
            this.lblGroups.Location = new System.Drawing.Point(418, 75);
            this.lblGroups.Name = "lblGroups";
            this.lblGroups.Size = new System.Drawing.Size(76, 13);
            this.lblGroups.TabIndex = 36;
            this.lblGroups.Text = "Current Group:";
            // 
            // document_typeBindingNavigator
            // 
            this.document_typeBindingNavigator.AddNewItem = null;
            this.document_typeBindingNavigator.BackColor = System.Drawing.SystemColors.Control;
            this.document_typeBindingNavigator.CountItem = null;
            this.document_typeBindingNavigator.DeleteItem = null;
            this.document_typeBindingNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.document_typeBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnDelete,
            this.btnSave,
            this.lbNewUser,
            this.txtGroup,
            this.tsbSaveAsPDF4User,
            this.tsbSaveAsPDF4Docs});
            this.document_typeBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.document_typeBindingNavigator.MoveFirstItem = null;
            this.document_typeBindingNavigator.MoveLastItem = null;
            this.document_typeBindingNavigator.MoveNextItem = null;
            this.document_typeBindingNavigator.MovePreviousItem = null;
            this.document_typeBindingNavigator.Name = "document_typeBindingNavigator";
            this.document_typeBindingNavigator.PositionItem = null;
            this.document_typeBindingNavigator.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.document_typeBindingNavigator.Size = new System.Drawing.Size(1168, 25);
            this.document_typeBindingNavigator.TabIndex = 49;
            this.document_typeBindingNavigator.Text = "bindingNavigator1";
            // 
            // btnAdd
            // 
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAdd.Image = global::SpiderDocs.Properties.Resources.add2;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.White;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.RightToLeftAutoMirrorImage = true;
            this.btnAdd.Size = new System.Drawing.Size(23, 22);
            this.btnAdd.Text = "Add new";
            this.btnAdd.ToolTipText = "Add new";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = global::SpiderDocs.Properties.Resources.delete;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.RightToLeftAutoMirrorImage = true;
            this.btnDelete.Size = new System.Drawing.Size(23, 22);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::SpiderDocs.Properties.Resources.salvar;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "Save Data";
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
            // txtGroup
            // 
            this.txtGroup.Name = "txtGroup";
            this.txtGroup.Size = new System.Drawing.Size(200, 25);
            this.txtGroup.Visible = false;
            // 
            // tsbSaveAsPDF4User
            // 
            this.tsbSaveAsPDF4User.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSaveAsPDF4User.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveAsPDF4User.Image")));
            this.tsbSaveAsPDF4User.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveAsPDF4User.Margin = new System.Windows.Forms.Padding(50, 1, 0, 2);
            this.tsbSaveAsPDF4User.Name = "tsbSaveAsPDF4User";
            this.tsbSaveAsPDF4User.Size = new System.Drawing.Size(121, 22);
            this.tsbSaveAsPDF4User.Text = "Save As PDF(Groups)";
            this.tsbSaveAsPDF4User.Click += new System.EventHandler(this.tsbSaveAsPDF4User_Click);
            // 
            // tsbSaveAsPDF4Docs
            // 
            this.tsbSaveAsPDF4Docs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSaveAsPDF4Docs.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveAsPDF4Docs.Image")));
            this.tsbSaveAsPDF4Docs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveAsPDF4Docs.Name = "tsbSaveAsPDF4Docs";
            this.tsbSaveAsPDF4Docs.Size = new System.Drawing.Size(109, 22);
            this.tsbSaveAsPDF4Docs.Text = "Save As PDF(Docs)";
            this.tsbSaveAsPDF4Docs.ToolTipText = "tsbSaveAsPDF4Docs";
            this.tsbSaveAsPDF4Docs.Click += new System.EventHandler(this.tsbSaveAsPDF4Docs_Click);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Width = 35;
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
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn2.HeaderText = "Id";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.ReadOnly = true;
            this.dataGridViewImageColumn2.Width = 35;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn3.HeaderText = "Name";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn4.HeaderText = "Name";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "ck";
            this.dataGridViewCheckBoxColumn1.HeaderText = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 20;
            // 
            // dtgBdFiles
            // 
            this.dtgBdFiles.AllowDrop = true;
            this.dtgBdFiles.AllowUserToAddRows = false;
            this.dtgBdFiles.AllowUserToDeleteRows = false;
            this.dtgBdFiles.AllowUserToOrderColumns = true;
            this.dtgBdFiles.AllowUserToResizeRows = false;
            this.dtgBdFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgBdFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgBdFiles.BackgroundColor = System.Drawing.Color.White;
            this.dtgBdFiles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dtgBdFiles.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgBdFiles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgBdFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgBdFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewImageColumn3,
            this.c_id_doc,
            this.c_title,
            this.c_version,
            this.c_date,
            this.name,
            this.reason,
            this.group_name,
            this.extension,
            this.id_sp_status});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgBdFiles.DefaultCellStyle = dataGridViewCellStyle4;
            this.dtgBdFiles.GridColor = System.Drawing.Color.Gainsboro;
            this.dtgBdFiles.Location = new System.Drawing.Point(721, 98);
            this.dtgBdFiles.Mode = SpiderDocsForms.en_DocumentDataGridViewMode.Database;
            this.dtgBdFiles.Name = "dtgBdFiles";
            this.dtgBdFiles.ReadOnly = true;
            this.dtgBdFiles.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Beige;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgBdFiles.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dtgBdFiles.RowHeadersVisible = false;
            this.dtgBdFiles.RowHeadersWidth = 20;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            this.dtgBdFiles.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dtgBdFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgBdFiles.Size = new System.Drawing.Size(435, 511);
            this.dtgBdFiles.TabIndex = 52;
            // 
            // dataGridViewImageColumn3
            // 
            this.dataGridViewImageColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewImageColumn3.DataPropertyName = "c_img";
            this.dataGridViewImageColumn3.HeaderText = "";
            this.dataGridViewImageColumn3.MinimumWidth = 20;
            this.dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
            this.dataGridViewImageColumn3.ReadOnly = true;
            this.dataGridViewImageColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn3.Width = 20;
            // 
            // c_id_doc
            // 
            this.c_id_doc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.c_id_doc.DataPropertyName = "id_doc";
            this.c_id_doc.HeaderText = "Id";
            this.c_id_doc.MinimumWidth = 42;
            this.c_id_doc.Name = "c_id_doc";
            this.c_id_doc.ReadOnly = true;
            this.c_id_doc.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.c_id_doc.Width = 42;
            // 
            // c_title
            // 
            this.c_title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.c_title.DataPropertyName = "title";
            dataGridViewCellStyle2.Format = "IN: {0}";
            dataGridViewCellStyle2.NullValue = null;
            this.c_title.DefaultCellStyle = dataGridViewCellStyle2;
            this.c_title.HeaderText = "Name";
            this.c_title.MinimumWidth = 10;
            this.c_title.Name = "c_title";
            this.c_title.ReadOnly = true;
            this.c_title.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.c_title.Width = 84;
            // 
            // c_version
            // 
            this.c_version.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.c_version.DataPropertyName = "version";
            this.c_version.FillWeight = 70F;
            this.c_version.HeaderText = "Version";
            this.c_version.MinimumWidth = 10;
            this.c_version.Name = "c_version";
            this.c_version.ReadOnly = true;
            this.c_version.Width = 59;
            // 
            // c_date
            // 
            this.c_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.c_date.DataPropertyName = "date";
            dataGridViewCellStyle3.Format = "dd/MM/yyyy hh:mm tt";
            dataGridViewCellStyle3.NullValue = null;
            this.c_date.DefaultCellStyle = dataGridViewCellStyle3;
            this.c_date.HeaderText = "Date";
            this.c_date.MinimumWidth = 10;
            this.c_date.Name = "c_date";
            this.c_date.ReadOnly = true;
            this.c_date.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "By";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // reason
            // 
            this.reason.DataPropertyName = "reason";
            this.reason.HeaderText = "Reason";
            this.reason.Name = "reason";
            this.reason.ReadOnly = true;
            // 
            // group_name
            // 
            this.group_name.DataPropertyName = "group_name";
            this.group_name.HeaderText = "Group Name";
            this.group_name.Name = "group_name";
            this.group_name.ReadOnly = true;
            this.group_name.Visible = false;
            // 
            // extension
            // 
            this.extension.DataPropertyName = "extension";
            this.extension.HeaderText = "extension";
            this.extension.Name = "extension";
            this.extension.ReadOnly = true;
            this.extension.Visible = false;
            // 
            // id_sp_status
            // 
            this.id_sp_status.DataPropertyName = "id_sp_status";
            this.id_sp_status.HeaderText = "id_sp_status";
            this.id_sp_status.Name = "id_sp_status";
            this.id_sp_status.ReadOnly = true;
            this.id_sp_status.Visible = false;
            // 
            // cmbNotificationGroup
            // 
            this.cmbNotificationGroup.DataSource = this.groupBindingSource;
            this.cmbNotificationGroup.DisplayMember = "group_name";
            this.cmbNotificationGroup.FormattingEnabled = true;
            this.cmbNotificationGroup.Location = new System.Drawing.Point(508, 72);
            this.cmbNotificationGroup.Name = "cmbNotificationGroup";
            this.cmbNotificationGroup.Size = new System.Drawing.Size(166, 21);
            this.cmbNotificationGroup.TabIndex = 61;
            this.cmbNotificationGroup.ValueMember = "id";
            this.cmbNotificationGroup.SelectedIndexChanged += new System.EventHandler(this.cmbNotificationGroup_SelectedIndexChanged);
            // 
            // lvGroup
            // 
            this.lvGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvGroup.BackColor = System.Drawing.Color.FloralWhite;
            this.lvGroup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8});
            this.lvGroup.FullRowSelect = true;
            this.lvGroup.HideSelection = false;
            this.lvGroup.Location = new System.Drawing.Point(12, 30);
            this.lvGroup.MultiSelect = false;
            this.lvGroup.Name = "lvGroup";
            this.lvGroup.Size = new System.Drawing.Size(302, 234);
            this.lvGroup.SmallImageList = this.imageList1;
            this.lvGroup.TabIndex = 62;
            this.lvGroup.UseCompatibleStateImageBehavior = false;
            this.lvGroup.View = System.Windows.Forms.View.Details;
            this.lvGroup.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvGroup_ItemSelectionChanged);
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
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 70);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox3);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.lvGroup);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel2.Controls.Add(this.lvUsersOfGroup);
            this.splitContainer1.Size = new System.Drawing.Size(386, 539);
            this.splitContainer1.SplitterDistance = 269;
            this.splitContainer1.TabIndex = 63;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 63;
            this.label1.Text = "Notification Groups";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(9, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 66;
            this.label4.Text = "Individuals";
            // 
            // lvUsersOfGroup
            // 
            this.lvUsersOfGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvUsersOfGroup.BackColor = System.Drawing.Color.FloralWhite;
            this.lvUsersOfGroup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10});
            this.lvUsersOfGroup.FullRowSelect = true;
            this.lvUsersOfGroup.HideSelection = false;
            this.lvUsersOfGroup.Location = new System.Drawing.Point(12, 28);
            this.lvUsersOfGroup.Name = "lvUsersOfGroup";
            this.lvUsersOfGroup.Size = new System.Drawing.Size(302, 238);
            this.lvUsersOfGroup.SmallImageList = this.imageList1;
            this.lvUsersOfGroup.TabIndex = 64;
            this.lvUsersOfGroup.UseCompatibleStateImageBehavior = false;
            this.lvUsersOfGroup.View = System.Windows.Forms.View.Details;
            this.lvUsersOfGroup.Click += new System.EventHandler(this.lvUsersOfGroup_Click);
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
            // lvAllowed
            // 
            this.lvAllowed.AllowDrop = true;
            this.lvAllowed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvAllowed.BackColor = System.Drawing.Color.FloralWhite;
            this.lvAllowed.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.lvAllowed.FullRowSelect = true;
            this.lvAllowed.HideSelection = false;
            this.lvAllowed.Location = new System.Drawing.Point(416, 98);
            this.lvAllowed.MultiSelect = false;
            this.lvAllowed.Name = "lvAllowed";
            this.lvAllowed.Size = new System.Drawing.Size(299, 525);
            this.lvAllowed.SmallImageList = this.imageList1;
            this.lvAllowed.TabIndex = 64;
            this.lvAllowed.UseCompatibleStateImageBehavior = false;
            this.lvAllowed.View = System.Windows.Forms.View.Details;
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
            // btnExcluir
            // 
            this.btnExcluir.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnExcluir.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnExcluir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSteelBlue;
            this.btnExcluir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcluir.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnExcluir.ImageKey = "icon_delete_user.png";
            this.btnExcluir.ImageList = this.imageList1;
            this.btnExcluir.Location = new System.Drawing.Point(675, 71);
            this.btnExcluir.Margin = new System.Windows.Forms.Padding(1);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnExcluir.Size = new System.Drawing.Size(53, 22);
            this.btnExcluir.TabIndex = 60;
            this.btnExcluir.Text = "Delete";
            this.btnExcluir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExcluir.UseVisualStyleBackColor = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // frmNotificationGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1168, 632);
            this.Controls.Add(this.lvAllowed);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cmbNotificationGroup);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.dtgBdFiles);
            this.Controls.Add(this.document_typeBindingNavigator);
            this.Controls.Add(this.lblGroups);
            this.MinimumSize = new System.Drawing.Size(745, 645);
            this.Name = "frmNotificationGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Notification Groups";
            this.Load += new System.EventHandler(this.frmGroupUser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.document_typeBindingNavigator)).EndInit();
            this.document_typeBindingNavigator.ResumeLayout(false);
            this.document_typeBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgBdFiles)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.BindingSource groupBindingSource;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblGroups;
		private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.BindingNavigator document_typeBindingNavigator;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.ToolStripLabel lbNewUser;
        private System.Windows.Forms.ToolStripButton btnAdd;
        public SpiderDocsForms.DocumentDataGridView dtgBdFiles;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_id_doc;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_title;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_version;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn reason;
        private System.Windows.Forms.DataGridViewTextBoxColumn group_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn extension;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_sp_status;
        private System.Windows.Forms.ToolStripButton tsbSaveAsPDF4User;
        private System.Windows.Forms.ToolStripButton tsbSaveAsPDF4Docs;
        private System.Windows.Forms.ComboBox cmbNotificationGroup;
        private System.Windows.Forms.ListView lvGroup;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListView lvUsersOfGroup;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lvAllowed;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        internal System.Windows.Forms.Button btnExcluir;
		private System.Windows.Forms.ToolStripTextBox txtGroup;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}