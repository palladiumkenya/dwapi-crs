using System;
using Dwapi.Crs.SharedKernel.Interfaces;
using Dwapi.Crs.SharedKernel.Tests.TestData.TestData.Models;

namespace Dwapi.Crs.SharedKernel.Tests.TestData.TestData.Interfaces
{
    public interface ITestCarRepository : IRepository<TestCar,Guid>
    {

    }
}
