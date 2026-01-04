using HealthcareAppointment.Application.Interfaces;
using HealthcareAppointment.Application.Shared;
using HealthcareAppointment.Domain.Entities;
using MediatR;

namespace HealthcareAppointment.Application.Features.Appointments.Cancel
{
    public record CancelAppointmentCommand(Guid AppointmentId) : IRequest<Result<bool>>;

    public class CancelAppointmentHandler(IBaseRepository<Appointment> repository, ICurrentUserService currentUser) : IRequestHandler<CancelAppointmentCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await repository.GetByIdAsync(request.AppointmentId);

            if (appointment is null)
                return Result<bool>.Failure("Appointment not found.");

            if (appointment.PatientId != currentUser.UserId)
                return Result<bool>.Failure("You are not authorized to cancel this appointment.");

            appointment.IsCancelled = true;

            await repository.UpdateAsync(appointment);

            return Result<bool>.Success(true);
        }
    }
}
