using Dwapi.Crs.Contracts.Crs;

namespace Dwapi.Crs.Service.Application.Domain
{
    public class ClientExchange : IClientExchange
    {
        public string ccc_no { get; set; }
        public string patient_clinic_no { get; set; }
        public string national_id { get; set; }
        public string passport_no { get; set; }
        public string huduma_no { get; set; }
        public string birth_cert_no { get; set; }
        public string alien_id_no { get; set; }
        public string driving_license_no { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string date_of_birth { get; set; }
        public string sex { get; set; }
        public string marital_status { get; set; }
        public string occupation { get; set; }
        public string education_level { get; set; }
        public string phone_number { get; set; }
        public string alt_phone_number { get; set; }
        public string spouse_phone_number { get; set; }
        public string next_of_kin_name { get; set; }
        public string next_of_kin_relationship { get; set; }
        public string next_of_kin_phone_number { get; set; }
        public string county { get; set; }
        public string subcounty { get; set; }
        public string ward { get; set; }
        public string location { get; set; }
        public string village { get; set; }
        public string landmark { get; set; }
        public string facility_name { get; set; }
        public string facility_mfl { get; set; }
        public string date_of_initiation { get; set; }
        public string treatment_outcome { get; set; }
        public string date_of_last_encounter { get; set; }
        public string date_of_last_viral_load { get; set; }
        public string date_of_next_appointment { get; set; }
        public string last_regimen { get; set; }
        public string last_regimen_line { get; set; }
        public string current_on_art { get; set; }
        public string date_of_hiv_diagnosis { get; set; }
        public string last_viral_load_result { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(ccc_no);
        }
    }
}
