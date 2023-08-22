using Accountancy.DataLayer.Extensions;
using Accountancy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Attribute = Accountancy.Domain.Entities.Attribute;

namespace Accountancy.DataLayer;

public class AppDbContext : DbContext
{
	public DbSet<Address> Addresses { get; set; }
	public DbSet<Attribute> Attributes { get; set; }
	public DbSet<Customer> Customers { get; set; }
	public DbSet<Invoice> Invoices { get; set; }
	public DbSet<InvoicePosition> InvoicePositions { get; set; }
	public DbSet<Product> Products { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var builder = new ConfigurationBuilder().AddJsonFile("AppSettings.json", true, true);
		var config = builder.Build();
		optionsBuilder.UseSqlServer(config["ConnectionString"]);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		//modelBuilder.SeedCustomers();
		//modelBuilder.SeedAddresses();
		modelBuilder.SeedData();
	}
}