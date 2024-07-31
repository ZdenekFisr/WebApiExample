using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiExample.Migrations
{
    /// <inheritdoc />
    public partial class RailVehiclesCreateUpdateHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RailVehicles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "RailVehicles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "RailVehicles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "RailVehicles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RailVehicles_CreatedBy",
                table: "RailVehicles",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RailVehicles_UpdatedBy",
                table: "RailVehicles",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_RailVehicles_AspNetUsers_CreatedBy",
                table: "RailVehicles",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RailVehicles_AspNetUsers_UpdatedBy",
                table: "RailVehicles",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RailVehicles_AspNetUsers_CreatedBy",
                table: "RailVehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_RailVehicles_AspNetUsers_UpdatedBy",
                table: "RailVehicles");

            migrationBuilder.DropIndex(
                name: "IX_RailVehicles_CreatedBy",
                table: "RailVehicles");

            migrationBuilder.DropIndex(
                name: "IX_RailVehicles_UpdatedBy",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "RailVehicles");
        }
    }
}
