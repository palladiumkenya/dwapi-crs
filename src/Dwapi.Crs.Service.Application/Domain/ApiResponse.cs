using System;
using System.Net;

namespace Dwapi.Crs.Service.Application.Domain
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; }
        public string? Response { get;  }
        
        public ApiResponse(HttpStatusCode statusCode, string? response)
        {
            StatusCode = statusCode;
            Response = response;
        }
    }
}