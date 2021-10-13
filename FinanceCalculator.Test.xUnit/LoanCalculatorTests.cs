using FinanceCalculator;
using FinanceCalculator.Exceptions;
using FinanceCalculator.Models;
using FinanceCalculator.Test.Common.Helpers;
using FinanceCalculator.Test.xUnit.Fixtures;
using System;
using System.Collections.Generic;
using Xunit;

namespace FinanceCalculator.Test.xUnit
{
	public class LoanCalculatorTest : IClassFixture<LoanCalculatorFixture>
	{
		LoanCalculatorFixture _fixture;

		// setup, before every test
		public LoanCalculatorTest(LoanCalculatorFixture fixture)
		{
			_fixture = fixture;
		}

		//[InlineData(0, 2000)] // primitive!
		//[InlineData(1, 2032)]
		//[InlineData(2, 2064)]
		//[InlineData(3, 2096)]
		//[Theory]
		//public void GetSavingsValueTest_Data(int year, int expected)
		//{
		//	decimal capital = 2000;
		//	decimal interestRate = 0.016m;

		//	decimal result = _fixture.Calculator.GetSavingsValue(capital, interestRate, year);

		//	Assert.Equal((decimal)expected, result);
		//}

		//[Theory]
		//[MemberData(nameof(GetSavingsData))]
		//public void GetSavingsValueTest_Data_Method(int year, decimal expected)
		//{
		//	decimal capital = 2000;
		//	decimal interestRate = 0.016m;

		//	decimal result = _fixture.Calculator.GetSavingsValue(capital, interestRate, year);

		//	Assert.Equal(expected, result);
		//}

		//public static IEnumerable<object[]> GetSavingsData() // not primitive!
		//{
		//	yield return new object[] { 0, 2000m };
		//	yield return new object[] { 1, 2032m };
		//	yield return new object[] { 2, 2064m };
		//	yield return new object[] { 3, 2096m };
		//}

		[Fact]
		//[Fact(Skip = "Needs fixing!")]
		[Trait("Category", "Calculation")]
		public void GetSavingsValueTest()
		{
			//Console.WriteLine($"Test start! {DateTime.Now}");

			decimal capital = 2000;
			decimal interestRate = 0.016m;
			int year = 3;

			decimal expected = 2096;
			decimal result = _fixture.Calculator.GetSavingsValue(capital, interestRate, year);

			Assert.Equal(expected, result);

			//Thread.Sleep(3000);
			//Console.WriteLine($"Test end! {DateTime.Now}");
		}

		[Fact]
		[Trait("Category", "Calculation")]
		[Trait("Property", "NegativeYearException")]
		public void GetSavingsValueTest_NegativeYear()
		{
			decimal capital = 2000;
			decimal interestRate = 0.016m;
			int year = -2;

			Assert.Throws<NegativeYearException>(() => _fixture.Calculator.GetSavingsValue(capital, interestRate, year));
		}

		[Fact]
		[Trait("Category", "Currency")]
		public void GetValueInHRKTest()
		{
			string code = "EUR";
			decimal original = 3;
			decimal expected = 22.56m;

			//if(!_finaService.IsConnected())
			//{
			//	Assert.Inconclusive("Can't connect to service!");
			//}
			decimal result = _fixture.Calculator.GetValueInHRK(code, original);

			Assert.Equal(expected, result);
		}

		[Fact]
		[Trait("Category", "Currency")]
		public void GetValueInDefaultCurrencyTest()
		{
			string code = "EUR";
			decimal original = 3;

			decimal expected = 1078.50m;
			decimal result = _fixture.Calculator.GetValueInDefaultCurrency(code, original);
			Assert.Equal(expected, result);
		}

		[Fact]
		public void IsPersonCreditableTest()
		{
			Person person = new PersonBuilder().WithIsEmployed(true)
											   .WithSalaries(new List<decimal>() { 1000m, 2000m, 3000m, 4000m, 5000m })
											   .Build();
			decimal fixDeduction = 1500m;
			decimal rate = 1000m;

			bool result = _fixture.Calculator.IsPersonCreditable(person, fixDeduction, rate);

			Assert.True(result);
		}

		[Fact]
		public void IsPersonCreditableTest_NotCreditable()
		{
			Person person = new PersonBuilder().WithIsEmployed(true)
											   .WithSalaries(new List<decimal>() { 1000m, 2000m, 3000m, 4000m, 5000m })
											   .Build();
			decimal fixDeduction = 3500m;
			decimal rate = 1000m;

			bool result = _fixture.Calculator.IsPersonCreditable(person, fixDeduction, rate);

			Assert.False(result);
		}

		[Fact]
		public void IsPersonCreditableTest_Unemployed()
		{
			Person person = new PersonBuilder().WithIsEmployed(false)
											   .Build();
			decimal fixDeduction = 1500m;
			decimal rate = 1000m;

			bool result = _fixture.Calculator.IsPersonCreditable(person, fixDeduction, rate);

			Assert.False(result);
		}
	}
}
