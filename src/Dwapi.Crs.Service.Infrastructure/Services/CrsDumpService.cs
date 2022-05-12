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
    public class CrsDumpService:ICrsDumpService
    {
        private readonly RestClient _client;
        private readonly CrsSettings _crsSettings;
        

        public CrsDumpService(RestClient client,CrsSettings crsSettings)
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

        private RestRequest CreatePostRequest<T>(string resource ,List<T> toPost) where T :class
        {
            var request = new RestRequest(resource,Method.Post);
            request.AddHeader("Authorization", $"Token {_crsSettings.Secret}");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json; charset=utf-8", toPost, ParameterType.RequestBody);
            return request;
        }
        public async Task<object> Dump(IEnumerable<ClientRegistryDto> clientRegistryDtos)
        {
            try
            {
                var req = CreatePostRequest("api/client", clientRegistryDtos.ToList());
                var res= await _client.ExecuteAsync(req);
                return res.Content;
            }
            catch (Exception e)
            {
                Log.Error("Dump error", e);
                throw;
            }
        }

        public async Task<object> Read(string resource)
        {
            try
            {
                var client = new RestClient(resource);
                var request = new RestRequest();
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Token 6dbcb8b85d09542641f9e9c12cff2eb6da5538c9");
                request.AddParameter("application/json", "{\n  \"ccc_no\": \"string\",\n  \"patient_clinic_no\": \"string\",\n  \"national_id\": \"string\",\n  \"passport_no\": \"string\",\n  \"huduma_no\": \"string\",\n  \"birth_cert_no\": \"string\",\n  \"alien_id_no\": \"string\",\n  \"driving_license_no\": \"string\",\n  \"first_name\": \"string\",\n  \"middle_name\": \"string\",\n  \"last_name\": \"string\",\n  \"date_of_birth\": \"2022-05-11\",\n  \"sex\": \"Male\",\n  \"marital_status\": \"Single\",\n  \"occupation\": \"string\",\n  \"education_level\": \"string\",\n  \"phone_number\": \"string\",\n  \"alt_phone_number\": \"string\",\n  \"spouse_phone_number\": \"string\",\n  \"next_of_kin_name\": \"string\",\n  \"next_of_kin_relationship\": \"string\",\n  \"next_of_kin_phone_number\": \"string\",\n  \"county\": \"string\",\n  \"subcounty\": \"string\",\n  \"ward\": \"string\",\n  \"location\": \"string\",\n  \"village\": \"string\",\n  \"landmark\": \"string\",\n  \"facility_name\": \"string\",\n  \"facility_mfl\": \"string\",\n  \"date_of_initiation\": \"2022-05-11\",\n  \"treatment_outcome\": \"string\",\n  \"date_of_last_encounter\": \"2022-05-11\",\n  \"date_of_last_viral_load\": \"2022-05-11\",\n  \"date_of_next_appointment\": \"2022-05-11\",\n  \"last_regimen\": \"string\",\n  \"last_regimen_line\": \"string\"\n}",  ParameterType.RequestBody);
                var response =await client.ExecuteAsync(request);
                Console.WriteLine(response.Content);
                return response.Content;
            }
            catch (Exception e)
            {
                Log.Error("Dump error", e);
                throw;
            }
        }
    }
}
