using System;

namespace Dwapi.Crs.Service.Application.Domain
{
    public class ReportErrorDto
    {
        public DateTime ErrorDate { get; set; }
        public string Error { get; set; }
        public string ErrorDetail { get; set; }
    }
}