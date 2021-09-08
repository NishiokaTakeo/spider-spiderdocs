using System;
using System.Linq;
using System.Collections.Generic;
using Spider.Common;
using lib = SpiderDocsModule.Library;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class ReviewUser
	{
		public int id_review;
		public int id_user;
		public string comment;
		public en_ReviewAction action;
		public int id_version;

		public void FinalizeReview(int IdVersion, string Comment)
		{
			action = en_ReviewAction.Finalize;
			id_version = IdVersion;
			comment = Comment;
			ReviewController.InsertOrUpdateReviewUser(this, false);
		}
	}

//---------------------------------------------------------------------------------
	public class Review
	{
		public int id_review;		

		public int owner_id = 0;

		public string owner_comment;

		Document doc;
		public int id_doc { get { return doc == null ? 0 : doc.id; } }

		en_ReviewStaus _status = en_ReviewStaus.NotInReview;
		public en_ReviewStaus status { get { return _status; } }

		public DateTime deadline;

		public List<ReviewUser> review_users = new List<ReviewUser>();

		public int id_version;

		public bool allow_checkout = false;

//---------------------------------------------------------------------------------
		public Review(int id_doc)
		{
			this.doc = DocumentController.GetDocument(id_doc);
		}

		public Review(int id_doc, en_ReviewStaus status)
		{
			this.doc = DocumentController.GetDocument(id_doc);

			_status = status;
		}

//---------------------------------------------------------------------------------
		public void StartReview(int userId, List<int> NextUserIds, DateTime DeadLine)
		{
			owner_id = userId;
			_status = en_ReviewStaus.UnReviewed;
			deadline = DeadLine;

			foreach(int id_user in NextUserIds)
			{
				ReviewUser review_user = new ReviewUser();
				
				review_user.action = en_ReviewAction.UnReviewed;
				review_user.id_user = id_user;

				review_users.Add(review_user);
			}

			int id_review = ReviewController.AddNewReview(this);

			doc.id_review = id_review;
			doc.id_event = EventIdController.GetEventId(en_Events.StartReview);
			doc.id_sp_status = en_file_Sp_Status.review;
			DocumentController.InsertOrUpdateDocument(null, doc, false);
			HistoryController.SaveDocumentHistoric(null, userId, doc);

			this.SendEmail(en_ReviewMailType.Start, doc);
		}

//---------------------------------------------------------------------------------
		public bool IsAllUsersFinalized()
		{
			bool ans = true;

			if(0 < review_users.Count(a => a.action != en_ReviewAction.Finalize))
				ans = false;

			return ans;
		}

//---------------------------------------------------------------------------------
		public void FinalizeReview(int userId)
		{
			_status = en_ReviewStaus.Reviewed;
			int id_review = ReviewController.UpdateReview(this);

			doc.id_sp_status = en_file_Sp_Status.normal;
			doc.id_event = EventIdController.GetEventId(en_Events.FinalizeReview);
			doc.id_review = id_review;
			DocumentController.InsertOrUpdateDocument(null, doc, false);
			HistoryController.SaveDocumentHistoric(null, userId, doc);

			this.SendEmail(en_ReviewMailType.Finalized, doc);
		}

//---------------------------------------------------------------------------------
		public bool IsOverDue()
		{
			return (deadline < DateTime.Now);
		}

//---------------------------------------------------------------------------------
// Email --------------------------------------------------------------------------
//---------------------------------------------------------------------------------
		public void SendEmail(en_ReviewMailType type, Document doc)
		{
			List<string> To = new List<string>();
			List<int> UserIds = new List<int>();
			string Subject = "";
			string Body = "";

			switch(type)
			{
			case en_ReviewMailType.Start:
				UserIds = this.review_users.Select(a => a.id_user).ToList();
				Subject = lib.msg_review_PassOn_Title;
				Body = lib.msg_review_PassOn;
				break;

			case en_ReviewMailType.Finalized:
				UserIds.Add(owner_id);
				Subject = lib.msg_review_Complete_Title;
				Body = lib.msg_review_Complete;
				break;

			case en_ReviewMailType.Remainder:
				UserIds = this.review_users.Where(a => a.action != en_ReviewAction.Finalize).Select(a => a.id_user).ToList();
				Subject =lib.msg_review_Remainder_Title;
				Body = lib.msg_review_Remainder;
				break;

			case en_ReviewMailType.Overdue:
				UserIds = this.review_users.Where(a => a.action != en_ReviewAction.Finalize).Select(a => a.id_user).ToList();
				Subject =lib.msg_review_Overdue_Title;
				Body = lib.msg_review_Overdue;
				break;
			}
			
			if(0 < UserIds.Count)
			{
				List<User> users = UserController.GetUser(true, false, UserIds.ToArray());
				List<string> ToList = new List<string>();

				foreach(User user in users)
					ToList.Add(user.email);

				User owner = UserController.GetUser(true, owner_id);
				Subject = String.Format(Subject, owner.name);
				Body = String.Format(Body + lib.msg_mail_review_Body, doc.id, doc.title, doc.name_folder, doc.version, deadline.ToString(ConstData.DATE), this.owner_comment);

				DmsFile<Document>.MailDmsFile(doc, ToList, Subject, Body);
			}
		}

//---------------------------------------------------------------------------------
	}
}
