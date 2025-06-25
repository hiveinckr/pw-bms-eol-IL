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
	/// NewNameSave.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class NewNameSave : Window
	{
		MODEL_INFO _ReadInfo = new MODEL_INFO();
		public NewNameSave(ref MODEL_INFO _minfo)
		{
			InitializeComponent();
			_ReadInfo = _minfo.CopyJsons<MODEL_INFO>();
			tbModelName.Text = _ReadInfo.strModelName;
		}

		private void btOK_Click(object sender, RoutedEventArgs e)
		{
			_ReadInfo.strModelName = tbModelName.Text;
			theApp.SaveModelInfo(_ReadInfo, tbModelName.Text);
			theApp._ModelInfo = _ReadInfo.CopyJsons<MODEL_INFO>();

			theApp._LotCount.nLotCount = 0;
			theApp._LotCount.nOkCount = 0;
			theApp._LotCount.nNGCount = 0;
			theApp._LotCount.tProductClearTime = DateTime.Now;
			theApp.SaveModelProductCount(theApp._LotCount, theApp._ModelInfo.strModelName);
			this.DialogResult = true;
		}

		private void btCancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}
	}
}

