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
	/// SystemSetting.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class SystemSetting : Window
	{
		public SystemSetting()
		{
			InitializeComponent();

			_Collection.UpdateConfig();

			gdWorkInfo.ItemsSource = _Collection.ConfigWorkOptionView;

			cbLanguageCheck.Items.Add("KOREA");
			cbLanguageCheck.Items.Add("ENGLISH");
			cbLanguageCheck.SelectedItem = _Config.strLanguage;

			if (_Config.strLanguage == "ENGLISH")
			{
				Title = "System Setting";

				lbWorkInfo.Content = "Job Information";
				lbEtcInfo.Content = "Job Settings";


				btSaveCloseContent.Content = " Save and Close ";
				btSaveContent.Content = " Save ";
				btCloseContent.Content = " Cancle ";

			}
			else
			{
				Title = "시스템 설정";

				lbWorkInfo.Content = "작업 정보";
				lbEtcInfo.Content = "기타 정보";


				btSaveCloseContent.Content = " 저장 및 닫기 ";
				btSaveContent.Content = " 저  장 ";
				btCloseContent.Content = " 취  소 ";
			}
		}

		private void btSave_Click(object sender, RoutedEventArgs e)
		{
			_Collection.ReadConfig();
			_Config.strLanguage = cbLanguageCheck.SelectedValue.ToString();
			theApp.SaveIniFile();

			if (_Config.strLanguage == "ENGLISH")
			{
				Title = "System Setting";

				lbWorkInfo.Content = "Job Information";
				lbEtcInfo.Content = "Job Settings";


				btSaveCloseContent.Content = " Save and Close ";
				btSaveContent.Content = " Save ";
				btCloseContent.Content = " Cancle ";

			}
			else
			{
				Title = "시스템 설정";

				lbWorkInfo.Content = "작업 정보";
				lbEtcInfo.Content = "기타 정보";


				btSaveCloseContent.Content = " 저장 및 닫기 ";
				btSaveContent.Content = " 저  장 ";
				btCloseContent.Content = " 취  소 ";
			}
		}

		private void btSaveClose_Click(object sender, RoutedEventArgs e)
		{
			_Collection.ReadConfig();
			_Config.strLanguage = cbLanguageCheck.SelectedValue.ToString();
			theApp.SaveIniFile();


			this.Close();
		}


		private void btClose_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}


