//var numbers = new List<int> { 0, 8, 9, 13, 22, 31, 1, 5, 3, 7, 99, 50, 314 };

//var evenNumberQuerySyntax = from x in numbers
//							where x % 2 == 0
//							select x;

//Console.WriteLine("Query Syntax:");
//foreach (var item in evenNumberQuerySyntax)
//{
//    Console.Write($"{item}, ");
//}

//var evenNumberMethodSyntax = numbers.Where(x => x % 2 == 0);
//Console.WriteLine("\nMethod Syntax:");
//foreach (var item in evenNumberMethodSyntax)
//{
//	Console.Write($"{item}, ");
//}

//List<Book> books = new()
//{
//	new Book{ Id = 1, Title = "Jak zostać programistą", Price = 20.1m },
//	new Book{ Id = 2, Title = "Programista C#", Price = 20.71m },
//	new Book{ Id = 3, Title = "Entity Framework Core", Price = 54m },
//	new Book{ Id = 4, Title = "Testy jednostkowe", Price = 32m },
//	new Book{ Id = 5, Title = "Pigułka wiedzy C#", Price = 120.12m },
//	new Book{ Id = 6, Title = "Platforma .NET", Price = 20.1m },
//};

//var booksCheaperThan50 = books.Where(x => x.Price < 50);
//foreach (var item in booksCheaperThan50)
//{
//    Console.WriteLine(item.Title);
//}

//Console.WriteLine();

//var isAnyDotNetBookCheaperThan50 = books.Any(x => x.Price < 50 && x.Title.Contains(".NET"));
//Console.WriteLine(isAnyDotNetBookCheaperThan50);

//Console.WriteLine();

//var isAllBooksIdGreaterThan0 = books.All(x => x.Id > 0);
//Console.WriteLine(isAllBooksIdGreaterThan0);

//Console.WriteLine();

//var csharpBooksOrderedByPrice = books
//	.Where(x => x.Title.Contains("C#"))
//	.OrderBy(x => x.Price);
//foreach (var item in csharpBooksOrderedByPrice)
//{
//	Console.WriteLine($"{item.Title} - {item.Price} zł);
//}

//Console.WriteLine();

//var booksOrderedByPriceAndId = books
//	.OrderBy(x => x.Price)
//  .ThenBy(x => x.Id);
//foreach (var item in booksOrderedByPriceAndId)
//{
//	Console.WriteLine($"{item.Title} - {item.Price} zł);
//}

//Console.WriteLine();

//var csharpBooksCheaperThan50 = books.Where(x => x.Price < 50 && x.Title.Contains("C#"));
//foreach (var item in csharpBooksCheaperThan50)
//{
//	Console.WriteLine(item.Title);
//}

//Console.WriteLine();

// Różnice między First a Single

// First - zwróci 1 element, nawet jak warunek spełnia więcej elementów, rzuca wyjątek,
//			jeżeli żaden element nie spełnia warunku lub kolekcja pusta
// FirstOrDefault - jedyna różnica, że w razie braku elementów zwraca domyślną wartość dla danego typu

// Single - zwróci 1 element, wtedy gdy warunek spełnia tylko 1 element, w przeciwnym wypadku rzuca wyjątek że więcej niż 1 element spełnia podany warunek
//			jeżeli żaden element nie spełnia warunku lub kolekcja pusta
// SingleOrDefault - jedyna różnica, że w razie braku elementów zwraca domyślną wartość dla danego typu

//var bookPrice2 = books.First(x => x.Price == 2);
//var bookPrice3 = books.First(x => x.Price == 3);

// są też metody Last i LastOrDefault

//Console.WriteLine();

//var booksTitleWithIdGreaterThan5 = books
//	.Where(x => x.Id > 5)
//	.Select(x => new MyBook { Info = $"{x.Id} - {x.Title}" });
//foreach (var item in booksTitleWithIdGreaterThan5)
//{
//	Console.WriteLine(item.Info);
//}

