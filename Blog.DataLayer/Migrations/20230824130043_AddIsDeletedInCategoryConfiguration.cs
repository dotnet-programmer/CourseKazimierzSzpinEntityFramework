using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.DataLayer.Migrations;

/// <inheritdoc />
public partial class AddIsDeletedInCategoryConfiguration : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<bool>(
			name: "IsDeleted",
			table: "Categories",
			type: "bit",
			nullable: true,
			defaultValue: false,
			oldClrType: typeof(bool),
			oldType: "bit",
			oldNullable: true);

		migrationBuilder.UpdateData(
			table: "Categories",
			keyColumn: "Id",
			keyValue: 1,
			column: "IsDeleted",
			value: false);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<bool>(
			name: "IsDeleted",
			table: "Categories",
			type: "bit",
			nullable: true,
			oldClrType: typeof(bool),
			oldType: "bit",
			oldNullable: true,
			oldDefaultValue: false);

		migrationBuilder.UpdateData(
			table: "Categories",
			keyColumn: "Id",
			keyValue: 1,
			column: "IsDeleted",
			value: null);
	}
}
