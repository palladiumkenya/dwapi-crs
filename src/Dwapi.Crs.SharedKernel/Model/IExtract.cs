using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Dwapi.Crs.SharedKernel.Model
{
    public interface IExtract
    {
        int PatientPk { get; set; }
        int SiteCode { get; set; }
        string Emr { get; set; }
        string Project { get; set; }
        bool? Processed { get; set; }
        string QueueId { get; set; }
        string Status { get; set; }
        DateTime? StatusDate { get; set; }
        DateTime? DateExtracted { get; set; }
        Guid FacilityId { get; set; }
    }
}
