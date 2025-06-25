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
	public partial class NewNameSave2 : Window
	{
		MODEL_INFO2 _ReadInfo = new MODEL_INFO2();
		public NewNameSave2(ref MODEL_INFO2 _minfo)
		{
			InitializeComponent();
			_ReadInfo = _minfo.CopyJsons<MODEL_INFO2>();
			tbModelName.Text = _ReadInfo.strModelName;
		}

		private void btOK_Click(object sender, RoutedEventArgs e)
		{
			_ReadInfo.strModelName = tbModelName.Text;
			theApp.SaveModelInfo2(_ReadInfo, tbModelName.Text);
			theApp._ModelInfo2 = _ReadInfo.CopyJsons<MODEL_INFO2>();

			theApp._LotCount2.nLotCount = 0;
			theApp._LotCount2.nOkCount = 0;
			theApp._LotCount2.nNGCount = 0;
			theApp._LotCount2.tProductClearTime = DateTime.Now;
			theApp.SaveModelProductCount2(theApp._LotCount2, theApp._ModelInfo2.strModelName);
			this.DialogResult = true;
		}

		private void btCancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}
	}
}

