using HealthcareAppointment.Application.Interfaces;
using HealthcareAppointment.Infrastructure.Persistance;
using HealthcareAppointment.Infrastructure.Repositories;
using HealthcareAppointment.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthcareAppointment.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IAiDoctorSelector, GroqAiDoctorSelector>();
            services.AddScoped<IJwtProvider, JwtProvider>();

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
