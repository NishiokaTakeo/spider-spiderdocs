using SpiderDocsForms;

namespace SpiderDocs
{
    partial class frmScan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScan));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSelectScan = new System.Windows.Forms.ToolStripButton();
            this.btnScan = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSelectFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImgRotate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnDeselect = new System.Windows.Forms.ToolStripButton();
            this.btnSelect = new System.Windows.Forms.ToolStripButton();
            this.gpProperty = new System.Windows.Forms.GroupBox();
            this.lblNotificationGroup = new System.Windows.Forms.Label();
            this.cboNotificationGroup = new SpiderCustomComponent.CheckComboBox();
            this.PropertyPanel = new SpiderDocsForms.PropertyPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTipats = new System.Windows.Forms.ToolTip(this.components);
            this.imageBoxCtrl = new SpiderDocs.PBPictureBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblTotPag = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotPagCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSelPagCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblProgressText = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.plSaveOptions = new System.Windows.Forms.Panel();
            this.cbSaveLocal = new System.Windows.Forms.CheckBox();
            this.txtBrowse = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.ckSaveSeparately = new System.Windows.Forms.CheckBox();
            this.ckSaveWorkSpace = new System.Windows.Forms.CheckBox();
            this.toolStripMenu.SuspendLayout();
            this.gpProperty.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.plSaveOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.toolStripSeparator2,
            this.btnSelectScan,
            this.btnScan,
            this.toolStripSeparator1,
            this.btnSelectFile,
            this.toolStripSeparator3,
            this.btnImgRotate,
            this.toolStripSeparator4,
            this.toolStripSeparator5,
            this.btnDelete,
            this.btnDeselect,
            this.btnSelect});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripMenu.Size = new System.Drawing.Size(866, 54);
            this.toolStripMenu.TabIndex = 1;
            this.toolStripMenu.Text = "testtttt";
            // 
            // btnSave
            // 
            this.btnSave.Image = global::SpiderDocs.Properties.Resources.btn_save;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Margin = new System.Windows.Forms.Padding(8, 1, 8, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(36, 51);
            this.btnSave.Text = "Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 54);
            // 
            // btnSelectScan
            // 
            this.btnSelectScan.Image = global::SpiderDocs.Properties.Resources.devices;
            this.btnSelectScan.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSelectScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectScan.Margin = new System.Windows.Forms.Padding(5, 1, 8, 2);
            this.btnSelectScan.Name = "btnSelectScan";
            this.btnSelectScan.Size = new System.Drawing.Size(87, 51);
            this.btnSelectScan.Text = "Select Scanner";
            this.btnSelectScan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSelectScan.Click += new System.EventHandler(this.btnSelectScan_Click);
            // 
            // btnScan
            // 
            this.btnScan.Image = global::SpiderDocs.Properties.Resources.scanner;
            this.btnScan.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnScan.Margin = new System.Windows.Forms.Padding(0, 1, 8, 2);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(36, 51);
            this.btnScan.Text = "Scan";
            this.btnScan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Image = global::SpiderDocs.Properties.Resources.import;
            this.btnSelectFile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSelectFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectFile.Margin = new System.Windows.Forms.Padding(0, 1, 1, 2);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(68, 51);
            this.btnSelectFile.Text = "Import File";
            this.btnSelectFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSelectFile.ToolTipText = "Import Image";
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 54);
            // 
            // btnImgRotate
            // 
            this.btnImgRotate.Image = ((System.Drawing.Image)(resources.GetObject("btnImgRotate.Image")));
            this.btnImgRotate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnImgRotate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImgRotate.Name = "btnImgRotate";
            this.btnImgRotate.Size = new System.Drawing.Size(57, 51);
            this.btnImgRotate.Text = "  Rotate  ";
            this.btnImgRotate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImgRotate.Click += new System.EventHandler(this.btnImgRotate_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 54);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 54);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(50, 51);
            this.btnDelete.Text = " Delete ";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.ToolTipText = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnDeselect
            // 
            this.btnDeselect.Image = global::SpiderDocs.Properties.Resources.uncheck;
            this.btnDeselect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDeselect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeselect.Name = "btnDeselect";
            this.btnDeselect.Size = new System.Drawing.Size(61, 51);
            this.btnDeselect.Text = " Deselect ";
            this.btnDeselect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeselect.ToolTipText = "Deselect all files scanned";
            this.btnDeselect.Visible = false;
            this.btnDeselect.Click += new System.EventHandler(this.btnDeselect_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Image = global::SpiderDocs.Properties.Resources.check;
            this.btnSelect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(65, 51);
            this.btnSelect.Text = " Select All ";
            this.btnSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSelect.ToolTipText = "Select all files scanned";
            this.btnSelect.Visible = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // gpProperty
            // 
            this.gpProperty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gpProperty.Controls.Add(this.lblNotificationGroup);
            this.gpProperty.Controls.Add(this.cboNotificationGroup);
            this.gpProperty.Controls.Add(this.PropertyPanel);
            this.gpProperty.Controls.Add(this.label2);
            this.gpProperty.Location = new System.Drawing.Point(1, 60);
            this.gpProperty.Name = "gpProperty";
            this.gpProperty.Size = new System.Drawing.Size(296, 262);
            this.gpProperty.TabIndex = 110;
            this.gpProperty.TabStop = false;
            // 
            // lblNotificationGroup
            // 
            this.lblNotificationGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNotificationGroup.AutoSize = true;
            this.lblNotificationGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotificationGroup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNotificationGroup.Location = new System.Drawing.Point(3, 238);
            this.lblNotificationGroup.Name = "lblNotificationGroup";
            this.lblNotificationGroup.Size = new System.Drawing.Size(95, 13);
            this.lblNotificationGroup.TabIndex = 1017;
            this.lblNotificationGroup.Text = "Notification Group:";
            // 
            // cboNotificationGroup
            // 
            this.cboNotificationGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cboNotificationGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboNotificationGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboNotificationGroup.CheckOnClick = true;
            this.cboNotificationGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboNotificationGroup.DropDownHeight = 1;
            this.cboNotificationGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboNotificationGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNotificationGroup.FormattingEnabled = true;
            this.cboNotificationGroup.IntegralHeight = false;
            this.cboNotificationGroup.Location = new System.Drawing.Point(101, 235);
            this.cboNotificationGroup.MultiSelectable = true;
            this.cboNotificationGroup.Name = "cboNotificationGroup";
            this.cboNotificationGroup.Size = new System.Drawing.Size(192, 21);
            this.cboNotificationGroup.TabIndex = 1016;
            this.cboNotificationGroup.ValueSeparator = ", ";
            // 
            // PropertyPanel
            // 
            this.PropertyPanel.FolderFilterPermission = SpiderDocsModule.en_Actions.None;
            this.PropertyPanel.FormMode = SpiderDocsForms.PropertyPanel.en_FormMode.Multiple;
            this.PropertyPanel.IsSameAttribute = false;
            this.PropertyPanel.Location = new System.Drawing.Point(6, 18);
            this.PropertyPanel.Name = "PropertyPanel";
            this.PropertyPanel.Size = new System.Drawing.Size(287, 211);
            this.PropertyPanel.TabIndex = 527;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoEllipsis = true;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(3, -2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Document details";
            // 
            // toolTipats
            // 
            this.toolTipats.IsBalloon = true;
            this.toolTipats.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // imageBoxCtrl
            // 
            this.imageBoxCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageBoxCtrl.BackColor = System.Drawing.Color.Transparent;
            this.imageBoxCtrl.Count = 0;
            this.imageBoxCtrl.Location = new System.Drawing.Point(0, 60);
            this.imageBoxCtrl.Name = "imageBoxCtrl";
            this.imageBoxCtrl.Size = new System.Drawing.Size(866, 479);
            this.imageBoxCtrl.TabIndex = 111;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTotPag,
            this.lblTotPagCount,
            this.toolStripStatusLabel1,
            this.lblSelPagCount,
            this.lblProgress,
            this.lblProgressText,
            this.lblSpace,
            this.progressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 551);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(866, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 112;
            this.statusStrip.Text = "statusStrip";
            // 
            // lblTotPag
            // 
            this.lblTotPag.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTotPag.Name = "lblTotPag";
            this.lblTotPag.Size = new System.Drawing.Size(62, 17);
            this.lblTotPag.Text = "Total Pag: ";
            this.lblTotPag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotPagCount
            // 
            this.lblTotPagCount.AutoSize = false;
            this.lblTotPagCount.Name = "lblTotPagCount";
            this.lblTotPagCount.Size = new System.Drawing.Size(45, 17);
            this.lblTotPagCount.Text = "0";
            this.lblTotPagCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(80, 17);
            this.toolStripStatusLabel1.Text = "Selected Pag: ";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSelPagCount
            // 
            this.lblSelPagCount.AutoSize = false;
            this.lblSelPagCount.Name = "lblSelPagCount";
            this.lblSelPagCount.Size = new System.Drawing.Size(45, 17);
            this.lblSelPagCount.Text = "0";
            this.lblSelPagCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = false;
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(60, 17);
            this.lblProgress.Text = "Progress: ";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProgressText
            // 
            this.lblProgressText.AutoSize = false;
            this.lblProgressText.Name = "lblProgressText";
            this.lblProgressText.Size = new System.Drawing.Size(210, 17);
            this.lblProgressText.Text = "WWWWWWWWWWWWWWWWWWWW";
            this.lblProgressText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSpace
            // 
            this.lblSpace.AutoSize = false;
            this.lblSpace.Name = "lblSpace";
            this.lblSpace.Size = new System.Drawing.Size(10, 17);
            this.lblSpace.Text = " ";
            // 
            // progressBar
            // 
            this.progressBar.AutoSize = false;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(230, 16);
            // 
            // plSaveOptions
            // 
            this.plSaveOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plSaveOptions.Controls.Add(this.cbSaveLocal);
            this.plSaveOptions.Controls.Add(this.txtBrowse);
            this.plSaveOptions.Controls.Add(this.btnBrowse);
            this.plSaveOptions.Controls.Add(this.ckSaveSeparately);
            this.plSaveOptions.Controls.Add(this.ckSaveWorkSpace);
            this.plSaveOptions.Location = new System.Drawing.Point(1, 322);
            this.plSaveOptions.Name = "plSaveOptions";
            this.plSaveOptions.Size = new System.Drawing.Size(296, 89);
            this.plSaveOptions.TabIndex = 526;
            // 
            // cbSaveLocal
            // 
            this.cbSaveLocal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSaveLocal.AutoSize = true;
            this.cbSaveLocal.Location = new System.Drawing.Point(9, 41);
            this.cbSaveLocal.Name = "cbSaveLocal";
            this.cbSaveLocal.Size = new System.Drawing.Size(161, 17);
            this.cbSaveLocal.TabIndex = 530;
            this.cbSaveLocal.Text = "Save this file to a local drive.";
            this.cbSaveLocal.UseVisualStyleBackColor = true;
            this.cbSaveLocal.CheckedChanged += new System.EventHandler(this.cbSaveLoc_CheckedChanged);
            // 
            // txtBrowse
            // 
            this.txtBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBrowse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBrowse.Enabled = false;
            this.txtBrowse.Location = new System.Drawing.Point(15, 62);
            this.txtBrowse.Name = "txtBrowse";
            this.txtBrowse.Size = new System.Drawing.Size(209, 20);
            this.txtBrowse.TabIndex = 529;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowse.Enabled = false;
            this.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowse.Location = new System.Drawing.Point(227, 59);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(66, 23);
            this.btnBrowse.TabIndex = 528;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // ckSaveSeparately
            // 
            this.ckSaveSeparately.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ckSaveSeparately.AutoSize = true;
            this.ckSaveSeparately.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckSaveSeparately.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ckSaveSeparately.Location = new System.Drawing.Point(9, 3);
            this.ckSaveSeparately.Name = "ckSaveSeparately";
            this.ckSaveSeparately.Size = new System.Drawing.Size(159, 17);
            this.ckSaveSeparately.TabIndex = 527;
            this.ckSaveSeparately.Text = "Save each page separately.";
            this.ckSaveSeparately.UseVisualStyleBackColor = true;
            // 
            // ckSaveWorkSpace
            // 
            this.ckSaveWorkSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ckSaveWorkSpace.AutoSize = true;
            this.ckSaveWorkSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckSaveWorkSpace.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ckSaveWorkSpace.Location = new System.Drawing.Point(9, 22);
            this.ckSaveWorkSpace.Name = "ckSaveWorkSpace";
            this.ckSaveWorkSpace.Size = new System.Drawing.Size(178, 17);
            this.ckSaveWorkSpace.TabIndex = 526;
            this.ckSaveWorkSpace.Text = "Save document in Work Space.";
            this.ckSaveWorkSpace.UseVisualStyleBackColor = true;
            this.ckSaveWorkSpace.CheckedChanged += new System.EventHandler(this.ckSaveWorkSpace_CheckedChanged);
            // 
            // frmScan
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(866, 573);
            this.Controls.Add(this.plSaveOptions);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.gpProperty);
            this.Controls.Add(this.toolStripMenu);
            this.Controls.Add(this.imageBoxCtrl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmScan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Spider Docs - Scan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmScan_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmScan_FormClosed);
            this.Load += new System.EventHandler(this.frmScan_Load);
            this.Shown += new System.EventHandler(this.frmScan_Shown);
            this.Resize += new System.EventHandler(this.frmScan_Resize);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.gpProperty.ResumeLayout(false);
            this.gpProperty.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.plSaveOptions.ResumeLayout(false);
            this.plSaveOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton btnScan;
        private System.Windows.Forms.ToolStripButton btnSelectFile;
        private System.Windows.Forms.ToolStripButton btnSelectScan;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox gpProperty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTipats;
		private System.Windows.Forms.ToolStripButton btnImgRotate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnSelect;
        private System.Windows.Forms.ToolStripButton btnDeselect;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblTotPag;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        public System.Windows.Forms.ToolStripStatusLabel lblTotPagCount;
        public System.Windows.Forms.ToolStripStatusLabel lblSelPagCount;
        private System.Windows.Forms.ToolStripStatusLabel lblProgressText;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblProgress;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		public PBPictureBox imageBoxCtrl;
		private System.Windows.Forms.ToolStripStatusLabel lblSpace;
		private System.Windows.Forms.Panel plSaveOptions;
		private System.Windows.Forms.CheckBox cbSaveLocal;
		private System.Windows.Forms.TextBox txtBrowse;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.CheckBox ckSaveSeparately;
		private System.Windows.Forms.CheckBox ckSaveWorkSpace;
        private PropertyPanel PropertyPanel;
        private System.Windows.Forms.Label lblNotificationGroup;
        private SpiderCustomComponent.CheckComboBox cboNotificationGroup;
    }
}