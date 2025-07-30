namespace Blog.UI.Linq;

internal class Linq
{
	private readonly List<Author> _authors =
	[
		new() { AuthorId = 1, Name = "Author1" },
		new() { AuthorId = 2, Name = "Author2" },
		new() { AuthorId = 3, Name = "Author3" },
	];

	private readonly List<Book> _books =
	[
		new() { Id = 1, Title = "Jak zostać programistą", Price = 20.1m, AuthorId = 1 },
		new() { Id = 2, Title = "Programista C#", Price = 20.71m, AuthorId = 2 },
		new() { Id = 3, Title = "Entity Framework Core", Price = 54m, AuthorId = 3 },
		new() { Id = 4, Title = "Testy jednostkowe", Price = 32m, AuthorId = 1 },
		new() { Id = 5, Title = "Pigułka wiedzy C#", Price = 120.12m, AuthorId = 2 },
		new() { Id = 6, Title = "Platforma .NET", Price = 20.1m, AuthorId = 2 },
	];

	// Pierwsze zapytania, filtrowanie i sortowanie danych, projektowanie wyników zapytań ******************************************************************
	public void Basics()
	{
		List<int> numbers = [0, 8, 9, 13, 22, 31, 1, 5, 3, 7, 99, 50, 314];

		var evenNumberQuerySyntax = from x in numbers
									where x % 2 == 0
									select x;

		Console.WriteLine("Query Syntax:");
		foreach (var item in evenNumberQuerySyntax)
		{
			Console.Write($"{item}, ");
		}

		var evenNumberMethodSyntax = numbers.Where(x => x % 2 == 0);
		Console.WriteLine("\nMethod Syntax:");
		foreach (var item in evenNumberMethodSyntax)
		{
			Console.Write($"{item}, ");
		}

		var booksCheaperThan50 = _books.Where(x => x.Price < 50);
		foreach (var item in booksCheaperThan50)
		{
			Console.WriteLine(item.Title);
		}

		Console.WriteLine();

		var isAnyDotNetBookCheaperThan50 = _books.Any(x => x.Price < 50 && x.Title.Contains(".NET"));
		Console.WriteLine(isAnyDotNetBookCheaperThan50);

		Console.WriteLine();

		var isAllBooksIdGreaterThan0 = _books.All(x => x.Id > 0);
		Console.WriteLine(isAllBooksIdGreaterThan0);

		Console.WriteLine();

		var csharpBooksOrderedByPrice = _books
			.Where(x => x.Title.Contains("C#"))
			.OrderBy(x => x.Price);
		foreach (var item in csharpBooksOrderedByPrice)
		{
			Console.WriteLine($"{item.Title} - {item.Price} zł");
		}

		Console.WriteLine();

		var booksOrderedByPriceAndId = _books
			.OrderBy(x => x.Price)
			.ThenBy(x => x.Id);
		foreach (var item in booksOrderedByPriceAndId)
		{
			Console.WriteLine($"{item.Title} - {item.Price} zł");
		}

		Console.WriteLine();

		var csharpBooksCheaperThan50 = _books.Where(x => x.Price < 50 && x.Title.Contains("C#"));
		foreach (var item in csharpBooksCheaperThan50)
		{
			Console.WriteLine(item.Title);
		}

		Console.WriteLine();

		// Różnice między First a Single

		// First - zwróci 1 element, nawet jak warunek spełnia więcej elementów, rzuca wyjątek,
		//			jeżeli żaden element nie spełnia warunku lub kolekcja pusta
		// FirstOrDefault - jedyna różnica, że w razie braku elementów zwraca domyślną wartość dla danego typu

		// Single - zwróci 1 element, wtedy gdy warunek spełnia tylko 1 element, w przeciwnym wypadku rzuca wyjątek że więcej niż 1 element spełnia podany warunek
		//			jeżeli żaden element nie spełnia warunku lub kolekcja pusta
		// SingleOrDefault - jedyna różnica, że w razie braku elementów zwraca domyślną wartość dla danego typu

		var bookPrice2 = _books.First(x => x.Price == 2);
		var bookPrice3 = _books.First(x => x.Price == 3);

		// są też metody Last i LastOrDefault

		Console.WriteLine();

		// projekcja wyników - Select

		var booksTitleWithIdGreaterThan5 = _books
			.Where(x => x.Id > 5)
			.Select(x => new MyBook { Info = $"{x.Id} - {x.Title}" });
		foreach (var item in booksTitleWithIdGreaterThan5)
		{
			Console.WriteLine(item.Info);
		}

		// użycie obiektu anonimowego
		var booksTitleWithIdGreaterThan5_2 = _books
			.Where(x => x.Id > 5)
			.Select(x => new { Something = $"{x.Id} - {x.Title}" });
		foreach (var item in booksTitleWithIdGreaterThan5_2)
		{
			Console.WriteLine(item.Something);
		}

		// Distinct + BookComparer
		var distinctBooks = _books
			.Where(x => x.Id > 1)
			.OrderBy(x => x.Price)
			.Distinct(new BookComparer());
	}

