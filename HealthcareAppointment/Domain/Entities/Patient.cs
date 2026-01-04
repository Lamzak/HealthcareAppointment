using HealthcareAppointment.Domain.Entities.Base;

namespace HealthcareAppointment.Domain.Entities
{
    public class Patient : BaseEntity
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
    }
}
