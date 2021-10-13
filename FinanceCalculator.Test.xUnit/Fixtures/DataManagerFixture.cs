using FinanceCalculator.Fina;
using FinanceCalculator.Settings;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Text;

namespace FinanceCalculator.Test.xUnit.Fixtures
{
	public class DataManagerFixture
	{
		public DataManager.DataManager DataManager { get; set; }

		public DataManagerFixture()
		{
			DataManager = new DataManager.DataManager();
		}
	}
}
