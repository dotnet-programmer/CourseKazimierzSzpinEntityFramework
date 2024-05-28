using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataLayer.Configurations;

internal class PostConfiguration : IEntityTypeConfiguration<Post>
{
	public void Configure(EntityTypeBuilder<Post> builder)
	{
		builder
			.ToTable("Posts2");

		builder
			.Property(x => x.Title)
			.HasMaxLength(100)
			.HasColumnName("Title2")
			.IsRequired();

		builder
			.Property(x => x.PostedOn)
			.HasColumnType("datetime");

		builder
			.Property(x => x.ShortDescription)
			.HasMaxLength(50);

		builder
			.Property(x => x.Description)
			.HasMaxLength(200)
			.IsUnicode(false)
			.HasDefaultValue("Description");

		builder
			.Property(x => x.Published)
			.IsRequired(false);

		// relacja 1:wiele, może być w dowolnym pliku konfiguracyjnym, tutaj jest w tym, gdzie jest klucz obcy
		builder
			.HasOne(x => x.User)
			.WithMany(x => x.Posts)
			.HasForeignKey(x => x.UserId)
			.OnDelete(DeleteBehavior.Restrict);

		// relacja 1:wiele
		builder
			.HasOne(x => x.ApprovedBy)
			.WithMany(x => x.PostsApproved)
			.HasForeignKey(x => x.ApprovedByUserId)
			.OnDelete(DeleteBehavior.Restrict);

		// relacja 1:wiele
		builder
			.HasOne(x => x.Category)
			.WithMany(x => x.Posts)
			.HasForeignKey(x => x.CategoryId)
			.OnDelete(DeleteBehavior.Restrict);

		// relacja wiele:wiele - zmiana nazwy tabeli łączącej
		//builder
		//	.HasMany(x => x.Tags)
		//	.WithMany(x => x.Posts)
		//	.UsingEntity(x => x.ToTable("PostsTagsMaps"));

		// relacja wiele:wiele - dodanie nowego pola w tabeli łączącej
		builder
			.HasMany(x => x.Tags)
			.WithMany(x => x.Posts)
			.UsingEntity<PostTag>(
				x => x.HasOne(x => x.Tag).WithMany().HasForeignKey(x => x.TagId),
				x => x.HasOne(x => x.Post).WithMany().HasForeignKey(x => x.PostId))
			.Property(x => x.CreatedDate)
			.HasDefaultValueSql("getdate()");
	}
}