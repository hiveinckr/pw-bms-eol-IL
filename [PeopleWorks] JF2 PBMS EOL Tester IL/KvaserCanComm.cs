using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using canlibCLSNET;
using Kvaser.Kvadblib;
using Peak.Can.Basic;

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	public class KvaserCanComm
	{
		private int handle = -1;

		public List<myCanData> mylstData = new List<myCanData>();
		public Dictionary<string, CanData> myReadData = new Dictionary<string, CanData>();
		public int nReadid;
		public byte[] btReadData = new byte[8];
		public bool bReadMessage = false;
		public int nReaddlc;
		int nReadflags;
		long lReadtime;
		int nWriteflags;
		public int nStation;


		public void ReadMessage()
		{
			if (handle >= 0)
			{
				if (Canlib.canRead(handle, out nReadid, btReadData, out nReaddlc, out nReadflags, out lReadtime) == Canlib.canStatus.canOK)
				{
					//theApp.AppendLogMsg($"CAN DATA READ 1 {nReadid.ToString("X")}", MSG_TYPE.INFO);

					if ((nReadflags & Canlib.canMSG_ERROR_FRAME) != Canlib.canMSG_ERROR_FRAME)
					{
						if (myReadData.ContainsKey(nReadid.ToString("X")))
						{
							myReadData[nReadid.ToString("X")].bData = btReadData;
							myReadData[nReadid.ToString("X")].bReadOk = true;
						}
						else
						{
							myReadData.Add(nReadid.ToString("X"), new CanData() { bReadOk = true, bData = btReadData });
						}
						//theApp.AppendLogMsg($"CAN DATA READ 3 {myReadData[nReadid.ToString("X")].bData[0].ToString("X")}", MSG_TYPE.INFO);
						bReadMessage = true;
						//theApp.lstData[nReadid] = new myCanData()
						//{
						//	_tTime = DateTime.Now,
						//	nID = nReadid,
						//	btData1 = btReadData[0],
						//	btData2 = btReadData[1],
						//	btData3 = btReadData[2],
						//	btData4 = btReadData[3],
						//	btData5 = btReadData[4],
						//	btData6 = btReadData[5],
						//	btData7 = btReadData[6],
						//	btData8 = btReadData[7],
						//	bNewData = true,
						//	strType = "RX"
						//};

						//mylstData[nReadid] = new myCanData()
						//{
						//	_tTime = DateTime.Now,
						//	nID = nReadid,
						//	btData1 = btReadData[0],
						//	btData2 = btReadData[1],
						//	btData3 = btReadData[2],
						//	btData4 = btReadData[3],
						//	btData5 = btReadData[4],
						//	btData6 = btReadData[5],
						//	btData7 = btReadData[6],
						//	btData8 = btReadData[7],
						//	bNewData = true,
						//	strType = "RX"
						//};
						byte[] btSendData2 = new byte[8];

						for (int i = 0; i < btReadData.Length; i++)
						{
							btSendData2[i] = btReadData[i];
						}



						if(_SysInfo.nTXCh <= 3)
						{
							theApp.CanLogDataWrite(new myCanData()
							{
								_tTime = DateTime.Now,
								nCh = _SysInfo.nTXCh,
								nID = nReadid,
								nLen = btReadData.Length,
								btData1 = btSendData2[0],
								btData2 = btSendData2[1],
								btData3 = btSendData2[2],
								btData4 = btSendData2[3],
								btData5 = btSendData2[4],
								btData6 = btSendData2[5],
								btData7 = btSendData2[6],
								btData8 = btSendData2[7],
								strType = "RX",
							});
						}
						else
						{

							theApp.CanLogDataWrite2(new myCanData2()
							{
								_tTime = DateTime.Now,
								nCh = _SysInfo.nTXCh,
								nID = nReadid,
								nLen = btReadData.Length,
								btData1 = btSendData2[0],
								btData2 = btSendData2[1],
								btData3 = btSendData2[2],
								btData4 = btSendData2[3],
								btData5 = btSendData2[4],
								btData6 = btSendData2[5],
								btData7 = btSendData2[6],
								btData8 = btSendData2[7],
								strType = "RX",
							});
						}
						





					}
				}
			}
		}


		// Can 초기화 Ch만 정하고 BITRATE 는 500K로 고정 Bus On도 동시에 함
		public bool CanInit(int nCh)
		{
			Canlib.canInitializeLibrary();

			mylstData.Clear();
			for (int i = 0; i < 0x50000; i++)
			{
				mylstData.Add(new myCanData());
			}

			int hnd = Canlib.canOpenChannel(nCh, Canlib.canOPEN_REQUIRE_INIT_ACCESS);

			if (hnd < 0)
			{
				theApp.AppendLogMsg($"{hnd}", MSG_TYPE.INFO);
				return false;
			}
			else
			{
				if (hnd >= 0)
				{
					handle = hnd;
				}
			}


			if (Canlib.canSetBusParams(handle, Canlib.canBITRATE_500K, 0, 0, 0, 0, 0) != Canlib.canStatus.canOK)
			{
				//theApp.AppendLogMsg($"Error ch{handle} / {(Canlib.canStatus)hnd}", MSG_TYPE.INFO);

				return false;
			}

			if (Canlib.canBusOn(handle) != Canlib.canStatus.canOK)
			{
				return false;
			}

			theApp.AppendLogMsg($"handle : {handle}", MSG_TYPE.INFO);
			return true;
		}


		// Can Close
		public bool CanClose()
		{
			if (Canlib.canBusOff(handle) != Canlib.canStatus.canOK)
			{
				return false;
			}

			if (Canlib.canClose(handle) != Canlib.canStatus.canOK)
			{
				return false;
			}
			return true;
		}


		public void WriteFrame(int nCh, int nId, byte[] btSendData, int nStation)
		{
			//theApp.AppendLogMsg(nId.ToString("X") + " / " +)
			nWriteflags = 0;
			Canlib.canWrite(handle, nId, btSendData, btSendData.Length, Canlib.canMSG_EXT);
			_SysInfo.bEolReadData = true;
			_SysInfo.nTXCh = nCh;
			byte[] btSendData2 = new byte[8];
			for (int i = 0; i < btSendData.Length; i++)
			{
				btSendData2[i] = btSendData[i];
			}
			
			if(nStation == 1)
			{
				theApp.CanLogDataWrite(new myCanData()
				{

					_tTime = DateTime.Now,
					nCh = nCh,
					nID = nId,
					nLen = btSendData.Length,
					btData1 = btSendData2[0],
					btData2 = btSendData2[1],
					btData3 = btSendData2[2],
					btData4 = btSendData2[3],
					btData5 = btSendData2[4],
					btData6 = btSendData2[5],
					btData7 = btSendData2[6],
					btData8 = btSendData2[7],
					strType = "TX"

				});
			}
			else
			{
				theApp.CanLogDataWrite2(new myCanData2()
				{

					_tTime = DateTime.Now,
					nCh = nCh,
					nID = nId,
					nLen = btSendData.Length,
					btData1 = btSendData2[0],
					btData2 = btSendData2[1],
					btData3 = btSendData2[2],
					btData4 = btSendData2[3],
					btData5 = btSendData2[4],
					btData6 = btSendData2[5],
					btData7 = btSendData2[6],
					btData8 = btSendData2[7],
					strType = "TX"

				});
			}
			

			//if (_Config.bUseCanLogShow)
			//{
			//	theApp.CanLogDataWrite(new myCanData()
			//	{
			//		_tTime = DateTime.Now,
			//		nID = nId,
			//		btData1 = btSendData[0],
			//		btData2 = btSendData[1],
			//		btData3 = btSendData[2],
			//		btData4 = btSendData[3],
			//		btData5 = btSendData[4],
			//		btData6 = btSendData[5],
			//		btData7 = btSendData[6],
			//		btData8 = btSendData[7],
			//		strType = "TX"
			//	});
			//}

		}
	}
}



