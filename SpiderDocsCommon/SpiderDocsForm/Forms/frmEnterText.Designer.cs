namespace SpiderDocs.Forms.WorkSpace
{
    partial class frmEnterText
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
            this.txtText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblExtra = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(101, 9);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(167, 20);
            this.txtText.TabIndex = 0;
            this.txtText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtText_KeyPress);
            this.txtText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtText_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter Text";
            // 
            // lblExtra
            // 
            this.lblExtra.AutoSize = true;
            this.lblExtra.Location = new System.Drawing.Point(273, 13);
            this.lblExtra.Name = "lblExtra";
            this.lblExtra.Size = new System.Drawing.Size(0, 13);
            this.lblExtra.TabIndex = 2;
            // 
            // frmEnterText
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(338, 39);
            this.Controls.Add(this.lblExtra);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmEnterText";
            this.Text = "Please Enter Text";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEnterText_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblExtra;
	}
}