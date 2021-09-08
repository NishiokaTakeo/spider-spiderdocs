using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiderDocs.Forms.WorkSpace
{
    public partial class frmEnterText : Form
    {

        public class ThreeWordsArgs
        {
            public ThreeWordsArgs(KeyEventArgs e, string s) { Text = s; Args = e; }
            public String Text { get; } // readonly
            public KeyEventArgs Args { get; }
        }

        public delegate void KeyUpEvntHandler(object sender, ThreeWordsArgs e);
        public delegate void BeforeCloseEvntHandler(object sender, KeyPressEventArgs e);

        public event KeyUpEvntHandler Entering;
        public event BeforeCloseEvntHandler Commit;
        public bool AutoCloseByEnter = true;
        private string original = string.Empty;

        public frmEnterText()
        {
            InitializeComponent();
        }

		public frmEnterText(string defaultText, string extra = "")
		{
			InitializeComponent();

			this.txtText.Text = original = defaultText;
			this.lblExtra.Text = extra;
		}

		private void txtText_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == (char)Keys.Enter)
            {
                Commit?.Invoke(sender, e);

                if (AutoCloseByEnter) this.Close();
            }
        }

        public string LabelText { set { this.label1.Text = value; } }
        public void SetText(string typed)
        {
            this.txtText.Text = typed;
        }

        private void txtText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                return;
            }

            Entering?.Invoke(this.txtText, new ThreeWordsArgs(e, this.txtText.Text));
        }

        private void frmEnterText_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
