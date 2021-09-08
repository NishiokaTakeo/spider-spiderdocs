using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;
using Document = SpiderDocsForms.Document;
using NLog;
using System.Linq;

namespace SpiderDocs
{
    public partial class frmFavourite : Spider.Forms.FormBase
    {
        static Logger logger = LogManager.GetCurrentClassLogger();


        public frmFavourite()
        {
            logger.Trace("Begin");
            InitializeComponent();
        }

        private void frmFileProperties_Load(object sender, EventArgs e)
		{
            logger.Trace("Begin");

            try
			{
                FavouriteLoad();
            }
			catch(Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        void FavouriteLoad()
        {
             FavariteProperty values = FavaritePropertyController.GetFavariteProperty(SpiderDocsApplication.CurrentUserId);
            List<FavaritePropertyItem> items = FavaritePropertyController.GetFavaritePropertyItem(values.id);
            List<DocumentAttribute> attrs = items.Select(item => item.ToAttribute()).ToList();

            this.propertyPanelNext.Initialize(values.id_doc_type,values.id_folder, en_Actions.CheckIn_Out);

            this.propertyPanelNext.AttrPanel.populateGrid(attrs, values.id_doc_type);
        }

		private void btnSave_Click(object sender, EventArgs e)
		{
            logger.Trace("Begin");

            int id_folder = (int)this.propertyPanelNext.Folder.SelectedValue, id_type = (int)this.propertyPanelNext.Type.SelectedValue;
            List<DocumentAttribute> attrs = this.propertyPanelNext.AttrPanel.getAttributeValues();

            try
            {
                FavaritePropertyController.SaveFavatiteProperty(SpiderDocsApplication.CurrentUserId,id_folder,id_type,attrs);

                MessageBox.Show("Favourite Property has been saved", lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            Close();
        }

		private void btnClose_Click(object sender, EventArgs e)
		{
            logger.Trace("Begin");

            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			Close();
		}

        private void btnClear_Click(object sender, EventArgs e)
        {
			DialogResult result = MessageBox.Show(lib.msg_ask_delete_favourite, lib.msg_messabox_title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if(result == DialogResult.Yes)
            {
                FavaritePropertyController.DeleteMyFavouriteProperty(SpiderDocsApplication.CurrentUserId);

                FavouriteLoad();

                MessageBox.Show("Favourite Property has been saved", lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);

                Close();
            }
        }
    }
}
