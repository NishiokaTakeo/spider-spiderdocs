namespace SpiderDocsForms
{
	public partial class DocumentSearch : NewListBase
	{
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

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbSaveNewVer = new System.Windows.Forms.Label();
            this.plList = new System.Windows.Forms.Panel();
            this.btnVersionSearch = new System.Windows.Forms.Button();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.dtgVersionFiles = new SpiderDocsForms.DocumentDataGridView();
            this.dtgVersionFiles_Icon = new System.Windows.Forms.DataGridViewImageColumn();
            this.c_id_doc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_id_version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_id_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_id_folder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_id_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_id_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_mail_in_out_prefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_mail_subject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_mail_from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_mail_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_mail_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_mail_is_composed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_folder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_docType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_author = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_extension = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_id_review = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_CheckOutUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_id_sp_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_created_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVersionId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtVersionName = new System.Windows.Forms.TextBox();
            this.cboVersionType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboVersionFolder = new FolderComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblReason = new System.Windows.Forms.Label();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
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
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgVersionFiles)).BeginInit();
            this.SuspendLayout();
            //
            // lbSaveNewVer
            //
            this.lbSaveNewVer.AutoSize = true;
            this.lbSaveNewVer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSaveNewVer.ForeColor = System.Drawing.Color.Black;
            this.lbSaveNewVer.Location = new System.Drawing.Point(0, 4);
            this.lbSaveNewVer.Name = "lbSaveNewVer";
            this.lbSaveNewVer.Size = new System.Drawing.Size(221, 13);
            this.lbSaveNewVer.TabIndex = 546;
            this.lbSaveNewVer.Text = "Select the file you are saving the new version";
            //
            // plList
            //
            this.plList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plList.Controls.Add(this.btnVersionSearch);
            this.plList.Controls.Add(this.txtReason);
            this.plList.Controls.Add(this.dtgVersionFiles);
            this.plList.Controls.Add(this.label4);
            this.plList.Controls.Add(this.txtVersionId);
            this.plList.Controls.Add(this.label1);
            this.plList.Controls.Add(this.txtVersionName);
            this.plList.Controls.Add(this.cboVersionType);
            this.plList.Controls.Add(this.label2);
            this.plList.Controls.Add(this.cboVersionFolder);
            this.plList.Controls.Add(this.label3);
            this.plList.Controls.Add(this.lblReason);
            this.plList.Location = new System.Drawing.Point(0, 20);
            this.plList.Name = "plList";
            this.plList.Size = new System.Drawing.Size(338, 362);
            this.plList.TabIndex = 551;
            //
            // btnVersionSearch
            //
            this.btnVersionSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVersionSearch.BackColor = System.Drawing.Color.White;
            this.btnVersionSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVersionSearch.Location = new System.Drawing.Point(269, 4);
            this.btnVersionSearch.Name = "btnVersionSearch";
            this.btnVersionSearch.Size = new System.Drawing.Size(65, 23);
            this.btnVersionSearch.TabIndex = 562;
            this.btnVersionSearch.Text = "Search";
            this.btnVersionSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVersionSearch.UseVisualStyleBackColor = false;
            this.btnVersionSearch.Click += new System.EventHandler(this.btnVersionSearch_Click);
            //
            // txtReason
            //
            this.txtReason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReason.Location = new System.Drawing.Point(3, 320);
            this.txtReason.MaxLength = 1800;
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(331, 39);
            this.txtReason.TabIndex = 560;
            //
            // dtgVersionFiles
            //
            this.dtgVersionFiles.AllowUserToAddRows = false;
            this.dtgVersionFiles.AllowUserToDeleteRows = false;
            this.dtgVersionFiles.AllowUserToResizeRows = false;
            this.dtgVersionFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgVersionFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgVersionFiles.BackgroundColor = System.Drawing.Color.White;
            this.dtgVersionFiles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dtgVersionFiles.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgVersionFiles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgVersionFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgVersionFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dtgVersionFiles_Icon,
            this.c_id_doc,
            this.c_id_version,
            this.c_id_user,
            this.c_id_folder,
            this.c_id_type,
            this.c_id_status,
            this.c_mail_in_out_prefix,
            this.c_title,
            this.c_mail_subject,
            this.c_mail_from,
            this.c_mail_to,
            this.c_mail_time,
            this.c_mail_is_composed,
            this.c_folder,
            this.c_docType,
            this.c_author,
            this.c_version,
            this.c_date,
            this.c_extension,
            this.c_id_review,
            this.c_CheckOutUser,
            this.c_id_sp_status,
            this.c_created_date});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgVersionFiles.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtgVersionFiles.GridColor = System.Drawing.Color.Gainsboro;
            this.dtgVersionFiles.Location = new System.Drawing.Point(3, 109);
            this.dtgVersionFiles.Mode = SpiderDocsForms.en_DocumentDataGridViewMode.DocumentSearchForExternalFiles;
            this.dtgVersionFiles.MultiSelect = false;
            this.dtgVersionFiles.Name = "dtgVersionFiles";
            this.dtgVersionFiles.ReadOnly = true;
            this.dtgVersionFiles.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Beige;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgVersionFiles.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dtgVersionFiles.RowHeadersVisible = false;
            this.dtgVersionFiles.RowHeadersWidth = 20;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            this.dtgVersionFiles.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dtgVersionFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgVersionFiles.Size = new System.Drawing.Size(331, 192);
            this.dtgVersionFiles.TabIndex = 559;
            this.dtgVersionFiles.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dtgVersionFiles_DataBindingComplete);
            this.dtgVersionFiles.SelectionChanged += new System.EventHandler(this.dtgVersionFiles_SelectionChanged);
            this.dtgVersionFiles.Click += new System.EventHandler(this.dtgVersionFiles_Click);
            this.dtgVersionFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtgVersionFiles_MouseDown);
            this.dtgVersionFiles.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dtgVersionFiles_MouseMove);
            //
            // dtgVersionFiles_Icon
            //
            this.dtgVersionFiles_Icon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dtgVersionFiles_Icon.HeaderText = "";
            this.dtgVersionFiles_Icon.MinimumWidth = 20;
            this.dtgVersionFiles_Icon.Name = "dtgVersionFiles_Icon";
            this.dtgVersionFiles_Icon.ReadOnly = true;
            this.dtgVersionFiles_Icon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgVersionFiles_Icon.Width = 20;
            //
            // c_id_doc
            //
            this.c_id_doc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.c_id_doc.DataPropertyName = "id";
            this.c_id_doc.HeaderText = "Id";
            this.c_id_doc.MinimumWidth = 42;
            this.c_id_doc.Name = "c_id_doc";
            this.c_id_doc.ReadOnly = true;
            this.c_id_doc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.c_id_doc.Width = 42;
            //
            // c_id_version
            //
            this.c_id_version.DataPropertyName = "id_version";
            this.c_id_version.HeaderText = "id_version";
            this.c_id_version.Name = "c_id_version";
            this.c_id_version.ReadOnly = true;
            this.c_id_version.Visible = false;
            //
            // c_id_user
            //
            this.c_id_user.DataPropertyName = "id_user";
            this.c_id_user.HeaderText = "id_user";
            this.c_id_user.Name = "c_id_user";
            this.c_id_user.ReadOnly = true;
            this.c_id_user.Visible = false;
            //
            // c_id_folder
            //
            this.c_id_folder.DataPropertyName = "id_folder";
            this.c_id_folder.HeaderText = "id_folder";
            this.c_id_folder.Name = "c_id_folder";
            this.c_id_folder.ReadOnly = true;
            this.c_id_folder.Visible = false;
            //
            // c_id_type
            //
            this.c_id_type.DataPropertyName = "id_type";
            this.c_id_type.HeaderText = "id_type";
            this.c_id_type.Name = "c_id_type";
            this.c_id_type.ReadOnly = true;
            this.c_id_type.Visible = false;
            //
            // c_id_status
            //
            this.c_id_status.DataPropertyName = "id_status";
            this.c_id_status.HeaderText = "id_status";
            this.c_id_status.Name = "c_id_status";
            this.c_id_status.ReadOnly = true;
            this.c_id_status.Visible = false;
            //
            // c_mail_in_out_prefix
            //
            this.c_mail_in_out_prefix.HeaderText = "";
            this.c_mail_in_out_prefix.Name = "c_mail_in_out_prefix";
            this.c_mail_in_out_prefix.ReadOnly = true;
            this.c_mail_in_out_prefix.Visible = false;
            //
            // c_title
            //
            this.c_title.DataPropertyName = "title";
            this.c_title.FillWeight = 70F;
            this.c_title.HeaderText = "Name";
            this.c_title.MinimumWidth = 2;
            this.c_title.Name = "c_title";
            this.c_title.ReadOnly = true;
            //
            // c_mail_subject
            //
            this.c_mail_subject.DataPropertyName = "mail_subject";
            this.c_mail_subject.HeaderText = "Subject";
            this.c_mail_subject.Name = "c_mail_subject";
            this.c_mail_subject.ReadOnly = true;
            this.c_mail_subject.Visible = false;
            //
            // c_mail_from
            //
            this.c_mail_from.DataPropertyName = "mail_from";
            this.c_mail_from.HeaderText = "From";
            this.c_mail_from.Name = "c_mail_from";
            this.c_mail_from.ReadOnly = true;
            this.c_mail_from.Visible = false;
            //
            // c_mail_to
            //
            this.c_mail_to.DataPropertyName = "mail_to";
            this.c_mail_to.HeaderText = "To";
            this.c_mail_to.Name = "c_mail_to";
            this.c_mail_to.ReadOnly = true;
            this.c_mail_to.Visible = false;
            //
            // c_mail_time
            //
            this.c_mail_time.DataPropertyName = "mail_time";
            this.c_mail_time.HeaderText = "Time";
            this.c_mail_time.Name = "c_mail_time";
            this.c_mail_time.ReadOnly = true;
            this.c_mail_time.Visible = false;
            //
            // c_mail_is_composed
            //
            this.c_mail_is_composed.DataPropertyName = "mail_is_composed";
            this.c_mail_is_composed.HeaderText = "IsComposed";
            this.c_mail_is_composed.Name = "c_mail_is_composed";
            this.c_mail_is_composed.ReadOnly = true;
            this.c_mail_is_composed.Visible = false;
            //
            // c_folder
            //
            this.c_folder.DataPropertyName = "document_folder";
            this.c_folder.FillWeight = 30F;
            this.c_folder.HeaderText = "Folder";
            this.c_folder.MinimumWidth = 2;
            this.c_folder.Name = "c_folder";
            this.c_folder.ReadOnly = true;
            //
            // c_docType
            //
            this.c_docType.DataPropertyName = "type";
            this.c_docType.FillWeight = 20F;
            this.c_docType.HeaderText = "Doc. Type";
            this.c_docType.MinimumWidth = 80;
            this.c_docType.Name = "c_docType";
            this.c_docType.ReadOnly = true;
            this.c_docType.Visible = false;
            //
            // c_author
            //
            this.c_author.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.c_author.DataPropertyName = "name";
            this.c_author.FillWeight = 10F;
            this.c_author.HeaderText = "Author";
            this.c_author.MinimumWidth = 80;
            this.c_author.Name = "c_author";
            this.c_author.ReadOnly = true;
            this.c_author.Visible = false;
            this.c_author.Width = 80;
            //
            // c_version
            //
            this.c_version.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.c_version.DataPropertyName = "version";
            this.c_version.FillWeight = 10F;
            this.c_version.HeaderText = "Version";
            this.c_version.MinimumWidth = 50;
            this.c_version.Name = "c_version";
            this.c_version.ReadOnly = true;
            this.c_version.Visible = false;
            this.c_version.Width = 50;
            //
            // c_date
            //
            this.c_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.c_date.DataPropertyName = "date";
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            dataGridViewCellStyle2.NullValue = null;
            this.c_date.DefaultCellStyle = dataGridViewCellStyle2;
            this.c_date.FillWeight = 10F;
            this.c_date.HeaderText = "Date";
            this.c_date.MinimumWidth = 120;
            this.c_date.Name = "c_date";
            this.c_date.ReadOnly = true;
            this.c_date.Visible = false;
            this.c_date.Width = 120;
            //
            // c_extension
            //
            this.c_extension.DataPropertyName = "extension";
            this.c_extension.HeaderText = "extension";
            this.c_extension.Name = "c_extension";
            this.c_extension.ReadOnly = true;
            this.c_extension.Visible = false;
            //
            // c_id_review
            //
            this.c_id_review.DataPropertyName = "id_review";
            this.c_id_review.HeaderText = "c_id_review";
            this.c_id_review.Name = "c_id_review";
            this.c_id_review.ReadOnly = true;
            this.c_id_review.Visible = false;
            //
            // c_CheckOutUser
            //
            this.c_CheckOutUser.HeaderText = "c_CheckOutUser";
            this.c_CheckOutUser.Name = "c_CheckOutUser";
            this.c_CheckOutUser.ReadOnly = true;
            this.c_CheckOutUser.Visible = false;
            //
            // c_id_sp_status
            //
            this.c_id_sp_status.DataPropertyName = "id_sp_status";
            this.c_id_sp_status.HeaderText = "c_id_sp_status";
            this.c_id_sp_status.Name = "c_id_sp_status";
            this.c_id_sp_status.ReadOnly = true;
            this.c_id_sp_status.Visible = false;
            //
            // c_created_date
            //
            this.c_created_date.DataPropertyName = "created_date";
            this.c_created_date.HeaderText = "c_created_date";
            this.c_created_date.Name = "c_created_date";
            this.c_created_date.ReadOnly = true;
            this.c_created_date.Visible = false;
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(5, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 558;
            this.label4.Text = "Id:";
            //
            // txtVersionId
            //
            this.txtVersionId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVersionId.Location = new System.Drawing.Point(55, 5);
            this.txtVersionId.Name = "txtVersionId";
            this.txtVersionId.Size = new System.Drawing.Size(66, 20);
            this.txtVersionId.TabIndex = 557;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(5, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 554;
            this.label1.Text = "Name:";
            //
            // txtVersionName
            //
            this.txtVersionName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVersionName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVersionName.Location = new System.Drawing.Point(55, 29);
            this.txtVersionName.Name = "txtVersionName";
            this.txtVersionName.Size = new System.Drawing.Size(279, 20);
            this.txtVersionName.TabIndex = 551;
            //
            // cboVersionType
            //
            this.cboVersionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboVersionType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboVersionType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboVersionType.DisplayMember = "client_name";
            this.cboVersionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVersionType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboVersionType.FormattingEnabled = true;
            this.cboVersionType.Location = new System.Drawing.Point(55, 82);
            this.cboVersionType.Name = "cboVersionType";
            this.cboVersionType.Size = new System.Drawing.Size(279, 21);
            this.cboVersionType.TabIndex = 553;
            this.cboVersionType.ValueMember = "id";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(5, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 555;
            this.label2.Text = "Type:";
            //
            // cboVersionFolder
            //
            this.cboVersionFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboVersionFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboVersionFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboVersionFolder.DisplayMember = "document_folder";
            this.cboVersionFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVersionFolder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboVersionFolder.FormattingEnabled = true;
            this.cboVersionFolder.Location = new System.Drawing.Point(55, 55);
            this.cboVersionFolder.Name = "cboVersionFolder";
            this.cboVersionFolder.Size = new System.Drawing.Size(279, 21);
            this.cboVersionFolder.TabIndex = 552;
            this.cboVersionFolder.ValueMember = "id";

            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(5, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 556;
            this.label3.Text = "Folder:";
            //
            // lblReason
            //
            this.lblReason.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblReason.AutoSize = true;
            this.lblReason.Location = new System.Drawing.Point(4, 304);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(47, 13);
            this.lblReason.TabIndex = 561;
            this.lblReason.Text = "Reason:";
            //
            // dataGridViewImageColumn1
            //
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.MinimumWidth = 20;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn1.Width = 20;
            //
            // dataGridViewTextBoxColumn1
            //
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 42;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.Width = 42;
            //
            // dataGridViewTextBoxColumn2
            //
            this.dataGridViewTextBoxColumn2.DataPropertyName = "id_version";
            this.dataGridViewTextBoxColumn2.HeaderText = "id_version";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Visible = false;
            //
            // dataGridViewTextBoxColumn3
            //
            this.dataGridViewTextBoxColumn3.DataPropertyName = "id_user";
            this.dataGridViewTextBoxColumn3.HeaderText = "id_user";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Visible = false;
            //
            // dataGridViewTextBoxColumn4
            //
            this.dataGridViewTextBoxColumn4.DataPropertyName = "id_folder";
            this.dataGridViewTextBoxColumn4.HeaderText = "id_folder";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Visible = false;
            //
            // dataGridViewTextBoxColumn5
            //
            this.dataGridViewTextBoxColumn5.DataPropertyName = "id_type";
            this.dataGridViewTextBoxColumn5.HeaderText = "id_type";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Visible = false;
            //
            // dataGridViewTextBoxColumn6
            //
            this.dataGridViewTextBoxColumn6.DataPropertyName = "id_status";
            this.dataGridViewTextBoxColumn6.HeaderText = "id_status";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Visible = false;
            //
            // dataGridViewTextBoxColumn7
            //
            this.dataGridViewTextBoxColumn7.DataPropertyName = "title";
            this.dataGridViewTextBoxColumn7.FillWeight = 70F;
            this.dataGridViewTextBoxColumn7.HeaderText = "Name";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 80;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 148;
            //
            // dataGridViewTextBoxColumn8
            //
            this.dataGridViewTextBoxColumn8.DataPropertyName = "document_folder";
            this.dataGridViewTextBoxColumn8.FillWeight = 30F;
            this.dataGridViewTextBoxColumn8.HeaderText = "Folder";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 90;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 119;
            //
            // dataGridViewTextBoxColumn9
            //
            this.dataGridViewTextBoxColumn9.DataPropertyName = "type";
            this.dataGridViewTextBoxColumn9.FillWeight = 20F;
            this.dataGridViewTextBoxColumn9.HeaderText = "Doc. Type";
            this.dataGridViewTextBoxColumn9.MinimumWidth = 80;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Visible = false;
            //
            // dataGridViewTextBoxColumn10
            //
            this.dataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn10.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn10.FillWeight = 10F;
            this.dataGridViewTextBoxColumn10.HeaderText = "Author";
            this.dataGridViewTextBoxColumn10.MinimumWidth = 80;
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Visible = false;
            this.dataGridViewTextBoxColumn10.Width = 80;
            //
            // dataGridViewTextBoxColumn11
            //
            this.dataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn11.DataPropertyName = "version";
            this.dataGridViewTextBoxColumn11.FillWeight = 10F;
            this.dataGridViewTextBoxColumn11.HeaderText = "Version";
            this.dataGridViewTextBoxColumn11.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Visible = false;
            this.dataGridViewTextBoxColumn11.Width = 50;
            //
            // dataGridViewTextBoxColumn12
            //
            this.dataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn12.DataPropertyName = "date";
            dataGridViewCellStyle6.Format = "dd/MM/yyyy";
            dataGridViewCellStyle6.NullValue = null;
            this.dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn12.FillWeight = 10F;
            this.dataGridViewTextBoxColumn12.HeaderText = "Date";
            this.dataGridViewTextBoxColumn12.MinimumWidth = 120;
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.Visible = false;
            this.dataGridViewTextBoxColumn12.Width = 120;
            //
            // dataGridViewTextBoxColumn13
            //
            this.dataGridViewTextBoxColumn13.DataPropertyName = "extension";
            this.dataGridViewTextBoxColumn13.HeaderText = "extension";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.Visible = false;
            //
            // dataGridViewTextBoxColumn14
            //
            this.dataGridViewTextBoxColumn14.DataPropertyName = "id_review";
            this.dataGridViewTextBoxColumn14.HeaderText = "c_id_review";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.Visible = false;
            //
            // dataGridViewTextBoxColumn15
            //
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            //
            // dataGridViewTextBoxColumn16
            //
            this.dataGridViewTextBoxColumn16.DataPropertyName = "id_sp_status";
            this.dataGridViewTextBoxColumn16.HeaderText = "c_id_sp_status";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.Visible = false;
            //
            // dataGridViewTextBoxColumn17
            //
            this.dataGridViewTextBoxColumn17.DataPropertyName = "created_date";
            this.dataGridViewTextBoxColumn17.HeaderText = "c_created_date";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.Visible = false;
            //
            // DocumentSearch
            //
            this.Controls.Add(this.plList);
            this.Controls.Add(this.lbSaveNewVer);
            this.Name = "DocumentSearch";
            this.Size = new System.Drawing.Size(339, 385);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.plList.ResumeLayout(false);
            this.plList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgVersionFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.Label lbSaveNewVer;
		private System.Windows.Forms.Panel plList;
		public System.Windows.Forms.Button btnVersionSearch;
		public System.Windows.Forms.TextBox txtReason;
		public SpiderDocsForms.DocumentDataGridView dtgVersionFiles;
		public System.Windows.Forms.Label label4;
		public System.Windows.Forms.TextBox txtVersionId;
		public System.Windows.Forms.Label label1;
		public System.Windows.Forms.TextBox txtVersionName;
		public System.Windows.Forms.ComboBox cboVersionType;
		public System.Windows.Forms.Label label2;
		public SpiderDocsForms.FolderComboBox cboVersionFolder;
		public System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblReason;
		private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
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
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
		private System.Windows.Forms.DataGridViewImageColumn dtgVersionFiles_Icon;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_id_doc;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_id_version;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_id_user;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_id_folder;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_id_type;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_id_status;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_mail_in_out_prefix;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_title;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_mail_subject;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_mail_from;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_mail_to;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_mail_time;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_mail_is_composed;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_folder;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_docType;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_author;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_version;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_date;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_extension;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_id_review;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_CheckOutUser;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_id_sp_status;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_created_date;
	}
}
