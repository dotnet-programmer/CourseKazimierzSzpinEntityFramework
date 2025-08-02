using Blog.DataLayer.Repositories;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Blog.DataLayer.IntegrationTests.Repositories;

internal class CategoryRepositoryTests
{
	private AppDbContext _context;
	private CategoryRepository _categoryRepository;
	private Category _category;

	private void Init()
	{
		var builder = new DbContextOptionsBuilder<AppDbContext>();

		// używaj nowej bazy danych w pamięci dla każdego testu
		builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

		_context = new AppDbContext(builder.Options);
		_categoryRepository = new CategoryRepository(_context);
		_category = new Category { Name = "Name", Description = "Desc", Url = "url" };
	}

	[Test]
	public async Task Add_WhenCalled_ShouldAddCategoryToDb()
	{
		Init();

		await _categoryRepository.Add(_category);

		var category = _context.Categories.FirstOrDefault(x => x.Name == _category.Name);

		Assert.That(category, Is.Not.Null);
	}

	[Test]
	public async Task Add_WhenCalled_ShouldUpdateCategoryId()
	{
		Init();

		await _categoryRepository.Add(_category);

		Assert.That(_category.Id, Is.Not.EqualTo(0));
	}
}