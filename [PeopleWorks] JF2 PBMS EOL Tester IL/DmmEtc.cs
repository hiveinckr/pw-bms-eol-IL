using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	public class DmmEtc
	{
		public bool bPortStat = false;
		public bool bPrintStart = false;


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

		// 이더넷 포트
		Stopwatch ReConnetTimer = new Stopwatch();
		Stopwatch AliveTimer = new Stopwatch();
		public string strReadMessage = "";
		byte[] buff = new byte[1024];


		public string strIP;
		public int nPort;


		public int nCurrentStep;

		public bool bReadOk = false;

		public void Process()
		{
			if (client != null && !bConnecting && !client.Connected)
			{
				nCurrentStep = 0;
				if (ReConnetTimer.IsRunning)
				{
					if (ReConnetTimer.ElapsedMilliseconds > 5000)           // 소켓이 닫혀있다면 5초마다 재접속 시도한다
					{
						SetPort();
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

		}

		public void SetPort()
		{
			IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(strIP), nPort);
			// Create a TCP/IP socket.  

			bConnecting = true;
			client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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
				//theApp.AppendLogMsg(e.ToString(), MSG_TYPE.INFO);
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
				client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
					new AsyncCallback(ReceiveCallback), state);
			}
			catch (Exception e)
			{
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
					strReadMessage = Encoding.ASCII.GetString(state.buffer, 0, bytesRead);

					//theApp.AppendLogMsg($"{strReadMessage}", MSG_TYPE.INFO);
					bReadOk = true;
					bReciveData = true;
					//theApp.AppendLogMsg(strReadMessage, MSG_TYPE.INFO);
					//theApp.AppendLogMsg(strReadData, MSG_TYPE.INFO);
					// Get the rest of the data.  
					client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		public void TriggerOn()
		{
			Send("start");
		}

		public void Send(String data, bool bUseEtx = false)
		{
			bReadOk = false;
			bReciveData = false;
			List<byte> sendData = new List<byte>();

			sendData.AddRange(Encoding.UTF8.GetBytes(data));

			if (bUseEtx)
			{
				sendData.Add(0x0D);
				sendData.Add(0x0A);
			}

			try
			{
				// Begin sending the data to the remote device.  
				client.BeginSend(sendData.ToArray(), 0, sendData.ToArray().Length, 0, new AsyncCallback(SendCallback), client);
			}
			catch (Exception _e)
			{
				//theApp.AppendLogMsg(_e.Message, MSG_TYPE.ERROR);
			}

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

			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		public bool IsReadData()
		{
			return bReadOk;
		}
	}
}
