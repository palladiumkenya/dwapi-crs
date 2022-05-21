using System;
using System.Net;
using Dwapi.Crs.SharedKernel.Model;

namespace Dwapi.Crs.Service.Application.Domain
{
    public class TransmissionLog:Entity<Guid>
    {
        public Registry Registry  { get;private set; }
        public Response Response { get;private  set; }
        public string ResponseInfo { get;private  set; }
        public Guid RegistryManifestId  { get; private  set; }

        private TransmissionLog()
        {
        }
        
        private TransmissionLog(Registry registry, Response response, string responseInfo, Guid registryManifestId)
        {
            Registry = registry;
            Response = response;
            ResponseInfo = responseInfo;
            RegistryManifestId = registryManifestId;
        }
        
        public static TransmissionLog New(Registry registry, HttpStatusCode response, string responseInfo, Guid registryManifestId)
        {
            var trl = new TransmissionLog(
                registry, Generate(response), responseInfo, registryManifestId
            );
            return trl;
        }

        private static Response Generate(HttpStatusCode response)
        {
            if (response == HttpStatusCode.BadRequest)
                return Response.Failed;

            return Response.Sent;
        }
    }
}