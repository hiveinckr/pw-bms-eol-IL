using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _PeopleWorks__JF2_PBMS_EOL_Tester_IL
{
	public class HIVE_Timer
	{
		private int tMesc = new int();
		private Stopwatch tTimer = new Stopwatch();

		public HIVE_Timer()
		{
			tMesc = 0;
			tTimer.Restart();
		}

		public void Start(int nIndex)
		{
			tMesc = nIndex;
			tTimer.Restart();
		}

		public bool Verify()
		{
			if (tTimer.ElapsedMilliseconds > tMesc)
			{
				tTimer.Stop();
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool Check()
		{
			if (tTimer.ElapsedMilliseconds > tMesc)
			{
				tTimer.Stop();
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
