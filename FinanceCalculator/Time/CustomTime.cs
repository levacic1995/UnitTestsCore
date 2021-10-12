using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceCalculator.Time
{
	public class CustomTime : ITime
	{
		public int Day { get; set; }

		public int Month { get; set; }

		public int Year { get; set; }

		public void IncreaseDay(int num)
		{
			int amount = Day + num;
			int carryOver = amount / 30;
			Day = amount % 30;

			IncreaseMonth(carryOver);
		}

		public void IncreaseMonth(int num)
		{
			int amount = Month + num;
			int carryOver = amount / 12;
			Month = amount % 12;

			IncreaseYear(carryOver);
		}

		public void IncreaseYear(int num)
		{
			Year += num;
		}
	}
}
