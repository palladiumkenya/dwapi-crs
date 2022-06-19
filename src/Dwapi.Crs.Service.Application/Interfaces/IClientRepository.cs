using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Service.Application.Domain.Dtos;
using Dwapi.Crs.SharedKernel.Custom;

namespace Dwapi.Crs.Service.Application.Interfaces
{
    public interface IClientRepository
    {
        List<ClientRegistry> Load(int page, int pageSize, Guid facilityId);
        public List<SiteDuplicateDto> LoadDuplicateSummary();
        List<ClientRegistry> LoadDuplicates(SiteDuplicateDto siteDuplicate);
        Task<int> DeDuplicate(SiteDuplicateDto siteDuplicate,IProgress<AppProgress> progress = null);
    }
}
