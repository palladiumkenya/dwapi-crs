using AutoMapper;
using Dwapi.Crs.Service.Application.Commands;
using Dwapi.Crs.Service.Application.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.Crs.Service.Application
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CrsProfile));
            services.AddMediatR(typeof(DumpClientsHandler).Assembly);
            return services;
        }
    }
}
