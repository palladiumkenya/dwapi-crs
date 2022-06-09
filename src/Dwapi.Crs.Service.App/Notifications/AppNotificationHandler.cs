using System.Threading;
using System.Threading.Tasks;
using Dwapi.Crs.Service.App.Hubs;
using Dwapi.Crs.Service.Application.Events;
using MediatR;

namespace Dwapi.Crs.Service.App.Notifications
{
    public class AppNotificationHandler:INotificationHandler<AppProgressReported>
    {
        private readonly TransmissionHub _transmissionHub;

        public AppNotificationHandler(TransmissionHub transmissionHub)
        {
            _transmissionHub = transmissionHub;
        }

        public async Task Handle(AppProgressReported notification, CancellationToken cancellationToken)
        {
           await  _transmissionHub.SendProgress(notification.AppProgress);
        }
    }
}