namespace SpiderDocs
{
    partial class frmDeletedFiles
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtgDeletedDocument = new System.Windows.Forms.DataGridView();
            this.id_doc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.who_created = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.who_deleted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDeletedDocument)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgDeletedDocument
            // 
            this.dtgDeletedDocument.AllowUserToAddRows = false;
            this.dtgDeletedDocument.AllowUserToDeleteRows = false;
            this.dtgDeletedDocument.AllowUserToResizeColumns = false;
            this.dtgDeletedDocument.AllowUserToResizeRows = false;
            this.dtgDeletedDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgDeletedDocument.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgDeletedDocument.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_doc,
            this.title,
            this.type,
            this.who_created,
            this.who_deleted,
            this.date,
            this.reason});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgDeletedDocument.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtgDeletedDocument.Location = new System.Drawing.Point(7, 31);
            this.dtgDeletedDocument.Name = "dtgDeletedDocument";
            this.dtgDeletedDocument.ReadOnly = true;
            this.dtgDeletedDocument.RowHeadersVisible = false;
            this.dtgDeletedDocument.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgDeletedDocument.Size = new System.Drawing.Size(767, 502);
            this.dtgDeletedDocument.TabIndex = 0;
            // 
            // id_doc
            // 
            this.id_doc.DataPropertyName = "id_doc";
            this.id_doc.HeaderText = "Id";
            this.id_doc.MinimumWidth = 50;
            this.id_doc.Name = "id_doc";
            this.id_doc.ReadOnly = true;
            this.id_doc.Width = 50;
            // 
            // title
            // 
            this.title.DataPropertyName = "title";
            this.title.FillWeight = 50F;
            this.title.HeaderText = "Name";
            this.title.MinimumWidth = 150;
            this.title.Name = "title";
            this.title.ReadOnly = true;
            this.title.Width = 150;
            // 
            // type
            // 
            this.type.DataPropertyName = "type";
            this.type.HeaderText = "Doc Type";
            this.type.MinimumWidth = 100;
            this.type.Name = "type";
            this.type.ReadOnly = true;
            // 
            // who_created
            // 
            this.who_created.DataPropertyName = "whocreated";
            this.who_created.HeaderText = "Who created";
            this.who_created.MinimumWidth = 120;
            this.who_created.Name = "who_created";
            this.who_created.ReadOnly = true;
            this.who_created.Width = 120;
            // 
            // who_deleted
            // 
            this.who_deleted.DataPropertyName = "who_deleted";
            this.who_deleted.HeaderText = "Who deleted";
            this.who_deleted.MinimumWidth = 120;
            this.who_deleted.Name = "who_deleted";
            this.who_deleted.ReadOnly = true;
            this.who_deleted.Width = 120;
            // 
            // date
            // 
            this.date.DataPropertyName = "date";
            dataGridViewCellStyle1.Format = Spider.Common.ConstData.DATE;
            this.date.DefaultCellStyle = dataGridViewCellStyle1;
            this.date.HeaderText = "Date";
            this.date.MinimumWidth = 120;
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.Width = 120;
            // 
            // reason
            // 
            this.reason.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.reason.DataPropertyName = "reason";
            this.reason.HeaderText = "Reason";
            this.reason.MinimumWidth = 100;
            this.reason.Name = "reason";
            this.reason.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(291, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "List of all documents that have been deleted.";
            // 
            // frmDeletedFiles
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(780, 537);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtgDeletedDocument);
            this.MinimumSize = new System.Drawing.Size(796, 575);
            this.Name = "frmDeletedFiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Deleted Documents";
            this.Load += new System.EventHandler(this.frmDeletedDocs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgDeletedDocument)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgDeletedDocument;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_doc;
        private System.Windows.Forms.DataGridViewTextBoxColumn title;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn who_created;
        private System.Windows.Forms.DataGridViewTextBoxColumn who_deleted;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn reason;
        private System.Windows.Forms.Label label2;
    }
}