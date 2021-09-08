using System;
using System.Collections.Generic;
using Spider.Types;
using SpiderDocsModule;
using System.Linq;
using Spider.Common;
//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public enum en_SearchCriteriaMergeType
	{
		OFF = 0,
		Bottom,
		Top,

		Max
	}

//---------------------------------------------------------------------------------
    /*
	[Serializable]
    public class AttributeCriteriaCollectionBase : AttributeCriteriaCollectionBase<AttributeCriteriaBase>
	{
	}
    */
	[Serializable]
	public class AttributeCriteriaCollection
	{
        readonly string tb_AttributeValueSearch =
            "(SELECT COUNT(document.id) FROM document " +
            "inner join document_attribute on document.id = document_attribute.id_doc " +
            "inner join attribute_doc_type on document.id_type = attribute_doc_type.id_doc_type " +
            "and document_attribute.id_atb = attribute_doc_type.id_attribute " +
            "WHERE (<CRITERIA>) AND document.id = d.id)";

		public List<AttributeCriteriaBase> Attributes { get; set; }

		public AttributeCriteriaCollection()
		{
			Attributes = new List<AttributeCriteriaBase>();
		}

		public void Add(DocumentAttribute attr, bool wildcard = false)
		{
			AttributeCriteriaBase wrk = new AttributeCriteriaBase();
			wrk.Values = attr;
			wrk.UseWildCard = wildcard;

			Attributes.Add(wrk);
		}

        public void Add(int id_attr)
        {
            DocumentAttribute attr = new DocumentAttribute(){
                id=id_attr,
                atbValue = "%"
            };

            AttributeCriteriaBase wrk = new AttributeCriteriaBase();
            //wrk.id = id_attr;
            wrk.Values = attr;
            wrk.UseWildCard = true;
            
            Attributes.Add(wrk);
        }

		public int Count
		{
			get { return Attributes.Count; }
		}
		
		public AttributeCriteriaBase this[int index]
		{
			get { return Attributes[index]; }
		}


        string GetAttrCriteria(DocumentAttribute attr, bool UseWildCard)
        {
            System.Text.StringBuilder criteria = new System.Text.StringBuilder();
            criteria.Append("(document_attribute.id_atb = " + attr.id + " AND ");

            if (attr.IsValidValue())
            {
                switch (attr.id_type)
                {
                    case en_AttrType.Text:
                    case en_AttrType.Label:
                    case en_AttrType.ChkBox:
                        criteria.Append("document_attribute.atb_value ");

                        if (UseWildCard)
                            criteria.Append("LIKE ");
                        else
                            criteria.Append("= ");

                        criteria.Append("'" + attr.atbValue_str.Trim() + "'");
                        break;

                    case en_AttrType.Date:
                    case en_AttrType.DateTime:
                        criteria.Append("CONVERT (datetime, document_attribute.atb_value, 103) = '" + ((DateTime)attr.atbValue).ToString(ConstData.DB_DATE_TIME) + "'");
                        break;

                    case en_AttrType.DatePeriod:
                        criteria.Append("CONVERT (datetime, document_attribute.atb_value, 103) >= '" + ((Period)attr.atbValue).From.ToString(ConstData.DB_DATE_TIME) + "' AND ");
                        criteria.Append("CONVERT (datetime, document_attribute.atb_value, 103) <= '" + ((Period)attr.atbValue).To.ToString(ConstData.DB_DATE_TIME) + "'");
                        break;

                    case en_AttrType.Combo:
                    case en_AttrType.FixedCombo:
                    case en_AttrType.ComboSingleSelect:
                    case en_AttrType.FixedComboSingleSelect:
                        List<string> combo_criterias = new List<string>();
                        foreach (int val in (List<int>)attr.atbValue)
                            combo_criterias.Add("(document_attribute.atb_value = '" + val + "')");

                        criteria.Append("(" + String.Join(" or ", combo_criterias) + ")");
                        break;
                }

            }
            else
            {
                criteria.Append("document_attribute.atb_value ");

                if (UseWildCard)
                    criteria.Append("LIKE ");
                else
                    criteria.Append("= ");

                criteria.Append("'" + attr.atbValue_str.Trim() + "'");
            }

            criteria.Append(")");

            return criteria.ToString();
        }

        /// <summary>
        /// Make sql string and return it.
        /// </summary>
        /// <returns>SQL String </returns>
        public override string ToString()
        {
            string ans = string.Empty;

            if (0 < Attributes.Count)
            {
                List<string> criterias = new List<string>();
                for (int i = 0; i < Attributes.Count; i++)
                {
                    List<string> or_criterias = new List<string>();
                    AttributeCriteriaBase attr = Attributes[i];
                    or_criterias.Add(GetAttrCriteria(attr.Values, attr.UseWildCard));

                    List<DocumentAttributeCombo> combo_items = DocumentAttributeController.GetComboItemsByChildrenAtb(attr.Values.id, attr.Values.atbValue_str);
                    if (0 < combo_items.Count)
                    {
                        List<DocumentAttribute> attrs = DocumentAttributeController.GetAttributesCache(attr_id: combo_items.Select(a => a.id_atb).Distinct().ToArray());

                        foreach (DocumentAttribute target_attr in attrs)
                        {
                            List<DocumentAttributeCombo> target_combo_items = combo_items.FindAll(a => a.id_atb == target_attr.id);
                            target_attr.atbValue = target_combo_items.Select(a => a.id).ToList();
                            or_criterias.Add(GetAttrCriteria(target_attr, false));
                        }
                    }

                    // Attribute link value
                    // search records from document_attribute_link table. 
                    var links = DocumentAttributeController.GetLinkedAttributeBy(attr.Values.id, attr.Values.atbValue_str);
                    foreach(var linkedAttr in links)
                    {
                        //AttributeCriteriaBase liknedattr = linkedAttr;
                        AttributeCriteriaBase linkedCriteria = new AttributeCriteriaBase() {
                            Values = new DocumentAttribute()
                        };

                        linkedCriteria.Values.id = linkedAttr.id;
                        linkedCriteria.Values.atbValue = linkedAttr.atbValue;
                        linkedCriteria.Values.id_type = en_AttrType.Label; // linked attribute is read only.

                        or_criterias.Add(GetAttrCriteria(linkedCriteria.Values, linkedCriteria.UseWildCard));
                    }

                    criterias.Add("(" + String.Join(" or ", or_criterias) + ")");
                }

                if (0 < criterias.Count)
                    ans = tb_AttributeValueSearch.Replace("<CRITERIA>", String.Join(" or ", criterias)) + " >= " + criterias.Count.ToString();
                else
                    ans= "d.id = -1 "; // no result
            }

            return ans;
        }
	}

