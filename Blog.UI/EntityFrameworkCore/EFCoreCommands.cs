using Blog.DataLayer;
using Blog.DataLayer.Extensions;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace Blog.UI.EntityFrameworkCore;

// Komendy - dodawanie, aktualizowanie i usuwanie danych z bazy *****************************************************************************************
internal class EFCoreCommands
{
	// Dodawanie danych *********************************************************************************************************************************
	public async Task Add()
	{
		Category category = new()
		{
			Name = "New Category 1",
			Description = "New Description 1",
			Url = "cat1"
		};

		using (AppDbContext context = new())
		{
			// przy dodawaniu zaleca się używać wersji synchronicznej, bo asynchroniczna ma jakieś specjalne zastosowanie
			context.Categories.Add(category);

			DisplayEntriesInfo(context);
			
			// tutaj dane są dodawane i zapisywane w bazie, a po poprawnym wykonaniu nowe ID jest przypisywane do dodawanego obiektu
			await context.SaveChangesAsync();
			// W tym miejscu (po wykonaniu Save) category.Id ma już nową wartość
		}
	}

	// Zaawansowane dodawanie danych **********************************************************************************************************************
	public async Task AdvancedDataAdding()
	{
		Category postCategory = new()
		{
			Name = "New Category 2",
			Description = "New Description 2",
			Url = "cat2"
		};

		ContactInfo contactInfo = new()
		{
			Email = "new@mail.pl"
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
			Type = PostType.Plain,
			Url = "url",
			Tags = new List<Tag>
			{
				new() { Name = "tagasdf", Url = "tag-asdf" },
				new() { Name = "tagassdfa", Url = "tag-afdasas" }
			}
		};

		using (AppDbContext context = new())
		{
			context.Posts.Add(post);
			DisplayEntriesInfo(context);
			await context.SaveChangesAsync();
		}


		// jeżeli w bazie są jakieś dane to można używać znanych id
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
			Type = PostType.Sponsored,
			Url = "url2",
		};

		List<PostTag> postTags =
		[
			new PostTag{ TagId = 5, Post =  post2, CreatedDate = DateTime.Now },
			new PostTag{ TagId = 6, Post =  post2, CreatedDate = DateTime.Now },
			new PostTag{ TagId = 7, Post =  post2, CreatedDate = DateTime.Now }
		];

