using System;
using Spider.Data;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public enum en_Events
	{
		Created,
		Import,
		Scan,
		NewVer,
		Chkin,
		Chkout,
		Read,
		Cancel,
		Email,
		Exp,
		Outlook,
		Login,
		Property,
		Rollback,
		Archive,
		Copy,
		SaveNewVer,
		ChgAttr,
		ChgDT,
		ChgName,
		ChgFolder,
		StartReview,
		FinalizeReview,
		PassOnReview,
        UpVer,
		DupOK,
        CanceledArchive,
        Max,

		INVALID
	}

//---------------------------------------------------------------------------------
	public static class EventIdController
	{
//---------------------------------------------------------------------------------
		static int[] EventId = null;

//---------------------------------------------------------------------------------
		static void InitEventId()
		{
			EventId = new int[(int)en_Events.Max];
			
			SqlOperation sql = new SqlOperation("document_event", SqlOperationMode.Select);
			sql.Field("id");
			sql.Commit();

			int i = 0;
			while(sql.Read())
				EventId[i++] = Convert.ToInt32(sql.Result_Obj("id"));
		}

//---------------------------------------------------------------------------------
		public static int GetEventId(en_Events Event)
		{
			if(EventId == null)
				InitEventId();

			return EventId[(int)Event];
		}

//---------------------------------------------------------------------------------
		public static en_Events GetEvent(int id)
		{
			if(EventId == null)
				InitEventId();
			
			int i = 0;

			foreach(int wrk in EventId)
			{
				if(wrk == id)
					break;
				else
					i++;
			}

			return (en_Events)i;
		}

//---------------------------------------------------------------------------------
	}
}
