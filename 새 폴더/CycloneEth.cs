using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	public class CycloneEth
	{
		public int nStation;
		public string strIP;
		public int nPort;
		public int nMyPort;
		//Socket soket;
		public int nCurrentStep;
		byte[] buff = new byte[2048];                   // 수신버퍼

		Stopwatch ReConnetTimer = new Stopwatch();
		Stopwatch AliveTimer = new Stopwatch();
		string strReadData = "";
		string strOldString = "";

		public int nTiteX;
		public int nTiteY;
		public int nTiteZ;

		public bool bReadOk = false;

		bool bConnecting = false;

		private UdpClient udpClient;
		IPEndPoint remoteIP;
		IPEndPoint myIP;

		string strMessage = "";

		//================================================
		// 통신 데이터

		public string strBracode = "";
		public int[] nNowTiteCount = new int[20];
		public int[] nMaxTiteCount = new int[20];
		public int[] nTipUseStatus = new int[20];

		public string[] strsubName = new string[20];
		public string[] strsubBarcode = new string[20];
		public string strsubName1 = "";
		public string strsubBarcode1 = "";
		public bool bSubBarcode1Result = false;

		public string strsubName2 = "";
		public string strsubBarcode2 = "";
		public bool bSubBarcode2Result = false;

		public string strsubName3 = "";
		public string strsubBarcode3 = "";
		public bool bSubBarcode3Result = false;

		public bool bFinishFlag = false;

		public int nTiteComplitStep = 0;
		public int nTiteCwCCw = 0;
		public int nWorkStatus = 0;
		//================================================

		public bool bReciveData = false;

		public class StateObject
		{
			// Client socket.  
			public Socket workSocket = null;
			// Size of receive buffer.  
			public const int BufferSize = 1024;
			// Receive buffer.  
			public byte[] buffer = new byte[BufferSize];
			// Received data string.  
			public StringBuilder sb = new StringBuilder();
		}

		Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

		public void Process()
		{

			//소켓 연결이 되어있지않다면 해당 시간마다 연결 시도함
			//if (udpClient == null) { return; }

			//try
			//{
			//	if (udpClient != null && !bConnecting && !udpClient.Client.Connected)
			//	{
			//		nCurrentStep = 0;
			//		if (ReConnetTimer.IsRunning)
			//		{
			//			if (ReConnetTimer.ElapsedMilliseconds > 5000)           // 소켓이 닫혀있다면 5초마다 재접속 시도한다
			//			{
			//				theApp.AppendLogMsg("카운터 재연결 시도", MSG_TYPE.INFO);
			//				SetPort();
			//				//SetBeginPort();
			//				ReConnetTimer.Restart();
			//			}
			//		}
			//		else
			//		{
			//			ReConnetTimer.Restart();
			//		}
			//		return;
			//	}
			//}
			//catch(Exception e) { theApp.AppendLogMsg("NC : " + e.Message, MSG_TYPE.ERROR); }


			//if (udpClient == null || bConnecting || !udpClient.Client.Connected) { return; }



			//if (udpClient == null) { return; }
			if (client != null && !bConnecting && !client.Connected)
			{
				nCurrentStep = 0;
				if (ReConnetTimer.IsRunning)
				{
					if (ReConnetTimer.ElapsedMilliseconds > 5000)           // 소켓이 닫혀있다면 5초마다 재접속 시도한다
					{
						//SetPort();
						SetBeginPort();
						ReConnetTimer.Restart();
					}
				}
				else
				{
					ReConnetTimer.Restart();
				}
				return;
			}
			if (client == null || bConnecting || !client.Connected) { return; }

			// PROCESS
			switch (nCurrentStep)
			{

				case 0:
					AliveTimer.Restart();
					nCurrentStep = 100;
					break;

				case 100:
					//strMessage = "RSP_NUT_STATUS";     // RSP_NUT_STATUS,
					
					if (strOldString != strMessage)
					{
						//SendData(strMessage);
						BeginSend(strMessage);
						strOldString = strMessage;
					}

					//if()
					nCurrentStep = 0;
					break;

					//case 200:
					//	if (bReciveData)
					//	{
					//		bReciveData = false;
					//		theApp.AppendLogMsg("리시브 까지 옴", MSG_TYPE.ERROR);

					//		//theApp.AppendDebugMsg(strReadData, "Distance");
					//		if (strReadData.Split(',').Length == 2)
					//		{
					//			theApp.AppendLogMsg(strReadData.Split(',')[0], MSG_TYPE.ERROR);
					//			if (strReadData.Split(',')[0] == "OK")
					//			{

					//				bFinishFlag = true;
					//				nCurrentStep = 0;
					//			}


					//		}

					//		             // 정상적인 데이터가 들어왔다면 스텝 20번으로 이동

					//	}
					//	else
					//	{
					//		nCurrentStep = 0;
					//	}
					//	break;



			}

		}



		public void SetBeginPort()
		{
			try
			{
				IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(strIP), nPort);
				IPEndPoint myEp = new IPEndPoint(IPAddress.Any, nMyPort);
				// Create a TCP/IP socket.  
				bConnecting = true;
				client.Bind(myEp);
				client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
			}
			catch (Exception e)
			{
				bConnecting = false;

				theApp.AppendLogMsg("Cyclone Fx" + e.Message, MSG_TYPE.ERROR);
		


				Console.WriteLine(e.ToString());
			}


		}
		private void ConnectCallback(IAsyncResult ar)
		{
			try
			{
				// Retrieve the socket from the state object.  
				Socket client = (Socket)ar.AsyncState;

				// Complete the connection.  
				client.EndConnect(ar);

				Console.WriteLine("Socket connected to {0}",
					client.RemoteEndPoint.ToString());

				// Signal that the connection has been made.  
				ReceiveStart(client);
				bConnecting = false;
			}
			catch (Exception e)
			{
				bConnecting = false;

					theApp.AppendLogMsg("Cyclone Fx" + e.Message, MSG_TYPE.ERROR);
		

				Console.WriteLine(e.ToString());
			}
		}

		private void ReceiveStart(Socket client)
		{
			try
			{
				// Create the state object.  
				StateObject state = new StateObject();
				state.workSocket = client;

				// Begin receiving the data from the remote device.  
				client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
			}
			catch (Exception e)
			{

					theApp.AppendLogMsg("Cyclone Fx" + e.Message, MSG_TYPE.ERROR);
				

				Console.WriteLine(e.ToString());
			}
		}

		private void ReceiveCallback(IAsyncResult ar)
		{
			try
			{
				// Retrieve the state object and the client socket
				// from the asynchronous state object.  
				StateObject state = (StateObject)ar.AsyncState;
				Socket client = state.workSocket;

				// Read data from the remote device.  
				int bytesRead = client.EndReceive(ar);


				if (bytesRead > 0)
				{

					// There might be more data, so store the data received so far.  
					bReciveData = true;
					strReadData = Encoding.ASCII.GetString(state.buffer, 0, bytesRead);
					theApp.AppendLogMsg($"TABLET Recive : {strReadData}", MSG_TYPE.INFO);

					// Get the rest of the data.  
					client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
				}
			}
			catch (Exception e)
			{

					theApp.AppendLogMsg("Cyclone Fx" + e.Message, MSG_TYPE.ERROR);
				

				Console.WriteLine(e.ToString());
			}
		}


		public void SetPort()
		{

			try
			{
				bConnecting = true;
				//allDone.Reset();
				remoteIP = new IPEndPoint(IPAddress.Parse(strIP), nPort);
				myIP = new IPEndPoint(IPAddress.Parse(_Config.strCounterMyIP), nMyPort);
				udpClient = new UdpClient(myIP);
				udpClient.Connect(remoteIP);
				bConnecting = false;
			}
			catch (Exception ex) { theApp.AppendLogMsg(ex.Message, MSG_TYPE.ERROR); }

		}

		//ManualResetEvent allDone = new ManualResetEvent(false);

		//void ConnectCallback1(IAsyncResult ar)
		//{
		//	try
		//	{
		//		allDone.Set();
		//		Socket s = (Socket)ar.AsyncState;
		//		s.EndConnect(ar);
		//	}
		//	catch (Exception e) { theApp.AppendLogMsg(nStation.ToString()+"C" + ":" + e.Message, MSG_TYPE.ERROR); }
		//}


		public void BeginSend(String data, bool bNullOnly = false)
		{

			List<byte> sendData = new List<byte>();
			if (bNullOnly)
			{
				sendData.AddRange(Encoding.ASCII.GetBytes(data));
				sendData.Add(0x0D);
				sendData.Add(0x0A);
				//theApp.AppendLogMsg(data, MSG_TYPE.INFO);

			}
			else
			{
				sendData.AddRange(Encoding.ASCII.GetBytes(data));
				sendData.Add(0x0D);
				sendData.Add(0x0A);
				//theApp.AppendLogMsg(data, MSG_TYPE.INFO);
			}

			// Begin sending the data to the remote device.  
			client.BeginSend(sendData.ToArray(), 0, sendData.ToArray().Length, 0, new AsyncCallback(BeginSendCallback), client);
		}

		private void BeginSendCallback(IAsyncResult ar)
		{
			try
			{
				// Retrieve the socket from the state object.  
				Socket client = (Socket)ar.AsyncState;

				// Complete sending the data to the remote device.  
				int bytesSent = client.EndSend(ar);
				Console.WriteLine("Sent {0} bytes to server.", bytesSent);

				// Signal that all bytes have been sent.  
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}





	}


}
