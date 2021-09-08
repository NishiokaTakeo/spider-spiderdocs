using System;

namespace SpiderDocsModule
{
	public class DragDropAttribute:ICloneable
	{
        public int id_folder { get; set; } = 0;
		public int id_atb { get; set; } = 0;

        public string value_from { get; set; } = string.Empty;
        public string value_taken { get; set; } = string.Empty;

        public DragDropAttribute()
		{
		}

		public object Clone()
    	{
         	return this.MemberwiseClone();
    	}
	}
}
