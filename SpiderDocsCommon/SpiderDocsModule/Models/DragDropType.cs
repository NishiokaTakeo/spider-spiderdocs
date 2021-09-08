using System;

namespace SpiderDocsModule
{
	public class DragDropType : ICloneable
	{
        public int id { get; set; } = 0;
        public int id_folder { get; set; } = 0;
        public int id_type { get; set; } = 0;

        public DragDropType()
		{
		}

		public object Clone()
    	{
         	return this.MemberwiseClone();
    	}
	}
}
