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

//var csharpBooksCheaperThan50 = books.Where(x => x.Price < 50 && x.Title.Contains("C#"));
//foreach (var item in csharpBooksCheaperThan50)
//{
//	Console.WriteLine(item.Title);
//}

//class Book
//{
//	public int Id { get; set; }
//	public string Title { get; set; }
//	public decimal Price { get; set; }
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

//var booksWithAuthorsName = books.Join(authors,
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

//var booksNotRead = books.Except(readBooks, new BookComparer());
//Console.WriteLine();
//foreach (var item in booksNotRead)
//{
//    Console.WriteLine(item.Title);
//}

//var commonBooks = books.Intersect(readBooks, new BookComparer());
//Console.WriteLine();
//foreach (var item in commonBooks)
//{
//	Console.WriteLine(item.Title);
//}

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
//var answer4 = students.OrderBy(x => x.Name).ThenByDescending(x => x.StudentId).Select(x => x.Name);
//foreach (var student in answer4)
//{
//	Console.WriteLine($"{student}");
//}

//Console.WriteLine("\n5) Pobierz do jednej kolekcji informacji o nazwie studenta i opisie jego grupy.");
//var answer5 = students.Join(groups, student => student.GroupId, group => group.GroupId, (student, group) => new { Name = student.Name, Group = group.Description });
//foreach (var student in answer5)
//{
//	Console.WriteLine($"{student.Name} - {student.Group}");
//}

//Console.WriteLine("\n6) Wyświetl średnią wieku wszystkich studentów.");
//var answer6 = students.Average(x => x.Age);
//Console.WriteLine(answer6);

//Console.WriteLine("\n7) Pogrupuj studentów po grupie i wyświetl wszystkich studentów należącej do danej grupy po przecinku.");
//var answer7 = students.Join(groups, s => s.GroupId, g => g.GroupId, (s, g) => new { Student = s, Group = g })
//	.GroupBy(x => x.Student.GroupId)
//	.Select(x => new { GroupDesc = x.First().Group.Description, StudentsName = string.Join(", ", x.Select(x => x.Student.Name)) });
//foreach (var student in answer7)
//{
//	Console.WriteLine($"{student.GroupDesc} - {student.StudentsName}");
//}

//Console.WriteLine("\n8) Zastosuj paginacje i wyświetl 2 stronę z listą studentów zawierającą 10 rekordów.");
//int pageSize = 10;
//int pageNumber = 2;
//var answer8 = students.Skip((pageNumber - 1) * pageSize).Take(pageSize);
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

using Blog.DataLayer;
using Microsoft.EntityFrameworkCore;

using (AppDbContext context = new())
{
	var userWithId3 = context.Users.Find(3);
	Console.WriteLine(userWithId3.Login);

	var users = await context.Users.ToListAsync();
	foreach (var item in users)
	{
		Console.WriteLine(item.Login);
	}

	// eager loading
	var posts = context.Posts
		.Include(x => x.User)
		.ThenInclude(x => x.PostsApproved.Where(x => x.UserId > 1))
		.Include(x => x.ApprovedBy)
		.ToList();
	foreach (var item in posts)
	{
		Console.WriteLine(item.User.Login);
	}

	//// explicit loading
	//var post = await context
	//	.Posts
	//	.FirstOrDefaultAsync();
	//// ładowanie 1 elementu
	//context.Entry(post).Reference(x => x.User).Load();
	//// ładowanie kolekcji
	//context.Entry(post).Collection(x => x.Tags).Load();
	//context.Entry(post).Collection(x => x.Tags).Query().Where(x => x.Id > 3).Load();
	//Console.WriteLine(post.User.Login);

	////raw sql
	//var title = "Title 7";
	////var posts2 = await context.Posts.FromSqlRaw("SELECT * FROM Posts2 WHERE Title2={0}", title).ToListAsync();
	//// interpolated zabezpiecza przed sql injection
	//var posts2 = await context.Posts.FromSqlInterpolated($"SELECT * FROM Posts2 WHERE Title2={title}").ToListAsync();
	//foreach (var item in posts2)
	//{
	//	Console.WriteLine(item.Title);
	//}

	var posts3 = await context.Customs.FromSqlRaw("SELECT Description as FullDescription FROM Posts2").ToListAsync();
	foreach (var item in posts3)
	{
		Console.WriteLine(item.FullDescription);
	}
}
