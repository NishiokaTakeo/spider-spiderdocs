namespace SpiderDocsForms
{
    partial class AttributeSearch
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlAttributes = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlAttributes
            // 
            this.pnlAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAttributes.AutoScroll = true;
            this.pnlAttributes.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlAttributes.Location = new System.Drawing.Point(3, 3);
            this.pnlAttributes.Name = "pnlAttributes";
            this.pnlAttributes.Size = new System.Drawing.Size(250, 21);
            this.pnlAttributes.TabIndex = 124;
            // 
            // AttributeSearch
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnlAttributes);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AttributeSearch";
            this.Size = new System.Drawing.Size(251, 45);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Panel pnlAttributes;
    }
}
