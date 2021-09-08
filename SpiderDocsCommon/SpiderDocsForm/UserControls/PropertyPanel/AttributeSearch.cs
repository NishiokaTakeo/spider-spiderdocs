using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Globalization;
using SpiderDocsModule;
using Spider.Forms;
using Spider.Types;
using System.Reflection;

//---------------------------------------------------------------------------------
namespace SpiderDocsForms
{
    public partial class AttributeSearch : Spider.Forms.UserControlBaseWithDeclaration
    {
        //---------------------------------------------------------------------------------
        int x_start;
        int y_start;
        public int Fixed_X_Start = -1;
        public int Fixed_Y_Start = -1;

        int x_margin;
        int y_margin;

        int ctrl_h;
        int ctrl_w;

        List<DocumentAttribute> Attributes = new List<DocumentAttribute>();

        public bool IncludeComboChildren { get; set; } = true;
        //---------------------------------------------------------------------------------
        //bool _Search = false;
        //public bool Search
        //{
        //    get { return _Search; }
        //    set
        //    {
        //        _Search = value;
        //        populateGrid();
        //    }
        //}

        public bool Search { get; set; } = false;

        int _FolderId;
        public int FolderId
        {
            get { return _FolderId; }
            set
            {
                _FolderId = value;
            }
        }

        delegate void del_update();

        public ControlCollection AttributeControls
        {
            get { return this.pnlAttributes.Controls; }
        }

        public event KeyEventHandler KeyDown;


        bool _checkbox_three_state = false;
        public bool CheckBoxThreeState
        {
            get {
                return _checkbox_three_state;
            }
            set{
                _checkbox_three_state=value;
            }
        }
        //static public Dictionary<int, System.Collections.IEnumerable> ControlCaches { get; set; } = new Dictionary<int, System.Collections.IEnumerable>();

        //---------------------------------------------------------------------------------
        void CommonConstructor()
        {
            InitializeComponent();

            int height = (int)((this.Size.Height - pnlAttributes.Location.Y) * 0.98);
            pnlAttributes.Size = new System.Drawing.Size(pnlAttributes.Size.Width, height);
            pnlAttributes.AutoScroll = true;
            pnlAttributes.Anchor |= AnchorStyles.Bottom;
            pnlAttributes.Visible = true;
        }

        public AttributeSearch()
        {
            CommonConstructor();
        }


        //---------------------------------------------------------------------------------
        // Building this form -------------------------------------------------------------
        //---------------------------------------------------------------------------------
        public void populateGrid(params int[] DocTypeIds)
        {
            populateGrid(null, DocTypeIds, null);
        }

        public void populateGrid(List<DocumentAttribute> vals, params int[] DocTypeIds)
        {
            populateGrid(vals, DocTypeIds, null);
        }

        public void populateGrid(DocumentProperty doc)
        {
            FolderId = doc.id_folder;
            populateGrid(doc.Attrs, new int[] { doc.id_docType }, null);
        }

