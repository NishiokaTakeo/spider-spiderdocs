namespace SpiderDocs
{
    partial class frmImportFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportFiles));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtgLocalFile = new System.Windows.Forms.DataGridView();
            this.dtgSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gpDetails = new System.Windows.Forms.GroupBox();
            this.ckSameAtb = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboDocType = new System.Windows.Forms.ComboBox();
            this.cboFolder = new SpiderDocsForms.FolderComboBox(this.components);
            this.ckSaveLocal = new System.Windows.Forms.CheckBox();
            this.rdChangeName = new System.Windows.Forms.RadioButton();
            this.rdMaintain = new System.Windows.Forms.RadioButton();
            this.rdDeleteFile = new System.Windows.Forms.RadioButton();
            this.gpAfterSave = new System.Windows.Forms.GroupBox();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.btnSelectFiles = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblTotPag = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblProgressText = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.menudtgLocalFile = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUncheck = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblNotificationGroup = new System.Windows.Forms.Label();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cboNotificationGroup = new SpiderCustomComponent.CheckComboBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLocalFile)).BeginInit();
            this.gpDetails.SuspendLayout();
            this.gpAfterSave.SuspendLayout();
            this.toolStripMenu.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.menudtgLocalFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "folder.png");
            this.imageList2.Images.SetKeyName(1, "folder_256.png");
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(5, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(592, 459);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selected Files";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(469, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 17);
            this.label2.TabIndex = 112;
            this.label2.Text = "Document details";
            // 
            // dtgLocalFile
            // 
            this.dtgLocalFile.AllowDrop = true;
            this.dtgLocalFile.AllowUserToAddRows = false;
            this.dtgLocalFile.AllowUserToDeleteRows = false;
            this.dtgLocalFile.AllowUserToResizeRows = false;
            this.dtgLocalFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgLocalFile.BackgroundColor = System.Drawing.Color.FloralWhite;
            this.dtgLocalFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgLocalFile.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dtgLocalFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgLocalFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dtgSelect,
            this.dataGridViewImageColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.path,
            this.Status});
            this.dtgLocalFile.GridColor = System.Drawing.Color.Gainsboro;
            this.dtgLocalFile.Location = new System.Drawing.Point(6, 70);
            this.dtgLocalFile.Name = "dtgLocalFile";
            this.dtgLocalFile.ReadOnly = true;
            this.dtgLocalFile.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Beige;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgLocalFile.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgLocalFile.RowHeadersVisible = false;
            this.dtgLocalFile.RowHeadersWidth = 20;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FloralWhite;
            this.dtgLocalFile.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dtgLocalFile.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgLocalFile.Size = new System.Drawing.Size(588, 437);
            this.dtgLocalFile.TabIndex = 10;
            this.dtgLocalFile.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgLocalFile_CellContentClick);
            this.dtgLocalFile.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtgLocalFile_CellMouseClick);
            this.dtgLocalFile.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dtgLocalFile_RowsAdded);
            this.dtgLocalFile.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dtgLocalFile_RowsRemoved);
            this.dtgLocalFile.SelectionChanged += new System.EventHandler(this.dtgLocalFile_SelectionChanged);
            // 
            // dtgSelect
            // 
            this.dtgSelect.FalseValue = "";
            this.dtgSelect.HeaderText = "";
            this.dtgSelect.Name = "dtgSelect";
            this.dtgSelect.ReadOnly = true;
            this.dtgSelect.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgSelect.TrueValue = "";
            this.dtgSelect.Width = 25;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn1.Width = 25;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.FillWeight = 35F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Size";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Type";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // path
            // 
            this.path.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.path.FillWeight = 65F;
            this.path.HeaderText = "Original path";
            this.path.Name = "path";
            this.path.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // gpDetails
            // 
            this.gpDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpDetails.Controls.Add(this.ckSameAtb);
            this.gpDetails.Controls.Add(this.label1);
            this.gpDetails.Controls.Add(this.label4);
            this.gpDetails.Controls.Add(this.cboDocType);
            this.gpDetails.Controls.Add(this.cboFolder);
            this.gpDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpDetails.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gpDetails.Location = new System.Drawing.Point(608, 53);
            this.gpDetails.Name = "gpDetails";
            this.gpDetails.Size = new System.Drawing.Size(294, 303);
            this.gpDetails.TabIndex = 80;
            this.gpDetails.TabStop = false;
            // 
            // ckSameAtb
            // 
            this.ckSameAtb.AutoSize = true;
            this.ckSameAtb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckSameAtb.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ckSameAtb.Location = new System.Drawing.Point(9, 24);
            this.ckSameAtb.Name = "ckSameAtb";
            this.ckSameAtb.Size = new System.Drawing.Size(160, 17);
            this.ckSameAtb.TabIndex = 518;
            this.ckSameAtb.Text = "Use these values for all files.";
            this.ckSameAtb.UseVisualStyleBackColor = true;
            this.ckSameAtb.CheckedChanged += new System.EventHandler(this.ckSameAtb_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(4, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 111;
            this.label1.Text = "Folder:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(4, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 108;
            this.label4.Text = "Document Type:";
            // 
            // cboDocType
            // 
            this.cboDocType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboDocType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDocType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboDocType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDocType.FormattingEnabled = true;
            this.cboDocType.Location = new System.Drawing.Point(105, 74);
            this.cboDocType.Name = "cboDocType";
            this.cboDocType.Size = new System.Drawing.Size(178, 21);
            this.cboDocType.TabIndex = 2;
            this.cboDocType.SelectedIndexChanged += new System.EventHandler(this.cboDocType_SelectedIndexChanged);
            // 
            // cboFolder
            // 
            this.cboFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboFolder.DisplayMember = "document_folder";
            this.cboFolder.DropDownHeight = 1;
            this.cboFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFolder.FormattingEnabled = true;
            this.cboFolder.IntegralHeight = false;
            this.cboFolder.Location = new System.Drawing.Point(105, 47);
            this.cboFolder.Name = "cboFolder";
            this.cboFolder.Size = new System.Drawing.Size(178, 21);
            this.cboFolder.TabIndex = 1;
            this.cboFolder.ValueMember = "id";
            // 
            // ckSaveLocal
            // 
            this.ckSaveLocal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ckSaveLocal.AutoSize = true;
            this.ckSaveLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckSaveLocal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ckSaveLocal.Location = new System.Drawing.Point(608, 396);
            this.ckSaveLocal.Name = "ckSaveLocal";
            this.ckSaveLocal.Size = new System.Drawing.Size(178, 17);
            this.ckSaveLocal.TabIndex = 113;
            this.ckSaveLocal.Text = "Save document in Work Space.";
            this.ckSaveLocal.UseVisualStyleBackColor = true;
            this.ckSaveLocal.CheckedChanged += new System.EventHandler(this.ckSaveLocal_CheckedChanged);
            // 
            // rdChangeName
            // 
            this.rdChangeName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdChangeName.AutoSize = true;
            this.rdChangeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdChangeName.Location = new System.Drawing.Point(9, 19);
            this.rdChangeName.Name = "rdChangeName";
            this.rdChangeName.Size = new System.Drawing.Size(127, 17);
            this.rdChangeName.TabIndex = 71;
            this.rdChangeName.Text = "Change name (DMS-)";
            this.rdChangeName.UseVisualStyleBackColor = true;
            // 
            // rdMaintain
            // 
            this.rdMaintain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdMaintain.AutoSize = true;
            this.rdMaintain.Checked = true;
            this.rdMaintain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdMaintain.Location = new System.Drawing.Point(9, 42);
            this.rdMaintain.Name = "rdMaintain";
            this.rdMaintain.Size = new System.Drawing.Size(106, 17);
            this.rdMaintain.TabIndex = 69;
            this.rdMaintain.TabStop = true;
            this.rdMaintain.Text = "Maintain local file";
            this.rdMaintain.UseVisualStyleBackColor = true;
            // 
            // rdDeleteFile
            // 
            this.rdDeleteFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdDeleteFile.AutoSize = true;
            this.rdDeleteFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdDeleteFile.Location = new System.Drawing.Point(142, 17);
            this.rdDeleteFile.Name = "rdDeleteFile";
            this.rdDeleteFile.Size = new System.Drawing.Size(97, 17);
            this.rdDeleteFile.TabIndex = 70;
            this.rdDeleteFile.Text = "Delete local file";
            this.rdDeleteFile.UseVisualStyleBackColor = true;
            // 
            // gpAfterSave
            // 
            this.gpAfterSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gpAfterSave.Controls.Add(this.rdDeleteFile);
            this.gpAfterSave.Controls.Add(this.rdMaintain);
            this.gpAfterSave.Controls.Add(this.rdChangeName);
            this.gpAfterSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpAfterSave.Location = new System.Drawing.Point(608, 419);
            this.gpAfterSave.Name = "gpAfterSave";
            this.gpAfterSave.Size = new System.Drawing.Size(294, 63);
            this.gpAfterSave.TabIndex = 85;
            this.gpAfterSave.TabStop = false;
            this.gpAfterSave.Text = "After save:";
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSelectFiles});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripMenu.Size = new System.Drawing.Size(910, 52);
            this.toolStripMenu.TabIndex = 86;
            this.toolStripMenu.Text = "testtttt";
            // 
            // btnSelectFiles
            // 
            this.btnSelectFiles.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectFiles.Image")));
            this.btnSelectFiles.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSelectFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectFiles.Margin = new System.Windows.Forms.Padding(0, 1, 1, 2);
            this.btnSelectFiles.Name = "btnSelectFiles";
            this.btnSelectFiles.Size = new System.Drawing.Size(88, 49);
            this.btnSelectFiles.Text = "Add more files";
            this.btnSelectFiles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSelectFiles.ToolTipText = "Import Image";
            this.btnSelectFiles.Click += new System.EventHandler(this.btnSelectFiles_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTotPag,
            this.lblCount,
            this.lblProgress,
            this.lblProgressText,
            this.lblSpace,
            this.progressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 514);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(910, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 114;
            this.statusStrip.Text = "statusStrip";
            // 
            // lblTotPag
            // 
            this.lblTotPag.AutoSize = false;
            this.lblTotPag.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTotPag.Name = "lblTotPag";
            this.lblTotPag.Size = new System.Drawing.Size(40, 17);
            this.lblTotPag.Text = "Files: ";
            this.lblTotPag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = false;
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(45, 17);
            this.lblCount.Text = "0";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = false;
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(60, 17);
            this.lblProgress.Text = "Progress:";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProgressText
            // 
            this.lblProgressText.AutoSize = false;
            this.lblProgressText.Name = "lblProgressText";
            this.lblProgressText.Size = new System.Drawing.Size(200, 17);
            this.lblProgressText.Text = "  ";
            this.lblProgressText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSpace
            // 
            this.lblSpace.AutoSize = false;
            this.lblSpace.Name = "lblSpace";
            this.lblSpace.Size = new System.Drawing.Size(10, 17);
            this.lblSpace.Text = "  ";
            this.lblSpace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            this.progressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(270, 16);
            // 
            // menudtgLocalFile
            // 
            this.menudtgLocalFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCheck,
            this.menuUncheck});
            this.menudtgLocalFile.Name = "menuDtgSystemFiles";
            this.menudtgLocalFile.Size = new System.Drawing.Size(121, 48);
            // 
            // menuCheck
            // 
            this.menuCheck.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuCheck.Name = "menuCheck";
            this.menuCheck.Size = new System.Drawing.Size(120, 22);
            this.menuCheck.Text = "Check";
            this.menuCheck.Click += new System.EventHandler(this.menuCheck_Click);
            // 
            // menuUncheck
            // 
            this.menuUncheck.Name = "menuUncheck";
            this.menuUncheck.Size = new System.Drawing.Size(120, 22);
            this.menuUncheck.Text = "Uncheck";
            this.menuUncheck.Click += new System.EventHandler(this.menuUncheck_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(811, 488);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 23);
            this.btnSave.TabIndex = 517;
            this.btnSave.Text = "    Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblNotificationGroup
            // 
            this.lblNotificationGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNotificationGroup.AutoSize = true;
            this.lblNotificationGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotificationGroup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNotificationGroup.Location = new System.Drawing.Point(611, 367);
            this.lblNotificationGroup.Name = "lblNotificationGroup";
            this.lblNotificationGroup.Size = new System.Drawing.Size(95, 13);
            this.lblNotificationGroup.TabIndex = 1015;
            this.lblNotificationGroup.Text = "Notification Group:";
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.FalseValue = "";
            this.dataGridViewCheckBoxColumn1.HeaderText = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCheckBoxColumn1.TrueValue = "";
            this.dataGridViewCheckBoxColumn1.Width = 25;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.FillWeight = 65F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Original path";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Status";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // cboNotificationGroup
            // 
            this.cboNotificationGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cboNotificationGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboNotificationGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboNotificationGroup.CheckOnClick = true;
            this.cboNotificationGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboNotificationGroup.DropDownHeight = 1;
            this.cboNotificationGroup.FormattingEnabled = true;
            this.cboNotificationGroup.IntegralHeight = false;
            this.cboNotificationGroup.Location = new System.Drawing.Point(708, 362);
            this.cboNotificationGroup.MultiSelectable = true;
            this.cboNotificationGroup.Name = "cboNotificationGroup";
            this.cboNotificationGroup.Size = new System.Drawing.Size(194, 21);
            this.cboNotificationGroup.TabIndex = 1016;
            this.cboNotificationGroup.ValueSeparator = ", ";
            // 
            // frmImportFiles
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(910, 536);
            this.Controls.Add(this.cboNotificationGroup);
            this.Controls.Add(this.lblNotificationGroup);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.ckSaveLocal);
            this.Controls.Add(this.toolStripMenu);
            this.Controls.Add(this.gpAfterSave);
            this.Controls.Add(this.dtgLocalFile);
            this.Controls.Add(this.gpDetails);
            this.Controls.Add(this.groupBox2);
            this.MinimumSize = new System.Drawing.Size(722, 527);
            this.Name = "frmImportFiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Files";
            this.Load += new System.EventHandler(this.frmImportFiles_Load);
            this.Shown += new System.EventHandler(this.frmImportFiles_Shown);
            this.Resize += new System.EventHandler(this.frmImportFiles_Resize);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLocalFile)).EndInit();
            this.gpDetails.ResumeLayout(false);
            this.gpDetails.PerformLayout();
            this.gpAfterSave.ResumeLayout(false);
            this.gpAfterSave.PerformLayout();
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menudtgLocalFile.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ImageList imageList2;
        public System.Windows.Forms.DataGridView dtgLocalFile;
        private SpiderDocsForms.FolderComboBox cboFolder;
		private System.Windows.Forms.GroupBox gpDetails;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboDocType;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckSaveLocal;
        private System.Windows.Forms.RadioButton rdChangeName;
        private System.Windows.Forms.RadioButton rdMaintain;
        private System.Windows.Forms.RadioButton rdDeleteFile;
        private System.Windows.Forms.GroupBox gpAfterSave;
		private System.Windows.Forms.ToolStrip toolStripMenu;
		private System.Windows.Forms.ToolStripButton btnSelectFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblTotPag;
        public System.Windows.Forms.ToolStripStatusLabel lblCount;
        private System.Windows.Forms.ToolStripStatusLabel lblProgress;
        private System.Windows.Forms.ToolStripStatusLabel lblProgressText;
        private System.Windows.Forms.ToolStripStatusLabel lblSpace;
		private System.Windows.Forms.ToolStripProgressBar progressBar;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dtgSelect;
		private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn path;
		private System.Windows.Forms.DataGridViewTextBoxColumn Status;
		private System.Windows.Forms.ContextMenuStrip menudtgLocalFile;
		private System.Windows.Forms.ToolStripMenuItem menuCheck;
		private System.Windows.Forms.ToolStripMenuItem menuUncheck;
		public System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.CheckBox ckSameAtb;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Label lblNotificationGroup;
        private SpiderCustomComponent.CheckComboBox cboNotificationGroup;
    }
}