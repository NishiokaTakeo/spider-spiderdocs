using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Spider.Common;
using Spider.Types;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{

//---------------------------------------------------------------------------------
	[Serializable]
	public class DocumentAttribute:ICloneable
	{

        /// <summary>
        /// document_combo_item belong to current attribute
        /// </summary>
        public DocumentAttribute LinkedAttr {get;set;}

        static Logger logger = LogManager.GetCurrentClassLogger();

//---------------------------------------------------------------------------------

//---------------------------------------------------------------------------------
		bool Compare(DocumentAttribute src)
		{
			bool ans = true;

			PropertyInfo[] Properties = typeof(DocumentAttribute).GetProperties();

			foreach(PropertyInfo Property in Properties)
			{
				object src_val = Property.GetValue(src, null);
				object this_val = Property.GetValue(this, null);

				string src_str = (src_val != null ? src_val.ToString() : "");
				string this_str = (this_val != null ? this_val.ToString() : "");

				if(src_str != this_str)
				{
					ans = false;
					break;
				}
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		bool CompareValue(DocumentAttribute src)
		{
			bool ans = true;

			string str1 = "";
			string str2 = "";

			if(atbValue != null)
				str1 = atbValue_str;

			if(src.atbValue != null)
				str2 = src.atbValue_str;

			if(str1 != str2)
				ans = false;

			return ans;
		}

//---------------------------------------------------------------------------------
		public static DocumentAttribute Clone(DocumentAttribute attr)
		{
			DocumentAttribute copy_attr = new DocumentAttribute();
			PropertyInfo[] myObjectFields = typeof(DocumentAttribute).GetProperties();

			foreach(PropertyInfo fi in myObjectFields)
			{
				if(fi.CanWrite)
				{
					object val = Utilities.ObjectClone(fi.GetValue(attr, null));
					fi.SetValue(copy_attr, val, null);
				}
			}

			return copy_attr;
		}

		public static List<DocumentAttribute> Clone(List<DocumentAttribute> attrs)
		{
			List<DocumentAttribute> copy_attrs = new List<DocumentAttribute>();

			foreach(DocumentAttribute attr in attrs)
				copy_attrs.Add(Clone(attr));

			return copy_attrs;
		}

//---------------------------------------------------------------------------------
		public static List<DocumentAttribute> GetSameAttributeValues(List<DocumentAttribute> Base, List<List<DocumentAttribute>> CompList)
		{
			DocumentAttribute[] ans = new DocumentAttribute[Base.Count];
			Base.CopyTo(ans);

			foreach(List<DocumentAttribute> TargetAttrs in CompList)
			{
				foreach(DocumentAttribute wrk in ans)
				{
					DocumentAttribute TargetAttr = TargetAttrs.Find(a => a.id == wrk.id);

					if((TargetAttr == null) || (!TargetAttr.CompareValue(wrk)))
						wrk.InitializeValue();
				}
			}

			return ans.ToList();
		}

//---------------------------------------------------------------------------------
		public static bool CompareAttributes(List<DocumentAttribute> comp1, List<DocumentAttribute> comp2)
		{
			bool ans = true;

			if(comp1.Count != comp2.Count)
				return false;

			for(int i = 0; i < comp1.Count; i++)
			{
				if(!comp1[i].Compare(comp2[i]))
				{
					ans = false;
					break;
				}
			}

			return ans;
		}

//---------------------------------------------------------------------------------
        /// <summary>
        /// Check wheter attribute value is valid
        /// </summary>
        /// <returns>false if value isn't valid</returns>
		public bool IsValidValue()
		{
			if(atbValue == null)
				return false;

			bool ans = false;
			switch(this.id_type)
			{
			case en_AttrType.Text:
			case en_AttrType.Label:
				if((atbValue.GetType() == typeof(string)) && (!String.IsNullOrEmpty((string)atbValue)))
					ans = true;
				break;

			case en_AttrType.ChkBox:
				if((atbValue.GetType() == typeof(en_AttrCheckState)) && ((en_AttrCheckState)atbValue != en_AttrCheckState.Intermidiate))
					ans = true;
				break;

			case en_AttrType.Combo:
			case en_AttrType.FixedCombo:
			case en_AttrType.ComboSingleSelect:
			case en_AttrType.FixedComboSingleSelect:

                if (atbValue.GetType() == typeof(List<int>) && (0 < ((List<int>)atbValue).Count))
                {
					return true;
                }

                int v;
                if (int.TryParse(atbValue.ToString(), out v)) {

                    atbValue = new List<int>();
                    ((List<int>)atbValue).Add(v);
                    return true;

                }

				break;

			case en_AttrType.Date:
			case en_AttrType.DateTime:
				if((atbValue.GetType() == typeof(DateTime)) && ((DateTime)atbValue != new DateTime()))
					ans = true;
				break;

			case en_AttrType.DatePeriod:
				if((atbValue.GetType() == typeof(Period))
				&& ((((Period)atbValue).From != new DateTime()) || (((Period)atbValue).To != new DateTime())))
				{
					ans = true;
				}
				break;
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public bool IsValidValueToSave()
		{
			if(!IsValidValue())
				return false;

			bool ans = true;
			switch(this.id_type)
			{
			// DatePeriod cannot be saved into database
			case en_AttrType.DatePeriod:
				ans = false;
				break;
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public DocumentAttributeParams GetParamsByFolder(int id_folder, bool DefaultCorrection = true)
		{
			DocumentAttributeParams ans = null;

			if(this.system_field)
			{
				ans = new DocumentAttributeParams();
				ans.id_folder = 0;
				ans.required = ThreeStateBool.False;

			}else
			{
				ans = this.parameters.FirstOrDefault(a => a.id_folder == id_folder);

				if(DefaultCorrection)
				{
					DocumentAttributeParams Default = this.parameters.FirstOrDefault(a => a.id_folder == 0);

					if(ans == null)
					{
						ans = Default;

					}else if(0 < id_folder) // if a specific folde is selected
					{
						PropertyInfo[] props = typeof(DocumentAttributeParams).GetProperties();

						foreach(PropertyInfo prop in props)
						{
							object val = prop.GetValue(ans, null);
							object def_val = prop.GetValue(Default, null);

							if(typeof(ThreeStateBool) == val.GetType())
							{
								if((ThreeStateBool)val == ThreeStateBool.Intermidiate)
									prop.SetValue(ans, def_val, null);
							}
						}
					}
				}
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		int GetComboItemIdFromString(string val)
		{

			int ans = 0;

            if (string.IsNullOrWhiteSpace(val)) return ans;

            //List<DocumentAttributeCombo> combo_items = DocumentAttributeController.GetComboItems(false, new int[1] { this.id }, val);
            List<DocumentAttributeCombo> combo_items = DocumentAttributeController.GetComboItems( sort : false, attr_ids : new int[1] { this.id }, value_filter : val);

            if (AutoValueComplete && (combo_items.Count <= 0))
			{
                logger.Info("UNSUPPORT DETECT!:THIS SHOULD BE REPLACED TO comboitem.id");
                DocumentAttributeCombo new_item = new DocumentAttributeCombo();
				new_item.text = val;
				ans = DocumentAttributeController.InsertOrUpdateComboItem(new_item, this.id, true);

			}else if(combo_items.Count() > 0)
            {
                logger.Info("UNSUPPORT DETECT!:THIS SHOULD BE REPLACED TO comboitem.id");
                ans = combo_items.FirstOrDefault().id;
            }
            else
			{
                //val could be id of combo-item
                int.TryParse(val, out ans);
			}

			return ans;
		}


        #region DocumentAttributeView

        public int FolderId;

        DocumentAttributeParams GetParamsByFolder()
        {
            DocumentAttributeParams ans = this.GetParamsByFolder(FolderId, false);

            if (ans == null)
                ans = new DocumentAttributeParams();

            return ans;
        }

        public int required { get { return (int)GetParamsByFolder().required; } }
        public int int_id_type { get { return (int)this.id_type; } }

        #endregion

        #region DocumentAttributeBase

//---------------------------------------------------------------------------------
		public const int INVALID_POSITION = 99;
		public const int SYSTEM_ATTRIBUTES_START = 10000;

		public static string GetTypeName(en_AttrType type)
		{
			return tb_TypeName[(int)type - 1];
		}

		static protected readonly string[] tb_TypeName = new string[(int)en_AttrType.Max - 1]
		{
			"Text Box",
			"Check Box",
			"Date",
			"Combo Box",
			"",
			"",
			"",
			"Fixed Combo Box",
			"Label",
			"Combo Box Single",
			"Fixed Combo Box Single"
		};

//---------------------------------------------------------------------------------
		int _id_doc;
        public int id_doc
        {
            get { return _id_doc; }
            set
            {
                _id_doc = value;
            }
        }

		int _id;
        public int id
        {
            get { return _id; }
            set
            {
				if( _id != value)
				{
					LinkedAttr = null;
				}

                _id = value;
            }
        }

        /*
		virtual public int id
		{
			get { return _id; }
			set
			{
				_id = value;
			}
		}
        */
		public SystemAttributes SystemAttributeType
		{
			get
			{
				if(SYSTEM_ATTRIBUTES_START <= _id)
					return (SystemAttributes)_id;
				else
					return SystemAttributes.None;
			}

			set
			{
				if(value != SystemAttributes.None)
					_id = (int)value;
			}
		}

		public string name { get; set; }

		protected en_AttrType _id_type { get; set; }
		public en_AttrType id_type
		{
			set
			{
				_id_type = value;

				if(atbValue == null)
					InitializeValue();
			}
			get	{ return _id_type; }
		}

		public List<DocumentAttributeParams> parameters { get; set; }
		public int position { get; set; }
		public bool system_field { get; set; }
		public int period_research { get; set; }
		public int max_lengh { get; set; }
		public bool only_numbers { get; set; }
		public bool appear_query { get; set; }
		public bool appear_input { get; set; }

		/// <summary>
		/// <para>If true and set a value which does not exist in a value list of selected attribute type, the value will be automatically appended to the value list.</para>
		/// <para>Should be set before atbValue is entered.</para>
		/// </summary>
		public bool AutoValueComplete { get; set; }

        protected object _atbValue ;//= string.Empty;
		virtual public object atbValue
		{
			set
			{

                if (this.IsCombo() && (value.GetType() == typeof(string)))
				{
                    logger.Info("UNSUPPORT A USED ETECT!: {0}", value);
					int item_id = GetComboItemIdFromString((string)value);

					if( (0 < item_id) == false) return ;
				}

				if( _atbValue != value)
				{
					LinkedAttr = null;
				}

                _atbValue = value;

				//atbValue_Combo_str = "";
			}
			get {

                object tmp = _atbValue;
                if (_atbValue == null) return null;

                if (_atbValue.GetType() == typeof(bool))
                {
                    if ((bool)_atbValue)
                        tmp = en_AttrCheckState.True;
                    else
                        tmp = en_AttrCheckState.False;
                }
                else if (this.IsCombo())
                {
                    List<int> vals = new List<int>();
                    if (_atbValue.GetType() == typeof(int))
                    {
                        vals.Add((int)_atbValue);
                        tmp = vals;
                    }
                    else if(_atbValue.GetType() == typeof(string))
                    {
                        vals.Add(int.Parse(_atbValue.ToString()));
                        tmp = vals;
                    }
                }

                return tmp;
            }
		}

		public string atbValue_str
		{
			get
			{
				string ans = "";

				switch(id_type)
				{
				    case en_AttrType.Text:
				    case en_AttrType.Label:
					    ans = atbValue.ToString();
					    break;

				    case en_AttrType.ChkBox:
					    ans = ((en_AttrCheckState)atbValue).ToString();
					    break;

				    case en_AttrType.Date:
                        if ((DateTime)atbValue != new DateTime())
						    ans = ((DateTime)atbValue).ToString(ConstData.DATE);
					    break;

				    case en_AttrType.DateTime:
					    if((DateTime)atbValue != new DateTime())
						    ans = ((DateTime)atbValue).ToString(ConstData.DATE_TIME);
					    break;

				    case en_AttrType.DatePeriod:
					    ans = ((Period)atbValue).FromStr + "," + ((Period)atbValue).ToStr;
					    break;

				    case en_AttrType.Combo:
				    case en_AttrType.FixedCombo:
				    case en_AttrType.ComboSingleSelect:
				    case en_AttrType.FixedComboSingleSelect:

                        if (atbValue.GetType() == typeof(string)) return atbValue.ToString();

					    if(0 < ((List<int>)atbValue).Count)
						    ans = "'" + String.Join("','", (List<int>)atbValue) + "'";
					    break;
                    default:
                        if(atbValue == null)
                        {
                            logger.Error("atbValue is null: id:{0}, id_type:{1}", this.id,this.id_type);
                        }

                        ans = atbValue?.ToString();
                        break;
				}

				return ans;
			}

			set
			{
				switch(id_type)
				{
				case en_AttrType.Text:
				case en_AttrType.Label:
					atbValue = value;
					break;

				case en_AttrType.ChkBox:
					if(value.ToLower() == "false")
						atbValue = en_AttrCheckState.False;
					else if(value.ToLower() == "true")
						atbValue = en_AttrCheckState.True;
					else
						atbValue = en_AttrCheckState.Intermidiate;
					break;

				case en_AttrType.Date:
				case en_AttrType.DateTime:
					if(!String.IsNullOrEmpty(value))
						atbValue = DateTime.Parse(value);
					else
						atbValue = new DateTime();
					break;

				case en_AttrType.DatePeriod:
					string[] dates = value.Split(',');
					string from = "";
					string to = "";

					from = dates[0].Trim();

					if(2 < dates.Length)
						to = dates[1].Trim();

					Period period = new Period();
					period.SetDateFromString(from, to);

					atbValue = period;
					break;

				case en_AttrType.Combo:
				case en_AttrType.FixedCombo:
				case en_AttrType.ComboSingleSelect:
				case en_AttrType.FixedComboSingleSelect:
					List<int> intvals = new List<int>();
					value = value.Replace("'", "");
					string[] vals = value.Split(',');

					foreach(string val in vals)
					{
						int wrk;
						if(int.TryParse(val, out wrk))
							intvals.Add(wrk);
					}

					atbValue = intvals;
					break;
				}
			}
		}

        protected string atbValue_Combo_str { get; set; } = "";

        public string atbValueForUI
        {
            get
            {
                string ans = "";

                if (DocumentAttribute.IsComboTypes(this._id_type))
                {
                    if (String.IsNullOrEmpty(atbValue_Combo_str))
                    {
                        List<DocumentAttributeCombo> items = DocumentAttributeController.GetComboItems(sort: false, attr_ids: new int[] { this.id }, ids: ((List<int>)this.atbValue).ToArray());

                        atbValue_Combo_str = String.Join(", ", items.Select(a => a.text).ToArray());
                    }

                    ans = atbValue_Combo_str;

                }
                else
                {
                    ans = atbValue_str;
                }

                return ans;
            }
        }

        //---------------------------------------------------------------------------------
        public DocumentAttribute()
		{
			parameters = new List<DocumentAttributeParams>();
			id = 0;
			SystemAttributeType = SystemAttributes.None;
			id_type = en_AttrType.INVALID;
			position = INVALID_POSITION;
			period_research = 90;
			AutoValueComplete = false;
		}

        /*
		public DocumentAttributeBase()
		{
			parameters = new List<DocumentAttributeParams>();
			id = 0;
			SystemAttributeType = SystemAttributes.None;
			id_type = en_AttrType.INVALID;
			position = INVALID_POSITION;
			period_research = 90;
			AutoValueComplete = false;
		}
        */

//---------------------------------------------------------------------------------
		public void InitializeValue()
		{
			switch(this.id_type)
			{
			case en_AttrType.Text:
			case en_AttrType.Label:
				this.atbValue = "";
				break;

			case en_AttrType.ChkBox:
				this.atbValue = en_AttrCheckState.False;
				break;

			case en_AttrType.Combo:
			case en_AttrType.FixedCombo:
			case en_AttrType.ComboSingleSelect:
			case en_AttrType.FixedComboSingleSelect:
				this.atbValue = new List<int>();
				break;

			case en_AttrType.Date:
			case en_AttrType.DateTime:
				this.atbValue = new DateTime();
				break;

			case en_AttrType.DatePeriod:
				this.atbValue = new Period();
				break;
			}
		}

//---------------------------------------------------------------------------------
		static public bool IsComboTypes(en_AttrType type)
		{
			if((type == en_AttrType.Combo)
			|| (type == en_AttrType.FixedCombo)
			|| (type == en_AttrType.ComboSingleSelect)
            || (type == en_AttrType.FixedComboSingleSelect))
			{
				return true;

			}else
			{
				return false;
			}
		}

        public bool IsCombo()
        {
            return IsComboTypes(this.id_type);
        }
        public bool IsCheckBox()
        {
            return id_type == en_AttrType.ChkBox;
        }
        public bool IsDateTimeOrDate()
        {
            return id_type == en_AttrType.DateTime || id_type == en_AttrType.Date;
        }
        //---------------------------------------------------------------------------------
        #endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    /*
//---------------------------------------------------------------------------------
    /*
	public class DocumentAttributeView : DocumentAttribute
	{
		public int FolderId;

		DocumentAttributeParams GetParamsByFolder()
		{
			DocumentAttributeParams ans = this.GetParamsByFolder(FolderId, false);

			if(ans == null)
				ans= new DocumentAttributeParams();

			return ans;
		}

		public int required { get { return (int)GetParamsByFolder().required; } }
		public int int_id_type { get { return (int)this.id_type; } }
	}
//---------------------------------------------------------------------------------
     */
}
