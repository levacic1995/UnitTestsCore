using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceCalculator.Test.Common.Helpers
{
	public class TestData
	{
		public static string TransactionsFilePath
			=> @"TestData/transactions.csv";

		public static string TransactionsMissingDataFilePath
			=> @"TestData/transactions_missing_data.csv";

		public static string TransactionsInvalidDataFilePath
			=> @"TestData/transactions_invalid_data.csv";
	}
}
