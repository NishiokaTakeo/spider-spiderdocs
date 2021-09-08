namespace SpiderDocs
{
    partial class frmReportBuilder
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
            if (disposing && (components != null))
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportBuilder));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.pbAddField = new System.Windows.Forms.PictureBox();
			this.pbAddCondition = new System.Windows.Forms.PictureBox();
			this.document_typeBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
			this.btnAdd = new System.Windows.Forms.ToolStripButton();
			this.btnSave = new System.Windows.Forms.ToolStripButton();
			this.btnDelete = new System.Windows.Forms.ToolStripButton();
			this.tslblReportName = new System.Windows.Forms.ToolStripLabel();
			this.lbNewReport = new System.Windows.Forms.ToolStripLabel();
			this.tstxtReports = new System.Windows.Forms.ToolStripTextBox();
			this.tslblCategory = new System.Windows.Forms.ToolStripLabel();
			this.tscmbCategory = new System.Windows.Forms.ToolStripComboBox();
			this.lvFields = new System.Windows.Forms.ListView();
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.label1 = new System.Windows.Forms.Label();
			this.dgvFields = new System.Windows.Forms.DataGridView();
			this.dgvFilters = new System.Windows.Forms.DataGridView();
			this.label4 = new System.Windows.Forms.Label();
			this.lvFilter = new System.Windows.Forms.ListView();
			this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lvReportList = new System.Windows.Forms.ListView();
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.btnDelReport = new System.Windows.Forms.Button();
			this.btnGenerate = new System.Windows.Forms.Button();
			this.groupBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
			this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.label2 = new System.Windows.Forms.Label();
			this.lblSelectedCategory = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pbAddField)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbAddCondition)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.document_typeBindingNavigator)).BeginInit();
			this.document_typeBindingNavigator.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvFields)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvFilters)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "group.png");
			this.imageList1.Images.SetKeyName(1, "Preppy-icon.png");
			// 
			// pbAddField
			// 
			this.pbAddField.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.pbAddField.BackColor = System.Drawing.Color.WhiteSmoke;
			this.pbAddField.Image = ((System.Drawing.Image)(resources.GetObject("pbAddField.Image")));
			this.pbAddField.Location = new System.Drawing.Point(324, 131);
			this.pbAddField.Name = "pbAddField";
			this.pbAddField.Size = new System.Drawing.Size(32, 32);
			this.pbAddField.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbAddField.TabIndex = 64;
			this.pbAddField.TabStop = false;
			this.toolTip1.SetToolTip(this.pbAddField, "Add a field");
			this.pbAddField.Click += new System.EventHandler(this.pbAddField_Click);
			// 
			// pbAddCondition
			// 
			this.pbAddCondition.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.pbAddCondition.BackColor = System.Drawing.Color.WhiteSmoke;
			this.pbAddCondition.Image = ((System.Drawing.Image)(resources.GetObject("pbAddCondition.Image")));
			this.pbAddCondition.InitialImage = ((System.Drawing.Image)(resources.GetObject("pbAddCondition.InitialImage")));
			this.pbAddCondition.Location = new System.Drawing.Point(324, 133);
			this.pbAddCondition.Name = "pbAddCondition";
			this.pbAddCondition.Size = new System.Drawing.Size(32, 32);
			this.pbAddCondition.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbAddCondition.TabIndex = 65;
			this.pbAddCondition.TabStop = false;
			this.toolTip1.SetToolTip(this.pbAddCondition, "Add a condition");
			this.pbAddCondition.Click += new System.EventHandler(this.pbAddFilter_Click);
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
            this.btnSave,
            this.btnDelete,
            this.tslblReportName,
            this.lbNewReport,
            this.tstxtReports,
            this.tslblCategory,
            this.tscmbCategory});
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
			// btnSave
			// 
			this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnSave.Image = global::SpiderDocs.Properties.Resources.salvar;
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(23, 22);
			this.btnSave.Text = "Save Data";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
			// tslblReportName
			// 
			this.tslblReportName.Name = "tslblReportName";
			this.tslblReportName.Size = new System.Drawing.Size(77, 22);
			this.tslblReportName.Text = "Report Name";
			this.tslblReportName.Visible = false;
			// 
			// lbNewReport
			// 
			this.lbNewReport.ForeColor = System.Drawing.Color.Red;
			this.lbNewReport.Name = "lbNewReport";
			this.lbNewReport.Size = new System.Drawing.Size(105, 22);
			this.lbNewReport.Text = "*** New Report ***";
			this.lbNewReport.Visible = false;
			// 
			// tstxtReports
			// 
			this.tstxtReports.Name = "tstxtReports";
			this.tstxtReports.Size = new System.Drawing.Size(200, 25);
			this.tstxtReports.Visible = false;
			// 
			// tslblCategory
			// 
			this.tslblCategory.Name = "tslblCategory";
			this.tslblCategory.Size = new System.Drawing.Size(55, 22);
			this.tslblCategory.Text = "Category";
			this.tslblCategory.Visible = false;
			// 
			// tscmbCategory
			// 
			this.tscmbCategory.AutoCompleteCustomSource.AddRange(new string[] {
            "Report_Name"});
			this.tscmbCategory.Name = "tscmbCategory";
			this.tscmbCategory.Size = new System.Drawing.Size(121, 25);
			this.tscmbCategory.Visible = false;
			// 
			// lvFields
			// 
			this.lvFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lvFields.BackColor = System.Drawing.Color.FloralWhite;
			this.lvFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8});
			this.lvFields.FullRowSelect = true;
			this.lvFields.HideSelection = false;
			this.lvFields.Location = new System.Drawing.Point(12, 30);
			this.lvFields.MultiSelect = false;
			this.lvFields.Name = "lvFields";
			this.lvFields.Size = new System.Drawing.Size(302, 232);
			this.lvFields.SmallImageList = this.imageList1;
			this.lvFields.TabIndex = 62;
			this.lvFields.UseCompatibleStateImageBehavior = false;
			this.lvFields.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "";
			this.columnHeader7.Width = 24;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Fields for Select";
			this.columnHeader8.Width = 220;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(326, 69);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.pbAddField);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			this.splitContainer1.Panel1.Controls.Add(this.lvFields);
			this.splitContainer1.Panel1.Controls.Add(this.dgvFields);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.dgvFilters);
			this.splitContainer1.Panel2.Controls.Add(this.label4);
			this.splitContainer1.Panel2.Controls.Add(this.pbAddCondition);
			this.splitContainer1.Panel2.Controls.Add(this.lvFilter);
			this.splitContainer1.Size = new System.Drawing.Size(830, 536);
			this.splitContainer1.SplitterDistance = 267;
			this.splitContainer1.TabIndex = 63;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 13);
			this.label1.TabIndex = 63;
			this.label1.Text = "Fields";
			// 
			// dgvFields
			// 
			this.dgvFields.AllowDrop = true;
			this.dgvFields.AllowUserToAddRows = false;
			this.dgvFields.AllowUserToDeleteRows = false;
			this.dgvFields.AllowUserToOrderColumns = true;
			this.dgvFields.AllowUserToResizeRows = false;
			this.dgvFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvFields.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvFields.BackgroundColor = System.Drawing.Color.White;
			this.dgvFields.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvFields.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvFields.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
			this.dgvFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvFields.DefaultCellStyle = dataGridViewCellStyle20;
			this.dgvFields.GridColor = System.Drawing.Color.Gainsboro;
			this.dgvFields.Location = new System.Drawing.Point(366, 30);
			this.dgvFields.Name = "dgvFields";
			this.dgvFields.ReadOnly = true;
			this.dgvFields.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle21.SelectionForeColor = System.Drawing.Color.Beige;
			dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvFields.RowHeadersDefaultCellStyle = dataGridViewCellStyle21;
			this.dgvFields.RowHeadersVisible = false;
			this.dgvFields.RowHeadersWidth = 20;
			dataGridViewCellStyle22.BackColor = System.Drawing.Color.White;
			this.dgvFields.RowsDefaultCellStyle = dataGridViewCellStyle22;
			this.dgvFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvFields.Size = new System.Drawing.Size(457, 235);
			this.dgvFields.TabIndex = 52;
			this.dgvFields.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFields_CellClick);
			// 
			// dgvFilters
			// 
			this.dgvFilters.AllowDrop = true;
			this.dgvFilters.AllowUserToAddRows = false;
			this.dgvFilters.AllowUserToDeleteRows = false;
			this.dgvFilters.AllowUserToOrderColumns = true;
			this.dgvFilters.AllowUserToResizeRows = false;
			this.dgvFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvFilters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvFilters.BackgroundColor = System.Drawing.Color.White;
			this.dgvFilters.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvFilters.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvFilters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle23;
			this.dgvFilters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvFilters.DefaultCellStyle = dataGridViewCellStyle24;
			this.dgvFilters.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.dgvFilters.GridColor = System.Drawing.Color.Gainsboro;
			this.dgvFilters.Location = new System.Drawing.Point(366, 27);
			this.dgvFilters.Name = "dgvFilters";
			this.dgvFilters.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle25.SelectionForeColor = System.Drawing.Color.Beige;
			dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvFilters.RowHeadersDefaultCellStyle = dataGridViewCellStyle25;
			this.dgvFilters.RowHeadersVisible = false;
			this.dgvFilters.RowHeadersWidth = 20;
			dataGridViewCellStyle26.BackColor = System.Drawing.Color.White;
			this.dgvFilters.RowsDefaultCellStyle = dataGridViewCellStyle26;
			this.dgvFilters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvFilters.Size = new System.Drawing.Size(457, 235);
			this.dgvFilters.TabIndex = 66;
			this.dgvFilters.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFilters_CellClick);
			this.dgvFilters.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFilters_CellValueChanged);
			this.dgvFilters.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgvFilters_MouseUp);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label4.Location = new System.Drawing.Point(9, 12);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 13);
			this.label4.TabIndex = 66;
			this.label4.Text = "Conditions";
			// 
			// lvFilter
			// 
			this.lvFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lvFilter.BackColor = System.Drawing.Color.FloralWhite;
			this.lvFilter.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10});
			this.lvFilter.FullRowSelect = true;
			this.lvFilter.HideSelection = false;
			this.lvFilter.Location = new System.Drawing.Point(12, 28);
			this.lvFilter.Name = "lvFilter";
			this.lvFilter.Size = new System.Drawing.Size(302, 237);
			this.lvFilter.SmallImageList = this.imageList1;
			this.lvFilter.TabIndex = 64;
			this.lvFilter.UseCompatibleStateImageBehavior = false;
			this.lvFilter.View = System.Windows.Forms.View.Details;
			this.lvFilter.Click += new System.EventHandler(this.lvUsersOfGroup_Click);
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "";
			this.columnHeader9.Width = 25;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Fields for Conditions";
			this.columnHeader10.Width = 220;
			// 
			// lvReportList
			// 
			this.lvReportList.AllowDrop = true;
			this.lvReportList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lvReportList.BackColor = System.Drawing.Color.FloralWhite;
			this.lvReportList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
			this.lvReportList.FullRowSelect = true;
			this.lvReportList.HideSelection = false;
			this.lvReportList.Location = new System.Drawing.Point(14, 98);
			this.lvReportList.MultiSelect = false;
			this.lvReportList.Name = "lvReportList";
			this.lvReportList.Size = new System.Drawing.Size(299, 507);
			this.lvReportList.SmallImageList = this.imageList1;
			this.lvReportList.TabIndex = 64;
			this.lvReportList.UseCompatibleStateImageBehavior = false;
			this.lvReportList.View = System.Windows.Forms.View.Details;
			this.lvReportList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvReportList_ItemSelectionChanged);
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "";
			this.columnHeader3.Width = 25;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Report Name";
			this.columnHeader4.Width = 250;
			// 
			// btnDelReport
			// 
			this.btnDelReport.BackColor = System.Drawing.Color.WhiteSmoke;
			this.btnDelReport.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
			this.btnDelReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSteelBlue;
			this.btnDelReport.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelReport.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.btnDelReport.ImageKey = "icon_delete_user.png";
			this.btnDelReport.ImageList = this.imageList1;
			this.btnDelReport.Location = new System.Drawing.Point(260, 71);
			this.btnDelReport.Margin = new System.Windows.Forms.Padding(1);
			this.btnDelReport.Name = "btnDelReport";
			this.btnDelReport.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.btnDelReport.Size = new System.Drawing.Size(53, 22);
			this.btnDelReport.TabIndex = 60;
			this.btnDelReport.Text = "Delete";
			this.btnDelReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnDelReport.UseVisualStyleBackColor = false;
			this.btnDelReport.Click += new System.EventHandler(this.btnDelReport_Click);
			// 
			// btnGenerate
			// 
			this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGenerate.Location = new System.Drawing.Point(1074, 40);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(75, 23);
			this.btnGenerate.TabIndex = 65;
			this.btnGenerate.Text = "Generate";
			this.btnGenerate.UseVisualStyleBackColor = true;
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
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
			dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle27;
			this.dataGridViewTextBoxColumn2.HeaderText = "Id";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.Visible = false;
			// 
			// dataGridViewImageColumn1
			// 
			this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dataGridViewImageColumn1.DataPropertyName = "c_img";
			this.dataGridViewImageColumn1.HeaderText = "";
			this.dataGridViewImageColumn1.MinimumWidth = 20;
			this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
			this.dataGridViewImageColumn1.ReadOnly = true;
			this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewImageColumn1.Width = 35;
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
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(12, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(69, 17);
			this.label2.TabIndex = 66;
			this.label2.Text = "Category:";
			// 
			// lblSelectedCategory
			// 
			this.lblSelectedCategory.AutoSize = true;
			this.lblSelectedCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSelectedCategory.Location = new System.Drawing.Point(87, 40);
			this.lblSelectedCategory.Name = "lblSelectedCategory";
			this.lblSelectedCategory.Size = new System.Drawing.Size(43, 17);
			this.lblSelectedCategory.TabIndex = 67;
			this.lblSelectedCategory.Text = "xxxxx";
			// 
			// frmReportBuilder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(1168, 632);
			this.Controls.Add(this.lblSelectedCategory);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnGenerate);
			this.Controls.Add(this.lvReportList);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.btnDelReport);
			this.Controls.Add(this.document_typeBindingNavigator);
			this.MinimumSize = new System.Drawing.Size(745, 645);
			this.Name = "frmReportBuilder";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Report Builder";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmReportBuilder_FormClosed);
			this.Load += new System.EventHandler(this.frmReportBuilder_Load);
			((System.ComponentModel.ISupportInitialize)(this.pbAddField)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbAddCondition)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.document_typeBindingNavigator)).EndInit();
			this.document_typeBindingNavigator.ResumeLayout(false);
			this.document_typeBindingNavigator.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvFields)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvFilters)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.BindingSource groupBindingSource;
        private System.Windows.Forms.ToolTip toolTip1;
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
        private System.Windows.Forms.ToolStripLabel lbNewReport;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ListView lvFields;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbAddField;
        private System.Windows.Forms.PictureBox pbAddCondition;
        private System.Windows.Forms.ListView lvFilter;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lvReportList;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        internal System.Windows.Forms.Button btnDelReport;
        private System.Windows.Forms.ToolStripLabel tslblReportName;
        private System.Windows.Forms.ToolStripTextBox tstxtReports;
        private System.Windows.Forms.ToolStripLabel tslblCategory;
        private System.Windows.Forms.ToolStripComboBox tscmbCategory;
        public System.Windows.Forms.DataGridView dgvFilters;
        public System.Windows.Forms.DataGridView dgvFields;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblSelectedCategory;
	}
}