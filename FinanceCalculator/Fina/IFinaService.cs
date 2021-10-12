using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceCalculator.Fina
{
	public interface IFinaService
	{
		bool IsConnected();

		decimal GetCurrencyValue(string original, string destination);
	}
}
