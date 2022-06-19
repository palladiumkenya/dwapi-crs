using System;
using Dwapi.Crs.Service.Application.Domain;
using Dwapi.Crs.Service.Application.Interfaces;
using Dwapi.Crs.Service.Infrastructure.Repositories;
using Dwapi.Crs.Service.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using RestSharp.Authenticators;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.Crs.Service.Infrastructure
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var authSettings = new AuthSettings(
                configuration.GetValue<string>($"{nameof(AuthSettings)}:{nameof(AuthSettings.Authority)}"),
                configuration.GetValue<string>($"{nameof(AuthSettings)}:{nameof(AuthSettings.Origins)}"),
                configuration.GetValue<string>($"{nameof(AuthSettings)}:{nameof(AuthSettings.Client)}"),
                configuration.GetValue<string>($"{nameof(AuthSettings)}:{nameof(AuthSettings.Secret)}")
            );
            
            var crsSettings = new CrsSettings(
                configuration.GetValue<string>($"{nameof(CrsSettings)}:{nameof(CrsSettings.Url)}"),
                configuration.GetValue<bool>($"{nameof(CrsSettings)}:{nameof(CrsSettings.CertificateValidation)}"),
                configuration.GetValue<string>($"{nameof(CrsSettings)}:{nameof(CrsSettings.Client)}"),
                configuration.GetValue<string>($"{nameof(CrsSettings)}:{nameof(CrsSettings.Secret)}"),
                configuration.GetValue<int>($"{nameof(CrsSettings)}:{nameof(CrsSettings.Batches)}")
            );

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = authSettings.Authority;
                    options.RequireHttpsMetadata = false;
         
                    // options.Audience = "crsserviceapi";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,ValidateIssuer=false
                        
                        
                    };
                });
                // .AddOpenIdConnect("oidc", opt =>
                // {
                //     opt.SignInScheme = "Cookies";
                //     opt.Authority = authSettings.Authority;;
                //     opt.ClientId = authSettings.Client;;
                //     opt.ResponseType = "code id_token";
                //     opt.SaveTokens = true;
                //     opt.ClientSecret = authSettings.Secret;
                // });
                
            
            var options = new RestClientOptions(crsSettings.Url)
            {
                FollowRedirects = false
            };

            if (!crsSettings.CertificateValidation)
                options.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            services.AddDbContext<CrsServiceContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DwapiConnection"),
                    b => b.MigrationsAssembly(typeof(CrsServiceContext).Assembly.FullName)));

            services.AddSingleton<CrsSettings>(crsSettings);
            services.AddSingleton(new RestClient(options));
            services.AddScoped<ICrsDumpService, CrsDumpService>();
            
            services.AddScoped<IRegistryManifestRepository, RegistryManifestRepository>();
            services.AddScoped<ITransmissionLogRepository, TransmissionLogRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            
            try
            {
                DapperPlusManager.AddLicense("1755;700-ThePalladiumGroup", "218460a6-02d0-c26b-9add-e6b8d13ccbf4");
                if (!DapperPlusManager.ValidateLicense(out var licenseErrorMessage))
                {
                    throw new Exception(licenseErrorMessage);
                }
            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                throw;
            }

            return services;
        }
    }
}
