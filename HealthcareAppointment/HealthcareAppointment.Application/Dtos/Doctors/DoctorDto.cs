namespace HealthcareAppointment.Application.Dtos.Doctors
{
    public record DoctorDto(Guid Id, string Name, string Specialization, decimal PricePerVisit);
}
