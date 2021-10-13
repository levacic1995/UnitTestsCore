using FinanceCalculator.DataLoader;
using FinanceCalculator.Models;
using FinanceCalculator.Test.Common.Helpers;
using FinanceCalculator.Test.xUnit.Fixtures;
using System;
using System.Collections.Generic;
using Xunit;

namespace FinanceCalculator.Test.xUnit
{
	public class DataLoaderTests : IClassFixture<DataLoaderFixture>
	{
		DataLoaderFixture _fixture;

		// setup, before every test
		public DataLoaderTests(DataLoaderFixture fixture)
		{
			_fixture = fixture;
		}

		[Fact]
		public void LoadTransactionTest()
		{
			List<Transaction> expected = new List<Transaction>()
			{
				new Transaction() { Time = new DateTime(2000, 1, 1), Amount = 2123m, Location = "New York", Customer = "Ivo Ivic" },
				new Transaction() { Time = new DateTime(2009, 10, 22), Amount = 231m, Location = "Paris", Customer = "Hrvoje Horvat" },
				new Transaction() { Time = new DateTime(2010, 11, 24), Amount = 4324m, Location = "London", Customer = "Patrik Vujak" },
			};

			List<Transaction> transactions = _fixture.DataLoader.LoadTransactions(TestData.TransactionsFilePath);

			Assert.Equal(expected, transactions);
		}
	}
}
