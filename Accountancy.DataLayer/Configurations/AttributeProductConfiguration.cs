using Accountancy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accountancy.DataLayer.Configurations;

internal class AttributeProductConfiguration : IEntityTypeConfiguration<AttributeProduct>
{
	public void Configure(EntityTypeBuilder<AttributeProduct> builder)
	{
		builder
			.ToTable("AttributeProduct");
	}
}