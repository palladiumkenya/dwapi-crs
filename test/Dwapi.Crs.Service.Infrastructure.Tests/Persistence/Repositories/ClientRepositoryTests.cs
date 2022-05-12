using System;
using Dwapi.Crs.Service.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;

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
            Assert.True(clients.Any());
            Assert.AreEqual(res,clients.Count);
        }
    }
}
