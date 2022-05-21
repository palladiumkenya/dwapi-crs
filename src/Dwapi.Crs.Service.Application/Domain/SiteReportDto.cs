namespace Dwapi.Crs.Service.Application.Domain
{
    public class SiteReportDto
    {
        public int SiteCode { get; set; }
        public string Name { get; set; }
        public int? Attempts { get; set; }
        public string Transmitted => Attempts.HasValue && Attempts.Value > 0 ? "Yes" : "No";
        public override string ToString()
        {
            return $"{SiteCode},{Name} {Transmitted}";
        }
    }
}