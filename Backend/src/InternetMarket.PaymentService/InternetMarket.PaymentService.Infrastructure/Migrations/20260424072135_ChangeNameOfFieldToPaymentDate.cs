using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetMarket.PaymentService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameOfFieldToPaymentDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Transactions",
                newName: "PaymentDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "Transactions",
                newName: "UpdatedAt");
        }
    }
}