// użycie obiektu anonimowego
//var booksTitleWithIdGreaterThan5_2 = books
//	.Where(x => x.Id > 5)
//	.Select(x => new { Something = $"{x.Id} - {x.Title}" });
//foreach (var item in booksTitleWithIdGreaterThan5_2)
//{
//	Console.WriteLine(item.Something);
//}

//class Book
//{
//	public int Id { get; set; }
//	public string Title { get; set; }
//	public decimal Price { get; set; }
//}

//class MyBook
//{
//    public string Info { get; set; }
//}

// ***************************************************************************************************************

//using System.Diagnostics.CodeAnalysis;

//List<Author> authors = new()
//{
//	new Author{ AuthorId = 1, Name = "Author1" },
//	new Author{ AuthorId = 2, Name = "Author2" },
//	new Author{ AuthorId = 3, Name = "Author3" },
//};

//List<Book> books = new()
//{
//	new Book{ Id = 1, Title = "Jak zostać programistą", Price = 20.1m, AuthorId = 1 },
//	new Book{ Id = 2, Title = "Programista C#", Price = 20.71m, AuthorId = 2 },
//	new Book{ Id = 3, Title = "Entity Framework Core", Price = 54m, AuthorId = 3 },
//	new Book{ Id = 4, Title = "Testy jednostkowe", Price = 32m, AuthorId = 1 },
//	new Book{ Id = 5, Title = "Pigułka wiedzy C#", Price = 120.12m, AuthorId = 2 },
//	new Book{ Id = 6, Title = "Platforma .NET", Price = 20.1m, AuthorId = 2 },
//};

// lista bazowa - books
// dołączenie kolekcji - Join
//	1) wskazanie dołączanej kolekcji
//	2) wskazanie klucza obcego w kolekcji bazowej (od której się startuje)
//	3) wskazanie klucza głównego kolekcji dołączanej
//	4) projekcja wyników
//var booksWithAuthorsName = books.Join(
//									  authors,
//									  book => book.AuthorId,
//									  author => author.AuthorId,
//									  (book, author) => new { Title = book.Title, Author = author.Name })
//								.OrderBy(x => x.Title);

//foreach (var book in booksWithAuthorsName)
//{
//    Console.WriteLine($"{book.Title} - {book.Author}");
//}

//List<Book> readBooks = new()
//{
//	new Book{ Id = 1, Title = "Jak zostać programistą", Price = 20.1m, AuthorId = 1 },
//	new Book{ Id = 2, Title = "Programista C#", Price = 20.71m, AuthorId = 2 },
//	new Book{ Id = 3, Title = "Entity Framework Core", Price = 54m, AuthorId = 3 },
//};

// Except - zwraca nową kolekcję, zawierającą elementy z 1 kolekcji, które nie istnieją w drugiej
//var booksNotRead = books.Except(readBooks, new BookComparer());
//Console.WriteLine();
//foreach (var item in booksNotRead)
//{
//    Console.WriteLine(item.Title);
//}

// Intersect - zwraca nową kolekcję, zawierajacą wspólne elementy obu podanych kolekcji
//var commonBooks = books.Intersect(readBooks, new BookComparer());
//Console.WriteLine();
//foreach (var item in commonBooks)
//{
//	Console.WriteLine(item.Title);
//}

// Union - zwraca nową kolekcję, zawierajacą wszystkie elementy z obu kolekcji, ale bez powtórzeń,
// czyli jeśli jakiś element istnieje w obu kolekcjach to zostanie zwrócony tylko 1 raz
//var allDistinctBooks = books.Union(readBooks, new BookComparer());
//Console.WriteLine();
//foreach (var item in allDistinctBooks)
//{
//	Console.WriteLine(item.Title);
//}

//internal class Author
//{
//	public int AuthorId { get; set; }
//	public string Name { get; set; }
//}

