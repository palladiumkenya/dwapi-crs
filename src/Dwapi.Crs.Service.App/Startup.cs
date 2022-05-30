using System;
using System.Collections.Generic;
using Dwapi.Crs.Service.App.Filters;
using Dwapi.Crs.Service.Application;
using Dwapi.Crs.Service.Application.Domain;
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
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.Crs.Service.App
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IWebHostEnvironment environment,IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins(GetOrigins(Configuration))
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });
            services.AddControllersWithViews();
            services.AddInfrastructure(Configuration);
            services.AddApplication();
            services.AddSwaggerGen(c=>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "DWAPI Central CRS SERVICE APP API",
                        Version = "v1",
                    });
                c.CustomSchemaIds(x => x.FullName);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true; 
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
            
            
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();
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
                    // c.SupportedSubmitMethods(new Swashbuckle.AspNetCore.SwaggerUI.SubmitMethod[] { });
                });
            }
           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            EnsureMigrationOfContext<CrsServiceContext>(serviceProvider);
         


            // #region HangFire
            // try
            // {
            //     app.UseHangfireDashboard();
            //
            //     var options = new BackgroundJobServerOptions {ServerName  = "DWAPICRSAPP",WorkerCount = 1 };
            //     app.UseHangfireServer(options);
            //     GlobalJobFilters.Filters.Add(new ProlongExpirationTimeAttribute());
            //     GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute() { Attempts = 3 });
            // }
            // catch (Exception e)
            // {
            //     Log.Fatal(e, "Hangfire is down !");
            // }
            // #endregion

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
        
        private string[] GetOrigins(IConfiguration configuration)
        {
            return configuration.GetValue<string>($"{nameof(AuthSettings)}:{nameof(AuthSettings.Origins)}").Split(',');
        }
    }
}
