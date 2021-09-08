using SpiderDocsForms.UserControls;
using SpiderDocsModule.Models;
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
    public partial class frmSimpleList : Form
    {
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

        //public delegate void ExposeDblClickEventHandler(object sender, EventArgs e);

        public event SearchBox.ExposeDblClickEventHandler ExposeDblClickEvent;
        //public event SearchBox.ThreeWordsEventHandler ThreeWordsEvent;

        BindingList<Item> data = new BindingList<Item>();

        //public SearchBox SearchBox { get { return this.SearchBox; } }

        public frmSimpleList()
        {
            InitializeComponent();
            this.Items.DataSource = data;
            


            //this.Suggested.ThreeWordsEvent += Suggested_ThreeWordsEvent;
            //this.Suggested.ExposeDblClickEvent += Suggested_ExposeDblClickEvent;

        }
        public Item GetSelected()
        {
            return (Item)this.Items.SelectedItem;
        }
        public bool HasItem(string text)
        {
            foreach (var item in this.Items.Items)
                if (((Item)item).Text.ToString() == text) return true;

            return false;
        }
        public void Next()
        {
            if(this.Items.SelectedIndex >= this.Items.Items.Count -1 )
            {
                this.Items.SelectedIndex = 0;
                return;
            }

            ++this.Items.SelectedIndex;
        }

        public void Prev()
        {
            if (this.Items.SelectedIndex <= 0)
            {
                this.Items.SelectedIndex = this.Items.Items.Count -1 ;
                return;
            }

            --this.Items.SelectedIndex;
        }
        //public void SetText(string str)
        //{
        //    this.Suggested.txtSearch.Text = str;
        //}


        //private void Suggested_ThreeWordsEvent(object sender, SpiderDocsForms.UserControls.SearchBox.ThreeWordsArgs e)
        //{
        //    ThreeWordsEvent(sender, e);
        //}

        public void Update( IEnumerable<Item> items)
        {
            //this.Suggested.Update(items);
            data.Clear();
            items.ToList().ForEach(x => data.Add(x));

            this.Items.DataSource = data;
            this.Items.DisplayMember = "Text";
            this.Items.ValueMember = "key";
            this.Items.SelectedIndex = -1;
        }

        private void Suggested_DoubleClick(object sender, EventArgs e)
        {
            ExposeDblClickEvent(sender, e);
        }

        private void Suggested_KeyUp(object sender, KeyEventArgs e)
        {
                //if (e.KeyCode == Keys.Enter)
                //{
                //    ExposeDblClickEvent(sender, e);
                //    return;
                //}

            
        }

        private void Suggested_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == Keys.Enter)
            //{
                ExposeDblClickEvent(sender, e);
                return;
            //}
        }

        //private void Suggested_Enter(object sender, EventArgs e)
        //{            
        //    ExposeDblClickEvent(sender, e);
        //}

        //private void lbItems_DoubleClick(object sender, EventArgs e)
        //{
        //    ExposeDblClickEvent(sender, e);
        //}

        //private void txtText_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        ExposeDblClickEvent(sender, e);
        //        return;
        //    }

        //    if (this.txtText.Text.Length < 3) return;

        //    ThreeWordsEvent(this.txtText, new ThreeWordsArgs(this.txtText.Text));
        //}
    }
}
