using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	/// <summary>
	/// MainWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow : Window
	{
		DispatcherTimer _timer = new DispatcherTimer();
		public static UserStartMassage _UserStartMassage;
		public MainWindow()
		{
			//this.Title = "[PeopleWorks] JF2 RBMS BBMS EOL Tester IL (Ver. 20250613.1)";
			//Duplicate_execution(Title);   // 중복실행 방지

			InitializeComponent();

			if (System.Windows.Forms.SystemInformation.MonitorCount > 1)
			{
				System.Drawing.Rectangle secondaryScreenRectangle = Screen.AllScreens[0].WorkingArea;
				MainWindow2 window = new MainWindow2();
				window.Show();
				// Show 메소드와 속성 설정의 순서가 중요하다.
				window.WindowStartupLocation = WindowStartupLocation.Manual;
				window.Left = secondaryScreenRectangle.Left;
				window.Top = secondaryScreenRectangle.Top;
				window.Width = secondaryScreenRectangle.Width;
				window.Height = secondaryScreenRectangle.Height;
				window.WindowState = System.Windows.WindowState.Maximized;

			}
			
		
			gdLogView.ItemsSource = theApp.MainLogMessage;
			gdWorkList.ItemsSource = theApp._TestData;
			gdLogView_Copy.ItemsSource = theApp._TestResultList;


			((INotifyCollectionChanged)gdLogView.ItemsSource).CollectionChanged +=
			 (s, e) =>
			 {
				 if (e.Action ==
					 System.Collections.Specialized.NotifyCollectionChangedAction.Add)
				 {
					 gdLogView.ScrollIntoView(gdLogView.Items[gdLogView.Items.Count - 1]);
				 }
			 };
		

			((INotifyCollectionChanged)gdLogView_Copy.ItemsSource).CollectionChanged +=
			(s, e) =>
			{
				if (e.Action ==
					System.Collections.Specialized.NotifyCollectionChangedAction.Add)
				{
					gdLogView_Copy.ScrollIntoView(gdLogView_Copy.Items[gdLogView_Copy.Items.Count - 1]);
				}
			};





			lstLogData.ItemsSource = theApp.lstMyLog;

			theApp.initMachine();

			// 메인 쓰레드 시작
			theApp.MainThread.Start();
			

			// 타이머 시작
			_timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
			_timer.Tick += _timer_Tick;
			_timer.Start();
		}
		Mutex mutex = null;
		private void Duplicate_execution(string mutexName)
		{
			try
			{
				mutex = new Mutex(false, mutexName);
			}
			catch (Exception ex)
			{
				//                MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace + "\n\n" + "Application Exiting…", "Exception thrown");
				_SysInfo.bMainProcessStop = true;
				System.Windows.Application.Current.Shutdown();

			}
			if (mutex.WaitOne(0, false))
			{
				InitializeComponent();
			}
			else
			{
				//                MessageBox.Show("Application already startet.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
				_SysInfo.bMainProcessStop = true;
				System.Windows.Application.Current.Shutdown();

			}
		}



		private void _timer_Tick(object sender, EventArgs e)
		{
			lbNowTime.Content = DateTime.Now.ToLongTimeString();
			lbNowDay.Content = DateTime.Now.ToLongDateString();

			lbTotalProductCount.Content = theApp._Count.nTotalCount.ToString();
			lbModelName.Text = theApp._ModelInfo.strModelName;

			lbNowProductCount.Content = (theApp._LotCount.nOkCount + theApp._LotCount.nNGCount).ToString();
			lbNowOKCount.Content = theApp._LotCount.nOkCount.ToString();
			lbNowNGCount.Content = theApp._LotCount.nNGCount.ToString();

			lbEolPinCount.Content = _Config.nEolPinCount.ToString();
			if ((theApp._LotCount.nOkCount + theApp._LotCount.nNGCount) > 0)
			{
				lbNowOKCountPct.Content = (((theApp._LotCount.nOkCount * 1.0) / (theApp._LotCount.nOkCount + theApp._LotCount.nNGCount)) * 100.0).ToString("F1") + "%";
				lbNowNGCountPct.Content = (((theApp._LotCount.nNGCount * 1.0) / (theApp._LotCount.nOkCount + theApp._LotCount.nNGCount)) * 100.0).ToString("F1") + "%";
			}
			else
			{
				lbNowOKCountPct.Content = "0.0%";
				lbNowNGCountPct.Content = "0.0%";
			}
			lbProductCountClearTime.Content = theApp._LotCount.tProductClearTime.ToLongDateString() + " " + theApp._LotCount.tProductClearTime.ToLongTimeString();

			lbCTime.Content = theApp._tTackTimer.Elapsed.ToString("mm':'ss':'ff");




			if (_Config.strLanguage == "ENGLISH")
			{
				// 최상단
				btModelChnageContent.Content = "Change Model";
				btModelSetupContent.Content = "Model settings";

				// 생산 정보
				lbProductionInfo.Content = "Inspection Information";
				lbNowProduction.Content = "Current Inspection quantity";
				lbProductionClearTime.Content = "Inspection Quantity Initialization Time";

				btProductCountClearContent.Content = "Reset Inspection Quantity";


				btStartContent.Content = "Start";
				btStopContent.Content = "Stop";
				lbOKCount.Content = "Quantity Of OK Product";
				lbNGCount.Content = "Quantity Of NG Product";
				lbAllProduction.Content = "Total Inspection Quantity";
				lbGoodproductrate.Content = "OK Product Rate";
				lbBadproductrate.Content = "NG Product Rate";
				lbOptionalErrorHistory.Content = "Optional Error History";
				lbInspectionhistory.Content = "Inspection History";

				if (_SysInfo.eMainStatus == MAIN_STATUS.READY)
				{
					lbMainStatus.Content = "Waiting for work";
					lbMainStatus.Background = Brushes.DarkGreen;
					btStart.IsEnabled = true;
					btStop.IsEnabled = false;
					theApp.TowerLampBlink(LAMP_COLOR.TL_YELLOW);

				}
				else if (_SysInfo.eMainStatus == MAIN_STATUS.ING)
				{
					lbMainStatus.Content = "Inspecting";
					lbMainStatus.Background = Brushes.Ivory;
					btStart.IsEnabled = false;
					btStop.IsEnabled = true;
					theApp.TowerLampOn(LAMP_COLOR.TL_YELLOW);
				}
				else if (_SysInfo.eMainStatus == MAIN_STATUS.OK)
				{
					lbMainStatus.Content = "O.K";
					lbMainStatus.Background = Brushes.LightGreen;
					btStart.IsEnabled = true;
					btStop.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}
				else if (_SysInfo.eMainStatus == MAIN_STATUS.NG)
				{
					lbMainStatus.Content = "N.G";
					lbMainStatus.Background = Brushes.LightPink;
					btStart.IsEnabled = true;
					btStop.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}
				else if (_SysInfo.eMainStatus == MAIN_STATUS.EMG_STOP)
				{
					lbMainStatus.Content = "Forced to stop";
					lbMainStatus.Background = Brushes.LightPink;
					btStart.IsEnabled = true;
					btStop.IsEnabled = false;
					theApp.TowerLampBlink(LAMP_COLOR.TL_RED);
				}
				else if (_SysInfo.eMainStatus == MAIN_STATUS.OK_MASTER_OK)
				{
					lbMainStatus.Content = "O.K(OK quality master sample)";
					lbMainStatus.Background = Brushes.LightGreen;
					btStart.IsEnabled = true;
					btStop.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}
				else if (_SysInfo.eMainStatus == MAIN_STATUS.OK_MASTER_NG)
				{
					lbMainStatus.Content = "N.G(OK quality master sample)";
					lbMainStatus.Background = Brushes.LightPink;
					btStart.IsEnabled = true;
					btStop.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}
				else if (_SysInfo.eMainStatus == MAIN_STATUS.NG_MASTER_OK)
				{
					lbMainStatus.Content = "N.G(NG quality master sample)";
					lbMainStatus.Background = Brushes.LightPink;
					btStart.IsEnabled = true;
					btStop.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}
				else if (_SysInfo.eMainStatus == MAIN_STATUS.NG_MASTER_NG)
				{
					lbMainStatus.Content = "O.K(NG quality master sample)";
					lbMainStatus.Background = Brushes.LightGreen;
					btStart.IsEnabled = true;
					btStop.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}


			}
			else
			{
				// 최상단
				btModelChnageContent.Content = "모델 변경";
				btModelSetupContent.Content = "모델 설정";

				// 생산 정보
				lbProductionInfo.Content = "검사 정보";
				lbNowProduction.Content = "현재 검사 수량";
				lbProductionClearTime.Content = "검사 수량 초기화 시간";

				btProductCountClearContent.Content = "검사 수량 초기화";

				btStartContent.Content = "시  작";
				btStopContent.Content = "정  지";
				lbOKCount.Content = "양품 수량";
				lbNGCount.Content = "불량 수량";
				lbAllProduction.Content = "누적 검사 수량";
				lbGoodproductrate.Content = "양품률";
				lbBadproductrate.Content = "불량률";
				lbOptionalErrorHistory.Content = "선택항목 불량 내역";
				lbInspectionhistory.Content = "검사 이력";

				if (_SysInfo.eMainStatus == MAIN_STATUS.READY)
				{
					lbMainStatus.Content = "작업 대기중";
					lbMainStatus.Background = Brushes.DarkGreen;
					btStart.IsEnabled = true;
					btStop.IsEnabled = false;
					theApp.TowerLampBlink(LAMP_COLOR.TL_YELLOW);

				}
				else if (_SysInfo.eMainStatus == MAIN_STATUS.ING)
				{
					lbMainStatus.Content = "검사중";
					lbMainStatus.Background = Brushes.Ivory;
					btStart.IsEnabled = false;
					btStop.IsEnabled = true;
					theApp.TowerLampOn(LAMP_COLOR.TL_YELLOW);
				}
				else if (_SysInfo.eMainStatus == MAIN_STATUS.OK)
				{
					lbMainStatus.Content = "O.K";
					lbMainStatus.Background = Brushes.LightGreen;
					btStart.IsEnabled = true;
					btStop.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}
				else if (_SysInfo.eMainStatus == MAIN_STATUS.NG)
				{
					lbMainStatus.Content = "N.G";
					lbMainStatus.Background = Brushes.LightPink;
					btStart.IsEnabled = true;
					btStop.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}
				else if (_SysInfo.eMainStatus == MAIN_STATUS.EMG_STOP)
				{
					lbMainStatus.Content = "강제 정지됨";
					lbMainStatus.Background = Brushes.LightPink;
					btStart.IsEnabled = true;
					btStop.IsEnabled = false;
					theApp.TowerLampBlink(LAMP_COLOR.TL_RED);
				}
				else if (_SysInfo.eMainStatus == MAIN_STATUS.OK_MASTER_OK)
				{
					lbMainStatus.Content = "O.K(양품 마스터 샘플)";
					lbMainStatus.Background = Brushes.LightGreen;
					btStart.IsEnabled = true;
					btStop.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}
				else if (_SysInfo.eMainStatus == MAIN_STATUS.OK_MASTER_NG)
				{
					lbMainStatus.Content = "N.G(양품 마스터 샘플)";
					lbMainStatus.Background = Brushes.LightPink;
					btStart.IsEnabled = true;
					btStop.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}
				else if (_SysInfo.eMainStatus == MAIN_STATUS.NG_MASTER_OK)
				{
					lbMainStatus.Content = "N.G(불량 마스터 샘플)";
					lbMainStatus.Background = Brushes.LightPink;
					btStart.IsEnabled = true;
					btStop.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}
				else if (_SysInfo.eMainStatus == MAIN_STATUS.NG_MASTER_NG)
				{
					lbMainStatus.Content = "O.K(불량 마스터 샘플)";
					lbMainStatus.Background = Brushes.LightGreen;
					btStart.IsEnabled = true;
					btStop.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}


			}


			lbBarcode.Content = _SysInfo.strDispBarcode;
			lbSerialNum_Copy.Content = _SysInfo.strDispMac;

			if (_SysInfo.nWriteSerialNum != 0) lbSerialNum.Content = _SysInfo.nWriteSerialNum.ToString();

		}


		// 디버그 모니터
		private void btDebugMonitor_Click(object sender, RoutedEventArgs e)
		{
			DebugMonitor _Window = new DebugMonitor();
			_Window.ShowDialog();
		}


		// 수량정보 초기화
		private void btLotClear_Click(object sender, RoutedEventArgs e)
		{
			if (_Config.bUseAdminPass)
			{
				AdminPass _pwWindow = new AdminPass();

				if (_pwWindow.ShowDialog() == true)
				{
					if(_Config.strLanguage == "ENGLISH")
					{
						if (System.Windows.MessageBox.Show("Do you want to reset the inspection quantity?", "Reset Inspection Quantity", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
						{
							theApp._LotCount.nOkCount = 0;
							theApp._LotCount.nNGCount = 0;
							theApp._LotCount.nLotCount = 0;
							theApp._LotCount.nProductCount = 0;
							theApp._LotCount.tProductClearTime = DateTime.Now;
							theApp.SaveModelProductCount(theApp._LotCount, theApp._ModelInfo.strModelName);
						}
					}
					else
					{
						if (System.Windows.MessageBox.Show("검사 수량을 초기화 하시겠습니까?", "검사 수량 초기화", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
						{
							theApp._LotCount.nOkCount = 0;
							theApp._LotCount.nNGCount = 0;
							theApp._LotCount.nLotCount = 0;
							theApp._LotCount.nProductCount = 0;
							theApp._LotCount.tProductClearTime = DateTime.Now;
							theApp.SaveModelProductCount(theApp._LotCount, theApp._ModelInfo.strModelName);
						}
					}
					
				}
				else
				{
					if(_Config.strLanguage == "ENGLISH")
					{
						System.Windows.MessageBox.Show("Password does not match.");
					}
					else
					{
						System.Windows.MessageBox.Show("비밀번호가 일치하지 않습니다.");
					}
					
				}
			}
			else
			{
				if (_Config.strLanguage == "ENGLISH")
				{
					if (System.Windows.MessageBox.Show("Do you want to reset the inspection quantity?", "Reset Inspection Quantity", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
					{
						theApp._LotCount.nOkCount = 0;
						theApp._LotCount.nNGCount = 0;
						theApp._LotCount.tProductClearTime = DateTime.Now;
						theApp.SaveModelProductCount(theApp._LotCount, theApp._ModelInfo.strModelName);
					}
				}
				else
				{
					if (System.Windows.MessageBox.Show("검사 수량을 초기화 하시겠습니까?", "검사 수량 초기화", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
					{
						theApp._LotCount.nOkCount = 0;
						theApp._LotCount.nNGCount = 0;
						theApp._LotCount.tProductClearTime = DateTime.Now;
						theApp.SaveModelProductCount(theApp._LotCount, theApp._ModelInfo.strModelName);
					}
				}
			}
		}


		// 모델 변경
		private void btModelChnage_Click(object sender, RoutedEventArgs e)
		{
			ModelList _window = new ModelList();
			_window.ShowDialog();
		}

		// 스텝 모니터
		private void btStepMonitor_Click(object sender, RoutedEventArgs e)
		{
			StepMonitorV2 _window = new StepMonitorV2();
			_window.Show();
		}


		// 시스템 설정
		private void btSysSetting_Click(object sender, RoutedEventArgs e)
		{
			if (_Config.bUseAdminPass)
			{
				AdminPass _pwWindow = new AdminPass();

				if (_pwWindow.ShowDialog() == true)
				{
					SystemSetting _window = new SystemSetting();
					_window.ShowDialog();
				}
				else
				{
					if (_Config.strLanguage == "ENGLISH")
					{
						System.Windows.MessageBox.Show("Password does not match.");
					}
					else
					{
						System.Windows.MessageBox.Show("비밀번호가 일치하지 않습니다.");
					}
				}
			}
			else
			{
				SystemSetting _window = new SystemSetting();
				_window.ShowDialog();
			}


		}
		// IO 모니터 버튼 클릭 작업
		private void btIOMonitor_Click(object sender, RoutedEventArgs e)
		{
			DIOMonitor _window = new DIOMonitor();
			_window.ShowDialog();
		}
		// 버전 정보
		private void btVInfo_Click(object sender, RoutedEventArgs e)
		{
			VersionInfo _Window = new VersionInfo();
			_Window.ShowDialog();

			theApp.SaveResultData();
		}

		// 모델 설정
		private void btModelSetup_Click(object sender, RoutedEventArgs e)
		{
			if (_Config.bUseAdminPass)
			{
				AdminPass _pwWindow = new AdminPass();

				if (_pwWindow.ShowDialog() == true)
				{
					ModelSetup _Window = new ModelSetup();
					_Window.ShowDialog();
				}
				else
				{
					if (_Config.strLanguage == "ENGLISH")
					{
						System.Windows.MessageBox.Show("Password does not match.");
					}
					else
					{
						System.Windows.MessageBox.Show("비밀번호가 일치하지 않습니다.");
					}
				}
			}
			else
			{
				ModelSetup _Window = new ModelSetup();
				_Window.ShowDialog();
			}
		}

		public void TestLogSet(int nIndex, string strData, string strResult)
		{
			myTestData _td = new myTestData();

			_td.strTestName = theApp._TestData[nIndex].strTestName;
			_td.SpecMin = theApp._TestData[nIndex].SpecMin;
			_td.SpecMax = theApp._TestData[nIndex].SpecMax;
			_td.Unit = theApp._TestData[nIndex].Unit;
			_td.strType = theApp._TestData[nIndex].strType;
			_td.Cate = theApp._TestData[nIndex].Cate;

			_td.strResult = strResult;
			_td.Data = strData;

			theApp._TestData[nIndex] = _td;

			gdWorkList.ScrollIntoView(gdWorkList.Items[nIndex]);

			pgBarProcess.Maximum = gdWorkList.Items.Count;
			pgBarProcess.Value = nIndex;
		}

		// Can Data
		public void CanLogData(myCanData myData)
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
			lstLogData.ScrollIntoView(lstLogData.Items[lstLogData.Items.Count - 1]);

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

		}



		// 시작 버튼 클릭
		private void ControlButton_Click(object sender, RoutedEventArgs e)
		{
			switch (((System.Windows.Controls.Button)sender).Name)
			{
				case "btStart":

					//DateTime _dt1 = DateTime.Now;
					//DateTime _dt2 = _dt1.AddHours(8);

					//int _ts = _dt2.DayOfYear - _dt1.DayOfYear;

					//MessageBox.Show($"DT1 : {_dt1.DayOfYear} / DT2 : {_dt2} / Diff : {_ts}");

					//_SysInfo.nDispLen = 5;
					theApp.nProcessStep[(int)PROC_LIST.MAIN] = 1000;
					break;

				case "btStop":
					_SysInfo.bEMGStop = true;
					theApp.nProcessStep[(int)PROC_LIST.MAIN] = 80000;
					break;
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (!_SysInfo.bMainProcessStop)
			{
				if (_Config.strLanguage == "ENGLISH")
				{
					if (System.Windows.MessageBox.Show("Do you want to exit the program?", "Exit Program", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
					{
						_SysInfo.bMainProcessStop = true;
					}
				}
				else
				{
					if (System.Windows.MessageBox.Show("프로그램을 종료하시겠습니까?", "프로그램 종료", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
					{
						_SysInfo.bMainProcessStop = true;
					}
				}
				e.Cancel = true;
			}
		}

		private void gdWorkList_LoadingRow(object sender, DataGridRowEventArgs e)
		{
			e.Row.Header = (e.Row.GetIndex() + 1).ToString();
		}

		private void gdWorkList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			if (gdWorkList.SelectedIndex >= 0)
			{
				gdLogView_Copy1.ItemsSource = _SysInfo._listNgInfo[gdWorkList.SelectedIndex];
			}
		}

		private void lbBarcode_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			//UserBarcodeEdit _form = new UserBarcodeEdit();
			//_form.ShowDialog();
		}

		private void btAHIPOT1PinCountClear_Click(object sender, RoutedEventArgs e)
		{
			AdminPass _pwWindow = new AdminPass();

			if (_pwWindow.ShowDialog() == true)
			{
				_Config.nEolPinCount = 0;
			}
			else
			{
				if (_Config.strLanguage == "ENGLISH")
				{
					System.Windows.MessageBox.Show("Password does not match.");
				}
				else
				{
					System.Windows.MessageBox.Show("비밀번호가 일치하지 않습니다.");
				}
			}
			//theApp.nProcessStep[(int)PROC_LIST.MANUAL] = 3000;
		}



		public void ShowUserStartMsgMessege()
		{
			//StartCBCheck startCB = new StartCBCheck();
			//            startCB.Close();

			if (_UserStartMassage == null)
				_UserStartMassage = new UserStartMassage();
			_UserStartMassage.Show();

		}

		public void HideUserStartMsgMessege()
		{
			if (_UserStartMassage == null)
				_UserStartMassage = new UserStartMassage();
			_UserStartMassage.Hide();

		}

		private void btCellSimulator_Click(object sender, RoutedEventArgs e)
		{

			CellSimulator _window = new CellSimulator();
			_window.ShowDialog();
		}

		private void btNutCountP_Click(object sender, RoutedEventArgs e)
		{
			if(_SysInfo.nTipNowCount < _SysInfo.nTipMaxCount && _SysInfo.bTiteIngStart)
			{
				_SysInfo.nTipNowCount++;
				TestLogSet(_SysInfo.nMainWorkStep, _SysInfo.nTipNowCount.ToString(), "");
			}
			
		}

		private void btNutCountM_Click(object sender, RoutedEventArgs e)
		{
			if(_SysInfo.nTipNowCount > 0 && _SysInfo.bTiteIngStart)
			{
				_SysInfo.nTipNowCount--;
				TestLogSet(_SysInfo.nMainWorkStep, _SysInfo.nTipNowCount.ToString(), "");
			}
			
		}

		private void btCyclontestFile_Click(object sender, RoutedEventArgs e)
		{
			UInt32 connection_type = cyclone_control_api.CyclonePortType_USB;
			UInt32 handle = 0;

			//theApp.AppendLogMsg($"Contacting IP{_Config.strCycloneMyIP}", MSG_TYPE.LOG);
			//System.Windows.Forms.Application.DoEvents();
			//connection_type = convert_dropboxindex_to_connectiontype(1);
			handle = cyclone_control_api.connectToCyclone(_Config.strCycloneMyIP);
			if (handle == 0)
			{
				theApp.AppendLogMsg("Error Opening Device.", MSG_TYPE.LOG);
			}
			else
			{
				theApp.AppendLogMsg($"{ cyclone_control_api.getImageDescription(handle, Convert.ToByte(3))}",MSG_TYPE.INFO);

			}

		}

		private void btCyclontestConect_Click(object sender, RoutedEventArgs e)
		{
			UInt32 image_count = 0;
			//UInt32 connection_type = cyclone_control_api.CyclonePortType_USB;
			UInt32 handle = 0;


			theApp.AppendLogMsg($"Contacting IP{_Config.strCycloneMyIP}", MSG_TYPE.LOG);
			System.Windows.Forms.Application.DoEvents();
			//connection_type = cyclone_control_api.CyclonePortType_Ethernet;
			handle = cyclone_control_api.connectToCyclone(_Config.strCycloneMyIP);

			if (handle == 0)
			{
				theApp.AppendLogMsg("Error Opening Device.", MSG_TYPE.LOG);
			}
			else
			{
				image_count = cyclone_control_api.countCycloneImages(handle);
				theApp.AppendLogMsg($"Total Images ={image_count.ToString()}", MSG_TYPE.LOG);

			}

		}

		private void btCyclontest_Click(object sender, RoutedEventArgs e)
		{
			UInt32 connection_type = cyclone_control_api.CyclonePortType_USB;
			UInt32 handle1 = 0;

			bool cyclone1done = false;


			//connection_type = convert_dropboxindex_to_connectiontype(1);
			handle1 = cyclone_control_api.connectToCyclone(_Config.strCycloneMyIP);
			if (handle1 == 0)
			{
				theApp.AppendLogMsg($"Error Opening Device", MSG_TYPE.LOG);
				

			}
			else
			{
				theApp.AppendLogMsg($"Programming Image on IP1 ...", MSG_TYPE.LOG);

				cyclone_control_api.startImageExecution(handle1, Convert.ToByte(2));
			}
			

			System.Windows.Forms.Application.DoEvents();

			do
			{

				if (cyclone_control_api.checkCycloneExecutionStatus(handle1) == 0)
				{
					if (cyclone_control_api.getNumberOfErrors(handle1) == 0)
					{
						theApp.AppendLogMsg($"Programming was successful.", MSG_TYPE.LOG);

					}
					else
					{
						theApp.AppendLogMsg($"Error Code = {cyclone_control_api.getErrorCode(handle1, 1).ToString()}", MSG_TYPE.LOG);
					}
					cyclone1done = false;
				}

			} while (!(cyclone1done));

			/*
            // Program Dynamic Data test  
            bool dynamicDataResult;
            Byte[] managedByteArray = new Byte[] { (byte)'1', (byte)'2', (byte)'3', (byte)'4', (byte)'5', (byte)'6', (byte)'7', (byte)'8' };
            dynamicDataResult = cyclone_control_api.startDynamicDataProgram(handle1, 0x500, 0x8, managedByteArray);
            */
		}

		private void btBarcodeReprint_Click(object sender, RoutedEventArgs e)
		{
			BarcodeRePrint _window = new BarcodeRePrint();
			_window.ShowDialog();
		}

		public void ChangeUI(int nIndex)
		{

			foreach (Window window in System.Windows.Application.Current.Windows)
			{
				if (window.Title == "[PeopleWorks] JF2 RBMS BBMS EOL Tester IL2 (Ver. 20250613.1)") // 원하는 윈도우 찾기
				{
					// UI 컨트롤 직접 수정
					if (window is MainWindow2 customWindow)
					{
						//theApp.AppendLogMsg("들어오긴함?", MSG_TYPE.INFO);
						//theApp.AppendLogMsg($"{window.Title}", MSG_TYPE.INFO);
						//theApp.AppendLogMsg($"{nIndex}", MSG_TYPE.INFO);
						customWindow.gdWorkList.ScrollIntoView(customWindow.gdWorkList.Items[nIndex]);
						customWindow.pgBarProcess2.Maximum = gdWorkList.Items.Count;
						customWindow.pgBarProcess2.Value = nIndex;
					}

					break; // 찾은 윈도우만 수정하고 종료
				}
			}
		}
		public void ChangeUI2(int nIndex)
		{

			foreach (Window window in System.Windows.Application.Current.Windows)
			{
				if (window.Title == "[PeopleWorks] JF2 RBMS BBMS EOL Tester IL2 (Ver. 20250613.1)") // 원하는 윈도우 찾기
				{
					// UI 컨트롤 직접 수정
					if (window is MainWindow2 customWindow)
					{
						customWindow.lstLogData2.ScrollIntoView(customWindow.lstLogData2.Items[customWindow.lstLogData2.Items.Count - 1]);
					}

					break; // 찾은 윈도우만 수정하고 종료
				}
			}

			//App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE         // Log Clear
			//{
			//	MainWindow2 _window2 = new MainWindow2();
			//	_window2.TestLogSet2(nIndex, strData, strResult);

			//});
		}
	}
    
}

