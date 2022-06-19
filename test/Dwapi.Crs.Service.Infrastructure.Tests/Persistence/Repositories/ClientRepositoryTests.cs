using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Service.Application.Domain.Dtos;
using Dwapi.Crs.Service.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Dwapi.Crs.Service.Infrastructure.Tests.Persistence.Repositories
{
    [TestFixture]
    public class ClientRepositoryTests
    {
        private IClientRepository _clientRepository;

        [SetUp]
        public void SetUp()
        {
            _clientRepository = TestInitializer.ServiceProvider.GetService<IClientRepository>();
        }
        [TestCase(1,1,1)]
        [TestCase(3,11,11)]
        public void should_Get_Paged(int page,int pageSize,int res)
        {
            var clients = _clientRepository.Load(page, pageSize, new Guid("e98d512a-954d-441e-a9bf-ae930160c6e3"));
            Assert.True(EnumerableExtensions.Any(clients));
            Assert.AreEqual(res,clients.Count);
        }
        
        [Test]
        public void should_Load_Duplicate_Summary()
        {
            var clients = _clientRepository.LoadDuplicateSummary();
            Assert.True(EnumerableExtensions.Any(clients));
            
            foreach (var client in clients)
                Log.Debug($"{client.Name}-Count:{client.PatientPks.Count},[{client.AllPatientPks}]");
        }
        
        [TestCase(12602,4)]
        [TestCase(13258,2)]
        public void should_Load_Duplicates(int siteCode,int count)
        {
            var allSites = _clientRepository.LoadDuplicateSummary();
            var site = allSites.First(x => x.SiteCode == siteCode);
            
            var clients = _clientRepository.LoadDuplicates(site);
            Assert.AreEqual(count,clients.Count);

            foreach (var client in clients)
                Log.Debug($"{client.Id},{client.PatientPk}-{client.SiteCode}-{client.CCCNumber}");
        }
        
        [TestCase(12602,2)]
        [TestCase(13258,1)]
        public void should_DeDuplicates(int siteCode,int count)
        {
            var allSites = _clientRepository.LoadDuplicateSummary();
            var site = allSites.First(x => x.SiteCode == siteCode);
            
            var removed = _clientRepository.DeDuplicate(site).Result;
            
            Assert.AreEqual(count,removed);
        }
    }
}
