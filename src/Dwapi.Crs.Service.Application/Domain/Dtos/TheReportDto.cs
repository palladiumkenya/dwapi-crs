using System;
using Humanizer;

namespace Dwapi.Crs.Service.Application.Domain.Dtos
{
    public class TheReportDto
    {
        public Guid Id { get; set; }
        public int SiteCode { get; set; }
        public string Name { get; set; }
        public long Recieved { get; set; }
        public DateTime DateArrived { get; set; }
        public long? ActiveRecords { get; set; }
        public Guid? RegistryManifestId { get; set; }
        public string Status => ResponseStatusDate.HasValue ? ResponseStatus.ToString() : "Pending";
        public Response ResponseStatus { get; set; }
        public DateTime? ResponseStatusDate { get; set; }
        public string County { get;  set; }
        public string Agency { get;  set; }
        public string Partner { get;  set; }
        public bool IsPending => Status == "Pending";
        public bool IsReady => ResponseStatus == Response.Ready;
        public bool IsTransmitted => Status != "Pending";

        public string ArrivedAgo => DateArrived.Humanize(false);
        public string ResponseAgo => ResponseStatusDate.HasValue ? ResponseStatusDate.Humanize(false) : "";

        public override string ToString()
        {
            return $"{SiteCode} {Name} {Status}";
        }
    }
}