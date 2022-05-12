using System;
using Dwapi.Crs.Service.Application.Domain;
using Dwapi.Crs.Service.Application.Interfaces;
using Dwapi.Crs.Service.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using RestSharp.Authenticators;

namespace Dwapi.Crs.Service.Infrastructure
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var crsSettings = new CrsSettings(
                configuration.GetValue<string>($"{nameof(CrsSettings)}:{nameof(CrsSettings.Url)}"),
                configuration.GetValue<bool>($"{nameof(CrsSettings)}:{nameof(CrsSettings.CertificateValidation)}"),
                configuration.GetValue<string>($"{nameof(CrsSettings)}:{nameof(CrsSettings.Client)}"),
                configuration.GetValue<string>($"{nameof(CrsSettings)}:{nameof(CrsSettings.Secret)}")
            );

            var options = new RestClientOptions(crsSettings.Url)
            {
                FollowRedirects = false
            };
           
            if (!crsSettings.CertificateValidation)
                options.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            services.AddSingleton<CrsSettings>(crsSettings);
            services.AddSingleton(new RestClient(options));
            services.AddScoped<ICrsDumpService, CrsDumpService>();

            return services;
        }
    }
}
