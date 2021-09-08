namespace SpiderDocsForms
{
    partial class frmReasonNewVersion
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReasonNewVersion));
			this.txtReason = new System.Windows.Forms.TextBox();
			this.lblIdTitle = new System.Windows.Forms.Label();
			this.lblNameTitle = new System.Windows.Forms.Label();
			this.lblMessageTitle = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lblId = new System.Windows.Forms.Label();
			this.lblName = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblVersionTitle = new System.Windows.Forms.Label();
			this.btnDelete = new System.Windows.Forms.Button();
			this.pboxAppIcon = new System.Windows.Forms.PictureBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pboxAppIcon)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			//
			// txtReason
			//
			this.txtReason.Location = new System.Drawing.Point(12, 102);
			this.txtReason.MaxLength = 1800;
			this.txtReason.Multiline = true;
			this.txtReason.Name = "txtReason";
			this.txtReason.Size = new System.Drawing.Size(340, 86);
			this.txtReason.TabIndex = 0;
			//
			// lblIdTitle
			//
			this.lblIdTitle.AutoSize = true;
			this.lblIdTitle.Location = new System.Drawing.Point(15, 57);
			this.lblIdTitle.Name = "lblIdTitle";
			this.lblIdTitle.Size = new System.Drawing.Size(19, 13);
			this.lblIdTitle.TabIndex = 1;
			this.lblIdTitle.Text = "Id:";
			//
			// lblNameTitle
			//
			this.lblNameTitle.AutoSize = true;
			this.lblNameTitle.Location = new System.Drawing.Point(14, 38);
			this.lblNameTitle.Name = "lblNameTitle";
			this.lblNameTitle.Size = new System.Drawing.Size(38, 13);
			this.lblNameTitle.TabIndex = 2;
			this.lblNameTitle.Text = "Name:";
			//
			// lblMessageTitle
			//
			this.lblMessageTitle.AutoSize = true;
			this.lblMessageTitle.Location = new System.Drawing.Point(8, 9);
			this.lblMessageTitle.Name = "lblMessageTitle";
			this.lblMessageTitle.Size = new System.Drawing.Size(56, 13);
			this.lblMessageTitle.TabIndex = 3;
			this.lblMessageTitle.Text = "File details";
			//
			// label4
			//
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 86);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Write the reason:";
			//
			// lblId
			//
			this.lblId.AutoSize = true;
			this.lblId.Location = new System.Drawing.Point(58, 57);
			this.lblId.Name = "lblId";
			this.lblId.Size = new System.Drawing.Size(26, 13);
			this.lblId.TabIndex = 5;
			this.lblId.Text = "lblId";
			//
			// lblName
			//
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(58, 38);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(45, 13);
			this.lblName.TabIndex = 6;
			this.lblName.Text = "lblName";
			//
			// btnCancel
			//
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCancel.AutoSize = true;
			this.btnCancel.BackColor = System.Drawing.Color.Transparent;
			this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnCancel.Location = new System.Drawing.Point(255, 194);
			this.btnCancel.MaximumSize = new System.Drawing.Size(97, 23);
			this.btnCancel.MinimumSize = new System.Drawing.Size(97, 23);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(97, 23);
			this.btnCancel.TabIndex = 91;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			//
			// lblVersion
			//
			this.lblVersion.AutoSize = true;
			this.lblVersion.Location = new System.Drawing.Point(182, 57);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(52, 13);
			this.lblVersion.TabIndex = 129;
			this.lblVersion.Text = "lblVersion";
			//
			// lblVersionTitle
			//
			this.lblVersionTitle.AutoSize = true;
			this.lblVersionTitle.Location = new System.Drawing.Point(121, 57);
			this.lblVersionTitle.Name = "lblVersionTitle";
			this.lblVersionTitle.Size = new System.Drawing.Size(45, 13);
			this.lblVersionTitle.TabIndex = 128;
			this.lblVersionTitle.Text = "Version:";
			//
			// btnDelete
			//
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.AutoSize = true;
			this.btnDelete.BackColor = System.Drawing.Color.Transparent;
			this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
			this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnDelete.Location = new System.Drawing.Point(152, 194);
			this.btnDelete.MaximumSize = new System.Drawing.Size(97, 23);
			this.btnDelete.MinimumSize = new System.Drawing.Size(97, 23);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(97, 23);
			this.btnDelete.TabIndex = 4;
			this.btnDelete.Text = "    Save";
			this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnDelete.UseVisualStyleBackColor = false;
			this.btnDelete.Click += new System.EventHandler(this.btnSave_Click);
			//
			// pboxAppIcon
			//
			this.pboxAppIcon.Location = new System.Drawing.Point(320, 12);
			this.pboxAppIcon.Name = "pboxAppIcon";
			this.pboxAppIcon.Size = new System.Drawing.Size(32, 32);
			this.pboxAppIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pboxAppIcon.TabIndex = 511;
			this.pboxAppIcon.TabStop = false;
			//
			// panel1
			//
			this.panel1.AutoSize = true;
			this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.lblMessageTitle);
			this.panel1.Location = new System.Drawing.Point(-1, -2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(368, 29);
			this.panel1.TabIndex = 514;
			//
			// label5
			//
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.label5.Location = new System.Drawing.Point(12, 191);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(113, 13);
			this.label5.TabIndex = 515;
			this.label5.Text = "10 characters minimum";
			//
			// frmReasonNewVersion
			//
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(364, 224);
			this.Controls.Add(this.txtReason);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.pboxAppIcon);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.lblVersionTitle);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.lblId);
			this.Controls.Add(this.lblNameTitle);
			this.Controls.Add(this.lblIdTitle);
			this.Controls.Add(this.label4);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmReasonNewVersion";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Save New Version";
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			((System.ComponentModel.ISupportInitialize)(this.pboxAppIcon)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Button btnCancel;
		internal System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.PictureBox pboxAppIcon;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtReason;
		public System.Windows.Forms.Label lblMessageTitle;
		private System.Windows.Forms.Label lblId;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblIdTitle;
		private System.Windows.Forms.Label lblNameTitle;
		private System.Windows.Forms.Label lblVersionTitle;
    }
}