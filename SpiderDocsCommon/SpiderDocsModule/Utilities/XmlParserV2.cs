using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Linq;

//-----------------------------------------------------------
namespace SpiderDocsModule
{
	public class XmlParserV2
	{
        XDocument m_XmlDoc;
        private string m_KeyAttribute;
        private string m_RootAttribute;
//-----------------------------------------------------------
        public XmlParserV2(string path, string key, string root)
        {
            try
            {
                if (File.Exists(path))
                {
                    m_KeyAttribute = key;
                    m_RootAttribute = root;
                    XmlReader reader = XmlReader.Create(path);
                    m_XmlDoc = XDocument.Load(reader);
                }
                else
                {
                    m_XmlDoc = null;
                }
            }
            catch
            {
                m_XmlDoc = null;
            }
        }
//-----------------------------------------------------------
		public string GetValueByIndex(string strIndex, string attribute)
		{
            string rtnValue = "";
            if (m_XmlDoc != null)
            {
                foreach (var node in m_XmlDoc.Root.Elements(m_RootAttribute))
                {
                    string index = node.Element(m_KeyAttribute).Value;
                    if (index == strIndex)
                    {
                        rtnValue = node.Element(attribute).Value;
                        break;
                    }
                }
            }
            return rtnValue;
		}
//---------------------------------------------------------------------------------
	}
}




