using System;
using System.Collections.Generic;
using Dwapi.Crs.SharedKernel.Model;

namespace Dwapi.Crs.SharedKernel.Tests.TestData.TestData.Models
{
    public class TestCar:Entity<Guid>
    {
        public string Name { get; set; }
        public ICollection<TestModel> Models { get; set; }=new List<TestModel>();
    }
}
