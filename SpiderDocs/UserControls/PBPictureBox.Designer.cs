namespace SpiderDocs
{
    partial class PBPictureBox
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
            this.imageBox = new System.Windows.Forms.PictureBox();
            this.pictureStrip1 = new SpiderDocs.PictureStrip();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox
            // 
            this.imageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageBox.BackColor = System.Drawing.Color.Gainsboro;
            this.imageBox.ErrorImage = null;
            this.imageBox.InitialImage = null;
            this.imageBox.Location = new System.Drawing.Point(311, 0);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(289, 351);
            this.imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBox.TabIndex = 4;
            this.imageBox.TabStop = false;
            // 
            // pictureStrip1
            // 
            this.pictureStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureStrip1.AutoScroll = true;
            this.pictureStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureStrip1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureStrip1.Location = new System.Drawing.Point(0, 352);
            this.pictureStrip1.Name = "pictureStrip1";
            this.pictureStrip1.Size = new System.Drawing.Size(601, 125);
            this.pictureStrip1.TabIndex = 3;
            this.pictureStrip1.SubmitClicked += new SpiderDocs.PictureStrip.SubmitClickedHandler(this.pictureStrip1_Clicked);
            // 
            // PBPictureBox
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.pictureStrip1);
            this.Name = "PBPictureBox";
            this.Size = new System.Drawing.Size(600, 477);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public PictureStrip pictureStrip1;
        public System.Windows.Forms.PictureBox imageBox;
    }
}
