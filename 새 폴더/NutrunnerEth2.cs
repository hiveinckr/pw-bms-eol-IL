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
	class NutrunnerEth2
	{
		public int nStation;
		public string strIP;
		public int nPort;

		public bool bReadData = false;


		public string strLineName = "";

		public bool bPSet;
		public int nPSet;

		public string strPramID;
		string strTorque;
		string strAngle;
		string strTime;
		public string strResult;
		string strSaveResult;
		public string strRundownAngle;

		public string strToolId = "";

		public double dbTorqueData;
		public int nAngleData;

		public int nTiteX;
		public int nTiteY;
		public int nTiteZ;
		public int nTiteNum;

		public string strBarcode = "";

		public int nCurrentStep;
		//byte[] buff = new byte[2048];                   // 수신버퍼
		byte[] btEtx = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x00 };

		Stopwatch ReConnetTimer = new Stopwatch();
		Stopwatch AliveTimer = new Stopwatch();
		string strReadData = "";
		bool bAlive = false;
		bool bReciveData = false;
		bool bConnecting = false;
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

		Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

		public string strReadTorqueData = "";

		HIVE_Timer _tTimer = new HIVE_Timer();

		public void Process()
		{

			//소켓 연결이 되어있지않다면 해당 시간마다 연결 시도함
			if (client != null && !bConnecting && !client.Connected)
			{
				nCurrentStep = 0;
				if (ReConnetTimer.IsRunning)
				{
					if (ReConnetTimer.ElapsedMilliseconds > 5000)           // 소켓이 닫혀있다면 5초마다 재접속 시도한다
					{
						SetPort();
						ReConnetTimer.Restart();
						theApp.AppendLogMsg("너트러너 재접속 시도", MSG_TYPE.INFO);
					}
				}
				else
				{
					ReConnetTimer.Restart();
				}
				return;
			}
			if (client == null || bConnecting || !client.Connected) { return; }

			//Receive(client);

			// PROCESS
			switch (nCurrentStep)
			{
				case 0:
					theApp.AppendLogMsg("너트러너 재시작", MSG_TYPE.INFO);
					nCurrentStep = 10;
					break;

				case 10:
					Send("0020" + "0001" + "0010");
					//Array.Clear(buff, 0, buff.Length);  // 버퍼 클리어
					AliveTimer.Restart();
					nCurrentStep++;
					break;

				case 11:
					if (AliveTimer.ElapsedMilliseconds > 5000)
					{
						nCurrentStep = 0;
					}

					if (bReciveData)
					{
						bReciveData = false;
						//strReadData = Encoding.Default.GetString(buff);
						if (strReadData.Length > 8)
						{
							if (strReadData.Substring(4, 4) == "0002" || strReadData.Substring(4, 4) == "0004")
							{
								nCurrentStep = 20;
							}
						}
					}
					break;


				case 20:
					Send("0020" + "0060" + "9980");
					//Array.Clear(buff, 0, buff.Length);  // 버퍼 클리어
					nCurrentStep++;
					break;

				case 21:
					if (AliveTimer.ElapsedMilliseconds > 5000)
					{
						nCurrentStep = 0;
					}

					if (bReciveData)
					{
						bReciveData = false;
						//strReadData = Encoding.Default.GetString(buff);
						if (strReadData.Substring(4, 4) == "0005" || strReadData.Substring(4, 4) == "0004")
						{
							nCurrentStep = 30;
						}
					}
					break;


				case 30:

					Send("0020" + "9999" + "0010");
					//Array.Clear(buff, 0, buff.Length);  // 버퍼 클리어
					AliveTimer.Restart();
					bAlive = false;
					nCurrentStep++;
					break;

				case 31:
					if (AliveTimer.ElapsedMilliseconds > 10000)
					{
						if (bAlive)
						{
							nCurrentStep = 30;          // 3초마다 재전송
						}
						else
						{
							nCurrentStep = 0;
						}
					}

					if (bReciveData)
					{
						bReciveData = false;
						//데이터가 들어온 경우 비교
						//strReadData = Encoding.Default.GetString(buff);
						if (strReadData.Substring(4, 4) == "9999")
						{
							AliveTimer.Restart();
							bAlive = true;
						}
						else if (strReadData.Substring(4, 4) == "0061")
						{
							AliveTimer.Restart();
							bAlive = true;
							try
							{

								strReadTorqueData = strReadData.Replace("\0", String.Empty);
								//theApp.AppendLogMsg(strReadTorqueData, MSG_TYPE.LOG);
								bReadData = true;

							}
							catch { }

							nCurrentStep = 40;
							break;
						}
					}

					//if (bPSet)
					//{
					//	bPSet = false;
					//	nCurrentStep = 50;
					//	break;
					//}
					break;


				case 40:
					Send("0020" + "0062" + "0010");
					//Array.Clear(buff, 0, buff.Length);  // 버퍼 클리어
					AliveTimer.Restart();
					bAlive = true;

					nCurrentStep = 31;
					break;


				case 50:
					Send("00230018            " + nPSet.ToString("000"), true);
					//Array.Clear(buff, 0, buff.Length);  // 버퍼 클리어
					AliveTimer.Restart();
					bAlive = true;

					nCurrentStep = 31;
					break;
			}
		}

		public void SetPort()
		{
			IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(strIP), nPort);
			// Create a TCP/IP socket.  

			bConnecting = true;
			client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);

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
				theApp.AppendLogMsg("너트러너" + e.Message, MSG_TYPE.ERROR);

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
				theApp.AppendLogMsg("너트러너" + e.Message, MSG_TYPE.ERROR);

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
					strReadData = Encoding.ASCII.GetString(state.buffer, 0, bytesRead);

					//if(nStation == 1) theApp.AppendLogMsg($"[LOW_ETH2/{nStation}] : " + strReadData);

					bReciveData = true;

					//theApp.AppendLogMsg(strReadData, MSG_TYPE.INFO);
					// Get the rest of the data.  
					client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
				}
			}
			catch (Exception e)
			{
				theApp.AppendLogMsg("너트러너" + e.Message, MSG_TYPE.ERROR);

				Console.WriteLine(e.ToString());
			}
		}

		private void Send(String data, bool bNullOnly = false)
		{
			Stopwatch _tack = new Stopwatch();
			_tack.Start();
			List<byte> sendData = new List<byte>();
			if (bNullOnly)
			{
				sendData.AddRange(Encoding.UTF8.GetBytes(data));
				sendData.Add(0x00);
			}
			else
			{
				sendData.AddRange(Encoding.UTF8.GetBytes(data));
				sendData.AddRange(btEtx);
			}

			// Begin sending the data to the remote device.  
			client.BeginSend(sendData.ToArray(), 0, sendData.ToArray().Length, 0, new AsyncCallback(SendCallback), client);
			_tack.Stop();
		}

		private void SendCallback(IAsyncResult ar)
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
				theApp.AppendLogMsg("너트러너" + e.Message, MSG_TYPE.ERROR);

				Console.WriteLine(e.ToString());
			}
		}
	}
}
