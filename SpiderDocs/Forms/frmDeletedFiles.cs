using System;
using System.Windows.Forms;
using SpiderDocsModule;

namespace SpiderDocs
{
    public partial class frmDeletedFiles : Form
    {
        public frmDeletedFiles()
        {
            InitializeComponent();
        }

        private void frmDeletedDocs_Load(object sender, EventArgs e)
        {
            dtgDeletedDocument.DataSource = new DTS_DocumentDeleted().GetDataTable();
        }
    }
}
