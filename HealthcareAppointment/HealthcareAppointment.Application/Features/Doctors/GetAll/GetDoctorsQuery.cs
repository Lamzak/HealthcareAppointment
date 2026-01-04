using AutoMapper;
using HealthcareAppointment.Application.Dtos.Doctors;
using HealthcareAppointment.Application.Interfaces;
using HealthcareAppointment.Application.Shared;
using HealthcareAppointment.Domain.Entities;
using MediatR;

namespace HealthcareAppointment.Application.Features.Doctors.GetAll
{
    public record GetDoctorsQuery : IRequest<Result<IEnumerable<DoctorDto>>>;

    public class GetDoctorsHandler(IBaseRepository<Doctor> _repository, IMapper _mapper)
        : IRequestHandler<GetDoctorsQuery, Result<IEnumerable<DoctorDto>>>
    {
        public async Task<Result<IEnumerable<DoctorDto>>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {
            var doctors = await _repository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<DoctorDto>>(doctors);

            return Result<IEnumerable<DoctorDto>>.Success(dtos);
        }
    }
}
