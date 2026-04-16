using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetMarket.OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameOfFieldFromUntilPriceToUnitPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UntilPrice",
                table: "OrderItems",
                newName: "UnitPrice");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "OrderItems",
                newName: "UntilPrice");
        }
    }
}
