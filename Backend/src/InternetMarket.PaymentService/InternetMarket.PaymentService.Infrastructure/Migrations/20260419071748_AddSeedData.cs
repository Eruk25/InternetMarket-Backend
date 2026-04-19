using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InternetMarket.PaymentService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "IsActive", "Name", "SystemName" },
                values: new object[,]
                {
                    { new Guid("4dea45ad-4dbf-4ae2-b589-cb442554e357"), true, "Банковская карта", "Card" },
                    { new Guid("f40f776b-49cf-4d0e-b209-bb7a62ca6eb9"), true, "Наличные", "Cash" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("4dea45ad-4dbf-4ae2-b589-cb442554e357"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("f40f776b-49cf-4d0e-b209-bb7a62ca6eb9"));
        }
    }
}
