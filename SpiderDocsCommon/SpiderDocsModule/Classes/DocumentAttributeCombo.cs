using System;
using System.Collections.Generic;

namespace SpiderDocsModule
{
	[Serializable]
	public class DocumentAttributeCombo:ICloneable
    {
		public int id { set; get; }
		public int id_atb { set; get; }
		public string text { set; get; }
        public bool Selected { set; get; }
        public List<DocumentAttribute> children { set; get; }

		public DocumentAttributeCombo()
		{
            children = new List<DocumentAttribute>();
		}

        public override string ToString()
        {
            return text;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    [Serializable]
    public class ComboItemChildren
    {
        public int id { set; get; }
        public int id_atb { set; get; }
        public string text { set; get; }

        public override string ToString()
        {
            return text;
        }
    }

}