//internal class Book
//{
//	public int Id { get; set; }
//	public string Title { get; set; }
//	public decimal Price { get; set; }
//	public int AuthorId { get; set; }
//}

// używany np jako argument wywołania Distinct()
//class BookComparer : IEqualityComparer<Book>
//{
//	public bool Equals(Book? x, Book? y) => x.Id == y.Id;
//	public int GetHashCode([DisallowNull] Book obj) => obj.Id.GetHashCode();
//}

// ***************************************************************************************************************

//var authorsWithBooks = books.Join(
//	authors,
//	book => book.AuthorId,
//	author => author.Id,
//	(book, author) => new
//	{
//		Book = book,
//		Author = author
//	})
//	.GroupBy(x => x.Book.AuthorId)
//	.Select(x => new
//	{
//		AuthorId = x.Key,
//		Titles = string.Join(", ", x.Select(x => x.Book.Title)),
//		AuthorName = x.First().Author.Name
//	});
//foreach (var item in authorsWithBooks)
//{
//    Console.WriteLine($"Author: {item.AuthorName}{Environment.NewLine}Książki: {item.Titles}.{Environment.NewLine}{Environment.NewLine}");
//}

// ***************************************************************************************************************

// paginacja danych - wyświetlanie ograniczonej ilości rekordów; przydatne do ładowania tylko pojedynczej strony w tabelce
//int pageSize = 5;
//int pageNumber = 2;
//var paginationBooks = books.Skip((pageNumber - 1) * pageSize).Take(pageSize);

// ***************************************************************************************************************

// homework

//List<Student> students = new()
//{
//	new Student { StudentId = 1, Name = "Jan", Age = 20, GroupId = 1 },
//	new Student { StudentId = 2, Name = "Bożena", Age = 20, GroupId = 1 },
//	new Student { StudentId = 3, Name = "Marcin", Age = 21, GroupId = 2 },
//	new Student { StudentId = 4, Name = "Rafał", Age = 22, GroupId = 1 },
//	new Student { StudentId = 5, Name = "Paweł", Age = 27, GroupId = 1 },
//	new Student { StudentId = 6, Name = "Marcin", Age = 26, GroupId = 3 },
//	new Student { StudentId = 7, Name = "Anna", Age = 26, GroupId = 3 },
//	new Student { StudentId = 8, Name = "Władek", Age = 24, GroupId = 2 },
//	new Student { StudentId = 9, Name = "Franek", Age = 18, GroupId = 2 },
//	new Student { StudentId = 10, Name = "Grażyna", Age = 44, GroupId = 3 },
//	new Student { StudentId = 11, Name = "Halina", Age = 33, GroupId = 2 },
//	new Student { StudentId = 12, Name = "Sebastian", Age = 22, GroupId = 1 },
//	new Student { StudentId = 13, Name = "Wojtek", Age = 19, GroupId = 2 },
//	new Student { StudentId = 14, Name = "Dżesika", Age = 20, GroupId = 3 },
//	new Student { StudentId = 15, Name = "Brajan", Age = 29, GroupId = 2 },
//};

//List<Group> groups = new()
//{
//	new Group { GroupId = 1, Description = "Grupa 1"},
//	new Group { GroupId = 2, Description = "Grupa 2"},
//	new Group { GroupId = 3, Description = "Grupa 3"},
//};

//Console.WriteLine("1) Pobierz wszystkich studentów, którzy mają więcej niż 25 lat.");
//var answer1 = students.Where(x => x.Age > 25);
//foreach (var student in answer1)
//{
//	Console.WriteLine($"{student.StudentId}. {student.Name} - {student.Age} lat.");
//}

//Console.WriteLine("\n2) Sprawdź czy istnieje jakiś student, który ma więcej niż 40 lat.");
//var answer2 = students.Any(x => x.Age > 40);
//Console.WriteLine(answer2);

