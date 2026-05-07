using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetMarket.ProductService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPhysicalReservedAndAvailableQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Products",
                newName: "ReservedQuantity");

            migrationBuilder.AddColumn<int>(
                name: "AvailableQuantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhysicalQuantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableQuantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PhysicalQuantity",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ReservedQuantity",
                table: "Products",
                newName: "Quantity");
        }
    }
}
