using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExpandRailVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Performance",
                table: "RailVehicles",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RailVehicles",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

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

            migrationBuilder.AddColumn<double>(
                name: "EquivalentRotatingWeight",
                table: "RailVehicles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Length",
                table: "RailVehicles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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
                name: "PerformanceHybrid",
                table: "RailVehicles",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ResistanceConstant",
                table: "RailVehicles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ResistanceLinear",
                table: "RailVehicles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ResistanceQuadratic",
                table: "RailVehicles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<byte>(
                name: "Wheelsets",
                table: "RailVehicles",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "TractionDiagramPoint",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Speed = table.Column<double>(type: "float", nullable: false),
                    PullForce = table.Column<double>(type: "float", nullable: false),
                    RailVehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TractionDiagramPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TractionDiagramPoint_RailVehicles_RailVehicleId",
                        column: x => x.RailVehicleId,
                        principalTable: "RailVehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TractionDiagramPoint_RailVehicleId",
                table: "TractionDiagramPoint",
                column: "RailVehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TractionDiagramPoint");

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
                name: "EquivalentRotatingWeight",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "MaxPullForce",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "MaxSpeedHybrid",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "PerformanceHybrid",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "ResistanceConstant",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "ResistanceLinear",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "ResistanceQuadratic",
                table: "RailVehicles");

            migrationBuilder.DropColumn(
                name: "Wheelsets",
                table: "RailVehicles");

            migrationBuilder.AlterColumn<double>(
                name: "Performance",
                table: "RailVehicles",
                type: "float",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RailVehicles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