//Console.WriteLine("\n3) Pobierz wszystkich studentów o imieniu Marcin.");
//var answer3 = students.Where(x => x.Name.Contains("Marcin"));
//foreach (var student in answer3)
//{
//	Console.WriteLine($"{student.StudentId}. {student.Name} - {student.Age} lat.");
//}

//Console.WriteLine("\n4) Pobierz tylko nazwę każdego studenta i posortuj ich najpierw po nazwie, a następnie malejąco po Id");
//var answer4 = students
//	.OrderBy(x => x.Name)
//	.ThenByDescending(x => x.StudentId)
//	.Select(x => x.Name);
//foreach (var student in answer4)
//{
//	Console.WriteLine($"{student}");
//}

//Console.WriteLine("\n5) Pobierz do jednej kolekcji informacji o nazwie studenta i opisie jego grupy.");
//var answer5 = students.Join(
//		groups,
//		student => student.GroupId,
//		group => group.GroupId,
//		(student, group) => new { Name = student.Name, Group = group.Description }
//	);
//foreach (var student in answer5)
//{
//	Console.WriteLine($"{student.Name} - {student.Group}");
//}

//Console.WriteLine("\n6) Wyświetl średnią wieku wszystkich studentów.");
//var answer6 = students.Average(x => x.Age);
//Console.WriteLine(answer6);

//Console.WriteLine("\n7) Pogrupuj studentów po grupie i wyświetl wszystkich studentów należącej do danej grupy po przecinku.");
//var answer7 = students
//	.Join(
//		groups,
//		s => s.GroupId,
//		g => g.GroupId,
//		(s, g) => new { Student = s, Group = g })
//	.GroupBy(x => x.Student.GroupId)
//	.Select(x => new
//	{
//		GroupDesc = x.First().Group.Description,
//		StudentsName = string.Join(", ", x.Select(x => x.Student.Name))
//	});
//foreach (var student in answer7)
//{
//	Console.WriteLine($"{student.GroupDesc} - {student.StudentsName}");
//}

//Console.WriteLine("\n8) Zastosuj paginacje i wyświetl 2 stronę z listą studentów zawierającą 10 rekordów.");
//int pageSize = 10;
//int pageNumber = 2;
//var answer8 = students
//	.Skip((pageNumber - 1) * pageSize)
//	.Take(pageSize);
//foreach (var student in answer8)
//{
//	Console.WriteLine($"{student.StudentId}. {student.Name} - {student.Age} lat.");
//}

//internal class Student
//{
//	public int StudentId { get; set; }
//	public string Name { get; set; }
//	public int Age { get; set; }
//	public int GroupId { get; set; }
//}

//internal class Group
//{
//	public int GroupId { get; set; }
//	public string Description { get; set; }
//}

// ***************************************************************************************************************

//using Blog.DataLayer;
//using Microsoft.EntityFrameworkCore;

//using (AppDbContext context = new())
//{
//	var userWithId3 = context.Users.Find(3);
//	Console.WriteLine(userWithId3.Login);

//	var users = await context.Users.ToListAsync();
//	foreach (var item in users)
//	{
//		Console.WriteLine(item.Login);
//	}

//	// eager loading
// ThenInclude - pobranie informacji o tabelach powiązanych z tabelą dołączoną przez Include
//	var posts = context.Posts
//		.Include(x => x.User)
//		.ThenInclude(x => x.PostsApproved.Where(x => x.UserId > 1))
//		.Include(x => x.ApprovedBy)
//		.ToList();
//	foreach (var item in posts)
//	{
//		Console.WriteLine(item.User.Login);
//	}

//	//// explicit loading
//	//var post = await context
//	//	.Posts
//	//	.FirstOrDefaultAsync();
//	//// ładowanie 1 elementu
//	//context.Entry(post).Reference(x => x.User).Load();
//	//// ładowanie kolekcji
//	//context.Entry(post).Collection(x => x.Tags).Load();
//	//context.Entry(post).Collection(x => x.Tags).Query().Where(x => x.Id > 3).Load();
//	//Console.WriteLine(post.User.Login);

