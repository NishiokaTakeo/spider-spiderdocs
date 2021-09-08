using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpiderDocsModule;

namespace SpiderDocsForms
{
	public class DocumentDataObject : DataObject
	{
		List<Document> docs;
		bool DataSaved = false;

		public DocumentDataObject(List<Document> data) : base(DataFormats.FileDrop, new string[]{"dummy"})
		{
			docs = data;
		}

		public override object GetData(string format)
		{
			if(!DataSaved)
			{
				docs.RemoveAll(a => !a.IsActionAllowed(en_Actions.Export));

				foreach(Document doc in docs)
				{
					doc.Export(FileFolder.GetTempFolder() + doc.title);
					doc.path = FileFolder.GetTempFolder() + doc.title;
				}

				DataSaved = true;
			}
			
			base.SetData(format, docs.Select(a => a.path).ToArray());

		    return base.GetData(format);
		}

        public List<Document> GetOrigin()
        {
            return docs;
        }
	}
}
