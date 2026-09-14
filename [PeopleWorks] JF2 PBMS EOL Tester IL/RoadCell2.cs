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

		private string strReadTemp = "";
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
		public bool bIsconnected = false;



		public double dResult = 0.00;

		public bool bThreadResult = false;

		//public void Process()
		//{
		//	try
		//	{
		//		if (!port.IsOpen || !_SysInfo2.bLoadCellStart) { nCurrentStep = 0; }


		//		switch (nCurrentStep)
		//		{
		//			case 0:
		//				if (_SysInfo2.bLoadCellStart)
		//				{
		//					SendData("Z");
		//					tMainTimer.Start(10000);
		//					nCurrentStep = 10;
		//				}
		//				break;

		//			case 10:
		//				if (tMainTimer.Verify())
		//				{
		//					// 시간내 데이터가 안들어오면 0번으로 이동
		//					_SysInfo2.bLoadCellInitOK = false;
		//					bPortStat = false;
		//					nCurrentStep = 0;
		//				}

		//				port.Read(buff, nBufferIndex++, 1);

		//				theApp.AppendLogMsg2($"LoadCell2 : {port.Read(buff, nBufferIndex++, 1)}" , MSG_TYPE.INFO);

		//				if (nBufferIndex == 1 && buff[0] == 0x0D || nBufferIndex == 1 && buff[0] == 0x0A)
		//				{
		//					ClearReadBuffer();
		//				}
		//				else if (buff[Math.Max((nBufferIndex - 1), 0)] == 0x0D)
		//				{
		//					strReadData = Encoding.Default.GetString(buff, 0, nBufferIndex - 1);
		//					//theApp.AppendDebugMsg(strReadBarcode, "Scanner #" + nIndex.ToString());

		//					theApp.AppendDebugMsg(strReadData, "LOAD CELL R 2");

		//					ClearReadBuffer();
		//					nCurrentStep = 50;


		//				}



		//				break;

		//			case 50:
		//				if (strReadData == "ZA")
		//				{
		//					_SysInfo2.bLoadCellInitOK = true;
		//					nCurrentStep = 100;
		//				}
		//				else
		//				{
		//					_SysInfo2.bLoadCellInitOK = false;
		//					nCurrentStep = 0;
		//				}
		//				break;

		//			case 100:
		//				strReadData = "";
		//				_SysInfo2.strReadLoadCellData = "";
		//				_SysInfo2.dbLoadCellData = 0.00;
		//				_SysInfo2.dbLoadCellData2 = 0.00;
		//				SendData("I");
		//				tMainTimer.Start(10000);
		//				nCurrentStep = 110;
		//				break;

		//			case 110:
		//				if (tMainTimer.Verify())
		//				{
		//					// 시간내 데이터가 안들어오면 0번으로 이동
		//					_SysInfo2.bLoadCellInitOK = false;
		//					bPortStat = false;
		//					nCurrentStep = 0;
		//				}

		//				port.Read(buff, nBufferIndex++, 1);

		//				if (nBufferIndex == 1 && buff[0] == 0x0D || nBufferIndex == 1 && buff[0] == 0x0A)
		//				{
		//					ClearReadBuffer();
		//				}
		//				else if (buff[Math.Max((nBufferIndex - 1), 0)] == 0x0D)
		//				{
		//					strReadData = Encoding.Default.GetString(buff, 0, nBufferIndex - 1);
		//					//theApp.AppendDebugMsg(strReadBarcode, "Scanner #" + nIndex.ToString());


		//					ClearReadBuffer();
		//					nCurrentStep = 120;

		//				}

		//				break;

		//			case 120:
		//				_SysInfo2.strReadLoadCellData = strReadData.Substring(3, 8);
		//				double.TryParse(_SysInfo2.strReadLoadCellData, out _SysInfo2.dbLoadCellData);
		//				_SysInfo2.dbLoadCellData2 = _SysInfo2.dbLoadCellData2 * 0.00981;
		//				nCurrentStep = 125;
		//				break;

		//			case 125:
		//				if (_SysInfo2.dbLoadCellData2 >= 1)
		//				{
		//					theApp.AppendDebugMsg(_SysInfo2.dbLoadCellData2.ToString(), "LOAD CELL DATA 2");
		//					theApp.AppendDebugMsg(_SysInfo2.dbLoadCellMaxData.ToString(), "LOAD CELL MAX 2");
		//				}

		//				if (_SysInfo2.dbLoadCellData2 > _SysInfo2.dbLoadCellMaxData)
		//				{
		//					_SysInfo2.dbLoadCellMaxData = _SysInfo2.dbLoadCellData2;

		//					tMainTimer.Start(200);
		//					nCurrentStep = 130;
		//				}
		//				else
		//				{
		//					tMainTimer.Start(200);
		//					nCurrentStep = 130;
		//				}

		//				break;

		//			case 130:
		//				if (!tMainTimer.Verify()) { break; }
		//				ClearReadBuffer();
		//				nCurrentStep = 100;
		//				break;

		//		}
		//	}
		//	catch (Exception e)
		//	{
		//		ClearReadBuffer();
		//		theApp.AppendLogMsg2("LoadCell : " + e.Message, MSG_TYPE.ERROR);
		//		nCurrentStep = 0;
		//	}
		//}



		public async void SetPort(String strPortName, int nBaudRate, Parity nParity, int nDataBit, StopBits nStopBit)
		{
			port.PortName = strPortName;
			port.BaudRate = nBaudRate;
			port.Parity = nParity;
			port.DataBits = nDataBit;
			port.StopBits = nStopBit;
			port.RtsEnable = true;
			port.DtrEnable = true;

			await Task.Run(() =>
			{
				if (PortOpen())
				{
					// 성공시 메인 로그를 띄우고 바코드 값을 읽는다.{strPortName}//46
					theApp.AppendLogMsg(String.Format("<COM{0}> LoadCell #2 Port Open Success", _Config.nLoadCellPort2), MSG_TYPE.INFO);
					bIsconnected = true;
					port.DataReceived += _Port_DataReceived;
				}
				else
				{
					// 실패시 메인 로그를 띄운다.[QTE002] //48
					theApp.AppendLogMsg(String.Format("<COM{0}> LoadCell #2 Port Open Fail", _Config.nLoadCellPort2), MSG_TYPE.ERROR);
					bIsconnected = false;
				}
			});
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
				theApp.AppendDebugMsg(strData, "LOAD CELL #2 S");
			}
			catch {/* theApp.AppendLogMsg2("로드셀 통신 에러",MSG_TYPE.LOG);*/ }
		}

		private void _Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			try
			{
				//SerialPort recvData = (SerialPort)sender;
				//strReadTemp += recvData.ReadExisting();         // 데이터 전부 가져오기 0D 0A 

				SerialPort recvData = (SerialPort)sender;
				strReadTemp += recvData.ReadExisting();         // 데이터 전부 가져오기 0D 0A 


				if (strReadTemp.Length > 1 && strReadTemp[strReadTemp.Length - 1] == 0x0A) // 0부터 시작이므로 데이터 길이에서 1을 빼야된다
				{
					bReadData = true;
					strReadData = strReadTemp.Replace("\r", "").Replace("\n", "");
					strReadTemp = "";

					theApp.AppendDebugMsg(strReadData, "LOAD CELL #2 R");

					//heApp.LeakCommandLog("READ 1", strReadData);
					//LeakCheck(strReadData);
				}
				else if (strReadTemp.Length > 1 && strReadTemp[strReadTemp.Length - 1] == 0x0D)
				{
					bReadData = true;
					strReadData = strReadTemp.Replace("\r", "").Replace("\n", "");
					strReadTemp = "";
					theApp.AppendDebugMsg(strReadData, "LOAD CELL #2 R");

					//theApp.LeakCommandLog("READ 2", strReadData);
					//LeakCheck(strReadData);
				}


				//}
			}
			catch (Exception _e)
			{

				//theApp.AppendDebugMsg($"I28 Receive Error : {_e.Message}", "I28");
			}
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
