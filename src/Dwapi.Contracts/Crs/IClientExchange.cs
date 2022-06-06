namespace Dwapi.Crs.Contracts.Crs
{
    public interface IClientExchange
    {
        string ccc_no { get; set; }
        string patient_clinic_no { get; set; }
        string national_id { get; set; }
        string passport_no { get; set; }
        string huduma_no { get; set; }
        string birth_cert_no { get; set; }
        string alien_id_no { get; set; }
        string driving_license_no { get; set; }
        string first_name { get; set; }
        string middle_name { get; set; }
        string last_name { get; set; }
        string date_of_birth { get; set; }
        string sex { get; set; }
        string marital_status { get; set; }
        string occupation { get; set; }
        string education_level { get; set; }
        string phone_number { get; set; }
        string alt_phone_number { get; set; }
        string spouse_phone_number { get; set; }
        string next_of_kin_name { get; set; }
        string next_of_kin_relationship { get; set; }
        string next_of_kin_phone_number { get; set; }
        string county { get; set; }
        string subcounty { get; set; }
        string ward { get; set; }
        string location { get; set; }
        string village { get; set; }
        string landmark { get; set; }
        string facility_name { get; set; }
        string facility_mfl { get; set; }
        string date_of_initiation { get; set; }
        string treatment_outcome { get; set; }
        string date_of_last_encounter { get; set; }
        string date_of_last_viral_load { get; set; }
        string date_of_next_appointment { get; set; }
        string last_regimen { get; set; }
        string last_regimen_line { get; set; }
        
        // DateTime? DateOfHIVDiagnosis { get; set; }
        // string LastViralLoadResult { get; set; }
    }
}
