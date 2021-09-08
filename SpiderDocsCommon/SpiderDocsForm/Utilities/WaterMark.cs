using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using Ovml = DocumentFormat.OpenXml.Vml.Office;
using V = DocumentFormat.OpenXml.Vml;
using Wvml = DocumentFormat.OpenXml.Vml.Wordprocessing;
using SpiderDocsModule;

namespace SpiderDocsForms
{
	class WaterMark
	{
		public static bool addWaterMark_pdf(string path, string idDoc, string numVersion)
		{
			//create stream of filestream or memorystream etc. to create output file
			FileStream stream = new FileStream(path, FileMode.Open);
			
			byte[] pdfByte = new byte[stream.Length];
			stream.Read(pdfByte, 0, pdfByte.Length); 

			//create pdfreader object to read sorce pdf
			iTextSharp.text.pdf.PdfReader pdfReader = new iTextSharp.text.pdf.PdfReader(pdfByte);


			//create pdfstamper object which is used to add addtional content to source pdf file
			PdfStamper pdfStamper = new PdfStamper(pdfReader, stream);

			//iterate through all pages in source pdf
			for(int pageIndex = 1; pageIndex <= pdfReader.NumberOfPages; pageIndex++)
			{
				//Rectangle class in iText represent geomatric representation... in this case, rectanle object would contain page geomatry
				iTextSharp.text.Rectangle pageRectangle = pdfReader.GetPageSizeWithRotation(pageIndex);
				//pdfcontentbyte object contains graphics and text content of page returned by pdfstamper
				PdfContentByte pdfData = pdfStamper.GetOverContent(pageIndex);

				//create fontsize for watermark
				pdfData.SetFontAndSize(BaseFont.CreateFont(@"c:\windows\fonts\Calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 90);
				//create new graphics state and assign opacity
				PdfGState graphicsState = new PdfGState();
				graphicsState.FillOpacity = 0.20F;
				//set graphics state to pdfcontentbyte
				pdfData.SetGState(graphicsState);
				//set color of watermark
				pdfData.SetColorFill(BaseColor.BLUE);
				//indicates start of writing of text
				pdfData.BeginText();
				//show text as per position and rotation
				pdfData.ShowTextAligned(Element.ALIGN_CENTER, "Version " + numVersion, pageRectangle.Width / 2, pageRectangle.Height / 2, 45);
				//call endText to invalid font set
				pdfData.EndText();


				//create fontsize for watermark
				pdfData.SetFontAndSize(BaseFont.CreateFont(@"c:\windows\fonts\Calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 8);
				//create new graphics state and assign opacity
				graphicsState = new PdfGState();
				graphicsState.FillOpacity = 0.8F;
				//set graphics state to pdfcontentbyte
				pdfData.SetGState(graphicsState);
				//set color of watermark
				pdfData.SetColorFill(BaseColor.BLACK);

				//indicates start of writing of text
				pdfData.BeginText();

				//show text as per position and rotation
				pdfData.ShowTextAligned(Element.ALIGN_CENTER, "SpiderDocs: " + idDoc, 50, 5, 0);
				//call endText to invalid font set
				pdfData.EndText();


			}
			//close stamper and output filestream
			pdfStamper.Close();
			stream.Close();

			return true;

		}

		public static void addWaterMark_word(WordprocessingDocument doc, int version_numVersion, en_file_Status bd_id_status)
		{

			if(doc.MainDocumentPart.HeaderParts.Count() == 0)
			{

				doc.MainDocumentPart.DeleteParts(doc.MainDocumentPart.HeaderParts);
				var newHeaderPart = doc.MainDocumentPart.AddNewPart<HeaderPart>();
				var rId = doc.MainDocumentPart.GetIdOfPart(newHeaderPart);
				var headerRef = new HeaderReference();
				headerRef.Id = rId;
				var sectionProps = doc.MainDocumentPart.Document.Body.Elements<SectionProperties>().LastOrDefault();
				if(sectionProps == null)
				{
					sectionProps = new SectionProperties();
					doc.MainDocumentPart.Document.Body.Append(sectionProps);
				}
				sectionProps.RemoveAllChildren<HeaderReference>();
				sectionProps.Append(headerRef);
				var header = new DocumentFormat.OpenXml.Wordprocessing.Header();
				var paragraph = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
				var run = new DocumentFormat.OpenXml.Wordprocessing.Run();
				var text = new DocumentFormat.OpenXml.Wordprocessing.Text();
				text.Text = "";
				run.Append(text);
				paragraph.Append(run);
				header.Append(paragraph);
				newHeaderPart.Header = header;
				newHeaderPart.Header.Save();

			}


			foreach(HeaderPart headerPart in doc.MainDocumentPart.HeaderParts)
			{

				SdtBlock sdtBlock1 = new SdtBlock();
				SdtProperties sdtProperties1 = new SdtProperties();
				SdtId sdtId1 = new SdtId() { Val = 87908844 };
				SdtContentDocPartObject sdtContentDocPartObject1 = new SdtContentDocPartObject();
				DocPartGallery docPartGallery1 = new DocPartGallery() { Val = "Watermarks" };
				DocPartUnique docPartUnique1 = new DocPartUnique();

				sdtContentDocPartObject1.Append(docPartGallery1);
				sdtContentDocPartObject1.Append(docPartUnique1);
				sdtProperties1.Append(sdtId1);
				sdtProperties1.Append(sdtContentDocPartObject1);

				SdtContentBlock sdtContentBlock1 = new SdtContentBlock();
				DocumentFormat.OpenXml.Wordprocessing.Paragraph paragraph2 = new DocumentFormat.OpenXml.Wordprocessing.Paragraph() { RsidParagraphAddition = "00656E18", RsidRunAdditionDefault = "00656E18" };
				ParagraphProperties paragraphProperties2 = new ParagraphProperties();
				ParagraphStyleId paragraphStyleId2 = new ParagraphStyleId() { Val = "Header" };

				paragraphProperties2.Append(paragraphStyleId2);
				DocumentFormat.OpenXml.Wordprocessing.Run run1 = new DocumentFormat.OpenXml.Wordprocessing.Run();
				DocumentFormat.OpenXml.Wordprocessing.RunProperties runProperties1 = new DocumentFormat.OpenXml.Wordprocessing.RunProperties();
				NoProof noProof1 = new NoProof();
				Languages languages1 = new Languages() { EastAsia = "zh-TW" };
				runProperties1.Append(noProof1);
				runProperties1.Append(languages1);

				DocumentFormat.OpenXml.Wordprocessing.Picture picture1 = new DocumentFormat.OpenXml.Wordprocessing.Picture();

				V.Shapetype shapetype1 = new V.Shapetype() { Id = "_x0000_t136", CoordinateSize = "21600,21600", OptionalNumber = 136, Adjustment = "10800", EdgePath = "m@7,l@8,m@5,21600l@6,21600e" };
				V.Formulas formulas1 = new V.Formulas();
				V.Formula formula1 = new V.Formula() { Equation = "sum #0 0 10800" };
				V.Formula formula2 = new V.Formula() { Equation = "prod #0 2 1" };
				V.Formula formula3 = new V.Formula() { Equation = "sum 21600 0 @1" };
				V.Formula formula4 = new V.Formula() { Equation = "sum 0 0 @2" };
				V.Formula formula5 = new V.Formula() { Equation = "sum 21600 0 @3" };
				V.Formula formula6 = new V.Formula() { Equation = "if @0 @3 0" };
				V.Formula formula7 = new V.Formula() { Equation = "if @0 21600 @1" };
				V.Formula formula8 = new V.Formula() { Equation = "if @0 0 @2" };
				V.Formula formula9 = new V.Formula() { Equation = "if @0 @4 21600" };
				V.Formula formula10 = new V.Formula() { Equation = "mid @5 @6" };
				V.Formula formula11 = new V.Formula() { Equation = "mid @8 @5" };
				V.Formula formula12 = new V.Formula() { Equation = "mid @7 @8" };
				V.Formula formula13 = new V.Formula() { Equation = "mid @6 @7" };
				V.Formula formula14 = new V.Formula() { Equation = "sum @6 0 @5" };

				formulas1.Append(formula1);
				formulas1.Append(formula2);
				formulas1.Append(formula3);
				formulas1.Append(formula4);
				formulas1.Append(formula5);
				formulas1.Append(formula6);
				formulas1.Append(formula7);
				formulas1.Append(formula8);
				formulas1.Append(formula9);
				formulas1.Append(formula10);
				formulas1.Append(formula11);
				formulas1.Append(formula12);
				formulas1.Append(formula13);
				formulas1.Append(formula14);

				V.Path path1 = new V.Path() { AllowTextPath = true, ConnectionPointType = Ovml.ConnectValues.Custom, ConnectionPoints = "@9,0;@10,10800;@11,21600;@12,10800", ConnectAngles = "270,180,90,0" };
				V.TextPath textPath1 = new V.TextPath() { On = true, FitShape = true };

				V.ShapeHandles shapeHandles1 = new V.ShapeHandles();
				V.ShapeHandle shapeHandle1 = new V.ShapeHandle() { Position = "#0,bottomRight", XRange = "6629,14971" };
				shapeHandles1.Append(shapeHandle1);

				Ovml.Lock lock1 = new Ovml.Lock() { Extension = V.ExtensionHandlingBehaviorValues.Edit, TextLock = true, ShapeType = true };

				shapetype1.Append(formulas1);
				shapetype1.Append(path1);
				shapetype1.Append(textPath1);
				shapetype1.Append(shapeHandles1);
				shapetype1.Append(lock1);

				V.Shape shape1 = new V.Shape() { Id = "PowerPlusWaterMarkObject357476642", Style = "position:absolute;left:0;text-align:left;margin-left:0;margin-top:0;width:527.85pt;height:131.95pt;rotation:315;z-index:-251656192;mso-position-horizontal:center;mso-position-horizontal-relative:margin;mso-position-vertical:center;mso-position-vertical-relative:margin", OptionalString = "_x0000_s2049", AllowInCell = false, FillColor = "silver", Stroked = false, Type = "#_x0000_t136" };
				V.Fill fill1 = new V.Fill() { Opacity = ".5" };
				V.TextPath textPath2 = new V.TextPath() { Style = "font-family:\"Calibri\";font-size:50pt", String = (bd_id_status == en_file_Status.archived ? " (Archieved)" : "Version " + version_numVersion) };
				Wvml.TextWrap textWrap1 = new Wvml.TextWrap() { AnchorX = Wvml.HorizontalAnchorValues.Margin, AnchorY = Wvml.VerticalAnchorValues.Margin };

				shape1.Append(fill1);
				shape1.Append(textPath2);
				shape1.Append(textWrap1);

				picture1.Append(shapetype1);
				picture1.Append(shape1);

				run1.Append(runProperties1);
				run1.Append(picture1);

				paragraph2.Append(paragraphProperties2);
				paragraph2.Append(run1);
			   
				sdtContentBlock1.Append(paragraph2);
				sdtBlock1.Append(sdtProperties1);
				sdtBlock1.Append(sdtContentBlock1);
				headerPart.Header.Append(sdtBlock1);

				//break;
			}
		}
	}
}
