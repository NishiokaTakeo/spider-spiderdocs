using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using SpiderDocsModule;
namespace SpiderCustomComponent
{

    public class CheckComboBox : ComboBox
    {
        /// <summary>
        /// Internal class to represent the dropdown list of the CheckComboBox
        /// </summary>
        internal class Dropdown : Form
        {
            // ---------------------------------- internal class CCBoxEventArgs --------------------------------------------
            /// <summary>
            /// Custom EventArgs encapsulating value as to whether the combo box value(s) should be assignd to or not.
            /// </summary>
            internal class CCBoxEventArgs : EventArgs
            {
                private bool assignValues;
                public bool AssignValues
                {
                    get { return assignValues; }
                    set { assignValues = value; }
                }
                private EventArgs e;
                public EventArgs EventArgs
                {
                    get { return e; }
                    set { e = value; }
                }
                public CCBoxEventArgs(EventArgs e, bool assignValues)
                    : base()
                {
                    this.e = e;
                    this.assignValues = assignValues;
                }
            }

            // ---------------------------------- internal class CustomCheckedListBox --------------------------------------------

            /// <summary>
            /// A custom CheckedListBox being shown within the dropdown form representing the dropdown list of the CheckComboBox.
            /// </summary>
            internal class CustomCheckedListBox : CheckedListBox
            {
                private int curSelIndex = -1;
                public int itemMaxWidth
                {
                    get;
                    set;
                }
                public CustomCheckedListBox()
                    : base()
                {
                    this.SelectionMode = SelectionMode.One;
                    this.HorizontalScrollbar = true;
                    DoubleBuffered = true;
                    DrawMode = DrawMode.OwnerDrawFixed;
                }

                protected override void OnDrawItem(DrawItemEventArgs e)
                {
                    // return If not item is 
                    if (this.Items.Count <= 0)
                        return;

                    Font font = new Font(Font.FontFamily, 9, FontStyle.Regular);


                    string text = System.Text.RegularExpressions.Regex.Replace(Items[e.Index].ToString(), @"\s|\t|\r\n|\r|\n", " ");

                    Size checkSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, System.Windows.Forms.VisualStyles.CheckBoxState.MixedNormal);
                    int dx = (e.Bounds.Height + 2 - checkSize.Width + 2) / 2;
                    e.DrawBackground();
                    bool isChecked = GetItemChecked(e.Index);//For some reason e.State doesn't work so we have to do this instead.
                    CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(dx, e.Bounds.Top + dx -2), isChecked ? System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal : System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
                    using (StringFormat sf = new StringFormat { LineAlignment = StringAlignment.Center })
                    {
                        using (Brush brush = new SolidBrush(ForeColor))
                        {
                            e.Graphics.DrawString(text, font, brush, new Rectangle(e.Bounds.Height, e.Bounds.Top, e.Bounds.Width + 100, e.Bounds.Height), sf);
                        }
                    }
                }


                public override int ItemHeight
                {
                    get
                    {
                        return Font.Height + 10;
                    }
                    set { }
                }

                /// <summary>
                /// Intercepts the keyboard input, [Enter] confirms a selection and [Esc] cancels it.
                /// </summary>
                /// <param name="e">The Key event arguments</param>
                protected override void OnKeyDown(KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        // Enact selection.
                        ((CheckComboBox.Dropdown)Parent).OnDeactivate(new CCBoxEventArgs(null, true));
                        e.Handled = true;

                    }
                    else if (e.KeyCode == Keys.Escape)
                    {
                        // Cancel selection.
                        ((CheckComboBox.Dropdown)Parent).OnDeactivate(new CCBoxEventArgs(null, false));
                        e.Handled = true;

                    }
                    else if (e.KeyCode == Keys.Delete)
                    {
                        // Delete unckecks all, [Shift + Delete] checks all.
                        for (int i = 0; i < Items.Count; i++)
                        {
                            SetItemChecked(i, e.Shift);
                        }
                        e.Handled = true;
                    }
                    // If no Enter or Esc keys presses, let the base class handle it.
                    base.OnKeyDown(e);
                }

                protected override void OnMouseMove(MouseEventArgs e)
                {
                    base.OnMouseMove(e);
                    int index = IndexFromPoint(e.Location);
                    if ((index >= 0) && (index != curSelIndex))
                    {
                        curSelIndex = index;
                        SetSelected(index, true);
                    }
                }

            } // end internal class CustomCheckedListBox

