using System;
using SpiderDocsModule;

namespace SpiderDocsForms
{
	public class DocumentController : SpiderDocsModule.DocumentController<Document>
	{
		public static string RollBackVersion(int id, int id_version, int CurrentVersionNo, string reason)
		{
			return RollBackVersion(SpiderDocsApplication.CurrentUserId, SpiderDocsApplication.CurrentServerSettings.localDb, id, id_version, CurrentVersionNo, reason);
		}

		public static bool DeleteDocument(int id_doc, string reason)
		{
			return DeleteDocument(SpiderDocsApplication.CurrentUserId, id_doc, reason);
		}
	}
}
