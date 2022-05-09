using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Core.Interfaces.Repository;
using Dwapi.Crs.Core.Interfaces.Service;
using Dwapi.Crs.SharedKernel.Exceptions;
using Dwapi.Crs.SharedKernel.Model;
using Serilog;

namespace Dwapi.Crs.Core.Service
{
    public class CrsService : ICrsService
    {
        private readonly ILiveSyncService _syncService;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IClientRegistryRepository _patientCrsRepository;
        private List<SiteProfile> _siteProfiles = new List<SiteProfile>();

        public CrsService(ILiveSyncService syncService, IFacilityRepository facilityRepository,
            IClientRegistryRepository patientCrsRepository)
        {
            _syncService = syncService;
            _facilityRepository = facilityRepository;
            _patientCrsRepository = patientCrsRepository;
        }

        public void Process(IEnumerable<ClientRegistry> patients)
        {
            List<Guid> facilityIds = new List<Guid>();

            if (null == patients)
                return;
            if (!patients.Any())
                return;

            _siteProfiles = _facilityRepository.GetSiteProfiles().ToList();

            var batch = new List<ClientRegistry>();
            int count = 0;

            foreach (var patient in patients)
            {
                count++;
                try
                {
                    patient.FacilityId = GetFacilityId(patient.SiteCode);
                    patient.UpdateRefId();
                    batch.Add(patient);

                    facilityIds.Add(patient.FacilityId);
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Facility Id missing {patient.SiteCode}");
                }


                if (count == 1000)
                {
                    _patientCrsRepository.CreateBulk(batch);
                    count = 0;
                    batch = new List<ClientRegistry>();
                }

            }

            if (batch.Any())
                _patientCrsRepository.CreateBulk(batch);

            SyncClients(facilityIds);

        }

        public Guid GetFacilityId(int siteCode)
        {
            var profile = _siteProfiles.FirstOrDefault(x => x.SiteCode == siteCode);
            if (null == profile)
                throw new FacilityNotFoundException(siteCode);

            return profile.FacilityId;
        }

        private void SyncClients(List<Guid> facIlds)
        {
            if (facIlds.Any())
            {
                _syncService.SyncStats(facIlds.Distinct().ToList());
            }
        }
    }
}
