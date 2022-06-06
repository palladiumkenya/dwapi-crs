namespace Dwapi.Crs.Service.Application.Domain
{
    public class CrsSettings
    {
        public string Url { get; }
        public bool CertificateValidation { get; }
        public string Client { get; }
        public string Secret { get; }
        
        public int Batches { get; }

        public CrsSettings(string url, bool certificateValidation, string client, string secret,int batches)
        {
            Url = url;
            CertificateValidation = certificateValidation;
            Client = client;
            Secret = secret;
            Batches = batches;
        }
    }
}
