using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiderDocsForms.UserControls
{
    public partial class SearchBox : UserControl
    {
        ListBox Suggestion = new ListBox();
        public SearchBox()
        {
            InitializeComponent();
        }

        public class Item
        {
            public object Key { get; set; }
            public object Text { get; set; }

            public Item(object key, object text)
            {
                Key = key;

                Text = text;
            }
        }

        public delegate void ExposeDblClickEventHandler(object sender, EventArgs e);

        public event ExposeDblClickEventHandler ExposeDblClickEvent;

        public class ThreeWordsArgs
        {
            public ThreeWordsArgs(string s) { Text = s; }
            public String Text { get; } // readonly
        }

        public delegate void ThreeWordsEventHandler(object sender, ThreeWordsArgs e);

        public event ThreeWordsEventHandler ThreeWordsEvent;

        BindingList<Item> data = new BindingList<Item>();


        public SearchBox(string defaultText, string extra = "")
        {
            InitializeComponent();
            //var boxLocation = this.txtSearch.Location;
            //boxLocation.Y += txtSearch.Height;
            //Suggestion.Location = boxLocation;
            //Suggestion.Visible = false;
            //this.Suggestion.Location = new System.Drawing.Point(0, 0);
            //this.Suggestion.Size = new System.Drawing.Size(191, 120);
            //this.Suggestion.DoubleClick += lbItems_DoubleClick;
            
            //this.Controls.Add(this.Suggestion);

            this.txtSearch.Text = defaultText;
        }

        public void Update(IEnumerable<Item> items)
        {
            data.Clear();
            items.ToList().ForEach(x => data.Add(x));

            this.listBox1.DataSource = data;
            this.listBox1.DisplayMember = "Text";
            this.listBox1.ValueMember = "key";
        }

        private void lbItems_DoubleClick(object sender, EventArgs e)
        {
            ExposeDblClickEvent(sender, e);
        }
        private void txtText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                return;
            }

            if (this.txtSearch.Text.Length < 3) return;

            ThreeWordsEvent(Suggestion, new ThreeWordsArgs(this.txtSearch.Text));
        }

    }
}
