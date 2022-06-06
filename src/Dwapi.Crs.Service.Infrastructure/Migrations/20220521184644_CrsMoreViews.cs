using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.Crs.Service.Infrastructure.Migrations
{
    public partial class CrsMoreViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                        create view vFirstTimers as

                            select * from (SELECT 
                                     *,RANK() OVER (PARTITION BY SiteCode ORDER BY DateArrived ASC ) rank_no
                                FROM Manifests WHERE [End] is not null)x
                            where rank_no=1 and SiteCode not in (select SiteCode from RegistryManifests)

                        ");
            
            migrationBuilder.Sql(@"
create view vTransmissionAttempts as

SELECT        
	RegistryManifests.SiteCode,Count(TransmissionLogs.Id) Attempts
FROM            
	RegistryManifests INNER JOIN TransmissionLogs ON RegistryManifests.Id = TransmissionLogs.RegistryManifestId 
GROUP By RegistryManifests.SiteCode

                        ");
            
            migrationBuilder.Sql(@"
create view vSiteManifests as

select 
    distinct SiteCode,Max(Name) Name from Manifests
group by SiteCode
                        ");
            
            migrationBuilder.Sql(@"
create view vSiteReports as

SELECT        
    vSiteManifests.SiteCode, vSiteManifests.Name, vTransmissionAttempts.Attempts
FROM            
    vSiteManifests LEFT OUTER JOIN
vTransmissionAttempts ON vSiteManifests.SiteCode = vTransmissionAttempts.SiteCode
                        ");
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop view vFirstTimers");
            migrationBuilder.Sql(@"drop view vTransmissionAttempts");
            migrationBuilder.Sql(@"drop view vSiteManifests");
            migrationBuilder.Sql(@"drop view vSiteReports");
        }
    }
}
