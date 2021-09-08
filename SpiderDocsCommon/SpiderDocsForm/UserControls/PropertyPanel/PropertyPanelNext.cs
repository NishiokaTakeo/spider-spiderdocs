/*
 * This class is the class that merged PropertyPanel and PropertyPanelBase. Eventually, these two will be obsolate.
 */


using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using lib = SpiderDocsModule.Library;
using SpiderDocsModule;
using Spider.Forms;
using System.Linq;
using NLog;
using System.Drawing;

namespace SpiderDocsForms
{
    public partial class PropertyPanelNext : Spider.Forms.UserControlBase
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        //public TextBox tmp_txtTitle = new TextBox();
        //public FolderComboBox tmp_cboFolder = new FolderComboBox();
        //public ComboBox tmp_cboDocType = new ComboBox();
        //public AttributeSearch tmp_uscAttribute = new AttributeSearch();
        //bool initializing = false;

        public FolderComboBox Folder { get { return this.cboFolder; } set { this.cboFolder = value; } }
        public ComboBox Type { get { return this.cboDocType; } set { this.cboDocType = value; } }
        public AttributeSearch AttrPanel {get {return uscAttribute;} set { this.uscAttribute = value; } }
        public TextBox Title { get {return txtTitle; } set { this.txtTitle = value; } }
		public PropertyPanelNext()
		{
            InitializeComponent();
            //uscAttribute.populateGrid();
            
            //txtTitle.Visible = false;
		}

        public void Initialize(int id_doctype = 0, int id_folder = 0, en_Actions folder_per= 0)
		{
			cboDocType.DisplayMember = "type";
			cboDocType.ValueMember = "id";
			//populate combo folders
			cboFolder.DisplayMember = "document_folder";
			cboFolder.ValueMember = "id";

            _UpdateFolderList(id_folder, folder_per);
            _UpdateDocTypeList(id_doctype);
			
            cboDocType.SelectedIndexChanged += cboDocType_SelectedIndexChanged;
            cboFolder.SelectedIndexChanged += new System.EventHandler(cboFolder_SelectedIndexChanged);

		}

		public void UpdateFolderList(int id_folder)
		{
			_UpdateFolderList(id_folder);

			cboFolder_SelectedIndexChanged();
		}

		void _UpdateFolderList(int id_folder, en_Actions permission = 0){

            //List<Folder> folders = PermissionController.GetAssignedFolderToUser(only_edit_permitted_folders: true).OrderBy(a => a.document_folder).ToList();
            /*
			List<Folder> folders = PermissionController.FilterByPermission(SpiderDocsApplication.CurrentUserId,new Cache(SpiderDocsApplication.CurrentUserId).GetAssignedFolderToUser(),en_Actions.CheckIn_Out).OrderBy(a => a.document_folder).ToList();
			 
            folders.Insert(0, new Folder(0, "",0));

            //populate combo folders
            cboFolder.DataSource = folders;


            if ((0 < id_folder) && folders.Exists(a => a.id == id_folder))
				cboFolder.SelectedValue = id_folder;
			else
				cboFolder.SelectedIndex = 0;
            */
            /*
            if(id_folder > 0 )
                tmp_cboFolder.SelectedValue = id_folder;
            */

            cboFolder.UseDataSource4AssignedMe(0, permission);
            cboFolder.SelectedValue = id_folder;

            uscAttribute.FolderId = (int)cboFolder.SelectedValue;

		}

		protected int UpdateDocTypeList(int id_doctype)
		{
			_UpdateDocTypeList(id_doctype);

			cboDocType_SelectedIndexChanged();

			return cboDocType.SelectedIndex;
		}
		void _UpdateDocTypeList(int id_doctype){
						DTS_DocumentType DA_DocumentType = new DTS_DocumentType(true);
			DA_DocumentType.Select();

			cboDocType.DataSource = DA_DocumentType.GetDataTable("type");

			if(0 < id_doctype)
				cboDocType.SelectedValue = id_doctype;
			else
				cboDocType.SelectedIndex = 0;

		}
		public void ClearError(object objTitle = null)
		{
			if(objTitle != null)
				FormUtilities.PutErrorColour(objTitle, false, true);

			FormUtilities.PutErrorColour(cboFolder, false, true);
			FormUtilities.PutErrorColour(cboDocType, false, true);
		}

		public bool IsAllFieldsEntered(string extension, bool local)
		{
			if(txtTitle.Visible)
				return IsAllFieldsEntered(this.txtTitle, extension, local);
			else
				return IsAllFieldsEntered(null, extension, local);
		}

