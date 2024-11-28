using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.DataLayer.Migrations;

/// <inheritdoc />
public partial class CreateDeleteArticleProcedure : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.Sql(@"
				CREATE OR ALTER PROCEDURE dbo.DeleteArticle 
				@id int
				AS
				DELETE FROM Posts2
				WHERE Id=@id
				");

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.Sql(@"DROP PROCEDURE dbo.DeleteArticle");
}
