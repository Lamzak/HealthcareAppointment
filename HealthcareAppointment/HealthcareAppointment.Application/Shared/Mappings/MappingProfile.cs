using AutoMapper;
using HealthcareAppointment.Application.Dtos.Appointments;
using HealthcareAppointment.Application.Dtos.Doctors;
using HealthcareAppointment.Domain.Entities;

namespace HealthcareAppointment.Application.Shared.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Doctor
            CreateMap<Doctor, DoctorDto>();
            #endregion

            #region Appointment
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor != null ? src.Doctor.Name : "Unknown"));
            #endregion
        }
    }
}
