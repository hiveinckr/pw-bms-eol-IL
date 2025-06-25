using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
	/// BarcodePopUP2.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class BarcodePopUP2 : Window
	{
		DispatcherTimer _timer = new DispatcherTimer();

		public BarcodePopUP2()
		{
			InitializeComponent();

			var screens = Screen.AllScreens;
			//if (screens.Length > 1)
			//{
			//	var secondaryScreen = screens.FirstOrDefault(s => !s.Primary) ?? screens[2];
			//	var workingArea = secondaryScreen.WorkingArea;

			//	var window = new BarcodePopUP2();
			//	window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			//	window.Left = workingArea.Left;
			//	window.Top = workingArea.Top;
			//	window.Width = workingArea.Width;
			//	window.Height = workingArea.Height;
			//	window.WindowState = WindowState.Maximized;
			//	window.Show();
			//}
			//else
			//{
			//	// 보조 모니터가 없는 경우 기본 모니터에 열기
			//	var window = new PopUp2();
			//	window.Show();
			//}

			// 타이머 시작
			_timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
			_timer.Tick += _timer_Tick;
			_timer.Start();
		}

		private void _timer_Tick(object sender, EventArgs e)
		{
			lbMainStatus.Content = _SysInfo2.strBcdPopupContent;

			if (_SysInfo2._PopupStatus == MAIN_STATUS.READY)
			{
				lbMainStatus.Background = Brushes.LightBlue;
			}
			else if (_SysInfo2._PopupStatus == MAIN_STATUS.NG)
			{
				lbMainStatus.Background = Brushes.LightPink;
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
		}
	}
}