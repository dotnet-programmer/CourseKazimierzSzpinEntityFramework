using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.DataLayer.Migrations;

/// <inheritdoc />
public partial class CreateUserFullInfoView : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		var userFullInfoView = @"
			CREATE OR ALTER VIEW [dbo].[UserFullInfoView]
			AS
			SELECT u.Id, u.Login, ci.Email
			FROM dbo.Users AS u
			LEFT JOIN dbo.ContactInfo AS ci ON ci.UserId=u.Id";

		migrationBuilder.Sql(userFullInfoView);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
		=> migrationBuilder.Sql(@"DROP VIEW dbo.UserFullInfoView");
}