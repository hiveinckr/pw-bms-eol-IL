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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	/// <summary>
	/// UserControl1.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class UserControl1 : UserControl
	{
		public bool _bStat = false;


		public string strCate
		{

			set
			{
				lbCata.Content = value;
			}
		}


		public string strComment
		{
			set
			{
				lbComment.Content = value;
			}
		}


		public string strIndex
		{
			set
			{
				if (value.Substring(0, 1) == "Y") { lbIndex.Foreground = Brushes.Red; }
				if (value.Substring(0, 1) == "X") { lbIndex.Foreground = Brushes.Blue; }
				lbIndex.Content = value;
			}
		}

		public bool bStat
		{
			get
			{
				return _bStat;
			}
			set
			{
				_bStat = value;
				if (value)
				{
					imgIO.Source = new BitmapImage(new Uri("Resource/Untitled-3.png", UriKind.Relative));
				}
				else
				{
					imgIO.Source = new BitmapImage(new Uri("Resource/Untitled-1.png", UriKind.Relative));
				}
			}
		}

		public UserControl1()
		{
			InitializeComponent();
		}
	}
}

