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
	/// PopUp.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class PopUp : Window
	{
		DispatcherTimer _timer = new DispatcherTimer();

		public PopUp()
		{
			InitializeComponent();


			// 타이머 시작
			_timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
			_timer.Tick += _timer_Tick;
			_timer.Start();
		}

		private void _timer_Tick(object sender, EventArgs e)
		{
			lbMainStatus.Content = _SysInfo.strPopupContent;

			if (_SysInfo._PopupStatus == MAIN_STATUS.READY)
			{
				lbMainStatus.Background = Brushes.LightBlue;
			}
			else if (_SysInfo._PopupStatus == MAIN_STATUS.NG)
			{
				lbMainStatus.Background = Brushes.LightPink;
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
		}

		private void btNext_Click(object sender, RoutedEventArgs e)
		{
			_SysInfo._SwStatus = MAIN_STATUS.OK;
			this.Hide();
		}

		private void btStop_Click(object sender, RoutedEventArgs e)
		{
			_SysInfo._SwStatus = MAIN_STATUS.NG;
			this.Hide();
		}
	}
}


