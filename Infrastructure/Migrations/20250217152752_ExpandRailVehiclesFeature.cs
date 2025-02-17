using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExpandRailVehiclesFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TractionDiagramPoint_RailVehicles_RailVehicleId",
                table: "TractionDiagramPoint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TractionDiagramPoint",
                table: "TractionDiagramPoint");

            migrationBuilder.DropColumn(
                name: "DrivingWheelsets",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "EfficiencyDependent",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "EfficiencyIndependent",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "MaxPullForce",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "MaxSpeedHybrid",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "Performance",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "PerformanceHybrid",
                table: "RailVehicles");

            migrationBuilder.RenameTable(
                name: "TractionDiagramPoint",
                newName: "TractionDiagramPoints");

            migrationBuilder.RenameColumn(
                name: "RailVehicleId",
                table: "TractionDiagramPoints",
                newName: "VehicleTractionSystemId");

            migrationBuilder.RenameIndex(
                name: "IX_TractionDiagramPoint_RailVehicleId",
                table: "TractionDiagramPoints",
                newName: "IX_TractionDiagramPoints_VehicleTractionSystemId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "RailVehicles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TractionDiagramPoints",
                table: "TractionDiagramPoints",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ElectrificationTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Voltage = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectrificationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectrificationTypes_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ElectrificationTypes_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ElectrificationTypes_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ElectrificationTypes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trains",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MaxPullForce = table.Column<short>(type: "smallint", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trains_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trains_AspNetUsers_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trains_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trains_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTractionSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ElectrificationTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VoltageCoefficient = table.Column<double>(type: "float", nullable: true),
                    DrivingWheelsets = table.Column<byte>(type: "tinyint", nullable: false),
                    MaxSpeed = table.Column<short>(type: "smallint", nullable: false),
                    Performance = table.Column<short>(type: "smallint", nullable: false),
                    MaxPullForce = table.Column<short>(type: "smallint", nullable: false),
                    Efficiency = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTractionSystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleTractionSystems_ElectrificationTypes_ElectrificationTypeId",
                        column: x => x.ElectrificationTypeId,
                        principalTable: "ElectrificationTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehicleTractionSystems_RailVehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "RailVehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainVehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleCount = table.Column<short>(type: "smallint", nullable: false),
                    Position = table.Column<short>(type: "smallint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TrainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainVehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainVehicles_RailVehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "RailVehicles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrainVehicles_Trains_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Trains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectrificationTypes_CreatedBy",
                table: "ElectrificationTypes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ElectrificationTypes_DeletedBy",
                table: "ElectrificationTypes",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ElectrificationTypes_UpdatedBy",
                table: "ElectrificationTypes",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ElectrificationTypes_UserId",
                table: "ElectrificationTypes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Trains_CreatedBy",
                table: "Trains",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Trains_DeletedBy",
                table: "Trains",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Trains_UpdatedBy",
                table: "Trains",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Trains_UserId",
                table: "Trains",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainVehicles_TrainId",
                table: "TrainVehicles",
                column: "TrainId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainVehicles_VehicleId",
                table: "TrainVehicles",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTractionSystems_ElectrificationTypeId",
                table: "VehicleTractionSystems",
                column: "ElectrificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTractionSystems_VehicleId",
                table: "VehicleTractionSystems",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_TractionDiagramPoints_VehicleTractionSystems_VehicleTractionSystemId",
                table: "TractionDiagramPoints",
                column: "VehicleTractionSystemId",
                principalTable: "VehicleTractionSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TractionDiagramPoints_VehicleTractionSystems_VehicleTractionSystemId",
                table: "TractionDiagramPoints");

            migrationBuilder.DropTable(
                name: "TrainVehicles");

            migrationBuilder.DropTable(
                name: "VehicleTractionSystems");

            migrationBuilder.DropTable(
                name: "Trains");

            migrationBuilder.DropTable(
                name: "ElectrificationTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TractionDiagramPoints",
                table: "TractionDiagramPoints");

            migrationBuilder.RenameTable(
                name: "TractionDiagramPoints",
                newName: "TractionDiagramPoint");

            migrationBuilder.RenameColumn(
                name: "VehicleTractionSystemId",
                table: "TractionDiagramPoint",
                newName: "RailVehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_TractionDiagramPoints_VehicleTractionSystemId",
                table: "TractionDiagramPoint",
                newName: "IX_TractionDiagramPoint_RailVehicleId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "RailVehicles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<byte>(
                name: "DrivingWheelsets",
                table: "RailVehicles",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<double>(
                name: "EfficiencyDependent",
                table: "RailVehicles",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "EfficiencyIndependent",
                table: "RailVehicles",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "MaxPullForce",
                table: "RailVehicles",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "MaxSpeedHybrid",
                table: "RailVehicles",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Performance",
                table: "RailVehicles",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "PerformanceHybrid",
                table: "RailVehicles",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TractionDiagramPoint",
                table: "TractionDiagramPoint",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TractionDiagramPoint_RailVehicles_RailVehicleId",
                table: "TractionDiagramPoint",
                column: "RailVehicleId",
                principalTable: "RailVehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
