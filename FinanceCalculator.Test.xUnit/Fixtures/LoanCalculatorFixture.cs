using FinanceCalculator.Fina;
using FinanceCalculator.Settings;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Text;

namespace FinanceCalculator.Test.xUnit.Fixtures
{
	public class LoanCalculatorFixture
	{
		public LoanCalculator Calculator { get; set; }

		public IFinaService FinaService { get; set; }

		public LoanCalculatorFixture()
		{
			IOptions<FinanceCalculatorOptions> options = Options.Create(new FinanceCalculatorOptions()
			{
				DefaultCurrencyCode = "HUF"
			});

			Mock<IFinaService> finaService = new Mock<IFinaService>();
			finaService.Setup(_ => _.IsConnected()).Returns(false);
			finaService.Setup(_ => _.GetCurrencyValue("HRK", "EUR")).Returns(7.52m);
			finaService.Setup(_ => _.GetCurrencyValue("HUF", "EUR")).Returns(359.50m);

			FinaService = finaService.Object;
			Calculator = new LoanCalculator(options, FinaService);
		}
	}
}
