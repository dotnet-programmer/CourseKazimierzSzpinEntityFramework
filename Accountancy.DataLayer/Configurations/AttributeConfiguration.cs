using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Attribute = Accountancy.Domain.Entities.Attribute;

namespace Accountancy.DataLayer.Configurations;

internal class AttributeConfiguration : IEntityTypeConfiguration<Attribute>
{
	public void Configure(EntityTypeBuilder<Attribute> builder)
	{
		builder
			.ToTable("Attributes");

		builder
			.HasKey(x => x.AttributeId);

		builder
			.HasIndex(x => x.Name)
			.IsUnique();

		builder
			.Property(x => x.Name)
			.HasMaxLength(50)
			.IsRequired();
	}
}