            // --------------------------------------------------------------------------------------------------------

            // ********************************************* Data *********************************************

            private CheckComboBox ccbParent;

            // Keeps track of whether checked item(s) changed, hence the value of the CheckComboBox as a whole changed.
            // This is simply done via maintaining the old string-representation of the value(s) and the new one and comparing them!
            private string oldStrValue = "";
            public bool ValueChanged
            {
                get
                {
                    string newStrValue = ccbParent.Text;
                    if ((oldStrValue.Length > 0) && (newStrValue.Length > 0))
                    {
                        return (oldStrValue.CompareTo(newStrValue) != 0);
                    }
                    else
                    {
                        return (oldStrValue.Length != newStrValue.Length);
                    }
                }
            }

            // Array holding the checked states of the items. This will be used to reverse any changes if user cancels selection.
            bool[] checkedStateArr;

            // Whether the dropdown is closed.
            private bool dropdownClosed = true;

            private CustomCheckedListBox cclb;
            public CustomCheckedListBox List
            {
                get { return cclb; }
                set { cclb = value; }
            }

            // ********************************************* Construction *********************************************

            public Dropdown(CheckComboBox ccbParent)
            {
                this.ccbParent = ccbParent;
                InitializeComponent();
                this.ShowInTaskbar = false;
                // Add a handler to notify our parent of CheckBoxCheckedChanged events.
                this.cclb.ItemCheck += this.cclb_ItemCheck;
                this.cclb.SelectedIndexChanged += Cclb_SelectedIndexChanged;
                this.cclb.CheckOnClick = true;
                
            }

            // ********************************************* Methods *********************************************

            // Create a CustomCheckedListBox which fills up the entire form area.
            private void InitializeComponent()
            {
                this.cclb = new CustomCheckedListBox();
                this.SuspendLayout();
                // 
                // cclb
                // 
                this.cclb.BorderStyle = System.Windows.Forms.BorderStyle.None;
                this.cclb.Dock = System.Windows.Forms.DockStyle.Fill;
                this.cclb.FormattingEnabled = true;
                this.cclb.Location = new System.Drawing.Point(0, 0);
                this.cclb.Name = "cclb";
                this.cclb.Size = new System.Drawing.Size(47, 15);
                this.cclb.TabIndex = 0;
                
                // 
                // Dropdown( Form )
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
                this.BackColor = System.Drawing.SystemColors.Menu;
                this.ClientSize = new System.Drawing.Size(47, 16);
                this.ControlBox = false;
                this.Controls.Add(this.cclb);
                this.ForeColor = System.Drawing.SystemColors.ControlText;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                this.MinimizeBox = false;
                this.Name = "ccbParent";
                this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                this.ResumeLayout(false);                
            }


            /// <summary>
            /// Closes the dropdown portion and enacts any changes according to the specified boolean parameter.
            /// NOTE: even though the caller might ask for changes to be enacted, this doesn't necessarily mean
            ///       that any changes have occurred as such. Caller should check the ValueChanged property of the
            ///       CheckComboBox (after the dropdown has closed) to determine any actual value changes.
            /// </summary>
            /// <param name="enactChanges"></param>
            public void CloseDropdown(bool enactChanges)
            {
                if (dropdownClosed)
                {
                    return;
                }
                // Perform the actual selection and display of checked items.
                if (enactChanges)
                {
                    ccbParent.SelectedIndex = -1;
                    // Set the text portion equal to the string comprising all checked items (if any, otherwise empty!).
                    //ccbParent.Text = GetCheckedItemsStringValue();
                }
                else
                {
                    // Caller cancelled selection - need to restore the checked items to their original state.
                    for (int i = 0; i < cclb.Items.Count; i++)
                    {
                        cclb.SetItemChecked(i, checkedStateArr[i]);
                    }
                }
                // From now on the dropdown is considered closed. We set the flag here to prevent OnDeactivate() calling
                // this method once again after hiding this window.
                dropdownClosed = true;
                // Set the focus to our parent CheckComboBox and hide the dropdown check list.
                ccbParent.Focus();
                this.Hide();
                // Notify CheckComboBox that its dropdown is closed. (NOTE: it does not matter which parameters we pass to
                // OnDropDownClosed() as long as the argument is CCBoxEventArgs so that the method knows the notification has
                // come from our code and not from the framework).
                ccbParent.OnDropDownClosed(new CCBoxEventArgs(null, false));
            }

