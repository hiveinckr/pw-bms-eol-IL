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
	/// PbmsStartMassege2.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class PbmsStartMassege2 : Window
	{
		public PbmsStartMassege2()
		{
			InitializeComponent();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{

			Hide();
			e.Cancel = true;
		}
	}
}
