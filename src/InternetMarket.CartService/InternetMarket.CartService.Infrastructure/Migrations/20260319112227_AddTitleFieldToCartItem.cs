using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetMarket.CartService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTitleFieldToCartItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UntilPrice",
                table: "CartItems",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "CartItems",
                newName: "UntilPrice");
        }
    }
}
