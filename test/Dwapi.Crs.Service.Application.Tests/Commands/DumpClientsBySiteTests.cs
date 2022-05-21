using Dwapi.Crs.Service.Application.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.Crs.Service.Application.Tests.Commands
{
    [TestFixture]
    public class DumpClientsBySiteTests
    {
        
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.ServiceProvider.GetService<IMediator>();
        }
        
        [Test]
        public void should_Dump_Client()
        {
            var res = _mediator.Send(new DumpClientsBySite(new []{12602})).Result;
            Assert.True(res.IsSuccess);
        }
    }
}