//---------------------------------------------------------------------------------
    /*
	[Serializable]
    public class AttributeCriteriaBase : AttributeCriteriaBase
	{
	}
    */
	[Serializable]
    public class AttributeCriteriaBase //: DocumentAttribute
	{
		public DocumentAttribute Values { get; set; }
		public bool UseWildCard { get; set; }
	}

//---------------------------------------------------------------------------------
    /*
	[Serializable]
	public class SearchCriteriaBase : SearchCriteriaBase
		<DocumentAttributeBase,
		AttributeCriteriaBase,
		AttributeCriteriaCollectionBase>
	{
	}
    */

	[Serializable]
    public class SearchCriteria
	{
		[Serializable]
		public class ReviewCriteria
		{
			public en_ReviewStaus Status = en_ReviewStaus.INVALID;
			public List<int> UserIds = new List<int>();
		}

		public bool GetRecentDocuments { get; set; } = false;

        public List<int> DocIds { get; set; } = new List<int>();
        public string StrDocIds
		{
			get { return String.Join(" ", DocIds); }
			set
			{
				DocIds.Clear();
                if (value != null) {
                    string[] wrks = value.Split(' ');
                    foreach (string wrk in wrks)
                    {
                        int ans = 0;
                        int.TryParse(wrk.Trim(), out ans);

                        if (0 < ans)
                            DocIds.Add(ans);
                    }
                }
            }
		}

		public List<int> DocNotInIds { get; set; } =new List<int>();
		public List<int> DocTypeIds { get; set; } = new List<int>();
        public List<int> FolderIds { get; set; } = new List<int>();
        public List<int> UserIds { get; set; } = new List<int>();
        public AttributeCriteriaCollection AttributeCriterias { get; set; } = new AttributeCriteriaCollection();
        public List<string> Keywords { get; set; } = new List<string>();
        List<string> _titles = new List<string>();
        public List<string> Titles { get { _titles = _titles.Select( t => Document.ToValidTitle(t)).ToList(); return _titles; } set { _titles = value; } }
        public List<string> AbsoluteTitles { get; set; } = new List<string>();
        public List<string> Extensions { get; set; } = new List<string>();
        public List<Period> Date { get; set; } = new List<Period>();
        public ReviewCriteria Review { get; set; } = new ReviewCriteria();
        public en_SearchCriteriaMergeType MergeType { get; set; } = en_SearchCriteriaMergeType.OFF;
        public List<int> VersionIds { get; set; } = new List<int>();
        public List<en_file_Status> Statuses { get; set; } = new List<en_file_Status>();
        public List<en_file_Status> ExcludeStatuses { get; set; } = new List<en_file_Status>();
        public bool Archived { get; set; } = false;
        public List<en_file_Sp_Status> SpStatuses { get; set; } = new List<en_file_Sp_Status>();

//---------------------------------------------------------------------------------
	}		
}
