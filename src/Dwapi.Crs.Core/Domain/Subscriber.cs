using System;
using Dwapi.Crs.SharedKernel.Model;

namespace Dwapi.Crs.Core.Domain
{
    public class Subscriber:Entity<Guid>
    {
        public string Name { get; set; }
        public string AuthCode { get; set; }
        public string DocketId { get; set; }

        public Subscriber()
        {
        }

        public Subscriber(string name, string authCode, string docketId)
        {
            Name = name;
            AuthCode = authCode;
            DocketId = docketId;
        }
    }
}
