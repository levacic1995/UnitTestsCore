using FinanceCalculator.DataManager;
using FinanceCalculator.Models;
using FinanceCalculator.Test.Common.Database;
using FinanceCalculator.Test.Common.Helpers;
using FinanceCalculator.Test.xUnit.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FinanceCalculator.Test.xUnit
{
	public class DataManagerTests : IClassFixture<DataManagerFixture>
	{
		DataManagerFixture _fixture;

		// setup, before every test
		public DataManagerTests(DataManagerFixture fixture)
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

			List<Transaction> transactions = _fixture.DataManager.LoadTransactions(TestData.TransactionsFilePath);

			Assert.Equal(expected, transactions);
		}


		[Fact]
		public void SaveTransactionsTest_Sqlite()
		{
			List<Transaction> transactions = GetTestData();

			using (var context = ContextFactory.GetSqliteContext())
			{
				_fixture.DataManager.SaveTransactions(context, transactions);

				int dbCount = context.Transactions.Count();
				Assert.Equal(transactions.Count, dbCount);
			}
		}

		[Fact]
		public void SaveTransactionsTest_InMemory()
		{
			List<Transaction> transactions = GetTestData();

			using (var context = ContextFactory.GetInMemoryDbContext())
			{
				_fixture.DataManager.SaveTransactions(context, transactions);

				int dbCount = context.Transactions.Count();
				Assert.Equal(transactions.Count, dbCount);
			}
		}

		private List<Transaction> GetTestData()
		{
			return new List<Transaction>()
			{
				new Transaction() { Time = new DateTime(2000, 1, 1), Amount = 2123m, Location = "New York", Customer = "Ivo Ivic" },
				new Transaction() { Time = new DateTime(2009, 10, 22), Amount = 231m, Location = "Paris", Customer = "Hrvoje Horvat" },
				new Transaction() { Time = new DateTime(2010, 11, 24), Amount = 4324m, Location = "London", Customer = "Patrik Vujak" },
			};
		}
	}
}
