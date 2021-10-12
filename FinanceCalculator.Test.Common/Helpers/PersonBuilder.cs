using FinanceCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceCalculator.Test.Common.Helpers
{
	public class PersonBuilder
	{
		private Person _person = new Person();

        public PersonBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            this._person = new Person();
        }

        public PersonBuilder WithFirstName(string firstName)
		{
            this._person.FirstName = firstName;
            return this;
        }

        public PersonBuilder WithLastName(string lastName)
        {
            this._person.LastName = lastName;
            return this;
        }

        public PersonBuilder WithDateOfBirth(DateTime dateOfBirth)
        {
            this._person.DateOfBirth = dateOfBirth;
            return this;
        }

        public PersonBuilder WithIsEmployed(bool isEmployed)
        {
            this._person.IsEmployed = isEmployed;
            return this;
        }

        public PersonBuilder WithSalaries(List<decimal> salaries)
        {
            this._person.Salaries = salaries;
            return this;
        }

        public Person Build()
        {
            Person result = this._person;
            this.Reset();
            return result;
        }
    }
}
