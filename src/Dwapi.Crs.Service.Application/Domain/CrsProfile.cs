using AutoMapper;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.SharedKernel.Utils;

namespace Dwapi.Crs.Service.Application.Domain
{
    public class CrsProfile:Profile
    {
        public CrsProfile()
        { 
            CreateMap<ClientRegistry, ClientExchange>()
                .ForMember(dest => dest.ccc_no, opt => opt.MapFrom(src =>
                    src.CCCNumber))
                .ForMember(dest => dest.national_id, opt => opt.MapFrom(src =>
                    src.NationalId))
                .ForMember(dest => dest.passport_no, opt => opt.MapFrom(src =>
                    src.Passport))
                .ForMember(dest => dest.huduma_no, opt => opt.MapFrom(src =>
                    src.HudumaNumber))
                .ForMember(dest => dest.birth_cert_no, opt => opt.MapFrom(src =>
                    src.BirthCertificateNumber))
                .ForMember(dest => dest.alien_id_no, opt => opt.MapFrom(src =>
                    src.AlienIdNo))
                .ForMember(dest => dest.driving_license_no, opt => opt.MapFrom(src =>
                    src.DrivingLicenseNumber))
                .ForMember(dest => dest.patient_clinic_no, opt => opt.MapFrom(src =>
                    src.PatientClinicNumber))
                .ForMember(dest => dest.first_name, opt => opt.MapFrom(src =>
                    src.FirstName.ToLower()))
                .ForMember(dest => dest.middle_name, opt => opt.MapFrom(src =>
                    src.MiddleName.ToUpper()))
                .ForMember(dest => dest.last_name, opt => opt.MapFrom(src =>
                    src.LastName.ToUpper()))
                .ForMember(dest => dest.date_of_birth, opt => opt.MapFrom(src =>
                    src.DateOfBirth.ToDateFormat()))
                .ForMember(dest => dest.sex, opt => opt.MapFrom(src =>
                    src.Sex))
                .ForMember(dest => dest.marital_status, opt => opt.MapFrom(src =>
                    src.MaritalStatus))
                .ForMember(dest => dest.occupation, opt => opt.MapFrom(src =>
                    src.Occupation))
                .ForMember(dest => dest.education_level, opt => opt.MapFrom(src =>
                    src.HighestLevelOfEducation))
                .ForMember(dest => dest.phone_number, opt => opt.MapFrom(src =>
                    src.PhoneNumber))
                .ForMember(dest => dest.alt_phone_number, opt => opt.MapFrom(src =>
                    src.AlternativePhoneNumber))
                .ForMember(dest => dest.spouse_phone_number, opt => opt.MapFrom(src =>
                    src.SpousePhoneNumber))
                .ForMember(dest => dest.next_of_kin_name, opt => opt.MapFrom(src =>
                    src.NameOfNextOfKin.ToUpper()))
                .ForMember(dest => dest.next_of_kin_relationship, opt => opt.MapFrom(src =>
                    src.NextOfKinRelationship.ToUpper()))
                .ForMember(dest => dest.next_of_kin_phone_number, opt => opt.MapFrom(src =>
                    src.NextOfKinTelNo))
                .ForMember(dest => dest.county, opt => opt.MapFrom(src =>
                    src.County))
                .ForMember(dest => dest.subcounty, opt => opt.MapFrom(src =>
                    src.SubCounty))
                .ForMember(dest => dest.ward, opt => opt.MapFrom(src =>
                    src.Ward))
                .ForMember(dest => dest.location, opt => opt.MapFrom(src =>
                    src.Location))
                .ForMember(dest => dest.village, opt => opt.MapFrom(src =>
                    src.Village))
                .ForMember(dest => dest.landmark, opt => opt.MapFrom(src =>
                    src.Landmark))
                .ForMember(dest => dest.facility_name, opt => opt.MapFrom(src =>
                    src.FacilityName))
                .ForMember(dest => dest.facility_mfl, opt => opt.MapFrom(src =>
                    src.MFLCode))
                .ForMember(dest => dest.date_of_initiation, opt => opt.MapFrom(src =>
                    src.DateOfInitiation.ToDateFormat()))
                .ForMember(dest => dest.treatment_outcome, opt => opt.MapFrom(src =>
                    src.TreatmentOutcome))
                .ForMember(dest => dest.date_of_last_encounter, opt => opt.MapFrom(src =>
                    src.DateOfLastEncounter.ToDateFormat()))
                .ForMember(dest => dest.date_of_last_viral_load, opt => opt.MapFrom(src =>
                    src.DateOfLastViralLoad.ToDateFormat()))
                .ForMember(dest => dest.date_of_next_appointment, opt => opt.MapFrom(src =>
                    src.NextAppointmentDate.ToDateFormat()))
                .ForMember(dest => dest.last_regimen, opt => opt.MapFrom(src =>
                    src.LastRegimen))
                .ForMember(dest => dest.last_regimen_line, opt => opt.MapFrom(src =>
                    src.LastRegimenLine));
            // .ForMember(dest => dest.ccurrent_on_art, opt => opt.MapFrom(src =>
            //     src.CurrentOnART.ToUpper()));

        }
    }
}
