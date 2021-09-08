using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spider.Data;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class ReviewController
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

//---------------------------------------------------------------------------------
		static readonly string[] tb_ReviewFields = new string[]
		{
			//"id",
			"id_doc",
			"id_user",
			"status",
			"deadline",
			"comment",
			"id_version",
			"allow_checkout"
		};

		static readonly string[] tb_ReviewUserFields = new string[]
		{
			//"id_review",
			//"id_user",
			"comment",
			"action",
			"id_version"
		};

//---------------------------------------------------------------------------------
		public static Review GetReview(int id_doc)
		{
			Review ans = null;

			List<Review> lists = GetReview(true, false, new int[] { id_doc });

			if(0 < lists.Count)
				ans = lists[0];
			
			return ans;
		}

		public static List<Review> GetReview(bool latest, bool only_not_finished, params int[] id_docs)
		{
            logger.Trace("Begin");

            SqlOperation sql;
			List<int> LatestIds = new List<int>();
			List<Review> ans = new List<Review>();
			
			// Get the latest review id of each documents
			sql = new SqlOperation("document_review", SqlOperationMode.Select);

			if(latest)
			{
				sql.Field("MAX(id) AS id");
				sql.GroupBy("id_doc");

				if(0 < id_docs.Length)
					sql.Having_In("id_doc", id_docs);

			}else
			{
				sql.Field("id");

				if(0 < id_docs.Length)
					sql.Where_In("id_doc", id_docs);
			}

			if(only_not_finished)
				sql.Where("status", 0);

			sql.Commit();

			while(sql.Read())
				LatestIds.Add(Convert.ToInt32(sql.Result_Obj("id")));

            logger.Debug("0 < {0} ?", LatestIds.Count);


            if (0 < LatestIds.Count)
			{
				// Populate review data
				sql = new SqlOperation("document_review", SqlOperationMode.Select);
				sql.Field("id");
				sql.Fields(tb_ReviewFields);
				sql.Where_In("id", LatestIds);

				sql.Commit();

				// Distribute review data
				while(sql.Read())
				{
					Review wrk = new Review(
						Convert.ToInt32(sql.Result_Obj("id_doc")),
						(en_ReviewStaus)Convert.ToInt32(sql.Result_Obj("status"))
					);
                    logger.Debug("{0} == 0", wrk.id_doc);
                    if (wrk.id_doc == 0) continue;

					wrk.id_review = Convert.ToInt32(sql.Result_Obj("id"));
					wrk.owner_id = Convert.ToInt32(sql.Result_Obj("id_user"));
					wrk.deadline = Convert.ToDateTime(sql.Result_Obj("deadline"));
					wrk.owner_comment = sql.Result("comment");
					wrk.id_version = Convert.ToInt32(sql.Result_Obj("id_version"));
					wrk.allow_checkout = sql.Result<bool>("allow_checkout");

					wrk.review_users = GetReviewUser(wrk.id_review);

					ans.Add(wrk);
				}
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public static int AddNewReview(Review src)
		{
			int ans;

			ans = SqlOperationForReview(src, SqlOperationMode.Insert);

			if(0 < ans)
			{
				foreach(ReviewUser wrk in src.review_users)
				{
					wrk.id_review = ans;
					InsertOrUpdateReviewUser(wrk, true);
				}
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public static int UpdateReview(Review src)
		{
			return SqlOperationForReview(src, SqlOperationMode.Update);
		}

//---------------------------------------------------------------------------------
		static int SqlOperationForReview(Review src, SqlOperationMode mode)
		{
			int ans = 0;

			DateTime? deadline = null;

			if(src.deadline != DateTime.MinValue)
				deadline = src.deadline;

			string comment = src.owner_comment;
				
			object[] vals = new object[]
			{
				src.id_doc,
				src.owner_id,
				(int)src.status,
				deadline,
				comment,
				src.id_version,
				src.allow_checkout
			};

			SqlOperation sql = new SqlOperation("document_review", mode);
			sql.Fields(tb_ReviewFields, vals);

			if(mode == SqlOperationMode.Update)
			{
				sql.Where("id", src.id_review);
				ans = src.id_review;
				sql.Commit();

			}else
			{
				ans = sql.Commit<int>();
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		static List<ReviewUser> GetReviewUser(int id_review)
		{
			List<ReviewUser> ans = new List<ReviewUser>();
			
			SqlOperation sql = new SqlOperation("document_review_users", SqlOperationMode.Select);
			sql.Fields("id_review", "id_user");
			sql.Fields(tb_ReviewUserFields);
			sql.Where("id_review", id_review);
			sql.Commit();

			while(sql.Read())
			{
				ReviewUser wrk = new ReviewUser();

				wrk.id_review = id_review;
				wrk.id_user = sql.Result<int>("id_user");
				wrk.id_version = sql.Result<int>("id_version");
				wrk.comment = sql.Result("comment");
				wrk.action = (en_ReviewAction)sql.Result<int>("action");

				ans.Add(wrk);
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public static void InsertOrUpdateReviewUser(ReviewUser src, bool insert)
		{
			SqlOperation sql = null;

			if(insert)
			{
				sql = new SqlOperation("document_review_users", SqlOperationMode.Insert);
				sql.Field("id_review", src.id_review);
				sql.Field("id_user", src.id_user);

			}else
			{
				sql = new SqlOperation("document_review_users", SqlOperationMode.Update);
				sql.Where("id_review", src.id_review);
				sql.Where("id_user", src.id_user);
			}

			object[] vals = new object[]
			{
				src.comment,
				(int)src.action,
				src.id_version
			};

			sql.Fields(tb_ReviewUserFields, vals);
			sql.Commit();
		}

//---------------------------------------------------------------------------------
	}
}
