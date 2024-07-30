using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiExample.Migrations
{
    /// <inheritdoc />
    public partial class RailVehicleIsSoftDeletable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "RailVehicles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "RailVehicles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RailVehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_RailVehicles_DeletedBy",
                table: "RailVehicles",
                column: "DeletedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_RailVehicles_AspNetUsers_DeletedBy",
                table: "RailVehicles",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RailVehicles_AspNetUsers_DeletedBy",
                table: "RailVehicles");

            migrationBuilder.DropIndex(
                name: "IX_RailVehicles_DeletedBy",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RailVehicles");
        }
    }
}
