using System.Reflection;
using Blog.DataLayer.Extensions;
using Blog.Domain.Entities;
using Blog.Domain.Entities.Query;
using Blog.Domain.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Blog.DataLayer;

// ta klasa znajduje się w projekcie DataLayer, bo jest ściśle połączona z Entity Framework Core
public class AppDbContext : DbContext
{
	public static readonly ILoggerFactory _loggerFactory = new NLogLoggerFactory();

	public AppDbContext()
	{
	}

	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<Category> Categories { get; set; }
	public DbSet<ContactInfo> ContactInfo { get; set; }
	public DbSet<Post> Posts { get; set; }
	public DbSet<Tag> Tags { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<PostTag> PostTags { get; set; }
	public DbSet<Custom> Customs { get; set; }
	public DbSet<UserFullInfo> UserFullInfo { get; set; }

	//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	//{
	//	var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true);
	//	var config = builder.Build();

	//	optionsBuilder
	//		// ustawienie connection string dla SQL Server
	//		.UseSqlServer(config["ConnectionString"])

	//		// logowanie do konsoli zapytań SQL
	//		.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
	//		.EnableSensitiveDataLogging()

	//		// lazy loading - nuget - Microsoft.EntityFrameworkCore.Proxies + wszystkie właściwości nawigacyjne oznaczyć słowem kluczowym virtual
	//		// .UseLazyLoadingProxies()

	//		// wyłączenie śledzenia zmian dla całego contextu
	//		//.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)

	//		// logowanie za pomocą NLog do pliku
	//		.UseLoggerFactory(_loggerFactory);
	//}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		// potrzebne do pracy z bazą danych w pamięci
		// używany w testach jednostkowych, a dodany bo są 2 konstruktory,
		// jeśli zostanie wywołany konstruktor bez parametrów to musi zostać przeprowadzona konfiguracja
		if (!optionsBuilder.IsConfigured)
		{
			var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true);
			var config = builder.Build();

			optionsBuilder
				.UseLoggerFactory(_loggerFactory)
				.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
				.EnableSensitiveDataLogging()
				.UseSqlServer(config["ConnectionString"]);
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// konfiguracja bezpośrednio w AppDbContext
		//modelBuilder.Entity<Post>()
		//	.Property(x => x.Title)
		//	.IsRequired();

		// dodanie pojedynczej konfiguracji
		//modelBuilder.ApplyConfiguration(new PostConfiguration());

		// znajdź wszystkie klasy konfiguracyjne implementujące interfejs IEntityTypeConfiguration i zastosuj te konfiguracje
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		// wypełnienie bazy danych początkowymi danymi podczas tworzenia
		// konfiguracja tego zrobiona jako metoda rozszerzająca ModelBuilder
		modelBuilder.SeedCategories();
	}
}