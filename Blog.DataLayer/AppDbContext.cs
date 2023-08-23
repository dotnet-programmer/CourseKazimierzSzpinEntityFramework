using System.Reflection;
using Blog.DataLayer.Extensions;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Blog.DataLayer;

public class AppDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<ContactInfo> ContactInfo { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PostTag> PostTags { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var builder = new ConfigurationBuilder().AddJsonFile("AppSettings.json", true, true);
		var config = builder.Build();
		optionsBuilder.UseSqlServer(config["ConnectionString"]);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		modelBuilder.SeedCategories();
	}
}
