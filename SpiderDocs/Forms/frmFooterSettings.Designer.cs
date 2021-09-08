namespace SpiderDocs
{
    partial class frmFooterSettings
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
			this.label2 = new System.Windows.Forms.Label();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label7 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label6 = new System.Windows.Forms.Label();
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lvGroupd = new DragNDrop.DragAndDropListView();
			this.lvUsersOfGroup = new DragNDrop.DragAndDropListView();
			this.btnDelete = new System.Windows.Forms.Button();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.lvAttributes = new DragNDrop.DragAndDropListView();
			this.GeneralVal_C0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.GeneralVal_C1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lbAttributes = new System.Windows.Forms.Label();
			this.lvFooter = new DragNDrop.DragAndDropListView();
			this.lvFooter_C0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lvFooter_C1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.label2.Location = new System.Drawing.Point(10, 5);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 17);
			this.label2.TabIndex = 22;
			this.label2.Text = "Allowed";
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 25;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Name";
			this.columnHeader2.Width = 212;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label7.Location = new System.Drawing.Point(16, 31);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(290, 15);
			this.label7.TabIndex = 26;
			this.label7.Text = "Add attributes to the footer, then change their orders.";
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.label7);
			this.panel1.Location = new System.Drawing.Point(-5, -13);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(635, 63);
			this.panel1.TabIndex = 28;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label6.Location = new System.Drawing.Point(3, 6);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(44, 13);
			this.label6.TabIndex = 25;
			this.label6.Text = "Allowed";
			// 
			// columnHeader7
			// 
			this.columnHeader7.Width = 25;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Width = 25;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Width = 25;
			// 
			// lvGroupd
			// 
			this.lvGroupd.AllowReorder = true;
			this.lvGroupd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lvGroupd.BackColor = System.Drawing.Color.FloralWhite;
			this.lvGroupd.Corinthians = false;
			this.lvGroupd.FullRowSelect = true;
			this.lvGroupd.HideSelection = false;
			this.lvGroupd.LineColor = System.Drawing.Color.Gainsboro;
			this.lvGroupd.Location = new System.Drawing.Point(3, 21);
			this.lvGroupd.MultiSelect = false;
			this.lvGroupd.Name = "lvGroupd";
			this.lvGroupd.Reorder = false;
			this.lvGroupd.Size = new System.Drawing.Size(272, 294);
			this.lvGroupd.TabIndex = 23;
			this.lvGroupd.UseCompatibleStateImageBehavior = false;
			this.lvGroupd.View = System.Windows.Forms.View.Details;
			// 
			// lvUsersOfGroup
			// 
			this.lvUsersOfGroup.AllowReorder = true;
			this.lvUsersOfGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lvUsersOfGroup.BackColor = System.Drawing.Color.FloralWhite;
			this.lvUsersOfGroup.Corinthians = false;
			this.lvUsersOfGroup.FullRowSelect = true;
			this.lvUsersOfGroup.LineColor = System.Drawing.Color.Gainsboro;
			this.lvUsersOfGroup.Location = new System.Drawing.Point(5, 20);
			this.lvUsersOfGroup.Name = "lvUsersOfGroup";
			this.lvUsersOfGroup.Reorder = false;
			this.lvUsersOfGroup.Size = new System.Drawing.Size(270, 222);
			this.lvUsersOfGroup.TabIndex = 24;
			this.lvUsersOfGroup.UseCompatibleStateImageBehavior = false;
			this.lvUsersOfGroup.View = System.Windows.Forms.View.Details;
			// 
			// btnDelete
			// 
			this.btnDelete.FlatAppearance.BorderSize = 0;
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDelete.Image = global::SpiderDocs.Properties.Resources.delete;
			this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnDelete.Location = new System.Drawing.Point(550, 53);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(59, 21);
			this.btnDelete.TabIndex = 36;
			this.btnDelete.Text = "Delete";
			this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnExcluir_Click);
			// 
			// pictureBox2
			// 
			this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.pictureBox2.BackColor = System.Drawing.Color.WhiteSmoke;
			this.pictureBox2.Image = global::SpiderDocs.Properties.Resources.arrow_r_32;
			this.pictureBox2.Location = new System.Drawing.Point(293, 292);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(38, 38);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox2.TabIndex = 39;
			this.pictureBox2.TabStop = false;
			this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
			// 
			// lvAttributes
			// 
			this.lvAttributes.AllowReorder = true;
			this.lvAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lvAttributes.BackColor = System.Drawing.Color.FloralWhite;
			this.lvAttributes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.GeneralVal_C0,
            this.GeneralVal_C1});
			this.lvAttributes.Corinthians = false;
			this.lvAttributes.FullRowSelect = true;
			this.lvAttributes.HideSelection = false;
			this.lvAttributes.LineColor = System.Drawing.Color.Gainsboro;
			this.lvAttributes.Location = new System.Drawing.Point(15, 76);
			this.lvAttributes.MultiSelect = false;
			this.lvAttributes.Name = "lvAttributes";
			this.lvAttributes.Reorder = false;
			this.lvAttributes.Size = new System.Drawing.Size(272, 506);
			this.lvAttributes.TabIndex = 38;
			this.lvAttributes.UseCompatibleStateImageBehavior = false;
			this.lvAttributes.View = System.Windows.Forms.View.Details;
			this.lvAttributes.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ItemSelectionChanged);
			this.lvAttributes.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvFooter_DragDrop);
			this.lvAttributes.DoubleClick += new System.EventHandler(this.lvAttributes_DoubleClick);
			// 
			// GeneralVal_C0
			// 
			this.GeneralVal_C0.Text = "Name";
			this.GeneralVal_C0.Width = 172;
			// 
			// GeneralVal_C1
			// 
			this.GeneralVal_C1.Text = "Type";
			this.GeneralVal_C1.Width = 90;
			// 
			// lbAttributes
			// 
			this.lbAttributes.AutoSize = true;
			this.lbAttributes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbAttributes.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lbAttributes.Location = new System.Drawing.Point(12, 57);
			this.lbAttributes.Name = "lbAttributes";
			this.lbAttributes.Size = new System.Drawing.Size(51, 13);
			this.lbAttributes.TabIndex = 35;
			this.lbAttributes.Text = "Attributes";
			// 
			// lvFooter
			// 
			this.lvFooter.AllowDrop = true;
			this.lvFooter.AllowReorder = true;
			this.lvFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lvFooter.BackColor = System.Drawing.Color.FloralWhite;
			this.lvFooter.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvFooter_C0,
            this.lvFooter_C1});
			this.lvFooter.Corinthians = true;
			this.lvFooter.FullRowSelect = true;
			this.lvFooter.HideSelection = false;
			this.lvFooter.LineColor = System.Drawing.Color.Gainsboro;
			this.lvFooter.Location = new System.Drawing.Point(337, 76);
			this.lvFooter.MultiSelect = false;
			this.lvFooter.Name = "lvFooter";
			this.lvFooter.Reorder = true;
			this.lvFooter.Size = new System.Drawing.Size(272, 506);
			this.lvFooter.TabIndex = 34;
			this.lvFooter.UseCompatibleStateImageBehavior = false;
			this.lvFooter.View = System.Windows.Forms.View.Details;
			this.lvFooter.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ItemSelectionChanged);
			this.lvFooter.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvFooter_DragDrop);
			// 
			// lvFooter_C0
			// 
			this.lvFooter_C0.Text = "Name";
			this.lvFooter_C0.Width = 172;
			// 
			// lvFooter_C1
			// 
			this.lvFooter_C1.Text = "Type";
			this.lvFooter_C1.Width = 90;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Location = new System.Drawing.Point(334, 57);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(37, 13);
			this.label1.TabIndex = 37;
			this.label1.Text = "Footer";
			// 
			// frmFooterSettings
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(627, 594);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.lvAttributes);
			this.Controls.Add(this.lbAttributes);
			this.Controls.Add(this.lvFooter);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
			this.MinimumSize = new System.Drawing.Size(1, 1);
			this.Name = "frmFooterSettings";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Footer Settings";
			this.Load += new System.EventHandler(this.frmFooterSettings_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Panel panel1;
		private DragNDrop.DragAndDropListView lvGroupd;
		private DragNDrop.DragAndDropListView lvUsersOfGroup;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.PictureBox pictureBox2;
		private DragNDrop.DragAndDropListView lvAttributes;
		private System.Windows.Forms.ColumnHeader GeneralVal_C0;
		private System.Windows.Forms.ColumnHeader GeneralVal_C1;
		private System.Windows.Forms.Label lbAttributes;
		private DragNDrop.DragAndDropListView lvFooter;
		private System.Windows.Forms.ColumnHeader lvFooter_C0;
		private System.Windows.Forms.ColumnHeader lvFooter_C1;
		private System.Windows.Forms.Label label1;
    }
}