using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public static class PdfUtilities
	{
//---------------------------------------------------------------------------------
		/// <summary>
		/// <para>Convert an image file into a PDF file.</para>
		/// </summary>
		public static void ConvertToPdf(string ImagePath, string SavePath)
		{
			iTextSharp.text.Rectangle pageSize = null;

			using(var srcImage = new Bitmap(ImagePath))
			{
				pageSize = new iTextSharp.text.Rectangle(0, 0, srcImage.Width, srcImage.Height);
			}

			using(var ms = new MemoryStream())
			{
				var document = new iTextSharp.text.Document(pageSize, 0, 0, 0, 0);

				PdfWriter.GetInstance(document, ms).SetFullCompression();
				document.Open();

				var image = iTextSharp.text.Image.GetInstance(ImagePath);
				document.Add(image);
				document.Close();

				File.WriteAllBytes(SavePath, ms.ToArray());
			}
		}

//---------------------------------------------------------------------------------
	}
}
