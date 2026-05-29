using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetMarket.ShipmentService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentMethodFiled : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "Shipments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Shipments");
        }
    }
}
