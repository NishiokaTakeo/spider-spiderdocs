using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace SpiderDocsForms
{
	public class ImageUtilities
	{
		readonly static int EXIF_ROTATION = 274;
		readonly static short EXIF_TYPE_HEX = 3;
		
		readonly static List<byte> tb_ExifRotation = new List<byte>()
		{
			1,	//RotateNoneFlipNone
			6,	//Rotate90FlipNone
			3,	//Rotate180FlipNone
			8	//Rotate270FlipNone
		};

		readonly static List<byte> tb_ExifRotationFlip = new List<byte>()
		{
			2,	//RotateNoneFlipX
			7,	//Rotate90FlipY
			4,	//Rotate180FlipX
			5	//Rotate270FlipY
		};

		enum en_ExifRotation
		{
			RotateNoneFlipNone = 0,
			Rotate90FlipNone,
			Rotate180FlipNone,
			Rotate270FlipNone,

			Max
		}

		enum en_ExifRotationFlip
		{
			RotateNoneFlipX = 0,
			Rotate90FlipY,
			Rotate180FlipX,
			Rotate270FlipY,

			Max
		}

		public static void RotateBmpAsExif(Bitmap bmp, string path)
		{
			if(Path.GetExtension(path).ToLower() == ".jpg")
			{
				Bitmap jpg = new Bitmap(path);
				System.Drawing.Imaging.PropertyItem pi = GetExifProperty(jpg, EXIF_ROTATION);

				if(pi != null)
				{
					byte val = pi.Value[0];
					int idx;

					if(tb_ExifRotation.Contains(val))
					{
						idx = tb_ExifRotation.IndexOf(val);
						switch((en_ExifRotation)(idx))
						{
						case en_ExifRotation.RotateNoneFlipNone:
							bmp.RotateFlip(RotateFlipType.RotateNoneFlipNone);
							break;
						case en_ExifRotation.Rotate90FlipNone:
							bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
							break;
						case en_ExifRotation.Rotate180FlipNone:
							bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
							break;
						case en_ExifRotation.Rotate270FlipNone:
							bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
							break;
						}

					}else if(tb_ExifRotationFlip.Contains(val))
					{
						idx = tb_ExifRotationFlip.IndexOf(val);
						switch((en_ExifRotationFlip)(idx))
						{
						case en_ExifRotationFlip.RotateNoneFlipX:
							bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
							break;
						case en_ExifRotationFlip.Rotate90FlipY:
							bmp.RotateFlip(RotateFlipType.Rotate90FlipY);
							break;
						case en_ExifRotationFlip.Rotate180FlipX:
							bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
							break;
						case en_ExifRotationFlip.Rotate270FlipY:
							bmp.RotateFlip(RotateFlipType.Rotate270FlipY);
							break;
						}
					}
				}

				jpg.Dispose();
			}
		}

		public static void RotateJpg(string path)
		{
			string ext = Path.GetExtension(path).ToLower();

			if(ext == ".jpg")
			{
				string temp = path + "~";
				Bitmap bmp = new Bitmap(path);

				System.Drawing.Imaging.PropertyItem pi = GetExifProperty(bmp, EXIF_ROTATION);
				if(pi != null)
				{
					byte val = pi.Value[0];
					int idx;

					if(tb_ExifRotation.Contains(val))
					{
						idx = tb_ExifRotation.IndexOf(val) + 1;

						if((int)en_ExifRotation.Max <= idx)
							idx = (int)en_ExifRotation.RotateNoneFlipNone;

						pi.Value[0] = tb_ExifRotation[idx];


					}else if(tb_ExifRotationFlip.Contains(val))
					{
						idx = tb_ExifRotationFlip.IndexOf(val) + 1;

						if((int)en_ExifRotationFlip.Max <= idx)
							idx = (int)en_ExifRotationFlip.RotateNoneFlipX;

						pi.Value[0] = tb_ExifRotationFlip[idx];
					}
					
					bmp.SetPropertyItem(pi);
					bmp.Save(temp, System.Drawing.Imaging.ImageFormat.Jpeg);
					bmp.Dispose();

					File.Delete(path);
					File.Move(temp, path);
				}
			}
		}

		static System.Drawing.Imaging.PropertyItem GetExifProperty(Bitmap bmp, int id)
		{
			System.Drawing.Imaging.PropertyItem pi = null;

			for(int i = 0; i < bmp.PropertyItems.Length; i++)
			{
				pi = bmp.PropertyItems[i];
				if(pi.Id == id && pi.Type == EXIF_TYPE_HEX)
					break;
			}

			return pi;
		}
	}
}
