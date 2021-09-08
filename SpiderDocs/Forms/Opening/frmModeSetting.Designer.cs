namespace SpiderDocs {
	partial class frmModeSetting {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModeSetting));
            this.btnOpen = new System.Windows.Forms.Button();
            this.gpModes = new System.Windows.Forms.GroupBox();
            this.plServer2 = new System.Windows.Forms.Panel();
            this.txtPort2 = new System.Windows.Forms.TextBox();
            this.txtServer2 = new System.Windows.Forms.TextBox();
            this.ckAutoConnection2 = new System.Windows.Forms.CheckBox();
            this.ckWorkOffline2 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rbServer2 = new System.Windows.Forms.RadioButton();
            this.plServer = new System.Windows.Forms.Panel();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.ckAutoConnection = new System.Windows.Forms.CheckBox();
            this.ckWorkOffline = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rbStandalone = new System.Windows.Forms.RadioButton();
            this.rbServer = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gpModes.SuspendLayout();
            this.plServer2.SuspendLayout();
            this.plServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.AutoSize = true;
            this.btnOpen.Location = new System.Drawing.Point(241, 255);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(65, 23);
            this.btnOpen.TabIndex = 30;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // gpModes
            // 
            this.gpModes.Controls.Add(this.plServer2);
            this.gpModes.Controls.Add(this.rbServer2);
            this.gpModes.Controls.Add(this.plServer);
            this.gpModes.Controls.Add(this.rbStandalone);
            this.gpModes.Controls.Add(this.rbServer);
            this.gpModes.Location = new System.Drawing.Point(8, 6);
            this.gpModes.Name = "gpModes";
            this.gpModes.Size = new System.Drawing.Size(369, 227);
            this.gpModes.TabIndex = 31;
            this.gpModes.TabStop = false;
            this.gpModes.Text = "Please select database mode";
            // 
            // plServer2
            // 
            this.plServer2.Controls.Add(this.txtPort2);
            this.plServer2.Controls.Add(this.txtServer2);
            this.plServer2.Controls.Add(this.ckAutoConnection2);
            this.plServer2.Controls.Add(this.ckWorkOffline2);
            this.plServer2.Controls.Add(this.label3);
            this.plServer2.Controls.Add(this.label4);
            this.plServer2.Location = new System.Drawing.Point(29, 164);
            this.plServer2.Name = "plServer2";
            this.plServer2.Size = new System.Drawing.Size(319, 50);
            this.plServer2.TabIndex = 41;
            // 
            // txtPort2
            // 
            this.txtPort2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtPort2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPort2.Location = new System.Drawing.Point(153, 21);
            this.txtPort2.MaxLength = 4;
            this.txtPort2.Name = "txtPort2";
            this.txtPort2.Size = new System.Drawing.Size(58, 20);
            this.txtPort2.TabIndex = 40;
            // 
            // txtServer2
            // 
            this.txtServer2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtServer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServer2.Location = new System.Drawing.Point(8, 21);
            this.txtServer2.MaxLength = 30;
            this.txtServer2.Name = "txtServer2";
            this.txtServer2.Size = new System.Drawing.Size(139, 20);
            this.txtServer2.TabIndex = 39;
            // 
            // ckAutoConnection2
            // 
            this.ckAutoConnection2.AutoSize = true;
            this.ckAutoConnection2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckAutoConnection2.Location = new System.Drawing.Point(214, 5);
            this.ckAutoConnection2.Name = "ckAutoConnection2";
            this.ckAutoConnection2.Size = new System.Drawing.Size(102, 17);
            this.ckAutoConnection2.TabIndex = 47;
            this.ckAutoConnection2.Text = "Auto Connection";
            this.ckAutoConnection2.UseVisualStyleBackColor = true;
            // 
            // ckWorkOffline2
            // 
            this.ckWorkOffline2.AutoSize = true;
            this.ckWorkOffline2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckWorkOffline2.Location = new System.Drawing.Point(214, 26);
            this.ckWorkOffline2.Name = "ckWorkOffline2";
            this.ckWorkOffline2.Size = new System.Drawing.Size(82, 17);
            this.ckWorkOffline2.TabIndex = 46;
            this.ckWorkOffline2.Text = "Work Offline";
            this.ckWorkOffline2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(150, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "Address";
            // 
            // rbServer2
            // 
            this.rbServer2.AutoSize = true;
            this.rbServer2.Location = new System.Drawing.Point(19, 141);
            this.rbServer2.Name = "rbServer2";
            this.rbServer2.Size = new System.Drawing.Size(163, 17);
            this.rbServer2.TabIndex = 40;
            this.rbServer2.Text = "Remote Server Access mode";
            this.rbServer2.UseVisualStyleBackColor = true;
            this.rbServer2.CheckedChanged += new System.EventHandler(this.rbServer2_CheckedChanged);
            // 
            // plServer
            // 
            this.plServer.Controls.Add(this.txtPort);
            this.plServer.Controls.Add(this.txtServer);
            this.plServer.Controls.Add(this.ckAutoConnection);
            this.plServer.Controls.Add(this.ckWorkOffline);
            this.plServer.Controls.Add(this.label2);
            this.plServer.Controls.Add(this.label1);
            this.plServer.Location = new System.Drawing.Point(29, 78);
            this.plServer.Name = "plServer";
            this.plServer.Size = new System.Drawing.Size(319, 50);
            this.plServer.TabIndex = 39;
            // 
            // txtPort
            // 
            this.txtPort.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPort.Location = new System.Drawing.Point(153, 21);
            this.txtPort.MaxLength = 4;
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(58, 20);
            this.txtPort.TabIndex = 40;
            this.txtPort.TextChanged += new System.EventHandler(this.rbStandalone_CheckedChanged);
            this.txtPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPort_KeyPress_1);
            // 
            // txtServer
            // 
            this.txtServer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServer.Location = new System.Drawing.Point(8, 21);
            this.txtServer.MaxLength = 30;
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(139, 20);
            this.txtServer.TabIndex = 39;
            // 
            // ckAutoConnection
            // 
            this.ckAutoConnection.AutoSize = true;
            this.ckAutoConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckAutoConnection.Location = new System.Drawing.Point(214, 5);
            this.ckAutoConnection.Name = "ckAutoConnection";
            this.ckAutoConnection.Size = new System.Drawing.Size(102, 17);
            this.ckAutoConnection.TabIndex = 47;
            this.ckAutoConnection.Text = "Auto Connection";
            this.ckAutoConnection.UseVisualStyleBackColor = true;
            // 
            // ckWorkOffline
            // 
            this.ckWorkOffline.AutoSize = true;
            this.ckWorkOffline.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckWorkOffline.Location = new System.Drawing.Point(214, 26);
            this.ckWorkOffline.Name = "ckWorkOffline";
            this.ckWorkOffline.Size = new System.Drawing.Size(82, 17);
            this.ckWorkOffline.TabIndex = 46;
            this.ckWorkOffline.Text = "Work Offline";
            this.ckWorkOffline.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(150, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Address";
            // 
            // rbStandalone
            // 
            this.rbStandalone.AutoSize = true;
            this.rbStandalone.Enabled = false;
            this.rbStandalone.Location = new System.Drawing.Point(19, 29);
            this.rbStandalone.Name = "rbStandalone";
            this.rbStandalone.Size = new System.Drawing.Size(108, 17);
            this.rbStandalone.TabIndex = 30;
            this.rbStandalone.Text = "Standalone mode";
            this.rbStandalone.UseVisualStyleBackColor = true;
            this.rbStandalone.CheckedChanged += new System.EventHandler(this.rbStandalone_CheckedChanged);
            // 
            // rbServer
            // 
            this.rbServer.AutoSize = true;
            this.rbServer.Checked = true;
            this.rbServer.Location = new System.Drawing.Point(19, 55);
            this.rbServer.Name = "rbServer";
            this.rbServer.Size = new System.Drawing.Size(123, 17);
            this.rbServer.TabIndex = 31;
            this.rbServer.TabStop = true;
            this.rbServer.Text = "Server Access mode";
            this.rbServer.UseVisualStyleBackColor = true;
            this.rbServer.CheckedChanged += new System.EventHandler(this.rbServer_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(312, 255);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 23);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmModeSetting
            // 
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(385, 287);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gpModes);
            this.Controls.Add(this.btnOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModeSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mode Setting";
            this.Load += new System.EventHandler(this.frmModeSetting_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmModeSetting_KeyDown);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.gpModes.ResumeLayout(false);
            this.gpModes.PerformLayout();
            this.plServer2.ResumeLayout(false);
            this.plServer2.PerformLayout();
            this.plServer.ResumeLayout(false);
            this.plServer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.GroupBox gpModes;
		private System.Windows.Forms.RadioButton rbStandalone;
		private System.Windows.Forms.RadioButton rbServer;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Panel plServer;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtServer;
		private System.Windows.Forms.TextBox txtPort;
		private System.Windows.Forms.CheckBox ckWorkOffline;
		private System.Windows.Forms.CheckBox ckAutoConnection;
        private System.Windows.Forms.Panel plServer2;
        private System.Windows.Forms.TextBox txtPort2;
        private System.Windows.Forms.TextBox txtServer2;
        private System.Windows.Forms.CheckBox ckAutoConnection2;
        private System.Windows.Forms.CheckBox ckWorkOffline2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbServer2;
    }
}

