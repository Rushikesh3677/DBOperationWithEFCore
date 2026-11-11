using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBOperationWIthEFCoreApp.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Prices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "currencyid",
                table: "Prices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Prices_BookId",
                table: "Prices",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_currencyid",
                table: "Prices",
                column: "currencyid");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Books_BookId",
                table: "Prices",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Currencies_currencyid",
                table: "Prices",
                column: "currencyid",
                principalTable: "Currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Books_BookId",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Currencies_currencyid",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Prices_BookId",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Prices_currencyid",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "currencyid",
                table: "Prices");
        }
    }
}
