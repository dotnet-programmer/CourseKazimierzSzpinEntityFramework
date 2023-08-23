using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accountancy.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Configuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeProduct_Attributes_AttributesAttributeId",
                table: "AttributeProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeProduct_Products_ProductsProductId",
                table: "AttributeProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePositions_Invoices_InvoiceId",
                table: "InvoicePositions");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePositions_Products_ProductId",
                table: "InvoicePositions");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "ProductsProductId",
                table: "AttributeProduct",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "AttributesAttributeId",
                table: "AttributeProduct",
                newName: "AttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeProduct_ProductsProductId",
                table: "AttributeProduct",
                newName: "IX_AttributeProduct_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Customers",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nip",
                table: "Customers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Customers",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Attributes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Addresses",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Addresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Addresses",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Addresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Year_Month_Number",
                table: "Invoices",
                columns: new[] { "Year", "Month", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Nip",
                table: "Customers",
                column: "Nip",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_Name",
                table: "Attributes",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeProduct_Attributes_AttributeId",
                table: "AttributeProduct",
                column: "AttributeId",
                principalTable: "Attributes",
                principalColumn: "AttributeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeProduct_Products_ProductId",
                table: "AttributeProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePositions_Invoices_InvoiceId",
                table: "InvoicePositions",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePositions_Products_ProductId",
                table: "InvoicePositions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeProduct_Attributes_AttributeId",
                table: "AttributeProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeProduct_Products_ProductId",
                table: "AttributeProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePositions_Invoices_InvoiceId",
                table: "InvoicePositions");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePositions_Products_ProductId",
                table: "InvoicePositions");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Products_Name",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_Year_Month_Number",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Customers_Nip",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_Name",
                table: "Attributes");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "AttributeProduct",
                newName: "ProductsProductId");

            migrationBuilder.RenameColumn(
                name: "AttributeId",
                table: "AttributeProduct",
                newName: "AttributesAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeProduct_ProductId",
                table: "AttributeProduct",
                newName: "IX_AttributeProduct_ProductsProductId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Nip",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Attributes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeProduct_Attributes_AttributesAttributeId",
                table: "AttributeProduct",
                column: "AttributesAttributeId",
                principalTable: "Attributes",
                principalColumn: "AttributeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeProduct_Products_ProductsProductId",
                table: "AttributeProduct",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePositions_Invoices_InvoiceId",
                table: "InvoicePositions",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePositions_Products_ProductId",
                table: "InvoicePositions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
