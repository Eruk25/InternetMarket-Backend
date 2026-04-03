using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetMarket.ProductService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConversionToAddressField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Providers");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_ZipCode",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "Address_ZipCode",
                table: "Providers");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Providers",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");
        }
    }
}
