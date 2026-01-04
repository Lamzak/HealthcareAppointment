using FluentValidation;

namespace HealthcareAppointment.Application.Features.Authentication.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        }
    }
}
