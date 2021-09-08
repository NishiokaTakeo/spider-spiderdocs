using System;
using System.Collections.Generic;
using SpiderDocsModule;

namespace SpiderDocsForms
{
	static public class ReviewExtension
	{
		static public void FinalizeReview(this Review review)
		{
			review.FinalizeReview(SpiderDocsApplication.CurrentUserId);
		}

		static public void StartReview(this Review review, List<int> NextUserIds, DateTime DeadLine)
		{
			review.StartReview(SpiderDocsApplication.CurrentUserId, NextUserIds, DeadLine);
		}
	}
}
