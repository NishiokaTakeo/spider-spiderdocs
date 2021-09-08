using System;
using System.Data.Common;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Net;
using System.Net.Sockets;
using NLog;
//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class SpiderDocsServer
	{
//---------------------------------------------------------------------------------
		AsyncCallback m_pfnCallBack;
		Socket m_clientSocket;
		ServerSettings ServerSettings;

		public string err = "";

		public event Action<ServerSettings,bool> onConnected;
		public event Action onConnectionErr;
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();

//---------------------------------------------------------------------------------
		class SocketPacket
		{
			public System.Net.Sockets.Socket thisSocket;
			public byte[] dataBuffer = new byte[1024];
		}

//---------------------------------------------------------------------------------
		public SpiderDocsServer(ServerSettings ServerSettings)
		{
			this.ServerSettings = ServerSettings;
		}

//---------------------------------------------------------------------------------
		public bool Connect()
		{
			bool ans = false;

			if(ServerSettings.localDb)
			{
				Crypt crypt = new Crypt();
				ServerSettings.conn = crypt.Encrypt(DbManager.GetLocalDBConnectionStr(FileFolder.GetExecutePath()));
				ServerSettings.svmode = crypt.Encrypt(DbManager.GetServerModeStr(DbManager.en_sql_mode.LocalDb));

				Connected(TryConnectDatabase());

				ans = true;

			}else
			{
                try
                {
                    if (!String.IsNullOrEmpty(ServerSettings.server) && (0 < ServerSettings.port))
                    {
                        string hostIPAddress = Utilities.CheckIpAddress(ServerSettings.server);

                        // Create the socket instance
                        m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                        // Set the remote IP address
                        IPAddress ip = IPAddress.Parse(hostIPAddress);

                        // Create the end point 
                        IPEndPoint ipEnd = new IPEndPoint(ip, ServerSettings.port);

                        // Connect to the remote host
                        m_clientSocket.Connect(ipEnd);
                        if (m_clientSocket.Connected)
                            WaitForData();
                        
                        ans = true;
                    }
                }
                catch (System.Net.Sockets.SocketException ex)
                {
                    Disconnect();

                    err = ex.Message;

                    switch (ex.ErrorCode)
                    {
                        case 10061:
                            logger.Debug("Connection to {0}:{1} timeout.", ServerSettings.server, ServerSettings.port);
                            break;
                        default:
                            logger.Error(ex,"server:{0}, port:{1}",ServerSettings.server,ServerSettings.port);

                            break;
                    }

                    if (onConnectionErr != null)
                        onConnectionErr();
                }
                catch (Exception ex)
                {
                    logger.Error(ex,"server:{0}, port:{1}",ServerSettings.server,ServerSettings.port);

                    Disconnect();

                    err = ex.Message;

                    if (onConnectionErr != null)
                        onConnectionErr();
                }
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		void WaitForData()
		{
			try
			{
				if(m_pfnCallBack == null)
					m_pfnCallBack = new AsyncCallback(OnDataReceived);

				SocketPacket theSocPkt = new SocketPacket();
				theSocPkt.thisSocket = m_clientSocket;
				m_clientSocket.BeginReceive(theSocPkt.dataBuffer, 0, theSocPkt.dataBuffer.Length, SocketFlags.None, m_pfnCallBack, theSocPkt);

			}
			catch(SocketException se)
			{
                logger.Error(se);

				Disconnect();

				err = se.Message;

				if(onConnectionErr != null)
					onConnectionErr();
			}
		}

//---------------------------------------------------------------------------------
		void OnDataReceived(IAsyncResult asyn)
		{
			try
			{
                logger.Trace("BEFORE:SocketPacket theSockId = (SocketPacket)asyn.AsyncState;");
                SocketPacket theSockId = (SocketPacket)asyn.AsyncState;
				int iRx = theSockId.thisSocket.EndReceive(asyn);
                logger.Trace("BEFORE:char[] chars = new char[iRx + 1];");
                char[] chars = new char[iRx + 1];
				System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
				int charLen = d.GetChars(theSockId.dataBuffer, 0, iRx, chars, 0);
				System.String szData = new System.String(chars);
				string msgFromServer = szData.Replace("\0","");
                
                //treat the information
                String[] eachInfo = msgFromServer.Split('!');
                logger.Trace("BEFORE:if (eachInfo[0].IndexOf(\" < conn > \") > -1)");
                if (eachInfo[0].IndexOf("<conn>") > -1)
				{
					Crypt crypt = new Crypt();

					//Split the information
					String[] eachConn = eachInfo[1].Split(';');
					ServerSettings.conn = ServerSettings.GenerateConnStrFromPrams(crypt.Decrypt(eachConn[0]), crypt.Decrypt(eachConn[1]), crypt.Decrypt(eachConn[2]), crypt.Decrypt(eachConn[3]));
					ServerSettings.svmode = eachConn[4];
                    logger.Trace("BEFORE:Connected(TryConnectDatabase());");
                    Connected(TryConnectDatabase());
				}
			}
			catch(Exception ex)
			{
                logger.Error(ex, "server:{0}, port:{1}, CompletedSynchronously:{2}, IsCompleted:{3}", ServerSettings.server,ServerSettings.port, asyn.CompletedSynchronously, asyn.IsCompleted);

				Disconnect();

				err = ex.Message;

				if(onConnectionErr != null)
					onConnectionErr();
			}
		}

//---------------------------------------------------------------------------------
		bool TryConnectDatabase()
		{
			DbManager wrk = new DbManager(ServerSettings.conn, ServerSettings.svmode);

			return wrk.IsConnectionCheck();
		}

//---------------------------------------------------------------------------------
		void Connected(bool ConnectionChk)
		{
			Disconnect();

			if(onConnected != null)
            {
                onConnected(ServerSettings, ConnectionChk);                                
			}
		}

//---------------------------------------------------------------------------------
		void Disconnect()
		{
			if(m_clientSocket != null)
			{
				if(m_clientSocket.Connected)
					m_clientSocket.Shutdown(SocketShutdown.Send);

				m_clientSocket.Close();	
				m_clientSocket = null;
			}
		}

//---------------------------------------------------------------------------------
	}
}
