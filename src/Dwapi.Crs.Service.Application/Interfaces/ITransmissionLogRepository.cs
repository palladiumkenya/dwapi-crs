using System.Threading.Tasks;
using Dwapi.Crs.Service.Application.Domain;

namespace Dwapi.Crs.Service.Application.Interfaces
{
    public interface ITransmissionLogRepository
    {
        Task<bool> SaveLog(TransmissionLog log);
    }
}
