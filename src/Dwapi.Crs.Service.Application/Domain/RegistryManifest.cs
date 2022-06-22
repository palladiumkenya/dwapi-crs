using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.SharedKernel.Model;

namespace Dwapi.Crs.Service.Application.Domain
{
    public class RegistryManifest:Entity<Guid>
    {
        public Guid ManifestId  { get; private set; }
        public Guid FacilityId  { get; private set; }
        public int SiteCode { get;  private set; }
        public string Name { get;private set; }
        public long? Records { get;private set; }
        public long? ActiveRecords { get;private set; }
        public ICollection<TransmissionLog> TransmissionLogs { get; set; } = new List<TransmissionLog>();
        
        public bool CanBeSent => CheckReadiness();
        public bool CanBeSentNewOnly => CheckNewness();
        public bool CanBeSentFailed => CheckFailures();

        private bool CheckReadiness()
        {
            if (!TransmissionLogs.Any())
                return true;

            if (TransmissionLogs.Any(x => x.Response == Response.Failed))
                return true;

            return false;
        }
        
        private bool CheckFailures()
        {
            if (TransmissionLogs.Any(x => x.Response == Response.Failed))
                return true;
            
            return false;
        }

        private bool CheckNewness()
        {
            if (!TransmissionLogs.Any())
                return true;

            return false;
        }
        
        private RegistryManifest()
        {
        }

        public RegistryManifest(Guid manifestId, int siteCode, string name,Guid facilityId)
        {
            ManifestId = manifestId;
            SiteCode = siteCode;
            Name = name;
            FacilityId = facilityId;
        }

        public void UpdateRecords(long count)
        {
            Records = count;
        }
        
        public void UpdateActiveRecords(long count)
        {
            ActiveRecords = count;
        }

        public static  RegistryManifest Create(Manifest firstTime)
        {
            return new RegistryManifest(firstTime.Id, firstTime.SiteCode, firstTime.Name,firstTime.FacilityId);
        }

      
    }
}
