using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dwapi.Crs.Service.Application.Domain;
using Dwapi.Crs.Service.Application.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using Serilog;

namespace Dwapi.Crs.Service.Infrastructure.Services
{
    public class CrsDumpService : ICrsDumpService
    {
        private readonly RestClient _client;
        private readonly CrsSettings _crsSettings;

        public CrsDumpService(RestClient client, CrsSettings crsSettings)
        {
            _crsSettings = crsSettings;
            _client = client;
        }

        private RestRequest CreateGetRequest(string resource)
        {
            var request = new RestRequest(resource);
            request.AddHeader("Authorization", $"Token {_crsSettings.Secret}");
            request.AddHeader("Content-Type", "application/json");
            return request;
        }

        private RestRequest CreatePostRequest<T>(string resource, T toPost) where T : class
        {
            var request = new RestRequest(resource, Method.Post);
            request.AddHeader("Authorization", $"Token {_crsSettings.Secret}");
            request.AddJsonBody(toPost);
            return request;
        }

        private RestRequest CreatePostRequest<T>(string resource, List<T> toPost) where T : class
        {
            var request = new RestRequest(resource, Method.Post);
            request.AddHeader("Authorization", $"Token {_crsSettings.Secret}");
            request.AddJsonBody(toPost);
            return request;
        }

        public async Task<ApiResponse> Dump(ClientExchange clientRegistryDto)
        {
            try
            {
                var req = CreatePostRequest("api/client/", clientRegistryDto); 
                var res = await _client.ExecuteAsync(req);
                return new ApiResponse(res.StatusCode, res.Content);
            }
            catch (Exception e)
            {
                Log.Error(e,"Dump error");
                throw;
            }
        }

        public async Task<ApiResponse> Dump(IEnumerable<ClientExchange> clientRegistryDtos)
        {
            try
            {
                var req = CreatePostRequest("api/client/", clientRegistryDtos.ToList()); 
                var res = await _client.ExecuteAsync(req);
                return new ApiResponse(res.StatusCode, res.Content);
            }
            catch (Exception e)
            {
                Log.Error(e,"Dump error");
                throw;
            }
        }

        public async Task<ApiResponse> Read(string resource)
        {
            try
            {
                var req = CreateGetRequest(resource);
                var res = await _client.ExecuteAsync(req);
                return new ApiResponse(res.StatusCode, res.Content);
            }
            catch (Exception e)
            {
                Log.Error(e,"Read error");
                throw;
            }
        }
    }
}
