using FinanceCalculator;
using FinanceCalculator.Exceptions;
using FinanceCalculator.Fina;
using FinanceCalculator.Models;
using FinanceCalculator.Settings;
using FinanceCalculator.Test.Common.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FinanceCalculator.Test.MSTest
{
	[TestClass]
	public class LoanCalculatorTests
	{
		static LoanCalculator _calculator;
		static IFinaService _finaService;

		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			IOptions<FinanceCalculatorOptions> options = Microsoft.Extensions.Options.Options.Create(new FinanceCalculatorOptions()
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

		[TestInitialize]
		public void Initialize()
		{
			//Mock<IFinaService> finaService = new Mock<IFinaService>();
			//finaService.Setup(_ => _.GetCurrencyValue("HRK", "EUR")).Returns(7.52m);

			//_calculator = new LoanCalculator(finaService.Object);


			//_testPerson = PersonObjectMother.GetPersonWithNormalSalary();
		}

		//[TestMethod]
		//[assembly: Parallelize(Workers = 2, Scope = ExecutionScope.MethodLevel)]
		//public void GetSavingsValueTest_Parallel()
		//{
		//	decimal capital = 2000;
		//	decimal interestRate = 0.016m;
		//	int year = 3;

		//	decimal expected = 2096;
		//	decimal result = _calculator.GetSavingsValue(capital, interestRate, year);

		//	Assert.AreEqual(expected, result);
		//}

		//[TestMethod]
		//[DataRow(0, 2000)] // primitive!
		//[DataRow(1, 2032)]
		//[DataRow(2, 2064)]
		//[DataRow(3, 2096)]
		//public void GetSavingsValueTest_Data(int year, int expected)
		//{
		//	decimal capital = 2000;
		//	decimal interestRate = 0.016m;

		//	decimal result = _calculator.GetSavingsValue(capital, interestRate, year);

		//	Assert.AreEqual((decimal)expected, result);
		//}

		//[DataTestMethod]
		//[DynamicData(nameof(GetSavingsData), DynamicDataSourceType.Method)]
		//public void GetSavingsValueTest_Data_Method(int year, decimal expected)
		//{
		//	decimal capital = 2000;
		//	decimal interestRate = 0.016m;

		//	decimal result = _calculator.GetSavingsValue(capital, interestRate, year);

		//	Assert.AreEqual(expected, result);
		//}

		//private static IEnumerable<object[]> GetSavingsData() // not primitive!
		//{
		//	yield return new object[] { 0, 2000m };
		//	yield return new object[] { 1, 2032m };
		//	yield return new object[] { 2, 2064m };
		//	yield return new object[] { 3, 2096m };
		//}

		[TestMethod]
		[TestCategory("Calculation")]
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

		[TestMethod]
		[TestCategory("Calculation")]
		[TestProperty("Exception", "NegativeYearException")]
		public void GetSavingsValueTest_NegativeYear()
		{
			decimal capital = 2000;
			decimal interestRate = 0.016m;
			int year = -2;

			Assert.ThrowsException<NegativeYearException>(() => _calculator.GetSavingsValue(capital, interestRate, year));
		}

		[TestMethod]
		[TestCategory("Currency")]
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

		[TestMethod]
		[TestCategory("Currency")]
		public void GetValueInDefaultCurrencyTest()
		{
			string code = "EUR";
			decimal original = 3;

			decimal expected = 1078.50m;
			decimal result = _calculator.GetValueInDefaultCurrency(code, original);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void IsPersonCreditableTest_Pass()
		{
			Person person = new PersonBuilder().WithIsEmployed(true)
											   .WithSalaries(new List<decimal>() { 1000m, 2000m, 3000m, 4000m, 5000m })
											   .Build();
			decimal fixDeduction = 1500m;
			decimal rate = 1000m;

			bool result = _calculator.IsPersonCreditable(person, fixDeduction, rate);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void IsPersonCreditableTest_Fail()
		{
			Person person = new PersonBuilder().WithIsEmployed(true)
											   .WithSalaries(new List<decimal>() { 1000m, 2000m, 3000m, 4000m, 5000m })
											   .Build();
			decimal fixDeduction = 3500m;
			decimal rate = 1000m;

			bool result = _calculator.IsPersonCreditable(person, fixDeduction, rate);

			Assert.IsFalse(result);
		}

		[TestMethod]
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
