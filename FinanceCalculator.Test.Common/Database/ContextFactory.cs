using FinanceCalculator.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceCalculator.Test.Common.Database
{
	public class ContextFactory
	{
		public static FinanceContext GetSqliteContext()
		{
			DbContextOptions options = new DbContextOptionsBuilder<FinanceContext>()
				.UseSqlite("Filename=sqliteTest.db")
				.Options;

			FinanceContext context = new FinanceContext(options);

			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();
			return context;
		}

		public static FinanceContext GetInMemoryDbContext()
		{
			DbContextOptions options = new DbContextOptionsBuilder<FinanceContext>()
				.UseInMemoryDatabase(databaseName: "inMemoryTest.db")
				.Options;

			return GetContext(options);
		}

		private static FinanceContext GetContext(DbContextOptions options)
		{
			FinanceContext context = new FinanceContext(options);

			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();

			return context;
		}
	}
}
