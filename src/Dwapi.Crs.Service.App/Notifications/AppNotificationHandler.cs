using System.Threading;
using System.Threading.Tasks;
using Dwapi.Crs.Service.App.Hubs;
using Dwapi.Crs.Service.Application.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.Crs.Service.App.Notifications
{
    public class AppNotificationHandler:INotificationHandler<AppProgressReported>
    {
        private readonly IHubContext<TransmissionHub> _hubContext;

        public AppNotificationHandler(IHubContext<TransmissionHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Handle(AppProgressReported notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("DisplayProgress", notification.AppProgress, cancellationToken);
        }
    }
}