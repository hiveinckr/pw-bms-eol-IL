using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	/// StepMonitorV2.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class StepMonitorV2 : Window
	{
		ObservableCollection<LogMessage> lstStepMonitor = new ObservableCollection<LogMessage>();
		DispatcherTimer _timer = new DispatcherTimer();

		public StepMonitorV2()
		{
			InitializeComponent();


			gdMotionInfo.ItemsSource = lstStepMonitor;

			// 타이머 시작
			_timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
			_timer.Tick += _timer_Tick;
			_timer.Start();
		}

		private void _timer_Tick(object sender, EventArgs e)
		{
			SetStep();

			lb1.Content = theApp._Nutrunner.nCurrentStep.ToString();
			lb2.Content = theApp._Nutrunner2.nCurrentStep.ToString();
		}

		public void SetStep()
		{
			lstStepMonitor.Clear();

			for (int i = 0; i < (int)PROC_LIST.MAX; i++)
			{
				lstStepMonitor.Add(new LogMessage() { strType = ((PROC_LIST)i).ToString(), strComment = theApp.nProcessStep[i].ToString() });
			}
		}
	}
}


