using FinanceCalculator.Database;
using FinanceCalculator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FinanceCalculator.DataManager
{
	public class DataManager
	{
		public void SaveTransactions(FinanceContext context, List<Transaction> transactions)
		{
			context.Transactions.AddRange(transactions);
			context.SaveChanges();
		}

		public List<Transaction> LoadTransactions(string path)
		{
			List<Transaction> result = new List<Transaction>();
			string[] lines = File.ReadAllLines(path);
			foreach(string line in lines)
			{
				Transaction newTransaction = ConvertStringToTransaction(line);
				if(newTransaction != null)
				{
					result.Add(newTransaction);
				}
			}
			return result;
		}

		private Transaction ConvertStringToTransaction(string line)
		{
			string[] data = line.Split(',');

			try
			{
				DateTime time = DateTime.Parse(data[0]);
				decimal amount = decimal.Parse(data[1]);
				string location = data[2];
				string customer = data[3];

				return new Transaction()
				{
					Time = time,
					Amount = amount,
					Location = location,
					Customer = customer
				};
			}
			catch(Exception)
			{
				return null;
			}

		}
	}
}
