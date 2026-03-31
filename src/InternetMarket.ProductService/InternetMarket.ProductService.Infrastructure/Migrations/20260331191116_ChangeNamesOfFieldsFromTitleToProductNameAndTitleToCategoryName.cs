using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetMarket.ProductService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNamesOfFieldsFromTitleToProductNameAndTitleToCategoryName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Products",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Categories",
                newName: "CategoryName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "Title");
        }
    }
}
