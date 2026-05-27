using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetMarket.CartService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWeightLengthWidthHeightAndIsLargeSizeProductFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsLargeSizeProduct",
                table: "CartItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "IsLargeSizeProduct",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "CartItems");
        }
    }
}
