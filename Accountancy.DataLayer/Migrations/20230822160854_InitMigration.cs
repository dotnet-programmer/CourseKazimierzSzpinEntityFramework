using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accountancy.DataLayer.Migrations;

/// <inheritdoc />
public partial class InitMigration : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "Attributes",
			columns: table => new
			{
				AttributeId = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Attributes", x => x.AttributeId);
			});

		migrationBuilder.CreateTable(
			name: "Customers",
			columns: table => new
			{
				CustomerId = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
				Nip = table.Column<string>(type: "nvarchar(max)", nullable: false),
				PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
				Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
				IsDeleted = table.Column<bool>(type: "bit", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Customers", x => x.CustomerId);
			});

		migrationBuilder.CreateTable(
			name: "Products",
			columns: table => new
			{
				ProductId = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
				Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Products", x => x.ProductId);
			});

		migrationBuilder.CreateTable(
			name: "Addresses",
			columns: table => new
			{
				AddressId = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				State = table.Column<string>(type: "nvarchar(max)", nullable: false),
				City = table.Column<string>(type: "nvarchar(max)", nullable: false),
				Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
				PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
				CustomerId = table.Column<int>(type: "int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Addresses", x => x.AddressId);
				table.ForeignKey(
					name: "FK_Addresses_Customers_CustomerId",
					column: x => x.CustomerId,
					principalTable: "Customers",
					principalColumn: "CustomerId",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "Invoices",
			columns: table => new
			{
				InvoiceId = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				Number = table.Column<int>(type: "int", nullable: false),
				Year = table.Column<int>(type: "int", nullable: false),
				Month = table.Column<byte>(type: "tinyint", nullable: false),
				Type = table.Column<int>(type: "int", nullable: false),
				CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
				TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
				IsPaid = table.Column<bool>(type: "bit", nullable: false),
				CustomerId = table.Column<int>(type: "int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
				table.ForeignKey(
					name: "FK_Invoices_Customers_CustomerId",
					column: x => x.CustomerId,
					principalTable: "Customers",
					principalColumn: "CustomerId",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AttributeProduct",
			columns: table => new
			{
				AttributesAttributeId = table.Column<int>(type: "int", nullable: false),
				ProductsProductId = table.Column<int>(type: "int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AttributeProduct", x => new { x.AttributesAttributeId, x.ProductsProductId });
				table.ForeignKey(
					name: "FK_AttributeProduct_Attributes_AttributesAttributeId",
					column: x => x.AttributesAttributeId,
					principalTable: "Attributes",
					principalColumn: "AttributeId",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					name: "FK_AttributeProduct_Products_ProductsProductId",
					column: x => x.ProductsProductId,
					principalTable: "Products",
					principalColumn: "ProductId",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "InvoicePositions",
			columns: table => new
			{
				InvoicePositionId = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				Quantity = table.Column<int>(type: "int", nullable: false),
				InvoiceId = table.Column<int>(type: "int", nullable: false),
				ProductId = table.Column<int>(type: "int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_InvoicePositions", x => x.InvoicePositionId);
				table.ForeignKey(
					name: "FK_InvoicePositions_Invoices_InvoiceId",
					column: x => x.InvoiceId,
					principalTable: "Invoices",
					principalColumn: "InvoiceId",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					name: "FK_InvoicePositions_Products_ProductId",
					column: x => x.ProductId,
					principalTable: "Products",
					principalColumn: "ProductId",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.InsertData(
			table: "Customers",
			columns: new[] { "CustomerId", "Email", "IsDeleted", "Name", "Nip", "PhoneNumber" },
			values: new object[] { 1, "jakis.mail@poczta.pl", false, "Maciek Kowalski", "111-222-33-44", "1234567890" });

		migrationBuilder.InsertData(
			table: "Addresses",
			columns: new[] { "AddressId", "City", "CustomerId", "PostalCode", "State", "Street" },
			values: new object[] { 1, "Kielce", 1, "12-345", "Polska", "Warszawska 66/6" });

		migrationBuilder.CreateIndex(
			name: "IX_Addresses_CustomerId",
			table: "Addresses",
			column: "CustomerId",
			unique: true);

		migrationBuilder.CreateIndex(
			name: "IX_AttributeProduct_ProductsProductId",
			table: "AttributeProduct",
			column: "ProductsProductId");

		migrationBuilder.CreateIndex(
			name: "IX_InvoicePositions_InvoiceId",
			table: "InvoicePositions",
			column: "InvoiceId");

		migrationBuilder.CreateIndex(
			name: "IX_InvoicePositions_ProductId",
			table: "InvoicePositions",
			column: "ProductId");

		migrationBuilder.CreateIndex(
			name: "IX_Invoices_CustomerId",
			table: "Invoices",
			column: "CustomerId");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "Addresses");

		migrationBuilder.DropTable(
			name: "AttributeProduct");

		migrationBuilder.DropTable(
			name: "InvoicePositions");

		migrationBuilder.DropTable(
			name: "Attributes");

		migrationBuilder.DropTable(
			name: "Invoices");

		migrationBuilder.DropTable(
			name: "Products");

		migrationBuilder.DropTable(
			name: "Customers");
	}
}
