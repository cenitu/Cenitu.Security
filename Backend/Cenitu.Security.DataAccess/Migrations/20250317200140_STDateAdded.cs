using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cenitu.Security.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class STDateAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "StockTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 17, 23, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "ProductUnits",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "ProductUnits");
        }
    }
}
