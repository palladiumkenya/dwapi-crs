using System;
using Dwapi.Crs.Service.App.Filters;
using Dwapi.Crs.Service.Infrastructure;
using Dwapi.Crs.SharedKernel.Infrastructure.Data;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.Crs.Service.App
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment,IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddInfrastructure(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseSwagger();
            if (env.IsDevelopment())
            {
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DWAPI Central CRS SERVICE APP API");
                    //c.SupportedSubmitMethods(new Swashbuckle.AspNetCore.SwaggerUI.SubmitMethod[] { });
                });
            }
            else
            {
                app.UseSwaggerUI(c =>
                {
                    // Check if it NOT IIS c.SwaggerEndpoint("/swagger/v1/swagger.json", "DWAPI Central CRS API");
                    c.SwaggerEndpoint("../swagger/v1/swagger.json", "DWAPI Central CRS SERVICE APP API");
                    c.SupportedSubmitMethods(new Swashbuckle.AspNetCore.SwaggerUI.SubmitMethod[] { });
                });
            }
           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            EnsureMigrationOfContext<CrsServiceContext>(serviceProvider);
         


            #region HangFire
            try
            {
                app.UseHangfireDashboard();

                var options = new BackgroundJobServerOptions {ServerName  = "DWAPICRSAPP",WorkerCount = 1 };
                app.UseHangfireServer(options);
                GlobalJobFilters.Filters.Add(new ProlongExpirationTimeAttribute());
                GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute() { Attempts = 3 });
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Hangfire is down !");
            }
            #endregion

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

            Log.Debug(@"initializing Database [Complete]");
            Log.Debug(
                @"---------------------------------------------------------------------------------------------------");
            Log.Debug(@"

                        ________                          .__   
                        \______ \__  _  _______  ______ |__|  
                         |    |  \ \/ \/ /\__  \ \____ \|  | 
                         |    `   \     /  / __ \|  |_> >  |   
                        /_______  /\/\_/  (____  /   __/|__| /\  [ SERVICE APP ]
                                \/             \/|__|        \/       

            ");
            Log.Debug(
                @"---------------------------------------------------------------------------------------------------");
            Log.Debug("Dwapi Central CRS started !");
        }

        public static void EnsureMigrationOfContext<T>(IServiceProvider app) where T : BaseContext
        {
            var contextName = typeof(T).Name;
            Log.Debug($"initializing Database context: {contextName}");
            var context = app.GetService<T>();
            try
            {
                context.Database.Migrate();
                context.EnsureSeeded();
                Log.Debug($"initializing Database context: {contextName} [OK]");
            }
            catch (Exception e)
            {
                Log.Debug($"initializing Database context: {contextName} Error");
                Log.Debug($"{e}");
            }
        }
    }
}
