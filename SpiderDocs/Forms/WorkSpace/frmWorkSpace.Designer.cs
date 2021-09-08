using System;
using System.Windows.Forms;
using SpiderDocsForms;

namespace SpiderDocs
{
    partial class frmWorkSpace
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWorkSpace));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuDtgVersionFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuVersionOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVersionSendEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVersionSendEmailOriginal = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVersionSendEmailPdf = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVersionSendEmailDms = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVersionExport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVersionExportWorkSpace = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVersionExportHardDisk = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVersionExportHardDiskOriginal = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVersionExportHardDiskPdf = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVersionRollback = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVersionDms = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripTreeViewOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.menuLocalCheckIn = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLocalSendByEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLocalSendByEmailOriginal = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLocalSendByEmailPdf = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLocalDeleteFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLocalExport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLocalExportOriginal = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLocalExportPdf = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDtgLocalFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuLocalSaveAsNewVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuLocalChangeFileName = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolTipats = new System.Windows.Forms.ToolTip(this.components);
            this.pbCustomSearchFields = new System.Windows.Forms.PictureBox();
            this.pbLocalGridFull = new System.Windows.Forms.PictureBox();
            this.pbDbGridFull = new System.Windows.Forms.PictureBox();
            this.pbShowBothSides = new System.Windows.Forms.PictureBox();
            this.treeViewFolderExplore = new MultiSelectTreeview.MultiSelectTreeview();
            this.contextMenuStripFolderViewOption1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListExplorer = new System.Windows.Forms.ImageList(this.components);
            this.menuDbCheckOut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDbCheckOutNoFooter = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDbCheckOutFooter = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDbOpenRead = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDbSendByEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDbSendByEmailOriginal = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDbSendByEmailPdf = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDbSendByEmailDms = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDbExport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDbExportOriginal = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDbExportPdf = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDbProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDtgSystemFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuDbCancelCheckOut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuDbSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDbImportNewVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDbDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDbArchive = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDbReview = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDbDmsFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuDbRename = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDbGoToFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.checkOutWithFooterEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLocalEditPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.tabControlSearch = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cboReview = new System.Windows.Forms.ComboBox();
            this.lblReview = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.panelCustomSearchFields = new System.Windows.Forms.Panel();
            this.ck_Review = new System.Windows.Forms.CheckBox();
            this.ck_author = new System.Windows.Forms.CheckBox();
            this.ck_extension = new System.Windows.Forms.CheckBox();
            this.ck_docType = new System.Windows.Forms.CheckBox();
            this.ck_date = new System.Windows.Forms.CheckBox();
            this.ck_folder = new System.Windows.Forms.CheckBox();
            this.ck_name = new System.Windows.Forms.CheckBox();
            this.ck_keyword = new System.Windows.Forms.CheckBox();
            this.ck_id = new System.Windows.Forms.CheckBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblKeyword = new System.Windows.Forms.Label();
            this.dtEnd = new System.Windows.Forms.MaskedTextBox();
            this.lblFolder = new System.Windows.Forms.Label();
            this.dtBegin = new System.Windows.Forms.MaskedTextBox();
            this.txtKeyWord = new System.Windows.Forms.TextBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblExtension = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.pboxLoadingSearch = new System.Windows.Forms.PictureBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cboAuthor = new SpiderCustomComponent.CheckComboBox();
            this.cboExtension = new SpiderCustomComponent.CheckComboBox();
            this.cboDocType = new SpiderCustomComponent.CheckComboBox();
            this.cboFolder = new SpiderDocsForms.FolderComboBox(this.components);
            this.attributeSearch = new SpiderDocsForms.AttributeSearch();
            this.attributeSearch = new SpiderDocsForms.AttributeSearch();
            this.lblDocType = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFolderRename = new System.Windows.Forms.Button();
            this.btnFolderDelete = new System.Windows.Forms.Button();
            this.btnFolderCreate = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.treeViewArchivedFolder = new MultiSelectTreeview.MultiSelectTreeview();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.treeViewMSExplorer = new MultiSelectTreeview.MultiSelectTreeview();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnExport = new System.Windows.Forms.Button();
            this.dtgBdFiles = new SpiderDocsForms.DocumentDataGridView();
            this.c_img = new System.Windows.Forms.DataGridViewImageColumn();
            this.c_id_doc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_mail_in_out_prefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_mail_subject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_mail_from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_mail_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_mail_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_mail_is_composed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_folder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_type_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_author = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_id_version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_atb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_id_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_id_folder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_id_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_id_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_extension = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_id_review = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_id_sp_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_created_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_id_checkout_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flpCustomGrid = new System.Windows.Forms.FlowLayoutPanel();
            this.ck_c_id = new System.Windows.Forms.CheckBox();
            this.ck_c_name = new System.Windows.Forms.CheckBox();
            this.ck_c_folder = new System.Windows.Forms.CheckBox();
            this.ck_c_docType = new System.Windows.Forms.CheckBox();
            this.ck_c_author = new System.Windows.Forms.CheckBox();
            this.ck_c_version = new System.Windows.Forms.CheckBox();
            this.ck_c_date = new System.Windows.Forms.CheckBox();
            this.cboAttributes = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tbDbFiles = new System.Windows.Forms.TabControl();
            this.tbProperties = new System.Windows.Forms.TabPage();
            this.lblBdByVal = new System.Windows.Forms.Label();
            this.lblBdDeadlineVal = new System.Windows.Forms.Label();
            this.lblBdBy = new System.Windows.Forms.Label();
            this.lblBdDeadline = new System.Windows.Forms.Label();
            this.lblBdId = new System.Windows.Forms.Label();
            this.lblBdIdTitle = new System.Windows.Forms.Label();
            this.lblBdCurrentVersion = new System.Windows.Forms.Label();
            this.pictureBoxBd = new System.Windows.Forms.PictureBox();
            this.lblBdUpdated = new System.Windows.Forms.Label();
            this.lblBdType = new System.Windows.Forms.Label();
            this.lblBdSize = new System.Windows.Forms.Label();
            this.lblBdUpdatedTitle = new System.Windows.Forms.Label();
            this.lblBdTypeTitle = new System.Windows.Forms.Label();
            this.lblBdSizeTitle = new System.Windows.Forms.Label();
            this.lblBdCurrentVersionTitle = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblBdName = new System.Windows.Forms.Label();
            this.lblSystemFile = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbVersion = new System.Windows.Forms.TabPage();
            this.dtgVersions = new SpiderDocsForms.DocumentDataGridView();
            this.versionb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cc_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_event = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iddoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbHistoric = new System.Windows.Forms.TabPage();
            this.dtgHist = new System.Windows.Forms.DataGridView();
            this.versionFake = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vevent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versiona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbPreview = new System.Windows.Forms.TabPage();
            this.BsrPreview = new SpiderDocs.DocBrowser();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.btnRefreshWorkSpace = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.dtgLocalFile = new SpiderDocsForms.DocumentDataGridView();
            this.dtgLocalFile_Icon = new System.Windows.Forms.DataGridViewImageColumn();
            this.dtgLocalFile_Status = new System.Windows.Forms.DataGridViewImageColumn();
            this.dtgLocalFile_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgLocalFile_mail_in_out_prefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgLocalFile_Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgLocalFile_mail_subject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgLocalFile_mail_from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgLocalFile_mail_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgLocalFile_mail_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgLocalFile_mail_is_composed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgLocalFile_Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgLocalFile_Ext = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgLocalFile_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgLocalFile_Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgLocalFile_Numver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbLocalFiles = new System.Windows.Forms.TabControl();
            this.tbDetailLocalFiles = new System.Windows.Forms.TabPage();
            this.lblLocalId = new System.Windows.Forms.Label();
            this.lblLocalIdTitle = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblSizeTitle = new System.Windows.Forms.Label();
            this.pictureBoxLocal = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblName = new System.Windows.Forms.Label();
            this.lblLocalFile = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.lblModifield = new System.Windows.Forms.Label();
            this.lblCreated = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblCreatedTitle = new System.Windows.Forms.Label();
            this.lblModifieldTitle = new System.Windows.Forms.Label();
            this.lblTypeTitle = new System.Windows.Forms.Label();
            this.lblStatusTitle = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
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
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn39 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStripFolderViewOption2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDtgVersionFiles.SuspendLayout();
            this.contextMenuStripTreeViewOptions.SuspendLayout();
            this.menuDtgLocalFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCustomSearchFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLocalGridFull)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDbGridFull)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbShowBothSides)).BeginInit();
            this.contextMenuStripFolderViewOption1.SuspendLayout();
            this.menuDtgSystemFiles.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.tabControlSearch.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panelCustomSearchFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxLoadingSearch)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgBdFiles)).BeginInit();
            this.flpCustomGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tbDbFiles.SuspendLayout();
            this.tbProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBd)).BeginInit();
            this.panel3.SuspendLayout();
            this.tbVersion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgVersions)).BeginInit();
            this.tbHistoric.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgHist)).BeginInit();
            this.tbPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLocalFile)).BeginInit();
            this.tbLocalFiles.SuspendLayout();
            this.tbDetailLocalFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLocal)).BeginInit();
            this.panel2.SuspendLayout();
            this.contextMenuStripFolderViewOption2.SuspendLayout();
            this.SuspendLayout();
            //
            // menuDtgVersionFiles
            //
            this.menuDtgVersionFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuVersionOpen,
            this.menuVersionSendEmail,
            this.menuVersionExport,
            this.menuVersionRollback,
            this.menuVersionDms});
            this.menuDtgVersionFiles.Name = "menuDtgVersionFiles";
            this.menuDtgVersionFiles.Size = new System.Drawing.Size(193, 114);
            //
            // menuVersionOpen
            //
            this.menuVersionOpen.Name = "menuVersionOpen";
            this.menuVersionOpen.Size = new System.Drawing.Size(192, 22);
            this.menuVersionOpen.Text = "Open (read only)";
            this.menuVersionOpen.Click += new System.EventHandler(this.menuVersionOpen_Click);
            //
            // menuVersionSendEmail
            //
            this.menuVersionSendEmail.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuVersionSendEmailOriginal,
            this.menuVersionSendEmailPdf,
            this.menuVersionSendEmailDms});
            this.menuVersionSendEmail.Name = "menuVersionSendEmail";
            this.menuVersionSendEmail.Size = new System.Drawing.Size(192, 22);
            this.menuVersionSendEmail.Text = "Send by E-mail";
            //
            // menuVersionSendEmailOriginal
            //
            this.menuVersionSendEmailOriginal.Name = "menuVersionSendEmailOriginal";
            this.menuVersionSendEmailOriginal.Size = new System.Drawing.Size(169, 22);
            this.menuVersionSendEmailOriginal.Text = "as Original";
            this.menuVersionSendEmailOriginal.Click += new System.EventHandler(this.menuVersionSendEmail_Click);
            //
            // menuVersionSendEmailPdf
            //
            this.menuVersionSendEmailPdf.Name = "menuVersionSendEmailPdf";
            this.menuVersionSendEmailPdf.Size = new System.Drawing.Size(169, 22);
            this.menuVersionSendEmailPdf.Text = "as PDF";
            this.menuVersionSendEmailPdf.Click += new System.EventHandler(this.menuVersionSendEmailPdf_Click);
            //
            // menuVersionSendEmailDms
            //
            this.menuVersionSendEmailDms.Name = "menuVersionSendEmailDms";
            this.menuVersionSendEmailDms.Size = new System.Drawing.Size(169, 22);
            this.menuVersionSendEmailDms.Text = "as Document Link";
            this.menuVersionSendEmailDms.Click += new System.EventHandler(this.menuVersionSendEmailDms_Click);
            //
            // menuVersionExport
            //
            this.menuVersionExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuVersionExportWorkSpace,
            this.menuVersionExportHardDisk});
            this.menuVersionExport.Name = "menuVersionExport";
            this.menuVersionExport.Size = new System.Drawing.Size(192, 22);
            this.menuVersionExport.Text = "Export to";
            //
            // menuVersionExportWorkSpace
            //
            this.menuVersionExportWorkSpace.Name = "menuVersionExportWorkSpace";
            this.menuVersionExportWorkSpace.Size = new System.Drawing.Size(136, 22);
            this.menuVersionExportWorkSpace.Text = "Work Space";
            this.menuVersionExportWorkSpace.Click += new System.EventHandler(this.menuVersionExportWorkSpace_Click);
            //
            // menuVersionExportHardDisk
            //
            this.menuVersionExportHardDisk.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuVersionExportHardDiskOriginal,
            this.menuVersionExportHardDiskPdf});
            this.menuVersionExportHardDisk.Name = "menuVersionExportHardDisk";
            this.menuVersionExportHardDisk.Size = new System.Drawing.Size(136, 22);
            this.menuVersionExportHardDisk.Text = "Hard Disk";
            //
            // menuVersionExportHardDiskOriginal
            //
            this.menuVersionExportHardDiskOriginal.Name = "menuVersionExportHardDiskOriginal";
            this.menuVersionExportHardDiskOriginal.Size = new System.Drawing.Size(130, 22);
            this.menuVersionExportHardDiskOriginal.Text = "as Original";
            this.menuVersionExportHardDiskOriginal.Click += new System.EventHandler(this.menuVersionExportHardDisk_Click);
            //
            // menuVersionExportHardDiskPdf
            //
            this.menuVersionExportHardDiskPdf.Name = "menuVersionExportHardDiskPdf";
            this.menuVersionExportHardDiskPdf.Size = new System.Drawing.Size(130, 22);
            this.menuVersionExportHardDiskPdf.Text = "as PDF";
            this.menuVersionExportHardDiskPdf.Click += new System.EventHandler(this.menuVersionExportHardDiskPdf_Click);
            //
            // menuVersionRollback
            //
            this.menuVersionRollback.Name = "menuVersionRollback";
            this.menuVersionRollback.Size = new System.Drawing.Size(192, 22);
            this.menuVersionRollback.Text = "Rollback";
            this.menuVersionRollback.Click += new System.EventHandler(this.rollbackToolStripMenuItem_Click);
            //
            // menuVersionDms
            //
            this.menuVersionDms.Name = "menuVersionDms";
            this.menuVersionDms.Size = new System.Drawing.Size(192, 22);
            this.menuVersionDms.Text = "Create Document Link";
            this.menuVersionDms.Click += new System.EventHandler(this.menuVersionDms_Click);
            //
            // contextMenuStripTreeViewOptions
            //
            this.contextMenuStripTreeViewOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextMenuStripTreeViewOptions.Name = "contextMenuStripTreeViewOptions";
            this.contextMenuStripTreeViewOptions.Size = new System.Drawing.Size(134, 48);
            //
            // toolStripMenuItem1
            //
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItem1.Text = "Colapse All";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            //
            // toolStripMenuItem2
            //
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItem2.Text = "Expand All";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            //
            // imageList2
            //
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "folder.png");
            //
            // menuLocalCheckIn
            //
            this.menuLocalCheckIn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuLocalCheckIn.Name = "menuLocalCheckIn";
            this.menuLocalCheckIn.Size = new System.Drawing.Size(180, 22);
            this.menuLocalCheckIn.Text = "Check In";
            this.menuLocalCheckIn.Click += new System.EventHandler(this.menuLocalCheckIn_Click);
            //
            // menuLocalSendByEmail
            //
            this.menuLocalSendByEmail.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLocalSendByEmailOriginal,
            this.menuLocalSendByEmailPdf});
            this.menuLocalSendByEmail.Name = "menuLocalSendByEmail";
            this.menuLocalSendByEmail.Size = new System.Drawing.Size(180, 22);
            this.menuLocalSendByEmail.Text = "Send by e-mail";
            //
            // menuLocalSendByEmailOriginal
            //
            this.menuLocalSendByEmailOriginal.Name = "menuLocalSendByEmailOriginal";
            this.menuLocalSendByEmailOriginal.Size = new System.Drawing.Size(130, 22);
            this.menuLocalSendByEmailOriginal.Text = "as Original";
            this.menuLocalSendByEmailOriginal.Click += new System.EventHandler(this.menuLocalSendByEmail_Click);
            //
            // menuLocalSendByEmailPdf
            //
            this.menuLocalSendByEmailPdf.Name = "menuLocalSendByEmailPdf";
            this.menuLocalSendByEmailPdf.Size = new System.Drawing.Size(130, 22);
            this.menuLocalSendByEmailPdf.Text = "as PDF";
            this.menuLocalSendByEmailPdf.Click += new System.EventHandler(this.menuLocalSendByEmailPdf_Click);
            //
            // menuLocalDeleteFile
            //
            this.menuLocalDeleteFile.Name = "menuLocalDeleteFile";
            this.menuLocalDeleteFile.Size = new System.Drawing.Size(180, 22);
            this.menuLocalDeleteFile.Text = "Delete";
            this.menuLocalDeleteFile.Click += new System.EventHandler(this.menuLocalDeleteFile_Click);
            //
            // menuLocalExport
            //
            this.menuLocalExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLocalExportOriginal,
            this.menuLocalExportPdf});
            this.menuLocalExport.Name = "menuLocalExport";
            this.menuLocalExport.Size = new System.Drawing.Size(180, 22);
            this.menuLocalExport.Text = "Export";
            //
            // menuLocalExportOriginal
            //
            this.menuLocalExportOriginal.Name = "menuLocalExportOriginal";
            this.menuLocalExportOriginal.Size = new System.Drawing.Size(130, 22);
            this.menuLocalExportOriginal.Text = "as Original";
            this.menuLocalExportOriginal.Click += new System.EventHandler(this.menuLocalExport_Click);
            //
            // menuLocalExportPdf
            //
            this.menuLocalExportPdf.Name = "menuLocalExportPdf";
            this.menuLocalExportPdf.Size = new System.Drawing.Size(130, 22);
            this.menuLocalExportPdf.Text = "as PDF";
            this.menuLocalExportPdf.Click += new System.EventHandler(this.menuLocalExportPdf_Click);
            //
            // menuDtgLocalFiles
            //
            this.menuDtgLocalFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLocalCheckIn,
            this.menuLocalSaveAsNewVersion,
            this.toolStripSeparator3,
            this.menuLocalSendByEmail,
            this.menuLocalExport,
            this.menuLocalChangeFileName,
            this.menuLocalDeleteFile});
            this.menuDtgLocalFiles.Name = "menuDtgSystemFiles";
            this.menuDtgLocalFiles.Size = new System.Drawing.Size(181, 142);
            //
            // menuLocalSaveAsNewVersion
            //
            this.menuLocalSaveAsNewVersion.Name = "menuLocalSaveAsNewVersion";
            this.menuLocalSaveAsNewVersion.Size = new System.Drawing.Size(180, 22);
            this.menuLocalSaveAsNewVersion.Text = "Save as New Version";
            this.menuLocalSaveAsNewVersion.Visible = false;
            //
            // toolStripSeparator3
            //
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            //
            // menuLocalChangeFileName
            //
            this.menuLocalChangeFileName.Name = "menuLocalChangeFileName";
            this.menuLocalChangeFileName.Size = new System.Drawing.Size(180, 22);
            this.menuLocalChangeFileName.Text = "Rename";
            this.menuLocalChangeFileName.Click += new System.EventHandler(this.changeFileNameToolStripMenuItem_Click);
            //
            // ExportFileDialog
            //
            this.ExportFileDialog.Title = "Export file...";
            //
            // toolTipats
            //
            this.toolTipats.IsBalloon = true;
            this.toolTipats.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            //
            // pbCustomSearchFields
            //
            this.pbCustomSearchFields.Image = ((System.Drawing.Image)(resources.GetObject("pbCustomSearchFields.Image")));
            this.pbCustomSearchFields.Location = new System.Drawing.Point(272, 15);
            this.pbCustomSearchFields.Name = "pbCustomSearchFields";
            this.pbCustomSearchFields.Size = new System.Drawing.Size(16, 16);
            this.pbCustomSearchFields.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbCustomSearchFields.TabIndex = 117;
            this.pbCustomSearchFields.TabStop = false;
            this.toolTipats.SetToolTip(this.pbCustomSearchFields, "Screen Customization");
            this.pbCustomSearchFields.Click += new System.EventHandler(this.pbCustomSearchFields_Click);
            //
            // pbLocalGridFull
            //
            this.pbLocalGridFull.Image = ((System.Drawing.Image)(resources.GetObject("pbLocalGridFull.Image")));
            this.pbLocalGridFull.Location = new System.Drawing.Point(22, 4);
            this.pbLocalGridFull.Name = "pbLocalGridFull";
            this.pbLocalGridFull.Size = new System.Drawing.Size(17, 17);
            this.pbLocalGridFull.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbLocalGridFull.TabIndex = 13;
            this.pbLocalGridFull.TabStop = false;
            this.toolTipats.SetToolTip(this.pbLocalGridFull, "Show Local Files (Work Space)");
            this.pbLocalGridFull.Click += new System.EventHandler(this.pbLocalGridFull_Click);
            //
            // pbDbGridFull
            //
            this.pbDbGridFull.Image = ((System.Drawing.Image)(resources.GetObject("pbDbGridFull.Image")));
            this.pbDbGridFull.Location = new System.Drawing.Point(3, 4);
            this.pbDbGridFull.Name = "pbDbGridFull";
            this.pbDbGridFull.Size = new System.Drawing.Size(17, 17);
            this.pbDbGridFull.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbDbGridFull.TabIndex = 11;
            this.pbDbGridFull.TabStop = false;
            this.toolTipats.SetToolTip(this.pbDbGridFull, "Show DataBase Files");
            this.pbDbGridFull.Click += new System.EventHandler(this.pbDbGridFull_Click);
            //
            // pbShowBothSides
            //
            this.pbShowBothSides.Image = ((System.Drawing.Image)(resources.GetObject("pbShowBothSides.Image")));
            this.pbShowBothSides.Location = new System.Drawing.Point(41, 4);
            this.pbShowBothSides.Name = "pbShowBothSides";
            this.pbShowBothSides.Size = new System.Drawing.Size(17, 17);
            this.pbShowBothSides.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbShowBothSides.TabIndex = 12;
            this.pbShowBothSides.TabStop = false;
            this.toolTipats.SetToolTip(this.pbShowBothSides, "Show Both Sides");
            this.pbShowBothSides.Click += new System.EventHandler(this.pbShowBothSides_Click);
            //
            // treeViewFolderExplore
            //
            this.treeViewFolderExplore.AllowDrop = true;
            this.treeViewFolderExplore.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewFolderExplore.ContextMenuStrip = this.contextMenuStripFolderViewOption1;
            this.treeViewFolderExplore.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewFolderExplore.ImageIndex = 0;
            this.treeViewFolderExplore.ImageList = this.imageListExplorer;
            this.treeViewFolderExplore.Location = new System.Drawing.Point(0, 0);
            this.treeViewFolderExplore.Name = "treeViewFolderExplore";
            this.treeViewFolderExplore.SelectedImageIndex = 0;
            this.treeViewFolderExplore.SelectedNodes = ((System.Collections.Generic.List<System.Windows.Forms.TreeNode>)(resources.GetObject("treeViewFolderExplore.SelectedNodes")));
            this.treeViewFolderExplore.Size = new System.Drawing.Size(272, 571);
            this.treeViewFolderExplore.TabIndex = 1;
            this.treeViewFolderExplore.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_BeforeExpand);
            this.treeViewFolderExplore.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeViewFolderExplore_ItemDrag);
            this.treeViewFolderExplore.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewFolderExplore_BeforeSelect);
            this.treeViewFolderExplore.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewFolderExplore_AfterSelect);
            this.treeViewFolderExplore.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewFolderExplore_NodeMouseClick);
            this.treeViewFolderExplore.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeViewFolderExplore_DragDrop);
            this.treeViewFolderExplore.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeViewFolderExplore_DragEnter);
            this.treeViewFolderExplore.DragOver += new System.Windows.Forms.DragEventHandler(this.treeViewFolderExplore_DragOver);
            this.treeViewFolderExplore.DragLeave += new System.EventHandler(this.treeViewFolderExplore_DragLeave);
            this.treeViewFolderExplore.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.treeView_KeyPress);
            this.treeViewFolderExplore.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewFolderExplore_MouseDown);
            //
            // contextMenuStripFolderViewOption1
            //
            this.contextMenuStripFolderViewOption1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3});
            this.contextMenuStripFolderViewOption1.Name = "contextMenuStripTreeViewOptions";
            this.contextMenuStripFolderViewOption1.Size = new System.Drawing.Size(145, 26);
            //
            // toolStripMenuItem3
            //
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(144, 22);
            this.toolStripMenuItem3.Text = "Create Folder";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            //
            // imageListExplorer
            //
            this.imageListExplorer.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListExplorer.ImageStream")));
            this.imageListExplorer.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListExplorer.Images.SetKeyName(0, "folder.png");
            this.imageListExplorer.Images.SetKeyName(1, "folder.png");
            //
            // menuDbCheckOut
            //
            this.menuDbCheckOut.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDbCheckOutNoFooter,
            this.menuDbCheckOutFooter});
            this.menuDbCheckOut.Enabled = false;
            this.menuDbCheckOut.Name = "menuDbCheckOut";
            this.menuDbCheckOut.Size = new System.Drawing.Size(198, 22);
            this.menuDbCheckOut.Text = "Check Out (Edit)";
            this.menuDbCheckOut.Click += new System.EventHandler(this.menuDbCheckOut_Click);
            //
            // menuDbCheckOutNoFooter
            //
            this.menuDbCheckOutNoFooter.Name = "menuDbCheckOutNoFooter";
            this.menuDbCheckOutNoFooter.Size = new System.Drawing.Size(152, 22);
            this.menuDbCheckOutNoFooter.Text = "without Footer";
            this.menuDbCheckOutNoFooter.Click += new System.EventHandler(this.menuDbCheckOutWithoutFooter_Click);
            //
            // menuDbCheckOutFooter
            //
            this.menuDbCheckOutFooter.Name = "menuDbCheckOutFooter";
            this.menuDbCheckOutFooter.Size = new System.Drawing.Size(152, 22);
            this.menuDbCheckOutFooter.Text = "with Footer";
            this.menuDbCheckOutFooter.Click += new System.EventHandler(this.menuDbCheckOutFooter_Click);
            //
            // menuDbOpenRead
            //
            this.menuDbOpenRead.Name = "menuDbOpenRead";
            this.menuDbOpenRead.Size = new System.Drawing.Size(198, 22);
            this.menuDbOpenRead.Text = "Open to Read";
            this.menuDbOpenRead.Click += new System.EventHandler(this.menuDbOpenRead_Click);
            //
            // menuDbSendByEmail
            //
            this.menuDbSendByEmail.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDbSendByEmailOriginal,
            this.menuDbSendByEmailPdf,
            this.menuDbSendByEmailDms});
            this.menuDbSendByEmail.Enabled = false;
            this.menuDbSendByEmail.Name = "menuDbSendByEmail";
            this.menuDbSendByEmail.Size = new System.Drawing.Size(198, 22);
            this.menuDbSendByEmail.Text = "Send by e-mail";
            //
            // menuDbSendByEmailOriginal
            //
            this.menuDbSendByEmailOriginal.Name = "menuDbSendByEmailOriginal";
            this.menuDbSendByEmailOriginal.Size = new System.Drawing.Size(169, 22);
            this.menuDbSendByEmailOriginal.Text = "as Original";
            this.menuDbSendByEmailOriginal.Click += new System.EventHandler(this.menuDbSendByEmail_Click);
            //
            // menuDbSendByEmailPdf
            //
            this.menuDbSendByEmailPdf.Enabled = false;
            this.menuDbSendByEmailPdf.Name = "menuDbSendByEmailPdf";
            this.menuDbSendByEmailPdf.Size = new System.Drawing.Size(169, 22);
            this.menuDbSendByEmailPdf.Text = "as PDF";
            this.menuDbSendByEmailPdf.Click += new System.EventHandler(this.menuDbSendByEmailPdf_Click);
            //
            // menuDbSendByEmailDms
            //
            this.menuDbSendByEmailDms.Name = "menuDbSendByEmailDms";
            this.menuDbSendByEmailDms.Size = new System.Drawing.Size(169, 22);
            this.menuDbSendByEmailDms.Text = "as Document Link";
            this.menuDbSendByEmailDms.Click += new System.EventHandler(this.menuDbSendByEmailDms_Click);
            //
            // menuDbExport
            //
            this.menuDbExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDbExportOriginal,
            this.menuDbExportPdf});
            this.menuDbExport.Enabled = false;
            this.menuDbExport.Name = "menuDbExport";
            this.menuDbExport.Size = new System.Drawing.Size(198, 22);
            this.menuDbExport.Text = "Export";
            //
            // menuDbExportOriginal
            //
            this.menuDbExportOriginal.Name = "menuDbExportOriginal";
            this.menuDbExportOriginal.Size = new System.Drawing.Size(130, 22);
            this.menuDbExportOriginal.Text = "as Original";
            this.menuDbExportOriginal.Click += new System.EventHandler(this.menuDbExport_Click);
            //
            // menuDbExportPdf
            //
            this.menuDbExportPdf.Enabled = false;
            this.menuDbExportPdf.Name = "menuDbExportPdf";
            this.menuDbExportPdf.Size = new System.Drawing.Size(130, 22);
            this.menuDbExportPdf.Text = "as PDF";
            this.menuDbExportPdf.Click += new System.EventHandler(this.menuDbExportPdf_Click);
            //
            // menuDbProperties
            //
            this.menuDbProperties.Name = "menuDbProperties";
            this.menuDbProperties.Size = new System.Drawing.Size(198, 22);
            this.menuDbProperties.Text = "Properties";
            this.menuDbProperties.Click += new System.EventHandler(this.menuDbProperties_Click);
            //
            // menuDtgSystemFiles
            //
            this.menuDtgSystemFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDbCheckOut,
            this.menuDbCancelCheckOut,
            this.toolStripSeparator2,
            this.menuDbSaveAs,
            this.menuDbImportNewVersion,
            this.menuDbOpenRead,
            this.menuDbSendByEmail,
            this.menuDbExport,
            this.menuDbDelete,
            this.menuDbArchive,
            this.menuDbReview,
            this.menuDbDmsFile,
            this.toolStripSeparator1,
            this.menuDbRename,
            this.menuDbGoToFolder,
            this.menuDbProperties});
            this.menuDtgSystemFiles.Name = "menuDtgSystemFiles";
            this.menuDtgSystemFiles.Size = new System.Drawing.Size(199, 324);
            //
            // menuDbCancelCheckOut
            //
            this.menuDbCancelCheckOut.Enabled = false;
            this.menuDbCancelCheckOut.Name = "menuDbCancelCheckOut";
            this.menuDbCancelCheckOut.Size = new System.Drawing.Size(198, 22);
            this.menuDbCancelCheckOut.Text = "Discard Check Out";
            this.menuDbCancelCheckOut.Click += new System.EventHandler(this.menuDbCancelCheckOut_Click);
            //
            // toolStripSeparator2
            //
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(195, 6);
            //
            // menuDbSaveAs
            //
            this.menuDbSaveAs.Name = "menuDbSaveAs";
            this.menuDbSaveAs.Size = new System.Drawing.Size(198, 22);
            this.menuDbSaveAs.Text = "Save as New Document";
            this.menuDbSaveAs.Visible = false;
            this.menuDbSaveAs.Click += new System.EventHandler(this.menuDbSaveAs_Click);
            //
            // menuDbImportNewVersion
            //
            this.menuDbImportNewVersion.Enabled = false;
            this.menuDbImportNewVersion.Name = "menuDbImportNewVersion";
            this.menuDbImportNewVersion.Size = new System.Drawing.Size(198, 22);
            this.menuDbImportNewVersion.Text = "Import New Version";
            this.menuDbImportNewVersion.Click += new System.EventHandler(this.menuDbImportNewVersion_Click);
            //
            // menuDbDelete
            //
            this.menuDbDelete.Enabled = false;
            this.menuDbDelete.Name = "menuDbDelete";
            this.menuDbDelete.Size = new System.Drawing.Size(198, 22);
            this.menuDbDelete.Text = "Delete";
            this.menuDbDelete.Click += new System.EventHandler(this.menuDbDelete_Click);
            //
            // menuDbArchive
            //
            this.menuDbArchive.Enabled = false;
            this.menuDbArchive.Name = "menuDbArchive";
            this.menuDbArchive.Size = new System.Drawing.Size(198, 22);
            this.menuDbArchive.Text = "Archive";
            this.menuDbArchive.Click += new System.EventHandler(this.menuDbArchive_Click);
            //
            // menuDbReview
            //
            this.menuDbReview.Enabled = false;
            this.menuDbReview.Name = "menuDbReview";
            this.menuDbReview.Size = new System.Drawing.Size(198, 22);
            this.menuDbReview.Text = "Review";
            this.menuDbReview.Click += new System.EventHandler(this.menuDbReview_Click);
            //
            // menuDbDmsFile
            //
            this.menuDbDmsFile.Name = "menuDbDmsFile";
            this.menuDbDmsFile.Size = new System.Drawing.Size(198, 22);
            this.menuDbDmsFile.Text = "Create Document Link";
            this.menuDbDmsFile.Click += new System.EventHandler(this.menuDbDmsFile_Click);
            //
            // toolStripSeparator1
            //
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(195, 6);
            //
            // menuDbRename
            //
            this.menuDbRename.Name = "menuDbRename";
            this.menuDbRename.Size = new System.Drawing.Size(198, 22);
            this.menuDbRename.Text = "Rename";
            this.menuDbRename.Click += new System.EventHandler(this.menuDbRename_Click);
            //
            // menuDbGoToFolder
            //
            this.menuDbGoToFolder.Enabled = false;
            this.menuDbGoToFolder.Name = "menuDbGoToFolder";
            this.menuDbGoToFolder.Size = new System.Drawing.Size(198, 22);
            this.menuDbGoToFolder.Text = "Go to this Folder";
            this.menuDbGoToFolder.Click += new System.EventHandler(this.menuDbGoToFolder_Click);
            //
            // saveFileDialog
            //
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            //
            // checkOutWithFooterEditToolStripMenuItem
            //
            this.checkOutWithFooterEditToolStripMenuItem.Name = "checkOutWithFooterEditToolStripMenuItem";
            this.checkOutWithFooterEditToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.checkOutWithFooterEditToolStripMenuItem.Text = "Check Out With Footer (Edit)";
            //
            // menuLocalEditPDF
            //
            this.menuLocalEditPDF.Name = "menuLocalEditPDF";
            this.menuLocalEditPDF.Size = new System.Drawing.Size(32, 19);
            //
            // panelLeft
            //
            this.panelLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelLeft.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLeft.Controls.Add(this.tabControlSearch);
            this.panelLeft.Controls.Add(this.panel6);
            this.panelLeft.Location = new System.Drawing.Point(-2, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(285, 694);
            this.panelLeft.TabIndex = 11;
            //
            // tabControlSearch
            //
            this.tabControlSearch.AllowDrop = true;
            this.tabControlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlSearch.Controls.Add(this.tabPage2);
            this.tabControlSearch.Controls.Add(this.tabPage3);
            this.tabControlSearch.Controls.Add(this.tabPage1);
            this.tabControlSearch.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlSearch.Location = new System.Drawing.Point(2, 34);
            this.tabControlSearch.Name = "tabControlSearch";
            this.tabControlSearch.SelectedIndex = 0;
            this.tabControlSearch.Size = new System.Drawing.Size(280, 655);
            this.tabControlSearch.TabIndex = 1008;
            //
            // tabPage2
            //
            this.tabPage2.Controls.Add(this.cboReview);
            this.tabPage2.Controls.Add(this.lblReview);
            this.tabPage2.Controls.Add(this.btnClear);
            this.tabPage2.Controls.Add(this.panelCustomSearchFields);
            this.tabPage2.Controls.Add(this.lblTitle);
            this.tabPage2.Controls.Add(this.lblId);
            this.tabPage2.Controls.Add(this.txtTitle);
            this.tabPage2.Controls.Add(this.lblKeyword);
            this.tabPage2.Controls.Add(this.dtEnd);
            this.tabPage2.Controls.Add(this.lblFolder);
            this.tabPage2.Controls.Add(this.dtBegin);
            this.tabPage2.Controls.Add(this.txtKeyWord);
            this.tabPage2.Controls.Add(this.lblDate);
            this.tabPage2.Controls.Add(this.txtId);
            this.tabPage2.Controls.Add(this.lblExtension);
            this.tabPage2.Controls.Add(this.lblAuthor);
            this.tabPage2.Controls.Add(this.pboxLoadingSearch);
            this.tabPage2.Controls.Add(this.btnSearch);
            this.tabPage2.Controls.Add(this.cboAuthor);
            this.tabPage2.Controls.Add(this.cboExtension);
            this.tabPage2.Controls.Add(this.cboDocType);
            this.tabPage2.Controls.Add(this.cboFolder);
            this.tabPage2.Controls.Add(this.attributeSearch);
            this.tabPage2.Controls.Add(this.lblDocType);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(272, 629);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Advanced Search";
            this.tabPage2.UseVisualStyleBackColor = true;
            //
            // cboReview
            //
            this.cboReview.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReview.FormattingEnabled = true;
            this.cboReview.Items.AddRange(new object[] {
            "---",
            "Unreviewed",
            "Reviewed",
            "Unreviewed (All Users)",
            "Reviewed (All Users)"});
            this.cboReview.Location = new System.Drawing.Point(91, 207);
            this.cboReview.Name = "cboReview";
            this.cboReview.Size = new System.Drawing.Size(145, 21);
            this.cboReview.TabIndex = 1014;
            //
            // lblReview
            //
            this.lblReview.AutoSize = true;
            this.lblReview.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReview.Location = new System.Drawing.Point(2, 210);
            this.lblReview.Name = "lblReview";
            this.lblReview.Size = new System.Drawing.Size(46, 13);
            this.lblReview.TabIndex = 1013;
            this.lblReview.Text = "Review:";
            //
            // btnClear
            //
            this.btnClear.AutoSize = true;
            this.btnClear.Location = new System.Drawing.Point(165, 234);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(96, 23);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "All Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            //
            // panelCustomSearchFields
            //
            this.panelCustomSearchFields.AutoScroll = true;
            this.panelCustomSearchFields.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelCustomSearchFields.Controls.Add(this.ck_Review);
            this.panelCustomSearchFields.Controls.Add(this.ck_author);
            this.panelCustomSearchFields.Controls.Add(this.ck_extension);
            this.panelCustomSearchFields.Controls.Add(this.ck_docType);
            this.panelCustomSearchFields.Controls.Add(this.ck_date);
            this.panelCustomSearchFields.Controls.Add(this.ck_folder);
            this.panelCustomSearchFields.Controls.Add(this.ck_name);
            this.panelCustomSearchFields.Controls.Add(this.ck_keyword);
            this.panelCustomSearchFields.Controls.Add(this.ck_id);
            this.panelCustomSearchFields.Location = new System.Drawing.Point(239, 6);
            this.panelCustomSearchFields.Name = "panelCustomSearchFields";
            this.panelCustomSearchFields.Size = new System.Drawing.Size(22, 224);
            this.panelCustomSearchFields.TabIndex = 110;
            this.panelCustomSearchFields.Visible = false;
            //
            // ck_Review
            //
            this.ck_Review.AutoSize = true;
            this.ck_Review.Checked = true;
            this.ck_Review.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_Review.Location = new System.Drawing.Point(3, 204);
            this.ck_Review.Name = "ck_Review";
            this.ck_Review.Size = new System.Drawing.Size(15, 14);
            this.ck_Review.TabIndex = 543;
            this.ck_Review.UseVisualStyleBackColor = true;
            this.ck_Review.CheckedChanged += new System.EventHandler(this.ck_CheckedChanged);
            //
            // ck_author
            //
            this.ck_author.AutoSize = true;
            this.ck_author.Checked = true;
            this.ck_author.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_author.Location = new System.Drawing.Point(3, 129);
            this.ck_author.Name = "ck_author";
            this.ck_author.Size = new System.Drawing.Size(15, 14);
            this.ck_author.TabIndex = 542;
            this.ck_author.UseVisualStyleBackColor = true;
            this.ck_author.CheckedChanged += new System.EventHandler(this.ck_CheckedChanged);
            //
            // ck_extension
            //
            this.ck_extension.AutoSize = true;
            this.ck_extension.Checked = true;
            this.ck_extension.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_extension.Location = new System.Drawing.Point(3, 154);
            this.ck_extension.Name = "ck_extension";
            this.ck_extension.Size = new System.Drawing.Size(15, 14);
            this.ck_extension.TabIndex = 541;
            this.ck_extension.UseVisualStyleBackColor = true;
            this.ck_extension.CheckedChanged += new System.EventHandler(this.ck_CheckedChanged);
            //
            // ck_docType
            //
            this.ck_docType.AutoSize = true;
            this.ck_docType.Checked = true;
            this.ck_docType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_docType.Location = new System.Drawing.Point(3, 179);
            this.ck_docType.Name = "ck_docType";
            this.ck_docType.Size = new System.Drawing.Size(15, 14);
            this.ck_docType.TabIndex = 540;
            this.ck_docType.UseVisualStyleBackColor = true;
            this.ck_docType.CheckedChanged += new System.EventHandler(this.ck_CheckedChanged);
            //
            // ck_date
            //
            this.ck_date.AutoSize = true;
            this.ck_date.Checked = true;
            this.ck_date.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_date.Location = new System.Drawing.Point(3, 104);
            this.ck_date.Name = "ck_date";
            this.ck_date.Size = new System.Drawing.Size(15, 14);
            this.ck_date.TabIndex = 539;
            this.ck_date.UseVisualStyleBackColor = true;
            this.ck_date.CheckedChanged += new System.EventHandler(this.ck_CheckedChanged);
            //
            // ck_folder
            //
            this.ck_folder.AutoSize = true;
            this.ck_folder.Checked = true;
            this.ck_folder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_folder.Location = new System.Drawing.Point(3, 77);
            this.ck_folder.Name = "ck_folder";
            this.ck_folder.Size = new System.Drawing.Size(15, 14);
            this.ck_folder.TabIndex = 538;
            this.ck_folder.UseVisualStyleBackColor = true;
            this.ck_folder.CheckedChanged += new System.EventHandler(this.ck_CheckedChanged);
            //
            // ck_name
            //
            this.ck_name.AutoSize = true;
            this.ck_name.Checked = true;
            this.ck_name.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_name.Location = new System.Drawing.Point(3, 51);
            this.ck_name.Name = "ck_name";
            this.ck_name.Size = new System.Drawing.Size(15, 14);
            this.ck_name.TabIndex = 537;
            this.ck_name.UseVisualStyleBackColor = true;
            this.ck_name.CheckedChanged += new System.EventHandler(this.ck_CheckedChanged);
            //
            // ck_keyword
            //
            this.ck_keyword.AutoSize = true;
            this.ck_keyword.Checked = true;
            this.ck_keyword.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_keyword.Location = new System.Drawing.Point(3, 26);
            this.ck_keyword.Name = "ck_keyword";
            this.ck_keyword.Size = new System.Drawing.Size(15, 14);
            this.ck_keyword.TabIndex = 536;
            this.ck_keyword.UseVisualStyleBackColor = true;
            this.ck_keyword.CheckedChanged += new System.EventHandler(this.ck_CheckedChanged);
            //
            // ck_id
            //
            this.ck_id.AutoSize = true;
            this.ck_id.Checked = true;
            this.ck_id.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_id.Location = new System.Drawing.Point(3, 2);
            this.ck_id.Name = "ck_id";
            this.ck_id.Size = new System.Drawing.Size(15, 14);
            this.ck_id.TabIndex = 535;
            this.ck_id.UseVisualStyleBackColor = true;
            this.ck_id.CheckedChanged += new System.EventHandler(this.ck_CheckedChanged);
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(2, 57);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(38, 13);
            this.lblTitle.TabIndex = 125;
            this.lblTitle.Text = "Name:";
            //
            // lblId
            //
            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblId.Location = new System.Drawing.Point(2, 8);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(19, 13);
            this.lblId.TabIndex = 110;
            this.lblId.Text = "Id:";
            //
            // txtTitle
            //
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle.Location = new System.Drawing.Point(91, 55);
            this.txtTitle.MaxLength = 200;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(146, 22);
            this.txtTitle.TabIndex = 4;
            this.txtTitle.Tag = "";
            //
            // lblKeyword
            //
            this.lblKeyword.AutoSize = true;
            this.lblKeyword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKeyword.Location = new System.Drawing.Point(2, 32);
            this.lblKeyword.Name = "lblKeyword";
            this.lblKeyword.Size = new System.Drawing.Size(51, 13);
            this.lblKeyword.TabIndex = 111;
            this.lblKeyword.Text = "Keyword:";
            //
            // dtEnd
            //
            this.dtEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtEnd.Location = new System.Drawing.Point(159, 107);
            this.dtEnd.Mask = "00/00/0000";
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(61, 22);
            this.dtEnd.TabIndex = 7;
            this.dtEnd.ValidatingType = typeof(System.DateTime);
            this.dtEnd.Click += new System.EventHandler(this.dt_Click);
            //
            // lblFolder
            //
            this.lblFolder.AutoSize = true;
            this.lblFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFolder.Location = new System.Drawing.Point(2, 83);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(39, 13);
            this.lblFolder.TabIndex = 112;
            this.lblFolder.Text = "Folder:";
            //
            // dtBegin
            //
            this.dtBegin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtBegin.Location = new System.Drawing.Point(91, 107);
            this.dtBegin.Mask = "00/00/0000";
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(62, 22);
            this.dtBegin.TabIndex = 6;
            this.dtBegin.ValidatingType = typeof(System.DateTime);
            this.dtBegin.Click += new System.EventHandler(this.dt_Click);
            //
            // txtKeyWord
            //
            this.txtKeyWord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKeyWord.Location = new System.Drawing.Point(91, 30);
            this.txtKeyWord.MaxLength = 200;
            this.txtKeyWord.Name = "txtKeyWord";
            this.txtKeyWord.Size = new System.Drawing.Size(146, 22);
            this.txtKeyWord.TabIndex = 2;
            this.txtKeyWord.Tag = "";
            //
            // lblDate
            //
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(2, 110);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(73, 13);
            this.lblDate.TabIndex = 113;
            this.lblDate.Text = "Created Date:";
            //
            // txtId
            //
            this.txtId.BackColor = System.Drawing.SystemColors.Window;
            this.txtId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtId.Location = new System.Drawing.Point(91, 6);
            this.txtId.MaxLength = 10;
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(98, 22);
            this.txtId.TabIndex = 1;
            this.txtId.Tag = "";
            this.txtId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtId_KeyPress);
            //
            // lblExtension
            //
            this.lblExtension.AutoSize = true;
            this.lblExtension.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExtension.Location = new System.Drawing.Point(1, 160);
            this.lblExtension.Name = "lblExtension";
            this.lblExtension.Size = new System.Drawing.Size(56, 13);
            this.lblExtension.TabIndex = 115;
            this.lblExtension.Text = "Extension:";
            //
            // lblAuthor
            //
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuthor.Location = new System.Drawing.Point(2, 135);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(41, 13);
            this.lblAuthor.TabIndex = 114;
            this.lblAuthor.Text = "Author:";
            //
            // pboxLoadingSearch
            //
            this.pboxLoadingSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pboxLoadingSearch.Image = ((System.Drawing.Image)(resources.GetObject("pboxLoadingSearch.Image")));
            this.pboxLoadingSearch.Location = new System.Drawing.Point(235, 588);
            this.pboxLoadingSearch.Name = "pboxLoadingSearch";
            this.pboxLoadingSearch.Size = new System.Drawing.Size(24, 24);
            this.pboxLoadingSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxLoadingSearch.TabIndex = 1011;
            this.pboxLoadingSearch.TabStop = false;
            this.pboxLoadingSearch.Visible = false;
            //
            // btnSearch
            //
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.BackColor = System.Drawing.Color.White;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(3, 576);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(266, 50);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "                     Search            ";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.UseCompatibleTextRendering = true;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            //
            // cboAuthor
            //
            this.cboAuthor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboAuthor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboAuthor.CheckOnClick = true;
            this.cboAuthor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboAuthor.DropDownHeight = 1;
            this.cboAuthor.FormattingEnabled = true;
            this.cboAuthor.IntegralHeight = false;
            this.cboAuthor.Location = new System.Drawing.Point(91, 132);
            this.cboAuthor.MultiSelectable = true;
            this.cboAuthor.Name = "cboAuthor";
            this.cboAuthor.Size = new System.Drawing.Size(146, 23);
            this.cboAuthor.TabIndex = 8;
            this.cboAuthor.ValueSeparator = ", ";
            this.cboAuthor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbo_KeyDown);
            //
            // cboExtension
            //
            this.cboExtension.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboExtension.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboExtension.CheckOnClick = true;
            this.cboExtension.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboExtension.DropDownHeight = 1;
            this.cboExtension.FormattingEnabled = true;
            this.cboExtension.IntegralHeight = false;
            this.cboExtension.Location = new System.Drawing.Point(91, 157);
            this.cboExtension.MultiSelectable = true;
            this.cboExtension.Name = "cboExtension";
            this.cboExtension.Size = new System.Drawing.Size(146, 23);
            this.cboExtension.TabIndex = 9;
            this.cboExtension.ValueSeparator = ", ";
            this.cboExtension.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbo_KeyDown);
            //
            // cboDocType
            //
            this.cboDocType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboDocType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDocType.CheckOnClick = true;
            this.cboDocType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboDocType.DropDownHeight = 1;
            this.cboDocType.FormattingEnabled = true;
            this.cboDocType.IntegralHeight = false;
            this.cboDocType.Location = new System.Drawing.Point(91, 182);
            this.cboDocType.MultiSelectable = true;
            this.cboDocType.Name = "cboDocType";
            this.cboDocType.Size = new System.Drawing.Size(146, 23);
            this.cboDocType.TabIndex = 10;
            this.cboDocType.ValueSeparator = ", ";
            this.cboDocType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbo_KeyDown);
            //
            // cboFolder
            //
            this.cboFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboFolder.DisplayMember = "document_folder";
            this.cboFolder.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboFolder.DropDownHeight = 1;
            this.cboFolder.FormattingEnabled = true;
            this.cboFolder.IntegralHeight = false;
            this.cboFolder.Location = new System.Drawing.Point(91, 80);
            this.cboFolder.Name = "cboFolder";
            this.cboFolder.Size = new System.Drawing.Size(146, 23);
            this.cboFolder.TabIndex = 5;
            this.cboFolder.ValueMember = "id";
            this.cboFolder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbo_KeyDown);
            //
            // attributeSearch
            //
            this.attributeSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.attributeSearch.AutoScroll = true;
            this.attributeSearch.BackColor = System.Drawing.Color.Transparent;
            this.attributeSearch.CheckBoxThreeState = false;
            this.attributeSearch.FolderId = 0;
            this.attributeSearch.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.attributeSearch.IncludeComboChildren = true;
            this.attributeSearch.Location = new System.Drawing.Point(4, 258);
            this.attributeSearch.Name = "attributeSearch";
            this.attributeSearch.Search = false;
            this.attributeSearch.Size = new System.Drawing.Size(265, 313);
            this.attributeSearch.TabIndex = 11;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            //
            // lblDocType
            //
            this.lblDocType.AutoSize = true;
            this.lblDocType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocType.Location = new System.Drawing.Point(2, 185);
            this.lblDocType.Name = "lblDocType";
            this.lblDocType.Size = new System.Drawing.Size(86, 13);
            this.lblDocType.TabIndex = 118;
            this.lblDocType.Text = "Document Type:";
            //
            // tabPage3
            //
            this.tabPage3.Controls.Add(this.treeViewFolderExplore);
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Controls.Add(this.btnFolderRename);
            this.tabPage3.Controls.Add(this.btnFolderDelete);
            this.tabPage3.Controls.Add(this.btnFolderCreate);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(272, 629);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Explorer";
            this.tabPage3.UseVisualStyleBackColor = true;
            //
            // panel1
            //
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(272, 0);
            this.panel1.TabIndex = 5;
            //
            // btnFolderRename
            //
            this.btnFolderRename.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnFolderRename.FlatAppearance.BorderSize = 0;
            this.btnFolderRename.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFolderRename.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFolderRename.ForeColor = System.Drawing.Color.DimGray;
            this.btnFolderRename.Image = ((System.Drawing.Image)(resources.GetObject("btnFolderRename.Image")));
            this.btnFolderRename.Location = new System.Drawing.Point(189, 575);
            this.btnFolderRename.Name = "btnFolderRename";
            this.btnFolderRename.Size = new System.Drawing.Size(80, 50);
            this.btnFolderRename.TabIndex = 4;
            this.btnFolderRename.Text = "Rename";
            this.btnFolderRename.UseVisualStyleBackColor = true;
            this.btnFolderRename.Visible = false;
            this.btnFolderRename.Click += new System.EventHandler(this.btnFolderRename_Click);
            //
            // btnFolderDelete
            //
            this.btnFolderDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnFolderDelete.FlatAppearance.BorderSize = 0;
            this.btnFolderDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFolderDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFolderDelete.ForeColor = System.Drawing.Color.DimGray;
            this.btnFolderDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnFolderDelete.Image")));
            this.btnFolderDelete.Location = new System.Drawing.Point(95, 575);
            this.btnFolderDelete.Name = "btnFolderDelete";
            this.btnFolderDelete.Size = new System.Drawing.Size(80, 50);
            this.btnFolderDelete.TabIndex = 3;
            this.btnFolderDelete.Text = "Delete";
            this.btnFolderDelete.UseVisualStyleBackColor = true;
            this.btnFolderDelete.Visible = false;
            this.btnFolderDelete.Click += new System.EventHandler(this.btnFolderDelete_Click);
            //
            // btnFolderCreate
            //
            this.btnFolderCreate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnFolderCreate.FlatAppearance.BorderSize = 0;
            this.btnFolderCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFolderCreate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFolderCreate.ForeColor = System.Drawing.Color.DimGray;
            this.btnFolderCreate.Image = ((System.Drawing.Image)(resources.GetObject("btnFolderCreate.Image")));
            this.btnFolderCreate.Location = new System.Drawing.Point(0, 575);
            this.btnFolderCreate.Margin = new System.Windows.Forms.Padding(0);
            this.btnFolderCreate.Name = "btnFolderCreate";
            this.btnFolderCreate.Size = new System.Drawing.Size(80, 50);
            this.btnFolderCreate.TabIndex = 2;
            this.btnFolderCreate.Text = "Create";
            this.btnFolderCreate.UseVisualStyleBackColor = true;
            this.btnFolderCreate.Click += new System.EventHandler(this.btnFolderCreate_Click);
            //
            // tabPage1
            //
            this.tabPage1.Controls.Add(this.treeViewArchivedFolder);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(272, 629);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Archived";
            this.tabPage1.UseVisualStyleBackColor = true;
            //
            // treeViewArchivedFolder
            //
            this.treeViewArchivedFolder.AllowDrop = true;
            this.treeViewArchivedFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewArchivedFolder.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewArchivedFolder.ImageIndex = 0;
            this.treeViewArchivedFolder.ImageList = this.imageListExplorer;
            this.treeViewArchivedFolder.Location = new System.Drawing.Point(3, 3);
            this.treeViewArchivedFolder.Name = "treeViewArchivedFolder";
            this.treeViewArchivedFolder.SelectedImageIndex = 0;
            this.treeViewArchivedFolder.SelectedNodes = ((System.Collections.Generic.List<System.Windows.Forms.TreeNode>)(resources.GetObject("treeViewArchivedFolder.SelectedNodes")));
            this.treeViewArchivedFolder.Size = new System.Drawing.Size(266, 623);
            this.treeViewArchivedFolder.TabIndex = 2;
            this.treeViewArchivedFolder.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_BeforeExpand);
            this.treeViewArchivedFolder.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewArchivedFolder_AfterSelect);
            this.treeViewArchivedFolder.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.treeView_KeyPress);
            //
            // panel6
            //
            this.panel6.AutoSize = true;
            this.panel6.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.btnRefresh);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.pbCustomSearchFields);
            this.panel6.Controls.Add(this.panel4);
            this.panel6.Location = new System.Drawing.Point(-7, -9);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(297, 41);
            this.panel6.TabIndex = 128;
            //
            // btnRefresh
            //
            this.btnRefresh.AutoSize = true;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(131, 11);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(73, 25);
            this.btnRefresh.TabIndex = 118;
            this.btnRefresh.Text = "Recent";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 85;
            this.label1.Text = "Search";
            //
            // panel4
            //
            this.panel4.Controls.Add(this.pbLocalGridFull);
            this.panel4.Controls.Add(this.pbDbGridFull);
            this.panel4.Controls.Add(this.pbShowBothSides);
            this.panel4.Location = new System.Drawing.Point(210, 11);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(61, 23);
            this.panel4.TabIndex = 11;
            //
            // treeViewMSExplorer
            //
            this.treeViewMSExplorer.ContextMenuStrip = this.contextMenuStripTreeViewOptions;
            this.treeViewMSExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewMSExplorer.HotTracking = true;
            this.treeViewMSExplorer.ImageIndex = 0;
            this.treeViewMSExplorer.ImageList = this.imageList2;
            this.treeViewMSExplorer.LineColor = System.Drawing.Color.Empty;
            this.treeViewMSExplorer.Location = new System.Drawing.Point(3, 3);
            this.treeViewMSExplorer.Name = "treeViewMSExplorer";
            this.treeViewMSExplorer.SelectedImageIndex = 0;
            this.treeViewMSExplorer.SelectedNodes = ((System.Collections.Generic.List<System.Windows.Forms.TreeNode>)(resources.GetObject("treeViewMSExplorer.SelectedNodes")));
            this.treeViewMSExplorer.Size = new System.Drawing.Size(266, 623);
            this.treeViewMSExplorer.TabIndex = 0;
            //
            // splitContainer
            //
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(287, 0);
            this.splitContainer.Name = "splitContainer";
            //
            // splitContainer.Panel1
            //
            this.splitContainer.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer.Panel1MinSize = 0;
            //
            // splitContainer.Panel2
            //
            this.splitContainer.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer.Panel2MinSize = 0;
            this.splitContainer.Size = new System.Drawing.Size(1152, 687);
            this.splitContainer.SplitterDistance = 531;
            this.splitContainer.SplitterWidth = 8;
            this.splitContainer.TabIndex = 9;
            this.splitContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Paint);
            //
            // splitContainer2
            //
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            //
            // splitContainer2.Panel1
            //
            this.splitContainer2.Panel1.Controls.Add(this.btnExport);
            this.splitContainer2.Panel1.Controls.Add(this.dtgBdFiles);
            this.splitContainer2.Panel1.Controls.Add(this.flpCustomGrid);
            this.splitContainer2.Panel1.Controls.Add(this.label14);
            this.splitContainer2.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer2.Panel1.Click += new System.EventHandler(this.splitContainer2_Panel1_Click);
            this.splitContainer2.Panel1MinSize = 115;
            //
            // splitContainer2.Panel2
            //
            this.splitContainer2.Panel2.Controls.Add(this.tbDbFiles);
            this.splitContainer2.Panel2MinSize = 128;
            this.splitContainer2.Size = new System.Drawing.Size(525, 681);
            this.splitContainer2.SplitterDistance = 502;
            this.splitContainer2.SplitterWidth = 8;
            this.splitContainer2.TabIndex = 81;
            this.splitContainer2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer2_Paint);
            //
            // btnExport
            //
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.AutoSize = true;
            this.btnExport.Location = new System.Drawing.Point(447, 15);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 119;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            //
            // dtgBdFiles
            //
            this.dtgBdFiles.AllowDrop = true;
            this.dtgBdFiles.AllowUserToAddRows = false;
            this.dtgBdFiles.AllowUserToDeleteRows = false;
            this.dtgBdFiles.AllowUserToOrderColumns = true;
            this.dtgBdFiles.AllowUserToResizeRows = false;
            this.dtgBdFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgBdFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgBdFiles.BackgroundColor = System.Drawing.Color.White;
            this.dtgBdFiles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dtgBdFiles.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgBdFiles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgBdFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgBdFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c_img,
            this.c_id_doc,
            this.c_mail_in_out_prefix,
            this.c_title,
            this.c_mail_subject,
            this.c_mail_from,
            this.c_mail_to,
            this.c_mail_time,
            this.c_mail_is_composed,
            this.c_folder,
            this.c_type_desc,
            this.c_author,
            this.c_version,
            this.c_id_version,
            this.c_date,
            this.c_atb,
            this.c_id_user,
            this.c_id_folder,
            this.c_id_type,
            this.c_id_status,
            this.c_extension,
            this.c_id_review,
            this.c_id_sp_status,
            this.c_created_date,
            this.c_id_checkout_user,
            this.c_size});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgBdFiles.DefaultCellStyle = dataGridViewCellStyle8;
            this.dtgBdFiles.GridColor = System.Drawing.Color.Gainsboro;
            this.dtgBdFiles.Location = new System.Drawing.Point(5, 45);
            this.dtgBdFiles.Mode = SpiderDocsForms.en_DocumentDataGridViewMode.Database;
            this.dtgBdFiles.Name = "dtgBdFiles";
            this.dtgBdFiles.ReadOnly = true;
            this.dtgBdFiles.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Beige;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgBdFiles.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dtgBdFiles.RowHeadersVisible = false;
            this.dtgBdFiles.RowHeadersWidth = 20;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            this.dtgBdFiles.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dtgBdFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgBdFiles.Size = new System.Drawing.Size(517, 454);
            this.dtgBdFiles.TabIndex = 10;
            this.dtgBdFiles.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgBdFiles_CellDoubleClick);
            this.dtgBdFiles.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtgBdFiles_CellMouseClick);
            this.dtgBdFiles.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.dtgBdFiles_CellToolTipTextNeeded);
            this.dtgBdFiles.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dtgBdFiles_DataBindingComplete);
            this.dtgBdFiles.SelectionChanged += new System.EventHandler(this.dtgBdFiles_SelectionChanged);
            this.dtgBdFiles.Click += new System.EventHandler(this.dtgBdFiles_Click);
            this.dtgBdFiles.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dtgBdFiles_MouseClick);
            this.dtgBdFiles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgBdFiles_MouseUp);
            //
            // c_img
            //
            this.c_img.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.c_img.HeaderText = "";
            this.c_img.MinimumWidth = 20;
            this.c_img.Name = "c_img";
            this.c_img.ReadOnly = true;
            this.c_img.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.c_img.Width = 20;
            //
            // c_id_doc
            //
            this.c_id_doc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.c_id_doc.DataPropertyName = "id";
            this.c_id_doc.HeaderText = "Id";
            this.c_id_doc.MinimumWidth = 42;
            this.c_id_doc.Name = "c_id_doc";
            this.c_id_doc.ReadOnly = true;
            this.c_id_doc.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.c_id_doc.Width = 42;
            //
            // c_mail_in_out_prefix
            //
            this.c_mail_in_out_prefix.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.c_mail_in_out_prefix.DefaultCellStyle = dataGridViewCellStyle2;
            this.c_mail_in_out_prefix.HeaderText = "";
            this.c_mail_in_out_prefix.MinimumWidth = 35;
            this.c_mail_in_out_prefix.Name = "c_mail_in_out_prefix";
            this.c_mail_in_out_prefix.ReadOnly = true;
            this.c_mail_in_out_prefix.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.c_mail_in_out_prefix.Width = 35;
            //
            // c_title
            //
            this.c_title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.c_title.DataPropertyName = "title";
            dataGridViewCellStyle3.Format = "IN: {0}";
            dataGridViewCellStyle3.NullValue = null;
            this.c_title.DefaultCellStyle = dataGridViewCellStyle3;
            this.c_title.HeaderText = "Name";
            this.c_title.MinimumWidth = 10;
            this.c_title.Name = "c_title";
            this.c_title.ReadOnly = true;
            this.c_title.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.c_title.Width = 84;
            //
            // c_mail_subject
            //
            this.c_mail_subject.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.c_mail_subject.DataPropertyName = "mail_subject";
            this.c_mail_subject.HeaderText = "Subject";
            this.c_mail_subject.MinimumWidth = 10;
            this.c_mail_subject.Name = "c_mail_subject";
            this.c_mail_subject.ReadOnly = true;
            this.c_mail_subject.Visible = false;
            this.c_mail_subject.Width = 84;
            //
            // c_mail_from
            //
            this.c_mail_from.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.c_mail_from.DataPropertyName = "mail_from";
            this.c_mail_from.FillWeight = 70F;
            this.c_mail_from.HeaderText = "From";
            this.c_mail_from.MinimumWidth = 10;
            this.c_mail_from.Name = "c_mail_from";
            this.c_mail_from.ReadOnly = true;
            this.c_mail_from.Width = 59;
            //
            // c_mail_to
            //
            this.c_mail_to.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.c_mail_to.DataPropertyName = "mail_to";
            dataGridViewCellStyle4.Format = "dd/MM/yyyy hh:mm tt";
            this.c_mail_to.DefaultCellStyle = dataGridViewCellStyle4;
            this.c_mail_to.FillWeight = 70F;
            this.c_mail_to.HeaderText = "To";
            this.c_mail_to.MinimumWidth = 10;
            this.c_mail_to.Name = "c_mail_to";
            this.c_mail_to.ReadOnly = true;
            this.c_mail_to.Width = 59;
            //
            // c_mail_time
            //
            this.c_mail_time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.c_mail_time.DataPropertyName = "mail_time";
            dataGridViewCellStyle5.Format = "dd/MM/yyyy hh:mm tt";
            dataGridViewCellStyle5.NullValue = null;
            this.c_mail_time.DefaultCellStyle = dataGridViewCellStyle5;
            this.c_mail_time.FillWeight = 70F;
            this.c_mail_time.HeaderText = "Received";
            this.c_mail_time.MinimumWidth = 10;
            this.c_mail_time.Name = "c_mail_time";
            this.c_mail_time.ReadOnly = true;
            this.c_mail_time.Width = 59;
            //
            // c_mail_is_composed
            //
            this.c_mail_is_composed.DataPropertyName = "mail_is_composed";
            this.c_mail_is_composed.HeaderText = "MailIsComposed";
            this.c_mail_is_composed.Name = "c_mail_is_composed";
            this.c_mail_is_composed.ReadOnly = true;
            this.c_mail_is_composed.Visible = false;
            //
            // c_folder
            //
            this.c_folder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.c_folder.DataPropertyName = "document_folder";
            this.c_folder.HeaderText = "Folder";
            this.c_folder.MinimumWidth = 10;
            this.c_folder.Name = "c_folder";
            this.c_folder.ReadOnly = true;
            this.c_folder.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.c_folder.Width = 84;
            //
            // c_type_desc
            //
            this.c_type_desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.c_type_desc.DataPropertyName = "type";
            this.c_type_desc.HeaderText = "Doc. Type";
            this.c_type_desc.MinimumWidth = 10;
            this.c_type_desc.Name = "c_type_desc";
            this.c_type_desc.ReadOnly = true;
            this.c_type_desc.Width = 85;
            //
            // c_author
            //
            this.c_author.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.c_author.DataPropertyName = "name";
            this.c_author.FillWeight = 70F;
            this.c_author.HeaderText = "Author";
            this.c_author.MinimumWidth = 10;
            this.c_author.Name = "c_author";
            this.c_author.ReadOnly = true;
            this.c_author.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.c_author.Width = 59;
            //
            // c_version
            //
            this.c_version.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.c_version.DataPropertyName = "version";
            this.c_version.FillWeight = 70F;
            this.c_version.HeaderText = "Version";
            this.c_version.MinimumWidth = 10;
            this.c_version.Name = "c_version";
            this.c_version.ReadOnly = true;
            this.c_version.Width = 59;
            //
            // c_id_version
            //
            this.c_id_version.DataPropertyName = "id_version";
            this.c_id_version.HeaderText = "id_version";
            this.c_id_version.Name = "c_id_version";
            this.c_id_version.ReadOnly = true;
            this.c_id_version.Visible = false;
            //
            // c_date
            //
            this.c_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.c_date.DataPropertyName = "date";
            dataGridViewCellStyle6.Format = "dd/MM/yyyy hh:mm tt";
            dataGridViewCellStyle6.NullValue = null;
            this.c_date.DefaultCellStyle = dataGridViewCellStyle6;
            this.c_date.HeaderText = "Date";
            this.c_date.MinimumWidth = 10;
            this.c_date.Name = "c_date";
            this.c_date.ReadOnly = true;
            this.c_date.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            //
            // c_atb
            //
            this.c_atb.DataPropertyName = "atb_value";
            this.c_atb.HeaderText = "atb";
            this.c_atb.MinimumWidth = 80;
            this.c_atb.Name = "c_atb";
            this.c_atb.ReadOnly = true;
            this.c_atb.Visible = false;
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
            dataGridViewCellStyle7.Format = "dd/MM/yyyy";
            this.c_created_date.DefaultCellStyle = dataGridViewCellStyle7;
            this.c_created_date.HeaderText = "c_created_date";
            this.c_created_date.Name = "c_created_date";
            this.c_created_date.ReadOnly = true;
            this.c_created_date.Visible = false;
            //
            // c_id_checkout_user
            //
            this.c_id_checkout_user.DataPropertyName = "id_checkout_user";
            this.c_id_checkout_user.HeaderText = "c_id_checkout_user";
            this.c_id_checkout_user.Name = "c_id_checkout_user";
            this.c_id_checkout_user.ReadOnly = true;
            this.c_id_checkout_user.Visible = false;
            //
            // c_size
            //
            this.c_size.DataPropertyName = "size";
            this.c_size.HeaderText = "c_size";
            this.c_size.Name = "c_size";
            this.c_size.ReadOnly = true;
            this.c_size.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.c_size.Visible = false;
            //
            // flpCustomGrid
            //
            this.flpCustomGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpCustomGrid.AutoSize = true;
            this.flpCustomGrid.BackColor = System.Drawing.Color.LightSteelBlue;
            this.flpCustomGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpCustomGrid.Controls.Add(this.ck_c_id);
            this.flpCustomGrid.Controls.Add(this.ck_c_name);
            this.flpCustomGrid.Controls.Add(this.ck_c_folder);
            this.flpCustomGrid.Controls.Add(this.ck_c_docType);
            this.flpCustomGrid.Controls.Add(this.ck_c_author);
            this.flpCustomGrid.Controls.Add(this.ck_c_version);
            this.flpCustomGrid.Controls.Add(this.ck_c_date);
            this.flpCustomGrid.Controls.Add(this.cboAttributes);
            this.flpCustomGrid.Location = new System.Drawing.Point(5, 46);
            this.flpCustomGrid.Name = "flpCustomGrid";
            this.flpCustomGrid.Size = new System.Drawing.Size(517, 52);
            this.flpCustomGrid.TabIndex = 118;
            this.flpCustomGrid.Visible = false;
            //
            // ck_c_id
            //
            this.ck_c_id.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ck_c_id.AutoSize = true;
            this.ck_c_id.Checked = true;
            this.ck_c_id.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_c_id.Location = new System.Drawing.Point(3, 3);
            this.ck_c_id.Name = "ck_c_id";
            this.ck_c_id.Size = new System.Drawing.Size(35, 17);
            this.ck_c_id.TabIndex = 534;
            this.ck_c_id.Text = "Id";
            this.ck_c_id.UseVisualStyleBackColor = true;
            this.ck_c_id.CheckedChanged += new System.EventHandler(this.ck_c_CheckedChanged);
            //
            // ck_c_name
            //
            this.ck_c_name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ck_c_name.AutoSize = true;
            this.ck_c_name.Checked = true;
            this.ck_c_name.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_c_name.Location = new System.Drawing.Point(44, 3);
            this.ck_c_name.Name = "ck_c_name";
            this.ck_c_name.Size = new System.Drawing.Size(54, 17);
            this.ck_c_name.TabIndex = 535;
            this.ck_c_name.Text = "Name";
            this.ck_c_name.UseVisualStyleBackColor = true;
            this.ck_c_name.CheckedChanged += new System.EventHandler(this.ck_c_CheckedChanged);
            //
            // ck_c_folder
            //
            this.ck_c_folder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ck_c_folder.AutoSize = true;
            this.ck_c_folder.Checked = true;
            this.ck_c_folder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_c_folder.Location = new System.Drawing.Point(104, 3);
            this.ck_c_folder.Name = "ck_c_folder";
            this.ck_c_folder.Size = new System.Drawing.Size(55, 17);
            this.ck_c_folder.TabIndex = 536;
            this.ck_c_folder.Text = "Folder";
            this.ck_c_folder.UseVisualStyleBackColor = true;
            this.ck_c_folder.CheckedChanged += new System.EventHandler(this.ck_c_CheckedChanged);
            //
            // ck_c_docType
            //
            this.ck_c_docType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ck_c_docType.AutoSize = true;
            this.ck_c_docType.Checked = true;
            this.ck_c_docType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_c_docType.Location = new System.Drawing.Point(165, 3);
            this.ck_c_docType.Name = "ck_c_docType";
            this.ck_c_docType.Size = new System.Drawing.Size(76, 17);
            this.ck_c_docType.TabIndex = 537;
            this.ck_c_docType.Text = "Doc. Type";
            this.ck_c_docType.UseVisualStyleBackColor = true;
            this.ck_c_docType.CheckedChanged += new System.EventHandler(this.ck_c_CheckedChanged);
            //
            // ck_c_author
            //
            this.ck_c_author.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ck_c_author.AutoSize = true;
            this.ck_c_author.Checked = true;
            this.ck_c_author.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_c_author.Location = new System.Drawing.Point(247, 3);
            this.ck_c_author.Name = "ck_c_author";
            this.ck_c_author.Size = new System.Drawing.Size(57, 17);
            this.ck_c_author.TabIndex = 538;
            this.ck_c_author.Text = "Author";
            this.ck_c_author.UseVisualStyleBackColor = true;
            this.ck_c_author.CheckedChanged += new System.EventHandler(this.ck_c_CheckedChanged);
            //
            // ck_c_version
            //
            this.ck_c_version.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ck_c_version.AutoSize = true;
            this.ck_c_version.Checked = true;
            this.ck_c_version.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_c_version.Location = new System.Drawing.Point(310, 3);
            this.ck_c_version.Name = "ck_c_version";
            this.ck_c_version.Size = new System.Drawing.Size(61, 17);
            this.ck_c_version.TabIndex = 539;
            this.ck_c_version.Text = "Version";
            this.ck_c_version.UseVisualStyleBackColor = true;
            this.ck_c_version.CheckedChanged += new System.EventHandler(this.ck_c_CheckedChanged);
            //
            // ck_c_date
            //
            this.ck_c_date.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ck_c_date.AutoSize = true;
            this.ck_c_date.Checked = true;
            this.ck_c_date.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_c_date.Location = new System.Drawing.Point(377, 3);
            this.ck_c_date.Name = "ck_c_date";
            this.ck_c_date.Size = new System.Drawing.Size(49, 17);
            this.ck_c_date.TabIndex = 540;
            this.ck_c_date.Text = "Date";
            this.ck_c_date.UseVisualStyleBackColor = true;
            this.ck_c_date.CheckedChanged += new System.EventHandler(this.ck_c_CheckedChanged);
            //
            // cboAttributes
            //
            this.cboAttributes.FormattingEnabled = true;
            this.cboAttributes.Location = new System.Drawing.Point(3, 26);
            this.cboAttributes.Name = "cboAttributes";
            this.cboAttributes.Size = new System.Drawing.Size(151, 21);
            this.cboAttributes.TabIndex = 533;
            //
            // label14
            //
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label14.Location = new System.Drawing.Point(57, 18);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(102, 17);
            this.label14.TabIndex = 115;
            this.label14.Text = "Database Files";
            //
            // pictureBox1
            //
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(14, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 36);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            //
            // tbDbFiles
            //
            this.tbDbFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDbFiles.Controls.Add(this.tbProperties);
            this.tbDbFiles.Controls.Add(this.tbVersion);
            this.tbDbFiles.Controls.Add(this.tbHistoric);
            this.tbDbFiles.Controls.Add(this.tbPreview);
            this.tbDbFiles.ItemSize = new System.Drawing.Size(59, 18);
            this.tbDbFiles.Location = new System.Drawing.Point(3, 3);
            this.tbDbFiles.Name = "tbDbFiles";
            this.tbDbFiles.SelectedIndex = 0;
            this.tbDbFiles.Size = new System.Drawing.Size(519, 0);
            this.tbDbFiles.TabIndex = 79;
            this.tbDbFiles.SelectedIndexChanged += new System.EventHandler(this.tbDbFiles_SelectedIndexChanged);
            //
            // tbProperties
            //
            this.tbProperties.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbProperties.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbProperties.Controls.Add(this.lblBdByVal);
            this.tbProperties.Controls.Add(this.lblBdDeadlineVal);
            this.tbProperties.Controls.Add(this.lblBdBy);
            this.tbProperties.Controls.Add(this.lblBdDeadline);
            this.tbProperties.Controls.Add(this.lblBdId);
            this.tbProperties.Controls.Add(this.lblBdIdTitle);
            this.tbProperties.Controls.Add(this.lblBdCurrentVersion);
            this.tbProperties.Controls.Add(this.pictureBoxBd);
            this.tbProperties.Controls.Add(this.lblBdUpdated);
            this.tbProperties.Controls.Add(this.lblBdType);
            this.tbProperties.Controls.Add(this.lblBdSize);
            this.tbProperties.Controls.Add(this.lblBdUpdatedTitle);
            this.tbProperties.Controls.Add(this.lblBdTypeTitle);
            this.tbProperties.Controls.Add(this.lblBdSizeTitle);
            this.tbProperties.Controls.Add(this.lblBdCurrentVersionTitle);
            this.tbProperties.Controls.Add(this.panel3);
            this.tbProperties.Location = new System.Drawing.Point(4, 22);
            this.tbProperties.Name = "tbProperties";
            this.tbProperties.Padding = new System.Windows.Forms.Padding(3);
            this.tbProperties.Size = new System.Drawing.Size(511, 0);
            this.tbProperties.TabIndex = 1;
            this.tbProperties.Text = "Properties";
            //
            // lblBdByVal
            //
            this.lblBdByVal.AutoSize = true;
            this.lblBdByVal.ForeColor = System.Drawing.Color.Black;
            this.lblBdByVal.Location = new System.Drawing.Point(310, 90);
            this.lblBdByVal.Name = "lblBdByVal";
            this.lblBdByVal.Size = new System.Drawing.Size(117, 13);
            this.lblBdByVal.TabIndex = 114;
            this.lblBdByVal.Text = "WWWWWWWWWW";
            //
            // lblBdDeadlineVal
            //
            this.lblBdDeadlineVal.AutoSize = true;
            this.lblBdDeadlineVal.ForeColor = System.Drawing.Color.Black;
            this.lblBdDeadlineVal.Location = new System.Drawing.Point(103, 90);
            this.lblBdDeadlineVal.Name = "lblBdDeadlineVal";
            this.lblBdDeadlineVal.Size = new System.Drawing.Size(117, 13);
            this.lblBdDeadlineVal.TabIndex = 113;
            this.lblBdDeadlineVal.Text = "WWWWWWWWWW";
            //
            // lblBdBy
            //
            this.lblBdBy.AutoSize = true;
            this.lblBdBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBdBy.Location = new System.Drawing.Point(247, 90);
            this.lblBdBy.Name = "lblBdBy";
            this.lblBdBy.Size = new System.Drawing.Size(55, 13);
            this.lblBdBy.TabIndex = 112;
            this.lblBdBy.Text = "Reviewer:";
            this.lblBdBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblBdDeadline
            //
            this.lblBdDeadline.AutoSize = true;
            this.lblBdDeadline.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBdDeadline.Location = new System.Drawing.Point(6, 90);
            this.lblBdDeadline.Name = "lblBdDeadline";
            this.lblBdDeadline.Size = new System.Drawing.Size(91, 13);
            this.lblBdDeadline.TabIndex = 111;
            this.lblBdDeadline.Text = "Review Deadline:";
            this.lblBdDeadline.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblBdId
            //
            this.lblBdId.AutoSize = true;
            this.lblBdId.ForeColor = System.Drawing.Color.Black;
            this.lblBdId.Location = new System.Drawing.Point(103, 30);
            this.lblBdId.Name = "lblBdId";
            this.lblBdId.Size = new System.Drawing.Size(117, 13);
            this.lblBdId.TabIndex = 107;
            this.lblBdId.Text = "WWWWWWWWWW";
            //
            // lblBdIdTitle
            //
            this.lblBdIdTitle.AutoSize = true;
            this.lblBdIdTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBdIdTitle.Location = new System.Drawing.Point(78, 30);
            this.lblBdIdTitle.Name = "lblBdIdTitle";
            this.lblBdIdTitle.Size = new System.Drawing.Size(19, 13);
            this.lblBdIdTitle.TabIndex = 106;
            this.lblBdIdTitle.Text = "Id:";
            this.lblBdIdTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblBdCurrentVersion
            //
            this.lblBdCurrentVersion.AutoSize = true;
            this.lblBdCurrentVersion.ForeColor = System.Drawing.Color.Black;
            this.lblBdCurrentVersion.Location = new System.Drawing.Point(310, 49);
            this.lblBdCurrentVersion.Name = "lblBdCurrentVersion";
            this.lblBdCurrentVersion.Size = new System.Drawing.Size(117, 13);
            this.lblBdCurrentVersion.TabIndex = 105;
            this.lblBdCurrentVersion.Text = "WWWWWWWWWW";
            //
            // pictureBoxBd
            //
            this.pictureBoxBd.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxBd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxBd.Location = new System.Drawing.Point(6, 7);
            this.pictureBoxBd.Name = "pictureBoxBd";
            this.pictureBoxBd.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxBd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBd.TabIndex = 102;
            this.pictureBoxBd.TabStop = false;
            this.pictureBoxBd.Visible = false;
            //
            // lblBdUpdated
            //
            this.lblBdUpdated.AutoSize = true;
            this.lblBdUpdated.ForeColor = System.Drawing.Color.Black;
            this.lblBdUpdated.Location = new System.Drawing.Point(310, 30);
            this.lblBdUpdated.Name = "lblBdUpdated";
            this.lblBdUpdated.Size = new System.Drawing.Size(117, 13);
            this.lblBdUpdated.TabIndex = 103;
            this.lblBdUpdated.Text = "WWWWWWWWWW";
            //
            // lblBdType
            //
            this.lblBdType.AutoSize = true;
            this.lblBdType.ForeColor = System.Drawing.Color.Black;
            this.lblBdType.Location = new System.Drawing.Point(103, 70);
            this.lblBdType.Name = "lblBdType";
            this.lblBdType.Size = new System.Drawing.Size(117, 13);
            this.lblBdType.TabIndex = 99;
            this.lblBdType.Text = "WWWWWWWWWW";
            //
            // lblBdSize
            //
            this.lblBdSize.AutoSize = true;
            this.lblBdSize.ForeColor = System.Drawing.Color.Black;
            this.lblBdSize.Location = new System.Drawing.Point(103, 49);
            this.lblBdSize.Name = "lblBdSize";
            this.lblBdSize.Size = new System.Drawing.Size(117, 13);
            this.lblBdSize.TabIndex = 98;
            this.lblBdSize.Text = "WWWWWWWWWW";
            //
            // lblBdUpdatedTitle
            //
            this.lblBdUpdatedTitle.AutoSize = true;
            this.lblBdUpdatedTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBdUpdatedTitle.Location = new System.Drawing.Point(251, 30);
            this.lblBdUpdatedTitle.Name = "lblBdUpdatedTitle";
            this.lblBdUpdatedTitle.Size = new System.Drawing.Size(51, 13);
            this.lblBdUpdatedTitle.TabIndex = 94;
            this.lblBdUpdatedTitle.Text = "Updated:";
            this.lblBdUpdatedTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblBdTypeTitle
            //
            this.lblBdTypeTitle.AutoSize = true;
            this.lblBdTypeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBdTypeTitle.Location = new System.Drawing.Point(41, 70);
            this.lblBdTypeTitle.Name = "lblBdTypeTitle";
            this.lblBdTypeTitle.Size = new System.Drawing.Size(56, 13);
            this.lblBdTypeTitle.TabIndex = 93;
            this.lblBdTypeTitle.Text = "Extension:";
            this.lblBdTypeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblBdSizeTitle
            //
            this.lblBdSizeTitle.AutoSize = true;
            this.lblBdSizeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBdSizeTitle.Location = new System.Drawing.Point(67, 49);
            this.lblBdSizeTitle.Name = "lblBdSizeTitle";
            this.lblBdSizeTitle.Size = new System.Drawing.Size(30, 13);
            this.lblBdSizeTitle.TabIndex = 92;
            this.lblBdSizeTitle.Text = "Size:";
            this.lblBdSizeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblBdCurrentVersionTitle
            //
            this.lblBdCurrentVersionTitle.AutoSize = true;
            this.lblBdCurrentVersionTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBdCurrentVersionTitle.Location = new System.Drawing.Point(221, 49);
            this.lblBdCurrentVersionTitle.Name = "lblBdCurrentVersionTitle";
            this.lblBdCurrentVersionTitle.Size = new System.Drawing.Size(81, 13);
            this.lblBdCurrentVersionTitle.TabIndex = 75;
            this.lblBdCurrentVersionTitle.Text = "Current version:";
            this.lblBdCurrentVersionTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // panel3
            //
            this.panel3.BackColor = System.Drawing.Color.Gainsboro;
            this.panel3.Controls.Add(this.lblBdName);
            this.panel3.Controls.Add(this.lblSystemFile);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(503, 21);
            this.panel3.TabIndex = 104;
            //
            // lblBdName
            //
            this.lblBdName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBdName.AutoSize = true;
            this.lblBdName.ForeColor = System.Drawing.Color.Black;
            this.lblBdName.Location = new System.Drawing.Point(100, 4);
            this.lblBdName.Name = "lblBdName";
            this.lblBdName.Size = new System.Drawing.Size(392, 13);
            this.lblBdName.TabIndex = 81;
            this.lblBdName.Text = "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";
            //
            // lblSystemFile
            //
            this.lblSystemFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSystemFile.AutoSize = true;
            this.lblSystemFile.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblSystemFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSystemFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSystemFile.Location = new System.Drawing.Point(426, 2);
            this.lblSystemFile.Name = "lblSystemFile";
            this.lblSystemFile.Size = new System.Drawing.Size(72, 15);
            this.lblSystemFile.TabIndex = 121;
            this.lblSystemFile.Text = "Files: WWW";
            //
            // label9
            //
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(51, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 72;
            this.label9.Text = "Name:";
            //
            // tbVersion
            //
            this.tbVersion.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbVersion.Controls.Add(this.dtgVersions);
            this.tbVersion.Location = new System.Drawing.Point(4, 22);
            this.tbVersion.Name = "tbVersion";
            this.tbVersion.Size = new System.Drawing.Size(511, 0);
            this.tbVersion.TabIndex = 0;
            this.tbVersion.Text = "Versions";
            //
            // dtgVersions
            //
            this.dtgVersions.AllowUserToAddRows = false;
            this.dtgVersions.AllowUserToDeleteRows = false;
            this.dtgVersions.AllowUserToOrderColumns = true;
            this.dtgVersions.AllowUserToResizeRows = false;
            this.dtgVersions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgVersions.BackgroundColor = System.Drawing.SystemColors.HighlightText;
            this.dtgVersions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgVersions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dtgVersions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgVersions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.versionb,
            this.c_user,
            this.cc_date,
            this.version,
            this.c_event,
            this.iddoc,
            this.id,
            this.reason});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgVersions.DefaultCellStyle = dataGridViewCellStyle14;
            this.dtgVersions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgVersions.Location = new System.Drawing.Point(0, 0);
            this.dtgVersions.Margin = new System.Windows.Forms.Padding(0);
            this.dtgVersions.Mode = SpiderDocsForms.en_DocumentDataGridViewMode.Version;
            this.dtgVersions.MultiSelect = false;
            this.dtgVersions.Name = "dtgVersions";
            this.dtgVersions.ReadOnly = true;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgVersions.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dtgVersions.RowHeadersVisible = false;
            this.dtgVersions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgVersions.Size = new System.Drawing.Size(511, 0);
            this.dtgVersions.TabIndex = 1;
            this.dtgVersions.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgVersions_CellDoubleClick_1);
            this.dtgVersions.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtgVersions_CellMouseClick);
            //
            // versionb
            //
            this.versionb.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.versionb.DataPropertyName = "versionb";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.versionb.DefaultCellStyle = dataGridViewCellStyle12;
            this.versionb.FillWeight = 50F;
            this.versionb.HeaderText = "Version";
            this.versionb.MinimumWidth = 10;
            this.versionb.Name = "versionb";
            this.versionb.ReadOnly = true;
            this.versionb.Width = 50;
            //
            // c_user
            //
            this.c_user.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.c_user.DataPropertyName = "name";
            this.c_user.HeaderText = "User";
            this.c_user.MinimumWidth = 10;
            this.c_user.Name = "c_user";
            this.c_user.ReadOnly = true;
            //
            // cc_date
            //
            this.cc_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cc_date.DataPropertyName = "date";
            dataGridViewCellStyle13.Format = "dd/MM/yyyy hh:mm tt";
            this.cc_date.DefaultCellStyle = dataGridViewCellStyle13;
            this.cc_date.HeaderText = "Date";
            this.cc_date.MinimumWidth = 10;
            this.cc_date.Name = "cc_date";
            this.cc_date.ReadOnly = true;
            //
            // version
            //
            this.version.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.version.DataPropertyName = "version";
            this.version.HeaderText = "Version";
            this.version.MinimumWidth = 10;
            this.version.Name = "version";
            this.version.ReadOnly = true;
            this.version.Visible = false;
            //
            // c_event
            //
            this.c_event.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.c_event.DataPropertyName = "event";
            this.c_event.FillWeight = 52F;
            this.c_event.HeaderText = "Event";
            this.c_event.MinimumWidth = 10;
            this.c_event.Name = "c_event";
            this.c_event.ReadOnly = true;
            this.c_event.Width = 52;
            //
            // iddoc
            //
            this.iddoc.DataPropertyName = "id_doc";
            this.iddoc.HeaderText = "id_doc";
            this.iddoc.Name = "iddoc";
            this.iddoc.ReadOnly = true;
            this.iddoc.Visible = false;
            //
            // id
            //
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            //
            // reason
            //
            this.reason.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.reason.DataPropertyName = "reason";
            this.reason.FillWeight = 207F;
            this.reason.HeaderText = "Reason";
            this.reason.MinimumWidth = 10;
            this.reason.Name = "reason";
            this.reason.ReadOnly = true;
            //
            // tbHistoric
            //
            this.tbHistoric.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbHistoric.Controls.Add(this.dtgHist);
            this.tbHistoric.Location = new System.Drawing.Point(4, 22);
            this.tbHistoric.Name = "tbHistoric";
            this.tbHistoric.Size = new System.Drawing.Size(511, 0);
            this.tbHistoric.TabIndex = 2;
            this.tbHistoric.Text = "History";
            //
            // dtgHist
            //
            this.dtgHist.AllowUserToAddRows = false;
            this.dtgHist.AllowUserToDeleteRows = false;
            this.dtgHist.AllowUserToResizeRows = false;
            this.dtgHist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgHist.BackgroundColor = System.Drawing.SystemColors.HighlightText;
            this.dtgHist.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgHist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dtgHist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgHist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.versionFake,
            this.userr,
            this.date,
            this.vevent,
            this.versiona});
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgHist.DefaultCellStyle = dataGridViewCellStyle19;
            this.dtgHist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgHist.Location = new System.Drawing.Point(0, 0);
            this.dtgHist.MultiSelect = false;
            this.dtgHist.Name = "dtgHist";
            this.dtgHist.ReadOnly = true;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgHist.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dtgHist.RowHeadersVisible = false;
            this.dtgHist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgHist.Size = new System.Drawing.Size(511, 0);
            this.dtgHist.TabIndex = 1;
            //
            // versionFake
            //
            this.versionFake.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.versionFake.DataPropertyName = "versionb";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.versionFake.DefaultCellStyle = dataGridViewCellStyle17;
            this.versionFake.FillWeight = 50F;
            this.versionFake.HeaderText = "Version";
            this.versionFake.MinimumWidth = 10;
            this.versionFake.Name = "versionFake";
            this.versionFake.ReadOnly = true;
            this.versionFake.Width = 50;
            //
            // userr
            //
            this.userr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.userr.DataPropertyName = "name";
            this.userr.HeaderText = "User";
            this.userr.MinimumWidth = 10;
            this.userr.Name = "userr";
            this.userr.ReadOnly = true;
            //
            // date
            //
            this.date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.date.DataPropertyName = "date";
            dataGridViewCellStyle18.Format = "dd/MM/yyyy hh:mm tt";
            dataGridViewCellStyle18.NullValue = null;
            this.date.DefaultCellStyle = dataGridViewCellStyle18;
            this.date.HeaderText = "Date";
            this.date.MinimumWidth = 10;
            this.date.Name = "date";
            this.date.ReadOnly = true;
            //
            // vevent
            //
            this.vevent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.vevent.DataPropertyName = "event";
            this.vevent.FillWeight = 259F;
            this.vevent.HeaderText = "Event";
            this.vevent.MinimumWidth = 10;
            this.vevent.Name = "vevent";
            this.vevent.ReadOnly = true;
            //
            // versiona
            //
            this.versiona.DataPropertyName = "version";
            this.versiona.HeaderText = "Version1";
            this.versiona.Name = "versiona";
            this.versiona.ReadOnly = true;
            this.versiona.Visible = false;
            //
            // tbPreview
            //
            this.tbPreview.Controls.Add(this.BsrPreview);
            this.tbPreview.Location = new System.Drawing.Point(4, 22);
            this.tbPreview.Name = "tbPreview";
            this.tbPreview.Padding = new System.Windows.Forms.Padding(3);
            this.tbPreview.Size = new System.Drawing.Size(511, 0);
            this.tbPreview.TabIndex = 3;
            this.tbPreview.Text = "Preview";
            this.tbPreview.UseVisualStyleBackColor = true;
            //
            // BsrPreview
            //
            this.BsrPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BsrPreview.Location = new System.Drawing.Point(4, 4);
            this.BsrPreview.Name = "BsrPreview";
            this.BsrPreview.Size = new System.Drawing.Size(504, 0);
            this.BsrPreview.TabIndex = 0;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            //
            // splitContainer3
            //
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            //
            // splitContainer3.Panel1
            //
            this.splitContainer3.Panel1.Controls.Add(this.btnRefreshWorkSpace);
            this.splitContainer3.Panel1.Controls.Add(this.label8);
            this.splitContainer3.Panel1.Controls.Add(this.pictureBox3);
            this.splitContainer3.Panel1.Controls.Add(this.dtgLocalFile);
            this.splitContainer3.Panel1MinSize = 120;
            //
            // splitContainer3.Panel2
            //
            this.splitContainer3.Panel2.Controls.Add(this.tbLocalFiles);
            this.splitContainer3.Panel2MinSize = 128;
            this.splitContainer3.Size = new System.Drawing.Size(190, 681);
            this.splitContainer3.SplitterDistance = 502;
            this.splitContainer3.SplitterWidth = 8;
            this.splitContainer3.TabIndex = 81;
            this.splitContainer3.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer3_Paint);
            //
            // btnRefreshWorkSpace
            //
            this.btnRefreshWorkSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshWorkSpace.AutoSize = true;
            this.btnRefreshWorkSpace.Location = new System.Drawing.Point(110, 15);
            this.btnRefreshWorkSpace.Name = "btnRefreshWorkSpace";
            this.btnRefreshWorkSpace.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshWorkSpace.TabIndex = 120;
            this.btnRefreshWorkSpace.Text = "Refresh";
            this.btnRefreshWorkSpace.UseVisualStyleBackColor = true;
            this.btnRefreshWorkSpace.Click += new System.EventHandler(this.btnRefreshWorkSpace_Click);
            //
            // label8
            //
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(58, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 17);
            this.label8.TabIndex = 114;
            this.label8.Text = "Work Space";
            //
            // pictureBox3
            //
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(17, 6);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(35, 35);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            //
            // dtgLocalFile
            //
            this.dtgLocalFile.AllowUserToAddRows = false;
            this.dtgLocalFile.AllowUserToDeleteRows = false;
            this.dtgLocalFile.AllowUserToOrderColumns = true;
            this.dtgLocalFile.AllowUserToResizeRows = false;
            this.dtgLocalFile.EditMode =DataGridViewEditMode.EditProgrammatically;
            this.dtgLocalFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgLocalFile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgLocalFile.BackgroundColor = System.Drawing.Color.White;
            this.dtgLocalFile.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dtgLocalFile.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgLocalFile.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this.dtgLocalFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgLocalFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dtgLocalFile_Icon,
            this.dtgLocalFile_Status,
            this.dtgLocalFile_Id,
            this.dtgLocalFile_mail_in_out_prefix,
            this.dtgLocalFile_Title,
            this.dtgLocalFile_mail_subject,
            this.dtgLocalFile_mail_from,
            this.dtgLocalFile_mail_to,
            this.dtgLocalFile_mail_time,
            this.dtgLocalFile_mail_is_composed,
            this.dtgLocalFile_Size,
            this.dtgLocalFile_Ext,
            this.dtgLocalFile_Date,
            this.dtgLocalFile_Path,
            this.dtgLocalFile_Numver});
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgLocalFile.DefaultCellStyle = dataGridViewCellStyle25;
            this.dtgLocalFile.GridColor = System.Drawing.Color.Gainsboro;
            this.dtgLocalFile.Location = new System.Drawing.Point(3, 45);
            this.dtgLocalFile.Mode = SpiderDocsForms.en_DocumentDataGridViewMode.Local;
            this.dtgLocalFile.Name = "dtgLocalFile";
            this.dtgLocalFile.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.Color.Beige;
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgLocalFile.RowHeadersDefaultCellStyle = dataGridViewCellStyle26;
            this.dtgLocalFile.RowHeadersVisible = false;
            this.dtgLocalFile.RowHeadersWidth = 20;
            dataGridViewCellStyle27.BackColor = System.Drawing.Color.White;
            this.dtgLocalFile.RowsDefaultCellStyle = dataGridViewCellStyle27;
            this.dtgLocalFile.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgLocalFile.Size = new System.Drawing.Size(182, 454);
            this.dtgLocalFile.TabIndex = 9;
            this.dtgLocalFile.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgLocalFile_CellDoubleClick);
            this.dtgLocalFile.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtgLocalFile_CellMouseClick);
            this.dtgLocalFile.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dtgLocalFile_CellValidating);
            this.dtgLocalFile.SelectionChanged += new System.EventHandler(this.dtgLocalFile_SelectionChanged);
            this.dtgLocalFile.Click += new System.EventHandler(this.dtgLocalFile_Click);
            this.dtgLocalFile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtgLocalFile_KeyDown);
            this.dtgLocalFile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtgLocalFile_KeyPress);
            this.dtgLocalFile.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dtgLocalFile_MouseClick);
            //
            // dtgLocalFile_Icon
            //
            this.dtgLocalFile_Icon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dtgLocalFile_Icon.HeaderText = "";
            this.dtgLocalFile_Icon.MinimumWidth = 22;
            this.dtgLocalFile_Icon.Name = "dtgLocalFile_Icon";
            this.dtgLocalFile_Icon.ReadOnly = true;
            this.dtgLocalFile_Icon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgLocalFile_Icon.Width = 22;
            //
            // dtgLocalFile_Status
            //
            this.dtgLocalFile_Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dtgLocalFile_Status.HeaderText = "";
            this.dtgLocalFile_Status.MinimumWidth = 22;
            this.dtgLocalFile_Status.Name = "dtgLocalFile_Status";
            this.dtgLocalFile_Status.ReadOnly = true;
            this.dtgLocalFile_Status.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgLocalFile_Status.Width = 22;
            //
            // dtgLocalFile_Id
            //
            this.dtgLocalFile_Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dtgLocalFile_Id.HeaderText = "Id";
            this.dtgLocalFile_Id.MinimumWidth = 42;
            this.dtgLocalFile_Id.Name = "dtgLocalFile_Id";
            this.dtgLocalFile_Id.ReadOnly = true;
            this.dtgLocalFile_Id.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgLocalFile_Id.Width = 42;
            //
            // dtgLocalFile_mail_in_out_prefix
            //
            this.dtgLocalFile_mail_in_out_prefix.HeaderText = "";
            this.dtgLocalFile_mail_in_out_prefix.Name = "dtgLocalFile_mail_in_out_prefix";
            this.dtgLocalFile_mail_in_out_prefix.ReadOnly = true;
            this.dtgLocalFile_mail_in_out_prefix.Visible = false;
            //
            // dtgLocalFile_Title
            //
            this.dtgLocalFile_Title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dtgLocalFile_Title.FillWeight = 124F;
            this.dtgLocalFile_Title.HeaderText = "Title";
            this.dtgLocalFile_Title.MinimumWidth = 10;
            this.dtgLocalFile_Title.Name = "dtgLocalFile_Title";
            this.dtgLocalFile_Title.ReadOnly = true;
            this.dtgLocalFile_Title.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgLocalFile_Title.Width = 116;
            //
            // dtgLocalFile_mail_subject
            //
            this.dtgLocalFile_mail_subject.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dtgLocalFile_mail_subject.FillWeight = 124F;
            this.dtgLocalFile_mail_subject.HeaderText = "Subject";
            this.dtgLocalFile_mail_subject.MinimumWidth = 10;
            this.dtgLocalFile_mail_subject.Name = "dtgLocalFile_mail_subject";
            this.dtgLocalFile_mail_subject.ReadOnly = true;
            this.dtgLocalFile_mail_subject.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgLocalFile_mail_subject.Visible = false;
            this.dtgLocalFile_mail_subject.Width = 84;
            //
            // dtgLocalFile_mail_from
            //
            this.dtgLocalFile_mail_from.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle22.Format = "dd/MM/yyyy hh:mm tt";
            this.dtgLocalFile_mail_from.DefaultCellStyle = dataGridViewCellStyle22;
            this.dtgLocalFile_mail_from.FillWeight = 120F;
            this.dtgLocalFile_mail_from.HeaderText = "From";
            this.dtgLocalFile_mail_from.MinimumWidth = 10;
            this.dtgLocalFile_mail_from.Name = "dtgLocalFile_mail_from";
            this.dtgLocalFile_mail_from.ReadOnly = true;
            this.dtgLocalFile_mail_from.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgLocalFile_mail_from.Visible = false;
            //
            // dtgLocalFile_mail_to
            //
            this.dtgLocalFile_mail_to.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dtgLocalFile_mail_to.FillWeight = 124F;
            this.dtgLocalFile_mail_to.HeaderText = "To";
            this.dtgLocalFile_mail_to.MinimumWidth = 10;
            this.dtgLocalFile_mail_to.Name = "dtgLocalFile_mail_to";
            this.dtgLocalFile_mail_to.ReadOnly = true;
            this.dtgLocalFile_mail_to.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgLocalFile_mail_to.Visible = false;
            this.dtgLocalFile_mail_to.Width = 70;
            //
            // dtgLocalFile_mail_time
            //
            this.dtgLocalFile_mail_time.HeaderText = "Time";
            this.dtgLocalFile_mail_time.Name = "dtgLocalFile_mail_time";
            this.dtgLocalFile_mail_time.ReadOnly = true;
            this.dtgLocalFile_mail_time.Visible = false;
            //
            // dtgLocalFile_mail_is_composed
            //
            this.dtgLocalFile_mail_is_composed.HeaderText = "IsComposed";
            this.dtgLocalFile_mail_is_composed.Name = "dtgLocalFile_mail_is_composed";
            this.dtgLocalFile_mail_is_composed.ReadOnly = true;
            this.dtgLocalFile_mail_is_composed.Visible = false;
            //
            // dtgLocalFile_Size
            //
            this.dtgLocalFile_Size.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle23.Format = "# KB";
            dataGridViewCellStyle23.NullValue = null;
            this.dtgLocalFile_Size.DefaultCellStyle = dataGridViewCellStyle23;
            this.dtgLocalFile_Size.FillWeight = 65F;
            this.dtgLocalFile_Size.HeaderText = "Size";
            this.dtgLocalFile_Size.MinimumWidth = 10;
            this.dtgLocalFile_Size.Name = "dtgLocalFile_Size";
            this.dtgLocalFile_Size.ReadOnly = true;
            this.dtgLocalFile_Size.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgLocalFile_Size.Width = 65;
            //
            // dtgLocalFile_Ext
            //
            this.dtgLocalFile_Ext.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dtgLocalFile_Ext.FillWeight = 60F;
            this.dtgLocalFile_Ext.HeaderText = "Extension";
            this.dtgLocalFile_Ext.MinimumWidth = 10;
            this.dtgLocalFile_Ext.Name = "dtgLocalFile_Ext";
            this.dtgLocalFile_Ext.ReadOnly = true;
            this.dtgLocalFile_Ext.Visible = false;
            this.dtgLocalFile_Ext.Width = 60;
            //
            // dtgLocalFile_Date
            //
            this.dtgLocalFile_Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle24.Format = "dd/MM/yyyy hh:mm tt";
            this.dtgLocalFile_Date.DefaultCellStyle = dataGridViewCellStyle24;
            this.dtgLocalFile_Date.FillWeight = 120F;
            this.dtgLocalFile_Date.HeaderText = "Created";
            this.dtgLocalFile_Date.MinimumWidth = 10;
            this.dtgLocalFile_Date.Name = "dtgLocalFile_Date";
            this.dtgLocalFile_Date.ReadOnly = true;
            //
            // dtgLocalFile_Path
            //
            this.dtgLocalFile_Path.HeaderText = "path";
            this.dtgLocalFile_Path.Name = "dtgLocalFile_Path";
            this.dtgLocalFile_Path.ReadOnly = true;
            this.dtgLocalFile_Path.Visible = false;
            //
            // dtgLocalFile_Numver
            //
            this.dtgLocalFile_Numver.HeaderText = "num_version";
            this.dtgLocalFile_Numver.Name = "dtgLocalFile_Numver";
            this.dtgLocalFile_Numver.ReadOnly = true;
            this.dtgLocalFile_Numver.Visible = false;
            //
            // tbLocalFiles
            //
            this.tbLocalFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLocalFiles.Controls.Add(this.tbDetailLocalFiles);
            this.tbLocalFiles.Location = new System.Drawing.Point(3, 3);
            this.tbLocalFiles.Name = "tbLocalFiles";
            this.tbLocalFiles.SelectedIndex = 0;
            this.tbLocalFiles.Size = new System.Drawing.Size(182, 0);
            this.tbLocalFiles.TabIndex = 80;
            this.tbLocalFiles.SelectedIndexChanged += new System.EventHandler(this.tbLocalFiles_SelectedIndexChanged);
            //
            // tbDetailLocalFiles
            //
            this.tbDetailLocalFiles.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbDetailLocalFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDetailLocalFiles.Controls.Add(this.lblLocalId);
            this.tbDetailLocalFiles.Controls.Add(this.lblLocalIdTitle);
            this.tbDetailLocalFiles.Controls.Add(this.lblStatus);
            this.tbDetailLocalFiles.Controls.Add(this.lblSizeTitle);
            this.tbDetailLocalFiles.Controls.Add(this.pictureBoxLocal);
            this.tbDetailLocalFiles.Controls.Add(this.panel2);
            this.tbDetailLocalFiles.Controls.Add(this.lblModifield);
            this.tbDetailLocalFiles.Controls.Add(this.lblCreated);
            this.tbDetailLocalFiles.Controls.Add(this.lblType);
            this.tbDetailLocalFiles.Controls.Add(this.lblSize);
            this.tbDetailLocalFiles.Controls.Add(this.lblCreatedTitle);
            this.tbDetailLocalFiles.Controls.Add(this.lblModifieldTitle);
            this.tbDetailLocalFiles.Controls.Add(this.lblTypeTitle);
            this.tbDetailLocalFiles.Controls.Add(this.lblStatusTitle);
            this.tbDetailLocalFiles.Location = new System.Drawing.Point(4, 22);
            this.tbDetailLocalFiles.Name = "tbDetailLocalFiles";
            this.tbDetailLocalFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tbDetailLocalFiles.Size = new System.Drawing.Size(174, 0);
            this.tbDetailLocalFiles.TabIndex = 1;
            this.tbDetailLocalFiles.Text = "Properties";
            //
            // lblLocalId
            //
            this.lblLocalId.AutoSize = true;
            this.lblLocalId.ForeColor = System.Drawing.Color.Black;
            this.lblLocalId.Location = new System.Drawing.Point(103, 29);
            this.lblLocalId.Name = "lblLocalId";
            this.lblLocalId.Size = new System.Drawing.Size(117, 13);
            this.lblLocalId.TabIndex = 109;
            this.lblLocalId.Text = "WWWWWWWWWW";
            //
            // lblLocalIdTitle
            //
            this.lblLocalIdTitle.AutoSize = true;
            this.lblLocalIdTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalIdTitle.Location = new System.Drawing.Point(80, 29);
            this.lblLocalIdTitle.Name = "lblLocalIdTitle";
            this.lblLocalIdTitle.Size = new System.Drawing.Size(19, 13);
            this.lblLocalIdTitle.TabIndex = 108;
            this.lblLocalIdTitle.Text = "Id:";
            this.lblLocalIdTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblStatus
            //
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.Black;
            this.lblStatus.Location = new System.Drawing.Point(297, 70);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(117, 13);
            this.lblStatus.TabIndex = 93;
            this.lblStatus.Text = "WWWWWWWWWW";
            //
            // lblSizeTitle
            //
            this.lblSizeTitle.AutoSize = true;
            this.lblSizeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSizeTitle.Location = new System.Drawing.Point(68, 49);
            this.lblSizeTitle.Name = "lblSizeTitle";
            this.lblSizeTitle.Size = new System.Drawing.Size(30, 13);
            this.lblSizeTitle.TabIndex = 92;
            this.lblSizeTitle.Text = "Size:";
            this.lblSizeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // pictureBoxLocal
            //
            this.pictureBoxLocal.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxLocal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxLocal.Location = new System.Drawing.Point(7, 8);
            this.pictureBoxLocal.Name = "pictureBoxLocal";
            this.pictureBoxLocal.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxLocal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLocal.TabIndex = 89;
            this.pictureBoxLocal.TabStop = false;
            this.pictureBoxLocal.Visible = false;
            //
            // panel2
            //
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.Controls.Add(this.lblName);
            this.panel2.Controls.Add(this.lblLocalFile);
            this.panel2.Controls.Add(this.label36);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(166, 21);
            this.panel2.TabIndex = 91;
            //
            // lblName
            //
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.AutoSize = true;
            this.lblName.ForeColor = System.Drawing.Color.Black;
            this.lblName.Location = new System.Drawing.Point(101, 4);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(293, 13);
            this.lblName.TabIndex = 81;
            this.lblName.Text = "WWWWWWWWWWWWWWWWWWWWWWWWWW";
            //
            // lblLocalFile
            //
            this.lblLocalFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLocalFile.AutoSize = true;
            this.lblLocalFile.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblLocalFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLocalFile.Location = new System.Drawing.Point(89, 2);
            this.lblLocalFile.Name = "lblLocalFile";
            this.lblLocalFile.Size = new System.Drawing.Size(72, 15);
            this.lblLocalFile.TabIndex = 81;
            this.lblLocalFile.Text = "Files: WWW";
            //
            // label36
            //
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(52, 4);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(43, 13);
            this.label36.TabIndex = 72;
            this.label36.Text = "Name:";
            //
            // lblModifield
            //
            this.lblModifield.AutoSize = true;
            this.lblModifield.ForeColor = System.Drawing.Color.Black;
            this.lblModifield.Location = new System.Drawing.Point(297, 49);
            this.lblModifield.Name = "lblModifield";
            this.lblModifield.Size = new System.Drawing.Size(117, 13);
            this.lblModifield.TabIndex = 90;
            this.lblModifield.Text = "WWWWWWWWWW";
            //
            // lblCreated
            //
            this.lblCreated.AutoSize = true;
            this.lblCreated.ForeColor = System.Drawing.Color.Black;
            this.lblCreated.Location = new System.Drawing.Point(297, 29);
            this.lblCreated.Name = "lblCreated";
            this.lblCreated.Size = new System.Drawing.Size(117, 13);
            this.lblCreated.TabIndex = 87;
            this.lblCreated.Text = "WWWWWWWWWW";
            //
            // lblType
            //
            this.lblType.AutoSize = true;
            this.lblType.ForeColor = System.Drawing.Color.Black;
            this.lblType.Location = new System.Drawing.Point(103, 70);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(117, 13);
            this.lblType.TabIndex = 83;
            this.lblType.Text = "WWWWWWWWWW";
            //
            // lblSize
            //
            this.lblSize.AutoSize = true;
            this.lblSize.ForeColor = System.Drawing.Color.Black;
            this.lblSize.Location = new System.Drawing.Point(103, 49);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(117, 13);
            this.lblSize.TabIndex = 82;
            this.lblSize.Text = "WWWWWWWWWW";
            //
            // lblCreatedTitle
            //
            this.lblCreatedTitle.AutoSize = true;
            this.lblCreatedTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedTitle.Location = new System.Drawing.Point(246, 29);
            this.lblCreatedTitle.Name = "lblCreatedTitle";
            this.lblCreatedTitle.Size = new System.Drawing.Size(47, 13);
            this.lblCreatedTitle.TabIndex = 78;
            this.lblCreatedTitle.Text = "Created:";
            this.lblCreatedTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblModifieldTitle
            //
            this.lblModifieldTitle.AutoSize = true;
            this.lblModifieldTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModifieldTitle.Location = new System.Drawing.Point(242, 49);
            this.lblModifieldTitle.Name = "lblModifieldTitle";
            this.lblModifieldTitle.Size = new System.Drawing.Size(50, 13);
            this.lblModifieldTitle.TabIndex = 77;
            this.lblModifieldTitle.Text = "Modified:";
            this.lblModifieldTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblTypeTitle
            //
            this.lblTypeTitle.AutoSize = true;
            this.lblTypeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTypeTitle.Location = new System.Drawing.Point(43, 70);
            this.lblTypeTitle.Name = "lblTypeTitle";
            this.lblTypeTitle.Size = new System.Drawing.Size(56, 13);
            this.lblTypeTitle.TabIndex = 74;
            this.lblTypeTitle.Text = "Extension:";
            this.lblTypeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblStatusTitle
            //
            this.lblStatusTitle.AutoSize = true;
            this.lblStatusTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusTitle.Location = new System.Drawing.Point(253, 70);
            this.lblStatusTitle.Name = "lblStatusTitle";
            this.lblStatusTitle.Size = new System.Drawing.Size(40, 13);
            this.lblStatusTitle.TabIndex = 73;
            this.lblStatusTitle.Text = "Status:";
            this.lblStatusTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // dataGridViewTextBoxColumn1
            //
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle28;
            this.dataGridViewTextBoxColumn1.FillWeight = 50F;
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
            this.dataGridViewTextBoxColumn2.DataPropertyName = "title";
            this.dataGridViewTextBoxColumn2.FillWeight = 30F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Name";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 80;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            //
            // dataGridViewTextBoxColumn3
            //
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "document_folder";
            dataGridViewCellStyle29.Format = "dd/MM/yyyy hh:mm tt";
            dataGridViewCellStyle29.NullValue = null;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle29;
            this.dataGridViewTextBoxColumn3.FillWeight = 20F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Folder";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 90;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 90;
            //
            // dataGridViewTextBoxColumn4
            //
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "type";
            this.dataGridViewTextBoxColumn4.FillWeight = 1F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Created";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 120;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 120;
            //
            // dataGridViewTextBoxColumn5
            //
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn5.FillWeight = 10F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Author";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 80;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Visible = false;
            this.dataGridViewTextBoxColumn5.Width = 80;
            //
            // dataGridViewImageColumn1
            //
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.MinimumWidth = 22;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn1.Width = 22;
            //
            // dataGridViewTextBoxColumn6
            //
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "version";
            this.dataGridViewTextBoxColumn6.FillWeight = 10F;
            this.dataGridViewTextBoxColumn6.HeaderText = "origName";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Visible = false;
            this.dataGridViewTextBoxColumn6.Width = 50;
            //
            // dataGridViewTextBoxColumn7
            //
            this.dataGridViewTextBoxColumn7.DataPropertyName = "id_version";
            this.dataGridViewTextBoxColumn7.HeaderText = "id_doc";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Visible = false;
            //
            // dataGridViewTextBoxColumn8
            //
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn8.DataPropertyName = "date";
            dataGridViewCellStyle30.Format = "dd/MM/yyyy hh:mm tt";
            dataGridViewCellStyle30.NullValue = null;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle30;
            this.dataGridViewTextBoxColumn8.FillWeight = 10F;
            this.dataGridViewTextBoxColumn8.HeaderText = "num_version";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 110;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Visible = false;
            this.dataGridViewTextBoxColumn8.Width = 110;
            //
            // dataGridViewTextBoxColumn9
            //
            this.dataGridViewTextBoxColumn9.DataPropertyName = "atb_value";
            this.dataGridViewTextBoxColumn9.HeaderText = "atb";
            this.dataGridViewTextBoxColumn9.MinimumWidth = 80;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Visible = false;
            //
            // dataGridViewTextBoxColumn10
            //
            this.dataGridViewTextBoxColumn10.DataPropertyName = "id_user";
            this.dataGridViewTextBoxColumn10.HeaderText = "id_user";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Visible = false;
            //
            // dataGridViewTextBoxColumn11
            //
            this.dataGridViewTextBoxColumn11.DataPropertyName = "id_folder";
            this.dataGridViewTextBoxColumn11.HeaderText = "id_folder";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Visible = false;
            //
            // dataGridViewTextBoxColumn12
            //
            this.dataGridViewTextBoxColumn12.DataPropertyName = "id_type";
            this.dataGridViewTextBoxColumn12.HeaderText = "id_type";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Visible = false;
            //
            // dataGridViewTextBoxColumn13
            //
            this.dataGridViewTextBoxColumn13.DataPropertyName = "id_status";
            this.dataGridViewTextBoxColumn13.HeaderText = "id_status";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.Visible = false;
            //
            // dataGridViewTextBoxColumn14
            //
            this.dataGridViewTextBoxColumn14.DataPropertyName = "extension";
            this.dataGridViewTextBoxColumn14.HeaderText = "extension";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.Visible = false;
            //
            // dataGridViewTextBoxColumn15
            //
            this.dataGridViewTextBoxColumn15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn15.DataPropertyName = "versionb";
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn15.DefaultCellStyle = dataGridViewCellStyle31;
            this.dataGridViewTextBoxColumn15.HeaderText = "Version";
            this.dataGridViewTextBoxColumn15.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            this.dataGridViewTextBoxColumn15.Visible = false;
            this.dataGridViewTextBoxColumn15.Width = 50;
            //
            // dataGridViewTextBoxColumn16
            //
            this.dataGridViewTextBoxColumn16.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn16.DataPropertyName = "name";
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn16.DefaultCellStyle = dataGridViewCellStyle32;
            this.dataGridViewTextBoxColumn16.HeaderText = "User";
            this.dataGridViewTextBoxColumn16.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.Visible = false;
            //
            // dataGridViewTextBoxColumn17
            //
            this.dataGridViewTextBoxColumn17.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn17.DataPropertyName = "date";
            dataGridViewCellStyle33.Format = "dd/MM/yyyy hh:mm tt";
            this.dataGridViewTextBoxColumn17.DefaultCellStyle = dataGridViewCellStyle33;
            this.dataGridViewTextBoxColumn17.HeaderText = "Date";
            this.dataGridViewTextBoxColumn17.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.ReadOnly = true;
            this.dataGridViewTextBoxColumn17.Visible = false;
            //
            // dataGridViewTextBoxColumn18
            //
            this.dataGridViewTextBoxColumn18.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn18.DataPropertyName = "version";
            this.dataGridViewTextBoxColumn18.HeaderText = "Version";
            this.dataGridViewTextBoxColumn18.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.ReadOnly = true;
            this.dataGridViewTextBoxColumn18.Visible = false;
            this.dataGridViewTextBoxColumn18.Width = 50;
            //
            // dataGridViewTextBoxColumn19
            //
            this.dataGridViewTextBoxColumn19.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn19.DataPropertyName = "event";
            this.dataGridViewTextBoxColumn19.FillWeight = 20F;
            this.dataGridViewTextBoxColumn19.HeaderText = "Event";
            this.dataGridViewTextBoxColumn19.MinimumWidth = 40;
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            this.dataGridViewTextBoxColumn19.Visible = false;
            this.dataGridViewTextBoxColumn19.Width = 77;
            //
            // dataGridViewTextBoxColumn20
            //
            this.dataGridViewTextBoxColumn20.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn20.DataPropertyName = "id_doc";
            this.dataGridViewTextBoxColumn20.FillWeight = 20F;
            this.dataGridViewTextBoxColumn20.HeaderText = "id_doc";
            this.dataGridViewTextBoxColumn20.MinimumWidth = 40;
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.ReadOnly = true;
            this.dataGridViewTextBoxColumn20.Visible = false;
            this.dataGridViewTextBoxColumn20.Width = 77;
            //
            // dataGridViewTextBoxColumn21
            //
            this.dataGridViewTextBoxColumn21.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn21.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn21.FillWeight = 20F;
            this.dataGridViewTextBoxColumn21.HeaderText = "id";
            this.dataGridViewTextBoxColumn21.MinimumWidth = 40;
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.ReadOnly = true;
            this.dataGridViewTextBoxColumn21.Visible = false;
            this.dataGridViewTextBoxColumn21.Width = 52;
            //
            // dataGridViewTextBoxColumn22
            //
            this.dataGridViewTextBoxColumn22.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn22.DataPropertyName = "reason";
            this.dataGridViewTextBoxColumn22.FillWeight = 80F;
            this.dataGridViewTextBoxColumn22.HeaderText = "Reason";
            this.dataGridViewTextBoxColumn22.MinimumWidth = 60;
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.ReadOnly = true;
            this.dataGridViewTextBoxColumn22.Visible = false;
            //
            // dataGridViewTextBoxColumn23
            //
            this.dataGridViewTextBoxColumn23.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn23.DataPropertyName = "versionb";
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn23.DefaultCellStyle = dataGridViewCellStyle34;
            this.dataGridViewTextBoxColumn23.FillWeight = 80F;
            this.dataGridViewTextBoxColumn23.HeaderText = "Version";
            this.dataGridViewTextBoxColumn23.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.ReadOnly = true;
            this.dataGridViewTextBoxColumn23.Visible = false;
            this.dataGridViewTextBoxColumn23.Width = 50;
            //
            // dataGridViewTextBoxColumn24
            //
            this.dataGridViewTextBoxColumn24.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn24.DataPropertyName = "name";
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn24.DefaultCellStyle = dataGridViewCellStyle35;
            this.dataGridViewTextBoxColumn24.FillWeight = 80F;
            this.dataGridViewTextBoxColumn24.HeaderText = "User";
            this.dataGridViewTextBoxColumn24.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.ReadOnly = true;
            this.dataGridViewTextBoxColumn24.Visible = false;
            this.dataGridViewTextBoxColumn24.Width = 306;
            //
            // dataGridViewTextBoxColumn25
            //
            this.dataGridViewTextBoxColumn25.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn25.DataPropertyName = "date";
            dataGridViewCellStyle36.Format = "dd/MM/yyyy hh:mm tt";
            this.dataGridViewTextBoxColumn25.DefaultCellStyle = dataGridViewCellStyle36;
            this.dataGridViewTextBoxColumn25.FillWeight = 80F;
            this.dataGridViewTextBoxColumn25.HeaderText = "Date";
            this.dataGridViewTextBoxColumn25.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.ReadOnly = true;
            this.dataGridViewTextBoxColumn25.Visible = false;
            this.dataGridViewTextBoxColumn25.Width = 207;
            //
            // dataGridViewTextBoxColumn26
            //
            this.dataGridViewTextBoxColumn26.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn26.DataPropertyName = "event";
            this.dataGridViewTextBoxColumn26.FillWeight = 207F;
            this.dataGridViewTextBoxColumn26.HeaderText = "Event";
            this.dataGridViewTextBoxColumn26.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.ReadOnly = true;
            //
            // dataGridViewTextBoxColumn27
            //
            this.dataGridViewTextBoxColumn27.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn27.DataPropertyName = "version";
            this.dataGridViewTextBoxColumn27.FillWeight = 50F;
            this.dataGridViewTextBoxColumn27.HeaderText = "Version1";
            this.dataGridViewTextBoxColumn27.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.dataGridViewTextBoxColumn27.ReadOnly = true;
            this.dataGridViewTextBoxColumn27.Visible = false;
            this.dataGridViewTextBoxColumn27.Width = 265;
            //
            // dataGridViewTextBoxColumn28
            //
            this.dataGridViewTextBoxColumn28.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn28.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn28.HeaderText = "Title";
            this.dataGridViewTextBoxColumn28.MinimumWidth = 80;
            this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            this.dataGridViewTextBoxColumn28.ReadOnly = true;
            this.dataGridViewTextBoxColumn28.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn28.Visible = false;
            this.dataGridViewTextBoxColumn28.Width = 341;
            //
            // dataGridViewTextBoxColumn29
            //
            this.dataGridViewTextBoxColumn29.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn29.DataPropertyName = "title";
            this.dataGridViewTextBoxColumn29.FillWeight = 1F;
            this.dataGridViewTextBoxColumn29.HeaderText = "Size";
            this.dataGridViewTextBoxColumn29.MinimumWidth = 65;
            this.dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
            this.dataGridViewTextBoxColumn29.ReadOnly = true;
            this.dataGridViewTextBoxColumn29.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn29.Visible = false;
            this.dataGridViewTextBoxColumn29.Width = 65;
            //
            // dataGridViewTextBoxColumn30
            //
            this.dataGridViewTextBoxColumn30.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn30.DataPropertyName = "document_folder";
            this.dataGridViewTextBoxColumn30.FillWeight = 1F;
            this.dataGridViewTextBoxColumn30.HeaderText = "Extension";
            this.dataGridViewTextBoxColumn30.MinimumWidth = 60;
            this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
            this.dataGridViewTextBoxColumn30.ReadOnly = true;
            this.dataGridViewTextBoxColumn30.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn30.Visible = false;
            this.dataGridViewTextBoxColumn30.Width = 60;
            //
            // dataGridViewTextBoxColumn31
            //
            this.dataGridViewTextBoxColumn31.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn31.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn31.FillWeight = 1F;
            this.dataGridViewTextBoxColumn31.HeaderText = "Created";
            this.dataGridViewTextBoxColumn31.MinimumWidth = 120;
            this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
            this.dataGridViewTextBoxColumn31.ReadOnly = true;
            this.dataGridViewTextBoxColumn31.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn31.Visible = false;
            this.dataGridViewTextBoxColumn31.Width = 120;
            //
            // dataGridViewImageColumn2
            //
            this.dataGridViewImageColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.MinimumWidth = 22;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.ReadOnly = true;
            this.dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn2.Width = 22;
            //
            // dataGridViewImageColumn3
            //
            this.dataGridViewImageColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewImageColumn3.HeaderText = "";
            this.dataGridViewImageColumn3.MinimumWidth = 22;
            this.dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
            this.dataGridViewImageColumn3.ReadOnly = true;
            this.dataGridViewImageColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn3.Width = 22;
            //
            // dataGridViewTextBoxColumn32
            //
            this.dataGridViewTextBoxColumn32.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn32.DataPropertyName = "document_folder";
            this.dataGridViewTextBoxColumn32.FillWeight = 20F;
            this.dataGridViewTextBoxColumn32.HeaderText = "path";
            this.dataGridViewTextBoxColumn32.MinimumWidth = 60;
            this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
            this.dataGridViewTextBoxColumn32.ReadOnly = true;
            this.dataGridViewTextBoxColumn32.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn32.Visible = false;
            this.dataGridViewTextBoxColumn32.Width = 60;
            //
            // dataGridViewTextBoxColumn33
            //
            this.dataGridViewTextBoxColumn33.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn33.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn33.FillWeight = 10F;
            this.dataGridViewTextBoxColumn33.HeaderText = "origName";
            this.dataGridViewTextBoxColumn33.MinimumWidth = 120;
            this.dataGridViewTextBoxColumn33.Name = "dataGridViewTextBoxColumn33";
            this.dataGridViewTextBoxColumn33.ReadOnly = true;
            this.dataGridViewTextBoxColumn33.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn33.Visible = false;
            this.dataGridViewTextBoxColumn33.Width = 120;
            //
            // dataGridViewTextBoxColumn34
            //
            this.dataGridViewTextBoxColumn34.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn34.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn34.FillWeight = 10F;
            this.dataGridViewTextBoxColumn34.HeaderText = "id_doc";
            this.dataGridViewTextBoxColumn34.MinimumWidth = 120;
            this.dataGridViewTextBoxColumn34.Name = "dataGridViewTextBoxColumn34";
            this.dataGridViewTextBoxColumn34.ReadOnly = true;
            this.dataGridViewTextBoxColumn34.Visible = false;
            this.dataGridViewTextBoxColumn34.Width = 120;
            //
            // dataGridViewTextBoxColumn35
            //
            this.dataGridViewTextBoxColumn35.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn35.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn35.FillWeight = 10F;
            this.dataGridViewTextBoxColumn35.HeaderText = "num_version";
            this.dataGridViewTextBoxColumn35.MinimumWidth = 120;
            this.dataGridViewTextBoxColumn35.Name = "dataGridViewTextBoxColumn35";
            this.dataGridViewTextBoxColumn35.ReadOnly = true;
            this.dataGridViewTextBoxColumn35.Visible = false;
            this.dataGridViewTextBoxColumn35.Width = 120;
            //
            // dataGridViewTextBoxColumn36
            //
            this.dataGridViewTextBoxColumn36.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn36.FillWeight = 120F;
            this.dataGridViewTextBoxColumn36.HeaderText = "id_doc";
            this.dataGridViewTextBoxColumn36.MinimumWidth = 10;
            this.dataGridViewTextBoxColumn36.Name = "dataGridViewTextBoxColumn36";
            this.dataGridViewTextBoxColumn36.ReadOnly = true;
            this.dataGridViewTextBoxColumn36.Visible = false;
            //
            // dataGridViewTextBoxColumn37
            //
            this.dataGridViewTextBoxColumn37.HeaderText = "num_version";
            this.dataGridViewTextBoxColumn37.Name = "dataGridViewTextBoxColumn37";
            this.dataGridViewTextBoxColumn37.ReadOnly = true;
            this.dataGridViewTextBoxColumn37.Visible = false;
            //
            // dataGridViewTextBoxColumn38
            //
            this.dataGridViewTextBoxColumn38.HeaderText = "num_version";
            this.dataGridViewTextBoxColumn38.Name = "dataGridViewTextBoxColumn38";
            this.dataGridViewTextBoxColumn38.ReadOnly = true;
            this.dataGridViewTextBoxColumn38.Visible = false;
            //
            // dataGridViewTextBoxColumn39
            //
            this.dataGridViewTextBoxColumn39.HeaderText = "num_version";
            this.dataGridViewTextBoxColumn39.Name = "dataGridViewTextBoxColumn39";
            this.dataGridViewTextBoxColumn39.ReadOnly = true;
            this.dataGridViewTextBoxColumn39.Visible = false;
            //
            // contextMenuStripFolderViewOption2
            //
            this.contextMenuStripFolderViewOption2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.contextMenuStripFolderViewOption2.Name = "contextMenuStripTreeViewOptions";
            this.contextMenuStripFolderViewOption2.Size = new System.Drawing.Size(118, 70);
            //
            // toolStripMenuItem4
            //
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItem4.Text = "Rename";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            //
            // toolStripMenuItem5
            //
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItem5.Text = "Add";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            //
            // toolStripMenuItem6
            //
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItem6.Text = "Remove";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            //
            // frmWorkSpace
            //
            this.AllowDrop = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1441, 690);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.splitContainer);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(922, 630);
            this.Name = "frmWorkSpace";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Work Space";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.frmWorkSpace_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmWorkSpace_FormClosing);
            this.Load += new System.EventHandler(this.frmWorkSpace_Load);
            this.Shown += new System.EventHandler(this.frmWorkSpace_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmWorkSpace_KeyDown);
            this.Resize += new System.EventHandler(this.frmWorkSpace_Resize);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.menuDtgVersionFiles.ResumeLayout(false);
            this.contextMenuStripTreeViewOptions.ResumeLayout(false);
            this.menuDtgLocalFiles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCustomSearchFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLocalGridFull)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDbGridFull)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbShowBothSides)).EndInit();
            this.contextMenuStripFolderViewOption1.ResumeLayout(false);
            this.menuDtgSystemFiles.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.tabControlSearch.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panelCustomSearchFields.ResumeLayout(false);
            this.panelCustomSearchFields.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxLoadingSearch)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgBdFiles)).EndInit();
            this.flpCustomGrid.ResumeLayout(false);
            this.flpCustomGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tbDbFiles.ResumeLayout(false);
            this.tbProperties.ResumeLayout(false);
            this.tbProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBd)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tbVersion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgVersions)).EndInit();
            this.tbHistoric.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgHist)).EndInit();
            this.tbPreview.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLocalFile)).EndInit();
            this.tbLocalFiles.ResumeLayout(false);
            this.tbDetailLocalFiles.ResumeLayout(false);
            this.tbDetailLocalFiles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLocal)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.contextMenuStripFolderViewOption2.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panelLeft;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tbDbFiles;
        private System.Windows.Forms.TabPage tbProperties;
        private System.Windows.Forms.Label lblBdCurrentVersionTitle;
        private System.Windows.Forms.TabPage tbVersion;
        private System.Windows.Forms.TabPage tbHistoric;
        private System.Windows.Forms.TabControl tbLocalFiles;
		private System.Windows.Forms.TabPage tbDetailLocalFiles;
        private System.Windows.Forms.Label lblCreated;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblStatusTitle;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label lblModifield;
        private System.Windows.Forms.PictureBox pictureBoxLocal;
        private System.Windows.Forms.Label lblTypeTitle;
        private System.Windows.Forms.Label lblBdUpdated;
		private System.Windows.Forms.PictureBox pictureBoxBd;
        private System.Windows.Forms.Label lblBdType;
		private System.Windows.Forms.Label lblBdSize;
        private System.Windows.Forms.Label lblBdUpdatedTitle;
        private System.Windows.Forms.Label lblBdTypeTitle;
        private System.Windows.Forms.Label lblBdSizeTitle;

        private System.Windows.Forms.Label lblBdCurrentVersion;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblBdName;
		private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblCreatedTitle;
        private System.Windows.Forms.Label lblModifieldTitle;
		public DocumentDataGridView dtgLocalFile;
        private System.Windows.Forms.Label lblBdId;
        private System.Windows.Forms.Label lblBdIdTitle;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblSizeTitle;
        private SpiderDocsForms.FolderComboBox cboFolder;
        private SpiderCustomComponent.CheckComboBox cboExtension;
        private SpiderCustomComponent.CheckComboBox cboAuthor;
        private SpiderCustomComponent.CheckComboBox cboDocType;
        private System.Windows.Forms.ToolStripMenuItem menuLocalCheckIn;
		private System.Windows.Forms.ToolStripMenuItem menuLocalSendByEmail;
        private System.Windows.Forms.ToolStripMenuItem menuLocalDeleteFile;
        private System.Windows.Forms.ToolStripMenuItem menuLocalExport;
        private System.Windows.Forms.ContextMenuStrip menuDtgLocalFiles;
        private System.Windows.Forms.SaveFileDialog ExportFileDialog;
        private System.Windows.Forms.ToolTip toolTipats;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ToolStripMenuItem menuDbCheckOut;
        private System.Windows.Forms.ToolStripMenuItem menuDbOpenRead;
        private System.Windows.Forms.ToolStripMenuItem menuDbSendByEmail;
        private System.Windows.Forms.ToolStripMenuItem menuDbExport;
        private System.Windows.Forms.ToolStripMenuItem menuDbProperties;
        private System.Windows.Forms.ContextMenuStrip menuDtgSystemFiles;
        private System.Windows.Forms.ToolStripMenuItem menuVersionOpen;
        private System.Windows.Forms.ToolStripMenuItem menuVersionSendEmail;
        private System.Windows.Forms.ToolStripMenuItem menuVersionExport;
        private System.Windows.Forms.ToolStripMenuItem menuVersionExportWorkSpace;
        private System.Windows.Forms.ToolStripMenuItem menuVersionExportHardDisk;
        private System.Windows.Forms.ToolStripMenuItem menuVersionRollback;
        private System.Windows.Forms.ContextMenuStrip menuDtgVersionFiles;
        private DocumentDataGridView dtgVersions;
        private System.Windows.Forms.DataGridView dtgHist;
        private System.Windows.Forms.PictureBox pbDbGridFull;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pbLocalGridFull;
        private System.Windows.Forms.PictureBox pbShowBothSides;
        private System.Windows.Forms.Label lblExtension;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.Label lblKeyword;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblDocType;
        private System.Windows.Forms.MaskedTextBox dtEnd;
        private System.Windows.Forms.MaskedTextBox dtBegin;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        public System.Windows.Forms.SplitContainer splitContainer2;
        public System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ToolStripMenuItem menuDbDelete;
        private System.Windows.Forms.ToolStripMenuItem menuDbArchive;
        public DocumentDataGridView dtgBdFiles;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ToolStripMenuItem menuLocalChangeFileName;
        private System.Windows.Forms.ToolStripMenuItem menuDbSaveAs;
		public System.Windows.Forms.TextBox txtKeyWord;
        private System.Windows.Forms.ToolStripMenuItem menuDbCancelCheckOut;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuDbImportNewVersion;
        private System.Windows.Forms.ToolStripMenuItem menuLocalSaveAsNewVersion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.FlowLayoutPanel flpCustomGrid;
        private System.Windows.Forms.ComboBox cboAttributes;
        private System.Windows.Forms.PictureBox pbCustomSearchFields;
        private System.Windows.Forms.CheckBox ck_c_id;
        private System.Windows.Forms.CheckBox ck_c_name;
        private System.Windows.Forms.CheckBox ck_c_folder;
        private System.Windows.Forms.CheckBox ck_c_docType;
        private System.Windows.Forms.CheckBox ck_c_author;
        private System.Windows.Forms.CheckBox ck_c_version;
        private System.Windows.Forms.CheckBox ck_c_date;
        private System.Windows.Forms.Panel panelCustomSearchFields;
        private System.Windows.Forms.CheckBox ck_author;
        private System.Windows.Forms.CheckBox ck_extension;
        private System.Windows.Forms.CheckBox ck_docType;
        private System.Windows.Forms.CheckBox ck_date;
        private System.Windows.Forms.CheckBox ck_folder;
        private System.Windows.Forms.CheckBox ck_name;
        private System.Windows.Forms.CheckBox ck_keyword;
		private System.Windows.Forms.CheckBox ck_id;
		private AttributeSearch attributeSearch;
        private System.Windows.Forms.TabControl tabControlSearch;
        private System.Windows.Forms.TabPage tabPage2;
        private MultiSelectTreeview.MultiSelectTreeview treeViewMSExplorer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTreeViewOptions;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private System.Windows.Forms.ImageList imageListExplorer;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.PictureBox pboxLoadingSearch;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.TabPage tbPreview;
		private DocBrowser BsrPreview;
		private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
		private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
		private System.Windows.Forms.ToolStripMenuItem checkOutWithFooterEditToolStripMenuItem;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
		private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn35;
		private System.Windows.Forms.ToolStripMenuItem menuDbReview;
		private System.Windows.Forms.Label lblReview;
		private System.Windows.Forms.CheckBox ck_Review;
		private System.Windows.Forms.ComboBox cboReview;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn36;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;
		private System.Windows.Forms.Label lblBdDeadline;
		private System.Windows.Forms.Label lblLocalId;
		private System.Windows.Forms.Label lblLocalIdTitle;
		private System.Windows.Forms.Label lblBdByVal;
		private System.Windows.Forms.Label lblBdDeadlineVal;
		private System.Windows.Forms.Label lblBdBy;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.ToolStripMenuItem menuDbSendByEmailPdf;
		private System.Windows.Forms.ToolStripMenuItem menuDbSendByEmailDms;
		private System.Windows.Forms.ToolStripMenuItem menuDbExportPdf;
		private System.Windows.Forms.ToolStripMenuItem menuDbCheckOutFooter;
		private System.Windows.Forms.ToolStripMenuItem menuDbDmsFile;
		private System.Windows.Forms.ToolStripMenuItem menuLocalSendByEmailPdf;
		private System.Windows.Forms.ToolStripMenuItem menuLocalExportPdf;
		private System.Windows.Forms.ToolStripMenuItem menuVersionExportHardDiskPdf;
		private System.Windows.Forms.ToolStripMenuItem menuVersionSendEmailPdf;
		private System.Windows.Forms.ToolStripMenuItem menuVersionSendEmailDms;
		private System.Windows.Forms.ToolStripMenuItem menuVersionDms;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn38;
		private System.Windows.Forms.DataGridViewTextBoxColumn versionb;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_user;
		private System.Windows.Forms.DataGridViewTextBoxColumn cc_date;
		private System.Windows.Forms.DataGridViewTextBoxColumn version;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_event;
		private System.Windows.Forms.DataGridViewTextBoxColumn iddoc;
		private System.Windows.Forms.DataGridViewTextBoxColumn id;
		private System.Windows.Forms.DataGridViewTextBoxColumn reason;
		private System.Windows.Forms.ToolStripMenuItem menuVersionSendEmailOriginal;
		private System.Windows.Forms.ToolStripMenuItem menuVersionExportHardDiskOriginal;
		private System.Windows.Forms.ToolStripMenuItem menuLocalSendByEmailOriginal;
		private System.Windows.Forms.ToolStripMenuItem menuLocalExportOriginal;
		private System.Windows.Forms.ToolStripMenuItem menuDbCheckOutNoFooter;
		private System.Windows.Forms.ToolStripMenuItem menuDbSendByEmailOriginal;
		private System.Windows.Forms.ToolStripMenuItem menuDbExportOriginal;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn39;
		private System.Windows.Forms.DataGridViewTextBoxColumn versionFake;
		private System.Windows.Forms.DataGridViewTextBoxColumn userr;
		private System.Windows.Forms.DataGridViewTextBoxColumn date;
		private System.Windows.Forms.DataGridViewTextBoxColumn vevent;
		private System.Windows.Forms.DataGridViewTextBoxColumn versiona;
		private System.Windows.Forms.Button btnExport;
		private System.Windows.Forms.Label lblSystemFile;
		public System.Windows.Forms.Label lblLocalFile;
		private System.Windows.Forms.ToolStripMenuItem menuLocalEditPDF;
		private System.Windows.Forms.DataGridViewImageColumn dtgLocalFile_Icon;
		private System.Windows.Forms.DataGridViewImageColumn dtgLocalFile_Status;
		private System.Windows.Forms.DataGridViewTextBoxColumn dtgLocalFile_Id;
		private System.Windows.Forms.DataGridViewTextBoxColumn dtgLocalFile_mail_in_out_prefix;
		private System.Windows.Forms.DataGridViewTextBoxColumn dtgLocalFile_Title;
		private System.Windows.Forms.DataGridViewTextBoxColumn dtgLocalFile_mail_subject;
		private System.Windows.Forms.DataGridViewTextBoxColumn dtgLocalFile_mail_from;
		private System.Windows.Forms.DataGridViewTextBoxColumn dtgLocalFile_mail_to;
		private System.Windows.Forms.DataGridViewTextBoxColumn dtgLocalFile_mail_time;
		private System.Windows.Forms.DataGridViewTextBoxColumn dtgLocalFile_mail_is_composed;
		private System.Windows.Forms.DataGridViewTextBoxColumn dtgLocalFile_Size;
		private System.Windows.Forms.DataGridViewTextBoxColumn dtgLocalFile_Ext;
		private System.Windows.Forms.DataGridViewTextBoxColumn dtgLocalFile_Date;
		private System.Windows.Forms.DataGridViewTextBoxColumn dtgLocalFile_Path;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgLocalFile_Numver;
        private System.Windows.Forms.DataGridViewImageColumn c_img;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_id_doc;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_mail_in_out_prefix;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_title;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_mail_subject;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_mail_from;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_mail_to;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_mail_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_mail_is_composed;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_folder;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_type_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_author;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_version;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_id_version;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_atb;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_id_user;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_id_folder;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_id_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_id_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_extension;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_id_review;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_id_sp_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_created_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_id_checkout_user;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_size;
        private System.Windows.Forms.Button btnRefreshWorkSpace;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFolderViewOption1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFolderViewOption2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private MultiSelectTreeview.MultiSelectTreeview treeViewFolderExplore;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFolderRename;
        private System.Windows.Forms.Button btnFolderDelete;
        private System.Windows.Forms.Button btnFolderCreate;
        private TabPage tabPage1;
        private MultiSelectTreeview.MultiSelectTreeview treeViewArchivedFolder;
        private ToolStripMenuItem menuDbRename;
        private ToolStripMenuItem menuDbGoToFolder;
    }
}