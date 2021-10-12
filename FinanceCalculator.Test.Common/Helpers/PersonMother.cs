using FinanceCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceCalculator.Test.Common.Helpers
{
	public class PersonMother
	{
		public static Person GetPersonWithNormalSalary()
		{
			return new Person()
			{ 
				Salaries = new List<decimal>() { 1000m, 2000m, 3000m, 4000m, 5000m } 
			};
		}

		public static Person GetPersonWithTooFewSalaries()
		{
			return new Person
			{
				Salaries = new List<decimal>() { 1000m, 2000m }
			};
		}

		public static Person GetPersonWithNoSalary()
		{
			return new Person
			{
				Salaries = new List<decimal>()
			};
		}
	}
}
