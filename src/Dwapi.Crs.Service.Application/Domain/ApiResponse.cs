using System;
using System.Net;
using Dwapi.Crs.Service.Application.Domain.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace Dwapi.Crs.Service.Application.Domain
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; }
        public string Response { get;  }
        
        public DumpResponseDto ResponseDto { get;  }
        
        public ApiResponse(HttpStatusCode statusCode, string response)
        {
            StatusCode = statusCode;
            Response = response;
            if (statusCode == HttpStatusCode.BadRequest)
            {
                try
                {
                    ResponseDto = JsonConvert.DeserializeObject<DumpResponseDto>($"DumpResponseDto:{response}");
                    Response = $"{ResponseDto}";
                }
                catch (Exception e)
                {
                    Log.Error(e, "Respone conversion Error");
                }
            }
        }
    }
}