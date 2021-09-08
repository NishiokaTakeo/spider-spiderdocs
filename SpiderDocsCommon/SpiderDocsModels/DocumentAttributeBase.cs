//using System;
//using System.Linq;
//using System.Reflection;
//using System.Collections.Generic;
//using Spider.Common;
//using Spider.Types;
//
////---------------------------------------------------------------------------------
//namespace SpiderDocsModule
//{
////---------------------------------------------------------------------------------
//	public enum en_AttrType
//	{
//		INVALID = 0,
//		Text = 1,
//		ChkBox = 2,
//		Date = 3,
//		Combo = 4,
//
//		DatePeriod = 5, // only for search
//		// MultipleCombo = 6, this is not used now
//		DateTime = 7, // only to determin if time should be included in string or not
//		FixedCombo = 8,
//		Label = 9,
//		ComboSingleSelect = 10,
//		FixedComboSingleSelect = 11,
//		Max
//	}
//
//	public enum en_AttrList
//	{
//		Id = 0,
//		Val,
//		Type,
//
//		Max
//	}
//
//	public enum en_AttrCheckState
//	{
//		False = 0,
//		True,
//		Intermidiate
//	}
//
//	// id more than 10000 is reserved for system fileds.
//	public enum SystemAttributes
//	{
//		None = 0,
//		MailSubject = 10000,
//		MailFrom = 10001,
//		MailTime = 10002,
//		MailTo = 10003,
//		MailIsComposed = 10004,
//
//		Max
//	}
//
////---------------------------------------------------------------------------------
//	//[Serializable]
	//public class DocumentAttributeBase
	//{
//--//-------------------------------------------------------------------------------
	//	public const int INVALID_POSITION = 99;
	//	public const int SYSTEM_ATTRIBUTES_START = 10000;
//
	//	public static string GetTypeName(en_AttrType type)
	//	{
	//		return tb_TypeName[(int)type - 1];
	//	}
//
	//	static protected readonly string[] tb_TypeName = new string[(int)en_AttrType.Max - 1]
	//	{
	//		"Text Box",
	//		"Check Box",
	//		"Date",
	//		"Combo Box",
	//		"",
	//		"",
	//		"",
	//		"Fixed Combo Box",
	//		"Label",
	//		"Combo Box Single",
	//		"Fixed Combo Box Single"
	//	};
//
//--//-------------------------------------------------------------------------------
	//	int _id_doc;
    //    public int id_doc
    //    {
    //        get { return _id_doc; }
    //        set
    //        {
    //            _id_doc = value;
    //        }
    //    }
//
	//	int _id;
    //    public int id
    //    {
    //        get { return _id; }
    //        set
    //        {
    //            _id = value;
    //        }
    //    }
//
    //    /*
	//	virtual public int id
	//	{
	//		get { return _id; }
	//		set
	//		{
	//			_id = value;
	//		}
	//	}
    //    */
	//	public SystemAttributes SystemAttributeType
	//	{
	//		get
	//		{
	//			if(SYSTEM_ATTRIBUTES_START <= _id)
	//				return (SystemAttributes)_id;
	//			else
	//				return SystemAttributes.None;
	//		}
//
	//		set
	//		{
	//			if(value != SystemAttributes.None)
	//				_id = (int)value;
	//		}
	//	}
//
	//	public string name { get; set; }
//
	//	protected en_AttrType _id_type { get; set; }
	//	public en_AttrType id_type
	//	{
	//		set
	//		{
	//			_id_type = value;
//
	//			if(atbValue == null)
	//				InitializeValue();
	//		}
	//		get	{ return _id_type; }
	//	}
//
	//	public List<DocumentAttributeParams> parameters { get; set; }
	//	public int position { get; set; }
	//	public bool system_field { get; set; }
	//	public int period_research { get; set; }
	//	public int max_lengh { get; set; }
	//	public bool only_numbers { get; set; }
	//	public bool appear_query { get; set; }
	//	public bool appear_input { get; set; }
//
	//	/// <summary>
	//	/// <para>If true and set a value which does not exist in a value list of selected attribute type, the value will be automatically //appended to the value list.</para>
	//	/// <para>Should be set before atbValue is entered.</para>
	//	/// </summary>
	//	public bool AutoValueComplete { get; set; }
//
	//	protected object _atbValue;
	//	virtual public object atbValue
	//	{
	//		set
	//		{
	//			if(value.GetType() == typeof(bool))
	//			{
	//				if((bool)value)
	//					_atbValue = en_AttrCheckState.True;
	//				else
	//					_atbValue = en_AttrCheckState.False;
//
	//			}else if(IsComboTypes(id_type))
	//			{
	//				if(value.GetType() == typeof(int))
	//				{
	//					List<int> vals = new List<int>();
	//					vals.Add((int)value);
	//					_atbValue = vals;
//
	//				}else
	//				{
	//					_atbValue = value;
	//				}
//
	//			}else
	//			{
	//				_atbValue = value;
	//			}
//
	//			atbValue_Combo_str = "";
	//		}
	//		get { return _atbValue; }
	//	}
//
	//	public string atbValue_str
	//	{
	//		get
	//		{
	//			string ans = "";
