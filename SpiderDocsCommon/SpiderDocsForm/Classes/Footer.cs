using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Office.Interop.Word;
using Spider.IO;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;

//---------------------------------------------------------------------------------
namespace SpiderDocsForms
{
	public class Footer : SpiderDocsModule.Footer
	{
//---------------------------------------------------------------------------------
		public static void AddFooter(Document doc)
		{
			// Support only for Word files
			if(FileFolder.OfficeCheck(doc.extension) == en_OfficeType.Word)
			{
				// Get a footer string
				string footer = MakeFooter(doc);

				// Apply the footer
				Microsoft.Office.Interop.Word.Application oWord;
				oWord = new Microsoft.Office.Interop.Word.Application();
				oWord.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;
				oWord.Visible = false; //to avoid displaying the Word Application

				object strDocName = doc.path;
				object objBool = false;
				object objNull = System.Reflection.Missing.Value;

				Microsoft.Office.Interop.Word.Document oMyDoc = oWord.Documents.Open(ref strDocName, ref objBool, ref objBool, ref objBool, ref objNull,
													   ref objNull, ref objNull, ref objNull, ref objNull, ref objNull, 
													   ref objNull, ref objNull, ref objNull, ref objNull, ref objNull, ref objNull);
				
				if(oMyDoc.Sections.Count > 0)
				{
					if(oMyDoc.ProtectionType != WdProtectionType.wdNoProtection)
					{
						System.Windows.Forms.MessageBox.Show(lib.msg_footer_protected, 
															 lib.msg_messabox_title, System.Windows.Forms.MessageBoxButtons.OK, 
															 System.Windows.Forms.MessageBoxIcon.Error);
					}else
					{
						oMyDoc.Sections[1].Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Text = footer;
						oMyDoc.Save();
					}
				}

				((Microsoft.Office.Interop.Word._Document)oMyDoc).Close();
				((Microsoft.Office.Interop.Word._Application)oWord).Quit();
			}
		}

//---------------------------------------------------------------------------------
		static string MakeFooter(Document doc)
		{
			string ans = "";

			// Get a footer structure
			List<Footer> footers = FooterController.GetFooter();

			// Make footer string according to each attributes
			foreach(Footer footer in footers)
			{
				string data = "";
				bool dataExist = false;

				// Get footer type (ordinary file information or attributes)
				en_FooterType type = (en_FooterType)footer.field_type;

				switch(type)
				{
				case en_FooterType.RegAttr:
					data = MakeFooter_RegAttr((en_RegAttr)footer.field_id, doc);
					dataExist = true;
					break;

				case en_FooterType.Attr:
					string text = "";
					DocumentAttribute val = doc.Attrs.Find(a => a.id == footer.field_id);

					if((val != null) && (val.IsValidValue()))
					{
						// Get combo box value
						if(DocumentAttribute.IsComboTypes(val.id_type))
						{
							text = val.atbValueForUI;
							dataExist = true;

						}else
						{
							text = val.atbValue_str;
							dataExist = true;
						}

						data = val.name + ": " + text;
					}
					break;
				}

				// Join the value
				if(dataExist)
					ans += (data + "  ");
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		static string MakeFooter_RegAttr(en_RegAttr type, Document src)
		{
			string data = "";

			switch(type)
			{
			case en_RegAttr.Id:
				data = "Id: " + src.id.ToString();
				break;

			case en_RegAttr.Name:
				data = Path.GetFileNameWithoutExtension(src.title);
				break;

			case en_RegAttr.Folder:
				data = src.name_folder;
				break;

			case en_RegAttr.DocType:
				data = src.name_docType;
				break;

			case en_RegAttr.Version:
				data = "V " + src.version;
				break;

			case en_RegAttr.Author:
				data = src.author;
				break;

			case en_RegAttr.Date:
				data = src.date.ToString("d");
				break;
			}

			return data;
		}

//---------------------------------------------------------------------------------
	}
}
