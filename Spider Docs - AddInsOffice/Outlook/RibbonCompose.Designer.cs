namespace AddInOutlook2013 {
	partial class RibbonCompose : SpiderDocsOutlookRibbon {
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
			this.tab1 = this.Factory.CreateRibbonTab();
			this.group2 = this.Factory.CreateRibbonGroup();
			this.menuSaveSpiderCompose = this.Factory.CreateRibbonMenu();
			this.btnSaveCompose = this.Factory.CreateRibbonButton();
			this.buttonSaveWorkspaceCompose = this.Factory.CreateRibbonButton();
			this.tab1.SuspendLayout();
			this.group2.SuspendLayout();
			// 
			// tab1
			// 
			this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
			this.tab1.ControlId.OfficeId = "TabNewMailMessage";
			this.tab1.Groups.Add(this.group2);
			this.tab1.Label = "TabNewMailMessage";
			this.tab1.Name = "tab1";
			// 
			// group2
			// 
			this.group2.Items.Add(this.menuSaveSpiderCompose);
			this.group2.Label = "Spider Docs";
			this.group2.Name = "group2";
			// 
			// menuSaveSpiderCompose
			// 
			this.menuSaveSpiderCompose.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.menuSaveSpiderCompose.Image = global::AddInOutlook2013.Properties.Resources.outmail;
			this.menuSaveSpiderCompose.Items.Add(this.btnSaveCompose);
			this.menuSaveSpiderCompose.Items.Add(this.buttonSaveWorkspaceCompose);
			this.menuSaveSpiderCompose.Label = "Save to SpiderDocs";
			this.menuSaveSpiderCompose.Name = "menuSaveSpiderCompose";
			this.menuSaveSpiderCompose.ShowImage = true;
			// 
			// btnSaveCompose
			// 
			this.btnSaveCompose.Image = global::AddInOutlook2013.Properties.Resources.outmail;
			this.btnSaveCompose.Label = "Save  to database";
			this.btnSaveCompose.Name = "btnSaveCompose";
			this.btnSaveCompose.ScreenTip = "Save e-mail to Spider Docs";
			this.btnSaveCompose.ShowImage = true;
			this.btnSaveCompose.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSaveToDatabse_Click);
			// 
			// buttonSaveWorkspaceCompose
			// 
			this.buttonSaveWorkspaceCompose.Image = global::AddInOutlook2013.Properties.Resources.workspace;
			this.buttonSaveWorkspaceCompose.Label = "Save to workspace";
			this.buttonSaveWorkspaceCompose.Name = "buttonSaveWorkspaceCompose";
			this.buttonSaveWorkspaceCompose.ShowImage = true;
			this.buttonSaveWorkspaceCompose.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSaveWorkSpace_Click);
			// 
			// RibbonCompose
			// 
			this.Name = "RibbonCompose";
			this.RibbonType = "Microsoft.Outlook.Mail.Compose";
			this.Tabs.Add(this.tab1);
			this.tab1.ResumeLayout(false);
			this.tab1.PerformLayout();
			this.group2.ResumeLayout(false);
			this.group2.PerformLayout();

		}

		#endregion

		internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
		internal Microsoft.Office.Tools.Ribbon.RibbonMenu menuSaveSpiderCompose;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSaveCompose;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSaveWorkspaceCompose;
	}

	partial class ThisRibbonCollection {
		internal RibbonCompose RibbonCompose {
			get {
			return this.GetRibbon<RibbonCompose>();
			}
		}
	}
}
