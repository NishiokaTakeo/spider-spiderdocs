namespace UpdateWaitDialog {
	partial class frmMain {
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.pboxLoadingSearch = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pboxLoadingSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // pboxLoadingSearch
            // 
            this.pboxLoadingSearch.Image = ((System.Drawing.Image)(resources.GetObject("pboxLoadingSearch.Image")));
            this.pboxLoadingSearch.Location = new System.Drawing.Point(114, 61);
            this.pboxLoadingSearch.Name = "pboxLoadingSearch";
            this.pboxLoadingSearch.Size = new System.Drawing.Size(48, 48);
            this.pboxLoadingSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxLoadingSearch.TabIndex = 1012;
            this.pboxLoadingSearch.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(70, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 24);
            this.label2.TabIndex = 1014;
            this.label2.Text = "Please wait...";
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(271, 121);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pboxLoadingSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spider Docs is Updating";
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pboxLoadingSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pboxLoadingSearch;
		private System.Windows.Forms.Label label2;
	}
}