            protected override void OnActivated(EventArgs e)
            {
                base.OnActivated(e);
                dropdownClosed = false;
                // Assign the old string value to compare with the new value for any changes.
                oldStrValue = ccbParent.Text;
                // Make a copy of the checked state of each item, in cace caller cancels selection.
                checkedStateArr = new bool[cclb.Items.Count];
                for (int i = 0; i < cclb.Items.Count; i++)
                {
                    checkedStateArr[i] = cclb.GetItemChecked(i);
                }
                
            }

            protected override void OnDeactivate(EventArgs e)
            {
                base.OnDeactivate(e);
                CCBoxEventArgs ce = e as CCBoxEventArgs;
                if (ce != null)
                {
                    CloseDropdown(ce.AssignValues);

                }
                else
                {
                    // If not custom event arguments passed, means that this method was called from the
                    // framework. We assume that the checked values should be registered regardless.
                    CloseDropdown(true);
                }
            }

            private void cclb_ItemCheck(object sender, ItemCheckEventArgs e)
            {
                if (!ccbParent.MultiSelectable)
                {
                    for (int ix = 0; ix < ccbParent.Items.Count; ++ix)
                        if (ix != e.Index) ccbParent.SetItemChecked(ix, false);
                }
            }

            private void Cclb_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (ccbParent.CheckBoxCheckedChanged != null)
                {
                    ccbParent.CheckBoxCheckedChanged(sender, e);
                }
            }

        } // end internal class Dropdown



        public string GetCheckedItemsStringValue()
        {
            string ListText = String.Empty;
            int StartIndex = 0;

            for (int Index = StartIndex; Index < CheckedItems.Count; Index++)
            {
                DocumentAttributeCombo Item = CheckedItems[Index] as DocumentAttributeCombo;

                ListText += string.IsNullOrEmpty(ListText) ? "\"" + Item.text + "\"" : String.Format(this.ValueSeparator + " \"{0}\"", Item.text);
            }

            return ListText;
        }

        // ******************************** Data ********************************
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        // A form-derived object representing the drop-down list of the checked combo box.
        private Dropdown dropdown;

        public bool MultiSelectable { get; set; }

        // The valueSeparator character(s) between the ticked elements as they appear in the 
        // text portion of the CheckComboBox.
        private string valueSeparator;
        public string ValueSeparator
        {
            get { return valueSeparator; }
            set { valueSeparator = value; }
        }

        public bool CheckOnClick
        {
            get { return dropdown.List.CheckOnClick; }
            set { dropdown.List.CheckOnClick = value; }
        }

        public new string DisplayMember
        {
            get { return dropdown.List.DisplayMember; }
            set { dropdown.List.DisplayMember = value; }
        }

        public new CheckedListBox.ObjectCollection Items
        {
            get { return dropdown.List.Items; }
        }

        public CheckedListBox.CheckedItemCollection CheckedItems
        {
            get { return dropdown.List.CheckedItems; }
        }

        public CheckedListBox.CheckedIndexCollection CheckedIndices
        {
            get { return dropdown.List.CheckedIndices; }
        }

        public bool ValueChanged
        {
            get { return dropdown.ValueChanged; }
        }

        // Event handler for when an item check state changes.
        public event EventHandler CheckBoxCheckedChanged;
        //public EventHandler CheckBoxCheckedChanged;

        public string SelectedItemsText
        {
            get
            {
                return GetCheckedItemsStringValue();

            }
        }
        public Control DropDownControl {
            get {
                return this.dropdown;
            }
        }
        // ******************************** Construction ********************************
        public List<T> getComboValue<T>()
        {

            List<T> vals = new List<T>();

            if (CheckedItems.Count > 0)
            {
                for (int i = 0; i < CheckedItems.Count; i++)
                {
                    vals.Add((T)CheckedItems[i]);
                }
            }

            return vals;
        }

        public CheckComboBox()
            : base()
        {
            // We want to do the drawing of the dropdown.
            this.DrawMode = DrawMode.OwnerDrawVariable;
            // Default value separator.
            this.valueSeparator = ", ";
            // This prevents the actual ComboBox dropdown to show, although it's not strickly-speaking necessary.
            // But including this remove a slight flickering just before our dropdown appears (which is caused by
            // the empty-dropdown list of the ComboBox which is displayed for fractions of a second).
            this.DropDownHeight = 1;
            // This is the default setting - text portion is editable and user must click the arrow button
            // to see the list portion. Although we don't want to allow the user to edit the text portion
            // the DropDownList style is not being used because for some reason it wouldn't allow the text
            // portion to be programmatically set. Hence we set it as editable but disable keyboard input (see below).
            this.DropDownStyle = ComboBoxStyle.DropDown;
            this.dropdown = new Dropdown(this);
            // CheckOnClick style for the dropdown (NOTE: must be set after dropdown is created).
            this.CheckOnClick = true;

            MultiSelectable = true;
            //this.MaxDropDownItems = 50;
            //this.dropdown.List.Select();
        }

        // ******************************** Operations ********************************
        public void Clear()
        {
            Items.Clear();
        }

        public void AddItem(Object o, bool is_selected = false)
        {
            Items.Add(o);
            SetItemChecked((Items.Count - 1), is_selected);

            this.Text = GetCheckedItemsStringValue();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnDropDown(EventArgs e)
        {
            
            DoDropDown(e);
        }

        private void DoDropDown(EventArgs e)
        {

            if (!dropdown.Visible)
            {
                int maxwidth = 150;
                Rectangle rect = RectangleToScreen(this.ClientRectangle);
                dropdown.Location = new Point(rect.X, rect.Y + this.Size.Height);
                int count = dropdown.List.Items.Count;
                if (count > this.MaxDropDownItems)
                {
                    count = this.MaxDropDownItems;
                }
                else if (count == 0)
                {
                    count = 1;
                }

                var e2 = this.dropdown.List.Items.GetEnumerator();

                while (e2.MoveNext())
                {
                    int cur = (e2.Current.ToString().Length * 9 /* 9 px */) + 10 + 4; // text width + checkbox width + 
                    if (maxwidth < cur)
                        maxwidth = cur;
                };

                dropdown.Size = new Size(maxwidth, (dropdown.List.ItemHeight) * count + 2);

                dropdown.Show(this);

                //dropdown.ActiveControl = this.dropdown.List.contr
            }

            base.OnDropDown(e);

        }

        protected override void OnDropDownClosed(EventArgs e)
        {
            // Call the handlers for this event only if the call comes from our code - NOT the framework's!
            // NOTE: that is because the events were being fired in a wrong order, due to the actual dropdown list
            //       of the ComboBox which lies underneath our dropdown and gets involved every time.
            if (e is Dropdown.CCBoxEventArgs)
            {
                base.OnDropDownClosed(e);
            }
            this.Text = GetCheckedItemsStringValue();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                // Signal that the dropdown is "down". This is required so that the behaviour of the dropdown is the same
                // when it is a result of user pressing the Down_Arrow (which we handle and the framework wouldn't know that
                // the list portion is down unless we tell it so).
                // NOTE: all that so the DropDownClosed event fires correctly!                
                OnDropDown(null);
            }
            // Make sure that certain keys or combinations are not blocked.
            e.Handled = !e.Alt && !(e.KeyCode == Keys.Tab) &&
                !((e.KeyCode == Keys.Left) || (e.KeyCode == Keys.Right) || (e.KeyCode == Keys.Home) || (e.KeyCode == Keys.End));

            base.OnKeyDown(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            e.Handled = true;
            base.OnKeyPress(e);
        }
    
        public void SetItemChecked(int index, bool isChecked)
        {
            if (index < 0 || index > Items.Count)
            {
                throw new ArgumentOutOfRangeException("index", "value out of range");
            }
            else
            {
                dropdown.List.SetItemChecked(index, isChecked);
                ((DocumentAttributeCombo)Items[index]).Selected = isChecked;
            }

            this.Text = GetCheckedItemsStringValue();
        }

        public void SetDropDownWidth(int p)
        {
            //throw new NotImplementedException();
            this.dropdown.Width = p;
        }

        public void ClearSelection()
        {
            for (int i = 0; i < this.dropdown.List.Items.Count; ++i)
                SetItemChecked(i, false);

            this.Text = GetCheckedItemsStringValue();

        }
    } // end public class CheckComboBox

}
