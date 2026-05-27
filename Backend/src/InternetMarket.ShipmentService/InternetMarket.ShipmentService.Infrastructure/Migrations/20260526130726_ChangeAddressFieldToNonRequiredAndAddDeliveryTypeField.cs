using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetMarket.ShipmentService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAddressFieldToNonRequiredAndAddDeliveryTypeField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CdekCities");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Shipments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryType",
                table: "Shipments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryType",
                table: "Shipments");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Shipments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CdekCities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CdekCities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CdekCities_Name",
                table: "CdekCities",
                column: "Name");
        }
    }
}
