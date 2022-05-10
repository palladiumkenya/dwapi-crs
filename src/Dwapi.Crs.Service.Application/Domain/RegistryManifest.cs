using System;
using System.Collections.Generic;
using Dwapi.Crs.SharedKernel.Model;

namespace Dwapi.Crs.Service.Application.Domain
{
    public class RegistryManifest:Entity<Guid>
    {
        public Guid ManifestId  { get; set; }
        public int SiteCode { get; set; }
        public string Name { get; set; }
        public ICollection<TransmissionLog> TransmissionLogs { get; set; } = new List<TransmissionLog>();
    }
}