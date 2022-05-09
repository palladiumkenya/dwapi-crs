using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Dwapi.Crs.Core.Command;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Core.Interfaces.Repository;
using Dwapi.Crs.Core.Interfaces.Service;
using Dwapi.Crs.Core.Service;
using Dwapi.Crs.Infrastructure.Data;
using Dwapi.Crs.Infrastructure.Data.Repository;
using Dwapi.Crs.SharedKernel.Tests.TestData.TestData;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Z.Dapper.Plus;

namespace Dwapi.Crs.Core.Tests.Service
{
    public class CrsServiceTests
    {
        private ServiceProvider _serviceProvider;
        private List<ClientRegistry> _patientIndices;

        private List<ClientRegistry> _patientIndicesSiteB;
        private CrsContext _context;
        private ICrsService _crsService;
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


            DapperPlusManager.AddLicense("1755;700-ThePalladiumGroup", "2073303b-0cfc-fbb9-d45f-1723bb282a3c");
            if (!Z.Dapper.Plus.DapperPlusManager.ValidateLicense(out var licenseErrorMessage))
            {
                throw new Exception(licenseErrorMessage);
            }


            Uri endPointA = new Uri(liveSync); // this is the endpoint HttpClient will hit
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = endPointA,
            };


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
            _patientIndices = TestDataFactory
                .TestClients(1, facilities.First(x => x.SiteCode == 1).Id);
            _patientIndicesSiteB =
                TestDataFactory
                    .TestClients(2, facilities.First(x => x.SiteCode == 2).Id);
        }

        [SetUp]
        public void SetUp()
        {
            _manifestService = _serviceProvider.GetService<IManifestService>();
            _mediator = _serviceProvider.GetService<IMediator>();
            _crsService = _serviceProvider.GetService<ICrsService>();
        }


        [Test]
        public void should_Process_After_Manifest()
        {
            var manifest = TestDataFactory.TestManifests(1).First();
            manifest.SiteCode = 1;
            var patients = _context.ClientRegistries.ToList();
            Assert.False(patients.Any());

            var id = _mediator.Send(new SaveManifest(manifest)).Result;
            _manifestService.Process(manifest.SiteCode);
            _crsService.Process(_patientIndices);
            Assert.True(_context.ClientRegistries.Any(x=>x.SiteCode==1));
        }

        [Test]
        public void should_Process_Recodrds_Without_Clients()
        {
            var manifests = TestDataFactory.TestManifests();
            manifests[0].SiteCode = 1;
            manifests[1].SiteCode = 2;
            var patients = _context.ClientRegistries.ToList();
            Assert.False(patients.Any());

            var id = _mediator.Send(new SaveManifest(manifests[0])).Result;
            _manifestService.Process(manifests[0].SiteCode);
            _crsService.Process(_patientIndices);
            Assert.True(_context.ClientRegistries.Any(x => x.SiteCode == 1));

            var id2 = _mediator.Send(new SaveManifest(manifests[1])).Result;
            _manifestService.Process(manifests[1].SiteCode);
            _crsService.Process(_patientIndicesSiteB);
            Assert.True(_context.ClientRegistries.Any(x => x.SiteCode == 1));
            Assert.True(_context.ClientRegistries.Any(x => x.SiteCode == 2));
        }
    }
}
