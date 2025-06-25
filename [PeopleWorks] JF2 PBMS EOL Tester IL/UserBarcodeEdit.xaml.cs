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
	/// UserBarcodeEdit.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class UserBarcodeEdit : Window
	{
		public UserBarcodeEdit()
		{
			InitializeComponent();
			tbPassWord.Text = _SysInfo.strReadBarcode;
			tbPassWord.Focus();
			tbPassWord.SelectAll();
		}

		private void btOK_Click(object sender, RoutedEventArgs e)
		{
			SaveBarcodeInfo();
			this.Close();
		}

		private void btCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void tbPassWord_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				SaveBarcodeInfo();
				this.Close();
			}
		}


		private void SaveBarcodeInfo()
		{
			if (_SysInfo.strReadBarcode != tbPassWord.Text)
			{
				//_SysInfo.strReadBarcode = tbPassWord.Text;
				////_SysInfo.dtTestStartTime = DateTime.Now;
				////_SysInfo.strSaveFileName = _SysInfo.strReadBarcode + DateTime.Now.ToString("_HHmmss");

				//theApp._LotCount.nLotCount++;
				//theApp.SaveModelProductCount(theApp._LotCount, theApp._ModelInfo.strModelName);
				//uint.TryParse(theApp._LotCount.tProductClearTime.ToString("yyMMdd"), out _SysInfo.nWriteSerialNum);
				//_SysInfo.nWriteSerialNum *= 10000;
				//_SysInfo.nWriteSerialNum += theApp._LotCount.nLotCount;
			}
		}
	}
}

