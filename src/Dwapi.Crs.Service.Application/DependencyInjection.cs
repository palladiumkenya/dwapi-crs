using System;
using AutoMapper;
using Dwapi.Crs.Service.Application.Commands;
using Dwapi.Crs.Service.Application.Domain;
using Dwapi.Crs.Service.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.Crs.Service.Infrastructure
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