		using (AppDbContext context = new())
		{
			// dodawanie pojedynczego obiektu
			context.Posts.Add(post2);

			// dodawanie listy obiektów
			context.PostTags.AddRange(postTags);

			DisplayEntriesInfo(context);
			await context.SaveChangesAsync();

			// użycie nugeta EFCore.BulkExtensions, optymalizuje dodawanie wielu rekordów do bazy danych
			// używając BulkExtension nie trzeba wywoływać Save
			//await context.BulkInsertAsync(postTags);
		}
	}

	// Aktualizowanie danych *********************************************************************************************************************************
	public async Task Update()
	{
		// 1. edycja obiektu będącego w tym samym DbContext - ten sposób aktualizuje tylko zmienione pole w tabeli, ale wywołuje 2 zapytania, najpierw select, później update
		using (AppDbContext context = new())
		{
			Category category1 = await context.Categories.FindAsync(4);
			category1.Description = "y";
			DisplayEntriesInfo(context);
			await context.SaveChangesAsync();
		}

		// 2. edycja obiektu z innego miejsca niż DbContext
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

			DisplayEntriesInfo(context);
			await context.SaveChangesAsync();
		}
	}

	// Zaawansowane aktualizowanie danych **********************************************************************************************************************
	public async Task AdvancedDataUpdating()
	{
		// aktualizacja obiektów powiązanych - dane istnieją w bazie danych
		using (AppDbContext context = new())
		{
			var post = await context.Posts.FindAsync(7);
			post.ApprovedByUserId = 5;
			DisplayEntriesInfo(context);
			await context.SaveChangesAsync();
		}

		// aktualizacja obiektów powiązanych - nowe dane, nie ma ich jeszcze w bazie
		ContactInfo contactInfo = new()
		{
			Email = "abc@wp.pl"
		};

		User user = new()
		{
			Login = "test12345",
			Password = "password",
			ContactInfo = contactInfo
		};

		using (AppDbContext context = new())
		{
			var post = await context.Posts.FindAsync(5);
			post.ApprovedBy = user;
			DisplayEntriesInfo(context);
			await context.SaveChangesAsync();
		}

		// aktualizacja obiektów z relacją wiele:wiele
		List<Tag> newTags =
		[
			new() { Id = 1 },
			new() { Id = 2 },
			new() { Id = 3 },
			new() { Id = 4 },
		];

		using (AppDbContext context = new())
		{
			var post = await context.Posts.FindAsync(7);

			var postTagsFromDB = await context.PostTags
				.Where(x => x.PostId == post.Id)
				.AsNoTracking()
				.ToListAsync();

			context.TryUpdateManyToMany(
				// stare elementy
				postTagsFromDB,
				// nowe elementy, czyli to co powinno zostać zapisane, projekcja na pasujący typ PostTag
				newTags.Select(x => new PostTag { TagId = x.Id, PostId = post.Id }),
				// klucz
				x => x.TagId);

			DisplayEntriesInfo(context);
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
			DisplayEntriesInfo(context);

			// wolniejszy zapis używając save z DbContext
			//await context.SaveChangesAsync();

			// szybszy zapis używając biblioteki EFCore.BulkExtensions, w tym przypadku można oznaczyć poprzednie zapytanie jako AsNoTracking
			await context.BulkUpdateAsync(tags);
		}
	}

	// Usuwanie danych *********************************************************************************************************************************
	public async Task Delete()
	{
		using (AppDbContext context = new())
		{
			var category = await context.Categories.FindAsync(10);
			context.Categories.Remove(category);

			// jeżeli znane jest Id obiektu to można od razu go użyć poprzez stworzenie nowego obiektu zawierającego tylko to Id
			context.Categories.Remove(new Category { Id = 9 });

			DisplayEntriesInfo(context);
			await context.SaveChangesAsync();
		}
	}

	// Zaawansowane usuwanie danych **********************************************************************************************************************
	public async Task AdvancedDataDeletion()
	{
		// usuwanie danych które są używane jako klucze obce
		using (AppDbContext context = new())
		{
			// kategoria do usunięcia, która ma powiązania z wieloma postami
			Category category = new() { Id = 4 };

			// pobranie postów powiązanych z wybraną kategorią
			var postsToDelete = await context.Posts
				.Where(x => x.CategoryId == category.Id)
				.ToListAsync();

			// najpierw należy usunąć wpisy powiązane
			context.Posts.RemoveRange(postsToDelete);
			// await context.BulkDeleteAsync(postsToDelete);

			// na końcu usunięcie głównego wpisu
			context.Categories.Remove(category);

			DisplayEntriesInfo(context);
			await context.SaveChangesAsync();
		}

		// można zrobić też tzw. Soft Delete, czyli nie usuwać danych, a dodać flagę IsDeleted, która będzie oznaczana na True przy "usuwaniu" danych
		// przy pobieraniu danych pamiętać o dodawaniu warunku Where(x => x.IsDeleted == False)
		using (AppDbContext context = new())
		{
			Category category = await context.Categories.FindAsync(11);
			category.IsDeleted = true;

			DisplayEntriesInfo(context);
			await context.SaveChangesAsync();
		}
	}

	// Transakcje *****************************************************************************************************************************************
	public async Task Transactions()
	{
		Category category = new() { Description = "abc123", Name = "Name222", Url = "URL" };

		using (AppDbContext context = new())
		{
			using var transaction = await context.Database.BeginTransactionAsync();
			{
				context.Categories.Add(category);
				context.Categories.Remove(new Category { Id = 5 });
				context.Categories.Remove(new Category { Id = 999 });
				DisplayEntriesInfo(context);
				await context.SaveChangesAsync();
				await transaction.CommitAsync();
			}
		}
	}

	// Konflikty współbieżności ****************************************************************************************************************************
	public async Task ConcurrencyConflicts()
	{
		// domyślne zachowanie - jeśli kilka zmian w kilku kontekstach, to w bazie danych zostana zapisane zmiany w tym, który był zapisany jako ostatni
		// domyślne zachowanie można zmienić - na dowolną właściwość można ustawić tzw. token współbieżności (concurrency token)
		// dodaje się go w klasie konfiguracyjnej dla danej encji - builder.Property(x => x.Description).IsConcurrencyToken();
		// sprawia to, że zostanie rzucony wyjątek błędu zapisu
		using (AppDbContext context1 = new())
		{
			var category1 = await context1.Categories.FindAsync(2);
			category1.Description = "123";

			using (AppDbContext context2 = new())
			{
				var category2 = await context2.Categories.FindAsync(2);
				category2.Description = "321";

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
									// tutaj można zdecydować która wartość ma zostać zapisana
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
	}

	// Widoki **********************************************************************************************************************************************
	// 1. Dodać nową pustą migrację
	// 2. Dodać w niej ręcznie sql tworzący widok
	// 3. Dodać nową klasę (encję) zawierającą pola z widoku
	// 4. Dodać w AppDbContext nowy DbSet
	// 5. Dodać plik konfiguracyjny z zapisem builder.HasNoKey().ToView("UserFullInfoView"); HasNoKey oznacza żeby nie tworzyć nowej tabeli w bazie danych
	// 6. update-database
	public async Task Views()
	{
		using (AppDbContext context = new())
		{
			// Aby wywołać widok w kontekście, wystarczy wywołać nazwę tego widoku:
			var users = await context.UserFullInfo.ToListAsync();
			foreach (var item in users)
			{
				Console.WriteLine($"{item.Id} - {item.Login} - {item.Email}");
			}
		}
	}

	// Procedury **********************************************************************************************************************************************
	// Są 2 rodzaje procedur: zwracajace dane i bez zwracanych danych, w EF Core obie wywoływane w inny sposób
	// 1. Dodać nową pustą migrację
	// 2. Dodać w niej ręcznie sql tworzący procedurę
	// 3. a) Jeżeli procedura nie zwraca wartości, to nie trzeba dodawać nowej encji (klasy)
	// 3. b) Jeżeli procedura zwraca wartość odpowiadającą jednej z klas, to nie trzeba tworzyć nowej
	// 3. c) Jeżeli procedura zwraca wartość której nie ma w klasach encji, to trzeba dodać nową
	//    + dodać w AppDbContext nowy DbSet
	//    + dodać plik konfiguracyjny z zapisem builder.HasNoKey();
	// 4. update-database
	public async Task Procedures()
	{
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
	}

	private static void DisplayEntriesInfo(AppDbContext context)
	{
		foreach (var item in context.ChangeTracker.Entries())
		{
			Console.WriteLine($"Encja: {item.Entity.GetType().Name}, Stan: {item.State}");
		}
	}
}