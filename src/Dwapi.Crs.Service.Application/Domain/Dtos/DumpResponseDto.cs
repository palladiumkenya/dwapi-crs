using System.Collections.Generic;
using System.Linq;

namespace Dwapi.Crs.Service.Application.Domain.Dtos
{
    public class DumpResponseDto
    {
        public string MaritalStatusSummary => GenerateMarital();
        public string CccNoSummary => GenerateCcc();
        public string SexSummary => GenerateSex();
        public List<string> marital_status { get; set; }
        public List<string> ccc_no { get; set; }
        public List<string> sex { get; set; }
        public List<string> date_of_hiv_diagnosis { get; set; }

        private string GenerateMarital()
        {
            if (marital_status.Any())
            {
                var res = marital_status.Distinct().ToList();
                return string.Join(',', res);
            }
            return string.Empty;
        }

        private string GenerateCcc()
        {
            if (ccc_no.Any())
            {
                var res = ccc_no.Distinct().ToList();
                return string.Join(',', res);
            }

            return string.Empty;
        }

        private string GenerateSex()
        {
            if (sex.Any())
            {
                var res = sex.Distinct().ToList();
                return  string.Join(',', res);
            }

            return string.Empty;
        }

        public override string ToString()
        {
            return $"{SexSummary},{MaritalStatusSummary},{CccNoSummary}".Trim();
        }
    }
}