using Blog.Domain.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataLayer.Configurations.Views;

internal class UserFullInfoConfiguration : IEntityTypeConfiguration<UserFullInfo>
{
	public void Configure(EntityTypeBuilder<UserFullInfo> builder)
		=> builder
			.HasNoKey() // żeby nie tworzyć tabeli w bazie danych, bo to jest widok
			.ToView("UserFullInfoView");
}