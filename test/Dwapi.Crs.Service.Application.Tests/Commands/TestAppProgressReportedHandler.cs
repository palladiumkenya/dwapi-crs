using System.Threading;
using System.Threading.Tasks;
using Dwapi.Crs.Service.Application.Events;
using MediatR;
using Serilog;

public  class TestAppProgressReportedHandler:INotificationHandler<AppProgressReported>
{
    public Task Handle(AppProgressReported notification, CancellationToken cancellationToken)
    {
        Log.Information(new string('+',50));
        Log.Information($"{notification.AppProgress} {notification.AppProgress.WhenAgo}");
        Log.Information(new string('*',50));
        return Task.CompletedTask;

    }
}