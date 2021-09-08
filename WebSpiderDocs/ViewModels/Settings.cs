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
	[Serializable]
	public class SettingsViewModel
	{
		public PublicSettings Public {get;set;}
		public UserGlobalSettings UserGlobal { get; set; }
		public cl_WorkspaceGridsize WorkSpaceGridSize { get; set; }
		public cl_WorkspaceCustomize WorkspaceCustomize { get; set; }
	}
}
