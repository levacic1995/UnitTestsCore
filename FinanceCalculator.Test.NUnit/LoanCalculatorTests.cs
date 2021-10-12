using FinanceCalculator;
using FinanceCalculator.Exceptions;
using FinanceCalculator.Fina;
using FinanceCalculator.Settings;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace FinanceCalculator.Test.NUnit
{
	public class LoanCalculatorTests
	{
		LoanCalculator _calculator;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			IOptions<FinanceCalculatorOptions> options = Options.Create(new FinanceCalculatorOptions()
			{
				DefaultCurrencyCode = "HUF"
			});

			Mock<IFinaService> finaService = new Mock<IFinaService>();
			finaService.Setup(_ => _.GetCurrencyValue("HRK", "EUR")).Returns(7.52m);
			finaService.Setup(_ => _.GetCurrencyValue("HUF", "EUR")).Returns(359.50m);

			_calculator = new LoanCalculator(options, finaService.Object);
		}

		[SetUp]
		public void SetUp()
		{
			//Mock<IFinaService> finaService = new Mock<IFinaService>();
			//finaService.Setup(_ => _.GetCurrencyValue("HRK", "EUR")).Returns(7.52m);

			//_calculator = new LoanCalculator(finaService.Object);
		}

		[Test]
		public void GetSavingsValueTest()
		{
			decimal capital = 2000;
			decimal interestRate = 0.016m;
			int year = 3;

			decimal expected = 2096;
			decimal result = _calculator.GetSavingsValue(capital, interestRate, year);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void GetSavingsValueTest_NegativeYear()
		{
			decimal capital = 2000;
			decimal interestRate = 0.016m;
			int year = -2;

			Assert.Throws<NegativeYearException>(() => _calculator.GetSavingsValue(capital, interestRate, year));
		}

		[Test]
		public void GetValueInHRKTest()
		{
			string code = "EUR";
			decimal original = 3;

			decimal expected = 22.56m;
			decimal result = _calculator.GetValueInHRK(code, original);
			Assert.AreEqual(expected, result);
		}


		[Test]
		public void GetValueInDefaultCurrencyTest()
		{
			string code = "EUR";
			decimal original = 3;

			decimal expected = 1078.50m;
			decimal result = _calculator.GetValueInDefaultCurrency(code, original);
			Assert.AreEqual(expected, result);
		}
	}
}