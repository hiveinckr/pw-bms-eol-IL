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
	/// UserTitleMassage.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class UserTitleMassage : Window
	{
		DispatcherTimer _timer = new DispatcherTimer();
		public UserTitleMassage()
		{
			InitializeComponent();

			// 타이머 시작
			_timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
			_timer.Tick += _timer_Tick;
			_timer.Start();
		}

		private void _timer_Tick(object sender, EventArgs e)
		{
			lbMainStatus.Content = $"Station #1 Use a nutrunner to tighten the bolts.";

		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			
			Hide();
			e.Cancel = true;
		}
	}
}
