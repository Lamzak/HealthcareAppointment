namespace HealthcareAppointment.Domain.Entities.Base
{
    public abstract class BaseEntity : Trackable
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
