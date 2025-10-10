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
	/// BarcodeRePrint2.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class BarcodeRePrint2 : Window
	{
		public BarcodeRePrint2()
		{
			InitializeComponent();
			//tbABCD1.Text = theApp._ModelInfo2.strComment1 + theApp._ModelInfo2.strComment2;
			//tbABCD2.Text = ":" + theApp._LotCount2.tProductClearTime.ToString("yyMMdd") /*+ theApp._ModelInfo.strBCDSymbol */+ (theApp._LotCount2.nLotCount /*+ theApp._ModelInfo.nStartLotNum*/).ToString("D4") + "Z";
		}

		private void btControl_Click(object sender, RoutedEventArgs e)
		{
			theApp._BarcodePrint2.nBCDsize = theApp._ModelInfo2.nBCDsize;
			theApp._BarcodePrint2.nBCDStringHeight = theApp._ModelInfo2.nBCDStringHeight;
			theApp._BarcodePrint2.nBCDStringWidth = theApp._ModelInfo2.nBCDStringWidth;
			theApp._BarcodePrint2.nBCDbcdOffsetX = theApp._ModelInfo2.nBCDbcdOffsetX;
			theApp._BarcodePrint2.nBCDbcdOffsetY = theApp._ModelInfo2.nBCDbcdOffsetY;
			theApp._BarcodePrint2.nBCDtextOffsetX = theApp._ModelInfo2.nBCDtextOffsetX;
			theApp._BarcodePrint2.nBCDtextOffsetY = theApp._ModelInfo2.nBCDtextOffsetY;
			theApp._BarcodePrint2.strModelInfo = tbABCD1.Text;
			theApp._BarcodePrint2.strPrintBCD = tbABCD2.Text;
			theApp._BarcodePrint2.bManualMode = false;
			theApp._BarcodePrint2.bPrintStart = true;
		}
	}
}
