using FluentValidation;

namespace HealthcareAppointment.Application.Features.Appointments.Add
{
    public class AddAppointmentCommandValidator : AbstractValidator<AddAppointmentCommand>
    {
        public AddAppointmentCommandValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("Doctor ID is required.");

            RuleFor(x => x.Time)
                .NotEmpty().WithMessage("Appointment time is required.")
                .GreaterThan(DateTime.UtcNow).WithMessage("You cannot book an appointment in the past.");
        }
    }
}
