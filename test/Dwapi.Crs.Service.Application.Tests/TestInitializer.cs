using System;
using Dwapi.Crs.Service.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.Crs.Service.Application.Tests
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
            services.AddApplication();
            ServiceProvider = services.BuildServiceProvider();
            
            InitDB();
        }

        private void InitDB()
        {
            var context = ServiceProvider.GetService<CrsServiceContext>();
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
    }
}

