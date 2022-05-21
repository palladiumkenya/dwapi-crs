using System;
using System.Collections.Generic;
using System.Linq;

namespace Dwapi.Crs.Service.Application.Domain
{
    public class ReportDto
    {
        public int SiteCode { get; set; }
        public string Name { get; set; }
        public long? Records { get; set; }
        public DateTime Created { get; set; }
        public bool IsSuccess => !Errors.Any();
        public List<ReportErrorDto> Errors { get; set; } = new List<ReportErrorDto>();
        public override string ToString()
        {
            return $"{SiteCode}-{Name},Success:{IsSuccess}, Errors:{Errors.Count}";
        }
    }
}