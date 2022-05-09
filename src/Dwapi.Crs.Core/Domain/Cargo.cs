using System;
using Dwapi.Crs.SharedKernel.Enums;
using Dwapi.Crs.SharedKernel.Model;

namespace Dwapi.Crs.Core.Domain
{
    public class Cargo : Entity<Guid>
    {
        public CargoType Type { get; set; }
        public string Items { get; set; }
        public Guid ManifestId { get; set; }

        public Cargo()
        {
        }
    }
}