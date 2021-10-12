using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceCalculator.Models
{
	public class Transaction
	{
		public DateTime Time { get; set; }

		public decimal Amount { get; set; }

		public string Location { get; set; }

		public string Customer { get; set; }

		public override bool Equals(object obj)
		{
			if ((obj == null) || !typeof(Transaction).Equals(obj.GetType()))
			{
				return false;
			}
			else
			{
				Transaction t = (Transaction)obj;
				return (Time == t.Time) && (Amount == t.Amount) && (Location == t.Location) && (Customer == t.Customer);
			}
		}
	}
}
