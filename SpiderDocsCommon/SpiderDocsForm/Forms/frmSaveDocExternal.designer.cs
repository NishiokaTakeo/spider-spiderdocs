namespace SpiderDocsForms
{
	public partial class frmSaveDocExternal
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSaveDocExternal));
			this._SaveBtn = new DocumentSaveButtons();
			this._tabControl = new System.Windows.Forms.TabControl();
			this._tabNewFile = new System.Windows.Forms.TabPage();
			this.SaveNewFileContainer = new System.Windows.Forms.SplitContainer();
			this.dtgNewDocList = new DocumentListInsert();
			this.AttrPanel = new PropertyPanelNext();
			this.tabNewVersion = new System.Windows.Forms.TabPage();
			this.SaveVerFileContainer = new System.Windows.Forms.SplitContainer();
			this.dtgNewVerList = new NewVerList();
			this.dtgVerSearch = new DocumentSearch();
			this._tabControl.SuspendLayout();
			this._tabNewFile.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SaveNewFileContainer)).BeginInit();
			this.SaveNewFileContainer.Panel1.SuspendLayout();
			this.SaveNewFileContainer.Panel2.SuspendLayout();
			this.SaveNewFileContainer.SuspendLayout();
			this.tabNewVersion.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SaveVerFileContainer)).BeginInit();
			this.SaveVerFileContainer.Panel1.SuspendLayout();
			this.SaveVerFileContainer.Panel2.SuspendLayout();
			this.SaveVerFileContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// _SaveBtn
			// 
			this._SaveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._SaveBtn.FormMode = DocumentSaveButtons_FormMode.Normal;
			this._SaveBtn.IsChangeName = false;
			this._SaveBtn.IsDeleteFile = false;
			this._SaveBtn.IsSaveLocal = false;
			this._SaveBtn.Location = new System.Drawing.Point(7, 396);
			this._SaveBtn.Name = "_SaveBtn";
			this._SaveBtn.SaveMode = DocumentSaveButtons.en_SaveMode.NewDoc;
			this._SaveBtn.SavePath = "";
			this._SaveBtn.Size = new System.Drawing.Size(802, 75);
			this._SaveBtn.TabIndex = 520;
			// 
			// _tabControl
			// 
			this._tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._tabControl.Controls.Add(this._tabNewFile);
			this._tabControl.Controls.Add(this.tabNewVersion);
			this._tabControl.Location = new System.Drawing.Point(2, 3);
			this._tabControl.Name = "_tabControl";
			this._tabControl.SelectedIndex = 0;
			this._tabControl.Size = new System.Drawing.Size(814, 392);
			this._tabControl.TabIndex = 519;
			this._tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
			// 
			// _tabNewFile
			// 
			this._tabNewFile.BackColor = System.Drawing.Color.WhiteSmoke;
			this._tabNewFile.Controls.Add(this.SaveNewFileContainer);
			this._tabNewFile.Location = new System.Drawing.Point(4, 22);
			this._tabNewFile.Name = "_tabNewFile";
			this._tabNewFile.Padding = new System.Windows.Forms.Padding(3);
			this._tabNewFile.Size = new System.Drawing.Size(806, 366);
			this._tabNewFile.TabIndex = 0;
			this._tabNewFile.Text = "Save New Document";
			// 
			// SaveNewFileContainer
			// 
			this.SaveNewFileContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SaveNewFileContainer.Location = new System.Drawing.Point(3, 3);
			this.SaveNewFileContainer.Name = "SaveNewFileContainer";
			// 
			// SaveNewFileContainer.Panel1
			// 
			this.SaveNewFileContainer.Panel1.Controls.Add(this.dtgNewDocList);
			// 
			// SaveNewFileContainer.Panel2
			// 
			this.SaveNewFileContainer.Panel2.Controls.Add(this.AttrPanel);
			this.SaveNewFileContainer.Size = new System.Drawing.Size(800, 360);
			this.SaveNewFileContainer.SplitterDistance = 500;
			this.SaveNewFileContainer.TabIndex = 2;
			// 
			// dtgNewDocList
			// 
			this.dtgNewDocList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dtgNewDocList.Location = new System.Drawing.Point(3, 3);
			this.dtgNewDocList.Name = "dtgNewDocList";
			this.dtgNewDocList.Size = new System.Drawing.Size(494, 354);
			this.dtgNewDocList.TabIndex = 1;
			// 
			// _AttrPanel
			// 
			this.AttrPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.AttrPanel.FormMode = PropertyPanelNext.en_FormMode.Multiple;
			this.AttrPanel.IsSameAttribute = false;
			this.AttrPanel.Location = new System.Drawing.Point(3, 3);
			this.AttrPanel.Name = "_AttrPanel";
			this.AttrPanel.Size = new System.Drawing.Size(290, 354);
			this.AttrPanel.TabIndex = 2;
			// 
			// _tabNewVersion
			// 
			this.tabNewVersion.BackColor = System.Drawing.Color.WhiteSmoke;
			this.tabNewVersion.Controls.Add(this.SaveVerFileContainer);
			this.tabNewVersion.Location = new System.Drawing.Point(4, 22);
			this.tabNewVersion.Name = "_tabNewVersion";
			this.tabNewVersion.Padding = new System.Windows.Forms.Padding(3);
			this.tabNewVersion.Size = new System.Drawing.Size(806, 366);
			this.tabNewVersion.TabIndex = 1;
			this.tabNewVersion.Text = "Save New Version";
			// 
			// SaveVerFileContainer
			// 
			this.SaveVerFileContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SaveVerFileContainer.Location = new System.Drawing.Point(3, 3);
			this.SaveVerFileContainer.Name = "SaveVerFileContainer";
            // 
            // dtgVerSearch
            // 
            this.dtgVerSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgVerSearch.Count = 0;
            this.dtgVerSearch.Location = new System.Drawing.Point(3, 3);
            this.dtgVerSearch.Name = "_dtgVerSearch";
            this.dtgVerSearch.ReasonTxt = "";
            this.dtgVerSearch.Size = new System.Drawing.Size(290, 354);
            this.dtgVerSearch.TabIndex = 3;
            // 
            // SaveVerFileContainer.Panel1
            // 
            this.SaveVerFileContainer.Panel1.Controls.Add(this.dtgNewVerList);
			// 
			// SaveVerFileContainer.Panel2
			// 
			this.SaveVerFileContainer.Panel2.Controls.Add(this.dtgVerSearch);
			this.SaveVerFileContainer.Size = new System.Drawing.Size(800, 360);
			this.SaveVerFileContainer.SplitterDistance = 500;
			this.SaveVerFileContainer.TabIndex = 3;
			// 
			// dtgNewVerList
			// 
			this.dtgNewVerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dtgNewVerList.Location = new System.Drawing.Point(3, 3);
			this.dtgNewVerList.Name = "dtgNewVerList";
			this.dtgNewVerList.Size = new System.Drawing.Size(494, 354);
			this.dtgNewVerList.TabIndex = 2;

			// 
			// frmSaveDocExternal
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(816, 476);
			this.Controls.Add(this._SaveBtn);
			this.Controls.Add(this._tabControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmSaveDocExternal";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Spider Docs";
			this.Load += new System.EventHandler(this.frmSaveDocExternal_Load);
			this.Shown += new System.EventHandler(this.frmSaveDocExternal_Shown);
			this._tabControl.ResumeLayout(false);
			this._tabNewFile.ResumeLayout(false);
			this.SaveNewFileContainer.Panel1.ResumeLayout(false);
			this.SaveNewFileContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SaveNewFileContainer)).EndInit();
			this.SaveNewFileContainer.ResumeLayout(false);
			this.tabNewVersion.ResumeLayout(false);
			this.SaveVerFileContainer.Panel1.ResumeLayout(false);
			this.SaveVerFileContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SaveVerFileContainer)).EndInit();
			this.SaveVerFileContainer.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion
		private System.Windows.Forms.TabControl _tabControl;
		private System.Windows.Forms.TabPage _tabNewFile;
		private System.Windows.Forms.TabPage tabNewVersion;
		private DocumentSaveButtons _SaveBtn;
		private System.Windows.Forms.SplitContainer SaveNewFileContainer;
		private DocumentListInsert dtgNewDocList;
		private PropertyPanelNext AttrPanel;
		private System.Windows.Forms.SplitContainer SaveVerFileContainer;
		private NewVerList dtgNewVerList;
		private DocumentSearch dtgVerSearch;
    }
}