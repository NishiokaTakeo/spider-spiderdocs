using System;
using System.Collections.Generic;
using System.IO;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class DocumentProperty : DocumentBase
	{
//---------------------------------------------------------------------------------
		//new public List<DocumentAttribute> Attrs { get; set; }

//---------------------------------------------------------------------------------
		public DocumentProperty()
		{
			Attrs = new List<DocumentAttribute>();
		}

//---------------------------------------------------------------------------------
		public void PropertyCopy(DocumentProperty src)
		{
			id_docType = src.id_docType;
			id_folder = src.id_folder;
			Attrs = DocumentAttribute.Clone(src.Attrs);
		}

//---------------------------------------------------------------------------------
		public static bool PropertyCompare(DocumentProperty origVal, DocumentProperty newVal)
		{
			bool ans = false;

			if((origVal.id_docType == newVal.id_docType) &&
                (origVal.id_folder == newVal.id_folder) &&
               //(origVal.id_notification_group == newVal.id_notification_group) &&
			   (DocumentAttribute.CompareAttributes(origVal.Attrs, newVal.Attrs)))
			{
				ans = true;
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public static T PropertyGetSameVal<T>(T Base, List<T> CompList) where T : DocumentProperty, new()
		{
			List<List<DocumentAttribute>> CompAttrList = new List<List<DocumentAttribute>>();
			T ans = new T();
			ans.PropertyCopy(Base);

			foreach(T Comp in CompList)
			{
				if(Base.id_docType != Comp.id_docType)
					ans.id_docType = 0;

				if(Base.id_folder != Comp.id_folder)
					ans.id_folder = 0;

				CompAttrList.Add(Comp.Attrs);
			}

			if((0 < ans.id_docType) && !CompList.Exists(a => a.id_docType != ans.id_docType))
				ans.Attrs = DocumentAttribute.GetSameAttributeValues(Base.Attrs, CompAttrList);

			return ans;
		}

//---------------------------------------------------------------------------------
		public void SetCreationDate(string path)
		{
			FileInfo infoFile;
			infoFile = new FileInfo(path);
            
			//this.SetDateAttribute(infoFile.CreationTime);
            this.SetDateAttribute(DateTime.Now);            
		}

//---------------------------------------------------------------------------------
		public void SetDateAttribute(DateTime date)
		{
			for(int i = 0; i < Attrs.Count; i++)
			{
				if(Attrs[i].id_type == en_AttrType.Date)
					Attrs[i].atbValue = date;
			}
		}

//---------------------------------------------------------------------------------
	}
}
