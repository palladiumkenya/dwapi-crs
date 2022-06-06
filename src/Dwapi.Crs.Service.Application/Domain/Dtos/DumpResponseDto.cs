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
            if (marital_status.Any())
            {
                var res = marital_status.Distinct().ToList();
                return string.Join(',', res);
            }

            return string.Empty;
        }

        private string GenerateSex()
        {
            if (marital_status.Any())
            {
                var res = marital_status.Distinct().ToList();
                return  string.Join(',', res);
            }

            return string.Empty;
        }

        public override string ToString()
        {
            return $"{SexSummary},{MaritalStatusSummary},{CccNoSummary}";
        }
    }
}