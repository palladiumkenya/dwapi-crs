using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Service.Application.Interfaces;

namespace Dwapi.Crs.Service.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly CrsServiceContext _context;

        public ClientRepository(CrsServiceContext context)
        {
            _context = context;
        }

        public List<ClientRegistry> Load(int page, int pageSize, Guid facilityId)
        {
            page = page < 0 ? 1 : page;
            pageSize = pageSize < 0 ? 1 : pageSize;
            
             
            var skip = (page - 1) * pageSize; 
            
            return _context
                .ClientRegistries
                .Where(x => x.FacilityId == facilityId)
                .OrderBy(x=>x.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToList();
        }
    }
}
