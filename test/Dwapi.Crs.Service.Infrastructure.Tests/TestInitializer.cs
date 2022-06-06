using System;
using Dwapi.Crs.Service.Application.Interfaces;
using Dwapi.Crs.Service.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.Crs.Service.Infrastructure.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
        public static IServiceProvider ServiceProvider;

        [OneTimeSetUp]
        public void Init()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();


            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();
            services.AddInfrastructure(config);
            ServiceProvider = services.BuildServiceProvider();
            
            InitDB();
        }

        private void InitDB()
        {
            var ctxx = ServiceProvider.GetService<CrsServiceContext>();
            ctxx.Database.Migrate();
        }
    }
}

