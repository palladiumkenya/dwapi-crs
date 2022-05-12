using System.Threading.Tasks;
using Dwapi.Crs.Service.Application.Interfaces;

namespace Dwapi.Crs.Service.Infrastructure.Repositories
{
    public class RegistryManifestRepository:IRegistryManifestRepository
    {
        private readonly CrsServiceContext _context;

        public RegistryManifestRepository(CrsServiceContext context)
        {
            _context = context;
        }

        public Task Generate()
        {
            // get first time

            string sql = "";
            
            
            throw new System.NotImplementedException();
        }
    }
}