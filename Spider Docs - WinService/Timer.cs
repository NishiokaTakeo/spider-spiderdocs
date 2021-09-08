using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using SpiderDocsModule;
using NLog;
//---------------------------------------------------------------------------------
namespace SpiderDocsWinService
{
    class TimerInt
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

        //---------------------------------------------------------------------------------
        readonly double TM_BASE = double.Parse(System.Configuration.ConfigurationManager.AppSettings["TM_BASE"]); //60000;   // 1 minute (MS)
        readonly int TM_TRGGR_CNT = int.Parse(System.Configuration.ConfigurationManager.AppSettings["TM_TRGGR_CNT"]);   //3;     // 1 hour (MIN)

        //const int TM_1HOUR = 60;		// 1 hour (MIN)
        //const int TM_24HOUR = 1440;		// 1 day (MIN)

        int m_Counter = 0;
		//int m_24Hour = 0;

		System.Timers.Timer m_Timer = new System.Timers.Timer();

//---------------------------------------------------------------------------------	
		public TimerInt()
		{
            logger.Info("[Timer Started] BASE CIRCLE:{0}ms, TRGGR_CNT:{1}", TM_BASE, TM_TRGGR_CNT);

            m_Timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
			m_Timer.Interval = TM_BASE;
			m_Timer.Enabled = true;
		}

//---------------------------------------------------------------------------------
		public void RunEvent()
		{
            //m_Counter = TM_TRGGR_CNT;
            //m_24Hour = TM_24HOUR;

            m_Counter++;

            m_Timer.Interval = 1;
		}

//---------------------------------------------------------------------------------
		void TimerElapsed(object sender, ElapsedEventArgs e)
		{
			try
			{
				if(m_Timer.Enabled)
				{
                    logger.Trace("Timer elapsed");

					m_Timer.Enabled = false;
					m_Counter++;
					//m_24Hour++;
					
					//if(m_1Hour % 5 == 0)
					//{
						
					//}

					if(TM_TRGGR_CNT <= m_Counter)
					{
						m_Counter = 0;
						CheckReview();

                        Notify();
                    }
				}
			}
			catch(Exception err)
			{
				System.Windows.Forms.MessageBox.Show(err.ToString());
			}
			finally
			{
				m_Timer.Interval = TM_BASE;
				m_Timer.Enabled = true;
			}
		}

//---------------------------------------------------------------------------------
		void CheckReview()
		{
            logger.Trace("Begin");

            List<Review> reviews = ReviewController.GetReview(true, true);

            logger.Debug("0 > reviews.Count ?");

            if (0 < reviews.Count)
			{
                
                List<Document> docs = DocumentController<Document>.GetDocument(reviews.Select(a => a.id_doc).ToArray());
                logger.Debug(" 0 > {0}", docs.Count);

                // remainder is sent only once a day
                // do not want to send email in midnight
                foreach (Review review in reviews)
				{
                    Document doc = docs.FirstOrDefault(a => a.id == review.id_doc);

                    logger.Debug("Can proceed : {0}", doc != null);

                    if (doc == null) continue;

                    TimeSpan Remainds = review.deadline - DateTime.Now;

                    logger.Debug("Status : {0}", doc.id_sp_status);

                    if (doc.id_sp_status != en_file_Sp_Status.review_overdue)
					{
						if(Remainds.TotalHours <= 0)
						{
                            logger.Info("Notify : {0}", doc.id);

                            doc.ChangeStatus(sp_status: en_file_Sp_Status.review_overdue);
							review.SendEmail(en_ReviewMailType.Overdue, doc);

						}else if(((int)Remainds.TotalHours == 1)
							  || ((int)Remainds.TotalHours == 24)
							  || ((int)Remainds.TotalHours == 72))
						{
                            logger.Info("Notify : {0}", doc.id);

                            review.SendEmail(en_ReviewMailType.Remainder, doc);
						}
                    }                    
                }
            }
		}

		void Notify()
		{
            try { 
			    var ques = ScheduleNotificationAmendedController.List();

			    foreach(var que in ques)
			    {
				    var task = new TaskNotification(que);
				    task.Run();

				    ScheduleNotificationAmendedController.Delete(que.id);
			    }
            }catch(Exception ex)
            {
                logger.Error(ex);
            }

        }
//---------------------------------------------------------------------------------
	}
}