		public bool IsAllFieldsEntered(object objTitle, string extension, bool local)
		{
			bool ans = true;
			ClearError(objTitle);

			//Title
			if(objTitle != null)
			{
				string title = "";

				Control control = objTitle as Control;
				if(control != null)
					title = control.Text;

				DataGridViewCell cell = objTitle as DataGridViewCell;
				if(cell != null)
					title = cell.Value.ToString();

				if(String.IsNullOrEmpty(title))
				{
					if(ans)
						MessageBox.Show(lib.msg_required_doc_title, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

					FormUtilities.PutErrorColour(objTitle, ans);
					ans = false;

				}else if(!FileFolder.IsValidFileName(title))
				{
					if(ans)
						MessageBox.Show(lib.msg_wrong_file_name, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

					FormUtilities.PutErrorColour(objTitle, ans);
					ans = false;

				}else if(!local
					  && !SpiderDocsApplication.CurrentPublicSettings.allow_duplicatedName
				      //&& (0 < tmp_cboDocType.SelectedIndex)
					  && (DocumentController.IsDocumentExists(Convert.ToInt32(cboFolder.SelectedValue), title + extension)))
				{
					//Check if already exist file in the same folder with the same name
					if(ans)
						MessageBox.Show(lib.msg_existing_file, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

					FormUtilities.PutErrorColour(objTitle, ans);
					ans = false;
				}
			}

			//Folder
			if(!local && ((cboFolder.SelectedValue == null) || ((int)cboFolder.SelectedValue <= 0)))
			{
				if(ans)
					MessageBox.Show(lib.msg_required_doc_folder, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				FormUtilities.PutErrorColour(cboFolder, ans);
				ans = false;
			}

			//Document type
			//if(!local && ((tmp_cboDocType.SelectedValue == null) || ((int)tmp_cboDocType.SelectedValue <= 0)))
			//{
			//	if(ans)
			//		MessageBox.Show(lib.msg_required_doc_type, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

			//	FormUtilities.PutErrorColour(tmp_cboDocType, ans);
			//	ans = false;
			//}

			//attributes
			if(!local)
			{
				bool attr_check = uscAttribute.checkMandadoryAttributes(ans);
				ans = (ans == true ? attr_check : ans);
			}

			return ans;
		}

		virtual protected void cboDocType_SelectedIndexChanged(object sender, EventArgs e)
		{
			cboDocType_SelectedIndexChanged();

            _cboDocType_SelectedIndexChanged(sender, e);


        }

        void cboDocType_SelectedIndexChanged()
		{
            if ((cboDocType.SelectedValue != null) && (0 < (int)cboDocType.SelectedValue))
                uscAttribute.populateGrid((int)cboDocType.SelectedValue);            
		}

		private void cboFolder_SelectedIndexChanged(object sender, EventArgs e)
		{
			cboFolder_SelectedIndexChanged();
		}

		void cboFolder_SelectedIndexChanged()
		{
			if((cboFolder.SelectedValue != null) && (0 < (int)cboFolder.SelectedValue))
				uscAttribute.FolderId = (int)cboFolder.SelectedValue;

			if((cboDocType.SelectedValue != null) && (0 < (int)cboDocType.SelectedValue))
				uscAttribute.populateGrid(uscAttribute.getAttributeValuesCopy(), (int)cboDocType.SelectedValue);
		}

        #region PropertyPanel

        public enum en_FormMode
        {
            Multiple,
            Multiple_PDF,
            Single,
            Max
        }

        en_FormMode _FormMode;
        public en_FormMode FormMode
        {
            get { return _FormMode; }
            set { }
        }
        public delegate void EventFunc(object sender, EventArgs e);

        int OldSelectedIndex;
        public bool IsSameAttribute
        {
            get { return ckSameAtb.Checked; }
            set { }
        }

        public void Setup(en_FormMode mode, string title = "", int id_doctype = 0, int id_folder = 0, en_Actions folder_per = 0)
        {
            logger.Trace("Begin");

            FavariteProperty values = FavaritePropertyController.GetFavariteProperty(SpiderDocsApplication.CurrentUserId);
            List<FavaritePropertyItem> items = FavaritePropertyController.GetFavaritePropertyItem(values.id);
            List<DocumentAttribute> attrs = items.Select(item => item.ToAttribute()).ToList();
            Initialize(values.id_doc_type, values.id_folder,folder_per);
            uscAttribute.populateGrid(attrs, values.id_doc_type);

            //cboFolder.DropDownHeight = 1;
            //Initialize(id_doctype, id_folder);

            OldSelectedIndex = cboDocType.SelectedIndex;

            ChangeFormMode(mode, title);
        }



        //---------------------------------------------------------------------------------
        public event EventFunc DocType_Changed;
        void _cboDocType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (OldSelectedIndex == cboDocType.SelectedIndex)
                return;

            OldSelectedIndex = cboDocType.SelectedIndex;

            try
            {
                UpdateAttribute();

                if (cboDocType.SelectedIndex > 0)
                {
                    if (DocType_Changed != null)
                        DocType_Changed(sender, e);
                }
            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.ToString(), lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //---------------------------------------------------------------------------------
        public event EventFunc SameAtb_CheckedChanged;

        //---------------------------------------------------------------------------------
        public void ChangeFormMode(en_FormMode mode, string title = "")
        {
            logger.Trace("Begin");

            _FormMode = mode;

            if (_FormMode == en_FormMode.Multiple)
            {

                //txtTitle.Location = new Point(plSameAtb.Location.X, plSameAtb.Location.Y);
                plSameAtb.Location = new Point(0, plSameAtb.Location.Y);

                plTitle.Visible = false;
                plSameAtb.Visible = true;

            }
            else
            {
                //plSameAtb.Location = new Point(txtTitle.Location.X, txtTitle.Location.Y);
                plTitle.Location = new Point(0, txtTitle.Location.Y);

                plSameAtb.Visible = false;
                plTitle.Visible = true;

                txtTitle.Text = title;  // Set file name to text box.
            }

            if (_FormMode == en_FormMode.Multiple_PDF)
            {
                ckSameAtb.Checked = true;
            }
        }

        //---------------------------------------------------------------------------------
        public void GetPropertyVal<T>(T dst) where T : SpiderDocsModule.DocumentProperty
        {
            logger.Trace("Begin");

            if ((cboDocType.SelectedValue == null) || (Convert.ToInt32(cboDocType.SelectedValue) <= 0))
                dst.id_docType = -1;
            else
                dst.id_docType = Convert.ToInt32(cboDocType.SelectedValue);

            if ((cboFolder.SelectedValue == null) || (Convert.ToInt32(cboFolder.SelectedValue) <= 0))
                dst.id_folder = -1;
            else
                dst.id_folder = Convert.ToInt32(cboFolder.SelectedValue);

            dst.Attrs = uscAttribute.getAttributeValuesCopy();
        }

        //---------------------------------------------------------------------------------		
        public void PopulateAttrVal<T>(T wrk) where T : SpiderDocsModule.DocumentProperty
        {
            logger.Trace("Begin");

            if ((!ckSameAtb.Checked) && (wrk != null))
            {
                cboFolder.SelectedValue = wrk.id_folder;
                cboDocType.SelectedValue = wrk.id_docType;

                UpdateAttribute(wrk);
            }
        }

        //---------------------------------------------------------------------------------
        public DocumentProperty getCurrentProperty()
        {
            logger.Trace("Begin");

            DocumentProperty ans = new DocumentProperty();

            ans.id_folder = Convert.ToInt32(cboFolder.SelectedValue);
            ans.id_docType = Convert.ToInt32(cboDocType.SelectedValue);
            ans.Attrs = uscAttribute.getAttributeValues();

            return ans;
        }

        //---------------------------------------------------------------------------------
        void UpdateDocTypeList()
        {
            logger.Trace("Begin");

            OldSelectedIndex = UpdateDocTypeList(cboDocType.SelectedIndex);
        }

        //---------------------------------------------------------------------------------
        void UpdateAttribute()
        {
            logger.Trace("Begin");

            UpdateAttribute<SpiderDocsModule.DocumentProperty>(null);
        }

        void UpdateAttribute<T>(T wrk) where T : SpiderDocsModule.DocumentProperty
        {
            logger.Trace("Begin");

            if (cboDocType.SelectedIndex > 0)
            {
                uscAttribute.Enabled = true;

                if (wrk == null)
                    uscAttribute.populateGrid(Convert.ToInt32(cboDocType.SelectedValue));
                else
                    uscAttribute.populateGrid(wrk);

                uscAttribute.updateNow();

            }
            else
            {
                uscAttribute.Enabled = false;
                uscAttribute.ClearPanel();
            }
        }

        //---------------------------------------------------------------------------------
        void TimerElapsed(object sender)
        {

            if (0 < MMF.ReadData<int>(MMF_Items.PropertyUpdateReq))
            {
                MMF.WriteData<int>(0, MMF_Items.PropertyUpdateReq);

                UpdateFolderList(0);
                UpdateDocTypeList();
                UpdateAttribute();
            }
        }
        #endregion
    }


}
