using HealthcareAppointment.Application.Interfaces;
using HealthcareAppointment.Application.Shared;
using HealthcareAppointment.Domain.Entities;
using MediatR;

namespace HealthcareAppointment.Application.Features.Authentication.Register
{
    public record RegisterCommand(string FullName, string Email, string Password) : IRequest<Result<Guid>>;

    public class RegisterHandler(IBaseRepository<Patient> _repository) : IRequestHandler<RegisterCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetAllAsync(p => p.Email == request.Email);
            if (existing.Any()) return Result<Guid>.Failure("Email already exists");

            var patient = new Patient
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            await _repository.AddAsync(patient);
            return Result<Guid>.Success(patient.Id);
        }
    }
}
