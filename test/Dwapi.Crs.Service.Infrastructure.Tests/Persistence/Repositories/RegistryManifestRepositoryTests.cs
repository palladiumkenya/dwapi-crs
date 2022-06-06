using System.Linq;
using Dwapi.Crs.Service.Application.Domain;
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
        
        [Test]
        public void should_Process()
        {
            var res = _manifestRepository.Process().Result;
            Assert.True(res);
        }
        
        [Test]
        public void should_Get_Ready_Upload()
        {
            var manifests = _manifestRepository.GetReadyForSending().Result;
            Assert.True(manifests.Any());
        }
        
        [Test]
        public void should_Get_Ready_Upload_Sites()
        {
            var manifests = _manifestRepository.GetReadyForSending(new []{13075}).Result;
            Assert.True(manifests.Any());
        }

        [Test]
        public void should_Get_ErrorReport()
        {
            var manifests = _manifestRepository.GetErrorReport(12602).Result;
            Assert.True(manifests.TransmissionLogs.Any(x => x.Response != Response.Sent));
        }
    }
}
