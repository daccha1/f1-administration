using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace f1_administraiton_API.Migrations
{
    /// <inheritdoc />
    public partial class ModelFixing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "SeatingZones",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "RaceDays",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "SeatingZones");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "RaceDays");
        }
    }
}
