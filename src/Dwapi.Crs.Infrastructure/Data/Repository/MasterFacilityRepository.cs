using System.Collections.Generic;
using System.Linq;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Core.Interfaces.Repository;
using Dwapi.Crs.SharedKernel.Infrastructure.Data;

namespace Dwapi.Crs.Infrastructure.Data.Repository
{
    public class MasterFacilityRepository:BaseRepository<MasterFacility,int>, IMasterFacilityRepository
    {
        public MasterFacilityRepository(CrsContext context) : base(context)
        {
        }

        public MasterFacility GetBySiteCode(int siteCode)
        {
            return DbSet.Find(siteCode);
        }

        public List<MasterFacility> GetLastSnapshots(int siteCode)
        {
            return DbSet.Where(x =>  x.SnapshotSiteCode == siteCode)
                .ToList();
        }
    }
}