//
	//			switch(id_type)
	//			{
	//			    case en_AttrType.Text:
	//			    case en_AttrType.Label:
	//				    ans = atbValue.ToString();
	//				    break;
//
	//			    case en_AttrType.ChkBox:
	//				    ans = ((en_AttrCheckState)atbValue).ToString();
	//				    break;
//
	//			    case en_AttrType.Date:
	//				    if((DateTime)atbValue != new DateTime())
	//					    ans = ((DateTime)atbValue).ToString(ConstData.DATE);
	//				    break;
//
	//			    case en_AttrType.DateTime:
	//				    if((DateTime)atbValue != new DateTime())
	//					    ans = ((DateTime)atbValue).ToString(ConstData.DATE_TIME);
	//				    break;
//
	//			    case en_AttrType.DatePeriod:
	//				    ans = ((Period)atbValue).FromStr + "," + ((Period)atbValue).ToStr;
	//				    break;
//
	//			    case en_AttrType.Combo:
	//			    case en_AttrType.FixedCombo:
	//			    case en_AttrType.ComboSingleSelect:
	//			    case en_AttrType.FixedComboSingleSelect:
	//				    if(0 < ((List<int>)atbValue).Count)
	//					    ans = "'" + String.Join("','", (List<int>)atbValue) + "'";
	//				    break;
    //                default:
    //                    ans = atbValue.ToString();
    //                    break;
	//			}
//
	//			return ans;
	//		}
//
	//		set
	//		{
	//			switch(id_type)
	//			{
	//			case en_AttrType.Text:
	//			case en_AttrType.Label:
	//				atbValue = value;
	//				break;
//
	//			case en_AttrType.ChkBox:
	//				if(value.ToLower() == "false")
	//					atbValue = en_AttrCheckState.False;
	//				else if(value.ToLower() == "true")
	//					atbValue = en_AttrCheckState.True;
	//				else
	//					atbValue = en_AttrCheckState.Intermidiate;
	//				break;
//
	//			case en_AttrType.Date:
	//			case en_AttrType.DateTime:
	//				if(!String.IsNullOrEmpty(value))
	//					atbValue = DateTime.Parse(value);
	//				else
	//					atbValue = new DateTime();
	//				break;
//
	//			case en_AttrType.DatePeriod:
	//				string[] dates = value.Split(',');
	//				string from = "";
	//				string to = "";
//
	//				from = dates[0].Trim();
//
	//				if(2 < dates.Length)
	//					to = dates[1].Trim();
//
	//				Period period = new Period();
	//				period.SetDateFromString(from, to);
//
	//				atbValue = period;
	//				break;
//
	//			case en_AttrType.Combo:
	//			case en_AttrType.FixedCombo:
	//			case en_AttrType.ComboSingleSelect:
	//			case en_AttrType.FixedComboSingleSelect:
	//				List<int> intvals = new List<int>();
	//				value = value.Replace("'", "");
	//				string[] vals = value.Split(',');
//
	//				foreach(string val in vals)
	//				{
	//					int wrk;
	//					if(int.TryParse(val, out wrk))
	//						intvals.Add(wrk);
	//				}
//
	//				atbValue = intvals;
	//				break;
	//			}
	//		}
	//	}
//
	//	protected string atbValue_Combo_str { get; set; }
//
//--//-------------------------------------------------------------------------------
	//	public DocumentAttributeBase()
	//	{
	//		parameters = new List<DocumentAttributeParams>();
	//		id = 0;
	//		SystemAttributeType = SystemAttributes.None;
	//		id_type = en_AttrType.INVALID;
	//		position = INVALID_POSITION;
	//		period_research = 90;
	//		AutoValueComplete = false;
	//	}
//
//--//-------------------------------------------------------------------------------
	//	public void InitializeValue()
	//	{
	//		switch(this.id_type)
	//		{
	//		case en_AttrType.Text:
	//		case en_AttrType.Label:
	//			this.atbValue = "";
	//			break;
//
	//		case en_AttrType.ChkBox:
	//			this.atbValue = en_AttrCheckState.False;
	//			break;
//
	//		case en_AttrType.Combo:
	//		case en_AttrType.FixedCombo:
	//		case en_AttrType.ComboSingleSelect:
	//		case en_AttrType.FixedComboSingleSelect:
	//			this.atbValue = new List<int>();
	//			break;
//
	//		case en_AttrType.Date:
	//		case en_AttrType.DateTime:
	//			this.atbValue = new DateTime();
	//			break;
//
	//		case en_AttrType.DatePeriod:
	//			this.atbValue = new Period();
	//			break;
	//		}
	//	}
//
//--//-------------------------------------------------------------------------------
	//	static public bool IsComboTypes(en_AttrType type)
	//	{
	//		if((type == en_AttrType.Combo)
	//		|| (type == en_AttrType.FixedCombo)
	//		|| (type == en_AttrType.ComboSingleSelect)
    //        || (type == en_AttrType.FixedComboSingleSelect))
	//		{
	//			return true;
//
	//		}else
	//		{
	//			return false;
	//		}
	//	}
//
    //    public bool IsCombo()
    //    {
    //        return IsComboTypes(this.id_type);
    //    }
//
//--//-------------------------------------------------------------------------------
	//}
//}
