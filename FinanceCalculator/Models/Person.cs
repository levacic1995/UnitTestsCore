using FinanceCalculator.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceCalculator.Models
{
	public class Person
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime DateOfBirth { get; set; }

		public bool IsEmployed { get; set; }

		public List<decimal> Salaries { get; set; }

		public void AddSalary(decimal newSalary)
		{
			Salaries.Add(newSalary);
		}

		public decimal GetAverageSalary()
		{
			if (Salaries.Count == 0)
			{
				throw new NoDataException();
			}

			if (Salaries.Count < 3)
			{
				throw new TooFewMonthsException();
			}

			return Salaries.Skip(Salaries.Count - 3).Take(3).Average();
		}
	}
}
