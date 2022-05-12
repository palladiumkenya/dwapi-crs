using System.Collections.Generic;
using System.Linq;
using Dwapi.Crs.Service.Application.Domain;
using FizzWare.NBuilder;

namespace Dwapi.Crs.Service.Infrastructure.Tests.TestData
{
    public class RegistryTestData
    {
        public static List<ClientRegistryDto> GetData(int count=5)
        {
            return Builder<ClientRegistryDto>.CreateListOfSize(count)
                .Build()
                .ToList();
        }
    }
}