        public void populateGrid(List<DocumentAttribute> vals = null, int[] doctype_ids = null, int[] attr_ids = null)
        {
            attr_ids = attr_ids ?? new int[]{};
            doctype_ids = doctype_ids ??  new int[]{};

            pnlAttributes.Controls.Clear();

            List<int> all_attr_ids = new List<int>(attr_ids);

            if ( 0 < doctype_ids.Length )
            {
                all_attr_ids.AddRange(DocumentAttributeController.GetIdListByDocType(doc_type_id: doctype_ids).Select(x => x.id_attribute));

                // show no attributes if document type id is specified.
                if( all_attr_ids.Count == 0 ) return;
            }


            if (Search || (0 < all_attr_ids.Count))
            {
                Attributes = DocumentAttributeController.GetAttributesCache(true, attr_id: all_attr_ids.ToArray());

                foreach (DocumentAttribute Attribute in Attributes)
                {
                    // set value to attributes if parameter is passed
                    if (vals != null)
                    {
                        DocumentAttribute val = vals.Find(a => a.id == Attribute.id);
                        if ((val != null) && (val.IsValidValue()))
                        {
                            Attribute.atbValue = val.atbValue;

                            //Todo: search document value link
                            Attribute.LinkedAttr = DocumentAttributeController.GetLinkedAttribute(Attribute.id, val.atbValue_str);
                        }
                    }
                    else if (!Search && ((Attribute.id_type == en_AttrType.Date) || (Attribute.id_type == en_AttrType.DateTime)))
                    {
                        Attribute.atbValue = DateTime.Today;
                    }
                    //If search mode and current attribute is checkbox, three state is enable so that we know the user check on, off and did not touch.
                    else if (Attribute.id_type == en_AttrType.ChkBox) {
                        Attribute.atbValue = en_AttrCheckState.Intermidiate;
                        CheckBoxThreeState = true;
                    }
                }

                int x, y;

                InitPositionParams();

                pnlAttributes.SuspendLayout();

                List<Label> lables = new List<Label>();

                y = y_start;
                foreach (DocumentAttribute attr in Attributes)
                {
                    bool mandatory = false;

                    if (!Search)
                    {
                        DocumentAttributeParams param = attr.GetParamsByFolder(FolderId);
                        mandatory = param.required.GetBool();
                    }

                    lables.Add(addLabel(x_start, y, attr.name, attr.id, mandatory));
                    y += (ctrl_h + y_margin);
                }

				if (Attributes.Count == 0) return;

                x = x_start + lables.Max(a => a.Width) + x_margin;
                y = y_start;
                ctrl_w = (int)((this.Width - x) * 0.95);

                int i = 0;
                int height = 0;
                foreach (Label label in lables)
                {
                    switch (Attributes[i].id_type)
                    {
                        case en_AttrType.Text: //textbox
                        case en_AttrType.Label:
                            if ((Attributes[i].id_type == en_AttrType.Text) || Search)
                                height = AddTextBox(i, x, y, Attributes[i], false);
                            else
                                height = AddTextBox(i, x, y, Attributes[i], true);
                            break;

                        case en_AttrType.ChkBox: //checkbox
                            height = AddCheckBox(i, x, y, Attributes[i]);
                            break;

                        case en_AttrType.Date: //date
                        case en_AttrType.DateTime: //datetime
                            height = AddDateBox(i, x, y, Attributes[i]);
                            break;

                        case en_AttrType.Combo: //combobox
                        case en_AttrType.FixedCombo:
                        case en_AttrType.ComboSingleSelect:
                        case en_AttrType.FixedComboSingleSelect:
                            height = AddComboBox(i, x, y, Attributes[i]);
                            break;
                    }

                    label.Location = new Point(label.Location.X, y);

                    y += (height + y_margin);
                    i++;
                }

                if (AutoSize)
                    this.Height = y;

                pnlAttributes.ResumeLayout();
            }
        }
/*
        public void SetDefaultValue()
        {
            // Set default value
            for( int i = 0; i < this.AttributeControls.Count; i++)
            {
                Control ctr = this.AttributeControls[i];
                DocumentAttribute attr = getAttributeValue(ctr);

                if ((bool)ctr.Tag != true) continue;

                if( attr.id_type == en_AttrType.Date && string.IsNullOrWhiteSpace(attr.atbValue_str))
                {
                    attr.atbValue = DateTime.Today;
                    ctr.Text = ((DateTime)attr.atbValue).ToString(Spider.Common.ConstData.DATE);
                }
            }
        }
 */
        void InitPositionParams()
        {
            if (Fixed_X_Start < 0)
                x_start = (int)(font_size * 0.5);
            else
                x_start = Fixed_X_Start;

            if (Fixed_Y_Start < 0)
                y_start = (int)(font_size * 0.5);
            else
                y_start = Fixed_Y_Start;

            x_margin = (int)(font_size * 0.5);
            y_margin = (int)(font_size * 0.3);

            ctrl_h = (int)(font_size * 1.8);
        }

        //---------------------------------------------------------------------------------
        Label addLabel(int x, int y, string label, int id, bool mandatory)
        {
            Label lbl_sample = new Label();
            lbl_sample.AutoEllipsis = true;
            lbl_sample.Name = "lbl_" + id;
            lbl_sample.Location = new Point(x, y);
            lbl_sample.AutoSize = true;
            lbl_sample.MaximumSize = new Size(0, ctrl_h);
            lbl_sample.MinimumSize = new Size(0, ctrl_h);
            lbl_sample.TextAlign = ContentAlignment.MiddleLeft;
            lbl_sample.Text = label + ":" + (mandatory == true ? "*" : "");
            lbl_sample.Visible = true;
            this.pnlAttributes.Controls.Add(lbl_sample);

            return lbl_sample;
        }

