using Dwapi.Crs.Service.Application.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.Crs.Service.Application.Tests.Commands
{
    [TestFixture]
    public class GenerateDumpTests
    {
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.ServiceProvider.GetService<IMediator>();
        }

        [Test]
        public void should_Generate_Dump()
        {
            var res = _mediator.Send(new GenerateDump()).Result;
            Assert.True(res.IsSuccess);
        }
    }
}
