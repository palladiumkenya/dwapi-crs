using System;
using Dwapi.Crs.SharedKernel.Infrastructure.Data;
using Dwapi.Crs.SharedKernel.Tests.TestData.TestData.Interfaces;
using Dwapi.Crs.SharedKernel.Tests.TestData.TestData.Models;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.Crs.SharedKernel.Infrastructure.Tests.TestData
{

    public class TestCarRepository :BaseRepository<TestCar,Guid>,  ITestCarRepository
    {
        public TestCarRepository(DbContext context) : base(context)
        {
        }
    }
}
