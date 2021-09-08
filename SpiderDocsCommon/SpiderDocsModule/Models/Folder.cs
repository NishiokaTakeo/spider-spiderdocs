using System;

namespace SpiderDocsModule
{
	public class Folder:ICloneable
	{
		public int id { get; set; }
		public string document_folder { get; set; }
		
		public int id_parent{get;set;}
        public bool archived { get; set; }

        public Folder()
		{
		}

		public Folder(int id, string document_folder,int id_parent, bool archived = false)
		{
			this.id = id;
			this.document_folder = document_folder;
            this.id_parent = id_parent;
            this.archived = archived;
        }

		public object Clone()
    	{
         	return this.MemberwiseClone();
    	}
	}
}
