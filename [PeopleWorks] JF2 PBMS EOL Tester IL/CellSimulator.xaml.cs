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
	/// CellSimulator.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class CellSimulator : Window
	{
		DispatcherTimer _timer = new DispatcherTimer();
		public CellSimulator()
		{
			InitializeComponent();
			_timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
			_timer.Tick += _timer_Tick;
			_timer.Start();
		}

		private void _timer_Tick(object sender, EventArgs e)
		{


		}

		private void btC_S1_ON_Click(object sender, RoutedEventArgs e)
		{
			AdminPass _pwWindow = new AdminPass();

			if (_pwWindow.ShowDialog() == true)
			{
				theApp.nProcessStep[(int)PROC_LIST.MANUAL] = 1000;
			}

			else
			{
				System.Windows.MessageBox.Show("비밀번호가 일치하지 않습니다.");
			}
		}


		private void btC_S2_ON_Click(object sender, RoutedEventArgs e)
		{
			AdminPass _pwWindow = new AdminPass();

			if (_pwWindow.ShowDialog() == true)
			{
				theApp.nProcessStep[(int)PROC_LIST.MANUAL] = 2000;
			}

			else
			{
				System.Windows.MessageBox.Show("비밀번호가 일치하지 않습니다.");
			}

		}

		private void btC_S1_OFF_Click(object sender, RoutedEventArgs e)
		{
			theApp.nProcessStep[(int)PROC_LIST.MANUAL] = 1050;
		}

		private void btC_S2_OFF_Click(object sender, RoutedEventArgs e)
		{
			theApp.nProcessStep[(int)PROC_LIST.MANUAL] = 2050;
		}

		private void btC_S1_Read_Click(object sender, RoutedEventArgs e)
		{
			theApp.nProcessStep[(int)PROC_LIST.MANUAL] = 3000;
		}

		private void btC_S2_Read_Click(object sender, RoutedEventArgs e)
		{
			theApp.nProcessStep[(int)PROC_LIST.MANUAL] = 4000;
		}
	}
}
