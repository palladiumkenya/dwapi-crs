using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.Crs.Infrastructure.Migrations
{
    public partial class CrsSatellite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SatelliteId",
                table: "ClientRegistries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SatelliteId",
                table: "ClientRegistries");
        }
    }
}
