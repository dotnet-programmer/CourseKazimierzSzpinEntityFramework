using Accountancy.DataLayer;
using Accountancy.DataLayer.Extensions;
using Accountancy.Domain.Entities;
using Accountancy.Domain.Enums;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Attribute = Accountancy.Domain.Entities.Attribute;

namespace Accountancy.UI;

internal class Homework2
{
	public static async Task StartHomework2()
	{
		Console.ReadKey(true);
		Console.WriteLine("\n1) Dodaj do każdej tabeli przynajmniej 1 rekord za pomocą Entity Framework Core.\n");

		Address address = new()
		{
			State = "NewState1",
			City = "NewCity1",
			PostalCode = "12345",
			Street = "Street1",
		};

		Customer customer = new()
		{
			Name = "NewCustomer1",
			Nip = "12-12-12",
			PhoneNumber = "1234567890",
			Email = "nc1@nc1",
			Address = address,
		};

		List<Attribute> attributes =
		[
			new Attribute { Name = "NowyAtrybut1" },
			new Attribute { Name = "NowyAtrybut2" },
			new Attribute { Name = "NowyAtrybut3" },
		];

		using (AppDbContext context = new())
		{
			// Add and save attributes first
			context.Attributes.AddRange(attributes);
			await context.SaveChangesAsync();

			Invoice invoice = new()
			{
				Number = 1,
				Year = DateTime.Now.Year,
				Month = (byte)DateTime.Now.Month,
				Type = InvoiceType.FA,
				CreatedDate = DateTime.Now,
				IsPaid = false,
				Customer = customer,
				TotalPrice = 1,
				InvoicePositions =
				[
					new InvoicePosition
					{
						Quantity = 5,
						Product = new Product
						{
							Name = "NowyProdukt1",
							Price = 1500,
							Attributes = attributes
						}
					}
				]
			};

			context.Invoices.Add(invoice);
			await context.SaveChangesAsync();
		}

		Console.ReadKey(true);
		Console.WriteLine("\n2) Zmień i zaktualizuj wartość pola IsPaid w tabeli Invoices dla dowolnej faktury.\n");

		using (AppDbContext context = new())
		{
			var invoiceToUpdate = await context.Invoices.FindAsync(3);
			if (invoiceToUpdate is not null)
			{
				invoiceToUpdate.IsPaid = true;
				await context.SaveChangesAsync();
			}
		}

		Console.ReadKey(true);
		Console.WriteLine("\n3) Zaktualizuj atrybuty do wybranego produktu. Tak żeby przynajmniej 1 był nowy i przynajmniej 1 został usunięty.\n");

		using (AppDbContext context = new())
		{
			var productAttributesFromDB = await context.AttributeProducts
				.Where(x => x.ProductId == 1)
				.AsNoTracking()
				.ToListAsync();

			context.TryUpdateManyToMany(
				productAttributesFromDB,
				[
					new() { ProductId = 1, AttributeId = 1 },
					new() { ProductId = 1, AttributeId = 2 },
					new() { ProductId = 1, AttributeId = 3 },
				],
				x => x.AttributeId
			);
			await context.SaveChangesAsync();
		}

		Console.ReadKey(true);
		Console.WriteLine("\n4) Usuń dowolnego klienta z bazy danych za pomocą Entity Framework Core.\n");

		using (AppDbContext context = new())
		{
			context.Customers.Remove(new Customer { CustomerId = 3 });
			await context.SaveChangesAsync();
		}

		Console.ReadKey(true);
		Console.WriteLine("\n5) Dodaj 3000 nowych produktów do bazy danych.\n");

		List<Product> productsToAdd = [];
		for (int i = 0; i < 3000; i++)
		{
			productsToAdd.Add(new() { Name = $"ProductName{i}", Price = i + 10 });
		}

		using (AppDbContext context = new())
		{
			//context.AddRange(products);
			await context.BulkInsertAsync(productsToAdd);
		}

		Console.ReadKey(true);
		Console.WriteLine("\n6) Wszystkim produktom w bazie danych zwiększ cenę o 10.\n");

		using (AppDbContext context = new())
		{
			var products = await context.Products.ToListAsync();
			products.ForEach(x => x.Price += 10);
			await context.BulkUpdateAsync(products);
		}

		Console.ReadKey(true);
		Console.WriteLine("\n7) Usuń wszystkie produkty z bazy danych, które wcześniej dodałeś.\n");

		using (AppDbContext context = new())
		{
			var productsToDelete = await context.Products.Where(x => x.ProductId > 6).ToListAsync();
			//context.RemoveRange(productsToDelete);
			//await context.SaveChangesAsync();
			await context.BulkDeleteAsync(productsToDelete);
		}

		Console.ReadKey(true);
		Console.WriteLine("\n8) Obsłuż miękkie usuwanie klientów i usuń w ten sposób dowolnego klienta.\n");

		using (AppDbContext context = new())
		{
			var customerToDelete = await context.Customers.FindAsync(5);
			if (customerToDelete is not null)
			{
				customerToDelete.IsDeleted = true;
				await context.SaveChangesAsync();
			}
		}

		Console.ReadKey(true);
		Console.WriteLine("\n9) Stwórz i wywołaj nowy widok, który na podstawie przekazanego id będzie zwracał wszystkie informacje o kliencie i jego adresie.\n");

		using (AppDbContext context = new())
		{
			var customersWithAddress = await context.CustomerAddressView.ToListAsync();
			foreach (var item in customersWithAddress)
			{
				Console.WriteLine($"{item.CustomerId} {item.Name} {item.Nip} {item.Email} {item.PhoneNumber} - {item.Address}");
			}
		}

		Console.ReadKey(true);
		Console.WriteLine("\n10) Stwórz i wywołaj procedurę, która będzie usuwała produkt na podstawie przekazanego id.\n");

		using (AppDbContext context = new())
		{
			await context.Database.ExecuteSqlInterpolatedAsync($"DeleteProduct {5}");
		}
	}
}