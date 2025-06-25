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
	/// DIOMonitor.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class DIOMonitor : Window
	{
		DispatcherTimer _timer = new DispatcherTimer();

		UserControl1[] _Uc;
		public DIOMonitor()
		{
			InitializeComponent();
			_Uc = new UserControl1[] { uc1, uc2, uc3, uc4, uc5, uc6, uc7, uc8, uc9, uc10,
									   uc11, uc12, uc13, uc14, uc15, uc16, uc17, uc18, uc19, uc20,
									   uc21, uc22, uc23, uc24, uc25, uc26, uc27, uc28, uc29, uc30,
									   uc31, uc32, uc33, uc34, uc35, uc36, uc37, uc38, uc39, uc40,
									   uc41, uc42, uc43, uc44, uc45, uc46, uc47, uc48, uc49, uc50,
									   uc51, uc52, uc53, uc54, uc55, uc56, uc57, uc58, uc59, uc60,
									   uc61, uc62, uc63, uc64};

			//for (int i = 0; i < 16; i++)
			//{
			//	tabX.Items.Add($"X{i * 32:X3} ~ X{((i + 1) * 32) - 1:X3}");
			//	tabY.Items.Add($"Y{i * 32:X3} ~ Y{((i + 1) * 32) - 1:X3}");

			//}

			_timer.Interval = TimeSpan.FromMilliseconds(10.0);
			_timer.Tick += _timer_Tick;
			_timer.Start();
		}

		private void _timer_Tick(object sender, EventArgs e)
		{

			for (int i = 0; i < 64; i++)
			{
				if (i < 32)
				{
					_Uc[i].bStat = theApp.GetDIOPort((DI)i + (tabX.SelectedIndex * 32));
					_Uc[i].strIndex = String.Format("X{0:X3}", i + (tabX.SelectedIndex * 32));
					_Uc[i].strCate = String.Format("{0}", _Define.DITable[0].Rows[i + (tabX.SelectedIndex * 32)]["Cate"]);
					_Uc[i].strComment = String.Format("{0}", _Define.DITable[0].Rows[i + (tabX.SelectedIndex * 32)]["Comment"]);


				}
				else
				{
					_Uc[i].bStat = theApp.GetDIOPortStat((DO)i - 32 + (tabY.SelectedIndex * 32));
					_Uc[i].strIndex = String.Format("Y{0:X3}", i - 32 + (tabY.SelectedIndex * 32));
					_Uc[i].strCate = String.Format("{0}", _Define.DOTable[0].Rows[i - 32 + (tabY.SelectedIndex * 32)]["Cate"]);
					_Uc[i].strComment = String.Format("{0}", _Define.DOTable[0].Rows[i - 32 + (tabY.SelectedIndex * 32)]["Comment"]);
				}

			}
		}

		private void y_MouseDown(object sender, MouseButtonEventArgs e)
		{
			theApp.SetDIOPort((DO)(int.Parse(((UserControl1)sender).Name.Substring(2, 2)) - 33 + (tabY.SelectedIndex * 32)), !((UserControl1)sender).bStat);
		}
	}
}

