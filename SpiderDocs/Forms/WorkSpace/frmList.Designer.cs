namespace SpiderDocs.Forms.WorkSpace
{
    partial class frmSimpleList
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
            this.Items = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // Suggested
            // 
            this.Items.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Items.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Items.FormattingEnabled = true;
            this.Items.ItemHeight = 18;
            this.Items.Location = new System.Drawing.Point(0, 0);
            this.Items.Name = "Suggested";
            this.Items.Size = new System.Drawing.Size(300, 150);
            this.Items.TabIndex = 0;
            this.Items.DoubleClick += new System.EventHandler(this.Suggested_DoubleClick);
            this.Items.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Suggested_KeyPress);
            this.Items.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Suggested_KeyUp);
            // 
            // frmList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 150);
            this.Controls.Add(this.Items);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmList";
            this.ShowInTaskbar = false;
            this.Text = "frmList";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox Items;
    }
}