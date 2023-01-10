namespace Dwapi.Crs.Service.Application.Domain
{
    public class Auth0Settings
    {
        public string Authority { get; }
        public  string[] Origins { get; }
        public  string Audience { get; }
        
        public string Client { get; }
        
        public string Secret { get; }
        
        public Auth0Settings(string authority, string origins, string audience,string client,string secret)
        {
            Authority = authority;
            Audience = audience;
            Origins = GenertateParams(origins);
            Client = client;
            Secret = secret;
        }

        private string[] GenertateParams(string origins)
        {
            return origins.Split(',');
        }
    }
}