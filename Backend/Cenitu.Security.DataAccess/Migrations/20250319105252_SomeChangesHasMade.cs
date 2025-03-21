using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cenitu.Security.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SomeChangesHasMade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "StockTransactionLines",
                newName: "TransactionUnitQuantity");

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "StockTransactionLines",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactionLines_UnitId",
                table: "StockTransactionLines",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactionLines_Units_UnitId",
                table: "StockTransactionLines",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactionLines_Units_UnitId",
                table: "StockTransactionLines");

            migrationBuilder.DropIndex(
                name: "IX_StockTransactionLines_UnitId",
                table: "StockTransactionLines");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "StockTransactionLines");

            migrationBuilder.RenameColumn(
                name: "TransactionUnitQuantity",
                table: "StockTransactionLines",
                newName: "Quantity");
        }
    }
}
