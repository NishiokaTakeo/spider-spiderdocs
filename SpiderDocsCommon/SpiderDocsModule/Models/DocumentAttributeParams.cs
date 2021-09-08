using System;
using Spider.Types;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
//---------------------------------------------------------------------------------
	[Serializable]
	public class DocumentAttributeParams
	{
//---------------------------------------------------------------------------------
		public int id_folder { get; set; }
		public ThreeStateBool required { get; set; }

		public DocumentAttributeParams()
		{
			required = ThreeStateBool.Intermidiate;
		}

//---------------------------------------------------------------------------------
	}
}
