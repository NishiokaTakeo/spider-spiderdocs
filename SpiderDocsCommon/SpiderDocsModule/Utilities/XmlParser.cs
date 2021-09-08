using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

//-----------------------------------------------------------
namespace SpiderDocsModule
{
	class XmlParser
	{
//-----------------------------------------------------------
		public class XmlSetting
		{
			public string[] ele;
			public int ele_idx = 0;
			
			public string[] val;
			public int val_idx = 0;
		}

//-----------------------------------------------------------
		public bool WriteXml(string fname, XmlSetting src)
		{
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;  
			settings.NewLineOnAttributes = true;
			settings.Encoding = Encoding.UTF8;
			XmlWriter writer = XmlWriter.Create(fname + "~", settings);

			writer.WriteStartDocument();
			bool ok = WriteXml(writer, src);
			writer.WriteEndDocument();
			writer.Close();

			try
			{
				if(ok)
				{
					File.Delete(fname);
					File.Move(fname + "~", fname);

				}else
				{
					File.Delete(fname + "~");
				}
			}
			catch(IOException Error)
			{
				System.Windows.Forms.MessageBox.Show(Error.Message);
			}

			return ok;
		}

//-----------------------------------------------------------
		bool WriteXml(XmlWriter writer, XmlSetting src)
		{
			bool ok = true;

			int ele_idx_st = src.ele_idx;

			string ele = src.ele[src.ele_idx];
			string val = "";

			// Loop check
			bool loop;
			int loop_cnt;
			int loop_key = ele.IndexOf("*");

			if(0 <= loop_key)
			{
				loop = true;
				loop_cnt = int.Parse(ele.Substring(loop_key + 1));

			}else
			{
				loop = false;
				loop_cnt = 1;	// Least 1 loop.
			}

			// Loop for writing the xml.
			for(int i = 0; i < loop_cnt; i++)
			{
				// Write a parent.
				if(loop)
				{
					src.ele_idx = ele_idx_st;

					ele = src.ele[src.ele_idx];
					writer.WriteStartElement(ele.Substring(1, (loop_key - 1)) + i.ToString());

				}else
				{
					writer.WriteStartElement(ele.Substring(1));
				}
				src.ele_idx++;

				// Write children.
				ele = src.ele[src.ele_idx];
				while(ele != "-")
				{
					if(ele.Substring(0, 1) == "+")
					{
						if(!WriteXml(writer, src))
						{
							ok = false;
							break;
						}

					}else if(src.val_idx < src.val.Length)
					{
						val = src.val[src.val_idx++];

						if(val == "")
							val = " ";
						
						writer.WriteElementString(ele, val);

					}else
					{
						ok = false;
						break;
					}

					src.ele_idx++;
					if(src.ele_idx < src.ele.Length)
					{
						ele = src.ele[src.ele_idx];

					}else
					{
						ok = false;
						break;
					}
				}

				writer.WriteEndElement();
			}

			return ok;
		}

//-----------------------------------------------------------
		public bool ReadXml(string fname, XmlSetting src)
		{
			XmlReader reader;

			try
			{
				reader = XmlReader.Create(fname);
			}
			catch
			{
				return false;
			}

			if(reader.IsStartElement())
				ReadXml(reader, src);

			reader.Close();

			return true;
		}

//-----------------------------------------------------------
		bool ReadXml(XmlReader reader, XmlSetting src)
		{
			bool ok = true;

			int ele_idx_st = src.ele_idx;

			string ele = src.ele[src.ele_idx];

			// Loop check
			bool loop;
			int loop_cnt;
			int loop_key = ele.IndexOf("*");

			if(0 <= loop_key)
			{
				loop = true;
				loop_cnt = int.Parse(ele.Substring(loop_key + 1));

			}else
			{
				loop = false;
				loop_cnt = 1;	// Least 1 loop.
			}

			// Loop for reading the xml.
			for(int i = 0; i < loop_cnt; i++)
			{
				// Read a parent.
				if(loop)
				{
					src.ele_idx = ele_idx_st;
					ele = src.ele[src.ele_idx];
					reader.ReadStartElement(ele.Substring(1, (loop_key - 1)) + i.ToString());

				}else
				{
					reader.ReadStartElement(ele.Substring(1));
				}
				src.ele_idx++;
				reader.Read();

				// Read children.
				ele = src.ele[src.ele_idx];
				while(ele != "-")
				{
					if(ele.Substring(0, 1) == "+")
					{
						if(!ReadXml(reader, src))
						{
							ok = false;
							break;
						}

					}else if(src.val_idx < src.val.Length)
					{
						reader.ReadStartElement(ele);
						src.val[src.val_idx] = reader.ReadString().Trim();
						src.val_idx++;
						reader.ReadEndElement();

					}else
					{
						ok = false;
						break;
					}

					src.ele_idx++;
					reader.Read();
					if(src.ele_idx < src.ele.Length)
					{
						ele = src.ele[src.ele_idx];

					}else
					{
						ok = false;
						break;
					}
				}

				reader.ReadEndElement();

			}
			return ok;
		}

//---------------------------------------------------------------------------------
	}
}




