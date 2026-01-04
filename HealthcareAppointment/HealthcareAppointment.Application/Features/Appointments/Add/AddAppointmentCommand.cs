using HealthcareAppointment.Application.Interfaces;
using HealthcareAppointment.Application.Shared;
using HealthcareAppointment.Domain.Entities;
using MediatR;

namespace HealthcareAppointment.Application.Features.Appointments.Add
{
    public record AddAppointmentCommand(Guid DoctorId, DateTime Time, string? Symptoms) : IRequest<Result<Guid>>;

    public class AddAppointmentCommandHandler(IBaseRepository<Appointment> _repository, ICurrentUserService _currentUser)
        : IRequestHandler<AddAppointmentCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(AddAppointmentCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser.UserId is null) return Result<Guid>.Failure("User is not authenticated");

            var appointment = new Appointment
            {
                PatientId = _currentUser.UserId.Value,
                DoctorId = request.DoctorId,
                AppointmentTime = request.Time,
                Symptoms = request.Symptoms,
                IsCancelled = false
            };

            await _repository.AddAsync(appointment);
            return Result<Guid>.Success(appointment.Id);
        }
    }
}
