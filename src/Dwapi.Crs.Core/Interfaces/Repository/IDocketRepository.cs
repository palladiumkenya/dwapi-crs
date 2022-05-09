using System.Threading.Tasks;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.SharedKernel.Interfaces;

namespace Dwapi.Crs.Core.Interfaces.Repository
{
    public interface IDocketRepository : IRepository<Docket, string>
    {
       Task<Docket> FindAsync(string docket);
    }
}