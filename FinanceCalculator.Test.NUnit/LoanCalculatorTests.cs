using FinanceCalculator;
using FinanceCalculator.Exceptions;
using FinanceCalculator.Fina;
using FinanceCalculator.Models;
using FinanceCalculator.Settings;
using FinanceCalculator.Test.Common.Helpers;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace FinanceCalculator.Test.NUnit
{
	[TestFixture]
	public class LoanCalculatorTests
	{
		static LoanCalculator _calculator;
		static IFinaService _finaService;

		[OneTimeSetUp]
		public static void OneTimeSetUp()
		{
			IOptions<FinanceCalculatorOptions> options = Options.Create(new FinanceCalculatorOptions()
			{
				DefaultCurrencyCode = "HUF"
			});

			Mock<IFinaService> finaService = new Mock<IFinaService>();
			finaService.Setup(_ => _.IsConnected()).Returns(false);
			finaService.Setup(_ => _.GetCurrencyValue("HRK", "EUR")).Returns(7.52m);
			finaService.Setup(_ => _.GetCurrencyValue("HUF", "EUR")).Returns(359.50m);

			_finaService = finaService.Object;
			_calculator = new LoanCalculator(options, _finaService);
		}

		[SetUp]
		public void SetUp()
		{
			//Mock<IFinaService> finaService = new Mock<IFinaService>();
			//finaService.Setup(_ => _.GetCurrencyValue("HRK", "EUR")).Returns(7.52m);

			//_calculator = new LoanCalculator(finaService.Object);

			//_testPerson = PersonObjectMother.GetPersonWithNormalSalary();
		}

		//[Test]
		//[TestCase(0, ExpectedResult = 2000)] // primitive!
		//[TestCase(1, ExpectedResult = 2032)]
		//[TestCase(2, ExpectedResult = 2064)]
		//[TestCase(3, ExpectedResult = 2096)]
		//public decimal GetSavingsValueTest_Data(int year)
		//{
		//	decimal capital = 2000;
		//	decimal interestRate = 0.016m;

		//	decimal result = _calculator.GetSavingsValue(capital, interestRate, year);

		//	return result;
		//}

		//[TestCaseSource("SavingsData")]
		//public decimal GetSavingsValueTest_Data_Method(int year)
		//{
		//	decimal capital = 2000;
		//	decimal interestRate = 0.016m;

		//	decimal result = _calculator.GetSavingsValue(capital, interestRate, year);

		//	return result;
		//}

		//private static IEnumerable<TestCaseData> SavingsData() // not primitive!
		//{
		//	return new List<TestCaseData>()
		//	{
		//		new TestCaseData(0).Returns(2000m),
		//		new TestCaseData(1).Returns(2032m),
		//		new TestCaseData(2).Returns(2064m),
		//		new TestCaseData(3).Returns(2096m)
		//	};
		//}

		[Test]
		[Category("Calculation")]
		//[Ignore("Needs fixing!")]
		public void GetSavingsValueTest()
		{
			//Console.WriteLine($"Test start! {DateTime.Now}");

			decimal capital = 2000;
			decimal interestRate = 0.016m;
			int year = 3;

			decimal expected = 2096;
			decimal result = _calculator.GetSavingsValue(capital, interestRate, year);

			Assert.AreEqual(expected, result);

			//Thread.Sleep(3000);
			//Console.WriteLine($"Test end! {DateTime.Now}");
		}

		[Test]
		[Category("Calculation")]
		[Property("Exception", "NegativeYearException")]
		public void GetSavingsValueTest_NegativeYear()
		{
			decimal capital = 2000;
			decimal interestRate = 0.016m;
			int year = -2;

			Assert.Throws<NegativeYearException>(() => _calculator.GetSavingsValue(capital, interestRate, year));
		}

		[Test]
		[Category("Currency")]
		public void GetValueInHRKTest()
		{
			string code = "EUR";
			decimal original = 3;
			decimal expected = 22.56m;

			//if(!_finaService.IsConnected())
			//{
			//	Assert.Inconclusive("Can't connect to service!");
			//}
			decimal result = _calculator.GetValueInHRK(code, original);

			Assert.AreEqual(expected, result);
		}

		[Test]
		[Category("Currency")]
		public void GetValueInDefaultCurrencyTest()
		{
			string code = "EUR";
			decimal original = 3;

			decimal expected = 1078.50m;
			decimal result = _calculator.GetValueInDefaultCurrency(code, original);
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void IsPersonCreditableTest()
		{
			Person person = new PersonBuilder().WithIsEmployed(true)
											   .WithSalaries(new List<decimal>() { 1000m, 2000m, 3000m, 4000m, 5000m })
											   .Build();
			decimal fixDeduction = 1500m;
			decimal rate = 1000m;

			bool result = _calculator.IsPersonCreditable(person, fixDeduction, rate);

			Assert.IsTrue(result);
		}

		[Test]
		public void IsPersonCreditableTest_NotCreditable()
		{
			Person person = new PersonBuilder().WithIsEmployed(true)
											   .WithSalaries(new List<decimal>() { 1000m, 2000m, 3000m, 4000m, 5000m })
											   .Build();
			decimal fixDeduction = 3500m;
			decimal rate = 1000m;

			bool result = _calculator.IsPersonCreditable(person, fixDeduction, rate);

			Assert.IsFalse(result);
		}

		[Test]
		public void IsPersonCreditableTest_Unemployed()
		{
			Person person = new PersonBuilder().WithIsEmployed(false)
											   .Build();
			decimal fixDeduction = 1500m;
			decimal rate = 1000m;

			bool result = _calculator.IsPersonCreditable(person, fixDeduction, rate);

			Assert.IsFalse(result);
		}
	}
}