using Accountancy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accountancy.DataLayer.Configurations;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
	public void Configure(EntityTypeBuilder<Customer> builder)
	{
		builder
			.ToTable("Customers");

		builder
			.HasKey(x => x.CustomerId);

		builder
			.HasIndex(x => x.Nip)
			.IsUnique();

		builder
			.Property(x => x.Name)
			.HasMaxLength(250);

		builder
			.Property(x => x.Nip)
			.HasMaxLength(13)
			.IsRequired();

		builder
			.Property(x => x.PhoneNumber)
			.HasMaxLength(25);

		builder
			.HasOne(x => x.Address)
			.WithOne(x => x.Customer)
			.HasForeignKey<Address>(x => x.CustomerId);

		builder
			.Property(x => x.IsDeleted)
			.HasDefaultValue(false);
	}
}