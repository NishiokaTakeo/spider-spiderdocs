using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsForms
{
	public partial class PropertyPanel : PropertyPanelBase
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

        //---------------------------------------------------------------------------------
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

//---------------------------------------------------------------------------------
		public delegate void EventFunc(object sender, EventArgs e);

		int OldSelectedIndex;

		public bool IsSameAttribute
		{
			get { return ckSameAtb.Checked; }
			set { }
		}
        /*
		public FolderComboBox m_cboFolder
		{
			get { return this.cboFolder; }
			set { }
		}

		public ComboBox m_cboDocType
		{
			get { return this.cboDocType; }
			set { }
		}

		public AttributeSearch m_uscAttribute
		{
			get { return this.uscAttribute; }
			set { }
		}

		public bool Search
		{
			get { return this.uscAttribute.Search; }
			set { this.uscAttribute.Search = value; }
		}
        */

        protected FormUserControlTimer m_Timer;

        //---------------------------------------------------------------------------------
        public PropertyPanel()
		{
            logger.Trace("Begin");

            InitializeComponent();
            //uscAttribute.populateGrid();

            tmp_txtTitle = txtTitle;
			tmp_cboFolder = cboFolder;
			tmp_cboDocType = cboDocType;
			tmp_uscAttribute = uscAttribute;

            m_Timer = new FormUserControlTimer(this);
        }
        
        //---------------------------------------------------------------------------------
        private void PropertyPanel_Load(object sender,EventArgs e)
		{
            logger.Trace("Begin");
            
            if (!this.DesignMode) cboFolder.UseDataSource4AssignedMe(permission: base.FolderFilterPermission);
			if (!this.DesignMode) Setup(en_FormMode.Single);

            this.m_Timer.OnTimerElapsed += TimerElapsed;
		}

        private void PropertyPanel_Disposed(object sender, System.EventArgs e)
        {
            logger.Trace("Begin");

            m_Timer.Dispose();
        }

        /*
//---------------------------------------------------------------------------------
		public void build(en_FormMode mode)
		{
			build(mode, "");
		}
        */
        //OK
        public void Setup(en_FormMode mode, string title = "", int id_doctype = 0 ,int  id_folder = 0)
		{
            logger.Trace("Begin");
            FavariteProperty values = FavaritePropertyController.GetFavariteProperty(SpiderDocsApplication.CurrentUserId);
            
            List<FavaritePropertyItem> items = FavaritePropertyController.GetFavaritePropertyItem(values.id);
            
            List<DocumentAttribute> attrs = items.Select(item => item.ToAttribute()).ToList();
            
            Initialize(values.id_doc_type, values.id_folder);
            
            uscAttribute.populateGrid(attrs, values.id_doc_type);
            

            //cboFolder.DropDownHeight = 1;
            //Initialize(id_doctype, id_folder);

            OldSelectedIndex = cboDocType.SelectedIndex;

			ChangeFormMode(mode, title);
		}

//---------------------------------------------------------------------------------
		public event EventFunc DocType_Changed;
		override protected void cboDocType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(OldSelectedIndex == cboDocType.SelectedIndex)
				return;

			OldSelectedIndex = cboDocType.SelectedIndex;

			try
			{
				UpdateAttribute();

				if(cboDocType.SelectedIndex > 0)
				{
					if(DocType_Changed != null)
						DocType_Changed(sender, e);
				}
			}
			catch(System.Exception error)
			{
				MessageBox.Show(error.ToString(), lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

//---------------------------------------------------------------------------------
		public event EventFunc SameAtb_CheckedChanged;
		private void ckSameAtb_CheckedChanged(object sender, EventArgs e)
		{
			if(SameAtb_CheckedChanged != null)
				SameAtb_CheckedChanged(sender, e);
		}

//---------------------------------------------------------------------------------
		public void ChangeFormMode(en_FormMode mode, string title = "")
		{
            logger.Trace("Begin");

            _FormMode = mode;
			
			if(_FormMode == en_FormMode.Multiple)
			{
				plTitle.Location = new Point(plSameAtb.Location.X, plSameAtb.Location.Y);
				plSameAtb.Location = new Point(0, plSameAtb.Location.Y);

				plTitle.Visible = false;
				plSameAtb.Visible = true;

			}else
			{
				plSameAtb.Location = new Point(plTitle.Location.X, plTitle.Location.Y);
				plTitle.Location = new Point(0, plTitle.Location.Y);

				plSameAtb.Visible = false;
				plTitle.Visible = true;

				txtTitle.Text = title;	// Set file name to text box.
			}

			if(_FormMode == en_FormMode.Multiple_PDF)
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

			if((cboFolder.SelectedValue == null) || (Convert.ToInt32(cboFolder.SelectedValue) <= 0))
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

				if(wrk == null)
					uscAttribute.populateGrid(Convert.ToInt32(cboDocType.SelectedValue));
				else
					uscAttribute.populateGrid(wrk);

				uscAttribute.updateNow();

			}else
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
        /*
        private void cboFolder_MouseClick(object sender, MouseEventArgs e)
        {
            int beforeIdFolder = (int)cboFolder.SelectedValue;

            frmFolderExplorer explorer = new frmFolderExplorer(beforeIdFolder);

            explorer.ShowDialog();
            
            if( explorer.SelectedFolder.id == 0 )
            {
                //MessageBox.Show(lib.msg_required_doc_folder, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cboFolder.SelectedValue = explorer.SelectedFolder.id;
        }
        */
        //---------------------------------------------------------------------------------
    }
}
