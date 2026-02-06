using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	/// <summary>
	/// ResultMassage2.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class ResultMassage2 : Window
	{

		DispatcherTimer _timer = new DispatcherTimer();
		public ResultMassage2()
		{
			InitializeComponent();

			// 타이머 시작
			_timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
			_timer.Tick += _timer_Tick;
			_timer.Start();
		}

		private void _timer_Tick(object sender, EventArgs e)
		{
			if (_Config.strLanguage == "ENGLISH")
			{
				// 최상

				 if (_SysInfo2.eMainStatus == MAIN_STATUS2.OK)
				{
					lbMainStatus.Content = "O.K";
					lbMainStatus.Background = Brushes.LightGreen;

				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.NG)
				{
					lbMainStatus.Content = "N.G";
					lbMainStatus.Background = Brushes.LightPink;

				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.EMG_STOP)
				{
					lbMainStatus.Content = "Forced to stop";
					lbMainStatus.Background = Brushes.LightPink;

				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.OK_MASTER_OK)
				{
					lbMainStatus.Content = "O.K(OK quality master sample)";
					lbMainStatus.Background = Brushes.LightGreen;

				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.OK_MASTER_NG)
				{
					lbMainStatus.Content = "N.G(OK quality master sample)";
					lbMainStatus.Background = Brushes.LightPink;
	
				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.NG_MASTER_OK)
				{
					lbMainStatus.Content = "N.G(NG quality master sample)";
					lbMainStatus.Background = Brushes.LightPink;

				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.NG_MASTER_NG)
				{
					lbMainStatus.Content = "O.K(NG quality master sample)";
					lbMainStatus.Background = Brushes.LightGreen;

				}


			}
			else
			{
				// 최상단;


				if (_SysInfo2.eMainStatus == MAIN_STATUS2.OK)
				{
					lbMainStatus.Content = "O.K";
					lbMainStatus.Background = Brushes.LightGreen;

				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.NG)
				{
					lbMainStatus.Content = "N.G";
					lbMainStatus.Background = Brushes.LightPink;

				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.EMG_STOP)
				{
					lbMainStatus.Content = "강제 정지됨";
					lbMainStatus.Background = Brushes.LightPink;

				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.OK_MASTER_OK)
				{
					lbMainStatus.Content = "O.K(양품 마스터 샘플)";
					lbMainStatus.Background = Brushes.LightGreen;

				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.OK_MASTER_NG)
				{
					lbMainStatus.Content = "N.G(양품 마스터 샘플)";
					lbMainStatus.Background = Brushes.LightPink;

				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.NG_MASTER_OK)
				{
					lbMainStatus.Content = "N.G(불량 마스터 샘플)";
					lbMainStatus.Background = Brushes.LightPink;

				}
				else if (_SysInfo2.eMainStatus == MAIN_STATUS2.NG_MASTER_NG)
				{
					lbMainStatus.Content = "O.K(불량 마스터 샘플)";
					lbMainStatus.Background = Brushes.LightGreen;

				}


			}


		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{

			Hide();
			e.Cancel = true;
		}
	}
}
