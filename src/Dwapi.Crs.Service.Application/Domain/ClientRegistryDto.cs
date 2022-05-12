using System;
using Dwapi.Crs.Contracts.Crs;
using Newtonsoft.Json;

namespace Dwapi.Crs.Service.Application.Domain
{
    public class ClientRegistryDto:IClientRegistry
    {
        [JsonProperty("ccc_no")]
        public string CCCNumber { get; set; }
        [JsonProperty("national_id")]
        public string NationalId { get; set; }
        [JsonProperty("passport_no")]
        public string Passport { get; set; }
        [JsonProperty("huduma_no")]
        public string HudumaNumber { get; set; }
        [JsonProperty("birth_cert_no")]
        public string BirthCertificateNumber { get; set; }
        [JsonProperty("alien_id_no")]
        public string AlienIdNo { get; set; }
        [JsonProperty("driving_license_no")]
        public string DrivingLicenseNumber { get; set; }
        [JsonProperty("patient_clinic_no")]
        public string PatientClinicNumber { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("middle_name")]
        public string MiddleName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; } 
        [JsonProperty("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }
        [JsonProperty("sex")]
        public string Sex { get; set; }
        [JsonProperty("marital_status")]
        public string MaritalStatus { get; set; }
        [JsonProperty("occupation")]
        public string Occupation { get; set; }
        [JsonProperty("education_level")]
        public string HighestLevelOfEducation { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
        [JsonProperty("alt_phone_number")]
        public string AlternativePhoneNumber { get; set; }
        [JsonProperty("spouse_phone_number")]
        public string SpousePhoneNumber { get; set; }
        [JsonProperty("next_of_kin_name")]
        public string NameOfNextOfKin { get; set; }
        [JsonProperty("next_of_kin_relationship")]
        public string NextOfKinRelationship { get; set; }
        [JsonProperty("next_of_kin_phone_number")]
        public string NextOfKinTelNo { get; set; }
        [JsonProperty("county")]
        public string County { get; set; }
        [JsonProperty("subcounty")]
        public string SubCounty { get; set; }
        [JsonProperty("ward")]
        public string Ward { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("village")]
        public string Village { get; set; }
        [JsonProperty("landmark")]
        public string Landmark { get; set; }
        [JsonProperty("facility_name")]
        public string FacilityName { get; set; }
        [JsonProperty("facility_mfl")]
        public string MFLCode { get; set; }
        [JsonProperty("date_of_initiation")]
        public DateTime? DateOfInitiation { get; set; }
        [JsonProperty("treatment_outcome")]
        public string TreatmentOutcome { get; set; }
        [JsonProperty("date_of_last_encounter")]
        public DateTime? DateOfLastEncounter { get; set; }
        [JsonProperty("date_of_last_viral_load")]
        public DateTime? DateOfLastViralLoad { get; set; }
        [JsonProperty("date_of_next_appointment")]
        public DateTime? NextAppointmentDate { get; set; }
        [JsonProperty("last_regimen")]
        public string LastRegimen { get; set; }
        [JsonProperty("last_regimen_line")]
        public string LastRegimenLine { get; set; }
        [JsonProperty("current_on_art")]
        public string CurrentOnART { get; set; }
        
        [JsonIgnore]
        public DateTime? Date_Created { get; set; }
        [JsonIgnore]
        public DateTime? Date_Last_Modified { get; set; }
        [JsonIgnore]
        public int? SatelliteId { get; set; }
    }
}
