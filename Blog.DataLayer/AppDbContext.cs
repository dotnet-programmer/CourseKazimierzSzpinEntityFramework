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

public class AppDbContext : DbContext
{
	public static readonly ILoggerFactory _loggerFactory = new NLogLoggerFactory();

	public DbSet<Category> Categories { get; set; }
	public DbSet<ContactInfo> ContactInfo { get; set; }
	public DbSet<Post> Posts { get; set; }
	public DbSet<Tag> Tags { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<PostTag> PostTags { get; set; }
	public DbSet<Custom> Customs { get; set; }
	public DbSet<UserFullInfo> UserFullInfo { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var builder = new ConfigurationBuilder().AddJsonFile("AppSettings.json", true, true);
		var config = builder.Build();

		optionsBuilder
			// ustawienie connection string dla SQL Server
			.UseSqlServer(config["ConnectionString"])

			// logowanie do konsoli zapytań SQL
			.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
			.EnableSensitiveDataLogging()

			// lazy loading - nuget - Microsoft.EntityFrameworkCore.Proxies + wszystkie właściwości nawigacyjne oznaczyć słowem kluczowym virtual
			// .UseLazyLoadingProxies()

			// wyłączenie śledzenia zmian dla całego contextu
			//.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)

			// logowanie za pomocą NLog do pliku
			.UseLoggerFactory(_loggerFactory);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// dodanie pojedynczej konfiguracji
		//modelBuilder.ApplyConfiguration(new PostConfiguration());

		// znajdź wszystkie klasy konfiguracyjne implementujące interfejs IEntityTypeConfiguration i zastosuj te konfiguracje
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		// wypełnienie bazy danych początkowymi danymi podczas tworzenia
		// konfiguracja tego zrobiona jako metoda rozszerzająca ModelBuilder
		modelBuilder.SeedCategories();
	}
}
