namespace Dwapi.Crs.Service.Application.Domain
{
    public class AuthSettings
    {
        public string Authority { get; }
        public  string[] Origins { get; }
        public string Client { get; }
        public string Secret { get; }

        public AuthSettings(string authority, string origins, string client, string secret)
        {
            Authority = authority;
            Client = client;
            Secret = secret;
            Origins = GenertateParams(origins);
        }

        private string[] GenertateParams(string origins)
        {
            return origins.Split(',');
        }
    }
}