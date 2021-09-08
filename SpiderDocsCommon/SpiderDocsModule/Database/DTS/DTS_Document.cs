using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Spider.Data;
using Spider.Common;
using Spider.Types;
using NLog;
//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
//---------------------------------------------------------------------------------
	public class DTS_Document : DTS_Base
	{
        static Logger logger = LogManager.GetCurrentClassLogger();
		readonly string[,] tb_select = new string[,]
		{
			{"id"					,"id"				},
			{"id_latest_version"	,"id_version"		},
			{"id_user"				,"id_user"			},
			{"id_folder"			,"id_folder"		},
			{"title"				,"title"			},
			{"extension"			,"extension"		},
			{"name"					,"name"				},
			{"document_folder"		,"document_folder"	},
			{"id_status"			,"id_status"		},
			{"version"				,"version"			},
			{"id_type"				,"id_type"			},
			{"type"					,"type"				},
			{"date"					,"date"				},
			{"id_review"			,"id_review"		},
			{"id_sp_status"			,"id_sp_status"		},
			{"created_date"			,"created_date"		},
			{"id_checkout_user"		,"id_checkout_user"	},
			{"filesize"				,"size"				},
			{"mail_subject"			,"mail_subject"		},
			{"mail_from"			,"mail_from"		},
			{"mail_to"				,"mail_to"			},
			{"mail_time"			,"mail_time"		},
			{"mail_is_composed"		,"mail_is_composed"}/*,
            {"id_notification_group","id_notification_group"}*/
        };

//---------------------------------------------------------------------------------
		// This query is bad and slow. To optimize it, we need to reconsider database structure for attributes.
		// The structure originally does not make sence.
			string  sql_KeywordInAttribute = @"
				(
					EXISTS(
						SELECT attr_val.id
						FROM view_document_attribute_value attr_val
						WHERE
						attr_val.id_doc = d.id
						AND attr_val.atb_value like '%<KEYWORD>%'
					)
                    OR (<FILEDATA>)
				) ".Trim('\r', '\n');

//---------------------------------------------------------------------------------
		public List<SearchCriteria> Criteria = new List<SearchCriteria>();
        public int CustomColumnId = 0;
		int m_maxDocs;
		int m_userId;
		bool m_localDb;

//---------------------------------------------------------------------------------
        public DTS_Document(): this(SpiderDocsApplication.CurrentUserId, SpiderDocsApplication.CurrentPublicSettings.maxDocs, SpiderDocsApplication.CurrentServerSettings.localDb)
        {

        }

		public DTS_Document(int userId, int maxDocs, bool localDb)
		{
			m_maxDocs = maxDocs;
			m_userId = userId;
			m_localDb = localDb;
		}

//---------------------------------------------------------------------------------
		public void SetRecentDocumentsCriteria(SearchCriteria criteria)
		{

            this.Criteria.Clear();

			criteria.GetRecentDocuments = true;
			this.Criteria.Add(criteria);
		}

//---------------------------------------------------------------------------------
		public void Select(int maxDocs = 0)
		{
			try
			{
                if (maxDocs <= 0)
					maxDocs = m_maxDocs;

				Search(maxDocs);

				table.Columns["id"].DefaultValue = 0;
				table.Columns["id_version"].DefaultValue = 0;
				table.Columns["id_user"].DefaultValue = 0;
				table.Columns["id_folder"].DefaultValue = 0;
				table.Columns["title"].DefaultValue = DBNull.Value;
				table.Columns["extension"].DefaultValue = DBNull.Value;
				table.Columns["name"].DefaultValue = DBNull.Value;
				table.Columns["document_folder"].DefaultValue = DBNull.Value;
				table.Columns["id_status"].DefaultValue = 0;
				table.Columns["version"].DefaultValue = DBNull.Value;
				table.Columns["id_type"].DefaultValue = 0;
				table.Columns["type"].DefaultValue = DBNull.Value;
				table.Columns["date"].DefaultValue = DBNull.Value;
				table.Columns["id_review"].DefaultValue = 0;
				table.Columns["id_sp_status"].DefaultValue = DBNull.Value;
				table.Columns["created_date"].DefaultValue = DBNull.Value;
				table.Columns["id_checkout_user"].DefaultValue = 0;
				table.Columns["size"].DefaultValue = 0;
				table.Columns["mail_subject"].DefaultValue = DBNull.Value;
				table.Columns["mail_from"].DefaultValue = DBNull.Value;
				table.Columns["mail_to"].DefaultValue = DBNull.Value;
				table.Columns["mail_time"].DefaultValue = DBNull.Value;
				table.Columns["mail_is_composed"].DefaultValue = DBNull.Value;
                //table.Columns["id_notification_group"].DefaultValue = 0;

                if (0 < CustomColumnId)
				{
					table.Columns["atb_value"].DefaultValue = DBNull.Value;

                    DocumentAttribute attr = DocumentAttributeController<SpiderDocsModule.Document>.GetAttribute(CustomColumnId);
					if(DocumentAttribute.IsComboTypes(attr.id_type))
					{
						foreach(DataRow row in table.Rows)
						{
							if(row["atb_value"].GetType() != typeof(string))
								continue;

							attr.atbValue_str = Convert.ToString(row["atb_value"]);
							row["atb_value"] = attr.atbValueForUI;
						}
					}
				}

				DataColumn[] columns = new DataColumn[1];
				columns[0] = table.Columns["id"];
				table.PrimaryKey = columns;

			}
			catch(Exception error)
			{
				logger.Error(error);
			}
		}

        // public void Delete()
        // {
        //     for (int i = table.Rows.Count - 1; i >= 0; i--)
        //     {
        //         DataRow row = table.Rows[i];
        //         if (row.RowState == DataRowState.Deleted) { table.Rows.RemoveAt(i); }
        //     }
        // }

		//---------------------------------------------------------------------------------
		/// <summary>
		/// Search document. by criteria
		/// </summary>
		/// <param name="maxDocs"></param>
        void Search(int maxDocs)
		{
			foreach(SearchCriteria wrk in Criteria)
			{
				SqlOperation sql = new SqlOperation("view_document_search AS d", SqlOperationMode.Select_Table);

                for (int i = 0; i < tb_select.GetLength(0); i++)
					sql.Field(string.Format("{0}.{1} AS {2}","d",tb_select[i, 0],tb_select[i, 1]) );

				sql.SetMaxResult(maxDocs);

				if(wrk.GetRecentDocuments)
				{
					_Where4Recent(ref sql, wrk);
				}
				else
				{
					_Where4Search(ref sql, wrk);
				}

                sql.OrderBy("id", Spider.Data.Sql.SqlOperation.en_order_by.Descent);

				sql.Commit();


                //DataTable _table = FilterBy(sql.table, wrk);


                //MergeTable(wrk.MergeType, _table);
                MergeTable(wrk.MergeType, sql.table);

                maxDocs = m_maxDocs - table.Rows.Count;
				if(maxDocs <= 0)
					break;
			}
		}

        /// <summary>
        /// Set conditions for Where/InnerJoin, etc.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="wrk"></param>
		void _Where4Search(ref SqlOperation sql,SearchCriteria wrk)
		{
			if(CustomColumnId != 0)
			{
				sql.Field("atb.atb_value AS atb_value");
				sql.LeftJoin("document_attribute atb", "d.id = atb.id_doc AND atb.id_atb = " + CustomColumnId);
			}

			if(0 < wrk.DocIds.Count)
				sql.Where_In("d.id", wrk.DocIds);

			if(0 < wrk.DocNotInIds.Count)
				sql.Where_Not_In("d.id", wrk.DocNotInIds);

			if(0 < wrk.Titles.Count)
			{
				SqlOperationCriteriaCollection criterias = new SqlOperationCriteriaCollection();
				criterias.m_AndOr = SqlOperationAndOr.OR;

				foreach(string title in wrk.Titles)
                {
                    var criteriaLike = new SqlOperationCriteria("d.title", SqlOperationOperator.LIKE_BOTH, title/*.Trim()*/);
                    criteriaLike.EscapeLike = "\\";
                    criterias.Add(criteriaLike);
                }

                sql.Where(criterias);
			}

			if(0 < wrk.AbsoluteTitles.Count)
				sql.Where_In("d.title", wrk.AbsoluteTitles.ToArray());

			foreach(Spider.Types.Period Date in wrk.Date)
			{
				string From = "";
				string To = "";

				if(Date.From != new DateTime())
					From = Date.FromStr;

				if(Date.To != new DateTime())
					To = Date.ToStr;

				if(!String.IsNullOrEmpty(From) && !String.IsNullOrEmpty(To))
					sql.Where("d.[date] between '" + From + "' and '" + To + "'");
				else if(!String.IsNullOrEmpty(From))
					sql.Where("d.[date] > '" + From + "'");
				else if(!String.IsNullOrEmpty(To))
					sql.Where("d.[date] < '" + To + "'");
			}

			if(0 < wrk.UserIds.Count)
				sql.Where_In("d.id_user", wrk.UserIds);

			if(0 < wrk.Extensions.Count)
				sql.Where_In("d.extension", wrk.Extensions);

			if(0 < wrk.DocTypeIds.Count)
				sql.Where("d.id_type IN (" + String.Join(", ", wrk.DocTypeIds) + ")");

			if(0 < wrk.SpStatuses.Count)
				sql.Where_In("d.id_sp_status",  wrk.SpStatuses.Cast<int>().ToArray());

			if(wrk.Review.Status != en_ReviewStaus.INVALID)
			{
				sql.LeftJoin("document_review AS rev", "d.id_review = rev.id");

				string filter = "";
				if(0 < wrk.Review.UserIds.Count)
				{
					sql.InnerJoin("document_review_users rev_user", "rev_user.id_review = d.id_review and rev_user.id_user in (" + String.Join(",", wrk.Review.UserIds) + ")");

					filter = ((int)en_ReviewAction.Finalize).ToString();
					if(wrk.Review.Status == en_ReviewStaus.UnReviewed)
						sql.Where("rev_user.action <> " + filter);
					else
						sql.Where("rev_user.action = " + filter);

				}else
				{
					filter = ((int)en_ReviewStaus.UnReviewed).ToString();
					if(wrk.Review.Status == en_ReviewStaus.UnReviewed)
						sql.Where("rev.status = " + filter);
					else
						sql.Where("rev.status <> " + filter);
				}
			}

			string criteria = wrk.AttributeCriterias.ToString() ;
			if (!string.IsNullOrWhiteSpace(criteria))
				sql.Where(criteria);


			if (0 < wrk.ExcludeStatuses.Count)
			{
				// new code
				sql.Where_Not_In("d.id_status", wrk.ExcludeStatuses.Cast<int>().ToArray());
			}
			else
            {
                if (0 < wrk.Statuses.Count)
                    logger.Info("wrk.Statuses is sill used ");

                // original code
                if (0 < wrk.Statuses.Count)
					sql.Where_In("d.id_status", wrk.Statuses.Cast<int>().ToArray());
				else
				{
					sql.Where("d.id_status <> " + ((int)en_file_Status.deleted).ToString());
				}
			}

            if (0 < wrk.Keywords.Count)
			{
				SqlOperationCriteriaCollection criterias = new SqlOperationCriteriaCollection();
				criterias.m_AndOr = SqlOperationAndOr.OR;

				foreach (string tmp in wrk.Keywords)
				{
					string Keyword = tmp.Trim();

					criterias.Add(new SqlOperationCriteria("d.title", SqlOperationOperator.LIKE_BOTH, Keyword));

					string AttributeKeywordSearchSql = sql_KeywordInAttribute;

					AttributeKeywordSearchSql = AttributeKeywordSearchSql.Replace("<KEYWORD>", Keyword);

					// work around for SQL server slowness when using OR and CONTAINS at a same time
					string[] words = Keyword.Split(' ');
					string DataFieldName = (m_localDb ? "v.filetext" : "v.filedata");
					string versiontbl = (m_localDb ? "document_txt" : "document_version");

					for (int i = 0; i < words.Length; i++)
						words[i] = "CONTAINS (" + DataFieldName + ", '\"" + words[i] + "\"')";

					string filesql = " EXISTS( Select id from " + versiontbl + " as v WITH (NOLOCK) where v.id= d.id_latest_version AND " + String.Join(" OR ", words) + " )";

					AttributeKeywordSearchSql = AttributeKeywordSearchSql.Replace("<FILEDATA>", filesql);

					criterias.Add(new SqlOperationCriteria(AttributeKeywordSearchSql, SqlOperationOperator.RAW,null));
				}

				sql.Where(criterias);
            }

            if( wrk.VersionIds.Count() > 0)
            {
                sql.Where(string.Format("EXISTS( SELECT id FROM document_version as _v WHERE _v.id in ({0}) and _v.id_doc = d.id ) ",string.Join(",",wrk.VersionIds)));
            }

            if ( wrk.FolderIds.Count > 1000) logger.Warn("FolderIds.Count > 1000. Check logic");

            if (0 < wrk.FolderIds.Count)
                sql.Where(" d.id_folder IN (" + String.Join(", ", wrk.FolderIds) + ")");
            else if(wrk.Archived)
                sql.Where($" EXISTS ( select vf.id from document_folder_archived as vf WITH (NOLOCK) where and vf.id = d.id_folder ) ");
            else
                sql.Where($" EXISTS ( select vf.id_folder from fnGetUserPermission(d.id_folder, {m_userId}) as vf WHERE vf.id_permission = 2 ) ");
        }

        /// <summary>
        /// Set conditions for Recent Document
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="wrk"></param>

        void _Where4Recent(ref SqlOperation sql,SearchCriteria wrk)
		{
			sql.LeftJoin("d", "user_recent_document", "id", "id_doc");
			sql.Where("user_recent_document.id_user", m_userId);
			sql.OrderBy("user_recent_document.date", SqlOperation.en_order_by.Descent);

			if (0 < wrk.ExcludeStatuses.Count)
			{
				// new code
				sql.Where_Not_In("d.id_status", wrk.ExcludeStatuses.Cast<int>().ToArray());
			}
			else
			{
                if (0 < wrk.Statuses.Count)
                    logger.Info("wrk.Statuses is sill used ");

				// original code
				if (0 < wrk.Statuses.Count)
					sql.Where_In("d.id_status", wrk.Statuses.Cast<int>().ToArray());
				else
				{
					sql.Where("d.id_status <> " + ((int)en_file_Status.deleted).ToString());
				}
			}
		}

		/// <summary>
		/// Merge table
		/// </summary>
		/// <param name="type"></param>
		/// <param name="source"></param>
		void MergeTable(en_SearchCriteriaMergeType type, DataTable source)
		{
			switch(type)
			{
				default:
					table = source;
					break;

				case en_SearchCriteriaMergeType.Bottom:
					table.Merge(source);
					break;

				case en_SearchCriteriaMergeType.Top:
					source.Merge(table);
					table = source;
					break;
			}
		}

//---------------------------------------------------------------------------------
		public List<T> GetDocuments<T>() where T : Document, new ()
		{
			List<T> ans = new List<T>();

			foreach(DataRow row in table.Rows)
			{
				T doc = new T();

				try { doc.id = Convert.ToInt32(row["id"]); } catch {}
				try { doc.id_version = Convert.ToInt32(row["id_version"]); } catch {}
                try { doc.id_latest_version = Convert.ToInt32(row["id_version"]); } catch { }
				try { doc.id_user = Convert.ToInt32(row["id_user"]); } catch {}
				try { doc.id_folder = Convert.ToInt32(row["id_folder"]); } catch {}
				try { doc.id_docType = Convert.ToInt32(row["id_type"]); } catch {}
				try { doc.id_status = (en_file_Status)Convert.ToInt32(row["id_status"]); } catch {}
				try { doc.title = (string)row["title"]; } catch {}
				try { doc.version = Convert.ToInt32((row["version"].ToString().Replace("V ",""))); } catch {}
				try { doc.date = (DateTime)row["created_date"]; } catch {}
				try { doc.extension = (string)row["extension"]; } catch {}
				try { doc.id_review = Convert.ToInt32(row["id_review"]); } catch {}
				try { doc.id_sp_status = (en_file_Sp_Status)Convert.ToInt32(row["id_sp_status"]); } catch {}
				try { doc.id_checkout_user = Convert.ToInt32(row["id_checkout_user"]); } catch {}
				try { doc.size = Convert.ToInt32(row["size"]); } catch {}
                //try { doc.id_notification_group = Convert.ToInt32(row["id_notification_group"]); } catch { }

                ans.Add(doc);
			}

			if(0 < ans.Count)
				ans = DocumentAttributeController<T>.SetToDocument(ans);

			return ans;
		}

        //---------------------------------------------------------------------------------
    }
}
