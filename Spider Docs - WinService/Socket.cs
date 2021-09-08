using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.ServiceProcess;
using SpiderDocsServerModule;
using SpiderDocsModule;
using SpiderDocsApplication = SpiderDocsServerModule.SpiderDocsApplication;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsWinService
{
	public partial class Form1 : Form
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

		Socket m_mainSocket;
		AsyncCallback pfnWorkerCallBack;
		TimerInt timer;

//---------------------------------------------------------------------------------
		public class SocketPacket
		{
			// Constructor which takes a Socket and a client number
			public SocketPacket(Socket socket)
			{
				m_currentSocket = socket;
			}

			public Socket m_currentSocket;

			// Buffer to store the data sent by the client
			public byte[] dataBuffer = new byte[1024];
		}

//---------------------------------------------------------------------------------
		public Form1()
		{
			InitializeComponent();

			// Specify the call back function which is to be 
			// invoked when there is any write activity by the 
			// connected client
			pfnWorkerCallBack = new AsyncCallback(OnDataReceived);
		}

//---------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e)
		{
			startService();
		}

//---------------------------------------------------------------------------------
		private void button2_Click(object sender,EventArgs e)
		{
            if( timer == null)
            {
                logger.Error("timer hasn't been started.");

                return;
            }

			timer.RunEvent();
		}

//---------------------------------------------------------------------------------
		public void startService()
		{
            logger.Info("A Service has been started.");

            if (!String.IsNullOrEmpty(SpiderDocsApplication.ConnectionSettings.conn))
			{
				try
				{
					//create the listening socket...
					m_mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, SpiderDocsApplication.ServiceSettings.Port);

					//bind to local IP Address...
					m_mainSocket.Bind(ipLocal);

					//start listening...
					m_mainSocket.Listen(50);

					// create the call back for any client connections...
					m_mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);

					if(timer == null)
						timer = new TimerInt();
				}
				catch(Exception error)
				{
					logger.Error(error);
					stopService();
				}
			}
		}

//---------------------------------------------------------------------------------
		public void stopService()
		{
			try
			{
				if((m_mainSocket != null) && m_mainSocket.IsBound)
					m_mainSocket.Shutdown(SocketShutdown.Both);

			}catch {}
		}

//---------------------------------------------------------------------------------
		public void OnClientConnect(IAsyncResult asyn)
		{
			bool ans = false;

			try
			{
				// Here we complete/end the BeginAccept() asynchronous call
				// by calling EndAccept() - which returns the reference to
				// a new Socket object
				Socket workerSocket = m_mainSocket.EndAccept(asyn);

				// Send a welcome message to client
				string val = String.Join(";",
								SpiderDocsApplication.ConnectionSettings._database_address,
								SpiderDocsApplication.ConnectionSettings._database_name,
								SpiderDocsApplication.ConnectionSettings._user,
								SpiderDocsApplication.ConnectionSettings._password,
								SpiderDocsApplication.ConnectionSettings._mode);

				string msgToClient = "<conn>" + "!" + val;

				SendMsgToClient(msgToClient, workerSocket);

				// Let the worker Socket do the further processing for the 
				// just connected client
				WaitForData(workerSocket);

				// Since the main Socket is now free, it can go back and wait for
				// other clients who are attempting to connect
				m_mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);

				ans = true;

			}
			catch(ObjectDisposedException ex)
			{
                logger.Error(ex);
				Debugger.Log(0, "1", "\n OnClientConnection: Socket has been closed\n");
			}
			catch(SocketException error)
			{
				logger.Error(error);
			}
			finally
			{
				if(!ans)
				{
					stopService();
					startService();
				}
			}
		}

//---------------------------------------------------------------------------------
		void SendMsgToClient(string msg, Socket workerSocket)
		{
			// Convert the reply to byte array
			byte[] byData = Encoding.ASCII.GetBytes(msg);

			workerSocket.Send(byData);
		}

//---------------------------------------------------------------------------------
		void WaitForData(Socket soc)
		{
			try
			{
				SocketPacket theSocPkt = new SocketPacket(soc);
				soc.BeginReceive(theSocPkt.dataBuffer, 0, theSocPkt.dataBuffer.Length, SocketFlags.None, pfnWorkerCallBack, theSocPkt);

			}
			catch(SocketException error)
			{
				logger.Error(error);
			}
		}

//---------------------------------------------------------------------------------
		void OnDataReceived(IAsyncResult asyn)
		{
			SocketPacket socketData = (SocketPacket)asyn.AsyncState;

			try
			{
				// Complete the BeginReceive() asynchronous call by EndReceive() method
				// which will return the number of characters written to the stream 
				// by the client
				int iRx = socketData.m_currentSocket.EndReceive(asyn);
				char[] chars = new char[iRx + 1];

				// Extract the characters as a buffer
				Decoder d = Encoding.UTF8.GetDecoder();

			}
			catch(ObjectDisposedException ex)
			{
                logger.Error(ex);
                Debugger.Log(0, "1", "\nOnDataReceived: Socket has been closed\n");
			}
			catch(SocketException se)
			{
                logger.Error(se);
                if (se.ErrorCode == 10054) // Error code for Connection reset by peer
				{
					// Remove the reference to the worker socket of the closed client
					// so that this object will get garbage collected
					socketData.m_currentSocket = null;
				}
			}
		}

//---------------------------------------------------------------------------------
		private void bthTestService_Click(object sender,EventArgs e)
		{
			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.WindowStyle = ProcessWindowStyle.Hidden;
			startInfo.FileName = "cmd.exe";

			ServiceController[] services = ServiceController.GetServices();
			ServiceController service = services.FirstOrDefault(s => s.ServiceName == "Spider Docs Server Test");

			if(service == null)
			 	startInfo.Arguments = "/C sc create \"Spider Docs Server Test\" start= delayed-auto binPath= \"" + FileFolder.GetExecutePath() + "SpiderDocsWinService.exe\"";
			else
				startInfo.Arguments = "/C sc delete \"Spider Docs Server Test\"";

			// To get administrator right
			startInfo.Verb = "runas";

			Process process = new Process();
			process.StartInfo = startInfo;
			process.Start();
			process.WaitForExit();
		}

//---------------------------------------------------------------------------------
	}
}
