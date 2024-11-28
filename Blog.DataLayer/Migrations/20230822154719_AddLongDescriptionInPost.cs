using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.DataLayer.Migrations;

/// <inheritdoc />
public partial class AddLongDescriptionInPost : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<string>(
			name: "LongDescription",
			table: "Posts",
			type: "nvarchar(max)",
			nullable: false,
			defaultValue: "");

		// INFO - dodanie własnej komendy SQL, która aktualizuje każde pole LongDescription wartością z Description
		migrationBuilder.Sql(@"UPDATE Posts SET LongDescription = Description;");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropColumn(
			name: "LongDescription",
			table: "Posts");
}
