using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cenitu.Security.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TransactionSourcePropertyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionSource",
                table: "StockTransactions",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionSource",
                table: "StockTransactions");
        }
    }
}
