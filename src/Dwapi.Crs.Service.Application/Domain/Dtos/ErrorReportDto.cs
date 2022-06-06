using System;
using System.Collections.Generic;

namespace Dwapi.Crs.Service.Application.Domain.Dtos
{
    public class ErrorReportDto
    {
        public int SiteCode { get;   set; }
        public string Name { get; set; }
        public long? Records { get; set; }
        public long? ActiveRecords { get; set; }
        public List<TransmissionLogDto> TransmissionLogs { get; set; } = new List<TransmissionLogDto>();
    }
}