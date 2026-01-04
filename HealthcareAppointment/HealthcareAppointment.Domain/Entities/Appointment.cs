using HealthcareAppointment.Domain.Entities.Base;

namespace HealthcareAppointment.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public string? Symptoms { get; set; }
        public bool IsCancelled { get; set; }
        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
    }
}
