using Dwapi.Crs.Service.Application.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.Crs.Service.Application.Tests.Commands
{
    [TestFixture]
    public class DumpClientsTests
    {
        
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.ServiceProvider.GetService<IMediator>();
        }

        [Test]
        public void should_Dump_Clients()
        {
            var res = _mediator.Send(new DumpClients()).Result;
            Assert.True(res.IsSuccess);
        }
    }
}
