using System.Threading.Tasks;

namespace Dwapi.Crs.Service.Application.Interfaces
{
    public interface IRegistryManifestRepository
    {
        Task Generate();
    }
}
