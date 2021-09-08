using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SpiderDocsModule;
using SpiderDocsForms;
using lib = SpiderDocsModule.Library;
using Document = SpiderDocsForms.Document;
using Spider.Drawing;
using NLog;
using System.Linq;

namespace SpiderDocs
{
	public partial class frmFileProperties : Spider.Forms.FormBase
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

		Document doc;
		//bool formloaded = false;
		PropertyPanelBase tmp_PropertyPanel = new PropertyPanelBase();

        private List<Document> docs = new List<Document>();

        /// <summary>
        /// true if single file is selected, false if multiple file is selected
        /// </summary>
        public bool isSingleFile
        {
            get { return (this.docs.Count == 1); }
        }

		public frmFileProperties(int id_doc)
		{ 
			InitializeComponent();

			doc = DocumentController.GetDocument(id_doc);

			//document type
			tmp_PropertyPanel.tmp_txtTitle = txtTitle;
			tmp_PropertyPanel.tmp_cboFolder = cboFolder;
			tmp_PropertyPanel.tmp_cboDocType = cboDocType;
            tmp_PropertyPanel.tmp_cboNotificationGroup = cboNotificationGroup;
			tmp_PropertyPanel.tmp_uscAttribute = panel;

            int[] ids_ngroup = DocumentNotificationGroupController.Select(id_doc: doc.id).Select( x => x.id_notification_group).ToArray();

			tmp_PropertyPanel.Initialize(doc.id_docType, doc.id_folder, ids_ngroup);

            // add doc to list. 
            this.docs.Add(doc);
		}

        // Overload for multiple file.
        /// <summary>
        /// For multiple file. You will use this constractor if you select multiple file.
        /// In this case, this.doc is empty document which is temporary. manipulated  file will be this.docs
        /// </summary>
        /// <param name="docs"></param>
        public frmFileProperties(int[] ids)
        {
            InitializeComponent();
            
            //set as empty document if id_doc is not passed so that shows a property for multple files you select
            doc = new Document();
            doc.extension = "txt";

            //document type
            tmp_PropertyPanel.tmp_txtTitle = txtTitle;
            tmp_PropertyPanel.tmp_cboFolder = cboFolder;
            tmp_PropertyPanel.tmp_cboDocType = cboDocType;
            tmp_PropertyPanel.tmp_cboNotificationGroup = cboNotificationGroup;
            tmp_PropertyPanel.tmp_uscAttribute = panel;
            tmp_PropertyPanel.Initialize(doc.id_docType, doc.id_folder);

            //set doc instance like 'frmFileProperties(int id_doc)' contractor, Actual data is docs. doc is empty data.
            foreach( int id_doc in ids)
                this.docs.Add(DocumentController.GetDocument(id_doc));
        }
        
		private void frmFileProperties_Load(object sender, EventArgs e)
		{

			try
			{
                this.lblNotificationGroup.Enabled = this.cboNotificationGroup.Enabled = PermissionController.IsFeatureEnabled("SubMenu_NotificationGroup");

                int font_size = new TextBox().Font.Height;
				int start_x = lblFolder.Left + lblFolder.Width + (font_size / 2);
				int ctrl_w = lblExt.Left - start_x - (font_size / 2);

				//title
				txtTitle.Text = doc.title.Replace(doc.extension, "");
				txtTitle.Left = start_x;
				txtTitle.Width = ctrl_w;

				//extension
				lblExt.Text = doc.extension.Trim();

				//icon
                //Return icon image or multi-files-icon image when user select multiple files. This is low priority. currently only single file more is working.
                pictureBox.Image = new IconManager().GetLargeIcon(doc.extension);

				cboFolder.Left = start_x;
				cboFolder.Width = ctrl_w;

				cboDocType.Left = start_x;
				cboDocType.Width = ctrl_w;

				//attributes
				panel.populateGrid(doc);

				btnCopy.Left = lblExtLinkTitle.Left + lblExtLinkTitle.Width + (font_size / 2);
				lbExtLink.Text = SpiderDocs.Utilities.CreateExternalLink(doc.id);
				
				if(lbExtLink.Text != "")
					btnCopy.Enabled = true;
                
                // control form's enable if single file is selected.because data in appearance is actual data.
                if (isSingleFile)
                {
                    //button save
                    btnSave.Enabled = doc.IsActionAllowed(en_Actions.Properties);
                    txtTitle.Enabled = btnSave.Enabled;
                    cboFolder.Enabled = btnSave.Enabled;
                    cboDocType.Enabled = btnSave.Enabled;
                    panel.Enabled = btnSave.Enabled;
                    cboNotificationGroup.Enabled = btnSave.Enabled;

                    //Name, extension and WebLink are disable and invisible. If multiple file is selected. because data in appearance is empty. (temporary)
                }
                else
                {
                    lblName.Visible = txtTitle.Visible = lblExt.Visible = btnCopy.Visible = lbExtLink.Visible = lblExtLinkTitle.Visible = lbExtLink.Visible = false;
                }
			}
			catch(Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			// formloaded = true;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{

            //doc is actual data if it is single selected, doc is empty(temporary) doc if it is multiple selected
            if (tmp_PropertyPanel.IsAllFieldsEntered(doc.extension, false))
            {
				List<Document> checkedDocs = new List<Document>();

                // check first
                foreach (Document _doc in docs)
                {
                    //fill values to doc
                    SyncValuesFromUI(_doc);

                    if (!_doc.isNotDuplicated())
                    {
                        //MessageBox.Show(lib.msg_found_duplicate_docs, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

					if (!_doc.__WarnForDuplicate(true) )
					{
                        //if (MessageBox.Show(lib.msg_warn_existing_file, lib.msg_messabox_title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.Yes)
                        //	return;
                        //else
                        //	_doc.hasAccepted = true;
                        return;
					}

					checkedDocs.Add(_doc);
				}            



                //for each docs you selected
                foreach (Document doc in checkedDocs)
                {
                    //add parameter doc to updateDocumentDetails
                    updateDocumentDetails(doc);
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        private void SyncValuesFromUI(Document doc)
        {
            int id_folder = Convert.ToInt32(cboFolder.SelectedValue);
            int id_docType = Convert.ToInt32(cboDocType.SelectedValue);

            if (isSingleFile)
            {
                string title = txtTitle.Text + doc.extension;

                doc.title = title;
            }
            // multiple file selected
            else
            {
                //Only id_folder, id_docType and Attrs are updated if multiple-file

            }

            doc.id_folder = id_folder;
            doc.id_docType = id_docType;

            doc.Attrs = panel.getAttributeValues();
        }

        /// <summary>
        /// Update document. 
        /// Only id_folder are id_docType,Attrs are updated if multiple-file
        /// </summary>
        /// <param name="doc">will be updated to database</param>
		private void updateDocumentDetails(Document doc)
		{

            string ans = doc.UpdateProperty();

            int[] ids_ngroup = cboNotificationGroup.getComboValue<DocumentAttributeCombo>().Select(a => a.id).ToArray();
            DocumentNotificationGroupController.SaveOne(null, doc.id, ids_ngroup);

			if(!String.IsNullOrEmpty(ans))
			{
				//Utilities.regLog(ans);
                logger.Error("System Error {0}",ans);
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
    
		private void txtTitle_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utilities.checkKeychar((int)e.KeyChar);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			Close();
		}

		private void btnCopy_Click(object sender,EventArgs e)
		{
			Clipboard.SetText(lbExtLink.Text);
		}

        public void SetComboCache(Dictionary<int, System.Collections.IEnumerable> db)
        {
            //this.panel.ControlCaches = db;
        }
	}
}
