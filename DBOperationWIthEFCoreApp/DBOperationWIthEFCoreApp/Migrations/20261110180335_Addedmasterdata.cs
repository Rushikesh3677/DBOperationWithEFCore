using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DBOperationWIthEFCoreApp.Migrations
{
    /// <inheritdoc />
    public partial class Addedmasterdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Languages",
                newName: "id");

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "id", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "Marathi", "Marathi" },
                    { 2, "English", "English" },
                    { 3, "Tamil", "Tamil" },
                    { 4, "Sanskrit", "Sanskrit" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Languages",
                newName: "Id");
        }
    }
}
