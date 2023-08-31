using Accountancy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accountancy.DataLayer.Extensions;

public static class ModelBuilderExtensions
{
	public static void SeedData(this ModelBuilder modelBuilder)
	{
		SeedCustomers(modelBuilder);
		SeedAddresses(modelBuilder);
	}

	private static void SeedCustomers(this ModelBuilder modelBuilder)
		=> modelBuilder.Entity<Customer>().HasData(
			new Customer
			{
				CustomerId = 1,
				Name = "Maciek Kowalski",
				Nip = "111-222-33-44",
				PhoneNumber = "1234567890",
				Email = "jakis.mail@poczta.pl",
			}
		);

	private static void SeedAddresses(this ModelBuilder modelBuilder)
		=> modelBuilder.Entity<Address>().HasData(
			new Address
			{
				AddressId = 1,
				CustomerId = 1,
				State = "Polska",
				City = "Kielce",
				Street = "Warszawska 66/6",
				PostalCode = "12-345",
			}
		);
}