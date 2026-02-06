using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	public class HoneyWell
	{
		public string strReadBarcode;
		public bool bReadOk = false;

		// 시리얼 포트
		public SerialPort _Port = new SerialPort();
		private Stopwatch _tTimer = new Stopwatch();


		private byte[] byStx = new byte[] { 0x16 };
		private byte[] byEtx = new byte[] { 0x0D };

		// 쓰레드 플래그
		private bool bWorkStop = false;

		// 수신 데이터
		private int Len = 0;
		private byte[] rcvBuff = new byte[1024];


		public void Process()
		{
			// while (!bWorkStop)
			if (!_Port.IsOpen || _Port == null) { return; }
			if (!isPortAlive(_Port.PortName)) { return; }

			{
				try
				{
					if (_Port.BytesToRead > 0)
					{
						if (Len > 1000) { ClearReadBuffer(); }

						_Port.Read(rcvBuff, Len++, 1);

						if (rcvBuff[Math.Max((Len - 1), 0)] == 0x0D)
						{
							strReadBarcode = Encoding.Default.GetString(rcvBuff, 0, Len - 1);
							theApp.AppendLogMsg("Read Barcode : " + strReadBarcode, MSG_TYPE.INFO);
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


		public void SetPort(String strPortName, int nBaudRate, Parity nParity, int nDataBit, StopBits nStopBit)
		{
			_Port.PortName = strPortName;
			_Port.BaudRate = nBaudRate;
			_Port.Parity = nParity;
			_Port.DataBits = nDataBit;
			_Port.StopBits = nStopBit;
			_Port.RtsEnable = true;
			_Port.DtrEnable = true;

		}


		public void TriggerOn()
		{
			try
			{
				_Port.Write(byStx, 0, byStx.Length);
				_Port.Write("T");
				_Port.Write(byEtx, 0, byEtx.Length);
			}
			catch { }
		}

		public void TriggerOff()
		{
			try
			{
				_Port.Write(byStx, 0, byStx.Length);
				_Port.Write("U");
				_Port.Write(byEtx, 0, byEtx.Length);
			}
			catch { }
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

		public void Start()
		{
			bWorkStop = false;
			//Thread Proc = new Thread(new ThreadStart(Process));
			//Proc.Start();
		}

		public void Stop()
		{
			bWorkStop = true;
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

		public bool isPortAlive(string str)
		{
			try
			{
				String[] m1 = SerialPort.GetPortNames();
				if (m1 == null)
				{
					return false;
				}
				else if (m1.Length > 0)
				{
					for (int i = 0; i < m1.Length; i++)
					{
						if (m1[i] == str)
						{
							return true;
						}
					}
					return false;
				}
				else
				{
					return false;
				}

			}
			catch (Exception e)
			{
				theApp.AppendLogMsg($"{e.Message}", MSG_TYPE.ERROR);
				return false;
			}
		}
	}
}
