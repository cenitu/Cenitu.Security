using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cenitu.Security.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RecipeLineBooleansAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "TransactionType",
                table: "StockTransactionLines",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "TransactionConversionFactor",
                table: "StockTransactionLines",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsMainIgredientOrPackaging",
                table: "RecipeLines",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRawMaterialOrSemiFinished",
                table: "RecipeLines",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMainIgredientOrPackaging",
                table: "RecipeLines");

            migrationBuilder.DropColumn(
                name: "IsRawMaterialOrSemiFinished",
                table: "RecipeLines");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionType",
                table: "StockTransactionLines",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<decimal>(
                name: "TransactionConversionFactor",
                table: "StockTransactionLines",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldNullable: true);
        }
    }
}
