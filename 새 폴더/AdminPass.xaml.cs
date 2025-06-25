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
	/// AdminPass.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class AdminPass : Window
	{
		public AdminPass()
		{
			InitializeComponent();

			tbPassWord.Focus();
		}


		// 취소 버튼 클릭시
		private void btCancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}



		// OK 버튼 클릭시
		private void btOK_Click(object sender, RoutedEventArgs e)
		{
			// 패스워드 일치시
			if (_Config.strAdminPass == tbPassWord.Password || _Config.strMasterPass == tbPassWord.Password)
			{
				this.DialogResult = true;
			}
			else
			{
				this.DialogResult = false;
			}
		}

		// 키다운 이벤트
		private void tbPassWord_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				if (_Config.strAdminPass == tbPassWord.Password || _Config.strMasterPass == tbPassWord.Password)
				{
					this.DialogResult = true;
				}
				else
				{
					this.DialogResult = false;
				}
			}
		}
	}
}



