using AutoMapper;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Service.Application.Domain.Converters;
using Dwapi.Crs.Service.Application.Domain.Dtos;
using Dwapi.Crs.SharedKernel.Utils;

namespace Dwapi.Crs.Service.Application.Domain
{
    public class CrsProfile : Profile
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
                .ForMember(dest => dest.first_name, opt => opt.ConvertUsing(new NameConverter(), src =>
                    src.FirstName))
                .ForMember(dest => dest.middle_name, opt => opt.MapFrom(src =>
                    src.MiddleName.ToUpper()))
                .ForMember(dest => dest.last_name, opt => opt.ConvertUsing(new NameConverter(), src =>
                    src.LastName))
                .ForMember(dest => dest.date_of_birth, opt => opt.ConvertUsing(new DateConverter(), src =>
                    src.DateOfBirth))
                .ForMember(dest => dest.sex, opt => opt.ConvertUsing(new SexConverter(), src =>
                    src.Sex))
                .ForMember(dest => dest.marital_status, opt => opt.ConvertUsing(new MaritalConverter(), src =>
                    src.MaritalStatus))
                .ForMember(dest => dest.occupation, opt => opt.MapFrom(src =>
                    src.Occupation))
                .ForMember(dest => dest.education_level, opt => opt.MapFrom(src =>
                    src.HighestLevelOfEducation))
                .ForMember(dest => dest.phone_number, opt => opt.ConvertUsing(new PhoneConverter(), src =>
                    src.PhoneNumber))
                .ForMember(dest => dest.alt_phone_number, opt => opt.ConvertUsing(new PhoneConverter(), src =>
                    src.AlternativePhoneNumber))
                .ForMember(dest => dest.spouse_phone_number, opt => opt.ConvertUsing(new PhoneConverter(), src =>
                    src.SpousePhoneNumber))
                .ForMember(dest => dest.next_of_kin_name, opt => opt.MapFrom(src =>
                    src.NameOfNextOfKin.Truncate(59).ToUpper()))
                .ForMember(dest => dest.next_of_kin_relationship, opt => opt.MapFrom(src =>
                    src.NextOfKinRelationship.ToUpper()))
                .ForMember(dest => dest.next_of_kin_phone_number, opt => opt.ConvertUsing(new PhoneConverter(), src =>
                    src.NextOfKinTelNo))
                .ForMember(dest => dest.county, opt => opt.MapFrom(src =>
                    src.County.Truncate(59)))
                .ForMember(dest => dest.subcounty, opt => opt.MapFrom(src =>
                    src.SubCounty.Truncate(59)))
                .ForMember(dest => dest.ward, opt => opt.MapFrom(src =>
                    src.Ward.Truncate(59)))
                .ForMember(dest => dest.location, opt => opt.MapFrom(src =>
                    src.Location.Truncate(59)))
                .ForMember(dest => dest.village, opt => opt.MapFrom(src =>
                    src.Village.Truncate(59)))
                .ForMember(dest => dest.landmark, opt => opt.MapFrom(src =>
                    src.Landmark.Truncate(59)))
                .ForMember(dest => dest.facility_name, opt => opt.MapFrom(src =>
                    src.FacilityName.Truncate(59)))
                .ForMember(dest => dest.facility_mfl, opt => opt.MapFrom(src =>
                    src.MFLCode))
                .ForMember(dest => dest.date_of_initiation, opt => opt.ConvertUsing(new DateConverter(), src =>
                    src.DateOfInitiation))
                .ForMember(dest => dest.treatment_outcome, opt => opt.MapFrom(src =>
                    src.TreatmentOutcome))
                .ForMember(dest => dest.date_of_last_encounter, opt => opt.ConvertUsing(new DateConverter(), src =>
                    src.DateOfLastEncounter))
                .ForMember(dest => dest.date_of_last_viral_load, opt => opt.ConvertUsing(new DateConverter(), src =>
                    src.DateOfLastViralLoad))
                .ForMember(dest => dest.date_of_next_appointment, opt => opt.ConvertUsing(new DateConverter(), src =>
                    src.NextAppointmentDate))
                .ForMember(dest => dest.last_regimen, opt => opt.MapFrom(src =>
                    src.LastRegimen))
                .ForMember(dest => dest.last_regimen_line, opt => opt.MapFrom(src =>
                    src.LastRegimenLine))
                .ForMember(dest => dest.current_on_art, opt => opt.MapFrom(src =>
                    src.CurrentOnART.ToUpper()))
                .ForMember(dest => dest.date_of_hiv_diagnosis, opt => opt.ConvertUsing(new DateConverter(), src =>
                    src.DateOfHIVDiagnosis))
                .ForMember(dest => dest.last_viral_load_result, opt => opt.MapFrom(src =>
                    src.LastViralLoadResult));

            CreateMap<RegistryManifest, ErrorReportDto>();
            CreateMap<TransmissionLog, TransmissionLogDto>();
        }
    }
}