using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace f1_administraiton_API.Migrations
{
    /// <inheritdoc />
    public partial class addedseedforraces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "CircuitName", "City", "Coutry" },
                values: new object[,]
                {
                    { 1, "Circuit de Monaco", "Monte Carlo", "Monaco" },
                    { 2, "Red Bull Ring", "Spielberg", "Austria" },
                    { 3, "Yas Marina Circuit", "Abu Dhabi", "United Arab Emirates" }
                });

            migrationBuilder.InsertData(
                table: "Races",
                columns: new[] { "Id", "AdditionalInfo", "EuroBasePrice", "GrandPrixName", "LocationId" },
                values: new object[,]
                {
                    { 1, "Monaco street circuit race.", 500.00m, "Monaco Grand Prix", 1 },
                    { 2, "Austrian Grand Prix race.", 250.00m, "Red Bull Ring", 2 },
                    { 3, "Season finale at Yas Marina Circuit.", 350.00m, "Abu Dhabi GP", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
