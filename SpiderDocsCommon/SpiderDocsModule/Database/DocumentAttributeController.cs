using System;
using System.Collections.Generic;
using System.Linq;
using Spider.Data;
using Spider.Types;
using System.Windows.Forms;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class DocumentAttributeController : DocumentAttributeController<SpiderDocsModule.Document>
	{
	}

    public class DocumentAttributeController<T> where T : SpiderDocsModule.Document
	{
        static Logger logger = LogManager.GetCurrentClassLogger();
//---------------------------------------------------------------------------------
		static readonly string[] tb_ViewDocumentAttribute = new string[]{
			"id_doc",
			"id_atb",
			"id_field_type",
			"atb_value"
		};

		static readonly string[] tb_AttributeEdit = new string[]
		{
			"name",
			"id_type",
			"period_research",
			"max_lengh",
			"only_numbers",
			"appear_query",
			"appear_input"
		};

		static readonly string[] tb_Attribute = new string[]
		{
			"id",
			"system_field"

		}.Concat(tb_AttributeEdit).ToArray();

		static readonly string[] tb_AttributeParamsEdit = new string[]
		{
			"id_attribute",
			"id_folder",
			"required"
		};

		static readonly string[] tb_AttributeParams = new string[]
		{
			"id"

		}.Concat(tb_AttributeParamsEdit).ToArray();

		static readonly string[] tb_AttributeDocType = new string[]
		{
			"id_attribute",
			"position"
		};

		public static readonly string[] tb_DocumentAttribute = new string[]
		{
			"id_doc",
			"id_atb",
			"atb_value",
			"id_field_type"
		};

		public static readonly string[] tb_LinkedAttribute = new string[]
		{
			"id_atb",
			"atb_value",
			"linked_id_atb",
			"linked_value"
		};
//---------------------------------------------------------------------------------
// this is an ordinary function to get attribute ----------------------------------
//---------------------------------------------------------------------------------
		public static DocumentAttribute GetAttribute(int attr_id)
		{
			return GetAttributesCache(false, new int[] { attr_id }).FirstOrDefault() ?? new DocumentAttribute();
		}
        public static List<DocumentAttribute> GetAttributesCache(bool exclude_system_fields = false, params int[] attr_id)
		{
			IEnumerable<DocumentAttribute> cached =  Cache.GetAttributes();

			if((attr_id != null) && (0 < attr_id.Length))
				cached = cached.Where( a => attr_id.Contains(a.id) );

			if( exclude_system_fields )
				cached = cached.Where( a => a.system_field == false) ;

			return cached.ToList();
		}

        public static List<DocumentAttribute> GetAttributes(bool exclude_system_fields = false, params int[] attr_id)
		{
            List<DocumentAttribute> ans = new List<DocumentAttribute>();

			SqlOperation sql;

			sql = new SqlOperation("attributes", SqlOperationMode.Select);
			sql.Fields(tb_Attribute);

			if((attr_id != null) && (0 < attr_id.Length))
				sql.Where_In("id", attr_id);

			if(exclude_system_fields)
				sql.Where("system_field", false);

			sql.Where("[enabled]",true);

			sql.Commit();

			while(sql.Read())
			{
                DocumentAttribute wrk = new DocumentAttribute();

				wrk.id = Convert.ToInt32(sql.Result_Obj("id"));

				if(sql.Result<bool>("system_field"))
				{
					wrk.SystemAttributeType = (SystemAttributes)sql.Result<int>("id");
					wrk.system_field = true;
				}

				wrk.name = sql.Result("name");
				wrk.id_type = (en_AttrType)Convert.ToInt32(sql.Result_Obj("id_type"));
				wrk.period_research = Convert.ToInt32(sql.Result_Obj("period_research"));
				wrk.max_lengh = Convert.ToInt32(sql.Result_Obj("max_lengh"));
				wrk.only_numbers = (bool)sql.Result_Obj("only_numbers");
				wrk.appear_query = (bool)sql.Result_Obj("appear_query");
				wrk.appear_input = (bool)sql.Result_Obj("appear_input");

				ans.Add(wrk);
			}


			if(0 < ans.Count())
			{
				sql = new SqlOperation("attribute_params", SqlOperationMode.Select);
				sql.Fields(tb_AttributeParams);
				sql.Where_In("id_attribute", ans.Select(a => a.id).ToArray());
				sql.Commit();

				while(sql.Read())
				{
					DocumentAttributeParams parameters = new DocumentAttributeParams();
					parameters.id_folder = sql.Result<int>("id_folder");
					parameters.required = (ThreeStateBool)sql.Result<int>("required");

                    DocumentAttribute wrk = ans.First(a => a.id == sql.Result<int>("id_attribute"));
					wrk.parameters.Add(parameters);
				}
			}

			return ans;
		}

        static public DocumentAttribute GetLinkedAttribute(int id_atb, string atb_value)
        {
			atb_value = atb_value?.Trim();

            var attr = new DocumentAttribute();

            var sql = new SqlOperation("document_attribute_link", SqlOperationMode.Select);

            sql.Fields("id_atb", "atb_value", "linked_id_atb", "linked_value");

            sql.Where("id_atb", id_atb);
            sql.Where("atb_value", atb_value);

            sql.Commit();

            while (sql.Read())
            {
				attr = GetAttribute(sql.Result<int>("linked_id_atb"));
                attr.atbValue = sql.Result("linked_value");
                //attr.id_type = en_AttrType.Label;

                return attr;
            }

            return null;
        }

        static public bool DeleteLinkedAttribute(int id_atb, string atb_value, SqlOperation sql = null)
        {
            var attr = new DocumentAttribute();

            sql = sql == null ?
								new SqlOperation("document_attribute_link", SqlOperationMode.Delete)
								:
								SqlOperation.GetSqlOperation(sql, "document_attribute_link", SqlOperationMode.Delete);

            sql.Where("id_atb", id_atb);
            sql.Where("atb_value", atb_value);

            sql.Commit();

            return true;
        }

		static public bool InsertLinkedAttribute( int key_id, string key_value, int linkedAtbId, string linkedValue , SqlOperation sql = null)
        {
			key_value = key_value.Trim();
			linkedValue = linkedValue.Trim();

            var attr = new DocumentAttribute();

            sql = sql == null ?
								new SqlOperation("document_attribute_link", SqlOperationMode.Insert)
								:
								SqlOperation.GetSqlOperation(sql, "document_attribute_link", SqlOperationMode.Insert);

			object[] vals = new object[]
			{
				key_id,
				key_value,
				linkedAtbId,
				linkedValue
			};

			sql.Fields(tb_LinkedAttribute, vals);
			sql.Commit();

            return true;
        }


        static public List<DocumentAttribute> GetLinkedAttributeBy(int linkedIdAtb, string linkedValue)
        {
            linkedValue = linkedValue?.Trim();

            var attrs = new List<DocumentAttribute>();

            var sql = new SqlOperation("document_attribute_link", SqlOperationMode.Select);

            sql.Fields("id_atb", "atb_value", "linked_id_atb", "linked_value");

            sql.Where("linked_id_atb", linkedIdAtb);
            sql.Where("linked_value", linkedValue);

            sql.Commit();

            while (sql.Read())
            {
                var attr = GetAttribute(sql.Result<int>("id_atb"));
                attr.atbValue = sql.Result("atb_value");

                attrs.Add(attr);

                //attr.id_type = en_AttrType.Label;

                //return attrs;
            }

            return attrs;
        }
        //---------------------------------------------------------------------------------
        static public DocumentAttribute UpdateOrInsertAttribute(DocumentAttribute attr)
		{
			object[] vals =
			{
				attr.name,
				(int)attr.id_type,
				attr.period_research,
				attr.max_lengh,
				attr.only_numbers,
				attr.appear_query,
				attr.appear_input
			};

			SqlOperation sql;

			if(0 < attr.id)
			{
				sql = new SqlOperation("attributes", SqlOperationMode.Update);
				sql.Where("id", attr.id);
				sql.Fields(tb_AttributeEdit, vals);
				sql.Commit();

				sql = new SqlOperation("attribute_params", SqlOperationMode.Delete);
				sql.Where("id_attribute", attr.id);
				sql.Commit();

			}else
			{
				sql = new SqlOperation();
				sql.BeginTran();

				sql.Reload("attributes", SqlOperationMode.Select_Scalar);
				sql.Where("id", SqlOperationOperator.LESS_THAN, DocumentAttribute.SYSTEM_ATTRIBUTES_START);
				attr.id = sql.GetMaxId() + 1;

				sql.Reload("attributes", SqlOperationMode.Insert);
				sql.Field("id", attr.id);
				sql.Fields(tb_AttributeEdit, vals);
				sql.Commit();

				sql.CommitTran();
			}

			foreach(DocumentAttributeParams wrk in attr.parameters)
			{
				object[] vals2 =
				{
					attr.id,
					wrk.id_folder,
					(int)wrk.required
				};

				sql = new SqlOperation("attribute_params", SqlOperationMode.Insert);
				sql.Fields(tb_AttributeParamsEdit, vals2);
				sql.Commit();
			}

			return attr;
		}

//---------------------------------------------------------------------------------
        /// <summary>
        /// Remove attribute and all relative relationship data.
        /// Attributes have relationship to attribute_params, attributes_doc_type,attribute_combo_item and attirbute_combo_item_children
        /// </summary>
        /// <param name="attr_id">attribute ID</param>
		public static void DeleteAttribute(int attr_id)
		{
			SqlOperation sql;

			sql = new SqlOperation("attributes", SqlOperationMode.Delete);
			sql.Where("id", attr_id);
			sql.Commit();

			sql = new SqlOperation("attribute_params", SqlOperationMode.Delete);
			sql.Where("id_attribute", attr_id);
			sql.Commit();

            sql = new SqlOperation("attribute_doc_type", SqlOperationMode.Delete);
            sql.Where("id_attribute", attr_id);
            sql.Commit();

            /*
             both attribute_combo_item and attirbute_combo_item_children should be removed at this time.
            */
        }

//---------------------------------------------------------------------------------
// set attributes with values to documents ----------------------------------------
//---------------------------------------------------------------------------------
		public static T SetToDocument(T doc)
		{
			List<T> docs = new List<T>();
			docs.Add(doc);

			return SetToDocument(docs)[0];
		}

		public static List<T> SetToDocument(List<T> docs)
		{
			GetByDocId(false, false, docs, docs.Select(a => a.id).ToArray(), null);

			return docs;
		}

////---------------------------------------------------------------------------------
//// get attribute list of specified documents' document types ----------------------
////---------------------------------------------------------------------------------
//		public static List<DocumentAttribute> GetListByFolderId(params int[] id_folder)
//		{
//			return GetByDocId(true, true, null, null, id_folder);
//		}

//---------------------------------------------------------------------------------
// this is a local function to deal attributes and documents ----------------------
//---------------------------------------------------------------------------------
		static List<DocumentAttribute> GetByDocId(bool list, bool exclude_system_fields, List<T> docs, int[] id_doc, int[] id_folder)
		{

			if((docs != null) && (0 < docs.Count))
			{
				foreach(T doc in docs)
					doc.Attrs.Clear();
			}

			List<DocumentAttribute> ans = new List<DocumentAttribute>();

			SqlOperation sql = new SqlOperation("view_document_attribute", SqlOperationMode.Select);
			sql.Distinct = list;

			sql.Fields(tb_ViewDocumentAttribute);

			if(id_doc != null)
				sql.Where_In("id_doc", id_doc);

			if(id_folder != null)
				sql.Where_In("id_folder", id_folder);

			sql.Where("id_status",SqlOperationOperator.LESS_THAN, en_file_Status.deleted);
			sql.Where("atb_value",SqlOperationOperator.NOT_EQUAL,"");

			if(exclude_system_fields)
			{
				sql.Where("system_field",SqlOperationOperator.EQUAL,false);
			}

            sql.OrderBy("id_doc", SqlOperation.en_order_by.Ascent);
            sql.OrderBy("id_atb", SqlOperation.en_order_by.Ascent);
            sql.OrderBy("id_field_type", SqlOperation.en_order_by.Ascent);

			sql.Commit();

            string lastgroup = string.Empty;
			int _id_doc = 0;
            en_AttrType _type = en_AttrType.INVALID;

			/*
				Addattribute to ans
			*/
            Action<int,en_AttrType, DocumentAttribute> Add = delegate(int __id_doc,en_AttrType attrtype, DocumentAttribute _attribute)
            {
                if (!DocumentAttribute.IsComboTypes(attrtype))
                {
                    ans.Add(new DocumentAttribute
                    {
                        id = _attribute.id,
                        atbValue = _attribute.atbValue,
                    });
                }
                else
                {
                    // if id_doc, id_atb and id_field_type are changed,Add value to collection like '1','5' format.
                    //_attribute.atbValue = ((string)_attribute.atbValue).Replace("''", "','");
                    _attribute.atbValue = ((string)_attribute.atbValue).Replace("''", ",").Replace("'","");
                    ans.Add(new DocumentAttribute
                    {
                        id = _attribute.id,
                        atbValue = _attribute.atbValue,
                    });
                }

				if((docs != null) && (0 < docs.Count))
                {
                    T target = docs.Find(a => a.id == __id_doc);
                    target.Attrs.Add(_attribute);
                }
            };

            /*
             * Create List<DocumentAttribute> from SQL. combovalue needs to be '1','5','10' format from three rows like [1,5,10].( this is prefomance improvement )
             */
            DocumentAttribute attribute = new DocumentAttribute() { atbValue = string.Empty }; ;
			while(sql.Read())
			{
                string
                    group = string.Format("{0}-{1}-{2}", sql.Result_Int("id_doc"), sql.Result_Int("id_atb"), sql.Result_Int("id_field_type"));

                en_AttrType type = (en_AttrType)Enum.Parse(typeof(en_AttrType), sql.Result_Int("id_field_type").ToString());


                // will set group only first record.
                lastgroup = string.IsNullOrEmpty(lastgroup) ? group : lastgroup;

                if (lastgroup != group)
                {
                    Add(_id_doc,_type, attribute);

                    attribute = new DocumentAttribute() { atbValue = string.Empty};

                }

                if (!DocumentAttribute.IsComboTypes(type))
                {
					_id_doc = sql.Result_Int("id_doc");
                    attribute.id = sql.Result_Int("id_atb");
                    attribute.atbValue = sql.Result("atb_value");
                }
                else
                {
					_id_doc = sql.Result_Int("id_doc");
                    attribute.id = sql.Result_Int("id_atb");
					attribute.atbValue += string.Format("'{0}'", sql.Result("atb_value"));
                    //attribute.atbValue += string.Format("'{0}'", sql.Result("atb_value").Replace("'",""));
                }

				_type = type;

                lastgroup = group;
			}

            if (!string.IsNullOrEmpty(lastgroup))
                Add(_id_doc,_type, attribute);

			List<DocumentAttribute> attrs = GetAttributesCache(exclude_system_fields, ans.Select(x => { return x.id;}).Distinct().ToArray());

			// Look up attribute from attrs and copy it to ans.
            for (int i = 0; i < ans.Count; i++)
				ans[i] = SetAttibute(ans[i], attrs);

			if((docs != null) && (0 < docs.Count))
			{
				foreach(T doc in docs)
				{
					for(int i = 0; i < doc.Attrs.Count; i++)
						doc.Attrs[i] = SetAttibute(doc.Attrs[i], attrs);
				}
			}

			if(list)
			{
				List<int> IndexToRemove = new List<int>();

				for(int i = (ans.Count - 1); 0 <= i; i--)
				{
					if(DocumentAttribute.IsComboTypes(ans[i].id_type) && (1 < ((List<int>)ans[i].atbValue).Count))
					{
						foreach(int search in ((List<int>)ans[i].atbValue))
						{
							if(!ans.Exists(a =>
								   DocumentAttribute.IsComboTypes(a.id_type)
								&& ((List<int>)a.atbValue).Count == 1
								&& ((List<int>)a.atbValue)[0] == search))
							{

                                /*
                                 * Bug Fix , cast error date to int
                                 * //DocumentAttribute wrk = DocumentAttribute.Clone(ans[0]);
                                 */
                                DocumentAttribute wrk = DocumentAttribute.Clone(ans[i]);

								List<int> vals = new List<int>();
								vals.Add(search);
								wrk.atbValue = vals;

								ans.Add(wrk);
							}
						}

						IndexToRemove.Add(i);
					}
				}

				foreach(int idx in IndexToRemove)
					ans.RemoveAt(idx);
			}

			return ans;
		}


		static DocumentAttribute SetAttibute(DocumentAttribute target, List<DocumentAttribute> src)
		{
			object backup = target.atbValue;
			string val_str = Convert.ToString(backup);

			DocumentAttribute wrk = src.Find(a => a.id == target.id);
			target = Utilities.ObjectClone(wrk);

			switch(target.id_type)
			{
			case en_AttrType.Text:
			case en_AttrType.Label:
				target.atbValue = val_str;
				break;

			case en_AttrType.ChkBox:
				if(val_str == "True")
					target.atbValue = en_AttrCheckState.True;
				else
					target.atbValue = en_AttrCheckState.False;
				break;

			case en_AttrType.Date:
			case en_AttrType.DateTime:
				target.atbValue = DateTime.Parse(val_str);
				break;

			case en_AttrType.Combo:
			case en_AttrType.FixedCombo:
			case en_AttrType.ComboSingleSelect:
			case en_AttrType.FixedComboSingleSelect:
				target.atbValue_str = val_str;
				break;
			}

			return target;
		}

//---------------------------------------------------------------------------------
// get DISTINCT attribute id of specified document types --------------------------
//---------------------------------------------------------------------------------
		public static List<AttributeDocType> GetIdListByDocType(params int[] doc_type_id)
		{
            if (doc_type_id == null) doc_type_id = new int[] { };

            SqlOperation sql;
			List<AttributeDocType> ans = new List<AttributeDocType>();

			// Get attribute ids which belong to the document type
			sql = new SqlOperation("view_attribute_doc_type", SqlOperationMode.Select);
			sql.Fields("id","id_doc_type","id_attribute","position","duplicate_chk");

			if(0 < doc_type_id.Length)
			{
				sql.Where_In("id_doc_type", doc_type_id);
				sql.OrderBy("position", SqlOperation.en_order_by.Ascent);

			}else
			{
				sql.Distinct = true;
			}

			sql.Commit();

			while(sql.Read())
			{
				ans.Add( new AttributeDocType(){
					id =  Convert.ToInt32(sql.Result_Obj("id")),
					id_doc_type =  Convert.ToInt32(sql.Result_Obj("id_doc_type")),
					id_attribute =  Convert.ToInt32(sql.Result_Obj("id_attribute")),
					position =  Convert.ToInt32(sql.Result_Obj("position")),
                    duplicate_chk = Convert.ToBoolean(sql.Result_Obj("duplicate_chk"))
                });
			}

			return ans;
		}

//---------------------------------------------------------------------------------
// Save and Update Attributes -----------------------------------------------------
//---------------------------------------------------------------------------------
        /// <summary>
        /// Save Attribute to document_attributes table.
        /// Combobox item will be inserted multiple rows
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="objDoc"></param>
        public static void SaveAttribute(SqlOperation sql, Document objDoc)
		{
			sql = SqlOperation.GetSqlOperation(sql, "document_attribute", SqlOperationMode.Delete);

			sql.Where("id_doc", objDoc.id);

			SqlOperationCriteriaCollection Collection = new SqlOperationCriteriaCollection();
			Collection.m_AndOr = SqlOperationAndOr.OR;
			Collection.Add(new SqlOperationCriteria("id_atb", SqlOperationOperator.LESS_THAN, DocumentAttribute.SYSTEM_ATTRIBUTES_START));
			if(0 < objDoc.Attrs.Count)
				Collection.Add(new SqlOperationCriteria("id_atb", SqlOperationOperator.IN, objDoc.Attrs.Select(a => a.id).ToArray()));
			sql.Where(Collection);

			sql.Commit();

			foreach(DocumentAttribute Attr in objDoc.Attrs)
			{
				if(!Attr.IsValidValueToSave())
					continue;

                List<string> values = Attr.IsCombo()
                                        ? ((List<int>)Attr.atbValue).Select(x => x.ToString()).ToList()
                                        : new List<string>() { Attr.atbValue_str };

                values.ForEach(atb_value =>
                {
                    sql = SqlOperation.GetSqlOperation(sql, "document_attribute", SqlOperationMode.Insert);

                    object[] vals = new object[]
				    {
					    objDoc.id,
					    Attr.id,
					    atb_value,
					    (int)Attr.id_type
				    };

                    sql.Fields(tb_DocumentAttribute, vals);
                    sql.Commit();
                });

				// // Delete/Insert linked attributes if linked value is filled
				// if( Attr.LinkedAttr?.id > 0

				// 	&&

			 	// 	false == string.IsNullOrWhiteSpace(Attr.LinkedAttr?.atbValue_str))
				// {
				// 	// Insert new linked value if not exists.
				// 	var linkedAttr = GetLinkedAttribute(Attr.id, Attr.atbValue_str ) ;
				// 	if( linkedAttr == null)
				// 	{
				// 		InsertLinkedAttribute(Attr.id,Attr.atbValue_str, Attr.LinkedAttr.id, Attr.LinkedAttr.atbValue_str, sql);
				// 	}
				// }
			}

			objDoc.UpdateLinkedAttribute();
		}

//---------------------------------------------------------------------------------
// methods for combo box type attribute -------------------------------------------
//---------------------------------------------------------------------------------
		public static List<DocumentAttributeCombo> GetComboItems(params int[] attr_ids)
		{
			return GetComboItems(false, attr_ids, null);
		}

		public static List<DocumentAttributeCombo> GetComboItems(bool sort, params int[] attr_ids)
		{
			return GetComboItems(sort, attr_ids, null);
		}

		public static List<DocumentAttributeCombo> GetComboItems(bool sort = false, int[] attr_ids = null, string value_filter = null, int[] ids = null, bool include_children = true)
		{
			List<DocumentAttributeCombo> ans = new List<DocumentAttributeCombo>();

			SqlOperation sql = new SqlOperation("view_attribute_combo_item", SqlOperationMode.Select);
			sql.Fields("id", "value", "id_atb");

			if((attr_ids != null) && (0 < attr_ids.Length))
				sql.Where_In("id_atb", attr_ids);

			if(!String.IsNullOrEmpty(value_filter))
				sql.Where("value", value_filter);

			if((ids != null) && (0 < ids.Length))
				sql.Where_In("id", ids);

			sql.Commit();

            // Temporary fix
			while(sql.Read())
			{
                DocumentAttributeCombo wrk = new DocumentAttributeCombo
                {
                    id = sql.Result<int>("id"),
                    id_atb = sql.Result<int>("id_atb"),
                    text = sql.Result("value")
                };

                ans.Add(wrk);
			}

            if (0 < ans.Count && include_children)
            {
                BindChildren(ref ans, ans.Select(x => x.id_atb).Distinct().ToArray());
            }


            if (sort)
                ans = ans.OrderBy(a => a.text).ToList();

            return ans;
		}

        //---------------------------------------------------------------------------------
        /// <summary>
        /// Insert/Update AttributeComboItem and Children if children exists.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="id_atb"></param>
        /// <param name="insert"></param>
        /// <returns>id of attribute_combo_item</returns>
        public static int InsertOrUpdateComboItem(DocumentAttributeCombo item, int id_atb, bool insert = false)
		{

            if(logger.IsDebugEnabled) logger.Debug("{0}, {1}, {2}", id_atb, insert, Newtonsoft.Json.JsonConvert.SerializeObject(item));

            int ans = 0;
			SqlOperation sql;
            try {
                if(insert)
                {
                    sql = new SqlOperation("attribute_combo_item", SqlOperationMode.Insert);

                }else
                {
                    sql = new SqlOperation("attribute_combo_item", SqlOperationMode.Update);
                    sql.Where("id", item.id);
                    ans = item.id;
                }

                if(0 < id_atb)
                    sql.Field("id_atb", id_atb);

                sql.Field("value", item.text);

                if (insert)
                    ans = sql.Commit<int>();
                else
                    sql.Commit();


                InsertOrUpdateComboItemChildren(ans, item.children);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return ans;
		}

		/// <summary>
		/// Update/Insert Combo Item Children
		/// </summary>
		/// <param name="id_combo_item"></param>
		/// <param name="children"></param>
		public static void InsertOrUpdateComboItemChildren(int id_combo_item,List<DocumentAttribute> children)
		{
			if( id_combo_item == 0 || children == null ) return;

			SqlOperation sql = new SqlOperation("attribute_combo_item_children", SqlOperationMode.Delete);
            sql.Where("id_combo_item", id_combo_item);
            sql.Commit();

            foreach (DocumentAttribute attr in children)
			{
				if(!String.IsNullOrEmpty(attr.atbValue_str))
				{
					sql = new SqlOperation("attribute_combo_item_children", SqlOperationMode.Insert);
					sql.Field("id_combo_item", id_combo_item);
					sql.Field("id_atb", attr.id);
					sql.Field("atb_value", attr.atbValue_str);
					sql.Commit();
				}
			}
		}

//---------------------------------------------------------------------------------
		public static void DeleteComboItems(int[] ids = null, int[] attr_ids = null)
		{
			SqlOperation sql;

			List<int> DeleteIds = new List<int>();

			if(ids != null)
				DeleteIds.AddRange(ids);

			if(attr_ids != null)
			{
				List<DocumentAttributeCombo> combo = GetComboItems(attr_ids);
				DeleteIds.AddRange(combo.Select(a => a.id));
			}

			DeleteComboItemChildren(ids, null);

			sql = new SqlOperation("attribute_combo_item", SqlOperationMode.Delete);
			sql.Where_In("id", ids.ToArray());
			sql.Commit();
		}

//---------------------------------------------------------------------------------
// methods for combo box children attribute ---------------------------------------
//---------------------------------------------------------------------------------
		static void DeleteComboItemChildren(int[] ids, int[] attr_ids)
		{
			SqlOperation sql = new SqlOperation("attribute_combo_item_children", SqlOperationMode.Delete);

			if(ids != null)
				sql.Where_In("id_combo_item", ids);

			if(attr_ids != null)
				sql.Where_In("id_atb", attr_ids);

			sql.Commit();
		}

//---------------------------------------------------------------------------------
		public static bool IsComboItemChildren(int id_atb)
		{
			SqlOperation sql = new SqlOperation("attribute_combo_item_children", SqlOperationMode.Select_Scalar);
			sql.Where("id_atb", id_atb);

			return (0 < sql.GetCount("id_combo_item"));
		}

//---------------------------------------------------------------------------------
		public static List<DocumentAttributeCombo> GetComboItemsByChildrenAtb(int id_children_atb, string atb_value = "")
		{
			List<DocumentAttributeCombo> ans = new List<DocumentAttributeCombo>();

			SqlOperation sql = new SqlOperation("attribute_combo_item_children", SqlOperationMode.Select);
			sql.Distinct = true;
			sql.Field("id_combo_item");
			sql.Where("id_atb", id_children_atb);

			if(!String.IsNullOrEmpty(atb_value))
				sql.Where("atb_value", SqlOperationOperator.LIKE_BOTH, atb_value);

			sql.Commit();

			List<int> id_combo_item = new List<int>();
			while(sql.Read())
				id_combo_item.Add(sql.Result<int>("id_combo_item"));

			if(0 < id_combo_item.Count)
				ans = GetComboItems(ids: id_combo_item.ToArray());

			return ans;
		}

//---------------------------------------------------------------------------------
// Generic methods ----------------------------------------------------------------
//---------------------------------------------------------------------------------
		public static bool IsComboItemUsed(int id_atb, int value)
		{
			SqlOperation sql = new SqlOperation("document_attribute", SqlOperationMode.Select_Scalar);
			sql.Where("id_atb", id_atb);
			//sql.Where_In("id_field_type", (int)en_AttrType.Combo, (int)en_AttrType.FixedCombo, (int)en_AttrType.ComboSingleSelect, (int)en_AttrType.FixedComboSingleSelect);
            sql.Where_In("id_field_type", (int)en_AttrType.Combo, (int)en_AttrType.FixedCombo, (int)en_AttrType.ComboSingleSelect, (int)en_AttrType.FixedComboSingleSelect);
			sql.Where("atb_value", SqlOperationOperator.LIKE_BOTH, "'" + value + "'");

			return (0 < sql.GetCount("id_doc"));
		}

//---------------------------------------------------------------------------------
		public static bool IsAttributeValueExists(int id_atb)
		{
			SqlOperation sql = new SqlOperation("document_attribute", SqlOperationMode.Select_Scalar);
			sql.Where("id_atb", id_atb);

			return (0 < sql.GetCount("id_doc"));
		}

        class AttributeItemChildren
        {
            public int id_combo_item;
            public int id_atb;
            public string atb_value;
        }

		static void BindChildren(ref List<DocumentAttributeCombo> ans, int[] attr_ids = null)
        {
            if (!Cache.HasComboChild()) return;

            if (0 == ans.Count) return;

            if (attr_ids == null) return;

            Dictionary<int, System.Collections.IEnumerable> ControlCaches = new Dictionary<int, System.Collections.IEnumerable>();// disable cache

            foreach ( int key in attr_ids)
            {
                if (ControlCaches.FindIndex(ctrl => ctrl.Key == key) == -1)
                {
                    ControlCaches.Add(key, new List<AttributeItemChildren>());

                    // Add combo item children to cache
                    SqlOperation sql = new SqlOperation("attribute_combo_item_children", SqlOperationMode.Select);
                    sql.InnerJoin("attribute_combo_item_children", "attribute_combo_item", "id_combo_item", "id");
                    sql.Fields("attribute_combo_item_children.id_combo_item", "attribute_combo_item_children.id_atb", "attribute_combo_item_children.atb_value");
                    sql.Where_In("attribute_combo_item.id_atb", key);
                    if(ans.Select(x => x.id).Count() < 10 ) sql.Where_In("attribute_combo_item_children.id_combo_item", ans.Select(x => x.id).ToArray());

                    sql.Commit();

                    while (sql.Read())
                    {
                        AttributeItemChildren cld = new AttributeItemChildren
                        {
                            id_combo_item = sql.Result<int>("id_combo_item"),
                            id_atb = sql.Result<int>("id_atb"),
                            atb_value = sql.Result("atb_value")
                        };

                        ((List<AttributeItemChildren>)ControlCaches[key]).Add(cld);

                    }
                }

                List<AttributeItemChildren> attribute_combo_item_children = ControlCaches[key] as List<AttributeItemChildren>;

                if (0 < attribute_combo_item_children.Count)
                {

                    List<DocumentAttribute> work = DocumentAttributeController.GetAttributesCache(
                                                        false,
                                                        attribute_combo_item_children.Select(a => a.id_atb).Distinct().ToArray());

                    foreach(var combo in ans)
                    {
                        var child = attribute_combo_item_children.FirstOrDefault(c => c.id_combo_item == combo.id);

                        if (child == null || child.id_atb == 0) continue;

                        DocumentAttribute attr = work.FirstOrDefault(a => a.id == child.id_atb);
                        if (attr == null) continue;

                        attr = Utilities.ObjectClone(attr);
                        attr.atbValue_str = child.atb_value;
                        combo.children.Add(attr);

                    }
                }
            }
        }

        public static bool HasComboChild()
        {
            List<DocumentAttributeCombo> ans = new List<DocumentAttributeCombo>();

            SqlOperation sql = new SqlOperation("attribute_combo_item_children", SqlOperationMode.Select_Scalar);

            int c = sql.GetCount("id_combo_item") ;

            return c > 0;
        }


        public static List<DocumentAttribute> Merge(List<DocumentAttribute> dest, List<DocumentAttribute> source)
        {
            List<DocumentAttribute> ans = new List<DocumentAttribute>();

            ans.AddRange(dest);

            source.ForEach(attr =>
            {
                if(!ans.Exists( t => t.id == attr.id))
                {
                    ans.Add(attr);

                    return;
                }

                var attr_to = ans.Find(t => t.id == attr.id);

                int idx = ans.FindIndex(t => t.id == attr.id);

                ans[idx]= attr;
            });

            return ans;

        }
    }
}
