using FinanceCalculator.Test.Common.Helpers;
using FinanceCalculator.Time;
using System;
using System.Text;
using Xunit;

namespace FinanceCalculator.Test.xUnit
{
	public class TimeTests
	{
		CustomTime _testTime;

		public TimeTests()
		{
			_testTime = CustomTimeMother.GetTime();
		}

		[Fact]
		public void DayIncreaseTest()
		{
			int expectedDay = 29;
			int expectedMonth = 10;
			int expectedYear = 2010;

			_testTime.IncreaseDay(2);

			Assert.Equal(expectedDay, _testTime.Day);
			Assert.Equal(expectedMonth, _testTime.Month);
			Assert.Equal(expectedYear, _testTime.Year);
		}

		[Fact]
		public void DayIncreaseTest_CarryOver_OnlyMonth()
		{
			int expectedDay = 3;
			int expectedMonth = 11;
			int expectedYear = 2010;

			_testTime.IncreaseDay(6);

			Assert.Equal(expectedDay, _testTime.Day);
			Assert.Equal(expectedMonth, _testTime.Month);
			Assert.Equal(expectedYear, _testTime.Year);
		}

		[Fact]
		public void DayIncreaseTest_CarryOver_MonthAndYear()
		{
			int expectedDay = 3;
			int expectedMonth = 1;
			int expectedYear = 2011;

			_testTime.IncreaseDay(66);

			Assert.Equal(expectedDay, _testTime.Day);
			Assert.Equal(expectedMonth, _testTime.Month);
			Assert.Equal(expectedYear, _testTime.Year);
		}

		[Fact]
		public void MonthIncreaseTest()
		{
			int expectedDay = 27;
			int expectedMonth = 11;
			int expectedYear = 2010;

			_testTime.IncreaseMonth(1);

			Assert.Equal(expectedDay, _testTime.Day);
			Assert.Equal(expectedMonth, _testTime.Month);
			Assert.Equal(expectedYear, _testTime.Year);
		}

		[Fact]
		public void MonthIncreaseTest_CarryOver()
		{
			int expectedDay = 27;
			int expectedMonth = 1;
			int expectedYear = 2011;

			_testTime.IncreaseMonth(3);

			Assert.Equal(expectedDay, _testTime.Day);
			Assert.Equal(expectedMonth, _testTime.Month);
			Assert.Equal(expectedYear, _testTime.Year);
		}

		[Fact]
		public void YearIncreaseTest()
		{
			int expectedDay = 27;
			int expectedMonth = 10;
			int expectedYear = 2012;

			_testTime.IncreaseYear(2);

			Assert.Equal(expectedDay, _testTime.Day);
			Assert.Equal(expectedMonth, _testTime.Month);
			Assert.Equal(expectedYear, _testTime.Year);
		}
	}
}
