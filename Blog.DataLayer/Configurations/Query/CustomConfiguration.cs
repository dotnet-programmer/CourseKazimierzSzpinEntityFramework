using Blog.Domain.Entities.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataLayer.Configurations.Query;

internal class CustomConfiguration : IEntityTypeConfiguration<Custom>
{
	public void Configure(EntityTypeBuilder<Custom> builder)
		=> builder
			.HasNoKey()
			.ToView("Custom");
}