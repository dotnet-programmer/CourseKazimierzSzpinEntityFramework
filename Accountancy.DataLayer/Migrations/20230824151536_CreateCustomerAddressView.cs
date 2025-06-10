using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accountancy.DataLayer.Migrations;

/// <inheritdoc />
public partial class CreateCustomerAddressView : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
		=> migrationBuilder.Sql(@"
			CREATE OR ALTER VIEW dbo.CustomerAddressView
			AS
			SELECT C.CustomerId, C.Name, C.Nip, C.PhoneNumber, C.Email, C.IsDeleted, A.State + ' ' + A.City + ' ' + A.Street + ' ' + A.PostalCode AS Address
			FROM dbo.Customers AS C
			LEFT JOIN dbo.Addresses AS A ON A.CustomerId = C.CustomerId
		");

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
		=> migrationBuilder.Sql(@"DROP VIEW dbo.CustomerAddressView");
}