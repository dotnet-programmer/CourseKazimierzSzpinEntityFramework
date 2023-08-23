using Accountancy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accountancy.DataLayer.Configurations;

internal class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
	public void Configure(EntityTypeBuilder<Invoice> builder)
	{
		builder
			.ToTable("Invoices");

		builder
			.HasKey(x => x.InvoiceId);

		builder
			.HasIndex(x => new { x.Year, x.Month, x.Number })
			.IsUnique();

		builder
			.HasOne(x => x.Customer)
			.WithMany(x => x.Invoices)
			.HasForeignKey(x => x.CustomerId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}