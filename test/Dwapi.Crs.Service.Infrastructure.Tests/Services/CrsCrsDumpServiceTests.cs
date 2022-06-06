using System.Net;
using Dwapi.Crs.Service.Application.Interfaces;
using Dwapi.Crs.Service.Infrastructure.Tests.TestData;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.Crs.Service.Infrastructure.Tests.Services
{
    [TestFixture]
    public class gCrsCrsDumpServiceTests
    {
        private ICrsDumpService _crsDumpService;

        [SetUp]
        public void SetUp()
        {
            _crsDumpService = TestInitializer.ServiceProvider.GetService<ICrsDumpService>();
        }
        
        [Test]
        public void should_Dump()
        {
            var data = RegistryTestData.GetData();
            var res = _crsDumpService.Dump(data).Result;
            Assert.AreEqual(HttpStatusCode.OK,res.StatusCode);
            Assert.False(string.IsNullOrWhiteSpace(res.Response));
            Log.Debug(res.Response);
        }
        [Test]
        public void should_Read()
        {
            var res = _crsDumpService.Read("api/client/662761d3-a63a-42b1-a98d-9ad3122252e6/").Result;
            Assert.AreEqual(HttpStatusCode.OK,res.StatusCode);
            Assert.False(string.IsNullOrWhiteSpace(res.Response));
            Log.Debug(res.Response);
        }
    }
}
