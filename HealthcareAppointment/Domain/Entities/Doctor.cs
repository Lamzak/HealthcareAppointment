using HealthcareAppointment.Domain.Entities.Base;

namespace HealthcareAppointment.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public required string Name { get; set; }
        public required string Specialization { get; set; }
        public decimal PricePerVisit { get; set; }
    }
}
