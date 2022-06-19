using Dwapi.Crs.Service.Application.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

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
            var ress = _mediator.Send(new GenerateDump()).Result;
            Assert.True(ress.IsSuccess);
            
            var res = _mediator.Send(new DumpClients()).Result;
            Assert.True(res.IsSuccess);
        }
        
        [Test]
        public void should_Parse_Response()
        {
            string resp = @"
[{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{'landmark':['Ensure this field has no more than 60 characters.']}]";
            var outs= resp.Replace("{},","").Replace(",{}","");
            Log.Debug(outs);Assert.Pass();
        }
    }
}
