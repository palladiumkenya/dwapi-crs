using Dwapi.Crs.Service.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.Crs.Service.Infrastructure.Tests.Persistence.Repositories
{
    [TestFixture]
    public class RegistryManifestRepositoryTests
    {
        private IRegistryManifestRepository _manifestRepository;

        [SetUp]
        public void SetUp()
        {
            _manifestRepository = TestInitializer.ServiceProvider.GetService<IRegistryManifestRepository>();
        }
        [Test]
        public void should_Get_First_Complete_Upload()
        {
            var manifests = _manifestRepository.GetFirstTimers().Result;
            Assert.True(manifests.Any());
        }
        
        [Test]
        public void should_Generate()
        {
            var res = _manifestRepository.Generate().Result;
            Assert.True(res);
        }
    }
}
