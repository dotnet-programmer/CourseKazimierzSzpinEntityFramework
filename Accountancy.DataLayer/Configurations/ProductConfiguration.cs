using Accountancy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accountancy.DataLayer.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder
			.ToTable("Products");

		builder
			.HasKey(x => x.ProductId);

		builder
			.HasIndex(x => x.Name)
			.IsUnique();

		builder
			.Property(x => x.Name)
			.HasMaxLength(150)
			.IsRequired();

		builder
			.HasMany(x => x.Attributes)
			.WithMany(x => x.Products)
			.UsingEntity<AttributeProduct>(
				x => x.HasOne(x => x.Attribute).WithMany().HasForeignKey(x => x.AttributeId),
				x => x.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId));
	}
}