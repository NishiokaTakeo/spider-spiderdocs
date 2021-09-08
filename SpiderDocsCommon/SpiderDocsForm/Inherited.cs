// The purpose of this file is to avoid ambiguous reference among same class names over multiple name spaces.
using System;
using SpiderDocsModule;

namespace SpiderDocsForms
{
	public class DocumentAttributeController : SpiderDocsModule.DocumentAttributeController<Document>
	{
	}

	public class FooterController : SpiderDocsModule.FooterController<Footer>
	{
	}
    /*
	public class DTS_Folder : SpiderDocsModule.DTS_Folder
	{
		public DTS_Folder(bool combo, bool permitted, bool only_edit_permitted_folders = false, params int[] ids) : base(SpiderDocsApplication.CurrentUserId, combo, permitted, only_edit_permitted_folders, ids)
		{
		}
	}
	
    public class DTS_Document : SpiderDocsModule.DTS_Document
	{
		public DTS_Document() : base(SpiderDocsApplication.CurrentUserId, SpiderDocsApplication.CurrentPublicSettings.maxDocs, SpiderDocsApplication.CurrentServerSettings.localDb)
		{
		}
	}
    */
}
