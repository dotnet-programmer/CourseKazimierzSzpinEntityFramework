using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.DataLayer.Migrations;

/// <inheritdoc />
public partial class SetConfigurationPost : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey(
			name: "FK_Posts_Categories_CategoryId",
			table: "Posts");

		migrationBuilder.DropForeignKey(
			name: "FK_Posts_Users_UserId",
			table: "Posts");

		migrationBuilder.DropForeignKey(
			name: "FK_PostTag_Posts_PostsId",
			table: "PostTag");

		migrationBuilder.DropPrimaryKey(
			name: "PK_Posts",
			table: "Posts");

		migrationBuilder.RenameTable(
			name: "Posts",
			newName: "Posts2");

		migrationBuilder.RenameColumn(
			name: "Title",
			table: "Posts2",
			newName: "Title2");

		migrationBuilder.RenameIndex(
			name: "IX_Posts_UserId",
			table: "Posts2",
			newName: "IX_Posts2_UserId");

		migrationBuilder.RenameIndex(
			name: "IX_Posts_CategoryId",
			table: "Posts2",
			newName: "IX_Posts2_CategoryId");

		migrationBuilder.AlterColumn<string>(
			name: "ShortDescription",
			table: "Posts2",
			type: "nvarchar(50)",
			maxLength: 50,
			nullable: false,
			oldClrType: typeof(string),
			oldType: "nvarchar(max)");

		migrationBuilder.AlterColumn<bool>(
			name: "Published",
			table: "Posts2",
			type: "bit",
			nullable: true,
			oldClrType: typeof(bool),
			oldType: "bit");

		migrationBuilder.AlterColumn<DateTime>(
			name: "PostedOn",
			table: "Posts2",
			type: "datetime",
			nullable: false,
			oldClrType: typeof(DateTime),
			oldType: "datetime2");

		migrationBuilder.AlterColumn<string>(
			name: "Description",
			table: "Posts2",
			type: "varchar(200)",
			unicode: false,
			maxLength: 200,
			nullable: false,
			defaultValue: "Description",
			oldClrType: typeof(string),
			oldType: "nvarchar(max)");

		migrationBuilder.AlterColumn<string>(
			name: "Title2",
			table: "Posts2",
			type: "nvarchar(100)",
			maxLength: 100,
			nullable: false,
			oldClrType: typeof(string),
			oldType: "nvarchar(max)");

		migrationBuilder.AddPrimaryKey(
			name: "PK_Posts2",
			table: "Posts2",
			column: "Id");

		migrationBuilder.AddForeignKey(
			name: "FK_Posts2_Categories_CategoryId",
			table: "Posts2",
			column: "CategoryId",
			principalTable: "Categories",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);

		migrationBuilder.AddForeignKey(
			name: "FK_Posts2_Users_UserId",
			table: "Posts2",
			column: "UserId",
			principalTable: "Users",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);

		migrationBuilder.AddForeignKey(
			name: "FK_PostTag_Posts2_PostsId",
			table: "PostTag",
			column: "PostsId",
			principalTable: "Posts2",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey(
			name: "FK_Posts2_Categories_CategoryId",
			table: "Posts2");

		migrationBuilder.DropForeignKey(
			name: "FK_Posts2_Users_UserId",
			table: "Posts2");

		migrationBuilder.DropForeignKey(
			name: "FK_PostTag_Posts2_PostsId",
			table: "PostTag");

		migrationBuilder.DropPrimaryKey(
			name: "PK_Posts2",
			table: "Posts2");

		migrationBuilder.RenameTable(
			name: "Posts2",
			newName: "Posts");

		migrationBuilder.RenameColumn(
			name: "Title2",
			table: "Posts",
			newName: "Title");

		migrationBuilder.RenameIndex(
			name: "IX_Posts2_UserId",
			table: "Posts",
			newName: "IX_Posts_UserId");

		migrationBuilder.RenameIndex(
			name: "IX_Posts2_CategoryId",
			table: "Posts",
			newName: "IX_Posts_CategoryId");

		migrationBuilder.AlterColumn<string>(
			name: "ShortDescription",
			table: "Posts",
			type: "nvarchar(max)",
			nullable: false,
			oldClrType: typeof(string),
			oldType: "nvarchar(50)",
			oldMaxLength: 50);

		migrationBuilder.AlterColumn<bool>(
			name: "Published",
			table: "Posts",
			type: "bit",
			nullable: false,
			defaultValue: false,
			oldClrType: typeof(bool),
			oldType: "bit",
			oldNullable: true);

		migrationBuilder.AlterColumn<DateTime>(
			name: "PostedOn",
			table: "Posts",
			type: "datetime2",
			nullable: false,
			oldClrType: typeof(DateTime),
			oldType: "datetime");

		migrationBuilder.AlterColumn<string>(
			name: "Description",
			table: "Posts",
			type: "nvarchar(max)",
			nullable: false,
			oldClrType: typeof(string),
			oldType: "varchar(200)",
			oldUnicode: false,
			oldMaxLength: 200,
			oldDefaultValue: "Description");

		migrationBuilder.AlterColumn<string>(
			name: "Title",
			table: "Posts",
			type: "nvarchar(max)",
			nullable: false,
			oldClrType: typeof(string),
			oldType: "nvarchar(100)",
			oldMaxLength: 100);

		migrationBuilder.AddPrimaryKey(
			name: "PK_Posts",
			table: "Posts",
			column: "Id");

		migrationBuilder.AddForeignKey(
			name: "FK_Posts_Categories_CategoryId",
			table: "Posts",
			column: "CategoryId",
			principalTable: "Categories",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);

		migrationBuilder.AddForeignKey(
			name: "FK_Posts_Users_UserId",
			table: "Posts",
			column: "UserId",
			principalTable: "Users",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);

		migrationBuilder.AddForeignKey(
			name: "FK_PostTag_Posts_PostsId",
			table: "PostTag",
			column: "PostsId",
			principalTable: "Posts",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);
	}
}
