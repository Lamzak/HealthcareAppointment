namespace HealthcareAppointment.Application.Dtos.Appointments
{
    public record AppointmentDto(Guid Id, Guid DoctorId, string DoctorName, DateTime AppointmentTime, string? Symptoms, bool IsCancelled);
}
