using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.Crs.Service.Infrastructure.Migrations
{
    public partial class SpotFacsViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
ALTER VIEW vTheReport
AS
SELECT        vAllManifests.Id, vAllManifests.SiteCode, vAllManifests.Name, vAllManifests.Recieved, vAllManifests.DateArrived, vAllManifests.ActiveRecords, vAllManifests.RegistryManifestId, 
                         vTransmissionReport.ResponseStatus, vTransmissionReport.ResponseStatusDate, SpotFacilities.County, SpotFacilities.Agency, SpotFacilities.Partner
FROM            vAllManifests LEFT OUTER JOIN
                         SpotFacilities ON vAllManifests.SiteCode = SpotFacilities.Code LEFT OUTER JOIN
                         vTransmissionReport ON vAllManifests.RegistryManifestId = vTransmissionReport.RegistryManifestId
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
