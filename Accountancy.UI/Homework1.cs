using Accountancy.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace Accountancy.UI;

internal class Homework1
{
	public static async Task StartHomework1()
	{
		using (AppDbContext context = new())
		{
			Console.WriteLine("1) Na początek skonfiguruj odpowiednio naszą aplikację, tak aby wszystkie zapytania na bazie danych były wyświetlane w konsoli oraz zapisywane do pliku. Odblokuj również szczegółowe dane o zapytaniach.");
			
			Console.WriteLine("\n2) Spróbuj dodać póki co jeszcze bezpośrednio w SQL Management Server kilka rekordów do bazy danych.\n");
			
			Console.WriteLine("\n3) Napisz pierwszą kwerendę, która pobierze wszystkie faktury, dołączając właściwości nawigacyjne, czyli wszystkie informacje o kliencie wraz z adresem oraz o pozycjach dla każdej faktury bez śledzenie danych.\n");
			var invoices = await context.Invoices
				.Include(x => x.Customer)
				.ThenInclude(x => x.Address)
				.Include(x => x.InvoicePositions)
				.AsNoTracking()
				.ToListAsync();

			Console.ReadKey(true);
			Console.WriteLine("\n4) Pobierz produkty, których cena jest z przedziału od 100 do 200 wraz z atrybutami, ale tylko takimi, które mają Id większe niż 5 bez śledzenie danych.\n");
			context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			var products = await context.Products
				.Where(x => x.Price > 100 && x.Price < 200)
				.Include(x => x.Attributes.Where(x => x.AttributeId > 5))
				.ToListAsync();

			Console.ReadKey(true);
			Console.WriteLine("\n5) Pobierz pełne numery wszystkich faktur, to znaczy Numer/Miesiac/Rok wraz z ich średnią wartością dla 1 pozycji faktury.\n");
			var invoicesWithAvg1ProductPrice = await context.Invoices
				.Include(x => x.InvoicePositions)
				.ThenInclude(x => x.Product)
				.Select(x => new
				{
					InvoiceNumber = $"{x.Number}/{x.Month}/{x.Year}",
					Avg1ProductPrice = x.InvoicePositions.Select(x => x.Product.Price).Average()
				})
				.ToListAsync();

			Console.ReadKey(true);
			Console.WriteLine("\n6) Pobierz wszystkie atrybuty, których nazwa zaczyna się od „a” oraz kończy się na „z”.\n");
			var attributesFromAToZ = await context.Attributes
				.Where(x => x.Name.ToUpper().StartsWith("A") && x.Name.ToUpper().EndsWith("Z"))
				.ToListAsync();

			Console.ReadKey(true);
			Console.WriteLine("\n7) Pobierz wszystkich klientów, którzy nie posiadają nipu lub telefonu i posortuj po nazwie.\n");
			var customersWithoutNipOrPhone = await context.Customers
				.Where(x => string.IsNullOrWhiteSpace(x.Nip) || string.IsNullOrWhiteSpace(x.PhoneNumber))
				.OrderBy(x => x.Name)
				.ToListAsync();

			Console.ReadKey(true);
			Console.WriteLine("\n8) Pobierz wszystkie faktury, które nie zostały zapłacone, a ich wartość całkowita jest większa niż 1000.\n");
			var unpaidInvoices = await context.Invoices
				.Where(x => x.IsPaid == false && x.TotalPrice > 1000)
				.ToListAsync();
		}
	}
}