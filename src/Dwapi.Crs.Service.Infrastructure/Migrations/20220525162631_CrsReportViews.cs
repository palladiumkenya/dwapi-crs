using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.Crs.Service.Infrastructure.Migrations
{
    public partial class CrsReportViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                      
create view vAllManifests as
SELECT        
	Manifests.Id, Manifests.SiteCode, Manifests.Name, Manifests.Recieved, Manifests.DateArrived, 
	RegistryManifests.ActiveRecords, RegistryManifests.Id AS RegistryManifestId
FROM           
	Manifests LEFT OUTER JOIN	RegistryManifests ON Manifests.Id = RegistryManifests.ManifestId
                        ");
            migrationBuilder.Sql(@"
                      create view vTransmissionReport as
SELECT        
	RegistryManifestId, MIN(Response) AS ResponseStatus, MAX(Created) AS ResponseStatusDate
FROM            
	TransmissionLogs
GROUP BY 
	RegistryManifestId

                        ");
            migrationBuilder.Sql(@"
                      
CREATE VIEW vTheReport
AS
SELECT        
	vAllManifests.Id, vAllManifests.SiteCode, vAllManifests.Name, vAllManifests.Recieved, vAllManifests.DateArrived, vAllManifests.ActiveRecords, vAllManifests.RegistryManifestId, 
    vTransmissionReport.ResponseStatus, vTransmissionReport.ResponseStatusDate
FROM            
	vAllManifests LEFT OUTER JOIN vTransmissionReport ON vAllManifests.RegistryManifestId = vTransmissionReport.RegistryManifestId

                        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop view vTheReport");
            migrationBuilder.Sql(@"drop view vTransmissionReport");
            migrationBuilder.Sql(@"drop view vAllManifests");
        }
    }
}
