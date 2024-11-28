using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataLayer.Configurations;

internal class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInfo>
{
	public void Configure(EntityTypeBuilder<ContactInfo> builder) => builder
			.Property(x => x.Email)
			.IsRequired()
			.HasMaxLength(50);
}
