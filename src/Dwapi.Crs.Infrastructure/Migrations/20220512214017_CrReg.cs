using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.Crs.Infrastructure.Migrations
{
    public partial class CrReg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastRegimen",
                table: "ClientRegistries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastRegimenLine",
                table: "ClientRegistries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastRegimen",
                table: "ClientRegistries");

            migrationBuilder.DropColumn(
                name: "LastRegimenLine",
                table: "ClientRegistries");
        }
    }
}
