using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.DataLayer.Migrations;

/// <inheritdoc />
public partial class SetConfigurationUser : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<string>(
			name: "Password",
			table: "Users",
			type: "nvarchar(500)",
			maxLength: 500,
			nullable: false,
			oldClrType: typeof(string),
			oldType: "nvarchar(max)");

		migrationBuilder.AlterColumn<string>(
			name: "Login",
			table: "Users",
			type: "nvarchar(25)",
			maxLength: 25,
			nullable: false,
			oldClrType: typeof(string),
			oldType: "nvarchar(max)");

		migrationBuilder.CreateIndex(
			name: "IX_Users_Login",
			table: "Users",
			column: "Login",
			unique: true);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropIndex(
			name: "IX_Users_Login",
			table: "Users");

		migrationBuilder.AlterColumn<string>(
			name: "Password",
			table: "Users",
			type: "nvarchar(max)",
			nullable: false,
			oldClrType: typeof(string),
			oldType: "nvarchar(500)",
			oldMaxLength: 500);

		migrationBuilder.AlterColumn<string>(
			name: "Login",
			table: "Users",
			type: "nvarchar(max)",
			nullable: false,
			oldClrType: typeof(string),
			oldType: "nvarchar(25)",
			oldMaxLength: 25);
	}
}
