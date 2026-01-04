using AutoMapper;
using HealthcareAppointment.Application.Dtos.Appointments;
using HealthcareAppointment.Application.Interfaces;
using HealthcareAppointment.Application.Shared;
using HealthcareAppointment.Domain.Entities;
using MediatR;

namespace HealthcareAppointment.Application.Features.Appointments.GetAll
{
    public record GetMyAppointmentsQuery : IRequest<Result<IEnumerable<AppointmentDto>>>;

    public class GetMyAppointmentsHandler(IBaseRepository<Appointment> _repository, ICurrentUserService _currentUser, IMapper _mapper)
        : IRequestHandler<GetMyAppointmentsQuery, Result<IEnumerable<AppointmentDto>>>
    {
        public async Task<Result<IEnumerable<AppointmentDto>>> Handle(GetMyAppointmentsQuery request, CancellationToken cancellationToken)
        {
            if (_currentUser.UserId is null)
                return Result<IEnumerable<AppointmentDto>>.Failure("User not found");

            var appointments = await _repository.GetAllAsync(
               a => a.PatientId == _currentUser.UserId.Value,
               a => a.Doctor
            );

            var dtos = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);

            return Result<IEnumerable<AppointmentDto>>.Success(dtos);
        }
    }
}
