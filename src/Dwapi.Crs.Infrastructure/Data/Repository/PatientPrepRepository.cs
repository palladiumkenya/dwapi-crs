using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Core.Interfaces.Repository;
using Dwapi.Crs.SharedKernel.Infrastructure.Data;

namespace Dwapi.Crs.Infrastructure.Data.Repository
{
    public class ClientRegistryRepository : BaseRepository<ClientRegistry,Guid>, IClientRegistryRepository
    {
        public ClientRegistryRepository(CrsContext context) : base(context)
        {
        }

        public void Process(Guid facilityId,IEnumerable<ClientRegistry> clients)
        {
            var mpi = clients.ToList();

            if (mpi.Any())
            {
                mpi.ForEach(x => x.FacilityId = facilityId);
                CreateBulk(mpi);
            }
        }
    }
}
