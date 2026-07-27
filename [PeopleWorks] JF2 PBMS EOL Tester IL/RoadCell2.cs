using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	class RoadCell2
	{
		public bool bPortStat = false;
		public SerialPort port = new SerialPort();
		private Stopwatch watch = new Stopwatch();
		private HIVE_Timer tMainTimer = new HIVE_Timer();


		private int nBufferIndex = 0;
		public int nCurrentStep = 0;
		public int nCurrentStepS = 0;
		private byte[] buff = new byte[1024];        // 수신버퍼 최대 사이즈
		public string strReadData = "";
		public string strReportData = "";
		public string[] strReportDatas = new string[1000];

		public bool bScanStart = false;
		public bool bScanStop = false;
		public bool bReadData = false;



		public double dResult = 0.00;

		public bool bThreadResult = false;

		public void Process()
		{
			try
			{
				if (!port.IsOpen || !_SysInfo2.bLoadCellStart) { nCurrentStep = 0; }


				switch (nCurrentStep)
				{
					case 0:
						if (_SysInfo2.bLoadCellStart)
						{
							SendData("Z");
							tMainTimer.Start(10000);
							nCurrentStep = 10;
						}
						break;

					case 10:
						if (tMainTimer.Verify())
						{
							// 시간내 데이터가 안들어오면 0번으로 이동
							_SysInfo2.bLoadCellInitOK = false;
							bPortStat = false;
							nCurrentStep = 0;
						}

						port.Read(buff, nBufferIndex++, 1);
						if (nBufferIndex == 1 && buff[0] == 0x0D || nBufferIndex == 1 && buff[0] == 0x0A)
						{
							ClearReadBuffer();
						}
						else if (buff[Math.Max((nBufferIndex - 1), 0)] == 0x0D)
						{
							strReadData = Encoding.Default.GetString(buff, 0, nBufferIndex - 1);
							//theApp.AppendDebugMsg(strReadBarcode, "Scanner #" + nIndex.ToString());

							theApp.AppendDebugMsg(strReadData, "LOAD CELL R 2");

							ClearReadBuffer();
							nCurrentStep = 50;


						}



						break;

					case 50:
						if (strReadData == "ZA")
						{
							_SysInfo2.bLoadCellInitOK = true;
							nCurrentStep = 100;
						}
						else
						{
							_SysInfo2.bLoadCellInitOK = false;
							nCurrentStep = 0;
						}
						break;

					case 100:
						strReadData = "";
						_SysInfo2.strReadLoadCellData = "";
						_SysInfo2.dbLoadCellData = 0.00;
						_SysInfo2.dbLoadCellData2 = 0.00;
						SendData("I");
						tMainTimer.Start(10000);
						nCurrentStep = 110;
						break;

					case 110:
						if (tMainTimer.Verify())
						{
							// 시간내 데이터가 안들어오면 0번으로 이동
							_SysInfo2.bLoadCellInitOK = false;
							bPortStat = false;
							nCurrentStep = 0;
						}

						port.Read(buff, nBufferIndex++, 1);

						if (nBufferIndex == 1 && buff[0] == 0x0D || nBufferIndex == 1 && buff[0] == 0x0A)
						{
							ClearReadBuffer();
						}
						else if (buff[Math.Max((nBufferIndex - 1), 0)] == 0x0D)
						{
							strReadData = Encoding.Default.GetString(buff, 0, nBufferIndex - 1);
							//theApp.AppendDebugMsg(strReadBarcode, "Scanner #" + nIndex.ToString());


							ClearReadBuffer();
							nCurrentStep = 120;

						}

						break;

					case 120:
						_SysInfo2.strReadLoadCellData = strReadData.Substring(3, 8);
						double.TryParse(_SysInfo2.strReadLoadCellData, out _SysInfo2.dbLoadCellData);
						_SysInfo2.dbLoadCellData2 = _SysInfo2.dbLoadCellData2 * 0.00981;
						nCurrentStep = 125;
						break;

					case 125:
						if (_SysInfo2.dbLoadCellData2 >= 1)
						{
							theApp.AppendDebugMsg(_SysInfo2.dbLoadCellData2.ToString(), "LOAD CELL DATA 2");
							theApp.AppendDebugMsg(_SysInfo2.dbLoadCellMaxData.ToString(), "LOAD CELL MAX 2");
						}

						if (_SysInfo2.dbLoadCellData2 > _SysInfo2.dbLoadCellMaxData)
						{
							_SysInfo2.dbLoadCellMaxData = _SysInfo2.dbLoadCellData2;

							tMainTimer.Start(200);
							nCurrentStep = 130;
						}
						else
						{
							tMainTimer.Start(200);
							nCurrentStep = 130;
						}

						break;

					case 130:
						if (!tMainTimer.Verify()) { break; }
						ClearReadBuffer();
						nCurrentStep = 100;
						break;

				}
			}
			catch (Exception e)
			{
				ClearReadBuffer();
				theApp.AppendLogMsg2("LoadCell : " + e.Message, MSG_TYPE.ERROR);
				nCurrentStep = 0;
			}
		}



		public void SetPort(String strPortName, int nBaudRate, Parity nParity, int nDataBit, StopBits nStopBit)
		{
			port.PortName = strPortName;
			port.BaudRate = nBaudRate;
			port.Parity = nParity;
			port.DataBits = nDataBit;
			port.StopBits = nStopBit;
			bPortStat = false;

			port.RtsEnable = true;
			port.DtrEnable = true;
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

		public void CloseComm()
		{
			port.Close();
		}



		private void ClearReadBuffer()
		{
			Array.Clear(buff, 0, buff.Length);
			nBufferIndex = 0;
		}

		public void SendData(string strData)
		{
			try
			{
				port.Write(strData);
				port.Write(new byte[] { 0x0D }, 0, 1);
				port.Write(new byte[] { 0x0A }, 0, 1);
				//theApp.AppendDebugMsg(strData, "LOAD CELL S");
			}
			catch { }
		}

		public void ClearBuffer()
		{
			while (port.BytesToRead > 0)
			{
				port.ReadByte();
			}
		}
	}
}
