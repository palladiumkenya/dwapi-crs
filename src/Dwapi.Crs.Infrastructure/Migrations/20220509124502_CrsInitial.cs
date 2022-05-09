using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.Crs.Infrastructure.Migrations
{
    public partial class CrsInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dockets",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Instance = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dockets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterFacilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 120, nullable: true),
                    County = table.Column<string>(maxLength: 120, nullable: true),
                    SnapshotDate = table.Column<DateTime>(nullable: true),
                    SnapshotSiteCode = table.Column<int>(nullable: true),
                    SnapshotVersion = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterFacilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    AuthCode = table.Column<string>(nullable: true),
                    DocketId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscribers_Dockets_DocketId",
                        column: x => x.DocketId,
                        principalTable: "Dockets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    SiteCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 120, nullable: true),
                    MasterFacilityId = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Emr = table.Column<string>(nullable: true),
                    SnapshotDate = table.Column<DateTime>(nullable: true),
                    SnapshotSiteCode = table.Column<int>(nullable: true),
                    SnapshotVersion = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facilities_MasterFacilities_MasterFacilityId",
                        column: x => x.MasterFacilityId,
                        principalTable: "MasterFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientRegistries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    PatientPk = table.Column<int>(nullable: false),
                    SiteCode = table.Column<int>(nullable: false),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    Processed = table.Column<bool>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: true),
                    FacilityId = table.Column<Guid>(nullable: false),
                    CCCNumber = table.Column<string>(nullable: true),
                    NationalId = table.Column<string>(nullable: true),
                    Passport = table.Column<string>(nullable: true),
                    HudumaNumber = table.Column<string>(nullable: true),
                    BirthCertificateNumber = table.Column<string>(nullable: true),
                    AlienIdNo = table.Column<string>(nullable: true),
                    DrivingLicenseNumber = table.Column<string>(nullable: true),
                    PatientClinicNumber = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    HighestLevelOfEducation = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    AlternativePhoneNumber = table.Column<string>(nullable: true),
                    SpousePhoneNumber = table.Column<string>(nullable: true),
                    NameOfNextOfKin = table.Column<string>(nullable: true),
                    NextOfKinRelationship = table.Column<string>(nullable: true),
                    NextOfKinTelNo = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    SubCounty = table.Column<string>(nullable: true),
                    Ward = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Village = table.Column<string>(nullable: true),
                    Landmark = table.Column<string>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    MFLCode = table.Column<string>(nullable: true),
                    DateOfInitiation = table.Column<DateTime>(nullable: true),
                    TreatmentOutcome = table.Column<string>(nullable: true),
                    DateOfLastEncounter = table.Column<DateTime>(nullable: true),
                    DateOfLastViralLoad = table.Column<DateTime>(nullable: true),
                    NextAppointmentDate = table.Column<DateTime>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRegistries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientRegistries_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manifests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    SiteCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Sent = table.Column<int>(nullable: false),
                    Recieved = table.Column<int>(nullable: false),
                    DateLogged = table.Column<DateTime>(nullable: false),
                    DateArrived = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StatusDate = table.Column<DateTime>(nullable: false),
                    FacilityId = table.Column<Guid>(nullable: false),
                    EmrId = table.Column<Guid>(nullable: true),
                    EmrName = table.Column<string>(nullable: true),
                    EmrSetup = table.Column<int>(nullable: false),
                    Session = table.Column<Guid>(nullable: true),
                    Start = table.Column<DateTime>(nullable: true),
                    End = table.Column<DateTime>(nullable: true),
                    Tag = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manifests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manifests_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cargoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Items = table.Column<string>(nullable: true),
                    ManifestId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cargoes_Manifests_ManifestId",
                        column: x => x.ManifestId,
                        principalTable: "Manifests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes_ManifestId",
                table: "Cargoes",
                column: "ManifestId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRegistries_FacilityId",
                table: "ClientRegistries",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_MasterFacilityId",
                table: "Facilities",
                column: "MasterFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Manifests_FacilityId",
                table: "Manifests",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_DocketId",
                table: "Subscribers",
                column: "DocketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cargoes");

            migrationBuilder.DropTable(
                name: "ClientRegistries");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "Manifests");

            migrationBuilder.DropTable(
                name: "Dockets");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "MasterFacilities");
        }
    }
}
