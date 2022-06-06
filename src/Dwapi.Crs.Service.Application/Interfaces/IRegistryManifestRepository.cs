using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Service.Application.Domain;
using Dwapi.Crs.Service.Application.Domain.Dtos;

namespace Dwapi.Crs.Service.Application.Interfaces
{
    public interface IRegistryManifestRepository
    {
        Task<List<Manifest>> GetFirstTimers();
        Task<bool> Generate();
        Task<bool> Process();
        Task<List<RegistryManifest>> GetReadyForSending(int [] siteCode=null);
        
        Task<RegistryManifest> GetErrorReport(int siteCode);
        Task<List<TheReportDto>> GetTheReport();
    }
}
