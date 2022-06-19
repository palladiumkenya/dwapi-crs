using System;
using System.Collections.Generic;
using System.Linq;

namespace Dwapi.Crs.Service.Application.Domain.Dtos
{
    public class SiteDuplicateDto
    {
        public int SiteCode { get; set; }
        public string Name { get; set; }
        public List<int> PatientPks { get; set; }
        public string AllPatientPks => string.Join(',', PatientPks);
        public int Total => PatientPks.Count;

        public static List<SiteDuplicateDto> New(List<SiteDuplicateSummaryDto> summaryDtos)
        {
            var list = new List<SiteDuplicateDto>();
            var x = summaryDtos.GroupBy(x => new {x.SiteCode,x.Name});

            foreach (var g in x)
            {
                var site = new SiteDuplicateDto
                {
                    SiteCode = g.Key.SiteCode,
                    Name = g.Key.Name,
                    PatientPks = g.Select(x => x.PatientPk).ToList()
                };
                list.Add(site);
            }

            return list;
        }
    }
}