namespace AddInPowerPoint2013
{
    partial class Ribbon : AddInModules.SpiderDocsRibbon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.tab1 = this.Factory.CreateRibbonTab();
			this.group1 = this.Factory.CreateRibbonGroup();
			this.menuSaveSpider = this.Factory.CreateRibbonMenu();
			this.btnSave = this.Factory.CreateRibbonButton();
			this.buttonSaveWorkspace = this.Factory.CreateRibbonButton();
			this.timerCheckSystemStatus = new System.Windows.Forms.Timer(this.components);
			this.tab1.SuspendLayout();
			this.group1.SuspendLayout();
			// 
			// tab1
			// 
			this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
			this.tab1.ControlId.OfficeId = "TabHome";
			this.tab1.Groups.Add(this.group1);
			this.tab1.Label = "TabHome";
			this.tab1.Name = "tab1";
			// 
			// group1
			// 
			this.group1.Items.Add(this.menuSaveSpider);
			this.group1.Label = "Spider Docs";
			this.group1.Name = "group1";
			// 
			// menuSaveSpider
			// 
			this.menuSaveSpider.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.menuSaveSpider.Image = global::AddInPowerPoint2013.Properties.Resources.addinPowerPoint;
			this.menuSaveSpider.Items.Add(this.btnSave);
			this.menuSaveSpider.Items.Add(this.buttonSaveWorkspace);
			this.menuSaveSpider.Label = "Save to  SpiderDocs";
			this.menuSaveSpider.Name = "menuSaveSpider";
			this.menuSaveSpider.ShowImage = true;
			// 
			// btnSave
			// 
			this.btnSave.Image = global::AddInPowerPoint2013.Properties.Resources.addinPowerPoint;
			this.btnSave.Label = "Save  to database";
			this.btnSave.Name = "btnSave";
			this.btnSave.ScreenTip = "Save e-mail to Spider Docs";
			this.btnSave.ShowImage = true;
			this.btnSave.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSave_Click);
			// 
			// buttonSaveWorkspace
			// 
			this.buttonSaveWorkspace.Enabled = false;
			this.buttonSaveWorkspace.Image = global::AddInPowerPoint2013.Properties.Resources.workspace;
			this.buttonSaveWorkspace.Label = "Save to workspace";
			this.buttonSaveWorkspace.Name = "buttonSaveWorkspace";
			this.buttonSaveWorkspace.ShowImage = true;
			this.buttonSaveWorkspace.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSaveWorkspace_Click);
			// 
			// Ribbon
			// 
			this.Name = "Ribbon";
			this.RibbonType = "Microsoft.PowerPoint.Presentation";
			this.Tabs.Add(this.tab1);
			this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon_Load);
			this.tab1.ResumeLayout(false);
			this.tab1.PerformLayout();
			this.group1.ResumeLayout(false);
			this.group1.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menuSaveSpider;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSave;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSaveWorkspace;
        public System.Windows.Forms.Timer timerCheckSystemStatus;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon Ribbon1
        {
            get { return this.GetRibbon<Ribbon>(); }
        }
    }
}
