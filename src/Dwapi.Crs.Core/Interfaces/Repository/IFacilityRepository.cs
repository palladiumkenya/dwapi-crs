using System;
using System.Collections.Generic;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Core.Exchange;
using Dwapi.Crs.SharedKernel.Interfaces;
using Dwapi.Crs.SharedKernel.Model;

namespace Dwapi.Crs.Core.Interfaces.Repository
{
    public interface IFacilityRepository : IRepository<Facility, Guid>
    {
        IEnumerable<SiteProfile> GetSiteProfiles();
        IEnumerable<SiteProfile> GetSiteProfiles(List<int> siteCodes);

        IEnumerable<StatsDto> GetFacStats(IEnumerable<Guid> facilityIds);
        StatsDto GetFacStats(Guid facilityId);
        Facility GetBySiteCode(int siteCode);
    }
}
