using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.DataLayer.Migrations;

/// <inheritdoc />
public partial class CreateAllPostInCategoryProcedure : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.Sql(@"
				CREATE OR ALTER PROCEDURE dbo.AllPostInCategory 
				@id int
				AS
				SELECT Id, Title2, Description, ShortDescription, Url, Published, PostedOn, Modified, Type, CategoryId, UserId, ApprovedByUserId
				FROM dbo.Posts2
				WHERE CategoryId=@id
				");

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.Sql(@"DROP PROCEDURE dbo.AllPostInCategory");
}
