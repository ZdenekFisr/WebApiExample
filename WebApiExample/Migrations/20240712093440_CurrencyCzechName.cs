using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiExample.Migrations
{
    /// <inheritdoc />
    public partial class CurrencyCzechName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrencyCzechNames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OneUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TwoToFourUnits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FiveOrMoreUnits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitGrammaticalGender = table.Column<int>(type: "int", nullable: false),
                    OneSubunit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TwoToFourSubunits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FiveOrMoreSubunits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubunitGrammaticalGender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyCzechNames", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyCzechNames");
        }
    }
}
