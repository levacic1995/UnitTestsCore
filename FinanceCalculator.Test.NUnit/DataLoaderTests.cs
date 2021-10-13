﻿using FinanceCalculator.DataLoader;
using FinanceCalculator.Models;
using FinanceCalculator.Test.Common.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FinanceCalculator.Test.NUnit
{
	[TestFixture]
	public class DataLoaderTests
	{
		static DataLoader.DataLoader _dataLoader;


		[OneTimeSetUp]
		public static void OneTimeSetUp()
		{
			_dataLoader = new DataLoader.DataLoader();
		}

		[Test]
		public void LoadTransactionTest()
		{
			List<Transaction> expected = new List<Transaction>()
			{
				new Transaction() { Time = new DateTime(2000, 1, 1), Amount = 2123m, Location = "New York", Customer = "Ivo Ivic" },
				new Transaction() { Time = new DateTime(2009, 10, 22), Amount = 231m, Location = "Paris", Customer = "Hrvoje Horvat" },
				new Transaction() { Time = new DateTime(2010, 11, 24), Amount = 4324m, Location = "London", Customer = "Patrik Vujak" },
			};

			List<Transaction> transactions = _dataLoader.LoadTransactions(TestData.TransactionsFilePath);

			// CollectionAssert.AreEquivalent(expected, transactions); //hash code :)
			CollectionAssert.AreEqual(expected, transactions);
		}
	}
}