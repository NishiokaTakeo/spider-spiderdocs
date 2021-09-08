using System;
using System.Collections.Generic;
using SpiderDocsModule;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class DocumentPropertyBase
	{
//---------------------------------------------------------------------------------
		public int id_docType { get; set; }
		public int id_folder { get; set; }
        public List<DocumentAttribute> Attrs { get; set; }

//---------------------------------------------------------------------------------
		public DocumentPropertyBase()
		{
			id_docType = -1;
			id_folder = -1;
            Attrs = new List<DocumentAttribute>();
		}

//---------------------------------------------------------------------------------
	}
}
