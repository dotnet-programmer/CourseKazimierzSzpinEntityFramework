using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataLayer.Extensions;

public static class ModelBuilderExtensions
{
	public static void SeedCategories(this ModelBuilder modelBuilder)
		=> modelBuilder
			.Entity<Category>()
			.HasData(new Category
			{
				Id = 1,
				Name = "General",
				Description = "All general posts",
				Url = "general"
			});
}