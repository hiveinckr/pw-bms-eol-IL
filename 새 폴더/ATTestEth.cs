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
	public class ATTestEth
	{
		public bool bPortStat = false;
		public bool bPrintStart = false;


		bool bReciveData = false;
		bool bConnecting = false;
		List<string> _ReadList = new List<string>();

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

		// 이더넷 포트
		Stopwatch ReConnetTimer = new Stopwatch();
		Stopwatch AliveTimer = new Stopwatch();
		public string strReadBarcode = "";
		byte[] buff = new byte[1024];


		public int nCurrentStep;

		public bool bReadOk = false;
		public bool bConnected = false;



		public void OpenPort(string strIp, int nPort)
		{
			IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(strIp), nPort);
			// Create a TCP/IP socket.  

			bConnecting = true;
			bConnected = false;


			try
			{
				if (!client.Connected)
				{
					client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
				}
				else
				{
					bConnected = true;
				}
			}
			catch (Exception _e) { theApp.AppendLogMsg(_e.Message, MSG_TYPE.INFO); }

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
				bConnected = true;
			}
			catch (Exception e)
			{
				bConnecting = false;
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
					strReadBarcode = Encoding.UTF8.GetString(state.buffer, 0, bytesRead);
					string strReadData = strReadBarcode.Replace("\r\n", "");
					//string[] lines = strReadBarcode.Split(new string[] { "\r\n" }, StringSplitOptions.None);

					_ReadList.Add(strReadData);
					theApp.AppendLogMsg(strReadData, MSG_TYPE.INFO);

					//for (int i = 0; i < lines.Length; i++)
					//{
					//	_ReadList.Add(lines[i]);
					//	theApp.AppendLogMsg(lines[i], MSG_TYPE.INFO);
					//}


					bReadOk = true;
					// There might be more data, so store the data received so far.  
					bReciveData = true;

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

		public string GetReciveData(int nIndex)
		{
			if (nIndex >= _ReadList.Count)
			{
				return "NG";
			}
			else
			{
				return _ReadList[nIndex];
			}
		}
		public void Send(String data)
		{
			_ReadList.Clear();
			List<byte> sendData = new List<byte>();

			sendData.AddRange(Encoding.UTF8.GetBytes(data));


			// Begin sending the data to the remote device.  
			client.BeginSend(sendData.ToArray(), 0, sendData.ToArray().Length, 0, new AsyncCallback(SendCallback), client);
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

	}
}

