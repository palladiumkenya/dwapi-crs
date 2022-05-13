using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.Crs.Service.Application.Domain;

namespace Dwapi.Crs.Service.Application.Interfaces
{
    public interface ICrsDumpService
    {
        Task<ApiResponse> Dump(ClientExchange clientRegistryDto);
        Task<ApiResponse> Dump(IEnumerable<ClientExchange> clientRegistryDtos);
        Task<ApiResponse> Read(string resource);
    }
}
