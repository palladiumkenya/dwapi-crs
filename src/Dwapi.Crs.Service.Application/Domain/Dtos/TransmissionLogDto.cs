using System;

namespace Dwapi.Crs.Service.Application.Domain.Dtos
{
    public class TransmissionLogDto
    {
        public Response Response { get; set; }
        public string ResponseInfo { get; set; }
        public virtual DateTime? Created { get; set; }
    }
}