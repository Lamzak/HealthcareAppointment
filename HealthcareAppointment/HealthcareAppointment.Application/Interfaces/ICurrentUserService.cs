namespace HealthcareAppointment.Application.Interfaces
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
    }
}
