using FinanceCalculator.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceCalculator.Database
{
	public class FinanceContext : DbContext
	{
		public DbSet<Transaction> Transactions { get; set; }

		public FinanceContext(DbContextOptions options) : base(options)
		{ }
    }
}