// wyłączenie śledzenia zmian
//using (var context = new AppDbContext())
//{
//	context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
//}

//	////raw sql
//	//var title = "Title 7";
//	////var posts2 = await context.Posts.FromSqlRaw("SELECT * FROM Posts2 WHERE Title2={0}", title).ToListAsync();
//	//// interpolated zabezpiecza przed sql injection
//	//var posts2 = await context.Posts.FromSqlInterpolated($"SELECT * FROM Posts2 WHERE Title2={title}").ToListAsync();
//	//foreach (var item in posts2)
//	//{
//	//	Console.WriteLine(item.Title);
//	//}

//	var posts3 = await context.Customs.FromSqlRaw("SELECT Description as FullDescription FROM Posts2").ToListAsync();
//	foreach (var item in posts3)
//	{
//		Console.WriteLine(item.FullDescription);
//	}
//}

// ***************************************************************************************************************

using Blog.DataLayer;
using Blog.DataLayer.Extensions;
using Blog.Domain.Entities;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

Category category = new()
{
	Name = "New Category 1",
	Description = "New Description 1",
	Url = "cat1"
};

using (AppDbContext context = new())
{
	context.Categories.Add(category);
	await context.SaveChangesAsync();
}

Category postCategory = new()
{
	Name = "New Category 2",
	Description = "New Description 2",
	Url = "cat2"
};
ContactInfo contactInfo = new()
{
	Email = "new@mail.1"
};
User user = new()
{
	Login = "New user 1",
	Password = "password",
	ContactInfo = contactInfo
};
Post post = new()
{
	ApprovedBy = user,
	User = user,
	Category = postCategory,
	Description = "desc1",
	PostedOn = DateTime.Now,
	Published = true,
	ShortDescription = "desc1",
	Title = "New Title 1",
	Type = Blog.Domain.Enums.PostType.Plain,
	Url = "url",
	Tags =
	[
		new Tag { Name = "tagasdf", Url = "tag-asdf" },
		new Tag { Name = "tagassdfa", Url = "tag-afdasas" }
	]
};
using (AppDbContext context = new())
{
	context.Posts.Add(post);
	DisplayEntriesInfo(context);
	await context.SaveChangesAsync();
}

Post post2 = new()
{
	ApprovedByUserId = 2, // jeśli istnieje taki user w bazie danych to można wpisać od razu jego Id
	UserId = 3,
	CategoryId = 1,
	Description = "desc2",
	PostedOn = DateTime.Now,
	Published = false,
	ShortDescription = "desc2",
	Title = "New Title 2",
	Type = Blog.Domain.Enums.PostType.Sponsored,
	Url = "url2",
};
List<PostTag> postTags =
[
	new PostTag{ TagId = 5, Post =  post2, CreatedDate = DateTime.Now }
];
using (AppDbContext context = new())
{
	context.Posts.Add(post2);

	//context.PostTags.AddRange(postTag);
	// użycie nugeta EFCore.BulkExtensions, optymalizuje dodawanie wielu rekordów do bazy danych
	await context.BulkInsertAsync(postTags);

	DisplayEntriesInfo(context);

	//używając BulkExtension nie trzeba wywoływać Save
	//await context.SaveChangesAsync();
}

static void DisplayEntriesInfo(AppDbContext context)
{
	foreach (var item in context.ChangeTracker.Entries())
	{
		Console.WriteLine($"--- Encja: {item.Entity.GetType().Name}, Stan: {item.State}");
	}
}

// edycja obiektu będącego w tym samym DbContext - ten sposób aktualizuje tylko zmienione pole w tabeli, ale wywołuje 2 zapytania, najpierw select, później update
using (AppDbContext context = new())
{
	Category category1 = await context.Categories.FindAsync(4);
	category1.Description = "y";
	DisplayEntriesInfo(context);
	await context.SaveChangesAsync();
}

