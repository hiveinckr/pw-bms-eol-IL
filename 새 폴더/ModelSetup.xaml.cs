using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	/// <summary>
	/// ModelSetup.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class ModelSetup : Window
	{
		DispatcherTimer _timer = new DispatcherTimer();
		MODEL_INFO _mInfo = new MODEL_INFO();


		public ModelSetup()
		{
			InitializeComponent();

			_mInfo = theApp._ModelInfo.CopyJsons<MODEL_INFO>();

			_Collection.Update(ref _mInfo);
			gdWorkInfo.ItemsSource = _Collection.workInfoView;
			gdWorkInfo_Copy.ItemsSource = _Collection.workOptionView;

			gdPoinInfo.ItemsSource = _mInfo._TestInfo;

			gdItemName.ItemsSource = _Collection.PsetNameList;
			gdItemName1.ItemsSource = _Collection.CanSetNameList;

			//gdPoinInfo.ItemsSource = _Collection.workPointView;
			//gdWorkInfo_Copy1.ItemsSource = _Collection.specInfoView;
			ShowETCInfo();
			// 타이머 시작
			_timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
			_timer.Tick += _timer_Tick;
			_timer.Start();
		}


		private void _timer_Tick(object sender, EventArgs e)
		{
			//DispMainLabel();


			if (_Config.strLanguage == "ENGLISH")
			{
				// 모델 설정 공통 부분
				btModelChnageContent.Content = "Change Model";
				btNewNameSaveContent.Content = "Save As New Name";
				lbModelInfo.Content = "Model Information";
				lbModelNameContent.Content = "Model Name";
				lbLot.Content = "Product Number";
				lbComment.Content = "Explanation";
				lbWorkInfo.Content = "Work Information";
				lbWorkOption.Content = "Work Options";
				btSaveCloseContent.Content = "Save And Close";
				btSaveContent.Content = "Save";
				btCloseContent.Content = "Cancle";
				//InspectionSet.Content = "Test Settings";



			}
			else
			{
				// 모델 설정 공통 부분
				btModelChnageContent.Content = "모델 변경";
				btNewNameSaveContent.Content = "새 이름으로 저장";
				lbModelInfo.Content = "모델 정보";
				lbModelNameContent.Content = "모델명";
				lbLot.Content = "품번";
				lbComment.Content = "설명";
				lbWorkInfo.Content = "작업 정보";
				lbWorkOption.Content = "작업 옵션";
				btSaveCloseContent.Content = "저장 및 닫기";
				btSaveContent.Content = "저  장";
				btCloseContent.Content = "취  소";
				//InspectionSet.Content = "검사 설정";
			}

			lbModelName.Text = _mInfo.strModelName;
			//DipsMotionPos();
		}


		// 기타 정보 표시
		private void ShowETCInfo()
		{
			tbComment1.Text = _mInfo.strComment1;
			tbComment2.Text = _mInfo.strComment2;
			tbComment3.Text = _mInfo.strComment3;
		}


		// 기타 정보 로드
		private void LoadETCInfo()
		{
			_mInfo.strComment1 = tbComment1.Text;
			_mInfo.strComment2 = tbComment2.Text;
			_mInfo.strComment3 = tbComment3.Text;
		}


		// 저장버튼 클릭시
		private void btSave_Click(object sender, RoutedEventArgs e)
		{
			// 그리드 데이터 읽기
			_Collection.Read(ref _mInfo);
			LoadETCInfo();

			// 데이터 저장
			theApp.SaveModelInfo(_mInfo, _mInfo.strModelName);
			theApp._ModelInfo = _mInfo.CopyJsons<MODEL_INFO>();
		}


		// 저장 & 닫기버튼 클릭시
		private void btSaveClose_Click(object sender, RoutedEventArgs e)
		{
			// 그리드 데이터 읽기
			_Collection.Read(ref _mInfo);
			LoadETCInfo();

			// 데이터 저장
			theApp._ModelInfo = _mInfo.CopyJsons<MODEL_INFO>();
			theApp.SaveModelInfo(_mInfo, _mInfo.strModelName);

			this.Close();
		}

		// 취소버튼 클릭시
		private void btClose_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}


		//  모델 변경 클릭시
		private void btModelChnage_Click(object sender, RoutedEventArgs e)
		{
			ModelList _window = new ModelList();
			if (_window.ShowDialog() == true)
			{
				_mInfo = theApp._ModelInfo.CopyJsons<MODEL_INFO>();

				_Collection.Update(ref _mInfo);
				ShowETCInfo();
				gdWorkInfo.ItemsSource = _Collection.workInfoView;
				gdWorkInfo_Copy.ItemsSource = _Collection.workOptionView;

				gdPoinInfo.ItemsSource = _mInfo._TestInfo;

				gdItemName.ItemsSource = _Collection.PsetNameList;
				gdItemName1.ItemsSource = _Collection.CanSetNameList;
			}
		}

		// 새이름으로 저장 클릭시
		private void btNewNameSave_Click(object sender, RoutedEventArgs e)
		{
			_Collection.Read(ref _mInfo);
			LoadETCInfo();

			NewNameSave _window = new NewNameSave(ref _mInfo);

			if (_window.ShowDialog() == true)
			{
				_mInfo = theApp._ModelInfo.CopyJsons<MODEL_INFO>();

				_Collection.Update(ref _mInfo);
				ShowETCInfo();
				gdWorkInfo.ItemsSource = _Collection.workInfoView;
				gdWorkInfo_Copy.ItemsSource = _Collection.workOptionView;

				gdPoinInfo.ItemsSource = _mInfo._TestInfo;

				gdItemName.ItemsSource = _Collection.PsetNameList;
				gdItemName1.ItemsSource = _Collection.CanSetNameList;
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			_Collection.Read(ref _mInfo);
			LoadETCInfo();

			if (JsonConvert.SerializeObject(theApp._ModelInfo) != JsonConvert.SerializeObject(_mInfo))
			{
				if (System.Windows.MessageBox.Show("변경된 내용이 있습니다. 저장하지 않고 닫으시겠습니까?", "모델설정", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
				{
					e.Cancel = true;
				}
			}
		}

		private void gdPoinInfo_LoadingRow(object sender, DataGridRowEventArgs e)
		{
			e.Row.Header = (e.Row.GetIndex() + 1).ToString();
		}

		private void btAddItems_Click(object sender, RoutedEventArgs e)
		{
			_mInfo._TestInfo.Add(new MODEL_SET());
		}

		private void btAddItems_Copy_Click(object sender, RoutedEventArgs e)
		{
			if (gdPoinInfo.SelectedIndex >= 0)
			{
				_mInfo._TestInfo[gdPoinInfo.SelectedIndex]._DataInfo.Add(new DATA_SET());
				//gdPoinInfo_Copy.Items.Add(new DATA_SET());
			}
		}

		private void gdPoinInfo_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			if (gdPoinInfo.SelectedIndex >= 0)
			{
				gdPoinInfo_Copy.ItemsSource = _mInfo._TestInfo[gdPoinInfo.SelectedIndex]._DataInfo;

				if (_mInfo._TestInfo[gdPoinInfo.SelectedIndex].nTestItem == 4)
				{
					gdItemName1.ItemsSource = _Collection.IONameList;
				}
				else if (_mInfo._TestInfo[gdPoinInfo.SelectedIndex].nTestItem == 0)
				{
					gdItemName1.ItemsSource = _Collection.CanSetNameList;
				}
			}
		}

		private void btAddItems_Copy1_Click(object sender, RoutedEventArgs e)
		{
			if (gdPoinInfo.SelectedIndex >= 0)
			{
				_mInfo._TestInfo.Insert(gdPoinInfo.SelectedIndex, new MODEL_SET());
			}
		}

		private void btAddItems_Copy2_Click(object sender, RoutedEventArgs e)
		{
			if (gdPoinInfo.SelectedIndex >= 0 && gdPoinInfo_Copy.SelectedIndex >= 0)
			{
				_mInfo._TestInfo[gdPoinInfo.SelectedIndex]._DataInfo.Insert(gdPoinInfo_Copy.SelectedIndex, new DATA_SET());
				//gdPoinInfo_Copy.Items.Add(new DATA_SET());
			}
		}

		private void btCopyData_Click(object sender, RoutedEventArgs e)
		{
			if (gdPoinInfo.SelectedIndex >= 0)
			{
				MODEL_SET copyData = new MODEL_SET();
				//copyData = _mInfo._TestInfo[gdPoinInfo.SelectedIndex].CopyJsons<MODEL_SET>();
				//_mInfo._TestInfo.Insert(gdPoinInfo.SelectedIndex, copyData);

				foreach (MODEL_SET data in gdPoinInfo.SelectedItems)
				{
					copyData = data.CopyJsons<MODEL_SET>();
					_mInfo._TestInfo.Insert(gdPoinInfo.SelectedIndex, copyData);
				}
				//gdPoinInfo_Copy.Items.Add(new DATA_SET());
			}
		}

		private void btDelete_Click(object sender, RoutedEventArgs e)
		{
			if (gdPoinInfo.SelectedIndex >= 0)
			{
				if (System.Windows.MessageBox.Show($"선택된 항목을 삭제하시겠습니까? (총 {gdPoinInfo.SelectedItems.Count}개)", "항목 삭제", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
				{

					for (int i = gdPoinInfo.SelectedItems.Count - 1; i >= 0; i--)
					{
						_mInfo._TestInfo.RemoveAt(gdPoinInfo.Items.IndexOf(gdPoinInfo.SelectedItems[i]));
					}

				}

			}
		}

		private void btDelete_Copy_Click(object sender, RoutedEventArgs e)
		{
			if (gdPoinInfo.SelectedIndex >= 0 && gdPoinInfo_Copy.SelectedIndex >= 0)
			{
				if (System.Windows.MessageBox.Show($"선택된 항목을 삭제하시겠습니까? (총 {gdPoinInfo_Copy.SelectedItems.Count}개)", "항목 삭제", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
				{

					for (int i = gdPoinInfo_Copy.SelectedItems.Count - 1; i >= 0; i--)
					{
						_mInfo._TestInfo[gdPoinInfo.SelectedIndex]._DataInfo.RemoveAt(gdPoinInfo_Copy.Items.IndexOf(gdPoinInfo_Copy.SelectedItems[i]));
					}

				}
				//_mInfo._TestInfo[gdPoinInfo.SelectedIndex]._DataInfo.RemoveAt(gdPoinInfo_Copy.SelectedIndex);
				//gdPoinInfo_Copy.Items.Add(new DATA_SET());
			}
		}

		private void btUpSch_Click(object sender, RoutedEventArgs e)
		{
			if (gdPoinInfo.SelectedIndex >= 0)
			{
				_mInfo._TestInfo.Move(gdPoinInfo.SelectedIndex, gdPoinInfo.SelectedIndex - 1);
				//_mInfo._TestInfo.RemoveAt(gdPoinInfo.SelectedIndex);
			}
		}

		private void btDownSch_Click(object sender, RoutedEventArgs e)
		{
			if (gdPoinInfo.SelectedIndex >= 0)
			{
				_mInfo._TestInfo.Move(gdPoinInfo.SelectedIndex, gdPoinInfo.SelectedIndex + 1);
				//_mInfo._TestInfo.RemoveAt(gdPoinInfo.SelectedIndex);
			}
		}

		private void btEolDataCopy_Click(object sender, RoutedEventArgs e)
		{
			if (gdPoinInfo_Copy.SelectedIndex >= 0 && gdPoinInfo_Copy.SelectedIndex >= 0)
			{
				DATA_SET copyData = new DATA_SET();

				foreach (DATA_SET data in gdPoinInfo_Copy.SelectedItems)
				{
					copyData = data.CopyJsons<DATA_SET>();
					_mInfo._TestInfo[gdPoinInfo.SelectedIndex]._DataInfo.Insert(gdPoinInfo_Copy.SelectedIndex, copyData);
				}
			}

		}

		private void btEolDataCopy_Copy_Click(object sender, RoutedEventArgs e)
		{
			if (gdPoinInfo_Copy.SelectedIndex >= 0 && gdPoinInfo_Copy.SelectedIndex >= 0)
			{
				DATA_SET copyData = new DATA_SET();

				foreach (DATA_SET data in gdPoinInfo_Copy.SelectedItems)
				{
					copyData = data.CopyJsons<DATA_SET>();
					int nAddr = 0;
					int.TryParse(data.strValue1, out nAddr);
					nAddr += 1;
					data.strValue1 = nAddr.ToString();
					_mInfo._TestInfo[gdPoinInfo.SelectedIndex]._DataInfo.Insert(gdPoinInfo_Copy.SelectedIndex, copyData);
				}
			}
		}
	}
}

