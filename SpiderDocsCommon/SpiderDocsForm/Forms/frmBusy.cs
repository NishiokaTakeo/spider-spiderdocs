using System;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

//---------------------------------------------------------------------------------
namespace SpiderDocsForms
{
	public partial class frmBusy : Form
	{

//---------------------------------------------------------------------------------
		public frmBusy(string message = "")
		{
			InitializeComponent();

            if(message != "")
                this.label2.Text = message;			
		}

		private void frmMain_Shown(object sender, EventArgs e)
		{
            
        }

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        //---------------------------------------------------------------------------------
    }
}
