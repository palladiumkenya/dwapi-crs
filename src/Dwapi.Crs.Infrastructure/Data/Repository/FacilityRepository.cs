using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Core.Exchange;
using Dwapi.Crs.Core.Interfaces.Repository;
using Dwapi.Crs.SharedKernel.Infrastructure.Data;
using Dwapi.Crs.SharedKernel.Model;
using Serilog;

namespace Dwapi.Crs.Infrastructure.Data.Repository
{
    public class FacilityRepository : BaseRepository<Facility, Guid>, IFacilityRepository
    {
        public FacilityRepository(CrsContext context) : base(context)
        {
        }

        public IEnumerable<SiteProfile> GetSiteProfiles()
        {
            return GetAll().Select(x => new SiteProfile(x.SiteCode, x.Id));
        }

        public IEnumerable<SiteProfile> GetSiteProfiles(List<int> siteCodes)
        {
            return GetAll(x=>siteCodes.Contains(x.SiteCode)).Select(x => new SiteProfile(x.SiteCode, x.Id));
        }

        public IEnumerable<StatsDto> GetFacStats(IEnumerable<Guid> facilityIds)
        {
            var list = new List<StatsDto>();
            foreach (var facilityId in facilityIds)
            {
                try
                {
                    var stat = GetFacStats(facilityId);
                    if(null!=stat)
                        list.Add(stat);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }


            }
            return list;
        }

        public StatsDto GetFacStats(Guid facilityId)
        {
            string sql = $@"
select
(select top 1 {nameof(Facility.SiteCode)} from {nameof(CrsContext.Facilities)} where {nameof(Facility.Id)}='{facilityId}') FacilityCode,
(select ISNULL(max({nameof(ClientRegistry.Created)}),GETDATE()) from {nameof(CrsContext.ClientRegistries)} where {nameof(ClientRegistry.FacilityId)}='{facilityId}') Updated,
(select count(id) from {nameof(CrsContext.ClientRegistries)} where facilityid='{facilityId}') {nameof(ClientRegistry)}
";

            var result = GetDbConnection().Query<dynamic>(sql).FirstOrDefault();

            if (null != result)
            {
                var stats=new StatsDto(result.FacilityCode,result.Updated);
                stats.AddStats($"{nameof(ClientRegistry)}",result.ClientRegistry);
             
                return stats;
            }

            return null;
        }

        public Facility GetBySiteCode(int siteCode)
        {
            return DbSet.FirstOrDefault(x=>x.SiteCode==siteCode);
        }
    }
}
