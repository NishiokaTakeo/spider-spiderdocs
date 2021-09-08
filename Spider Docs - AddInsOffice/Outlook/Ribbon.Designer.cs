using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Tools.Ribbon;
using lib = SpiderDocsModule.Library;
using SpiderDocsForms;
using Document = SpiderDocsForms.Document;
using AddInModules;

namespace AddInOutlook2013
{
    partial class Ribbon : SpiderDocsOutlookRibbon
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
            this.CurrentMail = null;
			if(disposing && (components != null))
            {
				components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.tab1 = this.Factory.CreateRibbonTab();
			this.group1 = this.Factory.CreateRibbonGroup();
			this.tab2 = this.Factory.CreateRibbonTab();
			this.group2 = this.Factory.CreateRibbonGroup();
			this.menuSaveSpider = this.Factory.CreateRibbonMenu();
			this.btnSaveToDatabse = this.Factory.CreateRibbonButton();
			this.btnSaveWorkSpace = this.Factory.CreateRibbonButton();
			this.menuSaveSpiderRead = this.Factory.CreateRibbonMenu();
			this.btnSaveRead = this.Factory.CreateRibbonButton();
			this.buttonSaveWorkspaceRead = this.Factory.CreateRibbonButton();
			this.tab1.SuspendLayout();
			this.group1.SuspendLayout();
			this.tab2.SuspendLayout();
			this.group2.SuspendLayout();
			// 
			// tab1
			// 
			this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
			this.tab1.ControlId.OfficeId = "TabMail";
			this.tab1.Groups.Add(this.group1);
			this.tab1.Label = "TabMail";
			this.tab1.Name = "tab1";
			// 
			// group1
			// 
			this.group1.Items.Add(this.menuSaveSpider);
			this.group1.Label = "SpiderDocs";
			this.group1.Name = "group1";
			// 
			// tab2
			// 
			this.tab2.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
			this.tab2.ControlId.OfficeId = "TabReadMessage";
			this.tab2.Groups.Add(this.group2);
			this.tab2.Label = "TabReadMessage";
			this.tab2.Name = "tab2";
			// 
			// group2
			// 
			this.group2.Items.Add(this.menuSaveSpiderRead);
			this.group2.Label = "Spider Docs";
			this.group2.Name = "group2";
			// 
			// menuSaveSpider
			// 
			this.menuSaveSpider.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.menuSaveSpider.Image = global::AddInOutlook2013.Properties.Resources.outmail;
			this.menuSaveSpider.Items.Add(this.btnSaveToDatabse);
			this.menuSaveSpider.Items.Add(this.btnSaveWorkSpace);
			this.menuSaveSpider.Label = "Save to SpiderDocs";
			this.menuSaveSpider.Name = "menuSaveSpider";
			this.menuSaveSpider.ShowImage = true;
			// 
			// btnSaveToDatabse
			// 
			this.btnSaveToDatabse.Description = "Save to Database";
			this.btnSaveToDatabse.Image = global::AddInOutlook2013.Properties.Resources.outmail;
			this.btnSaveToDatabse.Label = "Save to Database";
			this.btnSaveToDatabse.Name = "btnSaveToDatabse";
			this.btnSaveToDatabse.ScreenTip = "Save e-mail to Spider Docs";
			this.btnSaveToDatabse.ShowImage = true;
			this.btnSaveToDatabse.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSaveToDatabse_Click);
			// 
			// btnSaveWorkSpace
			// 
			this.btnSaveWorkSpace.Description = "Save to Workspace";
			this.btnSaveWorkSpace.Image = global::AddInOutlook2013.Properties.Resources.workspace;
			this.btnSaveWorkSpace.Label = "Save to Workspace";
			this.btnSaveWorkSpace.Name = "btnSaveWorkSpace";
			this.btnSaveWorkSpace.ShowImage = true;
			this.btnSaveWorkSpace.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSaveWorkSpace_Click);
			// 
			// menuSaveSpiderRead
			// 
			this.menuSaveSpiderRead.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.menuSaveSpiderRead.Image = global::AddInOutlook2013.Properties.Resources.outmail;
			this.menuSaveSpiderRead.Items.Add(this.btnSaveRead);
			this.menuSaveSpiderRead.Items.Add(this.buttonSaveWorkspaceRead);
			this.menuSaveSpiderRead.Label = "Save to SpiderDocs";
			this.menuSaveSpiderRead.Name = "menuSaveSpiderRead";
			this.menuSaveSpiderRead.ShowImage = true;
			// 
			// btnSaveRead
			// 
			this.btnSaveRead.Image = global::AddInOutlook2013.Properties.Resources.outmail;
			this.btnSaveRead.Label = "Save  to database";
			this.btnSaveRead.Name = "btnSaveRead";
			this.btnSaveRead.ScreenTip = "Save e-mail to Spider Docs";
			this.btnSaveRead.ShowImage = true;
			this.btnSaveRead.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSaveToDatabse_Click);
			// 
			// buttonSaveWorkspaceRead
			// 
			this.buttonSaveWorkspaceRead.Image = global::AddInOutlook2013.Properties.Resources.workspace;
			this.buttonSaveWorkspaceRead.Label = "Save to workspace";
			this.buttonSaveWorkspaceRead.Name = "buttonSaveWorkspaceRead";
			this.buttonSaveWorkspaceRead.ShowImage = true;
			this.buttonSaveWorkspaceRead.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSaveWorkSpace_Click);
			// 
			// Ribbon
			// 
			this.Name = "Ribbon";
			this.RibbonType = "Microsoft.Outlook.Explorer, Microsoft.Outlook.Mail.Read";
			this.Tabs.Add(this.tab1);
			this.Tabs.Add(this.tab2);
			this.tab1.ResumeLayout(false);
			this.tab1.PerformLayout();
			this.group1.ResumeLayout(false);
			this.group1.PerformLayout();
			this.tab2.ResumeLayout(false);
			this.tab2.PerformLayout();
			this.group2.ResumeLayout(false);
			this.group2.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSaveToDatabse;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSaveWorkSpace;
        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab2;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSaveRead;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSaveWorkspaceRead;
		internal Microsoft.Office.Tools.Ribbon.RibbonMenu menuSaveSpider;
		internal Microsoft.Office.Tools.Ribbon.RibbonMenu menuSaveSpiderRead;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon ribbon
        {
            get
			{
				return this.GetRibbon<Ribbon>(); }
        }
    }
}
