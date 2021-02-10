using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogniMnemo
{
	public static class EbbinghausCurve
	{
		private static readonly Dictionary<byte, TimeSpan> _ebbinghausCurve = new Dictionary<byte, TimeSpan>() { { 0, new TimeSpan(0,0,5)}
																											, { 1, new TimeSpan(0,0,25) }
																											, { 2, new TimeSpan(0,2,0) }
																											, { 3, new TimeSpan(0,10,0) }
																											, { 4, new TimeSpan(1,0,0) }
																											, { 5, new TimeSpan(5,0,0) }
																											, { 6, new TimeSpan(1,0,0,0) }
																											, { 7, new TimeSpan(5,0,0,0) }
																											, { 8, new TimeSpan(30,0,0,0) }
																											, { 9, new TimeSpan(150,0,0,0) }
																											, { 10, new TimeSpan(720,0,0,0) }
																											, { 11, new TimeSpan(3600,0,0,0) }
																											, { 12, new TimeSpan(21600,0,0,0) }
		};
		//todo:maybe need instead of dateoflastrecall just a datetime.now?
		public static DateTime GetTimeRecallByForgettingCurve(DateTime dateOfLastRecall, byte level, char plusorminus)
		{
			return plusorminus=='+' ? dateOfLastRecall += _ebbinghausCurve[level]: level!=0?dateOfLastRecall += _ebbinghausCurve[level--]: dateOfLastRecall += _ebbinghausCurve[0];
		}
	}
}
