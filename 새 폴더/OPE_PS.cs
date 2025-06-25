using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	public class OPE_PS
	{

		private string strUnitName = "";


		private SerialPort port = new SerialPort();
		private string strReadData = "";
		public string strTrimData = "";
		private byte[] ReadBuff;
		private List<byte> lstReadData = new List<byte>();

		private bool bReadOk = false;

		public void Process()
		{
			// 포트가 열려있지 않은 경우 리턴한다
			if (!port.IsOpen) { return; }

			try
			{

				int nByteReadSize = port.BytesToRead;

				if (nByteReadSize > 0)
				{
					ReadBuff = new byte[nByteReadSize];
					port.Read(ReadBuff, 0, nByteReadSize);

					lstReadData.AddRange(ReadBuff);

					// 정상적인 STX가 들어왔다면 데이터 유효성 검사를 시작한다.
					if (lstReadData[lstReadData.Count - 1] == 0x0A)
					{
						strReadData = Encoding.Default.GetString(lstReadData.ToArray());
						strTrimData = strReadData.Trim(new Char[] { ' ', '\r', '\n' });
						theApp.AppendDebugMsg(strTrimData, strUnitName);
						//theApp.AppendLogMsg(strTrimData, MSG_TYPE.LOG);
						bReadOk = true;
					}

				}

			}
			catch { }

		}

		public void SendData(string strSendData)
		{
			try
			{
				bReadOk = false;
				lstReadData.Clear();        //  Send시 마다 버퍼 클리어
				port.Write(strSendData);
				port.Write(new byte[] { 0x0A }, 0, 1);
				theApp.AppendDebugMsg(strSendData, "PC > " + strUnitName);

			}
			catch { }
		}

		public void Send(string strSendData)
		{
			try
			{
				bReadOk = false;
				lstReadData.Clear();        //  Send시 마다 버퍼 클리어
				port.Write(strSendData);
				port.Write(new byte[] { 0x0D }, 0, 1);
				port.Write(new byte[] { 0x0A }, 0, 1);
				theApp.AppendDebugMsg(strSendData, "PC > " + strUnitName);
				//theApp.AppendLogMsg(strSendData, MSG_TYPE.LOG);
			}
			catch { }
		}

		public void SetPort(String strPortName, int nBaudRate, Parity nParity, int nDataBit, StopBits nStopBit, string unitName)
		{
			port.PortName = strPortName;
			port.BaudRate = nBaudRate;
			port.Parity = nParity;
			port.DataBits = nDataBit;
			port.StopBits = nStopBit;

			strUnitName = unitName;
		}


		public bool PortOpen()
		{
			if (port.IsOpen)
			{
				try
				{
					port.Close();
					port.Open();
					return true;
				}
				catch { return false; }
			}
			else
			{
				try
				{
					port.Open();
					return true;
				}
				catch { return false; }
			}

		}


		public string GetReadData()
		{
			return strTrimData;
		}

		public bool IsReadData()
		{
			return bReadOk;
		}



		public void CloseComm()
		{
			try
			{
				port.Close();
			}
			catch { }
		}

		public bool PortIsAlive()
		{
			try
			{
				return port.IsOpen;
			}
			catch { return false; }
		}




	}
}
