using Accountancy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accountancy.DataLayer.Configurations;

internal class InvoicePositionConfiguration : IEntityTypeConfiguration<InvoicePosition>
{
	public void Configure(EntityTypeBuilder<InvoicePosition> builder)
	{
		builder
			.ToTable("InvoicePositions");

		builder
			.HasKey(x => x.InvoicePositionId);

		builder
			.HasOne(x => x.Invoice)
			.WithMany(x => x.InvoicePositions)
			.HasForeignKey(x => x.InvoiceId)
			.OnDelete(DeleteBehavior.Restrict);

		builder
			.HasOne(x => x.Product)
			.WithMany(x => x.InvoicePositions)
			.HasForeignKey(x => x.ProductId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}