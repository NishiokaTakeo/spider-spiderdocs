//using System;
//using System.Collections.Generic;
//using Spider.Types;
//using SpiderDocsModule;
//
////---------------------------------------------------------------------------------
//namespace SpiderDocsModule
//{
//	public enum en_SearchCriteriaMergeType
//	{
//		OFF = 0,
//		Bottom,
//		Top,
//
//		Max
//	}
//
////---------------------------------------------------------------------------------
//	[Serializable]
//    public class AttributeCriteriaCollectionBase : AttributeCriteriaCollectionBase<AttributeCriteriaBase>
//	{
//	}
//
//	[Serializable]
//	public class AttributeCriteriaCollectionBase<AttributeCriteria>        
//	    where AttributeCriteria : AttributeCriteriaBase<DocumentAttribute>, new()
//	{
//		public List<AttributeCriteria> Attributes { get; set; }
//
//		public AttributeCriteriaCollectionBase()
//		{
//			Attributes = new List<AttributeCriteria>();
//		}
//
//		public void Add(DocumentAttribute attr, bool wildcard = false)
//		{
//			AttributeCriteria wrk = new AttributeCriteria();
//			wrk.Values = attr;
//			wrk.UseWildCard = wildcard;
//
//			Attributes.Add(wrk);
//		}
//
//		public int Count
//		{
//			get { return Attributes.Count; }
//		}
//		
//		public AttributeCriteria this[int index]
//		{
//			get { return Attributes[index]; }
//		}
//	}
//
////---------------------------------------------------------------------------------
//	[Serializable]
//    public class AttributeCriteriaBase : AttributeCriteriaBase<DocumentAttribute>
//	{
//	}
//
//	[Serializable]
//    public class AttributeCriteriaBase : DocumentAttribute
//	{
//		public DocumentAttribute Values { get; set; }
//		public bool UseWildCard { get; set; }
//	}
//
////---------------------------------------------------------------------------------
//    /*
//	[Serializable]
//	public class SearchCriteriaBase : SearchCriteriaBase
//		<DocumentAttributeBase,
//		AttributeCriteriaBase,
//		AttributeCriteriaCollectionBase>
//	{
//	}
//    */
//
//	[Serializable]
//	public class SearchCriteriaBase<AttributeCriteriaBase,AttributeCriteriaCollection>
//        
//		where AttributeCriteriaBase : AttributeCriteriaBase<DocumentAttribute>, new()
//		where AttributeCriteriaCollection : AttributeCriteriaCollectionBase<AttributeCriteriaBase>, new()
//	{
//		[Serializable]
//		public class ReviewCriteria
//		{
//			public en_ReviewStaus Status = en_ReviewStaus.INVALID;
//			public List<int> UserIds = new List<int>();
//		}
//
//		public bool GetRecentDocuments { get; set; }
//
//		public List<int> DocIds { get; set; }
//		public string StrDocIds
//		{
//			get { return String.Join(" ", DocIds); }
//			set
//			{
//				DocIds.Clear();
//                if (value != null) {
//                    string[] wrks = value.Split(' ');
//                    foreach (string wrk in wrks)
//                    {
//                        int ans = 0;
//                        int.TryParse(wrk.Trim(), out ans);
//
//                        if (0 < ans)
//                            DocIds.Add(ans);
//                    }
//                }
//            }
//		}
//
//		public List<int> DocNotInIds { get; set; }
//		public List<int> DocTypeIds { get; set; }
//		public List<int> FolderIds { get; set; }
//		public List<int> UserIds { get; set; }
//		public AttributeCriteriaCollection AttributeCriterias { get; set; }
//		public List<string> Keywords { get; set; }
//		public List<string> Titles { get; set; }
//		public List<string> AbsoluteTitles { get; set; }
//		public List<string> Extensions { get; set; }
//		public List<Period> Date { get; set; }
//		public ReviewCriteria Review { get; set; }
//		public en_SearchCriteriaMergeType MergeType { get; set; }
//
//		public List<en_file_Status> Statuses { get; set; }
//        public List<en_file_Status> ExcludeStatuses { get; set; }
//
//		public List<en_file_Sp_Status> SpStatuses { get; set; }
//
//		public SearchCriteriaBase()
//		{
//			GetRecentDocuments = false;
//			DocIds = new List<int>();
//			DocNotInIds = new List<int>();
//			DocTypeIds = new List<int>();
//			FolderIds = new List<int>();
//			UserIds = new List<int>();
//			AttributeCriterias = new AttributeCriteriaCollection();
//			Keywords = new List<string>();
//			Titles = new List<string>();
//			AbsoluteTitles = new List<string>();
//			Extensions = new List<string>();
//			Date = new List<Period>();
//			Review = new ReviewCriteria();
//			MergeType = en_SearchCriteriaMergeType.OFF;
//			Statuses = new List<en_file_Status>();
//            ExcludeStatuses = new List<en_file_Status>();
//            SpStatuses = new List<en_file_Sp_Status>();
//		}
//
////---------------------------------------------------------------------------------
//	}
//}
//