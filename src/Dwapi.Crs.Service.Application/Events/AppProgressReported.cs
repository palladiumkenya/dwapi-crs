using Dwapi.Crs.SharedKernel.Custom;
using MediatR;

namespace Dwapi.Crs.Service.Application.Events
{
    public class AppProgressReported:INotification
    {
        public AppProgress AppProgress { get; }

        public AppProgressReported(AppProgress appProgress)
        {
            AppProgress = appProgress;
        }
    }
}