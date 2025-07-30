using Blog.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace Blog.UI.EntityFrameworkCore;

// Kwerendy - zapytania na bazie danych, które zwracają różne dane *************************************************************************************
internal class EFCoreQueries
{
	// Podstawowe zapytania ****************************************************************************************************************************
	public async Task Basics()
	{
		using (AppDbContext context = new())
		{
			var posts = context.Posts;
			foreach (var post in posts)
			{
				Console.WriteLine(post.Title);
			}

			// EF Core ma metodę, której nie ma w Linq - Find() - pobiera obiekt na podstawie przekazanego ID
			var userWithId3 = context.Users.Find(3);
			Console.WriteLine(userWithId3.Login);

			// metody asynchroniczne
			var users = await context.Users.ToListAsync();
			foreach (var item in users)
			{
				Console.WriteLine(item.Login);
			}
		}
	}

	// Podgląd zapytań na bazie danych *********************************************************************************************************************
	public void DatabaseQueryPreview()
	{
		// Podgląd zapytań w aplikacji SQL Server Profiler:
		using (AppDbContext context = new())
		{
			var users = context.Users.TagWith("Get all users");
		}

		// Można wyświetlić zapytania w konsoli.

		// Można też zapisywać zapytania SQL w pliku za pomocą loggera.
		// NuGety: NLog + NLog.Extensions.Logging instalowane w projekcie Blog.DataLayer + plik konfiguracyjny nlog.config (pamiętać żeby ustawiać go jako "Copy if newer")
		// w klasie AppDbContext pole z fabryką NLoga: public static readonly ILoggerFactory _loggerFactory = new NLogLoggerFactory();

		// W AppDbContext w metodzie OnConfiguring:
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder
		//
		//      // Dodatkowa konfiguracja, żeby wyświetlić wszystkie przekazane parametry zapytania:
		//		.EnableSensitiveDataLogging()
		//
		//      // Logowanie zapytań do konsoli:
		//		.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
		//
		//		// Logowanie za pomocą NLog do pliku:
		//		.UseLoggerFactory(_loggerFactory);
		//}
	}

	// W EFCore są 3 rodzaje ładowania danych:
	// Lazy loading - domyślnie zablokowane
	// Eager Loading - za pomocą Include() i ThenInclude()
	// Explicit Loading - ładowanie jawne

	// wstępne/zachłanne ładowanie danych - tworzy 1 większe zapytanie pobierające informacje o encjach powiązanych ******************************************
	public void EagerLoading()
	{
		// ThenInclude - pobranie informacji o tabelach powiązanych z tabelą dołączoną przez Include
		using (AppDbContext context = new())
		{
			var posts = context.Posts
				.Include(x => x.User)
				.ThenInclude(x => x.PostsApproved.Where(x => x.UserId > 1))
				.Include(x => x.ApprovedBy)
				.ToList();
			foreach (var item in posts)
			{
				Console.WriteLine(item.User.Login);
			}
		}
	}

	// Explicit Loading - ładowanie jawne *******************************************************************************************************************
	public void ExplicitLoading()
	{
		using (AppDbContext context = new())
		{
			var post = context.Posts.FirstOrDefault();

			// ładowanie 1 elementu
			context.Entry(post).Reference(x => x.User).Load();

			// ładowanie kolekcji
			context.Entry(post).Collection(x => x.Tags).Load();
			context.Entry(post).Collection(x => x.Tags).Query().Where(x => x.Id > 3).Load();

			Console.WriteLine(post.User.Login);
		}
	}

	// Śledzenie zniam obiektów *****************************************************************************************************************************
	public void ChangeTracking()
	{
		// wyłączenie śledzenia zmian
		using (AppDbContext context = new())
		{
			// wyłączenie śledzenia zmian dla całego kontekstu:
			context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

			// wyłączenie śledzenia zmian dla pojedynczego zapytania:
			var posts = context.Posts.AsNoTracking();

			// można dostać się do kolekcji elementów które są śledzone:
			DisplayEntriesInfo(context);
		}

		// Można też wyłączyć śledzenie zmian dla całego contextu - AppDbContext w metodzie OnConfiguring:
		//optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
	}

	// Nietypowe zapytania + czysty SQL ************************************************************************************************************************
	public void RawSql()
	{
		using (AppDbContext context = new())
		{
			// można używać niektórych metod np. string.ToUpper():
			var posts = context.Posts.Where(x => x.Title.ToUpper() == "TITLE 7");

			// parametryzowany raw sql - niezalecany - podatny na ataki SQL Injecition
			var title = "Title 7";
			var posts1 = context.Posts.FromSqlRaw("SELECT * FROM Posts2 WHERE Title2={0}", title).ToList();

			// parametryzowany interpolated zabezpiecza przed SQL Injecition
			var posts2 = context.Posts.FromSqlInterpolated($"SELECT * FROM Posts2 WHERE Title2={title}").ToList();
			foreach (var item in posts2)
			{
				Console.WriteLine(item.Title);
			}

			var posts3 = context.Customs.FromSqlRaw("SELECT Description as FullDescription FROM Posts2").ToList();
			foreach (var item in posts3)
			{
				Console.WriteLine(item.FullDescription);
			}

			// żeby zwrócić nietypowe kolumny które nie pasują do żadnej encji domenowej:
			// - zrobić dodatkową klasę (tutaj w Blog.Domain -> Entities -> Query -> Custom.cs)
			// - dodać klase konfiguracyjną z wpisem: builder.HasNoKey().ToView("Custom"); - to dlatego, żeby nie tworzyć takiej tabeli w bazie danych
			// - w AppDbContext trzeba dodać nowy DbSet
			var customItems = context.Customs.FromSqlRaw("SELECT Description as FullDescription FROM Posts2").ToList();
			foreach (var item in customItems)
			{
				Console.WriteLine(item.FullDescription);
			}
		}
	}

	private static void DisplayEntriesInfo(AppDbContext context)
	{
		foreach (var item in context.ChangeTracker.Entries())
		{
			Console.WriteLine($"Encja: {item.Entity.GetType().Name}, Stan: {item.State}");
		}
	}
}