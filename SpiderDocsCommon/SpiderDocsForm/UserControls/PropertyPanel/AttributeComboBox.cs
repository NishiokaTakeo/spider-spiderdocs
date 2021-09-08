using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
//using PresentationControls;
using SpiderDocsModule;
//---------------------------------------------------------------------------------
namespace SpiderDocsForms
{
    public partial class AttributeComboBox : Spider.Forms.UserControlBase
    {
        bool Loaded = false;
        Color BackupBackColor;

        bool binding = false;

        //List<DocumentAttributeCombo> _combo_items;
        public List<DocumentAttributeCombo> DataSource { get; set; } = new List<DocumentAttributeCombo>();

        DocumentAttribute _m_attr;
        public DocumentAttribute m_attr { get { return _m_attr; } }

        public object SelectedValue
        {
            get { return cboComboBox.SelectedValue; }
            set { cboComboBox.SelectedValue = value; }
        }

        public int SelectedIndex
        {
            get { return cboComboBox.SelectedIndex; }
            set { cboComboBox.SelectedIndex = value; }
        }

        AttributeSearch ComboChildren = new AttributeSearch();

        //---------------------------------------------------------------------------------
        public AttributeComboBox(DocumentAttribute attr, bool search, bool MultiSelectable)//, List<DocumentAttributeCombo> source = null)
        {
            InitializeComponent();

            _m_attr = attr;

            ComboChildren.Width = this.Width;
            ComboChildren.Fixed_X_Start = 0;
            ComboChildren.Fixed_Y_Start = 0;
            ComboChildren.Top = cboComboBox.Top + cboComboBox.Height;
            ComboChildren.Anchor |= AnchorStyles.Right;
            ComboChildren.AutoSize = true;
            cboComboBox.CheckBoxCheckedChanged += cboComboBox_CheckBoxCheckedChanged;

            if (!search)
            {
                this.Controls.Add(ComboChildren);
                cboComboBox.MultiSelectable = MultiSelectable;

            }
            else
            {
                ComboChildren.Visible = false;
            }

            /*
            if(source == null) { 
                DataSource = DocumentAttributeController.GetComboItems(true, m_attr.id);
                Bind();
            }
            */
            if (search || (m_attr.id_type == en_AttrType.FixedCombo))
            {
                btnAdd.Visible = false;
                cboComboBox.Width += btnAdd.Width;
            }
            
        }
        

        //---------------------------------------------------------------------------------
        private void AttributeComboBox_BackColorChanged(object sender, EventArgs e)
        {
            if (Loaded && this.BackColor != BackupBackColor)
            {
                cboComboBox.BackColor = this.BackColor;
                this.BackColor = BackupBackColor;
            }
        }

        //---------------------------------------------------------------------------------
        private void AttributeComboBox_Load(object sender, EventArgs e)
        {
            BackupBackColor = Utilities.ObjectClone(this.BackColor);
            Loaded = true;
        }

        //---------------------------------------------------------------------------------
        void cbo_sample_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        //---------------------------------------------------------------------------------
        void cbo_sample_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                e.Handled = true;
        }

        //---------------------------------------------------------------------------------
        void cboComboBox_CheckBoxCheckedChanged(object sender, EventArgs e)
        {
            if (!binding)
                UpdateChildrenValues();
        }

        //---------------------------------------------------------------------------------
        void btnAdd_Click(object sender, EventArgs e)
        {
            m_attr.atbValue = GetSelectedComboItems();

            frmAttributeComboItem frm = new frmAttributeComboItem();
            frm.id_atb = m_attr.id;
            frm.nameCombo = m_attr.name;
            frm.ShowDialog();

            DataSource = DocumentAttributeController.GetComboItems(true, m_attr.id);

            Bind();
        }

        //---------------------------------------------------------------------------------
        public void Bind()
        {
            binding = true;

            //DataSource = DocumentAttributeController.GetComboItems(true, m_attr.id);

            DataSource.ForEach(delegate(DocumentAttributeCombo item)
            {
                if (((List<int>)m_attr.atbValue).Contains(item.id)) item.Selected = true;
                cboComboBox.AddItem(item, item.Selected);
            });

            cboComboBox.Text = cboComboBox.SelectedItemsText;

            binding = false;

            UpdateChildrenValues();

        }

        //---------------------------------------------------------------------------------
        void UpdateChildrenValues()
        {
            List<DocumentAttributeCombo> children = GetSelectedComboItemsObject().Where(x => x.children.Count> 0).ToList();

            if (ComboChildren.Visible && (0 < DataSource.Count) && (0 < children.Count))
            {
                DocumentAttributeCombo target = children.FirstOrDefault();

                if (target == null)
                {
                    target = Utilities.ObjectClone(DataSource[0]);
                    target.children.ForEach(a => a.InitializeValue());
                }

                ComboChildren.populateGrid(target.children, null, target.children.Select(a => a.id).ToArray());
                this.Height = ComboChildren.Height + cboComboBox.Height;
            }
        }

        //---------------------------------------------------------------------------------
        /// <summary>
        /// Get list for comboboxitem
        /// </summary>
        /// <returns>ids</returns>
        public List<int> GetSelectedComboItems()
        {
            List<int> ans = cboComboBox.getComboValue<DocumentAttributeCombo>().Select(a => a.id).ToList();
            return ans.Distinct().ToList();
        }

        //---------------------------------------------------------------------------------
        public List<DocumentAttributeCombo> GetSelectedComboItemsObject()
        {
            return cboComboBox.getComboValue<DocumentAttributeCombo>();
        }

        //---------------------------------------------------------------------------------
    }
}
