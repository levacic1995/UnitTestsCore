using FinanceCalculator;
using FinanceCalculator.Exceptions;
using System;
using Xunit;

namespace FinanceCalculator.Test.xUnit
{
	public class LoanCalculatorTest
	{
		LoanCalculator _calculator = new LoanCalculator(null, null);

		[Fact]
		public void GetSavingsValueTest()
		{
			decimal capital = 2000;
			decimal interestRate = 0.016m;
			int year = 3;

			decimal expected = 2096;
			decimal result = _calculator.GetSavingsValue(capital, interestRate, year);

			Assert.Equal(expected, result);
		}

		[Fact]
		public void GetSavingsValueTest_NegativeYear()
		{
			decimal capital = 2000;
			decimal interestRate = 0.016m;
			int year = -2;

			Assert.Throws<NegativeYearException>(() => _calculator.GetSavingsValue(capital, interestRate, year));
		}
	}
}
