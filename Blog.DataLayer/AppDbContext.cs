using System.Reflection;
using Blog.DataLayer.Extensions;
using Blog.Domain.Entities;
using Blog.Domain.Entities.Views;
using Blog.Domain.Entities.Query;
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

			// logowanie za pomocą NLog do pliku
			.UseLoggerFactory(_loggerFactory);

		// lazy loading - nuget - Microsoft.EntityFrameworkCore.Proxies + wszystkie właściwości nawigacyjne oznaczyć słowem kluczowym virtual
		// .UseLazyLoadingProxies();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		modelBuilder.SeedCategories();
	}
}
