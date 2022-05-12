using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.Crs.Core.Domain;

namespace Dwapi.Crs.Service.Application.Interfaces
{
    public interface IRegistryManifestRepository
    {
        Task<List<Manifest>> GetFirstTimers();
        Task<bool> Generate();
    }
}
