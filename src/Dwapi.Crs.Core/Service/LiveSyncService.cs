using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Core.Domain.Dto;
using Dwapi.Crs.Core.Exchange;
using Dwapi.Crs.Core.Interfaces.Repository;
using Dwapi.Crs.Core.Interfaces.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace Dwapi.Crs.Core.Service
{
    public class LiveSyncService:ILiveSyncService
    {
        private readonly HttpClient _httpClient;
        private readonly IFacilityRepository _facilityRepository;
        private readonly JsonSerializerSettings _serializerSettings;

        public LiveSyncService(HttpClient httpClient, IFacilityRepository facilityRepository)
        {
            _httpClient = httpClient;

            _facilityRepository = facilityRepository;
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public async void SyncManifest(Manifest manifest,int clientCount)
        {
            string requestEndpoint = "manifest";
            try
            {
                var dto = new ManifestDto(manifest,clientCount);
                var content = JsonConvert.SerializeObject(dto,_serializerSettings);
                var toSend=new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(requestEndpoint,toSend);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
               Log.Error(e.Message);
            }
        }

        public async void SyncStats(List<Guid> facilityId)
        {
            string requestEndpoint = "stats";

            var stats = _facilityRepository.GetFacStats(facilityId);
            foreach (var stat in stats)
            {
                try
                {
                    var content = JsonConvert.SerializeObject(stat,_serializerSettings);
                    var toSend=new StringContent(content, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync(requestEndpoint,toSend);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }
            }
        }

        public async void SyncMetrics(List<MetricDto> metrics)
        {

            string requestEndpoint = "metric";

            try
            {
                var content = JsonConvert.SerializeObject(metrics, _serializerSettings);

                var toSend = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(requestEndpoint, toSend
                );
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                Log.Error($"{requestEndpoint} POST...");
                Log.Error(e.Message);
            }
        }

        public async Task SyncHandshake(List<HandshakeDto> dto)
        {
            string requestEndpoint = "handshake";

            try
            {
                var content = JsonConvert.SerializeObject(dto,_serializerSettings);
                var toSend=new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(requestEndpoint,toSend);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                Log.Error($"{requestEndpoint} POST...");
                Log.Error(e.Message);
            }
        }
    }
}
