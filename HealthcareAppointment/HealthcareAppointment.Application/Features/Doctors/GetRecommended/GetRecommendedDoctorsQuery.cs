using AutoMapper;
using HealthcareAppointment.Application.Dtos.Doctors;
using HealthcareAppointment.Application.Interfaces;
using HealthcareAppointment.Application.Shared;
using HealthcareAppointment.Domain.Entities;
using MediatR;

namespace HealthcareAppointment.Application.Features.Doctors.GetRecommended
{
    public record GetRecommendedDoctorsQuery(string Symptoms) : IRequest<Result<IEnumerable<DoctorDto>>>;

    public class GetRecommendedDoctorsHandler(
      IBaseRepository<Doctor> _repository,
      IAiDoctorSelector _aiSelector,
      IMapper _mapper) : IRequestHandler<GetRecommendedDoctorsQuery, Result<IEnumerable<DoctorDto>>>
    {
        public async Task<Result<IEnumerable<DoctorDto>>> Handle(GetRecommendedDoctorsQuery request, CancellationToken cancellationToken)
        {
            var specialization = await _aiSelector.GetSpecializationFromSymptomsAsync(request.Symptoms);

            if (string.IsNullOrEmpty(specialization))
                return Result<IEnumerable<DoctorDto>>.Failure("Could not identify a specialization based on symptoms.");

            var doctors = await _repository.GetAllAsync(d => d.Specialization == specialization);

            var dtos = _mapper.Map<IEnumerable<DoctorDto>>(doctors);

            return Result<IEnumerable<DoctorDto>>.Success(dtos);
        }
    }
}
