using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataLayer.Configurations;

internal class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
{
	public void Configure(EntityTypeBuilder<PostTag> builder)
		=> builder
			.ToTable("PostsTagsMaps");
}