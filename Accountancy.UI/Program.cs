using Accountancy.DataLayer;
using Accountancy.DataLayer.Extensions;
using Accountancy.Domain.Entities;
using Accountancy.Domain.Enums;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Attribute = Accountancy.Domain.Entities.Attribute;

// ***** HOMEWORK 1 *****

//using (AppDbContext context = new())
//{
//	Console.WriteLine("3) Napisz pierwszą kwerendę, która pobierze wszystkie faktury, dołączając właściwości nawigacyjne,");
//	var invoices = await context.Invoices
//		.Include(x => x.Customer)
//		.ThenInclude(x => x.Address)
//		.Include(x => x.InvoicePositions)
//		.AsNoTracking()
//		.ToListAsync();

//	Console.WriteLine("4) Pobierz produkty, których cena jest z przedziału od 100 do 200 wraz z atrybutami, ale tylko takimi, które mają Id większe niż 5 bez śledzenie danych.");
//	var products = await context.Products
//		.Where(x => x.Price > 100 && x.Price < 200)
//		.Include(x => x.Attributes.Where(x => x.AttributeId > 5))
//		.AsNoTracking()
//		.ToListAsync();

//	Console.WriteLine("5) Pobierz pełne numery wszystkich faktur, to znaczy Numer/Miesiac/Rok wraz z ich średnią wartością dla 1 pozycji faktury.");
//	var invoicesWithAvg1ProductPrice = await context.Invoices
//		.Include(x => x.InvoicePositions)
//		.ThenInclude(x => x.Product)
//		.Select(x => new
//		{
//			InvoiceNumber = $"{x.Number}/{x.Month}/{x.Year}",
//			Avg1ProductPrice = x.InvoicePositions.Select(x => x.Product.Price).Average()
//		})
//		.ToListAsync();

//	Console.WriteLine("6) Pobierz wszystkie atrybuty, których nazwa zaczyna się od „a” oraz kończy się na „z”.");
//	var attributesFromAToZ = await context.Attributes
//		.Where(x => x.Name.ToUpper().StartsWith('A') && x.Name.ToUpper().EndsWith('Z'))
//		.ToListAsync();

//	Console.WriteLine("7) Pobierz wszystkich klientów, którzy nie posiadają nipu lub telefonu i posortuj po nazwie.");
//	var customersWithoutNipOrPhone = await context.Customers
//		.Where(x => string.IsNullOrWhiteSpace(x.Nip) || string.IsNullOrWhiteSpace(x.PhoneNumber))
//		.OrderBy(x => x.Name)
//		.ToListAsync();

//	Console.WriteLine("8) Pobierz wszystkie faktury, które nie zostały zapłacone, a ich wartość całkowita jest większa niż 1000.");
//	var unpaidInvoices = await context.Invoices
//		.Where(x => x.IsPaid == false && x.TotalPrice > 1000)
//		.ToListAsync();
//}

// ***** HOMEWORK 2 *****

// 1) Dodaj do każdej tabeli przynajmniej 1 rekord za pomocą Entity Framework Core.

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
List<Attribute> attributes = new()
{
	new Attribute { Name = "NowyAtrybut1" },
	new Attribute { Name = "NowyAtrybut2" },
	new Attribute { Name = "NowyAtrybut3" },
};
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
	InvoicePositions = new List<InvoicePosition>
	{
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
	}
};
using (AppDbContext context = new())
{
	context.Invoices.Add(invoice);
	await context.SaveChangesAsync();
}

//2) Zmień i zaktualizuj wartość pola IsPaid w tabeli Invoices dla dowolnej faktury.
using (AppDbContext context = new())
{
	var invoiceToUpdate = await context.Invoices.FindAsync(3);
	invoiceToUpdate.IsPaid = true;
	await context.SaveChangesAsync();
}

// 3) Zaktualizuj atrybuty do wybranego produktu. Tak żeby przynajmniej 1 był nowy i przynajmniej 1 został usunięty.
using (AppDbContext context = new())
{
	//var product = await context.Products.FindAsync(2);
	//var productAttributesFromDB = await context.AttributeProducts.Where(x => x.ProductId == product.ProductId).ToListAsync();
	int productId = 2;
	var productAttributesFromDB = await context.AttributeProducts
		.Where(x => x.ProductId == productId)
		.AsNoTracking()
		.ToListAsync();

	//2 - 3 - 4
	context.TryUpdateManyToMany(
		productAttributesFromDB,
		new List<AttributeProduct>
		{
			new AttributeProduct { ProductId = productId, AttributeId = 2 },
			new AttributeProduct { ProductId = productId, AttributeId = 3 },
			new AttributeProduct { ProductId = productId, AttributeId = 4 },
		},
		x => x.AttributeId
		);
	await context.SaveChangesAsync();
}

// 4) Usuń dowolnego klienta z bazy danych za pomocą Entity Framework Core.
using (AppDbContext context = new())
{
	context.Customers.Remove(new Customer { CustomerId = 3 });
	await context.SaveChangesAsync();
}

// 5) Dodaj 3000 nowych produktów do bazy danych.
List<Product> products = new();
for (int i = 0; i < 3000; i++)
{
	products.Add(new Product { Name = $"ProductName{i}", Price = i + 10 });
}
using (AppDbContext context = new())
{
	//context.AddRange(products);
	await context.BulkInsertAsync(products);
}

// 6) Wszystkim produktom w bazie danych zwiększ cenę o 10.
using (AppDbContext context = new())
{
	var products2 = await context.Products.ToListAsync();
	products2.ForEach(x => x.Price += 10);
	await context.BulkUpdateAsync(products2);
}

// 7) Usuń wszystkie produkty z bazy danych, które wcześniej dodałeś.
using (AppDbContext context = new())
{
	var productsToDelete = await context.Products.Where(x => x.ProductId > 6).ToListAsync();
	//context.RemoveRange(productsToDelete);
	//await context.SaveChangesAsync();
	await context.BulkDeleteAsync(productsToDelete);
}

// 8) Obsłuż miękkie usuwanie klientów i usuń w ten sposób dowolnego klienta.
using (AppDbContext context = new())
{
	var customerToDelete = await context.Customers.FindAsync(5);
	customerToDelete.IsDeleted = true;
	await context.SaveChangesAsync();
}

// 9) Stwórz i wywołaj nowy widok, który na podstawie przekazanego id będzie zwracał wszystkie informacje o kliencie i jego adresie.
using (AppDbContext context = new())
{
	var customersWithAddress = await context.CustomerAddressView.ToListAsync();
	foreach (var item in customersWithAddress)
	{
		Console.WriteLine($"{item.CustomerId} {item.Name} {item.Nip} {item.Email} {item.PhoneNumber} - {item.Address}");
	}
}

// 10) Stwórz i wywołaj procedurę, która będzie usuwała produkt na podstawie przekazanego id.
using (AppDbContext context = new())
{
	int value1 = 666;
	await context.Database.ExecuteSqlInterpolatedAsync($"DeleteProduct {value1}");
}