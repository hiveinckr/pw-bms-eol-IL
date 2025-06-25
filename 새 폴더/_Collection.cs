using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	public class _Collection
	{
		public static ObservableCollection<SpecView> workInfoView = new ObservableCollection<SpecView>();
		public static ObservableCollection<SpecView> workOptionView = new ObservableCollection<SpecView>();
		public static ObservableCollection<SpecView> ConfigWorkOptionView = new ObservableCollection<SpecView>();

		public static ObservableCollection<SpecView> workInfoView2 = new ObservableCollection<SpecView>();
		public static ObservableCollection<SpecView> workOptionView2 = new ObservableCollection<SpecView>();
		public static ObservableCollection<SpecView> ConfigWorkOptionView2 = new ObservableCollection<SpecView>();

		public static ObservableCollection<SchName> PsetNameList = new ObservableCollection<SchName>();
		public static ObservableCollection<SchName> CanSetNameList = new ObservableCollection<SchName>();
		public static ObservableCollection<SchName> IONameList = new ObservableCollection<SchName>();

		public static void InitPsetList()
		{
			PsetNameList.Clear();
			PsetNameList.Add(new SchName { nSchID = 0, strSchName = "EOL" });
			PsetNameList.Add(new SchName { nSchID = 1, strSchName = "ADC" });
			PsetNameList.Add(new SchName { nSchID = 2, strSchName = "P/S_1" });
			PsetNameList.Add(new SchName { nSchID = 12, strSchName = "P/S_1(Curr)" });
			PsetNameList.Add(new SchName { nSchID = 3, strSchName = "P/S_2" });
			PsetNameList.Add(new SchName { nSchID = 13, strSchName = "P/S_2(Curr)" });
			PsetNameList.Add(new SchName { nSchID = 4, strSchName = "I/O" });
			PsetNameList.Add(new SchName { nSchID = 5, strSchName = "DMM(Voltage)" });
			PsetNameList.Add(new SchName { nSchID = 6, strSchName = "DMM(Freq)" });
			PsetNameList.Add(new SchName { nSchID = 7, strSchName = "DMM(Res)" });
			PsetNameList.Add(new SchName { nSchID = 8, strSchName = "POP UP" });
			PsetNameList.Add(new SchName { nSchID = 9, strSchName = "Ping Test" });
			PsetNameList.Add(new SchName { nSchID = 10, strSchName = "R.S/W_Name" });
			PsetNameList.Add(new SchName { nSchID = 11, strSchName = "B.S/W_Name" });
			PsetNameList.Add(new SchName { nSchID = 14, strSchName = "C/S_1" });
			PsetNameList.Add(new SchName { nSchID = 15, strSchName = "C/S_2" });
			PsetNameList.Add(new SchName { nSchID = 16, strSchName = "DMM(Curr.Min)" });
			PsetNameList.Add(new SchName { nSchID = 17, strSchName = "DMM(RMS)" });
			PsetNameList.Add(new SchName { nSchID = 18, strSchName = "Delay" });
			PsetNameList.Add(new SchName { nSchID = 19, strSchName = "EOL(Repeat)" });
			PsetNameList.Add(new SchName { nSchID = 20, strSchName = "DMM(V/Count)" });
			PsetNameList.Add(new SchName { nSchID = 21, strSchName = "ADC(DMM)" });
			PsetNameList.Add(new SchName { nSchID = 22, strSchName = "DMM(Curr.A)" });
			PsetNameList.Add(new SchName { nSchID = 23, strSchName = "Bolt Tight" });
			PsetNameList.Add(new SchName { nSchID = 24, strSchName = "Firmware Update" });
			PsetNameList.Add(new SchName { nSchID = 25, strSchName = "Barcode Save" });





			CanSetNameList.Clear();
			CanSetNameList.Add(new SchName { nSchID = 0, strSchName = "MODBUS_CAN_WRITE(SINGLE)" });
			CanSetNameList.Add(new SchName { nSchID = 4, strSchName = "MODBUS_CAN_WRITE(MULT)" });
			CanSetNameList.Add(new SchName { nSchID = 1, strSchName = "MODBUS_CAN_READ(COMP)" });
			CanSetNameList.Add(new SchName { nSchID = 2, strSchName = "MODBUS_CAN_READ(BUFF)" });
			//CanSetNameList.Add(new SchName { nSchID = 10, strSchName = "MODBUS_CAN_READ(BIT)" });
			CanSetNameList.Add(new SchName { nSchID = 3, strSchName = "DEALY" });
			CanSetNameList.Add(new SchName { nSchID = 5, strSchName = "CAN_WRITE" });
			CanSetNameList.Add(new SchName { nSchID = 6, strSchName = "CAN_READ" });
			CanSetNameList.Add(new SchName { nSchID = 7, strSchName = "MODBUS_TCP_WRITE(MULT)" });
			CanSetNameList.Add(new SchName { nSchID = 8, strSchName = "MODBUS_TCP_READ(COMP)" });
			CanSetNameList.Add(new SchName { nSchID = 9, strSchName = "MODBUS_TCP_READ(BUFF)" });

			IONameList.Clear();
			IONameList.Add(new SchName { nSchID = 0, strSchName = "RY_01" });
		

		}

		// 모델정보 생성
		public static void Update(ref MODEL_INFO _mInfo)
		{
			if (_Config.strLanguage == "ENGLISH")
			{
				workInfoView.Clear();
				workInfoView.Add(new SpecView() { strName = "Barcode Symbol", strValue = _mInfo.strBarcodSymbol });
				workInfoView.Add(new SpecView() { strName = "FUSE Barcode Symbol", strValue = _mInfo.strFuseBarcodSymbol });
				workInfoView.Add(new SpecView() { strName = "CASE Barcode Symbol", strValue = _mInfo.strCaseBarcodSymbol });
				workInfoView.Add(new SpecView() { strName = "Serial Number Index", strValue = _mInfo.nSerailNumIndex.ToString() });
				workInfoView.Add(new SpecView() { strName = "Master Sample Barcode(OK)", strValue = _mInfo.strMasterOkSampleBarcode });
				workInfoView.Add(new SpecView() { strName = "Master Sample Barcode(NG)", strValue = _mInfo.strMasterNgSampleBarcode });
				workInfoView.Add(new SpecView() { strName = "Resistance Value", strValue = _mInfo.dbResistance.ToString() });
				workInfoView.Add(new SpecView() { strName = "DMM Scan Speed", strValue = _mInfo.dbDmmScanSpeed.ToString() });
				workInfoView.Add(new SpecView() { strName = "DMM MIN Scan Speed", strValue = _mInfo.dbDmmMScanSpeed.ToString() });
				workInfoView.Add(new SpecView() { strName = "DMM AVG Scan Speed", strValue = _mInfo.dbDmmAScanSpeed.ToString() });
				workInfoView.Add(new SpecView() { strName = "Printer barcode size", strValue = _mInfo.nBCDsize.ToString() });
				workInfoView.Add(new SpecView() { strName = "Printer text height", strValue = _mInfo.nBCDStringHeight.ToString() });
				workInfoView.Add(new SpecView() { strName = "Printer text width", strValue = _mInfo.nBCDStringWidth.ToString() });
				workInfoView.Add(new SpecView() { strName = "Printer barcode left/right position", strValue = _mInfo.nBCDbcdOffsetX.ToString() });
				workInfoView.Add(new SpecView() { strName = "Printer barcode up/down position", strValue = _mInfo.nBCDbcdOffsetY.ToString() });
				workInfoView.Add(new SpecView() { strName = "Printer text left/right position ", strValue = _mInfo.nBCDtextOffsetX.ToString() });
				workInfoView.Add(new SpecView() { strName = "Printer text up/down position", strValue = _mInfo.nBCDtextOffsetY.ToString() });

				workOptionView.Clear();
				workOptionView.Add(new SpecView() { bUseSpec = _mInfo.bUseRMDTestMode, strName = "RMD Test Mode" });
			}
			else
			{
				workInfoView.Clear();
				workInfoView.Add(new SpecView() { strName = "바코드 구분자", strValue = _mInfo.strBarcodSymbol });
				workInfoView.Add(new SpecView() { strName = "FUSE 바코드 구분자", strValue = _mInfo.strFuseBarcodSymbol });
				workInfoView.Add(new SpecView() { strName = "CASE 바코드 구분자", strValue = _mInfo.strCaseBarcodSymbol });
				workInfoView.Add(new SpecView() { strName = "시리얼 넘버 시작위치", strValue = _mInfo.nSerailNumIndex.ToString() });
				workInfoView.Add(new SpecView() { strName = "마스터 샘플 바코드(OK)", strValue = _mInfo.strMasterOkSampleBarcode });
				workInfoView.Add(new SpecView() { strName = "마스터 샘플 바코드(NG)", strValue = _mInfo.strMasterNgSampleBarcode });
				workInfoView.Add(new SpecView() { strName = "저항 값 입력", strValue = _mInfo.dbResistance.ToString() });
				workInfoView.Add(new SpecView() { strName = "전압 검사 스캔 시간", strValue = _mInfo.dbDmmScanSpeed.ToString() });
				workInfoView.Add(new SpecView() { strName = "전류 검사 스캔 시간(MIN)", strValue = _mInfo.dbDmmMScanSpeed.ToString() });
				workInfoView.Add(new SpecView() { strName = "전류 검사 스캔 시간(AVG)", strValue = _mInfo.dbDmmAScanSpeed.ToString() });
				workInfoView.Add(new SpecView() { strName = "바코드 프린터 바코드 크기", strValue = _mInfo.nBCDsize.ToString() });
				workInfoView.Add(new SpecView() { strName = "바코드 프린터 텍스트 높이", strValue = _mInfo.nBCDStringHeight.ToString() });
				workInfoView.Add(new SpecView() { strName = "바코드 프린터 텍스트 두께", strValue = _mInfo.nBCDStringWidth.ToString() });
				workInfoView.Add(new SpecView() { strName = "바코드 프린터 바코드 좌우 위치", strValue = _mInfo.nBCDbcdOffsetX.ToString() });
				workInfoView.Add(new SpecView() { strName = "바코드 프린터 바코드 상하 위치", strValue = _mInfo.nBCDbcdOffsetY.ToString() });
				workInfoView.Add(new SpecView() { strName = "바코드 프린터 텍스트 좌우 위치 ", strValue = _mInfo.nBCDtextOffsetX.ToString() });
				workInfoView.Add(new SpecView() { strName = "바코드 프린터 텍스트 상하 위치", strValue = _mInfo.nBCDtextOffsetY.ToString() });

				workOptionView.Clear();
				workOptionView.Add(new SpecView() { bUseSpec = _mInfo.bUseRMDTestMode, strName = "RMD 테스트 모드" });
			}

		


			//workInfoView.Add(new SpecView() { strName = "(Y2) 제품 폭", strValue = _mInfo.dbY2ProductRecivePos.ToString("F2") });
			//workInfoView.Add(new SpecView() { strName = "(Y2) 제품 고정 거리", strValue = _mInfo.dbY2ProductFixDist.ToString("F2") });

		
			
		

			//PsetNameList.Clear();
			//PsetNameList.Add(new SchName { nSchID = 0, strSchName = "EOL" });
			//PsetNameList.Add(new SchName { nSchID = 1, strSchName = "ADC" });
			//PsetNameList.Add(new SchName { nSchID = 2, strSchName = "P/S_1" });
			//PsetNameList.Add(new SchName { nSchID = 3, strSchName = "P/S_2" });
			//PsetNameList.Add(new SchName { nSchID = 4, strSchName = "I/O" });
			//PsetNameList.Add(new SchName { nSchID = 5, strSchName = "DMM(Voltage)" });
			//PsetNameList.Add(new SchName { nSchID = 6, strSchName = "DMM(Freq)" });
			//PsetNameList.Add(new SchName { nSchID = 7, strSchName = "DMM(Res)" });
			//PsetNameList.Add(new SchName { nSchID = 8, strSchName = "POP UP" });
			//PsetNameList.Add(new SchName { nSchID = 9, strSchName = "Ping Test" });


			//CanSetNameList.Clear();
			//CanSetNameList.Add(new SchName { nSchID = 0, strSchName = "MODBUS_CAN_WRITE(SINGLE)" });
			//CanSetNameList.Add(new SchName { nSchID = 4, strSchName = "MODBUS_CAN_WRITE(MULT)" });
			//CanSetNameList.Add(new SchName { nSchID = 1, strSchName = "MODBUS_CAN_READ(COMP)" });
			//CanSetNameList.Add(new SchName { nSchID = 2, strSchName = "MODBUS_CAN_READ(BUFF)" });
			//CanSetNameList.Add(new SchName { nSchID = 3, strSchName = "DEALY" });
			//CanSetNameList.Add(new SchName { nSchID = 5, strSchName = "CAN_WRITE" });
			//CanSetNameList.Add(new SchName { nSchID = 6, strSchName = "CAN_READ" });
			//CanSetNameList.Add(new SchName { nSchID = 7, strSchName = "MODBUS_TCP_WRITE(MULT)" });
			//CanSetNameList.Add(new SchName { nSchID = 8, strSchName = "MODBUS_TCP_READ(COMP)" });
			//CanSetNameList.Add(new SchName { nSchID = 9, strSchName = "MODBUS_TCP_READ(BUFF)" });

			//IONameList.Clear();
			//IONameList.Add(new SchName { nSchID = 0, strSchName = "BBMS_CB" });
			//IONameList.Add(new SchName { nSchID = 1, strSchName = "BBMS_FB1" });
			//IONameList.Add(new SchName { nSchID = 2, strSchName = "BBMS_FB2" });
			//IONameList.Add(new SchName { nSchID = 3, strSchName = "BBMS_FB3" });
			//IONameList.Add(new SchName { nSchID = 4, strSchName = "RBMS_FUSE" });
			//IONameList.Add(new SchName { nSchID = 5, strSchName = "RBMS_MC1" });
			//IONameList.Add(new SchName { nSchID = 6, strSchName = "RBMS_MC2" });
			//IONameList.Add(new SchName { nSchID = 7, strSchName = "RBMS_CB" });
			//IONameList.Add(new SchName { nSchID = 8, strSchName = "RBMS_DS" });
			//IONameList.Add(new SchName { nSchID = 9, strSchName = "RBMS_FB1" });
			//IONameList.Add(new SchName { nSchID = 10, strSchName = "RBMS_FB2" });
			//IONameList.Add(new SchName { nSchID = 11, strSchName = "RBMS_FB3" });
			//IONameList.Add(new SchName { nSchID = 12, strSchName = "RBMS_HARD_WIRE_IN" });
			//IONameList.Add(new SchName { nSchID = 13, strSchName = "RBMS_BI_WAKE_IN_X2" });
			//IONameList.Add(new SchName { nSchID = 14, strSchName = "RBMS_BI_WAKE_IN_X3" });
			//IONameList.Add(new SchName { nSchID = 15, strSchName = "BBMS_HARD_WIRE_IN" });
			//IONameList.Add(new SchName { nSchID = 23, strSchName = "BBMS_BI_WAKE_IN_X2" });
			//IONameList.Add(new SchName { nSchID = 24, strSchName = "BBMS_BI_WAKE_IN_X3" });
			//IONameList.Add(new SchName { nSchID = 25, strSchName = "MBMS_WAKE" });
		}

		// 모델정보 읽기
		public static void Read(ref MODEL_INFO _mInfo)
		{
			int nSpecIndex = 0;


			nSpecIndex = 0;
			_mInfo.strBarcodSymbol = workInfoView[nSpecIndex++].strValue;
			_mInfo.strFuseBarcodSymbol = workInfoView[nSpecIndex++].strValue;
			_mInfo.strCaseBarcodSymbol = workInfoView[nSpecIndex++].strValue;
			int.TryParse(workInfoView[nSpecIndex++].strValue, out _mInfo.nSerailNumIndex);
			_mInfo.strMasterOkSampleBarcode = workInfoView[nSpecIndex++].strValue;
			_mInfo.strMasterNgSampleBarcode = workInfoView[nSpecIndex++].strValue;
			double.TryParse(workInfoView[nSpecIndex++].strValue, out _mInfo.dbResistance);
			double.TryParse(workInfoView[nSpecIndex++].strValue, out _mInfo.dbDmmScanSpeed);
			double.TryParse(workInfoView[nSpecIndex++].strValue, out _mInfo.dbDmmMScanSpeed);
			double.TryParse(workInfoView[nSpecIndex++].strValue, out _mInfo.dbDmmAScanSpeed);
			int.TryParse(workInfoView[nSpecIndex++].strValue, out _mInfo.nBCDsize);
			int.TryParse(workInfoView[nSpecIndex++].strValue, out _mInfo.nBCDStringHeight);
			int.TryParse(workInfoView[nSpecIndex++].strValue, out _mInfo.nBCDStringWidth);
			int.TryParse(workInfoView[nSpecIndex++].strValue, out _mInfo.nBCDbcdOffsetX);
			int.TryParse(workInfoView[nSpecIndex++].strValue, out _mInfo.nBCDbcdOffsetY);
			int.TryParse(workInfoView[nSpecIndex++].strValue, out _mInfo.nBCDtextOffsetX);
			int.TryParse(workInfoView[nSpecIndex++].strValue, out _mInfo.nBCDtextOffsetY);

			//double.TryParse(workInfoView[nSpecIndex++].strValue, out _mInfo.dbXReadPos);
			//double.TryParse(workInfoView[nSpecIndex++].strValue, out _mInfo.dbY3SensorWidthPos);
			//double.TryParse(workInfoView[nSpecIndex++].strValue, out _mInfo.dbY2ProductRecivePos);
			//double.TryParse(workInfoView[nSpecIndex++].strValue, out _mInfo.dbY2ProductFixDist);

			nSpecIndex = 0;
			_mInfo.bUseRMDTestMode = workOptionView[nSpecIndex++].bUseSpec;
			//int.TryParse(workOptionView[nSpecIndex++].strValue, out nReadData);
			//_mInfo.nJumpCount = nReadData;

		

		}

		public static void Update2(ref MODEL_INFO2 _mInfo)
		{
			if(_Config.strLanguage == "ENGLISH")
			{
				workInfoView2.Clear();
				workInfoView2.Add(new SpecView() { strName = "Barcode Symbol", strValue = _mInfo.strBarcodSymbol });
				workInfoView2.Add(new SpecView() { strName = "FUSE Barcode Symbol", strValue = _mInfo.strFuseBarcodSymbol });
				workInfoView2.Add(new SpecView() { strName = "CASE Barcode Symbol", strValue = _mInfo.strCaseBarcodSymbol });
				workInfoView2.Add(new SpecView() { strName = "Serial Number Index", strValue = _mInfo.nSerailNumIndex.ToString() });
				workInfoView2.Add(new SpecView() { strName = "Master Sample Barcode(OK)", strValue = _mInfo.strMasterOkSampleBarcode });
				workInfoView2.Add(new SpecView() { strName = "Master Sample Barcode(NG)", strValue = _mInfo.strMasterNgSampleBarcode });
				workInfoView2.Add(new SpecView() { strName = "Resistance Value", strValue = _mInfo.dbResistance.ToString() });
				workInfoView2.Add(new SpecView() { strName = "DMM Scan Speed", strValue = _mInfo.dbDmmScanSpeed.ToString() });
				workInfoView2.Add(new SpecView() { strName = "DMM MIN Scan Speed", strValue = _mInfo.dbDmmMScanSpeed.ToString() });
				workInfoView2.Add(new SpecView() { strName = "DMM AVG Scan Speed", strValue = _mInfo.dbDmmAScanSpeed.ToString() });
				workInfoView2.Add(new SpecView() { strName = "Printer barcode size", strValue = _mInfo.nBCDsize.ToString() });
				workInfoView2.Add(new SpecView() { strName = "Printer text height", strValue = _mInfo.nBCDStringHeight.ToString() });
				workInfoView2.Add(new SpecView() { strName = "Printer text width", strValue = _mInfo.nBCDStringWidth.ToString() });
				workInfoView2.Add(new SpecView() { strName = "Printer barcode left/right position", strValue = _mInfo.nBCDbcdOffsetX.ToString() });
				workInfoView2.Add(new SpecView() { strName = "Printer barcode up/down position", strValue = _mInfo.nBCDbcdOffsetY.ToString() });
				workInfoView2.Add(new SpecView() { strName = "Printer text left/right position ", strValue = _mInfo.nBCDtextOffsetX.ToString() });
				workInfoView2.Add(new SpecView() { strName = "Printer text up/down position", strValue = _mInfo.nBCDtextOffsetY.ToString() });

				workOptionView2.Clear();
				workOptionView2.Add(new SpecView() { bUseSpec = _mInfo.bUseRMDTestMode, strName = "RMD Test Mode" });

				
			}
			else
			{
				workInfoView2.Clear();
				workInfoView2.Add(new SpecView() { strName = "바코드 구분자", strValue = _mInfo.strBarcodSymbol });
				workInfoView2.Add(new SpecView() { strName = "FUSE 바코드 구분자", strValue = _mInfo.strFuseBarcodSymbol });
				workInfoView2.Add(new SpecView() { strName = "CASE 바코드 구분자", strValue = _mInfo.strCaseBarcodSymbol });
				workInfoView2.Add(new SpecView() { strName = "시리얼 넘버 시작위치", strValue = _mInfo.nSerailNumIndex.ToString() });
				workInfoView2.Add(new SpecView() { strName = "마스터 샘플 바코드(OK)", strValue = _mInfo.strMasterOkSampleBarcode });
				workInfoView2.Add(new SpecView() { strName = "마스터 샘플 바코드(NG)", strValue = _mInfo.strMasterNgSampleBarcode });
				workInfoView2.Add(new SpecView() { strName = "저항 값 입력", strValue = _mInfo.dbResistance.ToString() });
				workInfoView2.Add(new SpecView() { strName = "전압 검사 스캔 시간", strValue = _mInfo.dbDmmScanSpeed.ToString() });
				workInfoView2.Add(new SpecView() { strName = "전류 검사 스캔 시간(MIN)", strValue = _mInfo.dbDmmMScanSpeed.ToString() });
				workInfoView2.Add(new SpecView() { strName = "전류 검사 스캔 시간(AVG)", strValue = _mInfo.dbDmmAScanSpeed.ToString() });
				workInfoView2.Add(new SpecView() { strName = "프린터 바코드 크기", strValue = _mInfo.nBCDsize.ToString() });
				workInfoView2.Add(new SpecView() { strName = "프린터 텍스트 높이", strValue = _mInfo.nBCDStringHeight.ToString() });
				workInfoView2.Add(new SpecView() { strName = "프린터 텍스트 두께", strValue = _mInfo.nBCDStringWidth.ToString() });
				workInfoView2.Add(new SpecView() { strName = "프린터 바코드 좌우 위치", strValue = _mInfo.nBCDbcdOffsetX.ToString() });
				workInfoView2.Add(new SpecView() { strName = "프린터 바코드 상하 위치", strValue = _mInfo.nBCDbcdOffsetY.ToString() });
				workInfoView2.Add(new SpecView() { strName = "프린터 텍스트 좌우 위치 ", strValue = _mInfo.nBCDtextOffsetX.ToString() });
				workInfoView2.Add(new SpecView() { strName = "프린터 텍스트 상하 위치", strValue = _mInfo.nBCDtextOffsetY.ToString() });


				workOptionView2.Clear();
				workOptionView2.Add(new SpecView() { bUseSpec = _mInfo.bUseRMDTestMode, strName = "RMD 테스트 모드" });
			}
		

		}

		// 모델정보 읽기
		public static void Read2(ref MODEL_INFO2 _mInfo)
		{
			int nSpecIndex = 0;


			nSpecIndex = 0;
			_mInfo.strBarcodSymbol = workInfoView2[nSpecIndex++].strValue;
			_mInfo.strFuseBarcodSymbol = workInfoView2[nSpecIndex++].strValue;
			_mInfo.strCaseBarcodSymbol = workInfoView2[nSpecIndex++].strValue;
			int.TryParse(workInfoView2[nSpecIndex++].strValue, out _mInfo.nSerailNumIndex);
			_mInfo.strMasterOkSampleBarcode = workInfoView2[nSpecIndex++].strValue;
			_mInfo.strMasterNgSampleBarcode = workInfoView2[nSpecIndex++].strValue;
			double.TryParse(workInfoView2[nSpecIndex++].strValue, out _mInfo.dbResistance);
			double.TryParse(workInfoView2[nSpecIndex++].strValue, out _mInfo.dbDmmScanSpeed);
			double.TryParse(workInfoView2[nSpecIndex++].strValue, out _mInfo.dbDmmMScanSpeed);
			double.TryParse(workInfoView2[nSpecIndex++].strValue, out _mInfo.dbDmmAScanSpeed);
			int.TryParse(workInfoView2[nSpecIndex++].strValue, out _mInfo.nBCDsize);
			int.TryParse(workInfoView2[nSpecIndex++].strValue, out _mInfo.nBCDStringHeight);
			int.TryParse(workInfoView2[nSpecIndex++].strValue, out _mInfo.nBCDStringWidth);
			int.TryParse(workInfoView2[nSpecIndex++].strValue, out _mInfo.nBCDbcdOffsetX);
			int.TryParse(workInfoView2[nSpecIndex++].strValue, out _mInfo.nBCDbcdOffsetY);
			int.TryParse(workInfoView2[nSpecIndex++].strValue, out _mInfo.nBCDtextOffsetX);
			int.TryParse(workInfoView2[nSpecIndex++].strValue, out _mInfo.nBCDtextOffsetY);

			//double.TryParse(workInfoView[nSpecIndex++].strValue, out _mInfo.dbXReadPos);
			//double.TryParse(workInfoView[nSpecIndex++].strValue, out _mInfo.dbY3SensorWidthPos);
			//double.TryParse(workInfoView[nSpecIndex++].strValue, out _mInfo.dbY2ProductRecivePos);
			//double.TryParse(workInfoView[nSpecIndex++].strValue, out _mInfo.dbY2ProductFixDist);

			nSpecIndex = 0;
			_mInfo.bUseRMDTestMode = workOptionView2[nSpecIndex++].bUseSpec;
			//int.TryParse(workOptionView[nSpecIndex++].strValue, out nReadData);
			//_mInfo.nJumpCount = nReadData;

			

		}


		public static void UpdateConfig()
		{
			if(_Config.strLanguage == "ENGLISH")
			{
				ConfigWorkOptionView.Clear();
				ConfigWorkOptionView.Add(new SpecView() { bUseSpec = _Config.bUseAdminPass, strName = "Use Admin Password", strValue = _Config.strAdminPass });
				ConfigWorkOptionView.Add(new SpecView() { bUseSpec = _Config.bUseLotAutoClear, strName = "Use Lot Auto Clear", strValue = _Config.nLotClearTime.ToString("D4") });
				ConfigWorkOptionView.Add(new SpecView() { bUseSpec = _Config.bUseMasterCheck, strName = "Use Master Sample Check", strValue = _Config.strMasterCheckDateTime.ToString("D4") });
			}
			else
			{
				ConfigWorkOptionView.Clear();
				ConfigWorkOptionView.Add(new SpecView() { bUseSpec = _Config.bUseAdminPass, strName = "관리자 비밀번호 사용함", strValue = _Config.strAdminPass });
				ConfigWorkOptionView.Add(new SpecView() { bUseSpec = _Config.bUseLotAutoClear, strName = "검사수량 자동 초기화 사용함", strValue = _Config.nLotClearTime.ToString("D4") });
				ConfigWorkOptionView.Add(new SpecView() { bUseSpec = _Config.bUseMasterCheck, strName = "마스터 샘플 검사기능 사용함", strValue = _Config.strMasterCheckDateTime.ToString("D4") });
			}
		

		}

		public static void ReadConfig()
		{
			int nSpecIndex = 0;

			nSpecIndex = 0;

			_Config.bUseAdminPass = ConfigWorkOptionView[nSpecIndex].bUseSpec;                              // 관리자 비밀번호 사용여부
			_Config.strAdminPass = ConfigWorkOptionView[nSpecIndex++].strValue;                             // 관리자 비밀번호

			_Config.bUseLotAutoClear = ConfigWorkOptionView[nSpecIndex].bUseSpec;                              // 수량 자동 클리어 사용함
			int.TryParse(ConfigWorkOptionView[nSpecIndex++].strValue, out _Config.nLotClearTime);              // 수량 자동 초기화 시간

			_Config.bUseMasterCheck = ConfigWorkOptionView[nSpecIndex].bUseSpec;                              // 수량 자동 클리어 사용함
			int.TryParse(ConfigWorkOptionView[nSpecIndex++].strValue, out _Config.strMasterCheckDateTime);              // 수량 자동 초기화 시간
		}
	}

	public class myCanData
	{
		public DateTime _tTime { get; set; }
		public int nCh { get; set; }
		public uint uID { get; set; }
		public int nID { get; set; }
		public int nLen { get; set; }
		public byte btData1 { get; set; }
		public byte btData2 { get; set; }
		public byte btData3 { get; set; }
		public byte btData4 { get; set; }
		public byte btData5 { get; set; }
		public byte btData6 { get; set; }
		public byte btData7 { get; set; }
		public byte btData8 { get; set; }
		public bool bNewData { get; set; }
		public string strType { get; set; }

		public string strData1 { get; set; }
		public string strData2 { get; set; }
		public string strData3 { get; set; }
		public string strData4 { get; set; }
		public string strData5 { get; set; }
		public string strData6 { get; set; }
		public string strData7 { get; set; }
		public string strData8 { get; set; }

	}

	public class myCanData2
	{
		public DateTime _tTime { get; set; }
		public int nCh { get; set; }
		public uint uID { get; set; }
		public int nID { get; set; }
		public int nLen { get; set; }
		public byte btData1 { get; set; }
		public byte btData2 { get; set; }
		public byte btData3 { get; set; }
		public byte btData4 { get; set; }
		public byte btData5 { get; set; }
		public byte btData6 { get; set; }
		public byte btData7 { get; set; }
		public byte btData8 { get; set; }
		public bool bNewData { get; set; }
		public string strType { get; set; }

		public string strData1 { get; set; }
		public string strData2 { get; set; }
		public string strData3 { get; set; }
		public string strData4 { get; set; }
		public string strData5 { get; set; }
		public string strData6 { get; set; }
		public string strData7 { get; set; }
		public string strData8 { get; set; }

	}

	public class myTestData
	{
		public string strTestName { get; set; }
		public string strResult { get; set; }
		public string strType { get; set; }
		public string SpecMin { get; set; }
		public string Data { get; set; }
		public string SpecMax { get; set; }
		public string Unit { get; set; }
		public string Cate { get; set; }        // Category
		public int nNowCount { get; set; }
		public int nMaxCount { get; set; }
		public int nSch { get; set; }
		public double dbData { get; set; }
	}

	public class myTestData2
	{
		public string strTestName { get; set; }
		public string strResult { get; set; }
		public string strType { get; set; }
		public string SpecMin { get; set; }
		public string Data { get; set; }
		public string SpecMax { get; set; }
		public string Unit { get; set; }
		public string Cate { get; set; }        // Category
		public int nNowCount { get; set; }
		public int nMaxCount { get; set; }
		public int nSch { get; set; }
		public double dbData { get; set; }
	}

	public class NgInfoList
	{
		public string strAddr { get; set; }
		public string strSource { get; set; }
		public string strRead { get; set; }
		public string strTestResult { get; set; }
	}

	public class NgInfoList2
	{
		public string strAddr { get; set; }
		public string strSource { get; set; }
		public string strRead { get; set; }
		public string strTestResult { get; set; }
	}

	public class myModbusCanData
	{
		public DateTime _tTime { get; set; }
		public int nData { get; set; }
		public bool bNewData { get; set; }
	}

	public class SpecView
	{
		public string strName { get; set; }
		public string strValue { get; set; }
		public string strValue2 { get; set; }
		public string strValue3 { get; set; }
		public string strValue4 { get; set; }
		public string strValue5 { get; set; }
		public string strValue6 { get; set; }
		public bool bUseSpec { get; set; }
	}


	public class SchName
	{
		public int nSchID { get; set; }
		public string strSchName { get; set; }
	}


	public class TestTotalResult
	{
		public DateTime dtAppendTime { get; set; }
		public string strBCD { get; set; }
		public string strMin { get; set; }
		public string strMax { get; set; }
		public string strDev { get; set; }
		public string strTotalResult { get; set; }
	}
}
