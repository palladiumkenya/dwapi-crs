using System;
using System.Linq;
using System.Net.Http;
using Dwapi.Crs.Core.Command;
using Dwapi.Crs.Core.Interfaces.Repository;
using Dwapi.Crs.Core.Interfaces.Service;
using Dwapi.Crs.Core.Service;
using Dwapi.Crs.Infrastructure.Data;
using Dwapi.Crs.Infrastructure.Data.Repository;
using Dwapi.Crs.SharedKernel.Enums;
using Dwapi.Crs.SharedKernel.Tests.TestData.TestData;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Z.Dapper.Plus;

namespace Dwapi.Crs.Core.Tests.Service
{
    public class ManifestHandlersTests
    {
        private ServiceProvider _serviceProvider;
        private CrsContext _context;
        private IManifestService _manifestService;
        private IMediator _mediator;

        [OneTimeSetUp]
        public void Init()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["ConnectionStrings:DwapiConnectionDev"];
            var liveSync = config["LiveSync"];
            Uri endPointA = new Uri(liveSync); // this is the endpoint HttpClient will hit
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = endPointA,
            };
            DapperPlusManager.AddLicense("1755;700-ThePalladiumGroup", "2073303b-0cfc-fbb9-d45f-1723bb282a3c");
            if (!Z.Dapper.Plus.DapperPlusManager.ValidateLicense(out var licenseErrorMessage))
            {
                throw new Exception(licenseErrorMessage);
            }

            _serviceProvider = new ServiceCollection()
                .AddDbContext<CrsContext>(o => o.UseSqlServer(connectionString))

                .AddScoped<IDocketRepository, DocketRepository>()
                .AddScoped<IMasterFacilityRepository, MasterFacilityRepository>()

                .AddScoped<IFacilityRepository, FacilityRepository>()
                .AddScoped<IManifestRepository, ManifestRepository>()
                .AddScoped<IClientRegistryRepository, ClientRegistryRepository>()

                .AddScoped<IFacilityRepository, FacilityRepository>()
                .AddScoped<IMasterFacilityRepository, MasterFacilityRepository>()
                .AddScoped<IClientRegistryRepository, ClientRegistryRepository>()
                .AddScoped<IManifestRepository, ManifestRepository>()


                .AddScoped<ICrsService, CrsService>()
                .AddScoped<ILiveSyncService, LiveSyncService>()
                .AddScoped<IManifestService, ManifestService>()
                .AddSingleton<HttpClient>(httpClient)
                .AddMediatR(typeof(ValidateFacilityHandler))
                .BuildServiceProvider();
            _context = _serviceProvider.GetService<CrsContext>();
            _context.Database.EnsureDeleted();
            _context.Database.Migrate();
            _context.MasterFacilities.AddRange(TestDataFactory.TestMasterFacilities());
            var facilities = TestDataFactory.TestFacilities();
            _context.Facilities.AddRange(facilities);
            _context.SaveChanges();
            _context.ClientRegistries.AddRange(TestDataFactory.TestClients(1, facilities.First(x => x.SiteCode == 1).Id));
            _context.ClientRegistries.AddRange(TestDataFactory.TestClients(2, facilities.First(x => x.SiteCode == 2).Id));
            _context.SaveChanges();

            //1,
        }

        [SetUp]
        public void SetUp()
        {
            _manifestService = _serviceProvider.GetService<IManifestService>();
            _mediator = _serviceProvider.GetService<IMediator>();
        }

        [Test]
        public void should_Clear_By_Site()
        {
            var sitePatients = _context.ClientRegistries.ToList();
            Assert.True(sitePatients.Any(x=>x.SiteCode==1));
            Assert.True(sitePatients.Any(x => x.SiteCode == 2));

            var manifests = TestDataFactory.TestManifests(1);
            manifests.ForEach(x =>
            {
                x.SiteCode = 1;
                x.EmrSetup = EmrSetup.SingleFacility;
            });
            var id=_mediator.Send(new SaveManifest(manifests.First())).Result;
            _manifestService.Process(manifests.First().SiteCode);

            var remainingPatients = _context.ClientRegistries.ToList();
            Assert.False(remainingPatients.Any(x => x.SiteCode == 1 && x.Project!="IRDO"));
            Assert.True(remainingPatients.Any(x => x.SiteCode == 2 && x.Project!="IRDO"));
        }

        [Test]
        public void should_Clear_By_Community_Site()
        {
            var sitePatients = _context.ClientRegistries.ToList();
            Assert.True(sitePatients.Any(x=>x.SiteCode==1));
            Assert.True(sitePatients.Any(x => x.SiteCode == 2));

            var manifests = TestDataFactory.TestManifests(1);
            manifests.ForEach(x =>
            {
                x.SiteCode = 2;
                x.EmrSetup = EmrSetup.Community;
            });
            var id=_mediator.Send(new SaveManifest(manifests.First())).Result;
            _manifestService.Process(manifests.First().SiteCode);

            var remainingPatients = _context.ClientRegistries.ToList();
            Assert.False(remainingPatients.Any(x => x.SiteCode == 2 && x.Project=="IRDO"));
            Assert.True(remainingPatients.Any(x => x.SiteCode == 1 && x.Project=="IRDO"));
        }
    }
}
