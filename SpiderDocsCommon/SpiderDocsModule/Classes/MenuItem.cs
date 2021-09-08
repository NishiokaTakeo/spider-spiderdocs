using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpiderDocsModule
{
	public class TopMenuItem
	{
		public int id { get; set; }
		public string name { get; set; }
		public string internal_name { get; set; }
		public int order;

		public List<TopMenuItem> SubItems = new List<TopMenuItem>();
	}
}
