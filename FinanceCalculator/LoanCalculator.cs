using System;
using FinanceCalculator.Exceptions;
using FinanceCalculator.Fina;
using FinanceCalculator.Models;
using FinanceCalculator.Settings;
using Microsoft.Extensions.Options;

namespace FinanceCalculator
{
	public class LoanCalculator
	{
		private readonly IOptions<FinanceCalculatorOptions> _options;
		private readonly IFinaService _finaService;

		public LoanCalculator(IOptions<FinanceCalculatorOptions> options, IFinaService finaService)
		{
			_options = options;
			_finaService = finaService;
		}

		public decimal GetSavingsValue(decimal capital, decimal interestRate, int year)
		{
			if(year < 0)
			{
				throw new NegativeYearException();
			}

			return capital + (capital * interestRate * year);
		}

		public decimal GetValueInHRK(string currency, decimal value)
		{
			decimal exchangeRate = _finaService.GetCurrencyValue("HRK", currency);
			return value * exchangeRate;
		}

		public decimal GetValueInDefaultCurrency(string currency, decimal value)
		{
			decimal exchangeRate = _finaService.GetCurrencyValue(_options.Value.DefaultCurrencyCode, currency);
			return value * exchangeRate;
		}

		public bool IsPersonCreditable(Person person, decimal fixDeduction, decimal rate)
		{
			if(!person.IsEmployed)
			{
				return false;
			}

			decimal value = (person.GetAverageSalary() - fixDeduction) / rate;
			return value >= 1;
		}
	}
}
