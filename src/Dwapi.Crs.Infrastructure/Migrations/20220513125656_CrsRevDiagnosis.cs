using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.Crs.Infrastructure.Migrations
{
    public partial class CrsRevDiagnosis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfHIVDiagnosis",
                table: "ClientRegistries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastViralLoadResult",
                table: "ClientRegistries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfHIVDiagnosis",
                table: "ClientRegistries");

            migrationBuilder.DropColumn(
                name: "LastViralLoadResult",
                table: "ClientRegistries");
        }
    }
}
