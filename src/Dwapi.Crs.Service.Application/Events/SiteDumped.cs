using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.Crs.Service.Application.Domain;
using Dwapi.Crs.Service.Application.Interfaces;
using MediatR;
using Serilog;

namespace Dwapi.Crs.Service.Application.Events
{
    public class SiteDumped : INotification
    {
        public Guid ManifestId { get; }
        public HttpStatusCode Response  { get; }
        public string ResponseInfo  { get; }
        public DateTime ResponseDate { get; } = DateTime.Now;

        public SiteDumped(Guid manifestId, HttpStatusCode response, string responseInfo)
        {
            ManifestId = manifestId;
            Response = response;
            ResponseInfo = responseInfo;
        }
    }

    public class SiteDumpedHandler:INotificationHandler<SiteDumped>
    {
        private readonly ITransmissionLogRepository _logRepository;

        public SiteDumpedHandler(ITransmissionLogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task Handle(SiteDumped notification, CancellationToken cancellationToken)
        {
            Log.Debug("Logging response...");
            var log = TransmissionLog.New(Registry.ClientRegistry, notification.Response, notification.ResponseInfo,
                notification.ManifestId);

           await  _logRepository.SaveLog(log);
        }
    }
}