        //---------------------------------------------------------------------------------
        int AddTextBox(int i, int x, int y, DocumentAttribute attr, bool label_apparence)
        {
            AttributeTextBox textbox = new AttributeTextBox(ctrl_h, ctrl_w, font_size, FolderId, Search, attr, label_apparence);
            textbox.Location = new Point(x, y);
            textbox.TabIndex = i;
            textbox.textbox.KeyDown += textbox_KeyDown;

            pnlAttributes.Controls.Add(textbox);

            return textbox.Height;
        }

        void textbox_KeyDown(object sender, KeyEventArgs e)
        {
            // hit enter key
            if (e.KeyValue == 13)
            {
                if (this.KeyDown != null)
                {
                    this.KeyDown(this, e);
                }
            }
        }

        //---------------------------------------------------------------------------------
        int AddCheckBox(int i, int x, int y, DocumentAttribute attr)
        {
            CheckBox ck_sample = new CheckBox();

            //ck_sample.ThreeState = false;
            ck_sample.ThreeState = this.CheckBoxThreeState;
            ck_sample.Height = ctrl_h;
            ck_sample.TabIndex = i;
            ck_sample.AutoSize = true;
            ck_sample.BackColor = Color.Transparent;
            ck_sample.ForeColor = Color.White;
            ck_sample.Location = new Point(x, y + 3);
            ck_sample.Name = "ck_" + attr.id;
            ck_sample.Text = "";
            ck_sample.UseVisualStyleBackColor = true;
            ck_sample.Checked = false;
            ck_sample.Tag = attr.GetParamsByFolder(FolderId)?.required.GetBool(); // Tag is used for mandatory check

            if ((en_AttrCheckState)attr.atbValue == en_AttrCheckState.Intermidiate)
                ck_sample.CheckState = CheckState.Indeterminate;
            else if ((en_AttrCheckState)attr.atbValue == en_AttrCheckState.True)
                ck_sample.Checked = true;

            pnlAttributes.Controls.Add(ck_sample);

            return ck_sample.Height;
        }

        //---------------------------------------------------------------------------------
        int AddDateBox(int i, int x, int y, DocumentAttribute attr)
        {
            int ans = 0;
            int AddCnt = (Search ? 2 : 1);
            int date_w = (ctrl_w - x_margin) / AddCnt;
            int x_val = x;

            for (int j = 0; j < AddCnt; j++)
            {
                MaskedTextBox dt_sample = new MaskedTextBox();

                dt_sample.TabIndex = i;
                dt_sample.Location = new Point(x_val, y);
                dt_sample.Mask = "00/00/0000";
                dt_sample.Width = date_w;
                dt_sample.Height = ctrl_h;
                dt_sample.ValidatingType = typeof(System.DateTime);
                dt_sample.Click += new System.EventHandler(dt_sample_Click);
                dt_sample.LostFocus += new System.EventHandler(dt_sample_LostFocus);
                dt_sample.Tag = attr.GetParamsByFolder(FolderId)?.required.GetBool(); // Tag is used for mandatory check

                if (Search)
                {
                    dt_sample.Name = "dt_" + attr.id + "_" + (j + 1);

                    if (!String.IsNullOrEmpty(attr.atbValue_str))
                    {
                        string[] texts = attr.atbValue_str.Split(',');
                        if (j < texts.Length)
                            dt_sample.Text = texts[j];
                    }

                }
                else
                {
                    dt_sample.Name = "dt_" + attr.id;
                    dt_sample.Text = attr.atbValue_str;
                }

                pnlAttributes.Controls.Add(dt_sample);
                ans = dt_sample.Height;

                x_val += (dt_sample.Width + x_margin);
            }

            return ans;
        }

