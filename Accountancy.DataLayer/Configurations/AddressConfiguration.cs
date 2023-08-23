using Accountancy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accountancy.DataLayer.Configurations;

internal class AddressConfiguration : IEntityTypeConfiguration<Address>
{
	public void Configure(EntityTypeBuilder<Address> builder)
	{
		builder
			.ToTable("Addresses");

		builder
			.HasKey(x => x.AddressId);

		builder
			.Property(x => x.State)
			.HasMaxLength(100);

		builder
			.Property(x => x.City)
			.HasMaxLength(100)
			.IsRequired();

		builder
			.Property(x => x.Street)
			.HasMaxLength(150)
			.IsRequired();

		builder
			.Property(x => x.PostalCode)
			.HasMaxLength(10);
	}
}