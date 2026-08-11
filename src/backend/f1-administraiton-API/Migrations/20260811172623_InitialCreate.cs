using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace f1_administraiton_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SelectedCurrency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Coutry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CircuitName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromoCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isValid = table.Column<bool>(type: "bit", nullable: false),
                    isActivated = table.Column<bool>(type: "bit", nullable: false),
                    DiscountPercent = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrandPrixName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EuroBasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Races_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Passes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<int>(type: "int", nullable: false),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    Secret = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrenctId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    PromoCodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passes_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Passes_PromoCodes_PromoCodeId",
                        column: x => x.PromoCodeId,
                        principalTable: "PromoCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Agenda = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceDays", x => new { x.RaceId, x.Id });
                    table.ForeignKey(
                        name: "FK_RaceDays_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaddockPasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassId = table.Column<int>(type: "int", nullable: false),
                    Secret = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GarageAccess = table.Column<bool>(type: "bit", nullable: false),
                    GaragePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PitLaneAccess = table.Column<bool>(type: "bit", nullable: false),
                    PitLanePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FoodAccess = table.Column<bool>(type: "bit", nullable: false),
                    FoodPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DrinkAccess = table.Column<bool>(type: "bit", nullable: false),
                    DrinkPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaddockPasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaddockPasses_Passes_PassId",
                        column: x => x.PassId,
                        principalTable: "Passes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatingZones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RaceDayId = table.Column<int>(type: "int", nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    SeatingType = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Benefits = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatingZones", x => new { x.RaceId, x.RaceDayId, x.Id });
                    table.ForeignKey(
                        name: "FK_SeatingZones_RaceDays_RaceId_RaceDayId",
                        columns: x => new { x.RaceId, x.RaceDayId },
                        principalTable: "RaceDays",
                        principalColumns: new[] { "RaceId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PassAndZones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatingZoneRaceId = table.Column<int>(type: "int", nullable: false),
                    SeatingZoneRaceDayId = table.Column<int>(type: "int", nullable: false),
                    SeatingZoneId = table.Column<int>(type: "int", nullable: false),
                    PassId = table.Column<int>(type: "int", nullable: false),
                    AdditionalInformation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassAndZones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassAndZones_Passes_PassId",
                        column: x => x.PassId,
                        principalTable: "Passes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassAndZones_SeatingZones_SeatingZoneRaceId_SeatingZoneRaceDayId_SeatingZoneId",
                        columns: x => new { x.SeatingZoneRaceId, x.SeatingZoneRaceDayId, x.SeatingZoneId },
                        principalTable: "SeatingZones",
                        principalColumns: new[] { "RaceId", "RaceDayId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_PaddockPasses_PassId",
                table: "PaddockPasses",
                column: "PassId");

            migrationBuilder.CreateIndex(
                name: "IX_PassAndZones_PassId",
                table: "PassAndZones",
                column: "PassId");

            migrationBuilder.CreateIndex(
                name: "IX_PassAndZones_SeatingZoneRaceId_SeatingZoneRaceDayId_SeatingZoneId",
                table: "PassAndZones",
                columns: new[] { "SeatingZoneRaceId", "SeatingZoneRaceDayId", "SeatingZoneId" });

            migrationBuilder.CreateIndex(
                name: "IX_Passes_CurrencyId",
                table: "Passes",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Passes_PromoCodeId",
                table: "Passes",
                column: "PromoCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_LocationId",
                table: "Races",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaddockPasses");

            migrationBuilder.DropTable(
                name: "PassAndZones");

            migrationBuilder.DropTable(
                name: "Passes");

            migrationBuilder.DropTable(
                name: "SeatingZones");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "PromoCodes");

            migrationBuilder.DropTable(
                name: "RaceDays");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
