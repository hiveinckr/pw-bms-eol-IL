using Newtonsoft.Json;
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

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	/// <summary>
	/// UserStartMassage.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class UserStartMassage : Window
	{
		public UserStartMassage()
		{
			InitializeComponent();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			_SysInfo.bEMGStop = true;
			_SysInfo.bReadMainBcd = false;
			_SysInfo.bReadMacBcd = false;
			theApp.nProcessStep[(int)PROC_LIST.MAIN] = 80000;
			e.Cancel = true;
		}
	}
}
