using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.Crs.Service.Application.Domain;

namespace Dwapi.Crs.Service.Application.Interfaces
{
    public interface ICrsDumpService
    {
        Task<object> Dump(IEnumerable<ClientRegistryDto> clientRegistryDtos);
        Task<object> Read(string resource);
    }
}
