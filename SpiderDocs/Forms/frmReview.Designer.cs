namespace SpiderDocs
{
    partial class frmReview
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
			//PresentationControls.CheckBoxProperties checkBoxProperties1 = new PresentationControls.CheckBoxProperties();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReview));
			this.txtComment = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblStatus = new System.Windows.Forms.Label();
			this.lblFileVer = new System.Windows.Forms.Label();
			this.lblFolder = new System.Windows.Forms.Label();
			this.lblFileName = new System.Windows.Forms.Label();
			this.lblDocId = new System.Windows.Forms.Label();
			this.tabReview = new System.Windows.Forms.TabControl();
			this.pgReview = new System.Windows.Forms.TabPage();
			this.btnCheckout = new System.Windows.Forms.Button();
			this.gbAction = new System.Windows.Forms.GroupBox();
			this.rbStartReview = new System.Windows.Forms.RadioButton();
			this.rbFinish = new System.Windows.Forms.RadioButton();
			this.plStartReview = new System.Windows.Forms.Panel();
			this.ckAllowCheckOut = new System.Windows.Forms.CheckBox();
            this.cboUser = new SpiderCustomComponent.CheckComboBox();
			this.dtDeadlineTime = new System.Windows.Forms.DateTimePicker();
			this.lblDeadlineSet = new System.Windows.Forms.Label();
			this.lblUser = new System.Windows.Forms.Label();
			this.dtDeadline = new System.Windows.Forms.DateTimePicker();
			this.lblEnterComment = new System.Windows.Forms.Label();
			this.txtOwnerComment = new System.Windows.Forms.TextBox();
			this.lblComment = new System.Windows.Forms.Label();
			this.btnOpen = new System.Windows.Forms.Button();
			this.pgLog = new System.Windows.Forms.TabPage();
			this.lblUsersComment = new System.Windows.Forms.Label();
			this.dtgReview = new System.Windows.Forms.DataGridView();
			this.dtgReviewNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dtgReviewName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dtgReviewAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dtgReviewComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.txtReviewComment = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panel1.SuspendLayout();
			this.tabReview.SuspendLayout();
			this.pgReview.SuspendLayout();
			this.gbAction.SuspendLayout();
			this.plStartReview.SuspendLayout();
			this.pgLog.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtgReview)).BeginInit();
			this.SuspendLayout();
			// 
			// txtComment
			// 
			this.txtComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtComment.Enabled = false;
			this.txtComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtComment.Location = new System.Drawing.Point(268, 159);
			this.txtComment.MaxLength = 250;
			this.txtComment.Multiline = true;
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = new System.Drawing.Size(290, 98);
			this.txtComment.TabIndex = 1;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.BackColor = System.Drawing.Color.Transparent;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnCancel.Location = new System.Drawing.Point(474, 317);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(87, 26);
			this.btnCancel.TabIndex = 1002;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = false;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.lblStatus);
			this.panel1.Controls.Add(this.lblFileVer);
			this.panel1.Controls.Add(this.lblFolder);
			this.panel1.Controls.Add(this.lblFileName);
			this.panel1.Controls.Add(this.lblDocId);
			this.panel1.Location = new System.Drawing.Point(-6, -6);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(583, 31);
			this.panel1.TabIndex = 118;
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatus.ForeColor = System.Drawing.Color.Red;
			this.lblStatus.Location = new System.Drawing.Point(500, 10);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(77, 15);
			this.lblStatus.TabIndex = 9;
			this.lblStatus.Text = "OVER DUE";
			this.lblStatus.Visible = false;
			// 
			// lblFileVer
			// 
			this.lblFileVer.AutoSize = true;
			this.lblFileVer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFileVer.Location = new System.Drawing.Point(423, 10);
			this.lblFileVer.Name = "lblFileVer";
			this.lblFileVer.Size = new System.Drawing.Size(68, 15);
			this.lblFileVer.TabIndex = 7;
			this.lblFileVer.Text = "Version: 12";
			// 
			// lblFolder
			// 
			this.lblFolder.AutoSize = true;
			this.lblFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFolder.Location = new System.Drawing.Point(292, 10);
			this.lblFolder.Name = "lblFolder";
			this.lblFolder.Size = new System.Drawing.Size(125, 15);
			this.lblFolder.TabIndex = 6;
			this.lblFolder.Text = "Folder: WWWWWWW";
			// 
			// lblFileName
			// 
			this.lblFileName.AutoSize = true;
			this.lblFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFileName.Location = new System.Drawing.Point(74, 10);
			this.lblFileName.Name = "lblFileName";
			this.lblFileName.Size = new System.Drawing.Size(212, 15);
			this.lblFileName.TabIndex = 5;
			this.lblFileName.Text = "Name: WWWWWWWWWWWWWWW";
			// 
			// lblDocId
			// 
			this.lblDocId.AutoSize = true;
			this.lblDocId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDocId.Location = new System.Drawing.Point(8, 10);
			this.lblDocId.Name = "lblDocId";
			this.lblDocId.Size = new System.Drawing.Size(60, 15);
			this.lblDocId.TabIndex = 4;
			this.lblDocId.Text = "ID: 12345";
			// 
			// tabReview
			// 
			this.tabReview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.tabReview.Controls.Add(this.pgReview);
			this.tabReview.Controls.Add(this.pgLog);
			this.tabReview.Location = new System.Drawing.Point(2, 27);
			this.tabReview.Name = "tabReview";
			this.tabReview.SelectedIndex = 0;
			this.tabReview.Size = new System.Drawing.Size(570, 286);
			this.tabReview.TabIndex = 1004;
			// 
			// pgReview
			// 
			this.pgReview.BackColor = System.Drawing.Color.WhiteSmoke;
			this.pgReview.Controls.Add(this.gbAction);
			this.pgReview.Controls.Add(this.btnCheckout);
			this.pgReview.Controls.Add(this.lblEnterComment);
			this.pgReview.Controls.Add(this.txtOwnerComment);
			this.pgReview.Controls.Add(this.txtComment);
			this.pgReview.Controls.Add(this.lblComment);
			this.pgReview.Controls.Add(this.btnOpen);
			this.pgReview.Location = new System.Drawing.Point(4, 22);
			this.pgReview.Name = "pgReview";
			this.pgReview.Padding = new System.Windows.Forms.Padding(3);
			this.pgReview.Size = new System.Drawing.Size(562, 260);
			this.pgReview.TabIndex = 0;
			this.pgReview.Text = "Review";
			// 
			// btnCheckout
			// 
			this.btnCheckout.BackColor = System.Drawing.Color.Transparent;
			this.btnCheckout.Enabled = false;
			this.btnCheckout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnCheckout.Location = new System.Drawing.Point(3, 72);
			this.btnCheckout.Name = "btnCheckout";
			this.btnCheckout.Size = new System.Drawing.Size(259, 60);
			this.btnCheckout.TabIndex = 1009;
			this.btnCheckout.Text = "Check out";
			this.btnCheckout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCheckout.UseVisualStyleBackColor = false;
			this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
			// 
			// gbAction
			// 
			this.gbAction.Controls.Add(this.rbStartReview);
			this.gbAction.Controls.Add(this.rbFinish);
			this.gbAction.Controls.Add(this.plStartReview);
			this.gbAction.Enabled = false;
			this.gbAction.Location = new System.Drawing.Point(268, 6);
			this.gbAction.Name = "gbAction";
			this.gbAction.Size = new System.Drawing.Size(290, 134);
			this.gbAction.TabIndex = 1004;
			this.gbAction.TabStop = false;
			this.gbAction.Text = "Action";
			// 
			// rbStartReview
			// 
			this.rbStartReview.AutoSize = true;
			this.rbStartReview.Location = new System.Drawing.Point(12, 38);
			this.rbStartReview.Name = "rbStartReview";
			this.rbStartReview.Size = new System.Drawing.Size(81, 17);
			this.rbStartReview.TabIndex = 1;
			this.rbStartReview.Text = "Start review";
			this.rbStartReview.UseVisualStyleBackColor = true;
			this.rbStartReview.CheckedChanged += new System.EventHandler(this.rbPassOn_CheckedChanged);
			// 
			// rbFinish
			// 
			this.rbFinish.AutoSize = true;
			this.rbFinish.Checked = true;
			this.rbFinish.Location = new System.Drawing.Point(12, 19);
			this.rbFinish.Name = "rbFinish";
			this.rbFinish.Size = new System.Drawing.Size(105, 17);
			this.rbFinish.TabIndex = 0;
			this.rbFinish.TabStop = true;
			this.rbFinish.Text = "Finish this review";
			this.rbFinish.UseVisualStyleBackColor = true;
			// 
			// plStartReview
			// 
			this.plStartReview.Controls.Add(this.ckAllowCheckOut);
			this.plStartReview.Controls.Add(this.cboUser);
			this.plStartReview.Controls.Add(this.dtDeadlineTime);
			this.plStartReview.Controls.Add(this.lblDeadlineSet);
			this.plStartReview.Controls.Add(this.lblUser);
			this.plStartReview.Controls.Add(this.dtDeadline);
			this.plStartReview.Location = new System.Drawing.Point(30, 53);
			this.plStartReview.Name = "plStartReview";
			this.plStartReview.Size = new System.Drawing.Size(258, 81);
			this.plStartReview.TabIndex = 1014;
			// 
			// ckAllowCheckOut
			// 
			this.ckAllowCheckOut.AutoSize = true;
			this.ckAllowCheckOut.Location = new System.Drawing.Point(6, 53);
			this.ckAllowCheckOut.Name = "ckAllowCheckOut";
			this.ckAllowCheckOut.Size = new System.Drawing.Size(188, 17);
			this.ckAllowCheckOut.TabIndex = 1019;
			this.ckAllowCheckOut.Text = "Allow selected users to check out.";
			this.ckAllowCheckOut.UseVisualStyleBackColor = true;
			// 
			// cboUser
			// 
			//checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
			//this.cboUser.CheckBoxProperties = checkBoxProperties1;
			//this.cboUser.DisplayMemberSingleItem = "";
			this.cboUser.Location = new System.Drawing.Point(55, 4);
			this.cboUser.Name = "cboUser";
			this.cboUser.Size = new System.Drawing.Size(195, 21);
			this.cboUser.TabIndex = 1018;
			// 
			// dtDeadlineTime
			// 
			this.dtDeadlineTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dtDeadlineTime.Location = new System.Drawing.Point(155, 27);
			this.dtDeadlineTime.Name = "dtDeadlineTime";
			this.dtDeadlineTime.ShowUpDown = true;
			this.dtDeadlineTime.Size = new System.Drawing.Size(95, 20);
			this.dtDeadlineTime.TabIndex = 1017;
			// 
			// lblDeadlineSet
			// 
			this.lblDeadlineSet.AutoSize = true;
			this.lblDeadlineSet.Location = new System.Drawing.Point(3, 30);
			this.lblDeadlineSet.Name = "lblDeadlineSet";
			this.lblDeadlineSet.Size = new System.Drawing.Size(52, 13);
			this.lblDeadlineSet.TabIndex = 1016;
			this.lblDeadlineSet.Text = "Deadline:";
			// 
			// lblUser
			// 
			this.lblUser.AutoSize = true;
			this.lblUser.Location = new System.Drawing.Point(3, 7);
			this.lblUser.Name = "lblUser";
			this.lblUser.Size = new System.Drawing.Size(32, 13);
			this.lblUser.TabIndex = 1015;
			this.lblUser.Text = "User:";
			// 
			// dtDeadline
			// 
			this.dtDeadline.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtDeadline.Location = new System.Drawing.Point(55, 27);
			this.dtDeadline.Name = "dtDeadline";
			this.dtDeadline.Size = new System.Drawing.Size(95, 20);
			this.dtDeadline.TabIndex = 1014;
			// 
			// lblEnterComment
			// 
			this.lblEnterComment.AutoSize = true;
			this.lblEnterComment.Location = new System.Drawing.Point(265, 143);
			this.lblEnterComment.Name = "lblEnterComment";
			this.lblEnterComment.Size = new System.Drawing.Size(104, 13);
			this.lblEnterComment.TabIndex = 1008;
			this.lblEnterComment.Text = "Enter your comment:";
			// 
			// txtOwnerComment
			// 
			this.txtOwnerComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtOwnerComment.Enabled = false;
			this.txtOwnerComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtOwnerComment.Location = new System.Drawing.Point(3, 159);
			this.txtOwnerComment.MaxLength = 250;
			this.txtOwnerComment.Multiline = true;
			this.txtOwnerComment.Name = "txtOwnerComment";
			this.txtOwnerComment.Size = new System.Drawing.Size(259, 98);
			this.txtOwnerComment.TabIndex = 1006;
			// 
			// lblComment
			// 
			this.lblComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lblComment.AutoSize = true;
			this.lblComment.Location = new System.Drawing.Point(3, 143);
			this.lblComment.Name = "lblComment";
			this.lblComment.Size = new System.Drawing.Size(193, 13);
			this.lblComment.TabIndex = 1007;
			this.lblComment.Text = "Comment from WWWWWWWWWW: ";
			// 
			// btnOpen
			// 
			this.btnOpen.BackColor = System.Drawing.Color.Transparent;
			this.btnOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnOpen.Location = new System.Drawing.Point(3, 6);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(259, 60);
			this.btnOpen.TabIndex = 1005;
			this.btnOpen.Text = "Open to read";
			this.btnOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOpen.UseVisualStyleBackColor = false;
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// pgLog
			// 
			this.pgLog.BackColor = System.Drawing.Color.WhiteSmoke;
			this.pgLog.Controls.Add(this.lblUsersComment);
			this.pgLog.Controls.Add(this.dtgReview);
			this.pgLog.Controls.Add(this.txtReviewComment);
			this.pgLog.Location = new System.Drawing.Point(4, 22);
			this.pgLog.Name = "pgLog";
			this.pgLog.Padding = new System.Windows.Forms.Padding(3);
			this.pgLog.Size = new System.Drawing.Size(562, 260);
			this.pgLog.TabIndex = 1;
			this.pgLog.Text = "Log";
			// 
			// lblUsersComment
			// 
			this.lblUsersComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblUsersComment.AutoSize = true;
			this.lblUsersComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblUsersComment.ForeColor = System.Drawing.Color.Black;
			this.lblUsersComment.Location = new System.Drawing.Point(295, 3);
			this.lblUsersComment.Name = "lblUsersComment";
			this.lblUsersComment.Size = new System.Drawing.Size(54, 13);
			this.lblUsersComment.TabIndex = 1008;
			this.lblUsersComment.Text = "Comment:";
			// 
			// dtgReview
			// 
			this.dtgReview.AllowDrop = true;
			this.dtgReview.AllowUserToAddRows = false;
			this.dtgReview.AllowUserToDeleteRows = false;
			this.dtgReview.AllowUserToResizeRows = false;
			this.dtgReview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dtgReview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dtgReview.BackgroundColor = System.Drawing.Color.White;
			this.dtgReview.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dtgReview.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dtgReview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dtgReview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dtgReview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dtgReviewNo,
            this.dtgReviewName,
            this.dtgReviewAction,
            this.dtgReviewComment});
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dtgReview.DefaultCellStyle = dataGridViewCellStyle3;
			this.dtgReview.GridColor = System.Drawing.Color.Gainsboro;
			this.dtgReview.Location = new System.Drawing.Point(3, 3);
			this.dtgReview.MultiSelect = false;
			this.dtgReview.Name = "dtgReview";
			this.dtgReview.ReadOnly = true;
			this.dtgReview.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Beige;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dtgReview.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dtgReview.RowHeadersVisible = false;
			this.dtgReview.RowHeadersWidth = 20;
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
			this.dtgReview.RowsDefaultCellStyle = dataGridViewCellStyle5;
			this.dtgReview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dtgReview.Size = new System.Drawing.Size(286, 254);
			this.dtgReview.TabIndex = 537;
			this.dtgReview.SelectionChanged += new System.EventHandler(this.dtgReview_SelectionChanged);
			// 
			// dtgReviewNo
			// 
			this.dtgReviewNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.dtgReviewNo.DefaultCellStyle = dataGridViewCellStyle2;
			this.dtgReviewNo.HeaderText = "No.";
			this.dtgReviewNo.MinimumWidth = 30;
			this.dtgReviewNo.Name = "dtgReviewNo";
			this.dtgReviewNo.ReadOnly = true;
			this.dtgReviewNo.Width = 30;
			// 
			// dtgReviewName
			// 
			this.dtgReviewName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dtgReviewName.FillWeight = 80F;
			this.dtgReviewName.HeaderText = "Name";
			this.dtgReviewName.Name = "dtgReviewName";
			this.dtgReviewName.ReadOnly = true;
			// 
			// dtgReviewAction
			// 
			this.dtgReviewAction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dtgReviewAction.FillWeight = 60F;
			this.dtgReviewAction.HeaderText = "Action";
			this.dtgReviewAction.Name = "dtgReviewAction";
			this.dtgReviewAction.ReadOnly = true;
			// 
			// dtgReviewComment
			// 
			this.dtgReviewComment.HeaderText = "dtgReviewComment";
			this.dtgReviewComment.Name = "dtgReviewComment";
			this.dtgReviewComment.ReadOnly = true;
			this.dtgReviewComment.Visible = false;
			// 
			// txtReviewComment
			// 
			this.txtReviewComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtReviewComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtReviewComment.Enabled = false;
			this.txtReviewComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtReviewComment.Location = new System.Drawing.Point(295, 19);
			this.txtReviewComment.MaxLength = 250;
			this.txtReviewComment.Multiline = true;
			this.txtReviewComment.Name = "txtReviewComment";
			this.txtReviewComment.Size = new System.Drawing.Size(264, 238);
			this.txtReviewComment.TabIndex = 1007;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.BackColor = System.Drawing.Color.Transparent;
			this.btnOK.Enabled = false;
			this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnOK.Location = new System.Drawing.Point(378, 317);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(87, 26);
			this.btnOK.TabIndex = 1003;
			this.btnOK.Text = "OK";
			this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
			this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle6;
			this.dataGridViewTextBoxColumn1.HeaderText = "Id";
			this.dataGridViewTextBoxColumn1.MinimumWidth = 42;
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn1.Width = 42;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn2.DataPropertyName = "name";
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
			this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle7;
			this.dataGridViewTextBoxColumn2.FillWeight = 70F;
			this.dataGridViewTextBoxColumn2.HeaderText = "Name";
			this.dataGridViewTextBoxColumn2.MinimumWidth = 80;
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn3.DataPropertyName = "result";
			this.dataGridViewTextBoxColumn3.FillWeight = 46.06452F;
			this.dataGridViewTextBoxColumn3.HeaderText = "Result";
			this.dataGridViewTextBoxColumn3.MinimumWidth = 80;
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.Visible = false;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn4.DataPropertyName = "comment";
			this.dataGridViewTextBoxColumn4.FillWeight = 70F;
			this.dataGridViewTextBoxColumn4.HeaderText = "Comment";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.Visible = false;
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dataGridViewTextBoxColumn5.DataPropertyName = "id";
			this.dataGridViewTextBoxColumn5.HeaderText = "Id";
			this.dataGridViewTextBoxColumn5.MinimumWidth = 42;
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn5.Visible = false;
			this.dataGridViewTextBoxColumn5.Width = 42;
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn6.DataPropertyName = "name";
			this.dataGridViewTextBoxColumn6.FillWeight = 70F;
			this.dataGridViewTextBoxColumn6.HeaderText = "Name";
			this.dataGridViewTextBoxColumn6.MinimumWidth = 80;
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn6.Visible = false;
			// 
			// dataGridViewTextBoxColumn7
			// 
			this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn7.DataPropertyName = "login";
			this.dataGridViewTextBoxColumn7.FillWeight = 70F;
			this.dataGridViewTextBoxColumn7.HeaderText = "dtgUsers_login";
			this.dataGridViewTextBoxColumn7.MinimumWidth = 80;
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			this.dataGridViewTextBoxColumn7.Visible = false;
			// 
			// dataGridViewTextBoxColumn8
			// 
			this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dataGridViewTextBoxColumn8.DataPropertyName = "password";
			this.dataGridViewTextBoxColumn8.HeaderText = "dtgUsers_password";
			this.dataGridViewTextBoxColumn8.MinimumWidth = 42;
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			this.dataGridViewTextBoxColumn8.ReadOnly = true;
			this.dataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn8.Visible = false;
			this.dataGridViewTextBoxColumn8.Width = 42;
			// 
			// dataGridViewTextBoxColumn9
			// 
			this.dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn9.DataPropertyName = "active";
			this.dataGridViewTextBoxColumn9.FillWeight = 70F;
			this.dataGridViewTextBoxColumn9.HeaderText = "dtgUsers_active";
			this.dataGridViewTextBoxColumn9.MinimumWidth = 80;
			this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			this.dataGridViewTextBoxColumn9.ReadOnly = true;
			this.dataGridViewTextBoxColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn9.Visible = false;
			// 
			// dataGridViewTextBoxColumn10
			// 
			this.dataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn10.DataPropertyName = "id_permission";
			this.dataGridViewTextBoxColumn10.FillWeight = 70F;
			this.dataGridViewTextBoxColumn10.HeaderText = "dtgUsers_id_permission";
			this.dataGridViewTextBoxColumn10.MinimumWidth = 80;
			this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
			this.dataGridViewTextBoxColumn10.ReadOnly = true;
			this.dataGridViewTextBoxColumn10.Visible = false;
			// 
			// dataGridViewTextBoxColumn11
			// 
			this.dataGridViewTextBoxColumn11.DataPropertyName = "id_email";
			this.dataGridViewTextBoxColumn11.FillWeight = 46.06452F;
			this.dataGridViewTextBoxColumn11.HeaderText = "dtgUsers_id_email";
			this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
			this.dataGridViewTextBoxColumn11.ReadOnly = true;
			this.dataGridViewTextBoxColumn11.Visible = false;
			this.dataGridViewTextBoxColumn11.Width = 61;
			// 
			// dataGridViewTextBoxColumn12
			// 
			this.dataGridViewTextBoxColumn12.DataPropertyName = "comment";
			this.dataGridViewTextBoxColumn12.HeaderText = "Comment";
			this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
			this.dataGridViewTextBoxColumn12.ReadOnly = true;
			this.dataGridViewTextBoxColumn12.Visible = false;
			// 
			// dataGridViewTextBoxColumn13
			// 
			this.dataGridViewTextBoxColumn13.DataPropertyName = "comment";
			this.dataGridViewTextBoxColumn13.HeaderText = "dtgReviewers_Comment";
			this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
			this.dataGridViewTextBoxColumn13.ReadOnly = true;
			this.dataGridViewTextBoxColumn13.Visible = false;
			// 
			// frmReview
			// 
			this.AcceptButton = this.btnOK;
			this.AutoSize = true;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(573, 345);
			this.Controls.Add(this.tabReview);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmReview";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Review";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReview_FormClosing);
			this.Load += new System.EventHandler(this.frmReview_Load);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tabReview.ResumeLayout(false);
			this.pgReview.ResumeLayout(false);
			this.pgReview.PerformLayout();
			this.gbAction.ResumeLayout(false);
			this.gbAction.PerformLayout();
			this.plStartReview.ResumeLayout(false);
			this.plStartReview.PerformLayout();
			this.pgLog.ResumeLayout(false);
			this.pgLog.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtgReview)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.TextBox txtComment;
		internal System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Panel panel1;
		public System.Windows.Forms.Label lblFileVer;
		public System.Windows.Forms.Label lblFolder;
		public System.Windows.Forms.Label lblFileName;
		public System.Windows.Forms.Label lblDocId;
		private System.Windows.Forms.TabControl tabReview;
		private System.Windows.Forms.TabPage pgReview;
		internal System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.GroupBox gbAction;
		private System.Windows.Forms.RadioButton rbStartReview;
		private System.Windows.Forms.RadioButton rbFinish;
		private System.Windows.Forms.TabPage pgLog;
		private System.Windows.Forms.TextBox txtOwnerComment;
		private System.Windows.Forms.Label lblComment;
		private System.Windows.Forms.Label lblEnterComment;
		public System.Windows.Forms.Label lblUsersComment;
		private System.Windows.Forms.TextBox txtReviewComment;
		public System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.DataGridView dtgReview;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
		internal System.Windows.Forms.Button btnOK;
		internal System.Windows.Forms.Button btnCheckout;
		private System.Windows.Forms.Panel plStartReview;
		private System.Windows.Forms.CheckBox ckAllowCheckOut;
        private SpiderCustomComponent.CheckComboBox cboUser;
		private System.Windows.Forms.DateTimePicker dtDeadlineTime;
		private System.Windows.Forms.Label lblDeadlineSet;
		private System.Windows.Forms.Label lblUser;
		private System.Windows.Forms.DateTimePicker dtDeadline;
		private System.Windows.Forms.DataGridViewTextBoxColumn dtgReviewNo;
		private System.Windows.Forms.DataGridViewTextBoxColumn dtgReviewName;
		private System.Windows.Forms.DataGridViewTextBoxColumn dtgReviewAction;
		private System.Windows.Forms.DataGridViewTextBoxColumn dtgReviewComment;
    }
}