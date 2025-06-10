using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataLayer.Configurations;

internal class TagConfiguration : IEntityTypeConfiguration<Tag>
{
	public void Configure(EntityTypeBuilder<Tag> builder)
	{
		builder
			.Property(x => x.Name)
			.IsRequired()
			.HasMaxLength(50);

		builder
			.Property(x => x.Url)
			.IsRequired()
			.HasMaxLength(100);

		builder
			.HasIndex(x => x.Name)
			.IsUnique();
	}
}