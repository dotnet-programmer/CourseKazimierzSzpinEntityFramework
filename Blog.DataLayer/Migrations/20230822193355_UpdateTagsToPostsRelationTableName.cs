using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTagsToPostsRelationTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Posts2_PostsId",
                table: "PostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Tags_TagsId",
                table: "PostTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag");

            migrationBuilder.RenameTable(
                name: "PostTag",
                newName: "PostsTagsMaps");

            migrationBuilder.RenameIndex(
                name: "IX_PostTag_TagsId",
                table: "PostsTagsMaps",
                newName: "IX_PostsTagsMaps_TagsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostsTagsMaps",
                table: "PostsTagsMaps",
                columns: new[] { "PostsId", "TagsId" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostsTagsMaps_Posts2_PostsId",
                table: "PostsTagsMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_PostsTagsMaps_Tags_TagsId",
                table: "PostsTagsMaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostsTagsMaps",
                table: "PostsTagsMaps");

            migrationBuilder.RenameTable(
                name: "PostsTagsMaps",
                newName: "PostTag");

            migrationBuilder.RenameIndex(
                name: "IX_PostsTagsMaps_TagsId",
                table: "PostTag",
                newName: "IX_PostTag_TagsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag",
                columns: new[] { "PostsId", "TagsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Posts2_PostsId",
                table: "PostTag",
                column: "PostsId",
                principalTable: "Posts2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Tags_TagsId",
                table: "PostTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
