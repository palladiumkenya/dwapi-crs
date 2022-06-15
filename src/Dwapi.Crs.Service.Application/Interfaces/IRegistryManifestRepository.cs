using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Service.Application.Domain;
using Dwapi.Crs.Service.Application.Domain.Dtos;
using Dwapi.Crs.SharedKernel.Custom;

namespace Dwapi.Crs.Service.Application.Interfaces
{
    public interface IRegistryManifestRepository
    {
        Task<List<Manifest>> GetFirstTimers();
        Task<int> Generate(IProgress<AppProgress> progress = null);
        Task<int> Process(IProgress<AppProgress> progress = null);
        Task<List<RegistryManifest>> GetReadyForSending(bool newOnly = true, int[] siteCode = null);
        Task<List<RegistryManifest>> GetNewForSending(int[] siteCode = null);
        Task<List<RegistryManifest>> GetFailedForSending(int[] siteCode = null);
        Task<RegistryManifest> GetErrorReport(int siteCode);
        Task<List<TheReportDto>> GetTheReport();
    }
}
