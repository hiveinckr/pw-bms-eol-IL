using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	public class ModbusTCP
	{
		public bool bResultOk = false;
		byte[] bytes;
		byte[] sendBytes;
		byte[] receiveBytes;
		//지정된 ip와 포트를 사용해 메시지를 전달하고 그 결과를 바이트 배열로 받는다
		/// <summary>
		/// TCP 통신을 수행한다
		/// </summary>
		/// <param name="ip">Target IP</param>
		/// <param name="port">Target Port Number</param>
		/// <param name="msg">CRC가 포함된 보낼 메시지</param>
		/// <returns>통신 결과 바이트 배열</returns>
		public byte[] TcpSocket(string ip, Int32 port, byte[] sendData)
		{
			TcpClient tcpClient = new TcpClient();
			tcpClient.SendTimeout = 3000;
			tcpClient.ReceiveTimeout = 3000;
			tcpClient.SendBufferSize = 512;
			tcpClient.ReceiveBufferSize = 512;

			bytes = new byte[tcpClient.ReceiveBufferSize];
			sendBytes = sendData;

			receiveBytes = new byte[512];
			//if (sendBytes[1] == (byte)6)
			//{
			//    receiveBytes = new byte[sendBytes.Length + 1];
			//}
			//else
			//{
			//    receiveBytes = new byte[(sendBytes[4] * 16 + sendBytes[5]) * 2 + 5];
			//}

			try
			{
				tcpClient.Connect(ip, port);

				NetworkStream networkStream = tcpClient.GetStream();

				if (networkStream.CanWrite && networkStream.CanRead)
				{
					networkStream.Write(sendBytes, 0, sendBytes.Length);

					string strSendData = "";
					for (int i = 0; i < sendBytes.Length; i++)
					{
						strSendData += sendBytes[i].ToString("X2") + ",";
					}
					strSendData += "\r\n";
					//theApp.PLCLogDataWrite("TX", strSendData);
					//theApp.AppendLogMsg($"Send : {strSendData}", MSG_TYPE.INFO);

					//theApp.AppendLogMsg(tcpClient.ReceiveBufferSize.ToString(), MSG_TYPE.INFO);
					networkStream.Read(bytes, 0, tcpClient.ReceiveBufferSize);

					strSendData = "";
					for (int i = 0; i < 20; i++)
					{
						strSendData += bytes[i].ToString("X2") + ",";
					}

					//theApp.AppendLogMsg($"Read : {strSendData}", MSG_TYPE.INFO);
					strSendData = "";
					for (int i = 0; i <= receiveBytes.Length - 1; i++)
					{
						receiveBytes[i] = bytes[i];
						strSendData += bytes[i].ToString("X2") + ",";
					}
					strSendData += "\r\n";
					//theApp.PLCLogDataWrite("RX", strSendData);
					bResultOk = true;
				}
				else
				{
					bResultOk = false;
				}

			}
			catch (SocketException ex1)
			{
				theApp.AppendLogMsg(ex1.Message, MSG_TYPE.INFO);
				bResultOk = false;
			}
			catch (ObjectDisposedException ex1)
			{
				theApp.AppendLogMsg(ex1.Message, MSG_TYPE.INFO);
				bResultOk = false;
			}
			catch (Exception ex1)
			{
				theApp.AppendLogMsg(ex1.Message, MSG_TYPE.INFO);
				bResultOk = false;
			}
			finally
			{
				tcpClient.Close();
			}

			return receiveBytes;
		}
	}
}