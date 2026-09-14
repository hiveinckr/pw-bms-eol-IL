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
			lb3.Content = theApp._LoadCell.nCurrentStep.ToString();
			lb4.Content = theApp._LoadCell2.nCurrentStep.ToString();
		}

		public void SetStep()
		{
			lstStepMonitor.Clear();

			for (int i = 0; i < (int)PROC_LIST.MAX; i++)
			{
				lstStepMonitor.Add(new LogMessage() { strType = ((PROC_LIST)i).ToString(), strComment = theApp.nProcessStep[i].ToString() });
			}
		}

		private void btModelSetup_Click(object sender, RoutedEventArgs e)
		{
			MakerPass _pwWindow = new MakerPass();

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

		private void btModelSetup2_Click(object sender, RoutedEventArgs e)
		{
			MakerPass2 _pwWindow = new MakerPass2();

			if (_pwWindow.ShowDialog() == true)
			{
				ModelSetup2 _Window = new ModelSetup2();
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
	}
}


