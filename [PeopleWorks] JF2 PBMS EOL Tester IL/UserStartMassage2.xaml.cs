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

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	/// <summary>
	/// UserStartMassage2.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class UserStartMassage2 : Window
	{
		public UserStartMassage2()
		{
			InitializeComponent();
			//var screens = Screen.AllScreens;
			
			//if (screens.Length > 1)
			//{
			//	var secondaryScreen = screens.FirstOrDefault(s => !s.Primary) ?? screens[2];
			//	var workingArea = secondaryScreen.WorkingArea;

			//	var window = new UserStartMassage2();
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
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			_SysInfo2.bEMGStop = true;
			_SysInfo2.bReadMainBcd = false;
			_SysInfo2.bReadMacBcd = false;
			theApp.nProcessStep[(int)PROC_LIST.MAIN2] = 80000;
			e.Cancel = true;
		}
	}
}
