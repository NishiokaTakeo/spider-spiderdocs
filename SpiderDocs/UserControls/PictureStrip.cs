using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SpiderDocsForms;
using SpiderDocsModule;

namespace SpiderDocs
{
	public partial class PictureStrip : System.Windows.Forms.UserControl
	{
		readonly int ThumbMargin = 30;

		public int maxPicture
		{
			set { }
			get { return panel2.Controls.Count; }
		}

		List<Panel> TmpPanel = new List<Panel>();
		public List<string> FilePath = new List<string>();

		public delegate void SubmitClickedHandler(object sender, EventArgs e, int action);
		[Category("Action")]
		[Description("Fires when the Submit button is clicked.")]

		public event SubmitClickedHandler SubmitClicked;

		public PictureStrip()
		{
		   InitializeComponent();
		}

		public void deletSelectedPag()
		{
			ArrayList cpn = new ArrayList();
			foreach(Control c in panel2.Controls)
			{
				if(((Panel)c).BackColor == Color.RoyalBlue)
					cpn.Add(c);           
			}

			for(int i = 0; i < cpn.Count; i++)
				panel2.Controls.Remove(((Panel)cpn[i]));
		}

		public void selectAllFiles()
		{
			foreach(Control c in panel2.Controls)
				((Panel)c).BackColor = Color.RoyalBlue;
		}

		public void desselectAllFiles()
		{
			foreach(Control c in panel2.Controls)
				((Panel)c).BackColor = Color.Transparent;
		}

		public void AddPicture(string path)
		{
			Bitmap wrk = new Bitmap(path);

			//ImageUtilities.RotateBmpAsExif(wrk, path);
			Bitmap bm = ResizeImage(wrk, panel1.Width, panel1.Height - ThumbMargin);   

			wrk.Dispose();

			//create panel
			Panel pn = new Panel();
			FilePath.Add(path);
			pn.Name = "pn";
			pn.Tag = FilePath.Count - 1;
			pn.Size = new System.Drawing.Size(bm.Width, bm.Height + (ThumbMargin - 10));      
			pn.AllowDrop = true;
			pn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;       
			pn.BackgroundImage = bm;
			pn.BackColor = Color.RoyalBlue;
			pn.Click += new System.EventHandler  (image_Click);
			pn.MouseDown += new MouseEventHandler    (pb_MouseDown);
			pn.DragOver += new DragEventHandler     (pb_DragOver);
			pn.Cursor = System.Windows.Forms.Cursors.Hand;
			TmpPanel.Add(pn);
		}

		Bitmap ResizeImage(Bitmap image, double dw, double dh)
		{
			double hi;
			double imagew = image.Width;
			double imageh = image.Height;

			if((dh / dw) <= (imageh / imagew))
				hi = dh / imageh;
			else
				hi = dw / imagew;

			int w = (int)(imagew * hi);
			int h = (int)(imageh * hi);

			Bitmap result = new Bitmap(w, h);
			Graphics g = Graphics.FromImage(result);
			g.DrawImage(image, 0, 0, result.Width, result.Height);

			return result;
		}

		public int CommitAddPicture()
		{
			int ans;

			panel2.SuspendLayout();

			foreach(Panel pn in TmpPanel)
				panel2.Controls.Add(pn);

			panel2.ResumeLayout();

			ans = TmpPanel.Count;
			TmpPanel.Clear();

			return ans;
		}

		public void RotateImage(int idx)
		{
			Bitmap image;
			string temp = FilePath[idx] + "~";

			//string ext = Path.GetExtension(FilePath[idx]).ToLower();
			//if(ext == ".jpg")
			//{
				//ImageUtilities.RotateJpg(FilePath[idx]);

			//}else
			//{
				image = new Bitmap(FilePath[idx]);
				image.RotateFlip(RotateFlipType.Rotate90FlipNone);
				image.Save(temp);
				image.Dispose();
				File.Delete(FilePath[idx]);
				File.Move(temp, FilePath[idx]);
			//}

			foreach(Panel pn in panel2.Controls)
			{
				if((int)pn.Tag == idx)
				{
					pn.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
					pn.Refresh();
					break;
				}
			}
		}

		void pb_DragOver(object sender, DragEventArgs e)
		{
			try
			{
				base.OnDragOver(e);

				// is another dragable
				if(e.Data.GetData(typeof(Panel)) != null)
				{
					FlowLayoutPanel p = (FlowLayoutPanel)(sender as Panel).Parent;
					//Current Position             
					int myIndex = p.Controls.GetChildIndex((sender as Panel));

					//Dragged to control to location of next picturebox
					Panel q = (Panel)e.Data.GetData(typeof(Panel));
					p.Controls.SetChildIndex(q, myIndex);
				}
			}
			catch{}
		}

		void pb_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				if(e.Button == MouseButtons.Left)
				{
					Panel pn1 = (Panel)sender;

					if(pn1.BackColor == Color.RoyalBlue)
					{
						pn1.BackColor = Color.Transparent;
						((frmScan)(this.Parent).Parent).lblSelPagCount.Text = (Convert.ToInt32(((frmScan)(this.Parent).Parent).lblSelPagCount.Text) - 1).ToString();

					}else
					{
						pn1.BackColor = Color.RoyalBlue;
						((frmScan)(this.Parent).Parent).lblSelPagCount.Text = (Convert.ToInt32(((frmScan)(this.Parent).Parent).lblSelPagCount.Text) + 1).ToString();
					}
					
				}
				else
				{
					base.OnMouseDown(e);
					DoDragDrop(sender, DragDropEffects.All);
				}
			}
			catch{}
		}

		public void image_Click(object sender, EventArgs e)
		{
			try
			{
				SubmitClicked(sender, e, 0);
			}
			catch{}
		}

		public void clearImage()
		{
			object sender   = null;
			EventArgs e     = null;
			SubmitClicked(sender, e, 1);
		}

		public void RemoveAt(int idx)
		{
			int i = 0;

			foreach(Control c in panel2.Controls)
			{
				if((int)((Panel)c).Tag == idx)
				{
					panel2.Controls.RemoveAt(i);
					break;

				}else
				{
					i++;
				}
			}
		}
	}
}
