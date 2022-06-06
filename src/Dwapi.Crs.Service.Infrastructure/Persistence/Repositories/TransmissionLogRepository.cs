using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.Crs.Service.Application.Domain;
using Dwapi.Crs.Service.Application.Interfaces;
using Serilog;

namespace Dwapi.Crs.Service.Infrastructure.Repositories
{
    public class TransmissionLogRepository : ITransmissionLogRepository
    {
        private readonly CrsServiceContext _context;

        public TransmissionLogRepository(CrsServiceContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveLog(TransmissionLog log)
        {
            try
            {
                _context.Add(log);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Log.Error( e,"TransmissionLog error");
            }

            return false;
        }

        public async Task<bool> Clear(Guid manifestId)
        {
            try
            {
                var mani= _context.TransmissionLogs.Where(x=>x.RegistryManifestId==manifestId);
                if (mani.Any())
                {
                    _context.RemoveRange(mani);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                Log.Error( e,"TransmissionLog error");
            }

            return false;
        }
    }
}