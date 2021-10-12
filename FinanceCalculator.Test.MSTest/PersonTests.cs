using FinanceCalculator.Exceptions;
using FinanceCalculator.Models;
using FinanceCalculator.Test.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceCalculator.Test.MSTest
{
	[TestClass]
	public class PersonTests
	{
		private Person _testPerson;

		[TestMethod]
		public void GetAverageSalaryTest()
		{
			decimal expected = 4000m;
			_testPerson = PersonMother.GetPersonWithNormalSalary();

			decimal result = _testPerson.GetAverageSalary();

			Assert.AreEqual(expected, result);
		}


		[TestMethod]
		public void GetAverageSalaryTest_Builder()
		{
			decimal expected = 4000m;
			_testPerson = new PersonBuilder().WithFirstName("Jovica")
											 .WithLastName("Novakovic")
											 .WithDateOfBirth(new DateTime(1996, 9, 20))
											 .WithSalaries(new List<decimal>() { 1000m, 2000m, 3000m, 4000m, 5000m })
											 .Build();

			decimal result = _testPerson.GetAverageSalary();

			Assert.AreEqual(expected, result);
		}


		[TestMethod]
		public void GetAverageSalaryTest_Normal()
		{
			decimal expected = 4000m;
			_testPerson = new Person()
			{
				FirstName = "Jovica",
				LastName = "Novakovic",
				DateOfBirth = new DateTime(1996, 9, 20),
				Salaries = new List<decimal>() { 1000m, 2000m, 3000m, 4000m, 5000m }
			};

			decimal result = _testPerson.GetAverageSalary();

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		[ExpectedException(typeof(NoDataException))]
		public void GetAverageSalaryTest_NoSalaries()
		{
			_testPerson = PersonMother.GetPersonWithNoSalary();

			_testPerson.GetAverageSalary();
		}

		[TestMethod]
		[ExpectedException(typeof(TooFewMonthsException))]
		public void GetAverageSalaryTest_TooFewSalaries()
		{
			_testPerson = PersonMother.GetPersonWithTooFewSalaries();

			_testPerson.GetAverageSalary();
		}

		[TestMethod]
		public void AddSalaryTest()
		{
			decimal expected = 6;
			_testPerson = PersonMother.GetPersonWithNormalSalary();

			_testPerson.AddSalary(1000m);

			Assert.AreEqual(expected, _testPerson.Salaries.Count);
		}
	}
}
