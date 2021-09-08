namespace SpiderDocsForms
{
	partial class ScanDialog {
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
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.lblProgress = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			// ProgressBar
			//
			this.ProgressBar.Location = new System.Drawing.Point(15, 37);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(271, 23);
			this.ProgressBar.TabIndex = 0;
			//
			// lblProgress
			//
			this.lblProgress.AutoSize = true;
			this.lblProgress.Location = new System.Drawing.Point(12, 9);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(274, 13);
			this.lblProgress.TabIndex = 3;
			this.lblProgress.Text = "Progress: WWWWWWWWWWWWWWWWWWWW";
			//
			// btnCancel
			//
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(211, 66);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			//
			// ScanDialog
			//
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(302, 93);
			this.ControlBox = false;
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.lblProgress);
			this.Controls.Add(this.ProgressBar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ScanDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Now Progressing...";
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private System.Windows.Forms.ProgressBar ProgressBar;
		private System.Windows.Forms.Label lblProgress;
		private System.Windows.Forms.Button btnCancel;
	}
}