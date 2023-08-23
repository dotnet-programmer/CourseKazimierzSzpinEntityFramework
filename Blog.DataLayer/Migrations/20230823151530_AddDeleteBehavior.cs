using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts2_Categories_CategoryId",
                table: "Posts2");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts2_Users_ApprovedByUserId",
                table: "Posts2");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts2_Users_UserId",
                table: "Posts2");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts2_Categories_CategoryId",
                table: "Posts2",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts2_Users_ApprovedByUserId",
                table: "Posts2",
                column: "ApprovedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts2_Users_UserId",
                table: "Posts2",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts2_Categories_CategoryId",
                table: "Posts2");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts2_Users_ApprovedByUserId",
                table: "Posts2");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts2_Users_UserId",
                table: "Posts2");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts2_Categories_CategoryId",
                table: "Posts2",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts2_Users_ApprovedByUserId",
                table: "Posts2",
                column: "ApprovedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts2_Users_UserId",
                table: "Posts2",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
