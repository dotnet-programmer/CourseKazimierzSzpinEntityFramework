using Blog.Domain.Entities;

namespace Blog.DataLayer.Repositories;

public class CategoryRepository(AppDbContext context)
{
	public async Task Add(Category category)
	{
		context.Categories.Add(category);
		await context.SaveChangesAsync();
	}
}