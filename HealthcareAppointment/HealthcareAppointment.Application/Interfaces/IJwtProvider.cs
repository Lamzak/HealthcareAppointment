using HealthcareAppointment.Domain.Entities;

namespace HealthcareAppointment.Application.Interfaces
{
    public interface IJwtProvider
    {
        string Generate(Patient patient);
    }
}
