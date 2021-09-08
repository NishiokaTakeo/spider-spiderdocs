using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SpiderDocsForms;
using SpiderDocsModule;

namespace SpiderDocs
{
	public partial class PBPictureBox : Spider.Forms.UserControlBase
	{
		int LastIdx = -1;

		public int Count
		{
			set {}
			get { return this.pictureStrip1.maxPicture;	}
		}

		public void AddPicture(string path)
		{
			this.pictureStrip1.AddPicture(path);
		}

		public int CommitAddPicture()
		{
			int ans = pictureStrip1.CommitAddPicture();
			LastIdx = (int)pictureStrip1.panel2.Controls[pictureStrip1.panel2.Controls.Count - 1].Tag;

			SetImageBox(LastIdx);

			return ans;
		}

		public PBPictureBox()
		{
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}

		public void clearImage()
		{
			if(imageBox.Image != null)
			{
				imageBox.Image.Dispose();
				imageBox.Image = null;
			}

			LastIdx = -1;
		}

		void SetImageBox(int idx)
		{
			if(imageBox.Image != null)
				imageBox.Image.Dispose();

			Bitmap bm = new Bitmap(GetFilePath(idx));
			//ImageUtilities.RotateBmpAsExif(bm, GetFilePath(idx));
			
			imageBox.Image = bm;
		}

		public int GetSelectedPageCnt()
		{
			int n = 0;

			foreach(Control c in pictureStrip1.panel2.Controls)
			{
				if(((Panel)c).BackColor == Color.RoyalBlue)
				{
					n++;
				}
			}

			return n;
		}

		public ArrayList GetSelectedFileIdx()
		{
			ArrayList arraySelectedFiles = new ArrayList();

			foreach(Control c in pictureStrip1.panel2.Controls)
			{
				if(((Panel)c).BackColor == Color.RoyalBlue)
					arraySelectedFiles.Add((int)c.Tag);
			}

			return arraySelectedFiles;
		}

		public List<string> GetSelectedFilePaths()
		{
			List<string> paths = new List<string>();
			ArrayList ids = GetSelectedFileIdx();

			foreach(int id in ids)
				paths.Add(GetFilePath(id));

			return paths;
		}

		public string GetFilePath(int idx)
		{
			return pictureStrip1.FilePath[idx];
		}

		public void deletSelectedPag()
		{
			pictureStrip1.deletSelectedPag();
		}

		public void selectAllFiles()
		{
			pictureStrip1.selectAllFiles();
		}

		public void desselectAllFiles()
		{
			pictureStrip1.desselectAllFiles();
		}

		public void RemoveSelectedFiles()
		{
			ArrayList idxs = GetSelectedFileIdx();
			
			foreach(int idx in idxs)
				pictureStrip1.RemoveAt(idx);
		}

		private void pictureStrip1_Clicked(object sender, EventArgs e, int action)
		{
			if(action == 1)
			{
				clearImage();

			}else
			{
				LastIdx = (int)((Panel)sender).Tag;
				SetImageBox(LastIdx);
			}
		}

		public void ImageRotate()
		{
			if(0 <= LastIdx)
			{
				int wrk = LastIdx;
				clearImage();
				LastIdx = wrk;

				pictureStrip1.RotateImage(LastIdx);
				SetImageBox(LastIdx);
			}
		}
	}
}

