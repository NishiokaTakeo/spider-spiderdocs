using System;
using System.Linq;

namespace SpiderDocsModule
{
	public class FavariteProperty
	{
        public int id { get; set; } = 0;
		public int id_user { get; set; } = 0;
        public int id_folder { get;set; } = 0;
        public int id_doc_type { get; set; } = 0;

        public FavariteProperty()
		{
		}

	}

    public class FavaritePropertyItem
    {
        public int id { get; set; } = 0;
        public int id_favourite_propery { get; set; } = 0;
        public int id_atb { get; set; } = 0;
        public string atb_value { get; set; } = string.Empty;

        public FavaritePropertyItem()
        {

        }

        public DocumentAttribute ToAttribute()
        {
            DocumentAttribute attr = DocumentAttributeController.GetAttribute(this.id_atb);

            if (attr.IsCombo())
                attr.atbValue = atb_value.Split(',').ToList().Select(x => int.Parse(x)).ToList();
            else if(attr.IsCheckBox())
                attr.atbValue = bool.Parse(atb_value);
            else if(attr.IsDateTimeOrDate())
                attr.atbValue = DateTime.Parse(atb_value);
            else
                attr.atbValue= atb_value;

            return attr;
        }
    }
}
