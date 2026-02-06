using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
	/// ModelList2.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class ModelList2 : Window
	{
		ObservableCollection<ModelListView> lstModel = new ObservableCollection<ModelListView>();
		public ModelList2()
		{
			InitializeComponent();
			gdCMABCD.ItemsSource = lstModel;


			string strFolderPath = String.Format("MODEL2\\");
			DirectoryInfo dir = new DirectoryInfo(strFolderPath);
			if (dir.Exists == false) { dir.Create(); }

			string[] files = Directory.GetFiles(String.Format("MODEL2\\"), "*.rcp", SearchOption.AllDirectories);

			foreach (string s in files)
			{

				FileInfo fileInfo = new FileInfo(s);

				//lstModel.Add(new MyResultData() { strBCD = fileInfo.FullName.Substring(fileInfo.FullName.LastIndexOf("MODEL\\") + 6, (fileInfo.FullName.LastIndexOf(".") - fileInfo.FullName.LastIndexOf("MODEL\\") - 6)) });

				lstModel.Add(new ModelListView() { strBCD = System.IO.Path.GetFileNameWithoutExtension(fileInfo.Name) });
			}
		}


		private void btCancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}

		private void btOK_Click(object sender, RoutedEventArgs e)
		{
			if (gdCMABCD.SelectedIndex >= 0)
			{
				theApp.LoadModelInfo2(ref theApp._ModelInfo2, lstModel[gdCMABCD.SelectedIndex].strBCD);
				theApp.LoadModelProductCount2(ref theApp._LotCount2, lstModel[gdCMABCD.SelectedIndex].strBCD);
				theApp.nProcessStep[(int)PROC_LIST.MAIN2] = 0;
				this.DialogResult = true;
			}
		}

		private void gdCMABCD_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (gdCMABCD.SelectedIndex >= 0)
			{
				theApp.LoadModelInfo2(ref theApp._ModelInfo2, lstModel[gdCMABCD.SelectedIndex].strBCD);
				theApp.LoadModelProductCount2(ref theApp._LotCount2, lstModel[gdCMABCD.SelectedIndex].strBCD);
				theApp.nProcessStep[(int)PROC_LIST.MAIN2] = 0;
				this.DialogResult = true;
			}
		}



		// 결과 데이터 정보
		public class ModelListView
		{
			public DateTime tNowTime { get; set; }
			public string strBCD { get; set; }
			public string strResult { get; set; }
		}
	}
}


