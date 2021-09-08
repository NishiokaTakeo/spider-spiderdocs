using System;
using System.Linq;
using System.Timers;
using System.ComponentModel;
using System.Threading;

namespace Spider.Task
{
	public class SpiderThread
	{
		Thread workerThread;
		Func<object,object> m_Work;
		Action<object> m_CallBack;
		System.Timers.Timer m_Timeout = new System.Timers.Timer();

		public SpiderThread(Func<object,object> WorkFunction, Action<object> CallBackFunction = null, int Timeout = 0)
		{
			m_Work = WorkFunction;
			m_CallBack = CallBackFunction;

			m_Timeout.Interval = Timeout;
			m_Timeout.AutoReset = false;
			m_Timeout.Elapsed += new ElapsedEventHandler(TimeoutElapsed);
		}

		public void Start(object arg = null)
		{
			BackgroundWorker bw = new BackgroundWorker();
			bw.DoWork += new DoWorkEventHandler(thread_Work);
			bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(thread_WorkDone);

			if(0 < m_Timeout.Interval)
				m_Timeout.Start();
			
			bw.RunWorkerAsync(arg);			
		}

		void thread_Work(object sender, DoWorkEventArgs e)
		{
			workerThread = Thread.CurrentThread;
			e.Result = m_Work(e.Argument);
		}

		void thread_WorkDone(object sender, RunWorkerCompletedEventArgs e)
		{
			m_Timeout.Stop();

			if(m_CallBack != null)
				m_CallBack(e.Result);
		}

		void TimeoutElapsed(object sender, ElapsedEventArgs e)
		{
			workerThread.Abort();
            workerThread = null;
		}
	}
}
