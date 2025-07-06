using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	class NutrunnerEth
	{
		public int nStation;
		public string strIP;
		public int nPort;

		public bool bReadData = false;


		public string strLineName = "";

		public bool bPSet;
		public int nPSet;
		public int nTiteNum;

		public string strPramID;
		string strTorque;
		string strAngle;
		string strTime;
		public string strResult;
		string strSaveResult;
		public string strRundownAngle;

		public string strReadBarcode;


		public string strTorqueMin;
		public string strTorqueMax;
		public string strRundownAngleMin;
		public string strRundownAngleMax;
		public string strAngleMin;
		public string strAngleMax;

		public string strToolId = "";

		public double dbTorqueData;
		public int nAngleData;

		public int nErrCode = 0;

		public string strBarcode = "";

		public int nCurrentStep;
		//byte[] buff = new byte[2048];                   // 수신버퍼
		byte[] btEtx = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x00 };
		public TITE_STATUS _Status;
		Stopwatch ReConnetTimer = new Stopwatch();
		Stopwatch AliveTimer = new Stopwatch();
		string strReadData = "";
		bool bAlive = false;
		bool bReciveData = false;
		bool bConnecting = false;

		public string strReadTorqueData = "";

		HIVE_Timer _tTimer = new HIVE_Timer();

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
						theApp.AppendLogMsg("Retry connecting the nutrunner controller", MSG_TYPE.INFO);
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
					Send("0020" + "0060" + "0020");
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

						if (strReadData.Substring(4, 4) == "9999")
						{
						

							AliveTimer.Restart();
							bAlive = true;
						}
						else if (strReadData.Substring(4, 4) == "0061")
						{
							
							
							try
							{

								strReadTorqueData = strReadData.Replace("\0", String.Empty);
								//theApp.AppendLogMsg(strReadTorqueData, MSG_TYPE.LOG);
								bReadData = true;
				
								strPramID = strReadData.Substring(92, 3);                          // Parameter Sst ID
								strTorqueMin = strReadData.Substring(159, 6);                         // Torque Min
								strTorqueMax = strReadData.Substring(167, 6);                         // Torque Max
								strTorque = strReadData.Substring(183, 6);                         // Torque

								strAngleMin = strReadData.Substring(191, 5);                          // Angle Min
								strAngleMax = strReadData.Substring(196, 5);                          // Angle Max
								strAngle = strReadData.Substring(212, 5);                          // Angle


								strResult = strReadData.Substring(120, 1);
								strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");         // 현재 시간 기준으로 저장


								strRundownAngleMin = strReadData.Substring(219, 5);                   // Rundown Angle [ Torque 를 적용받지않는 Angle값 ]
								strRundownAngleMax = strReadData.Substring(226, 5);                   // Rundown Angle [ Torque 를 적용받지않는 Angle값 ]
								strRundownAngle = strReadData.Substring(233, 5);                   // Rundown Angle [ Torque 를 적용받지않는 Angle값 ]
																								   // ㄴ 요청사유 : 원자재 불량으로 인한 사회불량 발생

								if (strResult == "0") strSaveResult = "NG";
								else if (strResult == "1") strSaveResult = "OK";
								else strSaveResult = "N/A";

								double.TryParse(strTorque, out dbTorqueData);           // 토크값 변환
								int.TryParse(strAngle, out nAngleData);                 // 앵글값 변환


								// 이더넷 체결 정보 전송
								if (strResult == "0") { _Status = TITE_STATUS.NG; }
								else if (strResult == "1") { _Status = TITE_STATUS.OK; }
								else { _Status = TITE_STATUS.NG; }

								bReadData = true;
							}
							catch { }



							nCurrentStep = 40;
							break;
						}
					}

					if (bPSet)
					{
						bPSet = false;
						nCurrentStep = 50;
						//theApp.AppendLogMsg("스케쥴 할당됨");
						break;
					}
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
					AliveTimer.Restart();
					//Array.Clear(buff, 0, buff.Length);  // 버퍼 클리어
					bAlive = true;

					nCurrentStep = 31;
					break;


			
			}
		}

		public void SaveResultData()
		{
			try
			{
				string strMessge = "";

		
					strMessge = String.Format("ST : {0}, ", nStation);                 // 스테이션 번호
					strMessge += "Time : " + strTime;                                   // 시간
					strMessge += ", Param ID :" + strPramID;                            // 파라미터 ID
					//strMessge += ", Tip : " + nTipNum.ToString();
					strMessge += ", Num : " + nTiteNum.ToString();

					strMessge += ", TorqueMin :" + strTorqueMin;                              // 토크값
					strMessge += ", TorqueMax :" + strTorqueMax;                              // 토크값
					strMessge += ", Torque :" + strTorque;                              // 토크값

					strMessge += ", AngleMin : " + strAngleMin;                               // 앵글값
					strMessge += ", AngleMax : " + strAngleMax;                               // 앵글값
					strMessge += ", Angle : " + strAngle;                               // 앵글값

					strMessge += ", Rundown Angle Min : " + strRundownAngleMin;                // 런다운 앵글
					strMessge += ", Rundown Angle Max : " + strRundownAngleMax;                // 런다운 앵글
					strMessge += ", Rundown Angle : " + strRundownAngle;                // 런다운 앵글

					strMessge += ", Result : " + strSaveResult;                         // 결과값
					strMessge += "\r\n";
				
				if(nStation == 1)
				{
					string strFolderPath = String.Format(@"DATA\\NUTRUNNER{0}\\", nStation);

					DirectoryInfo dir = new DirectoryInfo(strFolderPath);
					if (dir.Exists == false) { dir.Create(); }

					string savePath = String.Format("{0}{1}.txt", strFolderPath, DateTime.Now.ToString("yyMMdd"));
					System.IO.File.AppendAllText(savePath, strMessge, Encoding.Default);



					strFolderPath = String.Format(@"TEMP_DATA\\ST{0}\\", nStation);

					dir = new DirectoryInfo(strFolderPath);
					if (dir.Exists == false) { dir.Create(); }

					savePath = String.Format("{0}ST{1}.txt", strFolderPath, nStation);
					System.IO.File.AppendAllText(savePath, strMessge, Encoding.Default);


					strFolderPath = String.Format(@"DATA\\ST{0}_NUT\\", nStation);

					dir = new DirectoryInfo(strFolderPath);
					if (dir.Exists == false) { dir.Create(); }

					savePath = String.Format("{0}{1}.txt", strFolderPath, strBarcode);
					System.IO.File.AppendAllText(savePath, strMessge, Encoding.Default);
				}
				else
				{
					string strFolderPath = String.Format(@"DATA2\\NUTRUNNER\\");

					DirectoryInfo dir = new DirectoryInfo(strFolderPath);
					if (dir.Exists == false) { dir.Create(); }

					string savePath = String.Format("{0}{1}.txt", strFolderPath, DateTime.Now.ToString("yyMMdd"));
					System.IO.File.AppendAllText(savePath, strMessge, Encoding.Default);



					strFolderPath = String.Format(@"TEMP_DATA2\\ST\\");

					dir = new DirectoryInfo(strFolderPath);
					if (dir.Exists == false) { dir.Create(); }

					savePath = String.Format("{0}ST.txt", strFolderPath);
					System.IO.File.AppendAllText(savePath, strMessge, Encoding.Default);


					strFolderPath = String.Format(@"DATA2\\NUT\\");

					dir = new DirectoryInfo(strFolderPath);
					if (dir.Exists == false) { dir.Create(); }

					savePath = String.Format("{0}{1}.txt", strFolderPath, strBarcode);
					System.IO.File.AppendAllText(savePath, strMessge, Encoding.Default);
				}
				

			}
			catch (Exception e)
			{
				if (nStation == 1)
				{

					theApp.AppendLogMsg("Nutrunner" + e.Message, MSG_TYPE.ERROR);

					Console.WriteLine(e.ToString());

				}
				else
				{

					theApp.AppendLogMsg2("Nutrunner" + e.Message, MSG_TYPE.ERROR);

					Console.WriteLine(e.ToString());
				}
			}



		}


		public void SetPort()
		{
			try
			{
				IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(strIP), nPort);
				// Create a TCP/IP socket.  

				bConnecting = true;
				client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
			}
			catch (Exception e)
			{
				if (nStation == 1)
				{

					theApp.AppendLogMsg("너트러너" + e.Message, MSG_TYPE.ERROR);

					Console.WriteLine(e.ToString());

				}
				else
				{

					theApp.AppendLogMsg2("너트러너" + e.Message, MSG_TYPE.ERROR);

					Console.WriteLine(e.ToString());
				}

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
				if (nStation == 1)
				{

					theApp.AppendLogMsg("너트러너" + e.Message, MSG_TYPE.ERROR);

					Console.WriteLine(e.ToString());

				}
				else
				{

					theApp.AppendLogMsg2("너트러너" + e.Message, MSG_TYPE.ERROR);

					Console.WriteLine(e.ToString());
				}
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
				if (nStation == 1)
				{

					theApp.AppendLogMsg("너트러너" + e.Message, MSG_TYPE.ERROR);

					Console.WriteLine(e.ToString());

				}
				else
				{

					theApp.AppendLogMsg2("너트러너" + e.Message, MSG_TYPE.ERROR);

					Console.WriteLine(e.ToString());
				}
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
					//theApp.AppendDebugMsg($"{strReadData}", "NUT R");
					//if(nStation == 1) theApp.AppendLogMsg($"[LOW_ETH1/{nStation}] : " + strReadData);

					bReciveData = true;

					//theApp.AppendLogMsg(strReadData, MSG_TYPE.INFO);
					// Get the rest of the data.  
					client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
				}
			}
			catch (Exception e)
			{
				if (nStation == 1)
				{

					theApp.AppendLogMsg("너트러너" + e.Message, MSG_TYPE.ERROR);

					Console.WriteLine(e.ToString());

				}
				else
				{

					theApp.AppendLogMsg2("너트러너" + e.Message, MSG_TYPE.ERROR);

					Console.WriteLine(e.ToString());
				}
			}
		}

		private void Send(String data, bool bNullOnly = false)
		{
			try
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
					//theApp.AppendDebugMsg($"{data}, {btEtx}", "NUT S");
				}

				// Begin sending the data to the remote device.  
				client.BeginSend(sendData.ToArray(), 0, sendData.ToArray().Length, 0, new AsyncCallback(SendCallback), client);
				_tack.Stop();
			}
			catch (Exception e)
			{
				if (nStation == 1)
				{

					theApp.AppendLogMsg("너트러너" + e.Message, MSG_TYPE.ERROR);

					Console.WriteLine(e.ToString());

				}
				else
				{

					theApp.AppendLogMsg2("너트러너" + e.Message, MSG_TYPE.ERROR);

					Console.WriteLine(e.ToString());
				}
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

				// Signal that all bytes have been sent.  
			}
			catch (Exception e)
			{
				if (nStation == 1)
				{

					theApp.AppendLogMsg("너트러너" + e.Message, MSG_TYPE.ERROR);

					Console.WriteLine(e.ToString());

				}
				else
				{

					theApp.AppendLogMsg2("너트러너" + e.Message, MSG_TYPE.ERROR);

					Console.WriteLine(e.ToString());
				}
			}
		}
	}
}

