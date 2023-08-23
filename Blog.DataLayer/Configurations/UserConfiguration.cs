using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataLayer.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder
			.Property(x=>x.Login)
			.IsRequired()
			.HasMaxLength(25);

		builder
			.Property(x => x.Password)
			.IsRequired()
			.HasMaxLength(500);

		builder
			.HasIndex(x => x.Login)
			.IsUnique();

		// relacja 1:1, w konfiguracji ustawia się jak np. nazwa klucza obcego jest inna niż w konwencji
		builder
			.HasOne(x => x.ContactInfo)
			.WithOne(x => x.User)
			.HasForeignKey<ContactInfo>(x => x.UserId);

		// relacja 1:wiele, może być w dowolnym pliku konfiguracyjnym, tutaj jest w tym, gdzie nie ma klucza obcego
		//builder
		//	.HasMany(x => x.Posts)
		//	.WithOne(x => x.User)
		//	.HasForeignKey(x => x.UserId);

		//builder
		//	.HasMany(x => x.PostsApproved)
		//	.WithOne(x => x.ApprovedBy)
		//	.HasForeignKey(x => x.ApprovedByUserId);
	}
}
