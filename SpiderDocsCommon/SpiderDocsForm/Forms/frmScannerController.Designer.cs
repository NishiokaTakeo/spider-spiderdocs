namespace SpiderDocsForms
{
    partial class frmScannerController
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblScanner = new System.Windows.Forms.Label();
            this.btnDefault = new System.Windows.Forms.Button();
            this.gbColourDepth = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.rbColourDepth3 = new System.Windows.Forms.RadioButton();
            this.rbColourDepth1 = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbResolution = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbScanType = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbColourDepth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 521F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(736, 673);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(218, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(515, 667);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblScanner);
            this.groupBox1.Controls.Add(this.btnDefault);
            this.groupBox1.Controls.Add(this.gbColourDepth);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnPreview);
            this.groupBox1.Controls.Add(this.btnScan);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbResolution);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbScanType);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 667);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scanner Setting";
            // 
            // lblScanner
            // 
            this.lblScanner.AutoSize = true;
            this.lblScanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScanner.Location = new System.Drawing.Point(9, 29);
            this.lblScanner.Name = "lblScanner";
            this.lblScanner.Size = new System.Drawing.Size(43, 16);
            this.lblScanner.TabIndex = 18;
            this.lblScanner.Text = "xxxxx";
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(6, 424);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(196, 32);
            this.btnDefault.TabIndex = 16;
            this.btnDefault.Text = "Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // gbColourDepth
            // 
            this.gbColourDepth.Controls.Add(this.pictureBox2);
            this.gbColourDepth.Controls.Add(this.rbColourDepth3);
            this.gbColourDepth.Controls.Add(this.rbColourDepth1);
            this.gbColourDepth.Location = new System.Drawing.Point(6, 63);
            this.gbColourDepth.Name = "gbColourDepth";
            this.gbColourDepth.Size = new System.Drawing.Size(197, 137);
            this.gbColourDepth.TabIndex = 15;
            this.gbColourDepth.TabStop = false;
            this.gbColourDepth.Text = "Colour Depth";
            this.gbColourDepth.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::SpiderDocsForms.Properties.Resources.scan_colour_depth;
            this.pictureBox2.Location = new System.Drawing.Point(3, 19);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(114, 96);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // rbColourDepth3
            // 
            this.rbColourDepth3.AutoSize = true;
            this.rbColourDepth3.Location = new System.Drawing.Point(122, 39);
            this.rbColourDepth3.Name = "rbColourDepth3";
            this.rbColourDepth3.Size = new System.Drawing.Size(53, 17);
            this.rbColourDepth3.TabIndex = 14;
            this.rbColourDepth3.TabStop = true;
            this.rbColourDepth3.Text = "Photo";
            this.rbColourDepth3.UseVisualStyleBackColor = true;
            // 
            // rbColourDepth1
            // 
            this.rbColourDepth1.AutoSize = true;
            this.rbColourDepth1.Location = new System.Drawing.Point(122, 77);
            this.rbColourDepth1.Name = "rbColourDepth1";
            this.rbColourDepth1.Size = new System.Drawing.Size(74, 17);
            this.rbColourDepth1.TabIndex = 12;
            this.rbColourDepth1.TabStop = true;
            this.rbColourDepth1.Text = "Document";
            this.rbColourDepth1.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(6, 462);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(196, 32);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(6, 348);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(196, 32);
            this.btnPreview.TabIndex = 8;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(6, 386);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(196, 32);
            this.btnScan.TabIndex = 7;
            this.btnScan.Text = "Start scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 267);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Resolution";
            // 
            // cmbResolution
            // 
            this.cmbResolution.FormattingEnabled = true;
            this.cmbResolution.Location = new System.Drawing.Point(6, 286);
            this.cmbResolution.Name = "cmbResolution";
            this.cmbResolution.Size = new System.Drawing.Size(196, 21);
            this.cmbResolution.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 211);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Scan Type";
            // 
            // cmbScanType
            // 
            this.cmbScanType.FormattingEnabled = true;
            this.cmbScanType.Location = new System.Drawing.Point(6, 230);
            this.cmbScanType.Name = "cmbScanType";
            this.cmbScanType.Size = new System.Drawing.Size(196, 21);
            this.cmbScanType.TabIndex = 3;
            // 
            // frmScannerController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 673);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmScannerController";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Scanner";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmScannerController_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.frmScannerController_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbColourDepth.ResumeLayout(false);
            this.gbColourDepth.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblScanner;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.GroupBox gbColourDepth;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.RadioButton rbColourDepth3;
        private System.Windows.Forms.RadioButton rbColourDepth1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbResolution;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbScanType;
    }
}

