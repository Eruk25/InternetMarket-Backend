using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetMarket.UserService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEmailToVerificationToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NewEmail",
                table: "EmailVerificationTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewEmail",
                table: "EmailVerificationTokens");
        }
    }
}
