using System;
using Dwapi.Crs.SharedKernel.Model;

namespace Dwapi.Crs.Service.Application.Domain
{
    public class TransmissionLog:Entity<Guid>
    {
        public Registry Registry  { get; set; }
        public Response Response { get; set; }
        public string ResponseInfo { get; set; }
        public Guid RegistryManifestId  { get; set; }
    }
}