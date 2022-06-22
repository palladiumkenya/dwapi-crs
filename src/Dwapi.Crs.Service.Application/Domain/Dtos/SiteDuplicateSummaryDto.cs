namespace Dwapi.Crs.Service.Application.Domain.Dtos
{
    public class SiteDuplicateSummaryDto
    {
        public int PatientPk { get; set; }
        public string Name { get; set; }
        public int SiteCode { get; set; }
        public int NoOfDuplicates { get; set; }
    }
}