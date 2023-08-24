using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accountancy.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class CreateDeleteProductProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
				CREATE OR ALTER PROCEDURE dbo.DeleteProduct @id int
				AS
				DELETE FROM Products
				WHERE ProductId=@id
				");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"DROP PROCEDURE dbo.DeleteProduct");
        }
    }
}
