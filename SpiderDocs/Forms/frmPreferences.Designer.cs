namespace SpiderDocs
{
    partial class frmPreferences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPreferences));
            this.ckStardLogOn = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBoxDocDetails = new System.Windows.Forms.PictureBox();
            this.cbEnableFolderCreationByUser = new System.Windows.Forms.CheckBox();
            this.chkDefaultMerge = new System.Windows.Forms.CheckBox();
            this.chkPDFMerge = new System.Windows.Forms.CheckBox();
            this.cbOCRDefault = new System.Windows.Forms.CheckBox();
            this.ckExcludeArchive = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ckShowImportDialogNewMail = new System.Windows.Forms.CheckBox();
            this.ckOCR = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rb_checkoutfooter = new System.Windows.Forms.RadioButton();
            this.rb_checkout = new System.Windows.Forms.RadioButton();
            this.rb_readonly = new System.Windows.Forms.RadioButton();
            this.ckSaveCredentials = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDocDetails)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // ckStardLogOn
            // 
            this.ckStardLogOn.AutoSize = true;
            this.ckStardLogOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckStardLogOn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ckStardLogOn.Location = new System.Drawing.Point(11, 57);
            this.ckStardLogOn.Name = "ckStardLogOn";
            this.ckStardLogOn.Size = new System.Drawing.Size(251, 17);
            this.ckStardLogOn.TabIndex = 10;
            this.ckStardLogOn.Text = "Start SpiderDocs when I log on to my computer.";
            this.ckStardLogOn.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.pictureBoxDocDetails);
            this.groupBox1.Controls.Add(this.cbEnableFolderCreationByUser);
            this.groupBox1.Controls.Add(this.chkDefaultMerge);
            this.groupBox1.Controls.Add(this.chkPDFMerge);
            this.groupBox1.Controls.Add(this.cbOCRDefault);
            this.groupBox1.Controls.Add(this.ckExcludeArchive);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.ckOCR);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.ckSaveCredentials);
            this.groupBox1.Controls.Add(this.ckStardLogOn);
            this.groupBox1.Location = new System.Drawing.Point(10, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(405, 434);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "             General Settings";
            // 
            // pictureBoxDocDetails
            // 
            this.pictureBoxDocDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxDocDetails.Image = global::SpiderDocs.Properties.Resources.preferences;
            this.pictureBoxDocDetails.Location = new System.Drawing.Point(2, -5);
            this.pictureBoxDocDetails.Name = "pictureBoxDocDetails";
            this.pictureBoxDocDetails.Size = new System.Drawing.Size(40, 40);
            this.pictureBoxDocDetails.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDocDetails.TabIndex = 100;
            this.pictureBoxDocDetails.TabStop = false;
            // 
            // cbEnableFolderCreationByUser
            // 
            this.cbEnableFolderCreationByUser.AutoSize = true;
            this.cbEnableFolderCreationByUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEnableFolderCreationByUser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbEnableFolderCreationByUser.Location = new System.Drawing.Point(10, 146);
            this.cbEnableFolderCreationByUser.Name = "cbEnableFolderCreationByUser";
            this.cbEnableFolderCreationByUser.Size = new System.Drawing.Size(175, 17);
            this.cbEnableFolderCreationByUser.TabIndex = 22;
            this.cbEnableFolderCreationByUser.Text = "Enable users can create folders";
            this.cbEnableFolderCreationByUser.UseVisualStyleBackColor = true;
            // 
            // chkDefaultMerge
            // 
            this.chkDefaultMerge.AutoSize = true;
            this.chkDefaultMerge.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDefaultMerge.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkDefaultMerge.Location = new System.Drawing.Point(11, 100);
            this.chkDefaultMerge.Name = "chkDefaultMerge";
            this.chkDefaultMerge.Size = new System.Drawing.Size(85, 17);
            this.chkDefaultMerge.TabIndex = 21;
            this.chkDefaultMerge.Text = "Merge PDFs";
            this.chkDefaultMerge.UseVisualStyleBackColor = true;
            // 
            // chkPDFMerge
            // 
            this.chkPDFMerge.AutoSize = true;
            this.chkPDFMerge.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPDFMerge.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkPDFMerge.Location = new System.Drawing.Point(102, 100);
            this.chkPDFMerge.Name = "chkPDFMerge";
            this.chkPDFMerge.Size = new System.Drawing.Size(139, 17);
            this.chkPDFMerge.TabIndex = 20;
            this.chkPDFMerge.Text = "PDF Merge functionality";
            this.chkPDFMerge.UseVisualStyleBackColor = true;
            this.chkPDFMerge.Visible = false;
            // 
            // cbOCRDefault
            // 
            this.cbOCRDefault.AutoSize = true;
            this.cbOCRDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOCRDefault.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbOCRDefault.Location = new System.Drawing.Point(158, 80);
            this.cbOCRDefault.Name = "cbOCRDefault";
            this.cbOCRDefault.Size = new System.Drawing.Size(93, 17);
            this.cbOCRDefault.TabIndex = 19;
            this.cbOCRDefault.Text = "Yes as default";
            this.cbOCRDefault.UseVisualStyleBackColor = true;
            // 
            // ckExcludeArchive
            // 
            this.ckExcludeArchive.AutoSize = true;
            this.ckExcludeArchive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckExcludeArchive.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ckExcludeArchive.Location = new System.Drawing.Point(11, 123);
            this.ckExcludeArchive.Name = "ckExcludeArchive";
            this.ckExcludeArchive.Size = new System.Drawing.Size(163, 17);
            this.ckExcludeArchive.TabIndex = 18;
            this.ckExcludeArchive.Text = "Exclude archived documents";
            this.ckExcludeArchive.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ckShowImportDialogNewMail);
            this.groupBox3.Location = new System.Drawing.Point(6, 279);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(387, 47);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Office Add-in";
            // 
            // ckShowImportDialogNewMail
            // 
            this.ckShowImportDialogNewMail.AutoSize = true;
            this.ckShowImportDialogNewMail.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ckShowImportDialogNewMail.Location = new System.Drawing.Point(5, 19);
            this.ckShowImportDialogNewMail.Name = "ckShowImportDialogNewMail";
            this.ckShowImportDialogNewMail.Size = new System.Drawing.Size(149, 17);
            this.ckShowImportDialogNewMail.TabIndex = 13;
            this.ckShowImportDialogNewMail.Text = "Ask to import a sent email.";
            this.ckShowImportDialogNewMail.UseVisualStyleBackColor = true;
            // 
            // ckOCR
            // 
            this.ckOCR.AutoSize = true;
            this.ckOCR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckOCR.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ckOCR.Location = new System.Drawing.Point(11, 80);
            this.ckOCR.Name = "ckOCR";
            this.ckOCR.Size = new System.Drawing.Size(144, 17);
            this.ckOCR.TabIndex = 16;
            this.ckOCR.Text = "Enable OCR functionality";
            this.ckOCR.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rb_checkoutfooter);
            this.groupBox2.Controls.Add(this.rb_checkout);
            this.groupBox2.Controls.Add(this.rb_readonly);
            this.groupBox2.Location = new System.Drawing.Point(6, 183);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(388, 92);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Double-Click Behavior";
            // 
            // rb_checkoutfooter
            // 
            this.rb_checkoutfooter.AutoSize = true;
            this.rb_checkoutfooter.Location = new System.Drawing.Point(6, 65);
            this.rb_checkoutfooter.Name = "rb_checkoutfooter";
            this.rb_checkoutfooter.Size = new System.Drawing.Size(161, 17);
            this.rb_checkoutfooter.TabIndex = 2;
            this.rb_checkoutfooter.TabStop = true;
            this.rb_checkoutfooter.Text = "Check Out With Footer (Edit)";
            this.rb_checkoutfooter.UseVisualStyleBackColor = true;
            // 
            // rb_checkout
            // 
            this.rb_checkout.AutoSize = true;
            this.rb_checkout.Location = new System.Drawing.Point(6, 42);
            this.rb_checkout.Name = "rb_checkout";
            this.rb_checkout.Size = new System.Drawing.Size(103, 17);
            this.rb_checkout.TabIndex = 1;
            this.rb_checkout.TabStop = true;
            this.rb_checkout.Text = "Check Out (Edit)";
            this.rb_checkout.UseVisualStyleBackColor = true;
            // 
            // rb_readonly
            // 
            this.rb_readonly.AutoSize = true;
            this.rb_readonly.Location = new System.Drawing.Point(6, 19);
            this.rb_readonly.Name = "rb_readonly";
            this.rb_readonly.Size = new System.Drawing.Size(149, 17);
            this.rb_readonly.TabIndex = 0;
            this.rb_readonly.TabStop = true;
            this.rb_readonly.Text = "Open to Read (Read only)";
            this.rb_readonly.UseVisualStyleBackColor = true;
            // 
            // ckSaveCredentials
            // 
            this.ckSaveCredentials.AutoSize = true;
            this.ckSaveCredentials.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ckSaveCredentials.Location = new System.Drawing.Point(11, 34);
            this.ckSaveCredentials.Name = "ckSaveCredentials";
            this.ckSaveCredentials.Size = new System.Drawing.Size(127, 17);
            this.ckSaveCredentials.TabIndex = 12;
            this.ckSaveCredentials.Text = "Save login password.";
            this.ckSaveCredentials.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(319, 510);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 23);
            this.btnCancel.TabIndex = 93;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(215, 510);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 23);
            this.btnSave.TabIndex = 92;
            this.btnSave.Text = "    Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtPort);
            this.groupBox4.Controls.Add(this.txtServer);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.btnOpen);
            this.groupBox4.Location = new System.Drawing.Point(6, 332);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(387, 76);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Remote server details";
            // 
            // txtPort
            // 
            this.txtPort.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPort.Location = new System.Drawing.Point(155, 36);
            this.txtPort.MaxLength = 4;
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(58, 20);
            this.txtPort.TabIndex = 49;
            // 
            // txtServer
            // 
            this.txtServer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServer.Location = new System.Drawing.Point(10, 36);
            this.txtServer.MaxLength = 30;
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(139, 20);
            this.txtServer.TabIndex = 48;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(152, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 51;
            this.label2.Text = "Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Address";
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(314, 165);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 23);
            this.button1.TabIndex = 47;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnOpen
            // 
            this.btnOpen.AutoSize = true;
            this.btnOpen.Location = new System.Drawing.Point(243, 165);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(65, 23);
            this.btnOpen.TabIndex = 46;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // frmPreferences
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(515, 537);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1000, 1000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(100, 100);
            this.Name = "frmPreferences";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.frmPreferences_Load);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDocDetails)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox ckStardLogOn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ckSaveCredentials;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.PictureBox pictureBoxDocDetails;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton rb_checkout;
		private System.Windows.Forms.RadioButton rb_readonly;
		private System.Windows.Forms.CheckBox ckOCR;
		private System.Windows.Forms.RadioButton rb_checkoutfooter;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox ckShowImportDialogNewMail;
        private System.Windows.Forms.CheckBox ckExcludeArchive;
		private System.Windows.Forms.CheckBox cbOCRDefault;
        private System.Windows.Forms.CheckBox chkDefaultMerge;
        private System.Windows.Forms.CheckBox chkPDFMerge;
        private System.Windows.Forms.CheckBox cbEnableFolderCreationByUser;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnOpen;
    }
}