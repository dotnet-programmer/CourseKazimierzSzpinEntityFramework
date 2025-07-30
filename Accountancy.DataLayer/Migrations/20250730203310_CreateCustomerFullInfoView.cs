using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accountancy.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class CreateCustomerFullInfoView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
CREATE OR ALTER VIEW dbo.CustomerAddressView
AS
SELECT c.CustomerId, c.Name, c.Nip, c.PhoneNumber, c.Email, c.IsDeleted, ca.State, ca.City
FROM dbo.Customers AS c
LEFT JOIN dbo.CustomerAddresses AS ca ON ca.CustomerId = c.Id
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"DROP VIEW dbo.CustomerAddressView");
        }
    }
}