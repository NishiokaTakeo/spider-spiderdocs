using SpiderDocsModule.Classes.Scanners;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SpiderDocsForms
{
    public partial class frmScannerList : Form
    {
        IScanner _default ;
        public frmScannerList(List<IScanner> scanner, IScanner defaultScnr = null)
        {
            InitializeComponent();

            lbxScanner.Items.Clear();
            scanner.ForEach(device => lbxScanner.Items.Add(device));

            _default = defaultScnr;
        }


        private void frmScannerListcs_Load(object sender, EventArgs e)
        {
            //_default = lbxScanner.SelectedIndex > -1 ? lbxScanner.SelectedIndex : 0;
        }
        

        public IScanner ChosenScanner()
        {
            return (IScanner)lbxScanner.SelectedItem;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (IScanner item in lbxScanner.Items)
            {
                
                if (item.Equals(_default)/*Scanner.IsSameScanner(item, _default)*/)
                    _default = (IScanner)item;
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            SetScanner2List(_default);

            this.Close();
        }

        private void frmScannerList_Shown(object sender, EventArgs e)
        {
            SetScanner2List(_default);
        }

        void SetScanner2List(IScanner replacement)
        {            
            for (int i = 0; i < lbxScanner.Items.Count; i++)
            {
                IScanner item = (IScanner)lbxScanner.Items[i];
                if (item.Equals(replacement))
                    lbxScanner.SelectedItem = item;
            }

            
        }

    }
}