        //---------------------------------------------------------------------------------
        int AddComboBox(int idx, int x, int y, DocumentAttribute attr)
        {
            bool MultiSelectable = true;

            if ((attr.id_type == en_AttrType.ComboSingleSelect) || (attr.id_type == en_AttrType.FixedComboSingleSelect))
                MultiSelectable = false;


            //List<DocumentAttributeCombo> source = new List<DocumentAttributeCombo>();


            //if (ControlCaches.FindIndex(ctrl => ctrl.Key == attr.id) == -1)
            //{
            //    source = DocumentAttributeController.GetComboItems(true, attr.id);
            //    //if(IncludeComboChildren )
            //    //    source =  DocumentAttributeController.GetComboItems(true, attr.id);
            //    //else
            //    //    source = DocumentAttributeController.GetComboItems(sort:true, attr_ids:new int[] { attr.id },value_filter:null, ids:null, include_children:false);

            //    ControlCaches.Add(attr.id, source);
            //}


            AttributeComboBox cbo_sample_ck = new AttributeComboBox(attr, Search, MultiSelectable);
            //cbo_sample_ck.DataSource = ControlCaches[attr.id] as List<DocumentAttributeCombo>;
            cbo_sample_ck.DataSource = Cache.GetComboItems(attr.id).OrderBy(item => item.text).ToList();
            cbo_sample_ck.Bind();

            //AttributeComboBox cbo_sample_ck = new AttributeComboBox(attr, Search, MultiSelectable);
            cbo_sample_ck.cboComboBox.KeyDown += textbox_KeyDown;
            cbo_sample_ck.TabIndex = idx;
            cbo_sample_ck.Location = new Point(x, y);
            cbo_sample_ck.Name = "cbo_" + attr.id;
            cbo_sample_ck.Width = ctrl_w;
            cbo_sample_ck.Anchor |= AnchorStyles.Right;
            cbo_sample_ck.Tag = attr.GetParamsByFolder(FolderId)?.required.GetBool(); // Tag is used for mandatory check. Todo: Improve performance


            pnlAttributes.Controls.Add(cbo_sample_ck);

            return cbo_sample_ck.Height;
        }

        private void copyControl(Control sourceControl, Control targetControl)
        {
            // make sure these are the same
            if (sourceControl.GetType() != targetControl.GetType())
            {
                throw new Exception("Incorrect control types");
            }

            foreach (PropertyInfo sourceProperty in sourceControl.GetType().GetProperties())
            {
                object newValue = sourceProperty.GetValue(sourceControl, null);

                MethodInfo mi = sourceProperty.GetSetMethod(true);
                if (mi != null)
                {
                    sourceProperty.SetValue(targetControl, newValue, null);
                }
            }
        }
        //---------------------------------------------------------------------------------
        // Public methods for UI ----------------------------------------------------------
        //---------------------------------------------------------------------------------
        public void ClearPanel()
        {
            if (pnlAttributes != null)
                pnlAttributes.Controls.Clear();
        }

        //---------------------------------------------------------------------------------
        public void updateNow()
        {
            BeginInvoke(new del_update(del_updateFunc));
        }

        void del_updateFunc()
        {
            Update();
        }

        //---------------------------------------------------------------------------------
        // UI events ----------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        void dt_sample_Click(object sender, EventArgs e)
        {
            MaskedTextBox mask = ((MaskedTextBox)sender);
            MonthCalendar cld = (MonthCalendar)pnlAttributes.Controls["calendar_" + mask.Name];

            if (cld == null)
            {
                MonthCalendar calendar = new MonthCalendar();
                calendar.Location = new Point(33, mask.Location.Y + 18);
                calendar.Name = "calendar_" + mask.Name;
                calendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(calendar_DateSelected);
                pnlAttributes.Controls.Add(calendar);
                calendar.BringToFront();

            }
            else
            {
                pnlAttributes.Controls.RemoveByKey("calendar_" + mask.Name);
            }
        }

        //---------------------------------------------------------------------------------
        void dt_sample_LostFocus(object sender, System.EventArgs e)
        {
            try
            {
                MaskedTextBox mask = ((MaskedTextBox)sender);
                MonthCalendar cld = (MonthCalendar)pnlAttributes.Controls["calendar_" + mask.Name];

                if (cld != null)
                {
                    if ((cld.ContainsFocus) == false)
                        pnlAttributes.Controls.RemoveByKey("calendar_" + mask.Name);
                }
            }
            catch { }
        }

        //---------------------------------------------------------------------------------
        void calendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            string dd, mm, yyyy, date;
            MonthCalendar calendar = (MonthCalendar)sender;

            dd = calendar.SelectionStart.Day.ToString();
            mm = calendar.SelectionStart.Month.ToString();
            yyyy = calendar.SelectionStart.Year.ToString();