	// Złączenia kolekcji **************************************************************************************************************************
	public void CollectionJoins()
	{
		// lista bazowa - books
		// dołączenie kolekcji - Join
		//	1) wskazanie dołączanej kolekcji
		//	2) wskazanie klucza obcego w kolekcji bazowej (od której się startuje)
		//	3) wskazanie klucza głównego kolekcji dołączanej
		//	4) projekcja wyników
		var booksWithAuthorsName = _books
			.Join(
				_authors,
				book => book.AuthorId,
				author => author.AuthorId,
				(book, author) => new { book.Title, Author = author.Name })
			.OrderBy(x => x.Title);

		foreach (var item in booksWithAuthorsName)
		{
			Console.WriteLine($"{item.Title} - {item.Author}");
		}

		List<Book> booksRead =
		[
			new() { Id = 1, Title = "Jak zostać programistą", Price = 20.1m, AuthorId = 1 },
			new() { Id = 2, Title = "Programista C#", Price = 20.71m, AuthorId = 2 },
			new() { Id = 12, Title = "Kodowanie", Price = 54m, AuthorId = 3 },
		];

		BookComparer bookComparer = new();

		// Except - zwraca nową kolekcję, zawierającą elementy z 1 kolekcji, które nie istnieją w drugiej
		var booksNotRead = _books.Except(booksRead, bookComparer);
		Console.WriteLine();
		foreach (var item in booksNotRead)
		{
			Console.WriteLine(item.Title);
		}

		// Intersect - zwraca nową kolekcję, zawierajacą wspólne elementy obu podanych kolekcji
		var commonBooks = _books.Intersect(booksRead, bookComparer);
		Console.WriteLine();
		foreach (var item in commonBooks)
		{
			Console.WriteLine(item.Title);
		}

		// Union - zwraca nową kolekcję, zawierajacą wszystkie elementy z obu kolekcji, ale bez powtórzeń,
		// czyli jeśli jakiś element istnieje w obu kolekcjach to zostanie zwrócony tylko 1 raz
		var allDistinctBooks = _books.Union(booksRead, bookComparer);
		Console.WriteLine();
		foreach (var item in allDistinctBooks)
		{
			Console.WriteLine(item.Title);
		}
	}

	// Operacje agregacji danych (Max, Min, Average, Sum, Count) *********************************************************************************************
	public void DataAggregationOperations()
	{
		var mostExpensiveBook = _books.Max(x => x.Price);
		Console.WriteLine(mostExpensiveBook);
	}

	// Grupowanie danych ********************************************************************************************************************************
	public void DataGrouping()
	{
		var authorsWithBooks = _books
			.Join(
				_authors,
				book => book.AuthorId,
				author => author.AuthorId,
				(book, author) => new { Book = book, Author = author })
			// wskazanie po którym polu ma być grupowanie danych
			.GroupBy(x => x.Book.AuthorId)
			// projekcja wyników
			.Select(x => new
			{
				// Key to klucz grupowania
				AuthorId = x.Key,
				// złączenie wszystkich tytułów książek danego autora
				Titles = string.Join(", ", x.Select(x => x.Book.Title)),
				AuthorName = x.First().Author.Name
			});
		foreach (var item in authorsWithBooks)
		{
			Console.WriteLine($"Author: {item.AuthorName}{Environment.NewLine}Książki: {item.Titles}.{Environment.NewLine}{Environment.NewLine}");
		}
	}

