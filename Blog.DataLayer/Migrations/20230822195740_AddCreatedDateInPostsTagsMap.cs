using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.DataLayer.Migrations;

/// <inheritdoc />
public partial class AddCreatedDateInPostsTagsMap : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey(
			name: "FK_PostsTagsMaps_Posts2_PostsId",
			table: "PostsTagsMaps");

		migrationBuilder.DropForeignKey(
			name: "FK_PostsTagsMaps_Tags_TagsId",
			table: "PostsTagsMaps");

		migrationBuilder.RenameColumn(
			name: "TagsId",
			table: "PostsTagsMaps",
			newName: "TagId");

		migrationBuilder.RenameColumn(
			name: "PostsId",
			table: "PostsTagsMaps",
			newName: "PostId");

		migrationBuilder.RenameIndex(
			name: "IX_PostsTagsMaps_TagsId",
			table: "PostsTagsMaps",
			newName: "IX_PostsTagsMaps_TagId");

		migrationBuilder.AddColumn<DateTime>(
			name: "CreatedDate",
			table: "PostsTagsMaps",
			type: "datetime2",
			nullable: false,
			defaultValueSql: "getdate()");

		migrationBuilder.AddForeignKey(
			name: "FK_PostsTagsMaps_Posts2_PostId",
			table: "PostsTagsMaps",
			column: "PostId",
			principalTable: "Posts2",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);

		migrationBuilder.AddForeignKey(
			name: "FK_PostsTagsMaps_Tags_TagId",
			table: "PostsTagsMaps",
			column: "TagId",
			principalTable: "Tags",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey(
			name: "FK_PostsTagsMaps_Posts2_PostId",
			table: "PostsTagsMaps");

		migrationBuilder.DropForeignKey(
			name: "FK_PostsTagsMaps_Tags_TagId",
			table: "PostsTagsMaps");

		migrationBuilder.DropColumn(
			name: "CreatedDate",
			table: "PostsTagsMaps");

		migrationBuilder.RenameColumn(
			name: "TagId",
			table: "PostsTagsMaps",
			newName: "TagsId");

		migrationBuilder.RenameColumn(
			name: "PostId",
			table: "PostsTagsMaps",
			newName: "PostsId");

		migrationBuilder.RenameIndex(
			name: "IX_PostsTagsMaps_TagId",
			table: "PostsTagsMaps",
			newName: "IX_PostsTagsMaps_TagsId");

		migrationBuilder.AddForeignKey(
			name: "FK_PostsTagsMaps_Posts2_PostsId",
			table: "PostsTagsMaps",
			column: "PostsId",
			principalTable: "Posts2",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);

		migrationBuilder.AddForeignKey(
			name: "FK_PostsTagsMaps_Tags_TagsId",
			table: "PostsTagsMaps",
			column: "TagsId",
			principalTable: "Tags",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);
	}
}
