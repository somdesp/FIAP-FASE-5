using AutoMapper;
using FIAP.TECH.CORE.APPLICATION.DTO;
using FIAP.TECH.CORE.DOMAIN.Entities;

namespace FIAP.TECH.CORE.APPLICATION.Settings.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DoctorDto, Doctor>().ReverseMap();
        CreateMap<Doctor, PatientDetailsDto>();
        CreateMap<Doctor, DoctorUpdateDto>()
            .ReverseMap()
            .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       if (prop == null) return false;
                       return true;
                   }));

        CreateMap<PatientDto, Patient>().ReverseMap();
        CreateMap<Patient, PatientDetailsDto>();
        CreateMap<Patient, PatientUpdateDto>()
            .ReverseMap()
            .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       if (prop == null) return false;
                       return true;
                   }));

    }
}
