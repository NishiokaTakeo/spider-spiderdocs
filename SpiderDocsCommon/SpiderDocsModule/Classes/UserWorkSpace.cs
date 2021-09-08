using System;
using System.Collections.Generic;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class UserWorkSpace : ICloneable
	{
        public int id { get; set; } = 0;
		public int id_user { get; set; } = 0;
        public int id_version { get; set; } = 0;
        public string filename { get; set; } = "";
		public byte[] filedata { get; set; }
        public string filehash { get; set; } = "";
		public DateTime created_date { get; set; }
		public DateTime lastaccess_date { get; set; }
        public DateTime lastmodified_date { get; set; }


//---------------------------------------------------------------------------------
		public UserWorkSpace()
		{

		}

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //---------------------------------------------------------------------------------
    }
}
