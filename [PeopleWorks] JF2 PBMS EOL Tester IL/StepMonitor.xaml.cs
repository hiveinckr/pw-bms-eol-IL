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
	/// StepMonitor.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class StepMonitor : Window
	{
		DispatcherTimer _timer = new DispatcherTimer();

		public StepMonitor()
		{
			InitializeComponent();

			_timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
			_timer.Tick += _timer_Tick;
			_timer.Start();
		}

		private void _timer_Tick(object sender, EventArgs e)
		{
			lb1.Content = theApp.nProcessStep[(int)PROC_LIST.MAIN].ToString();
			lb2.Content = theApp.nProcessStep[(int)PROC_LIST.MANUAL].ToString();
			lb3.Content = theApp.nProcessStep[(int)PROC_LIST.SUB_EOL].ToString();
		}
	}
}
