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

        private RestRequest CreatePostRequest<T>(string resource ,T toPost) where T :class
        {
            var request = new RestRequest(resource,Method.Post);
            request.AddHeader("Authorization", $"Token {_crsSettings.Secret}");
            request.AddJsonBody(GenertateBody(toPost));
            return request;
        }
        private RestRequest CreatePostRequest<T>(string resource ,List<T> toPost) where T :class
        {
            var request = new RestRequest(resource,Method.Post);
            request.AddHeader("Authorization", $"Token {_crsSettings.Secret}");
            request.AddJsonBody(GenertateListBody(toPost));
            return request;
        }

        private object GenertateBody(object data)
        {
            var json=JsonConvert.SerializeObject(data);
            return JsonConvert.DeserializeObject<Root>(json);
        }
        
        private object GenertateListBody(object data)
        {
            var json=JsonConvert.SerializeObject(data);
            return JsonConvert.DeserializeObject<List<Root>>(json);
        }

        public async Task<ApiResponse> Dump(ClientRegistryDto clientRegistryDto)
        {
            try
            {
                var req = CreatePostRequest("api/client/", clientRegistryDto);
                // var json = req.Parameters.FirstOrDefault(x => x.Type == ParameterType.RequestBody);
                // Log.Debug(json.Value.ToString());
                var res= await _client.ExecuteAsync(req);
                return new ApiResponse(res.StatusCode, res.Content);
            }
            catch (Exception e)
            {
                Log.Error("Dump error", e);
                throw;
            }
        }

        public async Task<ApiResponse> Dump(IEnumerable<ClientRegistryDto> clientRegistryDtos)
        {
            try
            {
                var req = CreatePostRequest("api/client/", clientRegistryDtos.ToList());
                // var json = req.Parameters.FirstOrDefault(x => x.Type == ParameterType.RequestBody);
                // Log.Debug(json.Value.ToString());
                var res= await _client.ExecuteAsync(req);
                return new ApiResponse(res.StatusCode, res.Content);
            }
            catch (Exception e)
            {
                Log.Error("Dump error", e);
                throw;
            }
        }

        public async Task<ApiResponse> Read(string resource)
        {
            try
            {
                var req = CreateGetRequest(resource);
                var res= await _client.ExecuteAsync(req);
                return new ApiResponse(res.StatusCode, res.Content);
            }
            catch (Exception e)
            {
                Log.Error("Read error", e);
                throw;
            }
        }
        
        public class Root
        {
            private string _dateOfBirth;
            private string _dateOfLastEncounter;
            private string _dateOfLastViralLoad;
            private string _dateOfNextAppointment;
            private string _dateOfInitiation;
            public string ccc_no { get; set; }
            public string national_id { get; set; }
            public string passport_no { get; set; }
            public string huduma_no { get; set; }
            public string birth_cert_no { get; set; }
            public string alien_id_no { get; set; }
            public string driving_license_no { get; set; }
            public string patient_clinic_no { get; set; }
            public string first_name { get; set; }
            public string middle_name { get; set; }
            public string last_name { get; set; }

            public string date_of_birth
            {
                get => _dateOfBirth;
                set => _dateOfBirth = "1990-10-10";
            }

            public string sex { get; set; }
            public string marital_status { get; set; }
            public string occupation { get; set; }
            public string education_level { get; set; }
            public string phone_number { get; set; }
            public string alt_phone_number { get; set; }
            public string spouse_phone_number { get; set; }
            public string next_of_kin_name { get; set; }
            public string next_of_kin_relationship { get; set; }
            public string next_of_kin_phone_number { get; set; }
            public string county { get; set; }
            public string subcounty { get; set; }
            public string ward { get; set; }
            public string location { get; set; }
            public string village { get; set; }
            public string landmark { get; set; }
            public string facility_name { get; set; }
            public string facility_mfl { get; set; }

            public string date_of_initiation
            {
                get => _dateOfInitiation;
                set => _dateOfInitiation = "1990-10-10";
            }

            public string treatment_outcome { get; set; }

            public string date_of_last_encounter
            {
                get => _dateOfLastEncounter;
                set => _dateOfLastEncounter = "1990-10-10";
            }

            public string date_of_last_viral_load
            {
                get => _dateOfLastViralLoad;
                set => _dateOfLastViralLoad= "1990-10-10";
            }

            public string date_of_next_appointment
            {
                get => _dateOfNextAppointment;
                set => _dateOfNextAppointment = "1990-10-10";
            }

            public string last_regimen { get; set; }
            public string last_regimen_line { get; set; }
            public string current_on_art { get; set; }
        }
     }
}
