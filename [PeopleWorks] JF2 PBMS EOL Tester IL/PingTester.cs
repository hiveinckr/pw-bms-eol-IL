using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	internal class PingTester
	{
		AutoResetEvent waiter = new AutoResetEvent(false);
		Ping pingSender = new Ping();
		public PING_STATUS _Status = new PING_STATUS();

		public void InitPingTester()
		{
			pingSender.PingCompleted += new PingCompletedEventHandler(PingCompletedCallback);
		}

		public void SendPingData(string strIP)
		{
			string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
			byte[] buffer = Encoding.ASCII.GetBytes(data);
			int timeout = 3000;
			PingOptions options = new PingOptions(64, true);
			_Status = PING_STATUS.ING;
			pingSender.SendAsync(strIP, timeout, buffer, options, waiter);
		}

		private void PingCompletedCallback(object sender, PingCompletedEventArgs e)
		{
			// 만약 명령이 취소되면 메세지가 표시된다.
			if (e.Cancelled)
			{
				_Status = PING_STATUS.NG;
			}

			if (e.Error != null)
			{
				_Status = PING_STATUS.NG;
			}



			if (e.Reply == null)
			{
				_Status = PING_STATUS.NG;
				return;
			}


			if (e.Reply.Status == IPStatus.Success)
			{
				_Status = PING_STATUS.OK;
			}
			else
			{
				_Status = PING_STATUS.NG;
				theApp.AppendLogMsg(e.Reply.Status.ToString(), MSG_TYPE.INFO);
			}

		}
	}
}

