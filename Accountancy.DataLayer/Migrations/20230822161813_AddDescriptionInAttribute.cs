using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accountancy.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionInAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Attributes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Attributes");
        }
    }
}
