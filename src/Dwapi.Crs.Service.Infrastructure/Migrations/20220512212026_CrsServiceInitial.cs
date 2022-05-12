using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.Crs.Service.Infrastructure.Migrations
{
    public partial class CrsServiceInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistryManifests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    ManifestId = table.Column<Guid>(nullable: false),
                    FacilityId = table.Column<Guid>(nullable: false),
                    SiteCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Records = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistryManifests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransmissionLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Registry = table.Column<int>(nullable: false),
                    Response = table.Column<int>(nullable: false),
                    ResponseInfo = table.Column<string>(nullable: true),
                    RegistryManifestId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransmissionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransmissionLogs_RegistryManifests_RegistryManifestId",
                        column: x => x.RegistryManifestId,
                        principalTable: "RegistryManifests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransmissionLogs_RegistryManifestId",
                table: "TransmissionLogs",
                column: "RegistryManifestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransmissionLogs");

            migrationBuilder.DropTable(
                name: "RegistryManifests");
        }
    }
}
