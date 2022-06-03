using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.Crs.Service.Infrastructure.Migrations
{
    public partial class SpotFacs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpotFacilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Code = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    Agency = table.Column<string>(nullable: true),
                    Partner = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpotFacilities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpotFacilities");
        }
    }
}
