using System;
using System.Collections.Generic;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.SharedKernel.Model;

namespace Dwapi.Crs.Service.Application.Domain
{
    public class RegistryManifest:Entity<Guid>
    {
        public Guid ManifestId  { get; set; }
        public int SiteCode { get; set; }
        public string Name { get; set; }
        public ICollection<TransmissionLog> TransmissionLogs { get; set; } = new List<TransmissionLog>();
        
        private RegistryManifest()
        {
        }

        public RegistryManifest(Guid manifestId, int siteCode, string name)
        {
            ManifestId = manifestId;
            SiteCode = siteCode;
            Name = name;
        }

        public static  RegistryManifest Create(Manifest firstTime)
        {
            return new RegistryManifest(firstTime.Id, firstTime.SiteCode, firstTime.Name);
        }

      
    }
}
