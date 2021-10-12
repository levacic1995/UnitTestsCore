using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceCalculator.Time
{
	public interface ITime
	{
		void IncreaseDay(int num);

		void IncreaseMonth(int num);

		void IncreaseYear(int num);
	}
}
