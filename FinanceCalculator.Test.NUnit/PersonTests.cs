using FinanceCalculator.Exceptions;
using FinanceCalculator.Models;
using FinanceCalculator.Test.Common.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceCalculator.Test.NUnit
{
	[TestFixture]
	public class PersonTests
	{
		private Person _testPerson;

		[Test]
		public void GetAverageSalaryTest()
		{
			decimal expected = 4000m;
			_testPerson = PersonMother.GetPersonWithNormalSalary();

			decimal result = _testPerson.GetAverageSalary();

			Assert.AreEqual(expected, result);
		}


		[Test]
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


		[Test]
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

		[Test]
		public void GetAverageSalaryTest_NoSalaries()
		{
			_testPerson = PersonMother.GetPersonWithNoSalary();

			Assert.Throws<NoDataException>(() => _testPerson.GetAverageSalary());
		}

		[Test]
		public void GetAverageSalaryTest_TooFewSalaries()
		{
			_testPerson = PersonMother.GetPersonWithTooFewSalaries();

			Assert.Throws<TooFewMonthsException>(() => _testPerson.GetAverageSalary());
		}

		[Test]
		public void AddSalaryTest()
		{
			decimal expected = 6;
			_testPerson = PersonMother.GetPersonWithNormalSalary();

			_testPerson.AddSalary(1000m);

			Assert.AreEqual(expected, _testPerson.Salaries.Count);
		}
	}
}
