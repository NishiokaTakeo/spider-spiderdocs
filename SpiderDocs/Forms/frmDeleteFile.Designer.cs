namespace SpiderDocs
{
    partial class frmDeleteFile
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeleteFile));
			this.txtReason = new System.Windows.Forms.TextBox();
			this.lblId_Title = new System.Windows.Forms.Label();
			this.lblName_Title = new System.Windows.Forms.Label();
			this.lblMsg = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lblId = new System.Windows.Forms.Label();
			this.lblName = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtReason
			// 
			this.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtReason.Location = new System.Drawing.Point(12, 98);
			this.txtReason.MaxLength = 1800;
			this.txtReason.Multiline = true;
			this.txtReason.Name = "txtReason";
			this.txtReason.Size = new System.Drawing.Size(340, 83);
			this.txtReason.TabIndex = 0;
			// 
			// lblId_Title
			// 
			this.lblId_Title.AutoSize = true;
			this.lblId_Title.Location = new System.Drawing.Point(12, 37);
			this.lblId_Title.Name = "lblId_Title";
			this.lblId_Title.Size = new System.Drawing.Size(19, 13);
			this.lblId_Title.TabIndex = 1;
			this.lblId_Title.Text = "Id:";
			// 
			// lblName_Title
			// 
			this.lblName_Title.AutoSize = true;
			this.lblName_Title.Location = new System.Drawing.Point(12, 57);
			this.lblName_Title.Name = "lblName_Title";
			this.lblName_Title.Size = new System.Drawing.Size(38, 13);
			this.lblName_Title.TabIndex = 2;
			this.lblName_Title.Text = "Name:";
			// 
			// lblMsg
			// 
			this.lblMsg.AutoSize = true;
			this.lblMsg.Location = new System.Drawing.Point(11, 10);
			this.lblMsg.Name = "lblMsg";
			this.lblMsg.Size = new System.Drawing.Size(197, 13);
			this.lblMsg.TabIndex = 3;
			this.lblMsg.Text = "Are you sure you want to delete this file?";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 82);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Write the reason:";
			// 
			// lblId
			// 
			this.lblId.AutoSize = true;
			this.lblId.Location = new System.Drawing.Point(56, 37);
			this.lblId.Name = "lblId";
			this.lblId.Size = new System.Drawing.Size(26, 13);
			this.lblId.TabIndex = 5;
			this.lblId.Text = "lblId";
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(56, 57);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(45, 13);
			this.lblName.TabIndex = 6;
			this.lblName.Text = "lblName";
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.Color.Gainsboro;
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.btnCancel);
			this.panel3.Controls.Add(this.btnDelete);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(0, 196);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(374, 32);
			this.panel3.TabIndex = 127;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCancel.BackColor = System.Drawing.Color.Transparent;
			this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnCancel.Location = new System.Drawing.Point(281, 3);
			this.btnCancel.MaximumSize = new System.Drawing.Size(97, 23);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(70, 23);
			this.btnCancel.TabIndex = 91;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.BackColor = System.Drawing.Color.Transparent;
			this.btnDelete.Image = global::SpiderDocs.Properties.Resources.delete;
			this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnDelete.Location = new System.Drawing.Point(206, 3);
			this.btnDelete.MaximumSize = new System.Drawing.Size(97, 23);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(73, 23);
			this.btnDelete.TabIndex = 4;
			this.btnDelete.Text = "Delete";
			this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnDelete.UseVisualStyleBackColor = false;
			this.btnDelete.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// frmDeleteFile
			// 
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(374, 228);
			this.Controls.Add(this.txtReason);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.lblId);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.lblMsg);
			this.Controls.Add(this.lblName_Title);
			this.Controls.Add(this.lblId_Title);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(380, 253);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(380, 253);
			this.Name = "frmDeleteFile";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Delete File";
			this.Load += new System.EventHandler(this.frmDeleteFile_Load);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label lblId_Title;
        private System.Windows.Forms.Label lblName_Title;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel panel3;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnDelete;
    }
}