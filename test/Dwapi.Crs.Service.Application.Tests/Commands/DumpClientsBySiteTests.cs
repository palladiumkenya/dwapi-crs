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
        
        [TestCase(11259)]
        public void should_Dump_Client(int site)
        {
            var ress = _mediator.Send(new GenerateDump()).Result;
            Assert.True(ress.IsSuccess);
            
            var res = _mediator.Send(new DumpClientsBySite(new []{site})).Result;
            Assert.True(res.IsSuccess);
        }
    }
}
