using AutoMapper;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.SharedKernel.Utils;

namespace Dwapi.Crs.Service.Application.Domain.Resolvers
{
    public class NameResolver : IValueResolver<ClientRegistry, ClientExchange, string>
    {
        public string Resolve(ClientRegistry source, ClientExchange destination, string member, ResolutionContext context)
        {
            return source.LastName.Transfrom("Name").ToUpper();
        }
    }
    
    public class SexResolver : IValueResolver<ClientRegistry, ClientExchange, string>
    {
        public string Resolve(ClientRegistry source, ClientExchange destination, string member, ResolutionContext context)
        {
            return source.LastName.Transfrom("Sex").ToUpper();
        }
    }
    
    public class MaritalResolver : IValueResolver<ClientRegistry, ClientExchange, string>
    {
        public string Resolve(ClientRegistry source, ClientExchange destination, string member, ResolutionContext context)
        {
            return source.LastName.Transfrom("Marital").ToUpper();
        }
    }
    
    public class PhoneResolver : IValueResolver<ClientRegistry, ClientExchange, string>
    {
        public string Resolve(ClientRegistry source, ClientExchange destination, string member, ResolutionContext context)
        {
            return source.LastName.Transfrom("Name").ToUpper();
        }
    }
    
    public class DateResolver : IValueResolver<ClientRegistry, ClientExchange, string>
    {
        public string Resolve(ClientRegistry source, ClientExchange destination, string member, ResolutionContext context)
        {
            return source.LastName.Transfrom("Name").ToUpper();
        }
    }
}