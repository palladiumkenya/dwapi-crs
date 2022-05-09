using System.Collections.Generic;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.SharedKernel.Interfaces;

namespace Dwapi.Crs.Core.Interfaces.Repository
{
    public interface IMasterFacilityRepository:IRepository<MasterFacility,int>
    {
        MasterFacility GetBySiteCode(int siteCode);
        List<MasterFacility> GetLastSnapshots(int siteCode);
    }
}
