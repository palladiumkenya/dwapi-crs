using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.Crs.Infrastructure.Migrations
{
    public partial class CrsReve01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentOnART",
                table: "ClientRegistries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentOnART",
                table: "ClientRegistries");
        }
    }
}
