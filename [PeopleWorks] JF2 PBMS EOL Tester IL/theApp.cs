
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Threading;








namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	internal class theApp
	{

		public static ObservableCollection<LogMessage> MainLogMessage = new ObservableCollection<LogMessage>();
		public static ObservableCollection<LogMessage> MainLogMessage2 = new ObservableCollection<LogMessage>();
		public static ObservableCollection<LogMessage> DebugMessage = new ObservableCollection<LogMessage>();
		public static ObservableCollection<myCanData> lstMyLog = new ObservableCollection<myCanData>();
		public static ObservableCollection<myCanData2> lstMyLog2 = new ObservableCollection<myCanData2>();
		public static ObservableCollection<myTestData> _TestData = new ObservableCollection<myTestData>();
		public static ObservableCollection<myTestData2> _TestData2 = new ObservableCollection<myTestData2>();
		public static ObservableCollection<TestTotalResult> _TestResultList = new ObservableCollection<TestTotalResult>();
		public static ObservableCollection<TestTotalResult> _TestResultList2 = new ObservableCollection<TestTotalResult>();

		public static ATTestEth _ATTester = new ATTestEth();

		public static MASTER_INFO_CHECK _MasterTestInfo = new MASTER_INFO_CHECK();
		//
		public static OPE_PS[] PowerSupply = new OPE_PS[4];
		public static OPE_PS KeysiteDmm = new OPE_PS();
		public static OPE_PS KeysiteDmm2 = new OPE_PS();
		public static BarcodeReader _BcdReader = new BarcodeReader();
		public static BarcodeReader _BcdReader2 = new BarcodeReader();

		public static ClientSoket _CellSimulator1 = new ClientSoket();
		public static ClientSoket _CellSimulator2 = new ClientSoket();
		public static ClientSoket _CellSimulator3 = new ClientSoket();
		public static ClientSoket _CellSimulator4 = new ClientSoket();
		public static DmmEtc _KeysiteDmmEtc = new DmmEtc();
		public static DmmEtc _KeysiteDmmEtc2 = new DmmEtc();
		public static NutrunnerEth _Nutrunner = new NutrunnerEth();
		public static NutrunnerEth _Nutrunner2 = new NutrunnerEth();


		 public static BarcodePrint _BarcodePrint = new BarcodePrint();
		 public static BarcodePrint _BarcodePrint2 = new BarcodePrint();


		// 메인 쓰레드
		public static Thread MainThread = new Thread(WorkMain);
		public static Thread MainThread2 = new Thread(WorkMain2);

		// 모델 정보
		public static MODEL_INFO _ModelInfo = new MODEL_INFO();
		public static MODEL_INFO2 _ModelInfo2 = new MODEL_INFO2();

		//========== Can 통신
		//public static PcanComm[] _CanComm = new PcanComm[6];
		public static KvaserCanComm[] _CanComm = new KvaserCanComm[8];


		// 수량 정보
		public static LotCount _LotCount = new LotCount();
		public static LotCount _LotCount2 = new LotCount();
		public static List<double> cellT = new List<double>();
		public static List<double> cellT2 = new List<double>();
		// 누적 수량 정보
		public static ProductCount _Count = new ProductCount();
		public static ProductCount _Count2 = new ProductCount();

		//========== 프로세스 스텝
		public static int[] nProcessStep = new int[100];

		// 타이머
		public static HIVE_Timer[] tMainTimer = new HIVE_Timer[100];


		public static Stopwatch _tTackTimer = new Stopwatch();
		public static Stopwatch _tTackTimer2 = new Stopwatch();

		public static String[] strReadBuff = new String[100];
		public static String[] strReadBuff2 = new String[100];

		public static Ping _pingSender = new Ping();
		public static Ping _pingSender2 = new Ping();
		public static PingOptions _pingoptions = new PingOptions();

		public static PopUp _PopUpWindow;
		public static PopUp2 _PopUpWindow2;
		public static MasterPopup _MasterPopup;
		public static BarcodePopUP _BarcodePopUP;
		public static BarcodePopUP2 _BarcodePopUP2;



		public static TcpClient tcpClient = new TcpClient();
		public static ClientSoket _EthDmm = new ClientSoket();

		public static ModbusTCP _ModbusSoket = new ModbusTCP();
		public static ModbusTCP _ModbusSoket2 = new ModbusTCP();

		public static List<myCanData> lstData = new List<myCanData>();
		public static List<myCanData2> lstData2 = new List<myCanData2>();

		public static PingTester _PingTest = new PingTester();
		public static UserStartMassage2 _UserStartMassage2;

		public static CYCLON_STATUS _CyStatus = new CYCLON_STATUS();
		public static CYCLON_STATUS2 _CyStatus2 = new CYCLON_STATUS2();


		// 최초 시작시 장비관련 데이터 초기화
		public static void initMachine()
		{
			for (int i = 0; i < tMainTimer.Length; i++) { tMainTimer[i] = new HIVE_Timer(); }
			for (int i = 0; i < PowerSupply.Length; i++) { PowerSupply[i] = new OPE_PS(); }
			for (int i = 0; i < _CanComm.Length; i++) { _CanComm[i] = new KvaserCanComm(); }
			//for (int i = 0; i < _CanComm.Length; i++) { _CanComm[i] = new PcanComm(); }
			//for (int i = 0; i < _KvaserCanComm.Length; i++) { _KvaserCanComm[i] = new KvaserCanComm(); }

			for (int i = 0; i < 0x50000; i++)
			{
				lstData.Add(new myCanData());
			}

			for (int i = 0; i < 0x50000; i++)
			{
				lstData2.Add(new myCanData2());
			}

			for (int i = 0; i < _SysInfo._listNgInfo.Length; i++)
			{
				_SysInfo._listNgInfo[i] = new ObservableCollection<NgInfoList>();
			}

			for (int i = 0; i < _SysInfo2._listNgInfo.Length; i++)
			{
				_SysInfo2._listNgInfo[i] = new ObservableCollection<NgInfoList>();
			}

			// IO 정보 초기화
			_Define.CreateDIOInfo();
			_Collection.InitPsetList();
			// INI 파일 로드
			LoadIniFile();
			LoadModelInfo(ref _ModelInfo, _Config.strCurrentModel); // 현재 모델 불러오기
			LoadModelProductCount(ref _LotCount, _Config.strCurrentModel); // 현재 카운트 불러오기
			LoadProductCount();

			LoadModelInfo2(ref _ModelInfo2, _Config.strCurrentModel2); // 현재 모델 불러오기
			LoadModelProductCount2(ref _LotCount2, _Config.strCurrentModel2); // 현재 카운트 불러오기
			LoadProductCount2();

			// COMM Port 초기화
			InitCommPort();
			_PingTest.InitPingTester();
			CAXL.AxlOpen(7);

		}

		public static void WorkMain()
		{
			Thread.Sleep(500);

			int nThreadCount = 0;

			// 메인 쓰레드
			while (!_SysInfo.bMainProcessStop)
			{
				PowerSupply[0].Process();
				PowerSupply[1].Process();
				//PowerSupply[2].Process();
				//PowerSupply[3].Process();
				_BcdReader.Process();
				//_BcdReader2.Process();
				_Nutrunner.Process();
				//_Nutrunner2.Process();

				_CanComm[0].ReadMessage();
				_CanComm[1].ReadMessage();
				_CanComm[2].ReadMessage();
				_CanComm[3].ReadMessage();
				_CanComm[4].ReadMessage();
				_CanComm[5].ReadMessage();
				_CanComm[6].ReadMessage();
				_CanComm[7].ReadMessage();

				_BarcodePrint.Process();
				//_BarcodePrint2.Process();


				if (_Config.bDmmEtcMode)
				{
					_KeysiteDmmEtc.Process();
				}
				else
				{
					KeysiteDmm.Process();
				}

				//if (_Config.bDmmEtcMode)
				//{
				//	_KeysiteDmmEtc2.Process();
				//}
				//else
				//{
				//	KeysiteDmm2.Process();
				//}


				_CellSimulator1.Process();
				_CellSimulator2.Process();
				//_CellSimulator3.Process();
				//_CellSimulator4.Process();
				//_Cyclone.Process();

				PROC_MAIN();
				//PROC_MAIN2();
				SUB_EOL();
				//SUB_EOL2();
				PingTest();
				//PingTest2();
				TowerBuzzerProcess();
				TowerLampProcess();
				LotCountInfoUpdate();
				//LotCountInfoUpdate2();
				PROC_MANUAL();
				SUB_TITE_PROC();
				//SUB_TITE_PROC2();
				
				if (nThreadCount > 100)
				{
					nThreadCount = 0;
					Thread.Sleep(1);
				}
				else
				{
					nThreadCount++;
				}
			}

			for (int i = 0; i < 32; i++)
			{
				SetDIOPort((DO)i, false);
			}
			Thread.Sleep(100);

			for (int i = 0; i < 8; i++)
			{
				_CanComm[i].CanClose();
			}

			PowerSupply[0].CloseComm();
			PowerSupply[1].CloseComm();
			//PowerSupply[2].CloseComm();
			//PowerSupply[3].CloseComm();
			_BcdReader.CloseComm();
			//_BcdReader2.CloseComm();
			KeysiteDmm.CloseComm();
			//KeysiteDmm2.CloseComm();

			while (PowerSupply[0].PortIsAlive()) { Thread.Sleep(1); }
			while (PowerSupply[1].PortIsAlive()) { Thread.Sleep(1); }
			while (_BcdReader.IsOpened()) { Thread.Sleep(1); }
			while (KeysiteDmm.PortIsAlive()) { Thread.Sleep(1); }
			//while (PowerSupply[2].PortIsAlive()) { Thread.Sleep(1); }
			//while (PowerSupply[3].PortIsAlive()) { Thread.Sleep(1); }
			//while (_BcdReader2.IsOpened()) { Thread.Sleep(1); }
			//while (KeysiteDmm2.PortIsAlive()) { Thread.Sleep(1); }

			SaveIniFile();


			Thread.Sleep(1000);
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE
			{
				App.Current.Shutdown();
			});
		}

		public static void WorkMain2()
		{
			Thread.Sleep(500);

			int nThreadCount = 0;

			// 메인 쓰레드
			while (!_SysInfo.bMainProcessStop)
			{
				PowerSupply[2].Process();
				PowerSupply[3].Process();
				_BcdReader2.Process();
				_Nutrunner2.Process();

				_BarcodePrint2.Process();

				if (_Config.bDmmEtcMode)
				{
					_KeysiteDmmEtc2.Process();
				}
				else
				{
					KeysiteDmm2.Process();
				}

				_CellSimulator3.Process();
				_CellSimulator4.Process();
				//_Cyclone.Process();


				PROC_MAIN2();
				SUB_EOL2();
				PingTest2();
				LotCountInfoUpdate2();
				SUB_TITE_PROC2();

				if (nThreadCount > 100)
				{
					nThreadCount = 0;
					Thread.Sleep(1);
				}
				else
				{
					nThreadCount++;
				}
			}

			PowerSupply[2].CloseComm();
			PowerSupply[3].CloseComm();
			_BcdReader2.CloseComm();
			KeysiteDmm2.CloseComm();

			while (PowerSupply[2].PortIsAlive()) { Thread.Sleep(1); }
			while (PowerSupply[3].PortIsAlive()) { Thread.Sleep(1); }
			while (_BcdReader2.IsOpened()) { Thread.Sleep(1); }
			while (KeysiteDmm2.PortIsAlive()) { Thread.Sleep(1); }

			Thread.Sleep(1000);
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE
			{
				App.Current.Shutdown();
			});
		}



		public static void PROC_MAIN()
		{

			int nStepIndex = (int)PROC_LIST.MAIN;

			if (_SysInfo.bReadMainBcd)
			{
				SetDIOPort(DO.SW_LAMP1, true);
				SetDIOPort(DO.SW_LAMP2, true);
			}
			else
			{
				SetDIOPort(DO.SW_LAMP1, false);
				SetDIOPort(DO.SW_LAMP2, false);
			}


			switch (nProcessStep[nStepIndex])
			{

				case 0:
					if (_BcdReader.bReadOk)
					{
						_BcdReader.bReadOk = false;

						_SysInfo.strReadBarcode = _BcdReader.strReadBarcode;
						
						if(_ModelInfo.bUseRbmsTest && !_ModelInfo.bUseRMDTestMode)
						{

							uint nReadSerialNum = 0;


							if (CheckBarcode(_SysInfo.strReadBarcode, _ModelInfo.strBarcodSymbol))
							{
								if (uint.TryParse(_SysInfo.strReadBarcode.Substring(_ModelInfo.nSerailNumIndex, 10), out nReadSerialNum))
								{

									// Mater 바코드 여부 판별
									if (CheckBarcode(_BcdReader.strReadBarcode, _ModelInfo.strMasterOkSampleBarcode))
									{
										// 마스터 바코드일 경우 마스터 검사 루틴 진행
										_SysInfo.bOkMasterSampleTestIng = true;
										_SysInfo.bNgMasterSampleTestIng = false;
										_SysInfo.nWriteSerialNum = nReadSerialNum;
										_SysInfo.strDispBarcode = _SysInfo.strReadBarcode;
										_SysInfo.bReadMainBcd = true;

										if (!_SysInfo.bReadMacBcd) { _SysInfo.strDispMac = ""; }

									}
									else if (CheckBarcode(_BcdReader.strReadBarcode, _ModelInfo.strMasterNgSampleBarcode))
									{
										// 마스터 바코드일 경우 마스터 검사 루틴 진행
										_SysInfo.bOkMasterSampleTestIng = false;
										_SysInfo.bNgMasterSampleTestIng = true;
										_SysInfo.nWriteSerialNum = nReadSerialNum;
										_SysInfo.strDispBarcode = _SysInfo.strReadBarcode;
										_SysInfo.bReadMainBcd = true;

										if (!_SysInfo.bReadMacBcd) { _SysInfo.strDispMac = ""; }
									}
									else
									{
										// 마스터 검사 진행여부 체크하는 루틴 추가
										if (_Config.bUseMasterCheck && !CheckMasterTestFinish(_ModelInfo.strModelName))
										{

											// 마스터 팝업 발생

											_SysInfo.nTL_Beep = 3;
											ShowMasterPopUpWindow();



										}
										else
										{
											_SysInfo.bOkMasterSampleTestIng = false;
											_SysInfo.bNgMasterSampleTestIng = false;
											_SysInfo.nWriteSerialNum = nReadSerialNum;
											_SysInfo.strDispBarcode = _SysInfo.strReadBarcode;
											_SysInfo.bReadMainBcd = true;

											if (!_SysInfo.bReadMacBcd) { _SysInfo.strDispMac = ""; }
										}

									}

								}
								else
								{
									theApp.AppendLogMsg("Serial number format does not match", MSG_TYPE.ERROR);
									_SysInfo.nTL_Beep = 3;
								}
							}
							else if (_SysInfo.strReadBarcode.Length == 12)
							{
								_SysInfo.bReadMacOk = true;
								_SysInfo.nReadMacHigh = 0;
								_SysInfo.nReadMacLow = 0;

								_SysInfo.strMacAdress = _SysInfo.strReadBarcode;

								_SysInfo.bReadMacOk &= int.TryParse(_SysInfo.strReadBarcode.Substring(0, 2), System.Globalization.NumberStyles.HexNumber, null, out _SysInfo.nReadMac);
								_SysInfo.nReadMacHigh = _SysInfo.nReadMac * 0x100;
								_SysInfo.bReadMacOk &= int.TryParse(_SysInfo.strReadBarcode.Substring(2, 2), System.Globalization.NumberStyles.HexNumber, null, out _SysInfo.nReadMac);
								_SysInfo.nReadMacHigh += _SysInfo.nReadMac;
								_SysInfo.bReadMacOk &= int.TryParse(_SysInfo.strReadBarcode.Substring(4, 2), System.Globalization.NumberStyles.HexNumber, null, out _SysInfo.nReadMac);
								_SysInfo.nReadMacMid = _SysInfo.nReadMac * 0x100;
								_SysInfo.bReadMacOk &= int.TryParse(_SysInfo.strReadBarcode.Substring(6, 2), System.Globalization.NumberStyles.HexNumber, null, out _SysInfo.nReadMac);
								_SysInfo.nReadMacMid += _SysInfo.nReadMac;
								_SysInfo.bReadMacOk &= int.TryParse(_SysInfo.strReadBarcode.Substring(8, 2), System.Globalization.NumberStyles.HexNumber, null, out _SysInfo.nReadMac);
								_SysInfo.nReadMacLow = _SysInfo.nReadMac * 0x100;
								_SysInfo.bReadMacOk &= int.TryParse(_SysInfo.strReadBarcode.Substring(10, 2), System.Globalization.NumberStyles.HexNumber, null, out _SysInfo.nReadMac);
								_SysInfo.nReadMacLow += _SysInfo.nReadMac;

								if (_SysInfo.bReadMacOk)
								{
									_SysInfo.strDispMac = _SysInfo.nReadMacHigh.ToString("X4") + _SysInfo.nReadMacMid.ToString("X4") + _SysInfo.nReadMacLow.ToString("X4");
									_SysInfo.bReadMacBcd = true;
									if (!_SysInfo.bReadMainBcd) { _SysInfo.strDispBarcode = ""; }
								}
								else
								{
									theApp.AppendLogMsg("Pbms Mac format does not match.", MSG_TYPE.ERROR);
									_SysInfo.nTL_Beep = 3;
								}
							}
							else
							{
								theApp.AppendLogMsg("Rbms Barcode format does not match.", MSG_TYPE.ERROR);
								_SysInfo.nTL_Beep = 3;
							}

							if (_SysInfo.bReadMainBcd && _SysInfo.bReadMacBcd)
							{
								ShowUserStartMessege();
								nProcessStep[nStepIndex] = 2;
							}
						}
						else
						{
							uint nReadSerialNum = 0;

							if (CheckBarcode(_SysInfo.strReadBarcode, _ModelInfo.strBarcodSymbol))
							{


								if (uint.TryParse(_SysInfo.strReadBarcode.Substring(_ModelInfo.nSerailNumIndex, 10), out nReadSerialNum))
								{

									// Mater 바코드 여부 판별
									if (CheckBarcode(_BcdReader.strReadBarcode, _ModelInfo.strMasterOkSampleBarcode))
									{
										// 마스터 바코드일 경우 마스터 검사 루틴 진행
										_SysInfo.bOkMasterSampleTestIng = true;
										_SysInfo.bNgMasterSampleTestIng = false;
										_SysInfo.nWriteSerialNum = nReadSerialNum;
										_SysInfo.strDispBarcode = _SysInfo.strReadBarcode;
										_SysInfo.bReadMainBcd = true;
										ShowUserStartMessege();
										if (!_SysInfo.bReadMacBcd) { _SysInfo.strDispMac = ""; }
										nProcessStep[nStepIndex] = 2;

									}
									else if (CheckBarcode(_BcdReader.strReadBarcode, _ModelInfo.strMasterNgSampleBarcode))
									{
										// 마스터 바코드일 경우 마스터 검사 루틴 진행
										_SysInfo.bOkMasterSampleTestIng = false;
										_SysInfo.bNgMasterSampleTestIng = true;
										_SysInfo.nWriteSerialNum = nReadSerialNum;
										_SysInfo.strDispBarcode = _SysInfo.strReadBarcode;
										_SysInfo.bReadMainBcd = true;
										ShowUserStartMessege();
										if (!_SysInfo.bReadMacBcd) { _SysInfo.strDispMac = ""; }
										nProcessStep[nStepIndex] = 2;
									}
									else
									{
										// 마스터 검사 진행여부 체크하는 루틴 추가
										if (_Config.bUseMasterCheck && !CheckMasterTestFinish(_ModelInfo.strModelName))
										{

											// 마스터 팝업 발생

											_SysInfo.nTL_Beep = 3;
											ShowMasterPopUpWindow();
											nProcessStep[nStepIndex] = 2;

										}
										else
										{
											_SysInfo.bOkMasterSampleTestIng = false;
											_SysInfo.bNgMasterSampleTestIng = false;
											_SysInfo.nWriteSerialNum = nReadSerialNum;
											_SysInfo.strDispBarcode = _SysInfo.strReadBarcode;
											_SysInfo.bReadMainBcd = true;
											ShowUserStartMessege();
											if (!_SysInfo.bReadMacBcd) { _SysInfo.strDispMac = ""; }
											nProcessStep[nStepIndex] = 2;
										}

									}

								}
								else
								{
									theApp.AppendLogMsg("Pbms Serial number format does not match.", MSG_TYPE.ERROR);
									_SysInfo.nTL_Beep = 3;
								}
							}
							else
							{
								theApp.AppendLogMsg("Pbms Barcode format does not match.", MSG_TYPE.ERROR);
								_SysInfo.nTL_Beep = 3;
							}
						}
						



					}

					break;

				case 2:
					if (GetDIOPort(DI.START_SW1) && GetDIOPort(DI.START_SW2))
					{
						tMainTimer[nStepIndex].Start(50);
						nProcessStep[nStepIndex]++;

					}
					break;

				case 3:
					if (GetDIOPort(DI.START_SW1) && GetDIOPort(DI.START_SW2) && tMainTimer[nStepIndex].Verify())
					{
						HideUserStartMessege();
						nProcessStep[nStepIndex] = 1000;
					}
					else if (!GetDIOPort(DI.START_SW1) || !GetDIOPort(DI.START_SW2))
					{
						nProcessStep[nStepIndex] = 2;
					}
					break;

				case 100:
					break;



				//// Soket Test
				//case 50:
				//	tcpClient.Connect("127.0.0.1", 502)
				//	nProcessStep[nStepIndex]++;
				//	break;

				//case 51:
				//	if(_ModbusSoket.)
				//	break;


				// 작업 시작
				case 1000:
					if (_ModelInfo.bUseRMDTestMode)
					{
						_Config.nEolPinCount++;
						nProcessStep[nStepIndex] = 2000;
						break;
						//if (_SysInfo.bReadMainBcd)
						//{
						//	nProcessStep[nStepIndex] = 2000;
						//	return;
						//}
						//else
						//{
						//	if (!_SysInfo.bReadMainBcd) { AppendLogMsg("바코드가 스캔되지 않습니다.", MSG_TYPE.ERROR); }
						//	_SysInfo.nTL_Beep = 3;
						//	nProcessStep[nStepIndex] = 0;
						//	return;
						//}

					}
					
					if(_ModelInfo.bUseRbmsTest && !_ModelInfo.bUseRMDTestMode)
					{

						if (_SysInfo.bReadMainBcd && _SysInfo.bReadMacBcd)
						{
							_Config.nEolPinCount++;
							nProcessStep[nStepIndex] = 2000;
						}
						else
						{
							if (!_SysInfo.bReadMainBcd) { AppendLogMsg("Barcode not scanning.", MSG_TYPE.ERROR); }
							if (!_SysInfo.bReadMacBcd) { AppendLogMsg("MAC Barcode not scanning.", MSG_TYPE.ERROR); }
							_SysInfo.nTL_Beep = 3;
							nProcessStep[nStepIndex] = 0;
						}
					}
					else
					{
						if (_SysInfo.bReadMainBcd /*&& _SysInfo.bReadMacBcd*/)
						{
							_Config.nEolPinCount++;
							nProcessStep[nStepIndex] = 2000;
						}
						else
						{
							if (!_SysInfo.bReadMainBcd) { AppendLogMsg("Barcode not scanning.", MSG_TYPE.ERROR); }
							//if (!_SysInfo.bReadMacBcd) { AppendLogMsg("MAC 바코드가 스캔되지 않습니다.", MSG_TYPE.ERROR); }
							_SysInfo.nTL_Beep = 3;
							nProcessStep[nStepIndex] = 0;
						}
					}

					
					break;



				// Unit 초기화 작업
				case 2000:
					_SysInfo.nTL_Beep = 1;
					_SysInfo.nMainWorkStep = 0;
					_SysInfo.nSubWorkStep = 0;
					_SysInfo.nVoltCount = 0;
					_SysInfo.bEMGStop = false;	
					_SysInfo.dtTestStartTime = DateTime.Now;
					_SysInfo.strSaveFileName = _SysInfo.strDispBarcode + DateTime.Now.ToString("_HHmmss");

					_tTackTimer.Restart();
					NgDataClear();
					SetDIOPort(DO.RY_01 + 1 - 1, false);
					SetDIOPort(DO.RY_01 + 15 - 1, false);
					SetDIOPort(DO.RY_01 + 16 - 1, false);
					SetDIOPort(DO.RY_01 + 17 - 1, false);
					SetDIOPort(DO.RY_01 + 18 - 1, false);
					SetDIOPort(DO.RY_01 + 19 - 1, false);
					SetDIOPort(DO.RY_01 + 20 - 1, false);
					SetDIOPort(DO.RY_01 + 21 - 1, false);
					SetDIOPort(DO.RY_01 + 22 - 1, false);
					SetDIOPort(DO.RY_01 + 23 - 1, false);
					SetDIOPort(DO.RY_01 + 24 - 1, false);
					SetDIOPort(DO.RY_01 + 25 - 1, false);
					SetDIOPort(DO.RY_01 + 26 - 1, false);
					SetDIOPort(DO.RY_01 + 27 - 1, false);
					SetDIOPort(DO.RY_01 + 28 - 1, false);
					SetDIOPort(DO.RY_01 + 29 - 1, false);
					SetDIOPort(DO.RY_01 + 30 - 1, false);
					SetDIOPort(DO.RY_01 + 31 - 1, false);
					SetDIOPort(DO.RY_01 + 32 - 1, false);
					SetDIOPort(DO.RY_01 + 33 - 1, false);
					SetDIOPort(DO.RY_01 + 34 - 1, false);
					SetDIOPort(DO.RY_01 + 35 - 1, false);

					App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
					{
						TestUIClearSet();
					});

					tMainTimer[nStepIndex].Start(500);
					_SysInfo.eMainStatus = MAIN_STATUS.ING;
					nProcessStep[nStepIndex] = 2500;
					break;

				case 2500:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					nProcessStep[nStepIndex] = 3000;
					break;

				// 초기화 완료 검사 시작스텝
				case 3000:
					if (_SysInfo.nMainWorkStep >= _ModelInfo._TestInfo.Count)
					{
						nProcessStep[nStepIndex] = 80000;
					}
					else
					{
						if (_SysInfo.nMainWorkStep > 0)
						{
							//if (_TestData[_SysInfo.nMainWorkStep - 1].strResult == "NG")
							//{
							//	// 불량 발생시 PopUp 발생
							//	AppendLogMsg("11111", MSG_TYPE.LOG);
							//	_SysInfo.bTestNG = false;
							//	nProcessStep[nStepIndex] = 3100;
							//}
							//else
							//{
							//	AppendLogMsg("2222", MSG_TYPE.LOG);
							//	nProcessStep[nStepIndex] = 4000;
							//}

							if (_SysInfo.bTestNG)
							{
								// 불량 발생시 PopUp 발생
							
								_SysInfo.bTestNG = false;
								nProcessStep[nStepIndex] = 3100;
							}
							else
							{
					
								nProcessStep[nStepIndex] = 4000;
							}
						}
						else
						{
						
							nProcessStep[nStepIndex] = 4000;
						}
					}
					break;

				case 3100:
					_SysInfo.strPopupContent = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep - 1].strTestName + " NG";
					_SysInfo._SwStatus = MAIN_STATUS.READY;
					_SysInfo._PopupStatus = MAIN_STATUS.NG;
					ShowNGPopUpWindow();
					nProcessStep[nStepIndex]++;
					break;

				case 3101:
					if (_SysInfo._SwStatus == MAIN_STATUS.OK)
					{
						nProcessStep[nStepIndex] = 4000;
					}
					else if (_SysInfo._SwStatus == MAIN_STATUS.NG)
					{
						nProcessStep[nStepIndex] = 80000;
					}

					if (GetDIOPort(DI.START_SW1))
					{
						CloseNGPopUpWindow();
						_SysInfo._SwStatus = MAIN_STATUS.OK;
						nProcessStep[nStepIndex] = 3102;
					}
					else if (GetDIOPort(DI.START_SW2))
					{
						CloseNGPopUpWindow();
						_SysInfo._SwStatus = MAIN_STATUS.NG;
						nProcessStep[nStepIndex] = 3102;
					}
					break;

				case 3102:
					if (!GetDIOPort(DI.START_SW1) && !GetDIOPort(DI.START_SW2))
					{
						nProcessStep[nStepIndex]++;
					}
					break;

				case 3103:
					if (_SysInfo._SwStatus == MAIN_STATUS.OK)
					{
						nProcessStep[nStepIndex] = 4000;
					}
					else if (_SysInfo._SwStatus == MAIN_STATUS.NG)
					{
						nProcessStep[nStepIndex] = 80000;
					}
					break;


				case 4000:
					if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].bUseItem)
					{
						// EOL 검사 스텝
						if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 0)
						{
							_SysInfo.nSubWorkStep = 0;
							_SysInfo.bEolNg = false;
							nProcessStep[nStepIndex] = 20000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 1)
						{
							//ADC Calc Step
							nProcessStep[nStepIndex] = 30000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 2)
						{
							//PS1
							nProcessStep[nStepIndex] = 31000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 12)
						{
							//PS1 _ Curr
							nProcessStep[nStepIndex] = 31200;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 3)
						{
							//PS2
							nProcessStep[nStepIndex] = 32000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 13)
						{
							//PS2 _ Curr
							nProcessStep[nStepIndex] = 32200;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 4)
						{
							//IO Step
							_SysInfo.nSubWorkStep = 0;
							nProcessStep[nStepIndex] = 33000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 5)
						{
							//DMM Step 전압
							nProcessStep[nStepIndex] = 34000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 6)
						{
							//DMM Step 전류
							nProcessStep[nStepIndex] = 35000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 7)
						{
							//DMM Step 저항
							nProcessStep[nStepIndex] = 36000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 8)
						{
							//POP UP
							nProcessStep[nStepIndex] = 37000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 9)
						{
							//ping
							nProcessStep[nStepIndex] = 38000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 10)
						{
							//Version Parse (R_Platform)
							nProcessStep[nStepIndex] = 39000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 11)
						{
							//Version Parse (B_Platform)
							nProcessStep[nStepIndex] = 40000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 14)
						{
							// C/S_1
							nProcessStep[nStepIndex] = 41000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 15)
						{
							// C/S_2
							nProcessStep[nStepIndex] = 42000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 16)
						{
							// DMM(Curr)
							//_SysInfo.bEolReadData = false;
							_SysInfo.nCurrNGRetryCount = 0;
							cellT.Clear();
							nProcessStep[nStepIndex] = 43000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 17)
						{
							// DMM(RMS)
							nProcessStep[nStepIndex] = 44000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 18)
						{
							// Delay
							nProcessStep[nStepIndex] = 45000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 19)
						{
							// EOL(Repeat)
							_SysInfo.nRepeatWorkStep = 0;
							nProcessStep[nStepIndex] = 46000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 20)
						{
							// DMM(V/Count)
							nProcessStep[nStepIndex] = 47000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 21)
						{
							// ADC(DMM)
							nProcessStep[nStepIndex] = 48000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 22)
						{
							// DMM(Curr.A)
							_SysInfo.nCurrNGRetryCount = 0;
							cellT.Clear();
							nProcessStep[nStepIndex] = 49000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 23)
						{
							// 체결
							_SysInfo.nTipNowCount = 0;

							nProcessStep[nStepIndex] = 50000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 24)
						{
							// 펌웨어 업데이트
							_SysInfo.strCyclonFileName = "";
							_SysInfo.bGetFileNameOK = false;
							nProcessStep[nStepIndex] = 51000;
						}
						else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 25)
						{
							// Barcode Save
							_SysInfo.strCaseBcd = "";
							_SysInfo.strFuseBcd = "";
							_SysInfo.strPBMSBcd = "";
							_SysInfo.strDispBarcodeFront = "";
							_SysInfo.strDispBarcodeBack = "";
							nProcessStep[nStepIndex] = 52000;
						}
						//else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 26)
						//{

						//	nProcessStep[nStepIndex] = 53000;
						//}
						//else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].nTestItem == 27)
						//{
						//	nProcessStep[nStepIndex] = 54000;
						//}
					}
					else
					{
						TestResultSet(_SysInfo.nMainWorkStep, "", "PASS");
						_SysInfo.nMainWorkStep++;
						nProcessStep[nStepIndex] = 3000;
					}
					break;



				// PowerSupply 스텝
				case 10000:
					if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID == 0 || _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID == 1)
					{
						nProcessStep[nStepIndex] = 10010;
					}
					else
					{
						AppendLogMsg("Power supply ID setting error", MSG_TYPE.ERROR);
						nProcessStep[nStepIndex] = 90000;
					}
					break;


				// 1번 채널 Power Supply 설정
				case 10010:
					PowerSupply[_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID].SendData("*IDN?");
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex]++;
					break;

				case 10011:
					if (tMainTimer[nStepIndex].Verify())
					{
						AppendLogMsg("Power supply initialization failed", MSG_TYPE.ERROR);
						nProcessStep[nStepIndex] = 90000;
					}

					if (PowerSupply[_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID].IsReadData())
					{
						nProcessStep[nStepIndex] = 10020;
					}
					break;

				// 전압설정
				case 10020:
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue1, out _SysInfo.dbCommSendData);

					PowerSupply[_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID].SendData($"VOLT {_SysInfo.dbCommSendData}");
					tMainTimer[nStepIndex].Start(200);
					nProcessStep[nStepIndex]++;
					break;

				case 10021:
					if (!tMainTimer[nStepIndex].Verify()) { break; }

					nProcessStep[nStepIndex] = 10030;
					break;

				case 10030:
					PowerSupply[_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID].SendData("VOLT?");
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex]++;
					break;

				case 10031:
					if (tMainTimer[nStepIndex].Verify())
					{
						AppendLogMsg("Power Supply Voltage Setting Failed", MSG_TYPE.ERROR);
						nProcessStep[nStepIndex] = 90000;
					}

					if (PowerSupply[_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID].IsReadData())
					{
						nProcessStep[nStepIndex] = 10040;
					}
					break;

				case 10040:
					double.TryParse(PowerSupply[_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID].GetReadData(), out _SysInfo.dbCommReadData);

					if (_SysInfo.dbCommReadData == _SysInfo.dbCommSendData)
					{
						nProcessStep[nStepIndex] = 10100;
					}
					break;



				case 10100:
					_SysInfo.nSubWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;




				// EOL MODE
				case 20000:
					if (_SysInfo.nSubWorkStep >= _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo.Count)
					{
						if (_SysInfo.bEolNg)
						{
							TestResultSet(_SysInfo.nMainWorkStep, "", "NG");
							_SysInfo.bTestNG = true;
						}
						else
						{
							TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
						}
						nProcessStep[nStepIndex] = 29000;
					}
					else
					{
						nProcessStep[nStepIndex] = 21000;
					}
					break;


				case 21000:
					if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nTestType == 0)
					{
						// Modbus Can Write
						nProcessStep[nStepIndex] = 21100;
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nTestType == 1)
					{
						// Modbus Can Read(Comp)
						nProcessStep[nStepIndex] = 21200;
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nTestType == 2)
					{
						// Modbus Can Read(Buff)
						nProcessStep[nStepIndex] = 21300;
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nTestType == 3)
					{
						// Dealy
						nProcessStep[nStepIndex] = 21400;
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nTestType == 4)
					{
						// Modbus Can Write(Multi)
						nProcessStep[nStepIndex] = 21500;
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nTestType == 5)
					{
						// Can Write
						nProcessStep[nStepIndex] = 21600;
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nTestType == 6)
					{
						// Can Read
						nProcessStep[nStepIndex] = 21700;
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nTestType == 7)
					{
						// TCP Write
						nProcessStep[nStepIndex] = 21800;
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nTestType == 8)
					{
						// TCP Read ( Comp )
						nProcessStep[nStepIndex] = 21900;
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nTestType == 9)
					{
						// TCP Read ( Buff )
						nProcessStep[nStepIndex] = 22000;
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nTestType == 10)
					{
						// TCP Read ( Buff )
						nProcessStep[nStepIndex] = 22100;
					}
					break;



				// Modbus Can Write
				case 21100:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID.ToString(), out _SysInfo.nCanCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue1.ToString(), out _SysInfo.nCanAddr);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue2.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nCanData);
					//tMainTimer[nStepIndex].Start(1000);
					SendWriteCommand(_SysInfo.nCanCh, _SysInfo.nCanAddr, _SysInfo.nCanData, 1);
					nProcessStep[nStepIndex]++;
					break;

				case 21101:
					//if (!tMainTimer[nStepIndex].Verify()) { break; }
					//if (_CanComm[_SysInfo.nCanCh].bReadMessage)
					//{
					//	if (_CanComm[_SysInfo.nCanCh].btReadData[0] == 0x06)
					//	{
					//		//if ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3] == _SysInfo.nCanData)
					//		//{

					//		//}
					//		_SysInfo.nSubWorkStep++;
					//		nProcessStep[nStepIndex] = 20000;
					//	}
					//}
					_SysInfo.nSubWorkStep++;
					nProcessStep[nStepIndex] = 20000;
					break;


				// Modbus Can Read(COMP)
				case 21200:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID.ToString(), out _SysInfo.nCanCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue1.ToString(), out _SysInfo.nCanAddr);
					//int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue2.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nCanData);
					_SysInfo.nCanData = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue2.ToString());
					SendReadCommand(_SysInfo.nCanCh, _SysInfo.nCanAddr ,1);

					tMainTimer[nStepIndex].Start(5000);
					nProcessStep[nStepIndex]++;
					break;

				case 21201:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), "TIME OUT", "NG");
						_SysInfo.bEolNg = true;
						_SysInfo.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}


					if (_CanComm[_SysInfo.nCanCh].bReadMessage)
					{
						if (_CanComm[_SysInfo.nCanCh].btReadData[0] == 0x03)
						{
							if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue3 != null && _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue3 != "")
							{
								if (int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue3.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMaskingData))
								{
									_SysInfo.nCompData = ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3]) & _SysInfo.nMaskingData;

									if (_SysInfo.nCompData == _SysInfo.nCanData)
									{
										NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3]).ToString("X4") + $"({(_SysInfo.nMaskingData.ToString("X4"))})", "OK");
										//AppendLogMsg($"OK / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
									}
									else
									{
										_SysInfo.bEolNg = true;
										NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3]).ToString("X4") + $"({(_SysInfo.nMaskingData.ToString("X4"))})", "NG");
										//AppendLogMsg($"NG / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
									}
								}
								else
								{
									_SysInfo.nCompData = ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3]);
									if (_SysInfo.nCompData == _SysInfo.nCanData)
									{
										NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), _SysInfo.nCompData.ToString("X4"), "OK");
										//AppendLogMsg($"OK / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
									}
									else
									{
										_SysInfo.bEolNg = true;
										NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), _SysInfo.nCompData.ToString("X4"), "NG");
										//AppendLogMsg($"NG / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
									}
								}
							}
							else
							{
								_SysInfo.nCompData = ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3]);
								if (_SysInfo.nCompData == _SysInfo.nCanData)
								{
									NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), _SysInfo.nCompData.ToString("X4"), "OK");
									//AppendLogMsg($"OK / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
								}
								else
								{
									_SysInfo.bEolNg = true;
									NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), _SysInfo.nCompData.ToString("X4"), "NG");
									//AppendLogMsg($"NG / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
								}
							}


						}
						else
						{
							_SysInfo.bEolNg = true;
							NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), "DATA ERROR", "NG");
						}
						_SysInfo.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;

				case 21300:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID.ToString(), out _SysInfo.nCanCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue1.ToString(), out _SysInfo.nCanAddr);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue2.ToString(), out _SysInfo.nBuffIndex);
					tMainTimer[nStepIndex].Start(5000);
					SendReadCommand(_SysInfo.nCanCh, _SysInfo.nCanAddr, 1);
					nProcessStep[nStepIndex]++;
					break;

				case 21301:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), "", "TIME OUT", "NG");
						_SysInfo.bEolNg = true;
						_SysInfo.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}

					if (_CanComm[_SysInfo.nCanCh].bReadMessage)
					{
						if (_CanComm[_SysInfo.nCanCh].btReadData[0] == 0x03)
						{
							_SysInfo.nReadDataBuff[_SysInfo.nBuffIndex] = (_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3];
							NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), $"BUFFER{_SysInfo.nBuffIndex}", ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3]).ToString("X4"), "");
						}
						_SysInfo.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;


				case 21400:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue1.ToString(), out _SysInfo.nCanAddr);
					tMainTimer[nStepIndex].Start(_SysInfo.nCanAddr);
					nProcessStep[nStepIndex]++;
					break;

				case 21401:
					if (!tMainTimer[nStepIndex].Verify()) { break; }

					_SysInfo.nSubWorkStep++;
					nProcessStep[nStepIndex] = 20000;
					break;


				case 21500:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID.ToString(), out _SysInfo.nCanCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue1.ToString(), out _SysInfo.nCanAddr);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue2.ToString(), out _SysInfo.nCanMultiCount);
					if (_SysInfo.nCanMultiCount > 0) { _SysInfo.nMultiSendData[0] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue3.ToString()); }
					if (_SysInfo.nCanMultiCount > 1) { _SysInfo.nMultiSendData[1] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue4.ToString()); }
					if (_SysInfo.nCanMultiCount > 2) { _SysInfo.nMultiSendData[2] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue5.ToString()); }
					if (_SysInfo.nCanMultiCount > 3) { _SysInfo.nMultiSendData[3] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue6.ToString()); }
					if (_SysInfo.nCanMultiCount > 4) { _SysInfo.nMultiSendData[4] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue7.ToString()); }
					if (_SysInfo.nCanMultiCount > 5) { _SysInfo.nMultiSendData[5] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue8.ToString()); }
					if (_SysInfo.nCanMultiCount > 6) { _SysInfo.nMultiSendData[6] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue9.ToString()); }
					if (_SysInfo.nCanMultiCount > 7) { _SysInfo.nMultiSendData[7] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue10.ToString()); }
					if (_SysInfo.nCanMultiCount > 8) { _SysInfo.nMultiSendData[8] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue11.ToString()); }
					if (_SysInfo.nCanMultiCount > 9) { _SysInfo.nMultiSendData[9] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue12.ToString()); }
					if (_SysInfo.nCanMultiCount > 10) { _SysInfo.nMultiSendData[10] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue13.ToString()); }
					if (_SysInfo.nCanMultiCount > 11) { _SysInfo.nMultiSendData[11] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue14.ToString()); }
					if (_SysInfo.nCanMultiCount > 12) { _SysInfo.nMultiSendData[12] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue15.ToString()); }
					if (_SysInfo.nCanMultiCount > 13) { _SysInfo.nMultiSendData[13] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue16.ToString()); }
					if (_SysInfo.nCanMultiCount > 14) { _SysInfo.nMultiSendData[14] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue17.ToString()); }
					if (_SysInfo.nCanMultiCount > 15) { _SysInfo.nMultiSendData[15] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue18.ToString()); }
					if (_SysInfo.nCanMultiCount > 16) { _SysInfo.nMultiSendData[16] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue19.ToString()); }
					if (_SysInfo.nCanMultiCount > 17) { _SysInfo.nMultiSendData[17] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue20.ToString()); }
					if (_SysInfo.nCanMultiCount > 18) { _SysInfo.nMultiSendData[18] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue21.ToString()); }
					if (_SysInfo.nCanMultiCount > 19) { _SysInfo.nMultiSendData[19] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue22.ToString()); }
					if (_SysInfo.nCanMultiCount > 20) { _SysInfo.nMultiSendData[20] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue23.ToString()); }
					if (_SysInfo.nCanMultiCount > 21) { _SysInfo.nMultiSendData[21] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue24.ToString()); }
					if (_SysInfo.nCanMultiCount > 22) { _SysInfo.nMultiSendData[22] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue25.ToString()); }
					if (_SysInfo.nCanMultiCount > 23) { _SysInfo.nMultiSendData[23] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue26.ToString()); }
					if (_SysInfo.nCanMultiCount > 24) { _SysInfo.nMultiSendData[24] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue27.ToString()); }
					if (_SysInfo.nCanMultiCount > 25) { _SysInfo.nMultiSendData[25] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue28.ToString()); }
					if (_SysInfo.nCanMultiCount > 26) { _SysInfo.nMultiSendData[26] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue29.ToString()); }
					if (_SysInfo.nCanMultiCount > 27) { _SysInfo.nMultiSendData[27] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue30.ToString()); }
					if (_SysInfo.nCanMultiCount > 28) { _SysInfo.nMultiSendData[28] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue31.ToString()); }
					if (_SysInfo.nCanMultiCount > 29) { _SysInfo.nMultiSendData[29] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue32.ToString()); }
					if (_SysInfo.nCanMultiCount > 30) { _SysInfo.nMultiSendData[30] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue33.ToString()); }
					if (_SysInfo.nCanMultiCount > 31) { _SysInfo.nMultiSendData[31] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue34.ToString()); }
					if (_SysInfo.nCanMultiCount > 32) { _SysInfo.nMultiSendData[32] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue35.ToString()); }
					if (_SysInfo.nCanMultiCount > 33) { _SysInfo.nMultiSendData[33] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue36.ToString()); }
					if (_SysInfo.nCanMultiCount > 34) { _SysInfo.nMultiSendData[34] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue37.ToString()); }
					if (_SysInfo.nCanMultiCount > 35) { _SysInfo.nMultiSendData[35] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue38.ToString()); }
					if (_SysInfo.nCanMultiCount > 36) { _SysInfo.nMultiSendData[36] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue39.ToString()); }
					if (_SysInfo.nCanMultiCount > 37) { _SysInfo.nMultiSendData[37] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue40.ToString()); }
					if (_SysInfo.nCanMultiCount > 38) { _SysInfo.nMultiSendData[38] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue41.ToString()); }
					if (_SysInfo.nCanMultiCount > 39) { _SysInfo.nMultiSendData[39] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue42.ToString()); }
					if (_SysInfo.nCanMultiCount > 40) { _SysInfo.nMultiSendData[40] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue43.ToString()); }
					if (_SysInfo.nCanMultiCount > 41) { _SysInfo.nMultiSendData[41] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue44.ToString()); }
					if (_SysInfo.nCanMultiCount > 42) { _SysInfo.nMultiSendData[42] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue45.ToString()); }
					if (_SysInfo.nCanMultiCount > 43) { _SysInfo.nMultiSendData[43] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue46.ToString()); }
					if (_SysInfo.nCanMultiCount > 44) { _SysInfo.nMultiSendData[44] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue47.ToString()); }
					if (_SysInfo.nCanMultiCount > 45) { _SysInfo.nMultiSendData[45] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue48.ToString()); }
					if (_SysInfo.nCanMultiCount > 46) { _SysInfo.nMultiSendData[46] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue49.ToString()); }
					if (_SysInfo.nCanMultiCount > 47) { _SysInfo.nMultiSendData[47] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue50.ToString()); }
					if (_SysInfo.nCanMultiCount > 48) { _SysInfo.nMultiSendData[48] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue51.ToString()); }
					if (_SysInfo.nCanMultiCount > 49) { _SysInfo.nMultiSendData[49] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue52.ToString()); }
					if (_SysInfo.nCanMultiCount > 50) { _SysInfo.nMultiSendData[50] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue53.ToString()); }
					if (_SysInfo.nCanMultiCount > 51) { _SysInfo.nMultiSendData[51] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue54.ToString()); }
					if (_SysInfo.nCanMultiCount > 52) { _SysInfo.nMultiSendData[52] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue55.ToString()); }
					if (_SysInfo.nCanMultiCount > 53) { _SysInfo.nMultiSendData[53] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue56.ToString()); }
					if (_SysInfo.nCanMultiCount > 54) { _SysInfo.nMultiSendData[54] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue57.ToString()); }
					if (_SysInfo.nCanMultiCount > 55) { _SysInfo.nMultiSendData[55] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue58.ToString()); }
					if (_SysInfo.nCanMultiCount > 56) { _SysInfo.nMultiSendData[56] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue59.ToString()); }
					if (_SysInfo.nCanMultiCount > 57) { _SysInfo.nMultiSendData[57] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue60.ToString()); }
					if (_SysInfo.nCanMultiCount > 58) { _SysInfo.nMultiSendData[58] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue61.ToString()); }
					if (_SysInfo.nCanMultiCount > 59) { _SysInfo.nMultiSendData[59] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue62.ToString()); }
					if (_SysInfo.nCanMultiCount > 60) { _SysInfo.nMultiSendData[60] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue63.ToString()); }
					if (_SysInfo.nCanMultiCount > 61) { _SysInfo.nMultiSendData[61] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue64.ToString()); }
					if (_SysInfo.nCanMultiCount > 62) { _SysInfo.nMultiSendData[62] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue65.ToString()); }
					if (_SysInfo.nCanMultiCount > 63) { _SysInfo.nMultiSendData[63] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue66.ToString()); }
					if (_SysInfo.nCanMultiCount > 64) { _SysInfo.nMultiSendData[64] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue67.ToString()); }
					if (_SysInfo.nCanMultiCount > 65) { _SysInfo.nMultiSendData[65] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue68.ToString()); }
					if (_SysInfo.nCanMultiCount > 66) { _SysInfo.nMultiSendData[66] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue69.ToString()); }
					if (_SysInfo.nCanMultiCount > 67) { _SysInfo.nMultiSendData[67] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue70.ToString()); }
					if (_SysInfo.nCanMultiCount > 68) { _SysInfo.nMultiSendData[68] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue71.ToString()); }
					if (_SysInfo.nCanMultiCount > 69) { _SysInfo.nMultiSendData[69] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue72.ToString()); }
					if (_SysInfo.nCanMultiCount > 70) { _SysInfo.nMultiSendData[70] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue73.ToString()); }
					if (_SysInfo.nCanMultiCount > 71) { _SysInfo.nMultiSendData[71] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue74.ToString()); }
					if (_SysInfo.nCanMultiCount > 72) { _SysInfo.nMultiSendData[72] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue75.ToString()); }
					if (_SysInfo.nCanMultiCount > 73) { _SysInfo.nMultiSendData[73] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue76.ToString()); }
					if (_SysInfo.nCanMultiCount > 74) { _SysInfo.nMultiSendData[74] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue77.ToString()); }
					if (_SysInfo.nCanMultiCount > 75) { _SysInfo.nMultiSendData[75] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue78.ToString()); }
					if (_SysInfo.nCanMultiCount > 76) { _SysInfo.nMultiSendData[76] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue79.ToString()); }
					if (_SysInfo.nCanMultiCount > 77) { _SysInfo.nMultiSendData[77] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue80.ToString()); }
					if (_SysInfo.nCanMultiCount > 78) { _SysInfo.nMultiSendData[78] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue81.ToString()); }
					if (_SysInfo.nCanMultiCount > 79) { _SysInfo.nMultiSendData[79] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue82.ToString()); }
					if (_SysInfo.nCanMultiCount > 80) { _SysInfo.nMultiSendData[80] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue83.ToString()); }
					if (_SysInfo.nCanMultiCount > 81) { _SysInfo.nMultiSendData[81] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue84.ToString()); }
					if (_SysInfo.nCanMultiCount > 82) { _SysInfo.nMultiSendData[82] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue85.ToString()); }
					if (_SysInfo.nCanMultiCount > 83) { _SysInfo.nMultiSendData[83] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue86.ToString()); }
					if (_SysInfo.nCanMultiCount > 84) { _SysInfo.nMultiSendData[84] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue87.ToString()); }
					if (_SysInfo.nCanMultiCount > 85) { _SysInfo.nMultiSendData[85] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue88.ToString()); }
					if (_SysInfo.nCanMultiCount > 86) { _SysInfo.nMultiSendData[86] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue89.ToString()); }
					if (_SysInfo.nCanMultiCount > 87) { _SysInfo.nMultiSendData[87] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue90.ToString()); }
					if (_SysInfo.nCanMultiCount > 88) { _SysInfo.nMultiSendData[88] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue91.ToString()); }
					if (_SysInfo.nCanMultiCount > 89) { _SysInfo.nMultiSendData[89] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue92.ToString()); }
					if (_SysInfo.nCanMultiCount > 90) { _SysInfo.nMultiSendData[90] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue93.ToString()); }
					if (_SysInfo.nCanMultiCount > 91) { _SysInfo.nMultiSendData[91] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue94.ToString()); }
					if (_SysInfo.nCanMultiCount > 92) { _SysInfo.nMultiSendData[92] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue95.ToString()); }
					if (_SysInfo.nCanMultiCount > 93) { _SysInfo.nMultiSendData[93] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue96.ToString()); }
					if (_SysInfo.nCanMultiCount > 94) { _SysInfo.nMultiSendData[94] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue97.ToString()); }
					if (_SysInfo.nCanMultiCount > 95) { _SysInfo.nMultiSendData[95] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue98.ToString()); }
					if (_SysInfo.nCanMultiCount > 96) { _SysInfo.nMultiSendData[96] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue99.ToString()); }
					if (_SysInfo.nCanMultiCount > 97) { _SysInfo.nMultiSendData[97] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue100.ToString()); }

					//if (_SysInfo.nCanMultiCount > 0) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue3.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[0]); }
					//if (_SysInfo.nCanMultiCount > 1) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue4.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[1]); }
					//if (_SysInfo.nCanMultiCount > 2) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue5.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[2]); }
					//if (_SysInfo.nCanMultiCount > 3) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue6.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[3]); }
					//if (_SysInfo.nCanMultiCount > 4) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue7.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[4]); }
					//if (_SysInfo.nCanMultiCount > 5) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue8.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[5]); }
					//if (_SysInfo.nCanMultiCount > 6) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue9.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[6]); }
					//if (_SysInfo.nCanMultiCount > 7) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue10.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[7]); }
					//if (_SysInfo.nCanMultiCount > 8) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue11.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[8]); }
					//if (_SysInfo.nCanMultiCount > 9) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue12.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[9]); }
					//if (_SysInfo.nCanMultiCount > 10) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue13.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[10]); }
					//if (_SysInfo.nCanMultiCount > 11) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue14.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[11]); }
					//if (_SysInfo.nCanMultiCount > 12) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue15.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[12]); }
					//if (_SysInfo.nCanMultiCount > 13) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue16.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[13]); }
					//if (_SysInfo.nCanMultiCount > 14) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue17.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[14]); }
					//if (_SysInfo.nCanMultiCount > 15) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue18.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[15]); }
					//if (_SysInfo.nCanMultiCount > 16) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue19.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[16]); }
					//if (_SysInfo.nCanMultiCount > 17) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue20.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[17]); }

					//tMainTimer[nStepIndex].Start(1000);
					SendWriteMultiCommand(_SysInfo.nCanCh, _SysInfo.nCanAddr, _SysInfo.nMultiSendData, _SysInfo.nCanMultiCount, 1);
					nProcessStep[nStepIndex]++;
					break;

				case 21501:
					//if (!tMainTimer[nStepIndex].Verify()) { break; }
					//if (_CanComm[_SysInfo.nCanCh].bReadMessage)
					//{
					//	if (_CanComm[_SysInfo.nCanCh].btReadData[0] == 0x06)
					//	{
					//		//if ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3] == _SysInfo.nCanData)
					//		//{

					//		//}
					//		_SysInfo.nSubWorkStep++;
					//		nProcessStep[nStepIndex] = 20000;
					//	}
					//}
					_SysInfo.nSubWorkStep++;
					nProcessStep[nStepIndex] = 20000;
					break;


				// Can Write
				case 21600:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID.ToString(), out _SysInfo.nCanCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue1, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nCanStartAddr);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue2.ToString(), out _SysInfo.nCanMultiCount);
					if (_SysInfo.nCanMultiCount > 0) { _SysInfo.nMultiSendData[0] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue3.ToString()); }
					if (_SysInfo.nCanMultiCount > 1) { _SysInfo.nMultiSendData[1] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue4.ToString()); }
					if (_SysInfo.nCanMultiCount > 2) { _SysInfo.nMultiSendData[2] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue5.ToString()); }
					if (_SysInfo.nCanMultiCount > 3) { _SysInfo.nMultiSendData[3] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue6.ToString()); }
					if (_SysInfo.nCanMultiCount > 4) { _SysInfo.nMultiSendData[4] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue7.ToString()); }
					if (_SysInfo.nCanMultiCount > 5) { _SysInfo.nMultiSendData[5] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue8.ToString()); }
					if (_SysInfo.nCanMultiCount > 6) { _SysInfo.nMultiSendData[6] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue9.ToString()); }
					if (_SysInfo.nCanMultiCount > 7) { _SysInfo.nMultiSendData[7] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue10.ToString()); }
					if (_SysInfo.nCanMultiCount > 8) { _SysInfo.nMultiSendData[8] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue11.ToString()); }
					if (_SysInfo.nCanMultiCount > 9) { _SysInfo.nMultiSendData[9] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue12.ToString()); }
					if (_SysInfo.nCanMultiCount > 10) { _SysInfo.nMultiSendData[10] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue13.ToString()); }
					if (_SysInfo.nCanMultiCount > 11) { _SysInfo.nMultiSendData[11] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue14.ToString()); }
					if (_SysInfo.nCanMultiCount > 12) { _SysInfo.nMultiSendData[12] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue15.ToString()); }
					if (_SysInfo.nCanMultiCount > 13) { _SysInfo.nMultiSendData[13] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue16.ToString()); }
					if (_SysInfo.nCanMultiCount > 14) { _SysInfo.nMultiSendData[14] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue17.ToString()); }
					if (_SysInfo.nCanMultiCount > 15) { _SysInfo.nMultiSendData[15] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue18.ToString()); }
					if (_SysInfo.nCanMultiCount > 16) { _SysInfo.nMultiSendData[16] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue19.ToString()); }
					if (_SysInfo.nCanMultiCount > 17) { _SysInfo.nMultiSendData[17] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue20.ToString()); }
					if (_SysInfo.nCanMultiCount > 18) { _SysInfo.nMultiSendData[18] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue21.ToString()); }
					if (_SysInfo.nCanMultiCount > 19) { _SysInfo.nMultiSendData[19] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue22.ToString()); }
					if (_SysInfo.nCanMultiCount > 20) { _SysInfo.nMultiSendData[20] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue23.ToString()); }
					if (_SysInfo.nCanMultiCount > 21) { _SysInfo.nMultiSendData[21] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue24.ToString()); }
					if (_SysInfo.nCanMultiCount > 22) { _SysInfo.nMultiSendData[22] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue25.ToString()); }
					if (_SysInfo.nCanMultiCount > 23) { _SysInfo.nMultiSendData[23] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue26.ToString()); }
					if (_SysInfo.nCanMultiCount > 24) { _SysInfo.nMultiSendData[24] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue27.ToString()); }
					if (_SysInfo.nCanMultiCount > 25) { _SysInfo.nMultiSendData[25] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue28.ToString()); }
					if (_SysInfo.nCanMultiCount > 26) { _SysInfo.nMultiSendData[26] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue29.ToString()); }
					if (_SysInfo.nCanMultiCount > 27) { _SysInfo.nMultiSendData[27] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue30.ToString()); }
					if (_SysInfo.nCanMultiCount > 28) { _SysInfo.nMultiSendData[28] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue31.ToString()); }
					if (_SysInfo.nCanMultiCount > 29) { _SysInfo.nMultiSendData[29] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue32.ToString()); }
					if (_SysInfo.nCanMultiCount > 30) { _SysInfo.nMultiSendData[30] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue33.ToString()); }
					if (_SysInfo.nCanMultiCount > 31) { _SysInfo.nMultiSendData[31] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue34.ToString()); }
					if (_SysInfo.nCanMultiCount > 32) { _SysInfo.nMultiSendData[32] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue35.ToString()); }
					if (_SysInfo.nCanMultiCount > 33) { _SysInfo.nMultiSendData[33] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue36.ToString()); }
					if (_SysInfo.nCanMultiCount > 34) { _SysInfo.nMultiSendData[34] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue37.ToString()); }
					if (_SysInfo.nCanMultiCount > 35) { _SysInfo.nMultiSendData[35] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue38.ToString()); }
					if (_SysInfo.nCanMultiCount > 36) { _SysInfo.nMultiSendData[36] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue39.ToString()); }
					if (_SysInfo.nCanMultiCount > 37) { _SysInfo.nMultiSendData[37] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue40.ToString()); }
					if (_SysInfo.nCanMultiCount > 38) { _SysInfo.nMultiSendData[38] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue41.ToString()); }
					if (_SysInfo.nCanMultiCount > 39) { _SysInfo.nMultiSendData[39] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue42.ToString()); }
					if (_SysInfo.nCanMultiCount > 40) { _SysInfo.nMultiSendData[40] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue43.ToString()); }
					if (_SysInfo.nCanMultiCount > 41) { _SysInfo.nMultiSendData[41] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue44.ToString()); }
					if (_SysInfo.nCanMultiCount > 42) { _SysInfo.nMultiSendData[42] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue45.ToString()); }
					if (_SysInfo.nCanMultiCount > 43) { _SysInfo.nMultiSendData[43] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue46.ToString()); }
					if (_SysInfo.nCanMultiCount > 44) { _SysInfo.nMultiSendData[44] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue47.ToString()); }
					if (_SysInfo.nCanMultiCount > 45) { _SysInfo.nMultiSendData[45] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue48.ToString()); }
					if (_SysInfo.nCanMultiCount > 46) { _SysInfo.nMultiSendData[46] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue49.ToString()); }
					if (_SysInfo.nCanMultiCount > 47) { _SysInfo.nMultiSendData[47] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue50.ToString()); }
					if (_SysInfo.nCanMultiCount > 48) { _SysInfo.nMultiSendData[48] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue51.ToString()); }
					if (_SysInfo.nCanMultiCount > 49) { _SysInfo.nMultiSendData[49] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue52.ToString()); }
					if (_SysInfo.nCanMultiCount > 50) { _SysInfo.nMultiSendData[50] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue53.ToString()); }
					if (_SysInfo.nCanMultiCount > 51) { _SysInfo.nMultiSendData[51] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue54.ToString()); }
					if (_SysInfo.nCanMultiCount > 52) { _SysInfo.nMultiSendData[52] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue55.ToString()); }
					if (_SysInfo.nCanMultiCount > 53) { _SysInfo.nMultiSendData[53] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue56.ToString()); }
					if (_SysInfo.nCanMultiCount > 54) { _SysInfo.nMultiSendData[54] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue57.ToString()); }
					if (_SysInfo.nCanMultiCount > 55) { _SysInfo.nMultiSendData[55] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue58.ToString()); }
					if (_SysInfo.nCanMultiCount > 56) { _SysInfo.nMultiSendData[56] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue59.ToString()); }
					if (_SysInfo.nCanMultiCount > 57) { _SysInfo.nMultiSendData[57] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue60.ToString()); }
					if (_SysInfo.nCanMultiCount > 58) { _SysInfo.nMultiSendData[58] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue61.ToString()); }
					if (_SysInfo.nCanMultiCount > 59) { _SysInfo.nMultiSendData[59] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue62.ToString()); }
					if (_SysInfo.nCanMultiCount > 60) { _SysInfo.nMultiSendData[60] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue63.ToString()); }
					if (_SysInfo.nCanMultiCount > 61) { _SysInfo.nMultiSendData[61] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue64.ToString()); }
					if (_SysInfo.nCanMultiCount > 62) { _SysInfo.nMultiSendData[62] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue65.ToString()); }
					if (_SysInfo.nCanMultiCount > 63) { _SysInfo.nMultiSendData[63] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue66.ToString()); }
					if (_SysInfo.nCanMultiCount > 64) { _SysInfo.nMultiSendData[64] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue67.ToString()); }
					if (_SysInfo.nCanMultiCount > 65) { _SysInfo.nMultiSendData[65] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue68.ToString()); }
					if (_SysInfo.nCanMultiCount > 66) { _SysInfo.nMultiSendData[66] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue69.ToString()); }
					if (_SysInfo.nCanMultiCount > 67) { _SysInfo.nMultiSendData[67] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue70.ToString()); }
					if (_SysInfo.nCanMultiCount > 68) { _SysInfo.nMultiSendData[68] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue71.ToString()); }
					if (_SysInfo.nCanMultiCount > 69) { _SysInfo.nMultiSendData[69] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue72.ToString()); }
					if (_SysInfo.nCanMultiCount > 70) { _SysInfo.nMultiSendData[70] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue73.ToString()); }
					if (_SysInfo.nCanMultiCount > 71) { _SysInfo.nMultiSendData[71] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue74.ToString()); }
					if (_SysInfo.nCanMultiCount > 72) { _SysInfo.nMultiSendData[72] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue75.ToString()); }
					if (_SysInfo.nCanMultiCount > 73) { _SysInfo.nMultiSendData[73] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue76.ToString()); }
					if (_SysInfo.nCanMultiCount > 74) { _SysInfo.nMultiSendData[74] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue77.ToString()); }
					if (_SysInfo.nCanMultiCount > 75) { _SysInfo.nMultiSendData[75] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue78.ToString()); }
					if (_SysInfo.nCanMultiCount > 76) { _SysInfo.nMultiSendData[76] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue79.ToString()); }
					if (_SysInfo.nCanMultiCount > 77) { _SysInfo.nMultiSendData[77] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue80.ToString()); }
					if (_SysInfo.nCanMultiCount > 78) { _SysInfo.nMultiSendData[78] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue81.ToString()); }
					if (_SysInfo.nCanMultiCount > 79) { _SysInfo.nMultiSendData[79] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue82.ToString()); }
					if (_SysInfo.nCanMultiCount > 80) { _SysInfo.nMultiSendData[80] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue83.ToString()); }
					if (_SysInfo.nCanMultiCount > 81) { _SysInfo.nMultiSendData[81] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue84.ToString()); }
					if (_SysInfo.nCanMultiCount > 82) { _SysInfo.nMultiSendData[82] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue85.ToString()); }
					if (_SysInfo.nCanMultiCount > 83) { _SysInfo.nMultiSendData[83] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue86.ToString()); }
					if (_SysInfo.nCanMultiCount > 84) { _SysInfo.nMultiSendData[84] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue87.ToString()); }
					if (_SysInfo.nCanMultiCount > 85) { _SysInfo.nMultiSendData[85] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue88.ToString()); }
					if (_SysInfo.nCanMultiCount > 86) { _SysInfo.nMultiSendData[86] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue89.ToString()); }
					if (_SysInfo.nCanMultiCount > 87) { _SysInfo.nMultiSendData[87] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue90.ToString()); }
					if (_SysInfo.nCanMultiCount > 88) { _SysInfo.nMultiSendData[88] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue91.ToString()); }
					if (_SysInfo.nCanMultiCount > 89) { _SysInfo.nMultiSendData[89] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue92.ToString()); }
					if (_SysInfo.nCanMultiCount > 90) { _SysInfo.nMultiSendData[90] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue93.ToString()); }
					if (_SysInfo.nCanMultiCount > 91) { _SysInfo.nMultiSendData[91] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue94.ToString()); }
					if (_SysInfo.nCanMultiCount > 92) { _SysInfo.nMultiSendData[92] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue95.ToString()); }
					if (_SysInfo.nCanMultiCount > 93) { _SysInfo.nMultiSendData[93] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue96.ToString()); }
					if (_SysInfo.nCanMultiCount > 94) { _SysInfo.nMultiSendData[94] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue97.ToString()); }
					if (_SysInfo.nCanMultiCount > 95) { _SysInfo.nMultiSendData[95] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue98.ToString()); }
					if (_SysInfo.nCanMultiCount > 96) { _SysInfo.nMultiSendData[96] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue99.ToString()); }
					if (_SysInfo.nCanMultiCount > 97) { _SysInfo.nMultiSendData[97] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue100.ToString()); }

					SendCanData(_SysInfo.nCanCh, _SysInfo.nCanStartAddr, _SysInfo.nMultiSendData, _SysInfo.nCanMultiCount, 1);
					_SysInfo.nSubWorkStep++;
					nProcessStep[nStepIndex] = 20000;
					break;

				case 21700:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID.ToString(), out _SysInfo.nCanCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue1, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nCanStartAddr);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue2.ToString(), out _SysInfo.nCanMultiCount);
					if (_SysInfo.nCanMultiCount > 0) { _SysInfo.strValueBuff[0] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue3; }
					if (_SysInfo.nCanMultiCount > 1) { _SysInfo.strValueBuff[1] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue4; }
					if (_SysInfo.nCanMultiCount > 2) { _SysInfo.strValueBuff[2] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue5; }
					if (_SysInfo.nCanMultiCount > 3) { _SysInfo.strValueBuff[3] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue6; }
					if (_SysInfo.nCanMultiCount > 4) { _SysInfo.strValueBuff[4] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue7; }
					if (_SysInfo.nCanMultiCount > 5) { _SysInfo.strValueBuff[5] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue8; }
					if (_SysInfo.nCanMultiCount > 6) { _SysInfo.strValueBuff[6] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue9; }
					if (_SysInfo.nCanMultiCount > 7) { _SysInfo.strValueBuff[7] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue10; }
					if (_SysInfo.nCanMultiCount > 8) { _SysInfo.strValueBuff[8] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue11; }
					if (_SysInfo.nCanMultiCount > 9) { _SysInfo.strValueBuff[9] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue12; }
					if (_SysInfo.nCanMultiCount > 10) { _SysInfo.strValueBuff[10] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue13; }
					if (_SysInfo.nCanMultiCount > 11) { _SysInfo.strValueBuff[11] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue14; }
					if (_SysInfo.nCanMultiCount > 12) { _SysInfo.strValueBuff[12] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue15; }
					if (_SysInfo.nCanMultiCount > 13) { _SysInfo.strValueBuff[13] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue16; }
					if (_SysInfo.nCanMultiCount > 14) { _SysInfo.strValueBuff[14] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue17; }
					if (_SysInfo.nCanMultiCount > 15) { _SysInfo.strValueBuff[15] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue18; }
					if (_SysInfo.nCanMultiCount > 16) { _SysInfo.strValueBuff[16] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue19; }
					if (_SysInfo.nCanMultiCount > 17) { _SysInfo.strValueBuff[17] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue20; }
					if (_SysInfo.nCanMultiCount > 18) { _SysInfo.strValueBuff[18] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue21; }
					if (_SysInfo.nCanMultiCount > 19) { _SysInfo.strValueBuff[19] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue22; }
					if (_SysInfo.nCanMultiCount > 20) { _SysInfo.strValueBuff[20] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue23; }
					if (_SysInfo.nCanMultiCount > 21) { _SysInfo.strValueBuff[21] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue24; }
					if (_SysInfo.nCanMultiCount > 22) { _SysInfo.strValueBuff[22] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue25; }
					if (_SysInfo.nCanMultiCount > 23) { _SysInfo.strValueBuff[23] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue26; }
					if (_SysInfo.nCanMultiCount > 24) { _SysInfo.strValueBuff[24] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue27; }
					if (_SysInfo.nCanMultiCount > 25) { _SysInfo.strValueBuff[25] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue28; }
					if (_SysInfo.nCanMultiCount > 26) { _SysInfo.strValueBuff[26] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue29; }
					if (_SysInfo.nCanMultiCount > 27) { _SysInfo.strValueBuff[27] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue30; }
					if (_SysInfo.nCanMultiCount > 28) { _SysInfo.strValueBuff[28] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue31; }
					if (_SysInfo.nCanMultiCount > 29) { _SysInfo.strValueBuff[29] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue32; }
					if (_SysInfo.nCanMultiCount > 30) { _SysInfo.strValueBuff[30] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue33; }
					if (_SysInfo.nCanMultiCount > 31) { _SysInfo.strValueBuff[31] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue34; }
					if (_SysInfo.nCanMultiCount > 32) { _SysInfo.strValueBuff[32] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue35; }
					if (_SysInfo.nCanMultiCount > 33) { _SysInfo.strValueBuff[33] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue36; }
					if (_SysInfo.nCanMultiCount > 34) { _SysInfo.strValueBuff[34] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue37; }
					if (_SysInfo.nCanMultiCount > 35) { _SysInfo.strValueBuff[35] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue38; }
					if (_SysInfo.nCanMultiCount > 36) { _SysInfo.strValueBuff[36] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue39; }
					if (_SysInfo.nCanMultiCount > 37) { _SysInfo.strValueBuff[37] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue40; }
					if (_SysInfo.nCanMultiCount > 38) { _SysInfo.strValueBuff[38] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue41; }
					if (_SysInfo.nCanMultiCount > 39) { _SysInfo.strValueBuff[39] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue42; }
					if (_SysInfo.nCanMultiCount > 40) { _SysInfo.strValueBuff[40] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue43; }
					if (_SysInfo.nCanMultiCount > 41) { _SysInfo.strValueBuff[41] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue44; }
					if (_SysInfo.nCanMultiCount > 42) { _SysInfo.strValueBuff[42] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue45; }
					if (_SysInfo.nCanMultiCount > 43) { _SysInfo.strValueBuff[43] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue46; }
					if (_SysInfo.nCanMultiCount > 44) { _SysInfo.strValueBuff[44] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue47; }
					if (_SysInfo.nCanMultiCount > 45) { _SysInfo.strValueBuff[45] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue48; }
					if (_SysInfo.nCanMultiCount > 46) { _SysInfo.strValueBuff[46] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue49; }
					if (_SysInfo.nCanMultiCount > 47) { _SysInfo.strValueBuff[47] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue50; }
					if (_SysInfo.nCanMultiCount > 48) { _SysInfo.strValueBuff[48] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue51; }
					if (_SysInfo.nCanMultiCount > 49) { _SysInfo.strValueBuff[49] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue52; }
					if (_SysInfo.nCanMultiCount > 50) { _SysInfo.strValueBuff[50] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue53; }
					if (_SysInfo.nCanMultiCount > 51) { _SysInfo.strValueBuff[51] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue54; }
					if (_SysInfo.nCanMultiCount > 52) { _SysInfo.strValueBuff[52] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue55; }
					if (_SysInfo.nCanMultiCount > 53) { _SysInfo.strValueBuff[53] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue56; }
					if (_SysInfo.nCanMultiCount > 54) { _SysInfo.strValueBuff[54] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue57; }
					if (_SysInfo.nCanMultiCount > 55) { _SysInfo.strValueBuff[55] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue58; }
					if (_SysInfo.nCanMultiCount > 56) { _SysInfo.strValueBuff[56] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue59; }
					if (_SysInfo.nCanMultiCount > 57) { _SysInfo.strValueBuff[57] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue60; }
					if (_SysInfo.nCanMultiCount > 58) { _SysInfo.strValueBuff[58] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue61; }
					if (_SysInfo.nCanMultiCount > 59) { _SysInfo.strValueBuff[59] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue62; }
					if (_SysInfo.nCanMultiCount > 60) { _SysInfo.strValueBuff[60] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue63; }
					if (_SysInfo.nCanMultiCount > 61) { _SysInfo.strValueBuff[61] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue64; }
					if (_SysInfo.nCanMultiCount > 62) { _SysInfo.strValueBuff[62] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue65; }
					if (_SysInfo.nCanMultiCount > 63) { _SysInfo.strValueBuff[63] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue66; }
					if (_SysInfo.nCanMultiCount > 64) { _SysInfo.strValueBuff[64] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue67; }
					if (_SysInfo.nCanMultiCount > 65) { _SysInfo.strValueBuff[65] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue68; }
					if (_SysInfo.nCanMultiCount > 66) { _SysInfo.strValueBuff[66] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue69; }
					if (_SysInfo.nCanMultiCount > 67) { _SysInfo.strValueBuff[67] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue70; }
					if (_SysInfo.nCanMultiCount > 68) { _SysInfo.strValueBuff[68] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue71; }
					if (_SysInfo.nCanMultiCount > 69) { _SysInfo.strValueBuff[69] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue72; }
					if (_SysInfo.nCanMultiCount > 70) { _SysInfo.strValueBuff[70] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue73; }
					if (_SysInfo.nCanMultiCount > 71) { _SysInfo.strValueBuff[71] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue74; }
					if (_SysInfo.nCanMultiCount > 72) { _SysInfo.strValueBuff[72] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue75; }
					if (_SysInfo.nCanMultiCount > 73) { _SysInfo.strValueBuff[73] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue76; }
					if (_SysInfo.nCanMultiCount > 74) { _SysInfo.strValueBuff[74] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue77; }
					if (_SysInfo.nCanMultiCount > 75) { _SysInfo.strValueBuff[75] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue78; }
					if (_SysInfo.nCanMultiCount > 76) { _SysInfo.strValueBuff[76] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue79; }
					if (_SysInfo.nCanMultiCount > 77) { _SysInfo.strValueBuff[77] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue80; }
					if (_SysInfo.nCanMultiCount > 78) { _SysInfo.strValueBuff[78] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue81; }
					if (_SysInfo.nCanMultiCount > 79) { _SysInfo.strValueBuff[79] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue82; }
					if (_SysInfo.nCanMultiCount > 80) { _SysInfo.strValueBuff[80] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue83; }
					if (_SysInfo.nCanMultiCount > 81) { _SysInfo.strValueBuff[81] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue84; }
					if (_SysInfo.nCanMultiCount > 82) { _SysInfo.strValueBuff[82] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue85; }
					if (_SysInfo.nCanMultiCount > 83) { _SysInfo.strValueBuff[83] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue86; }
					if (_SysInfo.nCanMultiCount > 84) { _SysInfo.strValueBuff[84] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue87; }
					if (_SysInfo.nCanMultiCount > 85) { _SysInfo.strValueBuff[85] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue88; }
					if (_SysInfo.nCanMultiCount > 86) { _SysInfo.strValueBuff[86] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue89; }
					if (_SysInfo.nCanMultiCount > 87) { _SysInfo.strValueBuff[87] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue90; }
					if (_SysInfo.nCanMultiCount > 88) { _SysInfo.strValueBuff[88] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue91; }
					if (_SysInfo.nCanMultiCount > 89) { _SysInfo.strValueBuff[89] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue92; }
					if (_SysInfo.nCanMultiCount > 90) { _SysInfo.strValueBuff[90] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue93; }
					if (_SysInfo.nCanMultiCount > 91) { _SysInfo.strValueBuff[91] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue94; }
					if (_SysInfo.nCanMultiCount > 92) { _SysInfo.strValueBuff[92] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue95; }
					if (_SysInfo.nCanMultiCount > 93) { _SysInfo.strValueBuff[93] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue96; }
					if (_SysInfo.nCanMultiCount > 94) { _SysInfo.strValueBuff[94] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue97; }
					if (_SysInfo.nCanMultiCount > 95) { _SysInfo.strValueBuff[95] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue98; }
					if (_SysInfo.nCanMultiCount > 96) { _SysInfo.strValueBuff[96] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue99; }
					if (_SysInfo.nCanMultiCount > 97) { _SysInfo.strValueBuff[97] = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue100; }

					tMainTimer[nStepIndex].Start(5000);
					nProcessStep[nStepIndex]++;
					break;

				case 21701:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString("X"), "", "TIME OUT", "NG");
						_SysInfo.bEolNg = true;
						_SysInfo.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}

					if (_CanComm[_SysInfo.nCanCh].bReadMessage && _CanComm[_SysInfo.nCanCh].nReadid == _SysInfo.nCanStartAddr && _CanComm[_SysInfo.nCanCh].nReaddlc == _SysInfo.nCanMultiCount)
					{
						bool bCompResult = true;
						string strSource = "";
						string strRead = "";

						for (int i = 0; i < _SysInfo.nCanMultiCount; i++)
						{
							strSource += _SysInfo.strValueBuff[i];
							strRead += _CanComm[_SysInfo.nCanCh].btReadData[i].ToString("X2");

							if (!GetCompareCanData(_SysInfo.strValueBuff[i], _CanComm[_SysInfo.nCanCh].btReadData[i]))
							{
								bCompResult = false;
							}
						}

						if (bCompResult)
						{
							NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString("X"), strSource, strRead, "OK");
						}
						else
						{
							NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString("X"), strSource, strRead, "NG");
							_SysInfo.bEolNg = true;
						}

						_SysInfo.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;




				case 21800:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID.ToString(), out _SysInfo.nCanCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue1, out _SysInfo.nCanAddr);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue2.ToString(), out _SysInfo.nCanMultiCount);
					if (_SysInfo.nCanMultiCount > 0) { _SysInfo.nMultiSendData[0] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue3.ToString()); }
					if (_SysInfo.nCanMultiCount > 1) { _SysInfo.nMultiSendData[1] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue4.ToString()); }
					if (_SysInfo.nCanMultiCount > 2) { _SysInfo.nMultiSendData[2] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue5.ToString()); }
					if (_SysInfo.nCanMultiCount > 3) { _SysInfo.nMultiSendData[3] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue6.ToString()); }
					if (_SysInfo.nCanMultiCount > 4) { _SysInfo.nMultiSendData[4] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue7.ToString()); }
					if (_SysInfo.nCanMultiCount > 5) { _SysInfo.nMultiSendData[5] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue8.ToString()); }
					if (_SysInfo.nCanMultiCount > 6) { _SysInfo.nMultiSendData[6] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue9.ToString()); }
					if (_SysInfo.nCanMultiCount > 7) { _SysInfo.nMultiSendData[7] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue10.ToString()); }
					if (_SysInfo.nCanMultiCount > 8) { _SysInfo.nMultiSendData[8] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue11.ToString()); }
					if (_SysInfo.nCanMultiCount > 9) { _SysInfo.nMultiSendData[9] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue12.ToString()); }
					if (_SysInfo.nCanMultiCount > 10) { _SysInfo.nMultiSendData[10] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue13.ToString()); }
					if (_SysInfo.nCanMultiCount > 11) { _SysInfo.nMultiSendData[11] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue14.ToString()); }
					if (_SysInfo.nCanMultiCount > 12) { _SysInfo.nMultiSendData[12] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue15.ToString()); }
					if (_SysInfo.nCanMultiCount > 13) { _SysInfo.nMultiSendData[13] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue16.ToString()); }
					if (_SysInfo.nCanMultiCount > 14) { _SysInfo.nMultiSendData[14] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue17.ToString()); }
					if (_SysInfo.nCanMultiCount > 15) { _SysInfo.nMultiSendData[15] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue18.ToString()); }
					if (_SysInfo.nCanMultiCount > 16) { _SysInfo.nMultiSendData[16] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue19.ToString()); }
					if (_SysInfo.nCanMultiCount > 17) { _SysInfo.nMultiSendData[17] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue20.ToString()); }
					if (_SysInfo.nCanMultiCount > 18) { _SysInfo.nMultiSendData[18] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue21.ToString()); }
					if (_SysInfo.nCanMultiCount > 19) { _SysInfo.nMultiSendData[19] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue22.ToString()); }
					if (_SysInfo.nCanMultiCount > 20) { _SysInfo.nMultiSendData[20] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue23.ToString()); }
					if (_SysInfo.nCanMultiCount > 21) { _SysInfo.nMultiSendData[21] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue24.ToString()); }
					if (_SysInfo.nCanMultiCount > 22) { _SysInfo.nMultiSendData[22] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue25.ToString()); }
					if (_SysInfo.nCanMultiCount > 23) { _SysInfo.nMultiSendData[23] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue26.ToString()); }
					if (_SysInfo.nCanMultiCount > 24) { _SysInfo.nMultiSendData[24] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue27.ToString()); }
					if (_SysInfo.nCanMultiCount > 25) { _SysInfo.nMultiSendData[25] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue28.ToString()); }
					if (_SysInfo.nCanMultiCount > 26) { _SysInfo.nMultiSendData[26] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue29.ToString()); }
					if (_SysInfo.nCanMultiCount > 27) { _SysInfo.nMultiSendData[27] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue30.ToString()); }
					if (_SysInfo.nCanMultiCount > 28) { _SysInfo.nMultiSendData[28] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue31.ToString()); }
					if (_SysInfo.nCanMultiCount > 29) { _SysInfo.nMultiSendData[29] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue32.ToString()); }
					if (_SysInfo.nCanMultiCount > 30) { _SysInfo.nMultiSendData[30] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue33.ToString()); }
					if (_SysInfo.nCanMultiCount > 31) { _SysInfo.nMultiSendData[31] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue34.ToString()); }
					if (_SysInfo.nCanMultiCount > 32) { _SysInfo.nMultiSendData[32] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue35.ToString()); }
					if (_SysInfo.nCanMultiCount > 33) { _SysInfo.nMultiSendData[33] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue36.ToString()); }
					if (_SysInfo.nCanMultiCount > 34) { _SysInfo.nMultiSendData[34] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue37.ToString()); }
					if (_SysInfo.nCanMultiCount > 35) { _SysInfo.nMultiSendData[35] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue38.ToString()); }
					if (_SysInfo.nCanMultiCount > 36) { _SysInfo.nMultiSendData[36] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue39.ToString()); }
					if (_SysInfo.nCanMultiCount > 37) { _SysInfo.nMultiSendData[37] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue40.ToString()); }
					if (_SysInfo.nCanMultiCount > 38) { _SysInfo.nMultiSendData[38] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue41.ToString()); }
					if (_SysInfo.nCanMultiCount > 39) { _SysInfo.nMultiSendData[39] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue42.ToString()); }
					if (_SysInfo.nCanMultiCount > 40) { _SysInfo.nMultiSendData[40] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue43.ToString()); }
					if (_SysInfo.nCanMultiCount > 41) { _SysInfo.nMultiSendData[41] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue44.ToString()); }
					if (_SysInfo.nCanMultiCount > 42) { _SysInfo.nMultiSendData[42] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue45.ToString()); }
					if (_SysInfo.nCanMultiCount > 43) { _SysInfo.nMultiSendData[43] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue46.ToString()); }
					if (_SysInfo.nCanMultiCount > 44) { _SysInfo.nMultiSendData[44] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue47.ToString()); }
					if (_SysInfo.nCanMultiCount > 45) { _SysInfo.nMultiSendData[45] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue48.ToString()); }
					if (_SysInfo.nCanMultiCount > 46) { _SysInfo.nMultiSendData[46] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue49.ToString()); }
					if (_SysInfo.nCanMultiCount > 47) { _SysInfo.nMultiSendData[47] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue50.ToString()); }
					if (_SysInfo.nCanMultiCount > 48) { _SysInfo.nMultiSendData[48] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue51.ToString()); }
					if (_SysInfo.nCanMultiCount > 49) { _SysInfo.nMultiSendData[49] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue52.ToString()); }
					if (_SysInfo.nCanMultiCount > 50) { _SysInfo.nMultiSendData[50] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue53.ToString()); }
					if (_SysInfo.nCanMultiCount > 51) { _SysInfo.nMultiSendData[51] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue54.ToString()); }
					if (_SysInfo.nCanMultiCount > 52) { _SysInfo.nMultiSendData[52] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue55.ToString()); }
					if (_SysInfo.nCanMultiCount > 53) { _SysInfo.nMultiSendData[53] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue56.ToString()); }
					if (_SysInfo.nCanMultiCount > 54) { _SysInfo.nMultiSendData[54] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue57.ToString()); }
					if (_SysInfo.nCanMultiCount > 55) { _SysInfo.nMultiSendData[55] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue58.ToString()); }
					if (_SysInfo.nCanMultiCount > 56) { _SysInfo.nMultiSendData[56] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue59.ToString()); }
					if (_SysInfo.nCanMultiCount > 57) { _SysInfo.nMultiSendData[57] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue60.ToString()); }
					if (_SysInfo.nCanMultiCount > 58) { _SysInfo.nMultiSendData[58] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue61.ToString()); }
					if (_SysInfo.nCanMultiCount > 59) { _SysInfo.nMultiSendData[59] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue62.ToString()); }
					if (_SysInfo.nCanMultiCount > 60) { _SysInfo.nMultiSendData[60] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue63.ToString()); }
					if (_SysInfo.nCanMultiCount > 61) { _SysInfo.nMultiSendData[61] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue64.ToString()); }
					if (_SysInfo.nCanMultiCount > 62) { _SysInfo.nMultiSendData[62] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue65.ToString()); }
					if (_SysInfo.nCanMultiCount > 63) { _SysInfo.nMultiSendData[63] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue66.ToString()); }
					if (_SysInfo.nCanMultiCount > 64) { _SysInfo.nMultiSendData[64] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue67.ToString()); }
					if (_SysInfo.nCanMultiCount > 65) { _SysInfo.nMultiSendData[65] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue68.ToString()); }
					if (_SysInfo.nCanMultiCount > 66) { _SysInfo.nMultiSendData[66] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue69.ToString()); }
					if (_SysInfo.nCanMultiCount > 67) { _SysInfo.nMultiSendData[67] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue70.ToString()); }
					if (_SysInfo.nCanMultiCount > 68) { _SysInfo.nMultiSendData[68] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue71.ToString()); }
					if (_SysInfo.nCanMultiCount > 69) { _SysInfo.nMultiSendData[69] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue72.ToString()); }
					if (_SysInfo.nCanMultiCount > 70) { _SysInfo.nMultiSendData[70] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue73.ToString()); }
					if (_SysInfo.nCanMultiCount > 71) { _SysInfo.nMultiSendData[71] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue74.ToString()); }
					if (_SysInfo.nCanMultiCount > 72) { _SysInfo.nMultiSendData[72] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue75.ToString()); }
					if (_SysInfo.nCanMultiCount > 73) { _SysInfo.nMultiSendData[73] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue76.ToString()); }
					if (_SysInfo.nCanMultiCount > 74) { _SysInfo.nMultiSendData[74] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue77.ToString()); }
					if (_SysInfo.nCanMultiCount > 75) { _SysInfo.nMultiSendData[75] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue78.ToString()); }
					if (_SysInfo.nCanMultiCount > 76) { _SysInfo.nMultiSendData[76] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue79.ToString()); }
					if (_SysInfo.nCanMultiCount > 77) { _SysInfo.nMultiSendData[77] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue80.ToString()); }
					if (_SysInfo.nCanMultiCount > 78) { _SysInfo.nMultiSendData[78] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue81.ToString()); }
					if (_SysInfo.nCanMultiCount > 79) { _SysInfo.nMultiSendData[79] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue82.ToString()); }
					if (_SysInfo.nCanMultiCount > 80) { _SysInfo.nMultiSendData[80] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue83.ToString()); }
					if (_SysInfo.nCanMultiCount > 81) { _SysInfo.nMultiSendData[81] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue84.ToString()); }
					if (_SysInfo.nCanMultiCount > 82) { _SysInfo.nMultiSendData[82] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue85.ToString()); }
					if (_SysInfo.nCanMultiCount > 83) { _SysInfo.nMultiSendData[83] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue86.ToString()); }
					if (_SysInfo.nCanMultiCount > 84) { _SysInfo.nMultiSendData[84] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue87.ToString()); }
					if (_SysInfo.nCanMultiCount > 85) { _SysInfo.nMultiSendData[85] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue88.ToString()); }
					if (_SysInfo.nCanMultiCount > 86) { _SysInfo.nMultiSendData[86] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue89.ToString()); }
					if (_SysInfo.nCanMultiCount > 87) { _SysInfo.nMultiSendData[87] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue90.ToString()); }
					if (_SysInfo.nCanMultiCount > 88) { _SysInfo.nMultiSendData[88] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue91.ToString()); }
					if (_SysInfo.nCanMultiCount > 89) { _SysInfo.nMultiSendData[89] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue92.ToString()); }
					if (_SysInfo.nCanMultiCount > 90) { _SysInfo.nMultiSendData[90] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue93.ToString()); }
					if (_SysInfo.nCanMultiCount > 91) { _SysInfo.nMultiSendData[91] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue94.ToString()); }
					if (_SysInfo.nCanMultiCount > 92) { _SysInfo.nMultiSendData[92] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue95.ToString()); }
					if (_SysInfo.nCanMultiCount > 93) { _SysInfo.nMultiSendData[93] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue96.ToString()); }
					if (_SysInfo.nCanMultiCount > 94) { _SysInfo.nMultiSendData[94] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue97.ToString()); }
					if (_SysInfo.nCanMultiCount > 95) { _SysInfo.nMultiSendData[95] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue98.ToString()); }
					if (_SysInfo.nCanMultiCount > 96) { _SysInfo.nMultiSendData[96] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue99.ToString()); }
					if (_SysInfo.nCanMultiCount > 97) { _SysInfo.nMultiSendData[97] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue100.ToString()); }


					SendTCPMultiCommand(_SysInfo.nCanCh, _SysInfo.nCanAddr, _SysInfo.nMultiSendData, _SysInfo.nCanMultiCount);
					nProcessStep[nStepIndex]++;
					break;

				case 21801:
					_SysInfo.nSubWorkStep++;
					nProcessStep[nStepIndex] = 20000;
					break;


				// Modbus TCP Read(COMP)
				case 21900:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID.ToString(), out _SysInfo.nCanCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue1.ToString(), out _SysInfo.nCanAddr);
					//int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue2.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nCanData);
					_SysInfo.nCanData = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue2.ToString());
					SendTCPReadCommand(_SysInfo.nCanCh, _SysInfo.nCanAddr);

					tMainTimer[nStepIndex].Start(5000);
					nProcessStep[nStepIndex]++;
					break;

				case 21901:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), "TIME OUT", "NG");
						_SysInfo.bEolNg = true;
						_SysInfo.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}


					if (_ModbusSoket.bResultOk)
					{

						if (_SysInfo.btTcpReadData[7] == 0x03)
						{
							if ((_SysInfo.btTcpReadData[9] * 0x100) + _SysInfo.btTcpReadData[10] == _SysInfo.nCanData)
							{
								NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), ((_SysInfo.btTcpReadData[9] * 0x100) + _SysInfo.btTcpReadData[10]).ToString("X4"), "OK");
								//AppendLogMsg($"OK / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
							}
							else
							{
								_SysInfo.bEolNg = true;
								NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), ((_SysInfo.btTcpReadData[9] * 0x100) + _SysInfo.btTcpReadData[10]).ToString("X4"), "NG");
								//AppendLogMsg($"NG / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
							}
						}
						else
						{
							_SysInfo.bEolNg = true;
							NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), "DATA ERROR", "NG");
						}
						_SysInfo.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;



				case 22000:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID.ToString(), out _SysInfo.nCanCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue1.ToString(), out _SysInfo.nCanAddr);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue2.ToString(), out _SysInfo.nBuffIndex);
					tMainTimer[nStepIndex].Start(5000);
					SendTCPReadCommand(_SysInfo.nCanCh, _SysInfo.nCanAddr);
					nProcessStep[nStepIndex]++;
					break;

				case 22001:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), "", "TIME OUT", "NG");
						_SysInfo.bEolNg = true;
						_SysInfo.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}

					if (_ModbusSoket.bResultOk)
					{
						if (_SysInfo.btTcpReadData[7] == 0x03)
						{
							_SysInfo.nReadDataBuff[_SysInfo.nBuffIndex] = (_SysInfo.btTcpReadData[9] * 0x100) + _SysInfo.btTcpReadData[10];
							NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), $"BUFFER{_SysInfo.nBuffIndex}", ((_SysInfo.btTcpReadData[9] * 0x100) + _SysInfo.btTcpReadData[10]).ToString("X4"), "");
						}
						_SysInfo.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;

				case 22100:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID.ToString(), out _SysInfo.nCanCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue1.ToString(), out _SysInfo.nCanAddr);
					//int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue2.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nCanData);
					_SysInfo.nCanData = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue2.ToString());
					SendReadCommand(_SysInfo.nCanCh, _SysInfo.nCanAddr, 1);

					tMainTimer[nStepIndex].Start(5000);
					nProcessStep[nStepIndex]++;
					break;

				case 22101:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), "TIME OUT", "NG");
						_SysInfo.bEolNg = true;
						_SysInfo.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}


					if (_CanComm[_SysInfo.nCanCh].bReadMessage)
					{
						if (_CanComm[_SysInfo.nCanCh].btReadData[0] == 0x03)
						{
							if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue3 != null && _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue3 != "")
							{
								if (int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue3.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMaskingData))
								{
									_SysInfo.nCompData = ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3]) & _SysInfo.nMaskingData;

									if (_SysInfo.nCompData == _SysInfo.nCanData)
									{
										NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3]).ToString("X4") + $"({(_SysInfo.nMaskingData.ToString("X4"))})", "OK");
										//AppendLogMsg($"OK / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
									}
									else
									{
										_SysInfo.bEolNg = true;
										NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3]).ToString("X4") + $"({(_SysInfo.nMaskingData.ToString("X4"))})", "NG");
										//AppendLogMsg($"NG / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
									}
								}
								else
								{
									_SysInfo.nCompData = ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3]);
									if (_SysInfo.nCompData == _SysInfo.nCanData)
									{
										NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), _SysInfo.nCompData.ToString("X4"), "OK");
										//AppendLogMsg($"OK / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
									}
									else
									{
										_SysInfo.bEolNg = true;
										NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), _SysInfo.nCompData.ToString("X4"), "NG");
										//AppendLogMsg($"NG / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
									}
								}
							}
							else
							{
								_SysInfo.nCompData = ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3]);
								if (_SysInfo.nCompData == _SysInfo.nCanData)
								{
									NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), _SysInfo.nCompData.ToString("X4"), "OK");
									//AppendLogMsg($"OK / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
								}
								else
								{
									_SysInfo.bEolNg = true;
									NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), _SysInfo.nCompData.ToString("X4"), "NG");
									//AppendLogMsg($"NG / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
								}
							}


						}
						else
						{
							_SysInfo.bEolNg = true;
							NgDataSet(_SysInfo.nMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), "DATA ERROR", "NG");
						}
						_SysInfo.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;

				// Sub Item 통신 종료
				case 29000:
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;



				// ADC Step
				case 30000:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nBuffIndex);
					double.TryParse(new DataTable().Compute(string.Format($"{_SysInfo.nReadDataBuff[_SysInfo.nBuffIndex]}{_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2}"), null).ToString(), out _SysInfo.dbCalcData);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3, out _SysInfo.nDispLen);

					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMin, out _SysInfo.dbSpecMin);
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMax, out _SysInfo.dbSpecMax);

					if (_SysInfo.dbCalcData > _SysInfo.dbSpecMax || _SysInfo.dbCalcData < _SysInfo.dbSpecMin)
					{
						TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F" + _SysInfo.nDispLen.ToString()), "NG");
						_SysInfo.bTestNG = true;
					}
					else
					{
						TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F" + _SysInfo.nDispLen.ToString()), "OK");
					}
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;


				// 파워서플라이 1번
				case 31000:
					nProcessStep[nStepIndex] = 31010;
					break;

				case 31005:
					PowerSupply[0].SendData("*CLS");
					nProcessStep[nStepIndex] = 31010;
					break;

				// 1번 채널 Power Supply 설정
				case 31010:
					PowerSupply[0].SendData("*IDN?");
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex]++;
					break;

				case 31011:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg("파워서플라이 초기화 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 90000;
					//}

					if (PowerSupply[0].IsReadData())
					{
						nProcessStep[nStepIndex] = 31020;
					}
					break;

				// 전압설정
				case 31020:
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.dbCommSendData);

					PowerSupply[0].SendData($"VOLT {_SysInfo.dbCommSendData}");
					tMainTimer[nStepIndex].Start(200);
					nProcessStep[nStepIndex]++;
					break;

				case 31021:
					if (!tMainTimer[nStepIndex].Verify()) { break; }

					nProcessStep[nStepIndex] = 31030;
					break;

				case 31030:
					PowerSupply[0].SendData("VOLT?");
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex]++;
					break;

				case 31031:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg("파워서플라이 Voltage Setting 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 90000;
					//}

					if (PowerSupply[0].IsReadData())
					{
						nProcessStep[nStepIndex] = 31040;
					}
					break;

				case 31040:
					double.TryParse(PowerSupply[0].GetReadData(), out _SysInfo.dbCommReadData);

					if (_SysInfo.dbCommReadData == _SysInfo.dbCommSendData)
					{
						nProcessStep[nStepIndex] = 31050;
					}
					break;

				case 31050:
					if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2 == "0")
					{
						PowerSupply[0].SendData("OUTP OFF");
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2 == "1")
					{
						PowerSupply[0].SendData("OUTP ON");
					}
					TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
					nProcessStep[nStepIndex] = 31100;
					break;



				case 31100:
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;




				// 파워서플라이 1번
				case 31200:
					nProcessStep[nStepIndex] = 31210;
					break;

				case 31205:
					PowerSupply[0].SendData("*CLS");
					nProcessStep[nStepIndex] = 31210;
					break;

				// 1번 채널 Power Supply 설정
				case 31210:
					PowerSupply[0].SendData("*IDN?");
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex]++;
					break;

				case 31211:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg("파워서플라이 초기화 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 90000;
					//}

					if (PowerSupply[0].IsReadData())
					{
						nProcessStep[nStepIndex] = 31220;
					}
					break;

				// 전압설정
				case 31220:
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMin, out _SysInfo.dbSpecMin);
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMax, out _SysInfo.dbSpecMax);

					PowerSupply[0].SendData($"MEAS:CURR?");
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex]++;
					break;

				case 31221:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg("파워서플라이 Voltage Setting 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 90000;
					//}

					if (PowerSupply[0].IsReadData())
					{
						nProcessStep[nStepIndex] = 31240;
					}
					break;

				case 31240:
					double.TryParse(PowerSupply[0].GetReadData(), out _SysInfo.dbCommReadData);

					if (_SysInfo.dbCommReadData > _SysInfo.dbSpecMax || _SysInfo.dbCommReadData < _SysInfo.dbSpecMin)
					{
						TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCommReadData.ToString(), "NG");
						_SysInfo.bTestNG = true;
					}
					else
					{
						TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCommReadData.ToString(), "OK");
					}
					nProcessStep[nStepIndex] = 31300;
					break;


				case 31300:
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;






				// 파워서플라이 1번
				case 32000:
					nProcessStep[nStepIndex] = 32010;
					break;

				case 32005:
					PowerSupply[1].SendData("*CLS");
					nProcessStep[nStepIndex] = 32010;
					break;

				// 1번 채널 Power Supply 설정
				case 32010:
					PowerSupply[1].SendData("*IDN?");
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex]++;
					break;

				case 32011:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg("파워서플라이 초기화 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 90000;
					//}

					if (PowerSupply[1].IsReadData())
					{
						nProcessStep[nStepIndex] = 32020;
					}
					break;

				// 전압설정
				case 32020:
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.dbCommSendData);

					PowerSupply[1].SendData($"VOLT {_SysInfo.dbCommSendData}");
					tMainTimer[nStepIndex].Start(200);
					nProcessStep[nStepIndex]++;
					break;

				case 32021:
					if (!tMainTimer[nStepIndex].Verify()) { break; }

					nProcessStep[nStepIndex] = 32030;
					break;

				case 32030:
					PowerSupply[1].SendData("VOLT?");
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex]++;
					break;

				case 32031:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg("파워서플라이 Voltage Setting 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 90000;
					//}

					if (PowerSupply[1].IsReadData())
					{
						nProcessStep[nStepIndex] = 32040;
					}
					break;

				case 32040:
					double.TryParse(PowerSupply[1].GetReadData(), out _SysInfo.dbCommReadData);

					if (_SysInfo.dbCommReadData == _SysInfo.dbCommSendData)
					{
						nProcessStep[nStepIndex] = 32050;
					}
					break;

				case 32050:
					if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2 == "0")
					{
						PowerSupply[1].SendData("OUTP OFF");
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2 == "1")
					{
						PowerSupply[1].SendData("OUTP ON");
					}
					TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
					nProcessStep[nStepIndex] = 32100;
					break;


				case 32100:
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;


				// 파워서플라이 1번
				case 32200:
					nProcessStep[nStepIndex] = 32210;
					break;


				case 32205:
					PowerSupply[1].SendData("*CLS");
					nProcessStep[nStepIndex] = 32210;
					break;

				// 1번 채널 Power Supply 설정
				case 32210:
					PowerSupply[1].SendData("*IDN?");
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex]++;
					break;

				case 32211:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg("파워서플라이 초기화 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 90000;
					//}

					if (PowerSupply[1].IsReadData())
					{
						nProcessStep[nStepIndex] = 32220;
					}
					break;

				// 전압설정
				case 32220:
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMin, out _SysInfo.dbSpecMin);
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMax, out _SysInfo.dbSpecMax);

					PowerSupply[1].SendData($"MEAS:CURR?");
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex]++;
					break;

				case 32221:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg("파워서플라이 Voltage Setting 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 90000;
					//}

					if (PowerSupply[1].IsReadData())
					{
						nProcessStep[nStepIndex] = 32240;
					}
					break;

				case 32240:
					double.TryParse(PowerSupply[0].GetReadData(), out _SysInfo.dbCommReadData);

					if (_SysInfo.dbCommReadData > _SysInfo.dbSpecMax || _SysInfo.dbCommReadData < _SysInfo.dbSpecMin)
					{
						TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCommReadData.ToString(), "NG");
						_SysInfo.bTestNG = true;
					}
					else
					{
						TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCommReadData.ToString(), "OK");
					}
					nProcessStep[nStepIndex] = 32300;
					break;


				case 32300:
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;




				case 33000:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nIOIndex);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2, out _SysInfo.nIOState);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3, out _SysInfo.nDelayTime);



					SetDIOPort(DO.RY_01 + _SysInfo.nIOIndex - 1, _SysInfo.nIOState == 1);



					nProcessStep[nStepIndex] = 33900;
					break;


				//	//IO 제어 스텝
				//case 33000:
				//	if (_SysInfo.nSubWorkStep >= _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo.Count)
				//	{
				//		nProcessStep[nStepIndex] = 33900;
				//	}
				//	else
				//	{
				//		nProcessStep[nStepIndex] = 33100;
				//	}
				//	break;

				//case 33100:
				//	if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue1 == "0")
				//	{
				//		SetDIOPort(DO.RY_01 + _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nTestType, false);
				//	}
				//	else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue1 == "1")
				//	{
				//		SetDIOPort(DO.RY_01 + _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nTestType, true);
				//	}
				//	_SysInfo.nSubWorkStep++;
				//	nProcessStep[nStepIndex] = 33900;
				//	break;

				// Sub Item 통신 종료
				case 33900:
					TestResultSet(_SysInfo.nMainWorkStep, "OK", "OK");
					tMainTimer[nStepIndex].Start(_SysInfo.nDelayTime);
					nProcessStep[nStepIndex] = 33910;
					break;
				
				case 33910:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
				
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;



				case 34000:
					nProcessStep[nStepIndex] = 34005;
					break;

				case 34005:
					if (_Config.bDmmEtcMode)
					{

						_KeysiteDmmEtc.Send($"*RST", true);
						nProcessStep[nStepIndex] = 34010;
					}
					else
					{
						KeysiteDmm.Send($"*RST");
						nProcessStep[nStepIndex] = 34010;
					}

					break;


				case 34010:
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc.Send("*IDN?", true);
						nProcessStep[nStepIndex]++;
					}
					else
					{
						KeysiteDmm.Send("*IDN?");
						nProcessStep[nStepIndex]++;
					}

					break;

				case 34011:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc.IsReadData())
						{
							nProcessStep[nStepIndex] = 34020;
						}
					}
					else
					{
						if (KeysiteDmm.IsReadData())
						{
							nProcessStep[nStepIndex] = 34020;
						}
					}

					break;

				// 전압설정
				case 34020:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nDmmCh);

					if (_Config.bDmmEtcMode)
					{
						if (_SysInfo.nDmmCh > 20)
						{
							_KeysiteDmmEtc.Send($"SENSe:FUNCtion \"VOLTage\", (@{200 + _SysInfo.nDmmCh - 20})", true);
						}
						else
						{
							_KeysiteDmmEtc.Send($"SENSe:FUNCtion \"VOLTage\", (@{100 + _SysInfo.nDmmCh})", true);
						}
					}
					else
					{
						if (_SysInfo.nDmmCh > 20)
						{
							KeysiteDmm.Send($"SENSe:FUNCtion \"VOLTage\", (@{200 + _SysInfo.nDmmCh - 20})");
						}
						else
						{
							KeysiteDmm.Send($"SENSe:FUNCtion \"VOLTage\",(@{100 + _SysInfo.nDmmCh})");
						}
					}


					nProcessStep[nStepIndex]++;
					break;

				case 34021:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nDmmCh);

					if (_Config.bDmmEtcMode)
					{
						if (_SysInfo.nDmmCh > 20)
						{
							_KeysiteDmmEtc.Send($"ROUTe:CLOSe  (@{200 + _SysInfo.nDmmCh - 20})", true);
						}
						else
						{
							_KeysiteDmmEtc.Send($"ROUTe:CLOSe  (@{100 + _SysInfo.nDmmCh})", true);
						}
					}
					else
					{
						if (_SysInfo.nDmmCh > 20)
						{
							KeysiteDmm.Send($"ROUTe:CLOSe  (@{200 + _SysInfo.nDmmCh - 20})");
						}
						else
						{
							KeysiteDmm.Send($"ROUTe:CLOSe  (@{100 + _SysInfo.nDmmCh})");
						}
					}

					tMainTimer[nStepIndex].Start(200);
					nProcessStep[nStepIndex]++;
					break;


				case 34022:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc.Send($"READ?", true);

					}
					else
					{
						KeysiteDmm.Send($"READ?");
					}

					tMainTimer[nStepIndex].Start(1000);
					nProcessStep[nStepIndex]++;
					break;

				case 34023:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc.IsReadData())
						{
							nProcessStep[nStepIndex] = 34040;
						}
					}
					else
					{
						if (KeysiteDmm.IsReadData())
						{
							nProcessStep[nStepIndex] = 34040;
						}
					}
					break;


				case 34040:
					//theApp.AppendLogMsg(KeysiteDmm.GetReadData(), MSG_TYPE.INFO);
					if (_Config.bDmmEtcMode)
					{
						double.TryParse(_KeysiteDmmEtc.strReadMessage, out _SysInfo.dbCommReadData);
					}
					else
					{
						double.TryParse(KeysiteDmm.GetReadData(), out _SysInfo.dbCommReadData);
					}

					//AppendLogMsg(_SysInfo.dbCommReadData.ToString(), MSG_TYPE.INFO);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3, out _SysInfo.nBuffIndex);
					double.TryParse(new DataTable().Compute(string.Format($"{_SysInfo.dbCommReadData}{_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2}"), null).ToString(), out _SysInfo.dbCalcData);

					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMin, out _SysInfo.dbSpecMin);
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMax, out _SysInfo.dbSpecMax);

					// 0은 양쪽다 비교 , 1은 Min만 비교, 2는 Max만 비교
					if (_SysInfo.nBuffIndex == 0)
					{
						if (_SysInfo.dbCalcData > _SysInfo.dbSpecMax || _SysInfo.dbCalcData < _SysInfo.dbSpecMin)
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "NG");
							_SysInfo.bTestNG = true;
						}
						else
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "OK");
						}
					}
					else if (_SysInfo.nBuffIndex == 1)
					{
						if (_SysInfo.dbCalcData < _SysInfo.dbSpecMin)
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "NG");
							_SysInfo.bTestNG = true;
						}
						else
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "OK");
						}
					}
					else if (_SysInfo.nBuffIndex == 2)
					{
						if (_SysInfo.dbCalcData > _SysInfo.dbSpecMax)
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "NG");
							_SysInfo.bTestNG = true;
						}
						else
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "OK");
						}
					}

					//TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCommReadData.ToString("F2"), "OK");
					nProcessStep[nStepIndex] = 34050;
					break;

				case 34050:
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;


				// 파워서플라이 1번
				case 35000:
					nProcessStep[nStepIndex] = 35010;
					break;

				// 1번 채널 Power Supply 설정
				case 35010:
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc.Send("*IDN?", true);
						nProcessStep[nStepIndex]++;
					}
					else
					{
						KeysiteDmm.Send("*IDN?");
						nProcessStep[nStepIndex]++;
					}
					break;

				case 35011:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc.IsReadData())
						{
							nProcessStep[nStepIndex] = 35020;
						}
					}
					else
					{
						if (KeysiteDmm.IsReadData())
						{
							nProcessStep[nStepIndex] = 35020;
						}
					}
					break;

				// 전압설정
				case 35020:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nDmmCh);
					if (_Config.bDmmEtcMode)
					{

						if (_SysInfo.nDmmCh > 20)
						{
							_KeysiteDmmEtc.Send($"MEAS:FREQ? (@{200 + _SysInfo.nDmmCh - 20})", true);
						}
						else
						{
							_KeysiteDmmEtc.Send($"MEAS:FREQ? (@{100 + _SysInfo.nDmmCh})", true);
						}
					}
					else
					{
						if (_SysInfo.nDmmCh > 20)
						{
							KeysiteDmm.Send($"MEAS:FREQ? (@{200 + _SysInfo.nDmmCh - 20})");
						}
						else
						{
							KeysiteDmm.Send($"MEAS:FREQ? (@{100 + _SysInfo.nDmmCh})");
						}
					}


					nProcessStep[nStepIndex]++;
					break;

				case 35021:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc.IsReadData())
						{
							nProcessStep[nStepIndex] = 35040;
						}
					}
					else
					{
						if (KeysiteDmm.IsReadData())
						{
							nProcessStep[nStepIndex] = 35040;
						}
					}
					break;

				case 35040:
					//theApp.AppendLogMsg(KeysiteDmm.GetReadData(), MSG_TYPE.INFO);
					if (_Config.bDmmEtcMode)
					{
						double.TryParse(_KeysiteDmmEtc.strReadMessage, out _SysInfo.dbCommReadData);
					}
					else
					{
						double.TryParse(KeysiteDmm.GetReadData(), out _SysInfo.dbCommReadData);
					}
					//TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCommReadData.ToString("F2"), "OK");

					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3, out _SysInfo.nBuffIndex);
					double.TryParse(new DataTable().Compute(string.Format($"{_SysInfo.dbCommReadData}{_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2}"), null).ToString(), out _SysInfo.dbCalcData);

					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMin, out _SysInfo.dbSpecMin);
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMax, out _SysInfo.dbSpecMax);

					// 0은 양쪽다 비교 , 1은 Min만 비교, 2는 Max만 비교
					if (_SysInfo.nBuffIndex == 0)
					{
						if (_SysInfo.dbCalcData > _SysInfo.dbSpecMax || _SysInfo.dbCalcData < _SysInfo.dbSpecMin)
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "NG");
							_SysInfo.bTestNG = true;
						}
						else
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "OK");
						}
					}
					else if (_SysInfo.nBuffIndex == 1)
					{
						if (_SysInfo.dbCalcData < _SysInfo.dbSpecMin)
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "NG");
							_SysInfo.bTestNG = true;
						}
						else
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "OK");
						}
					}
					else if (_SysInfo.nBuffIndex == 2)
					{
						if (_SysInfo.dbCalcData > _SysInfo.dbSpecMax)
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "NG");
							_SysInfo.bTestNG = true;
						}
						else
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "OK");
						}
					}
					nProcessStep[nStepIndex] = 35050;
					break;

				case 35050:
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;



				// 파워서플라이 1번

				case 36000:
					nProcessStep[nStepIndex] = 36005;
					break;

				case 36005:
					if (_Config.bDmmEtcMode)
					{

						_KeysiteDmmEtc.Send($"*RST", true);
						nProcessStep[nStepIndex] = 36010;
					}
					else
					{
						KeysiteDmm.Send($"*RST");
						nProcessStep[nStepIndex] = 36010;
					}

					break;

				// 1번 채널 Power Supply 설정
				case 36010:
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc.Send("*IDN?", true);
						nProcessStep[nStepIndex]++;
					}
					else
					{
						KeysiteDmm.Send("*IDN?");
						nProcessStep[nStepIndex]++;
					}
					break;

				case 36011:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc.IsReadData())
						{
							nProcessStep[nStepIndex] = 36020;
						}
					}
					else
					{
						if (KeysiteDmm.IsReadData())
						{
							nProcessStep[nStepIndex] = 36020;
						}
					}
					break;

				// 전압설정
				case 36020:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nDmmCh);

					if (_Config.bDmmEtcMode)
					{
						if (_SysInfo.nDmmCh > 20)
						{
							_KeysiteDmmEtc.Send($"SENSe:FUNCtion \"RESistance\", (@{200 + _SysInfo.nDmmCh - 20})", true);
						}
						else
						{
							_KeysiteDmmEtc.Send($"SENSe:FUNCtion \"RESistance\", (@{100 + _SysInfo.nDmmCh})", true);
						}
					}
					else
					{
						if (_SysInfo.nDmmCh > 20)
						{
							KeysiteDmm.Send($"SENSe:FUNCtion \"RESistance\", (@{200 + _SysInfo.nDmmCh - 20})");
						}
						else
						{
							KeysiteDmm.Send($"SENSe:FUNCtion \"RESistance\", (@{100 + _SysInfo.nDmmCh})");
						}
					}


					nProcessStep[nStepIndex]++;
					break;

				case 36021:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nDmmCh);

					if (_Config.bDmmEtcMode)
					{
						if (_SysInfo.nDmmCh > 20)
						{
							_KeysiteDmmEtc.Send($"ROUTe:CLOSe (@{200 + _SysInfo.nDmmCh - 20})", true);
						}
						else
						{
							_KeysiteDmmEtc.Send($"ROUTe:CLOSe (@{100 + _SysInfo.nDmmCh})", true);
						}
					}
					else
					{
						if (_SysInfo.nDmmCh > 20)
						{
							KeysiteDmm.Send($"ROUTe:CLOSe (@{200 + _SysInfo.nDmmCh - 20})");
						}
						else
						{
							KeysiteDmm.Send($"ROUTe:CLOSe (@{100 + _SysInfo.nDmmCh})");
						}
					}

					tMainTimer[nStepIndex].Start(200);
					nProcessStep[nStepIndex]++;
					break;


				case 36022:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc.Send($"READ?", true);

					}
					else
					{
						KeysiteDmm.Send($"READ?");
					}

					tMainTimer[nStepIndex].Start(1000);
					nProcessStep[nStepIndex] = 36030;
					break;

				case 36030:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc.IsReadData())
						{
							nProcessStep[nStepIndex] = 36040;
						}
					}
					else
					{
						if (KeysiteDmm.IsReadData())
						{
							nProcessStep[nStepIndex] = 36040;
						}
					}
					break;

				case 36040:
					//theApp.AppendLogMsg(KeysiteDmm.GetReadData(), MSG_TYPE.INFO);
					if (_Config.bDmmEtcMode)
					{
						double.TryParse(_KeysiteDmmEtc.strReadMessage, out _SysInfo.dbCommReadData);
					}
					else
					{
						double.TryParse(KeysiteDmm.GetReadData(), out _SysInfo.dbCommReadData);
					}


					if(_SysInfo.dbCommReadData < 0)
					{
						nProcessStep[nStepIndex] = 36022;
						break;
					}
					else
					{

						int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3, out _SysInfo.nBuffIndex);
						double.TryParse(new DataTable().Compute(string.Format($"{_SysInfo.dbCommReadData}{_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2}"), null).ToString(), out _SysInfo.dbCalcData);

						double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMin, out _SysInfo.dbSpecMin);
						double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMax, out _SysInfo.dbSpecMax);

						// 0은 양쪽다 비교 , 1은 Min만 비교, 2는 Max만 비교
						if (_SysInfo.nBuffIndex == 0)
						{
							if (_SysInfo.dbCalcData > _SysInfo.dbSpecMax || _SysInfo.dbCalcData < _SysInfo.dbSpecMin)
							{
								TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "NG");
								nProcessStep[nStepIndex] = 36045;
								break;
							}
							else
							{
								TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "OK");
								nProcessStep[nStepIndex] = 36050;
								break;
							}
						}
						else if (_SysInfo.nBuffIndex == 1)
						{
							if (_SysInfo.dbCalcData < _SysInfo.dbSpecMin)
							{
								TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "NG");
								nProcessStep[nStepIndex] = 36045;
								break;
							}
							else
							{
								TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "OK");
								nProcessStep[nStepIndex] = 36050;
								break;
							}
						}
						else if (_SysInfo.nBuffIndex == 2)
						{
							if (_SysInfo.dbCalcData > _SysInfo.dbSpecMax)
							{
								TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "NG");
								nProcessStep[nStepIndex] = 36045;
								break;
							}
							else
							{
								TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "OK");
								nProcessStep[nStepIndex] = 36050;
								break;
							}
						}

						

					}
		


					break;


				case 36045:
					_SysInfo.strPopupContent = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strTestName;
					_SysInfo._SwStatus = MAIN_STATUS.READY;
					_SysInfo._PopupStatus = MAIN_STATUS.READY;
					_SysInfo.nTL_Beep = 1;
					ShowRetryPopUpWindow();
					nProcessStep[nStepIndex]++;
					break;

				case 36046:
					if (_SysInfo._SwStatus == MAIN_STATUS.OK)
					{
						nProcessStep[nStepIndex] = 36048;
					}
					else if (_SysInfo._SwStatus == MAIN_STATUS.NG)
					{

						_SysInfo.bTestNG = true;
						nProcessStep[nStepIndex] = 36047;
					}
					else if (GetDIOPort(DI.START_SW1))
					{

						CloseRetryPopUpWindow();
						nProcessStep[nStepIndex] = 36048;
					}
					else if (GetDIOPort(DI.START_SW2))
					{

						_SysInfo.bTestNG = true;
						CloseRetryPopUpWindow();
						nProcessStep[nStepIndex] = 36047;
					}
					break;

				case 36047:
					if (!GetDIOPort(DI.START_SW1) && !GetDIOPort(DI.START_SW2))
					{
						nProcessStep[nStepIndex] = 36050;
					}
					break;

				case 36048:
					if (!GetDIOPort(DI.START_SW1) && !GetDIOPort(DI.START_SW2))
					{
						nProcessStep[nStepIndex] = 36022;
					}
					break;

				case 36050:
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;



				//PopUp 행정
				case 37000:
					_SysInfo.strPopupContent = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strTestName;
					_SysInfo._SwStatus = MAIN_STATUS.READY;
					_SysInfo._PopupStatus = MAIN_STATUS.READY;
					_SysInfo.nTL_Beep = 1;
					ShowPopUpWindow();
					nProcessStep[nStepIndex]++;
					break;

				case 37001:
					if (_SysInfo._SwStatus == MAIN_STATUS.OK)
					{
						TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
						nProcessStep[nStepIndex] = 37002;
					}
					else if (_SysInfo._SwStatus == MAIN_STATUS.NG)
					{
						TestResultSet(_SysInfo.nMainWorkStep, "", "NG");
						_SysInfo.bTestNG = true;
						nProcessStep[nStepIndex] = 80000;
					}
					else if (GetDIOPort(DI.START_SW1))
					{
						TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
						ClosePopUpWindow();
						nProcessStep[nStepIndex] = 37002;
					}
					else if (GetDIOPort(DI.START_SW2))
					{
						TestResultSet(_SysInfo.nMainWorkStep, "", "NG");
						_SysInfo.bTestNG = true;
						ClosePopUpWindow();
						nProcessStep[nStepIndex] = 80000;
					}
					break;

				case 37002:
					if (!GetDIOPort(DI.START_SW1) && !GetDIOPort(DI.START_SW2))
					{
						nProcessStep[nStepIndex]++;
					}
					break;

				case 37003:
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				// Ping Test
				case 38000:
					if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1 == "1")
					{
						nProcessStep[nStepIndex] = 38010;

					}
					else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1 == "0")
					{
						nProcessStep[nStepIndex] = 38020;
					}
					break;
				// Ping 테스트 시작
				case 38010:
					_SysInfo.bPingTestResult = MAIN_STATUS.READY;
					_SysInfo.strPingTestIP = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2;
					nProcessStep[(int)PROC_LIST.PING_TEST] = 100;
					nProcessStep[nStepIndex]++;
					break;

				case 38011:
					if (_SysInfo.bPingTestResult == MAIN_STATUS.OK)
					{
						TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
						nProcessStep[nStepIndex] = 38012;
					}
					else if (_SysInfo.bPingTestResult == MAIN_STATUS.NG)
					{
						TestResultSet(_SysInfo.nMainWorkStep, "", "NG");
						_SysInfo.bTestNG = true;
						nProcessStep[nStepIndex] = 38012;
					}
					break;

				case 38012:
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				// Ping테스트 종료
				case 38020:
					nProcessStep[(int)PROC_LIST.PING_TEST] = 0;
					TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;


				case 39000:
					{
						int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nBuffIndex);
						int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2, out _SysInfo.nBuffCount);

						string strVesion = "";
						bool bCharResult = true;
						for (int i = 0; i < _SysInfo.nBuffCount; i++)
						{
							strVesion += Convert.ToChar((_SysInfo.nReadDataBuff[_SysInfo.nBuffIndex + i] / 0x100));
							strVesion += Convert.ToChar((_SysInfo.nReadDataBuff[_SysInfo.nBuffIndex + i] % 0x100));
						}

						if ((_SysInfo.nBuffCount * 2) == _ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMin.Length && (_SysInfo.nBuffCount * 2) == _ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMax.Length)
						{
							for (int i = 0; i < _SysInfo.nBuffCount; i++)
							{
								if (i % 2 == 0)
								{
									if ((int)_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMin[i] > (_SysInfo.nReadDataBuff[_SysInfo.nBuffIndex + (i / 2)] / 0x100) || (int)_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMax[i] < (_SysInfo.nReadDataBuff[_SysInfo.nBuffIndex + (i / 2)] / 0x100))
									{
										bCharResult = false;
									}
								}
								else if (i % 2 == 1)
								{
									if ((int)_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMin[i] > (_SysInfo.nReadDataBuff[_SysInfo.nBuffIndex + (i / 2)] % 0x100) || (int)_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMax[i] < (_SysInfo.nReadDataBuff[_SysInfo.nBuffIndex + (i / 2)] % 0x100))
									{
										bCharResult = false;
									}
								}

							}

							if (bCharResult)
							{
								TestResultSet(_SysInfo.nMainWorkStep, strVesion, "OK");
							}
							else
							{
								TestResultSet(_SysInfo.nMainWorkStep, strVesion, "NG");
								_SysInfo.bTestNG = true;
							}
						}
						else
						{
							TestResultSet(_SysInfo.nMainWorkStep, strVesion, "NG");
							_SysInfo.bTestNG = true;
						}
						nProcessStep[nStepIndex]++;
					}
					break;

				case 39001:
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;


				case 40000:
					{
						int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nBuffIndex);
						int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2, out _SysInfo.nBuffCount);

						string strVesion = "";
						bool bCharResult = true;
						for (int i = 0; i < _SysInfo.nBuffCount; i++)
						{
							strVesion += Convert.ToChar((_SysInfo.nReadDataBuff[_SysInfo.nBuffIndex + i]));
							strVesion += Convert.ToChar((_SysInfo.nReadDataBuff[_SysInfo.nBuffIndex + i]));
						}

						if (_SysInfo.nBuffCount == _ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMin.Length && _SysInfo.nBuffCount == _ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMax.Length)
						{
							for (int i = 0; i < _SysInfo.nBuffCount; i++)
							{
								if ((int)_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMin[i] > _SysInfo.nReadDataBuff[_SysInfo.nBuffIndex + i] || (int)_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMax[i] < _SysInfo.nReadDataBuff[_SysInfo.nBuffIndex + i])
								{
									bCharResult = false;
								}
							}

							if (bCharResult)
							{
								TestResultSet(_SysInfo.nMainWorkStep, strVesion, "OK");
							}
							else
							{
								TestResultSet(_SysInfo.nMainWorkStep, strVesion, "NG");
								_SysInfo.bTestNG = true;
							}
						}
						else
						{
							TestResultSet(_SysInfo.nMainWorkStep, strVesion, "NG");
							_SysInfo.bTestNG = true;
						}
						nProcessStep[nStepIndex]++;
					}
					break;

				case 40001:
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				case 41000:
					_CellSimulator1.Send("*RST", true);
					nProcessStep[nStepIndex] = 41005;
					break;

				case 41005:
					_CellSimulator1.Send("*CLS", true);
					nProcessStep[nStepIndex] = 41010;
					break;

				case 41010:
					_CellSimulator1.Send("*IDN?", true);
					tMainTimer[nStepIndex].Start(10000);
					nProcessStep[nStepIndex]++;
					break;

				case 41011:
					if (tMainTimer[nStepIndex].Verify())
					{
						AppendLogMsg("Cell Simulator #1 initialization failed", MSG_TYPE.ERROR);
						nProcessStep[nStepIndex] = 90000;
					}

					if (_CellSimulator1.IsReadData())
					{
						nProcessStep[nStepIndex]++;
					}
					break;

				case 41012:
					_CellSimulator1.Send("SIM:OUTP?", true);
					nProcessStep[nStepIndex]++;
					break;

				case 41013:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg("셀시뮬레이터 #2 초기화 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 0;
					//}

					if (_CellSimulator1.IsReadData())
					{
						nProcessStep[nStepIndex]++;
					}
					break;

				case 41014:
					if (_CellSimulator1.strReadMessage == "1")
					{

						nProcessStep[nStepIndex] = 41050;
					}
					else
					{

						nProcessStep[nStepIndex] = 41015;
					}
					break;

				case 41015:
					//double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3, out _SysInfo.dbCellStates);
					//if(_SysInfo.dbCellStates == 0)
					//{

					nProcessStep[nStepIndex] = 41016;
					//}
					//else
					//{
					//	_CellSimulator1.Send("SIM:OUTP OFF", true);
					//	TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
					//	_SysInfo.nMainWorkStep++;
					//	nProcessStep[nStepIndex] = 3000;
					//}
					break;

				//case 41015:
				//	if (!tMainTimer[nStepIndex].Verify()) { break; }
				//	_CellSimulator1.Send("SYSTem:FRAME:STATe? 0", true);
				//	tMainTimer[nStepIndex].Start(300);
				//	nProcessStep[nStepIndex]++;
				//	break;

				case 41016:
					_CellSimulator1.Send("SIM:CONF:BMS:NUMB 1", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 41018;
					break;

				//case 41017:
				//	if (!tMainTimer[nStepIndex].Verify()) { break; }
				//	_CellSimulator1.Send("SIM:CONF:SAMP:TIME 10", true);
				//	tMainTimer[nStepIndex].Start(300);
				//	nProcessStep[nStepIndex]++;
				//	break;

				case 41018:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator1.Send("SIM:CONF:CELL:NUMB 1,16", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex]++;
					break;

				case 41019:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator1.Send("SIM:CONF:CELL:PARA 1,1,16,1,2", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 41030;
					break;

				case 41030:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.dbCellVolt);
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2, out _SysInfo.dbCellStartCH1);
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3, out _SysInfo.dbCellEndCH1);


					_CellSimulator1.Send($"SIM:PROG:CELL 1,1,{_SysInfo.dbCellStartCH1},{_SysInfo.dbCellEndCH1},{_SysInfo.dbCellVolt},3", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 41040;
					break;

				case 41040:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator1.Send("SIM:OUTP ON", true);
					tMainTimer[nStepIndex].Start(300);
					//TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
					//_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 41100;
					break;


				case 41050:
					//double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3, out _SysInfo.dbCellStates);
					//if (_SysInfo.dbCellStates == 0)
					//{
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 41060;
					//}
					//else
					//{
					//	_CellSimulator1.Send("SIM:OUTP OFF", true);
					//	TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
					//	_SysInfo.nMainWorkStep++;
					//	nProcessStep[nStepIndex] = 3000;
					//}
					break;

				case 41060:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.dbCellVolt);
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2, out _SysInfo.dbCellStartCH1);
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3, out _SysInfo.dbCellEndCH1);


					_CellSimulator1.Send($"SIM:PROG:CELL 1,1,{_SysInfo.dbCellStartCH1},{_SysInfo.dbCellEndCH1},{_SysInfo.dbCellVolt},3", true);
					tMainTimer[nStepIndex].Start(200);
					nProcessStep[nStepIndex] = 41070;
					break;

				case 41070:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator1.Send("SIM:OUTP:IMM", true);
					tMainTimer[nStepIndex].Start(200);
					//TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
					//_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 41100;
					break;


				case 41100:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator1.Send("SIM:OUTP?", true);
					nProcessStep[nStepIndex]++;
					break;

				case 41101:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg("셀시뮬레이터 #2 초기화 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 0;
					//}

					if (_CellSimulator1.IsReadData())
					{
						nProcessStep[nStepIndex]++;
					}
					break;

				case 41102:

					if (_SysInfo.dbCellVolt == 0)
					{
						TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
						_SysInfo.nMainWorkStep++;
						nProcessStep[nStepIndex] = 3000;
					}
					else
					{
						if (_CellSimulator1.strReadMessage == "1")
						{
							TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
							_SysInfo.nMainWorkStep++;
							nProcessStep[nStepIndex] = 3000;
						}
						else
						{
							TestResultSet(_SysInfo.nMainWorkStep, "", "NG");
							_SysInfo.bTestNG = true;
							_SysInfo.nMainWorkStep++;
							nProcessStep[nStepIndex] = 3000;
						}
					}
					
					break;

				case 42000:
					_CellSimulator2.Send("*RST", true);
					tMainTimer[nStepIndex].Start(200);
					nProcessStep[nStepIndex] = 42005;
					break;

				case 42005:
					_CellSimulator2.Send("*CLS", true);

					nProcessStep[nStepIndex] = 42010;
					break;

				case 42010:
					_CellSimulator2.Send("*IDN?", true);
					tMainTimer[nStepIndex].Start(10000);
					nProcessStep[nStepIndex]++;
					break;

				case 42011:
					if (tMainTimer[nStepIndex].Verify())
					{
						AppendLogMsg("Cell Simulator #2 initialization failed", MSG_TYPE.ERROR);
						nProcessStep[nStepIndex] = 90000;
					}

					if (_CellSimulator2.IsReadData())
					{
						nProcessStep[nStepIndex] = 42012;
					}
					break;

				case 42012:
					_CellSimulator2.Send("SIM:OUTP?", true);
					nProcessStep[nStepIndex]++;
					break;

				case 42013:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg("셀시뮬레이터 #2 초기화 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 0;
					//}

					if (_CellSimulator2.IsReadData())
					{
						nProcessStep[nStepIndex] = 42014;
					}
					break;

				case 42014:
					if (_CellSimulator2.strReadMessage == "1")
					{
						nProcessStep[nStepIndex] = 42050;
					}
					else
					{

						nProcessStep[nStepIndex] = 42015;
					}
					break;

				case 42015:

					//double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3, out _SysInfo.dbCellStates2);

					//if (_SysInfo.dbCellStates2 == 0)
					//{
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 42016;
					//}
					//else
					//{
					//	_CellSimulator2.Send("SIM:OUTP OFF", true);
					//	TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
					//	_SysInfo.nMainWorkStep++;
					//	nProcessStep[nStepIndex] = 3000;
					//}
					break;

				//case 42015:
				//	if (!tMainTimer[nStepIndex].Verify()) { break; }
				//	_CellSimulator2.Send("SYSTem:FRAME:STATe? 0", true);
				//	tMainTimer[nStepIndex].Start(300);
				//	nProcessStep[nStepIndex]++;
				//	break;

				case 42016:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator2.Send("SIM:CONF:BMS:NUMB 1", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 42018;
					break;

				//case 42017:
				//	if (!tMainTimer[nStepIndex].Verify()) { break; }
				//	_CellSimulator2.Send("SIM:CONF:SAMP:TIME 10", true);
				//	tMainTimer[nStepIndex].Start(300);
				//	nProcessStep[nStepIndex]++;
				//	break;

				case 42018:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator2.Send("SIM:CONF:CELL:NUMB 1,16", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex]++;
					break;

				case 42019:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator2.Send("SIM:CONF:CELL:PARA 1,1,16,1,2", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 42030;
					break;

				case 42030:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.dbCellVolt2);
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2, out _SysInfo.dbCellStartCH2);
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3, out _SysInfo.dbCellEndCH2);


					_CellSimulator2.Send($"SIM:PROG:CELL 1,1,{_SysInfo.dbCellStartCH2},{_SysInfo.dbCellEndCH2},{_SysInfo.dbCellVolt2},3", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 42040;
					break;

				case 42040:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator2.Send("SIM:OUTP ON", true);
					tMainTimer[nStepIndex].Start(300);
					//TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
					//_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 42100;
					break;

				case 42050:
					//double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3, out _SysInfo.dbCellStates);
					//if (_SysInfo.dbCellStates == 0)
					//{
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 42060;
					//}
					//else
					//{
					//	_CellSimulator2.Send("SIM:OUTP OFF", true);
					//	TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
					//	_SysInfo.nMainWorkStep++;
					//	nProcessStep[nStepIndex] = 3000;
					//}
					break;

				case 42060:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.dbCellVolt2);
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2, out _SysInfo.dbCellStartCH2);
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3, out _SysInfo.dbCellEndCH2);


					_CellSimulator2.Send($"SIM:PROG:CELL 1,1,{_SysInfo.dbCellStartCH2},{_SysInfo.dbCellEndCH2},{_SysInfo.dbCellVolt2},3", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 42070;
					break;

				case 42070:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator2.Send("SIM:OUTP:IMM", true);
					tMainTimer[nStepIndex].Start(300);
					//TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
					//_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 42100;
					break;


				case 42100:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator2.Send("SIM:OUTP?", true);
					nProcessStep[nStepIndex]++;
					break;

				case 42101:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg("셀시뮬레이터 #2 초기화 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 0;
					//}

					if (_CellSimulator2.IsReadData())
					{
						nProcessStep[nStepIndex]++;
					}
					break;

				case 42102:
					if(_SysInfo.dbCellVolt2 == 0)
					{
						TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
						_SysInfo.nMainWorkStep++;
						nProcessStep[nStepIndex] = 3000;
					}
					else
					{
						if (_CellSimulator2.strReadMessage == "1")
						{
							TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
							_SysInfo.nMainWorkStep++;
							nProcessStep[nStepIndex] = 3000;
						}
						else
						{
							TestResultSet(_SysInfo.nMainWorkStep, "", "NG");
							_SysInfo.bTestNG = true;
							_SysInfo.nMainWorkStep++;
							nProcessStep[nStepIndex] = 3000;
						}
					}
					
					break;

				case 43000:
					_SysInfo.dbCalcData = 0.0;
					_SysInfo.dbCommReadData = 0.0;
					nProcessStep[nStepIndex] = 43005;
					break;

				case 43005:
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc.Send("*RST", true);
						nProcessStep[nStepIndex] = 43010;
					}
					else
					{
						KeysiteDmm.Send("*RST");
						nProcessStep[nStepIndex] = 43010;
					}

					break;
				// 1번 채널 Power Supply 설정
				case 43010:
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc.Send("*IDN?", true);
						nProcessStep[nStepIndex]++;
					}
					else
					{
						KeysiteDmm.Send("*IDN?");
						nProcessStep[nStepIndex]++;
					}
					break;

				case 43011:
					_SysInfo.bFirstCheck = true;
					if (_Config.bDmmEtcMode)
					{
						if (_KeysiteDmmEtc.IsReadData())
						{
							//tMainTimer[nStepIndex].Start(200);
							nProcessStep[nStepIndex] = 43020;
						}
					}
					else
					{
						if (KeysiteDmm.IsReadData())
						{
							//tMainTimer[nStepIndex].Start(200);
							nProcessStep[nStepIndex] = 43020;
						}
					}
					break;

				case 43020:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nDmmCh);
					if (_Config.bDmmEtcMode)
					{
						//if (_SysInfo.nDmmCh == 1)
						//{
						//	_KeysiteDmmEtc.Send($"CONF:CURR:DC (@122)",true);
						//}
						//else
						//{
						_KeysiteDmmEtc.Send($"SENSe:FUNCtion \"CURRent\", (@{_SysInfo.nDmmCh})", true);
						//}
					}
					else
					{

						KeysiteDmm.Send($"SENSe:FUNCtion \"CURRent\", (@{_SysInfo.nDmmCh})");

					}

					nProcessStep[nStepIndex]++;
					break;

				case 43021:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nDmmCh);
					if (_Config.bDmmEtcMode)
					{
						//if (_SysInfo.nDmmCh == 1)
						//{
						//	_KeysiteDmmEtc.Send($"ROUT:SCAN (@{_SysInfo.nDmmCh})",true);
						//}
						//else
						//{
						_KeysiteDmmEtc.Send($"ROUTe:CLOSe (@{_SysInfo.nDmmCh})", true);

						AppendDebugMsg($"Curr M 검사 시작", "CURR");
						//}
					}
					else
					{
						//if (_SysInfo.nDmmCh == 1)
						//{
						//	KeysiteDmm.Send($"ROUT:SCAN (@{_SysInfo.nDmmCh})");
						//}
						//else
						//{
						KeysiteDmm.Send($"ROUTe:CLOSe (@{_SysInfo.nDmmCh})");
						AppendDebugMsg($"Curr M 검사 시작", "CURR");
						//}
					}


					nProcessStep[nStepIndex]++;
					break;

				case 43022:

					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nDmmCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3, out _SysInfo.nDmmTime);

					//if(_SysInfo.nDmmTime == 1)
					//{
					//	nProcessStep[nStepIndex] = 43023;
					//}
					//else
					//{
					//if (_Config.bDmmEtcMode)
					//{
					//	//if (_SysInfo.nDmmCh == 1)
					//	//{
					//	//	_KeysiteDmmEtc.Send($"SENS:CURR:APER {_ModelInfo.dbDmmAScanSpeed},(@{_SysInfo.nDmmCh})", true);
					//	//}
					//	//else
					//	//{
					//	_KeysiteDmmEtc.Send($"SENS:CURR:APER {_ModelInfo.dbDmmMScanSpeed},(@{_SysInfo.nDmmCh})", true);
					//	//}
					//}
					//else
					//{
					//	//if (_SysInfo.nDmmCh == 1)
					//	//{
					//	//	KeysiteDmm.Send($"SENS:CURR:APER {_ModelInfo.dbDmmAScanSpeed},(@{_SysInfo.nDmmCh})");
					//	//}
					//	//else
					//	//{
					//	KeysiteDmm.Send($"SENS:CURR:APER {_ModelInfo.dbDmmMScanSpeed},(@{_SysInfo.nDmmCh})");
					//	//}
					//}
					//tMainTimer[nStepIndex].Start(3000);
					nProcessStep[nStepIndex] = 43023;

					//}
					break;


				case 43023:
					//if (!tMainTimer[nStepIndex].Verify()) { break; }
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc.Send($"READ?", true);
					}
					else
					{
						KeysiteDmm.Send($"READ?");
					}

					//KeysiteDmm.Send($"MEAS:VOLT:DC? (@{_SysInfo.strDmmCh})");
					nProcessStep[nStepIndex] = 43024;
					break;

				case 43024:


					if (_Config.bDmmEtcMode)
					{
						if (_KeysiteDmmEtc.IsReadData())
						{
							nProcessStep[nStepIndex] = 43025;
							
							if (_SysInfo.bFirstCheck)
							{
								tMainTimer[nStepIndex].Start(200);
								_SysInfo.bFirstCheck = false;
							}
						}
					}
					else
					{
						if (KeysiteDmm.IsReadData())
						{
							nProcessStep[nStepIndex] = 43025;
							if (_SysInfo.bFirstCheck)
							{
								tMainTimer[nStepIndex].Start(200);
								_SysInfo.bFirstCheck = false;
							}
						}
					}
					break;

				case 43025:

					if (_Config.bDmmEtcMode)
					{
						double.TryParse(_KeysiteDmmEtc.strReadMessage, out _SysInfo.dbCommReadData);
						AppendDebugMsg($"Curr {_SysInfo.dbCommReadData}", "CURR");
						cellT.Add(_SysInfo.dbCommReadData);
						nProcessStep[nStepIndex] = 43026;
					}
					else
					{
						double.TryParse(KeysiteDmm.GetReadData(), out _SysInfo.dbCommReadData);
						AppendDebugMsg($"Curr {_SysInfo.dbCommReadData}", "CURR");
						cellT.Add(_SysInfo.dbCommReadData);
						nProcessStep[nStepIndex] = 43026;

					}

					break;

				case 43026:
					if (!tMainTimer[nStepIndex].Verify())
					{
						nProcessStep[nStepIndex] = 43023;

					}
					else
					{
						_SysInfo.dbCommReadMinData = cellT.Min();
						nProcessStep[nStepIndex] = 43040;

					}
					break;


				case 43040:
					//theApp.AppendLogMsg(KeysiteDmm.GetReadData(), MSG_TYPE.INFO);
					//if (_Config.bDmmEtcMode)
					//{
					//	double.TryParse(_KeysiteDmmEtc.strReadMessage, out _SysInfo.dbCommReadData);
					//}
					//else
					//{
					//	double.TryParse(KeysiteDmm.GetReadData(), out _SysInfo.dbCommReadData);
					//}

					//theApp.AppendLogMsg(_SysInfo.dbCommReadData.ToString(), MSG_TYPE.INFO);
					//int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3, out _SysInfo.nBuffIndex);
					double.TryParse(new DataTable().Compute(string.Format($"{_SysInfo.dbCommReadMinData}{_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2}"), null).ToString(), out _SysInfo.dbCalcData);
					//theApp.AppendLogMsg(_SysInfo.dbCalcData.ToString(), MSG_TYPE.INFO);
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMin, out _SysInfo.dbSpecMin);
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMax, out _SysInfo.dbSpecMax);

					// 0은 양쪽다 비교 , 1은 Min만 비교, 2는 Max만 비교
					//if (_SysInfo.nBuffIndex == 0)
					//{
					if (_SysInfo.dbCalcData > _SysInfo.dbSpecMax || _SysInfo.dbCalcData < _SysInfo.dbSpecMin)
					{
						if (_SysInfo.nCurrNGRetryCount > 5)
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "NG");
							_SysInfo.bTestNG = true;
						}
						else
						{
							_SysInfo.nCurrNGRetryCount++;
							AppendDebugMsg($"Curr NG {_SysInfo.dbCalcData}", "CURR");
							nProcessStep[nStepIndex] = 43000;
							break;
						}


					}
					else
					{

						TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "OK");
					}
					//}
					//else if (_SysInfo.nBuffIndex == 1)
					//{
					//	if (_SysInfo.dbCalcData < _SysInfo.dbSpecMin)
					//	{
					//		TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "NG");
					//	}
					//	else
					//	{
					//		TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "OK");
					//	}
					//}
					//else if (_SysInfo.nBuffIndex == 2)
					//{
					//	if (_SysInfo.dbCalcData > _SysInfo.dbSpecMax)
					//	{
					//		TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "NG");
					//	}
					//	else
					//	{
					//		TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "OK");
					//	}
					//}

					nProcessStep[nStepIndex] = 43050;
					break;

				case 43050:
					_SysInfo.nCurrNGRetryCount = 0;
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				case 44000:
					nProcessStep[nStepIndex] = 44005;
					break;

				case 44005:
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc.Send("*RST", true);
						nProcessStep[nStepIndex] = 44010;
					}
					else
					{
						KeysiteDmm.Send("*RST");
						nProcessStep[nStepIndex] = 44010;
					}

					break;

				// 1번 채널 Power Supply 설정
				case 44010:
					if (_Config.bDmmEtcMode)
					{

						_KeysiteDmmEtc.Send("*IDN?", true);
						nProcessStep[nStepIndex]++;
					}
					else
					{
						KeysiteDmm.Send("*IDN?");
						nProcessStep[nStepIndex]++;
					}

					break;

				case 44011:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc.IsReadData())
						{
							nProcessStep[nStepIndex] = 44020;
						}
					}
					else
					{
						if (KeysiteDmm.IsReadData())
						{
							nProcessStep[nStepIndex] = 44020;
						}
					}

					break;

				// 전압설정
				case 44020:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nDmmCh);
					if (_Config.bDmmEtcMode)
					{

						if (_SysInfo.nDmmCh > 20)
						{
							_KeysiteDmmEtc.Send($"SENSe:FUNCtion \"VOLTage\", (@{200 + _SysInfo.nDmmCh - 20})", true);
						}
						else
						{
							_KeysiteDmmEtc.Send($"SENSe:FUNCtion \"VOLTage\", (@{100 + _SysInfo.nDmmCh})", true);
						}
					}
					else
					{
						if (_SysInfo.nDmmCh > 20)
						{
							KeysiteDmm.Send($"SENSe:FUNCtion \"VOLTage\", (@{200 + _SysInfo.nDmmCh - 20})");
						}
						else
						{
							KeysiteDmm.Send($"SENSe:FUNCtion \"VOLTage\", (@{100 + _SysInfo.nDmmCh})");
						}
					}
					nProcessStep[nStepIndex] = 44021;
					break;

				case 44021:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nDmmCh);
					if (_Config.bDmmEtcMode)
					{

						if (_SysInfo.nDmmCh > 20)
						{
							_KeysiteDmmEtc.Send($"ROUT:SCAN (@{200 + _SysInfo.nDmmCh - 20})", true);
						}
						else
						{
							_KeysiteDmmEtc.Send($"ROUT:SCAN (@{100 + _SysInfo.nDmmCh})", true);
						}
					}
					else
					{
						if (_SysInfo.nDmmCh > 20)
						{
							KeysiteDmm.Send($"ROUT:SCAN (@{200 + _SysInfo.nDmmCh - 20})");
						}
						else
						{
							KeysiteDmm.Send($"ROUT:SCAN (@{100 + _SysInfo.nDmmCh})");
						}
					}
					nProcessStep[nStepIndex] = 44026;
					break;

				//case 44025:

				//	int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nDmmCh);
				//	if (_Config.bDmmEtcMode)
				//	{

				//		if (_SysInfo.nDmmCh > 20)
				//		{
				//			_KeysiteDmmEtc.Send($"SENS:VOLT:APER {_ModelInfo.dbDmmScanSpeed},(@{200 + _SysInfo.nDmmCh - 20})", true);
				//		}
				//		else
				//		{
				//			_KeysiteDmmEtc.Send($"SENS:VOLT:APER {_ModelInfo.dbDmmScanSpeed},(@{100 + _SysInfo.nDmmCh})", true);
				//		}
				//	}
				//	else
				//	{
				//		if (_SysInfo.nDmmCh > 20)
				//		{
				//			KeysiteDmm.Send($"SENS:VOLT:APER {_ModelInfo.dbDmmScanSpeed},(@{200 + _SysInfo.nDmmCh - 20})");
				//		}
				//		else
				//		{
				//			KeysiteDmm.Send($"SENS:VOLT:APER {_ModelInfo.dbDmmScanSpeed},(@{100 + _SysInfo.nDmmCh})");
				//		}
				//	}
				//	nProcessStep[nStepIndex] = 44030;
				//	break;

				case 44026:

					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nDmmCh);
					if (_Config.bDmmEtcMode)
					{

						if (_SysInfo.nDmmCh > 20)
						{
							_KeysiteDmmEtc.Send($"ROUTe:CLOSe (@{200 + _SysInfo.nDmmCh - 20})", true);
						}
						else
						{
							_KeysiteDmmEtc.Send($"ROUTe:CLOSe (@{100 + _SysInfo.nDmmCh})", true);
						}
					}
					else
					{
						if (_SysInfo.nDmmCh > 20)
						{
							KeysiteDmm.Send($"ROUTe:CLOSe (@{200 + _SysInfo.nDmmCh - 20})");
						}
						else
						{
							KeysiteDmm.Send($"ROUTe:CLOSe (@{100 + _SysInfo.nDmmCh})");
						}
					}
					nProcessStep[nStepIndex] = 44030;
					break;

				case 44030:
					//KeysiteDmm.Send($"MEAS:VOLT:DC? (@{_SysInfo.strDmmCh}");
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc.Send($"READ?", true);
					}
					else
					{
						KeysiteDmm.Send($"READ?");
					}
					nProcessStep[nStepIndex]++;
					break;

				case 44031:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc.IsReadData())
						{
							nProcessStep[nStepIndex] = 44040;
						}
					}
					else
					{
						if (KeysiteDmm.IsReadData())
						{
							nProcessStep[nStepIndex] = 44040;
						}
					}
					break;

				case 44040:

					//theApp.AppendLogMsg(KeysiteDmm.GetReadData(), MSG_TYPE.INFO);
					if (_Config.bDmmEtcMode)
					{
						double.TryParse(_KeysiteDmmEtc.strReadMessage, out _SysInfo.dbCommReadData);
					}
					else
					{
						double.TryParse(KeysiteDmm.GetReadData(), out _SysInfo.dbCommReadData);
					}

					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3, out _SysInfo.nBuffIndex);
					double.TryParse(new DataTable().Compute(string.Format($"{_SysInfo.dbCommReadData}{_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2}"), null).ToString(), out _SysInfo.dbCalcData);

					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMin, out _SysInfo.dbSpecMin);
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMax, out _SysInfo.dbSpecMax);

					//AppendLogMsg($"_SysInfo.dbCalcData = {_SysInfo.dbCalcData.ToString("F10")}", MSG_TYPE.LOG);

					_SysInfo.dbRMSCommReadData = (_SysInfo.dbCalcData / _ModelInfo.dbResistance) * 1000;
					// 0은 양쪽다 비교 , 1은 Min만 비교, 2는 Max만 비교
					if (_SysInfo.nBuffIndex == 0)
					{
						if (_SysInfo.dbRMSCommReadData > _SysInfo.dbSpecMax || _SysInfo.dbRMSCommReadData < _SysInfo.dbSpecMin)
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbRMSCommReadData.ToString("F4"), "NG");
							_SysInfo.bTestNG = true;
						}
						else
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbRMSCommReadData.ToString("F4"), "OK");
						}
					}
					else if (_SysInfo.nBuffIndex == 1)
					{
						if (_SysInfo.dbRMSCommReadData < _SysInfo.dbSpecMin)
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbRMSCommReadData.ToString("F4"), "NG");
							_SysInfo.bTestNG = true;
						}
						else
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbRMSCommReadData.ToString("F4"), "OK");
						}
					}
					else if (_SysInfo.nBuffIndex == 2)
					{
						if (_SysInfo.dbRMSCommReadData > _SysInfo.dbSpecMax)
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbRMSCommReadData.ToString("F4"), "NG");
							_SysInfo.bTestNG = true;
						}
						else
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbRMSCommReadData.ToString("F4"), "OK");
						}
					}

					nProcessStep[nStepIndex] = 44050;
					break;

				case 44050:
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				case 45000:
					nProcessStep[nStepIndex] = 45010;
					break;

				case 45010:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nDelayTime);
					tMainTimer[nStepIndex].Start(_SysInfo.nDelayTime);
					nProcessStep[nStepIndex] = 45020;
					break;

				case 45020:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				case 46000:
					nProcessStep[nStepIndex] = 46010;
					break;

				case 46010:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nSubEol);
					if (_SysInfo.nSubEol == 0)
					{
						_SysInfo.bSubEolStart = true;
						_SysInfo.nSubMainWorkStep = _SysInfo.nMainWorkStep;
						theApp.nProcessStep[(int)PROC_LIST.SUB_EOL] = 10000;
						nProcessStep[nStepIndex] = 46020;
					}
					else
					{
						_SysInfo.bSubEolStart = false;
						theApp.nProcessStep[(int)PROC_LIST.SUB_EOL] = 30000;
						TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
						nProcessStep[nStepIndex] = 46020;
					}
					break;

				case 46020:
					
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				case 47000:
					nProcessStep[nStepIndex] = 47005;
					break;

				//case 47005:
				//	KeysiteDmm.Send($"*CLS");
				//	nProcessStep[nStepIndex] = 47006;
				//	break;

				case 47005:
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc.Send("*RST", true);
						nProcessStep[nStepIndex] = 47010;
					}
					else
					{
						KeysiteDmm.Send("*RST");
						nProcessStep[nStepIndex] = 47010;
					}

					break;

				// 1번 채널 Power Supply 설정
				case 47010:
					if (_Config.bDmmEtcMode)
					{

						_KeysiteDmmEtc.Send("*IDN?", true);
						nProcessStep[nStepIndex]++;
					}
					else
					{
						KeysiteDmm.Send("*IDN?");
						nProcessStep[nStepIndex]++;
					}

					break;


				case 47011:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc.IsReadData())
						{
							nProcessStep[nStepIndex] = 47020;
						}
					}
					else
					{
						if (KeysiteDmm.IsReadData())
						{
							nProcessStep[nStepIndex] = 47020;
						}
					}

					break;

				// 전압설정
				case 47020:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nDmmCh);
					_SysInfo.strDmmCh = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1;
					//AppendLogMsg($"{_SysInfo.nDmmBuffIndex.ToString()}", MSG_TYPE.LOG);
					if (_Config.bDmmEtcMode)
					{

						_KeysiteDmmEtc.Send($"SENSe:FUNCtion \"VOLTage\", (@{_SysInfo.nDmmCh})", true);
					}
					else
					{
						KeysiteDmm.Send($"SENSe:FUNCtion \"VOLTage\", (@{_SysInfo.nDmmCh})");
					}

					//KeysiteDmm.Send($"SENS:VOLT:APER 0.20,(@101:120,201:210)");
					nProcessStep[nStepIndex] = 47021;
					break;


				case 47021:
					_SysInfo.strDmmCh = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1;
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nDmmCh);
					if (_Config.bDmmEtcMode)
					{

						_KeysiteDmmEtc.Send($"ROUTe:CLOSe (@{_SysInfo.nDmmCh})", true);
					}
					else
					{
						KeysiteDmm.Send($"ROUTe:CLOSe (@{_SysInfo.nDmmCh})");
					}

					nProcessStep[nStepIndex] = 47030;
					break;

				case 47022:
					_SysInfo.strDmmCh = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1;
					//if (_Config.bDmmEtcMode)
					//{

					//	_KeysiteDmmEtc.Send($"SENS:VOLT:APERture {_ModelInfo.dbDmmScanSpeed},(@{_SysInfo.strDmmCh})", true);
					//}
					//else
					//{
					//	KeysiteDmm.Send($"SENS:VOLT:APERture {_ModelInfo.dbDmmScanSpeed},(@{_SysInfo.strDmmCh})");
					//}


					nProcessStep[nStepIndex] = 47030;
					break;


				case 47030:

					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc.Send($"READ?", true);
					}
					else
					{
						KeysiteDmm.Send($"READ?");
					}
					//KeysiteDmm.Send($"MEAS:VOLT:DC? (@{_SysInfo.strDmmCh})");
					nProcessStep[nStepIndex] = 47035;
					break;

				case 47035:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc.IsReadData())
						{
							nProcessStep[nStepIndex] = 47040;
						}
					}
					else
					{
						if (KeysiteDmm.IsReadData())
						{
							nProcessStep[nStepIndex] = 47040;
						}
					}
					break;

				case 47040:
					if (_Config.bDmmEtcMode)
					{
						_SysInfo.strDmmReadData = _KeysiteDmmEtc.strReadMessage;
					}
					else
					{
						_SysInfo.strDmmReadData = KeysiteDmm.GetReadData();
					}

					double.TryParse(_SysInfo.strDmmReadData, out _SysInfo.dbDmmReadDataBuff[0]);

					_SysInfo.strDmmCh = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2;
					if (_SysInfo.strDmmCh == "")
					{
						nProcessStep[nStepIndex] = 47095;
					}
					else
					{
						nProcessStep[nStepIndex] = 47050;
					}
					break;

				//case 47050:
				//	if (_Config.bDmmEtcMode)
				//	{
				//		_KeysiteDmmEtc.Send("*RST", true);
				//		nProcessStep[nStepIndex] = 47051;
				//	}
				//	else
				//	{
				//		KeysiteDmm.Send("*RST");
				//		nProcessStep[nStepIndex] = 47051;
				//	}

				//	break;

				case 47050:
					_SysInfo.strDmmCh = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2;
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2, out _SysInfo.nDmmCh);
					//AppendLogMsg($"{_SysInfo.nDmmBuffIndex.ToString()}", MSG_TYPE.LOG);
					if (_Config.bDmmEtcMode)
					{

						_KeysiteDmmEtc.Send($"SENSe:FUNCtion \"VOLTage\", (@{_SysInfo.nDmmCh})", true);
					}
					else
					{
						KeysiteDmm.Send($"SENSe:FUNCtion \"VOLTage\", (@{_SysInfo.nDmmCh})");
					}

					//KeysiteDmm.Send($"SENS:VOLT:APER 0.20,(@101:120,201:210)");
					nProcessStep[nStepIndex]++;
					break;


				case 47051:
					_SysInfo.strDmmCh = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2;

					if (_Config.bDmmEtcMode)
					{

						_KeysiteDmmEtc.Send($"ROUTe:CLOSe (@{_SysInfo.nDmmCh})", true);
					}
					else
					{
						KeysiteDmm.Send($"ROUTe:CLOSe (@{_SysInfo.nDmmCh})");
					}

					nProcessStep[nStepIndex] = 47060;
					break;

				case 47053:
					_SysInfo.strDmmCh = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2;
					//if (_Config.bDmmEtcMode)
					//{

					//	_KeysiteDmmEtc.Send($"SENS:VOLT:APERture {_ModelInfo.dbDmmScanSpeed},(@{_SysInfo.strDmmCh})", true);
					//}
					//else
					//{
					//	KeysiteDmm.Send($"SENS:VOLT:APERture {_ModelInfo.dbDmmScanSpeed},(@{_SysInfo.strDmmCh})");
					//}


					nProcessStep[nStepIndex] = 47060;
					break;


				case 47060:

					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc.Send($"READ?", true);
					}
					else
					{
						KeysiteDmm.Send($"READ?");
					}
					//KeysiteDmm.Send($"MEAS:VOLT:DC? (@{_SysInfo.strDmmCh})");
					nProcessStep[nStepIndex] = 47065;
					break;

				case 47065:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc.IsReadData())
						{
							nProcessStep[nStepIndex] = 47066;
						}
					}
					else
					{
						if (KeysiteDmm.IsReadData())
						{
							nProcessStep[nStepIndex] = 47066;
						}
					}
					break;

				case 47066:
					if (_Config.bDmmEtcMode)
					{
						_SysInfo.strDmmReadData = _KeysiteDmmEtc.strReadMessage;
					}
					else
					{
						_SysInfo.strDmmReadData = KeysiteDmm.GetReadData();
					}
					double.TryParse(_SysInfo.strDmmReadData, out _SysInfo.dbDmmReadDataBuff[1]);

					_SysInfo.strDmmCh = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3;

					if (_SysInfo.strDmmCh == "")
					{
						nProcessStep[nStepIndex] = 47095;
					}
					else
					{
						nProcessStep[nStepIndex] = 47070;
					}
					break;

				//case 47070:
				//	if (_Config.bDmmEtcMode)
				//	{
				//		_KeysiteDmmEtc.Send("*RST", true);
				//		nProcessStep[nStepIndex] = 47051;
				//	}
				//	else
				//	{
				//		KeysiteDmm.Send("*RST");
				//		nProcessStep[nStepIndex] = 47051;
				//	}

				//	break;

				case 47070:
					_SysInfo.strDmmCh = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3;
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3, out _SysInfo.nDmmCh);
					//AppendLogMsg($"{_SysInfo.nDmmBuffIndex.ToString()}", MSG_TYPE.LOG);
					if (_Config.bDmmEtcMode)
					{

						_KeysiteDmmEtc.Send($"SENSe:FUNCtion \"VOLTage\", (@{_SysInfo.nDmmCh})", true);
					}
					else
					{
						KeysiteDmm.Send($"SENSe:FUNCtion \"VOLTage\", (@{_SysInfo.nDmmCh})");
					}

					//KeysiteDmm.Send($"SENS:VOLT:APER 0.20,(@101:120,201:210)");
					nProcessStep[nStepIndex]++;
					break;


				case 47071:
					_SysInfo.strDmmCh = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3;

					if (_Config.bDmmEtcMode)
					{

						_KeysiteDmmEtc.Send($"ROUTe:CLOSe (@{_SysInfo.nDmmCh})", true);
					}
					else
					{
						KeysiteDmm.Send($"ROUTe:CLOSe (@{_SysInfo.nDmmCh})");
					}

					nProcessStep[nStepIndex] = 47080;
					break;

				case 47072:
					_SysInfo.strDmmCh = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3;
					//if (_Config.bDmmEtcMode)
					//{

					//	_KeysiteDmmEtc.Send($"SENS:VOLT:APERture {_ModelInfo.dbDmmScanSpeed},(@{_SysInfo.strDmmCh})", true);
					//}
					//else
					//{
					//	KeysiteDmm.Send($"SENS:VOLT:APERture {_ModelInfo.dbDmmScanSpeed},(@{_SysInfo.strDmmCh})");
					//}


					nProcessStep[nStepIndex] = 47080;
					break;


				case 47080:

					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc.Send($"READ?", true);
					}
					else
					{
						KeysiteDmm.Send($"READ?");
					}
					//KeysiteDmm.Send($"MEAS:VOLT:DC? (@{_SysInfo.strDmmCh})");
					nProcessStep[nStepIndex] = 47085;
					break;

				case 47085:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc.IsReadData())
						{
							nProcessStep[nStepIndex] = 47086;
						}
					}
					else
					{
						if (KeysiteDmm.IsReadData())
						{
							nProcessStep[nStepIndex] = 47086;
						}
					}
					break;

				case 47086:
					if (_Config.bDmmEtcMode)
					{
						_SysInfo.strDmmReadData = _KeysiteDmmEtc.strReadMessage;
					}
					else
					{
						_SysInfo.strDmmReadData = KeysiteDmm.GetReadData();
					}
					double.TryParse(_SysInfo.strDmmReadData, out _SysInfo.dbDmmReadDataBuff[2]);
					nProcessStep[nStepIndex] = 47095;

					break;

				case 47095:
					TestResultSet(_SysInfo.nMainWorkStep, "", "OK");
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				case 48000:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nBuffIndex);
					double.TryParse(new DataTable().Compute(string.Format($"{_SysInfo.dbDmmReadDataBuff[_SysInfo.nBuffIndex]}{_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2}"), null).ToString(), out _SysInfo.dbCalcData);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3, out _SysInfo.nDispLen);

					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMin, out _SysInfo.dbSpecMin);
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMax, out _SysInfo.dbSpecMax);


					_SysInfo.dbRMSCommReadData = (_SysInfo.dbCalcData / _ModelInfo.dbResistance) * 1000;

					if (_SysInfo.dbRMSCommReadData > _SysInfo.dbSpecMax || _SysInfo.dbRMSCommReadData < _SysInfo.dbSpecMin)
					{
						TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbRMSCommReadData.ToString("F" + _SysInfo.nDispLen.ToString()), "NG");
						_SysInfo.bTestNG = true;
					}
					else
					{
						TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbRMSCommReadData.ToString("F" + _SysInfo.nDispLen.ToString()), "OK");
					}
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;


				case 49000:
					_SysInfo.dbCalcData = 0.0;
					_SysInfo.dbCommReadData = 0.0;
					nProcessStep[nStepIndex] = 49005;
					break;

				case 49005:
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc.Send("*RST", true);
						nProcessStep[nStepIndex] = 49010;
					}
					else
					{
						KeysiteDmm.Send("*RST");
						nProcessStep[nStepIndex] = 49010;
					}

					break;

				// 1번 채널 Power Supply 설정
				case 49010:
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc.Send("*IDN?", true);
						nProcessStep[nStepIndex]++;
					}
					else
					{
						KeysiteDmm.Send("*IDN?");
						nProcessStep[nStepIndex]++;
					}
					break;

				case 49011:
					_SysInfo.bFirstCheck = true;
					if (_Config.bDmmEtcMode)
					{
						if (_KeysiteDmmEtc.IsReadData())
						{
							//tMainTimer[nStepIndex].Start(200);
							nProcessStep[nStepIndex] = 49020;
						}
					}
					else
					{
						if (KeysiteDmm.IsReadData())
						{
							//tMainTimer[nStepIndex].Start(200);
							nProcessStep[nStepIndex] = 49020;
						}
					}
					break;

				case 49020:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nDmmCh);
					if (_Config.bDmmEtcMode)
					{
						//if (_SysInfo.nDmmCh == 1)
						//{
						//	_KeysiteDmmEtc.Send($"CONF:CURR:DC (@122)",true);
						//}
						//else
						//{
						_KeysiteDmmEtc.Send($"SENSe:FUNCtion \"CURRent\", (@{_SysInfo.nDmmCh})", true);
						//}
					}
					else
					{

						KeysiteDmm.Send($"SENSe:FUNCtion \"CURRent\", (@{_SysInfo.nDmmCh})");

					}

					nProcessStep[nStepIndex]++;
					break;

				case 49021:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nDmmCh);
					if (_Config.bDmmEtcMode)
					{
						//if (_SysInfo.nDmmCh == 1)
						//{
						//	_KeysiteDmmEtc.Send($"ROUT:SCAN (@{_SysInfo.nDmmCh})",true);
						//}
						//else
						//{
						_KeysiteDmmEtc.Send($"ROUTe:CLOSe (@{_SysInfo.nDmmCh})", true);
						AppendDebugMsg($"Curr A  검사 시작", "CURR");
						//}
					}
					else
					{
						//if (_SysInfo.nDmmCh == 1)
						//{
						//	KeysiteDmm.Send($"ROUT:SCAN (@{_SysInfo.nDmmCh})");
						//}
						//else
						//{
						KeysiteDmm.Send($"ROUTe:CLOSe (@{_SysInfo.nDmmCh})");
						AppendDebugMsg($"Curr A  검사 시작", "CURR");
						//}
					}


					nProcessStep[nStepIndex]++;
					break;

				case 49022:

					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nDmmCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue3, out _SysInfo.nDmmTime);

					//if(_SysInfo.nDmmTime == 1)
					//{
					//	nProcessStep[nStepIndex] = 43023;
					//}
					//else
					//{
					//if (_Config.bDmmEtcMode)
					//{
					//	//if (_SysInfo.nDmmCh == 1)
					//	//{
					//	//	_KeysiteDmmEtc.Send($"SENS:CURR:APER {_ModelInfo.dbDmmAScanSpeed},(@{_SysInfo.nDmmCh})", true);
					//	//}
					//	//else
					//	//{
					//	_KeysiteDmmEtc.Send($"SENS:CURR:APER {_ModelInfo.dbDmmMScanSpeed},(@{_SysInfo.nDmmCh})", true);
					//	//}
					//}
					//else
					//{
					//	//if (_SysInfo.nDmmCh == 1)
					//	//{
					//	//	KeysiteDmm.Send($"SENS:CURR:APER {_ModelInfo.dbDmmAScanSpeed},(@{_SysInfo.nDmmCh})");
					//	//}
					//	//else
					//	//{
					//	KeysiteDmm.Send($"SENS:CURR:APER {_ModelInfo.dbDmmMScanSpeed},(@{_SysInfo.nDmmCh})");
					//	//}
					//}
					//tMainTimer[nStepIndex].Start(3000);
					nProcessStep[nStepIndex] = 49023;

					//}
					break;


				case 49023:
					//if (!tMainTimer[nStepIndex].Verify()) { break; }
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc.Send($"READ?", true);
					}
					else
					{
						KeysiteDmm.Send($"READ?");
					}

					//KeysiteDmm.Send($"MEAS:VOLT:DC? (@{_SysInfo.strDmmCh})");
					nProcessStep[nStepIndex] = 49024;
					break;

				case 49024:


					if (_Config.bDmmEtcMode)
					{
						if (_KeysiteDmmEtc.IsReadData())
						{
							nProcessStep[nStepIndex] = 49025;

							if (_SysInfo.bFirstCheck)
							{
								tMainTimer[nStepIndex].Start(1000);
								_SysInfo.bFirstCheck = false;
							}
						}
					}
					else
					{
						if (KeysiteDmm.IsReadData())
						{
							nProcessStep[nStepIndex] = 49025;
							if (_SysInfo.bFirstCheck)
							{
								tMainTimer[nStepIndex].Start(1000);
								_SysInfo.bFirstCheck = false;
							}
						}
					}
					break;

				case 49025:

					if (_Config.bDmmEtcMode)
					{
						double.TryParse(_KeysiteDmmEtc.strReadMessage, out _SysInfo.dbCommReadData);
						AppendDebugMsg($"Curr AVG : {_SysInfo.dbCommReadData}", "CURR");
						cellT.Add(_SysInfo.dbCommReadData);
						nProcessStep[nStepIndex] = 49026;
					}
					else
					{
						double.TryParse(KeysiteDmm.GetReadData(), out _SysInfo.dbCommReadData);
						AppendDebugMsg($"Curr AVG : {_SysInfo.dbCommReadData}", "CURR");
						cellT.Add(_SysInfo.dbCommReadData);
						nProcessStep[nStepIndex] = 49026;

					}

					break;

				case 49026:
					if (!tMainTimer[nStepIndex].Verify())
					{
						nProcessStep[nStepIndex] = 49023;

					}
					else
					{
						_SysInfo.dbCommReadMinData = cellT.Average();
						nProcessStep[nStepIndex] = 49040;

					}
					break;


				case 49040:
					
					double.TryParse(new DataTable().Compute(string.Format($"{_SysInfo.dbCommReadMinData}{_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2}"), null).ToString(), out _SysInfo.dbCalcData);
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMin, out _SysInfo.dbSpecMin);
					double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMax, out _SysInfo.dbSpecMax);

					// 0은 양쪽다 비교 , 1은 Min만 비교, 2는 Max만 비교
					//if (_SysInfo.nBuffIndex == 0)
					//{
					if (_SysInfo.dbCalcData > _SysInfo.dbSpecMax || _SysInfo.dbCalcData < _SysInfo.dbSpecMin)
					{
						if (_SysInfo.nCurrNGRetryCount > 5)
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "NG");
							_SysInfo.bTestNG = true;
						}
						else
						{
							_SysInfo.nCurrNGRetryCount++;
							AppendDebugMsg($"Curr NG {_SysInfo.dbCalcData}", "CURR");
							nProcessStep[nStepIndex] = 49000;
							break;
						}


					}
					else
					{
						TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "OK");
					}
					//}
					//else if (_SysInfo.nBuffIndex == 1)
					//{
					//	if (_SysInfo.dbCalcData < _SysInfo.dbSpecMin)
					//	{
					//		TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "NG");
					//	}
					//	else
					//	{
					//		TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "OK");
					//	}
					//}
					//else if (_SysInfo.nBuffIndex == 2)
					//{
					//	if (_SysInfo.dbCalcData > _SysInfo.dbSpecMax)
					//	{
					//		TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "NG");
					//	}
					//	else
					//	{
					//		TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbCalcData.ToString("F4"), "OK");
					//	}
					//}

					nProcessStep[nStepIndex] = 49050;
					break;

				case 49050:
					_SysInfo.nCurrNGRetryCount = 0;
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;
				
				case 50000:
					bool bMidTestResult = true;
					for (int i = 0; i < _TestData.Count; i++)
					{
						if (_TestData[i].strResult == "NG")
						{
							bMidTestResult = false;
						}
					}

					if (bMidTestResult)
					{
						nProcessStep[nStepIndex] = 50001;
					}
					else
					{
						nProcessStep[nStepIndex] = 80000;
					}
					
					break;
				
				case 50001:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue2, out _SysInfo.nTipMaxCount);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nSetNutSch);
					_SysInfo.strTitleName = _ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strTestName;
					_SysInfo.bTiteIngStart = true;
					_SysInfo.bNutRetry = false;
					_SysInfo.bNutNext = false;
					//_SysInfo.nVoltCount++;
					ShowUserNutMessege();
					nProcessStep[nStepIndex] = 50020;
					break;

				case 50020:
					if (_SysInfo.bTiteOk)
					{
						HideUserNutMessege();
						_SysInfo.bNutRetryCheckOK = false;
						_SysInfo.bTiteIngStart = false;
						SetNutRunnerSch(50);
						//_SysInfo.dbNutData = _Nutrunner.dbTorqueData * 0.01;
						//TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbNutData.ToString("F2"), "OK");
						//_SysInfo.bTiteIngStart = false;
						//_SysInfo.bTiteOk = false;
						//SetNutRunnerSch(50);   // 너트러너 스케줄 설정
						//_SysInfo.nMainWorkStep++;
						nProcessStep[nStepIndex] = 50050;
					}
					break;

				case 50050:
					ShowNutRetryMessege();
					nProcessStep[nStepIndex] = 50055;
					break;

				case 50055:
					if (_SysInfo.bNutRetryCheckOK)
					{
						_SysInfo.bNutRetryCheckOK = false;
						nProcessStep[nStepIndex] = 50060;
					}
					else if (GetDIOPort(DI.START_SW1) && !GetDIOPort(DI.START_SW2))
					{
						HideNutRetryMessege();
						_SysInfo.bNutNext = true;
						_SysInfo.bNutRetry = false;
						nProcessStep[nStepIndex] = 50060;
					}
					else if (!GetDIOPort(DI.START_SW1) && GetDIOPort(DI.START_SW2))
					{
						HideNutRetryMessege();
						_SysInfo.bNutNext = false;
						_SysInfo.bNutRetry = true;
						nProcessStep[nStepIndex] = 50060;
					}
					break;

				case 50060:
					//if (_SysInfo.bNutRetryCheckOK)
					//{
						if (_SysInfo.bNutNext && !_SysInfo.bNutRetry)
						{
							_SysInfo.dbNutData = _Nutrunner.dbTorqueData * 0.01;
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.dbNutData.ToString("F2"), "OK");
							_SysInfo.bTiteIngStart = false;
							SetNutRunnerSch(50);   // 너트러너 스케줄 설정
							_SysInfo.bTiteOk = false;
							_SysInfo.nMainWorkStep++;
							nProcessStep[nStepIndex] = 3000;
						}
						else if (!_SysInfo.bNutNext && _SysInfo.bNutRetry)
						{
							//_SysInfo.nVoltCount--;
							nProcessStep[nStepIndex] = 3000;
						}
					//}
					
					break;


				case 51000:
					_SysInfo._FileNameResult = CyclonFileName_RESULT.READY;
					_CyStatus = CYCLON_STATUS.READY;
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strValue1, out _SysInfo.nCyclonHandle);
					CyclonReadFirWareName(_SysInfo.nCyclonHandle, _ModelInfo._TestInfo[_SysInfo.nMainWorkStep].strSpecMin);
					nProcessStep[nStepIndex] = 51005;
					break;

				case 51005:

					if(_SysInfo._FileNameResult != CyclonFileName_RESULT.READY)
					{
						if (_SysInfo._FileNameResult == CyclonFileName_RESULT.OK)
						{
							nProcessStep[nStepIndex] = 51009;
						}
						else
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.strCyclonFileName, "NG");
							_SysInfo.bTestNG = true;
							_SysInfo.nMainWorkStep++;
							nProcessStep[nStepIndex] = 3000;
							break;
						}
						
					}
					break;

				case 51009:
					CyclonInFirWare(_SysInfo.nCyclonHandle);
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex] = 51010;
					break;

				case 51010:
					if (!tMainTimer[nStepIndex].Verify()) { break; }

					if(_CyStatus != CYCLON_STATUS.READY)
					{
						if (_CyStatus == CYCLON_STATUS.OK)
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.strCyclonFileName, "OK");

							nProcessStep[nStepIndex] = 51050;
						}
						else
						{
							TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.strCyclonFileName, "NG");
							_SysInfo.bTestNG = true;
							nProcessStep[nStepIndex] = 51050;
						}

					}
					
					break;
					 
				case 51050:
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				case 52000:
					if (_ModelInfo.bUseRbmsTest)
					{
						nProcessStep[nStepIndex] = 52050;
					}
					else
					{
						_SysInfo._PopupStatus = MAIN_STATUS.READY;
						_BcdReader.strReadBarcode = "";
						if (_Config.strLanguage == "ENGLISH")
						{
							_SysInfo.strBcdPopupContent = "Please scan the PBMS barcode.";
						}
						else
						{
							_SysInfo.strBcdPopupContent = "PBMS 바코드를 스캔하여 주세요.";
						}
						_BcdReader.bReadOk = false;
						ShowBcdPopUpWindow();
						nProcessStep[nStepIndex] = 52005;
					}
					
					break;

				case 52005:
					if (_BcdReader.bReadOk)
					{
						_BcdReader.bReadOk = false;
						CloseBcdPopUpWindow();


						if (CheckBarcode( _BcdReader.strReadBarcode, _ModelInfo.strBarcodSymbol))
						{
							_SysInfo.strPBMSBcd = _BcdReader.strReadBarcode;							
							_SysInfo.strDispBarcodeBack = _SysInfo.strPBMSBcd.Substring(10, 12);
							nProcessStep[nStepIndex] = 52010;
						}
						else
						{
							nProcessStep[nStepIndex] = 52008;
						}
					}
					break;

				case 52008:
					_SysInfo._PopupStatus = MAIN_STATUS.NG;
					_SysInfo.nTL_Beep = 3;
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo.strBcdPopupContent = "Please scan the correct PBMS barcode.";
					}
					else
					{
						_SysInfo.strBcdPopupContent = "올바른 PBMS 바코드를 스캔하여 주세요.";
					}
					_BcdReader.bReadOk = false;
					ShowBcdPopUpWindow();
					nProcessStep[nStepIndex] = 52005;
					break;

				case 52010:
					_SysInfo._PopupStatus = MAIN_STATUS.READY;
					_BcdReader.strReadBarcode = "";
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo.strBcdPopupContent = "Please scan the Fuse barcode.";
					}
					else
					{
						_SysInfo.strBcdPopupContent = "Fuse 바코드를 스캔하여 주세요.";
					}
					_BcdReader.bReadOk = false;
					ShowBcdPopUpWindow();
					nProcessStep[nStepIndex] = 52015;
					break;

				case 52015:
					if (_BcdReader.bReadOk)
					{
						_BcdReader.bReadOk = false;
						CloseBcdPopUpWindow();

						if (CheckBarcode( _BcdReader.strReadBarcode, _ModelInfo.strFuseBarcodSymbol))
						{
							_SysInfo.strFuseBcd = _BcdReader.strReadBarcode;
							nProcessStep[nStepIndex] = 52020;
						}
						else
						{
							nProcessStep[nStepIndex] = 52018;
						}
					}
					break;

				case 52018:
					_SysInfo._PopupStatus = MAIN_STATUS.NG;
					_BcdReader.strReadBarcode = "";
					_SysInfo.nTL_Beep = 3;
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo.strBcdPopupContent = "Please scan the correct Fuse barcode.";
					}
					else
					{
						_SysInfo.strBcdPopupContent = "올바른 Fuse 바코드를 스캔하여 주세요.";
					}
					_BcdReader.bReadOk = false;
					ShowBcdPopUpWindow();
					nProcessStep[nStepIndex] = 52015;
					break;


				case 52020:
					_SysInfo._PopupStatus = MAIN_STATUS.READY;
					_BcdReader.strReadBarcode = "";
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo.strBcdPopupContent = "Please scan the Case barcode.";
					}
					else
					{
						_SysInfo.strBcdPopupContent = "Case 바코드를 스캔하여 주세요.";
					}
					_BcdReader.bReadOk = false;
					ShowBcdPopUpWindow();
					nProcessStep[nStepIndex] = 52025;
					break;

				case 52025:
					if (_BcdReader.bReadOk)
					{
						_BcdReader.bReadOk = false;
						CloseBcdPopUpWindow();

						if (CheckBarcode(_BcdReader.strReadBarcode,_ModelInfo.strCaseBarcodSymbol))
						{
							_SysInfo.strCaseBcd = _BcdReader.strReadBarcode;
							nProcessStep[nStepIndex] = 52030;
						}
						else
						{
							nProcessStep[nStepIndex] = 52028;
						}
					}
					break;

				case 52028:
					_SysInfo._PopupStatus = MAIN_STATUS.NG;
					_BcdReader.strReadBarcode = "";
					_SysInfo.nTL_Beep = 3;
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo.strBcdPopupContent = "Please scan the correct Case barcode.";
					}
					else
					{
						_SysInfo.strBcdPopupContent = "올바른 Case 바코드를 스캔하여 주세요.";
					}
					_BcdReader.bReadOk = false;
					ShowBcdPopUpWindow();
					nProcessStep[nStepIndex] = 52025;
					break;

				case 52030:
					//theApp._LotCount.nLotCount++;
					theApp._BarcodePrint.nBCDsize = theApp._ModelInfo.nBCDsize;
					theApp._BarcodePrint.nBCDStringHeight = theApp._ModelInfo.nBCDStringHeight;
					theApp._BarcodePrint.nBCDStringWidth = theApp._ModelInfo.nBCDStringWidth;
					theApp._BarcodePrint.nBCDbcdOffsetX = theApp._ModelInfo.nBCDbcdOffsetX;
					theApp._BarcodePrint.nBCDbcdOffsetY = theApp._ModelInfo.nBCDbcdOffsetY;
					theApp._BarcodePrint.nBCDtextOffsetX = theApp._ModelInfo.nBCDtextOffsetX;
					theApp._BarcodePrint.nBCDtextOffsetY = theApp._ModelInfo.nBCDtextOffsetY;
					theApp._BarcodePrint.strModelInfo = theApp._ModelInfo.strComment1;
					theApp._BarcodePrint.strPrintBCD = _SysInfo.strDispBarcodeBack;
					theApp._BarcodePrint.bManualMode = false;
					theApp._BarcodePrint.bPrintStart = true;
					//theApp.SaveModelProductCount(theApp._LotCount, theApp._ModelInfo.strModelName);
					SaveBarcodeInfo();
					nProcessStep[nStepIndex] = 52033;
					break;
				
				case 52033:
					_SysInfo._PopupStatus = MAIN_STATUS.READY;
					_BcdReader.strReadBarcode = "";
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo.strBcdPopupContent = "Please attach the Support Bracket barcode and scan it.";
					}
					else
					{
						_SysInfo.strBcdPopupContent = "Support Bracket 바코드를 부착 후 스캔하여 주세요.";
					}
					_BcdReader.bReadOk = false;
					ShowBcdPopUpWindow();
					nProcessStep[nStepIndex]++;
					break;

				case 52034:
					if (_BcdReader.bReadOk)
					{
						_BcdReader.bReadOk = false;
						CloseBcdPopUpWindow();

						if (_BcdReader.strReadBarcode == (_ModelInfo.strComment1 + _SysInfo.strDispBarcodeBack))
						{
							
							nProcessStep[nStepIndex] = 52040;
						}
						else
						{
							nProcessStep[nStepIndex] = 52035;
						}
					}
					break;


				case 52035:
					_SysInfo._PopupStatus = MAIN_STATUS.NG;
					_BcdReader.strReadBarcode = "";
					_SysInfo.nTL_Beep = 3;
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo.strBcdPopupContent = "Please scan the correct Support Bracket barcode.";
					}
					else
					{
						_SysInfo.strBcdPopupContent = "올바른 Support Bracket 바코드를 스캔하여 주세요.";
					}
					_BcdReader.bReadOk = false;
					ShowBcdPopUpWindow();
					nProcessStep[nStepIndex] = 52034;
					break;

				case 52040:
					TestResultSet(_SysInfo.nMainWorkStep, "OK", "OK");
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				case 52050:
					_SysInfo.strDispBarcodeFront = _SysInfo.strDispBarcode.Substring(0, 10);
					_SysInfo.strDispBarcodeBack = _SysInfo.strDispBarcode.Substring(10, 12);
					nProcessStep[nStepIndex] = 52055;
					break;


				case 52055:
					theApp._BarcodePrint.nBCDsize = theApp._ModelInfo.nBCDsize;
					theApp._BarcodePrint.nBCDStringHeight = theApp._ModelInfo.nBCDStringHeight;
					theApp._BarcodePrint.nBCDStringWidth = theApp._ModelInfo.nBCDStringWidth;
					theApp._BarcodePrint.nBCDbcdOffsetX = theApp._ModelInfo.nBCDbcdOffsetX;
					theApp._BarcodePrint.nBCDbcdOffsetY = theApp._ModelInfo.nBCDbcdOffsetY;
					theApp._BarcodePrint.nBCDtextOffsetX = theApp._ModelInfo.nBCDtextOffsetX;
					theApp._BarcodePrint.nBCDtextOffsetY = theApp._ModelInfo.nBCDtextOffsetY;
					theApp._BarcodePrint.strModelInfo = _SysInfo.strDispBarcodeFront;
					theApp._BarcodePrint.strPrintBCD = _SysInfo.strDispBarcodeBack;
					theApp._BarcodePrint.bManualMode = false;
					theApp._BarcodePrint.bPrintStart = true;
					nProcessStep[nStepIndex] = 52060;
					break;

				case 52060:
					_SysInfo._PopupStatus = MAIN_STATUS.READY;
					_BcdReader.strReadBarcode = "";
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo.strBcdPopupContent = "Please attach the RBMS barcode and scan it.";
						
					}
					else
					{
						_SysInfo.strBcdPopupContent = "RBMS 바코드를 부착 후 스캔하여 주세요.";
					}
					_BcdReader.bReadOk = false;
					ShowBcdPopUpWindow();
					nProcessStep[nStepIndex]++;
					break;

				case 52061:
					if (_BcdReader.bReadOk)
					{
						_BcdReader.bReadOk = false;
						CloseBcdPopUpWindow();

						if (CheckBarcode(_BcdReader.strReadBarcode, _ModelInfo.strRBMSBarcodSymbol))
						{

							nProcessStep[nStepIndex] = 52067;
						}
						else
						{
							nProcessStep[nStepIndex] = 52065;
						}
					}
					break;


				case 52065:
					_SysInfo._PopupStatus = MAIN_STATUS.NG;
					_BcdReader.strReadBarcode = "";
					_SysInfo.nTL_Beep = 3;
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo.strBcdPopupContent = "Please scan the correct RBMS barcode.";
					}
					else
					{
						_SysInfo.strBcdPopupContent = "올바른 RBMS 바코드를 스캔하여 주세요.";
					}
					_BcdReader.bReadOk = false;
					ShowBcdPopUpWindow();
					nProcessStep[nStepIndex] = 52067;
					break;

				case 52067:
					_SysInfo._PopupStatus = MAIN_STATUS.READY;
					_BcdReader.strReadBarcode = "";
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo.strBcdPopupContent = "Please scan the Mac Address barcode.";

					}
					else
					{
						_SysInfo.strBcdPopupContent = "Mac Adress 바코드를 스캔하여 주세요.";
					}
					_BcdReader.bReadOk = false;
					ShowBcdPopUpWindow();
					nProcessStep[nStepIndex]++;
					break;

				case 52068:
					if (_BcdReader.bReadOk)
					{
						_BcdReader.bReadOk = false;
						CloseBcdPopUpWindow();

						if (_BcdReader.strReadBarcode == _SysInfo.strMacAdress)
						{
							nProcessStep[nStepIndex] = 52070;
						}
						else
						{
							nProcessStep[nStepIndex] = 52069;
						}
					}
					break;
					

				case 52069:
					_SysInfo._PopupStatus = MAIN_STATUS.NG;
					_BcdReader.strReadBarcode = "";
					_SysInfo.nTL_Beep = 3;
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo.strBcdPopupContent = "Please scan the correct Mac Adress barcode.";
					}
					else
					{
						_SysInfo.strBcdPopupContent = "올바른 Mac Adress 바코드를 스캔하여 주세요.";
					}
					_BcdReader.bReadOk = false;
					ShowBcdPopUpWindow();
					nProcessStep[nStepIndex] = 52068;
					break;

				case 52070:
					TestResultSet(_SysInfo.nMainWorkStep, $"{_SysInfo.strMacAdress}", "OK");
					_SysInfo.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;




				// 검사 종료 스텝
				case 80000:
					//for (int i = 0; i < 32; i++)
					//{
					//	SetDIOPort((DO)i, false);
					//}
					PowerSupply[0].SendData("OUTP OFF");
					PowerSupply[1].SendData("OUTP OFF");
					_CellSimulator1.Send("SIM:OUTP OFF", true);
					_CellSimulator2.Send("SIM:OUTP OFF", true);
					ClosePopUpWindow();
					HideUserStartMessege();
					_SysInfo.bSubEolStart = false;
					_SysInfo.bTiteIngStart = false;
					_SysInfo.strMacAdress = "";
					SetNutRunnerSch(50);
					theApp.nProcessStep[(int)PROC_LIST.SUB_EOL] = 30000;
					theApp.nProcessStep[(int)PROC_LIST.SUB_TITE1] = 0;
					nProcessStep[nStepIndex] = 85000;
					break;

				// 검사 정상종료 스텝
				case 85000:
					bool bTestResult = true;
					for (int i = 0; i < _TestData.Count; i++)
					{
						if (_TestData[i].strResult == "NG")
						{
							bTestResult = false;
						}
					}
					if (_SysInfo.bEMGStop)
					{
						_SysInfo.eMainStatus = MAIN_STATUS.EMG_STOP;
						_SysInfo.nTL_Beep = 5;
						_LotCount.nNGCount++;
						SaveModelProductCount(_LotCount, _ModelInfo.strModelName);

						_LotCount.nTotalCount++;
						SaveProductCount();
						_SysInfo.strTotalResult = "USER_STOP";
					}
					else
					{
						if (bTestResult)
						{
							_SysInfo.eMainStatus = MAIN_STATUS.OK;
							_SysInfo.nTL_Beep = 2;
							_LotCount.nOkCount++;
							SaveModelProductCount(_LotCount, _ModelInfo.strModelName);
							_LotCount.nTotalCount++;
							SaveProductCount();
							_SysInfo.strTotalResult = "OK";
							//_LotCount.nLotCount++;
						}
						else
						{
							_SysInfo.eMainStatus = MAIN_STATUS.NG;
							_SysInfo.nTL_Beep = 5;
							_LotCount.nNGCount++;
							SaveModelProductCount(_LotCount, _ModelInfo.strModelName);
							_LotCount.nTotalCount++;
							SaveProductCount();
							_SysInfo.strTotalResult = "NG";
							//_LotCount.nLotCount++;
						}
					}

					if (_Config.bUseMasterCheck)
					{
						// OK 샘플 검사 작업시 
						if (_SysInfo.bOkMasterSampleTestIng)
						{
							if (_SysInfo.strTotalResult == "OK")
							{
								_MasterTestInfo.bMasterOkSampleTestFinish = true;
								_MasterTestInfo.dtMasterOkSampleTestTime = DateTime.Now;
								SaveMasterTestInfo(_MasterTestInfo, _ModelInfo.strModelName);
								_SysInfo.eMainStatus = MAIN_STATUS.OK_MASTER_OK;
							}
							else
							{
								_SysInfo.eMainStatus = MAIN_STATUS.OK_MASTER_NG;
							}
						}
						else if (_SysInfo.bNgMasterSampleTestIng)
						{
							if (_SysInfo.strTotalResult == "NG")
							{

								_MasterTestInfo.bMasterNgSampleTestFinish = true;
								_MasterTestInfo.dtMasterNgSampleTestTime = DateTime.Now;
								SaveMasterTestInfo(_MasterTestInfo, _ModelInfo.strModelName);
								_SysInfo.eMainStatus = MAIN_STATUS.NG_MASTER_OK;
							}
							else
							{
								_SysInfo.eMainStatus = MAIN_STATUS.NG_MASTER_NG;
							}
						}
					}




					_SysInfo.dtTestEndTime = DateTime.Now;

					_tTackTimer.Stop();
					TestTotalResultView(_SysInfo.strDispBarcode, _SysInfo.strTotalResult);
					SaveResultData();
					SetDIOPort(DO.RY_01 + 1 - 1, false);
					SetDIOPort(DO.RY_01 + 15 - 1, false);
					SetDIOPort(DO.RY_01 + 16 - 1, false);
					SetDIOPort(DO.RY_01 + 17 - 1, false);
					SetDIOPort(DO.RY_01 + 18 - 1, false);
					SetDIOPort(DO.RY_01 + 19 - 1, false);
					SetDIOPort(DO.RY_01 + 20 - 1, false);
					SetDIOPort(DO.RY_01 + 21 - 1, false);
					SetDIOPort(DO.RY_01 + 22 - 1, false);
					SetDIOPort(DO.RY_01 + 23 - 1, false);
					SetDIOPort(DO.RY_01 + 24 - 1, false);
					SetDIOPort(DO.RY_01 + 25 - 1, false);
					SetDIOPort(DO.RY_01 + 26 - 1, false);
					SetDIOPort(DO.RY_01 + 27 - 1, false);
					SetDIOPort(DO.RY_01 + 28 - 1, false);
					SetDIOPort(DO.RY_01 + 29 - 1, false);
					SetDIOPort(DO.RY_01 + 30 - 1, false);
					SetDIOPort(DO.RY_01 + 31 - 1, false);
					SetDIOPort(DO.RY_01 + 32 - 1, false);
					SetDIOPort(DO.RY_01 + 33 - 1, false);
					SetDIOPort(DO.RY_01 + 34 - 1, false);
					SetDIOPort(DO.RY_01 + 35 - 1, false);

					_SysInfo.bReadMacBcd = false;
					_SysInfo.bReadMainBcd = false;
					_SysInfo.nVoltCount = 0;
					nProcessStep[nStepIndex] = 86000;
					break;


				// 데이터 저장 스텝
				case 86000:

					nProcessStep[nStepIndex] = 100000;
					break;



				// 오류스텝
				case 90000:
					break;



				case 100000:
					nProcessStep[nStepIndex] = 0;
					break;
			}


		}

		public static void PROC_MAIN2()
		{

			int nStepIndex = (int)PROC_LIST.MAIN2;

			if (_SysInfo2.bReadMainBcd)
			{
				SetDIOPort(DO.SW_LAMP3, true);
				SetDIOPort(DO.SW_LAMP4, true);
			}
			else
			{
				SetDIOPort(DO.SW_LAMP3, false);
				SetDIOPort(DO.SW_LAMP4, false);
			}


			switch (nProcessStep[nStepIndex])
			{

				case 0:
					if (_BcdReader2.bReadOk)
					{
						_BcdReader2.bReadOk = false;

						_SysInfo2.strReadBarcode = _BcdReader2.strReadBarcode;

						if (_ModelInfo2.bUseRbmsTest && !_ModelInfo2.bUseRMDTestMode)
						{

							uint nReadSerialNum = 0;


							if (CheckBarcode(_SysInfo2.strReadBarcode, _ModelInfo2.strBarcodSymbol))
							{
								if (uint.TryParse(_SysInfo2.strReadBarcode.Substring(_ModelInfo2.nSerailNumIndex, 10), out nReadSerialNum))
								{

									// Mater 바코드 여부 판별
									if (CheckBarcode(_BcdReader2.strReadBarcode, _ModelInfo2.strMasterOkSampleBarcode))
									{
										// 마스터 바코드일 경우 마스터 검사 루틴 진행
										_SysInfo2.bOkMasterSampleTestIng = true;
										_SysInfo2.bNgMasterSampleTestIng = false;
										_SysInfo2.nWriteSerialNum = nReadSerialNum;
										_SysInfo2.strDispBarcode = _SysInfo2.strReadBarcode;
										_SysInfo2.bReadMainBcd = true;

										if (!_SysInfo2.bReadMacBcd) { _SysInfo2.strDispMac = ""; }

									}
									else if (CheckBarcode(_BcdReader2.strReadBarcode, _ModelInfo2.strMasterNgSampleBarcode))
									{
										// 마스터 바코드일 경우 마스터 검사 루틴 진행
										_SysInfo2.bOkMasterSampleTestIng = false;
										_SysInfo2.bNgMasterSampleTestIng = true;
										_SysInfo2.nWriteSerialNum = nReadSerialNum;
										_SysInfo2.strDispBarcode = _SysInfo2.strReadBarcode;
										_SysInfo2.bReadMainBcd = true;

										if (!_SysInfo2.bReadMacBcd) { _SysInfo2.strDispMac = ""; }
									}
									else
									{
										// 마스터 검사 진행여부 체크하는 루틴 추가
										if (_Config.bUseMasterCheck && !CheckMasterTestFinish(_ModelInfo2.strModelName))
										{

											// 마스터 팝업 발생

											_SysInfo2.nTL_Beep = 3;
											ShowMasterPopUpWindow();



										}
										else
										{
											_SysInfo2.bOkMasterSampleTestIng = false;
											_SysInfo2.bNgMasterSampleTestIng = false;
											_SysInfo2.nWriteSerialNum = nReadSerialNum;
											_SysInfo2.strDispBarcode = _SysInfo2.strReadBarcode;
											_SysInfo2.bReadMainBcd = true;

											if (!_SysInfo2.bReadMacBcd) { _SysInfo2.strDispMac = ""; }
										}

									}

								}
								else
								{
									theApp.AppendLogMsg2("Serial number format does not match.", MSG_TYPE.ERROR);
									_SysInfo2.nTL_Beep = 3;
								}
							}
							else if (_SysInfo2.strReadBarcode.Length == 12)
							{
								_SysInfo2.bReadMacOk = true;
								_SysInfo2.nReadMacHigh = 0;
								_SysInfo2.nReadMacLow = 0;

								_SysInfo2.strMacAdress = _SysInfo2.strReadBarcode;

								_SysInfo2.bReadMacOk &= int.TryParse(_SysInfo2.strReadBarcode.Substring(0, 2), System.Globalization.NumberStyles.HexNumber, null, out _SysInfo2.nReadMac);
								_SysInfo2.nReadMacHigh = _SysInfo2.nReadMac * 0x100;
								_SysInfo2.bReadMacOk &= int.TryParse(_SysInfo2.strReadBarcode.Substring(2, 2), System.Globalization.NumberStyles.HexNumber, null, out _SysInfo2.nReadMac);
								_SysInfo2.nReadMacHigh += _SysInfo2.nReadMac;
								_SysInfo2.bReadMacOk &= int.TryParse(_SysInfo2.strReadBarcode.Substring(4, 2), System.Globalization.NumberStyles.HexNumber, null, out _SysInfo2.nReadMac);
								_SysInfo2.nReadMacMid = _SysInfo2.nReadMac * 0x100;
								_SysInfo2.bReadMacOk &= int.TryParse(_SysInfo2.strReadBarcode.Substring(6, 2), System.Globalization.NumberStyles.HexNumber, null, out _SysInfo2.nReadMac);
								_SysInfo2.nReadMacMid += _SysInfo2.nReadMac;
								_SysInfo2.bReadMacOk &= int.TryParse(_SysInfo2.strReadBarcode.Substring(8, 2), System.Globalization.NumberStyles.HexNumber, null, out _SysInfo2.nReadMac);
								_SysInfo2.nReadMacLow = _SysInfo2.nReadMac * 0x100;
								_SysInfo2.bReadMacOk &= int.TryParse(_SysInfo2.strReadBarcode.Substring(10, 2), System.Globalization.NumberStyles.HexNumber, null, out _SysInfo2.nReadMac);
								_SysInfo2.nReadMacLow += _SysInfo2.nReadMac;

								if (_SysInfo2.bReadMacOk)
								{
									_SysInfo2.strDispMac = _SysInfo2.nReadMacHigh.ToString("X4") + _SysInfo2.nReadMacMid.ToString("X4") + _SysInfo2.nReadMacLow.ToString("X4");
									_SysInfo2.bReadMacBcd = true;
									if (!_SysInfo2.bReadMainBcd) { _SysInfo2.strDispBarcode = ""; }

								}
								else
								{
									theApp.AppendLogMsg2("Pbms Mac format does not match.", MSG_TYPE.ERROR);
									_SysInfo2.nTL_Beep = 3;
								}
							}
							else
							{
								theApp.AppendLogMsg2("Rbms Barcode format does not match.", MSG_TYPE.ERROR);
								_SysInfo2.nTL_Beep = 3;
							}

							if(_SysInfo2.bReadMainBcd && _SysInfo2.bReadMacBcd)
							{
								ShowUserStartMessege2();
								nProcessStep[nStepIndex] = 2;
							}
						}
						else
						{
							uint nReadSerialNum = 0;

							if (CheckBarcode(_SysInfo2.strReadBarcode, _ModelInfo2.strBarcodSymbol))
							{


								if (uint.TryParse(_SysInfo2.strReadBarcode.Substring(_ModelInfo2.nSerailNumIndex, 10), out nReadSerialNum))
								{

									// Mater 바코드 여부 판별
									if (CheckBarcode(_BcdReader2.strReadBarcode, _ModelInfo2.strMasterOkSampleBarcode))
									{
										// 마스터 바코드일 경우 마스터 검사 루틴 진행
										_SysInfo2.bOkMasterSampleTestIng = true;
										_SysInfo2.bNgMasterSampleTestIng = false;
										_SysInfo2.nWriteSerialNum = nReadSerialNum;
										_SysInfo2.strDispBarcode = _SysInfo2.strReadBarcode;
										_SysInfo2.bReadMainBcd = true;
										ShowUserStartMessege2();
										if (!_SysInfo2.bReadMacBcd) { _SysInfo2.strDispMac = ""; }
										nProcessStep[nStepIndex] = 2;

									}
									else if (CheckBarcode(_BcdReader2.strReadBarcode, _ModelInfo2.strMasterNgSampleBarcode))
									{
										// 마스터 바코드일 경우 마스터 검사 루틴 진행
										_SysInfo2.bOkMasterSampleTestIng = false;
										_SysInfo2.bNgMasterSampleTestIng = true;
										_SysInfo2.nWriteSerialNum = nReadSerialNum;
										_SysInfo2.strDispBarcode = _SysInfo2.strReadBarcode;
										_SysInfo2.bReadMainBcd = true;
										ShowUserStartMessege2();
										if (!_SysInfo2.bReadMacBcd) { _SysInfo2.strDispMac = ""; }
										nProcessStep[nStepIndex] = 2;
									}
									else
									{
										// 마스터 검사 진행여부 체크하는 루틴 추가
										if (_Config.bUseMasterCheck && !CheckMasterTestFinish(_ModelInfo2.strModelName))
										{

											// 마스터 팝업 발생

											_SysInfo.nTL_Beep = 3;
											ShowMasterPopUpWindow();
											nProcessStep[nStepIndex] = 2;

										}
										else
										{
											_SysInfo2.bOkMasterSampleTestIng = false;
											_SysInfo2.bNgMasterSampleTestIng = false;
											_SysInfo2.nWriteSerialNum = nReadSerialNum;
											_SysInfo2.strDispBarcode = _SysInfo2.strReadBarcode;
											_SysInfo2.bReadMainBcd = true;
											ShowUserStartMessege2();
											if (!_SysInfo2.bReadMacBcd) { _SysInfo2.strDispMac = ""; }
											nProcessStep[nStepIndex] = 2;
										}

									}

								}
								else
								{
									theApp.AppendLogMsg2("Serial number format does not match.", MSG_TYPE.ERROR);
									_SysInfo.nTL_Beep = 3;
								}
							}
							else
							{
								theApp.AppendLogMsg2("Barcode format does not match.", MSG_TYPE.ERROR);
								_SysInfo.nTL_Beep = 3;
							}
						}
						



					}

					break;

				case 2:
					if (GetDIOPort(DI.START_SW3) && GetDIOPort(DI.START_SW4))
					{
						tMainTimer[nStepIndex].Start(50);
						nProcessStep[nStepIndex]++;

					}
					break;

				case 3:
					if (GetDIOPort(DI.START_SW3) && GetDIOPort(DI.START_SW4) && tMainTimer[nStepIndex].Verify())
					{
						HideUserStartMessege2();
						nProcessStep[nStepIndex] = 1000;
					}
					else if (!GetDIOPort(DI.START_SW3) || !GetDIOPort(DI.START_SW4))
					{
					
						nProcessStep[nStepIndex] = 2;
					}
					break;

				case 100:
					break;



				//// Soket Test
				//case 50:
				//	tcpClient.Connect("127.0.0.1", 502)
				//	nProcessStep[nStepIndex]++;
				//	break;

				//case 51:
				//	if(_ModbusSoket.)
				//	break;


				// 작업 시작
				case 1000:
					if (_ModelInfo2.bUseRMDTestMode)
					{
						_Config.nEolPinCount2++;
						nProcessStep[nStepIndex] = 2000;
						break;
						//if (_SysInfo.bReadMainBcd)
						//{
						//	nProcessStep[nStepIndex] = 2000;
						//	return;
						//}
						//else
						//{
						//	if (!_SysInfo.bReadMainBcd) { AppendLogMsg2("바코드가 스캔되지 않습니다.", MSG_TYPE.ERROR); }
						//	_SysInfo.nTL_Beep = 3;
						//	nProcessStep[nStepIndex] = 0;
						//	return;
						//}

					}
					if (_ModelInfo.bUseRbmsTest && !_ModelInfo.bUseRMDTestMode)
					{

						if (_SysInfo2.bReadMainBcd && _SysInfo2.bReadMacBcd)
						{
							_Config.nEolPinCount++;
							nProcessStep[nStepIndex] = 2000;
						}
						else
						{
							if (!_SysInfo2.bReadMainBcd) { AppendLogMsg2("Barcode not scanning.", MSG_TYPE.ERROR); }
							if (!_SysInfo2.bReadMacBcd) { AppendLogMsg2("MAC Barcode not scanning.", MSG_TYPE.ERROR); }
							_SysInfo2.nTL_Beep = 3;
							nProcessStep[nStepIndex] = 0;
						}
					}
					else
					{
						if (_SysInfo2.bReadMainBcd /*&& _SysInfo.bReadMacBcd*/)
						{
							_Config.nEolPinCount2++;
							nProcessStep[nStepIndex] = 2000;
						}
						else
						{
							if (!_SysInfo2.bReadMainBcd) { AppendLogMsg2("Barcode not scanning.", MSG_TYPE.ERROR); }
							//if (!_SysInfo.bReadMacBcd) { AppendLogMsg2("MAC 바코드가 스캔되지 않습니다.", MSG_TYPE.ERROR); }
							_SysInfo.nTL_Beep = 3;
							nProcessStep[nStepIndex] = 0;
						}
					}

					
					break;



				// Unit 초기화 작업
				case 2000:
					_SysInfo.nTL_Beep = 1;
					_SysInfo2.nMainWorkStep = 0;
					_SysInfo2.nSubWorkStep = 0;
					_SysInfo2.bEMGStop = false;
					_SysInfo2.nVoltCount = 0;
					_SysInfo2.dtTestStartTime = DateTime.Now;
					_SysInfo2.strSaveFileName = _SysInfo2.strDispBarcode + DateTime.Now.ToString("_HHmmss");

					_tTackTimer2.Restart();
					NgDataClear2();

					SetDIOPort(DO.RY_01 + 2 - 1, false);
					SetDIOPort(DO.RY_01 + 36 - 1, false);
					SetDIOPort(DO.RY_01 + 37 - 1, false);
					SetDIOPort(DO.RY_01 + 38 - 1, false);
					SetDIOPort(DO.RY_01 + 39 - 1, false);
					SetDIOPort(DO.RY_01 + 40 - 1, false);
					SetDIOPort(DO.RY_01 + 41 - 1, false);
					SetDIOPort(DO.RY_01 + 42 - 1, false);
					SetDIOPort(DO.RY_01 + 43 - 1, false);
					SetDIOPort(DO.RY_01 + 44 - 1, false);
					SetDIOPort(DO.RY_01 + 45 - 1, false);
					SetDIOPort(DO.RY_01 + 46 - 1, false);
					SetDIOPort(DO.RY_01 + 47 - 1, false);
					SetDIOPort(DO.RY_01 + 48 - 1, false);
					SetDIOPort(DO.RY_01 + 49 - 1, false);
					SetDIOPort(DO.RY_01 + 50 - 1, false);
					SetDIOPort(DO.RY_01 + 51 - 1, false);
					SetDIOPort(DO.RY_01 + 52 - 1, false);
					SetDIOPort(DO.RY_01 + 53 - 1, false);
					SetDIOPort(DO.RY_01 + 54 - 1, false);
					SetDIOPort(DO.RY_01 + 55 - 1, false);
					SetDIOPort(DO.RY_01 + 56 - 1, false);

					App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
					{
						TestUIClearSet2();
					});

					tMainTimer[nStepIndex].Start(500);
					_SysInfo2.eMainStatus = MAIN_STATUS2.ING;
					nProcessStep[nStepIndex] = 2500;
					break;

				case 2500:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					nProcessStep[nStepIndex] = 3000;
					break;

				// 초기화 완료 검사 시작스텝
				case 3000:
					if (_SysInfo2.nMainWorkStep >= _ModelInfo2._TestInfo.Count)
					{
						nProcessStep[nStepIndex] = 80000;
					}
					else
					{
						if (_SysInfo2.nMainWorkStep > 0)
						{
							//if (_TestData2[_SysInfo2.nMainWorkStep - 1].strResult == "NG")
							//{
							//	// 불량 발생시 PopUp 발생
							//	AppendLogMsg("11111", MSG_TYPE.LOG);
							//	_SysInfo2.bTestNG = false;
							//	nProcessStep[nStepIndex] = 3100;
							//}
							//else
							//{
							//	AppendLogMsg("2222", MSG_TYPE.LOG);
							//	nProcessStep[nStepIndex] = 4000;
							//}

							if (_SysInfo2.bTestNG)
							{
								// 불량 발생시 PopUp 발생

								_SysInfo2.bTestNG = false;
								nProcessStep[nStepIndex] = 3100;
							}
							else
							{

								nProcessStep[nStepIndex] = 4000;
							}
						}
						else
						{
							nProcessStep[nStepIndex] = 4000;
						}
					}
					break;

				case 3100:
					_SysInfo2.strPopupContent = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep - 1].strTestName + " NG";
					_SysInfo2._SwStatus = MAIN_STATUS2.READY;
					_SysInfo2._PopupStatus = MAIN_STATUS2.NG;
					ShowNGPopUpWindow2();
					nProcessStep[nStepIndex]++;
					break;

				case 3101:
					if (_SysInfo2._SwStatus == MAIN_STATUS2.OK)
					{
						nProcessStep[nStepIndex] = 4000;
					}
					else if (_SysInfo2._SwStatus == MAIN_STATUS2.NG)
					{
						nProcessStep[nStepIndex] = 80000;
					}
					if (GetDIOPort(DI.START_SW3))
					{
						CloseNGPopUpWindow2();
						_SysInfo2._SwStatus = MAIN_STATUS2.OK;
						nProcessStep[nStepIndex] = 3102;
					}
					else if (GetDIOPort(DI.START_SW4))
					{
						CloseNGPopUpWindow2();
						_SysInfo2._SwStatus = MAIN_STATUS2.NG;
						nProcessStep[nStepIndex] = 3102;
					}
					break;

				case 3102:
					if (!GetDIOPort(DI.START_SW3) && !GetDIOPort(DI.START_SW4))
					{
						nProcessStep[nStepIndex]++;
					}
					break;

				case 3103:
					if (_SysInfo2._SwStatus == MAIN_STATUS2.OK)
					{
						nProcessStep[nStepIndex] = 4000;
					}
					else if (_SysInfo2._SwStatus == MAIN_STATUS2.NG)
					{
						nProcessStep[nStepIndex] = 80000;
					}
					break;


				case 4000:
					if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].bUseItem)
					{
						// EOL 검사 스텝
						if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 0)
						{
							_SysInfo2.nSubWorkStep = 0;
							_SysInfo2.bEolNg = false;
							nProcessStep[nStepIndex] = 20000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 1)
						{
							//ADC Calc Step
							nProcessStep[nStepIndex] = 30000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 2)
						{
							//PS1
							nProcessStep[nStepIndex] = 31000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 12)
						{
							//PS1 _ Curr
							nProcessStep[nStepIndex] = 31200;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 3)
						{
							//PS2
							nProcessStep[nStepIndex] = 32000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 13)
						{
							//PS2 _ Curr
							nProcessStep[nStepIndex] = 32200;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 4)
						{
							//IO Step
							
							nProcessStep[nStepIndex] = 33000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 5)
						{
							//DMM Step 전압
							nProcessStep[nStepIndex] = 34000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 6)
						{
							//DMM Step 전류
							nProcessStep[nStepIndex] = 35000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 7)
						{
							//DMM Step 저항
							nProcessStep[nStepIndex] = 36000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 8)
						{
							//DMM Step
							nProcessStep[nStepIndex] = 37000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 9)
						{
							//ping
							nProcessStep[nStepIndex] = 38000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 10)
						{
							//Version Parse (R_Platform)
							nProcessStep[nStepIndex] = 39000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 11)
						{
							//Version Parse (B_Platform)
							nProcessStep[nStepIndex] = 40000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 14)
						{
							// C/S_1
							nProcessStep[nStepIndex] = 41000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 15)
						{
							// C/S_2
							nProcessStep[nStepIndex] = 42000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 16)
						{
							// DMM(Curr)
							//_SysInfo2.bEolReadData = false;
							_SysInfo2.nCurrNGRetryCount = 0;
							cellT2.Clear();
							nProcessStep[nStepIndex] = 43000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 17)
						{
							// DMM(RMS)
							nProcessStep[nStepIndex] = 44000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 18)
						{
							// Delay
							nProcessStep[nStepIndex] = 45000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 19)
						{
							// EOL(Repeat)
							_SysInfo2.nRepeatWorkStep = 0;
							nProcessStep[nStepIndex] = 46000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 20)
						{
							// DMM(V/Count)
							nProcessStep[nStepIndex] = 47000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 21)
						{
							// ADC(DMM)
							nProcessStep[nStepIndex] = 48000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 22)
						{
							// DMM(Curr.A)
							_SysInfo2.nCurrNGRetryCount = 0;
							cellT2.Clear();
							nProcessStep[nStepIndex] = 49000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 23)
						{
							// 체결
							_SysInfo2.nTipNowCount = 0;
							nProcessStep[nStepIndex] = 50000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 24)
						{
							// 펌웨어 업데이트
							_SysInfo2.strCyclonFileName = "";
							_SysInfo2.bGetFileNameOK = false;
							nProcessStep[nStepIndex] = 51000;
						}
						else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].nTestItem == 25)
						{
							// Barcode Save
							_SysInfo2.strCaseBcd = "";
							_SysInfo2.strFuseBcd = "";
							_SysInfo2.strPBMSBcd = "";
							_SysInfo2.strDispBarcodeFront = "";
							_SysInfo2.strDispBarcodeBack = "";
							nProcessStep[nStepIndex] = 52000;
						}

					}
					else
					{
						TestResultSet2(_SysInfo2.nMainWorkStep, "", "PASS");
						_SysInfo2.nMainWorkStep++;
						nProcessStep[nStepIndex] = 3000;
					}
					break;



				// PowerSupply 스텝
				//case 10000:
				//	if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID == 0 || _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID == 1)
				//	{
				//		nProcessStep[nStepIndex] = 10010;
				//	}
				//	else
				//	{
				//		AppendLogMsg2("파워서플라이 ID 설정 에러", MSG_TYPE.ERROR);
				//		nProcessStep[nStepIndex] = 90000;
				//	}
				//	break;


				//// 1번 채널 Power Supply 설정
				//case 10010:
				//	PowerSupply[_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID].SendData("*IDN?");
				//	tMainTimer[nStepIndex].Start(2000);
				//	nProcessStep[nStepIndex]++;
				//	break;

				//case 10011:
				//	if (tMainTimer[nStepIndex].Verify())
				//	{
				//		AppendLogMsg2("파워서플라이 초기화 실패", MSG_TYPE.ERROR);
				//		nProcessStep[nStepIndex] = 90000;
				//	}

				//	if (PowerSupply[_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID].IsReadData())
				//	{
				//		nProcessStep[nStepIndex] = 10020;
				//	}
				//	break;

				//// 전압설정
				//case 10020:
				//	double.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue1, out _SysInfo.dbCommSendData);

				//	PowerSupply[_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID].SendData($"VOLT {_SysInfo.dbCommSendData}");
				//	tMainTimer[nStepIndex].Start(200);
				//	nProcessStep[nStepIndex]++;
				//	break;

				//case 10021:
				//	if (!tMainTimer[nStepIndex].Verify()) { break; }

				//	nProcessStep[nStepIndex] = 10030;
				//	break;

				//case 10030:
				//	PowerSupply[_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID].SendData("VOLT?");
				//	tMainTimer[nStepIndex].Start(2000);
				//	nProcessStep[nStepIndex]++;
				//	break;

				//case 10031:
				//	if (tMainTimer[nStepIndex].Verify())
				//	{
				//		AppendLogMsg2("파워서플라이 Voltage Setting 실패", MSG_TYPE.ERROR);
				//		nProcessStep[nStepIndex] = 90000;
				//	}

				//	if (PowerSupply[_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID].IsReadData())
				//	{
				//		nProcessStep[nStepIndex] = 10040;
				//	}
				//	break;

				//case 10040:
				//	double.TryParse(PowerSupply[_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nID].GetReadData(), out _SysInfo.dbCommReadData);

				//	if (_SysInfo.dbCommReadData == _SysInfo.dbCommSendData)
				//	{
				//		nProcessStep[nStepIndex] = 10100;
				//	}
				//	break;



				//case 10100:
				//	_SysInfo.nSubWorkStep++;
				//	nProcessStep[nStepIndex] = 3000;
				//	break;




				// EOL MODE
				case 20000:
					if (_SysInfo2.nSubWorkStep >= _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo.Count)
					{
						if (_SysInfo2.bEolNg)
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, "", "NG");
							_SysInfo2.bTestNG = true;
						}
						else
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
						}
						nProcessStep[nStepIndex] = 29000;
					}
					else
					{
						nProcessStep[nStepIndex] = 21000;
					}
					break;


				case 21000:
					if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nTestType == 0)
					{
						// Modbus Can Write
						nProcessStep[nStepIndex] = 21100;
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nTestType == 1)
					{
						// Modbus Can Read(Comp)
						nProcessStep[nStepIndex] = 21200;
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nTestType == 2)
					{
						// Modbus Can Read(Buff)
						nProcessStep[nStepIndex] = 21300;
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nTestType == 3)
					{
						// Dealy
						nProcessStep[nStepIndex] = 21400;
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nTestType == 4)
					{
						// Modbus Can Write(Multi)
						nProcessStep[nStepIndex] = 21500;
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nTestType == 5)
					{
						// Can Write
						nProcessStep[nStepIndex] = 21600;
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nTestType == 6)
					{
						// Can Read
						nProcessStep[nStepIndex] = 21700;
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nTestType == 7)
					{
						// TCP Write
						nProcessStep[nStepIndex] = 21800;
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nTestType == 8)
					{
						// TCP Read ( Comp )
						nProcessStep[nStepIndex] = 21900;
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nTestType == 9)
					{
						// TCP Read ( Buff )
						nProcessStep[nStepIndex] = 22000;
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nTestType == 10)
					{
						// TCP Read ( Buff )
						nProcessStep[nStepIndex] = 22100;
					}
					break;



				// Modbus Can Write
				case 21100:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nID.ToString(), out _SysInfo2.nCanCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue1.ToString(), out _SysInfo2.nCanAddr);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue2.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nCanData);
					//tMainTimer[nStepIndex].Start(1000);
					SendWriteCommand(_SysInfo2.nCanCh, _SysInfo2.nCanAddr, _SysInfo2.nCanData, 2);
					nProcessStep[nStepIndex]++;
					break;

				case 21101:
					//if (!tMainTimer[nStepIndex].Verify()) { break; }
					//if (_CanComm[_SysInfo2.nCanCh].bReadMessage)
					//{
					//	if (_CanComm[_SysInfo2.nCanCh].btReadData[0] == 0x06)
					//	{
					//		//if ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3] == _SysInfo2.nCanData)
					//		//{

					//		//}
					//		_SysInfo2.nSubWorkStep++;
					//		nProcessStep[nStepIndex] = 20000;
					//	}
					//}
					_SysInfo2.nSubWorkStep++;
					nProcessStep[nStepIndex] = 20000;
					break;


				// Modbus Can Read(COMP)
				case 21200:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nID.ToString(), out _SysInfo2.nCanCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue1.ToString(), out _SysInfo2.nCanAddr);
					//int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue2.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nCanData);
					_SysInfo2.nCanData = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue2.ToString());
					SendReadCommand(_SysInfo2.nCanCh, _SysInfo2.nCanAddr, 2);

					tMainTimer[nStepIndex].Start(5000);
					nProcessStep[nStepIndex]++;
					break;

				case 21201:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), "TIME OUT", "NG");
						_SysInfo2.bEolNg = true;
						_SysInfo2.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}


					if (_CanComm[_SysInfo2.nCanCh].bReadMessage)
					{
						if (_CanComm[_SysInfo2.nCanCh].btReadData[0] == 0x03)
						{
							if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue3 != null && _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue3 != "")
							{
								if (int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue3.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMaskingData))
								{
									_SysInfo2.nCompData = ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3]) & _SysInfo2.nMaskingData;

									if (_SysInfo2.nCompData == _SysInfo2.nCanData)
									{
										NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3]).ToString("X4") + $"({(_SysInfo2.nMaskingData.ToString("X4"))})", "OK");
										//AppendLogMsg2($"OK / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
									}
									else
									{
										_SysInfo2.bEolNg = true;
										NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3]).ToString("X4") + $"({(_SysInfo2.nMaskingData.ToString("X4"))})", "NG");
										//AppendLogMsg2($"NG / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
									}
								}
								else
								{
									_SysInfo2.nCompData = ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3]);
									if (_SysInfo2.nCompData == _SysInfo2.nCanData)
									{
										NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), _SysInfo2.nCompData.ToString("X4"), "OK");
										//AppendLogMsg2($"OK / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
									}
									else
									{
										_SysInfo2.bEolNg = true;
										NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), _SysInfo2.nCompData.ToString("X4"), "NG");
										//AppendLogMsg2($"NG / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
									}
								}
							}
							else
							{
								_SysInfo2.nCompData = ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3]);
								if (_SysInfo2.nCompData == _SysInfo2.nCanData)
								{
									NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), _SysInfo2.nCompData.ToString("X4"), "OK");
									//AppendLogMsg2($"OK / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
								}
								else
								{
									_SysInfo2.bEolNg = true;
									NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), _SysInfo2.nCompData.ToString("X4"), "NG");
									//AppendLogMsg2($"NG / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
								}
							}


						}
						else
						{
							_SysInfo2.bEolNg = true;
							NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), "DATA ERROR", "NG");
						}
						_SysInfo2.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;

				case 21300:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nID.ToString(), out _SysInfo2.nCanCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue1.ToString(), out _SysInfo2.nCanAddr);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue2.ToString(), out _SysInfo2.nBuffIndex);
					tMainTimer[nStepIndex].Start(5000);
					SendReadCommand(_SysInfo2.nCanCh, _SysInfo2.nCanAddr, 2);
					nProcessStep[nStepIndex]++;
					break;

				case 21301:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), "", "TIME OUT", "NG");
						_SysInfo2.bEolNg = true;
						_SysInfo2.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}

					if (_CanComm[_SysInfo2.nCanCh].bReadMessage)
					{
						if (_CanComm[_SysInfo2.nCanCh].btReadData[0] == 0x03)
						{
							_SysInfo2.nReadDataBuff[_SysInfo2.nBuffIndex] = (_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3];
							NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), $"BUFFER{_SysInfo2.nBuffIndex}", ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3]).ToString("X4"), "");
						}
						_SysInfo2.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;


				case 21400:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue1.ToString(), out _SysInfo2.nCanAddr);
					tMainTimer[nStepIndex].Start(_SysInfo2.nCanAddr);
					nProcessStep[nStepIndex]++;
					break;

				case 21401:
					if (!tMainTimer[nStepIndex].Verify()) { break; }

					_SysInfo2.nSubWorkStep++;
					nProcessStep[nStepIndex] = 20000;
					break;


				case 21500:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nID.ToString(), out _SysInfo2.nCanCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue1.ToString(), out _SysInfo2.nCanAddr);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue2.ToString(), out _SysInfo2.nCanMultiCount);
					if (_SysInfo2.nCanMultiCount > 0) { _SysInfo2.nMultiSendData[0] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue3.ToString()); }
					if (_SysInfo2.nCanMultiCount > 1) { _SysInfo2.nMultiSendData[1] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue4.ToString()); }
					if (_SysInfo2.nCanMultiCount > 2) { _SysInfo2.nMultiSendData[2] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue5.ToString()); }
					if (_SysInfo2.nCanMultiCount > 3) { _SysInfo2.nMultiSendData[3] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue6.ToString()); }
					if (_SysInfo2.nCanMultiCount > 4) { _SysInfo2.nMultiSendData[4] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue7.ToString()); }
					if (_SysInfo2.nCanMultiCount > 5) { _SysInfo2.nMultiSendData[5] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue8.ToString()); }
					if (_SysInfo2.nCanMultiCount > 6) { _SysInfo2.nMultiSendData[6] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue9.ToString()); }
					if (_SysInfo2.nCanMultiCount > 7) { _SysInfo2.nMultiSendData[7] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue10.ToString()); }
					if (_SysInfo2.nCanMultiCount > 8) { _SysInfo2.nMultiSendData[8] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue11.ToString()); }
					if (_SysInfo2.nCanMultiCount > 9) { _SysInfo2.nMultiSendData[9] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue12.ToString()); }
					if (_SysInfo2.nCanMultiCount > 10) { _SysInfo2.nMultiSendData[10] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue13.ToString()); }
					if (_SysInfo2.nCanMultiCount > 11) { _SysInfo2.nMultiSendData[11] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue14.ToString()); }
					if (_SysInfo2.nCanMultiCount > 12) { _SysInfo2.nMultiSendData[12] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue15.ToString()); }
					if (_SysInfo2.nCanMultiCount > 13) { _SysInfo2.nMultiSendData[13] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue16.ToString()); }
					if (_SysInfo2.nCanMultiCount > 14) { _SysInfo2.nMultiSendData[14] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue17.ToString()); }
					if (_SysInfo2.nCanMultiCount > 15) { _SysInfo2.nMultiSendData[15] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue18.ToString()); }
					if (_SysInfo2.nCanMultiCount > 16) { _SysInfo2.nMultiSendData[16] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue19.ToString()); }
					if (_SysInfo2.nCanMultiCount > 17) { _SysInfo2.nMultiSendData[17] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue20.ToString()); }
					if (_SysInfo2.nCanMultiCount > 18) { _SysInfo2.nMultiSendData[18] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue21.ToString()); }
					if (_SysInfo2.nCanMultiCount > 19) { _SysInfo2.nMultiSendData[19] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue22.ToString()); }
					if (_SysInfo2.nCanMultiCount > 20) { _SysInfo2.nMultiSendData[20] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue23.ToString()); }
					if (_SysInfo2.nCanMultiCount > 21) { _SysInfo2.nMultiSendData[21] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue24.ToString()); }
					if (_SysInfo2.nCanMultiCount > 22) { _SysInfo2.nMultiSendData[22] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue25.ToString()); }
					if (_SysInfo2.nCanMultiCount > 23) { _SysInfo2.nMultiSendData[23] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue26.ToString()); }
					if (_SysInfo2.nCanMultiCount > 24) { _SysInfo2.nMultiSendData[24] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue27.ToString()); }
					if (_SysInfo2.nCanMultiCount > 25) { _SysInfo2.nMultiSendData[25] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue28.ToString()); }
					if (_SysInfo2.nCanMultiCount > 26) { _SysInfo2.nMultiSendData[26] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue29.ToString()); }
					if (_SysInfo2.nCanMultiCount > 27) { _SysInfo2.nMultiSendData[27] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue30.ToString()); }
					if (_SysInfo2.nCanMultiCount > 28) { _SysInfo2.nMultiSendData[28] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue31.ToString()); }
					if (_SysInfo2.nCanMultiCount > 29) { _SysInfo2.nMultiSendData[29] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue32.ToString()); }
					if (_SysInfo2.nCanMultiCount > 30) { _SysInfo2.nMultiSendData[30] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue33.ToString()); }
					if (_SysInfo2.nCanMultiCount > 31) { _SysInfo2.nMultiSendData[31] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue34.ToString()); }
					if (_SysInfo2.nCanMultiCount > 32) { _SysInfo2.nMultiSendData[32] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue35.ToString()); }
					if (_SysInfo2.nCanMultiCount > 33) { _SysInfo2.nMultiSendData[33] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue36.ToString()); }
					if (_SysInfo2.nCanMultiCount > 34) { _SysInfo2.nMultiSendData[34] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue37.ToString()); }
					if (_SysInfo2.nCanMultiCount > 35) { _SysInfo2.nMultiSendData[35] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue38.ToString()); }
					if (_SysInfo2.nCanMultiCount > 36) { _SysInfo2.nMultiSendData[36] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue39.ToString()); }
					if (_SysInfo2.nCanMultiCount > 37) { _SysInfo2.nMultiSendData[37] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue40.ToString()); }
					if (_SysInfo2.nCanMultiCount > 38) { _SysInfo2.nMultiSendData[38] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue41.ToString()); }
					if (_SysInfo2.nCanMultiCount > 39) { _SysInfo2.nMultiSendData[39] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue42.ToString()); }
					if (_SysInfo2.nCanMultiCount > 40) { _SysInfo2.nMultiSendData[40] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue43.ToString()); }
					if (_SysInfo2.nCanMultiCount > 41) { _SysInfo2.nMultiSendData[41] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue44.ToString()); }
					if (_SysInfo2.nCanMultiCount > 42) { _SysInfo2.nMultiSendData[42] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue45.ToString()); }
					if (_SysInfo2.nCanMultiCount > 43) { _SysInfo2.nMultiSendData[43] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue46.ToString()); }
					if (_SysInfo2.nCanMultiCount > 44) { _SysInfo2.nMultiSendData[44] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue47.ToString()); }
					if (_SysInfo2.nCanMultiCount > 45) { _SysInfo2.nMultiSendData[45] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue48.ToString()); }
					if (_SysInfo2.nCanMultiCount > 46) { _SysInfo2.nMultiSendData[46] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue49.ToString()); }
					if (_SysInfo2.nCanMultiCount > 47) { _SysInfo2.nMultiSendData[47] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue50.ToString()); }
					if (_SysInfo2.nCanMultiCount > 48) { _SysInfo2.nMultiSendData[48] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue51.ToString()); }
					if (_SysInfo2.nCanMultiCount > 49) { _SysInfo2.nMultiSendData[49] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue52.ToString()); }
					if (_SysInfo2.nCanMultiCount > 50) { _SysInfo2.nMultiSendData[50] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue53.ToString()); }
					if (_SysInfo2.nCanMultiCount > 51) { _SysInfo2.nMultiSendData[51] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue54.ToString()); }
					if (_SysInfo2.nCanMultiCount > 52) { _SysInfo2.nMultiSendData[52] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue55.ToString()); }
					if (_SysInfo2.nCanMultiCount > 53) { _SysInfo2.nMultiSendData[53] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue56.ToString()); }
					if (_SysInfo2.nCanMultiCount > 54) { _SysInfo2.nMultiSendData[54] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue57.ToString()); }
					if (_SysInfo2.nCanMultiCount > 55) { _SysInfo2.nMultiSendData[55] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue58.ToString()); }
					if (_SysInfo2.nCanMultiCount > 56) { _SysInfo2.nMultiSendData[56] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue59.ToString()); }
					if (_SysInfo2.nCanMultiCount > 57) { _SysInfo2.nMultiSendData[57] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue60.ToString()); }
					if (_SysInfo2.nCanMultiCount > 58) { _SysInfo2.nMultiSendData[58] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue61.ToString()); }
					if (_SysInfo2.nCanMultiCount > 59) { _SysInfo2.nMultiSendData[59] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue62.ToString()); }
					if (_SysInfo2.nCanMultiCount > 60) { _SysInfo2.nMultiSendData[60] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue63.ToString()); }
					if (_SysInfo2.nCanMultiCount > 61) { _SysInfo2.nMultiSendData[61] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue64.ToString()); }
					if (_SysInfo2.nCanMultiCount > 62) { _SysInfo2.nMultiSendData[62] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue65.ToString()); }
					if (_SysInfo2.nCanMultiCount > 63) { _SysInfo2.nMultiSendData[63] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue66.ToString()); }
					if (_SysInfo2.nCanMultiCount > 64) { _SysInfo2.nMultiSendData[64] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue67.ToString()); }
					if (_SysInfo2.nCanMultiCount > 65) { _SysInfo2.nMultiSendData[65] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue68.ToString()); }
					if (_SysInfo2.nCanMultiCount > 66) { _SysInfo2.nMultiSendData[66] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue69.ToString()); }
					if (_SysInfo2.nCanMultiCount > 67) { _SysInfo2.nMultiSendData[67] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue70.ToString()); }
					if (_SysInfo2.nCanMultiCount > 68) { _SysInfo2.nMultiSendData[68] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue71.ToString()); }
					if (_SysInfo2.nCanMultiCount > 69) { _SysInfo2.nMultiSendData[69] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue72.ToString()); }
					if (_SysInfo2.nCanMultiCount > 70) { _SysInfo2.nMultiSendData[70] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue73.ToString()); }
					if (_SysInfo2.nCanMultiCount > 71) { _SysInfo2.nMultiSendData[71] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue74.ToString()); }
					if (_SysInfo2.nCanMultiCount > 72) { _SysInfo2.nMultiSendData[72] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue75.ToString()); }
					if (_SysInfo2.nCanMultiCount > 73) { _SysInfo2.nMultiSendData[73] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue76.ToString()); }
					if (_SysInfo2.nCanMultiCount > 74) { _SysInfo2.nMultiSendData[74] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue77.ToString()); }
					if (_SysInfo2.nCanMultiCount > 75) { _SysInfo2.nMultiSendData[75] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue78.ToString()); }
					if (_SysInfo2.nCanMultiCount > 76) { _SysInfo2.nMultiSendData[76] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue79.ToString()); }
					if (_SysInfo2.nCanMultiCount > 77) { _SysInfo2.nMultiSendData[77] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue80.ToString()); }
					if (_SysInfo2.nCanMultiCount > 78) { _SysInfo2.nMultiSendData[78] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue81.ToString()); }
					if (_SysInfo2.nCanMultiCount > 79) { _SysInfo2.nMultiSendData[79] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue82.ToString()); }
					if (_SysInfo2.nCanMultiCount > 80) { _SysInfo2.nMultiSendData[80] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue83.ToString()); }
					if (_SysInfo2.nCanMultiCount > 81) { _SysInfo2.nMultiSendData[81] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue84.ToString()); }
					if (_SysInfo2.nCanMultiCount > 82) { _SysInfo2.nMultiSendData[82] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue85.ToString()); }
					if (_SysInfo2.nCanMultiCount > 83) { _SysInfo2.nMultiSendData[83] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue86.ToString()); }
					if (_SysInfo2.nCanMultiCount > 84) { _SysInfo2.nMultiSendData[84] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue87.ToString()); }
					if (_SysInfo2.nCanMultiCount > 85) { _SysInfo2.nMultiSendData[85] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue88.ToString()); }
					if (_SysInfo2.nCanMultiCount > 86) { _SysInfo2.nMultiSendData[86] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue89.ToString()); }
					if (_SysInfo2.nCanMultiCount > 87) { _SysInfo2.nMultiSendData[87] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue90.ToString()); }
					if (_SysInfo2.nCanMultiCount > 88) { _SysInfo2.nMultiSendData[88] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue91.ToString()); }
					if (_SysInfo2.nCanMultiCount > 89) { _SysInfo2.nMultiSendData[89] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue92.ToString()); }
					if (_SysInfo2.nCanMultiCount > 90) { _SysInfo2.nMultiSendData[90] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue93.ToString()); }
					if (_SysInfo2.nCanMultiCount > 91) { _SysInfo2.nMultiSendData[91] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue94.ToString()); }
					if (_SysInfo2.nCanMultiCount > 92) { _SysInfo2.nMultiSendData[92] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue95.ToString()); }
					if (_SysInfo2.nCanMultiCount > 93) { _SysInfo2.nMultiSendData[93] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue96.ToString()); }
					if (_SysInfo2.nCanMultiCount > 94) { _SysInfo2.nMultiSendData[94] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue97.ToString()); }
					if (_SysInfo2.nCanMultiCount > 95) { _SysInfo2.nMultiSendData[95] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue98.ToString()); }
					if (_SysInfo2.nCanMultiCount > 96) { _SysInfo2.nMultiSendData[96] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue99.ToString()); }
					if (_SysInfo2.nCanMultiCount > 97) { _SysInfo2.nMultiSendData[97] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue100.ToString()); }

					//if (_SysInfo2.nCanMultiCount > 0) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue3.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[0]); }
					//if (_SysInfo2.nCanMultiCount > 1) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue4.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[1]); }
					//if (_SysInfo2.nCanMultiCount > 2) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue5.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[2]); }
					//if (_SysInfo2.nCanMultiCount > 3) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue6.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[3]); }
					//if (_SysInfo2.nCanMultiCount > 4) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue7.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[4]); }
					//if (_SysInfo2.nCanMultiCount > 5) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue8.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[5]); }
					//if (_SysInfo2.nCanMultiCount > 6) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue9.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[6]); }
					//if (_SysInfo2.nCanMultiCount > 7) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue10.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[7]); }
					//if (_SysInfo2.nCanMultiCount > 8) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue11.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[8]); }
					//if (_SysInfo2.nCanMultiCount > 9) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue12.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[9]); }
					//if (_SysInfo2.nCanMultiCount > 10) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue13.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[10]); }
					//if (_SysInfo2.nCanMultiCount > 11) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue14.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[11]); }
					//if (_SysInfo2.nCanMultiCount > 12) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue15.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[12]); }
					//if (_SysInfo2.nCanMultiCount > 13) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue16.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[13]); }
					//if (_SysInfo2.nCanMultiCount > 14) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue17.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[14]); }
					//if (_SysInfo2.nCanMultiCount > 15) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue18.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[15]); }
					//if (_SysInfo2.nCanMultiCount > 16) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue19.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[16]); }
					//if (_SysInfo2.nCanMultiCount > 17) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue20.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[17]); }

					//tMainTimer[nStepIndex].Start(1000);
					SendWriteMultiCommand(_SysInfo2.nCanCh, _SysInfo2.nCanAddr, _SysInfo2.nMultiSendData, _SysInfo2.nCanMultiCount, 2);
					nProcessStep[nStepIndex]++;
					break;

				case 21501:
					//if (!tMainTimer[nStepIndex].Verify()) { break; }
					//if (_CanComm[_SysInfo2.nCanCh].bReadMessage)
					//{
					//	if (_CanComm[_SysInfo2.nCanCh].btReadData[0] == 0x06)
					//	{
					//		//if ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3] == _SysInfo2.nCanData)
					//		//{

					//		//}
					//		_SysInfo2.nSubWorkStep++;
					//		nProcessStep[nStepIndex] = 20000;
					//	}
					//}
					_SysInfo2.nSubWorkStep++;
					nProcessStep[nStepIndex] = 20000;
					break;


				// Can Write
				case 21600:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nID.ToString(), out _SysInfo2.nCanCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue1, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nCanStartAddr);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue2.ToString(), out _SysInfo2.nCanMultiCount);
					if (_SysInfo2.nCanMultiCount > 0) { _SysInfo2.nMultiSendData[0] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue3.ToString()); }
					if (_SysInfo2.nCanMultiCount > 1) { _SysInfo2.nMultiSendData[1] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue4.ToString()); }
					if (_SysInfo2.nCanMultiCount > 2) { _SysInfo2.nMultiSendData[2] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue5.ToString()); }
					if (_SysInfo2.nCanMultiCount > 3) { _SysInfo2.nMultiSendData[3] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue6.ToString()); }
					if (_SysInfo2.nCanMultiCount > 4) { _SysInfo2.nMultiSendData[4] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue7.ToString()); }
					if (_SysInfo2.nCanMultiCount > 5) { _SysInfo2.nMultiSendData[5] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue8.ToString()); }
					if (_SysInfo2.nCanMultiCount > 6) { _SysInfo2.nMultiSendData[6] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue9.ToString()); }
					if (_SysInfo2.nCanMultiCount > 7) { _SysInfo2.nMultiSendData[7] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue10.ToString()); }
					if (_SysInfo2.nCanMultiCount > 8) { _SysInfo2.nMultiSendData[8] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue11.ToString()); }
					if (_SysInfo2.nCanMultiCount > 9) { _SysInfo2.nMultiSendData[9] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue12.ToString()); }
					if (_SysInfo2.nCanMultiCount > 10) { _SysInfo2.nMultiSendData[10] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue13.ToString()); }
					if (_SysInfo2.nCanMultiCount > 11) { _SysInfo2.nMultiSendData[11] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue14.ToString()); }
					if (_SysInfo2.nCanMultiCount > 12) { _SysInfo2.nMultiSendData[12] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue15.ToString()); }
					if (_SysInfo2.nCanMultiCount > 13) { _SysInfo2.nMultiSendData[13] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue16.ToString()); }
					if (_SysInfo2.nCanMultiCount > 14) { _SysInfo2.nMultiSendData[14] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue17.ToString()); }
					if (_SysInfo2.nCanMultiCount > 15) { _SysInfo2.nMultiSendData[15] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue18.ToString()); }
					if (_SysInfo2.nCanMultiCount > 16) { _SysInfo2.nMultiSendData[16] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue19.ToString()); }
					if (_SysInfo2.nCanMultiCount > 17) { _SysInfo2.nMultiSendData[17] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue20.ToString()); }
					if (_SysInfo2.nCanMultiCount > 18) { _SysInfo2.nMultiSendData[18] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue21.ToString()); }
					if (_SysInfo2.nCanMultiCount > 19) { _SysInfo2.nMultiSendData[19] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue22.ToString()); }
					if (_SysInfo2.nCanMultiCount > 20) { _SysInfo2.nMultiSendData[20] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue23.ToString()); }
					if (_SysInfo2.nCanMultiCount > 21) { _SysInfo2.nMultiSendData[21] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue24.ToString()); }
					if (_SysInfo2.nCanMultiCount > 22) { _SysInfo2.nMultiSendData[22] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue25.ToString()); }
					if (_SysInfo2.nCanMultiCount > 23) { _SysInfo2.nMultiSendData[23] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue26.ToString()); }
					if (_SysInfo2.nCanMultiCount > 24) { _SysInfo2.nMultiSendData[24] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue27.ToString()); }
					if (_SysInfo2.nCanMultiCount > 25) { _SysInfo2.nMultiSendData[25] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue28.ToString()); }
					if (_SysInfo2.nCanMultiCount > 26) { _SysInfo2.nMultiSendData[26] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue29.ToString()); }
					if (_SysInfo2.nCanMultiCount > 27) { _SysInfo2.nMultiSendData[27] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue30.ToString()); }
					if (_SysInfo2.nCanMultiCount > 28) { _SysInfo2.nMultiSendData[28] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue31.ToString()); }
					if (_SysInfo2.nCanMultiCount > 29) { _SysInfo2.nMultiSendData[29] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue32.ToString()); }
					if (_SysInfo2.nCanMultiCount > 30) { _SysInfo2.nMultiSendData[30] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue33.ToString()); }
					if (_SysInfo2.nCanMultiCount > 31) { _SysInfo2.nMultiSendData[31] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue34.ToString()); }
					if (_SysInfo2.nCanMultiCount > 32) { _SysInfo2.nMultiSendData[32] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue35.ToString()); }
					if (_SysInfo2.nCanMultiCount > 33) { _SysInfo2.nMultiSendData[33] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue36.ToString()); }
					if (_SysInfo2.nCanMultiCount > 34) { _SysInfo2.nMultiSendData[34] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue37.ToString()); }
					if (_SysInfo2.nCanMultiCount > 35) { _SysInfo2.nMultiSendData[35] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue38.ToString()); }
					if (_SysInfo2.nCanMultiCount > 36) { _SysInfo2.nMultiSendData[36] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue39.ToString()); }
					if (_SysInfo2.nCanMultiCount > 37) { _SysInfo2.nMultiSendData[37] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue40.ToString()); }
					if (_SysInfo2.nCanMultiCount > 38) { _SysInfo2.nMultiSendData[38] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue41.ToString()); }
					if (_SysInfo2.nCanMultiCount > 39) { _SysInfo2.nMultiSendData[39] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue42.ToString()); }
					if (_SysInfo2.nCanMultiCount > 40) { _SysInfo2.nMultiSendData[40] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue43.ToString()); }
					if (_SysInfo2.nCanMultiCount > 41) { _SysInfo2.nMultiSendData[41] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue44.ToString()); }
					if (_SysInfo2.nCanMultiCount > 42) { _SysInfo2.nMultiSendData[42] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue45.ToString()); }
					if (_SysInfo2.nCanMultiCount > 43) { _SysInfo2.nMultiSendData[43] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue46.ToString()); }
					if (_SysInfo2.nCanMultiCount > 44) { _SysInfo2.nMultiSendData[44] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue47.ToString()); }
					if (_SysInfo2.nCanMultiCount > 45) { _SysInfo2.nMultiSendData[45] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue48.ToString()); }
					if (_SysInfo2.nCanMultiCount > 46) { _SysInfo2.nMultiSendData[46] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue49.ToString()); }
					if (_SysInfo2.nCanMultiCount > 47) { _SysInfo2.nMultiSendData[47] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue50.ToString()); }
					if (_SysInfo2.nCanMultiCount > 48) { _SysInfo2.nMultiSendData[48] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue51.ToString()); }
					if (_SysInfo2.nCanMultiCount > 49) { _SysInfo2.nMultiSendData[49] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue52.ToString()); }
					if (_SysInfo2.nCanMultiCount > 50) { _SysInfo2.nMultiSendData[50] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue53.ToString()); }
					if (_SysInfo2.nCanMultiCount > 51) { _SysInfo2.nMultiSendData[51] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue54.ToString()); }
					if (_SysInfo2.nCanMultiCount > 52) { _SysInfo2.nMultiSendData[52] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue55.ToString()); }
					if (_SysInfo2.nCanMultiCount > 53) { _SysInfo2.nMultiSendData[53] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue56.ToString()); }
					if (_SysInfo2.nCanMultiCount > 54) { _SysInfo2.nMultiSendData[54] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue57.ToString()); }
					if (_SysInfo2.nCanMultiCount > 55) { _SysInfo2.nMultiSendData[55] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue58.ToString()); }
					if (_SysInfo2.nCanMultiCount > 56) { _SysInfo2.nMultiSendData[56] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue59.ToString()); }
					if (_SysInfo2.nCanMultiCount > 57) { _SysInfo2.nMultiSendData[57] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue60.ToString()); }
					if (_SysInfo2.nCanMultiCount > 58) { _SysInfo2.nMultiSendData[58] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue61.ToString()); }
					if (_SysInfo2.nCanMultiCount > 59) { _SysInfo2.nMultiSendData[59] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue62.ToString()); }
					if (_SysInfo2.nCanMultiCount > 60) { _SysInfo2.nMultiSendData[60] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue63.ToString()); }
					if (_SysInfo2.nCanMultiCount > 61) { _SysInfo2.nMultiSendData[61] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue64.ToString()); }
					if (_SysInfo2.nCanMultiCount > 62) { _SysInfo2.nMultiSendData[62] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue65.ToString()); }
					if (_SysInfo2.nCanMultiCount > 63) { _SysInfo2.nMultiSendData[63] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue66.ToString()); }
					if (_SysInfo2.nCanMultiCount > 64) { _SysInfo2.nMultiSendData[64] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue67.ToString()); }
					if (_SysInfo2.nCanMultiCount > 65) { _SysInfo2.nMultiSendData[65] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue68.ToString()); }
					if (_SysInfo2.nCanMultiCount > 66) { _SysInfo2.nMultiSendData[66] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue69.ToString()); }
					if (_SysInfo2.nCanMultiCount > 67) { _SysInfo2.nMultiSendData[67] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue70.ToString()); }
					if (_SysInfo2.nCanMultiCount > 68) { _SysInfo2.nMultiSendData[68] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue71.ToString()); }
					if (_SysInfo2.nCanMultiCount > 69) { _SysInfo2.nMultiSendData[69] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue72.ToString()); }
					if (_SysInfo2.nCanMultiCount > 70) { _SysInfo2.nMultiSendData[70] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue73.ToString()); }
					if (_SysInfo2.nCanMultiCount > 71) { _SysInfo2.nMultiSendData[71] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue74.ToString()); }
					if (_SysInfo2.nCanMultiCount > 72) { _SysInfo2.nMultiSendData[72] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue75.ToString()); }
					if (_SysInfo2.nCanMultiCount > 73) { _SysInfo2.nMultiSendData[73] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue76.ToString()); }
					if (_SysInfo2.nCanMultiCount > 74) { _SysInfo2.nMultiSendData[74] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue77.ToString()); }
					if (_SysInfo2.nCanMultiCount > 75) { _SysInfo2.nMultiSendData[75] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue78.ToString()); }
					if (_SysInfo2.nCanMultiCount > 76) { _SysInfo2.nMultiSendData[76] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue79.ToString()); }
					if (_SysInfo2.nCanMultiCount > 77) { _SysInfo2.nMultiSendData[77] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue80.ToString()); }
					if (_SysInfo2.nCanMultiCount > 78) { _SysInfo2.nMultiSendData[78] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue81.ToString()); }
					if (_SysInfo2.nCanMultiCount > 79) { _SysInfo2.nMultiSendData[79] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue82.ToString()); }
					if (_SysInfo2.nCanMultiCount > 80) { _SysInfo2.nMultiSendData[80] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue83.ToString()); }
					if (_SysInfo2.nCanMultiCount > 81) { _SysInfo2.nMultiSendData[81] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue84.ToString()); }
					if (_SysInfo2.nCanMultiCount > 82) { _SysInfo2.nMultiSendData[82] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue85.ToString()); }
					if (_SysInfo2.nCanMultiCount > 83) { _SysInfo2.nMultiSendData[83] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue86.ToString()); }
					if (_SysInfo2.nCanMultiCount > 84) { _SysInfo2.nMultiSendData[84] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue87.ToString()); }
					if (_SysInfo2.nCanMultiCount > 85) { _SysInfo2.nMultiSendData[85] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue88.ToString()); }
					if (_SysInfo2.nCanMultiCount > 86) { _SysInfo2.nMultiSendData[86] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue89.ToString()); }
					if (_SysInfo2.nCanMultiCount > 87) { _SysInfo2.nMultiSendData[87] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue90.ToString()); }
					if (_SysInfo2.nCanMultiCount > 88) { _SysInfo2.nMultiSendData[88] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue91.ToString()); }
					if (_SysInfo2.nCanMultiCount > 89) { _SysInfo2.nMultiSendData[89] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue92.ToString()); }
					if (_SysInfo2.nCanMultiCount > 90) { _SysInfo2.nMultiSendData[90] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue93.ToString()); }
					if (_SysInfo2.nCanMultiCount > 91) { _SysInfo2.nMultiSendData[91] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue94.ToString()); }
					if (_SysInfo2.nCanMultiCount > 92) { _SysInfo2.nMultiSendData[92] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue95.ToString()); }
					if (_SysInfo2.nCanMultiCount > 93) { _SysInfo2.nMultiSendData[93] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue96.ToString()); }
					if (_SysInfo2.nCanMultiCount > 94) { _SysInfo2.nMultiSendData[94] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue97.ToString()); }
					if (_SysInfo2.nCanMultiCount > 95) { _SysInfo2.nMultiSendData[95] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue98.ToString()); }
					if (_SysInfo2.nCanMultiCount > 96) { _SysInfo2.nMultiSendData[96] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue99.ToString()); }
					if (_SysInfo2.nCanMultiCount > 97) { _SysInfo2.nMultiSendData[97] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue100.ToString()); }

					SendCanData(_SysInfo2.nCanCh, _SysInfo2.nCanStartAddr, _SysInfo2.nMultiSendData, _SysInfo2.nCanMultiCount, 2);
					_SysInfo2.nSubWorkStep++;
					nProcessStep[nStepIndex] = 20000;
					break;

				case 21700:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nID.ToString(), out _SysInfo2.nCanCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue1, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nCanStartAddr);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue2.ToString(), out _SysInfo2.nCanMultiCount);
					if (_SysInfo2.nCanMultiCount > 0) { _SysInfo2.strValueBuff[0] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue3; }
					if (_SysInfo2.nCanMultiCount > 1) { _SysInfo2.strValueBuff[1] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue4; }
					if (_SysInfo2.nCanMultiCount > 2) { _SysInfo2.strValueBuff[2] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue5; }
					if (_SysInfo2.nCanMultiCount > 3) { _SysInfo2.strValueBuff[3] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue6; }
					if (_SysInfo2.nCanMultiCount > 4) { _SysInfo2.strValueBuff[4] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue7; }
					if (_SysInfo2.nCanMultiCount > 5) { _SysInfo2.strValueBuff[5] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue8; }
					if (_SysInfo2.nCanMultiCount > 6) { _SysInfo2.strValueBuff[6] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue9; }
					if (_SysInfo2.nCanMultiCount > 7) { _SysInfo2.strValueBuff[7] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue10; }
					if (_SysInfo2.nCanMultiCount > 8) { _SysInfo2.strValueBuff[8] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue11; }
					if (_SysInfo2.nCanMultiCount > 9) { _SysInfo2.strValueBuff[9] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue12; }
					if (_SysInfo2.nCanMultiCount > 10) { _SysInfo2.strValueBuff[10] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue13; }
					if (_SysInfo2.nCanMultiCount > 11) { _SysInfo2.strValueBuff[11] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue14; }
					if (_SysInfo2.nCanMultiCount > 12) { _SysInfo2.strValueBuff[12] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue15; }
					if (_SysInfo2.nCanMultiCount > 13) { _SysInfo2.strValueBuff[13] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue16; }
					if (_SysInfo2.nCanMultiCount > 14) { _SysInfo2.strValueBuff[14] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue17; }
					if (_SysInfo2.nCanMultiCount > 15) { _SysInfo2.strValueBuff[15] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue18; }
					if (_SysInfo2.nCanMultiCount > 16) { _SysInfo2.strValueBuff[16] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue19; }
					if (_SysInfo2.nCanMultiCount > 17) { _SysInfo2.strValueBuff[17] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue20; }
					if (_SysInfo2.nCanMultiCount > 18) { _SysInfo2.strValueBuff[18] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue21; }
					if (_SysInfo2.nCanMultiCount > 19) { _SysInfo2.strValueBuff[19] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue22; }
					if (_SysInfo2.nCanMultiCount > 20) { _SysInfo2.strValueBuff[20] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue23; }
					if (_SysInfo2.nCanMultiCount > 21) { _SysInfo2.strValueBuff[21] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue24; }
					if (_SysInfo2.nCanMultiCount > 22) { _SysInfo2.strValueBuff[22] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue25; }
					if (_SysInfo2.nCanMultiCount > 23) { _SysInfo2.strValueBuff[23] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue26; }
					if (_SysInfo2.nCanMultiCount > 24) { _SysInfo2.strValueBuff[24] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue27; }
					if (_SysInfo2.nCanMultiCount > 25) { _SysInfo2.strValueBuff[25] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue28; }
					if (_SysInfo2.nCanMultiCount > 26) { _SysInfo2.strValueBuff[26] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue29; }
					if (_SysInfo2.nCanMultiCount > 27) { _SysInfo2.strValueBuff[27] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue30; }
					if (_SysInfo2.nCanMultiCount > 28) { _SysInfo2.strValueBuff[28] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue31; }
					if (_SysInfo2.nCanMultiCount > 29) { _SysInfo2.strValueBuff[29] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue32; }
					if (_SysInfo2.nCanMultiCount > 30) { _SysInfo2.strValueBuff[30] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue33; }
					if (_SysInfo2.nCanMultiCount > 31) { _SysInfo2.strValueBuff[31] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue34; }
					if (_SysInfo2.nCanMultiCount > 32) { _SysInfo2.strValueBuff[32] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue35; }
					if (_SysInfo2.nCanMultiCount > 33) { _SysInfo2.strValueBuff[33] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue36; }
					if (_SysInfo2.nCanMultiCount > 34) { _SysInfo2.strValueBuff[34] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue37; }
					if (_SysInfo2.nCanMultiCount > 35) { _SysInfo2.strValueBuff[35] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue38; }
					if (_SysInfo2.nCanMultiCount > 36) { _SysInfo2.strValueBuff[36] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue39; }
					if (_SysInfo2.nCanMultiCount > 37) { _SysInfo2.strValueBuff[37] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue40; }
					if (_SysInfo2.nCanMultiCount > 38) { _SysInfo2.strValueBuff[38] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue41; }
					if (_SysInfo2.nCanMultiCount > 39) { _SysInfo2.strValueBuff[39] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue42; }
					if (_SysInfo2.nCanMultiCount > 40) { _SysInfo2.strValueBuff[40] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue43; }
					if (_SysInfo2.nCanMultiCount > 41) { _SysInfo2.strValueBuff[41] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue44; }
					if (_SysInfo2.nCanMultiCount > 42) { _SysInfo2.strValueBuff[42] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue45; }
					if (_SysInfo2.nCanMultiCount > 43) { _SysInfo2.strValueBuff[43] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue46; }
					if (_SysInfo2.nCanMultiCount > 44) { _SysInfo2.strValueBuff[44] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue47; }
					if (_SysInfo2.nCanMultiCount > 45) { _SysInfo2.strValueBuff[45] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue48; }
					if (_SysInfo2.nCanMultiCount > 46) { _SysInfo2.strValueBuff[46] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue49; }
					if (_SysInfo2.nCanMultiCount > 47) { _SysInfo2.strValueBuff[47] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue50; }
					if (_SysInfo2.nCanMultiCount > 48) { _SysInfo2.strValueBuff[48] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue51; }
					if (_SysInfo2.nCanMultiCount > 49) { _SysInfo2.strValueBuff[49] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue52; }
					if (_SysInfo2.nCanMultiCount > 50) { _SysInfo2.strValueBuff[50] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue53; }
					if (_SysInfo2.nCanMultiCount > 51) { _SysInfo2.strValueBuff[51] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue54; }
					if (_SysInfo2.nCanMultiCount > 52) { _SysInfo2.strValueBuff[52] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue55; }
					if (_SysInfo2.nCanMultiCount > 53) { _SysInfo2.strValueBuff[53] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue56; }
					if (_SysInfo2.nCanMultiCount > 54) { _SysInfo2.strValueBuff[54] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue57; }
					if (_SysInfo2.nCanMultiCount > 55) { _SysInfo2.strValueBuff[55] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue58; }
					if (_SysInfo2.nCanMultiCount > 56) { _SysInfo2.strValueBuff[56] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue59; }
					if (_SysInfo2.nCanMultiCount > 57) { _SysInfo2.strValueBuff[57] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue60; }
					if (_SysInfo2.nCanMultiCount > 58) { _SysInfo2.strValueBuff[58] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue61; }
					if (_SysInfo2.nCanMultiCount > 59) { _SysInfo2.strValueBuff[59] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue62; }
					if (_SysInfo2.nCanMultiCount > 60) { _SysInfo2.strValueBuff[60] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue63; }
					if (_SysInfo2.nCanMultiCount > 61) { _SysInfo2.strValueBuff[61] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue64; }
					if (_SysInfo2.nCanMultiCount > 62) { _SysInfo2.strValueBuff[62] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue65; }
					if (_SysInfo2.nCanMultiCount > 63) { _SysInfo2.strValueBuff[63] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue66; }
					if (_SysInfo2.nCanMultiCount > 64) { _SysInfo2.strValueBuff[64] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue67; }
					if (_SysInfo2.nCanMultiCount > 65) { _SysInfo2.strValueBuff[65] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue68; }
					if (_SysInfo2.nCanMultiCount > 66) { _SysInfo2.strValueBuff[66] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue69; }
					if (_SysInfo2.nCanMultiCount > 67) { _SysInfo2.strValueBuff[67] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue70; }
					if (_SysInfo2.nCanMultiCount > 68) { _SysInfo2.strValueBuff[68] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue71; }
					if (_SysInfo2.nCanMultiCount > 69) { _SysInfo2.strValueBuff[69] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue72; }
					if (_SysInfo2.nCanMultiCount > 70) { _SysInfo2.strValueBuff[70] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue73; }
					if (_SysInfo2.nCanMultiCount > 71) { _SysInfo2.strValueBuff[71] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue74; }
					if (_SysInfo2.nCanMultiCount > 72) { _SysInfo2.strValueBuff[72] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue75; }
					if (_SysInfo2.nCanMultiCount > 73) { _SysInfo2.strValueBuff[73] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue76; }
					if (_SysInfo2.nCanMultiCount > 74) { _SysInfo2.strValueBuff[74] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue77; }
					if (_SysInfo2.nCanMultiCount > 75) { _SysInfo2.strValueBuff[75] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue78; }
					if (_SysInfo2.nCanMultiCount > 76) { _SysInfo2.strValueBuff[76] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue79; }
					if (_SysInfo2.nCanMultiCount > 77) { _SysInfo2.strValueBuff[77] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue80; }
					if (_SysInfo2.nCanMultiCount > 78) { _SysInfo2.strValueBuff[78] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue81; }
					if (_SysInfo2.nCanMultiCount > 79) { _SysInfo2.strValueBuff[79] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue82; }
					if (_SysInfo2.nCanMultiCount > 80) { _SysInfo2.strValueBuff[80] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue83; }
					if (_SysInfo2.nCanMultiCount > 81) { _SysInfo2.strValueBuff[81] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue84; }
					if (_SysInfo2.nCanMultiCount > 82) { _SysInfo2.strValueBuff[82] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue85; }
					if (_SysInfo2.nCanMultiCount > 83) { _SysInfo2.strValueBuff[83] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue86; }
					if (_SysInfo2.nCanMultiCount > 84) { _SysInfo2.strValueBuff[84] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue87; }
					if (_SysInfo2.nCanMultiCount > 85) { _SysInfo2.strValueBuff[85] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue88; }
					if (_SysInfo2.nCanMultiCount > 86) { _SysInfo2.strValueBuff[86] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue89; }
					if (_SysInfo2.nCanMultiCount > 87) { _SysInfo2.strValueBuff[87] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue90; }
					if (_SysInfo2.nCanMultiCount > 88) { _SysInfo2.strValueBuff[88] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue91; }
					if (_SysInfo2.nCanMultiCount > 89) { _SysInfo2.strValueBuff[89] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue92; }
					if (_SysInfo2.nCanMultiCount > 90) { _SysInfo2.strValueBuff[90] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue93; }
					if (_SysInfo2.nCanMultiCount > 91) { _SysInfo2.strValueBuff[91] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue94; }
					if (_SysInfo2.nCanMultiCount > 92) { _SysInfo2.strValueBuff[92] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue95; }
					if (_SysInfo2.nCanMultiCount > 93) { _SysInfo2.strValueBuff[93] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue96; }
					if (_SysInfo2.nCanMultiCount > 94) { _SysInfo2.strValueBuff[94] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue97; }
					if (_SysInfo2.nCanMultiCount > 95) { _SysInfo2.strValueBuff[95] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue98; }
					if (_SysInfo2.nCanMultiCount > 96) { _SysInfo2.strValueBuff[96] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue99; }
					if (_SysInfo2.nCanMultiCount > 97) { _SysInfo2.strValueBuff[97] = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue100; }

					tMainTimer[nStepIndex].Start(5000);
					nProcessStep[nStepIndex]++;
					break;

				case 21701:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString("X"), "", "TIME OUT", "NG");
						_SysInfo2.bEolNg = true;
						_SysInfo2.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}

					if (_CanComm[_SysInfo2.nCanCh].bReadMessage && _CanComm[_SysInfo2.nCanCh].nReadid == _SysInfo2.nCanStartAddr && _CanComm[_SysInfo2.nCanCh].nReaddlc == _SysInfo2.nCanMultiCount)
					{
						bool bCompResult = true;
						string strSource = "";
						string strRead = "";

						for (int i = 0; i < _SysInfo2.nCanMultiCount; i++)
						{
							strSource += _SysInfo2.strValueBuff[i];
							strRead += _CanComm[_SysInfo2.nCanCh].btReadData[i].ToString("X2");

							if (!GetCompareCanData(_SysInfo2.strValueBuff[i], _CanComm[_SysInfo2.nCanCh].btReadData[i]))
							{
								bCompResult = false;
							}
						}

						if (bCompResult)
						{
							NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString("X"), strSource, strRead, "OK");
						}
						else
						{
							NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString("X"), strSource, strRead, "NG");
							_SysInfo2.bEolNg = true;
						}

						_SysInfo2.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;




				case 21800:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nID.ToString(), out _SysInfo2.nCanCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue1, out _SysInfo2.nCanAddr);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue2.ToString(), out _SysInfo2.nCanMultiCount);
					if (_SysInfo2.nCanMultiCount > 0) { _SysInfo2.nMultiSendData[0] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue3.ToString()); }
					if (_SysInfo2.nCanMultiCount > 1) { _SysInfo2.nMultiSendData[1] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue4.ToString()); }
					if (_SysInfo2.nCanMultiCount > 2) { _SysInfo2.nMultiSendData[2] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue5.ToString()); }
					if (_SysInfo2.nCanMultiCount > 3) { _SysInfo2.nMultiSendData[3] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue6.ToString()); }
					if (_SysInfo2.nCanMultiCount > 4) { _SysInfo2.nMultiSendData[4] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue7.ToString()); }
					if (_SysInfo2.nCanMultiCount > 5) { _SysInfo2.nMultiSendData[5] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue8.ToString()); }
					if (_SysInfo2.nCanMultiCount > 6) { _SysInfo2.nMultiSendData[6] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue9.ToString()); }
					if (_SysInfo2.nCanMultiCount > 7) { _SysInfo2.nMultiSendData[7] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue10.ToString()); }
					if (_SysInfo2.nCanMultiCount > 8) { _SysInfo2.nMultiSendData[8] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue11.ToString()); }
					if (_SysInfo2.nCanMultiCount > 9) { _SysInfo2.nMultiSendData[9] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue12.ToString()); }
					if (_SysInfo2.nCanMultiCount > 10) { _SysInfo2.nMultiSendData[10] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue13.ToString()); }
					if (_SysInfo2.nCanMultiCount > 11) { _SysInfo2.nMultiSendData[11] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue14.ToString()); }
					if (_SysInfo2.nCanMultiCount > 12) { _SysInfo2.nMultiSendData[12] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue15.ToString()); }
					if (_SysInfo2.nCanMultiCount > 13) { _SysInfo2.nMultiSendData[13] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue16.ToString()); }
					if (_SysInfo2.nCanMultiCount > 14) { _SysInfo2.nMultiSendData[14] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue17.ToString()); }
					if (_SysInfo2.nCanMultiCount > 15) { _SysInfo2.nMultiSendData[15] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue18.ToString()); }
					if (_SysInfo2.nCanMultiCount > 16) { _SysInfo2.nMultiSendData[16] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue19.ToString()); }
					if (_SysInfo2.nCanMultiCount > 17) { _SysInfo2.nMultiSendData[17] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue20.ToString()); }
					if (_SysInfo2.nCanMultiCount > 18) { _SysInfo2.nMultiSendData[18] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue21.ToString()); }
					if (_SysInfo2.nCanMultiCount > 19) { _SysInfo2.nMultiSendData[19] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue22.ToString()); }
					if (_SysInfo2.nCanMultiCount > 20) { _SysInfo2.nMultiSendData[20] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue23.ToString()); }
					if (_SysInfo2.nCanMultiCount > 21) { _SysInfo2.nMultiSendData[21] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue24.ToString()); }
					if (_SysInfo2.nCanMultiCount > 22) { _SysInfo2.nMultiSendData[22] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue25.ToString()); }
					if (_SysInfo2.nCanMultiCount > 23) { _SysInfo2.nMultiSendData[23] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue26.ToString()); }
					if (_SysInfo2.nCanMultiCount > 24) { _SysInfo2.nMultiSendData[24] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue27.ToString()); }
					if (_SysInfo2.nCanMultiCount > 25) { _SysInfo2.nMultiSendData[25] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue28.ToString()); }
					if (_SysInfo2.nCanMultiCount > 26) { _SysInfo2.nMultiSendData[26] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue29.ToString()); }
					if (_SysInfo2.nCanMultiCount > 27) { _SysInfo2.nMultiSendData[27] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue30.ToString()); }
					if (_SysInfo2.nCanMultiCount > 28) { _SysInfo2.nMultiSendData[28] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue31.ToString()); }
					if (_SysInfo2.nCanMultiCount > 29) { _SysInfo2.nMultiSendData[29] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue32.ToString()); }
					if (_SysInfo2.nCanMultiCount > 30) { _SysInfo2.nMultiSendData[30] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue33.ToString()); }
					if (_SysInfo2.nCanMultiCount > 31) { _SysInfo2.nMultiSendData[31] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue34.ToString()); }
					if (_SysInfo2.nCanMultiCount > 32) { _SysInfo2.nMultiSendData[32] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue35.ToString()); }
					if (_SysInfo2.nCanMultiCount > 33) { _SysInfo2.nMultiSendData[33] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue36.ToString()); }
					if (_SysInfo2.nCanMultiCount > 34) { _SysInfo2.nMultiSendData[34] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue37.ToString()); }
					if (_SysInfo2.nCanMultiCount > 35) { _SysInfo2.nMultiSendData[35] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue38.ToString()); }
					if (_SysInfo2.nCanMultiCount > 36) { _SysInfo2.nMultiSendData[36] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue39.ToString()); }
					if (_SysInfo2.nCanMultiCount > 37) { _SysInfo2.nMultiSendData[37] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue40.ToString()); }
					if (_SysInfo2.nCanMultiCount > 38) { _SysInfo2.nMultiSendData[38] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue41.ToString()); }
					if (_SysInfo2.nCanMultiCount > 39) { _SysInfo2.nMultiSendData[39] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue42.ToString()); }
					if (_SysInfo2.nCanMultiCount > 40) { _SysInfo2.nMultiSendData[40] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue43.ToString()); }
					if (_SysInfo2.nCanMultiCount > 41) { _SysInfo2.nMultiSendData[41] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue44.ToString()); }
					if (_SysInfo2.nCanMultiCount > 42) { _SysInfo2.nMultiSendData[42] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue45.ToString()); }
					if (_SysInfo2.nCanMultiCount > 43) { _SysInfo2.nMultiSendData[43] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue46.ToString()); }
					if (_SysInfo2.nCanMultiCount > 44) { _SysInfo2.nMultiSendData[44] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue47.ToString()); }
					if (_SysInfo2.nCanMultiCount > 45) { _SysInfo2.nMultiSendData[45] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue48.ToString()); }
					if (_SysInfo2.nCanMultiCount > 46) { _SysInfo2.nMultiSendData[46] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue49.ToString()); }
					if (_SysInfo2.nCanMultiCount > 47) { _SysInfo2.nMultiSendData[47] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue50.ToString()); }
					if (_SysInfo2.nCanMultiCount > 48) { _SysInfo2.nMultiSendData[48] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue51.ToString()); }
					if (_SysInfo2.nCanMultiCount > 49) { _SysInfo2.nMultiSendData[49] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue52.ToString()); }
					if (_SysInfo2.nCanMultiCount > 50) { _SysInfo2.nMultiSendData[50] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue53.ToString()); }
					if (_SysInfo2.nCanMultiCount > 51) { _SysInfo2.nMultiSendData[51] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue54.ToString()); }
					if (_SysInfo2.nCanMultiCount > 52) { _SysInfo2.nMultiSendData[52] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue55.ToString()); }
					if (_SysInfo2.nCanMultiCount > 53) { _SysInfo2.nMultiSendData[53] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue56.ToString()); }
					if (_SysInfo2.nCanMultiCount > 54) { _SysInfo2.nMultiSendData[54] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue57.ToString()); }
					if (_SysInfo2.nCanMultiCount > 55) { _SysInfo2.nMultiSendData[55] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue58.ToString()); }
					if (_SysInfo2.nCanMultiCount > 56) { _SysInfo2.nMultiSendData[56] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue59.ToString()); }
					if (_SysInfo2.nCanMultiCount > 57) { _SysInfo2.nMultiSendData[57] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue60.ToString()); }
					if (_SysInfo2.nCanMultiCount > 58) { _SysInfo2.nMultiSendData[58] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue61.ToString()); }
					if (_SysInfo2.nCanMultiCount > 59) { _SysInfo2.nMultiSendData[59] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue62.ToString()); }
					if (_SysInfo2.nCanMultiCount > 60) { _SysInfo2.nMultiSendData[60] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue63.ToString()); }
					if (_SysInfo2.nCanMultiCount > 61) { _SysInfo2.nMultiSendData[61] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue64.ToString()); }
					if (_SysInfo2.nCanMultiCount > 62) { _SysInfo2.nMultiSendData[62] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue65.ToString()); }
					if (_SysInfo2.nCanMultiCount > 63) { _SysInfo2.nMultiSendData[63] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue66.ToString()); }
					if (_SysInfo2.nCanMultiCount > 64) { _SysInfo2.nMultiSendData[64] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue67.ToString()); }
					if (_SysInfo2.nCanMultiCount > 65) { _SysInfo2.nMultiSendData[65] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue68.ToString()); }
					if (_SysInfo2.nCanMultiCount > 66) { _SysInfo2.nMultiSendData[66] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue69.ToString()); }
					if (_SysInfo2.nCanMultiCount > 67) { _SysInfo2.nMultiSendData[67] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue70.ToString()); }
					if (_SysInfo2.nCanMultiCount > 68) { _SysInfo2.nMultiSendData[68] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue71.ToString()); }
					if (_SysInfo2.nCanMultiCount > 69) { _SysInfo2.nMultiSendData[69] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue72.ToString()); }
					if (_SysInfo2.nCanMultiCount > 70) { _SysInfo2.nMultiSendData[70] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue73.ToString()); }
					if (_SysInfo2.nCanMultiCount > 71) { _SysInfo2.nMultiSendData[71] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue74.ToString()); }
					if (_SysInfo2.nCanMultiCount > 72) { _SysInfo2.nMultiSendData[72] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue75.ToString()); }
					if (_SysInfo2.nCanMultiCount > 73) { _SysInfo2.nMultiSendData[73] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue76.ToString()); }
					if (_SysInfo2.nCanMultiCount > 74) { _SysInfo2.nMultiSendData[74] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue77.ToString()); }
					if (_SysInfo2.nCanMultiCount > 75) { _SysInfo2.nMultiSendData[75] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue78.ToString()); }
					if (_SysInfo2.nCanMultiCount > 76) { _SysInfo2.nMultiSendData[76] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue79.ToString()); }
					if (_SysInfo2.nCanMultiCount > 77) { _SysInfo2.nMultiSendData[77] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue80.ToString()); }
					if (_SysInfo2.nCanMultiCount > 78) { _SysInfo2.nMultiSendData[78] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue81.ToString()); }
					if (_SysInfo2.nCanMultiCount > 79) { _SysInfo2.nMultiSendData[79] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue82.ToString()); }
					if (_SysInfo2.nCanMultiCount > 80) { _SysInfo2.nMultiSendData[80] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue83.ToString()); }
					if (_SysInfo2.nCanMultiCount > 81) { _SysInfo2.nMultiSendData[81] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue84.ToString()); }
					if (_SysInfo2.nCanMultiCount > 82) { _SysInfo2.nMultiSendData[82] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue85.ToString()); }
					if (_SysInfo2.nCanMultiCount > 83) { _SysInfo2.nMultiSendData[83] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue86.ToString()); }
					if (_SysInfo2.nCanMultiCount > 84) { _SysInfo2.nMultiSendData[84] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue87.ToString()); }
					if (_SysInfo2.nCanMultiCount > 85) { _SysInfo2.nMultiSendData[85] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue88.ToString()); }
					if (_SysInfo2.nCanMultiCount > 86) { _SysInfo2.nMultiSendData[86] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue89.ToString()); }
					if (_SysInfo2.nCanMultiCount > 87) { _SysInfo2.nMultiSendData[87] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue90.ToString()); }
					if (_SysInfo2.nCanMultiCount > 88) { _SysInfo2.nMultiSendData[88] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue91.ToString()); }
					if (_SysInfo2.nCanMultiCount > 89) { _SysInfo2.nMultiSendData[89] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue92.ToString()); }
					if (_SysInfo2.nCanMultiCount > 90) { _SysInfo2.nMultiSendData[90] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue93.ToString()); }
					if (_SysInfo2.nCanMultiCount > 91) { _SysInfo2.nMultiSendData[91] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue94.ToString()); }
					if (_SysInfo2.nCanMultiCount > 92) { _SysInfo2.nMultiSendData[92] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue95.ToString()); }
					if (_SysInfo2.nCanMultiCount > 93) { _SysInfo2.nMultiSendData[93] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue96.ToString()); }
					if (_SysInfo2.nCanMultiCount > 94) { _SysInfo2.nMultiSendData[94] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue97.ToString()); }
					if (_SysInfo2.nCanMultiCount > 95) { _SysInfo2.nMultiSendData[95] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue98.ToString()); }
					if (_SysInfo2.nCanMultiCount > 96) { _SysInfo2.nMultiSendData[96] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue99.ToString()); }
					if (_SysInfo2.nCanMultiCount > 97) { _SysInfo2.nMultiSendData[97] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue100.ToString()); }


					SendTCPMultiCommand2(_SysInfo2.nCanCh, _SysInfo2.nCanAddr, _SysInfo2.nMultiSendData, _SysInfo2.nCanMultiCount);
					nProcessStep[nStepIndex]++;
					break;

				case 21801:
					_SysInfo2.nSubWorkStep++;
					nProcessStep[nStepIndex] = 20000;
					break;


				// Modbus TCP Read(COMP)
				case 21900:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nID.ToString(), out _SysInfo2.nCanCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue1.ToString(), out _SysInfo2.nCanAddr);
					//int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue2.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nCanData);
					_SysInfo2.nCanData = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue2.ToString());
					SendTCPReadCommand2(_SysInfo2.nCanCh, _SysInfo2.nCanAddr);

					tMainTimer[nStepIndex].Start(5000);
					nProcessStep[nStepIndex]++;
					break;

				case 21901:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), "TIME OUT", "NG");
						_SysInfo2.bEolNg = true;
						_SysInfo2.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}


					if (_ModbusSoket2.bResultOk)
					{

						if (_SysInfo2.btTcpReadData[7] == 0x03)
						{
							if ((_SysInfo2.btTcpReadData[9] * 0x100) + _SysInfo2.btTcpReadData[10] == _SysInfo2.nCanData)
							{
								NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), ((_SysInfo2.btTcpReadData[9] * 0x100) + _SysInfo2.btTcpReadData[10]).ToString("X4"), "OK");
								//AppendLogMsg2($"OK / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
							}
							else
							{
								_SysInfo2.bEolNg = true;
								NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), ((_SysInfo2.btTcpReadData[9] * 0x100) + _SysInfo2.btTcpReadData[10]).ToString("X4"), "NG");
								//AppendLogMsg2($"NG / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
							}
						}
						else
						{
							_SysInfo2.bEolNg = true;
							NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), "DATA ERROR", "NG");
						}
						_SysInfo2.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;



				case 22000:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nID.ToString(), out _SysInfo2.nCanCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue1.ToString(), out _SysInfo2.nCanAddr);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue2.ToString(), out _SysInfo2.nBuffIndex);
					tMainTimer[nStepIndex].Start(5000);
					SendTCPReadCommand2(_SysInfo2.nCanCh, _SysInfo2.nCanAddr);
					nProcessStep[nStepIndex]++;
					break;

				case 22001:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), "", "TIME OUT", "NG");
						_SysInfo2.bEolNg = true;
						_SysInfo2.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}

					if (_ModbusSoket2.bResultOk)
					{
						if (_SysInfo2.btTcpReadData[7] == 0x03)
						{
							_SysInfo2.nReadDataBuff[_SysInfo2.nBuffIndex] = (_SysInfo2.btTcpReadData[9] * 0x100) + _SysInfo2.btTcpReadData[10];
							NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), $"BUFFER{_SysInfo2.nBuffIndex}", ((_SysInfo2.btTcpReadData[9] * 0x100) + _SysInfo2.btTcpReadData[10]).ToString("X4"), "");
						}
						_SysInfo2.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;

				case 22100:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].nID.ToString(), out _SysInfo2.nCanCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue1.ToString(), out _SysInfo2.nCanAddr);
					//int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue2.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nCanData);
					_SysInfo2.nCanData = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue2.ToString());
					SendReadCommand(_SysInfo2.nCanCh, _SysInfo2.nCanAddr, 2);

					tMainTimer[nStepIndex].Start(5000);
					nProcessStep[nStepIndex]++;
					break;

				case 22101:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), "TIME OUT", "NG");
						_SysInfo2.bEolNg = true;
						_SysInfo2.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}


					if (_CanComm[_SysInfo2.nCanCh].bReadMessage)
					{
						if (_CanComm[_SysInfo2.nCanCh].btReadData[0] == 0x03)
						{
							if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue3 != null && _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue3 != "")
							{
								if (int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nSubWorkStep].strValue3.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMaskingData))
								{
									_SysInfo2.nCompData = ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3]) & _SysInfo2.nMaskingData;

									if (_SysInfo2.nCompData == _SysInfo2.nCanData)
									{
										NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3]).ToString("X4") + $"({(_SysInfo2.nMaskingData.ToString("X4"))})", "OK");
										//AppendLogMsg2($"OK / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
									}
									else
									{
										_SysInfo2.bEolNg = true;
										NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3]).ToString("X4") + $"({(_SysInfo2.nMaskingData.ToString("X4"))})", "NG");
										//AppendLogMsg2($"NG / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
									}
								}
								else
								{
									_SysInfo2.nCompData = ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3]);
									if (_SysInfo2.nCompData == _SysInfo2.nCanData)
									{
										NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), _SysInfo2.nCompData.ToString("X4"), "OK");
										//AppendLogMsg2($"OK / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
									}
									else
									{
										_SysInfo2.bEolNg = true;
										NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), _SysInfo2.nCompData.ToString("X4"), "NG");
										//AppendLogMsg2($"NG / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
									}
								}
							}
							else
							{
								_SysInfo2.nCompData = ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3]);
								if (_SysInfo2.nCompData == _SysInfo2.nCanData)
								{
									NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), _SysInfo2.nCompData.ToString("X4"), "OK");
									//AppendLogMsg2($"OK / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
								}
								else
								{
									_SysInfo2.bEolNg = true;
									NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), _SysInfo2.nCompData.ToString("X4"), "NG");
									//AppendLogMsg2($"NG / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
								}
							}


						}
						else
						{
							_SysInfo2.bEolNg = true;
							NgDataSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), "DATA ERROR", "NG");
						}
						_SysInfo2.nSubWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;

				// Sub Item 통신 종료
				case 29000:
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;



				// ADC Step
				case 30000:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nBuffIndex);
					double.TryParse(new DataTable().Compute(string.Format($"{_SysInfo2.nReadDataBuff[_SysInfo2.nBuffIndex]}{_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2}"), null).ToString(), out _SysInfo2.dbCalcData);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3, out _SysInfo2.nDispLen);

					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMin, out _SysInfo2.dbSpecMin);
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMax, out _SysInfo2.dbSpecMax);

					if (_SysInfo2.dbCalcData > _SysInfo2.dbSpecMax || _SysInfo2.dbCalcData < _SysInfo2.dbSpecMin)
					{
						TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F" + _SysInfo2.nDispLen.ToString()), "NG");
						_SysInfo2.bTestNG = true;
					}
					else
					{
						TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F" + _SysInfo2.nDispLen.ToString()), "OK");
					}
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;


				// 파워서플라이 1번
				case 31000:
					nProcessStep[nStepIndex] = 31010;
					break;

				case 31005:
					PowerSupply[2].SendData("*CLS");
					nProcessStep[nStepIndex] = 31010;
					break;

				// 1번 채널 Power Supply 설정
				case 31010:
					PowerSupply[2].SendData("*IDN?");
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex]++;
					break;

				case 31011:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg2("파워서플라이 초기화 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 90000;
					//}

					if (PowerSupply[2].IsReadData())
					{
						nProcessStep[nStepIndex] = 31020;
					}
					break;

				// 전압설정
				case 31020:
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.dbCommSendData);

					PowerSupply[2].SendData($"VOLT {_SysInfo2.dbCommSendData}");
					tMainTimer[nStepIndex].Start(200);
					nProcessStep[nStepIndex]++;
					break;

				case 31021:
					if (!tMainTimer[nStepIndex].Verify()) { break; }

					nProcessStep[nStepIndex] = 31030;
					break;

				case 31030:
					PowerSupply[2].SendData("VOLT?");
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex]++;
					break;

				case 31031:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg2("파워서플라이 Voltage Setting 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 90000;
					//}

					if (PowerSupply[2].IsReadData())
					{
						nProcessStep[nStepIndex] = 31040;
					}
					break;

				case 31040:
					double.TryParse(PowerSupply[2].GetReadData(), out _SysInfo2.dbCommReadData);

					if (_SysInfo2.dbCommReadData == _SysInfo2.dbCommSendData)
					{
						nProcessStep[nStepIndex] = 31050;
					}
					break;

				case 31050:
					if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2 == "0")
					{
						PowerSupply[2].SendData("OUTP OFF");
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2 == "1")
					{
						PowerSupply[2].SendData("OUTP ON");
					}
					TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
					nProcessStep[nStepIndex] = 31100;
					break;



				case 31100:
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;




				// 파워서플라이 1번
				case 31200:
					nProcessStep[nStepIndex] = 31210;
					break;

				case 31205:
					PowerSupply[2].SendData("*CLS");
					nProcessStep[nStepIndex] = 31210;
					break;

				// 1번 채널 Power Supply 설정
				case 31210:
					PowerSupply[2].SendData("*IDN?");
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex]++;
					break;

				case 31211:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg2("파워서플라이 초기화 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 90000;
					//}

					if (PowerSupply[2].IsReadData())
					{
						nProcessStep[nStepIndex] = 31220;
					}
					break;

				// 전압설정
				case 31220:
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMin, out _SysInfo2.dbSpecMin);
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMax, out _SysInfo2.dbSpecMax);

					PowerSupply[2].SendData($"MEAS:CURR?");
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex]++;
					break;

				case 31221:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg2("파워서플라이 Voltage Setting 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 90000;
					//}

					if (PowerSupply[2].IsReadData())
					{
						nProcessStep[nStepIndex] = 31240;
					}
					break;

				case 31240:
					double.TryParse(PowerSupply[2].GetReadData(), out _SysInfo2.dbCommReadData);

					if (_SysInfo2.dbCommReadData > _SysInfo2.dbSpecMax || _SysInfo2.dbCommReadData < _SysInfo2.dbSpecMin)
					{
						TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCommReadData.ToString(), "NG");
						_SysInfo2.bTestNG = true;
					}
					else
					{
						TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCommReadData.ToString(), "OK");
					}
					nProcessStep[nStepIndex] = 31300;
					break;


				case 31300:
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;






				// 파워서플라이 1번
				case 32000:
					nProcessStep[nStepIndex] = 32010;
					break;

				case 32005:
					PowerSupply[3].SendData("*CLS");
					nProcessStep[nStepIndex] = 32010;
					break;

				// 1번 채널 Power Supply 설정
				case 32010:
					PowerSupply[3].SendData("*IDN?");
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex]++;
					break;

				case 32011:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg2("파워서플라이 초기화 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 90000;
					//}

					if (PowerSupply[3].IsReadData())
					{
						nProcessStep[nStepIndex] = 32020;
					}
					break;

				// 전압설정
				case 32020:
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.dbCommSendData);

					PowerSupply[3].SendData($"VOLT {_SysInfo2.dbCommSendData}");
					tMainTimer[nStepIndex].Start(200);
					nProcessStep[nStepIndex]++;
					break;

				case 32021:
					if (!tMainTimer[nStepIndex].Verify()) { break; }

					nProcessStep[nStepIndex] = 32030;
					break;

				case 32030:
					PowerSupply[3].SendData("VOLT?");
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex]++;
					break;

				case 32031:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg2("파워서플라이 Voltage Setting 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 90000;
					//}

					if (PowerSupply[3].IsReadData())
					{
						nProcessStep[nStepIndex] = 32040;
					}
					break;

				case 32040:
					double.TryParse(PowerSupply[3].GetReadData(), out _SysInfo2.dbCommReadData);

					if (_SysInfo2.dbCommReadData == _SysInfo2.dbCommSendData)
					{
						nProcessStep[nStepIndex] = 32050;
					}
					break;

				case 32050:
					if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2 == "0")
					{
						PowerSupply[3].SendData("OUTP OFF");
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2 == "1")
					{
						PowerSupply[3].SendData("OUTP ON");
					}
					TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
					nProcessStep[nStepIndex] = 32100;
					break;


				case 32100:
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;


				// 파워서플라이 1번
				case 32200:
					nProcessStep[nStepIndex] = 32210;
					break;


				case 32205:
					PowerSupply[3].SendData("*CLS");
					nProcessStep[nStepIndex] = 32210;
					break;

				// 1번 채널 Power Supply 설정
				case 32210:
					PowerSupply[3].SendData("*IDN?");
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex]++;
					break;

				case 32211:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg2("파워서플라이 초기화 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 90000;
					//}

					if (PowerSupply[3].IsReadData())
					{
						nProcessStep[nStepIndex] = 32220;
					}
					break;

				// 전압설정
				case 32220:
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMin, out _SysInfo2.dbSpecMin);
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMax, out _SysInfo2.dbSpecMax);

					PowerSupply[3].SendData($"MEAS:CURR?");
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex]++;
					break;

				case 32221:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg2("파워서플라이 Voltage Setting 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 90000;
					//}

					if (PowerSupply[3].IsReadData())
					{
						nProcessStep[nStepIndex] = 32240;
					}
					break;

				case 32240:
					double.TryParse(PowerSupply[3].GetReadData(), out _SysInfo2.dbCommReadData);

					if (_SysInfo2.dbCommReadData > _SysInfo2.dbSpecMax || _SysInfo2.dbCommReadData < _SysInfo2.dbSpecMin)
					{
						TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCommReadData.ToString(), "NG");
						_SysInfo2.bTestNG = true;
					}
					else
					{
						TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCommReadData.ToString(), "OK");
					}
					nProcessStep[nStepIndex] = 32300;
					break;


				case 32300:
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;







				// IO 제어 스텝

				case 33000:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nIOIndex);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2, out _SysInfo2.nIOState);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3, out _SysInfo2.nDelayTime);
					
					
					SetDIOPort(DO.RY_01 + _SysInfo2.nIOIndex - 1, _SysInfo2.nIOState == 1);
					nProcessStep[nStepIndex] = 33900;
					break;


				// IO 제어 스텝
				//case 33000:
				//	if (_SysInfo.nSubWorkStep >= _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo.Count)
				//	{
				//		nProcessStep[nStepIndex] = 33100;
				//	}
				//	else
				//	{
				//		nProcessStep[nStepIndex] = 33100;
				//	}
				//	break;

				//case 33100:
				//	if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue1 == "0")
				//	{
				//		SetDIOPort(DO.BBMS_CB + _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nTestType, false);
				//	}
				//	else if (_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].strValue1 == "1")
				//	{
				//		SetDIOPort(DO.BBMS_CB + _ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nSubWorkStep].nTestType, true);
				//	}
				//	_SysInfo.nSubWorkStep++;
				//	nProcessStep[nStepIndex] = 33900;
				//	break;

				// Sub Item 통신 종료
				case 33900:
					TestResultSet2(_SysInfo2.nMainWorkStep, "OK", "OK");
					tMainTimer[nStepIndex].Start(_SysInfo2.nDelayTime);
					nProcessStep[nStepIndex] = 33910;
					break;

				case 33910:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				case 34000:
					nProcessStep[nStepIndex] = 34005;
					break;

				case 34005:
					if (_Config.bDmmEtcMode)
					{

						_KeysiteDmmEtc2.Send($"*RST", true);
						nProcessStep[nStepIndex] = 34010;
					}
					else
					{
						KeysiteDmm2.Send($"*RST");
						nProcessStep[nStepIndex] = 34010;
					}

					break;


				case 34010:
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc2.Send("*IDN?", true);
						nProcessStep[nStepIndex]++;
					}
					else
					{
						KeysiteDmm2.Send("*IDN?");
						nProcessStep[nStepIndex]++;
					}

					break;

				case 34011:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc2.IsReadData())
						{
							nProcessStep[nStepIndex] = 34020;
						}
					}
					else
					{
						if (KeysiteDmm2.IsReadData())
						{
							nProcessStep[nStepIndex] = 34020;
						}
					}

					break;

				// 전압설정
				case 34020:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nDmmCh);

					if (_Config.bDmmEtcMode)
					{
						if (_SysInfo2.nDmmCh > 20)
						{
							_KeysiteDmmEtc2.Send($"SENSe:FUNCtion \"VOLTage\", (@{200 + _SysInfo2.nDmmCh - 20})", true);
						}
						else
						{
							_KeysiteDmmEtc2.Send($"SENSe:FUNCtion \"VOLTage\", (@{100 + _SysInfo2.nDmmCh})", true);
						}
					}
					else
					{
						if (_SysInfo2.nDmmCh > 20)
						{
							KeysiteDmm2.Send($"SENSe:FUNCtion \"VOLTage\", (@{200 + _SysInfo2.nDmmCh - 20})");
						}
						else
						{
							KeysiteDmm2.Send($"SENSe:FUNCtion \"VOLTage\",(@{100 + _SysInfo2.nDmmCh})");
						}
					}


					nProcessStep[nStepIndex]++;
					break;

				case 34021:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nDmmCh);

					if (_Config.bDmmEtcMode)
					{
						if (_SysInfo2.nDmmCh > 20)
						{
							_KeysiteDmmEtc2.Send($"ROUTe:CLOSe  (@{200 + _SysInfo2.nDmmCh - 20})", true);
						}
						else
						{
							_KeysiteDmmEtc2.Send($"ROUTe:CLOSe  (@{100 + _SysInfo2.nDmmCh})", true);
						}
					}
					else
					{
						if (_SysInfo2.nDmmCh > 20)
						{
							KeysiteDmm2.Send($"ROUTe:CLOSe  (@{200 + _SysInfo2.nDmmCh - 20})");
						}
						else
						{
							KeysiteDmm2.Send($"ROUTe:CLOSe  (@{100 + _SysInfo2.nDmmCh})");
						}
					}

					tMainTimer[nStepIndex].Start(200);
					nProcessStep[nStepIndex]++;
					break;


				case 34022:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc2.Send($"READ?", true);

					}
					else
					{
						KeysiteDmm2.Send($"READ?");
					}

					tMainTimer[nStepIndex].Start(1000);
					nProcessStep[nStepIndex]++;
					break;

				case 34023:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc2.IsReadData())
						{
							nProcessStep[nStepIndex] = 34040;
						}
					}
					else
					{
						if (KeysiteDmm2.IsReadData())
						{
							nProcessStep[nStepIndex] = 34040;
						}
					}
					break;


				case 34040:
					//theApp.AppendLogMsg2(KeysiteDmm2.GetReadData(), MSG_TYPE.INFO);
					if (_Config.bDmmEtcMode)
					{
						double.TryParse(_KeysiteDmmEtc2.strReadMessage, out _SysInfo2.dbCommReadData);
					}
					else
					{
						double.TryParse(KeysiteDmm2.GetReadData(), out _SysInfo2.dbCommReadData);
					}

					//AppendLogMsg2(_SysInfo2.dbCommReadData.ToString(), MSG_TYPE.INFO);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3, out _SysInfo2.nBuffIndex);
					double.TryParse(new DataTable().Compute(string.Format($"{_SysInfo2.dbCommReadData}{_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2}"), null).ToString(), out _SysInfo2.dbCalcData);

					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMin, out _SysInfo2.dbSpecMin);
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMax, out _SysInfo2.dbSpecMax);

					// 0은 양쪽다 비교 , 1은 Min만 비교, 2는 Max만 비교
					if (_SysInfo2.nBuffIndex == 0)
					{
						if (_SysInfo2.dbCalcData > _SysInfo2.dbSpecMax || _SysInfo2.dbCalcData < _SysInfo2.dbSpecMin)
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "NG");
							_SysInfo2.bTestNG = true;
						}
						else
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "OK");
						}
					}
					else if (_SysInfo2.nBuffIndex == 1)
					{
						if (_SysInfo2.dbCalcData < _SysInfo2.dbSpecMin)
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "NG");
							_SysInfo2.bTestNG = true;
						}
						else
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "OK");
						}
					}
					else if (_SysInfo2.nBuffIndex == 2)
					{
						if (_SysInfo2.dbCalcData > _SysInfo2.dbSpecMax)
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "NG");
							_SysInfo2.bTestNG = true;
						}
						else
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "OK");
						}
					}

					//TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCommReadData.ToString("F2"), "OK");
					nProcessStep[nStepIndex] = 34050;
					break;

				case 34050:
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;


				// 파워서플라이 1번
				case 35000:
					nProcessStep[nStepIndex] = 35010;
					break;

				// 1번 채널 Power Supply 설정
				case 35010:
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc2.Send("*IDN?", true);
						nProcessStep[nStepIndex]++;
					}
					else
					{
						KeysiteDmm2.Send("*IDN?");
						nProcessStep[nStepIndex]++;
					}
					break;

				case 35011:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc2.IsReadData())
						{
							nProcessStep[nStepIndex] = 35020;
						}
					}
					else
					{
						if (KeysiteDmm2.IsReadData())
						{
							nProcessStep[nStepIndex] = 35020;
						}
					}
					break;

				// 전압설정
				case 35020:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nDmmCh);
					if (_Config.bDmmEtcMode)
					{

						if (_SysInfo2.nDmmCh > 20)
						{
							_KeysiteDmmEtc2.Send($"MEAS:FREQ? (@{200 + _SysInfo2.nDmmCh - 20})", true);
						}
						else
						{
							_KeysiteDmmEtc2.Send($"MEAS:FREQ? (@{100 + _SysInfo2.nDmmCh})", true);
						}
					}
					else
					{
						if (_SysInfo2.nDmmCh > 20)
						{
							KeysiteDmm2.Send($"MEAS:FREQ? (@{200 + _SysInfo2.nDmmCh - 20})");
						}
						else
						{
							KeysiteDmm2.Send($"MEAS:FREQ? (@{100 + _SysInfo2.nDmmCh})");
						}
					}


					nProcessStep[nStepIndex]++;
					break;

				case 35021:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc2.IsReadData())
						{
							nProcessStep[nStepIndex] = 35040;
						}
					}
					else
					{
						if (KeysiteDmm2.IsReadData())
						{
							nProcessStep[nStepIndex] = 35040;
						}
					}
					break;

				case 35040:
					//theApp.AppendLogMsg2(KeysiteDmm2.GetReadData(), MSG_TYPE.INFO);
					if (_Config.bDmmEtcMode)
					{
						double.TryParse(_KeysiteDmmEtc2.strReadMessage, out _SysInfo2.dbCommReadData);
					}
					else
					{
						double.TryParse(KeysiteDmm2.GetReadData(), out _SysInfo2.dbCommReadData);
					}
					//TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCommReadData.ToString("F2"), "OK");

					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3, out _SysInfo2.nBuffIndex);
					double.TryParse(new DataTable().Compute(string.Format($"{_SysInfo2.dbCommReadData}{_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2}"), null).ToString(), out _SysInfo2.dbCalcData);

					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMin, out _SysInfo2.dbSpecMin);
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMax, out _SysInfo2.dbSpecMax);

					// 0은 양쪽다 비교 , 1은 Min만 비교, 2는 Max만 비교
					if (_SysInfo2.nBuffIndex == 0)
					{
						if (_SysInfo2.dbCalcData > _SysInfo2.dbSpecMax || _SysInfo2.dbCalcData < _SysInfo2.dbSpecMin)
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "NG");
							_SysInfo2.bTestNG = true;
						}
						else
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "OK");
						}
					}
					else if (_SysInfo2.nBuffIndex == 1)
					{
						if (_SysInfo2.dbCalcData < _SysInfo2.dbSpecMin)
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "NG");
							_SysInfo2.bTestNG = true;
						}
						else
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "OK");
						}
					}
					else if (_SysInfo2.nBuffIndex == 2)
					{
						if (_SysInfo2.dbCalcData > _SysInfo2.dbSpecMax)
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "NG");
							_SysInfo2.bTestNG = true;
						}
						else
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "OK");
						}
					}
					nProcessStep[nStepIndex] = 35050;
					break;

				case 35050:
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;



				// 파워서플라이 1번
				case 36000:
					nProcessStep[nStepIndex] = 36005;
					break;

				case 36005:
					if (_Config.bDmmEtcMode)
					{

						_KeysiteDmmEtc2.Send($"*RST", true);
						nProcessStep[nStepIndex] = 36010;
					}
					else
					{
						KeysiteDmm2.Send($"*RST");
						nProcessStep[nStepIndex] = 36010;
					}

					break;

				// 1번 채널 Power Supply 설정
				case 36010:
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc2.Send("*IDN?", true);
						nProcessStep[nStepIndex]++;
					}
					else
					{
						KeysiteDmm2.Send("*IDN?");
						nProcessStep[nStepIndex]++;
					}
					break;

				case 36011:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc2.IsReadData())
						{
							nProcessStep[nStepIndex] = 36020;
						}
					}
					else
					{
						if (KeysiteDmm2.IsReadData())
						{
							nProcessStep[nStepIndex] = 36020;
						}
					}
					break;

				// 전압설정
				case 36020:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nDmmCh);

					if (_Config.bDmmEtcMode)
					{
						if (_SysInfo2.nDmmCh > 20)
						{
							_KeysiteDmmEtc2.Send($"SENSe:FUNCtion \"RESistance\", (@{200 + _SysInfo2.nDmmCh - 20})", true);
						}
						else
						{
							_KeysiteDmmEtc2.Send($"SENSe:FUNCtion \"RESistance\", (@{100 + _SysInfo2.nDmmCh})", true);
						}
					}
					else
					{
						if (_SysInfo2.nDmmCh > 20)
						{
							KeysiteDmm2.Send($"SENSe:FUNCtion \"RESistance\", (@{200 + _SysInfo2.nDmmCh - 20})");
						}
						else
						{
							KeysiteDmm2.Send($"SENSe:FUNCtion \"RESistance\", (@{100 + _SysInfo2.nDmmCh})");
						}
					}


					nProcessStep[nStepIndex]++;
					break;

				case 36021:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nDmmCh);

					if (_Config.bDmmEtcMode)
					{
						if (_SysInfo2.nDmmCh > 20)
						{
							_KeysiteDmmEtc2.Send($"ROUTe:CLOSe (@{200 + _SysInfo2.nDmmCh - 20})", true);
						}
						else
						{
							_KeysiteDmmEtc2.Send($"ROUTe:CLOSe (@{100 + _SysInfo2.nDmmCh})", true);
						}
					}
					else
					{
						if (_SysInfo2.nDmmCh > 20)
						{
							KeysiteDmm2.Send($"ROUTe:CLOSe (@{200 + _SysInfo2.nDmmCh - 20})");
						}
						else
						{
							KeysiteDmm2.Send($"ROUTe:CLOSe (@{100 + _SysInfo2.nDmmCh})");
						}
					}

					tMainTimer[nStepIndex].Start(200);
					nProcessStep[nStepIndex]++;
					break;


				case 36022:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc2.Send($"READ?", true);

					}
					else
					{
						KeysiteDmm2.Send($"READ?");
					}

					tMainTimer[nStepIndex].Start(1000);
					nProcessStep[nStepIndex] = 36030;
					break;

				case 36030:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc2.IsReadData())
						{
							nProcessStep[nStepIndex] = 36040;
						}
					}
					else
					{
						if (KeysiteDmm2.IsReadData())
						{
							nProcessStep[nStepIndex] = 36040;
						}
					}
					break;

				case 36040:
					//theApp.AppendLogMsg2(KeysiteDmm2.GetReadData(), MSG_TYPE.INFO);
					if (_Config.bDmmEtcMode)
					{
						double.TryParse(_KeysiteDmmEtc2.strReadMessage, out _SysInfo2.dbCommReadData);
					}
					else
					{
						double.TryParse(KeysiteDmm2.GetReadData(), out _SysInfo2.dbCommReadData);
					}

					if (_SysInfo2.dbCommReadData < 0)
					{
						nProcessStep[nStepIndex] = 36022;
						break;
					}
					else
					{
						int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3, out _SysInfo2.nBuffIndex);
						double.TryParse(new DataTable().Compute(string.Format($"{_SysInfo2.dbCommReadData}{_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2}"), null).ToString(), out _SysInfo2.dbCalcData);

						double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMin, out _SysInfo2.dbSpecMin);
						double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMax, out _SysInfo2.dbSpecMax);

						// 0은 양쪽다 비교 , 1은 Min만 비교, 2는 Max만 비교
						if (_SysInfo2.nBuffIndex == 0)
						{
							if (_SysInfo2.dbCalcData > _SysInfo2.dbSpecMax || _SysInfo2.dbCalcData < _SysInfo2.dbSpecMin)
							{
								TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "NG");
								nProcessStep[nStepIndex] = 36045;
								break;
							}
							else
							{
								TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "OK");
								nProcessStep[nStepIndex] = 36050;
								break;
							}
						}
						else if (_SysInfo2.nBuffIndex == 1)
						{
							if (_SysInfo2.dbCalcData < _SysInfo2.dbSpecMin)
							{
								TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "NG");
								nProcessStep[nStepIndex] = 36045;
								break;
							}
							else
							{
								TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "OK");
								nProcessStep[nStepIndex] = 36050;
								break;
							}
						}
						else if (_SysInfo2.nBuffIndex == 2)
						{
							if (_SysInfo2.dbCalcData > _SysInfo2.dbSpecMax)
							{
								TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "NG");
								nProcessStep[nStepIndex] = 36045;
								break;

							}
							else
							{
								TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "OK");
								nProcessStep[nStepIndex] = 36050;
								break;
							}
						}

					
					}



						
					break;

				case 36045:
					_SysInfo2.strPopupContent = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strTestName;
					_SysInfo2._SwStatus = MAIN_STATUS2.READY;
					_SysInfo2._PopupStatus = MAIN_STATUS2.READY;
					_SysInfo.nTL_Beep = 1;
					ShowRetryPopUpWindow2();
					nProcessStep[nStepIndex]++;
					break;

				case 36046:
					if (_SysInfo2._SwStatus == MAIN_STATUS2.OK)
					{
						nProcessStep[nStepIndex] = 36048;
					}
					else if (_SysInfo2._SwStatus == MAIN_STATUS2.NG)
					{
						
						_SysInfo2.bTestNG = true;
						nProcessStep[nStepIndex] = 36047;
					}
					else if (GetDIOPort(DI.START_SW3))
					{

						CloseRetryPopUpWindow2();
						nProcessStep[nStepIndex] = 36048;
					}
					else if (GetDIOPort(DI.START_SW4))
					{
					
						_SysInfo2.bTestNG = true;
						CloseRetryPopUpWindow2();
						nProcessStep[nStepIndex] = 36047;
					}
					break;

				case 36047:
					if (!GetDIOPort(DI.START_SW3) && !GetDIOPort(DI.START_SW4))
					{
						nProcessStep[nStepIndex] = 36050;
					}
					break;

				case 36048:
					if (!GetDIOPort(DI.START_SW3) && !GetDIOPort(DI.START_SW4))
					{
						nProcessStep[nStepIndex] = 36022;
					}
					break;



				case 36050:
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;



				//PopUp 행정
				case 37000:
					_SysInfo2.strPopupContent = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strTestName;
					_SysInfo2._SwStatus = MAIN_STATUS2.READY;
					_SysInfo2._PopupStatus = MAIN_STATUS2.READY;
					_SysInfo.nTL_Beep = 1;
					ShowPopUpWindow2();
					nProcessStep[nStepIndex]++;
					break;

				case 37001:
					if (_SysInfo2._SwStatus == MAIN_STATUS2.OK)
					{
						TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
						nProcessStep[nStepIndex] = 37002;
					}
					else if (_SysInfo2._SwStatus == MAIN_STATUS2.NG)
					{
						TestResultSet2(_SysInfo2.nMainWorkStep, "", "NG");
						_SysInfo2.bTestNG = true;
						nProcessStep[nStepIndex] = 80000;
					}
					else if (GetDIOPort(DI.START_SW3))
					{
						TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
						ClosePopUpWindow2();
						nProcessStep[nStepIndex] = 37002;
					}
					else if (GetDIOPort(DI.START_SW4))
					{
						TestResultSet2(_SysInfo2.nMainWorkStep, "", "NG");
						_SysInfo2.bTestNG = true;
						ClosePopUpWindow2();
						nProcessStep[nStepIndex] = 80000;
					}
					break;

				case 37002:
					if (!GetDIOPort(DI.START_SW3) && !GetDIOPort(DI.START_SW4))
					{
						nProcessStep[nStepIndex]++;
					}
					break;

				case 37003:
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				// Ping Test
				case 38000:
					if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1 == "1")
					{
						nProcessStep[nStepIndex] = 38010;

					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1 == "0")
					{
						nProcessStep[nStepIndex] = 38020;
					}
					break;
				// Ping 테스트 시작
				case 38010:
					_SysInfo2.bPingTestResult = MAIN_STATUS.READY;
					_SysInfo2.strPingTestIP = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2;
					nProcessStep[(int)PROC_LIST.PING_TEST2] = 100;
					nProcessStep[nStepIndex]++;
					break;

				case 38011:
					if (_SysInfo2.bPingTestResult == MAIN_STATUS.OK)
					{
						TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
						nProcessStep[nStepIndex] = 38012;
					}
					else if (_SysInfo2.bPingTestResult == MAIN_STATUS.NG)
					{
						TestResultSet2(_SysInfo2.nMainWorkStep, "", "NG");
						_SysInfo2.bTestNG = true;
						nProcessStep[nStepIndex] = 38012;
					}
					break;

				case 38012:
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				// Ping테스트 종료
				case 38020:
					nProcessStep[(int)PROC_LIST.PING_TEST2] = 0;
					TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;


				case 39000:
					{
						int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nBuffIndex);
						int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2, out _SysInfo2.nBuffCount);

						string strVesion = "";
						bool bCharResult = true;
						for (int i = 0; i < _SysInfo2.nBuffCount; i++)
						{
							strVesion += Convert.ToChar((_SysInfo2.nReadDataBuff[_SysInfo2.nBuffIndex + i] / 0x100));
							strVesion += Convert.ToChar((_SysInfo2.nReadDataBuff[_SysInfo2.nBuffIndex + i] % 0x100));
						}

						if ((_SysInfo2.nBuffCount * 2) == _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMin.Length && (_SysInfo2.nBuffCount * 2) == _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMax.Length)
						{
							for (int i = 0; i < _SysInfo2.nBuffCount; i++)
							{
								if (i % 2 == 0)
								{
									if ((int)_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMin[i] > (_SysInfo2.nReadDataBuff[_SysInfo2.nBuffIndex + (i / 2)] / 0x100) || (int)_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMax[i] < (_SysInfo2.nReadDataBuff[_SysInfo2.nBuffIndex + (i / 2)] / 0x100))
									{
										bCharResult = false;
									}
								}
								else if (i % 2 == 1)
								{
									if ((int)_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMin[i] > (_SysInfo2.nReadDataBuff[_SysInfo2.nBuffIndex + (i / 2)] % 0x100) || (int)_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMax[i] < (_SysInfo2.nReadDataBuff[_SysInfo2.nBuffIndex + (i / 2)] % 0x100))
									{
										bCharResult = false;
									}
								}

							}

							if (bCharResult)
							{
								TestResultSet2(_SysInfo2.nMainWorkStep, strVesion, "OK");
							}
							else
							{
								TestResultSet2(_SysInfo2.nMainWorkStep, strVesion, "NG");
								_SysInfo2.bTestNG = true;
							}
						}
						else
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, strVesion, "NG");
							_SysInfo2.bTestNG = true;
						}
						nProcessStep[nStepIndex]++;
					}
					break;

				case 39001:
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;


				case 40000:
					{
						int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nBuffIndex);
						int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2, out _SysInfo2.nBuffCount);

						string strVesion = "";
						bool bCharResult = true;
						for (int i = 0; i < _SysInfo2.nBuffCount; i++)
						{
							strVesion += Convert.ToChar((_SysInfo2.nReadDataBuff[_SysInfo2.nBuffIndex + i]));
							strVesion += Convert.ToChar((_SysInfo2.nReadDataBuff[_SysInfo2.nBuffIndex + i]));
						}

						if (_SysInfo2.nBuffCount == _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMin.Length && _SysInfo2.nBuffCount == _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMax.Length)
						{
							for (int i = 0; i < _SysInfo2.nBuffCount; i++)
							{
								if ((int)_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMin[i] > _SysInfo2.nReadDataBuff[_SysInfo2.nBuffIndex + i] || (int)_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMax[i] < _SysInfo2.nReadDataBuff[_SysInfo2.nBuffIndex + i])
								{
									bCharResult = false;
								}
							}

							if (bCharResult)
							{
								TestResultSet2(_SysInfo2.nMainWorkStep, strVesion, "OK");
							}
							else
							{
								TestResultSet2(_SysInfo2.nMainWorkStep, strVesion, "NG");
								_SysInfo2.bTestNG = true;
							}
						}
						else
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, strVesion, "NG");
							_SysInfo2.bTestNG = true;
						}
						nProcessStep[nStepIndex]++;
					}
					break;

				case 40001:
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				case 41000:
					_CellSimulator3.Send("*RST", true);
					nProcessStep[nStepIndex] = 41005;
					break;

				case 41005:
					_CellSimulator3.Send("*CLS", true);
					nProcessStep[nStepIndex] = 41010;
					break;

				case 41010:
					_CellSimulator3.Send("*IDN?", true);
					tMainTimer[nStepIndex].Start(10000);
					nProcessStep[nStepIndex]++;
					break;

				case 41011:
					if (tMainTimer[nStepIndex].Verify())
					{
						AppendLogMsg("Cell Simulator #1 initialization failed", MSG_TYPE.ERROR);
						nProcessStep[nStepIndex] = 90000;
					}

					if (_CellSimulator3.IsReadData())
					{
						nProcessStep[nStepIndex]++;
					}
					break;

				case 41012:
					_CellSimulator3.Send("SIM:OUTP?", true);
					nProcessStep[nStepIndex]++;
					break;

				case 41013:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg2("셀시뮬레이터 #2 초기화 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 0;
					//}

					if (_CellSimulator3.IsReadData())
					{
						nProcessStep[nStepIndex]++;
					}
					break;

				case 41014:
					if (_CellSimulator3.strReadMessage == "1")
					{

						nProcessStep[nStepIndex] = 41050;
					}
					else
					{

						nProcessStep[nStepIndex] = 41015;
					}
					break;

				case 41015:
					//double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3, out _SysInfo2.dbCellStates);
					//if(_SysInfo2.dbCellStates == 0)
					//{

					nProcessStep[nStepIndex] = 41016;
					//}
					//else
					//{
					//	_CellSimulator3.Send("SIM:OUTP OFF", true);
					//	TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
					//	_SysInfo2.nMainWorkStep++;
					//	nProcessStep[nStepIndex] = 3000;
					//}
					break;

				//case 41015:
				//	if (!tMainTimer[nStepIndex].Verify()) { break; }
				//	_CellSimulator3.Send("SYSTem:FRAME:STATe? 0", true);
				//	tMainTimer[nStepIndex].Start(300);
				//	nProcessStep[nStepIndex]++;
				//	break;

				case 41016:
					_CellSimulator3.Send("SIM:CONF:BMS:NUMB 1", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 41018;
					break;

				//case 41017:
				//	if (!tMainTimer[nStepIndex].Verify()) { break; }
				//	_CellSimulator3.Send("SIM:CONF:SAMP:TIME 10", true);
				//	tMainTimer[nStepIndex].Start(300);
				//	nProcessStep[nStepIndex]++;
				//	break;

				case 41018:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator3.Send("SIM:CONF:CELL:NUMB 1,16", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex]++;
					break;

				case 41019:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator3.Send("SIM:CONF:CELL:PARA 1,1,16,1,2", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 41030;
					break;

				case 41030:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.dbCellVolt);
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2, out _SysInfo2.dbCellStartCH1);
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3, out _SysInfo2.dbCellEndCH1);


					_CellSimulator3.Send($"SIM:PROG:CELL 1,1,{_SysInfo2.dbCellStartCH1},{_SysInfo2.dbCellEndCH1},{_SysInfo2.dbCellVolt},3", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 41040;
					break;

				case 41040:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator3.Send("SIM:OUTP ON", true);
					tMainTimer[nStepIndex].Start(300);
					//TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
					//_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 41100;
					break;


				case 41050:
					//double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3, out _SysInfo2.dbCellStates);
					//if (_SysInfo2.dbCellStates == 0)
					//{
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 41060;
					//}
					//else
					//{
					//	_CellSimulator3.Send("SIM:OUTP OFF", true);
					//	TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
					//	_SysInfo2.nMainWorkStep++;
					//	nProcessStep[nStepIndex] = 3000;
					//}
					break;

				case 41060:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.dbCellVolt);
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2, out _SysInfo2.dbCellStartCH1);
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3, out _SysInfo2.dbCellEndCH1);


					_CellSimulator3.Send($"SIM:PROG:CELL 1,1,{_SysInfo2.dbCellStartCH1},{_SysInfo2.dbCellEndCH1},{_SysInfo2.dbCellVolt},3", true);
					tMainTimer[nStepIndex].Start(200);
					nProcessStep[nStepIndex] = 41070;
					break;

				case 41070:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator3.Send("SIM:OUTP:IMM", true);
					tMainTimer[nStepIndex].Start(200);
					//TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
					//_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 41100;
					break;


				case 41100:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator3.Send("SIM:OUTP?", true);
					nProcessStep[nStepIndex]++;
					break;

				case 41101:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg2("셀시뮬레이터 #2 초기화 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 0;
					//}

					if (_CellSimulator3.IsReadData())
					{
						nProcessStep[nStepIndex]++;
					}
					break;

				case 41102:
					if (_SysInfo2.dbCellVolt == 0)
					{
						TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
						_SysInfo2.nMainWorkStep++;
						nProcessStep[nStepIndex] = 3000;
					}
					else
					{
						if (_CellSimulator3.strReadMessage == "1")
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
							_SysInfo2.nMainWorkStep++;
							nProcessStep[nStepIndex] = 3000;
						}
						else
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, "", "NG");
							_SysInfo2.bTestNG = true;
							_SysInfo2.nMainWorkStep++;
							nProcessStep[nStepIndex] = 3000;
						}
					}
					
					break;


				case 42000:
					_CellSimulator4.Send("*RST", true);
					nProcessStep[nStepIndex] = 42005;
					break;

				case 42005:
					_CellSimulator4.Send("*CLS", true);
					nProcessStep[nStepIndex] = 42010;
					break;

				case 42010:
					_CellSimulator4.Send("*IDN?", true);
					tMainTimer[nStepIndex].Start(10000);
					nProcessStep[nStepIndex]++;
					break;

				case 42011:
					if (tMainTimer[nStepIndex].Verify())
					{
						AppendLogMsg("Cell Simulator #2 initialization failed", MSG_TYPE.ERROR);
						nProcessStep[nStepIndex] = 90000;
					}

					if (_CellSimulator4.IsReadData())
					{
						nProcessStep[nStepIndex] = 42012;
					}
					break;

				case 42012:
					_CellSimulator4.Send("SIM:OUTP?", true);
					nProcessStep[nStepIndex]++;
					break;

				case 42013:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg2("셀시뮬레이터 #2 초기화 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 0;
					//}

					if (_CellSimulator4.IsReadData())
					{
						nProcessStep[nStepIndex] = 42014;
					}
					break;

				case 42014:

					if (_CellSimulator4.strReadMessage == "1")
					{

						nProcessStep[nStepIndex] = 42050;
					}
					else
					{

						nProcessStep[nStepIndex] = 42015;
					}
					break;

				case 42015:

					//double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3, out _SysInfo2.dbCellStates2);

					//if (_SysInfo2.dbCellStates2 == 0)
					//{
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 42016;
					//}
					//else
					//{
					//	_CellSimulator4.Send("SIM:OUTP OFF", true);
					//	TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
					//	_SysInfo2.nMainWorkStep++;
					//	nProcessStep[nStepIndex] = 3000;
					//}
					break;

				//case 42015:
				//	if (!tMainTimer[nStepIndex].Verify()) { break; }
				//	_CellSimulator4.Send("SYSTem:FRAME:STATe? 0", true);
				//	tMainTimer[nStepIndex].Start(300);
				//	nProcessStep[nStepIndex]++;
				//	break;

				case 42016:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator4.Send("SIM:CONF:BMS:NUMB 1", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 42018;
					break;

				//case 42017:
				//	if (!tMainTimer[nStepIndex].Verify()) { break; }
				//	_CellSimulator4.Send("SIM:CONF:SAMP:TIME 10", true);
				//	tMainTimer[nStepIndex].Start(300);
				//	nProcessStep[nStepIndex]++;
				//	break;

				case 42018:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator4.Send("SIM:CONF:CELL:NUMB 1,16", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex]++;
					break;

				case 42019:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator4.Send("SIM:CONF:CELL:PARA 1,1,16,1,2", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 42030;
					break;

				case 42030:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.dbCellVolt2);
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2, out _SysInfo2.dbCellStartCH2);
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3, out _SysInfo2.dbCellEndCH2);


					_CellSimulator4.Send($"SIM:PROG:CELL 1,1,{_SysInfo2.dbCellStartCH2},{_SysInfo2.dbCellEndCH2},{_SysInfo2.dbCellVolt2},3", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 42040;
					break;

				case 42040:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator4.Send("SIM:OUTP ON", true);
					tMainTimer[nStepIndex].Start(300);
					//TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
					//_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 42100;
					break;

				case 42050:
					//double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3, out _SysInfo2.dbCellStates);
					//if (_SysInfo2.dbCellStates == 0)
					//{
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 42060;
					//}
					//else
					//{
					//	_CellSimulator4.Send("SIM:OUTP OFF", true);
					//	TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
					//	_SysInfo2.nMainWorkStep++;
					//	nProcessStep[nStepIndex] = 3000;
					//}
					break;

				case 42060:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.dbCellVolt2);
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2, out _SysInfo2.dbCellStartCH2);
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3, out _SysInfo2.dbCellEndCH2);


					_CellSimulator4.Send($"SIM:PROG:CELL 1,1,{_SysInfo2.dbCellStartCH2},{_SysInfo2.dbCellEndCH2},{_SysInfo2.dbCellVolt2},3", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 42070;
					break;

				case 42070:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator4.Send("SIM:OUTP:IMM", true);
					tMainTimer[nStepIndex].Start(300);
					//TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
					//_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 42100;
					break;


				case 42100:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator4.Send("SIM:OUTP?", true);
					nProcessStep[nStepIndex]++;
					break;

				case 42101:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg2("셀시뮬레이터 #2 초기화 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 0;
					//}

					if (_CellSimulator4.IsReadData())
					{
						nProcessStep[nStepIndex]++;
					}
					break;

				case 42102:
					if (_SysInfo2.dbCellVolt2 == 0)
					{
						TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
						_SysInfo2.nMainWorkStep++;
						nProcessStep[nStepIndex] = 3000;
					}
					else
					{
						if (_CellSimulator4.strReadMessage == "1")
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
							_SysInfo2.nMainWorkStep++;
							nProcessStep[nStepIndex] = 3000;
						}
						else
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, "", "NG");
							_SysInfo2.bTestNG = true;
							_SysInfo2.nMainWorkStep++;
							nProcessStep[nStepIndex] = 3000;
						}
					}
					
					break;

				case 43000:
					_SysInfo2.dbCalcData = 0.0;
					_SysInfo2.dbCommReadData = 0.0;
					nProcessStep[nStepIndex] = 43005;
					break;

				case 43005:
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc2.Send("*RST", true);
						nProcessStep[nStepIndex] = 43010;
					}
					else
					{
						KeysiteDmm2.Send("*RST");
						nProcessStep[nStepIndex] = 43010;
					}

					break;

				// 1번 채널 Power Supply 설정
				case 43010:
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc2.Send("*IDN?", true);
						nProcessStep[nStepIndex]++;
					}
					else
					{
						KeysiteDmm2.Send("*IDN?");
						nProcessStep[nStepIndex]++;
					}
					break;

				case 43011:
					_SysInfo2.bFirstCheck = true;
					if (_Config.bDmmEtcMode)
					{
						if (_KeysiteDmmEtc2.IsReadData())
						{
							//tMainTimer[nStepIndex].Start(200);
							nProcessStep[nStepIndex] = 43020;
						}
					}
					else
					{
						if (KeysiteDmm2.IsReadData())
						{
							//tMainTimer[nStepIndex].Start(200);
							nProcessStep[nStepIndex] = 43020;
						}
					}
					break;

				case 43020:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nDmmCh);
					if (_Config.bDmmEtcMode)
					{
						//if (_SysInfo2.nDmmCh == 1)
						//{
						//	_KeysiteDmmEtc2.Send($"CONF:CURR:DC (@122)",true);
						//}
						//else
						//{
						_KeysiteDmmEtc2.Send($"SENSe:FUNCtion \"CURRent\", (@{_SysInfo2.nDmmCh})", true);
						//}
					}
					else
					{

						KeysiteDmm2.Send($"SENSe:FUNCtion \"CURRent\", (@{_SysInfo2.nDmmCh})");

					}

					nProcessStep[nStepIndex]++;
					break;

				case 43021:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nDmmCh);
					if (_Config.bDmmEtcMode)
					{
						//if (_SysInfo2.nDmmCh == 1)
						//{
						//	_KeysiteDmmEtc2.Send($"ROUT:SCAN (@{_SysInfo2.nDmmCh})",true);
						//}
						//else
						//{
						_KeysiteDmmEtc2.Send($"ROUTe:CLOSe (@{_SysInfo2.nDmmCh})", true);
						AppendDebugMsg($"Curr M #2 검사 시작", "CURR");
						//}
					}
					else
					{
						//if (_SysInfo2.nDmmCh == 1)
						//{
						//	KeysiteDmm2.Send($"ROUT:SCAN (@{_SysInfo2.nDmmCh})");
						//}
						//else
						//{
						KeysiteDmm2.Send($"ROUTe:CLOSe (@{_SysInfo2.nDmmCh})");
						AppendDebugMsg($"Curr M #2 검사 시작", "CURR");
						//}
					}


					nProcessStep[nStepIndex]++;
					break;

				case 43022:

					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nDmmCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3, out _SysInfo2.nDmmTime);

					//if(_SysInfo2.nDmmTime == 1)
					//{
					//	nProcessStep[nStepIndex] = 43023;
					//}
					//else
					//{
					//if (_Config.bDmmEtcMode)
					//{
					//	//if (_SysInfo2.nDmmCh == 1)
					//	//{
					//	//	_KeysiteDmmEtc2.Send($"SENS:CURR:APER {_ModelInfo2.dbDmmAScanSpeed},(@{_SysInfo2.nDmmCh})", true);
					//	//}
					//	//else
					//	//{
					//	_KeysiteDmmEtc2.Send($"SENS:CURR:APER {_ModelInfo2.dbDmmMScanSpeed},(@{_SysInfo2.nDmmCh})", true);
					//	//}
					//}
					//else
					//{
					//	//if (_SysInfo2.nDmmCh == 1)
					//	//{
					//	//	KeysiteDmm2.Send($"SENS:CURR:APER {_ModelInfo2.dbDmmAScanSpeed},(@{_SysInfo2.nDmmCh})");
					//	//}
					//	//else
					//	//{
					//	KeysiteDmm2.Send($"SENS:CURR:APER {_ModelInfo2.dbDmmMScanSpeed},(@{_SysInfo2.nDmmCh})");
					//	//}
					//}
					//tMainTimer[nStepIndex].Start(3000);
					nProcessStep[nStepIndex] = 43023;

					//}
					break;


				case 43023:
					//if (!tMainTimer[nStepIndex].Verify()) { break; }
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc2.Send($"READ?", true);
					}
					else
					{
						KeysiteDmm2.Send($"READ?");
					}

					//KeysiteDmm2.Send($"MEAS:VOLT:DC? (@{_SysInfo2.strDmmCh})");
					nProcessStep[nStepIndex] = 43024;
					break;

				case 43024:


					if (_Config.bDmmEtcMode)
					{
						if (_KeysiteDmmEtc2.IsReadData())
						{
							nProcessStep[nStepIndex] = 43025;
							if (_SysInfo2.bFirstCheck)
							{
								tMainTimer[nStepIndex].Start(200);
								_SysInfo2.bFirstCheck = false;
							}
						}
					}
					else
					{
						if (KeysiteDmm2.IsReadData())
						{
							nProcessStep[nStepIndex] = 43025;
							if (_SysInfo2.bFirstCheck)
							{
								tMainTimer[nStepIndex].Start(200);
								_SysInfo2.bFirstCheck = false;
							}
						}
					}
					break;

				case 43025:

					if (_Config.bDmmEtcMode)
					{
						double.TryParse(_KeysiteDmmEtc2.strReadMessage, out _SysInfo2.dbCommReadData);
						AppendDebugMsg($"Curr {_SysInfo2.dbCommReadData}", "CURR");
						cellT2.Add(_SysInfo2.dbCommReadData);
						nProcessStep[nStepIndex] = 43026;
					}
					else
					{
						double.TryParse(KeysiteDmm2.GetReadData(), out _SysInfo2.dbCommReadData);
						AppendDebugMsg($"Curr {_SysInfo2.dbCommReadData}", "CURR");
						cellT2.Add(_SysInfo2.dbCommReadData);
						nProcessStep[nStepIndex] = 43026;

					}

					break;

				case 43026:
					if (!tMainTimer[nStepIndex].Verify())
					{

						nProcessStep[nStepIndex] = 43023;

					}
					else
					{
						_SysInfo2.dbCommReadMinData = cellT2.Min();
						nProcessStep[nStepIndex] = 43040;

					}
					break;


				case 43040:
					//theApp.AppendLogMsg2(KeysiteDmm2.GetReadData(), MSG_TYPE.INFO);
					//if (_Config.bDmmEtcMode)
					//{
					//	double.TryParse(_KeysiteDmmEtc2.strReadMessage, out _SysInfo2.dbCommReadData);
					//}
					//else
					//{
					//	double.TryParse(KeysiteDmm2.GetReadData(), out _SysInfo2.dbCommReadData);
					//}

					//theApp.AppendLogMsg2(_SysInfo2.dbCommReadData.ToString(), MSG_TYPE.INFO);
					//int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3, out _SysInfo2.nBuffIndex);
					double.TryParse(new DataTable().Compute(string.Format($"{_SysInfo2.dbCommReadMinData}{_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2}"), null).ToString(), out _SysInfo2.dbCalcData);
					//theApp.AppendLogMsg2(_SysInfo2.dbCalcData.ToString(), MSG_TYPE.INFO);
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMin, out _SysInfo2.dbSpecMin);
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMax, out _SysInfo2.dbSpecMax);

					// 0은 양쪽다 비교 , 1은 Min만 비교, 2는 Max만 비교
					//if (_SysInfo2.nBuffIndex == 0)
					//{
					if (_SysInfo2.dbCalcData > _SysInfo2.dbSpecMax || _SysInfo2.dbCalcData < _SysInfo2.dbSpecMin)
					{
						if (_SysInfo2.nCurrNGRetryCount > 5)
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "NG");
							_SysInfo2.bTestNG = true;
						}
						else
						{
							_SysInfo2.nCurrNGRetryCount++;
							AppendDebugMsg($"Curr NG {_SysInfo2.dbCalcData}", "CURR");
							nProcessStep[nStepIndex] = 43000;
							break;
						}


					}
					else
					{

						TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "OK");
					}
					//}
					//else if (_SysInfo2.nBuffIndex == 1)
					//{
					//	if (_SysInfo2.dbCalcData < _SysInfo2.dbSpecMin)
					//	{
					//		TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "NG");
					//	}
					//	else
					//	{
					//		TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "OK");
					//	}
					//}
					//else if (_SysInfo2.nBuffIndex == 2)
					//{
					//	if (_SysInfo2.dbCalcData > _SysInfo2.dbSpecMax)
					//	{
					//		TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "NG");
					//	}
					//	else
					//	{
					//		TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "OK");
					//	}
					//}

					nProcessStep[nStepIndex] = 43050;
					break;

				case 43050:
					_SysInfo2.nCurrNGRetryCount = 0;
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				case 44000:
					nProcessStep[nStepIndex] = 44005;
					break;

				case 44005:
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc2.Send("*RST", true);
						nProcessStep[nStepIndex] = 44010;
					}
					else
					{
						KeysiteDmm2.Send("*RST");
						nProcessStep[nStepIndex] = 44010;
					}

					break;

				// 1번 채널 Power Supply 설정
				case 44010:
					if (_Config.bDmmEtcMode)
					{

						_KeysiteDmmEtc2.Send("*IDN?", true);
						nProcessStep[nStepIndex]++;
					}
					else
					{
						KeysiteDmm2.Send("*IDN?");
						nProcessStep[nStepIndex]++;
					}

					break;

				case 44011:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc2.IsReadData())
						{
							nProcessStep[nStepIndex] = 44020;
						}
					}
					else
					{
						if (KeysiteDmm2.IsReadData())
						{
							nProcessStep[nStepIndex] = 44020;
						}
					}

					break;

				// 전압설정
				case 44020:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nDmmCh);
					if (_Config.bDmmEtcMode)
					{

						if (_SysInfo2.nDmmCh > 20)
						{
							_KeysiteDmmEtc2.Send($"SENSe:FUNCtion \"VOLTage\", (@{200 + _SysInfo2.nDmmCh - 20})", true);
						}
						else
						{
							_KeysiteDmmEtc2.Send($"SENSe:FUNCtion \"VOLTage\", (@{100 + _SysInfo2.nDmmCh})", true);
						}
					}
					else
					{
						if (_SysInfo2.nDmmCh > 20)
						{
							KeysiteDmm2.Send($"SENSe:FUNCtion \"VOLTage\", (@{200 + _SysInfo2.nDmmCh - 20})");
						}
						else
						{
							KeysiteDmm2.Send($"SENSe:FUNCtion \"VOLTage\", (@{100 + _SysInfo2.nDmmCh})");
						}
					}
					nProcessStep[nStepIndex] = 44021;
					break;

				case 44021:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nDmmCh);
					if (_Config.bDmmEtcMode)
					{

						if (_SysInfo2.nDmmCh > 20)
						{
							_KeysiteDmmEtc2.Send($"ROUT:SCAN (@{200 + _SysInfo2.nDmmCh - 20})", true);
						}
						else
						{
							_KeysiteDmmEtc2.Send($"ROUT:SCAN (@{100 + _SysInfo2.nDmmCh})", true);
						}
					}
					else
					{
						if (_SysInfo2.nDmmCh > 20)
						{
							KeysiteDmm2.Send($"ROUT:SCAN (@{200 + _SysInfo2.nDmmCh - 20})");
						}
						else
						{
							KeysiteDmm2.Send($"ROUT:SCAN (@{100 + _SysInfo2.nDmmCh})");
						}
					}
					nProcessStep[nStepIndex] = 44026;
					break;

				//case 44025:

				//	int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nDmmCh);
				//	if (_Config.bDmmEtcMode)
				//	{

				//		if (_SysInfo2.nDmmCh > 20)
				//		{
				//			_KeysiteDmmEtc2.Send($"SENS:VOLT:APER {_ModelInfo2.dbDmmScanSpeed},(@{200 + _SysInfo2.nDmmCh - 20})", true);
				//		}
				//		else
				//		{
				//			_KeysiteDmmEtc2.Send($"SENS:VOLT:APER {_ModelInfo2.dbDmmScanSpeed},(@{100 + _SysInfo2.nDmmCh})", true);
				//		}
				//	}
				//	else
				//	{
				//		if (_SysInfo2.nDmmCh > 20)
				//		{
				//			KeysiteDmm2.Send($"SENS:VOLT:APER {_ModelInfo2.dbDmmScanSpeed},(@{200 + _SysInfo2.nDmmCh - 20})");
				//		}
				//		else
				//		{
				//			KeysiteDmm2.Send($"SENS:VOLT:APER {_ModelInfo2.dbDmmScanSpeed},(@{100 + _SysInfo2.nDmmCh})");
				//		}
				//	}
				//	nProcessStep[nStepIndex] = 44030;
				//	break;

				case 44026:

					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nDmmCh);
					if (_Config.bDmmEtcMode)
					{

						if (_SysInfo2.nDmmCh > 20)
						{
							_KeysiteDmmEtc2.Send($"ROUTe:CLOSe (@{200 + _SysInfo2.nDmmCh - 20})", true);
						}
						else
						{
							_KeysiteDmmEtc2.Send($"ROUTe:CLOSe (@{100 + _SysInfo2.nDmmCh})", true);
						}
					}
					else
					{
						if (_SysInfo2.nDmmCh > 20)
						{
							KeysiteDmm2.Send($"ROUTe:CLOSe (@{200 + _SysInfo2.nDmmCh - 20})");
						}
						else
						{
							KeysiteDmm2.Send($"ROUTe:CLOSe (@{100 + _SysInfo2.nDmmCh})");
						}
					}
					nProcessStep[nStepIndex] = 44030;
					break;

				case 44030:
					//KeysiteDmm2.Send($"MEAS:VOLT:DC? (@{_SysInfo2.strDmmCh}");
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc2.Send($"READ?", true);
					}
					else
					{
						KeysiteDmm2.Send($"READ?");
					}
					nProcessStep[nStepIndex]++;
					break;

				case 44031:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc2.IsReadData())
						{
							nProcessStep[nStepIndex] = 44040;
						}
					}
					else
					{
						if (KeysiteDmm2.IsReadData())
						{
							nProcessStep[nStepIndex] = 44040;
						}
					}
					break;

				case 44040:

					//theApp.AppendLogMsg2(KeysiteDmm2.GetReadData(), MSG_TYPE.INFO);
					if (_Config.bDmmEtcMode)
					{
						double.TryParse(_KeysiteDmmEtc2.strReadMessage, out _SysInfo2.dbCommReadData);
					}
					else
					{
						double.TryParse(KeysiteDmm2.GetReadData(), out _SysInfo2.dbCommReadData);
					}

					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3, out _SysInfo2.nBuffIndex);
					double.TryParse(new DataTable().Compute(string.Format($"{_SysInfo2.dbCommReadData}{_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2}"), null).ToString(), out _SysInfo2.dbCalcData);

					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMin, out _SysInfo2.dbSpecMin);
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMax, out _SysInfo2.dbSpecMax);

					//AppendLogMsg2($"_SysInfo2.dbCalcData = {_SysInfo2.dbCalcData.ToString("F10")}", MSG_TYPE.LOG);

					_SysInfo2.dbRMSCommReadData = (_SysInfo2.dbCalcData / _ModelInfo2.dbResistance) * 1000;
					// 0은 양쪽다 비교 , 1은 Min만 비교, 2는 Max만 비교
					if (_SysInfo2.nBuffIndex == 0)
					{
						if (_SysInfo2.dbRMSCommReadData > _SysInfo2.dbSpecMax || _SysInfo2.dbRMSCommReadData < _SysInfo2.dbSpecMin)
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbRMSCommReadData.ToString("F4"), "NG");
							_SysInfo2.bTestNG = true;
						}
						else
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbRMSCommReadData.ToString("F4"), "OK");
						}
					}
					else if (_SysInfo2.nBuffIndex == 1)
					{
						if (_SysInfo2.dbRMSCommReadData < _SysInfo2.dbSpecMin)
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbRMSCommReadData.ToString("F4"), "NG");
							_SysInfo2.bTestNG = true;
						}
						else
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbRMSCommReadData.ToString("F4"), "OK");
						}
					}
					else if (_SysInfo2.nBuffIndex == 2)
					{
						if (_SysInfo2.dbRMSCommReadData > _SysInfo2.dbSpecMax)
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbRMSCommReadData.ToString("F4"), "NG");
							_SysInfo2.bTestNG = true;
						}
						else
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbRMSCommReadData.ToString("F4"), "OK");
						}
					}

					nProcessStep[nStepIndex] = 44050;
					break;

				case 44050:
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				case 45000:
					nProcessStep[nStepIndex] = 45010;
					break;

				case 45010:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nDelayTime);
					tMainTimer[nStepIndex].Start(_SysInfo2.nDelayTime);
					nProcessStep[nStepIndex] = 45020;
					break;

				case 45020:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				case 46000:
					nProcessStep[nStepIndex] = 46010;
					break;

				case 46010:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nSubEol);
					if (_SysInfo2.nSubEol == 0)
					{
						_SysInfo2.bSubEolStart = true;
						_SysInfo2.nSubMainWorkStep = _SysInfo2.nMainWorkStep;
						theApp.nProcessStep[(int)PROC_LIST.SUB_EOL2] = 10000;
						nProcessStep[nStepIndex] = 46020;
					}
					else
					{
						_SysInfo2.bSubEolStart = false;
						theApp.nProcessStep[(int)PROC_LIST.SUB_EOL2] = 30000;
						TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
						nProcessStep[nStepIndex] = 46020;
					}
					break;

				case 46020:
				
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				case 47000:
					nProcessStep[nStepIndex] = 47005;
					break;

				//case 47005:
				//	KeysiteDmm2.Send($"*CLS");
				//	nProcessStep[nStepIndex] = 47006;
				//	break;

				case 47005:
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc2.Send("*RST", true);
						nProcessStep[nStepIndex] = 47010;
					}
					else
					{
						KeysiteDmm2.Send("*RST");
						nProcessStep[nStepIndex] = 47010;
					}

					break;

				// 1번 채널 Power Supply 설정
				case 47010:
					if (_Config.bDmmEtcMode)
					{

						_KeysiteDmmEtc2.Send("*IDN?", true);
						nProcessStep[nStepIndex]++;
					}
					else
					{
						KeysiteDmm2.Send("*IDN?");
						nProcessStep[nStepIndex]++;
					}

					break;


				case 47011:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc2.IsReadData())
						{
							nProcessStep[nStepIndex] = 47020;
						}
					}
					else
					{
						if (KeysiteDmm2.IsReadData())
						{
							nProcessStep[nStepIndex] = 47020;
						}
					}

					break;

				// 전압설정
				case 47020:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nDmmCh);
					_SysInfo2.strDmmCh = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1;
					//AppendLogMsg2($"{_SysInfo2.nDmmBuffIndex.ToString()}", MSG_TYPE.LOG);
					if (_Config.bDmmEtcMode)
					{

						_KeysiteDmmEtc2.Send($"SENSe:FUNCtion \"VOLTage\", (@{_SysInfo2.nDmmCh})", true);
					}
					else
					{
						KeysiteDmm2.Send($"SENSe:FUNCtion \"VOLTage\", (@{_SysInfo2.nDmmCh})");
					}

					//KeysiteDmm2.Send($"SENS:VOLT:APER 0.20,(@101:120,201:210)");
					nProcessStep[nStepIndex] = 47021;
					break;


				case 47021:
					_SysInfo2.strDmmCh = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1;
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nDmmCh);
					if (_Config.bDmmEtcMode)
					{

						_KeysiteDmmEtc2.Send($"ROUTe:CLOSe (@{_SysInfo2.nDmmCh})", true);
					}
					else
					{
						KeysiteDmm2.Send($"ROUTe:CLOSe (@{_SysInfo2.nDmmCh})");
					}

					nProcessStep[nStepIndex] = 47030;
					break;

				case 47022:
					_SysInfo2.strDmmCh = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1;
					//if (_Config.bDmmEtcMode)
					//{

					//	_KeysiteDmmEtc2.Send($"SENS:VOLT:APERture {_ModelInfo2.dbDmmScanSpeed},(@{_SysInfo2.strDmmCh})", true);
					//}
					//else
					//{
					//	KeysiteDmm2.Send($"SENS:VOLT:APERture {_ModelInfo2.dbDmmScanSpeed},(@{_SysInfo2.strDmmCh})");
					//}


					nProcessStep[nStepIndex] = 47030;
					break;


				case 47030:

					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc2.Send($"READ?", true);
					}
					else
					{
						KeysiteDmm2.Send($"READ?");
					}
					//KeysiteDmm2.Send($"MEAS:VOLT:DC? (@{_SysInfo2.strDmmCh})");
					nProcessStep[nStepIndex] = 47035;
					break;

				case 47035:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc2.IsReadData())
						{
							nProcessStep[nStepIndex] = 47040;
						}
					}
					else
					{
						if (KeysiteDmm2.IsReadData())
						{
							nProcessStep[nStepIndex] = 47040;
						}
					}
					break;

				case 47040:
					if (_Config.bDmmEtcMode)
					{
						_SysInfo2.strDmmReadData = _KeysiteDmmEtc2.strReadMessage;
					}
					else
					{
						_SysInfo2.strDmmReadData = KeysiteDmm2.GetReadData();
					}

					double.TryParse(_SysInfo2.strDmmReadData, out _SysInfo2.dbDmmReadDataBuff[0]);

					_SysInfo2.strDmmCh = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2;
					if (_SysInfo2.strDmmCh == "")
					{
						nProcessStep[nStepIndex] = 47095;
					}
					else
					{
						nProcessStep[nStepIndex] = 47050;
					}
					break;

				//case 47050:
				//	if (_Config.bDmmEtcMode)
				//	{
				//		_KeysiteDmmEtc2.Send("*RST", true);
				//		nProcessStep[nStepIndex] = 47051;
				//	}
				//	else
				//	{
				//		KeysiteDmm2.Send("*RST");
				//		nProcessStep[nStepIndex] = 47051;
				//	}

				//	break;

				case 47050:
					_SysInfo2.strDmmCh = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2;
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2, out _SysInfo2.nDmmCh);
					//AppendLogMsg2($"{_SysInfo2.nDmmBuffIndex.ToString()}", MSG_TYPE.LOG);
					if (_Config.bDmmEtcMode)
					{

						_KeysiteDmmEtc2.Send($"SENSe:FUNCtion \"VOLTage\", (@{_SysInfo2.nDmmCh})", true);
					}
					else
					{
						KeysiteDmm2.Send($"SENSe:FUNCtion \"VOLTage\", (@{_SysInfo2.nDmmCh})");
					}

					//KeysiteDmm2.Send($"SENS:VOLT:APER 0.20,(@101:120,201:210)");
					nProcessStep[nStepIndex]++;
					break;


				case 47051:
					_SysInfo2.strDmmCh = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2;

					if (_Config.bDmmEtcMode)
					{

						_KeysiteDmmEtc2.Send($"ROUTe:CLOSe (@{_SysInfo2.nDmmCh})", true);
					}
					else
					{
						KeysiteDmm2.Send($"ROUTe:CLOSe (@{_SysInfo2.nDmmCh})");
					}

					nProcessStep[nStepIndex] = 47060;
					break;

				case 47053:
					_SysInfo2.strDmmCh = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2;
					//if (_Config.bDmmEtcMode)
					//{

					//	_KeysiteDmmEtc2.Send($"SENS:VOLT:APERture {_ModelInfo2.dbDmmScanSpeed},(@{_SysInfo2.strDmmCh})", true);
					//}
					//else
					//{
					//	KeysiteDmm2.Send($"SENS:VOLT:APERture {_ModelInfo2.dbDmmScanSpeed},(@{_SysInfo2.strDmmCh})");
					//}


					nProcessStep[nStepIndex] = 47060;
					break;


				case 47060:

					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc2.Send($"READ?", true);
					}
					else
					{
						KeysiteDmm2.Send($"READ?");
					}
					//KeysiteDmm2.Send($"MEAS:VOLT:DC? (@{_SysInfo2.strDmmCh})");
					nProcessStep[nStepIndex] = 47065;
					break;

				case 47065:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc2.IsReadData())
						{
							nProcessStep[nStepIndex] = 47066;
						}
					}
					else
					{
						if (KeysiteDmm2.IsReadData())
						{
							nProcessStep[nStepIndex] = 47066;
						}
					}
					break;

				case 47066:
					if (_Config.bDmmEtcMode)
					{
						_SysInfo2.strDmmReadData = _KeysiteDmmEtc2.strReadMessage;
					}
					else
					{
						_SysInfo2.strDmmReadData = KeysiteDmm2.GetReadData();
					}
					double.TryParse(_SysInfo2.strDmmReadData, out _SysInfo2.dbDmmReadDataBuff[1]);

					_SysInfo2.strDmmCh = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3;

					if (_SysInfo2.strDmmCh == "")
					{
						nProcessStep[nStepIndex] = 47095;
					}
					else
					{
						nProcessStep[nStepIndex] = 47070;
					}
					break;

				//case 47070:
				//	if (_Config.bDmmEtcMode)
				//	{
				//		_KeysiteDmmEtc2.Send("*RST", true);
				//		nProcessStep[nStepIndex] = 47051;
				//	}
				//	else
				//	{
				//		KeysiteDmm2.Send("*RST");
				//		nProcessStep[nStepIndex] = 47051;
				//	}

				//	break;

				case 47070:
					_SysInfo2.strDmmCh = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3;
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3, out _SysInfo2.nDmmCh);
					//AppendLogMsg2($"{_SysInfo2.nDmmBuffIndex.ToString()}", MSG_TYPE.LOG);
					if (_Config.bDmmEtcMode)
					{

						_KeysiteDmmEtc2.Send($"SENSe:FUNCtion \"VOLTage\", (@{_SysInfo2.nDmmCh})", true);
					}
					else
					{
						KeysiteDmm2.Send($"SENSe:FUNCtion \"VOLTage\", (@{_SysInfo2.nDmmCh})");
					}

					//KeysiteDmm2.Send($"SENS:VOLT:APER 0.20,(@101:120,201:210)");
					nProcessStep[nStepIndex]++;
					break;


				case 47071:
					_SysInfo2.strDmmCh = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3;

					if (_Config.bDmmEtcMode)
					{

						_KeysiteDmmEtc2.Send($"ROUTe:CLOSe (@{_SysInfo2.nDmmCh})", true);
					}
					else
					{
						KeysiteDmm2.Send($"ROUTe:CLOSe (@{_SysInfo2.nDmmCh})");
					}

					nProcessStep[nStepIndex] = 47080;
					break;

				case 47072:
					_SysInfo2.strDmmCh = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3;
					//if (_Config.bDmmEtcMode)
					//{

					//	_KeysiteDmmEtc2.Send($"SENS:VOLT:APERture {_ModelInfo2.dbDmmScanSpeed},(@{_SysInfo2.strDmmCh})", true);
					//}
					//else
					//{
					//	KeysiteDmm2.Send($"SENS:VOLT:APERture {_ModelInfo2.dbDmmScanSpeed},(@{_SysInfo2.strDmmCh})");
					//}


					nProcessStep[nStepIndex] = 47080;
					break;


				case 47080:

					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc2.Send($"READ?", true);
					}
					else
					{
						KeysiteDmm2.Send($"READ?");
					}
					//KeysiteDmm2.Send($"MEAS:VOLT:DC? (@{_SysInfo2.strDmmCh})");
					nProcessStep[nStepIndex] = 47085;
					break;

				case 47085:
					if (_Config.bDmmEtcMode)
					{

						if (_KeysiteDmmEtc2.IsReadData())
						{
							nProcessStep[nStepIndex] = 47086;
						}
					}
					else
					{
						if (KeysiteDmm2.IsReadData())
						{
							nProcessStep[nStepIndex] = 47086;
						}
					}
					break;

				case 47086:
					if (_Config.bDmmEtcMode)
					{
						_SysInfo2.strDmmReadData = _KeysiteDmmEtc2.strReadMessage;
					}
					else
					{
						_SysInfo2.strDmmReadData = KeysiteDmm2.GetReadData();
					}
					double.TryParse(_SysInfo2.strDmmReadData, out _SysInfo2.dbDmmReadDataBuff[2]);
					nProcessStep[nStepIndex] = 47095;

					break;

				case 47095:
					TestResultSet2(_SysInfo2.nMainWorkStep, "", "OK");
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				case 48000:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nBuffIndex);
					double.TryParse(new DataTable().Compute(string.Format($"{_SysInfo2.dbDmmReadDataBuff[_SysInfo2.nBuffIndex]}{_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2}"), null).ToString(), out _SysInfo2.dbCalcData);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3, out _SysInfo2.nDispLen);

					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMin, out _SysInfo2.dbSpecMin);
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMax, out _SysInfo2.dbSpecMax);


					_SysInfo2.dbRMSCommReadData = (_SysInfo2.dbCalcData / _ModelInfo2.dbResistance) * 1000;

					if (_SysInfo2.dbRMSCommReadData > _SysInfo2.dbSpecMax || _SysInfo2.dbRMSCommReadData < _SysInfo2.dbSpecMin)
					{
						TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbRMSCommReadData.ToString("F" + _SysInfo2.nDispLen.ToString()), "NG");
						_SysInfo2.bTestNG = true;
					}
					else
					{
						TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbRMSCommReadData.ToString("F" + _SysInfo2.nDispLen.ToString()), "OK");
					}
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;


				case 49000:
					_SysInfo2.dbCalcData = 0.0;
					_SysInfo2.dbCommReadData = 0.0;
					nProcessStep[nStepIndex] = 49005;
					break;

				case 49005:
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc2.Send("*RST", true);
						nProcessStep[nStepIndex] = 49010;
					}
					else
					{
						KeysiteDmm2.Send("*RST");
						nProcessStep[nStepIndex] = 49010;
					}

					break;

					// 1번 채널 Power Supply 설정
				case 49010:
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc2.Send("*IDN?", true);
						nProcessStep[nStepIndex]++;
					}
					else
					{
						KeysiteDmm2.Send("*IDN?");
						nProcessStep[nStepIndex]++;
					}
					break;

				case 49011:
					_SysInfo2.bFirstCheck = true;
					if (_Config.bDmmEtcMode)
					{
						if (_KeysiteDmmEtc2.IsReadData())
						{
							//tMainTimer[nStepIndex].Start(200);
							nProcessStep[nStepIndex] = 49020;
						}
					}
					else
					{
						if (KeysiteDmm2.IsReadData())
						{
							//tMainTimer[nStepIndex].Start(200);
							nProcessStep[nStepIndex] = 49020;
						}
					}
					break;

				case 49020:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nDmmCh);
					if (_Config.bDmmEtcMode)
					{
						//if (_SysInfo2.nDmmCh == 1)
						//{
						//	_KeysiteDmmEtc2.Send($"CONF:CURR:DC (@122)",true);
						//}
						//else
						//{
						_KeysiteDmmEtc2.Send($"SENSe:FUNCtion \"CURRent\", (@{_SysInfo2.nDmmCh})", true);
						//}
					}
					else
					{

						KeysiteDmm2.Send($"SENSe:FUNCtion \"CURRent\", (@{_SysInfo2.nDmmCh})");

					}

					nProcessStep[nStepIndex]++;
					break;

				case 49021:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nDmmCh);
					if (_Config.bDmmEtcMode)
					{
						//if (_SysInfo2.nDmmCh == 1)
						//{
						//	_KeysiteDmmEtc2.Send($"ROUT:SCAN (@{_SysInfo2.nDmmCh})",true);
						//}
						//else
						//{
						_KeysiteDmmEtc2.Send($"ROUTe:CLOSe (@{_SysInfo2.nDmmCh})", true);
						AppendDebugMsg($"Curr A #2  검사 시작", "CURR");
						//}
					}
					else
					{
						//if (_SysInfo2.nDmmCh == 1)
						//{
						//	KeysiteDmm2.Send($"ROUT:SCAN (@{_SysInfo2.nDmmCh})");
						//}
						//else
						//{
						KeysiteDmm2.Send($"ROUTe:CLOSe (@{_SysInfo2.nDmmCh})");
						AppendDebugMsg($"Curr A #2 검사 시작", "CURR");
						//}
					}


					nProcessStep[nStepIndex]++;
					break;

				case 49022:

					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nDmmCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3, out _SysInfo2.nDmmTime);

					//if(_SysInfo2.nDmmTime == 1)
					//{
					//	nProcessStep[nStepIndex] = 43023;
					//}
					//else
					//{
					//if (_Config.bDmmEtcMode)
					//{
					//	//if (_SysInfo2.nDmmCh == 1)
					//	//{
					//	//	_KeysiteDmmEtc2.Send($"SENS:CURR:APER {_ModelInfo2.dbDmmAScanSpeed},(@{_SysInfo2.nDmmCh})", true);
					//	//}
					//	//else
					//	//{
					//	_KeysiteDmmEtc2.Send($"SENS:CURR:APER {_ModelInfo2.dbDmmMScanSpeed},(@{_SysInfo2.nDmmCh})", true);
					//	//}
					//}
					//else
					//{
					//	//if (_SysInfo2.nDmmCh == 1)
					//	//{
					//	//	KeysiteDmm2.Send($"SENS:CURR:APER {_ModelInfo2.dbDmmAScanSpeed},(@{_SysInfo2.nDmmCh})");
					//	//}
					//	//else
					//	//{
					//	KeysiteDmm2.Send($"SENS:CURR:APER {_ModelInfo2.dbDmmMScanSpeed},(@{_SysInfo2.nDmmCh})");
					//	//}
					//}
					//tMainTimer[nStepIndex].Start(3000);
					nProcessStep[nStepIndex] = 49023;

					//}
					break;


				case 49023:
					//if (!tMainTimer[nStepIndex].Verify()) { break; }
					if (_Config.bDmmEtcMode)
					{
						_KeysiteDmmEtc2.Send($"READ?", true);
					}
					else
					{
						KeysiteDmm2.Send($"READ?");
					}

					//KeysiteDmm2.Send($"MEAS:VOLT:DC? (@{_SysInfo2.strDmmCh})");
					nProcessStep[nStepIndex] = 49024;
					break;

				case 49024:


					if (_Config.bDmmEtcMode)
					{
						if (_KeysiteDmmEtc2.IsReadData())
						{
							nProcessStep[nStepIndex] = 49025;
							if (_SysInfo2.bFirstCheck)
							{
								tMainTimer[nStepIndex].Start(1000);
								_SysInfo2.bFirstCheck = false;
							}
						}
					}
					else
					{
						if (KeysiteDmm2.IsReadData())
						{
							nProcessStep[nStepIndex] = 49025;
							if (_SysInfo2.bFirstCheck)
							{
								tMainTimer[nStepIndex].Start(1000);
								_SysInfo2.bFirstCheck = false;
							}
						}
					}
					break;

				case 49025:

					if (_Config.bDmmEtcMode)
					{
						double.TryParse(_KeysiteDmmEtc2.strReadMessage, out _SysInfo2.dbCommReadData);
						AppendDebugMsg($"Curr AVG #2 {_SysInfo2.dbCommReadData}", "CURR");
						cellT2.Add(_SysInfo2.dbCommReadData);
						nProcessStep[nStepIndex] = 49026;
					}
					else
					{
						double.TryParse(KeysiteDmm2.GetReadData(), out _SysInfo2.dbCommReadData);
						AppendDebugMsg($"Curr AVG #2 {_SysInfo2.dbCommReadData}", "CURR");
						cellT2.Add(_SysInfo2.dbCommReadData);
						nProcessStep[nStepIndex] = 49026;

					}

					break;

				case 49026:
					if (!tMainTimer[nStepIndex].Verify())
					{

						nProcessStep[nStepIndex] = 49023;

					}
					else
					{
						_SysInfo2.dbCommReadMinData = cellT2.Average();
						nProcessStep[nStepIndex] = 49040;

					}
					break;


				case 49040:
					//theApp.AppendLogMsg2(KeysiteDmm2.GetReadData(), MSG_TYPE.INFO);
					//if (_Config.bDmmEtcMode)
					//{
					//	double.TryParse(_KeysiteDmmEtc2.strReadMessage, out _SysInfo2.dbCommReadData);
					//}
					//else
					//{
					//	double.TryParse(KeysiteDmm2.GetReadData(), out _SysInfo2.dbCommReadData);
					//}

					//theApp.AppendLogMsg2(_SysInfo2.dbCommReadData.ToString(), MSG_TYPE.INFO);
					//int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue3, out _SysInfo2.nBuffIndex);
					double.TryParse(new DataTable().Compute(string.Format($"{_SysInfo2.dbCommReadMinData}{_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2}"), null).ToString(), out _SysInfo2.dbCalcData);
					//theApp.AppendLogMsg2(_SysInfo2.dbCalcData.ToString(), MSG_TYPE.INFO);
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMin, out _SysInfo2.dbSpecMin);
					double.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMax, out _SysInfo2.dbSpecMax);

					// 0은 양쪽다 비교 , 1은 Min만 비교, 2는 Max만 비교
					//if (_SysInfo2.nBuffIndex == 0)
					//{
					if (_SysInfo2.dbCalcData > _SysInfo2.dbSpecMax || _SysInfo2.dbCalcData < _SysInfo2.dbSpecMin)
					{
						if (_SysInfo2.nCurrNGRetryCount > 5)
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "NG");
							_SysInfo2.bTestNG = true;
						}
						else
						{
							_SysInfo2.nCurrNGRetryCount++;
							AppendDebugMsg($"Curr NG {_SysInfo2.dbCalcData}", "CURR");
							nProcessStep[nStepIndex] = 49000;
							break;
						}


					}
					else
					{

						TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "OK");
					}
					//}
					//else if (_SysInfo2.nBuffIndex == 1)
					//{
					//	if (_SysInfo2.dbCalcData < _SysInfo2.dbSpecMin)
					//	{
					//		TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "NG");
					//	}
					//	else
					//	{
					//		TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "OK");
					//	}
					//}
					//else if (_SysInfo2.nBuffIndex == 2)
					//{
					//	if (_SysInfo2.dbCalcData > _SysInfo2.dbSpecMax)
					//	{
					//		TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "NG");
					//	}
					//	else
					//	{
					//		TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbCalcData.ToString("F4"), "OK");
					//	}
					//}

					nProcessStep[nStepIndex] = 49050;
					break;

				case 49050:
					_SysInfo2.nCurrNGRetryCount = 0;
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				case 50000:
					bool bMidTestResult = true;
					for (int i = 0; i < _TestData2.Count; i++)
					{
						if (_TestData2[i].strResult == "NG")
						{
							bMidTestResult = false;
						}
					}

					if (bMidTestResult)
					{
						nProcessStep[nStepIndex] = 50001;
					}
					else
					{
						nProcessStep[nStepIndex] = 80000;
					}

					break;

				case 50001:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue2, out _SysInfo2.nTipMaxCount);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nSetNutSch);
					_SysInfo2.strTitleName = _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strTestName;
					_SysInfo2.bTiteIngStart = true;
					_SysInfo2.bNutRetry = false;
					_SysInfo2.bNutNext = false;
					//_SysInfo2.nVoltCount++;
					ShowUserNutMessege2();
					nProcessStep[nStepIndex] = 50020;
					break;

				case 50020:
					if (_SysInfo2.bTiteOk)
					{
					
						HideUserNutMessege2();
						_SysInfo2.bNutRetryCheckOK = false;
						_SysInfo2.bTiteIngStart = false;
						SetNutRunnerSch2(50);
						//_SysInfo2.dbNutData = _Nutrunner2.dbTorqueData * 0.01;
						//TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbNutData.ToString("F2"), "OK");
						//_SysInfo2.bTiteIngStart = false;
						//SetNutRunnerSch2(50);   // 너트러너 스케줄 설정
						//_SysInfo2.bTiteOk = false;
						//_SysInfo2.nMainWorkStep++;
					
						nProcessStep[nStepIndex] = 50050;
					}
					break;

				case 50050:
					ShowNutRetryMessege2();
					nProcessStep[nStepIndex] = 50055;
					break;

				case 50055:
					if(_SysInfo2.bNutRetryCheckOK)
					{
						_SysInfo2.bNutRetryCheckOK = false;
						nProcessStep[nStepIndex] = 50060;
					}
					else if(GetDIOPort(DI.START_SW3) && !GetDIOPort(DI.START_SW4))
					{
						HideNutRetryMessege2();
						_SysInfo2.bNutNext = true;
						_SysInfo2.bNutRetry = false;
						nProcessStep[nStepIndex] = 50060;
					}
					else if (!GetDIOPort(DI.START_SW3) && GetDIOPort(DI.START_SW4))
					{
						HideNutRetryMessege2();
						_SysInfo2.bNutNext = false;
						_SysInfo2.bNutRetry = true;
						nProcessStep[nStepIndex] = 50060;
					}
					break;

				case 50060:

						if (_SysInfo2.bNutNext && !_SysInfo2.bNutRetry)
						{
							_SysInfo2.dbNutData = _Nutrunner2.dbTorqueData * 0.01;
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbNutData.ToString("F2"), "OK");
							_SysInfo2.bTiteIngStart = false;
							SetNutRunnerSch2(50);   // 너트러너 스케줄 설정
							_SysInfo2.bTiteOk = false;
							_SysInfo2.nMainWorkStep++;
							nProcessStep[nStepIndex] = 3000;
						}
						else if (!_SysInfo2.bNutNext && _SysInfo2.bNutRetry)
						{
							//_SysInfo2.nVoltCount --;
							nProcessStep[nStepIndex] = 3000;
						}
						//else if (GetDIOPort(DI.START_SW3))
						//{
						//	_SysInfo2.dbNutData = _Nutrunner2.dbTorqueData * 0.01;
						//	TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.dbNutData.ToString("F2"), "OK");
						//	_SysInfo2.bTiteIngStart = false;
						//	SetNutRunnerSch2(50);   // 너트러너 스케줄 설정
						//	_SysInfo2.bTiteOk = false;
						//	_SysInfo2.nMainWorkStep++;
						//	nProcessStep[nStepIndex] = 3000;
						//}
						//else if (GetDIOPort(DI.START_SW4))
						//{
						//	//_SysInfo2.nVoltCount --;
						//	nProcessStep[nStepIndex] = 3000;
						//}
					
						
					break;

				case 51000:
					_SysInfo2._FileNameResult = CyclonFileName_RESULT2.READY;
					_CyStatus2 = CYCLON_STATUS2.READY;
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strValue1, out _SysInfo2.nCyclonHandle);
					
					CyclonReadFirWareName2(_SysInfo2.nCyclonHandle, _ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep].strSpecMin);
					nProcessStep[nStepIndex] = 51005;
					break;

				case 51005:

					if (_SysInfo2._FileNameResult != CyclonFileName_RESULT2.READY)
					{
						if (_SysInfo2._FileNameResult == CyclonFileName_RESULT2.OK)
						{
							nProcessStep[nStepIndex] = 51009;
						}
						else
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.strCyclonFileName, "NG");
							_SysInfo2.bTestNG = true;
							_SysInfo2.nMainWorkStep++;
							nProcessStep[nStepIndex] = 3000;
							break;
						}

					}
					break;

				case 51009:
					CyclonInFirWare2(_SysInfo2.nCyclonHandle);
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex] = 51010;
					break;

				case 51010:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					
					if(_CyStatus2 != CYCLON_STATUS2.READY)
					{
						if (_CyStatus2 == CYCLON_STATUS2.OK)
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.strCyclonFileName, "OK");

							nProcessStep[nStepIndex] = 51050;
						}
						else
						{
							TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.strCyclonFileName, "NG");
							_SysInfo2.bTestNG = true;

							nProcessStep[nStepIndex] = 51050;
						}

					}
					
					break;

				case 51050:
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				case 52000:
					if (_ModelInfo2.bUseRbmsTest)
					{
						nProcessStep[nStepIndex] = 52050;
					}
					else
					{
						_SysInfo2._PopupStatus = MAIN_STATUS2.READY;
						_BcdReader2.strReadBarcode = "";
						if (_Config.strLanguage == "ENGLISH")
						{
							_SysInfo2.strBcdPopupContent = "Please scan the PBMS barcode.";
						}
						else
						{
							_SysInfo2.strBcdPopupContent = "PBMS 바코드를 스캔하여 주세요.";
						}
						_BcdReader2.bReadOk = false;
						ShowBcdPopUpWindow2();
						nProcessStep[nStepIndex] = 52005;

					}
						
					break;

				case 52005:
					if(_BcdReader2.bReadOk)
					{
						_BcdReader2.bReadOk = false;
						CloseBcdPopUpWindow2();
						
						
						if (CheckBarcode( _BcdReader2.strReadBarcode,_ModelInfo2.strBarcodSymbol))
						{
							_SysInfo2.strPBMSBcd = _BcdReader2.strReadBarcode;
							_SysInfo2.strDispBarcodeBack = _SysInfo2.strPBMSBcd.Substring(10, 12);
							nProcessStep[nStepIndex] = 52010;
						}
						else
						{
							nProcessStep[nStepIndex] = 52008;
						}
					}
					break;

				case 52008:
					_SysInfo2._PopupStatus = MAIN_STATUS2.NG;
					_SysInfo.nTL_Beep = 3;
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo2.strBcdPopupContent = "Please scan the correct PBMS barcode.";
					}
					else
					{
						_SysInfo2.strBcdPopupContent = "올바른 PBMS 바코드를 스캔하여 주세요.";
					}
					_BcdReader2.bReadOk = false;
					ShowBcdPopUpWindow2();
					nProcessStep[nStepIndex] = 52005;
					break;

				case 52010:
					_SysInfo2._PopupStatus = MAIN_STATUS2.READY;
					_BcdReader2.strReadBarcode = "";
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo2.strBcdPopupContent = "Please scan the Fuse barcode.";
					}
					else
					{
						_SysInfo2.strBcdPopupContent = "Fuse 바코드를 스캔하여 주세요.";
					}
					_BcdReader2.bReadOk = false;
					ShowBcdPopUpWindow2();
					nProcessStep[nStepIndex] = 52015;
					break;

				case 52015:
					if (_BcdReader2.bReadOk)
					{
						_BcdReader2.bReadOk = false;
						CloseBcdPopUpWindow2();

						if (CheckBarcode( _BcdReader2.strReadBarcode,_ModelInfo2.strFuseBarcodSymbol))
						{
							_SysInfo2.strFuseBcd = _BcdReader2.strReadBarcode;
							nProcessStep[nStepIndex] = 52020;
						}
						else
						{
							nProcessStep[nStepIndex] = 52018;
						}
					}
					break;

				case 52018:
					_SysInfo2._PopupStatus = MAIN_STATUS2.NG;
					_BcdReader2.strReadBarcode = "";
					_SysInfo.nTL_Beep = 3;
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo2.strBcdPopupContent = "Please scan the correct Fuse barcode.";
					}
					else
					{
						_SysInfo2.strBcdPopupContent = "올바른 Fuse 바코드를 스캔하여 주세요.";
					}
					_BcdReader2.bReadOk = false;
					ShowBcdPopUpWindow2();
					nProcessStep[nStepIndex] = 52015;
					break;


				case 52020:
					_SysInfo2._PopupStatus = MAIN_STATUS2.READY;
					_BcdReader2.strReadBarcode = "";
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo2.strBcdPopupContent = "Please scan the Case barcode.";
					}
					else
					{
						_SysInfo2.strBcdPopupContent = "Case 바코드를 스캔하여 주세요.";
					}
					_BcdReader2.bReadOk = false;
					ShowBcdPopUpWindow2();
					nProcessStep[nStepIndex] = 52025;
					break;

				case 52025:
					if (_BcdReader2.bReadOk)
					{
						_BcdReader2.bReadOk = false;
						CloseBcdPopUpWindow2();

						if (CheckBarcode( _BcdReader2.strReadBarcode,_ModelInfo2.strCaseBarcodSymbol))
						{
							_SysInfo2.strCaseBcd = _BcdReader2.strReadBarcode;
							nProcessStep[nStepIndex] = 52030;
						}
						else
						{
							nProcessStep[nStepIndex] = 52028;
						}
					}
					break;

				case 52028:
					_SysInfo2._PopupStatus = MAIN_STATUS2.NG;
					_BcdReader2.strReadBarcode = "";
					_SysInfo.nTL_Beep = 3;
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo2.strBcdPopupContent = "Please scan the correct Case barcode.";
					}
					else
					{
						_SysInfo2.strBcdPopupContent = "올바른 Case 바코드를 스캔하여 주세요.";
					}
					_BcdReader2.bReadOk = false;
					ShowBcdPopUpWindow2();
					nProcessStep[nStepIndex] = 52025;
					break;

				case 52030:
					//_LotCount2.nLotCount++;
					_BarcodePrint2.nBCDsize = theApp._ModelInfo2.nBCDsize;
					_BarcodePrint2.nBCDStringHeight = theApp._ModelInfo2.nBCDStringHeight;
					_BarcodePrint2.nBCDStringWidth = theApp._ModelInfo2.nBCDStringWidth;
					_BarcodePrint2.nBCDbcdOffsetX = theApp._ModelInfo2.nBCDbcdOffsetX;
					_BarcodePrint2.nBCDbcdOffsetY = theApp._ModelInfo2.nBCDbcdOffsetY;
					_BarcodePrint2.nBCDtextOffsetX = theApp._ModelInfo2.nBCDtextOffsetX;
					_BarcodePrint2.nBCDtextOffsetY = theApp._ModelInfo2.nBCDtextOffsetY;
					_BarcodePrint2.strModelInfo = theApp._ModelInfo2.strComment1;
					_BarcodePrint2.strPrintBCD = _SysInfo2.strDispBarcodeBack;
					_BarcodePrint2.bManualMode = false;
					_BarcodePrint2.bPrintStart = true;
					//SaveModelProductCount2(theApp._LotCount2, theApp._ModelInfo2.strModelName);
					SaveBarcodeInfo2();
					nProcessStep[nStepIndex] = 52033;
					break;

				case 52033:
					_SysInfo2._PopupStatus = MAIN_STATUS2.READY;
					_BcdReader2.strReadBarcode = "";
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo2.strBcdPopupContent = "Please attach the Support Bracket barcode and scan it.";
					}
					else
					{
						_SysInfo2.strBcdPopupContent = "Support Bracket 바코드를 부착 후 스캔하여 주세요.";
					}
					_BcdReader2.bReadOk = false;
					ShowBcdPopUpWindow2();
					nProcessStep[nStepIndex]++;
					break;

				case 52034:
					if (_BcdReader2.bReadOk)
					{
						_BcdReader2.bReadOk = false;
						CloseBcdPopUpWindow2();

						if (_BcdReader2.strReadBarcode == (_ModelInfo2.strComment1 + _SysInfo2.strDispBarcodeBack))
						{

							nProcessStep[nStepIndex] = 52040;
						}
						else
						{
							nProcessStep[nStepIndex] = 52035;
						}
					}
					break;


				case 52035:
					_SysInfo2._PopupStatus = MAIN_STATUS2.NG;
					_BcdReader2.strReadBarcode = "";
					_SysInfo2.nTL_Beep = 3;
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo2.strBcdPopupContent = "Please scan the correct Support Bracket barcode.";
					}
					else
					{
						_SysInfo2.strBcdPopupContent = "올바른 Support Bracket 바코드를 스캔하여 주세요.";
					}
					_BcdReader2.bReadOk = false;
					ShowBcdPopUpWindow2();
					nProcessStep[nStepIndex] = 52034;
					break;

				case 52040:
					TestResultSet2(_SysInfo2.nMainWorkStep, "OK", "OK");
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;

				case 52050:
					_SysInfo2.strDispBarcodeFront = _SysInfo2.strDispBarcode.Substring(0, 10);
					_SysInfo2.strDispBarcodeBack = _SysInfo2.strDispBarcode.Substring(10, 12);
					nProcessStep[nStepIndex] = 52055;
					break;

					
				case 52055:
					theApp._BarcodePrint2.nBCDsize = theApp._ModelInfo2.nBCDsize;
					theApp._BarcodePrint2.nBCDStringHeight = theApp._ModelInfo2.nBCDStringHeight;
					theApp._BarcodePrint2.nBCDStringWidth = theApp._ModelInfo2.nBCDStringWidth;
					theApp._BarcodePrint2.nBCDbcdOffsetX = theApp._ModelInfo2.nBCDbcdOffsetX;
					theApp._BarcodePrint2.nBCDbcdOffsetY = theApp._ModelInfo2.nBCDbcdOffsetY;
					theApp._BarcodePrint2.nBCDtextOffsetX = theApp._ModelInfo2.nBCDtextOffsetX;
					theApp._BarcodePrint2.nBCDtextOffsetY = theApp._ModelInfo2.nBCDtextOffsetY;
					theApp._BarcodePrint2.strModelInfo = _SysInfo2.strDispBarcodeFront;
					theApp._BarcodePrint2.strPrintBCD = _SysInfo2.strDispBarcodeBack;
					theApp._BarcodePrint2.bManualMode = false;
					theApp._BarcodePrint2.bPrintStart = true;
					nProcessStep[nStepIndex] = 52060;
					break;

				case 52060:
					_SysInfo2._PopupStatus = MAIN_STATUS2.READY;
					_BcdReader2.strReadBarcode = "";
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo2.strBcdPopupContent = "Please attach the RBMS barcode and scan it.";

					}
					else
					{
						_SysInfo2.strBcdPopupContent = "RBMS 바코드를 부착 후 스캔하여 주세요.";
					}
					_BcdReader2.bReadOk = false;
					ShowBcdPopUpWindow2();
					nProcessStep[nStepIndex]++;
					break;

				case 52061:
					if (_BcdReader2.bReadOk)
					{
						_BcdReader2.bReadOk = false;
						CloseBcdPopUpWindow2();

						if (CheckBarcode(_BcdReader2.strReadBarcode, _ModelInfo2.strRBMSBarcodSymbol))
						{

							nProcessStep[nStepIndex] = 52067;
						}
						else
						{
							nProcessStep[nStepIndex] = 52065;
						}
					}
					break;


				case 52065:
					_SysInfo2._PopupStatus = MAIN_STATUS2.NG;
					_BcdReader2.strReadBarcode = "";
					_SysInfo.nTL_Beep = 3;
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo2.strBcdPopupContent = "Please scan the correct RBMS barcode.";
					}
					else
					{
						_SysInfo2.strBcdPopupContent = "올바른 RBMS 바코드를 스캔하여 주세요.";
					}
					_BcdReader2.bReadOk = false;
					ShowBcdPopUpWindow2();
					nProcessStep[nStepIndex] = 52061;
					break;

				case 52067:
					_SysInfo2._PopupStatus = MAIN_STATUS2.READY;
					_BcdReader2.strReadBarcode = "";
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo2.strBcdPopupContent = "Please scan the Mac Address barcode.";

					}
					else
					{
						_SysInfo2.strBcdPopupContent = "Mac Adress 바코드를 스캔하여 주세요.";
					}
					_BcdReader2.bReadOk = false;
					ShowBcdPopUpWindow2();
					nProcessStep[nStepIndex]++;
					break;

				case 52068:
					if (_BcdReader2.bReadOk)
					{
						_BcdReader2.bReadOk = false;
						CloseBcdPopUpWindow2();

						if (_BcdReader2.strReadBarcode == _SysInfo2.strMacAdress)
						{

							nProcessStep[nStepIndex] = 52070;
						}
						else
						{
							nProcessStep[nStepIndex] = 52069;
						}
					}
					break;


				case 52069:
					_SysInfo2._PopupStatus = MAIN_STATUS2.NG;
					_BcdReader2.strReadBarcode = "";
					_SysInfo2.nTL_Beep = 3;
					if (_Config.strLanguage == "ENGLISH")
					{
						_SysInfo2.strBcdPopupContent = "Please scan the correct Mac Adress barcode.";
					}
					else
					{
						_SysInfo2.strBcdPopupContent = "올바른 Mac Adress 바코드를 스캔하여 주세요.";
					}
					_BcdReader2.bReadOk = false;
					ShowBcdPopUpWindow2();
					nProcessStep[nStepIndex] = 52068;
					break;

				case 52070:
					TestResultSet2(_SysInfo2.nMainWorkStep, $"{_SysInfo2.strMacAdress}", "OK");
					_SysInfo2.nMainWorkStep++;
					nProcessStep[nStepIndex] = 3000;
					break;











				// 검사 종료 스텝
				case 80000:
					//for (int i = 0; i < 32; i++)
					//{
					//	SetDIOPort((DO)i, false);
					//}
					ClosePopUpWindow2();
					HideUserStartMessege2();
					PowerSupply[2].SendData("OUTP OFF");
					PowerSupply[3].SendData("OUTP OFF");
					_CellSimulator3.Send("SIM:OUTP OFF", true);
					_CellSimulator4.Send("SIM:OUTP OFF", true);
					_SysInfo2.bSubEolStart = false;
					_SysInfo2.bTiteIngStart = false;
					SetNutRunnerSch2(50);
					theApp.nProcessStep[(int)PROC_LIST.SUB_EOL2] = 30000;
					nProcessStep[nStepIndex] = 85000;
			
					break;

				// 검사 정상종료 스텝
				case 85000:
					bool bTestResult = true;
					for (int i = 0; i < _TestData2.Count; i++)
					{
						if (_TestData2[i].strResult == "NG")
						{
							bTestResult = false;
						}
					}


					if (_SysInfo2.bEMGStop)
					{
						_SysInfo2.eMainStatus = MAIN_STATUS2.EMG_STOP;
						_SysInfo.nTL_Beep = 5;
						_LotCount2.nNGCount++;
						SaveModelProductCount2(_LotCount2, _ModelInfo2.strModelName);

						_LotCount2.nTotalCount++;
						SaveProductCount2();
						_SysInfo2.strTotalResult = "USER_STOP";
					}
					else
					{
						if (bTestResult)
						{
							_SysInfo2.eMainStatus = MAIN_STATUS2.OK;
							_SysInfo.nTL_Beep = 2;
							_LotCount2.nOkCount++;
							SaveModelProductCount2(_LotCount2, _ModelInfo2.strModelName);
							_LotCount2.nTotalCount++;
							SaveProductCount2();
							_SysInfo2.strTotalResult = "OK";
							//_LotCount.nLotCount++;
						}
						else
						{
							_SysInfo2.eMainStatus = MAIN_STATUS2.NG;
							_SysInfo.nTL_Beep = 5;
							_LotCount2.nNGCount++;
							SaveModelProductCount2(_LotCount2, _ModelInfo2.strModelName);
							_LotCount2.nTotalCount++;
							SaveProductCount2();
							_SysInfo2.strTotalResult = "NG";
							//_LotCount.nLotCount++;
						}
					}

					if (_Config.bUseMasterCheck)
					{
						// OK 샘플 검사 작업시 
						if (_SysInfo2.bOkMasterSampleTestIng)
						{
							if (_SysInfo2.strTotalResult == "OK")
							{
								_MasterTestInfo.bMasterOkSampleTestFinish = true;
								_MasterTestInfo.dtMasterOkSampleTestTime = DateTime.Now;
								SaveMasterTestInfo(_MasterTestInfo, _ModelInfo2.strModelName);
								_SysInfo2.eMainStatus = MAIN_STATUS2.OK_MASTER_OK;
							}
							else
							{
								_SysInfo2.eMainStatus = MAIN_STATUS2.OK_MASTER_NG;
							}
						}
						else if (_SysInfo2.bNgMasterSampleTestIng)
						{
							if (_SysInfo2.strTotalResult == "NG")
							{

								_MasterTestInfo.bMasterNgSampleTestFinish = true;
								_MasterTestInfo.dtMasterNgSampleTestTime = DateTime.Now;
								SaveMasterTestInfo(_MasterTestInfo, _ModelInfo2.strModelName);
								_SysInfo2.eMainStatus = MAIN_STATUS2.NG_MASTER_OK;
							}
							else
							{
								_SysInfo2.eMainStatus = MAIN_STATUS2.NG_MASTER_NG;
							}
						}
					}




					_SysInfo2.dtTestEndTime = DateTime.Now;

					_tTackTimer2.Stop();
					TestTotalResultView2(_SysInfo2.strDispBarcode, _SysInfo2.strTotalResult);
					SaveResultData2();
					SetDIOPort(DO.RY_01 + 2 - 1, false);
					SetDIOPort(DO.RY_01 + 36 - 1, false);
					SetDIOPort(DO.RY_01 + 37 - 1, false);
					SetDIOPort(DO.RY_01 + 38 - 1, false);
					SetDIOPort(DO.RY_01 + 39 - 1, false);
					SetDIOPort(DO.RY_01 + 40 - 1, false);
					SetDIOPort(DO.RY_01 + 41 - 1, false);
					SetDIOPort(DO.RY_01 + 42 - 1, false);
					SetDIOPort(DO.RY_01 + 43 - 1, false);
					SetDIOPort(DO.RY_01 + 44 - 1, false);
					SetDIOPort(DO.RY_01 + 45 - 1, false);
					SetDIOPort(DO.RY_01 + 46 - 1, false);
					SetDIOPort(DO.RY_01 + 47 - 1, false);
					SetDIOPort(DO.RY_01 + 48 - 1, false);
					SetDIOPort(DO.RY_01 + 49 - 1, false);
					SetDIOPort(DO.RY_01 + 50 - 1, false);
					SetDIOPort(DO.RY_01 + 51 - 1, false);
					SetDIOPort(DO.RY_01 + 52 - 1, false);
					SetDIOPort(DO.RY_01 + 53 - 1, false);
					SetDIOPort(DO.RY_01 + 54 - 1, false);
					SetDIOPort(DO.RY_01 + 55 - 1, false);
					SetDIOPort(DO.RY_01 + 56 - 1, false);
					_SysInfo2.bReadMacBcd = false;
					_SysInfo2.bReadMainBcd = false;
					_SysInfo2.nVoltCount = 0;
					nProcessStep[nStepIndex] = 86000;
					break;


				// 데이터 저장 스텝
				case 86000:
					nProcessStep[nStepIndex] = 100000;
					break;



				// 오류스텝
				case 90000:
					break;



				case 100000:
					nProcessStep[nStepIndex] = 0;
					break;
			}


		}

		public static void PROC_MANUAL()
		{

			int nStepIndex = (int)PROC_LIST.MANUAL;

			switch (nProcessStep[nStepIndex])
			{
				case 0:
					break;



				case 1000:
					_CellSimulator1.Send("*IDN?", true);
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex]++;
					break;

				case 1001:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg("파워서플라이 초기화 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 0;
					//}

					if (_CellSimulator1.IsReadData())
					{
						nProcessStep[nStepIndex] = 1010;
					}
					break;

				case 1010:
					_CellSimulator1.Send("SIM:OUTP OFF", true);
					tMainTimer[nStepIndex].Start(500);
					nProcessStep[nStepIndex] = 1016;
					break;


				//case 1015:
				//	if (!tMainTimer[nStepIndex].Verify()) { break; }
				//	_CellSimulator1.Send("SYSTem:FRAME:STATe? 0", true);
				//	tMainTimer[nStepIndex].Start(300);
				//	nProcessStep[nStepIndex]++;
				//	break;

				case 1016:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator1.Send("SIM:CONF:BMS:NUMB 1", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 1018;
					break;

				//case 1017:
				//	if (!tMainTimer[nStepIndex].Verify()) { break; }
				//	_CellSimulator1.Send("SIM:CONF:SAMP:TIME 10", true);
				//	tMainTimer[nStepIndex].Start(300);
				//	nProcessStep[nStepIndex]++;
				//	break;

				case 1018:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator1.Send("SIM:CONF:CELL:NUMB 1,16", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex]++;
					break;

				case 1019:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator1.Send("SIM:CONF:CELL:PARA 1,1,16,1,2", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 1030;
					break;

				case 1030:
					if (!tMainTimer[nStepIndex].Verify()) { break; }

					_CellSimulator1.Send($"SIM:PROG:CELL 1,1,1,16,3.22,1", true);
					tMainTimer[nStepIndex].Start(500);
					nProcessStep[nStepIndex] = 1040;
					break;

				case 1040:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator1.Send("SIM:OUTP ON", true);
					nProcessStep[nStepIndex] = 0;
					break;

				case 1050:
					nProcessStep[nStepIndex]++;
					break;

				case 1051:
					tMainTimer[nStepIndex].Start(500);
					nProcessStep[nStepIndex]++;
					break;

				case 1052:
					if (!tMainTimer[nStepIndex].Verify()) { break; }

					_CellSimulator1.Send($"SIM:PROG:CELL 1,1,1,14,0,1", true);
					tMainTimer[nStepIndex].Start(500);
					nProcessStep[nStepIndex]++;
					break;

				case 1053:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator1.Send("SIM:OUTP ON", true);
					tMainTimer[nStepIndex].Start(1000);
					nProcessStep[nStepIndex]++;
					break;

				case 1054:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator1.Send("SIM:OUTP OFF", true);
					nProcessStep[nStepIndex] = 0;
					break;

				case 2000:
					_CellSimulator2.Send("*IDN?", true);
					tMainTimer[nStepIndex].Start(2000);
					nProcessStep[nStepIndex]++;
					break;

				case 2001:
					//if (tMainTimer[nStepIndex].Verify())
					//{
					//	AppendLogMsg("파워서플라이 초기화 실패", MSG_TYPE.ERROR);
					//	nProcessStep[nStepIndex] = 0;
					//}

					if (_CellSimulator2.IsReadData())
					{
						nProcessStep[nStepIndex] = 2010;
					}
					break;

				case 2010:
					_CellSimulator2.Send("SIM:OUTP OFF", true);
					tMainTimer[nStepIndex].Start(500);
					nProcessStep[nStepIndex] = 2016;
					break;

				//case 2015:
				//	if (!tMainTimer[nStepIndex].Verify()) { break; }
				//	_CellSimulator2.Send("SYSTem:FRAME:STATe? 0", true);
				//	tMainTimer[nStepIndex].Start(300);
				//	nProcessStep[nStepIndex]++;
				//	break;

				case 2016:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator2.Send("SIM:CONF:BMS:NUMB 1", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 2018;
					break;

				//case 2017:
				//	if (!tMainTimer[nStepIndex].Verify()) { break; }
				//	_CellSimulator2.Send("SIM:CONF:SAMP:TIME 10", true);
				//	tMainTimer[nStepIndex].Start(300);
				//	nProcessStep[nStepIndex]++;
				//	break;

				case 2018:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator2.Send("SIM:CONF:CELL:NUMB 1,14", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex]++;
					break;

				case 2019:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator2.Send("SIM:CONF:CELL:PARA 1,1,14,1,2", true);
					tMainTimer[nStepIndex].Start(300);
					nProcessStep[nStepIndex] = 2030;
					break;

				case 2030:
					if (!tMainTimer[nStepIndex].Verify()) { break; }

					_CellSimulator2.Send($"SIM:PROG:CELL 1,1,1,14,3.22,1", true);
					tMainTimer[nStepIndex].Start(500);
					nProcessStep[nStepIndex] = 2040;
					break;

				case 2040:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator2.Send("SIM:OUTP ON", true);
					nProcessStep[nStepIndex] = 0;
					break;

				case 2050:
					nProcessStep[nStepIndex]++;
					break;

				case 2051:
					tMainTimer[nStepIndex].Start(500);
					nProcessStep[nStepIndex]++;
					break;

				case 2052:
					if (!tMainTimer[nStepIndex].Verify()) { break; }

					_CellSimulator2.Send($"SIM:PROG:CELL 1,1,1,14,0,1", true);
					tMainTimer[nStepIndex].Start(500);
					nProcessStep[nStepIndex]++;
					break;

				case 2053:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator2.Send("SIM:OUTP ON", true);
					tMainTimer[nStepIndex].Start(1000);
					nProcessStep[nStepIndex]++;
					break;

				case 2054:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					_CellSimulator2.Send("SIM:OUTP OFF", true);
					nProcessStep[nStepIndex] = 0;
					break;









			}
		}

		public static void SUB_EOL()
		{

			int nStepIndex = (int)PROC_LIST.SUB_EOL;

			switch (nProcessStep[nStepIndex])
			{
				case 0:
					break;

				case 10000:

					nProcessStep[nStepIndex] = 11000;


					break;

				case 11000:
					if (_SysInfo.bSubEolStart)
					{
						nProcessStep[nStepIndex] = 20000;
					}
					else
					{
						nProcessStep[nStepIndex] = 30000;
					}
					break;

				case 20000:
					if (_SysInfo.nRepeatWorkStep >= _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo.Count)
					{
						if (_SysInfo.bEolNg)
						{
							TestResultSet(_SysInfo.nSubMainWorkStep, "", "NG");
						}
						else
						{
							TestResultSet(_SysInfo.nSubMainWorkStep, "", "OK");
						}
						_SysInfo.bEolReadData = false;
						nProcessStep[nStepIndex] = 29000;
					}
					else
					{
						_SysInfo.bEolReadData = false;
						nProcessStep[nStepIndex] = 21000;
					}
					break;

				case 20050:
					if (_SysInfo.bSubEolStart)
					{
						nProcessStep[nStepIndex] = 20060;
					}
					else
					{
						nProcessStep[nStepIndex] = 30000;
					}
					break;


				case 20060:
					if (_SysInfo.nRepeatWorkStep >= _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo.Count)
					{

						_SysInfo.bEolReadData = false;
						nProcessStep[nStepIndex] = 30000;
					}
					else
					{
						_SysInfo.bEolReadData = false;
						nProcessStep[nStepIndex] = 21000;
					}
					break;


				case 21000:
					if (_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].nTestType == 0)
					{
						// Modbus Can Write
						nProcessStep[nStepIndex] = 21100;
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].nTestType == 1)
					{
						// Modbus Can Read(Comp)
						nProcessStep[nStepIndex] = 21200;
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].nTestType == 2)
					{
						// Modbus Can Read(Buff)
						nProcessStep[nStepIndex] = 21300;
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].nTestType == 3)
					{
						// Dealy
						nProcessStep[nStepIndex] = 21400;
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].nTestType == 4)
					{
						// Modbus Can Write(Multi)
						nProcessStep[nStepIndex] = 21500;
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].nTestType == 5)
					{
						// Can Write
						nProcessStep[nStepIndex] = 21600;
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].nTestType == 6)
					{
						// Can Read
						nProcessStep[nStepIndex] = 21700;
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].nTestType == 7)
					{
						// TCP Write
						nProcessStep[nStepIndex] = 21800;
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].nTestType == 8)
					{
						// TCP Read ( Comp )
						nProcessStep[nStepIndex] = 21900;
					}
					else if (_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].nTestType == 9)
					{
						// TCP Read ( Buff )
						nProcessStep[nStepIndex] = 22000;
					}
					break;



				// Modbus Can Write
				case 21100:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].nID.ToString(), out _SysInfo.nCanCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue1.ToString(), out _SysInfo.nCanAddr);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue2.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nCanData);
					//tMainTimer[nStepIndex].Start(1000);
					SendWriteCommand(_SysInfo.nCanCh, _SysInfo.nCanAddr, _SysInfo.nCanData, 1);
					nProcessStep[nStepIndex]++;
					break;

				case 21101:
					//if (!tMainTimer[nStepIndex].Verify()) { break; }
					//if (_CanComm[_SysInfo.nCanCh].bReadMessage)
					//{
					//	if (_CanComm[_SysInfo.nCanCh].btReadData[0] == 0x06)
					//	{
					//		//if ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3] == _SysInfo.nCanData)
					//		//{

					//		//}
					//		_SysInfo.nRepeatWorkStep++;
					//		nProcessStep[nStepIndex] = 20000;
					//	}
					//}
					_SysInfo.nSubEOLWorkStep++;
					nProcessStep[nStepIndex] = 20000;
					break;


				// Modbus Can Read(COMP)
				case 21200:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].nID.ToString(), out _SysInfo.nCanCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue1.ToString(), out _SysInfo.nCanAddr);
					//int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue2.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nCanData);
					_SysInfo.nCanData = GetFuncData2(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue2.ToString());
					SendReadCommand(_SysInfo.nCanCh, _SysInfo.nCanAddr, 1);

					tMainTimer[nStepIndex].Start(5000);
					nProcessStep[nStepIndex]++;
					break;

				case 21201:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet(_SysInfo.nSubMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), "TIME OUT", "NG");
						_SysInfo.bEolNg = true;
						_SysInfo.nRepeatWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}


					if (_CanComm[_SysInfo.nCanCh].bReadMessage)
					{
						if (_CanComm[_SysInfo.nCanCh].btReadData[0] == 0x03)
						{
							if (_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue3 != null && _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue3 != "")
							{
								if (int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue3.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMaskingData))
								{
									_SysInfo.nCompData = ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3]) & _SysInfo.nMaskingData;

									if (_SysInfo.nCompData == _SysInfo.nCanData)
									{
										NgDataSet(_SysInfo.nSubMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3]).ToString("X4") + $"({(_SysInfo.nMaskingData.ToString("X4"))})", "OK");
										//AppendLogMsg($"OK / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
									}
									else
									{
										_SysInfo.bEolNg = true;
										NgDataSet(_SysInfo.nSubMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3]).ToString("X4") + $"({(_SysInfo.nMaskingData.ToString("X4"))})", "NG");
										//AppendLogMsg($"NG / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
									}
								}
								else
								{
									_SysInfo.nCompData = ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3]);
									if (_SysInfo.nCompData == _SysInfo.nCanData)
									{
										NgDataSet(_SysInfo.nSubMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), _SysInfo.nCompData.ToString("X4"), "OK");
										//AppendLogMsg($"OK / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
									}
									else
									{
										_SysInfo.bEolNg = true;
										NgDataSet(_SysInfo.nSubMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), _SysInfo.nCompData.ToString("X4"), "NG");
										//AppendLogMsg($"NG / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
									}
								}
							}
							else
							{
								_SysInfo.nCompData = ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3]);
								if (_SysInfo.nCompData == _SysInfo.nCanData)
								{
									NgDataSet(_SysInfo.nSubMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), _SysInfo.nCompData.ToString("X4"), "OK");
									//AppendLogMsg($"OK / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
								}
								else
								{
									_SysInfo.bEolNg = true;
									NgDataSet(_SysInfo.nSubMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), _SysInfo.nCompData.ToString("X4"), "NG");
									//AppendLogMsg($"NG / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
								}
							}


						}
						else
						{
							_SysInfo.bEolNg = true;
							NgDataSet(_SysInfo.nSubMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), "DATA ERROR", "NG");
						}
						_SysInfo.nRepeatWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;

				case 21300:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].nID.ToString(), out _SysInfo.nCanCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue1.ToString(), out _SysInfo.nCanAddr);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue2.ToString(), out _SysInfo.nBuffIndex);
					tMainTimer[nStepIndex].Start(5000);
					SendReadCommand(_SysInfo.nCanCh, _SysInfo.nCanAddr, 1);
					nProcessStep[nStepIndex]++;
					break;

				case 21301:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet(_SysInfo.nSubMainWorkStep, _SysInfo.nCanAddr.ToString(), "", "TIME OUT", "NG");
						_SysInfo.bEolNg = true;
						_SysInfo.nRepeatWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}

					if (_CanComm[_SysInfo.nCanCh].bReadMessage)
					{

						if (_CanComm[_SysInfo.nCanCh].btReadData[0] == 0x03)
						{

							_SysInfo.nReadDataBuff[_SysInfo.nBuffIndex] = (_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3];
							NgDataSet(_SysInfo.nSubMainWorkStep, _SysInfo.nCanAddr.ToString(), $"BUFFER{_SysInfo.nBuffIndex}", ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3]).ToString("X4"), "");
						}
						_SysInfo.nRepeatWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;


				case 21400:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue1.ToString(), out _SysInfo.nCanAddr);
					tMainTimer[nStepIndex].Start(_SysInfo.nCanAddr);
					nProcessStep[nStepIndex]++;
					break;

				case 21401:
					if (!tMainTimer[nStepIndex].Verify()) { break; }

					_SysInfo.nRepeatWorkStep++;
					nProcessStep[nStepIndex] = 20000;
					break;


				case 21500:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].nID.ToString(), out _SysInfo.nCanCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue1.ToString(), out _SysInfo.nCanAddr);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue2.ToString(), out _SysInfo.nCanMultiCount);
					if (_SysInfo.nCanMultiCount > 0) { _SysInfo.nMultiSendData[0] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue3.ToString()); }
					if (_SysInfo.nCanMultiCount > 1) { _SysInfo.nMultiSendData[1] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue4.ToString()); }
					if (_SysInfo.nCanMultiCount > 2) { _SysInfo.nMultiSendData[2] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue5.ToString()); }
					if (_SysInfo.nCanMultiCount > 3) { _SysInfo.nMultiSendData[3] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue6.ToString()); }
					if (_SysInfo.nCanMultiCount > 4) { _SysInfo.nMultiSendData[4] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue7.ToString()); }
					if (_SysInfo.nCanMultiCount > 5) { _SysInfo.nMultiSendData[5] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue8.ToString()); }
					if (_SysInfo.nCanMultiCount > 6) { _SysInfo.nMultiSendData[6] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue9.ToString()); }
					if (_SysInfo.nCanMultiCount > 7) { _SysInfo.nMultiSendData[7] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue10.ToString()); }
					if (_SysInfo.nCanMultiCount > 8) { _SysInfo.nMultiSendData[8] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue11.ToString()); }
					if (_SysInfo.nCanMultiCount > 9) { _SysInfo.nMultiSendData[9] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue12.ToString()); }
					if (_SysInfo.nCanMultiCount > 10) { _SysInfo.nMultiSendData[10] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue13.ToString()); }
					if (_SysInfo.nCanMultiCount > 11) { _SysInfo.nMultiSendData[11] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue14.ToString()); }
					if (_SysInfo.nCanMultiCount > 12) { _SysInfo.nMultiSendData[12] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue15.ToString()); }
					if (_SysInfo.nCanMultiCount > 13) { _SysInfo.nMultiSendData[13] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue16.ToString()); }
					if (_SysInfo.nCanMultiCount > 14) { _SysInfo.nMultiSendData[14] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue17.ToString()); }
					if (_SysInfo.nCanMultiCount > 15) { _SysInfo.nMultiSendData[15] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue18.ToString()); }
					if (_SysInfo.nCanMultiCount > 16) { _SysInfo.nMultiSendData[16] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue19.ToString()); }
					if (_SysInfo.nCanMultiCount > 17) { _SysInfo.nMultiSendData[17] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue20.ToString()); }
					if (_SysInfo.nCanMultiCount > 18) { _SysInfo.nMultiSendData[18] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue21.ToString()); }
					if (_SysInfo.nCanMultiCount > 19) { _SysInfo.nMultiSendData[19] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue22.ToString()); }
					if (_SysInfo.nCanMultiCount > 20) { _SysInfo.nMultiSendData[20] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue23.ToString()); }
					if (_SysInfo.nCanMultiCount > 21) { _SysInfo.nMultiSendData[21] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue24.ToString()); }
					if (_SysInfo.nCanMultiCount > 22) { _SysInfo.nMultiSendData[22] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue25.ToString()); }
					if (_SysInfo.nCanMultiCount > 23) { _SysInfo.nMultiSendData[23] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue26.ToString()); }
					if (_SysInfo.nCanMultiCount > 24) { _SysInfo.nMultiSendData[24] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue27.ToString()); }
					if (_SysInfo.nCanMultiCount > 25) { _SysInfo.nMultiSendData[25] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue28.ToString()); }
					if (_SysInfo.nCanMultiCount > 26) { _SysInfo.nMultiSendData[26] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue29.ToString()); }
					if (_SysInfo.nCanMultiCount > 27) { _SysInfo.nMultiSendData[27] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue30.ToString()); }
					if (_SysInfo.nCanMultiCount > 28) { _SysInfo.nMultiSendData[28] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue31.ToString()); }
					if (_SysInfo.nCanMultiCount > 29) { _SysInfo.nMultiSendData[29] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue32.ToString()); }
					if (_SysInfo.nCanMultiCount > 30) { _SysInfo.nMultiSendData[30] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue33.ToString()); }
					if (_SysInfo.nCanMultiCount > 31) { _SysInfo.nMultiSendData[31] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue34.ToString()); }
					if (_SysInfo.nCanMultiCount > 32) { _SysInfo.nMultiSendData[32] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue35.ToString()); }
					if (_SysInfo.nCanMultiCount > 33) { _SysInfo.nMultiSendData[33] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue36.ToString()); }
					if (_SysInfo.nCanMultiCount > 34) { _SysInfo.nMultiSendData[34] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue37.ToString()); }
					if (_SysInfo.nCanMultiCount > 35) { _SysInfo.nMultiSendData[35] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue38.ToString()); }
					if (_SysInfo.nCanMultiCount > 36) { _SysInfo.nMultiSendData[36] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue39.ToString()); }
					if (_SysInfo.nCanMultiCount > 37) { _SysInfo.nMultiSendData[37] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue40.ToString()); }
					if (_SysInfo.nCanMultiCount > 38) { _SysInfo.nMultiSendData[38] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue41.ToString()); }
					if (_SysInfo.nCanMultiCount > 39) { _SysInfo.nMultiSendData[39] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue42.ToString()); }
					if (_SysInfo.nCanMultiCount > 40) { _SysInfo.nMultiSendData[40] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue43.ToString()); }
					if (_SysInfo.nCanMultiCount > 41) { _SysInfo.nMultiSendData[41] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue44.ToString()); }
					if (_SysInfo.nCanMultiCount > 42) { _SysInfo.nMultiSendData[42] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue45.ToString()); }
					if (_SysInfo.nCanMultiCount > 43) { _SysInfo.nMultiSendData[43] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue46.ToString()); }
					if (_SysInfo.nCanMultiCount > 44) { _SysInfo.nMultiSendData[44] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue47.ToString()); }
					if (_SysInfo.nCanMultiCount > 45) { _SysInfo.nMultiSendData[45] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue48.ToString()); }
					if (_SysInfo.nCanMultiCount > 46) { _SysInfo.nMultiSendData[46] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue49.ToString()); }
					if (_SysInfo.nCanMultiCount > 47) { _SysInfo.nMultiSendData[47] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue50.ToString()); }
					if (_SysInfo.nCanMultiCount > 48) { _SysInfo.nMultiSendData[48] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue51.ToString()); }
					if (_SysInfo.nCanMultiCount > 49) { _SysInfo.nMultiSendData[49] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue52.ToString()); }
					if (_SysInfo.nCanMultiCount > 50) { _SysInfo.nMultiSendData[50] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue53.ToString()); }
					if (_SysInfo.nCanMultiCount > 51) { _SysInfo.nMultiSendData[51] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue54.ToString()); }
					if (_SysInfo.nCanMultiCount > 52) { _SysInfo.nMultiSendData[52] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue55.ToString()); }
					if (_SysInfo.nCanMultiCount > 53) { _SysInfo.nMultiSendData[53] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue56.ToString()); }
					if (_SysInfo.nCanMultiCount > 54) { _SysInfo.nMultiSendData[54] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue57.ToString()); }
					if (_SysInfo.nCanMultiCount > 55) { _SysInfo.nMultiSendData[55] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue58.ToString()); }
					if (_SysInfo.nCanMultiCount > 56) { _SysInfo.nMultiSendData[56] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue59.ToString()); }
					if (_SysInfo.nCanMultiCount > 57) { _SysInfo.nMultiSendData[57] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue60.ToString()); }
					if (_SysInfo.nCanMultiCount > 58) { _SysInfo.nMultiSendData[58] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue61.ToString()); }
					if (_SysInfo.nCanMultiCount > 59) { _SysInfo.nMultiSendData[59] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue62.ToString()); }
					if (_SysInfo.nCanMultiCount > 60) { _SysInfo.nMultiSendData[60] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue63.ToString()); }
					if (_SysInfo.nCanMultiCount > 61) { _SysInfo.nMultiSendData[61] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue64.ToString()); }
					if (_SysInfo.nCanMultiCount > 62) { _SysInfo.nMultiSendData[62] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue65.ToString()); }
					if (_SysInfo.nCanMultiCount > 63) { _SysInfo.nMultiSendData[63] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue66.ToString()); }
					if (_SysInfo.nCanMultiCount > 64) { _SysInfo.nMultiSendData[64] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue67.ToString()); }
					if (_SysInfo.nCanMultiCount > 65) { _SysInfo.nMultiSendData[65] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue68.ToString()); }
					if (_SysInfo.nCanMultiCount > 66) { _SysInfo.nMultiSendData[66] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue69.ToString()); }
					if (_SysInfo.nCanMultiCount > 67) { _SysInfo.nMultiSendData[67] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue70.ToString()); }
					if (_SysInfo.nCanMultiCount > 68) { _SysInfo.nMultiSendData[68] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue71.ToString()); }
					if (_SysInfo.nCanMultiCount > 69) { _SysInfo.nMultiSendData[69] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue72.ToString()); }
					if (_SysInfo.nCanMultiCount > 70) { _SysInfo.nMultiSendData[70] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue73.ToString()); }
					if (_SysInfo.nCanMultiCount > 71) { _SysInfo.nMultiSendData[71] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue74.ToString()); }
					if (_SysInfo.nCanMultiCount > 72) { _SysInfo.nMultiSendData[72] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue75.ToString()); }
					if (_SysInfo.nCanMultiCount > 73) { _SysInfo.nMultiSendData[73] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue76.ToString()); }
					if (_SysInfo.nCanMultiCount > 74) { _SysInfo.nMultiSendData[74] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue77.ToString()); }
					if (_SysInfo.nCanMultiCount > 75) { _SysInfo.nMultiSendData[75] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue78.ToString()); }
					if (_SysInfo.nCanMultiCount > 76) { _SysInfo.nMultiSendData[76] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue79.ToString()); }
					if (_SysInfo.nCanMultiCount > 77) { _SysInfo.nMultiSendData[77] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue80.ToString()); }
					if (_SysInfo.nCanMultiCount > 78) { _SysInfo.nMultiSendData[78] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue81.ToString()); }
					if (_SysInfo.nCanMultiCount > 79) { _SysInfo.nMultiSendData[79] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue82.ToString()); }
					if (_SysInfo.nCanMultiCount > 80) { _SysInfo.nMultiSendData[80] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue83.ToString()); }
					if (_SysInfo.nCanMultiCount > 81) { _SysInfo.nMultiSendData[81] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue84.ToString()); }
					if (_SysInfo.nCanMultiCount > 82) { _SysInfo.nMultiSendData[82] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue85.ToString()); }
					if (_SysInfo.nCanMultiCount > 83) { _SysInfo.nMultiSendData[83] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue86.ToString()); }
					if (_SysInfo.nCanMultiCount > 84) { _SysInfo.nMultiSendData[84] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue87.ToString()); }
					if (_SysInfo.nCanMultiCount > 85) { _SysInfo.nMultiSendData[85] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue88.ToString()); }
					if (_SysInfo.nCanMultiCount > 86) { _SysInfo.nMultiSendData[86] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue89.ToString()); }
					if (_SysInfo.nCanMultiCount > 87) { _SysInfo.nMultiSendData[87] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue90.ToString()); }
					if (_SysInfo.nCanMultiCount > 88) { _SysInfo.nMultiSendData[88] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue91.ToString()); }
					if (_SysInfo.nCanMultiCount > 89) { _SysInfo.nMultiSendData[89] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue92.ToString()); }
					if (_SysInfo.nCanMultiCount > 90) { _SysInfo.nMultiSendData[90] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue93.ToString()); }
					if (_SysInfo.nCanMultiCount > 91) { _SysInfo.nMultiSendData[91] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue94.ToString()); }
					if (_SysInfo.nCanMultiCount > 92) { _SysInfo.nMultiSendData[92] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue95.ToString()); }
					if (_SysInfo.nCanMultiCount > 93) { _SysInfo.nMultiSendData[93] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue96.ToString()); }
					if (_SysInfo.nCanMultiCount > 94) { _SysInfo.nMultiSendData[94] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue97.ToString()); }
					if (_SysInfo.nCanMultiCount > 95) { _SysInfo.nMultiSendData[95] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue98.ToString()); }
					if (_SysInfo.nCanMultiCount > 96) { _SysInfo.nMultiSendData[96] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue99.ToString()); }
					if (_SysInfo.nCanMultiCount > 97) { _SysInfo.nMultiSendData[97] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue100.ToString()); }

					//if (_SysInfo.nCanMultiCount > 0) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue3.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[0]); }
					//if (_SysInfo.nCanMultiCount > 1) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue4.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[1]); }
					//if (_SysInfo.nCanMultiCount > 2) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue5.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[2]); }
					//if (_SysInfo.nCanMultiCount > 3) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue6.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[3]); }
					//if (_SysInfo.nCanMultiCount > 4) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue7.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[4]); }
					//if (_SysInfo.nCanMultiCount > 5) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue8.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[5]); }
					//if (_SysInfo.nCanMultiCount > 6) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue9.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[6]); }
					//if (_SysInfo.nCanMultiCount > 7) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue10.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[7]); }
					//if (_SysInfo.nCanMultiCount > 8) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue11.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[8]); }
					//if (_SysInfo.nCanMultiCount > 9) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue12.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[9]); }
					//if (_SysInfo.nCanMultiCount > 10) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue13.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[10]); }
					//if (_SysInfo.nCanMultiCount > 11) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue14.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[11]); }
					//if (_SysInfo.nCanMultiCount > 12) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue15.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[12]); }
					//if (_SysInfo.nCanMultiCount > 13) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue16.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[13]); }
					//if (_SysInfo.nCanMultiCount > 14) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue17.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[14]); }
					//if (_SysInfo.nCanMultiCount > 15) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue18.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[15]); }
					//if (_SysInfo.nCanMultiCount > 16) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue19.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[16]); }
					//if (_SysInfo.nCanMultiCount > 17) { int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue20.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nMultiSendData[17]); }

					//tMainTimer[nStepIndex].Start(1000);
					SendWriteMultiCommand(_SysInfo.nCanCh, _SysInfo.nCanAddr, _SysInfo.nMultiSendData, _SysInfo.nCanMultiCount, 1);
					nProcessStep[nStepIndex]++;
					break;

				case 21501:
					//if (!tMainTimer[nStepIndex].Verify()) { break; }
					//if (_CanComm[_SysInfo.nCanCh].bReadMessage)
					//{
					//	if (_CanComm[_SysInfo.nCanCh].btReadData[0] == 0x06)
					//	{
					//		//if ((_CanComm[_SysInfo.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo.nCanCh].btReadData[3] == _SysInfo.nCanData)
					//		//{

					//		//}
					//		_SysInfo.nRepeatWorkStep++;
					//		nProcessStep[nStepIndex] = 20000;
					//	}
					//}
					_SysInfo.nRepeatWorkStep++;
					nProcessStep[nStepIndex] = 20000;
					break;


				// Can Write
				case 21600:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].nID.ToString(), out _SysInfo.nCanCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue1, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nCanStartAddr);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue2.ToString(), out _SysInfo.nCanMultiCount);
					if (_SysInfo.nCanMultiCount > 0) { _SysInfo.nMultiSendData[0] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue3.ToString()); }
					if (_SysInfo.nCanMultiCount > 1) { _SysInfo.nMultiSendData[1] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue4.ToString()); }
					if (_SysInfo.nCanMultiCount > 2) { _SysInfo.nMultiSendData[2] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue5.ToString()); }
					if (_SysInfo.nCanMultiCount > 3) { _SysInfo.nMultiSendData[3] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue6.ToString()); }
					if (_SysInfo.nCanMultiCount > 4) { _SysInfo.nMultiSendData[4] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue7.ToString()); }
					if (_SysInfo.nCanMultiCount > 5) { _SysInfo.nMultiSendData[5] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue8.ToString()); }
					if (_SysInfo.nCanMultiCount > 6) { _SysInfo.nMultiSendData[6] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue9.ToString()); }
					if (_SysInfo.nCanMultiCount > 7) { _SysInfo.nMultiSendData[7] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue10.ToString()); }
					if (_SysInfo.nCanMultiCount > 8) { _SysInfo.nMultiSendData[8] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue11.ToString()); }
					if (_SysInfo.nCanMultiCount > 9) { _SysInfo.nMultiSendData[9] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue12.ToString()); }
					if (_SysInfo.nCanMultiCount > 10) { _SysInfo.nMultiSendData[10] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue13.ToString()); }
					if (_SysInfo.nCanMultiCount > 11) { _SysInfo.nMultiSendData[11] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue14.ToString()); }
					if (_SysInfo.nCanMultiCount > 12) { _SysInfo.nMultiSendData[12] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue15.ToString()); }
					if (_SysInfo.nCanMultiCount > 13) { _SysInfo.nMultiSendData[13] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue16.ToString()); }
					if (_SysInfo.nCanMultiCount > 14) { _SysInfo.nMultiSendData[14] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue17.ToString()); }
					if (_SysInfo.nCanMultiCount > 15) { _SysInfo.nMultiSendData[15] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue18.ToString()); }
					if (_SysInfo.nCanMultiCount > 16) { _SysInfo.nMultiSendData[16] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue19.ToString()); }
					if (_SysInfo.nCanMultiCount > 17) { _SysInfo.nMultiSendData[17] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue20.ToString()); }
					if (_SysInfo.nCanMultiCount > 18) { _SysInfo.nMultiSendData[18] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue21.ToString()); }
					if (_SysInfo.nCanMultiCount > 19) { _SysInfo.nMultiSendData[19] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue22.ToString()); }
					if (_SysInfo.nCanMultiCount > 20) { _SysInfo.nMultiSendData[20] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue23.ToString()); }
					if (_SysInfo.nCanMultiCount > 21) { _SysInfo.nMultiSendData[21] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue24.ToString()); }
					if (_SysInfo.nCanMultiCount > 22) { _SysInfo.nMultiSendData[22] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue25.ToString()); }
					if (_SysInfo.nCanMultiCount > 23) { _SysInfo.nMultiSendData[23] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue26.ToString()); }
					if (_SysInfo.nCanMultiCount > 24) { _SysInfo.nMultiSendData[24] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue27.ToString()); }
					if (_SysInfo.nCanMultiCount > 25) { _SysInfo.nMultiSendData[25] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue28.ToString()); }
					if (_SysInfo.nCanMultiCount > 26) { _SysInfo.nMultiSendData[26] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue29.ToString()); }
					if (_SysInfo.nCanMultiCount > 27) { _SysInfo.nMultiSendData[27] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue30.ToString()); }
					if (_SysInfo.nCanMultiCount > 28) { _SysInfo.nMultiSendData[28] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue31.ToString()); }
					if (_SysInfo.nCanMultiCount > 29) { _SysInfo.nMultiSendData[29] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue32.ToString()); }
					if (_SysInfo.nCanMultiCount > 30) { _SysInfo.nMultiSendData[30] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue33.ToString()); }
					if (_SysInfo.nCanMultiCount > 31) { _SysInfo.nMultiSendData[31] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue34.ToString()); }
					if (_SysInfo.nCanMultiCount > 32) { _SysInfo.nMultiSendData[32] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue35.ToString()); }
					if (_SysInfo.nCanMultiCount > 33) { _SysInfo.nMultiSendData[33] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue36.ToString()); }
					if (_SysInfo.nCanMultiCount > 34) { _SysInfo.nMultiSendData[34] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue37.ToString()); }
					if (_SysInfo.nCanMultiCount > 35) { _SysInfo.nMultiSendData[35] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue38.ToString()); }
					if (_SysInfo.nCanMultiCount > 36) { _SysInfo.nMultiSendData[36] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue39.ToString()); }
					if (_SysInfo.nCanMultiCount > 37) { _SysInfo.nMultiSendData[37] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue40.ToString()); }
					if (_SysInfo.nCanMultiCount > 38) { _SysInfo.nMultiSendData[38] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue41.ToString()); }
					if (_SysInfo.nCanMultiCount > 39) { _SysInfo.nMultiSendData[39] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue42.ToString()); }
					if (_SysInfo.nCanMultiCount > 40) { _SysInfo.nMultiSendData[40] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue43.ToString()); }
					if (_SysInfo.nCanMultiCount > 41) { _SysInfo.nMultiSendData[41] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue44.ToString()); }
					if (_SysInfo.nCanMultiCount > 42) { _SysInfo.nMultiSendData[42] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue45.ToString()); }
					if (_SysInfo.nCanMultiCount > 43) { _SysInfo.nMultiSendData[43] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue46.ToString()); }
					if (_SysInfo.nCanMultiCount > 44) { _SysInfo.nMultiSendData[44] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue47.ToString()); }
					if (_SysInfo.nCanMultiCount > 45) { _SysInfo.nMultiSendData[45] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue48.ToString()); }
					if (_SysInfo.nCanMultiCount > 46) { _SysInfo.nMultiSendData[46] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue49.ToString()); }
					if (_SysInfo.nCanMultiCount > 47) { _SysInfo.nMultiSendData[47] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue50.ToString()); }
					if (_SysInfo.nCanMultiCount > 48) { _SysInfo.nMultiSendData[48] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue51.ToString()); }
					if (_SysInfo.nCanMultiCount > 49) { _SysInfo.nMultiSendData[49] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue52.ToString()); }
					if (_SysInfo.nCanMultiCount > 50) { _SysInfo.nMultiSendData[50] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue53.ToString()); }
					if (_SysInfo.nCanMultiCount > 51) { _SysInfo.nMultiSendData[51] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue54.ToString()); }
					if (_SysInfo.nCanMultiCount > 52) { _SysInfo.nMultiSendData[52] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue55.ToString()); }
					if (_SysInfo.nCanMultiCount > 53) { _SysInfo.nMultiSendData[53] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue56.ToString()); }
					if (_SysInfo.nCanMultiCount > 54) { _SysInfo.nMultiSendData[54] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue57.ToString()); }
					if (_SysInfo.nCanMultiCount > 55) { _SysInfo.nMultiSendData[55] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue58.ToString()); }
					if (_SysInfo.nCanMultiCount > 56) { _SysInfo.nMultiSendData[56] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue59.ToString()); }
					if (_SysInfo.nCanMultiCount > 57) { _SysInfo.nMultiSendData[57] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue60.ToString()); }
					if (_SysInfo.nCanMultiCount > 58) { _SysInfo.nMultiSendData[58] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue61.ToString()); }
					if (_SysInfo.nCanMultiCount > 59) { _SysInfo.nMultiSendData[59] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue62.ToString()); }
					if (_SysInfo.nCanMultiCount > 60) { _SysInfo.nMultiSendData[60] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue63.ToString()); }
					if (_SysInfo.nCanMultiCount > 61) { _SysInfo.nMultiSendData[61] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue64.ToString()); }
					if (_SysInfo.nCanMultiCount > 62) { _SysInfo.nMultiSendData[62] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue65.ToString()); }
					if (_SysInfo.nCanMultiCount > 63) { _SysInfo.nMultiSendData[63] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue66.ToString()); }
					if (_SysInfo.nCanMultiCount > 64) { _SysInfo.nMultiSendData[64] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue67.ToString()); }
					if (_SysInfo.nCanMultiCount > 65) { _SysInfo.nMultiSendData[65] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue68.ToString()); }
					if (_SysInfo.nCanMultiCount > 66) { _SysInfo.nMultiSendData[66] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue69.ToString()); }
					if (_SysInfo.nCanMultiCount > 67) { _SysInfo.nMultiSendData[67] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue70.ToString()); }
					if (_SysInfo.nCanMultiCount > 68) { _SysInfo.nMultiSendData[68] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue71.ToString()); }
					if (_SysInfo.nCanMultiCount > 69) { _SysInfo.nMultiSendData[69] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue72.ToString()); }
					if (_SysInfo.nCanMultiCount > 70) { _SysInfo.nMultiSendData[70] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue73.ToString()); }
					if (_SysInfo.nCanMultiCount > 71) { _SysInfo.nMultiSendData[71] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue74.ToString()); }
					if (_SysInfo.nCanMultiCount > 72) { _SysInfo.nMultiSendData[72] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue75.ToString()); }
					if (_SysInfo.nCanMultiCount > 73) { _SysInfo.nMultiSendData[73] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue76.ToString()); }
					if (_SysInfo.nCanMultiCount > 74) { _SysInfo.nMultiSendData[74] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue77.ToString()); }
					if (_SysInfo.nCanMultiCount > 75) { _SysInfo.nMultiSendData[75] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue78.ToString()); }
					if (_SysInfo.nCanMultiCount > 76) { _SysInfo.nMultiSendData[76] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue79.ToString()); }
					if (_SysInfo.nCanMultiCount > 77) { _SysInfo.nMultiSendData[77] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue80.ToString()); }
					if (_SysInfo.nCanMultiCount > 78) { _SysInfo.nMultiSendData[78] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue81.ToString()); }
					if (_SysInfo.nCanMultiCount > 79) { _SysInfo.nMultiSendData[79] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue82.ToString()); }
					if (_SysInfo.nCanMultiCount > 80) { _SysInfo.nMultiSendData[80] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue83.ToString()); }
					if (_SysInfo.nCanMultiCount > 81) { _SysInfo.nMultiSendData[81] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue84.ToString()); }
					if (_SysInfo.nCanMultiCount > 82) { _SysInfo.nMultiSendData[82] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue85.ToString()); }
					if (_SysInfo.nCanMultiCount > 83) { _SysInfo.nMultiSendData[83] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue86.ToString()); }
					if (_SysInfo.nCanMultiCount > 84) { _SysInfo.nMultiSendData[84] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue87.ToString()); }
					if (_SysInfo.nCanMultiCount > 85) { _SysInfo.nMultiSendData[85] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue88.ToString()); }
					if (_SysInfo.nCanMultiCount > 86) { _SysInfo.nMultiSendData[86] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue89.ToString()); }
					if (_SysInfo.nCanMultiCount > 87) { _SysInfo.nMultiSendData[87] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue90.ToString()); }
					if (_SysInfo.nCanMultiCount > 88) { _SysInfo.nMultiSendData[88] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue91.ToString()); }
					if (_SysInfo.nCanMultiCount > 89) { _SysInfo.nMultiSendData[89] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue92.ToString()); }
					if (_SysInfo.nCanMultiCount > 90) { _SysInfo.nMultiSendData[90] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue93.ToString()); }
					if (_SysInfo.nCanMultiCount > 91) { _SysInfo.nMultiSendData[91] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue94.ToString()); }
					if (_SysInfo.nCanMultiCount > 92) { _SysInfo.nMultiSendData[92] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue95.ToString()); }
					if (_SysInfo.nCanMultiCount > 93) { _SysInfo.nMultiSendData[93] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue96.ToString()); }
					if (_SysInfo.nCanMultiCount > 94) { _SysInfo.nMultiSendData[94] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue97.ToString()); }
					if (_SysInfo.nCanMultiCount > 95) { _SysInfo.nMultiSendData[95] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue98.ToString()); }
					if (_SysInfo.nCanMultiCount > 96) { _SysInfo.nMultiSendData[96] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue99.ToString()); }
					if (_SysInfo.nCanMultiCount > 97) { _SysInfo.nMultiSendData[97] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue100.ToString()); }
					SendCanData(_SysInfo.nCanCh, _SysInfo.nCanStartAddr, _SysInfo.nMultiSendData, _SysInfo.nCanMultiCount, 1);
					_SysInfo.nRepeatWorkStep++;
					nProcessStep[nStepIndex] = 20000;
					break;

				case 21700:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].nID.ToString(), out _SysInfo.nCanCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue1, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nCanStartAddr);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue2.ToString(), out _SysInfo.nCanMultiCount);
					if (_SysInfo.nCanMultiCount > 0) { _SysInfo.strValueBuff[0] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue3; }
					if (_SysInfo.nCanMultiCount > 1) { _SysInfo.strValueBuff[1] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue4; }
					if (_SysInfo.nCanMultiCount > 2) { _SysInfo.strValueBuff[2] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue5; }
					if (_SysInfo.nCanMultiCount > 3) { _SysInfo.strValueBuff[3] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue6; }
					if (_SysInfo.nCanMultiCount > 4) { _SysInfo.strValueBuff[4] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue7; }
					if (_SysInfo.nCanMultiCount > 5) { _SysInfo.strValueBuff[5] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue8; }
					if (_SysInfo.nCanMultiCount > 6) { _SysInfo.strValueBuff[6] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue9; }
					if (_SysInfo.nCanMultiCount > 7) { _SysInfo.strValueBuff[7] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue10; }
					if (_SysInfo.nCanMultiCount > 8) { _SysInfo.strValueBuff[8] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue11; }
					if (_SysInfo.nCanMultiCount > 9) { _SysInfo.strValueBuff[9] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue12; }
					if (_SysInfo.nCanMultiCount > 10) { _SysInfo.strValueBuff[10] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue13; }
					if (_SysInfo.nCanMultiCount > 11) { _SysInfo.strValueBuff[11] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue14; }
					if (_SysInfo.nCanMultiCount > 12) { _SysInfo.strValueBuff[12] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue15; }
					if (_SysInfo.nCanMultiCount > 13) { _SysInfo.strValueBuff[13] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue16; }
					if (_SysInfo.nCanMultiCount > 14) { _SysInfo.strValueBuff[14] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue17; }
					if (_SysInfo.nCanMultiCount > 15) { _SysInfo.strValueBuff[15] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue18; }
					if (_SysInfo.nCanMultiCount > 16) { _SysInfo.strValueBuff[16] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue19; }
					if (_SysInfo.nCanMultiCount > 17) { _SysInfo.strValueBuff[17] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue20; }
					if (_SysInfo.nCanMultiCount > 18) { _SysInfo.strValueBuff[18] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue21; }
					if (_SysInfo.nCanMultiCount > 19) { _SysInfo.strValueBuff[19] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue22; }
					if (_SysInfo.nCanMultiCount > 20) { _SysInfo.strValueBuff[20] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue23; }
					if (_SysInfo.nCanMultiCount > 21) { _SysInfo.strValueBuff[21] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue24; }
					if (_SysInfo.nCanMultiCount > 22) { _SysInfo.strValueBuff[22] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue25; }
					if (_SysInfo.nCanMultiCount > 23) { _SysInfo.strValueBuff[23] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue26; }
					if (_SysInfo.nCanMultiCount > 24) { _SysInfo.strValueBuff[24] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue27; }
					if (_SysInfo.nCanMultiCount > 25) { _SysInfo.strValueBuff[25] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue28; }
					if (_SysInfo.nCanMultiCount > 26) { _SysInfo.strValueBuff[26] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue29; }
					if (_SysInfo.nCanMultiCount > 27) { _SysInfo.strValueBuff[27] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue30; }
					if (_SysInfo.nCanMultiCount > 28) { _SysInfo.strValueBuff[28] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue31; }
					if (_SysInfo.nCanMultiCount > 29) { _SysInfo.strValueBuff[29] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue32; }
					if (_SysInfo.nCanMultiCount > 30) { _SysInfo.strValueBuff[30] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue33; }
					if (_SysInfo.nCanMultiCount > 31) { _SysInfo.strValueBuff[31] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue34; }
					if (_SysInfo.nCanMultiCount > 32) { _SysInfo.strValueBuff[32] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue35; }
					if (_SysInfo.nCanMultiCount > 33) { _SysInfo.strValueBuff[33] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue36; }
					if (_SysInfo.nCanMultiCount > 34) { _SysInfo.strValueBuff[34] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue37; }
					if (_SysInfo.nCanMultiCount > 35) { _SysInfo.strValueBuff[35] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue38; }
					if (_SysInfo.nCanMultiCount > 36) { _SysInfo.strValueBuff[36] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue39; }
					if (_SysInfo.nCanMultiCount > 37) { _SysInfo.strValueBuff[37] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue40; }
					if (_SysInfo.nCanMultiCount > 38) { _SysInfo.strValueBuff[38] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue41; }
					if (_SysInfo.nCanMultiCount > 39) { _SysInfo.strValueBuff[39] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue42; }
					if (_SysInfo.nCanMultiCount > 40) { _SysInfo.strValueBuff[40] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue43; }
					if (_SysInfo.nCanMultiCount > 41) { _SysInfo.strValueBuff[41] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue44; }
					if (_SysInfo.nCanMultiCount > 42) { _SysInfo.strValueBuff[42] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue45; }
					if (_SysInfo.nCanMultiCount > 43) { _SysInfo.strValueBuff[43] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue46; }
					if (_SysInfo.nCanMultiCount > 44) { _SysInfo.strValueBuff[44] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue47; }
					if (_SysInfo.nCanMultiCount > 45) { _SysInfo.strValueBuff[45] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue48; }
					if (_SysInfo.nCanMultiCount > 46) { _SysInfo.strValueBuff[46] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue49; }
					if (_SysInfo.nCanMultiCount > 47) { _SysInfo.strValueBuff[47] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue50; }
					if (_SysInfo.nCanMultiCount > 48) { _SysInfo.strValueBuff[48] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue51; }
					if (_SysInfo.nCanMultiCount > 49) { _SysInfo.strValueBuff[49] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue52; }
					if (_SysInfo.nCanMultiCount > 50) { _SysInfo.strValueBuff[50] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue53; }
					if (_SysInfo.nCanMultiCount > 51) { _SysInfo.strValueBuff[51] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue54; }
					if (_SysInfo.nCanMultiCount > 52) { _SysInfo.strValueBuff[52] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue55; }
					if (_SysInfo.nCanMultiCount > 53) { _SysInfo.strValueBuff[53] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue56; }
					if (_SysInfo.nCanMultiCount > 54) { _SysInfo.strValueBuff[54] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue57; }
					if (_SysInfo.nCanMultiCount > 55) { _SysInfo.strValueBuff[55] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue58; }
					if (_SysInfo.nCanMultiCount > 56) { _SysInfo.strValueBuff[56] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue59; }
					if (_SysInfo.nCanMultiCount > 57) { _SysInfo.strValueBuff[57] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue60; }
					if (_SysInfo.nCanMultiCount > 58) { _SysInfo.strValueBuff[58] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue61; }
					if (_SysInfo.nCanMultiCount > 59) { _SysInfo.strValueBuff[59] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue62; }
					if (_SysInfo.nCanMultiCount > 60) { _SysInfo.strValueBuff[60] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue63; }
					if (_SysInfo.nCanMultiCount > 61) { _SysInfo.strValueBuff[61] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue64; }
					if (_SysInfo.nCanMultiCount > 62) { _SysInfo.strValueBuff[62] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue65; }
					if (_SysInfo.nCanMultiCount > 63) { _SysInfo.strValueBuff[63] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue66; }
					if (_SysInfo.nCanMultiCount > 64) { _SysInfo.strValueBuff[64] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue67; }
					if (_SysInfo.nCanMultiCount > 65) { _SysInfo.strValueBuff[65] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue68; }
					if (_SysInfo.nCanMultiCount > 66) { _SysInfo.strValueBuff[66] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue69; }
					if (_SysInfo.nCanMultiCount > 67) { _SysInfo.strValueBuff[67] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue70; }
					if (_SysInfo.nCanMultiCount > 68) { _SysInfo.strValueBuff[68] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue71; }
					if (_SysInfo.nCanMultiCount > 69) { _SysInfo.strValueBuff[69] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue72; }
					if (_SysInfo.nCanMultiCount > 70) { _SysInfo.strValueBuff[70] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue73; }
					if (_SysInfo.nCanMultiCount > 71) { _SysInfo.strValueBuff[71] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue74; }
					if (_SysInfo.nCanMultiCount > 72) { _SysInfo.strValueBuff[72] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue75; }
					if (_SysInfo.nCanMultiCount > 73) { _SysInfo.strValueBuff[73] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue76; }
					if (_SysInfo.nCanMultiCount > 74) { _SysInfo.strValueBuff[74] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue77; }
					if (_SysInfo.nCanMultiCount > 75) { _SysInfo.strValueBuff[75] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue78; }
					if (_SysInfo.nCanMultiCount > 76) { _SysInfo.strValueBuff[76] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue79; }
					if (_SysInfo.nCanMultiCount > 77) { _SysInfo.strValueBuff[77] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue80; }
					if (_SysInfo.nCanMultiCount > 78) { _SysInfo.strValueBuff[78] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue81; }
					if (_SysInfo.nCanMultiCount > 79) { _SysInfo.strValueBuff[79] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue82; }
					if (_SysInfo.nCanMultiCount > 80) { _SysInfo.strValueBuff[80] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue83; }
					if (_SysInfo.nCanMultiCount > 81) { _SysInfo.strValueBuff[81] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue84; }
					if (_SysInfo.nCanMultiCount > 82) { _SysInfo.strValueBuff[82] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue85; }
					if (_SysInfo.nCanMultiCount > 83) { _SysInfo.strValueBuff[83] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue86; }
					if (_SysInfo.nCanMultiCount > 84) { _SysInfo.strValueBuff[84] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue87; }
					if (_SysInfo.nCanMultiCount > 85) { _SysInfo.strValueBuff[85] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue88; }
					if (_SysInfo.nCanMultiCount > 86) { _SysInfo.strValueBuff[86] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue89; }
					if (_SysInfo.nCanMultiCount > 87) { _SysInfo.strValueBuff[87] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue90; }
					if (_SysInfo.nCanMultiCount > 88) { _SysInfo.strValueBuff[88] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue91; }
					if (_SysInfo.nCanMultiCount > 89) { _SysInfo.strValueBuff[89] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue92; }
					if (_SysInfo.nCanMultiCount > 90) { _SysInfo.strValueBuff[90] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue93; }
					if (_SysInfo.nCanMultiCount > 91) { _SysInfo.strValueBuff[91] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue94; }
					if (_SysInfo.nCanMultiCount > 92) { _SysInfo.strValueBuff[92] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue95; }
					if (_SysInfo.nCanMultiCount > 93) { _SysInfo.strValueBuff[93] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue96; }
					if (_SysInfo.nCanMultiCount > 94) { _SysInfo.strValueBuff[94] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue97; }
					if (_SysInfo.nCanMultiCount > 95) { _SysInfo.strValueBuff[95] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue98; }
					if (_SysInfo.nCanMultiCount > 96) { _SysInfo.strValueBuff[96] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue99; }
					if (_SysInfo.nCanMultiCount > 97) { _SysInfo.strValueBuff[97] = _ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue100; }
					tMainTimer[nStepIndex].Start(5000);
					nProcessStep[nStepIndex]++;
					break;

				case 21701:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet(_SysInfo.nSubMainWorkStep, _SysInfo.nCanAddr.ToString("X"), "", "TIME OUT", "NG");
						_SysInfo.bEolNg = true;
						_SysInfo.nRepeatWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}

					if (_CanComm[_SysInfo.nCanCh].bReadMessage && _CanComm[_SysInfo.nCanCh].nReadid == _SysInfo.nCanStartAddr && _CanComm[_SysInfo.nCanCh].nReaddlc == _SysInfo.nCanMultiCount)
					{
						bool bCompResult = true;
						string strSource = "";
						string strRead = "";

						for (int i = 0; i < _SysInfo.nCanMultiCount; i++)
						{
							strSource += _SysInfo.strValueBuff[i];
							strRead += _CanComm[_SysInfo.nCanCh].btReadData[i].ToString("X2");

							if (!GetCompareCanData(_SysInfo.strValueBuff[i], _CanComm[_SysInfo.nCanCh].btReadData[i]))
							{
								bCompResult = false;
							}
						}

						if (bCompResult)
						{
							NgDataSet(_SysInfo.nSubMainWorkStep, _SysInfo.nCanAddr.ToString("X"), strSource, strRead, "OK");
						}
						else
						{
							NgDataSet(_SysInfo.nSubMainWorkStep, _SysInfo.nCanAddr.ToString("X"), strSource, strRead, "NG");
							_SysInfo.bEolNg = true;
						}

						_SysInfo.nRepeatWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;




				case 21800:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].nID.ToString(), out _SysInfo.nCanCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue1, out _SysInfo.nCanAddr);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue2.ToString(), out _SysInfo.nCanMultiCount);
					if (_SysInfo.nCanMultiCount > 0) { _SysInfo.nMultiSendData[0] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue3.ToString()); }
					if (_SysInfo.nCanMultiCount > 1) { _SysInfo.nMultiSendData[1] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue4.ToString()); }
					if (_SysInfo.nCanMultiCount > 2) { _SysInfo.nMultiSendData[2] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue5.ToString()); }
					if (_SysInfo.nCanMultiCount > 3) { _SysInfo.nMultiSendData[3] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue6.ToString()); }
					if (_SysInfo.nCanMultiCount > 4) { _SysInfo.nMultiSendData[4] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue7.ToString()); }
					if (_SysInfo.nCanMultiCount > 5) { _SysInfo.nMultiSendData[5] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue8.ToString()); }
					if (_SysInfo.nCanMultiCount > 6) { _SysInfo.nMultiSendData[6] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue9.ToString()); }
					if (_SysInfo.nCanMultiCount > 7) { _SysInfo.nMultiSendData[7] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue10.ToString()); }
					if (_SysInfo.nCanMultiCount > 8) { _SysInfo.nMultiSendData[8] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue11.ToString()); }
					if (_SysInfo.nCanMultiCount > 9) { _SysInfo.nMultiSendData[9] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue12.ToString()); }
					if (_SysInfo.nCanMultiCount > 10) { _SysInfo.nMultiSendData[10] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue13.ToString()); }
					if (_SysInfo.nCanMultiCount > 11) { _SysInfo.nMultiSendData[11] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue14.ToString()); }
					if (_SysInfo.nCanMultiCount > 12) { _SysInfo.nMultiSendData[12] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue15.ToString()); }
					if (_SysInfo.nCanMultiCount > 13) { _SysInfo.nMultiSendData[13] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue16.ToString()); }
					if (_SysInfo.nCanMultiCount > 14) { _SysInfo.nMultiSendData[14] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue17.ToString()); }
					if (_SysInfo.nCanMultiCount > 15) { _SysInfo.nMultiSendData[15] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue18.ToString()); }
					if (_SysInfo.nCanMultiCount > 16) { _SysInfo.nMultiSendData[16] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue19.ToString()); }
					if (_SysInfo.nCanMultiCount > 17) { _SysInfo.nMultiSendData[17] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue20.ToString()); }
					if (_SysInfo.nCanMultiCount > 18) { _SysInfo.nMultiSendData[18] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue21.ToString()); }
					if (_SysInfo.nCanMultiCount > 19) { _SysInfo.nMultiSendData[19] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue22.ToString()); }
					if (_SysInfo.nCanMultiCount > 20) { _SysInfo.nMultiSendData[20] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue23.ToString()); }
					if (_SysInfo.nCanMultiCount > 21) { _SysInfo.nMultiSendData[21] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue24.ToString()); }
					if (_SysInfo.nCanMultiCount > 22) { _SysInfo.nMultiSendData[22] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue25.ToString()); }
					if (_SysInfo.nCanMultiCount > 23) { _SysInfo.nMultiSendData[23] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue26.ToString()); }
					if (_SysInfo.nCanMultiCount > 24) { _SysInfo.nMultiSendData[24] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue27.ToString()); }
					if (_SysInfo.nCanMultiCount > 25) { _SysInfo.nMultiSendData[25] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue28.ToString()); }
					if (_SysInfo.nCanMultiCount > 26) { _SysInfo.nMultiSendData[26] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue29.ToString()); }
					if (_SysInfo.nCanMultiCount > 27) { _SysInfo.nMultiSendData[27] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue30.ToString()); }
					if (_SysInfo.nCanMultiCount > 28) { _SysInfo.nMultiSendData[28] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue31.ToString()); }
					if (_SysInfo.nCanMultiCount > 29) { _SysInfo.nMultiSendData[29] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue32.ToString()); }
					if (_SysInfo.nCanMultiCount > 30) { _SysInfo.nMultiSendData[30] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue33.ToString()); }
					if (_SysInfo.nCanMultiCount > 31) { _SysInfo.nMultiSendData[31] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue34.ToString()); }
					if (_SysInfo.nCanMultiCount > 32) { _SysInfo.nMultiSendData[32] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue35.ToString()); }
					if (_SysInfo.nCanMultiCount > 33) { _SysInfo.nMultiSendData[33] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue36.ToString()); }
					if (_SysInfo.nCanMultiCount > 34) { _SysInfo.nMultiSendData[34] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue37.ToString()); }
					if (_SysInfo.nCanMultiCount > 35) { _SysInfo.nMultiSendData[35] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue38.ToString()); }
					if (_SysInfo.nCanMultiCount > 36) { _SysInfo.nMultiSendData[36] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue39.ToString()); }
					if (_SysInfo.nCanMultiCount > 37) { _SysInfo.nMultiSendData[37] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue40.ToString()); }
					if (_SysInfo.nCanMultiCount > 38) { _SysInfo.nMultiSendData[38] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue41.ToString()); }
					if (_SysInfo.nCanMultiCount > 39) { _SysInfo.nMultiSendData[39] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue42.ToString()); }
					if (_SysInfo.nCanMultiCount > 40) { _SysInfo.nMultiSendData[40] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue43.ToString()); }
					if (_SysInfo.nCanMultiCount > 41) { _SysInfo.nMultiSendData[41] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue44.ToString()); }
					if (_SysInfo.nCanMultiCount > 42) { _SysInfo.nMultiSendData[42] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue45.ToString()); }
					if (_SysInfo.nCanMultiCount > 43) { _SysInfo.nMultiSendData[43] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue46.ToString()); }
					if (_SysInfo.nCanMultiCount > 44) { _SysInfo.nMultiSendData[44] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue47.ToString()); }
					if (_SysInfo.nCanMultiCount > 45) { _SysInfo.nMultiSendData[45] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue48.ToString()); }
					if (_SysInfo.nCanMultiCount > 46) { _SysInfo.nMultiSendData[46] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue49.ToString()); }
					if (_SysInfo.nCanMultiCount > 47) { _SysInfo.nMultiSendData[47] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue50.ToString()); }
					if (_SysInfo.nCanMultiCount > 48) { _SysInfo.nMultiSendData[48] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue51.ToString()); }
					if (_SysInfo.nCanMultiCount > 49) { _SysInfo.nMultiSendData[49] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue52.ToString()); }
					if (_SysInfo.nCanMultiCount > 50) { _SysInfo.nMultiSendData[50] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue53.ToString()); }
					if (_SysInfo.nCanMultiCount > 51) { _SysInfo.nMultiSendData[51] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue54.ToString()); }
					if (_SysInfo.nCanMultiCount > 52) { _SysInfo.nMultiSendData[52] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue55.ToString()); }
					if (_SysInfo.nCanMultiCount > 53) { _SysInfo.nMultiSendData[53] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue56.ToString()); }
					if (_SysInfo.nCanMultiCount > 54) { _SysInfo.nMultiSendData[54] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue57.ToString()); }
					if (_SysInfo.nCanMultiCount > 55) { _SysInfo.nMultiSendData[55] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue58.ToString()); }
					if (_SysInfo.nCanMultiCount > 56) { _SysInfo.nMultiSendData[56] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue59.ToString()); }
					if (_SysInfo.nCanMultiCount > 57) { _SysInfo.nMultiSendData[57] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue60.ToString()); }
					if (_SysInfo.nCanMultiCount > 58) { _SysInfo.nMultiSendData[58] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue61.ToString()); }
					if (_SysInfo.nCanMultiCount > 59) { _SysInfo.nMultiSendData[59] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue62.ToString()); }
					if (_SysInfo.nCanMultiCount > 60) { _SysInfo.nMultiSendData[60] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue63.ToString()); }
					if (_SysInfo.nCanMultiCount > 61) { _SysInfo.nMultiSendData[61] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue64.ToString()); }
					if (_SysInfo.nCanMultiCount > 62) { _SysInfo.nMultiSendData[62] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue65.ToString()); }
					if (_SysInfo.nCanMultiCount > 63) { _SysInfo.nMultiSendData[63] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue66.ToString()); }
					if (_SysInfo.nCanMultiCount > 64) { _SysInfo.nMultiSendData[64] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue67.ToString()); }
					if (_SysInfo.nCanMultiCount > 65) { _SysInfo.nMultiSendData[65] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue68.ToString()); }
					if (_SysInfo.nCanMultiCount > 66) { _SysInfo.nMultiSendData[66] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue69.ToString()); }
					if (_SysInfo.nCanMultiCount > 67) { _SysInfo.nMultiSendData[67] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue70.ToString()); }
					if (_SysInfo.nCanMultiCount > 68) { _SysInfo.nMultiSendData[68] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue71.ToString()); }
					if (_SysInfo.nCanMultiCount > 69) { _SysInfo.nMultiSendData[69] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue72.ToString()); }
					if (_SysInfo.nCanMultiCount > 70) { _SysInfo.nMultiSendData[70] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue73.ToString()); }
					if (_SysInfo.nCanMultiCount > 71) { _SysInfo.nMultiSendData[71] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue74.ToString()); }
					if (_SysInfo.nCanMultiCount > 72) { _SysInfo.nMultiSendData[72] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue75.ToString()); }
					if (_SysInfo.nCanMultiCount > 73) { _SysInfo.nMultiSendData[73] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue76.ToString()); }
					if (_SysInfo.nCanMultiCount > 74) { _SysInfo.nMultiSendData[74] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue77.ToString()); }
					if (_SysInfo.nCanMultiCount > 75) { _SysInfo.nMultiSendData[75] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue78.ToString()); }
					if (_SysInfo.nCanMultiCount > 76) { _SysInfo.nMultiSendData[76] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue79.ToString()); }
					if (_SysInfo.nCanMultiCount > 77) { _SysInfo.nMultiSendData[77] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue80.ToString()); }
					if (_SysInfo.nCanMultiCount > 78) { _SysInfo.nMultiSendData[78] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue81.ToString()); }
					if (_SysInfo.nCanMultiCount > 79) { _SysInfo.nMultiSendData[79] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue82.ToString()); }
					if (_SysInfo.nCanMultiCount > 80) { _SysInfo.nMultiSendData[80] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue83.ToString()); }
					if (_SysInfo.nCanMultiCount > 81) { _SysInfo.nMultiSendData[81] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue84.ToString()); }
					if (_SysInfo.nCanMultiCount > 82) { _SysInfo.nMultiSendData[82] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue85.ToString()); }
					if (_SysInfo.nCanMultiCount > 83) { _SysInfo.nMultiSendData[83] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue86.ToString()); }
					if (_SysInfo.nCanMultiCount > 84) { _SysInfo.nMultiSendData[84] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue87.ToString()); }
					if (_SysInfo.nCanMultiCount > 85) { _SysInfo.nMultiSendData[85] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue88.ToString()); }
					if (_SysInfo.nCanMultiCount > 86) { _SysInfo.nMultiSendData[86] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue89.ToString()); }
					if (_SysInfo.nCanMultiCount > 87) { _SysInfo.nMultiSendData[87] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue90.ToString()); }
					if (_SysInfo.nCanMultiCount > 88) { _SysInfo.nMultiSendData[88] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue91.ToString()); }
					if (_SysInfo.nCanMultiCount > 89) { _SysInfo.nMultiSendData[89] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue92.ToString()); }
					if (_SysInfo.nCanMultiCount > 90) { _SysInfo.nMultiSendData[90] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue93.ToString()); }
					if (_SysInfo.nCanMultiCount > 91) { _SysInfo.nMultiSendData[91] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue94.ToString()); }
					if (_SysInfo.nCanMultiCount > 92) { _SysInfo.nMultiSendData[92] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue95.ToString()); }
					if (_SysInfo.nCanMultiCount > 93) { _SysInfo.nMultiSendData[93] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue96.ToString()); }
					if (_SysInfo.nCanMultiCount > 94) { _SysInfo.nMultiSendData[94] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue97.ToString()); }
					if (_SysInfo.nCanMultiCount > 95) { _SysInfo.nMultiSendData[95] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue98.ToString()); }
					if (_SysInfo.nCanMultiCount > 96) { _SysInfo.nMultiSendData[96] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue99.ToString()); }
					if (_SysInfo.nCanMultiCount > 97) { _SysInfo.nMultiSendData[97] = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue100.ToString()); }

					SendTCPMultiCommand(_SysInfo.nCanCh, _SysInfo.nCanAddr, _SysInfo.nMultiSendData, _SysInfo.nCanMultiCount);
					nProcessStep[nStepIndex]++;
					break;

				case 21801:
					_SysInfo.nRepeatWorkStep++;
					nProcessStep[nStepIndex] = 20000;
					break;


				// Modbus TCP Read(COMP)
				case 21900:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].nID.ToString(), out _SysInfo.nCanCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue1.ToString(), out _SysInfo.nCanAddr);
					//int.TryParse(_ModelInfo._TestInfo[_SysInfo.nMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue2.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo.nCanData);
					_SysInfo.nCanData = GetFuncData(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue2.ToString());
					SendTCPReadCommand(_SysInfo.nCanCh, _SysInfo.nCanAddr);

					tMainTimer[nStepIndex].Start(5000);
					nProcessStep[nStepIndex]++;
					break;

				case 21901:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet(_SysInfo.nSubMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), "TIME OUT", "NG");
						_SysInfo.bEolNg = true;
						_SysInfo.nRepeatWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}


					if (_ModbusSoket.bResultOk)
					{

						if (_SysInfo.btTcpReadData[7] == 0x03)
						{
							if ((_SysInfo.btTcpReadData[9] * 0x100) + _SysInfo.btTcpReadData[10] == _SysInfo.nCanData)
							{
								NgDataSet(_SysInfo.nSubMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), ((_SysInfo.btTcpReadData[9] * 0x100) + _SysInfo.btTcpReadData[10]).ToString("X4"), "OK");
								//AppendLogMsg($"OK / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
							}
							else
							{
								_SysInfo.bEolNg = true;
								NgDataSet(_SysInfo.nSubMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), ((_SysInfo.btTcpReadData[9] * 0x100) + _SysInfo.btTcpReadData[10]).ToString("X4"), "NG");
								//AppendLogMsg($"NG / ADDR {_SysInfo.nCanAddr} / DATA {_SysInfo.nCanData:X4}", MSG_TYPE.INFO);
							}
						}
						else
						{
							_SysInfo.bEolNg = true;
							NgDataSet(_SysInfo.nSubMainWorkStep, _SysInfo.nCanAddr.ToString(), _SysInfo.nCanData.ToString("X4"), "DATA ERROR", "NG");
						}
						_SysInfo.nRepeatWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;



				case 22000:
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].nID.ToString(), out _SysInfo.nCanCh);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue1.ToString(), out _SysInfo.nCanAddr);
					int.TryParse(_ModelInfo._TestInfo[_SysInfo.nSubMainWorkStep]._DataInfo[_SysInfo.nRepeatWorkStep].strValue2.ToString(), out _SysInfo.nBuffIndex);
					tMainTimer[nStepIndex].Start(5000);
					SendTCPReadCommand(_SysInfo.nCanCh, _SysInfo.nCanAddr);
					nProcessStep[nStepIndex]++;
					break;

				case 22001:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet(_SysInfo.nSubMainWorkStep, _SysInfo.nCanAddr.ToString(), "", "TIME OUT", "NG");
						_SysInfo.bEolNg = true;
						_SysInfo.nRepeatWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}

					if (_ModbusSoket.bResultOk)
					{
						if (_SysInfo.btTcpReadData[7] == 0x03)
						{
							_SysInfo.nReadDataBuff[_SysInfo.nBuffIndex] = (_SysInfo.btTcpReadData[9] * 0x100) + _SysInfo.btTcpReadData[10];
							NgDataSet(_SysInfo.nSubMainWorkStep, _SysInfo.nCanAddr.ToString(), $"BUFFER{_SysInfo.nBuffIndex}", ((_SysInfo.btTcpReadData[9] * 0x100) + _SysInfo.btTcpReadData[10]).ToString("X4"), "");
						}
						_SysInfo.nRepeatWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;

				// Sub Item 통신 종료
				case 29000:
					_SysInfo.nRepeatWorkStep = 0;
					nProcessStep[nStepIndex] = 11000;
					break;

				case 30000:
					_SysInfo.nSubMainWorkStep = 0;
					nProcessStep[nStepIndex] = 0;
					break;


			}
		}

		public static void SUB_EOL2()
		{

			int nStepIndex = (int)PROC_LIST.SUB_EOL2;

			switch (nProcessStep[nStepIndex])
			{
				case 0:
					break;

				case 10000:

					
					nProcessStep[nStepIndex] = 11000;


					break;

				case 11000:
					if (_SysInfo2.bSubEolStart)
					{
						nProcessStep[nStepIndex] = 20000;
					}
					else
					{
						nProcessStep[nStepIndex] = 30000;
					}
					break;

				case 20000:
					if (_SysInfo2.nRepeatWorkStep >= _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo.Count)
					{
						if (_SysInfo2.bEolNg)
						{
							TestResultSet2(_SysInfo2.nSubMainWorkStep, "", "NG");
						}
						else
						{
							TestResultSet2(_SysInfo2.nSubMainWorkStep, "", "OK");
						}
						_SysInfo2.bEolReadData = false;
						nProcessStep[nStepIndex] = 29000;
					}
					else
					{
						_SysInfo2.bEolReadData = false;
						nProcessStep[nStepIndex] = 21000;
					}
					break;

				case 20050:
					if (_SysInfo2.bSubEolStart)
					{
						nProcessStep[nStepIndex] = 20060;
					}
					else
					{
						nProcessStep[nStepIndex] = 30000;
					}
					break;


				case 20060:
					if (_SysInfo2.nRepeatWorkStep >= _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo.Count)
					{

						_SysInfo2.bEolReadData = false;
						nProcessStep[nStepIndex] = 30000;
					}
					else
					{
						_SysInfo2.bEolReadData = false;
						nProcessStep[nStepIndex] = 21000;
					}
					break;


				case 21000:
					if (_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].nTestType == 0)
					{
						// Modbus Can Write
						nProcessStep[nStepIndex] = 21100;
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].nTestType == 1)
					{
						// Modbus Can Read(Comp)
						nProcessStep[nStepIndex] = 21200;
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].nTestType == 2)
					{
						// Modbus Can Read(Buff)
						nProcessStep[nStepIndex] = 21300;
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].nTestType == 3)
					{
						// Dealy
						nProcessStep[nStepIndex] = 21400;
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].nTestType == 4)
					{
						// Modbus Can Write(Multi)
						nProcessStep[nStepIndex] = 21500;
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].nTestType == 5)
					{
						// Can Write
						nProcessStep[nStepIndex] = 21600;
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].nTestType == 6)
					{
						// Can Read
						nProcessStep[nStepIndex] = 21700;
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].nTestType == 7)
					{
						// TCP Write
						nProcessStep[nStepIndex] = 21800;
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].nTestType == 8)
					{
						// TCP Read ( Comp )
						nProcessStep[nStepIndex] = 21900;
					}
					else if (_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].nTestType == 9)
					{
						// TCP Read ( Buff )
						nProcessStep[nStepIndex] = 22000;
					}
					break;



				// Modbus Can Write
				case 21100:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].nID.ToString(), out _SysInfo2.nCanCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue1.ToString(), out _SysInfo2.nCanAddr);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue2.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nCanData);
					//tMainTimer[nStepIndex].Start(1000);
					SendWriteCommand(_SysInfo2.nCanCh, _SysInfo2.nCanAddr, _SysInfo2.nCanData, 2);
					nProcessStep[nStepIndex]++;
					break;

				case 21101:
					//if (!tMainTimer[nStepIndex].Verify()) { break; }
					//if (_CanComm[_SysInfo2.nCanCh].bReadMessage)
					//{
					//	if (_CanComm[_SysInfo2.nCanCh].btReadData[0] == 0x06)
					//	{
					//		//if ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3] == _SysInfo2.nCanData)
					//		//{

					//		//}
					//		_SysInfo2.nRepeatWorkStep++;
					//		nProcessStep[nStepIndex] = 20000;
					//	}
					//}
					_SysInfo2.nSubEOLWorkStep++;
					nProcessStep[nStepIndex] = 20000;
					break;


				// Modbus Can Read(COMP)
				case 21200:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].nID.ToString(), out _SysInfo2.nCanCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue1.ToString(), out _SysInfo2.nCanAddr);
					//int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue2.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nCanData);
					_SysInfo2.nCanData = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue2.ToString());
					SendReadCommand(_SysInfo2.nCanCh, _SysInfo2.nCanAddr, 2);

					tMainTimer[nStepIndex].Start(5000);
					nProcessStep[nStepIndex]++;
					break;

				case 21201:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet2(_SysInfo2.nSubMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), "TIME OUT", "NG");
						_SysInfo2.bEolNg = true;
						_SysInfo2.nRepeatWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}


					if (_CanComm[_SysInfo2.nCanCh].bReadMessage)
					{
						if (_CanComm[_SysInfo2.nCanCh].btReadData[0] == 0x03)
						{
							if (_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue3 != null && _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue3 != "")
							{
								if (int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue3.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMaskingData))
								{
									_SysInfo2.nCompData = ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3]) & _SysInfo2.nMaskingData;

									if (_SysInfo2.nCompData == _SysInfo2.nCanData)
									{
										NgDataSet2(_SysInfo2.nSubMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3]).ToString("X4") + $"({(_SysInfo2.nMaskingData.ToString("X4"))})", "OK");
										//AppendLogMsg($"OK / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
									}
									else
									{
										_SysInfo2.bEolNg = true;
										NgDataSet2(_SysInfo2.nSubMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3]).ToString("X4") + $"({(_SysInfo2.nMaskingData.ToString("X4"))})", "NG");
										//AppendLogMsg($"NG / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
									}
								}
								else
								{
									_SysInfo2.nCompData = ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3]);
									if (_SysInfo2.nCompData == _SysInfo2.nCanData)
									{
										NgDataSet2(_SysInfo2.nSubMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), _SysInfo2.nCompData.ToString("X4"), "OK");
										//AppendLogMsg($"OK / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
									}
									else
									{
										_SysInfo2.bEolNg = true;
										NgDataSet2(_SysInfo2.nSubMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), _SysInfo2.nCompData.ToString("X4"), "NG");
										//AppendLogMsg($"NG / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
									}
								}
							}
							else
							{
								_SysInfo2.nCompData = ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3]);
								if (_SysInfo2.nCompData == _SysInfo2.nCanData)
								{
									NgDataSet2(_SysInfo2.nSubMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), _SysInfo2.nCompData.ToString("X4"), "OK");
									//AppendLogMsg($"OK / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
								}
								else
								{
									_SysInfo2.bEolNg = true;
									NgDataSet2(_SysInfo2.nSubMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), _SysInfo2.nCompData.ToString("X4"), "NG");
									//AppendLogMsg($"NG / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
								}
							}


						}
						else
						{
							_SysInfo2.bEolNg = true;
							NgDataSet2(_SysInfo2.nSubMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), "DATA ERROR", "NG");
						}
						_SysInfo2.nRepeatWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;

				case 21300:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].nID.ToString(), out _SysInfo2.nCanCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue1.ToString(), out _SysInfo2.nCanAddr);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue2.ToString(), out _SysInfo2.nBuffIndex);
					tMainTimer[nStepIndex].Start(5000);
					SendReadCommand(_SysInfo2.nCanCh, _SysInfo2.nCanAddr, 2);
					nProcessStep[nStepIndex]++;
					break;

				case 21301:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet2(_SysInfo2.nSubMainWorkStep, _SysInfo2.nCanAddr.ToString(), "", "TIME OUT", "NG");
						_SysInfo2.bEolNg = true;
						_SysInfo2.nRepeatWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}

					if (_CanComm[_SysInfo2.nCanCh].bReadMessage)
					{

						if (_CanComm[_SysInfo2.nCanCh].btReadData[0] == 0x03)
						{

							_SysInfo2.nReadDataBuff[_SysInfo2.nBuffIndex] = (_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3];
							NgDataSet2(_SysInfo2.nSubMainWorkStep, _SysInfo2.nCanAddr.ToString(), $"BUFFER{_SysInfo2.nBuffIndex}", ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3]).ToString("X4"), "");
						}
						_SysInfo2.nRepeatWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;


				case 21400:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue1.ToString(), out _SysInfo2.nCanAddr);
					tMainTimer[nStepIndex].Start(_SysInfo2.nCanAddr);
					nProcessStep[nStepIndex]++;
					break;

				case 21401:
					if (!tMainTimer[nStepIndex].Verify()) { break; }

					_SysInfo2.nRepeatWorkStep++;
					nProcessStep[nStepIndex] = 20000;
					break;


				case 21500:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].nID.ToString(), out _SysInfo2.nCanCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue1.ToString(), out _SysInfo2.nCanAddr);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue2.ToString(), out _SysInfo2.nCanMultiCount);
					if (_SysInfo2.nCanMultiCount > 0) { _SysInfo2.nMultiSendData[0] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue3.ToString()); }
					if (_SysInfo2.nCanMultiCount > 1) { _SysInfo2.nMultiSendData[1] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue4.ToString()); }
					if (_SysInfo2.nCanMultiCount > 2) { _SysInfo2.nMultiSendData[2] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue5.ToString()); }
					if (_SysInfo2.nCanMultiCount > 3) { _SysInfo2.nMultiSendData[3] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue6.ToString()); }
					if (_SysInfo2.nCanMultiCount > 4) { _SysInfo2.nMultiSendData[4] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue7.ToString()); }
					if (_SysInfo2.nCanMultiCount > 5) { _SysInfo2.nMultiSendData[5] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue8.ToString()); }
					if (_SysInfo2.nCanMultiCount > 6) { _SysInfo2.nMultiSendData[6] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue9.ToString()); }
					if (_SysInfo2.nCanMultiCount > 7) { _SysInfo2.nMultiSendData[7] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue10.ToString()); }
					if (_SysInfo2.nCanMultiCount > 8) { _SysInfo2.nMultiSendData[8] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue11.ToString()); }
					if (_SysInfo2.nCanMultiCount > 9) { _SysInfo2.nMultiSendData[9] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue12.ToString()); }
					if (_SysInfo2.nCanMultiCount > 10) { _SysInfo2.nMultiSendData[10] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue13.ToString()); }
					if (_SysInfo2.nCanMultiCount > 11) { _SysInfo2.nMultiSendData[11] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue14.ToString()); }
					if (_SysInfo2.nCanMultiCount > 12) { _SysInfo2.nMultiSendData[12] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue15.ToString()); }
					if (_SysInfo2.nCanMultiCount > 13) { _SysInfo2.nMultiSendData[13] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue16.ToString()); }
					if (_SysInfo2.nCanMultiCount > 14) { _SysInfo2.nMultiSendData[14] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue17.ToString()); }
					if (_SysInfo2.nCanMultiCount > 15) { _SysInfo2.nMultiSendData[15] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue18.ToString()); }
					if (_SysInfo2.nCanMultiCount > 16) { _SysInfo2.nMultiSendData[16] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue19.ToString()); }
					if (_SysInfo2.nCanMultiCount > 17) { _SysInfo2.nMultiSendData[17] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue20.ToString()); }
					if (_SysInfo2.nCanMultiCount > 18) { _SysInfo2.nMultiSendData[18] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue21.ToString()); }
					if (_SysInfo2.nCanMultiCount > 19) { _SysInfo2.nMultiSendData[19] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue22.ToString()); }
					if (_SysInfo2.nCanMultiCount > 20) { _SysInfo2.nMultiSendData[20] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue23.ToString()); }
					if (_SysInfo2.nCanMultiCount > 21) { _SysInfo2.nMultiSendData[21] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue24.ToString()); }
					if (_SysInfo2.nCanMultiCount > 22) { _SysInfo2.nMultiSendData[22] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue25.ToString()); }
					if (_SysInfo2.nCanMultiCount > 23) { _SysInfo2.nMultiSendData[23] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue26.ToString()); }
					if (_SysInfo2.nCanMultiCount > 24) { _SysInfo2.nMultiSendData[24] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue27.ToString()); }
					if (_SysInfo2.nCanMultiCount > 25) { _SysInfo2.nMultiSendData[25] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue28.ToString()); }
					if (_SysInfo2.nCanMultiCount > 26) { _SysInfo2.nMultiSendData[26] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue29.ToString()); }
					if (_SysInfo2.nCanMultiCount > 27) { _SysInfo2.nMultiSendData[27] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue30.ToString()); }
					if (_SysInfo2.nCanMultiCount > 28) { _SysInfo2.nMultiSendData[28] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue31.ToString()); }
					if (_SysInfo2.nCanMultiCount > 29) { _SysInfo2.nMultiSendData[29] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue32.ToString()); }
					if (_SysInfo2.nCanMultiCount > 30) { _SysInfo2.nMultiSendData[30] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue33.ToString()); }
					if (_SysInfo2.nCanMultiCount > 31) { _SysInfo2.nMultiSendData[31] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue34.ToString()); }
					if (_SysInfo2.nCanMultiCount > 32) { _SysInfo2.nMultiSendData[32] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue35.ToString()); }
					if (_SysInfo2.nCanMultiCount > 33) { _SysInfo2.nMultiSendData[33] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue36.ToString()); }
					if (_SysInfo2.nCanMultiCount > 34) { _SysInfo2.nMultiSendData[34] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue37.ToString()); }
					if (_SysInfo2.nCanMultiCount > 35) { _SysInfo2.nMultiSendData[35] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue38.ToString()); }
					if (_SysInfo2.nCanMultiCount > 36) { _SysInfo2.nMultiSendData[36] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue39.ToString()); }
					if (_SysInfo2.nCanMultiCount > 37) { _SysInfo2.nMultiSendData[37] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue40.ToString()); }
					if (_SysInfo2.nCanMultiCount > 38) { _SysInfo2.nMultiSendData[38] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue41.ToString()); }
					if (_SysInfo2.nCanMultiCount > 39) { _SysInfo2.nMultiSendData[39] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue42.ToString()); }
					if (_SysInfo2.nCanMultiCount > 40) { _SysInfo2.nMultiSendData[40] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue43.ToString()); }
					if (_SysInfo2.nCanMultiCount > 41) { _SysInfo2.nMultiSendData[41] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue44.ToString()); }
					if (_SysInfo2.nCanMultiCount > 42) { _SysInfo2.nMultiSendData[42] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue45.ToString()); }
					if (_SysInfo2.nCanMultiCount > 43) { _SysInfo2.nMultiSendData[43] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue46.ToString()); }
					if (_SysInfo2.nCanMultiCount > 44) { _SysInfo2.nMultiSendData[44] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue47.ToString()); }
					if (_SysInfo2.nCanMultiCount > 45) { _SysInfo2.nMultiSendData[45] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue48.ToString()); }
					if (_SysInfo2.nCanMultiCount > 46) { _SysInfo2.nMultiSendData[46] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue49.ToString()); }
					if (_SysInfo2.nCanMultiCount > 47) { _SysInfo2.nMultiSendData[47] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue50.ToString()); }
					if (_SysInfo2.nCanMultiCount > 48) { _SysInfo2.nMultiSendData[48] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue51.ToString()); }
					if (_SysInfo2.nCanMultiCount > 49) { _SysInfo2.nMultiSendData[49] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue52.ToString()); }
					if (_SysInfo2.nCanMultiCount > 50) { _SysInfo2.nMultiSendData[50] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue53.ToString()); }
					if (_SysInfo2.nCanMultiCount > 51) { _SysInfo2.nMultiSendData[51] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue54.ToString()); }
					if (_SysInfo2.nCanMultiCount > 52) { _SysInfo2.nMultiSendData[52] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue55.ToString()); }
					if (_SysInfo2.nCanMultiCount > 53) { _SysInfo2.nMultiSendData[53] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue56.ToString()); }
					if (_SysInfo2.nCanMultiCount > 54) { _SysInfo2.nMultiSendData[54] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue57.ToString()); }
					if (_SysInfo2.nCanMultiCount > 55) { _SysInfo2.nMultiSendData[55] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue58.ToString()); }
					if (_SysInfo2.nCanMultiCount > 56) { _SysInfo2.nMultiSendData[56] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue59.ToString()); }
					if (_SysInfo2.nCanMultiCount > 57) { _SysInfo2.nMultiSendData[57] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue60.ToString()); }
					if (_SysInfo2.nCanMultiCount > 58) { _SysInfo2.nMultiSendData[58] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue61.ToString()); }
					if (_SysInfo2.nCanMultiCount > 59) { _SysInfo2.nMultiSendData[59] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue62.ToString()); }
					if (_SysInfo2.nCanMultiCount > 60) { _SysInfo2.nMultiSendData[60] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue63.ToString()); }
					if (_SysInfo2.nCanMultiCount > 61) { _SysInfo2.nMultiSendData[61] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue64.ToString()); }
					if (_SysInfo2.nCanMultiCount > 62) { _SysInfo2.nMultiSendData[62] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue65.ToString()); }
					if (_SysInfo2.nCanMultiCount > 63) { _SysInfo2.nMultiSendData[63] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue66.ToString()); }
					if (_SysInfo2.nCanMultiCount > 64) { _SysInfo2.nMultiSendData[64] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue67.ToString()); }
					if (_SysInfo2.nCanMultiCount > 65) { _SysInfo2.nMultiSendData[65] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue68.ToString()); }
					if (_SysInfo2.nCanMultiCount > 66) { _SysInfo2.nMultiSendData[66] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue69.ToString()); }
					if (_SysInfo2.nCanMultiCount > 67) { _SysInfo2.nMultiSendData[67] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue70.ToString()); }
					if (_SysInfo2.nCanMultiCount > 68) { _SysInfo2.nMultiSendData[68] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue71.ToString()); }
					if (_SysInfo2.nCanMultiCount > 69) { _SysInfo2.nMultiSendData[69] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue72.ToString()); }
					if (_SysInfo2.nCanMultiCount > 70) { _SysInfo2.nMultiSendData[70] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue73.ToString()); }
					if (_SysInfo2.nCanMultiCount > 71) { _SysInfo2.nMultiSendData[71] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue74.ToString()); }
					if (_SysInfo2.nCanMultiCount > 72) { _SysInfo2.nMultiSendData[72] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue75.ToString()); }
					if (_SysInfo2.nCanMultiCount > 73) { _SysInfo2.nMultiSendData[73] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue76.ToString()); }
					if (_SysInfo2.nCanMultiCount > 74) { _SysInfo2.nMultiSendData[74] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue77.ToString()); }
					if (_SysInfo2.nCanMultiCount > 75) { _SysInfo2.nMultiSendData[75] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue78.ToString()); }
					if (_SysInfo2.nCanMultiCount > 76) { _SysInfo2.nMultiSendData[76] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue79.ToString()); }
					if (_SysInfo2.nCanMultiCount > 77) { _SysInfo2.nMultiSendData[77] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue80.ToString()); }
					if (_SysInfo2.nCanMultiCount > 78) { _SysInfo2.nMultiSendData[78] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue81.ToString()); }
					if (_SysInfo2.nCanMultiCount > 79) { _SysInfo2.nMultiSendData[79] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue82.ToString()); }
					if (_SysInfo2.nCanMultiCount > 80) { _SysInfo2.nMultiSendData[80] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue83.ToString()); }
					if (_SysInfo2.nCanMultiCount > 81) { _SysInfo2.nMultiSendData[81] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue84.ToString()); }
					if (_SysInfo2.nCanMultiCount > 82) { _SysInfo2.nMultiSendData[82] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue85.ToString()); }
					if (_SysInfo2.nCanMultiCount > 83) { _SysInfo2.nMultiSendData[83] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue86.ToString()); }
					if (_SysInfo2.nCanMultiCount > 84) { _SysInfo2.nMultiSendData[84] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue87.ToString()); }
					if (_SysInfo2.nCanMultiCount > 85) { _SysInfo2.nMultiSendData[85] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue88.ToString()); }
					if (_SysInfo2.nCanMultiCount > 86) { _SysInfo2.nMultiSendData[86] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue89.ToString()); }
					if (_SysInfo2.nCanMultiCount > 87) { _SysInfo2.nMultiSendData[87] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue90.ToString()); }
					if (_SysInfo2.nCanMultiCount > 88) { _SysInfo2.nMultiSendData[88] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue91.ToString()); }
					if (_SysInfo2.nCanMultiCount > 89) { _SysInfo2.nMultiSendData[89] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue92.ToString()); }
					if (_SysInfo2.nCanMultiCount > 90) { _SysInfo2.nMultiSendData[90] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue93.ToString()); }
					if (_SysInfo2.nCanMultiCount > 91) { _SysInfo2.nMultiSendData[91] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue94.ToString()); }
					if (_SysInfo2.nCanMultiCount > 92) { _SysInfo2.nMultiSendData[92] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue95.ToString()); }
					if (_SysInfo2.nCanMultiCount > 93) { _SysInfo2.nMultiSendData[93] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue96.ToString()); }
					if (_SysInfo2.nCanMultiCount > 94) { _SysInfo2.nMultiSendData[94] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue97.ToString()); }
					if (_SysInfo2.nCanMultiCount > 95) { _SysInfo2.nMultiSendData[95] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue98.ToString()); }
					if (_SysInfo2.nCanMultiCount > 96) { _SysInfo2.nMultiSendData[96] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue99.ToString()); }
					if (_SysInfo2.nCanMultiCount > 97) { _SysInfo2.nMultiSendData[97] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue100.ToString()); }

					//if (_SysInfo2.nCanMultiCount > 0) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue3.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[0]); }
					//if (_SysInfo2.nCanMultiCount > 1) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue4.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[1]); }
					//if (_SysInfo2.nCanMultiCount > 2) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue5.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[2]); }
					//if (_SysInfo2.nCanMultiCount > 3) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue6.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[3]); }
					//if (_SysInfo2.nCanMultiCount > 4) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue7.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[4]); }
					//if (_SysInfo2.nCanMultiCount > 5) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue8.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[5]); }
					//if (_SysInfo2.nCanMultiCount > 6) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue9.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[6]); }
					//if (_SysInfo2.nCanMultiCount > 7) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue10.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[7]); }
					//if (_SysInfo2.nCanMultiCount > 8) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue11.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[8]); }
					//if (_SysInfo2.nCanMultiCount > 9) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue12.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[9]); }
					//if (_SysInfo2.nCanMultiCount > 10) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue13.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[10]); }
					//if (_SysInfo2.nCanMultiCount > 11) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue14.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[11]); }
					//if (_SysInfo2.nCanMultiCount > 12) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue15.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[12]); }
					//if (_SysInfo2.nCanMultiCount > 13) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue16.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[13]); }
					//if (_SysInfo2.nCanMultiCount > 14) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue17.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[14]); }
					//if (_SysInfo2.nCanMultiCount > 15) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue18.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[15]); }
					//if (_SysInfo2.nCanMultiCount > 16) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue19.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[16]); }
					//if (_SysInfo2.nCanMultiCount > 17) { int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue20.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nMultiSendData[17]); }

					//tMainTimer[nStepIndex].Start(1000);
					SendWriteMultiCommand(_SysInfo2.nCanCh, _SysInfo2.nCanAddr, _SysInfo2.nMultiSendData, _SysInfo2.nCanMultiCount, 2);
					nProcessStep[nStepIndex]++;
					break;

				case 21501:
					//if (!tMainTimer[nStepIndex].Verify()) { break; }
					//if (_CanComm[_SysInfo2.nCanCh].bReadMessage)
					//{
					//	if (_CanComm[_SysInfo2.nCanCh].btReadData[0] == 0x06)
					//	{
					//		//if ((_CanComm[_SysInfo2.nCanCh].btReadData[2] * 0x100) + _CanComm[_SysInfo2.nCanCh].btReadData[3] == _SysInfo2.nCanData)
					//		//{

					//		//}
					//		_SysInfo2.nRepeatWorkStep++;
					//		nProcessStep[nStepIndex] = 20000;
					//	}
					//}
					_SysInfo2.nRepeatWorkStep++;
					nProcessStep[nStepIndex] = 20000;
					break;


				// Can Write
				case 21600:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].nID.ToString(), out _SysInfo2.nCanCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue1, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nCanStartAddr);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue2.ToString(), out _SysInfo2.nCanMultiCount);
					if (_SysInfo2.nCanMultiCount > 0) { _SysInfo2.nMultiSendData[0] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue3.ToString()); }
					if (_SysInfo2.nCanMultiCount > 1) { _SysInfo2.nMultiSendData[1] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue4.ToString()); }
					if (_SysInfo2.nCanMultiCount > 2) { _SysInfo2.nMultiSendData[2] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue5.ToString()); }
					if (_SysInfo2.nCanMultiCount > 3) { _SysInfo2.nMultiSendData[3] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue6.ToString()); }
					if (_SysInfo2.nCanMultiCount > 4) { _SysInfo2.nMultiSendData[4] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue7.ToString()); }
					if (_SysInfo2.nCanMultiCount > 5) { _SysInfo2.nMultiSendData[5] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue8.ToString()); }
					if (_SysInfo2.nCanMultiCount > 6) { _SysInfo2.nMultiSendData[6] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue9.ToString()); }
					if (_SysInfo2.nCanMultiCount > 7) { _SysInfo2.nMultiSendData[7] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue10.ToString()); }
					if (_SysInfo2.nCanMultiCount > 8) { _SysInfo2.nMultiSendData[8] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue11.ToString()); }
					if (_SysInfo2.nCanMultiCount > 9) { _SysInfo2.nMultiSendData[9] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue12.ToString()); }
					if (_SysInfo2.nCanMultiCount > 10) { _SysInfo2.nMultiSendData[10] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue13.ToString()); }
					if (_SysInfo2.nCanMultiCount > 11) { _SysInfo2.nMultiSendData[11] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue14.ToString()); }
					if (_SysInfo2.nCanMultiCount > 12) { _SysInfo2.nMultiSendData[12] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue15.ToString()); }
					if (_SysInfo2.nCanMultiCount > 13) { _SysInfo2.nMultiSendData[13] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue16.ToString()); }
					if (_SysInfo2.nCanMultiCount > 14) { _SysInfo2.nMultiSendData[14] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue17.ToString()); }
					if (_SysInfo2.nCanMultiCount > 15) { _SysInfo2.nMultiSendData[15] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue18.ToString()); }
					if (_SysInfo2.nCanMultiCount > 16) { _SysInfo2.nMultiSendData[16] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue19.ToString()); }
					if (_SysInfo2.nCanMultiCount > 17) { _SysInfo2.nMultiSendData[17] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue20.ToString()); }
					if (_SysInfo2.nCanMultiCount > 18) { _SysInfo2.nMultiSendData[18] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue21.ToString()); }
					if (_SysInfo2.nCanMultiCount > 19) { _SysInfo2.nMultiSendData[19] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue22.ToString()); }
					if (_SysInfo2.nCanMultiCount > 20) { _SysInfo2.nMultiSendData[20] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue23.ToString()); }
					if (_SysInfo2.nCanMultiCount > 21) { _SysInfo2.nMultiSendData[21] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue24.ToString()); }
					if (_SysInfo2.nCanMultiCount > 22) { _SysInfo2.nMultiSendData[22] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue25.ToString()); }
					if (_SysInfo2.nCanMultiCount > 23) { _SysInfo2.nMultiSendData[23] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue26.ToString()); }
					if (_SysInfo2.nCanMultiCount > 24) { _SysInfo2.nMultiSendData[24] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue27.ToString()); }
					if (_SysInfo2.nCanMultiCount > 25) { _SysInfo2.nMultiSendData[25] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue28.ToString()); }
					if (_SysInfo2.nCanMultiCount > 26) { _SysInfo2.nMultiSendData[26] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue29.ToString()); }
					if (_SysInfo2.nCanMultiCount > 27) { _SysInfo2.nMultiSendData[27] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue30.ToString()); }
					if (_SysInfo2.nCanMultiCount > 28) { _SysInfo2.nMultiSendData[28] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue31.ToString()); }
					if (_SysInfo2.nCanMultiCount > 29) { _SysInfo2.nMultiSendData[29] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue32.ToString()); }
					if (_SysInfo2.nCanMultiCount > 30) { _SysInfo2.nMultiSendData[30] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue33.ToString()); }
					if (_SysInfo2.nCanMultiCount > 31) { _SysInfo2.nMultiSendData[31] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue34.ToString()); }
					if (_SysInfo2.nCanMultiCount > 32) { _SysInfo2.nMultiSendData[32] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue35.ToString()); }
					if (_SysInfo2.nCanMultiCount > 33) { _SysInfo2.nMultiSendData[33] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue36.ToString()); }
					if (_SysInfo2.nCanMultiCount > 34) { _SysInfo2.nMultiSendData[34] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue37.ToString()); }
					if (_SysInfo2.nCanMultiCount > 35) { _SysInfo2.nMultiSendData[35] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue38.ToString()); }
					if (_SysInfo2.nCanMultiCount > 36) { _SysInfo2.nMultiSendData[36] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue39.ToString()); }
					if (_SysInfo2.nCanMultiCount > 37) { _SysInfo2.nMultiSendData[37] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue40.ToString()); }
					if (_SysInfo2.nCanMultiCount > 38) { _SysInfo2.nMultiSendData[38] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue41.ToString()); }
					if (_SysInfo2.nCanMultiCount > 39) { _SysInfo2.nMultiSendData[39] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue42.ToString()); }
					if (_SysInfo2.nCanMultiCount > 40) { _SysInfo2.nMultiSendData[40] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue43.ToString()); }
					if (_SysInfo2.nCanMultiCount > 41) { _SysInfo2.nMultiSendData[41] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue44.ToString()); }
					if (_SysInfo2.nCanMultiCount > 42) { _SysInfo2.nMultiSendData[42] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue45.ToString()); }
					if (_SysInfo2.nCanMultiCount > 43) { _SysInfo2.nMultiSendData[43] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue46.ToString()); }
					if (_SysInfo2.nCanMultiCount > 44) { _SysInfo2.nMultiSendData[44] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue47.ToString()); }
					if (_SysInfo2.nCanMultiCount > 45) { _SysInfo2.nMultiSendData[45] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue48.ToString()); }
					if (_SysInfo2.nCanMultiCount > 46) { _SysInfo2.nMultiSendData[46] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue49.ToString()); }
					if (_SysInfo2.nCanMultiCount > 47) { _SysInfo2.nMultiSendData[47] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue50.ToString()); }
					if (_SysInfo2.nCanMultiCount > 48) { _SysInfo2.nMultiSendData[48] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue51.ToString()); }
					if (_SysInfo2.nCanMultiCount > 49) { _SysInfo2.nMultiSendData[49] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue52.ToString()); }
					if (_SysInfo2.nCanMultiCount > 50) { _SysInfo2.nMultiSendData[50] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue53.ToString()); }
					if (_SysInfo2.nCanMultiCount > 51) { _SysInfo2.nMultiSendData[51] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue54.ToString()); }
					if (_SysInfo2.nCanMultiCount > 52) { _SysInfo2.nMultiSendData[52] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue55.ToString()); }
					if (_SysInfo2.nCanMultiCount > 53) { _SysInfo2.nMultiSendData[53] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue56.ToString()); }
					if (_SysInfo2.nCanMultiCount > 54) { _SysInfo2.nMultiSendData[54] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue57.ToString()); }
					if (_SysInfo2.nCanMultiCount > 55) { _SysInfo2.nMultiSendData[55] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue58.ToString()); }
					if (_SysInfo2.nCanMultiCount > 56) { _SysInfo2.nMultiSendData[56] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue59.ToString()); }
					if (_SysInfo2.nCanMultiCount > 57) { _SysInfo2.nMultiSendData[57] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue60.ToString()); }
					if (_SysInfo2.nCanMultiCount > 58) { _SysInfo2.nMultiSendData[58] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue61.ToString()); }
					if (_SysInfo2.nCanMultiCount > 59) { _SysInfo2.nMultiSendData[59] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue62.ToString()); }
					if (_SysInfo2.nCanMultiCount > 60) { _SysInfo2.nMultiSendData[60] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue63.ToString()); }
					if (_SysInfo2.nCanMultiCount > 61) { _SysInfo2.nMultiSendData[61] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue64.ToString()); }
					if (_SysInfo2.nCanMultiCount > 62) { _SysInfo2.nMultiSendData[62] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue65.ToString()); }
					if (_SysInfo2.nCanMultiCount > 63) { _SysInfo2.nMultiSendData[63] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue66.ToString()); }
					if (_SysInfo2.nCanMultiCount > 64) { _SysInfo2.nMultiSendData[64] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue67.ToString()); }
					if (_SysInfo2.nCanMultiCount > 65) { _SysInfo2.nMultiSendData[65] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue68.ToString()); }
					if (_SysInfo2.nCanMultiCount > 66) { _SysInfo2.nMultiSendData[66] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue69.ToString()); }
					if (_SysInfo2.nCanMultiCount > 67) { _SysInfo2.nMultiSendData[67] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue70.ToString()); }
					if (_SysInfo2.nCanMultiCount > 68) { _SysInfo2.nMultiSendData[68] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue71.ToString()); }
					if (_SysInfo2.nCanMultiCount > 69) { _SysInfo2.nMultiSendData[69] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue72.ToString()); }
					if (_SysInfo2.nCanMultiCount > 70) { _SysInfo2.nMultiSendData[70] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue73.ToString()); }
					if (_SysInfo2.nCanMultiCount > 71) { _SysInfo2.nMultiSendData[71] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue74.ToString()); }
					if (_SysInfo2.nCanMultiCount > 72) { _SysInfo2.nMultiSendData[72] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue75.ToString()); }
					if (_SysInfo2.nCanMultiCount > 73) { _SysInfo2.nMultiSendData[73] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue76.ToString()); }
					if (_SysInfo2.nCanMultiCount > 74) { _SysInfo2.nMultiSendData[74] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue77.ToString()); }
					if (_SysInfo2.nCanMultiCount > 75) { _SysInfo2.nMultiSendData[75] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue78.ToString()); }
					if (_SysInfo2.nCanMultiCount > 76) { _SysInfo2.nMultiSendData[76] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue79.ToString()); }
					if (_SysInfo2.nCanMultiCount > 77) { _SysInfo2.nMultiSendData[77] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue80.ToString()); }
					if (_SysInfo2.nCanMultiCount > 78) { _SysInfo2.nMultiSendData[78] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue81.ToString()); }
					if (_SysInfo2.nCanMultiCount > 79) { _SysInfo2.nMultiSendData[79] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue82.ToString()); }
					if (_SysInfo2.nCanMultiCount > 80) { _SysInfo2.nMultiSendData[80] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue83.ToString()); }
					if (_SysInfo2.nCanMultiCount > 81) { _SysInfo2.nMultiSendData[81] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue84.ToString()); }
					if (_SysInfo2.nCanMultiCount > 82) { _SysInfo2.nMultiSendData[82] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue85.ToString()); }
					if (_SysInfo2.nCanMultiCount > 83) { _SysInfo2.nMultiSendData[83] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue86.ToString()); }
					if (_SysInfo2.nCanMultiCount > 84) { _SysInfo2.nMultiSendData[84] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue87.ToString()); }
					if (_SysInfo2.nCanMultiCount > 85) { _SysInfo2.nMultiSendData[85] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue88.ToString()); }
					if (_SysInfo2.nCanMultiCount > 86) { _SysInfo2.nMultiSendData[86] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue89.ToString()); }
					if (_SysInfo2.nCanMultiCount > 87) { _SysInfo2.nMultiSendData[87] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue90.ToString()); }
					if (_SysInfo2.nCanMultiCount > 88) { _SysInfo2.nMultiSendData[88] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue91.ToString()); }
					if (_SysInfo2.nCanMultiCount > 89) { _SysInfo2.nMultiSendData[89] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue92.ToString()); }
					if (_SysInfo2.nCanMultiCount > 90) { _SysInfo2.nMultiSendData[90] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue93.ToString()); }
					if (_SysInfo2.nCanMultiCount > 91) { _SysInfo2.nMultiSendData[91] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue94.ToString()); }
					if (_SysInfo2.nCanMultiCount > 92) { _SysInfo2.nMultiSendData[92] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue95.ToString()); }
					if (_SysInfo2.nCanMultiCount > 93) { _SysInfo2.nMultiSendData[93] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue96.ToString()); }
					if (_SysInfo2.nCanMultiCount > 94) { _SysInfo2.nMultiSendData[94] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue97.ToString()); }
					if (_SysInfo2.nCanMultiCount > 95) { _SysInfo2.nMultiSendData[95] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue98.ToString()); }
					if (_SysInfo2.nCanMultiCount > 96) { _SysInfo2.nMultiSendData[96] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue99.ToString()); }
					if (_SysInfo2.nCanMultiCount > 97) { _SysInfo2.nMultiSendData[97] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue100.ToString()); }
					SendCanData(_SysInfo2.nCanCh, _SysInfo2.nCanStartAddr, _SysInfo2.nMultiSendData, _SysInfo2.nCanMultiCount, 2);
					_SysInfo2.nRepeatWorkStep++;
					nProcessStep[nStepIndex] = 20000;
					break;

				case 21700:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].nID.ToString(), out _SysInfo2.nCanCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue1, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nCanStartAddr);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue2.ToString(), out _SysInfo2.nCanMultiCount);
					if (_SysInfo2.nCanMultiCount > 0) { _SysInfo2.strValueBuff[0] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue3; }
					if (_SysInfo2.nCanMultiCount > 1) { _SysInfo2.strValueBuff[1] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue4; }
					if (_SysInfo2.nCanMultiCount > 2) { _SysInfo2.strValueBuff[2] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue5; }
					if (_SysInfo2.nCanMultiCount > 3) { _SysInfo2.strValueBuff[3] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue6; }
					if (_SysInfo2.nCanMultiCount > 4) { _SysInfo2.strValueBuff[4] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue7; }
					if (_SysInfo2.nCanMultiCount > 5) { _SysInfo2.strValueBuff[5] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue8; }
					if (_SysInfo2.nCanMultiCount > 6) { _SysInfo2.strValueBuff[6] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue9; }
					if (_SysInfo2.nCanMultiCount > 7) { _SysInfo2.strValueBuff[7] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue10; }
					if (_SysInfo2.nCanMultiCount > 8) { _SysInfo2.strValueBuff[8] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue11; }
					if (_SysInfo2.nCanMultiCount > 9) { _SysInfo2.strValueBuff[9] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue12; }
					if (_SysInfo2.nCanMultiCount > 10) { _SysInfo2.strValueBuff[10] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue13; }
					if (_SysInfo2.nCanMultiCount > 11) { _SysInfo2.strValueBuff[11] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue14; }
					if (_SysInfo2.nCanMultiCount > 12) { _SysInfo2.strValueBuff[12] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue15; }
					if (_SysInfo2.nCanMultiCount > 13) { _SysInfo2.strValueBuff[13] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue16; }
					if (_SysInfo2.nCanMultiCount > 14) { _SysInfo2.strValueBuff[14] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue17; }
					if (_SysInfo2.nCanMultiCount > 15) { _SysInfo2.strValueBuff[15] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue18; }
					if (_SysInfo2.nCanMultiCount > 16) { _SysInfo2.strValueBuff[16] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue19; }
					if (_SysInfo2.nCanMultiCount > 17) { _SysInfo2.strValueBuff[17] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue20; }
					if (_SysInfo2.nCanMultiCount > 18) { _SysInfo2.strValueBuff[18] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue21; }
					if (_SysInfo2.nCanMultiCount > 19) { _SysInfo2.strValueBuff[19] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue22; }
					if (_SysInfo2.nCanMultiCount > 20) { _SysInfo2.strValueBuff[20] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue23; }
					if (_SysInfo2.nCanMultiCount > 21) { _SysInfo2.strValueBuff[21] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue24; }
					if (_SysInfo2.nCanMultiCount > 22) { _SysInfo2.strValueBuff[22] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue25; }
					if (_SysInfo2.nCanMultiCount > 23) { _SysInfo2.strValueBuff[23] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue26; }
					if (_SysInfo2.nCanMultiCount > 24) { _SysInfo2.strValueBuff[24] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue27; }
					if (_SysInfo2.nCanMultiCount > 25) { _SysInfo2.strValueBuff[25] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue28; }
					if (_SysInfo2.nCanMultiCount > 26) { _SysInfo2.strValueBuff[26] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue29; }
					if (_SysInfo2.nCanMultiCount > 27) { _SysInfo2.strValueBuff[27] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue30; }
					if (_SysInfo2.nCanMultiCount > 28) { _SysInfo2.strValueBuff[28] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue31; }
					if (_SysInfo2.nCanMultiCount > 29) { _SysInfo2.strValueBuff[29] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue32; }
					if (_SysInfo2.nCanMultiCount > 30) { _SysInfo2.strValueBuff[30] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue33; }
					if (_SysInfo2.nCanMultiCount > 31) { _SysInfo2.strValueBuff[31] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue34; }
					if (_SysInfo2.nCanMultiCount > 32) { _SysInfo2.strValueBuff[32] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue35; }
					if (_SysInfo2.nCanMultiCount > 33) { _SysInfo2.strValueBuff[33] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue36; }
					if (_SysInfo2.nCanMultiCount > 34) { _SysInfo2.strValueBuff[34] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue37; }
					if (_SysInfo2.nCanMultiCount > 35) { _SysInfo2.strValueBuff[35] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue38; }
					if (_SysInfo2.nCanMultiCount > 36) { _SysInfo2.strValueBuff[36] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue39; }
					if (_SysInfo2.nCanMultiCount > 37) { _SysInfo2.strValueBuff[37] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue40; }
					if (_SysInfo2.nCanMultiCount > 38) { _SysInfo2.strValueBuff[38] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue41; }
					if (_SysInfo2.nCanMultiCount > 39) { _SysInfo2.strValueBuff[39] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue42; }
					if (_SysInfo2.nCanMultiCount > 40) { _SysInfo2.strValueBuff[40] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue43; }
					if (_SysInfo2.nCanMultiCount > 41) { _SysInfo2.strValueBuff[41] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue44; }
					if (_SysInfo2.nCanMultiCount > 42) { _SysInfo2.strValueBuff[42] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue45; }
					if (_SysInfo2.nCanMultiCount > 43) { _SysInfo2.strValueBuff[43] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue46; }
					if (_SysInfo2.nCanMultiCount > 44) { _SysInfo2.strValueBuff[44] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue47; }
					if (_SysInfo2.nCanMultiCount > 45) { _SysInfo2.strValueBuff[45] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue48; }
					if (_SysInfo2.nCanMultiCount > 46) { _SysInfo2.strValueBuff[46] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue49; }
					if (_SysInfo2.nCanMultiCount > 47) { _SysInfo2.strValueBuff[47] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue50; }
					if (_SysInfo2.nCanMultiCount > 48) { _SysInfo2.strValueBuff[48] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue51; }
					if (_SysInfo2.nCanMultiCount > 49) { _SysInfo2.strValueBuff[49] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue52; }
					if (_SysInfo2.nCanMultiCount > 50) { _SysInfo2.strValueBuff[50] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue53; }
					if (_SysInfo2.nCanMultiCount > 51) { _SysInfo2.strValueBuff[51] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue54; }
					if (_SysInfo2.nCanMultiCount > 52) { _SysInfo2.strValueBuff[52] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue55; }
					if (_SysInfo2.nCanMultiCount > 53) { _SysInfo2.strValueBuff[53] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue56; }
					if (_SysInfo2.nCanMultiCount > 54) { _SysInfo2.strValueBuff[54] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue57; }
					if (_SysInfo2.nCanMultiCount > 55) { _SysInfo2.strValueBuff[55] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue58; }
					if (_SysInfo2.nCanMultiCount > 56) { _SysInfo2.strValueBuff[56] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue59; }
					if (_SysInfo2.nCanMultiCount > 57) { _SysInfo2.strValueBuff[57] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue60; }
					if (_SysInfo2.nCanMultiCount > 58) { _SysInfo2.strValueBuff[58] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue61; }
					if (_SysInfo2.nCanMultiCount > 59) { _SysInfo2.strValueBuff[59] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue62; }
					if (_SysInfo2.nCanMultiCount > 60) { _SysInfo2.strValueBuff[60] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue63; }
					if (_SysInfo2.nCanMultiCount > 61) { _SysInfo2.strValueBuff[61] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue64; }
					if (_SysInfo2.nCanMultiCount > 62) { _SysInfo2.strValueBuff[62] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue65; }
					if (_SysInfo2.nCanMultiCount > 63) { _SysInfo2.strValueBuff[63] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue66; }
					if (_SysInfo2.nCanMultiCount > 64) { _SysInfo2.strValueBuff[64] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue67; }
					if (_SysInfo2.nCanMultiCount > 65) { _SysInfo2.strValueBuff[65] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue68; }
					if (_SysInfo2.nCanMultiCount > 66) { _SysInfo2.strValueBuff[66] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue69; }
					if (_SysInfo2.nCanMultiCount > 67) { _SysInfo2.strValueBuff[67] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue70; }
					if (_SysInfo2.nCanMultiCount > 68) { _SysInfo2.strValueBuff[68] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue71; }
					if (_SysInfo2.nCanMultiCount > 69) { _SysInfo2.strValueBuff[69] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue72; }
					if (_SysInfo2.nCanMultiCount > 70) { _SysInfo2.strValueBuff[70] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue73; }
					if (_SysInfo2.nCanMultiCount > 71) { _SysInfo2.strValueBuff[71] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue74; }
					if (_SysInfo2.nCanMultiCount > 72) { _SysInfo2.strValueBuff[72] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue75; }
					if (_SysInfo2.nCanMultiCount > 73) { _SysInfo2.strValueBuff[73] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue76; }
					if (_SysInfo2.nCanMultiCount > 74) { _SysInfo2.strValueBuff[74] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue77; }
					if (_SysInfo2.nCanMultiCount > 75) { _SysInfo2.strValueBuff[75] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue78; }
					if (_SysInfo2.nCanMultiCount > 76) { _SysInfo2.strValueBuff[76] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue79; }
					if (_SysInfo2.nCanMultiCount > 77) { _SysInfo2.strValueBuff[77] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue80; }
					if (_SysInfo2.nCanMultiCount > 78) { _SysInfo2.strValueBuff[78] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue81; }
					if (_SysInfo2.nCanMultiCount > 79) { _SysInfo2.strValueBuff[79] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue82; }
					if (_SysInfo2.nCanMultiCount > 80) { _SysInfo2.strValueBuff[80] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue83; }
					if (_SysInfo2.nCanMultiCount > 81) { _SysInfo2.strValueBuff[81] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue84; }
					if (_SysInfo2.nCanMultiCount > 82) { _SysInfo2.strValueBuff[82] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue85; }
					if (_SysInfo2.nCanMultiCount > 83) { _SysInfo2.strValueBuff[83] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue86; }
					if (_SysInfo2.nCanMultiCount > 84) { _SysInfo2.strValueBuff[84] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue87; }
					if (_SysInfo2.nCanMultiCount > 85) { _SysInfo2.strValueBuff[85] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue88; }
					if (_SysInfo2.nCanMultiCount > 86) { _SysInfo2.strValueBuff[86] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue89; }
					if (_SysInfo2.nCanMultiCount > 87) { _SysInfo2.strValueBuff[87] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue90; }
					if (_SysInfo2.nCanMultiCount > 88) { _SysInfo2.strValueBuff[88] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue91; }
					if (_SysInfo2.nCanMultiCount > 89) { _SysInfo2.strValueBuff[89] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue92; }
					if (_SysInfo2.nCanMultiCount > 90) { _SysInfo2.strValueBuff[90] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue93; }
					if (_SysInfo2.nCanMultiCount > 91) { _SysInfo2.strValueBuff[91] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue94; }
					if (_SysInfo2.nCanMultiCount > 92) { _SysInfo2.strValueBuff[92] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue95; }
					if (_SysInfo2.nCanMultiCount > 93) { _SysInfo2.strValueBuff[93] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue96; }
					if (_SysInfo2.nCanMultiCount > 94) { _SysInfo2.strValueBuff[94] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue97; }
					if (_SysInfo2.nCanMultiCount > 95) { _SysInfo2.strValueBuff[95] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue98; }
					if (_SysInfo2.nCanMultiCount > 96) { _SysInfo2.strValueBuff[96] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue99; }
					if (_SysInfo2.nCanMultiCount > 97) { _SysInfo2.strValueBuff[97] = _ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue100; }
					tMainTimer[nStepIndex].Start(5000);
					nProcessStep[nStepIndex]++;
					break;

				case 21701:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet2(_SysInfo2.nSubMainWorkStep, _SysInfo2.nCanAddr.ToString("X"), "", "TIME OUT", "NG");
						_SysInfo2.bEolNg = true;
						_SysInfo2.nRepeatWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}

					if (_CanComm[_SysInfo2.nCanCh].bReadMessage && _CanComm[_SysInfo2.nCanCh].nReadid == _SysInfo2.nCanStartAddr && _CanComm[_SysInfo2.nCanCh].nReaddlc == _SysInfo2.nCanMultiCount)
					{
						bool bCompResult = true;
						string strSource = "";
						string strRead = "";

						for (int i = 0; i < _SysInfo2.nCanMultiCount; i++)
						{
							strSource += _SysInfo2.strValueBuff[i];
							strRead += _CanComm[_SysInfo2.nCanCh].btReadData[i].ToString("X2");

							if (!GetCompareCanData(_SysInfo2.strValueBuff[i], _CanComm[_SysInfo2.nCanCh].btReadData[i]))
							{
								bCompResult = false;
							}
						}

						if (bCompResult)
						{
							NgDataSet2(_SysInfo2.nSubMainWorkStep, _SysInfo2.nCanAddr.ToString("X"), strSource, strRead, "OK");
						}
						else
						{
							NgDataSet2(_SysInfo2.nSubMainWorkStep, _SysInfo2.nCanAddr.ToString("X"), strSource, strRead, "NG");
							_SysInfo2.bEolNg = true;
						}

						_SysInfo2.nRepeatWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;




				case 21800:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].nID.ToString(), out _SysInfo2.nCanCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue1, out _SysInfo2.nCanAddr);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue2.ToString(), out _SysInfo2.nCanMultiCount);
					if (_SysInfo2.nCanMultiCount > 0) { _SysInfo2.nMultiSendData[0] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue3.ToString()); }
					if (_SysInfo2.nCanMultiCount > 1) { _SysInfo2.nMultiSendData[1] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue4.ToString()); }
					if (_SysInfo2.nCanMultiCount > 2) { _SysInfo2.nMultiSendData[2] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue5.ToString()); }
					if (_SysInfo2.nCanMultiCount > 3) { _SysInfo2.nMultiSendData[3] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue6.ToString()); }
					if (_SysInfo2.nCanMultiCount > 4) { _SysInfo2.nMultiSendData[4] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue7.ToString()); }
					if (_SysInfo2.nCanMultiCount > 5) { _SysInfo2.nMultiSendData[5] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue8.ToString()); }
					if (_SysInfo2.nCanMultiCount > 6) { _SysInfo2.nMultiSendData[6] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue9.ToString()); }
					if (_SysInfo2.nCanMultiCount > 7) { _SysInfo2.nMultiSendData[7] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue10.ToString()); }
					if (_SysInfo2.nCanMultiCount > 8) { _SysInfo2.nMultiSendData[8] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue11.ToString()); }
					if (_SysInfo2.nCanMultiCount > 9) { _SysInfo2.nMultiSendData[9] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue12.ToString()); }
					if (_SysInfo2.nCanMultiCount > 10) { _SysInfo2.nMultiSendData[10] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue13.ToString()); }
					if (_SysInfo2.nCanMultiCount > 11) { _SysInfo2.nMultiSendData[11] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue14.ToString()); }
					if (_SysInfo2.nCanMultiCount > 12) { _SysInfo2.nMultiSendData[12] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue15.ToString()); }
					if (_SysInfo2.nCanMultiCount > 13) { _SysInfo2.nMultiSendData[13] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue16.ToString()); }
					if (_SysInfo2.nCanMultiCount > 14) { _SysInfo2.nMultiSendData[14] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue17.ToString()); }
					if (_SysInfo2.nCanMultiCount > 15) { _SysInfo2.nMultiSendData[15] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue18.ToString()); }
					if (_SysInfo2.nCanMultiCount > 16) { _SysInfo2.nMultiSendData[16] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue19.ToString()); }
					if (_SysInfo2.nCanMultiCount > 17) { _SysInfo2.nMultiSendData[17] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue20.ToString()); }
					if (_SysInfo2.nCanMultiCount > 18) { _SysInfo2.nMultiSendData[18] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue21.ToString()); }
					if (_SysInfo2.nCanMultiCount > 19) { _SysInfo2.nMultiSendData[19] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue22.ToString()); }
					if (_SysInfo2.nCanMultiCount > 20) { _SysInfo2.nMultiSendData[20] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue23.ToString()); }
					if (_SysInfo2.nCanMultiCount > 21) { _SysInfo2.nMultiSendData[21] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue24.ToString()); }
					if (_SysInfo2.nCanMultiCount > 22) { _SysInfo2.nMultiSendData[22] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue25.ToString()); }
					if (_SysInfo2.nCanMultiCount > 23) { _SysInfo2.nMultiSendData[23] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue26.ToString()); }
					if (_SysInfo2.nCanMultiCount > 24) { _SysInfo2.nMultiSendData[24] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue27.ToString()); }
					if (_SysInfo2.nCanMultiCount > 25) { _SysInfo2.nMultiSendData[25] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue28.ToString()); }
					if (_SysInfo2.nCanMultiCount > 26) { _SysInfo2.nMultiSendData[26] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue29.ToString()); }
					if (_SysInfo2.nCanMultiCount > 27) { _SysInfo2.nMultiSendData[27] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue30.ToString()); }
					if (_SysInfo2.nCanMultiCount > 28) { _SysInfo2.nMultiSendData[28] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue31.ToString()); }
					if (_SysInfo2.nCanMultiCount > 29) { _SysInfo2.nMultiSendData[29] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue32.ToString()); }
					if (_SysInfo2.nCanMultiCount > 30) { _SysInfo2.nMultiSendData[30] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue33.ToString()); }
					if (_SysInfo2.nCanMultiCount > 31) { _SysInfo2.nMultiSendData[31] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue34.ToString()); }
					if (_SysInfo2.nCanMultiCount > 32) { _SysInfo2.nMultiSendData[32] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue35.ToString()); }
					if (_SysInfo2.nCanMultiCount > 33) { _SysInfo2.nMultiSendData[33] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue36.ToString()); }
					if (_SysInfo2.nCanMultiCount > 34) { _SysInfo2.nMultiSendData[34] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue37.ToString()); }
					if (_SysInfo2.nCanMultiCount > 35) { _SysInfo2.nMultiSendData[35] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue38.ToString()); }
					if (_SysInfo2.nCanMultiCount > 36) { _SysInfo2.nMultiSendData[36] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue39.ToString()); }
					if (_SysInfo2.nCanMultiCount > 37) { _SysInfo2.nMultiSendData[37] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue40.ToString()); }
					if (_SysInfo2.nCanMultiCount > 38) { _SysInfo2.nMultiSendData[38] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue41.ToString()); }
					if (_SysInfo2.nCanMultiCount > 39) { _SysInfo2.nMultiSendData[39] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue42.ToString()); }
					if (_SysInfo2.nCanMultiCount > 40) { _SysInfo2.nMultiSendData[40] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue43.ToString()); }
					if (_SysInfo2.nCanMultiCount > 41) { _SysInfo2.nMultiSendData[41] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue44.ToString()); }
					if (_SysInfo2.nCanMultiCount > 42) { _SysInfo2.nMultiSendData[42] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue45.ToString()); }
					if (_SysInfo2.nCanMultiCount > 43) { _SysInfo2.nMultiSendData[43] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue46.ToString()); }
					if (_SysInfo2.nCanMultiCount > 44) { _SysInfo2.nMultiSendData[44] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue47.ToString()); }
					if (_SysInfo2.nCanMultiCount > 45) { _SysInfo2.nMultiSendData[45] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue48.ToString()); }
					if (_SysInfo2.nCanMultiCount > 46) { _SysInfo2.nMultiSendData[46] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue49.ToString()); }
					if (_SysInfo2.nCanMultiCount > 47) { _SysInfo2.nMultiSendData[47] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue50.ToString()); }
					if (_SysInfo2.nCanMultiCount > 48) { _SysInfo2.nMultiSendData[48] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue51.ToString()); }
					if (_SysInfo2.nCanMultiCount > 49) { _SysInfo2.nMultiSendData[49] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue52.ToString()); }
					if (_SysInfo2.nCanMultiCount > 50) { _SysInfo2.nMultiSendData[50] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue53.ToString()); }
					if (_SysInfo2.nCanMultiCount > 51) { _SysInfo2.nMultiSendData[51] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue54.ToString()); }
					if (_SysInfo2.nCanMultiCount > 52) { _SysInfo2.nMultiSendData[52] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue55.ToString()); }
					if (_SysInfo2.nCanMultiCount > 53) { _SysInfo2.nMultiSendData[53] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue56.ToString()); }
					if (_SysInfo2.nCanMultiCount > 54) { _SysInfo2.nMultiSendData[54] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue57.ToString()); }
					if (_SysInfo2.nCanMultiCount > 55) { _SysInfo2.nMultiSendData[55] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue58.ToString()); }
					if (_SysInfo2.nCanMultiCount > 56) { _SysInfo2.nMultiSendData[56] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue59.ToString()); }
					if (_SysInfo2.nCanMultiCount > 57) { _SysInfo2.nMultiSendData[57] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue60.ToString()); }
					if (_SysInfo2.nCanMultiCount > 58) { _SysInfo2.nMultiSendData[58] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue61.ToString()); }
					if (_SysInfo2.nCanMultiCount > 59) { _SysInfo2.nMultiSendData[59] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue62.ToString()); }
					if (_SysInfo2.nCanMultiCount > 60) { _SysInfo2.nMultiSendData[60] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue63.ToString()); }
					if (_SysInfo2.nCanMultiCount > 61) { _SysInfo2.nMultiSendData[61] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue64.ToString()); }
					if (_SysInfo2.nCanMultiCount > 62) { _SysInfo2.nMultiSendData[62] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue65.ToString()); }
					if (_SysInfo2.nCanMultiCount > 63) { _SysInfo2.nMultiSendData[63] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue66.ToString()); }
					if (_SysInfo2.nCanMultiCount > 64) { _SysInfo2.nMultiSendData[64] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue67.ToString()); }
					if (_SysInfo2.nCanMultiCount > 65) { _SysInfo2.nMultiSendData[65] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue68.ToString()); }
					if (_SysInfo2.nCanMultiCount > 66) { _SysInfo2.nMultiSendData[66] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue69.ToString()); }
					if (_SysInfo2.nCanMultiCount > 67) { _SysInfo2.nMultiSendData[67] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue70.ToString()); }
					if (_SysInfo2.nCanMultiCount > 68) { _SysInfo2.nMultiSendData[68] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue71.ToString()); }
					if (_SysInfo2.nCanMultiCount > 69) { _SysInfo2.nMultiSendData[69] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue72.ToString()); }
					if (_SysInfo2.nCanMultiCount > 70) { _SysInfo2.nMultiSendData[70] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue73.ToString()); }
					if (_SysInfo2.nCanMultiCount > 71) { _SysInfo2.nMultiSendData[71] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue74.ToString()); }
					if (_SysInfo2.nCanMultiCount > 72) { _SysInfo2.nMultiSendData[72] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue75.ToString()); }
					if (_SysInfo2.nCanMultiCount > 73) { _SysInfo2.nMultiSendData[73] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue76.ToString()); }
					if (_SysInfo2.nCanMultiCount > 74) { _SysInfo2.nMultiSendData[74] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue77.ToString()); }
					if (_SysInfo2.nCanMultiCount > 75) { _SysInfo2.nMultiSendData[75] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue78.ToString()); }
					if (_SysInfo2.nCanMultiCount > 76) { _SysInfo2.nMultiSendData[76] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue79.ToString()); }
					if (_SysInfo2.nCanMultiCount > 77) { _SysInfo2.nMultiSendData[77] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue80.ToString()); }
					if (_SysInfo2.nCanMultiCount > 78) { _SysInfo2.nMultiSendData[78] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue81.ToString()); }
					if (_SysInfo2.nCanMultiCount > 79) { _SysInfo2.nMultiSendData[79] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue82.ToString()); }
					if (_SysInfo2.nCanMultiCount > 80) { _SysInfo2.nMultiSendData[80] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue83.ToString()); }
					if (_SysInfo2.nCanMultiCount > 81) { _SysInfo2.nMultiSendData[81] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue84.ToString()); }
					if (_SysInfo2.nCanMultiCount > 82) { _SysInfo2.nMultiSendData[82] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue85.ToString()); }
					if (_SysInfo2.nCanMultiCount > 83) { _SysInfo2.nMultiSendData[83] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue86.ToString()); }
					if (_SysInfo2.nCanMultiCount > 84) { _SysInfo2.nMultiSendData[84] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue87.ToString()); }
					if (_SysInfo2.nCanMultiCount > 85) { _SysInfo2.nMultiSendData[85] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue88.ToString()); }
					if (_SysInfo2.nCanMultiCount > 86) { _SysInfo2.nMultiSendData[86] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue89.ToString()); }
					if (_SysInfo2.nCanMultiCount > 87) { _SysInfo2.nMultiSendData[87] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue90.ToString()); }
					if (_SysInfo2.nCanMultiCount > 88) { _SysInfo2.nMultiSendData[88] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue91.ToString()); }
					if (_SysInfo2.nCanMultiCount > 89) { _SysInfo2.nMultiSendData[89] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue92.ToString()); }
					if (_SysInfo2.nCanMultiCount > 90) { _SysInfo2.nMultiSendData[90] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue93.ToString()); }
					if (_SysInfo2.nCanMultiCount > 91) { _SysInfo2.nMultiSendData[91] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue94.ToString()); }
					if (_SysInfo2.nCanMultiCount > 92) { _SysInfo2.nMultiSendData[92] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue95.ToString()); }
					if (_SysInfo2.nCanMultiCount > 93) { _SysInfo2.nMultiSendData[93] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue96.ToString()); }
					if (_SysInfo2.nCanMultiCount > 94) { _SysInfo2.nMultiSendData[94] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue97.ToString()); }
					if (_SysInfo2.nCanMultiCount > 95) { _SysInfo2.nMultiSendData[95] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue98.ToString()); }
					if (_SysInfo2.nCanMultiCount > 96) { _SysInfo2.nMultiSendData[96] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue99.ToString()); }
					if (_SysInfo2.nCanMultiCount > 97) { _SysInfo2.nMultiSendData[97] = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue100.ToString()); }

					SendTCPMultiCommand2(_SysInfo2.nCanCh, _SysInfo2.nCanAddr, _SysInfo2.nMultiSendData, _SysInfo2.nCanMultiCount);
					nProcessStep[nStepIndex]++;
					break;

				case 21801:
					_SysInfo2.nRepeatWorkStep++;
					nProcessStep[nStepIndex] = 20000;
					break;


				// Modbus TCP Read(COMP)
				case 21900:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].nID.ToString(), out _SysInfo2.nCanCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue1.ToString(), out _SysInfo2.nCanAddr);
					//int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue2.ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out _SysInfo2.nCanData);
					_SysInfo2.nCanData = GetFuncData2(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue2.ToString());
					SendTCPReadCommand2(_SysInfo2.nCanCh, _SysInfo2.nCanAddr);

					tMainTimer[nStepIndex].Start(5000);
					nProcessStep[nStepIndex]++;
					break;

				case 21901:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet2(_SysInfo2.nSubMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), "TIME OUT", "NG");
						_SysInfo2.bEolNg = true;
						_SysInfo2.nRepeatWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}


					if (_ModbusSoket2.bResultOk)
					{

						if (_SysInfo2.btTcpReadData[7] == 0x03)
						{
							if ((_SysInfo2.btTcpReadData[9] * 0x100) + _SysInfo2.btTcpReadData[10] == _SysInfo2.nCanData)
							{
								NgDataSet2(_SysInfo2.nSubMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), ((_SysInfo2.btTcpReadData[9] * 0x100) + _SysInfo2.btTcpReadData[10]).ToString("X4"), "OK");
								//AppendLogMsg($"OK / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
							}
							else
							{
								_SysInfo2.bEolNg = true;
								NgDataSet2(_SysInfo2.nSubMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), ((_SysInfo2.btTcpReadData[9] * 0x100) + _SysInfo2.btTcpReadData[10]).ToString("X4"), "NG");
								//AppendLogMsg($"NG / ADDR {_SysInfo2.nCanAddr} / DATA {_SysInfo2.nCanData:X4}", MSG_TYPE.INFO);
							}
						}
						else
						{
							_SysInfo2.bEolNg = true;
							NgDataSet2(_SysInfo2.nSubMainWorkStep, _SysInfo2.nCanAddr.ToString(), _SysInfo2.nCanData.ToString("X4"), "DATA ERROR", "NG");
						}
						_SysInfo2.nRepeatWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;



				case 22000:
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].nID.ToString(), out _SysInfo2.nCanCh);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue1.ToString(), out _SysInfo2.nCanAddr);
					int.TryParse(_ModelInfo2._TestInfo[_SysInfo2.nSubMainWorkStep]._DataInfo[_SysInfo2.nRepeatWorkStep].strValue2.ToString(), out _SysInfo2.nBuffIndex);
					tMainTimer[nStepIndex].Start(5000);
					SendTCPReadCommand2(_SysInfo2.nCanCh, _SysInfo2.nCanAddr);
					nProcessStep[nStepIndex]++;
					break;

				case 22001:
					if (tMainTimer[nStepIndex].Verify())
					{
						NgDataSet2(_SysInfo2.nSubMainWorkStep, _SysInfo2.nCanAddr.ToString(), "", "TIME OUT", "NG");
						_SysInfo2.bEolNg = true;
						_SysInfo2.nRepeatWorkStep++;
						nProcessStep[nStepIndex] = 20000;
						break;
					}

					if (_ModbusSoket2.bResultOk)
					{
						if (_SysInfo2.btTcpReadData[7] == 0x03)
						{
							_SysInfo2.nReadDataBuff[_SysInfo2.nBuffIndex] = (_SysInfo2.btTcpReadData[9] * 0x100) + _SysInfo2.btTcpReadData[10];
							NgDataSet2(_SysInfo2.nSubMainWorkStep, _SysInfo2.nCanAddr.ToString(), $"BUFFER{_SysInfo2.nBuffIndex}", ((_SysInfo2.btTcpReadData[9] * 0x100) + _SysInfo2.btTcpReadData[10]).ToString("X4"), "");
						}
						_SysInfo2.nRepeatWorkStep++;
						nProcessStep[nStepIndex] = 20000;
					}
					break;

				// Sub Item 통신 종료
				case 29000:
					_SysInfo2.nRepeatWorkStep = 0;
					nProcessStep[nStepIndex] = 11000;
					break;

				case 30000:
					_SysInfo2.nSubMainWorkStep = 0;
					nProcessStep[nStepIndex] = 0;
					break;


			}
		}

		public static void PingTest()
		{
			int nStepIndex = (int)PROC_LIST.PING_TEST;

			switch (nProcessStep[nStepIndex])
			{
				case 0:
					_SysInfo.nPingRetryCount = 0;
					break;


				case 100:
					PingReply rePly = _pingSender.Send(_SysInfo.strPingTestIP, 120);

					if (rePly.Status == IPStatus.Success)
					{
						_SysInfo.bPingTestResult = MAIN_STATUS.OK;
						nProcessStep[nStepIndex] = 101;
					}
					else
					{
						if (_SysInfo.nPingRetryCount > 15)
						{
							_SysInfo.bPingTestResult = MAIN_STATUS.NG;
							nProcessStep[nStepIndex] = 0;
						}
						else
						{
							_SysInfo.nPingRetryCount++;
							nProcessStep[nStepIndex] = 101;
						}

					}
					break;

				case 101:
					tMainTimer[nStepIndex].Start(1000);
					nProcessStep[nStepIndex] = 102;
					break;

				case 102:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					nProcessStep[nStepIndex] = 100;
					break;
			}
		}

		public static void PingTest2()
		{
			int nStepIndex = (int)PROC_LIST.PING_TEST2;

			switch (nProcessStep[nStepIndex])
			{
				case 0:
					_SysInfo2.nPingRetryCount = 0;
					break;


				case 100:
					PingReply rePly = _pingSender2.Send(_SysInfo2.strPingTestIP, 120);

					if (rePly.Status == IPStatus.Success)
					{
						_SysInfo2.bPingTestResult = MAIN_STATUS.OK;
						nProcessStep[nStepIndex] = 101;
					}
					else
					{
						if (_SysInfo2.nPingRetryCount > 15)
						{
							_SysInfo2.bPingTestResult = MAIN_STATUS.NG;
							nProcessStep[nStepIndex] = 0;
						}
						else
						{
							_SysInfo2.nPingRetryCount++;
							nProcessStep[nStepIndex] = 101;
						}

					}
					break;

				case 101:
					tMainTimer[nStepIndex].Start(1000);
					nProcessStep[nStepIndex] = 102;
					break;

				case 102:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					nProcessStep[nStepIndex] = 100;
					break;
			}
		}

		public static void SUB_TITE_PROC()
		{
			int nStepIndex = (int)PROC_LIST.SUB_TITE1;

			int nUseTipCount = 0;
			int nUseTip = 0;
			int nTiteNum = 0;

			if (_SysInfo.nTipNowCount >= _SysInfo.nTipMaxCount)
			{
				_SysInfo.bTiteOk= true;
			}
			else
			{
				_SysInfo.bTiteOk = false;
			}


			switch (nProcessStep[nStepIndex])
			{
				case 0:
					//체결위치 사용하면 100번으로 보내야됨

					nProcessStep[nStepIndex] = 200;
					break;


				// 체결기 사용 스테이션
				// 너트러너 사용 스테이션
				case 200:
					if(_SysInfo.bTiteIngStart)
					{
						SettiteStatusReady();
						nProcessStep[nStepIndex]++;
					}
				
				
					break;

				case 201:
					if (_SysInfo.bTiteIngStart)
					{
						SetNutRunnerSch(_SysInfo.nSetNutSch);   // 너트러너 스케줄 설정
						nProcessStep[nStepIndex]++;
						break;
					}
					else
					{
						nProcessStep[nStepIndex] = 200;
					}
					break;
				

				case 202:
					if (!_SysInfo.bTiteIngStart)
					{
						SetNutRunnerSch(50);
						nProcessStep[nStepIndex] = 0;
						break;
					}

					if (_SysInfo.nTipNowCount < _SysInfo.nTipMaxCount)
					{



						// 체결작업 진행
						if (GetTiteStatus() == TITE_STATUS.OK)
						{

							// OK 시그널이 들어옴
							theApp._Nutrunner.nTiteNum = _SysInfo.nTipNowCount;
							_SysInfo.nTipNowCount++;
							_SysInfo.nTiteLog = 2;
							theApp._Nutrunner.SaveResultData();	
							nProcessStep[nStepIndex]++;

						}
						else if (GetTiteStatus() == TITE_STATUS.NG)
						{
							theApp._Nutrunner.nTiteNum = _SysInfo.nTipNowCount;

							_SysInfo.nTiteLog = 3;
							theApp._Nutrunner.SaveResultData();
							theApp.AppendLogMsg(String.Format("Tip fastening defective"), MSG_TYPE.ERROR);
							_SysInfo.nTL_Beep = 3;
							nProcessStep[nStepIndex] = 0;
						}
						else
						{

						}
						
						


					}
	
					break;

				case 203:
					if (_SysInfo.nTipNowCount >= _SysInfo.nTipMaxCount)
					{

						_SysInfo.nTL_Beep = 2;
					
					}
					else
					{
						TestResultSet(_SysInfo.nMainWorkStep, _SysInfo.nTipNowCount.ToString(), "");
						_SysInfo.nTL_Beep = 1;

					}
					tMainTimer[nStepIndex].Start(100);
					nProcessStep[nStepIndex]++;
					break;

				case 204:
					if (tMainTimer[nStepIndex].Verify())
					{
						nProcessStep[nStepIndex] = 0;
					}
					break;
			}
		}

		public static void SUB_TITE_PROC2()
		{
			int nStepIndex = (int)PROC_LIST.SUB_TITE2;

			int nUseTipCount = 0;
			int nUseTip = 0;
			int nTiteNum = 0;

			if (_SysInfo2.nTipNowCount >= _SysInfo2.nTipMaxCount)
			{
				_SysInfo2.bTiteOk = true;
			}
			else
			{
				_SysInfo2.bTiteOk = false;
			}


			switch (nProcessStep[nStepIndex])
			{
				case 0:
					//체결위치 사용하면 100번으로 보내야됨

					nProcessStep[nStepIndex] = 200;
					break;


				// 체결기 사용 스테이션
				// 너트러너 사용 스테이션
				case 200:
					if (_SysInfo2.bTiteIngStart)
					{
						SettiteStatusReady2();
						nProcessStep[nStepIndex]++;
					}


					break;

				case 201:
					if (_SysInfo2.bTiteIngStart)
					{
						SetNutRunnerSch2(_SysInfo2.nSetNutSch);   // 너트러너 스케줄 설정
						nProcessStep[nStepIndex]++;
						break;
					}
					else
					{
						nProcessStep[nStepIndex] = 200;
					}
					break;


				case 202:
					if (!_SysInfo2.bTiteIngStart)
					{
						SetNutRunnerSch2(50);
						nProcessStep[nStepIndex] = 0;
						break;
					}

					if (_SysInfo2.nTipNowCount < _SysInfo2.nTipMaxCount)
					{



						// 체결작업 진행
						if (GetTiteStatus2() == TITE_STATUS.OK)
						{

							// OK 시그널이 들어옴
							theApp._Nutrunner2.nTiteNum = _SysInfo2.nTipNowCount;
							_SysInfo2.nTipNowCount++;
							_SysInfo2.nTiteLog = 2;
							theApp._Nutrunner2.SaveResultData();
							nProcessStep[nStepIndex]++;

						}
						else if (GetTiteStatus2() == TITE_STATUS.NG)
						{
							theApp._Nutrunner2.nTiteNum = _SysInfo2.nTipNowCount;

							_SysInfo2.nTiteLog = 3;
							theApp._Nutrunner2.SaveResultData();
							theApp.AppendLogMsg2(String.Format("Tip fastening defective"), MSG_TYPE.ERROR);
							_SysInfo.nTL_Beep = 3;
							nProcessStep[nStepIndex] = 0;
						}





					}

					break;

				case 203:
					if (_SysInfo2.nTipNowCount >= _SysInfo2.nTipMaxCount)
					{

						_SysInfo.nTL_Beep = 2;
						
					}
					else
					{
						TestResultSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nTipNowCount.ToString(), "");
						_SysInfo.nTL_Beep = 1;

					}
					tMainTimer[nStepIndex].Start(100);
					nProcessStep[nStepIndex]++;
					break;

				case 204:
					if (tMainTimer[nStepIndex].Verify())
					{
						nProcessStep[nStepIndex] = 0;
					}
					break;
			}
		}

		public static void SetNutRunnerSch(int nIndex)
		{
			theApp._Nutrunner.nPSet = nIndex;
			theApp._Nutrunner.bPSet = true;
		}


		public static TITE_STATUS GetTiteStatus()
		{
			return theApp._Nutrunner._Status;
		}

		public static void SettiteStatusReady()
		{
			theApp._Nutrunner._Status = TITE_STATUS.READY;
		}

		public static void SetNutRunnerSch2(int nIndex)
		{
			theApp._Nutrunner2.nPSet = nIndex;
			theApp._Nutrunner2.bPSet = true;
		}					 


		public static TITE_STATUS GetTiteStatus2()
		{
			return theApp._Nutrunner2._Status;
		}

		public static void SettiteStatusReady2()
		{
			theApp._Nutrunner2._Status = TITE_STATUS.READY;
		}




		public static void SendTCPMultiCommand(int nCh, int nStartAddr, int[] nData, int nCount)
		{
			byte[] btSendData = new byte[(nCount * 2) + 13];

			btSendData[0] = 0x01;
			btSendData[1] = 0x00;
			btSendData[2] = 0x00;
			btSendData[3] = 0x00;
			btSendData[4] = 0x00;
			btSendData[5] = (byte)(7 + (nCount * 2));
			btSendData[6] = 0x01;
			btSendData[7] = 0x10;
			btSendData[8] = (byte)(nStartAddr / 0x100);
			btSendData[9] = (byte)(nStartAddr % 0x100);
			btSendData[10] = 0x00;
			btSendData[11] = (byte)nCount;
			btSendData[12] = (byte)(nCount * 2);

			for (int i = 0; i < nCount; i++)
			{
				btSendData[13 + (i * 2)] = (byte)(nData[i] / 0x100);
				btSendData[13 + (i * 2) + 1] = (byte)(nData[i] % 0x100);
			}
			_SysInfo.btTcpReadData = _ModbusSoket.TcpSocket(_Config.strRBMSIP, _Config.nRBMSPort, btSendData);

		}


		public static void SendTCPReadCommand(int nCh, int nAddress)
		{
			byte[] btSendData = new byte[12];

			btSendData[0] = 0x01;
			btSendData[1] = 0x00;
			btSendData[2] = 0x00;
			btSendData[3] = 0x00;
			btSendData[4] = 0x00;
			btSendData[5] = 0x06;
			btSendData[6] = 0x01;
			btSendData[7] = 0x03;
			btSendData[8] = (byte)(nAddress / 0x100);
			btSendData[9] = (byte)(nAddress % 0x100);
			btSendData[10] = 0x00;
			btSendData[11] = 0x01;
			_ModbusSoket.bResultOk = false;
			_SysInfo.btTcpReadData = _ModbusSoket.TcpSocket(_Config.strRBMSIP, _Config.nRBMSPort, btSendData);

		}

		public static void SendTCPMultiCommand2(int nCh, int nStartAddr, int[] nData, int nCount)
		{
			byte[] btSendData = new byte[(nCount * 2) + 13];

			btSendData[0] = 0x01;
			btSendData[1] = 0x00;
			btSendData[2] = 0x00;
			btSendData[3] = 0x00;
			btSendData[4] = 0x00;
			btSendData[5] = (byte)(7 + (nCount * 2));
			btSendData[6] = 0x01;
			btSendData[7] = 0x10;
			btSendData[8] = (byte)(nStartAddr / 0x100);
			btSendData[9] = (byte)(nStartAddr % 0x100);
			btSendData[10] = 0x00;
			btSendData[11] = (byte)nCount;
			btSendData[12] = (byte)(nCount * 2);

			for (int i = 0; i < nCount; i++)
			{
				btSendData[13 + (i * 2)] = (byte)(nData[i] / 0x100);
				btSendData[13 + (i * 2) + 1] = (byte)(nData[i] % 0x100);
			}
			_SysInfo2.btTcpReadData = _ModbusSoket2.TcpSocket(_Config.strRBMSIP2, _Config.nRBMSPort2, btSendData);

		}


		public static void SendTCPReadCommand2(int nCh, int nAddress)
		{
			byte[] btSendData = new byte[12];

			btSendData[0] = 0x01;
			btSendData[1] = 0x00;
			btSendData[2] = 0x00;
			btSendData[3] = 0x00;
			btSendData[4] = 0x00;
			btSendData[5] = 0x06;
			btSendData[6] = 0x01;
			btSendData[7] = 0x03;
			btSendData[8] = (byte)(nAddress / 0x100);
			btSendData[9] = (byte)(nAddress % 0x100);
			btSendData[10] = 0x00;
			btSendData[11] = 0x01;
			_ModbusSoket2.bResultOk = false;
			_SysInfo2.btTcpReadData = _ModbusSoket2.TcpSocket(_Config.strRBMSIP2, _Config.nRBMSPort2, btSendData);

		}


		public static bool CheckMasterTestFinish(string strModelName)
		{
			bool bNgResult = true;
			bool bOkResult = true;
			// 마스터 정보 로드
			LoadMasterTestInfo(ref _MasterTestInfo, strModelName);

			if (DateTime.Now.Year == _MasterTestInfo.dtMasterOkSampleTestTime.Year &&
								(DateTime.Now.Month != _MasterTestInfo.dtMasterOkSampleTestTime.Month ||
								DateTime.Now.Day != _MasterTestInfo.dtMasterOkSampleTestTime.Day))              // 년이 같은경우 DayofYear에 의한 초기화
			{
				if (DateTime.Now.DayOfYear - _MasterTestInfo.dtMasterOkSampleTestTime.DayOfYear > 1)
				{
					bOkResult &= false;
				}
				else
				{
					if (DateTime.Now.Hour > (_Config.strMasterCheckDateTime / 100))
					{
						bOkResult &= false;
					}
					else if (DateTime.Now.Hour == (_Config.strMasterCheckDateTime / 100) &&
						DateTime.Now.Minute >= (_Config.strMasterCheckDateTime % 100))
					{
						bOkResult &= false;
					}
				}
			}
			else if (DateTime.Now.Year != _MasterTestInfo.dtMasterOkSampleTestTime.Year)             // 년이 다른 경우 클리어 날짜가 12월 31일 이고 오늘 날짜가 1월 1일이면 지정한 시간에 초기화
			{
				if (DateTime.Now.Month == 1 && DateTime.Now.Day == 1 && _MasterTestInfo.dtMasterOkSampleTestTime.Month == 12 && _MasterTestInfo.dtMasterOkSampleTestTime.Day == 31)
				{
					if (DateTime.Now.Hour > (_Config.strMasterCheckDateTime / 100))
					{
						bOkResult &= false;

					}
					else if (DateTime.Now.Hour == (_Config.strMasterCheckDateTime / 100) &&
						DateTime.Now.Minute >= (_Config.strMasterCheckDateTime % 100))
					{
						bOkResult &= false;
					}
				}
				else
				{
					bOkResult &= false;
				}
			}



			if (DateTime.Now.Year == _MasterTestInfo.dtMasterNgSampleTestTime.Year &&
								(DateTime.Now.Month != _MasterTestInfo.dtMasterNgSampleTestTime.Month ||
								DateTime.Now.Day != _MasterTestInfo.dtMasterNgSampleTestTime.Day))              // 년이 같은경우 DayofYear에 의한 초기화
			{
				if (DateTime.Now.DayOfYear - _MasterTestInfo.dtMasterNgSampleTestTime.DayOfYear > 1)
				{
					bNgResult &= false;
				}
				else
				{
					if (DateTime.Now.Hour > (_Config.strMasterCheckDateTime / 100))
					{
						bNgResult &= false;
					}
					else if (DateTime.Now.Hour == (_Config.strMasterCheckDateTime / 100) &&
						DateTime.Now.Minute >= (_Config.strMasterCheckDateTime % 100))
					{
						bNgResult &= false;
					}
				}
			}
			else if (DateTime.Now.Year != _MasterTestInfo.dtMasterNgSampleTestTime.Year)             // 년이 다른 경우 클리어 날짜가 12월 31일 이고 오늘 날짜가 1월 1일이면 지정한 시간에 초기화
			{
				if (DateTime.Now.Month == 1 && DateTime.Now.Day == 1 && _MasterTestInfo.dtMasterNgSampleTestTime.Month == 12 && _MasterTestInfo.dtMasterNgSampleTestTime.Day == 31)
				{
					if (DateTime.Now.Hour > (_Config.strMasterCheckDateTime / 100))
					{
						bNgResult &= false;

					}
					else if (DateTime.Now.Hour == (_Config.strMasterCheckDateTime / 100) &&
						DateTime.Now.Minute >= (_Config.strMasterCheckDateTime % 100))
					{
						bNgResult &= false;
					}
				}
				else
				{
					bNgResult &= false;
				}
			}


			if (!bOkResult) { _SysInfo.strMasterErrString = "양품 마스터 샘플 검사 이력이 없습니다."; }
			if (!bNgResult) { _SysInfo.strMasterErrString = "불량 마스터 샘플 검사 이력이 없습니다."; }
			if (!bOkResult && !bNgResult) { _SysInfo.strMasterErrString = "마스터 샘플 검사 이력이 없습니다."; } // 둘다 검사를 안한경우


			return bOkResult && bNgResult;

		}

		public static void SaveResultData()
		{
			try
			{
				string strSaveFolderPath = String.Format(@"DATA\\{0}\\{1}\\", _ModelInfo.strModelName, DateTime.Now.ToString("yyMMdd"));
				DirectoryInfo Savedir = new DirectoryInfo(strSaveFolderPath);
				if (Savedir.Exists == false) { Savedir.Create(); }


				string strSaveMessage = "";

				strSaveMessage += "Barcode," + _SysInfo.strDispBarcode + "\r\n";
				strSaveMessage += "Mac," + _SysInfo.strDispMac + "\r\n";
				strSaveMessage += "Test Start Time," + _SysInfo.dtTestStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n";
				strSaveMessage += "Test End Time," + _SysInfo.dtTestEndTime.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n";
				strSaveMessage += "Total Result," + _SysInfo.strTotalResult + "\r\n";

				for (int i = 0; i < _TestData.Count; i++)
				{
					strSaveMessage += _TestData[i].Cate + "," + _TestData[i].strTestName.Replace(',', '_') + "," + _TestData[i].strResult + "," + _TestData[i].SpecMin + "," + _TestData[i].Data + "," + _TestData[i].SpecMax + "," + _TestData[i].Unit + "\r\n";

					if (_SysInfo._listNgInfo[i].Count > 0 && _TestData[i].Cate == "EOL")
					{
						for (int j = 0; j < _SysInfo._listNgInfo[i].Count; j++)
						{
							strSaveMessage += _TestData[i].Cate + "," + _TestData[i].strTestName + $"EOL({j}:{_SysInfo._listNgInfo[i][j].strAddr})" + "," + _SysInfo._listNgInfo[i][j].strTestResult + "," + _SysInfo._listNgInfo[i][j].strSource + "," + _SysInfo._listNgInfo[i][j].strRead + "," + _SysInfo._listNgInfo[i][j].strSource + "," + "" + "\r\n";
						}
					}
				}

				string strSaveFilePath = String.Format(@"{0}{1}_EOL.txt", strSaveFolderPath, _SysInfo.strSaveFileName.Replace(':', '_'));
				File.AppendAllText(strSaveFilePath, strSaveMessage, Encoding.UTF8);



				// 토탈 데이터 저장행정
				if (!File.Exists(String.Format(@"{0}Total_Data.txt", strSaveFolderPath)))
				{
					strSaveMessage = "";

					strSaveMessage += "Barcode,";
					strSaveMessage += "Mac,";
					strSaveMessage += "Test Start Time,";
					strSaveMessage += "Test End Time,";
					strSaveMessage += "Total Result,";

					//for (int i = 0; i < _TestData.Count; i++)
					//{
					//	strSaveMessage += _TestData[i].strTestName.Replace(',', '_') + ",";
					//	if (_SysInfo._listNgInfo[i].Count > 0 && _TestData[i].Cate == "EOL")
					//	{
					//		for (int j = 0; j < _SysInfo._listNgInfo[i].Count; j++)
					//		{
					//			strSaveMessage += _TestData[i].strTestName + $"EOL({j}:{_SysInfo._listNgInfo[i][j].strAddr})" + ",";
					//		}
					//	}
					//}	

					for (int i = 0; i < _ModelInfo._TestInfo.Count; i++)
					{
						strSaveMessage += _ModelInfo._TestInfo[i].strTestName.Replace(',', '_') + ",";

						if (_ModelInfo._TestInfo[i].nTestItem == 0)
						{
							int nAddrCount = 0;

							for (int j = 0; j < _ModelInfo._TestInfo[i]._DataInfo.Count; j++)
							{
								if (_ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 1 ||
									_ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 2 ||
									_ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 6 ||
									_ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 8 ||
									_ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 9)
								{
									strSaveMessage += _ModelInfo._TestInfo[i].strTestName + $"EOL({nAddrCount++}:{_ModelInfo._TestInfo[i]._DataInfo[j].strValue1})" + ",";
								}

							}
						}
					}

					strSaveMessage += "\r\n";

					strSaveMessage += ",";
					strSaveMessage += ",";
					strSaveMessage += ",";
					strSaveMessage += ",";
					strSaveMessage += ",";

					//for (int i = 0; i < _TestData.Count; i++)
					//{
					//	strSaveMessage += _TestData[i].SpecMin + ",";

					//	if (_SysInfo._listNgInfo[i].Count > 0 && _TestData[i].Cate == "EOL")
					//	{
					//		for (int j = 0; j < _SysInfo._listNgInfo[i].Count; j++)
					//		{
					//			strSaveMessage += _SysInfo._listNgInfo[i][j].strSource + ",";
					//		}
					//	}
					//}
					for (int i = 0; i < _ModelInfo._TestInfo.Count; i++)
					{
						strSaveMessage += _TestData[i].SpecMin + ",";

						if (_ModelInfo._TestInfo[i].nTestItem == 0)
						{


							for (int j = 0; j < _ModelInfo._TestInfo[i]._DataInfo.Count; j++)
							{
								if (_ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 1 ||
									_ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 2 ||
									_ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 6 ||
									_ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 8 ||
									_ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 9)
								{
									if (_ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 2 || _ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 9)
									{
										strSaveMessage += $"BUFFER{_ModelInfo._TestInfo[i]._DataInfo[j].strValue2}" + ",";
									}
									else if (_ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 1 || _ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 8)
									{
										_SysInfo.nValue2Data = GetFuncData(_ModelInfo._TestInfo[i]._DataInfo[j].strValue2.ToString());
										strSaveMessage += _SysInfo.nValue2Data.ToString("X4") + ",";
									}
									else
									{
										strSaveMessage += _ModelInfo._TestInfo[i]._DataInfo[j].strValue2 + ",";
									}

								}

							}
						}
					}
					strSaveMessage += "\r\n";

					strSaveMessage += ",";
					strSaveMessage += ",";
					strSaveMessage += ",";
					strSaveMessage += ",";
					strSaveMessage += ",";
					//for (int i = 0; i < _TestData.Count; i++)
					//{
					//	strSaveMessage += _TestData[i].SpecMax + ",";

					//	if (_SysInfo._listNgInfo[i].Count > 0 && _TestData[i].Cate == "EOL")
					//	{
					//		for (int j = 0; j < _SysInfo._listNgInfo[i].Count; j++)
					//		{
					//			strSaveMessage += _SysInfo._listNgInfo[i][j].strSource + ",";
					//		}
					//	}
					//}
					for (int i = 0; i < _ModelInfo._TestInfo.Count; i++)
					{
						strSaveMessage += _TestData[i].SpecMax + ",";

						if (_ModelInfo._TestInfo[i].nTestItem == 0)
						{
							//int nAddrCount = 0;

							for (int j = 0; j < _ModelInfo._TestInfo[i]._DataInfo.Count; j++)
							{
								if (_ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 1 ||
									_ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 2 ||
									_ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 6 ||
									_ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 8 ||
									_ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 9)
								{
									if (_ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 2 || _ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 9)
									{
										strSaveMessage += $"BUFFER{_ModelInfo._TestInfo[i]._DataInfo[j].strValue2}" + ",";
									}
									else if (_ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 1 || _ModelInfo._TestInfo[i]._DataInfo[j].nTestType == 8)
									{
										_SysInfo.nValue2Data = GetFuncData(_ModelInfo._TestInfo[i]._DataInfo[j].strValue2.ToString());
										strSaveMessage += _SysInfo.nValue2Data.ToString("X4") + ",";
									}
									else
									{
										strSaveMessage += _ModelInfo._TestInfo[i]._DataInfo[j].strValue2 + ",";
									}

								}

							}
						}
					}
					strSaveMessage += "\r\n";

					strSaveFilePath = String.Format(@"{0}Total_Data.txt", strSaveFolderPath);
					File.AppendAllText(strSaveFilePath, strSaveMessage, Encoding.UTF8);
				}

				strSaveMessage = "";
				strSaveMessage += _SysInfo.strDispBarcode + ",";
				strSaveMessage += _SysInfo.strDispMac + ",";
				strSaveMessage += _SysInfo.dtTestStartTime.ToString("yyyy-MM-dd HH:mm:ss") + ",";
				strSaveMessage += _SysInfo.dtTestEndTime.ToString("yyyy-MM-dd HH:mm:ss") + ",";
				strSaveMessage += _SysInfo.strTotalResult + ",";

				for (int i = 0; i < _TestData.Count; i++)
				{
					if (_TestData[i].Data == "")
					{
						strSaveMessage += _TestData[i].strResult + ",";
					}
					else
					{
						strSaveMessage += _TestData[i].Data + ",";
					}

					if (_SysInfo._listNgInfo[i].Count > 0 && _TestData[i].Cate == "EOL")
					{
						for (int j = 0; j < _SysInfo._listNgInfo[i].Count; j++)
						{
							strSaveMessage += _SysInfo._listNgInfo[i][j].strRead + ",";
						}
					}
				}
				strSaveMessage += "\r\n";

				strSaveFilePath = String.Format(@"{0}Total_Data.txt", strSaveFolderPath);
				File.AppendAllText(strSaveFilePath, strSaveMessage, Encoding.UTF8);


				//MES 데이터 저장행정 ( MES 폴더에 저장 )
				string strMESSaveFolderPath = String.Format("{0}\\", _Config.strSaveMesDir);
				DirectoryInfo SaveMESdir = new DirectoryInfo(strMESSaveFolderPath);
				if (SaveMESdir.Exists == false) { SaveMESdir.Create(); }

				strSaveMessage = "";

				//strSaveMessage += "Barcode,";
				//strSaveMessage += "Mac,";
				//strSaveMessage += "Test Start Time,";
				//strSaveMessage += "Test End Time,";
				//strSaveMessage += "Total Result,";

				//for (int i = 0; i < _TestData.Count; i++)
				//{
				//	strSaveMessage += _TestData[i].strTestName.Replace(',', '_') + ",";
				//}
				//strSaveMessage += "\r\n";


				//strSaveMessage += ",";
				//strSaveMessage += ",";
				//strSaveMessage += ",";
				//strSaveMessage += ",";
				//strSaveMessage += ",";

				//for (int i = 0; i < _TestData.Count; i++)
				//{
				//	strSaveMessage += _TestData[i].SpecMin + ",";
				//	if (_SysInfo._listNgInfo[i].Count > 0 && _TestData[i].Cate == "EOL")
				//	{
				//		for (int j = 0; j < _SysInfo._listNgInfo[i].Count; j++)
				//		{
				//			strSaveMessage += _SysInfo._listNgInfo[i][j].strSource + ",";
				//		}
				//	}
				//}
				//strSaveMessage += "\r\n";

				//strSaveMessage += ",";
				//strSaveMessage += ",";
				//strSaveMessage += ",";
				//strSaveMessage += ",";
				//strSaveMessage += ",";
				//for (int i = 0; i < _TestData.Count; i++)
				//{
				//	strSaveMessage += _TestData[i].SpecMax + ",";
				//	if (_SysInfo._listNgInfo[i].Count > 0 && _TestData[i].Cate == "EOL")
				//	{
				//		for (int j = 0; j < _SysInfo._listNgInfo[i].Count; j++)
				//		{
				//			strSaveMessage += _SysInfo._listNgInfo[i][j].strSource + ",";
				//		}
				//	}
				//}
				//strSaveMessage += "\r\n";

				//strSaveMessage = "";
				//strSaveMessage += _SysInfo.strDispBarcode + ",";
				//strSaveMessage += _SysInfo.strDispMac + ",";
				//strSaveMessage += _SysInfo.dtTestStartTime.ToString("yyyy-MM-dd HH:mm:ss") + ",";
				//strSaveMessage += _SysInfo.dtTestEndTime.ToString("yyyy-MM-dd HH:mm:ss") + ",";
				//strSaveMessage += _SysInfo.strTotalResult + ",";

				//for (int i = 0; i < _TestData.Count; i++)
				//{
				//	if (_TestData[i].Data == "")
				//	{
				//		strSaveMessage += _TestData[i].strResult + ",";
				//	}
				//	else
				//	{
				//		strSaveMessage += _TestData[i].Data + ",";
				//	}
				//}
				//strSaveMessage += "\r\n";
				strSaveMessage += "Barcode," + _SysInfo.strDispBarcode + "\r\n";
				strSaveMessage += "Mac," + _SysInfo.strDispMac + "\r\n";
				strSaveMessage += "Test Start Time," + _SysInfo.dtTestStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n";
				strSaveMessage += "Test End Time," + _SysInfo.dtTestEndTime.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n";
				strSaveMessage += "Total Result," + _SysInfo.strTotalResult + "\r\n";

				for (int i = 0; i < _TestData.Count; i++)
				{
					strSaveMessage += _TestData[i].Cate + "," + _TestData[i].strTestName.Replace(',', '_') + "," + _TestData[i].strResult + "," + _TestData[i].SpecMin + "," + _TestData[i].Data + "," + _TestData[i].SpecMax + "," + _TestData[i].Unit + "\r\n";

					if (_SysInfo._listNgInfo[i].Count > 0 && _TestData[i].Cate == "EOL")
					{
						for (int j = 0; j < _SysInfo._listNgInfo[i].Count; j++)
						{
							strSaveMessage += _TestData[i].Cate + "," + _TestData[i].strTestName + $"EOL({j}:{_SysInfo._listNgInfo[i][j].strAddr})" + "," + _SysInfo._listNgInfo[i][j].strTestResult + "," + _SysInfo._listNgInfo[i][j].strSource + "," + _SysInfo._listNgInfo[i][j].strRead + "," + _SysInfo._listNgInfo[i][j].strSource + "," + "" + "\r\n";
						}
					}
				}

				string strMesSaveFilePath = String.Format(@"{0}{1}_EOL.csv", strMESSaveFolderPath, _SysInfo.strSaveFileName.Replace(':', '_'));
				File.WriteAllText(strMesSaveFilePath, strSaveMessage, Encoding.UTF8);

				strMESSaveFolderPath = String.Format(@"MES_BACKUP\\{0}\\{1}\\", _ModelInfo.strModelName, DateTime.Now.ToString("yyMMdd"));
				SaveMESdir = new DirectoryInfo(strMESSaveFolderPath);
				if (SaveMESdir.Exists == false) { SaveMESdir.Create(); }

				strMesSaveFilePath = String.Format(@"{0}{1}_EOL.csv", strMESSaveFolderPath, _SysInfo.strSaveFileName.Replace(':', '_'));
				File.WriteAllText(strMesSaveFilePath, strSaveMessage, Encoding.UTF8);

			}
			catch (Exception ex)
			{
				AppendLogMsg(ex.Message, MSG_TYPE.ERROR);
			}


		}


		public static void SaveResultData2()
		{
			try
			{
				string strSaveFolderPath = String.Format(@"DATA2\\{0}\\{1}\\", _ModelInfo2.strModelName, DateTime.Now.ToString("yyMMdd"));
				DirectoryInfo Savedir = new DirectoryInfo(strSaveFolderPath);
				if (Savedir.Exists == false) { Savedir.Create(); }


				string strSaveMessage = "";

				strSaveMessage += "Barcode," + _SysInfo2.strDispBarcode + "\r\n";
				strSaveMessage += "Mac," + _SysInfo2.strDispMac + "\r\n";
				strSaveMessage += "Test Start Time," + _SysInfo2.dtTestStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n";
				strSaveMessage += "Test End Time," + _SysInfo2.dtTestEndTime.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n";
				strSaveMessage += "Total Result," + _SysInfo2.strTotalResult + "\r\n";

				for (int i = 0; i < _TestData2.Count; i++)
				{
					strSaveMessage += _TestData2[i].Cate + "," + _TestData2[i].strTestName.Replace(',', '_') + "," + _TestData2[i].strResult + "," + _TestData2[i].SpecMin + "," + _TestData2[i].Data + "," + _TestData2[i].SpecMax + "," + _TestData2[i].Unit + "\r\n";

					if (_SysInfo2._listNgInfo[i].Count > 0 && _TestData2[i].Cate == "EOL")
					{
						for (int j = 0; j < _SysInfo2._listNgInfo[i].Count; j++)
						{
							strSaveMessage += _TestData2[i].Cate + "," + _TestData2[i].strTestName + $"EOL({j}:{_SysInfo2._listNgInfo[i][j].strAddr})" + "," + _SysInfo2._listNgInfo[i][j].strTestResult + "," + _SysInfo2._listNgInfo[i][j].strSource + "," + _SysInfo2._listNgInfo[i][j].strRead + "," + _SysInfo2._listNgInfo[i][j].strSource + "," + "" + "\r\n";
						}
					}
				}

				string strSaveFilePath = String.Format(@"{0}{1}_EOL.txt", strSaveFolderPath, _SysInfo2.strSaveFileName.Replace(':', '_'));
				File.AppendAllText(strSaveFilePath, strSaveMessage, Encoding.UTF8);



				// 토탈 데이터 저장행정
				if (!File.Exists(String.Format(@"{0}Total_Data.txt", strSaveFolderPath)))
				{
					strSaveMessage = "";

					strSaveMessage += "Barcode,";
					strSaveMessage += "Mac,";
					strSaveMessage += "Test Start Time,";
					strSaveMessage += "Test End Time,";
					strSaveMessage += "Total Result,";

					//for (int i = 0; i < _TestData2.Count; i++)
					//{
					//	strSaveMessage += _TestData2[i].strTestName.Replace(',', '_') + ",";
					//	if (_SysInfo2._listNgInfo[i].Count > 0 && _TestData2[i].Cate == "EOL")
					//	{
					//		for (int j = 0; j < _SysInfo2._listNgInfo[i].Count; j++)
					//		{
					//			strSaveMessage += _TestData2[i].strTestName + $"EOL({j}:{_SysInfo2._listNgInfo[i][j].strAddr})" + ",";
					//		}
					//	}
					//}	

					for (int i = 0; i < _ModelInfo2._TestInfo.Count; i++)
					{
						strSaveMessage += _ModelInfo2._TestInfo[i].strTestName.Replace(',', '_') + ",";

						if (_ModelInfo2._TestInfo[i].nTestItem == 0)
						{
							int nAddrCount = 0;

							for (int j = 0; j < _ModelInfo2._TestInfo[i]._DataInfo.Count; j++)
							{
								if (_ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 1 ||
									_ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 2 ||
									_ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 6 ||
									_ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 8 ||
									_ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 9)
								{
									strSaveMessage += _ModelInfo2._TestInfo[i].strTestName + $"EOL({nAddrCount++}:{_ModelInfo2._TestInfo[i]._DataInfo[j].strValue1})" + ",";
								}

							}
						}
					}

					strSaveMessage += "\r\n";

					strSaveMessage += ",";
					strSaveMessage += ",";
					strSaveMessage += ",";
					strSaveMessage += ",";
					strSaveMessage += ",";

					//for (int i = 0; i < _TestData2.Count; i++)
					//{
					//	strSaveMessage += _TestData2[i].SpecMin + ",";

					//	if (_SysInfo2._listNgInfo[i].Count > 0 && _TestData2[i].Cate == "EOL")
					//	{
					//		for (int j = 0; j < _SysInfo2._listNgInfo[i].Count; j++)
					//		{
					//			strSaveMessage += _SysInfo2._listNgInfo[i][j].strSource + ",";
					//		}
					//	}
					//}
					for (int i = 0; i < _ModelInfo2._TestInfo.Count; i++)
					{
						strSaveMessage += _TestData2[i].SpecMin + ",";

						if (_ModelInfo2._TestInfo[i].nTestItem == 0)
						{


							for (int j = 0; j < _ModelInfo2._TestInfo[i]._DataInfo.Count; j++)
							{
								if (_ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 1 ||
									_ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 2 ||
									_ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 6 ||
									_ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 8 ||
									_ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 9)
								{
									if (_ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 2 || _ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 9)
									{
										strSaveMessage += $"BUFFER{_ModelInfo2._TestInfo[i]._DataInfo[j].strValue2}" + ",";
									}
									else if (_ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 1 || _ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 8)
									{
										_SysInfo2.nValue2Data = GetFuncData2(_ModelInfo2._TestInfo[i]._DataInfo[j].strValue2.ToString());
										strSaveMessage += _SysInfo2.nValue2Data.ToString("X4") + ",";
									}
									else
									{
										strSaveMessage += _ModelInfo2._TestInfo[i]._DataInfo[j].strValue2 + ",";
									}

								}

							}
						}
					}
					strSaveMessage += "\r\n";

					strSaveMessage += ",";
					strSaveMessage += ",";
					strSaveMessage += ",";
					strSaveMessage += ",";
					strSaveMessage += ",";
					//for (int i = 0; i < _TestData2.Count; i++)
					//{
					//	strSaveMessage += _TestData2[i].SpecMax + ",";

					//	if (_SysInfo2._listNgInfo[i].Count > 0 && _TestData2[i].Cate == "EOL")
					//	{
					//		for (int j = 0; j < _SysInfo2._listNgInfo[i].Count; j++)
					//		{
					//			strSaveMessage += _SysInfo2._listNgInfo[i][j].strSource + ",";
					//		}
					//	}
					//}
					for (int i = 0; i < _ModelInfo2._TestInfo.Count; i++)
					{
						strSaveMessage += _TestData2[i].SpecMax + ",";

						if (_ModelInfo2._TestInfo[i].nTestItem == 0)
						{
							//int nAddrCount = 0;

							for (int j = 0; j < _ModelInfo2._TestInfo[i]._DataInfo.Count; j++)
							{
								if (_ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 1 ||
									_ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 2 ||
									_ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 6 ||
									_ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 8 ||
									_ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 9)
								{
									if (_ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 2 || _ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 9)
									{
										strSaveMessage += $"BUFFER{_ModelInfo2._TestInfo[i]._DataInfo[j].strValue2}" + ",";
									}
									else if (_ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 1 || _ModelInfo2._TestInfo[i]._DataInfo[j].nTestType == 8)
									{
										_SysInfo2.nValue2Data = GetFuncData2(_ModelInfo2._TestInfo[i]._DataInfo[j].strValue2.ToString());
										strSaveMessage += _SysInfo2.nValue2Data.ToString("X4") + ",";
									}
									else
									{
										strSaveMessage += _ModelInfo2._TestInfo[i]._DataInfo[j].strValue2 + ",";
									}

								}

							}
						}
					}
					strSaveMessage += "\r\n";

					strSaveFilePath = String.Format(@"{0}Total_Data.txt", strSaveFolderPath);
					File.AppendAllText(strSaveFilePath, strSaveMessage, Encoding.UTF8);
				}

				strSaveMessage = "";
				strSaveMessage += _SysInfo2.strDispBarcode + ",";
				strSaveMessage += _SysInfo2.strDispMac + ",";
				strSaveMessage += _SysInfo2.dtTestStartTime.ToString("yyyy-MM-dd HH:mm:ss") + ",";
				strSaveMessage += _SysInfo2.dtTestEndTime.ToString("yyyy-MM-dd HH:mm:ss") + ",";
				strSaveMessage += _SysInfo2.strTotalResult + ",";

				for (int i = 0; i < _TestData2.Count; i++)
				{
					if (_TestData2[i].Data == "")
					{
						strSaveMessage += _TestData2[i].strResult + ",";
					}
					else
					{
						strSaveMessage += _TestData2[i].Data + ",";
					}

					if (_SysInfo2._listNgInfo[i].Count > 0 && _TestData2[i].Cate == "EOL")
					{
						for (int j = 0; j < _SysInfo2._listNgInfo[i].Count; j++)
						{
							strSaveMessage += _SysInfo2._listNgInfo[i][j].strRead + ",";
						}
					}
				}
				strSaveMessage += "\r\n";

				strSaveFilePath = String.Format(@"{0}Total_Data.txt", strSaveFolderPath);
				File.AppendAllText(strSaveFilePath, strSaveMessage, Encoding.UTF8);






				//MES 데이터 저장행정 ( MES 폴더에 저장 )
				string strMESSaveFolderPath = String.Format("{0}\\", _Config.strSaveMesDir2);
				DirectoryInfo SaveMESdir = new DirectoryInfo(strMESSaveFolderPath);
				if (SaveMESdir.Exists == false) { SaveMESdir.Create(); }

				strSaveMessage = "";

				//strSaveMessage += "Barcode,";
				//strSaveMessage += "Mac,";
				//strSaveMessage += "Test Start Time,";
				//strSaveMessage += "Test End Time,";
				//strSaveMessage += "Total Result,";

				//for (int i = 0; i < _TestData2.Count; i++)
				//{
				//	strSaveMessage += _TestData2[i].strTestName.Replace(',', '_') + ",";
				//}
				//strSaveMessage += "\r\n";


				////strSaveMessage += ",";
				////strSaveMessage += ",";
				////strSaveMessage += ",";
				////strSaveMessage += ",";
				////strSaveMessage += ",";

				////for (int i = 0; i < _TestData2.Count; i++)
				////{
				////	strSaveMessage += _TestData2[i].SpecMin + ",";
				////	if (_SysInfo2._listNgInfo[i].Count > 0 && _TestData2[i].Cate == "EOL")
				////	{
				////		for (int j = 0; j < _SysInfo2._listNgInfo[i].Count; j++)
				////		{
				////			strSaveMessage += _SysInfo2._listNgInfo[i][j].strSource + ",";
				////		}
				////	}
				////}
				////strSaveMessage += "\r\n";

				////strSaveMessage += ",";
				////strSaveMessage += ",";
				////strSaveMessage += ",";
				////strSaveMessage += ",";
				////strSaveMessage += ",";
				////for (int i = 0; i < _TestData2.Count; i++)
				////{
				////	strSaveMessage += _TestData2[i].SpecMax + ",";
				////	if (_SysInfo2._listNgInfo[i].Count > 0 && _TestData2[i].Cate == "EOL")
				////	{
				////		for (int j = 0; j < _SysInfo2._listNgInfo[i].Count; j++)
				////		{
				////			strSaveMessage += _SysInfo2._listNgInfo[i][j].strSource + ",";
				////		}
				////	}
				////}
				////strSaveMessage += "\r\n";

				////strSaveMessage = "";
				//strSaveMessage += _SysInfo2.strDispBarcode + ",";
				//strSaveMessage += _SysInfo2.strDispMac + ",";
				//strSaveMessage += _SysInfo2.dtTestStartTime.ToString("yyyy-MM-dd HH:mm:ss") + ",";
				//strSaveMessage += _SysInfo2.dtTestEndTime.ToString("yyyy-MM-dd HH:mm:ss") + ",";
				//strSaveMessage += _SysInfo2.strTotalResult + ",";

				//for (int i = 0; i < _TestData2.Count; i++)
				//{
				//	if (_TestData2[i].Data == "")
				//	{
				//		strSaveMessage += _TestData2[i].strResult + ",";
				//	}
				//	else
				//	{
				//		strSaveMessage += _TestData2[i].Data + ",";
				//	}
				//}
				//strSaveMessage += "\r\n";

				strSaveMessage += "Barcode," + _SysInfo2.strDispBarcode + "\r\n";
				strSaveMessage += "Mac," + _SysInfo2.strDispMac + "\r\n";
				strSaveMessage += "Test Start Time," + _SysInfo2.dtTestStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n";
				strSaveMessage += "Test End Time," + _SysInfo2.dtTestEndTime.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n";
				strSaveMessage += "Total Result," + _SysInfo2.strTotalResult + "\r\n";

				for (int i = 0; i < _TestData2.Count; i++)
				{
					strSaveMessage += _TestData2[i].Cate + "," + _TestData2[i].strTestName.Replace(',', '_') + "," + _TestData2[i].strResult + "," + _TestData2[i].SpecMin + "," + _TestData2[i].Data + "," + _TestData2[i].SpecMax + "," + _TestData2[i].Unit + "\r\n";

					if (_SysInfo2._listNgInfo[i].Count > 0 && _TestData2[i].Cate == "EOL")
					{
						for (int j = 0; j < _SysInfo2._listNgInfo[i].Count; j++)
						{
							strSaveMessage += _TestData2[i].Cate + "," + _TestData2[i].strTestName + $"EOL({j}:{_SysInfo2._listNgInfo[i][j].strAddr})" + "," + _SysInfo2._listNgInfo[i][j].strTestResult + "," + _SysInfo2._listNgInfo[i][j].strSource + "," + _SysInfo2._listNgInfo[i][j].strRead + "," + _SysInfo2._listNgInfo[i][j].strSource + "," + "" + "\r\n";
						}
					}
				}

				string strMesSaveFilePath = String.Format(@"{0}{1}_EOL.csv", strMESSaveFolderPath, _SysInfo2.strSaveFileName.Replace(':', '_'));
				File.WriteAllText(strMesSaveFilePath, strSaveMessage, Encoding.UTF8);

				strMESSaveFolderPath = String.Format(@"MES_BACKUP\\{0}\\{1}\\", _ModelInfo2.strModelName, DateTime.Now.ToString("yyMMdd"));
				SaveMESdir = new DirectoryInfo(strMESSaveFolderPath);
				if (SaveMESdir.Exists == false) { SaveMESdir.Create(); }

				strMesSaveFilePath = String.Format(@"{0}{1}_EOL.csv", strMESSaveFolderPath, _SysInfo2.strSaveFileName.Replace(':', '_'));
				File.WriteAllText(strMesSaveFilePath, strSaveMessage, Encoding.UTF8);

			}
			catch (Exception ex)
			{
				AppendLogMsg(ex.Message, MSG_TYPE.ERROR);
			}


		}

		public static bool CheckBarcode(string readBarcode, string strTargetBarcode)
		{
			if (readBarcode.Length != strTargetBarcode.Length)
			{
				return false;
			}
			else
			{

				for (int nBCDLen = 0; nBCDLen < strTargetBarcode.Length; nBCDLen++)
				{
					if (strTargetBarcode[nBCDLen] == '*')  // 아무거나
					{

					}
					else if (strTargetBarcode[nBCDLen] == '@')   // 영문자
					{
						if (readBarcode[nBCDLen] < 0x41 || readBarcode[nBCDLen] > 0x7A)
						{
							return false;
						}
					}
					else if (strTargetBarcode[nBCDLen] == '#')   // 숫자
					{
						if (readBarcode[nBCDLen] < 0x30 || readBarcode[nBCDLen] > 0x39)
						{
							return false;
						}
					}
					else
					{
						if (strTargetBarcode[nBCDLen] != readBarcode[nBCDLen]) // 그외 경우 일치하지 않으면 NG
						{
							return false;
						}
					}
				}

			}

			return true;
		}



		public static bool GetCompareCanData(string strSource, byte btReadValue)
		{
			byte btConvert = 0x00;

			if (strSource == "*")
			{
				return true;
			}
			else
			{
				byte.TryParse(strSource, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out btConvert);

				if (btConvert == btReadValue)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		
		public static void CyclonInFirWare(int HandleNum)
		{
			UInt32 handle1 = 0;

			bool cyclone1done = false;


			//connection_type = convert_dropboxindex_to_connectiontype(1);
			handle1 = cyclone_control_api.connectToCyclone(_Config.strCycloneMyIP);
			if (handle1 == 0)
			{
				theApp.AppendLogMsg($"Error Opening Device", MSG_TYPE.LOG);
				_CyStatus = CYCLON_STATUS.NG;

			}
			else
			{
				theApp.AppendLogMsg($"Programming Image on IP1 ...", MSG_TYPE.LOG);

				cyclone_control_api.startImageExecution(handle1, Convert.ToByte(HandleNum));
				
			}


			System.Windows.Forms.Application.DoEvents();

			do
			{

				if (cyclone_control_api.checkCycloneExecutionStatus(handle1) == 0)
				{
					if (cyclone_control_api.getNumberOfErrors(handle1) == 0)
					{
						theApp.AppendLogMsg($"Programming was successful.", MSG_TYPE.LOG);
						_CyStatus = CYCLON_STATUS.OK;
						cyclone1done = true;
					}
					else
					{
						theApp.AppendLogMsg($"Error Code = {cyclone_control_api.getErrorCode(handle1, 1).ToString()}", MSG_TYPE.LOG);
						_CyStatus = CYCLON_STATUS.NG;
						cyclone1done = true;
					}
				


				}

			} while (!cyclone1done);

			
		}
		
		
		public static void CyclonReadFirWareName(int FileInNum, string strSearchString)
		{
		
			UInt32 handle = 0;
			string strName = "";
			//theApp.AppendLogMsg($"Contacting IP{_Config.strCycloneMyIP}", MSG_TYPE.LOG);
			//System.Windows.Forms.Application.DoEvents();
			//connection_type = convert_dropboxindex_to_connectiontype(1);
			handle = cyclone_control_api.connectToCyclone(_Config.strCycloneMyIP);
			
			if (handle == 0)
			{
				theApp.AppendLogMsg("Error Opening Device.", MSG_TYPE.LOG);
				//_SysInfo.bGetFileNameOK = true;
				_SysInfo._FileNameResult = CyclonFileName_RESULT.NG;
			}
			else
			{
				_SysInfo.strCyclonFileName = cyclone_control_api.getImageDescription(handle, Convert.ToByte(FileInNum));
				
				//_SysInfo.bGetFileNameOK = true;
				theApp.AppendLogMsg($"{cyclone_control_api.getImageDescription(handle, Convert.ToByte(FileInNum))}", MSG_TYPE.INFO);

				
				if (_SysInfo.strCyclonFileName.Contains(strSearchString))
				{
					_SysInfo._FileNameResult = CyclonFileName_RESULT.OK;
				}
				else
				{
					_SysInfo._FileNameResult = CyclonFileName_RESULT.NG;
				}
			}


		}

		

		public static void CyclonInFirWare2(int HandleNum)
		{
			UInt32 handle1 = 0;

			bool cyclone1done = false;


			//connection_type = convert_dropboxindex_to_connectiontype(1);
			handle1 = cyclone_control_api2.connectToCyclone(_Config.strCycloneMyIP2);
			if (handle1 == 0)
			{
				theApp.AppendLogMsg2($"Error Opening Device", MSG_TYPE.LOG);
				_CyStatus2 = CYCLON_STATUS2.NG;

			}
			else
			{
				theApp.AppendLogMsg2($"Programming Image on IP2 ...", MSG_TYPE.LOG);
				cyclone_control_api2.startImageExecution(handle1, Convert.ToByte(HandleNum));

			}


			System.Windows.Forms.Application.DoEvents();

			do
			{

				if (cyclone_control_api2.checkCycloneExecutionStatus(handle1) == 0)
				{
					if (cyclone_control_api2.getNumberOfErrors(handle1) == 0)
					{
						theApp.AppendLogMsg2($"Programming was successful.", MSG_TYPE.LOG);
						_CyStatus2 = CYCLON_STATUS2.OK;
						cyclone1done = true;
					}
					else
					{
						theApp.AppendLogMsg2($"Error Code = {cyclone_control_api2.getErrorCode(handle1, 1).ToString()}", MSG_TYPE.LOG);
						_CyStatus2 = CYCLON_STATUS2.NG;
						cyclone1done = true;
					}

				}

			} while (!cyclone1done);
			


		}

		public static void CyclonReadFirWareName2(int FileInNum, string strSearchString)
		{

			UInt32 handle = 0;
			string strName = "";
			//theApp.AppendLogMsg($"Contacting IP{_Config.strCycloneMyIP}", MSG_TYPE.LOG);
			//System.Windows.Forms.Application.DoEvents();
			//connection_type = convert_dropboxindex_to_connectiontype(1);
			handle = cyclone_control_api2.connectToCyclone(_Config.strCycloneMyIP);

			

			if (handle == 0)
			{
				theApp.AppendLogMsg2("Error Opening Device.", MSG_TYPE.LOG);
				//_SysInfo.bGetFileNameOK = true;
				_SysInfo2._FileNameResult = CyclonFileName_RESULT2.NG;
			}
			else
			{
				_SysInfo2.strCyclonFileName = cyclone_control_api2.getImageDescription(handle, Convert.ToByte(FileInNum));

				//_SysInfo.bGetFileNameOK = true;
				theApp.AppendLogMsg2($"{cyclone_control_api2.getImageDescription(handle, Convert.ToByte(FileInNum))}", MSG_TYPE.INFO);


				if (_SysInfo2.strCyclonFileName.Contains(strSearchString))
				{
					_SysInfo2._FileNameResult = CyclonFileName_RESULT2.OK;
				}
				else
				{
					_SysInfo2._FileNameResult = CyclonFileName_RESULT2.NG;
				}
			}


		}
		//public static void CyclonReadCRC(int FileInNum, string strSearchString)
		//{

		//	UInt32 handle = 0;
		//	string strName = "";
		//	//theApp.AppendLogMsg($"Contacting IP{_Config.strCycloneMyIP}", MSG_TYPE.LOG);
		//	//System.Windows.Forms.Application.DoEvents();
		//	//connection_type = convert_dropboxindex_to_connectiontype(1);
		//	handle = cyclone_control_api2.connectToCyclone(_Config.strCycloneMyIP2);

		//	if (handle == 0)
		//	{
		//		theApp.AppendLogMsg2("Error Opening Device.", MSG_TYPE.LOG);
		//		//_SysInfo.bGetFileNameOK = true;
		//		_SysInfo2._FileNameResult = CyclonFileName_RESULT2.NG;
		//	}
		//	else
		//	{
		//		_SysInfo2.strCyclonFileName = cyclone_control_api2.getImageDescription(handle, Convert.ToByte(FileInNum));

		//		//_SysInfo.bGetFileNameOK = true;
		//		theApp.AppendLogMsg2($"{cyclone_control_api2.getImageDescription(handle, Convert.ToByte(FileInNum))}", MSG_TYPE.INFO);


		//		if (_SysInfo2.strCyclonFileName.Contains(strSearchString))
		//		{
		//			_SysInfo2._FileNameResult = CyclonFileName_RESULT2.OK;
		//		}
		//		else
		//		{
		//			_SysInfo2._FileNameResult = CyclonFileName_RESULT2.NG;
		//		}
		//	}


		//}

		public static int GetFuncData(string strData)
		{
			int nResult = 0;
			//int nWirteSerialNum = 0;

			//int.TryParse(_LotCount.tProductClearTime.ToString("yyyyMMdd"), out nWirteSerialNum);
			//nWirteSerialNum *= 10000;
			//nWirteSerialNum += _LotCount.nLotCount;

			if (strData == "$SN1")
			{
				nResult = (int)(_SysInfo.nWriteSerialNum / 0x10000);
			}
			else if (strData == "$SN2")
			{
				nResult = (int)(_SysInfo.nWriteSerialNum % 0x10000);
			}
			else if (strData == "$MAC1")
			{
				nResult = _SysInfo.nReadMacHigh;
			}
			else if (strData == "$MAC2")
			{
				nResult = _SysInfo.nReadMacMid;
			}
			else if (strData == "$MAC3")
			{
				nResult = _SysInfo.nReadMacLow;
			}
			else
			{
				int.TryParse(strData, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out nResult);
			}

			return nResult;
		}

		public static int GetFuncData2(string strData)
		{
			int nResult = 0;
			//int nWirteSerialNum = 0;

			//int.TryParse(_LotCount.tProductClearTime.ToString("yyyyMMdd"), out nWirteSerialNum);
			//nWirteSerialNum *= 10000;
			//nWirteSerialNum += _LotCount.nLotCount;

			if (strData == "$SN1")
			{
				nResult = (int)(_SysInfo2.nWriteSerialNum / 0x10000);
			}
			else if (strData == "$SN2")
			{
				nResult = (int)(_SysInfo2.nWriteSerialNum % 0x10000);
			}
			else if (strData == "$MAC1")
			{
				nResult = _SysInfo2.nReadMacHigh;
			}
			else if (strData == "$MAC2")
			{
				nResult = _SysInfo2.nReadMacMid;
			}
			else if (strData == "$MAC3")
			{
				nResult = _SysInfo2.nReadMacLow;
			}
			else
			{
				int.TryParse(strData, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out nResult);
			}

			return nResult;
		}

		public static void SendCanData(int nCh, int nID, int[] nData, int nCount, int nStation)
		{
			byte[] btSendModByte = new byte[nCount];
			for (int i = 0; i < nCount; i++)
			{
				btSendModByte[i] = (byte)nData[i];
			}

			//_CanComm[nCh].WriteFrame(nID, btSendModByte, nCount, Peak.Can.Basic.TPCANMessageType.PCAN_MESSAGE_EXTENDED);
			_CanComm[nCh].WriteFrame(nCh, nID, btSendModByte, nStation);

		}

		public static void SendWriteCommand(int nCh, int nAddress, int nData, int nStation)
		{
			byte[] btSendData = new byte[8];

			btSendData[0] = 0x06;
			btSendData[1] = (byte)(nAddress / 0x100);
			btSendData[2] = (byte)(nAddress % 0x100);
			btSendData[3] = (byte)(nData / 0x100);
			btSendData[4] = (byte)(nData % 0x100);

			uint nCrcData = ModRTU_CRC(new byte[] { 0x01, btSendData[0], btSendData[1], btSendData[2], btSendData[3], btSendData[4] }, 6);

			btSendData[5] = (byte)(nCrcData % 0x100);
			btSendData[6] = (byte)(nCrcData / 0x100);
			//_CanComm[nCh].bReadMessage = false;
			//_CanComm[nCh].WriteFrame(0x10001, new byte[] { btSendData[0], btSendData[1], btSendData[2], btSendData[3], btSendData[4], btSendData[5], btSendData[6] }, 7, Peak.Can.Basic.TPCANMessageType.PCAN_MESSAGE_EXTENDED);

			_CanComm[nCh].WriteFrame(nCh, 0x10001, new byte[] { btSendData[0], btSendData[1], btSendData[2], btSendData[3], btSendData[4], btSendData[5], btSendData[6] }, nStation);

		}

		public static void SendReadCommand(int nCh, int nAddress, int nStation)
		{
			byte[] btSendData = new byte[8];

			btSendData[0] = 0x03;
			btSendData[1] = (byte)(nAddress / 0x100);
			btSendData[2] = (byte)(nAddress % 0x100);
			btSendData[3] = 0x00;
			btSendData[4] = 0x01;

			uint nCrcData = ModRTU_CRC(new byte[] { 0x01, btSendData[0], btSendData[1], btSendData[2], btSendData[3], btSendData[4] }, 6);

			btSendData[5] = (byte)(nCrcData % 0x100);
			btSendData[6] = (byte)(nCrcData / 0x100);
			_CanComm[nCh].bReadMessage = false;

			//_CanComm[nCh].WriteFrame(0x10001, new byte[] { btSendData[0], btSendData[1], btSendData[2], btSendData[3], btSendData[4], btSendData[5], btSendData[6] }, 7, Peak.Can.Basic.TPCANMessageType.PCAN_MESSAGE_EXTENDED);
			_CanComm[nCh].WriteFrame(nCh, 0x10001, new byte[] { btSendData[0], btSendData[1], btSendData[2], btSendData[3], btSendData[4], btSendData[5], btSendData[6] }, nStation);

		}



		public static void SendWriteMultiCommand(int nCh, int nStartAddr, int[] nData, int nCount, int nStation)
		{
			byte[] btSendData = new byte[(nCount * 2) + 8];

			btSendData[0] = 0x10;
			btSendData[1] = (byte)(nStartAddr / 0x100);
			btSendData[2] = (byte)(nStartAddr % 0x100);
			btSendData[3] = 0x00;
			btSendData[4] = (byte)nCount;
			btSendData[5] = (byte)(nCount * 2);

			for (int i = 0; i < nCount; i++)
			{
				btSendData[6 + (i * 2)] = (byte)(nData[i] / 0x100);
				btSendData[6 + (i * 2) + 1] = (byte)(nData[i] % 0x100);
			}

			byte[] btCRCCalc = new byte[(btSendData.Length - 2) + 1];

			btCRCCalc[0] = 0x01;
			for (int i = 0; i < btCRCCalc.Length - 1; i++)
			{
				btCRCCalc[1 + i] = btSendData[i];
			}

			uint nCrcData = ModRTU_CRC(btCRCCalc, btCRCCalc.Length);

			btSendData[6 + (nCount * 2)] = (byte)(nCrcData % 0x100);
			btSendData[7 + (nCount * 2)] = (byte)(nCrcData / 0x100);

			int nSendCount = btSendData.Length / 8;
			int nSendModCount = btSendData.Length % 8;

			if (nSendModCount == 0) { nSendCount--; }

			for (int i = 0; i < nSendCount; i++)
			{
				//uint nSendId = (uint)(nSendCount + 1) * 0x10000;
				//uint nSendNowId = (uint)i * 0x100;
				//uint nSendSlave = 0x01;

				int nSendId = (nSendCount + 1) * 0x10000;
				int nSendNowId = i * 0x100;
				int nSendSlave = 0x01;

				//_CanComm[nCh].WriteFrame(nSendId + nSendNowId + nSendSlave, new byte[] { btSendData[(i * 8) + 0], btSendData[(i * 8) + 1], btSendData[(i * 8) + 2], btSendData[(i * 8) + 3], btSendData[(i * 8) + 4], btSendData[(i * 8) + 5], btSendData[(i * 8) + 6], btSendData[(i * 8) + 7] }, 8, Peak.Can.Basic.TPCANMessageType.PCAN_MESSAGE_EXTENDED);
				_CanComm[nCh].WriteFrame(nCh, nSendId + nSendNowId + nSendSlave, new byte[] { btSendData[(i * 8) + 0], btSendData[(i * 8) + 1], btSendData[(i * 8) + 2], btSendData[(i * 8) + 3], btSendData[(i * 8) + 4], btSendData[(i * 8) + 5], btSendData[(i * 8) + 6], btSendData[(i * 8) + 7] }, nStation);

			}

			if (nSendModCount == 0)
			{
				byte[] btSendModByte = new byte[8];
				for (int i = 0; i < 8; i++)
				{
					btSendModByte[i] = btSendData[nSendCount * 8 + i];
				}

				//uint nSendId = (uint)(nSendCount + 1) * 0x10000;
				//uint nSendNowId = (uint)nSendCount * 0x100;
				//uint nSendSlave = 0x01;

				int nSendId = (nSendCount + 1) * 0x10000;
				int nSendNowId = nSendCount * 0x100;
				int nSendSlave = 0x01;

				//_CanComm[nCh].WriteFrame(nSendId + nSendNowId + nSendSlave, btSendModByte, 8, Peak.Can.Basic.TPCANMessageType.PCAN_MESSAGE_EXTENDED);
				_CanComm[nCh].WriteFrame(nCh, nSendId + nSendNowId + nSendSlave, btSendModByte, nStation);

			}

			if (nSendModCount > 0)
			{
				byte[] btSendModByte = new byte[nSendModCount];
				for (int i = 0; i < nSendModCount; i++)
				{
					btSendModByte[i] = btSendData[nSendCount * 8 + i];
				}

				//uint nSendId = (uint)(nSendCount + 1) * 0x10000;
				//uint nSendNowId = (uint)nSendCount * 0x100;
				//uint nSendSlave = 0x01;

				int nSendId = (nSendCount + 1) * 0x10000;
				int nSendNowId = nSendCount * 0x100;
				int nSendSlave = 0x01;

				//_CanComm[nCh].WriteFrame(nSendId + nSendNowId + nSendSlave, btSendModByte, nSendModCount, Peak.Can.Basic.TPCANMessageType.PCAN_MESSAGE_EXTENDED);
				_CanComm[nCh].WriteFrame(nCh, nSendId + nSendNowId + nSendSlave, btSendModByte, nStation);

			}
		}









		public static void ShowPopUpWindow()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).ShowPopupMessage();
			});

		}

		public static void ShowPopUpWindow2()
		{

			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).ShowPopupMessage2();
			});
			
		}

		public static void ShowRetryPopUpWindow()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).ShowRetryPopupMessage();
			});

		}

		public static void ShowRetryPopUpWindow2()
		{

			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).ShowRetryPopupMessage2();
			});

		}

		public static void ShowNGPopUpWindow()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).ShowNGPopupMessage();
			});

		}

		public static void ShowNGPopUpWindow2()
		{

			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).ShowNGPopupMessage2();
			});

		}

		// 팝업 열기 및 동작

		public static void ShowMasterPopUpWindow()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{

				if (_MasterPopup == null)
				{
					_MasterPopup = new MasterPopup();
					_MasterPopup.Show();
				}
				else
				{
					_MasterPopup.Show();
					_MasterPopup.Activate();
				}

			});
		}

		public static void CloseMasterPopUpWindow()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				if (_MasterPopup != null && _MasterPopup.IsActive)
				{
					_MasterPopup.Hide();
				}

			});
		}
		public static void ShowBcdPopUpWindow()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).ShowBcdCheckMessege();
			});
		}

		public static void CloseBcdPopUpWindow()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).HideBcdCheckMessege();
			});
		}

		public static void ShowBcdPopUpWindow2()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).ShowBcdCheckMessege2();
			});
		}

		public static void CloseBcdPopUpWindow2()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).HideBcdCheckMessege2();
			});
		}


		public static void ClosePopUpWindow()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).HidePopupMessage();
			});
			//App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			//{
			//	if (_PopUpWindow != null && _PopUpWindow.IsActive)
			//	{
			//		_PopUpWindow.Hide();
			//	}

			//});
		}

		public static void ClosePopUpWindow2()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).HidePopupMessage2();
			});

		}

		public static void CloseNGPopUpWindow()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).HideNGPopupMessage();
			});
			//App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			//{
			//	if (_PopUpWindow != null && _PopUpWindow.IsActive)
			//	{
			//		_PopUpWindow.Hide();
			//	}

			//});
		}


		public static void CloseNGPopUpWindow2()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).HideNGPopupMessage2();
			});

		}

		public static void CloseRetryPopUpWindow()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).HideRetryPopupMessage();
			});
			//App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			//{
			//	if (_PopUpWindow != null && _PopUpWindow.IsActive)
			//	{
			//		_PopUpWindow.Hide();
			//	}

			//});
		}

		public static void CloseRetryPopUpWindow2()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).HideRetryPopupMessage2();
			});
			//App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			//{
			//	if (_PopUpWindow != null && _PopUpWindow.IsActive)
			//	{
			//		_PopUpWindow.Hide();
			//	}

			//});
		}

		static void ShowUserStartMessege()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).ShowUserStartMsgMessege();
			});
		}
		static void HideUserStartMessege()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).HideUserStartMsgMessege();
			});
		}

		// 카운터 로드
		public static void ShowUserStartMessege2()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).ShowStartMessage2();
			});
			//App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			//{

			//	if (_UserStartMassage2 == null)
			//	{
			//		_UserStartMassage2 = new UserStartMassage2();
			//		_UserStartMassage2.Show();
			//	}
			//	else
			//	{
			//		_UserStartMassage2.Show();
			//		_UserStartMassage2.Activate();
			//	}

			//});
		}


		public static void HideUserStartMessege2()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).HideStartMessage2();
			});
			//App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			//{
			//	if (_UserStartMassage2 != null)
			//	{
			//		_UserStartMassage2.Hide();
			//	}

			//});
		}

		static void ShowUserNutMessege()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).ShowUserNutMsgMessege();
			});
		}
		static void HideUserNutMessege()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).HideUserNutMsgMessege();
			});
		}

		static void ShowUserNutMessege2()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).ShowUserNutMsgMessege2();
			});
		}
		static void HideUserNutMessege2()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).HideUserNutMsgMessege2();
			});
		}

		static void ShowNutRetryMessege()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).ShowNutRetryMessege();
			});
		}
		static void HideNutRetryMessege()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).HideNutRetryMessege();
			});
		}

		static void ShowNutRetryMessege2()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).ShowNutRetryMessege2();
			});
		}
		static void HideNutRetryMessege2()
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).HideNutRetryMessege2();
			});
		}


		public static UInt16 ModRTU_CRC(byte[] buf, int len)
		{
			UInt16 crc = 0xFFFF;

			for (int pos = 0; pos < len; pos++)
			{
				crc ^= (UInt16)buf[pos];          // XOR byte into least sig. byte of crc

				for (int i = 8; i != 0; i--)
				{    // Loop over each bit
					if ((crc & 0x0001) != 0)
					{      // If the LSB is set
						crc >>= 1;                    // Shift right and XOR 0xA001
						crc ^= 0xA001;
					}
					else                            // Else LSB is not set
						crc >>= 1;                    // Just shift right
				}
			}
			// Note, this number has low and high bytes swapped, so use it accordingly (or swap bytes)
			return crc;
		}


		public static UInt16 ModRTU_CRC(int[] nbuf, int len)
		{
			UInt16 crc = 0xFFFF;

			for (int pos = 0; pos < len; pos++)
			{
				crc ^= (UInt16)nbuf[pos];          // XOR byte into least sig. byte of crc

				for (int i = 8; i != 0; i--)
				{    // Loop over each bit
					if ((crc & 0x0001) != 0)
					{      // If the LSB is set
						crc >>= 1;                    // Shift right and XOR 0xA001
						crc ^= 0xA001;
					}
					else                            // Else LSB is not set
						crc >>= 1;                    // Just shift right
				}
			}
			// Note, this number has low and high bytes swapped, so use it accordingly (or swap bytes)
			return crc;
		}

		//====== LOT CLEAR ==========================================
		static void LotCountInfoUpdate()
		{
			if (_Config.bUseLotAutoClear)
			{


				if (DateTime.Now.Year == _LotCount.tProductClearTime.Year &&
					(DateTime.Now.Month != _LotCount.tProductClearTime.Month ||
					DateTime.Now.Day != _LotCount.tProductClearTime.Day))              // 년이 같은경우 DayofYear에 의한 초기화
				{
					if (DateTime.Now.DayOfYear - _LotCount.tProductClearTime.DayOfYear > 1)
					{
						_LotCount.nOkCount = 0;
						_LotCount.nNGCount = 0;
						_LotCount.nLotCount = 0;
						_LotCount.tProductClearTime = DateTime.Now;
						//SaveProductCount();
						SaveModelProductCount(_LotCount, _ModelInfo.strModelName);
					}
					else
					{
						if (DateTime.Now.Hour > (_Config.nLotClearTime / 100))
						{
							// 시간이 크면 초기화
							_LotCount.nOkCount = 0;
							_LotCount.nNGCount = 0;
							_LotCount.nLotCount = 0;
							_LotCount.tProductClearTime = DateTime.Now;
							//SaveProductCount();
							SaveModelProductCount(_LotCount, _ModelInfo.strModelName);
						}
						else if (DateTime.Now.Hour == (_Config.nLotClearTime / 100) &&
							DateTime.Now.Minute >= (_Config.nLotClearTime % 100))
						{
							// 시간이 같고 분이 크면 초기화
							_LotCount.nOkCount = 0;
							_LotCount.nNGCount = 0;
							_LotCount.nLotCount = 0;
							_LotCount.tProductClearTime = DateTime.Now;
							//SaveProductCount();
							SaveModelProductCount(_LotCount, _ModelInfo.strModelName);
						}
					}
				}
				else if (DateTime.Now.Year != _LotCount.tProductClearTime.Year)             // 년이 다른 경우 클리어 날짜가 12월 31일 이고 오늘 날짜가 1월 1일이면 지정한 시간에 초기화
				{
					if (DateTime.Now.Month == 1 && DateTime.Now.Day == 1 && _LotCount.tProductClearTime.Month == 12 && _LotCount.tProductClearTime.Day == 31)
					{
						if (DateTime.Now.Hour > (_Config.nLotClearTime / 100))
						{
							_LotCount.nOkCount = 0;
							_LotCount.nNGCount = 0;
							_LotCount.nLotCount = 0;
							_LotCount.tProductClearTime = DateTime.Now;
							//SaveProductCount();
							SaveModelProductCount(_LotCount, _ModelInfo.strModelName);
						}
						else if (DateTime.Now.Hour == (_Config.nLotClearTime / 100) &&
							DateTime.Now.Minute >= (_Config.nLotClearTime % 100))
						{
							_LotCount.nOkCount = 0;
							_LotCount.nNGCount = 0;
							_LotCount.nLotCount = 0;
							_LotCount.tProductClearTime = DateTime.Now;
							//SaveProductCount();
							SaveModelProductCount(_LotCount, _ModelInfo.strModelName);
						}
					}
					else
					{
						_LotCount.nOkCount = 0;
						_LotCount.nNGCount = 0;
						_LotCount.nLotCount = 0;
						_LotCount.tProductClearTime = DateTime.Now;
						//SaveProductCount();
						SaveModelProductCount(_LotCount, _ModelInfo.strModelName);
					}
				}
			}

		}
		static void LotCountInfoUpdate2()
		{
			if (_Config.bUseLotAutoClear)
			{


				if (DateTime.Now.Year == _LotCount2.tProductClearTime.Year &&
					(DateTime.Now.Month != _LotCount2.tProductClearTime.Month ||
					DateTime.Now.Day != _LotCount2.tProductClearTime.Day))              // 년이 같은경우 DayofYear에 의한 초기화
				{
					if (DateTime.Now.DayOfYear - _LotCount2.tProductClearTime.DayOfYear > 1)
					{
						_LotCount2.nOkCount = 0;
						_LotCount2.nNGCount = 0;
						_LotCount2.nLotCount = 0;
						_LotCount2.tProductClearTime = DateTime.Now;
						//SaveProductCount();
						SaveModelProductCount2(_LotCount2, _ModelInfo2.strModelName);
					}
					else
					{
						if (DateTime.Now.Hour > (_Config.nLotClearTime / 100))
						{
							// 시간이 크면 초기화
							_LotCount2.nOkCount = 0;
							_LotCount2.nNGCount = 0;
							_LotCount2.nLotCount = 0;
							_LotCount2.tProductClearTime = DateTime.Now;
							//SaveProductCount();
							SaveModelProductCount2(_LotCount2, _ModelInfo2.strModelName);
						}
						else if (DateTime.Now.Hour == (_Config.nLotClearTime / 100) &&
							DateTime.Now.Minute >= (_Config.nLotClearTime % 100))
						{
							// 시간이 같고 분이 크면 초기화
							_LotCount2.nOkCount = 0;
							_LotCount2.nNGCount = 0;
							_LotCount2.nLotCount = 0;
							_LotCount2.tProductClearTime = DateTime.Now;
							//SaveProductCount();
							SaveModelProductCount2(_LotCount2, _ModelInfo2.strModelName);
						}
					}
				}
				else if (DateTime.Now.Year != _LotCount2.tProductClearTime.Year)             // 년이 다른 경우 클리어 날짜가 12월 31일 이고 오늘 날짜가 1월 1일이면 지정한 시간에 초기화
				{
					if (DateTime.Now.Month == 1 && DateTime.Now.Day == 1 && _LotCount2.tProductClearTime.Month == 12 && _LotCount2.tProductClearTime.Day == 31)
					{
						if (DateTime.Now.Hour > (_Config.nLotClearTime / 100))
						{
							_LotCount2.nOkCount = 0;
							_LotCount2.nNGCount = 0;
							_LotCount2.nLotCount = 0;
							_LotCount2.tProductClearTime = DateTime.Now;
							//SaveProductCount();
							SaveModelProductCount2(_LotCount2, _ModelInfo2.strModelName);
						}
						else if (DateTime.Now.Hour == (_Config.nLotClearTime / 100) &&
							DateTime.Now.Minute >= (_Config.nLotClearTime % 100))
						{
							_LotCount2.nOkCount = 0;
							_LotCount2.nNGCount = 0;
							_LotCount2.nLotCount = 0;
							_LotCount2.tProductClearTime = DateTime.Now;
							//SaveProductCount();
							SaveModelProductCount2(_LotCount2, _ModelInfo2.strModelName);
						}
					}
					else
					{
						_LotCount2.nOkCount = 0;
						_LotCount2.nNGCount = 0;
						_LotCount2.nLotCount = 0;
						_LotCount2.tProductClearTime = DateTime.Now;
						//SaveProductCount();
						SaveModelProductCount2(_LotCount2, _ModelInfo2.strModelName);
					}
				}
			}

		}
		//===========================================================

		#region 타워램프
		public static void TowerLampProcess()
		{
			int nStepIndex = (int)PROC_LIST.TOWER_LAMP;

			switch (nProcessStep[nStepIndex])
			{
				case 0:
					if (_SysInfo.TL_Red != TOWER_LAMP.TL_OFF) { SetDIOPort(DO.TOWER_LAMP_RED, true); }
					else { SetDIOPort(DO.TOWER_LAMP_RED, false); }

					if (_SysInfo.TL_Yellow != TOWER_LAMP.TL_OFF) { SetDIOPort(DO.TOWER_LAMP_YELLOW, true); }
					else { SetDIOPort(DO.TOWER_LAMP_YELLOW, false); }

					if (_SysInfo.TL_Green != TOWER_LAMP.TL_OFF) { SetDIOPort(DO.TOWER_LAMP_GREEN, true); }
					else { SetDIOPort(DO.TOWER_LAMP_GREEN, false); }

					if (!(_SysInfo.nTL_Beep > 0))
					{
						if (_SysInfo.TL_Buzzer != TOWER_LAMP.TL_OFF) { SetDIOPort(DO.TOWER_LAMP_BUZZER, true); }
						else { SetDIOPort(DO.TOWER_LAMP_BUZZER, false); }
					}

					tMainTimer[nStepIndex].Start(500);
					nProcessStep[nStepIndex]++;
					break;


				case 1:
					if (!tMainTimer[nStepIndex].Verify()) { break; }

					if (_SysInfo.TL_Red == TOWER_LAMP.TL_ON) { SetDIOPort(DO.TOWER_LAMP_RED, true); }
					else { SetDIOPort(DO.TOWER_LAMP_RED, false); }

					if (_SysInfo.TL_Yellow == TOWER_LAMP.TL_ON) { SetDIOPort(DO.TOWER_LAMP_YELLOW, true); }
					else { SetDIOPort(DO.TOWER_LAMP_YELLOW, false); }

					if (_SysInfo.TL_Green == TOWER_LAMP.TL_ON) { SetDIOPort(DO.TOWER_LAMP_GREEN, true); }
					else { SetDIOPort(DO.TOWER_LAMP_GREEN, false); }

					if (!(_SysInfo.nTL_Beep > 0))
					{
						if (_SysInfo.TL_Buzzer == TOWER_LAMP.TL_ON) { SetDIOPort(DO.TOWER_LAMP_BUZZER, true); }
						else { SetDIOPort(DO.TOWER_LAMP_BUZZER, false); }
					}

					tMainTimer[nStepIndex].Start(500);
					nProcessStep[nStepIndex]++;
					break;

				case 2:
					if (!tMainTimer[nStepIndex].Verify()) { break; }

					nProcessStep[nStepIndex] = 0;
					break;
			}
		}

		public static void TowerBuzzerProcess()
		{

			int nStepIndex = (int)PROC_LIST.TOWER_BUZZER;

			switch (nProcessStep[nStepIndex])
			{
				case 0:
					if (_SysInfo.nTL_Beep > 0)
					{
						SetDIOPort(DO.TOWER_LAMP_BUZZER, true);
						tMainTimer[nStepIndex].Start(60);
						nProcessStep[nStepIndex]++;
					}
					break;

				case 1:
					if (!tMainTimer[nStepIndex].Verify()) { break; }

					SetDIOPort(DO.TOWER_LAMP_BUZZER, false);
					tMainTimer[nStepIndex].Start(100);

					if (_SysInfo.nTL_Beep > 0) { _SysInfo.nTL_Beep--; }
					nProcessStep[nStepIndex]++;
					break;

				case 2:
					if (!tMainTimer[nStepIndex].Verify()) { break; }
					nProcessStep[nStepIndex] = 0;
					break;
			}
		}

		public static void TowerLampOn(LAMP_COLOR LAMP_Clr)
		{
			if (LAMP_Clr == LAMP_COLOR.TL_RED)
			{
				_SysInfo.TL_Red = TOWER_LAMP.TL_ON;
				_SysInfo.TL_Yellow = TOWER_LAMP.TL_OFF;
				_SysInfo.TL_Green = TOWER_LAMP.TL_OFF;
			}

			if (LAMP_Clr == LAMP_COLOR.TL_YELLOW)
			{
				_SysInfo.TL_Red = TOWER_LAMP.TL_OFF;
				_SysInfo.TL_Yellow = TOWER_LAMP.TL_ON;
				_SysInfo.TL_Green = TOWER_LAMP.TL_OFF;
			}

			if (LAMP_Clr == LAMP_COLOR.TL_GREEN)
			{
				_SysInfo.TL_Red = TOWER_LAMP.TL_OFF;
				_SysInfo.TL_Yellow = TOWER_LAMP.TL_OFF;
				_SysInfo.TL_Green = TOWER_LAMP.TL_ON;
			}
		}

		public static void TowerLampBlink(LAMP_COLOR LAMP_Clr)
		{
			if (LAMP_Clr == LAMP_COLOR.TL_RED)
			{
				_SysInfo.TL_Red = TOWER_LAMP.TL_BLINK;
				_SysInfo.TL_Yellow = TOWER_LAMP.TL_OFF;
				_SysInfo.TL_Green = TOWER_LAMP.TL_OFF;
			}

			if (LAMP_Clr == LAMP_COLOR.TL_YELLOW)
			{
				_SysInfo.TL_Red = TOWER_LAMP.TL_OFF;
				_SysInfo.TL_Yellow = TOWER_LAMP.TL_BLINK;
				_SysInfo.TL_Green = TOWER_LAMP.TL_OFF;
			}

			if (LAMP_Clr == LAMP_COLOR.TL_GREEN)
			{
				_SysInfo.TL_Red = TOWER_LAMP.TL_OFF;
				_SysInfo.TL_Yellow = TOWER_LAMP.TL_OFF;
				_SysInfo.TL_Green = TOWER_LAMP.TL_BLINK;
			}
		}
		#endregion



		public static void InitCommPort()
		{

			PowerSupply[0].SetPort(String.Format("COM{0}", _Config.nPowerSupply1Port), _Config.nPowerSupply1BaudRate, Parity.None, 8, StopBits.One, "P/S #1");
			if (PowerSupply[0].PortOpen())
			{
				AppendLogMsg(String.Format("<COM{0}> Power Supply #1 Port Open Success", _Config.nPowerSupply1Port), MSG_TYPE.INFO);
			}
			else
			{
				AppendLogMsg(String.Format("<COM{0}> Power Supply #1 Port Open Fail", _Config.nPowerSupply1Port), MSG_TYPE.ERROR);
			}

			PowerSupply[1].SetPort(String.Format("COM{0}", _Config.nPowerSupply2Port), _Config.nPowerSupply2BaudRate, Parity.None, 8, StopBits.One, "P/S #2");
			if (PowerSupply[1].PortOpen())
			{
				AppendLogMsg(String.Format("<COM{0}> Power Supply #2 Port Open Success", _Config.nPowerSupply2Port), MSG_TYPE.INFO);
			}
			else
			{
				AppendLogMsg(String.Format("<COM{0}> Power Supply #2 Port Open Fail", _Config.nPowerSupply2Port), MSG_TYPE.ERROR);
			}

			PowerSupply[2].SetPort(String.Format("COM{0}", _Config.nPowerSupply3Port), _Config.nPowerSupply3BaudRate, Parity.None, 8, StopBits.One, "P/S #1");
			if (PowerSupply[2].PortOpen())
			{
				AppendLogMsg(String.Format("<COM{0}> Power Supply #3 Port Open Success", _Config.nPowerSupply3Port), MSG_TYPE.INFO);
			}
			else
			{
				AppendLogMsg(String.Format("<COM{0}> Power Supply #3 Port Open Fail", _Config.nPowerSupply3Port), MSG_TYPE.ERROR);
			}

			PowerSupply[3].SetPort(String.Format("COM{0}", _Config.nPowerSupply4Port), _Config.nPowerSupply4BaudRate, Parity.None, 8, StopBits.One, "P/S #2");
			if (PowerSupply[3].PortOpen())
			{
				AppendLogMsg(String.Format("<COM{0}> Power Supply #4 Port Open Success", _Config.nPowerSupply4Port), MSG_TYPE.INFO);
			}
			else
			{
				AppendLogMsg(String.Format("<COM{0}> Power Supply #4 Port Open Fail", _Config.nPowerSupply4Port), MSG_TYPE.ERROR);
			}

			_BcdReader.SetPort(String.Format("COM{0}", _Config.nBCDScanner1Port), _Config.nBCDScanner1BaudRate, Parity.None, 8, StopBits.One, "바코드리더 #1");
			if (_BcdReader.PortOpen())
			{
				_BcdReader.nStation = 1;
				AppendLogMsg(String.Format("<COM{0}>  Hand BCR #1 Port Open Success", _Config.nBCDScanner1Port), MSG_TYPE.INFO);
			}
			else
			{
				AppendLogMsg(String.Format("<COM{0}> Hand BCR #1 Port Open Fail", _Config.nBCDScanner1Port), MSG_TYPE.ERROR);
			}

			_BcdReader2.SetPort(String.Format("COM{0}", _Config.nBCDScanner2Port), _Config.nBCDScanner2BaudRate, Parity.None, 8, StopBits.One, "바코드리더 #2");
			if (_BcdReader2.PortOpen())
			{
				_BcdReader2.nStation = 2;
				AppendLogMsg(String.Format("<COM{0}> Hand BCR #2 Port Open Success", _Config.nBCDScanner2Port), MSG_TYPE.INFO);
			}
			else
			{
				AppendLogMsg(String.Format("<COM{0}> Hand BCR #2 Port Open Fail", _Config.nBCDScanner2Port), MSG_TYPE.ERROR);
			}

			_BarcodePrint.SetPort(String.Format("COM{0}", _Config.nBcdPrinterPort), 9600, Parity.None, 8, StopBits.One);
			if (_BarcodePrint.PortOpen())
			{
				AppendLogMsg(String.Format("<COM{0}> Barcode Printer #1 Port Open Success", _Config.nBcdPrinterPort), MSG_TYPE.INFO);
				_BarcodePrint.Start();
			}
			else
			{
				AppendLogMsg(String.Format("<COM{0}> Barcode Printer #1 Port Open Fail", _Config.nBcdPrinterPort), MSG_TYPE.INFO);
			}


			_BarcodePrint2.SetPort(String.Format("COM{0}", _Config.nBcdPrinterPort2), 9600, Parity.None, 8, StopBits.One);
			if (_BarcodePrint2.PortOpen())
			{
				AppendLogMsg(String.Format("<COM{0}> Barcode Printer #2 Port Open Success", _Config.nBcdPrinterPort2), MSG_TYPE.INFO);
				_BarcodePrint2.Start();
			}
			else
			{
				AppendLogMsg(String.Format("<COM{0}> Barcode Printer #2 Port Open Fail", _Config.nBcdPrinterPort2), MSG_TYPE.INFO);
			}


			if (!_Config.bDmmEtcMode)
			{
				KeysiteDmm.SetPort(String.Format("COM{0}", _Config.nDMM1Port), _Config.nDMM1BaudRate, Parity.None, 8, StopBits.One, "DMM 검사기 #1");
				if (KeysiteDmm.PortOpen())
				{
					AppendLogMsg(String.Format("<COM{0}> DMM Tester #1 Port Open Success", _Config.nDMM1Port), MSG_TYPE.INFO);
				}
				else
				{
					AppendLogMsg(String.Format("<COM{0}> DMM Tester #1 Port Open Fail", _Config.nDMM1Port), MSG_TYPE.ERROR);
				}

				KeysiteDmm2.SetPort(String.Format("COM{0}", _Config.nDMM2Port), _Config.nDMM2BaudRate, Parity.None, 8, StopBits.One, "DMM 검사기 #2");
				if (KeysiteDmm2.PortOpen())
				{
					AppendLogMsg(String.Format("<COM{0}> DMM Tester #2 Port Open Success", _Config.nDMM2Port), MSG_TYPE.INFO);
				}
				else
				{
					AppendLogMsg(String.Format("<COM{0}> DMM Tester #2 Port Open Fail", _Config.nDMM2Port), MSG_TYPE.ERROR);
				}
			}
			else
			{
				try
				{
					_KeysiteDmmEtc.strIP = _Config.strDmmIP;
					_KeysiteDmmEtc.nPort = _Config.nDmmPort;
					_KeysiteDmmEtc.SetPort();

					_KeysiteDmmEtc2.strIP = _Config.strDmmIP2;
					_KeysiteDmmEtc2.nPort = _Config.nDmmPort2;
					_KeysiteDmmEtc2.SetPort();


				}
				catch (Exception _e) { AppendLogMsg(_e.Message, MSG_TYPE.INFO); }

			}



			for (int i = 0; i < 8; i++)
			{
				if (_CanComm[i].CanInit(i))
				{

					AppendLogMsg($"CAN CH #{i + 1} Initialization successful", MSG_TYPE.INFO);

				}
				else
				{
					AppendLogMsg($"CAN CH #{i + 1} Initialization failed", MSG_TYPE.ERROR);

				}
			}

			try
			{
				_CellSimulator1.strIP = _Config.strCellSimulator1IP;
				_CellSimulator1.nPort = _Config.nCellSimulator1Port;
				_CellSimulator1.SetPort();

				_CellSimulator2.strIP = _Config.strCellSimulator2IP;
				_CellSimulator2.nPort = _Config.nCellSimulator2Port;
				_CellSimulator2.SetPort();

				_CellSimulator3.strIP = _Config.strCellSimulator3IP;
				_CellSimulator3.nPort = _Config.nCellSimulator3Port;
				_CellSimulator3.SetPort();

				_CellSimulator4.strIP = _Config.strCellSimulator4IP;
				_CellSimulator4.nPort = _Config.nCellSimulator4Port;
				_CellSimulator4.SetPort();

				_Nutrunner.strIP = _Config.strToolIP;
				_Nutrunner.nPort = _Config.nToolPort;
				_Nutrunner.nStation = 1;
				_Nutrunner.SetPort();

				_Nutrunner2.strIP = _Config.strToolIP2;
				_Nutrunner2.nPort = _Config.nToolPort2;
				_Nutrunner2.nStation = 2;
				_Nutrunner2.SetPort();



				//_Cyclone.strIP = _Config.strCycloneMyIP;
				//_Cyclone.nPort = _Config.nCycloneMyPort;
				//_Cyclone.nMyPort = _Config.nCounterMyPort;
				//_Cyclone.SetPort();

				//_Cyclone2.strIP = _Config.strCycloneMyIP2;
				//_Cyclone2.nPort = _Config.nCycloneMyPort2;
				//_Cyclone2.nMyPort = _Config.nCounterMyPort2;
				//_Cyclone2.SetPort();
			}
			catch (Exception _e) { AppendLogMsg(_e.Message, MSG_TYPE.INFO); }



		}

		public static void CanLogDataWrite(myCanData myData)
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				string strSaveFolderPath = String.Format(@"DATA\\{0}\\{1}\\", theApp._ModelInfo.strModelName, DateTime.Now.ToString("yyMMdd"));
				DirectoryInfo Savedir = new DirectoryInfo(strSaveFolderPath);
				if (Savedir.Exists == false) { Savedir.Create(); }


				if (theApp.lstMyLog.Count > 500)
				{
					theApp.lstMyLog.RemoveAt(0);
				}

				if (myData.nLen > 0) { myData.strData1 = myData.btData1.ToString("X2"); } else { myData.strData1 = ""; }
				if (myData.nLen > 1) { myData.strData2 = myData.btData2.ToString("X2"); } else { myData.strData2 = ""; }
				if (myData.nLen > 2) { myData.strData3 = myData.btData3.ToString("X2"); } else { myData.strData3 = ""; }
				if (myData.nLen > 3) { myData.strData4 = myData.btData4.ToString("X2"); } else { myData.strData4 = ""; }
				if (myData.nLen > 4) { myData.strData5 = myData.btData5.ToString("X2"); } else { myData.strData5 = ""; }
				if (myData.nLen > 5) { myData.strData6 = myData.btData6.ToString("X2"); } else { myData.strData6 = ""; }
				if (myData.nLen > 6) { myData.strData7 = myData.btData7.ToString("X2"); } else { myData.strData7 = ""; }
				if (myData.nLen > 7) { myData.strData8 = myData.btData8.ToString("X2"); } else { myData.strData8 = ""; }

				theApp.lstMyLog.Add(myData);

				//if (_Config.bUseCanLogSave)
				//{
				//string strFolderPath = String.Format(@"CAN_LOG\\");
				//DirectoryInfo dir = new DirectoryInfo(strFolderPath);
				//if (dir.Exists == false) { dir.Create(); }

				// LOG 데이터 저장
				string strMessge = "";

				strMessge += myData._tTime.ToString("yyyy-MM-dd HH:mm:ss:fffff") + ",";
				strMessge += myData.nCh + ",";
				strMessge += myData.strType + ",";
				strMessge += myData.nID.ToString("X") + ",";
				strMessge += myData.strData1 + ",";
				strMessge += myData.strData2 + ",";
				strMessge += myData.strData3 + ",";
				strMessge += myData.strData4 + ",";
				strMessge += myData.strData5 + ",";
				strMessge += myData.strData6 + ",";
				strMessge += myData.strData7 + ",";
				strMessge += myData.strData8 + "\r\n";

				string strSaveFilePath = String.Format(@"{0}{1}_CAN.txt", strSaveFolderPath, _SysInfo.strSaveFileName.Replace(':', '_'));
				File.AppendAllText(strSaveFilePath, strMessge);

				//string savePath = String.Format("{0}{1}.txt", strFolderPath, "TEST");
				////string savePath = String.Format("{0}{1}.txt", strFolderPath, _SysInfo.strReadBarcode);
				//System.IO.File.AppendAllText(savePath, strMessge, Encoding.Default);
				//}
			});
		}

		public static void CanLogDataWrite2(myCanData2 myData)
		{
		
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				string strSaveFolderPath = String.Format(@"DATA2\\{0}\\{1}\\", theApp._ModelInfo2.strModelName, DateTime.Now.ToString("yyMMdd"));
				DirectoryInfo Savedir = new DirectoryInfo(strSaveFolderPath);
				if (Savedir.Exists == false) { Savedir.Create(); }

				if (theApp.lstMyLog2.Count > 500)
				{
					theApp.lstMyLog2.RemoveAt(0);
				}


				if (myData.nLen > 0) { myData.strData1 = myData.btData1.ToString("X2"); } else { myData.strData1 = ""; }
				if (myData.nLen > 1) { myData.strData2 = myData.btData2.ToString("X2"); } else { myData.strData2 = ""; }
				if (myData.nLen > 2) { myData.strData3 = myData.btData3.ToString("X2"); } else { myData.strData3 = ""; }
				if (myData.nLen > 3) { myData.strData4 = myData.btData4.ToString("X2"); } else { myData.strData4 = ""; }
				if (myData.nLen > 4) { myData.strData5 = myData.btData5.ToString("X2"); } else { myData.strData5 = ""; }
				if (myData.nLen > 5) { myData.strData6 = myData.btData6.ToString("X2"); } else { myData.strData6 = ""; }
				if (myData.nLen > 6) { myData.strData7 = myData.btData7.ToString("X2"); } else { myData.strData7 = ""; }
				if (myData.nLen > 7) { myData.strData8 = myData.btData8.ToString("X2"); } else { myData.strData8 = ""; }

				theApp.lstMyLog2.Add(myData);
				

				//if (_Config.bUseCanLogSave)
				//{
				//string strFolderPath = String.Format(@"CAN_LOG\\");
				//DirectoryInfo dir = new DirectoryInfo(strFolderPath);
				//if (dir.Exists == false) { dir.Create(); }

				// LOG 데이터 저장
				string strMessge = "";

				strMessge += myData._tTime.ToString("yyyy-MM-dd HH:mm:ss:fffff") + ",";
				strMessge += myData.nCh + ",";
				strMessge += myData.strType + ",";
				strMessge += myData.nID.ToString("X") + ",";
				strMessge += myData.strData1 + ",";
				strMessge += myData.strData2 + ",";
				strMessge += myData.strData3 + ",";
				strMessge += myData.strData4 + ",";
				strMessge += myData.strData5 + ",";
				strMessge += myData.strData6 + ",";
				strMessge += myData.strData7 + ",";
				strMessge += myData.strData8 + "\r\n";

				string strSaveFilePath = String.Format(@"{0}{1}_CAN.txt", strSaveFolderPath, _SysInfo2.strSaveFileName.Replace(':', '_'));
				File.AppendAllText(strSaveFilePath, strMessge);

			});
			
		}


		public static void TestTotalResultView(string strBCD, string strResult)
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				if (_TestResultList.Count > 100)
				{
					_TestResultList.RemoveAt(0);
				}

				TestTotalResult logMessage = new TestTotalResult();

				logMessage.dtAppendTime = DateTime.Now;
				logMessage.strBCD = strBCD;
				logMessage.strTotalResult = strResult;

				_TestResultList.Add(logMessage);


			});
		}

		public static void TestTotalResultView2(string strBCD, string strResult)
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				if (_TestResultList2.Count > 100)
				{
					_TestResultList2.RemoveAt(0);
				}

				TestTotalResult logMessage = new TestTotalResult();

				logMessage.dtAppendTime = DateTime.Now;
				logMessage.strBCD = strBCD;
				logMessage.strTotalResult = strResult;

				_TestResultList2.Add(logMessage);


			});
		}





		// AX IO =================================================
		public static void SetDIOPort(DO nDO, bool bEnable)
		{
			try
			{

				if (bEnable)
					CAXD.AxdoWriteOutport((int)nDO, 1);
				else
					CAXD.AxdoWriteOutport((int)nDO, 0);
			}
			catch { }
		}


		public static bool GetDIOPort(DI nDI)
		{
			uint bResult = 0;

			try
			{
				CAXD.AxdiReadInport((int)nDI, ref bResult);
			}
			catch { }

			return (bResult != 0) ? true : false;
		}


		public static bool GetDIOPortStat(DO nDO)
		{
			uint bResult = 0;

			try
			{
				CAXD.AxdoReadOutport((int)nDO, ref bResult);
			}
			catch { }
			return (bResult != 0) ? true : false;
		}

		public static void TestUIClearSet()
		{
			string strCateName = "";
			_TestData.Clear();
			for (int i = 0; i < _ModelInfo._TestInfo.Count; i++)
			{
				for (int j = 0; j < _Collection.PsetNameList.Count; j++)
				{
					if (_ModelInfo._TestInfo[i].nTestItem == _Collection.PsetNameList[j].nSchID)
					{
						strCateName = _Collection.PsetNameList[j].strSchName;
					}
				}
				_TestData.Add(new myTestData()
				{
					strTestName = _ModelInfo._TestInfo[i].strTestName,
					SpecMin = _ModelInfo._TestInfo[i].strSpecMin,
					SpecMax = _ModelInfo._TestInfo[i].strSpecMax,
					Unit = _ModelInfo._TestInfo[i].strUnit,
					Cate = strCateName
				});
			}
		}

		public static void TestUIClearSet2()
		{
			//try
			//{
			string strCateName = "";
			_TestData2.Clear();
			for (int i = 0; i < _ModelInfo2._TestInfo.Count; i++)
			{
				for (int j = 0; j < _Collection.PsetNameList.Count; j++)
				{
					if (_ModelInfo2._TestInfo[i].nTestItem == _Collection.PsetNameList[j].nSchID)
					{
						strCateName = _Collection.PsetNameList[j].strSchName;
					}
				}
				_TestData2.Add(new myTestData2()
				{
					strTestName = _ModelInfo2._TestInfo[i].strTestName,
					SpecMin = _ModelInfo2._TestInfo[i].strSpecMin,
					SpecMax = _ModelInfo2._TestInfo[i].strSpecMax,
					Unit = _ModelInfo2._TestInfo[i].strUnit,
					Cate = strCateName
				});
			}
			//}
			//catch { }

		}
		
		//public static void TestCyclone()
		//{
		//	string cycloneIP = "192.168.1.100"; // Cyclone FX IP
		//	string imageFilePath = @"C:\Firmware\MyFirmware.sap"; // 펌웨어 이미지 경로

		//	try
		//	{
		//		CycloneControlClass cyclone = new CycloneControlClass();

		//		// Cyclone 연결
		//		int result = cyclone.ConnectToCyclone(cycloneIP);
		//		if (result != 0)
		//		{
		//			MessageBox.Show($"연결 실패 (코드: {result})");
		//			return;
		//		}

		//		// 이미지 로드
		//		result = cyclone.LoadImage(imageFilePath);
		//		if (result != 0)
		//		{
		//			MessageBox.Show($"이미지 로드 실패 (코드: {result})");
		//			cyclone.Disconnect();
		//			return;
		//		}

		//		// 프로그래밍 수행
		//		result = cyclone.ProgramImage();
		//		if (result != 0)
		//		{
		//			MessageBox.Show($"프로그래밍 실패 (코드: {result})");
		//		}
		//		else
		//		{
		//			MessageBox.Show("펌웨어 업데이트 성공!");
		//		}

		//		cyclone.Disconnect();
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show($"예외 발생: {ex.Message}");
		//	}

		//}

		// 테스트 데이터 클리어
		static void TestResultSet(int nIndex, string strData, string strResult)
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).TestLogSet(nIndex, strData, strResult);
			});
		}

		static void NgDataSet(int nIndex, string nAddr, string strSourceData, string strReadData, string strResult)
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				_SysInfo._listNgInfo[nIndex].Add(new NgInfoList() { strAddr = nAddr, strSource = strSourceData, strRead = strReadData, strTestResult = strResult });
			});
		}

		static void NgDataClear()
		{


			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				for (int i = 0; i < _SysInfo._listNgInfo.Length; i++)
				{
					_SysInfo._listNgInfo[i].Clear();
				}
			});
		}

		static void TestResultSet2(int nIndex, string strData, string strResult)
		{
			//App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE         // Log Clear
			//{
			//	_window2.TestLogSet2(nIndex, strData, strResult);
			//});

			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				((MainWindow)App.Current.MainWindow).ChangeUI(nIndex);
			});

			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				//	//MainWindow2 _window2 = new MainWindow2();
				//	//_window2.TestLogSet2(nIndex, strData, strResult);
				myTestData2 _td = new myTestData2();

				_td.strTestName = theApp._TestData2[nIndex].strTestName;
				_td.SpecMin = theApp._TestData2[nIndex].SpecMin;
				_td.SpecMax = theApp._TestData2[nIndex].SpecMax;
				_td.Unit = theApp._TestData2[nIndex].Unit;
				_td.strType = theApp._TestData2[nIndex].strType;
				_td.Cate = theApp._TestData2[nIndex].Cate;

				_td.strResult = strResult;
				_td.Data = strData;

				theApp._TestData2[nIndex] = _td;

			});
		}

		static void NgDataSet2(int nIndex, string nAddr, string strSourceData, string strReadData, string strResult)
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				_SysInfo2._listNgInfo[nIndex].Add(new NgInfoList() { strAddr = nAddr, strSource = strSourceData, strRead = strReadData, strTestResult = strResult });
			});
		}

		static void NgDataClear2()
		{
		
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				for (int i = 0; i < _SysInfo2._listNgInfo.Length; i++)
				{
					_SysInfo2._listNgInfo[i].Clear();
				}
			});
		}
		public static void AppendLogMsg(string strMessage, MSG_TYPE type)
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				if (MainLogMessage.Count > 100)
				{
					MainLogMessage.RemoveAt(0);
				}

				LogMessage logMessage = new LogMessage();

				logMessage.dtAppendTime = DateTime.Now;
				logMessage.strComment = strMessage;
				logMessage.strType = type.ToString();

				MainLogMessage.Add(logMessage);

				string strFolderPath = String.Format(@"LOG\\");
				DirectoryInfo dir = new DirectoryInfo(strFolderPath);
				if (dir.Exists == false) { dir.Create(); }

				// LOG 데이터 저장
				string strMessge = "";
				strMessge += logMessage.dtAppendTime.ToString("yyyy-MM-dd HH:mm:ss") + "\t";
				strMessge += logMessage.strType + "\t";
				strMessge += logMessage.strComment + "\r\n";

				string savePath = String.Format("{0}{1}.txt", strFolderPath, DateTime.Now.ToString("yyyy-MM-dd"));
				System.IO.File.AppendAllText(savePath, strMessge, Encoding.Default);
			});
		}

		public static void AppendLogMsg2(string strMessage, MSG_TYPE type)
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				if (MainLogMessage2.Count > 100)
				{
					MainLogMessage2.RemoveAt(0);
				}

				LogMessage logMessage = new LogMessage();

				logMessage.dtAppendTime = DateTime.Now;
				logMessage.strComment = strMessage;
				logMessage.strType = type.ToString();

				MainLogMessage2.Add(logMessage);

				string strFolderPath = String.Format(@"LOG2\\");
				DirectoryInfo dir = new DirectoryInfo(strFolderPath);
				if (dir.Exists == false) { dir.Create(); }

				// LOG 데이터 저장
				string strMessge = "";
				strMessge += logMessage.dtAppendTime.ToString("yyyy-MM-dd HH:mm:ss") + "\t";
				strMessge += logMessage.strType + "\t";
				strMessge += logMessage.strComment + "\r\n";

				string savePath = String.Format("{0}{1}.txt", strFolderPath, DateTime.Now.ToString("yyyy-MM-dd"));
				System.IO.File.AppendAllText(savePath, strMessge, Encoding.Default);
			});
		}




		public static void AppendDebugMsg(string strMessage, string unit)
		{
			App.Current.Dispatcher.InvokeAsync((Action)delegate // <--- HERE         // Log Clear
			{
				if (DebugMessage.Count > 100)
				{
					DebugMessage.RemoveAt(0);
				}

				LogMessage logMessage = new LogMessage();

				logMessage.dtAppendTime = DateTime.Now;
				logMessage.strComment = strMessage;
				logMessage.strType = unit;

				DebugMessage.Add(logMessage);


				string strFolderPath = String.Format(@"DEBUG_LOG\\");
				DirectoryInfo dir = new DirectoryInfo(strFolderPath);
				if (dir.Exists == false) { dir.Create(); }

				// LOG 데이터 저장
				string strMessge = "";
				strMessge += logMessage.dtAppendTime.ToString("yyyy-MM-dd HH:mm:ss") + "\t";
				strMessge += logMessage.strType + "\t";
				strMessge += logMessage.strComment + "\r\n";

				string savePath = String.Format("{0}{1}.txt", strFolderPath, DateTime.Now.ToString("yyyy-MM-dd"));
				System.IO.File.AppendAllText(savePath, strMessge, Encoding.Default);
			});
		}

		static void SaveBarcodeInfo()
		{
			string strOutputBCD = "";


			strOutputBCD = theApp._BarcodePrint.strModelInfo + theApp._BarcodePrint.strPrintBCD;
			string strDaySave = strOutputBCD + "," + theApp._ModelInfo.strComment2 + "," + DateTime.Now.ToString("yyyy'.'MM'.'dd") + " " + DateTime.Now.ToString("HH':'mm':'ss") + ",";
			string strPrintBcd = strOutputBCD + "\t" + theApp._ModelInfo.strComment2 + "\t" + DateTime.Now.ToString("yyyy'.'MM'.'dd") + " " + DateTime.Now.ToString("HH':'mm':'ss") + "\r\n";


			//// CMA
			//DataRow[] _cmaRow = _dtBCDInfo.Select("[Type] = 'CMA'");
			//for (int i = 0; i < _cmaRow.Count(); i++) { strPrintBcd += String.Format("{0}\tCMA\r\n", _cmaRow[i][0].ToString()); }

			//// BMS
			//DataRow[] _bmsRow = _dtBCDInfo.Select("[Type] = 'BMS'");
			//for (int i = 0; i < _bmsRow.Count(); i++) { strPrintBcd += String.Format("{0}\tBMS\r\n", _bmsRow[i][0].ToString()); }

			//// FUSE
			//DataRow[] _FuseRow = _dtBCDInfo.Select("[Type] = 'FUSE'");
			//for (int i = 0; i < _FuseRow.Count(); i++) { strPrintBcd += String.Format("{0}\tFUSE\r\n", _FuseRow[i][0].ToString()); }


			//for (int i = 0; i < 10; i++)
			//{
			//	strDaySave += _MC_Status.str_ST1_CMABarcode[i] + ",";
			//	strPrintBcd += _MC_Status.str_ST1_CMABarcode[i] + "\t" + "CMA" + "\r\n";
			//}



			strPrintBcd += _SysInfo.strPBMSBcd + "\t" + "PBMS" + "\r\n";
			strPrintBcd += _SysInfo.strFuseBcd + "\t" + "Fuse" + "\r\n";
			strPrintBcd += _SysInfo.strCaseBcd + "\t" + "Case" + "\r\n";
			



			try
			{
				//===============우리쪽 임시데이터 저장 =========
				// 바코드 파일
				string strSaveFolderPath = String.Format(@"DATA\\BCD\\{0}\\{1}\\", theApp._ModelInfo.strModelName, theApp._LotCount.tProductClearTime.ToString("yyMMdd"));
				DirectoryInfo Savedir = new DirectoryInfo(strSaveFolderPath);
				if (Savedir.Exists == false) { Savedir.Create(); }

				string strSaveFilePath = String.Format(@"{0}{1}.txt", strSaveFolderPath, _SysInfo.strSaveFileName.Replace(':', '_'));
				File.WriteAllText(strSaveFilePath, strPrintBcd);


				//=============== 마스터 데이터 저장 =========
				// 바코드 파일
				strSaveFolderPath = String.Format(@"MASTER\\BCD{0}\\", theApp._ModelInfo.strModelName);
				Savedir = new DirectoryInfo(strSaveFolderPath);
				if (Savedir.Exists == false) { Savedir.Create(); }

				strSaveFilePath = String.Format(@"{0}{1}{2}.txt", strSaveFolderPath, theApp._LotCount.tProductClearTime.ToString("yyMMdd"), _SysInfo.strSaveFileName.Replace(':', '_'));
				File.WriteAllText(strSaveFilePath, strPrintBcd);


				string strMESSaveFolderPath = String.Format("{0}\\", _Config.strBCDMesDir);
				DirectoryInfo SaveMESdir = new DirectoryInfo(strMESSaveFolderPath);
				if (SaveMESdir.Exists == false) { SaveMESdir.Create(); }

				strSaveFilePath = String.Format(@"{0}{1}.txt", strMESSaveFolderPath, _SysInfo.strSaveFileName.Replace(':', '_'));
				File.WriteAllText(strSaveFilePath, strPrintBcd);

				//========================== ERP 데이터 저장
				//strSaveFolderPath = String.Format(@"{0}\\", _Config.strMESDir);
				//Savedir = new DirectoryInfo(strSaveFolderPath);
				//if (Savedir.Exists == false) { Savedir.Create(); }

				//strSaveFilePath = String.Format(@"{0}{1}.txt", strSaveFolderPath, strOutputBCD);
				//File.WriteAllText(strSaveFilePath, strPrintBcd);


				//========================== BackUp 데이터 저장
				strSaveFolderPath = String.Format(@"BACKUP\\BCD\\{0}\\{1}\\", theApp._ModelInfo.strModelName, theApp._LotCount.tProductClearTime.ToString("yyMMdd"));
				Savedir = new DirectoryInfo(strSaveFolderPath);
				if (Savedir.Exists == false) { Savedir.Create(); }

				strSaveFilePath = String.Format(@"{0}{1}.txt", strSaveFolderPath, _SysInfo.strSaveFileName.Replace(':', '_'));
				File.WriteAllText(strSaveFilePath, strPrintBcd);

				//================= 일일 데이터 저장 ==================
				// CSV 파일로 열거형으로 저장


			}
			catch { }



		}

		static void SaveBarcodeInfo2()
		{
			string strOutputBCD = "";


			strOutputBCD = theApp._BarcodePrint2.strModelInfo + theApp._BarcodePrint2.strPrintBCD;
			string strDaySave = strOutputBCD + "," + theApp._ModelInfo2.strComment2 + "," + DateTime.Now.ToString("yyyy'.'MM'.'dd") + " " + DateTime.Now.ToString("HH':'mm':'ss") + ",";
			string strPrintBcd = strOutputBCD + "\t" + theApp._ModelInfo2.strComment2 + "\t" + DateTime.Now.ToString("yyyy'.'MM'.'dd") + " " + DateTime.Now.ToString("HH':'mm':'ss") + "\r\n";


			//// CMA
			//DataRow[] _cmaRow = _dtBCDInfo.Select("[Type] = 'CMA'");
			//for (int i = 0; i < _cmaRow.Count(); i++) { strPrintBcd += String.Format("{0}\tCMA\r\n", _cmaRow[i][0].ToString()); }

			//// BMS
			//DataRow[] _bmsRow = _dtBCDInfo.Select("[Type] = 'BMS'");
			//for (int i = 0; i < _bmsRow.Count(); i++) { strPrintBcd += String.Format("{0}\tBMS\r\n", _bmsRow[i][0].ToString()); }

			//// FUSE
			//DataRow[] _FuseRow = _dtBCDInfo.Select("[Type] = 'FUSE'");
			//for (int i = 0; i < _FuseRow.Count(); i++) { strPrintBcd += String.Format("{0}\tFUSE\r\n", _FuseRow[i][0].ToString()); }


			//for (int i = 0; i < 10; i++)
			//{
			//	strDaySave += _MC_Status.str_ST1_CMABarcode[i] + ",";
			//	strPrintBcd += _MC_Status.str_ST1_CMABarcode[i] + "\t" + "CMA" + "\r\n";
			//}

			strPrintBcd += _SysInfo2.strPBMSBcd + "\t" + "PBMS" + "\r\n";
			strPrintBcd += _SysInfo2.strFuseBcd + "\t" + "Fuse" + "\r\n";
			strPrintBcd += _SysInfo2.strCaseBcd + "\t" + "Case" + "\r\n";

			try
			{
				//===============우리쪽 임시데이터 저장 =========
				// 바코드 파일
				string strSaveFolderPath = String.Format(@"DATA2\\BCD\\{0}\\{1}\\", theApp._ModelInfo2.strModelName, theApp._LotCount2.tProductClearTime.ToString("yyMMdd"));
				DirectoryInfo Savedir = new DirectoryInfo(strSaveFolderPath);
				if (Savedir.Exists == false) { Savedir.Create(); }

				string strSaveFilePath = String.Format(@"{0}{1}.txt", strSaveFolderPath, _SysInfo2.strSaveFileName.Replace(':', '_'));
				File.WriteAllText(strSaveFilePath, strPrintBcd);


				//=============== 마스터 데이터 저장 =========
				// 바코드 파일
				strSaveFolderPath = String.Format(@"MASTER2\\BCD{0}\\", theApp._ModelInfo2.strModelName);
				Savedir = new DirectoryInfo(strSaveFolderPath);
				if (Savedir.Exists == false) { Savedir.Create(); }

				strSaveFilePath = String.Format(@"{0}{1}{2}.txt", strSaveFolderPath, theApp._LotCount2.tProductClearTime.ToString("yyMMdd"), _SysInfo2.strSaveFileName.Replace(':', '_'));
				File.WriteAllText(strSaveFilePath, strPrintBcd);;

				string strMESSaveFolderPath = String.Format("{0}\\", _Config.strBCDMesDir2);
				DirectoryInfo SaveMESdir = new DirectoryInfo(strMESSaveFolderPath);
				if (SaveMESdir.Exists == false) { SaveMESdir.Create(); }

				strSaveFilePath = String.Format(@"{0}{1}.txt", strMESSaveFolderPath, _SysInfo2.strSaveFileName.Replace(':', '_'));
				File.WriteAllText(strSaveFilePath, strPrintBcd);

				//========================== ERP 데이터 저장
				//strSaveFolderPath = String.Format(@"{0}\\", _Config.strMESDir);
				//Savedir = new DirectoryInfo(strSaveFolderPath);
				//if (Savedir.Exists == false) { Savedir.Create(); }

				//strSaveFilePath = String.Format(@"{0}{1}.txt", strSaveFolderPath, strOutputBCD);
				//File.WriteAllText(strSaveFilePath, strPrintBcd);


				//========================== BackUp 데이터 저장
				strSaveFolderPath = String.Format(@"BACKUP2\\BCD\\{0}\\{1}\\", theApp._ModelInfo2.strModelName, theApp._LotCount2.tProductClearTime.ToString("yyMMdd"));
				Savedir = new DirectoryInfo(strSaveFolderPath);
				if (Savedir.Exists == false) { Savedir.Create(); }

				strSaveFilePath = String.Format(@"{0}{1}.txt", strSaveFolderPath, _SysInfo2.strSaveFileName.Replace(':', '_'));
				File.WriteAllText(strSaveFilePath, strPrintBcd);

				//================= 일일 데이터 저장 ==================
				// CSV 파일로 열거형으로 저장


			}
			catch (Exception e)
			{
				AppendLogMsg2($"{e}",MSG_TYPE.ERROR);
			}


		}



		//====== 모델 정보 저장
		public static void SaveModelInfo(MODEL_INFO _ModelInfo, String _ModelName)
		{

			try
			{
				string strFolderPath = String.Format(@"MODEL\\");
				DirectoryInfo dir = new DirectoryInfo(strFolderPath);
				if (dir.Exists == false) { dir.Create(); }

				File.WriteAllText(String.Format("MODEL\\{0}.rcp", _ModelName), JsonConvert.SerializeObject(_ModelInfo));

			}
			catch (Exception _e) { AppendDebugMsg(_e.Message, "System"); }
			SaveWorkPageInI(_ModelInfo.strModelName);
			TestUIClearSet();

		}

		//====== 모델 정보 불러오기
		public static void LoadModelInfo(ref MODEL_INFO _ModelInfo, String _ModelName)
		{

			string strFolderPath = String.Format(@"MODEL\\");
			DirectoryInfo dir = new DirectoryInfo(strFolderPath);
			if (dir.Exists == false) { dir.Create(); }

			try
			{
				_ModelInfo = JsonConvert.DeserializeObject<MODEL_INFO>(File.ReadAllText(String.Format(@"MODEL\\{0}.rcp", _ModelName)));

			}

			catch (Exception _e) { _ModelInfo.strModelName = "_Noname_"; AppendDebugMsg(_e.Message, "System"); }
			TestUIClearSet();
			SaveWorkPageInI(_ModelInfo.strModelName);
		}

		public static void SaveWorkPageInI(string strModelName)
		{
			string strPath = @"D:\\_work_guide_data_\\Config.ini";


			_IniFile.IniWriteValue("CONFIG", "szMasterCurrentModel", strModelName, strPath);
		}

		public static void SaveModelInfo2(MODEL_INFO2 _ModelInfo, String _ModelName)
		{

			try
			{
				string strFolderPath = String.Format(@"MODEL2\\");
				DirectoryInfo dir = new DirectoryInfo(strFolderPath);
				if (dir.Exists == false) { dir.Create(); }

				File.WriteAllText(String.Format("MODEL2\\{0}.rcp", _ModelName), JsonConvert.SerializeObject(_ModelInfo));

			}
			catch (Exception _e) { AppendDebugMsg(_e.Message, "System"); }
			SaveWorkPageInI2(_ModelInfo.strModelName);
			TestUIClearSet2();

		}

		//====== 모델 정보 불러오기
		public static void LoadModelInfo2(ref MODEL_INFO2 _ModelInfo, String _ModelName)
		{

			string strFolderPath = String.Format(@"MODEL2\\");
			DirectoryInfo dir = new DirectoryInfo(strFolderPath);
			if (dir.Exists == false) { dir.Create(); }

			try
			{
				_ModelInfo = JsonConvert.DeserializeObject<MODEL_INFO2>(File.ReadAllText(String.Format(@"MODEL2\\{0}.rcp", _ModelName)));

			}

			catch (Exception _e) { _ModelInfo.strModelName = "_Noname_"; AppendDebugMsg(_e.Message, "System"); }
			TestUIClearSet2();
			SaveWorkPageInI2(_ModelInfo.strModelName);
		}

		public static void SaveWorkPageInI2(string strModelName)
		{
			string strPath = @"D:\\_work_guide_data_2\\Config.ini";


			_IniFile.IniWriteValue("CONFIG", "szMasterCurrentModel", strModelName, strPath);
		}




		//====== 모델 정보 저장
		public static void SaveMasterTestInfo(MASTER_INFO_CHECK _MasterInfo, String _ModelName)
		{

			try
			{
				string strFolderPath = String.Format(@"MASTER_CHECK\\");
				DirectoryInfo dir = new DirectoryInfo(strFolderPath);
				if (dir.Exists == false) { dir.Create(); }

				File.WriteAllText(String.Format("MASTER_CHECK\\{0}.info", _ModelName), JsonConvert.SerializeObject(_MasterInfo));

			}
			catch (Exception _e) { AppendDebugMsg(_e.Message, "System"); }

		}

		//====== 모델 정보 불러오기
		public static void LoadMasterTestInfo(ref MASTER_INFO_CHECK _MasterInfo, String _ModelName)
		{

			string strFolderPath = String.Format(@"MASTER_CHECK\\");
			DirectoryInfo dir = new DirectoryInfo(strFolderPath);
			if (dir.Exists == false) { dir.Create(); }

			try
			{
				_MasterInfo = JsonConvert.DeserializeObject<MASTER_INFO_CHECK>(File.ReadAllText(String.Format(@"MASTER_CHECK\\{0}.info", _ModelName)));
			}
			catch (Exception _e) { }
		}





		// 모델별 카운트 저장
		public static void SaveModelProductCount(LotCount _Count, string strModelName)
		{

			try
			{
				DirectoryInfo dir = new DirectoryInfo(@"Count\\");
				if (dir.Exists == false) { dir.Create(); }

				File.WriteAllText(String.Format("Count\\{0}.cnt", strModelName), JsonConvert.SerializeObject(_Count));

			}
			catch { }

		}

		// 카운터 로드
		public static void LoadModelProductCount(ref LotCount _Count, string strModelName)
		{
			DirectoryInfo dir = new DirectoryInfo(@"Count\\");
			if (dir.Exists == false) { dir.Create(); }

			try
			{
				_Count = JsonConvert.DeserializeObject<LotCount>(File.ReadAllText(String.Format("Count\\{0}.cnt", strModelName)));

			}
			catch (Exception _e) { AppendDebugMsg(_e.Message, "System"); }
		}


		public static void SaveProductCount()
		{
			try
			{
				File.WriteAllText(String.Format("Count.cnt"), JsonConvert.SerializeObject(_LotCount));

			}
			catch (Exception _e) { AppendDebugMsg(_e.Message, "System"); }

		}

		// 카운터 로드
		public static void LoadProductCount()
		{
			try
			{
				_LotCount = JsonConvert.DeserializeObject<LotCount>(File.ReadAllText(String.Format(@"Count.cnt")));

			}
			catch (Exception _e) { AppendDebugMsg(_e.Message, "System"); }
		}

		public static void SaveModelProductCount2(LotCount _Count, string strModelName)
		{

			try
			{
				DirectoryInfo dir = new DirectoryInfo(@"Count2\\");
				if (dir.Exists == false) { dir.Create(); }

				File.WriteAllText(String.Format("Count2\\{0}.cnt", strModelName), JsonConvert.SerializeObject(_Count));

			}
			catch { }

		}

		// 카운터 로드
		public static void LoadModelProductCount2(ref LotCount _Count, string strModelName)
		{
			DirectoryInfo dir = new DirectoryInfo(@"Count2\\");
			if (dir.Exists == false) { dir.Create(); }

			try
			{
				_Count = JsonConvert.DeserializeObject<LotCount>(File.ReadAllText(String.Format("Count2\\{0}.cnt", strModelName)));

			}
			catch (Exception _e) { AppendDebugMsg(_e.Message, "System"); }
		}


		public static void SaveProductCount2()
		{
			try
			{
				File.WriteAllText(String.Format("Count2.cnt"), JsonConvert.SerializeObject(_LotCount2));

			}
			catch (Exception _e) { AppendDebugMsg(_e.Message, "System"); }

		}

		// 카운터 로드
		public static void LoadProductCount2()
		{
			try
			{
				_LotCount2 = JsonConvert.DeserializeObject<LotCount>(File.ReadAllText(String.Format(@"Count2.cnt")));

			}
			catch (Exception _e) { AppendDebugMsg(_e.Message, "System"); }
		}

		





		//====== INI 저장
		public static void SaveIniFile()
		{
			string strPath = "./Config.ini";


			// 통신포트 데이터
			_IniFile.IniWriteValue("COMM", "nPowerSupply1Port", _Config.nPowerSupply1Port.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nPowerSupply1BaudRate", _Config.nPowerSupply1BaudRate.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nPowerSupply2Port", _Config.nPowerSupply2Port.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nPowerSupply2BaudRate", _Config.nPowerSupply2BaudRate.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nBCDScanner1Port", _Config.nBCDScanner1Port.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nPowerSupply3Port", _Config.nPowerSupply3Port.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nPowerSupply3BaudRate", _Config.nPowerSupply3BaudRate.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nPowerSupply4Port", _Config.nPowerSupply4Port.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nPowerSupply4BaudRate", _Config.nPowerSupply4BaudRate.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nBCDScanner1Port", _Config.nBCDScanner1Port.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nBCDScanner1BaudRate", _Config.nBCDScanner1BaudRate.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nDMM1Port", _Config.nDMM1Port.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nDMM1BaudRate", _Config.nDMM1BaudRate.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nBCDScanner2Port", _Config.nBCDScanner2Port.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nBCDScanner2BaudRate", _Config.nBCDScanner2BaudRate.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nDMM2Port", _Config.nDMM2Port.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nDMM2BaudRate", _Config.nDMM2BaudRate.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "strCellSimulator1IP", _Config.strCellSimulator1IP.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nCellSimulator1Port", _Config.nCellSimulator1Port.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "strCellSimulator2IP", _Config.strCellSimulator2IP.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nCellSimulator2Port", _Config.nCellSimulator2Port.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "strCellSimulator3IP", _Config.strCellSimulator3IP.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nCellSimulator3Port", _Config.nCellSimulator3Port.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "strCellSimulator4IP", _Config.strCellSimulator4IP.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nCellSimulator4Port", _Config.nCellSimulator4Port.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "strDmmIP", _Config.strDmmIP.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nDmmPort", _Config.nDmmPort.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "strDmmIP2", _Config.strDmmIP2.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nDmmPort2", _Config.nDmmPort2.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "strCycloneMyIP", _Config.strCycloneMyIP.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nCycloneMyPort", _Config.nCycloneMyPort.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "strCycloneMyIP2", _Config.strCycloneMyIP2.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nCycloneMyPort2", _Config.nCycloneMyPort2.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "strCounterMyIP", _Config.strCounterMyIP.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nCounterMyPort", _Config.nCounterMyPort.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "strCounterMyIP2", _Config.strCounterMyIP2.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nCounterMyPort2", _Config.nCounterMyPort2.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nBcdPrinterPort", _Config.nBcdPrinterPort.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nBcdPrinterPort2", _Config.nBcdPrinterPort2.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "strRBMSIP", _Config.strRBMSIP.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nRBMSPort", _Config.nRBMSPort.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "strRBMSIP2", _Config.strRBMSIP2.ToString(), strPath);
			_IniFile.IniWriteValue("COMM", "nRBMSPort2", _Config.nRBMSPort2.ToString(), strPath);


			// 마지막 작업모델
			_Config.strCurrentModel = _ModelInfo.strModelName;
			_Config.strCurrentModel2 = _ModelInfo2.strModelName;

			_IniFile.IniWriteValue("CURRENT", "strCurrentModel", _Config.strCurrentModel, strPath);
			_IniFile.IniWriteValue("CURRENT", "strCurrentModel2", _Config.strCurrentModel2, strPath);


			_IniFile.IniWriteValue("COMMON", "strAdminPass", _Config.strAdminPass, strPath);
			_IniFile.IniWriteValue("COMMON", "bUseAdminPass", _Config.bUseAdminPass.ToString(), strPath);
			_IniFile.IniWriteValue("COMMON", "strMasterPass", _Config.strMasterPass.ToString(), strPath);

			_IniFile.IniWriteValue("COMMON", "bUseMasterCheck", _Config.bUseMasterCheck.ToString(), strPath);
			_IniFile.IniWriteValue("COMMON", "strMasterCheckDateTime", _Config.strMasterCheckDateTime.ToString(), strPath);
			_IniFile.IniWriteValue("COMMON", "nEolPinCount", _Config.nEolPinCount.ToString(), strPath);
			_IniFile.IniWriteValue("COMMON", "nEolPinCount2", _Config.nEolPinCount2.ToString(), strPath);




			_IniFile.IniWriteValue("TOOL", $"strToolIP", _Config.strToolIP.ToString(), strPath);
			_IniFile.IniWriteValue("TOOL", $"nToolPort", _Config.nToolPort.ToString(), strPath);
			_IniFile.IniWriteValue("TOOL", $"strToolIP2", _Config.strToolIP2.ToString(), strPath);
			_IniFile.IniWriteValue("TOOL", $"nToolPort2", _Config.nToolPort2.ToString(), strPath);
			

			_IniFile.IniWriteValue("ETC", "nLotClearTime", _Config.nLotClearTime.ToString(), strPath);
			_IniFile.IniWriteValue("ETC", "bUseLotAutoClear", _Config.bUseLotAutoClear.ToString(), strPath);
			_IniFile.IniWriteValue("ETC", "bDmmEtcMode", _Config.bDmmEtcMode.ToString(), strPath);
			_IniFile.IniWriteValue("ETC", "strLanguage", _Config.strLanguage.ToString(), strPath);

			_IniFile.IniWriteValue("DIR", "strSaveMesDir", _Config.strSaveMesDir.ToString(), strPath);
			_IniFile.IniWriteValue("DIR", "strSaveMesDir2", _Config.strSaveMesDir2.ToString(), strPath);
			_IniFile.IniWriteValue("DIR", "strBCDMesDir", _Config.strBCDMesDir.ToString(), strPath);
			_IniFile.IniWriteValue("DIR", "strBCDMesDir2", _Config.strBCDMesDir2.ToString(), strPath);

		}

		//====== INI 불러오기
		public static void LoadIniFile()
		{
			string strPath = "./Config.ini";

			int.TryParse(_IniFile.IniReadValue("COMM", "nPowerSupply1Port", "3", strPath), out _Config.nPowerSupply1Port);
			int.TryParse(_IniFile.IniReadValue("COMM", "nPowerSupply1BaudRate", "9600", strPath), out _Config.nPowerSupply1BaudRate);
			int.TryParse(_IniFile.IniReadValue("COMM", "nPowerSupply2Port", "3", strPath), out _Config.nPowerSupply2Port);
			int.TryParse(_IniFile.IniReadValue("COMM", "nPowerSupply2BaudRate", "9600", strPath), out _Config.nPowerSupply2BaudRate);
			int.TryParse(_IniFile.IniReadValue("COMM", "nPowerSupply3Port", "3", strPath), out _Config.nPowerSupply3Port);
			int.TryParse(_IniFile.IniReadValue("COMM", "nPowerSupply3BaudRate", "9600", strPath), out _Config.nPowerSupply3BaudRate);
			int.TryParse(_IniFile.IniReadValue("COMM", "nPowerSupply4Port", "3", strPath), out _Config.nPowerSupply4Port);
			int.TryParse(_IniFile.IniReadValue("COMM", "nPowerSupply4BaudRate", "9600", strPath), out _Config.nPowerSupply4BaudRate);
			int.TryParse(_IniFile.IniReadValue("COMM", "nBCDScanner1Port", "3", strPath), out _Config.nBCDScanner1Port);
			int.TryParse(_IniFile.IniReadValue("COMM", "nBCDScanner1BaudRate", "9600", strPath), out _Config.nBCDScanner1BaudRate);
			int.TryParse(_IniFile.IniReadValue("COMM", "nDMM1Port", "3", strPath), out _Config.nDMM1Port);
			int.TryParse(_IniFile.IniReadValue("COMM", "nDMM1BaudRate", "9600", strPath), out _Config.nDMM1BaudRate);
			_Config.strCellSimulator1IP = _IniFile.IniReadValue("COMM", "strCellSimulator1IP", "192.168.0.1", strPath);
			int.TryParse(_IniFile.IniReadValue("COMM", "nCellSimulator1Port", "60000", strPath), out _Config.nCellSimulator1Port);
			_Config.strCellSimulator2IP = _IniFile.IniReadValue("COMM", "strCellSimulator2IP", "192.168.0.1", strPath);
			int.TryParse(_IniFile.IniReadValue("COMM", "nCellSimulator2Port", "60000", strPath), out _Config.nCellSimulator2Port);
			_Config.strDmmIP = _IniFile.IniReadValue("COMM", "strDmmIP", "192.168.0.1", strPath);
			int.TryParse(_IniFile.IniReadValue("COMM", "nDmmPort", "23", strPath), out _Config.nDmmPort);
			int.TryParse(_IniFile.IniReadValue("COMM", "nBCDScanner2Port", "3", strPath), out _Config.nBCDScanner2Port);
			int.TryParse(_IniFile.IniReadValue("COMM", "nBCDScanner2BaudRate", "9600", strPath), out _Config.nBCDScanner2BaudRate);
			int.TryParse(_IniFile.IniReadValue("COMM", "nDMM2Port", "3", strPath), out _Config.nDMM2Port);
			int.TryParse(_IniFile.IniReadValue("COMM", "nDMM2BaudRate", "9600", strPath), out _Config.nDMM2BaudRate);
			_Config.strCellSimulator3IP = _IniFile.IniReadValue("COMM", "strCellSimulator3IP", "192.168.0.1", strPath);
			int.TryParse(_IniFile.IniReadValue("COMM", "nCellSimulator3Port", "60000", strPath), out _Config.nCellSimulator3Port);
			_Config.strCellSimulator4IP = _IniFile.IniReadValue("COMM", "strCellSimulator4IP", "192.168.0.1", strPath);
			int.TryParse(_IniFile.IniReadValue("COMM", "nCellSimulator4Port", "60000", strPath), out _Config.nCellSimulator4Port);
			_Config.strDmmIP2 = _IniFile.IniReadValue("COMM", "strDmmIP2", "192.168.0.1", strPath);
			int.TryParse(_IniFile.IniReadValue("COMM", "nDmmPort2", "23", strPath), out _Config.nDmmPort2);
			_Config.strCycloneMyIP = _IniFile.IniReadValue("COMM", "strCycloneMyIP", "192.168.0.1", strPath);
			int.TryParse(_IniFile.IniReadValue("COMM", "nCycloneMyPort", "5001", strPath), out _Config.nCycloneMyPort);
			_Config.strCycloneMyIP2 = _IniFile.IniReadValue("COMM", "strCycloneMyIP2", "192.168.0.1", strPath);
			int.TryParse(_IniFile.IniReadValue("COMM", "nCycloneMyPort2", "5001", strPath), out _Config.nCycloneMyPort2);
	

			_Config.strCounterMyIP = _IniFile.IniReadValue("COMM", "strCounterMyIP", "192.168.0.1", strPath);
			int.TryParse(_IniFile.IniReadValue("COMM", "nCounterMyPort", "5001", strPath), out _Config.nCounterMyPort);
			_Config.strCounterMyIP2 = _IniFile.IniReadValue("COMM", "strCounterMyIP2", "192.168.0.1", strPath);
			int.TryParse(_IniFile.IniReadValue("COMM", "nCounterMyPort2", "5001", strPath), out _Config.nCounterMyPort2);

			int.TryParse(_IniFile.IniReadValue("COMM", "nBcdPrinterPort", "-1", strPath), out _Config.nBcdPrinterPort);
			int.TryParse(_IniFile.IniReadValue("COMM", "nBcdPrinterPort2", "-1", strPath), out _Config.nBcdPrinterPort2);

			_Config.strRBMSIP = _IniFile.IniReadValue("COMM", "strRBMSIP", "192.168.0.1", strPath);
			int.TryParse(_IniFile.IniReadValue("COMM", "nRBMSPort", "502", strPath), out _Config.nRBMSPort);
			_Config.strRBMSIP2 = _IniFile.IniReadValue("COMM", "strRBMSIP2", "192.168.0.1", strPath);
			int.TryParse(_IniFile.IniReadValue("COMM", "nRBMSPort2", "502", strPath), out _Config.nRBMSPort2);
			//마지막 작업 모델
			_Config.strCurrentModel = _IniFile.IniReadValue("CURRENT", "strCurrentModel", "_NONAME_", strPath);
			_Config.strCurrentModel2 = _IniFile.IniReadValue("CURRENT", "strCurrentModel2", "_NONAME_", strPath);



			bool.TryParse(_IniFile.IniReadValue("COMMON", "bUseAdminPass", "true", strPath), out _Config.bUseAdminPass);
			_Config.strAdminPass = _IniFile.IniReadValue("COMMON", "strAdminPass", "1234", strPath);
			_Config.strMasterPass = _IniFile.IniReadValue("COMMON", "strMasterPass", "ld1234", strPath);


			bool.TryParse(_IniFile.IniReadValue("COMMON", "bUseMasterCheck", "true", strPath), out _Config.bUseMasterCheck);
			int.TryParse(_IniFile.IniReadValue("COMMON", "strMasterCheckDateTime", "0800", strPath), out _Config.strMasterCheckDateTime);
			int.TryParse(_IniFile.IniReadValue("COMMON", "nEolPinCount", "0000", strPath), out _Config.nEolPinCount);
			int.TryParse(_IniFile.IniReadValue("COMMON", "nEolPinCount2", "0000", strPath), out _Config.nEolPinCount2);



			int.TryParse(_IniFile.IniReadValue("ETC", "nLotClearTime", "0800", strPath), out _Config.nLotClearTime);
			bool.TryParse(_IniFile.IniReadValue("ETC", "bUseLotAutoClear", "false", strPath), out _Config.bUseLotAutoClear);
			bool.TryParse(_IniFile.IniReadValue("ETC", "bDmmEtcMode", "false", strPath), out _Config.bDmmEtcMode);
			
			_Config.strLanguage = _IniFile.IniReadValue("ETC", "strLanguage", "KOREA", strPath);
			
			
			_Config.strSaveMesDir = _IniFile.IniReadValue("DIR", "strSaveMesDir", "D:\\", strPath);
			_Config.strSaveMesDir2 = _IniFile.IniReadValue("DIR", "strSaveMesDir2", "D:\\", strPath);
			_Config.strBCDMesDir = _IniFile.IniReadValue("DIR", "strBCDMesDir", "D:\\", strPath);
			_Config.strBCDMesDir2 = _IniFile.IniReadValue("DIR", "strBCDMesDir2", "D:\\", strPath);

		


			_Config.strToolIP = _IniFile.IniReadValue("TOOL", $"strToolIP", "172.30.1.45", strPath);
			int.TryParse(_IniFile.IniReadValue("TOOL", $"nToolPort", "4545", strPath), out _Config.nToolPort);
			_Config.strToolIP2 = _IniFile.IniReadValue("TOOL", $"strToolIP2", "172.30.1.45", strPath);
			int.TryParse(_IniFile.IniReadValue("TOOL", $"nToolPort2", "4545", strPath), out _Config.nToolPort2);
		}


	}




}