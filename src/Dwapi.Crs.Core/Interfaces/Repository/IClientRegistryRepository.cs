using System;
using System.Collections.Generic;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.SharedKernel.Interfaces;

namespace Dwapi.Crs.Core.Interfaces.Repository
{
    public interface IClientRegistryRepository : IRepository<ClientRegistry,Guid>
    {
        void Process(Guid facilityId,IEnumerable<ClientRegistry> clients);
    }
}
