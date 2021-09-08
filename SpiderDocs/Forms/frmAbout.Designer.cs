namespace SpiderDocs
{
    partial class frmAbout
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblComputerName = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::SpiderDocs.Properties.Resources.logospider_about;
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(357, 120);
			this.pictureBox1.TabIndex = 52;
			this.pictureBox1.TabStop = false;
			// 
			// lblComputerName
			// 
			this.lblComputerName.AutoSize = true;
			this.lblComputerName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblComputerName.Location = new System.Drawing.Point(102, 145);
			this.lblComputerName.Name = "lblComputerName";
			this.lblComputerName.Size = new System.Drawing.Size(199, 13);
			this.lblComputerName.TabIndex = 59;
			this.lblComputerName.Text = "Computer Name: WWWWWWWWWW";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.linkLabel1);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(27, 171);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(284, 71);
			this.groupBox1.TabIndex = 60;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Support";
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point(60, 43);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(202, 13);
			this.linkLabel1.TabIndex = 59;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "http://www.spiderdevelopments.com.au/";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label4.Location = new System.Drawing.Point(15, 43);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(49, 13);
			this.label4.TabIndex = 58;
			this.label4.Text = "Website:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label3.Location = new System.Drawing.Point(15, 21);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(219, 13);
			this.label3.TabIndex = 56;
			this.label3.Text = "E-mail: support@spiderdevelopments.com.au";
			// 
			// lblVersion
			// 
			this.lblVersion.AutoSize = true;
			this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblVersion.Location = new System.Drawing.Point(24, 145);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(72, 13);
			this.lblVersion.TabIndex = 61;
			this.lblVersion.Text = "Version: 1.3.6";
			// 
			// frmAbout
			// 
			this.AutoScroll = true;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(383, 251);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.lblComputerName);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmAbout";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.Load += new System.EventHandler(this.About_Load);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblComputerName;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblVersion;
    }
}