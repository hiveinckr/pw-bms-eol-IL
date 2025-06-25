using Peak.Can.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peak.Can.Basic;
using TPCANHandle = System.UInt16;
using TPCANBitrateFD = System.String;
using TPCANTimestampFD = System.UInt64;

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	public class PcanComm
	{
		private TPCANHandle m_PcanHandle;
		private TPCANBaudrate m_Baudrate;
		private TPCANType m_HwType;
		public TPCANMsg m_ReadMsg;
		public byte[] btReadData = new byte[8];
		public bool bReadMessage = false;
		private byte[] btSendData = new byte[8];
		private int nReadCount = 0;
		private int nCanCh = 0;
		//private myCanData _ReadCanData = new myCanData();
		// Can 통신 데이터
		public List<myCanData> lstData = new List<myCanData>();
		public List<myModbusCanData> lstModbusData = new List<myModbusCanData>();

		public TPCANStatus ReadMessage()
		{


			TPCANTimestamp CANTimeStamp;
			TPCANStatus stsResult;

			// We execute the "Read" function of the PCANBasic                
			//
			//m_ReadMsg.MSGTYPE = TPCANMessageType.PCAN_MESSAGE_EXTENDED;

			stsResult = PCANBasic.Read(m_PcanHandle, out m_ReadMsg, out CANTimeStamp);

			if (stsResult != TPCANStatus.PCAN_ERROR_QRCVEMPTY)
			{
				if (m_ReadMsg.ID != 0 && m_ReadMsg.ID != 1)
				{

					for (int i = 0; i < 8; i++)
					{
						if (i >= m_ReadMsg.LEN)
						{
							btReadData[i] = 0x00;
						}
						else
						{
							btReadData[i] = m_ReadMsg.DATA[i];
						}
					}

					bReadMessage = true;
					if(nCanCh <= 3)
					{
						theApp.CanLogDataWrite(new myCanData()
						{
							_tTime = DateTime.Now,
							nCh = nCanCh,
							uID = m_ReadMsg.ID,
							nLen = m_ReadMsg.LEN,
							btData1 = btReadData[0],
							btData2 = btReadData[1],
							btData3 = btReadData[2],
							btData4 = btReadData[3],
							btData5 = btReadData[4],
							btData6 = btReadData[5],
							btData7 = btReadData[6],
							btData8 = btReadData[7],
							strType = "RX"
						});
					}
					else
					{
						theApp.CanLogDataWrite2(new myCanData2()
						{
							_tTime = DateTime.Now,
							nCh = nCanCh,
							uID = m_ReadMsg.ID,
							nLen = m_ReadMsg.LEN,
							btData1 = btReadData[0],
							btData2 = btReadData[1],
							btData3 = btReadData[2],
							btData4 = btReadData[3],
							btData5 = btReadData[4],
							btData6 = btReadData[5],
							btData7 = btReadData[6],
							btData8 = btReadData[7],
							strType = "RX"
						});
					}
					
				}

			}



			return stsResult;
		}


		public void ClearBuffer()
		{
			m_ReadMsg = new TPCANMsg();
		}

		public TPCANStatus CanInit(TPCANBaudrate baudrate, TPCANType type, string strCh)
		{
			TPCANStatus bResult;

			m_Baudrate = baudrate;
			m_HwType = type;


			// Sets the connection status of the main-form
			//
			m_PcanHandle = Convert.ToUInt16(strCh, 16);

			bResult = PCANBasic.Initialize(m_PcanHandle, m_Baudrate, m_HwType, Convert.ToUInt32("0100", 16), Convert.ToUInt16("3"));

			return bResult;
		}

		public void CanaDataInit(int nCh)
		{
			nCanCh = nCh;
			for (int i = 0; i < 1048575; i++)
			{
				lstData.Add(new myCanData());
			}

			for (int i = 0; i < 1048575; i++)
			{
				lstModbusData.Add(new myModbusCanData());
			}
		}





		public void CloseCan()
		{
			PCANBasic.Uninitialize(m_PcanHandle);
		}


		// 8 바이트 전송 
		public TPCANStatus WriteFrame(uint nID, byte[] btData)
		{

			TPCANMsg CANMsg;

			CANMsg = new TPCANMsg();
			CANMsg.DATA = new byte[8];

			CANMsg.ID = nID;
			CANMsg.LEN = 8;
			CANMsg.MSGTYPE = TPCANMessageType.PCAN_MESSAGE_STANDARD;

			for (int i = 0; i < 8; i++)
			{
				CANMsg.DATA[i] = btData[i];
			}


			//MyProgram.CanLogDataWrite(new myCanData()
			//{

			//    _tTime = DateTime.Now,
			//    nID = CANMsg.ID,
			//    btData1 = CANMsg.DATA[0],
			//    btData2 = CANMsg.DATA[1],
			//    btData3 = CANMsg.DATA[2],
			//    btData4 = CANMsg.DATA[3],
			//    btData5 = CANMsg.DATA[4],
			//    btData6 = CANMsg.DATA[5],
			//    btData7 = CANMsg.DATA[6],
			//    btData8 = CANMsg.DATA[7],
			//    strType = "TX"

			//});

			return PCANBasic.Write(m_PcanHandle, ref CANMsg);
		}


		// 8 바이트 전송 
		public TPCANStatus WriteFrame(uint nID, byte[] btData, int nLen, TPCANMessageType msgtype)
		{

			TPCANMsg CANMsg;

			CANMsg = new TPCANMsg();
			CANMsg.DATA = new byte[8];

			CANMsg.ID = nID;
			CANMsg.LEN = (byte)nLen;
			CANMsg.MSGTYPE = msgtype;

			for (int i = 0; i < nLen; i++)
			{
				CANMsg.DATA[i] = btData[i];
			}

			for (int i = 0; i < 8; i++)
			{
				if (i >= nLen)
				{
					btSendData[i] = 0x00;
				}
				else
				{
					btSendData[i] = CANMsg.DATA[i];
				}
			}

			theApp.CanLogDataWrite(new myCanData()
			{

				_tTime = DateTime.Now,
				uID = CANMsg.ID,
				nLen = nLen,
				nCh = nCanCh,
				btData1 = btSendData[0],
				btData2 = btSendData[1],
				btData3 = btSendData[2],
				btData4 = btSendData[3],
				btData5 = btSendData[4],
				btData6 = btSendData[5],
				btData7 = btSendData[6],
				btData8 = btSendData[7],
				strType = "TX"

			});

			return PCANBasic.Write(m_PcanHandle, ref CANMsg);
		}
	}
}