            if (calendar.SelectionStart.Day.ToString().Length == 1)
                dd = "0" + calendar.SelectionStart.Day.ToString();

            if (calendar.SelectionStart.Month.ToString().Length == 1)
                mm = "0" + calendar.SelectionStart.Month.ToString();

            date = dd + mm + yyyy;

            MaskedTextBox maskTxt = (MaskedTextBox)pnlAttributes.Controls["dt_" + calendar.Name.ToString().Replace("calendar_dt_", "")];
            maskTxt.Text = date;

            pnlAttributes.Controls.Remove(calendar);
        }

        //---------------------------------------------------------------------------------
        // Public methods -----------------------------------------------------------------
        //---------------------------------------------------------------------------------
        public List<DocumentAttribute> getAttributeValues()
        {
            List<DocumentAttribute> ans = new List<DocumentAttribute>();

            foreach (Control control in pnlAttributes.Controls)
            {
                DocumentAttribute wrk = getAttributeValue(control);

                if ((wrk != null) && wrk.IsValidValue())
                    ans.Add(wrk);
            }

            return ans;
        }

        //---------------------------------------------------------------------------------
        DocumentAttribute getAttributeValue(Control control)
        {
            DocumentAttribute wrk = null;
            int id = 0;

            if (control is AttributeTextBox)
            {
                if (((AttributeTextBox)control).Name.Trim() != "")
                    id = int.Parse(control.Name.Replace("txt_", ""));

            }
            else if (control is CheckBox)
            {
                id = int.Parse(control.Name.Replace("ck_", ""));

            }
            else if (control is MaskedTextBox)
            {
                id = int.Parse(Regex.Replace(control.Name, @"dt_([0-9]+)_*.*", "$1"));

            }
            else if (control is AttributeComboBox)
            {
                id = int.Parse(control.Name.Replace("cbo_", ""));
            }

            if (0 < id)
            {
                wrk = Attributes.First(a => a.id == id);

                if (control is AttributeTextBox)
                {
                    wrk.atbValue = ((AttributeTextBox)control).textbox.Text;

                    if (((AttributeTextBox)control).label_apparence)
                        wrk.id_type = en_AttrType.Label;
                    else
                        wrk.id_type = en_AttrType.Text;

                }
                else if (control is CheckBox)
                {
                    if (((CheckBox)control).CheckState == CheckState.Indeterminate)
                        wrk.atbValue = en_AttrCheckState.Intermidiate;
                    else
                        wrk.atbValue = (((CheckBox)control).Checked);

                    wrk.id_type = en_AttrType.ChkBox;

                }
                else if (control is MaskedTextBox)
                {
                    if (Search)
                    {
                        wrk.id_type = en_AttrType.DatePeriod;
                        if (((MaskedTextBox)control).Name.Substring(((MaskedTextBox)control).Name.Length - 2, 2).ToString() == "_1")
                        {
                            Period period = new Period();

                            if (((MaskedTextBox)control).Text != Library.INVALID_DATE)
                                period.SetDateFromString(from: ((MaskedTextBox)control).Text);

                            MaskedTextBox dtEnd = (MaskedTextBox)pnlAttributes.Controls["dt_" + wrk.id.ToString() + "_2"];
                            if (dtEnd.Text != Library.INVALID_DATE)
                                period.SetDateFromString(to: dtEnd.Text);

                            if (period.IsFromOrToExists)
                                wrk.atbValue = period;

                        }
                        else
                        {
                            wrk = null;
                        }

                    }
                    else
                    {
                        wrk.id_type = en_AttrType.Date;
                        if (((MaskedTextBox)control).Text != Library.INVALID_DATE)
                            wrk.atbValue = DateTime.Parse(((MaskedTextBox)control).Text);
                        else wrk = null;
                    }

                }
                else if (control is AttributeComboBox)
                {
                    wrk = Attributes.First(a => a.id == id);
                    wrk.id_type = ((AttributeComboBox)control).m_attr.id_type;
                    wrk.atbValue = ((AttributeComboBox)control).GetSelectedComboItems();
                }
            }

            return wrk;
        }

        //---------------------------------------------------------------------------------
        public List<DocumentAttribute> getAttributeValuesCopy()
        {
            List<DocumentAttribute> wrk = getAttributeValues();
            return DocumentAttribute.Clone(wrk);
        }

