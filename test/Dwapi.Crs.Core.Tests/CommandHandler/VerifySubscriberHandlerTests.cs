using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.Crs.Core.Command;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Core.Interfaces.Repository;
using Dwapi.Crs.Infrastructure.Data;
using Dwapi.Crs.Infrastructure.Data.Repository;
using Dwapi.Crs.SharedKernel.Exceptions;
using Dwapi.Crs.SharedKernel.Model;
using Dwapi.Crs.SharedKernel.Tests.TestData.TestData;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.Crs.Core.Tests.CommandHandler
{
    [TestFixture]
    public class VerifySubscriberHandlerTests
    {
        private ServiceProvider _serviceProvider;
        private IMediator _mediator;
        private List<Docket> _dockets;

        [OneTimeSetUp]
        public void Init()
        {
            _serviceProvider = new ServiceCollection()
                .AddDbContext<CrsContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()))
                .AddTransient<IDocketRepository, DocketRepository>()
                .AddMediatR(typeof(VerifySubscriberHandler))
                .BuildServiceProvider();

            var context = _serviceProvider.GetService<CrsContext>();
            _dockets = TestDataFactory.TestDockets();
            context.Dockets.AddRange(_dockets);
            context.SaveChanges();
        }

        [SetUp]
        public void SetUp()
        {
            _mediator = _serviceProvider.GetService<IMediator>();
        }

        [Test]
        public void should_Throw_DocketNotFoundException()
        {
            var ex = Assert.Throws<System.AggregateException>(() => CheckSubscriber(new Subscriber("x", "x", "x")));
            Assert.AreEqual(typeof(DocketNotFoundException), ex.InnerException.GetType());
            Console.WriteLine($"{ex.InnerException.Message}");
        }

        [Test]
        public void should_Throw_SubscriberNotFoundException()
        {
            var ex = Assert.Throws<System.AggregateException>(() => CheckSubscriber(new Subscriber("x", "x", _dockets.First().Id)));
            Assert.AreEqual(typeof(SubscriberNotFoundException), ex.InnerException.GetType());
            Console.WriteLine($"{ex.InnerException.Message}");
        }

        [Test]
        public void should_Throw_SubscriberNotAuthorizedException()
        {
            var docket = _dockets.First();
            var ex = Assert.Throws<System.AggregateException>(() => CheckSubscriber(new Subscriber(docket.Subscribers.First().Name,"x",docket.Id)));
            Assert.AreEqual(typeof(SubscriberNotAuthorizedException), ex.InnerException.GetType());
            Console.WriteLine($"{ex.InnerException.Message}");
        }

        [Test]
        public void should_return_Docket_Validated_Subscriber()
        {
            var subscriber = CheckSubscriber(_dockets.First().Subscribers.First());
            Assert.NotNull(subscriber);
            Assert.True(subscriber.Verified);
            Console.WriteLine(subscriber.RegistryName);
        }

        private VerificationResponse CheckSubscriber(Subscriber subscriber)
        {
            return _mediator.Send(new VerifySubscriber(subscriber.DocketId,subscriber.Name,subscriber.AuthCode)).Result;
        }
    }
}
