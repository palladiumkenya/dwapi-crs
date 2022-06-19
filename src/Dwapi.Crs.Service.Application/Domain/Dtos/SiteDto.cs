namespace Dwapi.Crs.Service.Application.Domain.Dtos
{
    public class SiteDto
    {
        public int[] SiteCodes { get; set; }
        public  SiteDuplicateDto[] Sites { get; set; }
    }
}