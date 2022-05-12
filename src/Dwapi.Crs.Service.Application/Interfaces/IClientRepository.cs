using System;
using System.Collections.Generic;
using Dwapi.Crs.Core.Domain;

namespace Dwapi.Crs.Service.Application.Interfaces
{
    public interface IClientRepository
    {
        List<ClientRegistry> Load(int page, int pageSize, Guid facilityId);
    }
}
