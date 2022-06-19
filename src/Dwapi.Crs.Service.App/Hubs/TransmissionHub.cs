using System.Threading.Tasks;
using Dwapi.Crs.SharedKernel.Custom;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.Crs.Service.App.Hubs
{
    public class TransmissionHub : Hub
    {
        public async Task SendProgress(AppProgress progress)
        {
            await Clients.All.SendAsync("DisplayProgress", progress);
        }
    }
}