// edycja obiektu z innego miejsca niż DbContext
Category category2 = null;
using (AppDbContext context = new())
{
	category2 = await context.Categories.FindAsync(4);
	category2.Description = "x";
}
using (AppDbContext context = new())
{
	// albo metoda Update - ten sposób aktualizuje wszystkie pola w tabeli
	//context.Categories.Update(category2);

	// albo dołączając stan zmodyfikowany
	context.Categories.Attach(category2).State = EntityState.Modified;

	await context.SaveChangesAsync();
}

// aktualizacja obiektów powiązanych
using (AppDbContext context = new())
{
	var post3 = await context.Posts.FindAsync(7);
	post3.ApprovedByUserId = 5;
	await context.SaveChangesAsync();
}

ContactInfo contactInfo2 = new()
{
	Email = "abc@wp.pl"
};
User user2 = new()
{
	Login = "test12345",
	Password = "password",
	ContactInfo = contactInfo2
};
using (AppDbContext context = new())
{
	var post4 = await context.Posts.FindAsync(5);
	post4.ApprovedBy = user2;
	await context.SaveChangesAsync();
}

// aktualizacja obiektów z relacją wiele:wiele
List<Tag> newTags =
[
	new Tag { Id = 1 },
	new Tag { Id = 2 },
	new Tag { Id = 3 },
	new Tag { Id = 4 },
];
using (AppDbContext context = new())
{
	var post5 = await context.Posts.FindAsync(7);

	var postTagsFromDB = await context.PostTags
		.Where(x => x.PostId == post5.Id)
		.AsNoTracking()
		.ToListAsync();

	context.TryUpdateManyToMany(
		postTagsFromDB,
		newTags.Select(x => new PostTag { TagId = x.Id, PostId = post5.Id }),
		x => x.TagId);

	await context.SaveChangesAsync();
}

// aktualizacja wielu danych jednocześnie
using (AppDbContext context = new())
{
	var tags = await context.Tags.ToListAsync();
	foreach (var item in tags)
	{
		item.Name += "1";
	}
	// wolniejszy zapis używając save z DbContext albo szybszy używając biblioteki EFCore.BulkExtensions
	//await context.SaveChangesAsync();
	await context.BulkUpdateAsync(tags);
}

// usuwanie danych
using (AppDbContext context = new())
{
	Category category1 = await context.Categories.FindAsync(10);
	// jeżeli znane jest Id obiektu to można od razu go użyć bez znajdywania wpisu w bazie danych poprzez stworzenie nowego obiektu zawierającego tylko to Id
	// context.Categories.Remowe(new Category { Id = 9 });
	context.Categories.Remove(category1);
	await context.SaveChangesAsync();
}

// usuwanie danych które są używane jako klucze obce
// usunąć najpierw wpisy powiązane a na końcu główny wpis
Category category3 = new() { Id = 4 };
using (AppDbContext context = new())
{
	var postsToDelete = await context.Posts.Where(x => x.CategoryId == category3.Id).ToListAsync();
	context.Posts.RemoveRange(postsToDelete);
	// await context.BulkDeleteAsync(postsToDelete);
	context.Categories.Remove(category3);
	await context.SaveChangesAsync();
}

// można zrobić też tzw. Soft Delete, czyli nie usuwać danych, a dodać flagę IsDeleted, która będzie oznaczana na True przy "usuwaniu" danych
// przy pobieraniu danych pamiętać o dodawaniu warunku Where(x => x.IsDeleted == False)
using (AppDbContext context = new())
{
	Category category4 = await context.Categories.FindAsync(11);
	category4.IsDeleted = true;
	await context.SaveChangesAsync();
}