	// Paginacja danych ********************************************************************************************************************************
	public void DataPagination()
	{
		// paginacja danych - wyświetlanie ograniczonej ilości rekordów; przydatne do ładowania tylko pojedynczej strony w tabelce
		int pageSize = 5;
		int pageNumber = 2;
		var paginationBooks = _books
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize);
		foreach (var item in paginationBooks)
		{
			Console.WriteLine(item.Title);
		}
	}

	// Homework ********************************************************************************************************************************
	// 1) Pobierz wszystkich studentów, którzy mają więcej niż 25 lat.
	// 2) Sprawdź czy istnieje jakiś student, który ma więcej niż 40 lat.
	// 3) Pobierz wszystkich studentów o imieniu Marcin.
	// 4) Pobierz tylko nazwę każdego studenta i posortuj ich najpierw po nazwie, a następnie malejąco po Id
	// 5) Pobierz do jednej kolekcji informacji o nazwie studenta i opisie jego grupy.
	// 6) Wyświetl średnią wieku wszystkich studentów.
	// 7) Pogrupuj studentów po grupie i wyświetl wszystkich studentów należącej do danej grupy po przecinku.
	// 8) Zastosuj paginacje i wyświetl 2 stronę z listą studentów zawierającą 10 rekordów.
	public void Homework()
	{
		List<Group> groups =
		[
			new() { GroupId = 1, Description = "Grupa 1"},
			new() { GroupId = 2, Description = "Grupa 2"},
			new() { GroupId = 3, Description = "Grupa 3"},
		];

		List<Student> students =
		[
			new() { StudentId = 1, Name = "Jan", Age = 20, GroupId = 1 },
			new() { StudentId = 2, Name = "Bożena", Age = 20, GroupId = 1 },
			new() { StudentId = 3, Name = "Marcin", Age = 21, GroupId = 2 },
			new() { StudentId = 4, Name = "Rafał", Age = 22, GroupId = 1 },
			new() { StudentId = 5, Name = "Paweł", Age = 27, GroupId = 1 },
			new() { StudentId = 6, Name = "Marcin", Age = 26, GroupId = 3 },
			new() { StudentId = 7, Name = "Anna", Age = 26, GroupId = 3 },
			new() { StudentId = 8, Name = "Władek", Age = 24, GroupId = 2 },
			new() { StudentId = 9, Name = "Franek", Age = 18, GroupId = 2 },
			new() { StudentId = 10, Name = "Grażyna", Age = 44, GroupId = 3 },
			new() { StudentId = 11, Name = "Halina", Age = 33, GroupId = 2 },
			new() { StudentId = 12, Name = "Sebastian", Age = 22, GroupId = 1 },
			new() { StudentId = 13, Name = "Wojtek", Age = 19, GroupId = 2 },
			new() { StudentId = 14, Name = "Dżesika", Age = 20, GroupId = 3 },
			new() { StudentId = 15, Name = "Brajan", Age = 29, GroupId = 2 },
		];

		Console.WriteLine("1) Pobierz wszystkich studentów, którzy mają więcej niż 25 lat.");
		var answer1 = students.Where(x => x.Age > 25);
		foreach (var student in answer1)
		{
			Console.WriteLine($"{student.StudentId}. {student.Name} - {student.Age} lat.");
		}

		Console.WriteLine("\n2) Sprawdź czy istnieje jakiś student, który ma więcej niż 40 lat.");
		var answer2 = students.Any(x => x.Age > 40);
		Console.WriteLine(answer2);

		Console.WriteLine("\n3) Pobierz wszystkich studentów o imieniu Marcin.");
		var answer3 = students.Where(x => x.Name.Contains("Marcin"));
		foreach (var student in answer3)
		{
			Console.WriteLine($"{student.StudentId}. {student.Name} - {student.Age} lat.");
		}

		Console.WriteLine("\n4) Pobierz tylko nazwę każdego studenta i posortuj ich najpierw po nazwie, a następnie malejąco po Id");
		var answer4 = students
			.OrderBy(x => x.Name)
			.ThenByDescending(x => x.StudentId)
			.Select(x => x.Name);
		foreach (var student in answer4)
		{
			Console.WriteLine($"{student}");
		}

		Console.WriteLine("\n5) Pobierz do jednej kolekcji informacji o nazwie studenta i opisie jego grupy.");
		var answer5 = students
			.Join(
				groups,
				student => student.GroupId,
				group => group.GroupId,
				(student, group) => new { student.Name, Group = group.Description }
			);
		foreach (var student in answer5)
		{
			Console.WriteLine($"{student.Name} - {student.Group}");
		}

		Console.WriteLine("\n6) Wyświetl średnią wieku wszystkich studentów.");
		var answer6 = students.Average(x => x.Age);
		Console.WriteLine(answer6);

		Console.WriteLine("\n7) Pogrupuj studentów po grupie i wyświetl wszystkich studentów należącej do danej grupy po przecinku.");
		var answer7 = students
			.Join(
				groups,
				s => s.GroupId,
				g => g.GroupId,
				(s, g) => new { Student = s, Group = g })
			.GroupBy(x => x.Student.GroupId)
			.Select(x => new
			{
				GroupDesc = x.First().Group.Description,
				StudentsName = string.Join(", ", x.Select(x => x.Student.Name))
			});
		foreach (var student in answer7)
		{
			Console.WriteLine($"{student.GroupDesc} - {student.StudentsName}");
		}

		Console.WriteLine("\n8) Zastosuj paginacje i wyświetl 2 stronę z listą studentów zawierającą 10 rekordów.");
		int pageSize = 10;
		int pageNumber = 2;
		var answer8 = students
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize);
		foreach (var student in answer8)
		{
			Console.WriteLine($"{student.StudentId}. {student.Name} - {student.Age} lat.");
		}
	}
}