using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Dwapi.Crs.Service.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
                .Enrich.FromLogContext()
                .WriteTo.Console(LogEventLevel.Debug)
                .WriteTo.RollingFile(@"logs/{Date}.log", LogEventLevel.Error)
                .CreateLogger();

            try
            {
                Log.Information($"Starting Dwapi.CRS SERVICE APP...");
                var host = CreateHostBuilder(args).Build();
                //var config = host.Services.GetRequiredService<IConfiguration>();
                //BotSetup.Initialize(config);
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(GetConfig(args))
                .UseStartup<Startup>()
                .UseSerilog();

        private static IConfigurationRoot GetConfig(string[] args)
        {
            return new ConfigurationBuilder()
                .AddJsonFile("hosting.json", optional: true)
                .AddCommandLine(args).Build();
        }
    }
}
