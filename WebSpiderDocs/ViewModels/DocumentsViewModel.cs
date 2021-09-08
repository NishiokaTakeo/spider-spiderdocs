using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Spider.Common;
using SpiderDocsModule;

namespace WebSpiderDocs.Models
{
	// This "Requests" inheritance is just only workaround for reports which cannot access nested objects.
	//[Serializable]
	public class DocumentViewModel
	{
		public List<Document> docs { get; set; }
	}
}
