using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
	/// DebugMonitor.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class DebugMonitor : Window
	{
		public DebugMonitor()
		{
			InitializeComponent();
			gdLogView.ItemsSource = theApp.DebugMessage;

			((INotifyCollectionChanged)gdLogView.ItemsSource).CollectionChanged +=
			 (s, e) =>
			 {
				 if (e.Action ==
					 System.Collections.Specialized.NotifyCollectionChangedAction.Add)
				 {
					 gdLogView.ScrollIntoView(gdLogView.Items[gdLogView.Items.Count - 1]);
				 }
			 };
		}
	}
}
