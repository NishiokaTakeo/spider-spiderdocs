using SpiderDocsForms;

namespace SpiderDocs
{
    partial class frmFileProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFileProperties));
            this.cboFolder = new SpiderDocsForms.FolderComboBox(this.components);
            this.cboDocType = new System.Windows.Forms.ComboBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFolder = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblExt = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMessageTitle = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblExtLinkTitle = new System.Windows.Forms.Label();
            this.lbExtLink = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.panel = new SpiderDocsForms.AttributeSearch();
            this.panel = new SpiderDocsForms.AttributeSearch();
            this.lblNotificationGroup = new System.Windows.Forms.Label();
            this.cboNotificationGroup = new SpiderCustomComponent.CheckComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboFolder
            // 
            this.cboFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboFolder.DisplayMember = "document_folder";
            this.cboFolder.DropDownHeight = 1;
            this.cboFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFolder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboFolder.FormattingEnabled = true;
            this.cboFolder.IntegralHeight = false;
            this.cboFolder.Location = new System.Drawing.Point(107, 71);
            this.cboFolder.Name = "cboFolder";
            this.cboFolder.Size = new System.Drawing.Size(180, 21);
            this.cboFolder.TabIndex = 2;
            this.cboFolder.ValueMember = "id";
            // 
            // cboDocType
            // 
            this.cboDocType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDocType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboDocType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDocType.DisplayMember = "id";
            this.cboDocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDocType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboDocType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDocType.FormattingEnabled = true;
            this.cboDocType.Location = new System.Drawing.Point(107, 98);
            this.cboDocType.Name = "cboDocType";
            this.cboDocType.Size = new System.Drawing.Size(181, 21);
            this.cboDocType.TabIndex = 3;
            this.cboDocType.ValueMember = "id";
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox.Location = new System.Drawing.Point(301, 2);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(32, 32);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 103;
            this.pictureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(9, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 114;
            this.label1.Text = "Folder:";
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFolder.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFolder.Location = new System.Drawing.Point(9, 101);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(86, 13);
            this.lblFolder.TabIndex = 113;
            this.lblFolder.Text = "Document Type:";
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.Location = new System.Drawing.Point(107, 45);
            this.txtTitle.MaxLength = 250;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(181, 20);
            this.txtTitle.TabIndex = 1;
            this.txtTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTitle_KeyPress);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblName.Location = new System.Drawing.Point(9, 47);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 116;
            this.lblName.Text = "Name:";
            // 
            // lblExt
            // 
            this.lblExt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExt.AutoSize = true;
            this.lblExt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblExt.Location = new System.Drawing.Point(294, 47);
            this.lblExt.Name = "lblExt";
            this.lblExt.Size = new System.Drawing.Size(32, 13);
            this.lblExt.TabIndex = 117;
            this.lblExt.Text = "lblExt";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblMessageTitle);
            this.panel1.Location = new System.Drawing.Point(-6, -6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 31);
            this.panel1.TabIndex = 118;
            // 
            // lblMessageTitle
            // 
            this.lblMessageTitle.AutoSize = true;
            this.lblMessageTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessageTitle.Location = new System.Drawing.Point(12, 10);
            this.lblMessageTitle.Name = "lblMessageTitle";
            this.lblMessageTitle.Size = new System.Drawing.Size(130, 15);
            this.lblMessageTitle.TabIndex = 4;
            this.lblMessageTitle.Text = "Change file properties.";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(133, 419);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 23);
            this.btnSave.TabIndex = 1001;
            this.btnSave.Text = "    Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(233, 419);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(97, 23);
            this.btnClose.TabIndex = 1002;
            this.btnClose.Text = "Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(9, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 1003;
            this.label3.Text = "Attributes:";
            // 
            // lblExtLinkTitle
            // 
            this.lblExtLinkTitle.AutoSize = true;
            this.lblExtLinkTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExtLinkTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblExtLinkTitle.Location = new System.Drawing.Point(9, 366);
            this.lblExtLinkTitle.Name = "lblExtLinkTitle";
            this.lblExtLinkTitle.Size = new System.Drawing.Size(61, 15);
            this.lblExtLinkTitle.TabIndex = 1004;
            this.lblExtLinkTitle.Text = "Web Link:";
            // 
            // lbExtLink
            // 
            this.lbExtLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbExtLink.AutoSize = true;
            this.lbExtLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExtLink.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbExtLink.Location = new System.Drawing.Point(14, 389);
            this.lbExtLink.Name = "lbExtLink";
            this.lbExtLink.Size = new System.Drawing.Size(216, 13);
            this.lbExtLink.TabIndex = 1005;
            this.lbExtLink.Text = "WWWWWWWWWWWWWWWWWWW";
            // 
            // btnCopy
            // 
            this.btnCopy.AutoSize = true;
            this.btnCopy.BackColor = System.Drawing.Color.Transparent;
            this.btnCopy.Enabled = false;
            this.btnCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCopy.Location = new System.Drawing.Point(76, 363);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(41, 23);
            this.btnCopy.TabIndex = 1006;
            this.btnCopy.Text = "Copy";
            this.btnCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCopy.UseVisualStyleBackColor = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // panel
            // 
            this.panel.AutoScroll = true;
            this.panel.BackColor = System.Drawing.Color.Transparent;
            this.panel.CheckBoxThreeState = false;
            this.panel.FolderId = 0;
            this.panel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel.IncludeComboChildren = true;
            this.panel.Location = new System.Drawing.Point(12, 146);
            this.panel.Name = "panel";
            this.panel.Search = false;
            this.panel.Size = new System.Drawing.Size(321, 181);
            this.panel.TabIndex = 1007;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            // 
            // lblNotificationGroup
            // 
            this.lblNotificationGroup.AutoSize = true;
            this.lblNotificationGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotificationGroup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNotificationGroup.Location = new System.Drawing.Point(9, 341);
            this.lblNotificationGroup.Name = "lblNotificationGroup";
            this.lblNotificationGroup.Size = new System.Drawing.Size(95, 13);
            this.lblNotificationGroup.TabIndex = 1009;
            this.lblNotificationGroup.Text = "Notification Group:";
            // 
            // cboNotificationGroup
            // 
            this.cboNotificationGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboNotificationGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboNotificationGroup.CheckOnClick = true;
            this.cboNotificationGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboNotificationGroup.DropDownHeight = 1;
            this.cboNotificationGroup.FormattingEnabled = true;
            this.cboNotificationGroup.IntegralHeight = false;
            this.cboNotificationGroup.Location = new System.Drawing.Point(110, 336);
            this.cboNotificationGroup.MultiSelectable = true;
            this.cboNotificationGroup.Name = "cboNotificationGroup";
            this.cboNotificationGroup.Size = new System.Drawing.Size(200, 21);
            this.cboNotificationGroup.TabIndex = 1010;
            this.cboNotificationGroup.ValueSeparator = ", ";
            // 
            // frmFileProperties
            // 
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(335, 446);
            this.Controls.Add(this.cboNotificationGroup);
            this.Controls.Add(this.lblNotificationGroup);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.lbExtLink);
            this.Controls.Add(this.lblExtLinkTitle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblExt);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFolder);
            this.Controls.Add(this.cboDocType);
            this.Controls.Add(this.cboFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFileProperties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "File Properties";
            this.Load += new System.EventHandler(this.frmFileProperties_Load);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		private SpiderDocsForms.FolderComboBox cboFolder;
        private System.Windows.Forms.ComboBox cboDocType;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblExt;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.Label lblMessageTitle;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblExtLinkTitle;
		private System.Windows.Forms.Label lbExtLink;
		internal System.Windows.Forms.Button btnCopy;
		private AttributeSearch panel;
        private System.Windows.Forms.Label lblNotificationGroup;
        private SpiderCustomComponent.CheckComboBox cboNotificationGroup;
    }
}