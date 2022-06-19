using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Dwapi.Crs.Service.Application.Commands;
using Dwapi.Crs.Service.Application.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.Crs.Service.Application
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,List<Assembly> mediatrAssemblies = null)
        {
            services.AddAutoMapper(typeof(CrsProfile));
            if (null != mediatrAssemblies)
            {
                mediatrAssemblies.Add(typeof(DumpClientsHandler).Assembly);
                services.AddMediatR(mediatrAssemblies.ToArray());
            }
            else
            {
                services.AddMediatR(typeof(DumpClientsHandler).Assembly);    
            }
            
            return services;
        }
    }
}
