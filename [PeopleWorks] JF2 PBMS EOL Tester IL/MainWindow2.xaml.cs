using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	/// <summary>
	/// MainWindow2.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow2 : Window
	{
		DispatcherTimer _timer = new DispatcherTimer();
		public static UserStartMassage2 _UserStartMassage2;
		public static PopUp2 _PopUp2;
		public MainWindow2()
		{
			InitializeComponent();
			gdLogView.ItemsSource = theApp.MainLogMessage2;
			gdWorkList.ItemsSource = theApp._TestData2;
			gdLogView_Copy2.ItemsSource = theApp._TestResultList2;



			((INotifyCollectionChanged)gdLogView.ItemsSource).CollectionChanged +=
			 (s, e) =>
			 {
				 if (e.Action ==
					 System.Collections.Specialized.NotifyCollectionChangedAction.Add)
				 {
					 gdLogView.ScrollIntoView(gdLogView.Items[gdLogView.Items.Count - 1]);
				 }
			 };

			((INotifyCollectionChanged)gdLogView_Copy2.ItemsSource).CollectionChanged +=
			(s, e) =>
			{
				if (e.Action ==
					System.Collections.Specialized.NotifyCollectionChangedAction.Add)
				{
					gdLogView_Copy2.ScrollIntoView(gdLogView_Copy2.Items[gdLogView_Copy2.Items.Count - 1]);
				}
			};


			lstLogData2.ItemsSource = theApp.lstMyLog2;

			
			// 타이머 시작
			_timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
			_timer.Tick += _timer_Tick;
			_timer.Start();
		}

		private void _timer_Tick(object sender, EventArgs e)
		{
			lbNowTime.Content = DateTime.Now.ToLongTimeString();
			lbNowDay.Content = DateTime.Now.ToLongDateString();

			lbTotalProductCount2.Content = theApp._LotCount2.nTotalCount.ToString();
			lbModelName2.Text = theApp._ModelInfo2.strModelName;

			lbNowProductCount2.Content = (theApp._LotCount2.nOkCount + theApp._LotCount2.nNGCount).ToString();
			lbNowOKCount2.Content = theApp._LotCount2.nOkCount.ToString();
			lbNowNGCount2.Content = theApp._LotCount2.nNGCount.ToString();

			lbEolPinCount2.Content = _Config.nEolPinCount2.ToString();
			if ((theApp._LotCount2.nOkCount + theApp._LotCount2.nNGCount) > 0)
			{
				lbNowOKCountPct2.Content = (((theApp._LotCount2.nOkCount * 1.0) / (theApp._LotCount2.nOkCount + theApp._LotCount2.nNGCount)) * 100.0).ToString("F1") + "%";
				lbNowNGCountPct2.Content = (((theApp._LotCount2.nNGCount * 1.0) / (theApp._LotCount2.nOkCount + theApp._LotCount2.nNGCount)) * 100.0).ToString("F1") + "%";
			}				   
			else
			{
				lbNowOKCountPct2.Content = "0.0%";
				lbNowNGCountPct2.Content = "0.0%";
			}
			lbProductCountClearTime2.Content = theApp._LotCount2.tProductClearTime.ToLongDateString() + " " + theApp._LotCount2.tProductClearTime.ToLongTimeString();

			lbCTime2.Content = theApp._tTackTimer2.Elapsed.ToString("mm':'ss':'ff");





			if (_Config.strLanguage == "ENGLISH")
			{
				// 최상단
				btModelChnageContent.Content = "Change Model";
				btModelSetupContent.Content = "Model Settings";

				// 생산 정보
				lbProductionInfo.Content = "Inspection Information";
				lbNowProduction.Content = "Current Inspection Quantity";
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

				if (_SysInfo2.eMainStatus == MAIN_STATUS2.READY)
				{
					lbMainStatus2.Content = "Waiting for work";
					lbMainStatus2.Background = Brushes.DarkGreen;
					btStart2.IsEnabled = true;
					btStop2.IsEnabled = false;
					theApp.TowerLampBlink(LAMP_COLOR.TL_YELLOW);

				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.ING)
				{
					lbMainStatus2.Content = "Inspecting";
					lbMainStatus2.Background = Brushes.Ivory;
					btStart2.IsEnabled = false;
					btStop2.IsEnabled = true;
					theApp.TowerLampOn(LAMP_COLOR.TL_YELLOW);
				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.OK)
				{
					lbMainStatus2.Content = "O.K";
					lbMainStatus2.Background = Brushes.LightGreen;
					btStart2.IsEnabled = true;
					btStop2.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.NG)
				{
					lbMainStatus2.Content = "N.G";
					lbMainStatus2.Background = Brushes.LightPink;
					btStart2.IsEnabled = true;
					btStop2.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.EMG_STOP)
				{
					lbMainStatus2.Content = "Forced to stop";
					lbMainStatus2.Background = Brushes.LightPink;
					btStart2.IsEnabled = true;
					btStop2.IsEnabled = false;
					theApp.TowerLampBlink(LAMP_COLOR.TL_RED);
				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.OK_MASTER_OK)
				{
					lbMainStatus2.Content = "O.K(OK quality master sample)";
					lbMainStatus2.Background = Brushes.LightGreen;
					btStart2.IsEnabled = true;
					btStop2.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.OK_MASTER_NG)
				{
					lbMainStatus2.Content = "N.G(OK quality master sample)";
					lbMainStatus2.Background = Brushes.LightPink;
					btStart2.IsEnabled = true;
					btStop2.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.NG_MASTER_OK)
				{
					lbMainStatus2.Content = "N.G(NG quality master sample)";
					lbMainStatus2.Background = Brushes.LightPink;
					btStart2.IsEnabled = true;
					btStop2.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.NG_MASTER_NG)
				{
					lbMainStatus2.Content = "O.K(NG quality master sample)";
					lbMainStatus2.Background = Brushes.LightGreen;
					btStart2.IsEnabled = true;
					btStop2.IsEnabled = false;
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

				if (_SysInfo2.eMainStatus == MAIN_STATUS2.READY)
				{
					lbMainStatus2.Content = "작업 대기중";
					lbMainStatus2.Background = Brushes.DarkGreen;
					btStart2.IsEnabled = true;
					btStop2.IsEnabled = false;
					theApp.TowerLampBlink(LAMP_COLOR.TL_YELLOW);

				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.ING)
				{
					lbMainStatus2.Content = "검사중";
					lbMainStatus2.Background = Brushes.Ivory;
					btStart2.IsEnabled = false;
					btStop2.IsEnabled = true;
					theApp.TowerLampOn(LAMP_COLOR.TL_YELLOW);
				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.OK)
				{
					lbMainStatus2.Content = "O.K";
					lbMainStatus2.Background = Brushes.LightGreen;
					btStart2.IsEnabled = true;
					btStop2.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.NG)
				{
					lbMainStatus2.Content = "N.G";
					lbMainStatus2.Background = Brushes.LightPink;
					btStart2.IsEnabled = true;
					btStop2.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.EMG_STOP)
				{
					lbMainStatus2.Content = "강제 정지됨";
					lbMainStatus2.Background = Brushes.LightPink;
					btStart2.IsEnabled = true;
					btStop2.IsEnabled = false;
					theApp.TowerLampBlink(LAMP_COLOR.TL_RED);
				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.OK_MASTER_OK)
				{
					lbMainStatus2.Content = "O.K(양품 마스터 샘플)";
					lbMainStatus2.Background = Brushes.LightGreen;
					btStart2.IsEnabled = true;
					btStop2.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.OK_MASTER_NG)
				{
					lbMainStatus2.Content = "N.G(양품 마스터 샘플)";
					lbMainStatus2.Background = Brushes.LightPink;
					btStart2.IsEnabled = true;
					btStop2.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.NG_MASTER_OK)
				{
					lbMainStatus2.Content = "N.G(불량 마스터 샘플)";
					lbMainStatus2.Background = Brushes.LightPink;
					btStart2.IsEnabled = true;
					btStop2.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.NG_MASTER_NG)
				{
					lbMainStatus2.Content = "O.K(불량 마스터 샘플)";
					lbMainStatus2.Background = Brushes.LightGreen;
					btStart2.IsEnabled = true;
					btStop2.IsEnabled = false;
					theApp.TowerLampOn(LAMP_COLOR.TL_GREEN);
				}

			}


			lbBarcode2.Content = _SysInfo2.strDispBarcode;
			lbSerialNum_Copy2.Content = _SysInfo2.strDispMac;

			if (_SysInfo2.nWriteSerialNum != 0) lbSerialNum2.Content = _SysInfo2.nWriteSerialNum.ToString();


			//gdWorkList.ScrollIntoView(gdWorkList.Items[_SysInfo2.nMainWorkStep]);


		}
		private void btDebugMonitor_Click(object sender, RoutedEventArgs e)
		{
			DebugMonitor _Window = new DebugMonitor();
			_Window.ShowDialog();
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

		public void TestLogSet2(int nIndex, string strData, string strResult)
		{

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

		}

		// Can Data
		public void CanLogData2(myCanData2 myData)
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
				lstLogData2.ScrollIntoView(lstLogData2.Items[lstLogData2.Items.Count - 1]);

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
				case "btStart2":

					//DateTime _dt1 = DateTime.Now;
					//DateTime _dt2 = _dt1.AddHours(8);

					//int _ts = _dt2.DayOfYear - _dt1.DayOfYear;

					//MessageBox.Show($"DT1 : {_dt1.DayOfYear} / DT2 : {_dt2} / Diff : {_ts}");

					//_SysInfo.nDispLen = 5;
					//theApp.AppendLogMsg2("1", MSG_TYPE.INFO);
					theApp.nProcessStep[(int)PROC_LIST.MAIN2] = 1000;
					break;

				case "btStop2":
					_SysInfo2.bEMGStop = true;
				
					_SysInfo2.bReadMainBcd = false;
					_SysInfo2.bReadMacBcd = false;
					theApp.nProcessStep[(int)PROC_LIST.MAIN2] = 80000;
					break;
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
				gdLogView_Copy12.ItemsSource = _SysInfo2._listNgInfo[gdWorkList.SelectedIndex];
			}
		}

		private void lbBarcode_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			//UserBarcodeEdit _form = new UserBarcodeEdit();
			//_form.ShowDialog();
		}

		private void btAHIPOT1PinCountClear2_Click(object sender, RoutedEventArgs e)
		{
			AdminPass _pwWindow = new AdminPass();

			if (_pwWindow.ShowDialog() == true)
			{
				_Config.nEolPinCount2 = 0;
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



		public void ShowUserStartMsgMessege2()
		{
			if (System.Windows.Forms.SystemInformation.MonitorCount > 1)
			{
				System.Drawing.Rectangle secondaryScreenRectangle = Screen.AllScreens[0].WorkingArea;
				UserStartMassage2 window = new UserStartMassage2();
				window.Show();
				// Show 메소드와 속성 설정의 순서가 중요하다.
				window.WindowStartupLocation = WindowStartupLocation.Manual;
				window.Left = secondaryScreenRectangle.Left;
				window.Top = secondaryScreenRectangle.Top;
				window.Width = secondaryScreenRectangle.Width;
				window.Height = secondaryScreenRectangle.Height;
				window.WindowState = System.Windows.WindowState.Maximized;

			}

			//if (_UserStartMassage2 == null)
			//	_UserStartMassage2 = new UserStartMassage2();
			//_UserStartMassage2.Show();

		}

		public void HideUserStartMsgMessege2()
		{
			if (_UserStartMassage2 == null)
				_UserStartMassage2 = new UserStartMassage2();
			_UserStartMassage2.Hide();

		}

		private void btModelChnage2_Click(object sender, RoutedEventArgs e)
		{
			ModelList2 _window = new ModelList2();
			_window.ShowDialog();
		}

		private void btModelSetup_Click(object sender, RoutedEventArgs e)
		{
			if (_Config.bUseAdminPass)
			{
				AdminPass _pwWindow = new AdminPass();

				if (_pwWindow.ShowDialog() == true)
				{
					ModelSetup2 _Window = new ModelSetup2();
					_Window.Show();
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
				ModelSetup2 _Window = new ModelSetup2();
				_Window.Show();
			}
		}

		private void btNutCountP_Click(object sender, RoutedEventArgs e)
		{
			if (_SysInfo2.nTipNowCount < _SysInfo2.nTipMaxCount && _SysInfo2.bTiteIngStart)
			{
				_SysInfo2.nTipNowCount++;
				TestLogSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nTipNowCount.ToString(), "");
			}

		}

		private void btNutCountM_Click(object sender, RoutedEventArgs e)
		{
			if (_SysInfo2.nTipNowCount > 0 && _SysInfo2.bTiteIngStart)
			{
				_SysInfo2.nTipNowCount--;
				TestLogSet2(_SysInfo2.nMainWorkStep, _SysInfo2.nTipNowCount.ToString(), "");
			}

		}

		private void btLotClear_Click(object sender, RoutedEventArgs e)
		{
			if (_Config.bUseAdminPass)
			{
				AdminPass _pwWindow = new AdminPass();

				if (_pwWindow.ShowDialog() == true)
				{
					if (_Config.strLanguage == "ENGLISH")
					{
						if (System.Windows.MessageBox.Show("Do you want to reset the inspection quantity?", "Reset Inspection Quantity", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
						{
							theApp._LotCount2.nOkCount = 0;
							theApp._LotCount2.nNGCount = 0;
							theApp._LotCount2.nLotCount = 0;
							theApp._LotCount2.nProductCount = 0;
							theApp._LotCount2.tProductClearTime = DateTime.Now;
							theApp.SaveModelProductCount2(theApp._LotCount2, theApp._ModelInfo2.strModelName);
						}
					}
					else
					{
						if (System.Windows.MessageBox.Show("검사 수량을 초기화 하시겠습니까?", "검사 수량 초기화", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
						{
							theApp._LotCount2.nOkCount = 0;
							theApp._LotCount2.nNGCount = 0;
							theApp._LotCount2.nLotCount = 0;
							theApp._LotCount2.nProductCount = 0;
							theApp._LotCount2.tProductClearTime = DateTime.Now;
							theApp.SaveModelProductCount2(theApp._LotCount2, theApp._ModelInfo2.strModelName);
						}
					}

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
				if (_Config.strLanguage == "ENGLISH")
				{
					if (System.Windows.MessageBox.Show("Do you want to reset the inspection quantity?", "Reset Inspection Quantity", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
					{
						theApp._LotCount2.nOkCount = 0;
						theApp._LotCount2.nNGCount = 0;
						theApp._LotCount2.tProductClearTime = DateTime.Now;
						theApp.SaveModelProductCount2(theApp._LotCount2, theApp._ModelInfo2.strModelName);
					}
				}
				else
				{
					if (System.Windows.MessageBox.Show("검사 수량을 초기화 하시겠습니까?", "검사 수량 초기화", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
					{
						theApp._LotCount2.nOkCount = 0;
						theApp._LotCount2.nNGCount = 0;
						theApp._LotCount2.tProductClearTime = DateTime.Now;
						theApp.SaveModelProductCount2(theApp._LotCount2, theApp._ModelInfo2.strModelName);
					}
				}
			}
		}

		private void btBarcodeReprint_Click(object sender, RoutedEventArgs e)
		{
			BarcodeRePrint2 _window = new BarcodeRePrint2();
			_window.ShowDialog();
		}

		public void ShowPopUpMessage()
		{

			if (System.Windows.Forms.SystemInformation.MonitorCount > 1)
			{
				System.Drawing.Rectangle secondaryScreenRectangle = Screen.AllScreens[0].WorkingArea;
				PopUp2 window = new PopUp2();
				window.Show();
				// Show 메소드와 속성 설정의 순서가 중요하다.
				window.WindowStartupLocation = WindowStartupLocation.Manual;
				window.Left = secondaryScreenRectangle.Left;
				window.Top = secondaryScreenRectangle.Top;
				window.Width = secondaryScreenRectangle.Width;
				window.Height = secondaryScreenRectangle.Height;
				window.WindowState = System.Windows.WindowState.Maximized;

			}

			//if (_PopUp2 == null)
			//	_PopUp2 = new PopUp2();
			//_PopUp2.Show();
		}
	}
	
}

