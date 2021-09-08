using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpiderDocsForms
{
//---------------------------------------------------------------------------------
	public delegate void CheckBoxClickedHandler(bool state);

//---------------------------------------------------------------------------------
	public class DatagridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
	{
		const int PosOffset = 3;

		Point checkBoxLocation;
		Size checkBoxSize;
		Point cellLocation = new Point();
		System.Windows.Forms.VisualStyles.CheckBoxState cbState;
		public event CheckBoxClickedHandler OnCheckBoxClicked;
		object trueVal;
		object falseVal;

//---------------------------------------------------------------------------------	
		new public bool ReadOnly
		{
			get { return _Enabled; }
			set	{ Enabled = value; }
		}

//---------------------------------------------------------------------------------
		bool _Enabled = false;
		public bool Enabled
		{
			get { return _Enabled; }
			set
			{
				_Enabled = value;
				Set_cbState();
				Update();
			}
		}

//---------------------------------------------------------------------------------
		bool _Checked = false;
		public bool Checked
		{
			get { return _Checked; }
			set
			{
				_Checked = value;
				Set_cbState();
				Update();
			}
		}

//---------------------------------------------------------------------------------
		HorizontalAlignment _Align = HorizontalAlignment.Center;
		public HorizontalAlignment Align
		{
			get { return _Align; }
			set
			{
				_Align = value;
				Update();
			}
		}

//---------------------------------------------------------------------------------
		public DatagridViewCheckBoxHeaderCell(object t, object f)
		{
			trueVal = t;
			falseVal = f;
		}

//---------------------------------------------------------------------------------
		void Update()
		{
			if(this.DataGridView != null)
				this.DataGridView.InvalidateCell(this);
		}

//---------------------------------------------------------------------------------
		void Set_cbState()
		{
			int flag = 0;

			if(this.Enabled)	flag |= 0x01;	// 01 Set rectangle appearance.
			if(this.Checked)	flag |= 0x02;	// 10 Set check state.
			
			switch(flag)
			{
			case 0x00:	// 00
				cbState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedDisabled;
				break;

			case 0x01:	// 01
				cbState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
				break;

			case 0x02:	// 10
				cbState = System.Windows.Forms.VisualStyles.CheckBoxState.CheckedDisabled;
				break;

			case 0x03:	// 11
				cbState = System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal;
				break;
			}
		}

//---------------------------------------------------------------------------------
		protected override void Paint(System.Drawing.Graphics graphics, 
			System.Drawing.Rectangle clipBounds, 
			System.Drawing.Rectangle cellBounds, 
			int rowIndex, 
			DataGridViewElementStates dataGridViewElementState, 
			object value, 
			object formattedValue, 
			string errorText, 
			DataGridViewCellStyle cellStyle, 
			DataGridViewAdvancedBorderStyle advancedBorderStyle, 
			DataGridViewPaintParts paintParts)
		{
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, 
				dataGridViewElementState, value, 
				formattedValue, errorText, cellStyle, 
				advancedBorderStyle, paintParts);
			Point p = new Point();
			Size s = CheckBoxRenderer.GetGlyphSize(graphics, cbState);

			cbPosCalc(ref p ,s ,cellBounds);

			cellLocation = cellBounds.Location;
			checkBoxLocation = p;
			checkBoxSize = s;
			CheckBoxRenderer.DrawCheckBox
			(graphics, checkBoxLocation, cbState);
		}

//---------------------------------------------------------------------------------
		void cbPosCalc(ref Point p, Size s, System.Drawing.Rectangle cellBounds)
		{
			switch(Align)
			{
			default:
			case HorizontalAlignment.Center:
				p.X = cellBounds.Location.X + (cellBounds.Width / 2) - (s.Width / 2) ;
				break;

			case HorizontalAlignment.Left:
				p.X = cellBounds.Location.X  + s.Width + PosOffset;
				break;

			case HorizontalAlignment.Right:
				p.X = cellBounds.Location.X + cellBounds.Width - s.Width - PosOffset;
				break;
			}

			p.Y = cellBounds.Location.Y + (cellBounds.Height / 2) - (s.Height / 2);
		}
			
//---------------------------------------------------------------------------------
		protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
		{
			Point p = new Point(e.X + cellLocation.X, e.Y + cellLocation.Y);
			if(Enabled
			&&  p.X >= checkBoxLocation.X && p.X <= 
				checkBoxLocation.X + checkBoxSize.Width 
			&&  p.Y >= checkBoxLocation.Y && p.Y <= 
				checkBoxLocation.Y + checkBoxSize.Height)
			{
				Checked = !Checked;
				ChgAllTick();

				if(OnCheckBoxClicked != null)
				{
					OnCheckBoxClicked(Checked);
				}

				this.DataGridView.InvalidateCell(this);
			} 
		}
	
//---------------------------------------------------------------------------------
		public void ChkAllTick()
		{
			bool tmp_enabled = true;
			bool tmp_checked = true;

			if(this.DataGridView.Rows.Count <= 0)
			{
				tmp_enabled = false;
				tmp_checked = false;

			}else
			{
				object nagative = false;
				tmp_enabled = true;

				foreach(DataGridViewRow row in this.DataGridView.Rows)
				{
					if((row.Cells[this.ColumnIndex].Value == null)
					|| (!Validation(row.Cells[this.ColumnIndex].Value)))
					{

							tmp_checked = false;
							break;
					}
				}
			}

			Enabled = tmp_enabled;
			Checked = tmp_checked;
		}

//---------------------------------------------------------------------------------
		bool Validation(object val)
		{
			string type = val.GetType().ToString();

			switch(type)
			{
			case "System.Int16":
			case "System.Int32":
				if(0 < (int)val)
					return true;
				break;

			case "System.Int64":
				if(0 < Convert.ToInt64(val))
					return true;
				break;

			case "System.String":
				if(((string)val != "false") && ((string)val != "0") && ((string)val != ""))
					return true;
				break;
			
			case "System.Boolean":
				if((bool)val == true)
					return true;
				break;
			}

			return false;
		}

//---------------------------------------------------------------------------------
		private void ChgAllTick()
		{
			int cnt = DataGridView.Rows.Count;

			for(int i = 0; i < cnt; i++)
			{
				DataGridViewRow row = DataGridView.Rows[i];
			
				if(this.Checked)
					row.Cells[this.ColumnIndex].Value = trueVal;
				else
					row.Cells[this.ColumnIndex].Value = falseVal;
			}
		}

//---------------------------------------------------------------------------------
	}
}
