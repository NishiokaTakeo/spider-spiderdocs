namespace SpiderDocsForms
{
	partial class DocumentSaveButtons {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
		if(disposing && (components != null)) {
		components.Dispose();
		}
		base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentSaveButtons));
            this.plButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.plSaveLocal = new System.Windows.Forms.Panel();
            this.txtBrowse = new System.Windows.Forms.TextBox();
            this.cbSaveLoc = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.gpAfterSave = new System.Windows.Forms.GroupBox();
            this.rdMaintain = new System.Windows.Forms.RadioButton();
            this.rdChangeName = new System.Windows.Forms.RadioButton();
            this.rdDeleteFile = new System.Windows.Forms.RadioButton();
            this.chkPDF = new System.Windows.Forms.CheckBox();
            this.cboNotificationGroup = new SpiderCustomComponent.CheckComboBox();
            this.lblNotificationGroup = new System.Windows.Forms.Label();
            this.cbMerge = new System.Windows.Forms.CheckBox();
            this.plButtons.SuspendLayout();
            this.plSaveLocal.SuspendLayout();
            this.gpAfterSave.SuspendLayout();
            this.SuspendLayout();
            // 
            // plButtons
            // 
            this.plButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.plButtons.Controls.Add(this.btnCancel);
            this.plButtons.Controls.Add(this.btnSave);
            this.plButtons.Location = new System.Drawing.Point(421, 31);
            this.plButtons.Name = "plButtons";
            this.plButtons.Size = new System.Drawing.Size(187, 44);
            this.plButtons.TabIndex = 533;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(103, 18);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 23);
            this.btnCancel.TabIndex = 533;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(6, 18);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 23);
            this.btnSave.TabIndex = 534;
            this.btnSave.Text = "    Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // plSaveLocal
            // 
            this.plSaveLocal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plSaveLocal.Controls.Add(this.txtBrowse);
            this.plSaveLocal.Controls.Add(this.cbSaveLoc);
            this.plSaveLocal.Controls.Add(this.btnBrowse);
            this.plSaveLocal.Location = new System.Drawing.Point(8, 25);
            this.plSaveLocal.Name = "plSaveLocal";
            this.plSaveLocal.Size = new System.Drawing.Size(412, 50);
            this.plSaveLocal.TabIndex = 534;
            // 
            // txtBrowse
            // 
            this.txtBrowse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBrowse.Enabled = false;
            this.txtBrowse.Location = new System.Drawing.Point(15, 22);
            this.txtBrowse.Name = "txtBrowse";
            this.txtBrowse.Size = new System.Drawing.Size(258, 20);
            this.txtBrowse.TabIndex = 531;
            // 
            // cbSaveLoc
            // 
            this.cbSaveLoc.AutoSize = true;
            this.cbSaveLoc.Location = new System.Drawing.Point(0, 3);
            this.cbSaveLoc.Name = "cbSaveLoc";
            this.cbSaveLoc.Size = new System.Drawing.Size(161, 17);
            this.cbSaveLoc.TabIndex = 532;
            this.cbSaveLoc.Text = "Save this file to a local drive.";
            this.cbSaveLoc.UseVisualStyleBackColor = true;
            this.cbSaveLoc.CheckedChanged += new System.EventHandler(this.cbSaveLoc_CheckedChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowse.Enabled = false;
            this.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowse.Location = new System.Drawing.Point(276, 21);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(66, 22);
            this.btnBrowse.TabIndex = 530;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // gpAfterSave
            // 
            this.gpAfterSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpAfterSave.Controls.Add(this.rdMaintain);
            this.gpAfterSave.Controls.Add(this.rdChangeName);
            this.gpAfterSave.Controls.Add(this.rdDeleteFile);
            this.gpAfterSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpAfterSave.Location = new System.Drawing.Point(8, 25);
            this.gpAfterSave.Name = "gpAfterSave";
            this.gpAfterSave.Size = new System.Drawing.Size(413, 50);
            this.gpAfterSave.TabIndex = 538;
            this.gpAfterSave.TabStop = false;
            this.gpAfterSave.Text = "After save:";
            // 
            // rdMaintain
            // 
            this.rdMaintain.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rdMaintain.AutoSize = true;
            this.rdMaintain.Checked = true;
            this.rdMaintain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdMaintain.Location = new System.Drawing.Point(139, 22);
            this.rdMaintain.Name = "rdMaintain";
            this.rdMaintain.Size = new System.Drawing.Size(106, 17);
            this.rdMaintain.TabIndex = 69;
            this.rdMaintain.TabStop = true;
            this.rdMaintain.Tag = "";
            this.rdMaintain.Text = "Maintain local file";
            this.rdMaintain.UseVisualStyleBackColor = true;
            // 
            // rdChangeName
            // 
            this.rdChangeName.AutoSize = true;
            this.rdChangeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdChangeName.Location = new System.Drawing.Point(6, 22);
            this.rdChangeName.Name = "rdChangeName";
            this.rdChangeName.Size = new System.Drawing.Size(127, 17);
            this.rdChangeName.TabIndex = 71;
            this.rdChangeName.Text = "Change name (DMS-)";
            this.rdChangeName.UseVisualStyleBackColor = true;
            // 
            // rdDeleteFile
            // 
            this.rdDeleteFile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rdDeleteFile.AutoSize = true;
            this.rdDeleteFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdDeleteFile.Location = new System.Drawing.Point(251, 22);
            this.rdDeleteFile.Name = "rdDeleteFile";
            this.rdDeleteFile.Size = new System.Drawing.Size(97, 17);
            this.rdDeleteFile.TabIndex = 70;
            this.rdDeleteFile.Text = "Delete local file";
            this.rdDeleteFile.UseVisualStyleBackColor = true;
            // 
            // chkPDF
            // 
            this.chkPDF.AutoSize = true;
            this.chkPDF.Location = new System.Drawing.Point(8, 6);
            this.chkPDF.Name = "chkPDF";
            this.chkPDF.Size = new System.Drawing.Size(180, 17);
            this.chkPDF.TabIndex = 537;
            this.chkPDF.Text = "Save as PDF file. (OCR PDF file)";
            this.chkPDF.UseVisualStyleBackColor = true;
            this.chkPDF.CheckedChanged += new System.EventHandler(this.chkPDF_CheckedChanged);
            // 
            // cboNotificationGroup
            // 
            this.cboNotificationGroup.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cboNotificationGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboNotificationGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboNotificationGroup.CheckOnClick = true;
            this.cboNotificationGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboNotificationGroup.DropDownHeight = 1;
            this.cboNotificationGroup.FormattingEnabled = true;
            this.cboNotificationGroup.IntegralHeight = false;
            this.cboNotificationGroup.Location = new System.Drawing.Point(419, 13);
            this.cboNotificationGroup.MultiSelectable = true;
            this.cboNotificationGroup.Name = "cboNotificationGroup";
            this.cboNotificationGroup.Size = new System.Drawing.Size(184, 21);
            this.cboNotificationGroup.TabIndex = 539;
            this.cboNotificationGroup.ValueSeparator = ", ";
            // 
            // lblNotificationGroup
            // 
            this.lblNotificationGroup.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblNotificationGroup.AutoSize = true;
            this.lblNotificationGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotificationGroup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNotificationGroup.Location = new System.Drawing.Point(318, 16);
            this.lblNotificationGroup.Name = "lblNotificationGroup";
            this.lblNotificationGroup.Size = new System.Drawing.Size(95, 13);
            this.lblNotificationGroup.TabIndex = 540;
            this.lblNotificationGroup.Text = "Notification Group:";
            // 
            // cbMerge
            // 
            this.cbMerge.AutoSize = true;
            this.cbMerge.Location = new System.Drawing.Point(189, 6);
            this.cbMerge.Name = "cbMerge";
            this.cbMerge.Size = new System.Drawing.Size(77, 17);
            this.cbMerge.TabIndex = 541;
            this.cbMerge.Text = "Merge files";
            this.cbMerge.UseVisualStyleBackColor = true;
            this.cbMerge.CheckedChanged += new System.EventHandler(this.cbMerge_CheckedChanged);
            // 
            // DocumentSaveButtons
            // 
            this.Controls.Add(this.cbMerge);
            this.Controls.Add(this.cboNotificationGroup);
            this.Controls.Add(this.lblNotificationGroup);
            this.Controls.Add(this.gpAfterSave);
            this.Controls.Add(this.chkPDF);
            this.Controls.Add(this.plButtons);
            this.Controls.Add(this.plSaveLocal);
            this.Name = "DocumentSaveButtons";
            this.Size = new System.Drawing.Size(608, 80);
            this.Resize += new System.EventHandler(this.DocumentSaveButtons_Resize);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.plButtons.ResumeLayout(false);
            this.plSaveLocal.ResumeLayout(false);
            this.plSaveLocal.PerformLayout();
            this.gpAfterSave.ResumeLayout(false);
            this.gpAfterSave.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel plButtons;
		public System.Windows.Forms.Button btnCancel;
		public System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Panel plSaveLocal;
		private System.Windows.Forms.CheckBox cbSaveLoc;
		private System.Windows.Forms.TextBox txtBrowse;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.CheckBox chkPDF;
		private System.Windows.Forms.GroupBox gpAfterSave;
		private System.Windows.Forms.RadioButton rdDeleteFile;
		private System.Windows.Forms.RadioButton rdMaintain;
		private System.Windows.Forms.RadioButton rdChangeName;
        private SpiderCustomComponent.CheckComboBox cboNotificationGroup;
        private System.Windows.Forms.Label lblNotificationGroup;
        private System.Windows.Forms.CheckBox cbMerge;
    }
}
