using AutoMapper;
using Dwapi.Crs.Core.Domain;

namespace Dwapi.Crs.Service.Application.Domain
{
    public class CrsProfile:Profile
    {
        public CrsProfile()
        {
            CreateMap<ClientRegistry, ClientRegistryDto>();
        }
    }
}
