using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Core.Domain.Dto;

namespace Dwapi.Crs.Core.Interfaces.Service
{
    public interface ILiveSyncService
    {
        void SyncManifest(Manifest manifest,int clientCount);
        void SyncStats(List<Guid> facilityId);
       void SyncMetrics(List<MetricDto> metrics);
       Task SyncHandshake(List<HandshakeDto> handshakeDtos);
    }
}
