using System;

namespace SpiderDocsModule
{

	public class AttributeDocType
	{
		public int id { get; set; } = 0;
		public int id_doc_type { get; set; } = 0;
		public int id_attribute { get; set; } = 0;
		public int position { get; set; } = 0;
        public bool duplicate_chk { get; set; } = false;
	}
}
