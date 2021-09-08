using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OutlookMailReader;

namespace SpiderDocsModule
{
	public class OutlookMail
	{
		public OutlookMail()
		{
			//wrap standard IDataObject in OutlookDataObject
			//OutlookDataObject dataObject = new OutlookDataObject(e.Data);

			////get the names and data streams of the files dropped
			//string[] filenames = (string[])dataObject.GetData("FileGroupDescriptor");
			//MemoryStream[] filestreams = (MemoryStream[])dataObject.GetData("FileContents");

			//for (int fileIndex = 0; fileIndex < filenames.Length; fileIndex++)
			//{
			//	//use the fileindex to get the name and data stream
			//	string filename = filenames[fileIndex];
			//	MemoryStream filestream = filestreams[fileIndex];

			//	OutlookStorage.Message message = new OutlookStorage.Message(filestream);
			//	this.LoadMsgToTree(message, this.treeView1.Nodes.Add("MSG"));
			//	message.Dispose();
			//}
		}
	}
}
