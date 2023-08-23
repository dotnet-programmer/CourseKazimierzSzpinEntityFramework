using System.Reflection;
using Accountancy.DataLayer.Extensions;
using Accountancy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Attribute = Accountancy.Domain.Entities.Attribute;

namespace Accountancy.DataLayer;

public class AppDbContext : DbContext
{
	public static readonly ILoggerFactory _loggerFactory = new NLogLoggerFactory();

	public DbSet<Address> Addresses { get; set; }
	public DbSet<Attribute> Attributes { get; set; }
	public DbSet<Customer> Customers { get; set; }
	public DbSet<Invoice> Invoices { get; set; }
	public DbSet<InvoicePosition> InvoicePositions { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<AttributeProduct> AttributeProducts { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var builder = new ConfigurationBuilder().AddJsonFile("AppSettings.json", true, true);
		var config = builder.Build();

		optionsBuilder
			.UseSqlServer(config["ConnectionString"])
			.UseLoggerFactory(_loggerFactory)
			.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
			.EnableSensitiveDataLogging();

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		//modelBuilder.SeedCustomers();
		//modelBuilder.SeedAddresses();
		modelBuilder.SeedData();
	}
}