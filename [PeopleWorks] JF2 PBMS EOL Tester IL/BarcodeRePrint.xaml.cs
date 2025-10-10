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
	/// BarcodeRePrint.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class BarcodeRePrint : Window
	{
		public BarcodeRePrint()
		{
			InitializeComponent();
			//tbABCD1.Text = theApp._ModelInfo.strComment1 + theApp._ModelInfo.strComment2;
			//tbABCD2.Text = ":"+theApp._LotCount.tProductClearTime.ToString("yyMMdd") /*+ theApp._ModelInfo.strBCDSymbol */+ (theApp._LotCount.nLotCount /*+ theApp._ModelInfo.nStartLotNum*/).ToString("D4") + "Z";
		}

		private void btControl_Click(object sender, RoutedEventArgs e)
		{
			theApp._BarcodePrint.nBCDsize = theApp._ModelInfo.nBCDsize;
			theApp._BarcodePrint.nBCDStringHeight = theApp._ModelInfo.nBCDStringHeight;
			theApp._BarcodePrint.nBCDStringWidth = theApp._ModelInfo.nBCDStringWidth;
			theApp._BarcodePrint.nBCDbcdOffsetX = theApp._ModelInfo.nBCDbcdOffsetX;
			theApp._BarcodePrint.nBCDbcdOffsetY = theApp._ModelInfo.nBCDbcdOffsetY;
			theApp._BarcodePrint.nBCDtextOffsetX = theApp._ModelInfo.nBCDtextOffsetX;
			theApp._BarcodePrint.nBCDtextOffsetY = theApp._ModelInfo.nBCDtextOffsetY;
			theApp._BarcodePrint.strModelInfo = tbABCD1.Text;
			theApp._BarcodePrint.strPrintBCD = tbABCD2.Text;
			theApp._BarcodePrint.bManualMode = false;
			theApp._BarcodePrint.bPrintStart = true;
		}
	}
}
