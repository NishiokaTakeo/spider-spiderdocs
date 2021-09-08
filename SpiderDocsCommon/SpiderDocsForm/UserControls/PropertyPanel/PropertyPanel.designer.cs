namespace SpiderDocsForms {
	partial class PropertyPanel {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertyPanel));
            this.tmp_uscAttribute = new SpiderDocsForms.AttributeSearch();
            this.plAttributes = new System.Windows.Forms.Panel();
            this.uscAttribute = new SpiderDocsForms.AttributeSearch();
            this.uscAttribute = new SpiderDocsForms.AttributeSearch();
            this.cboDocType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cboFolder = new SpiderDocsForms.FolderComboBox(this.components);
            this.lblFolder = new System.Windows.Forms.Label();
            this.plSameAtb = new System.Windows.Forms.Panel();
            this.ckSameAtb = new System.Windows.Forms.CheckBox();
            this.plTitle = new System.Windows.Forms.Panel();
            this.labelName = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.plAttributes.SuspendLayout();
            this.plSameAtb.SuspendLayout();
            this.plTitle.SuspendLayout();
            this.SuspendLayout();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            // 
            // plAttributes
            // 
            this.plAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plAttributes.Controls.Add(this.uscAttribute);
            this.plAttributes.Controls.Add(this.cboDocType);
            this.plAttributes.Controls.Add(this.lblType);
            this.plAttributes.Controls.Add(this.cboFolder);
            this.plAttributes.Controls.Add(this.lblFolder);
            this.plAttributes.Location = new System.Drawing.Point(0, 43);
            this.plAttributes.Name = "plAttributes";
            this.plAttributes.Size = new System.Drawing.Size(339, 265);
            this.plAttributes.TabIndex = 528;
            // 
            // uscAttribute
            // 
            this.uscAttribute.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uscAttribute.AutoScroll = true;
            this.uscAttribute.BackColor = System.Drawing.Color.Transparent;
            this.uscAttribute.CheckBoxThreeState = false;
            //this.uscAttribute.ControlCaches = ((System.Collections.Generic.Dictionary<int, System.Collections.IEnumerable>)(resources.GetObject("uscAttribute.ControlCaches")));
            this.uscAttribute.FolderId = 0;
            this.uscAttribute.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uscAttribute.IncludeComboChildren = true;
            this.uscAttribute.Location = new System.Drawing.Point(2, 56);
            this.uscAttribute.Name = "uscAttribute";
            this.uscAttribute.Search = false;
            this.uscAttribute.Size = new System.Drawing.Size(334, 206);
            this.uscAttribute.TabIndex = 531;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            // 
            // cboDocType
            // 
            this.cboDocType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDocType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboDocType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDocType.DisplayMember = "client_name";
            this.cboDocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDocType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboDocType.FormattingEnabled = true;
            this.cboDocType.Location = new System.Drawing.Point(47, 29);
            this.cboDocType.Name = "cboDocType";
            this.cboDocType.Size = new System.Drawing.Size(284, 21);
            this.cboDocType.TabIndex = 528;
            this.cboDocType.ValueMember = "id";
            this.cboDocType.SelectedIndexChanged += new System.EventHandler(this.cboDocType_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.ForeColor = System.Drawing.Color.Black;
            this.lblType.Location = new System.Drawing.Point(3, 32);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(32, 13);
            this.lblType.TabIndex = 529;
            this.lblType.Text = "Type:";
            // 
            // cboFolder
            // 
            this.cboFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboFolder.DisplayMember = "id";
            this.cboFolder.DropDownHeight = 1;
            this.cboFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFolder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboFolder.FormattingEnabled = true;
            this.cboFolder.IntegralHeight = false;
            this.cboFolder.Location = new System.Drawing.Point(47, 2);
            this.cboFolder.Name = "cboFolder";
            this.cboFolder.Size = new System.Drawing.Size(284, 21);
            this.cboFolder.TabIndex = 527;
            this.cboFolder.ValueMember = "id";
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFolder.ForeColor = System.Drawing.Color.Black;
            this.lblFolder.Location = new System.Drawing.Point(3, 5);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(43, 13);
            this.lblFolder.TabIndex = 530;
            this.lblFolder.Text = "Folder:";
            // 
            // plSameAtb
            // 
            this.plSameAtb.AutoSize = true;
            this.plSameAtb.Controls.Add(this.ckSameAtb);
            this.plSameAtb.Location = new System.Drawing.Point(0, 13);
            this.plSameAtb.Name = "plSameAtb";
            this.plSameAtb.Size = new System.Drawing.Size(177, 27);
            this.plSameAtb.TabIndex = 529;
            this.plSameAtb.Visible = false;
            // 
            // ckSameAtb
            // 
            this.ckSameAtb.AutoSize = true;
            this.ckSameAtb.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckSameAtb.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ckSameAtb.Location = new System.Drawing.Point(3, 5);
            this.ckSameAtb.Name = "ckSameAtb";
            this.ckSameAtb.Size = new System.Drawing.Size(171, 17);
            this.ckSameAtb.TabIndex = 530;
            this.ckSameAtb.Text = "Use these values for all files.";
            this.ckSameAtb.UseVisualStyleBackColor = true;
            this.ckSameAtb.CheckedChanged += new System.EventHandler(this.ckSameAtb_CheckedChanged);
            // 
            // plTitle
            // 
            this.plTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plTitle.Controls.Add(this.labelName);
            this.plTitle.Controls.Add(this.txtTitle);
            this.plTitle.Location = new System.Drawing.Point(171, 13);
            this.plTitle.Name = "plTitle";
            this.plTitle.Size = new System.Drawing.Size(335, 27);
            this.plTitle.TabIndex = 532;
            this.plTitle.Visible = false;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.ForeColor = System.Drawing.Color.Black;
            this.labelName.Location = new System.Drawing.Point(3, 7);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(39, 13);
            this.labelName.TabIndex = 533;
            this.labelName.Text = "Name:";
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle.Location = new System.Drawing.Point(47, 4);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(284, 20);
            this.txtTitle.TabIndex = 532;
            // 
            // PropertyPanel
            // 
            this.Controls.Add(this.plTitle);
            this.Controls.Add(this.plSameAtb);
            this.Controls.Add(this.plAttributes);
            this.Name = "PropertyPanel";
            this.Size = new System.Drawing.Size(339, 308);
            this.Load += new System.EventHandler(this.PropertyPanel_Load);
            this.Disposed += PropertyPanel_Disposed;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.plAttributes.ResumeLayout(false);
            this.plAttributes.PerformLayout();
            this.plSameAtb.ResumeLayout(false);
            this.plSameAtb.PerformLayout();
            this.plTitle.ResumeLayout(false);
            this.plTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}



        #endregion

        private System.Windows.Forms.Panel plAttributes;
		private AttributeSearch uscAttribute;
		private System.Windows.Forms.ComboBox cboDocType;
		public System.Windows.Forms.Label lblType;
		private FolderComboBox cboFolder;
		public System.Windows.Forms.Label lblFolder;
		private System.Windows.Forms.Panel plSameAtb;
		private System.Windows.Forms.CheckBox ckSameAtb;
		private System.Windows.Forms.Panel plTitle;
		public System.Windows.Forms.Label labelName;
		private System.Windows.Forms.TextBox txtTitle;
	}
}
