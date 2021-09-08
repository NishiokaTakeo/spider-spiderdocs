using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using SpiderDocsModule;
using NLog;
namespace SpiderDocsForms
{
    public class FormUserControlTimer:IDisposable
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        static Object thisLock = new Object();
        uint LastGridUpdateCount = Utilities.GetTickCount();
        uint LastWorkSpaceUpdateCount;
        uint LastSendToUpdateCount;
        uint PreferenceChangedCount;

        System.Timers.Timer timer;
        int interval = 1000;// 60000;

        ContainerControl m_parent;

        public delegate void EventHandler(object arg);
        public event EventHandler OnTimerElapsed; // for generic purpose
        public event EventHandler OnGridUpdateRequestReceived;
        public event EventHandler OnWorkSpaceUpdateRequestReceived;
        public event EventHandler OnDMSFileOpenRequestReceived;
        public event EventHandler OnSendToReceived;
        public event EventHandler OnPreferenceChanged;
        public event EventHandler OnSyncWordSpace;

		string _callingMethod = "";

        public FormUserControlTimer(ContainerControl parent)
        {
            m_parent = parent;

            timer = new System.Timers.Timer(interval);
            timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            timer.AutoReset = false;
            timer.Enabled = true;
        }

        void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            /*
            if (m_parent.ParentForm == null)
                return;
            */

            logger.Trace("[Begin]");

            try {

                uint CurrentCount = Utilities.GetTickCount();

                if(OnGridUpdateRequestReceived != null)
                {
					_callingMethod = "OnGridUpdateRequestReceived";
                    if(LastGridUpdateCount < MMF.ReadData<uint>(MMF_Items.GridUpdateCount))
                    {
                        //logger.Trace("Invoke:OnGridUpdateRequestReceived");

                        m_parent.BeginInvoke(new Action(() => OnGridUpdateRequestReceived(null)));
                        LastGridUpdateCount = CurrentCount;
                    }
                }

                if(OnWorkSpaceUpdateRequestReceived != null)
                {
					_callingMethod = "OnWorkSpaceUpdateRequestReceived";

                    if(LastWorkSpaceUpdateCount <= MMF.ReadData<uint>(MMF_Items.WorkSpaceUpdateCount))
                    {
                        //logger.Trace("Invoke:OnWorkSpaceUpdateRequestReceived");

                        m_parent.Invoke(new Action(() => OnWorkSpaceUpdateRequestReceived(null)));
                        LastWorkSpaceUpdateCount = CurrentCount;
                    }
                }

                WorkDMSFileOpenRequestReceived();

                if(OnSendToReceived != null)
                {
					_callingMethod = "OnSendToReceived";

                    if(LastSendToUpdateCount <= MMF.ReadData<uint>(MMF_Items.SendTo))
                    {
                        //logger.Trace("Invoke:OnSendToReceived");

                        m_parent.Invoke(new Action(() => OnSendToReceived(null)));
                        LastSendToUpdateCount = CurrentCount;
                    }
                }

                if ((OnTimerElapsed != null) && !m_parent.IsDisposed && m_parent.IsHandleCreated)
                {
                    //logger.Trace("Invoke:OnTimerElapsed");
					_callingMethod = "OnTimerElapsed";
                    m_parent.Invoke(new Action(() => OnTimerElapsed(null)));
                }

                if (OnPreferenceChanged != null)
                {
					_callingMethod = "OnPreferenceChanged";

                    if (PreferenceChangedCount <= MMF.ReadData<uint>(MMF_Items.PreferenceChanged))
                    {
                        //logger.Trace("Invoke:OnPreferenceChanged");

                        m_parent.Invoke(new Action(() => OnPreferenceChanged(null)));
                        PreferenceChangedCount = CurrentCount;
                    }
                }

                // Workspace sync
                if(OnSyncWordSpace != null)
                {
					_callingMethod = "OnSyncWordSpace";

                    //logger.Trace("Invoke: OnSyncWordSpace");

                    m_parent?.Invoke(new Action(() => OnSyncWordSpace(null)));
                }

            }
            catch (Exception ex)
            {

                logger.Error(ex, "Parent:{0} , Method:{1}", m_parent?.ToString(), _callingMethod);
            }

            timer.Enabled = true;

            logger.Trace("[End]");
        }


        /// <summary>
        /// DMS File Open Request
        /// </summary>
        void WorkDMSFileOpenRequestReceived()
        {
            if (OnDMSFileOpenRequestReceived != null)
            {
				_callingMethod = "OnDMSFileOpenRequestReceived";

                string wrk;

                lock (thisLock)
                {
                    wrk = MMF.ReadData<string>(MMF_Items.DmsFilePath);

                    if (!String.IsNullOrEmpty(wrk))
                        MMF.WriteData<string>("", MMF_Items.DmsFilePath);
                }

                if (!String.IsNullOrEmpty(wrk) && !SpiderDocsApplication.CurrentUserSettings.offline)
                    m_parent.Invoke(new Action(() => OnDMSFileOpenRequestReceived(wrk)));
            }
        }

        public void Dispose()
        {
            timer.Stop();
        }
    }
}