        //---------------------------------------------------------------------------------
        public bool checkMandadoryAttributes(bool show_msg)
        {
            bool ans = true;
            bool FirstError = true;

            foreach (Control control in pnlAttributes.Controls)
            {
                if ((control.Tag != null) && (bool)control.Tag)
                {
                    FormUtilities.PutErrorColour(control, false, true);

                    DocumentAttribute attribute = getAttributeValue(control);

                    if ((attribute != null) && !attribute.IsValidValue())
                    {
                        FormUtilities.PutErrorColour(control, ans);
                        ans = false;

                        if (FirstError)
                        {
                            string StrId = "";
                            bool DateFormatErr = false;

                            FirstError = false;
                            control.Focus();

                            if (control is AttributeTextBox)
                            {
                                StrId = control.Name.Replace("txt_", "");

                            }
                            else if (control is CheckBox)
                            {
                                StrId = control.Name.Replace("ck_", "");

                            }
                            else if (control is MaskedTextBox)
                            {
                                StrId = control.Name.Replace("dt_", "");

                                if (((MaskedTextBox)control).Text != Library.INVALID_DATE) //check if the date informed is corret
                                    DateFormatErr = true;

                            }
                            else if (control is AttributeComboBox)
                            {
                                StrId = control.Name.Replace("cbo_", "");

                            }

                            Label lbl = (Label)pnlAttributes.Controls["lbl_" + StrId];
                            if (DateFormatErr)
                                MessageBox.Show("The data you supplied must be a valid date in the format dd/mm/yyyy.", "Incorret format date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                MessageBox.Show("The attribute " + lbl.Text.Replace(":", "").ToUpper() + " is required.", "Attribute not informed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }

            return ans;
        }

        //---------------------------------------------------------------------------------
        class AttributeTextBox : Panel
        {
            public bool label_apparence;
            public TextBox textbox = new TextBox();
            public AttributeSearch linkLabel;

            public AttributeTextBox(int height, int width, int font_size, int FolderId, bool Search, DocumentAttribute attr, bool label_apparence)
            {
                this.label_apparence = label_apparence;
                this.Name = "txt_" + attr.id;
                this.Height = height;
                this.Width = width;
                this.Anchor |= AnchorStyles.Right;

                textbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
                textbox.Width = this.Width;
                textbox.Top = (int)(((double)this.Height - (double)textbox.Height) / (double)2);
                textbox.MaxLength = attr.max_lengh;
                textbox.Anchor |= AnchorStyles.Right;
                textbox.Text = attr.atbValue_str;
                //textbox.Tag = attr.GetParamsByFolder(FolderId).required.GetBool(); // Tag is used for mandatory check
                this.Tag = attr.GetParamsByFolder(FolderId)?.required.GetBool(); // Tag is used for mandatory check

                if (!Search && attr.only_numbers)
                    textbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textbox_sample_KeyPress);

                if (label_apparence)
                {
                    textbox.ReadOnly = true;
                    textbox.BackColor = Color.LightGray;
                    textbox.TabStop = false;
                    textbox.Left = (int)((double)font_size * 0.3);

                }
                else
                {
                    this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                }

                this.BackColor = textbox.BackColor;
                this.Controls.Add(textbox);

                /* Added Attribute Children*/
                if(attr.LinkedAttr != null)
                {
                    linkLabel = new AttributeSearch();

                    linkLabel.Width = this.Width;


                    linkLabel.Fixed_X_Start = 0;
                    linkLabel.Fixed_Y_Start = 0;
                    linkLabel.Top = this.Top + this.Height;
                    linkLabel.Anchor |= AnchorStyles.Right;
                    linkLabel.AutoSize = true;
                    linkLabel.Text = attr.LinkedAttr.atbValue_str;
                    linkLabel.Left -= 3;

                    if (!Search)
                    {
                        this.Controls.Add(linkLabel);
                    }
                    else
                    {
                        linkLabel.Visible = false;

                    }

                    linkLabel.populateGrid( new DocumentAttribute[] { attr.LinkedAttr }.ToList(), null, new int[] { attr.LinkedAttr.id });

                    this.Height = this.Height + linkLabel.Height;
                }
            }
            void textbox_sample_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
                    e.Handled = true;
            }
        }

        //---------------------------------------------------------------------------------
    }
}
