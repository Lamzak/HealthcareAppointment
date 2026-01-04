using HealthcareAppointment.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HealthcareAppointment.Infrastructure.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        public Guid? UserId
        {
            get
            {
                var idClaim = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(idClaim)) return null;
                return Guid.TryParse(idClaim, out var userId) ? userId : null;
            }
        }
    }
}
