using FinanceCalculator.DataManager;
using FinanceCalculator.Models;
using FinanceCalculator.Test.Common.Database;
using FinanceCalculator.Test.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceCalculator.Test.MSTest
{
	[TestClass]
	public class DataManagerTests
	{
		static DataManager.DataManager _dataManager;

		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			_dataManager = new DataManager.DataManager();
		}

		[TestMethod]
		public void LoadTransactionTest()
		{
			List<Transaction> expected = new List<Transaction>()
			{
				new Transaction() { Time = new DateTime(2000, 1, 1), Amount = 2123m, Location = "New York", Customer = "Ivo Ivic" },
				new Transaction() { Time = new DateTime(2009, 10, 22), Amount = 231m, Location = "Paris", Customer = "Hrvoje Horvat" },
				new Transaction() { Time = new DateTime(2010, 11, 24), Amount = 4324m, Location = "London", Customer = "Patrik Vujak" },
			};

			List<Transaction> transactions = _dataManager.LoadTransactions(TestData.TransactionsFilePath);

			// CollectionAssert.AreEquivalent(expected, transactions); //hash code :)
			CollectionAssert.AreEqual(expected, transactions);
		}

		[TestMethod]
		public void LoadTransactionTest_MissingData()
		{
			List<Transaction> expected = new List<Transaction>()
			{
				new Transaction() { Time = new DateTime(2000, 1, 1), Amount = 2123m, Location = "New York", Customer = "Ivo Ivic" },
				new Transaction() { Time = new DateTime(2010, 11, 24), Amount = 4324m, Location = "London", Customer = "Patrik Vujak" },
			};

			List<Transaction> transactions = _dataManager.LoadTransactions(TestData.TransactionsMissingDataFilePath);

			// CollectionAssert.AreEquivalent(expected, transactions); //hash code :)
			CollectionAssert.AreEqual(expected, transactions);
		}

		[TestMethod]
		public void LoadTransactionTest_InvalidData()
		{
			List<Transaction> expected = new List<Transaction>()
			{
				new Transaction() { Time = new DateTime(2000, 1, 1), Amount = 2123m, Location = "New York", Customer = "Ivo Ivic" },
				new Transaction() { Time = new DateTime(2010, 11, 24), Amount = 4324m, Location = "London", Customer = "Patrik Vujak" },
			};

			List<Transaction> transactions = _dataManager.LoadTransactions(TestData.TransactionsInvalidDataFilePath);

			// CollectionAssert.AreEquivalent(expected, transactions); //hash code :)
			CollectionAssert.AreEqual(expected, transactions);
		}

		[TestMethod]
		public void SaveTransactionsTest_Sqlite()
		{
			List<Transaction> transactions = GetTestData();

			using (var context = ContextFactory.GetSqliteContext())
			{
				_dataManager.SaveTransactions(context, transactions);

				int dbCount = context.Transactions.Count();
				Assert.AreEqual(transactions.Count, dbCount);
			}
		}

		[TestMethod]
		public void SaveTransactionsTest_InMemory()
		{
			List<Transaction> transactions = GetTestData();

			using (var context = ContextFactory.GetInMemoryDbContext())
			{
				_dataManager.SaveTransactions(context, transactions);

				int dbCount = context.Transactions.Count();
				Assert.AreEqual(transactions.Count, dbCount);
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
