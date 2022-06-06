using System;
using Dwapi.Crs.SharedKernel.Model;

namespace Dwapi.Crs.Service.Application.Domain
{
    public class SpotFacility : Entity<Guid>
    {
        public int Code { get;  set; }
        public string Name { get;  set; }
        public string County { get;  set; }
        public string Agency { get;  set; }
        public string Partner { get;  set; }
    }
}