// transakcje
using (AppDbContext context = new())
{
	using var transaction = await context.Database.BeginTransactionAsync();
	{
		context.Categories.Add(category);
		context.Categories.Remove(new Category { Id = 5 });
		context.Categories.Remove(new Category { Id = 999 });
		await context.SaveChangesAsync();
		await transaction.CommitAsync();
	}
}

// konflikty współbieżności
// domyślne zachowanie - jeśli kilka zmian w kilku kontekstach, to w bazie danych zostana zapisane zmiany w tym, który był zapisany jako ostatni
// domyślne zachowanie można zmienić - na dowolną właściwość można ustawić tzw. token współbieżności (concurrency token), który sprawi że zostanie rzucony wyjątek błędu zapisu

using (var context1 = new AppDbContext())
{
	var category1 = await context1.Categories.FindAsync(2);
	category1.Description = "123";

	using (var context2 = new AppDbContext())
	{
		var category1a = await context2.Categories.FindAsync(2);
		category1a.Description = "321";

		try
		{
			await context1.SaveChangesAsync();
			await context2.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException exception)
		{
			foreach (var item in exception.Entries)
			{
				if (item.Entity is Category)
				{
					var proposedValues = item.CurrentValues;
					var databaseValues = item.GetDatabaseValues();
					foreach (var property in proposedValues.Properties)
					{
						var proposedValue = proposedValues[property];
						var databaseValue = databaseValues[property];
						if (proposedValue != databaseValue)
						{
							//
						}
					}
				}
			}
			throw;
		}
		catch (Exception)
		{
			throw;
		}
	}
}

// widoki
// 1. Dodać nową pustą migrację
// 2. Dodać w niej ręcznie sql tworzący widok
// 3. Dodać nową klasę (encję) zawierającą pola z widoku
// 4. Dodać w AppDbContext nowy DbSet
// 5. Dodać plik konfiguracyjny z zapisem builder.HasNoKey().ToView("UserFullInfoView");
//    HasNoKey oznacza żeby nie tworzyć nowej tabeli w bazie danych
// 6. update-database
using (AppDbContext context = new())
{
	// Aby wywołać widok w kontekście, wystarczy wywołać nazwę tego widoku:
	var users = await context.UserFullInfo.ToListAsync();
	foreach (var item in users)
	{
		Console.WriteLine($"{item.Id} - {item.Login} - {item.Email}");
	}
}

// procedury (2 rodzaje, zwracajace dane i bez zwracanych danych, w EF Core obie wywoływane w inny sposób)
// 1. Dodać nową pustą migrację
// 2. Dodać w niej ręcznie sql tworzący procedurę
// 3. a) Jeżeli procedura nie zwraca wartości, to nie trzeba dodawać nowej encji (klasy)
// 3. b) Jeżeli procedura zwraca wartość odpowiadającą jednej z klas, to nie trzeba tworzyc nowej
// 3. c) Jeżeli procedura zwraca wartość której nie ma w klasach encji, to trzeba dodać nową
//    Dodać w AppDbContext nowy DbSet
//    Dodać plik konfiguracyjny z zapisem builder.HasNoKey();
// 4. update-database

// Aby wywołać procedurę zwracającą wartość trzeba użyć FromSqlInterpolated przekazując nazwę procedury i ew. parametr wywołania:
using (AppDbContext context = new())
{
	var posts = await context.Posts
		.FromSqlInterpolated($"AllPostInCategory {11}")
		.ToListAsync();
	foreach (var item in posts)
	{
		Console.WriteLine($"{item.Title}");
	}
}

// Aby wywołać procedurę bez zwracanej wartości trzeba wywołać ExecuteSqlInterpolatedAsync na context.Database, przekazując nazwę procedury i ew. parametr wywołania
using (AppDbContext context = new())
{
	int value1 = 666;
	await context.Database.ExecuteSqlInterpolatedAsync($"DeleteArticle {value1}");
}