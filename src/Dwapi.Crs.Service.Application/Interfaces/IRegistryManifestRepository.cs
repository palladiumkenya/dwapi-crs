using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Service.Application.Domain;

namespace Dwapi.Crs.Service.Application.Interfaces
{
    public interface IRegistryManifestRepository
    {
        Task<List<Manifest>> GetFirstTimers();
        Task<bool> Generate();
        Task<bool> Process();
        Task<List<RegistryManifest>> GetReadyForSending(int [] siteCode=null);
        
        Task<List<RegistryManifest>> GetReport(int [] siteCode=null);
        Task<List<TheReportDto>> GetTheReport();
    }
}
