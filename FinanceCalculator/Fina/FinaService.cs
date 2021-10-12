using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceCalculator.Fina
{
	public class FinaService : IFinaService
	{
		Random r = new Random();

		public bool IsConnected()
		{
			return true;
		}

		public decimal GetCurrencyValue(string original, string destination)
		{
			return (decimal)r.NextDouble();
		}
	}
}
