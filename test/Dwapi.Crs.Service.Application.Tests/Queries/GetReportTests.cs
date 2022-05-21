using Dwapi.Crs.Service.Application.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.Crs.Service.Application.Tests.Queries
{
    [TestFixture]
    public class GetReportTests
    {
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.ServiceProvider.GetService<IMediator>();
        }

        [Test]
        public void should_Generate()
        {
            var res = _mediator.Send(new GetReport(null)).Result;
            Assert.True(res.IsSuccess);
            Assert.True(res.Value.Any());
            foreach (var reportDto in res.Value)
            {
                Log.Debug(reportDto.ToString());
            }
        }
        [Test]
        public void should_Generate_BySite()
        {
            var res = _mediator.Send(new GetReport(new[] { 12602 })).Result;
            Assert.True(res.IsSuccess);
            Assert.True(res.Value.Any());
            foreach (var reportDto in res.Value)
            {
                Log.Debug(reportDto.ToString());
            }
        }
    }
}
