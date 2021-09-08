namespace SpiderDocsForms
{
    partial class frmMessageBox
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
			this.btnCancel = new System.Windows.Forms.Button();
			this.ckNotify = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblUser = new System.Windows.Forms.Label();
			this.lblDate = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			//
			// btnCancel
			//
			this.btnCancel.AutoSize = true;
			this.btnCancel.Location = new System.Drawing.Point(184, 121);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "No";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			//
			// ckNotify
			//
			this.ckNotify.AutoSize = true;
			this.ckNotify.Location = new System.Drawing.Point(87, 73);
			this.ckNotify.Name = "ckNotify";
			this.ckNotify.Size = new System.Drawing.Size(191, 17);
			this.ckNotify.TabIndex = 1;
			this.ckNotify.Text = "Notify me when the file is available.";
			this.ckNotify.UseVisualStyleBackColor = true;
			//
			// label1
			//
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(85, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(194, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "This file is already open in edit mode by:";
			//
			// btnOk
			//
			this.btnOk.AutoSize = true;
			this.btnOk.Location = new System.Drawing.Point(265, 121);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 3;
			this.btnOk.Text = "Yes";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			//
			// pictureBox1
			//
			this.pictureBox1.Image = global::SpiderDocsForms.Properties.Resources.file_lock;
			this.pictureBox1.Location = new System.Drawing.Point(14, 5);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(50, 48);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			//
			// label2
			//
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(84, 28);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "User:";
			//
			// label3
			//
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(84, 46);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(37, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Since:";
			//
			// lblUser
			//
			this.lblUser.AutoSize = true;
			this.lblUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblUser.Location = new System.Drawing.Point(120, 29);
			this.lblUser.Name = "lblUser";
			this.lblUser.Size = new System.Drawing.Size(39, 13);
			this.lblUser.TabIndex = 7;
			this.lblUser.Text = "lblUser";
			//
			// lblDate
			//
			this.lblDate.AutoSize = true;
			this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDate.Location = new System.Drawing.Point(120, 46);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(40, 13);
			this.lblDate.TabIndex = 8;
			this.lblDate.Text = "lblDate";
			//
			// label4
			//
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(84, 96);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(216, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "Would you like to open it in read only mode?";
			//
			// panel1
			//
			this.panel1.AutoSize = true;
			this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(-6, -7);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(360, 30);
			this.panel1.TabIndex = 136;
			//
			// frmMessageBox
			//
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(357, 148);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.lblDate);
			this.Controls.Add(this.lblUser);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.ckNotify);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmMessageBox";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "File in use";
			this.Load += new System.EventHandler(this.frmMessageBox_Load);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox ckNotify;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label lblUser;
        public System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
    }
}