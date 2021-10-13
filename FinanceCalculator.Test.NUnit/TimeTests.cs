using FinanceCalculator.Test.Common.Helpers;
using FinanceCalculator.Time;
using NUnit.Framework;
using System;
using System.Text;

namespace FinanceCalculator.Test.NUnit
{
	[TestFixture]
	public class TimeTests
	{
		CustomTime _testTime;

		[SetUp] // OneTimeSetUp :)
		public void Initialize()
		{
			_testTime = CustomTimeMother.GetTime();
		}

		[Test]
		public void DayIncreaseTest()
		{
			int expectedDay = 29;
			int expectedMonth = 10;
			int expectedYear = 2010;

			_testTime.IncreaseDay(2);

			Assert.AreEqual(expectedDay, _testTime.Day);
			Assert.AreEqual(expectedMonth, _testTime.Month);
			Assert.AreEqual(expectedYear, _testTime.Year);
		}

		[Test]
		public void DayIncreaseTest_CarryOver_OnlyMonth()
		{
			int expectedDay = 3;
			int expectedMonth = 11;
			int expectedYear = 2010;

			_testTime.IncreaseDay(6);

			Assert.AreEqual(expectedDay, _testTime.Day);
			Assert.AreEqual(expectedMonth, _testTime.Month);
			Assert.AreEqual(expectedYear, _testTime.Year);
		}

		[Test]
		public void DayIncreaseTest_CarryOver_MonthAndYear()
		{
			int expectedDay = 3;
			int expectedMonth = 1;
			int expectedYear = 2011;

			_testTime.IncreaseDay(66);

			Assert.AreEqual(expectedDay, _testTime.Day);
			Assert.AreEqual(expectedMonth, _testTime.Month);
			Assert.AreEqual(expectedYear, _testTime.Year);
		}

		[Test]
		public void MonthIncreaseTest()
		{
			int expectedDay = 27;
			int expectedMonth = 11;
			int expectedYear = 2010;

			_testTime.IncreaseMonth(1);

			Assert.AreEqual(expectedDay, _testTime.Day);
			Assert.AreEqual(expectedMonth, _testTime.Month);
			Assert.AreEqual(expectedYear, _testTime.Year);
		}

		[Test]
		public void MonthIncreaseTest_CarryOver()
		{
			int expectedDay = 27;
			int expectedMonth = 1;
			int expectedYear = 2011;

			_testTime.IncreaseMonth(3);

			Assert.AreEqual(expectedDay, _testTime.Day);
			Assert.AreEqual(expectedMonth, _testTime.Month);
			Assert.AreEqual(expectedYear, _testTime.Year);
		}

		[Test]
		public void YearIncreaseTest()
		{
			int expectedDay = 27;
			int expectedMonth = 10;
			int expectedYear = 2012;

			_testTime.IncreaseYear(2);

			Assert.AreEqual(expectedDay, _testTime.Day);
			Assert.AreEqual(expectedMonth, _testTime.Month);
			Assert.AreEqual(expectedYear, _testTime.Year);
		}
	}
}
