using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Spider.Common;
using SpiderDocsModule;

namespace WebSpiderDocs.Models
{
	public class DocumentSearchViewModel : SearchCriteria
	{
		public int ItemCount { get; set; }

		public DocumentSearchViewModel()
		{
		}

		public void SetDefaultSetting()
		{
			ItemCount = 10;
		}
	}
}
