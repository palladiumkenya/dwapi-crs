using System.Threading.Tasks;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Core.Interfaces.Repository;
using Dwapi.Crs.SharedKernel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.Crs.Infrastructure.Data.Repository
{
    public class DocketRepository : BaseRepository<Docket, string>, IDocketRepository
    {
        public DocketRepository(CrsContext context) : base(context)
        {
        }
        public Task<Docket> FindAsync(string docket)
        {
           var ctx=Context as CrsContext;
            return ctx.Dockets.Include(x => x.Subscribers).AsTracking().FirstOrDefaultAsync(x => x.Id == docket);
        }
    }
}
