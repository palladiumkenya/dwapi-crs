﻿using System;

namespace Dwapi.Crs.Service.Application.Domain
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

        public override string ToString()
        {
            return $"{SiteCode} {Name} {Status}";
        }
    }
}