using FinanceCalculator.Fina;
using FinanceCalculator.Settings;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Text;

namespace FinanceCalculator.Test.xUnit.Fixtures
{
	public class DataLoaderFixture
	{
		public DataLoader.DataLoader DataLoader { get; set; }

		public DataLoaderFixture()
		{
			DataLoader = new DataLoader.DataLoader();
		}
	}
}
