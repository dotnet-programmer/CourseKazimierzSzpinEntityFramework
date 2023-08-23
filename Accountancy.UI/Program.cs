using Accountancy.DataLayer;
using Microsoft.EntityFrameworkCore;

using (AppDbContext context = new())
{
	Console.WriteLine("3) Napisz pierwszą kwerendę, która pobierze wszystkie faktury, dołączając właściwości nawigacyjne,");
	var invoices = await context.Invoices
		.Include(x => x.Customer)
		.ThenInclude(x => x.Address)
		.Include(x => x.InvoicePositions)
		.AsNoTracking()
		.ToListAsync();

	Console.WriteLine("4) Pobierz produkty, których cena jest z przedziału od 100 do 200 wraz z atrybutami, ale tylko takimi, które mają Id większe niż 5 bez śledzenie danych.");
	var products = await context.Products
		.Where(x => x.Price > 100 && x.Price < 200)
		.Include(x => x.Attributes.Where(x => x.AttributeId > 5))
		.AsNoTracking()
		.ToListAsync();

	Console.WriteLine("5) Pobierz pełne numery wszystkich faktur, to znaczy Numer/Miesiac/Rok wraz z ich średnią wartością dla 1 pozycji faktury.");
	var invoicesWithAvg1ProductPrice = await context.Invoices
		.Include(x => x.InvoicePositions)
		.ThenInclude(x => x.Product)
		.Select(x => new
		{
			InvoiceNumber = $"{x.Number}/{x.Month}/{x.Year}",
			Avg1ProductPrice = x.InvoicePositions.Select(x => x.Product.Price).Average()
		})
		.ToListAsync();

	Console.WriteLine("6) Pobierz wszystkie atrybuty, których nazwa zaczyna się od „a” oraz kończy się na „z”.");
	var attributesFromAToZ = await context.Attributes
		.Where(x => x.Name.ToUpper().StartsWith('A') && x.Name.ToUpper().EndsWith('Z'))
		.ToListAsync();

	Console.WriteLine("7) Pobierz wszystkich klientów, którzy nie posiadają nipu lub telefonu i posortuj po nazwie.");
	var customersWithoutNipOrPhone = await context.Customers
		.Where(x => string.IsNullOrWhiteSpace(x.Nip) || string.IsNullOrWhiteSpace(x.PhoneNumber))
		.OrderBy(x => x.Name)
		.ToListAsync();

	Console.WriteLine("8) Pobierz wszystkie faktury, które nie zostały zapłacone, a ich wartość całkowita jest większa niż 1000.");
	var unpaidInvoices = await context.Invoices
		.Where(x => x.IsPaid == false && x.TotalPrice > 1000)
		.ToListAsync();
}