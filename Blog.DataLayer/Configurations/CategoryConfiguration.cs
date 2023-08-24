using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataLayer.Configurations;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder
			.ToTable("Categories");

		builder
			.HasKey(x => x.Id);

		builder
			.HasIndex(x => x.Name)
			.IsUnique();

		builder
			.Property(x=>x.Name)
			.HasMaxLength(50);

		builder
			.Property(x=>x.Url)
			.HasMaxLength(500);

		builder
			.Property(x => x.Description)
			.HasMaxLength(200);

		builder
			.Property(x => x.IsDeleted)
			.IsRequired(false)
			.HasDefaultValue(false);
	}
}
