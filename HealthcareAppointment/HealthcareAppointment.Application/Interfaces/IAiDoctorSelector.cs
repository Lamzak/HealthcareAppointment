namespace HealthcareAppointment.Application.Interfaces
{
    public interface IAiDoctorSelector
    {
        Task<string> GetSpecializationFromSymptomsAsync(string symptoms);
    }
}
