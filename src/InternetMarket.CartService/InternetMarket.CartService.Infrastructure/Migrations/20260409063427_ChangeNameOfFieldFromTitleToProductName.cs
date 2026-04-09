using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetMarket.CartService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameOfFieldFromTitleToProductName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "CartItems",
                newName: "ProductName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "CartItems",
                newName: "Title");
        }
    }
}
