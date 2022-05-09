using System.Collections.Generic;
using Dwapi.Crs.Core.Domain;

namespace Dwapi.Crs.Core.Interfaces.Service
{
    public interface ICrsService
    {
        void Process(IEnumerable<ClientRegistry> patients);
    }
}
