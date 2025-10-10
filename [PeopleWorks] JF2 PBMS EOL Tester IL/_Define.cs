using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	public class _Define
	{

		public static DataTable[] DITable = new DataTable[3];
		public static DataTable[] DOTable = new DataTable[3];


		public static void CreateDIOInfo()
		{
			for (int i = 0; i < 3; i++)
			{
				DITable[i] = new DataTable();
				DOTable[i] = new DataTable();

				DITable[i].Columns.Add("Cate", typeof(string));
				DITable[i].Columns.Add("Comment", typeof(string));

				DOTable[i].Columns.Add("Cate", typeof(string));
				DOTable[i].Columns.Add("Comment", typeof(string));
			}


			for (int i = 0; i < 1024; i++)
			{
				DITable[0].Rows.Add("-", "-");
				DITable[1].Rows.Add("-", "-");
				DITable[2].Rows.Add("-", "-");

				DOTable[0].Rows.Add("-", "-");
				DOTable[1].Rows.Add("-", "-");
				DOTable[2].Rows.Add("-", "-");
			}

			// 입력
			IONaming(DI.START_SW1, "[ST #1]", "작업시작 SW1");
			IONaming(DI.START_SW2, "[ST #1]", "작업시작 SW2");

			IONaming(DI.START_SW3, "[ST #2]", "작업시작 SW1");
			IONaming(DI.START_SW4, "[ST #2]", "작업시작 SW2");



			for (int i = 0; i < 56; i++)
			{
				IONaming(DO.RY_01 + i, "[메인공통]", $"Relay 채널 #{i + 1}");
			}

			IONaming(DO.SW_LAMP1, "[ST #1]", "작업시작 SW1");
			IONaming(DO.SW_LAMP2, "[ST #1]", "작업시작 SW2");
			IONaming(DO.SW_LAMP3, "[ST #2]", "작업시작 SW1");
			IONaming(DO.SW_LAMP4, "[ST #2]", "작업시작 SW2");
			IONaming(DO.TOWER_LAMP_GREEN, "[타워램프]", "타워램프 - 녹색");
			IONaming(DO.TOWER_LAMP_YELLOW, "[타워램프]", "타워램프 - 황색");
			IONaming(DO.TOWER_LAMP_RED, "[타워램프]", "타워램프 - 적색");
			IONaming(DO.TOWER_LAMP_BUZZER, "[타워램프]", "타워램프 - 부저");
		}


		// DI 네임할당
		public static void IONaming(DI nIndex, string str1, string str2)
		{
			DITable[0].Rows[(int)nIndex]["Cate"] = str1;
			DITable[0].Rows[(int)nIndex]["Comment"] = str2;
		}

		// DO 네임할당
		public static void IONaming(DO nIndex, string str1, string str2)
		{
			DOTable[0].Rows[(int)nIndex]["Cate"] = str1;
			DOTable[0].Rows[(int)nIndex]["Comment"] = str2;
		}
	}


	public enum DI
	{

		START_SW1 = 0x00,
		START_SW2 = 0x01,
		START_SW3 = 0x02,
		START_SW4 = 0x03,

	}

	public enum DO
	{

		//BBMS_CB = 0x00,
		//BBMS_FB1 = 0x01,
		//BBMS_FB2 = 0x02,
		//BBMS_FB3 = 0x03,
		//RBMS_FUSE = 0x04,
		//RBMS_MC1 = 0x05,
		//RBMS_MC2 = 0x06,
		//RBMS_CB = 0x07,
		//RBMS_DS = 0x08,
		//RBMS_FB1 = 0x09,
		//RBMS_FB2 = 0x0A,
		//RBMS_FB3 = 0x0B,
		//RBMS_HARD_WIRE_IN = 0x0C,
		//RBMS_BI_WAKE_IN_X2 = 0x0D,
		//RBMS_BI_WAKE_IN_X3 = 0x0E,
		//BBMS_HARD_WIRE_IN = 0x0F,
		//BBMS_BI_WAKE_IN_X2 = 0x10,
		//BBMS_BI_WAKE_IN_X3 = 0x11,
		//MBMS_WAKE = 0x12,

		RY_01 = 0x00,

		SW_LAMP1 = 0x38,
		SW_LAMP2 = 0x39,
		SW_LAMP3 = 0x3A,
		SW_LAMP4 = 0x3B,
		TOWER_LAMP_BUZZER = 0x3C,
		TOWER_LAMP_GREEN = 0x3D,
		TOWER_LAMP_YELLOW = 0x3E,
		TOWER_LAMP_RED = 0x03F,
	

		
	}
	public class MODEL_INFO
	{
		// 모델 명
		public string strModelName;
		public string strComment1;
		public string strComment2;
		public string strComment3;
		public string strComment4;


		public ObservableCollection<MODEL_SET> _TestInfo = new ObservableCollection<MODEL_SET>();
		

		public string strBarcodSymbol = "";
		public string strFuseBarcodSymbol = "";
		public string strCaseBarcodSymbol = "";
		public string strSBBarcodSymbol = "";
		public string strRBMSBarcodSymbol = "";
		public int nSerailNumIndex = 0;
		public bool bUseRMDTestMode = false;
		public bool bUseQRCode = false;
		public bool bUseRbmsTest = false;


		public string strMasterOkSampleBarcode = "";
		public string strMasterNgSampleBarcode = "";
		public double dbResistance;
		public int nDmmScanCount;
		public double dbDmmScanSpeed;
		public double dbDmmMScanSpeed;
		public double dbDmmAScanSpeed;
		public double dbDmmScanInterval;
		public double dbDmmAScanInterval;

		public int nBCDsize;
		public int nBCDStringHeight;
		public int nBCDStringWidth;
		public int nBCDbcdOffsetX;
		public int nBCDbcdOffsetY;
		public int nBCDtextOffsetX;
		public int nBCDtextOffsetY;
		public int nBCDPrintCount;


	}

	public class MODEL_INFO2
	{
		// 모델 명
		public string strModelName;
		public string strComment1;
		public string strComment2;
		public string strComment3;
		public string strComment4;


		public ObservableCollection<MODEL_SET2> _TestInfo = new ObservableCollection<MODEL_SET2>();



		public string strBarcodSymbol = "";
		public string strFuseBarcodSymbol = "";
		public string strCaseBarcodSymbol = "";
		public string strSBBarcodSymbol = "";
		public string strRBMSBarcodSymbol = "";
		public int nSerailNumIndex = 0;
		public bool bUseRMDTestMode = false;
		public bool bUseQRCode = false;
		public bool bUseRbmsTest = false;


		public string strMasterOkSampleBarcode = "";
		public string strMasterNgSampleBarcode = "";
		public double dbResistance;
		public int nDmmScanCount;
		public double dbDmmScanSpeed;
		public double dbDmmMScanSpeed;
		public double dbDmmAScanSpeed;
		public double dbDmmScanInterval;
		public double dbDmmAScanInterval;


		//바코드 프린터기 설정
		public int nBCDsize;
		public int nBCDStringHeight;
		public int nBCDStringWidth;
		public int nBCDbcdOffsetX;
		public int nBCDbcdOffsetY;
		public int nBCDtextOffsetX;
		public int nBCDtextOffsetY;
		public int nBCDPrintCount;


	}

	[Serializable]
	public class MASTER_INFO_CHECK
	{
		public bool bMasterOkSampleTestFinish = false;
		public bool bMasterNgSampleTestFinish = false;

		public DateTime dtMasterOkSampleTestTime = new DateTime();
		public DateTime dtMasterNgSampleTestTime = new DateTime();

	}





	public class MODEL_SET
	{
		public string strTestName { get; set; }
		public bool bUseItem { get; set; }
		public bool bDisplayItem { get; set; }
		public int nTestItem { get; set; }

		public ObservableCollection<DATA_SET> _DataInfo = new ObservableCollection<DATA_SET>();

		public string strSpecMin { get; set; }
		public string strSpecMax { get; set; }
		public string strUnit { get; set; }

		public string strValue1 { get; set; }
		public string strValue2 { get; set; }
		public string strValue3 { get; set; }
		public string strValue4 { get; set; }
		public string strValue5 { get; set; }
	}


	public class DATA_SET
	{
		public int nTestType { get; set; }
		public int nID { get; set; }
		public string strTestName { get; set; }
		public string strValue1 { get; set; }
		public string strValue2 { get; set; }
		public string strValue3 { get; set; }
		public string strValue4 { get; set; }
		public string strValue5 { get; set; }
		public string strValue6 { get; set; }
		public string strValue7 { get; set; }
		public string strValue8 { get; set; }
		public string strValue9 { get; set; }
		public string strValue10 { get; set; }
		public string strValue11 { get; set; }
		public string strValue12 { get; set; }
		public string strValue13 { get; set; }
		public string strValue14 { get; set; }
		public string strValue15 { get; set; }
		public string strValue16 { get; set; }
		public string strValue17 { get; set; }
		public string strValue18 { get; set; }
		public string strValue19 { get; set; }
		public string strValue20 { get; set; }
		public string strValue21 { get; set; }
		public string strValue22 { get; set; }
		public string strValue23 { get; set; }
		public string strValue24 { get; set; }
		public string strValue25 { get; set; }
		public string strValue26 { get; set; }
		public string strValue27 { get; set; }
		public string strValue28 { get; set; }
		public string strValue29 { get; set; }
		public string strValue30 { get; set; }
		public string strValue31 { get; set; }
		public string strValue32 { get; set; }
		public string strValue33 { get; set; }
		public string strValue34 { get; set; }
		public string strValue35 { get; set; }
		public string strValue36 { get; set; }
		public string strValue37 { get; set; }
		public string strValue38 { get; set; }
		public string strValue39 { get; set; }
		public string strValue40 { get; set; }
		public string strValue41 { get; set; }
		public string strValue42 { get; set; }
		public string strValue43 { get; set; }
		public string strValue44 { get; set; }
		public string strValue45 { get; set; }
		public string strValue46 { get; set; }
		public string strValue47 { get; set; }
		public string strValue48 { get; set; }
		public string strValue49 { get; set; }
		public string strValue50 { get; set; }
		public string strValue51 { get; set; }
		public string strValue52 { get; set; }
		public string strValue53 { get; set; }
		public string strValue54 { get; set; }
		public string strValue55 { get; set; }
		public string strValue56 { get; set; }
		public string strValue57 { get; set; }
		public string strValue58 { get; set; }
		public string strValue59 { get; set; }
		public string strValue60 { get; set; }
		public string strValue61 { get; set; }
		public string strValue62 { get; set; }
		public string strValue63 { get; set; }
		public string strValue64 { get; set; }
		public string strValue65 { get; set; }
		public string strValue66 { get; set; }
		public string strValue67 { get; set; }
		public string strValue68 { get; set; }
		public string strValue69 { get; set; }
		public string strValue70 { get; set; }
		public string strValue71 { get; set; }
		public string strValue72 { get; set; }
		public string strValue73 { get; set; }
		public string strValue74 { get; set; }
		public string strValue75 { get; set; }
		public string strValue76 { get; set; }
		public string strValue77 { get; set; }
		public string strValue78 { get; set; }
		public string strValue79 { get; set; }
		public string strValue80 { get; set; }
		public string strValue81 { get; set; }
		public string strValue82 { get; set; }
		public string strValue83 { get; set; }
		public string strValue84 { get; set; }
		public string strValue85 { get; set; }
		public string strValue86 { get; set; }
		public string strValue87 { get; set; }
		public string strValue88 { get; set; }
		public string strValue89 { get; set; }
		public string strValue90 { get; set; }
		public string strValue91 { get; set; }
		public string strValue92 { get; set; }
		public string strValue93 { get; set; }
		public string strValue94 { get; set; }
		public string strValue95 { get; set; }
		public string strValue96 { get; set; }
		public string strValue97 { get; set; }
		public string strValue98 { get; set; }
		public string strValue99 { get; set; }
		public string strValue100 { get; set; }
	}

	public class MODEL_SET2
	{
		public string strTestName { get; set; }
		public bool bUseItem { get; set; }
		public bool bDisplayItem { get; set; }
		public int nTestItem { get; set; }

		public ObservableCollection<DATA_SET2> _DataInfo = new ObservableCollection<DATA_SET2>();

		public string strSpecMin { get; set; }
		public string strSpecMax { get; set; }
		public string strUnit { get; set; }

		public string strValue1 { get; set; }
		public string strValue2 { get; set; }
		public string strValue3 { get; set; }
		public string strValue4 { get; set; }
		public string strValue5 { get; set; }
	}


	public class DATA_SET2
	{
		public int nTestType { get; set; }
		public int nID { get; set; }
		public string strTestName { get; set; }
		public string strValue1 { get; set; }
		public string strValue2 { get; set; }
		public string strValue3 { get; set; }
		public string strValue4 { get; set; }
		public string strValue5 { get; set; }
		public string strValue6 { get; set; }
		public string strValue7 { get; set; }
		public string strValue8 { get; set; }
		public string strValue9 { get; set; }
		public string strValue10 { get; set; }
		public string strValue11 { get; set; }
		public string strValue12 { get; set; }
		public string strValue13 { get; set; }
		public string strValue14 { get; set; }
		public string strValue15 { get; set; }
		public string strValue16 { get; set; }
		public string strValue17 { get; set; }
		public string strValue18 { get; set; }
		public string strValue19 { get; set; }
		public string strValue20 { get; set; }
		public string strValue21 { get; set; }
		public string strValue22 { get; set; }
		public string strValue23 { get; set; }
		public string strValue24 { get; set; }
		public string strValue25 { get; set; }
		public string strValue26 { get; set; }
		public string strValue27 { get; set; }
		public string strValue28 { get; set; }
		public string strValue29 { get; set; }
		public string strValue30 { get; set; }
		public string strValue31 { get; set; }
		public string strValue32 { get; set; }
		public string strValue33 { get; set; }
		public string strValue34 { get; set; }
		public string strValue35 { get; set; }
		public string strValue36 { get; set; }
		public string strValue37 { get; set; }
		public string strValue38 { get; set; }
		public string strValue39 { get; set; }
		public string strValue40 { get; set; }
		public string strValue41 { get; set; }
		public string strValue42 { get; set; }
		public string strValue43 { get; set; }
		public string strValue44 { get; set; }
		public string strValue45 { get; set; }
		public string strValue46 { get; set; }
		public string strValue47 { get; set; }
		public string strValue48 { get; set; }
		public string strValue49 { get; set; }
		public string strValue50 { get; set; }
		public string strValue51 { get; set; }
		public string strValue52 { get; set; }
		public string strValue53 { get; set; }
		public string strValue54 { get; set; }
		public string strValue55 { get; set; }
		public string strValue56 { get; set; }
		public string strValue57 { get; set; }
		public string strValue58 { get; set; }
		public string strValue59 { get; set; }
		public string strValue60 { get; set; }
		public string strValue61 { get; set; }
		public string strValue62 { get; set; }
		public string strValue63 { get; set; }
		public string strValue64 { get; set; }
		public string strValue65 { get; set; }
		public string strValue66 { get; set; }
		public string strValue67 { get; set; }
		public string strValue68 { get; set; }
		public string strValue69 { get; set; }
		public string strValue70 { get; set; }
		public string strValue71 { get; set; }
		public string strValue72 { get; set; }
		public string strValue73 { get; set; }
		public string strValue74 { get; set; }
		public string strValue75 { get; set; }
		public string strValue76 { get; set; }
		public string strValue77 { get; set; }
		public string strValue78 { get; set; }
		public string strValue79 { get; set; }
		public string strValue80 { get; set; }
		public string strValue81 { get; set; }
		public string strValue82 { get; set; }
		public string strValue83 { get; set; }
		public string strValue84 { get; set; }
		public string strValue85 { get; set; }
		public string strValue86 { get; set; }
		public string strValue87 { get; set; }
		public string strValue88 { get; set; }
		public string strValue89 { get; set; }
		public string strValue90 { get; set; }
		public string strValue91 { get; set; }
		public string strValue92 { get; set; }
		public string strValue93 { get; set; }
		public string strValue94 { get; set; }
		public string strValue95 { get; set; }
		public string strValue96 { get; set; }
		public string strValue97 { get; set; }
		public string strValue98 { get; set; }
		public string strValue99 { get; set; }
		public string strValue100 { get; set; }
	}






	public static class _Config
	{
		// 마지막 작업한 모델이름
		public static string strCurrentModel = "";
		
		public static string strCurrentModel2 = "";


		public static int nPowerSupply1Port;
		public static int nPowerSupply1BaudRate;
		public static int nPowerSupply2Port;
		public static int nPowerSupply2BaudRate;
		public static int nPowerSupply3Port;
		public static int nPowerSupply3BaudRate;
		public static int nPowerSupply4Port;
		public static int nPowerSupply4BaudRate;
		public static int nBCDScanner1Port;
		public static int nBCDScanner1BaudRate;
		public static int nBCDScanner2Port;
		public static int nBCDScanner2BaudRate;

		public static int nDMM1Port;
		public static int nDMM1BaudRate;
		public static int nDMM2Port;
		public static int nDMM2BaudRate;

		public static string strCellSimulator1IP;
		public static int nCellSimulator1Port;
		public static string strCellSimulator2IP;
		public static int nCellSimulator2Port;
		public static string strCellSimulator3IP;
		public static int nCellSimulator3Port;
		public static string strCellSimulator4IP;
		public static int nCellSimulator4Port;
		public static string strDmmIP;
		public static int nDmmPort;
		public static string strDmmIP2;
		public static int nDmmPort2;
		public static string strRBMSIP;
		public static int nRBMSPort;
		public static string strRBMSIP2;
		public static int nRBMSPort2;

		public static string strCycloneMyIP;
		public static int nCycloneMyPort;

		public static string strCycloneMyIP2;
		public static int nCycloneMyPort2;

		public static string strCounterMyIP;
		public static int nCounterMyPort;
		public static string strCounterMyIP2;
		public static int nCounterMyPort2;

		public static int nBcdPrinterPort;
		public static int nBcdPrinterPort2;

		// LOT 자동 클리어 관련
		public static int nLotClearTime;
		public static bool bUseLotAutoClear;

		// 관리자 비밀번호 사용여부
		public static bool bUseAdminPass;
		public static string strAdminPass = "";
		public static string strMasterPass = "";

		public static string strSaveMesDir = "";
		public static string strSaveMesDir2 = "";
		public static string strBCDMesDir = "";
		public static string strBCDMesDir2 = "";

		public static int strMasterCheckDateTime = 0;
		public static bool bUseMasterCheck = false;

		public static bool bDmmEtcMode = false;

		public static int nEolPinCount;
		public static int nEolPinCount2;

		public static string strToolIP;
		public static int nToolPort;

		public static string strToolIP2;

		public static int nToolPort2;

		public static string strLanguage;
		
		public static string strCyclonDll;





	}


	public static class _SysInfo
	{
		// MainProcess 관련
		public static bool bMainProcessStop;


		// 타워 램프 
		public static TOWER_LAMP TL_Red;
		public static TOWER_LAMP TL_Yellow;
		public static TOWER_LAMP TL_Green;
		public static TOWER_LAMP TL_Buzzer;

		public static int nTL_Beep;

		public static int nMainWorkStep;
		public static int nSubMainWorkStep;
		public static int nSubWorkStep;
		public static int nSubEOLWorkStep;
		public static int nRepeatWorkStep;


		// 커뮤니테이션용 데이터
		public static double dbCommSendData = 0.0;
		public static double dbCommReadData = 0.0;
		public static double dbCommReadMinData = 0.0;
		public static double dbRMSCommReadData = 0.0;
		public static int nDmmCh = 0;
		public static int nDmmTime = 0;
		public static int nDelayTime = 0;
		public static int nSubEol = 0;
		public static string strDmmCh;

		public static bool bFirstCheck = false;

		public static string strDmmReadData;

		public static int nCanCh;
		public static int nCanAddr;
		public static int nCanData;
		public static int nBuffIndex;
		public static bool bEolNg;
		public static int nMaskingData = 0;
		public static int nCompData = 0;
		public static int nBuffCount = 0;
		public static int nDispLen = 0;

		public static string strPingTestIP = "";
		public static MAIN_STATUS bPingTestResult;

		public static int nCanStartAddr;
		public static int nCanMultiCount;
		public static int[] nMultiSendData = new int[150];
		public static string[] strValueBuff = new string[150];
		public static string strPopupContent = "";
		public static string strBcdPopupContent = "";

		public static int nSubCanStartAddr;
		public static int nSubCanMultiCount;
		public static int[] nSubMultiSendData = new int[150];
		public static string[] strSubValueBuff = new string[150];
		public static string strSubPopupContent = "";

		public static int[] nReadDataBuff = new int[150];
		public static string[] strDmmReadDataBuff = new string[150];
		public static int[] nDmmReadDataBuff = new int[150];
		public static double[] dbDmmReadDataBuff = new double[150];
		public static int nDmmBuffIndex;

		public static double dbVoltData = 0.0;
		public static double dbCalcData = 0.0;
		public static double dbCurrCalcData = 0.0;
		public static double dbSubCalcData = 0.0;


		public static ObservableCollection<NgInfoList>[] _listNgInfo = new ObservableCollection<NgInfoList>[1000];



		public static MAIN_STATUS eMainStatus = new MAIN_STATUS();


		public static int nPingRetryCount = 0;

		public static byte[] btTcpReadData;
		public static byte[] btSubTcpReadData;


		public static double dbSpecMax = 0.0;
		public static double dbSpecMin = 0.0;

		public static string strSpecMax;
		public static string strSpecMin;

		public static string strReadBarcode = "";
		public static string strReadBarcode2 = "";
		public static uint nWriteSerialNum = 0;
		public static uint nWriteSerialNum2 = 0;

		public static string strSaveFileName = "";
		public static DateTime dtTestStartTime = new DateTime();
		public static DateTime dtTestEndTime = new DateTime();
		public static string strTotalResult = "";
		public static bool bEMGStop = false;

		public static MAIN_STATUS _SwStatus = new MAIN_STATUS();
		public static MAIN_STATUS _PopupStatus = new MAIN_STATUS();

		public static string strRMDCommData = "";
		public static bool bRMDDataRead1 = false;
		public static bool bRMDDataRead2 = false;
		public static bool bRMDData1OK = false;
		public static bool bRMDData2OK = false;

		public static int nUserDelayTime = 0;


		public static int nReadMac = 0;
		public static int nReadMacHigh = 0; 
		public static int nReadMacMid = 0;
		public static int nReadMacLow = 0;
		public static bool bReadMacOk = false;

		public static bool bReadMainBcd = false;
		public static bool bReadMacBcd = false;

		public static bool bSubEolStart = false;

		public static string strDispBarcode = "";
		public static string strDispMac = "";
		public static string strMacAdress = "";
		public static string strPBMSBcd = "";
		public static string strFuseBcd = "";
		public static string strCaseBcd = "";

		public static string strDispBarcodeFront = "";
		public static string strDispBarcodeBack = "";


		public static bool bOkMasterSampleTestIng = false;
		public static bool bNgMasterSampleTestIng = false;


		public static string strMasterErrString = "";

		public static double dbCellVolt = 0.0;
		public static double dbCellStates = 0.0;
		public static double dbCellVolt2 = 0.0;
		public static double dbCellStates2 = 0.0;
		public static double dbCellStartCH1 = 0.0;
		public static double dbCellStartCH2 = 0.0;
		public static double dbCellEndCH1 = 0.0;
		public static double dbCellEndCH2 = 0.0;

		public static bool bEolReadData;
		public static int nCurrNGRetryCount;
		public static int nValue2Data;
		public static int nTXCh;

		public static double[] dbCellSimulator1V = new double[20];
		public static double[] dbCellSimulator2V = new double[20];
		public static string strSwName = "";

		public static bool bTiteOk;
		public static int nTiteWorkStep;
		public static bool bTiteIng;
		public static int nCount_Beep;

		public static int nTiteLog;
		public static int nPsetOldIndex;


		public static int nTipMaxCount;
		public static int nTipNowCount;
		public static int nSetNutSch;

		public static bool bTiteIngStart;

		public static bool bGetFileNameOK;

		public static int nIOIndex;
		public static int nIOState;
		public static int nCanStation;
		
		public static int nCyclonHandle;
		public static string strCyclonFileName;
		public static double dbNutData;

		public static int nVoltCount;

		public static bool bNutRetry;
		public static bool bNutNext;
		public static bool bNutRetryCheckOK;


		public static CyclonFileName_RESULT _FileNameResult = new CyclonFileName_RESULT();

		public static string strTitleName;
		public static bool bTestNG;

	}

	public static class _SysInfo2
	{
		// MainProcess 관련
		public static bool bMainProcessStop;


		// 타워 램프 
		public static TOWER_LAMP TL_Red;
		public static TOWER_LAMP TL_Yellow;
		public static TOWER_LAMP TL_Green;
		public static TOWER_LAMP TL_Buzzer;

		public static int nTL_Beep;

		public static int nMainWorkStep;
		public static int nSubMainWorkStep;
		public static int nSubWorkStep;
		public static int nSubEOLWorkStep;
		public static int nRepeatWorkStep;

		// 커뮤니테이션용 데이터
		public static double dbCommSendData = 0.0;
		public static double dbCommReadData = 0.0;
		public static double dbCommReadMinData = 0.0;
		public static double dbRMSCommReadData = 0.0;
		public static int nDmmCh = 0;
		public static int nDmmTime = 0;
		public static int nDelayTime = 0;
		public static int nSubEol = 0;
		public static string strDmmCh;

		public static bool bFirstCheck = false;

		public static string strDmmReadData;

		public static int nCanCh;
		public static int nCanAddr;
		public static int nCanData;
		public static int nBuffIndex;
		public static bool bEolNg;
		public static int nMaskingData = 0;
		public static int nCompData = 0;
		public static int nBuffCount = 0;
		public static int nDispLen = 0;

		public static string strPingTestIP = "";
		public static MAIN_STATUS bPingTestResult;

		public static int nCanStartAddr;
		public static int nCanMultiCount;
		public static int[] nMultiSendData = new int[150];
		public static string[] strValueBuff = new string[150];
		public static string strPopupContent = "";
		public static string strBcdPopupContent = "";
		public static string strPBMSBcd = "";
		public static string strFuseBcd = "";
		public static string strCaseBcd = "";

		public static int nSubCanStartAddr;
		public static int nSubCanMultiCount;
		public static int[] nSubMultiSendData = new int[150];
		public static string[] strSubValueBuff = new string[150];
		public static string strSubPopupContent = "";

		public static int[] nReadDataBuff = new int[150];
		public static string[] strDmmReadDataBuff = new string[150];
		public static int[] nDmmReadDataBuff = new int[150];
		public static double[] dbDmmReadDataBuff = new double[150];
		public static int nDmmBuffIndex;

		public static double dbVoltData = 0.0;
		public static double dbCalcData = 0.0;
		public static double dbCurrCalcData = 0.0;
		public static double dbSubCalcData = 0.0;

		

		public static ObservableCollection<NgInfoList>[] _listNgInfo = new ObservableCollection<NgInfoList>[1000];


		public static MAIN_STATUS2 eMainStatus = new MAIN_STATUS2();


		public static int nPingRetryCount = 0;

		public static byte[] btTcpReadData;
		public static byte[] btSubTcpReadData;

		public static double dbSpecMax = 0.0;
		public static double dbSpecMin = 0.0;

		public static string strSpecMax;
		public static string strSpecMin;

		public static string strReadBarcode = "";
		public static string strReadBarcode2 = "";
		public static uint nWriteSerialNum = 0;
		public static uint nWriteSerialNum2 = 0;

		public static string strSaveFileName = "";
		public static DateTime dtTestStartTime = new DateTime();
		public static DateTime dtTestEndTime = new DateTime();
		public static string strTotalResult = "";
		public static bool bEMGStop = false;

		public static MAIN_STATUS2 _SwStatus = new MAIN_STATUS2();
		public static MAIN_STATUS2 _PopupStatus = new MAIN_STATUS2();

		public static string strRMDCommData = "";
		public static bool bRMDDataRead1 = false;
		public static bool bRMDDataRead2 = false;
		public static bool bRMDData1OK = false;
		public static bool bRMDData2OK = false;

		public static int nUserDelayTime = 0;


		public static int nReadMac = 0;
		public static int nReadMacHigh = 0;
		public static int nReadMacMid = 0;
		public static int nReadMacLow = 0;
		public static bool bReadMacOk = false;

		public static bool bReadMainBcd = false;
		public static bool bReadMacBcd = false;

		public static bool bSubEolStart = false;

		public static string strDispBarcode = "";
		public static string strDispMac = "";
		public static string strMacAdress = "";

		public static string strDispBarcodeFront = "";
		public static string strDispBarcodeBack = "";

		public static bool bOkMasterSampleTestIng = false;
		public static bool bNgMasterSampleTestIng = false;


		public static string strMasterErrString = "";

		public static double dbCellVolt = 0.0;
		public static double dbCellStates = 0.0;
		public static double dbCellVolt2 = 0.0;
		public static double dbCellStates2 = 0.0;
		public static double dbCellStartCH1 = 0.0;
		public static double dbCellStartCH2 = 0.0;
		public static double dbCellEndCH1 = 0.0;
		public static double dbCellEndCH2 = 0.0;

		public static bool bEolReadData;
		public static int nCurrNGRetryCount;
		public static int nValue2Data;
		public static int nTXCh;

		public static double[] dbCellSimulator1V = new double[20];
		public static double[] dbCellSimulator2V = new double[20];
		public static string strSwName = "";


		public static bool bTiteOk;
		public static int nTiteWorkStep;
		public static bool bTiteIng;
		public static int nCount_Beep;

		public static int nTiteLog;
		public static int nPsetOldIndex;


		public static int nTipMaxCount;
		public static int nTipNowCount;
		public static int nSetNutSch;

		public static bool bTiteIngStart;
		public static bool bGetFileNameOK;

		public static int nIOIndex;
		public static int nIOState;
		
		public static int nCyclonHandle;
		public static string strCyclonFileName;
		public static double dbNutData;

		public static int nVoltCount;

		public static bool bNutRetry;
		public static bool bNutNext;
		public static bool bNutRetryCheckOK;
		public static bool bTestNG;

		public static string strTitleName;


		public static CyclonFileName_RESULT2 _FileNameResult = new CyclonFileName_RESULT2();
	}

	public class CanData
	{
		public DateTime _Time = new DateTime();
		public bool bReadOk = false;
		public byte[] bData = new byte[8];
	}

	public enum MAIN_STATUS
	{
		READY,
		ING,
		OK,
		NG,
		EMG_STOP,
		OK_MASTER_OK,
		OK_MASTER_NG,
		NG_MASTER_OK,
		NG_MASTER_NG,

	}

	public enum MAIN_STATUS2
	{
		READY,
		ING,
		OK,
		NG,
		EMG_STOP,
		OK_MASTER_OK,
		OK_MASTER_NG,
		NG_MASTER_OK,
		NG_MASTER_NG,

	}

	public enum PROC_LIST
	{
		MAIN,
		MAIN2,
		PING_TEST,
		PING_TEST2,
		TOWER_LAMP,
		TOWER_BUZZER,
		MANUAL,
		SUB_EOL,
		SUB_EOL2,
		CELLSIMULATOR1,
		CELLSIMULATOR2,
		CELLSIMULATOR3,
		CELLSIMULATOR4,
		SUB_TITE1,
		SUB_TITE2,
		MAX,
	}
	public enum TITE_STATUS
	{
		READY,
		OK,
		NG,
	}
	public enum MSG_TYPE
	{
		LOG,
		INFO,
		ERROR,
	}

	public enum LAMP_COLOR
	{
		TL_RED,
		TL_YELLOW,
		TL_GREEN,
		TL_BUZZER,
	}

	// 타워램프
	public enum TOWER_LAMP
	{
		TL_OFF,
		TL_ON,
		TL_BLINK,
	}

	// 커뮤니케이션 스테이터스
	public enum COMM_STATUS
	{
		READY,
		READ_OK,
		COMM_FAIL,
	}

	public enum PING_STATUS
	{
		ING,
		OK,
		NG,
		READY,
	}

	public enum CYCLON_STATUS
	{
		ING,
		OK,
		NG,
		READY,
	}
	public enum CYCLON_STATUS2
	{
		ING,
		OK,
		NG,
		READY,
	}

	public enum CyclonFileName_RESULT
	{
		READY,
		OK,
		NG,
		FAIL,
	}

	public enum CyclonFileName_RESULT2
	{
		READY,
		OK,
		NG,
		FAIL,
	}


	#region 카운트 설정
	[Serializable]
	public class ProductCount
	{
		public int nOkCount;
		public int nNgCount;
		public int nTotalCount;
		public int nPinCount;
		public int nStation5PinCount;
		public int nStation6PinCount;
		public DateTime tLotClearTiem;
	}



	[Serializable]
	public class LotCount
	{
		public int nLotCount;
		public int nProductCount;
		public int nSpareCount1;
		public int nSpareCount2;
		public int nSpareCount3;
		public int nTotalCount;
		public DateTime tLotClearTime;
		public DateTime tProductClearTime;
		public int nOkCount;
		public int nNGCount;
	}
	#endregion

	public class LogMessage
	{
		public DateTime dtAppendTime { get; set; }
		public string strType { get; set; }
		public string strComment { get; set; }
	}
	public class LogMessage2
	{
		public DateTime dtAppendTime { get; set; }
		public string strType { get; set; }
		public string strComment { get; set; }
	}



	public class _IniFile
	{
		[DllImport("kernel32.dll")]
		private static extern int GetPrivateProfileString(String section, String key, String def, StringBuilder retVal, int Size, String filePat);

		[DllImport("Kernel32.dll")]
		private static extern long WritePrivateProfileString(String Section, String Key, String val, String filePath);

		public static void IniWriteValue(String Section, String Key, String Value, string avaPath)
		{
			WritePrivateProfileString(Section, Key, Value, avaPath);
		}

		public static String IniReadValue(String Section, String Key, string def, string avsPath)
		{
			StringBuilder temp = new StringBuilder(2000);
			int i = GetPrivateProfileString(Section, Key, def, temp, 2000, avsPath);
			return temp.ToString();
		}
	}

	//public class _CyclonDLL
	//{
	//	[DllImport("CycloneControlSDK.dll", CallingConvention = CallingConvention.Cdecl)]
	//	public static extern int Cyclone_Connect(string connectionString);

	//	[DllImport("CycloneControlSDK.dll", CallingConvention = CallingConvention.Cdecl)]
	//	public static extern int Cyclone_GetImageID([MarshalAs(UnmanagedType.LPStr)] System.Text.StringBuilder imageID, int bufferSize);

	//	[DllImport("CycloneControlSDK.dll", CallingConvention = CallingConvention.Cdecl)]
	//	public static extern int Cyclone_StartImage();

	//	[DllImport("CycloneControlSDK.dll", CallingConvention = CallingConvention.Cdecl)]
	//	public static extern int Cyclone_Disconnect();
	//}

	

	public static class CopyJson
	{
		public static T CopyJsons<T>(this object src) where T : class
		{
			string strData = JsonConvert.SerializeObject(src);
			return JsonConvert.DeserializeObject<T>(strData);
		}
	}
}


