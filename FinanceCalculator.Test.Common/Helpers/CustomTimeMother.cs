using FinanceCalculator.Time;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceCalculator.Test.Common.Helpers
{
	public class CustomTimeMother
	{
		public static CustomTime GetTime()
		{
			return new CustomTime
			{
				Day = 27,
				Month = 10,
				Year = 2010
			};
		}
	}
}
