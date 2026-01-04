using HealthcareAppointment.Application.Interfaces;
using HealthcareAppointment.Application.Shared;
using HealthcareAppointment.Domain.Entities;
using MediatR;

namespace HealthcareAppointment.Application.Features.Authentication.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<Result<string>>;

    public class LoginHandler(IBaseRepository<Patient> _repository, IJwtProvider _jwtProvider) : IRequestHandler<LoginCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var patients = await _repository.GetAllAsync(p => p.Email == request.Email);
            var patient = patients.FirstOrDefault();

            if (patient is null || !BCrypt.Net.BCrypt.Verify(request.Password, patient.PasswordHash))
            {
                return Result<string>.Failure("Invalid credentials");
            }

            var token = _jwtProvider.Generate(patient);
            return Result<string>.Success(token);
        }
    }
}
