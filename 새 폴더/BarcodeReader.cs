using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	public class BarcodeReader
	{
		public string strReadBarcode;
		public bool bReadOk = false;

		private string strUnitName = "";

		// 시리얼 포트
		private SerialPort _Port = new SerialPort();
		private Stopwatch _tTimer = new Stopwatch();


		// 쓰레드 플래그
		private bool bWorkStop = false;

		// 수신 데이터
		private int Len = 0;

		private byte[] rcvBuff = new byte[1024];

		public int nStation = 0;


		public void Process()
		{
			// while (!bWorkStop)
			if (!_Port.IsOpen) { return; }

			{
				try
				{
					while (_Port.BytesToRead > 0)
					{
						if (Len > 1000) { ClearReadBuffer(); }

						_Port.Read(rcvBuff, Len++, 1);

						if (rcvBuff[Math.Max((Len - 1), 0)] == 0x0D)
						{
							strReadBarcode = Encoding.Default.GetString(rcvBuff, 0, Len - 1);
							//theApp.AppendDebugMsg(strReadBarcode, "Scanner #" + nIndex.ToString());

							if(nStation == 1)
							{
								theApp.AppendLogMsg(strReadBarcode, MSG_TYPE.LOG);
							}
							else if(nStation == 2)
							{
								theApp.AppendLogMsg2(strReadBarcode, MSG_TYPE.LOG);
							}
							

							//_SysInfo.strReadBarcode = strReadBarcode;
							bReadOk = true;
							ClearReadBuffer();
							_Port.ReadExisting();
						}

					}
				}
				catch
				{
					ClearReadBuffer();
				}
				//Thread.Sleep(1);
			}
		}


		public void SetPort(String strPortName, int nBaudRate, Parity nParity, int nDataBit, StopBits nStopBit, string UnitName)
		{
			_Port.PortName = strPortName;
			_Port.BaudRate = nBaudRate;
			_Port.Parity = nParity;
			_Port.DataBits = nDataBit;
			_Port.StopBits = nStopBit;
			_Port.RtsEnable = true;
			_Port.DtrEnable = true;

			strUnitName = UnitName;
		}

		// Port Open
		public bool PortOpen()
		{
			try
			{
				_Port.Open();
				return true;
			}
			catch
			{
				return false;
			}

		}

		public void CloseComm()
		{
			_Port.Close();
		}

		public bool IsOpened()
		{
			return _Port.IsOpen;
		}


		// Read 버퍼 클리어
		private void ClearReadBuffer()
		{
			Array.Clear(rcvBuff, 0, rcvBuff.Length);
			Len = 0;
		}
	}
}

