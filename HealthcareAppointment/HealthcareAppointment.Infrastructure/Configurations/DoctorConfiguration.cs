using HealthcareAppointment.Domain.Constants;
using HealthcareAppointment.Domain.Entities;
using HealthcareAppointment.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareAppointment.Infrastructure.Configurations
{
    public class DoctorConfiguration : BaseEntityConfiguration<Doctor>
    {
        public override void Configure(EntityTypeBuilder<Doctor> builder)
        {
            base.Configure(builder);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Specialization)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(d => d.PricePerVisit)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasData(
                new Doctor
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Dr. Gregory House",
                    Specialization = DoctorSpecializations.Diagnostician,
                    PricePerVisit = 500,
                    CreatedBy = SystemUsers.Id,
                    CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc)
                },
                new Doctor
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Dr. Strange",
                    Specialization = DoctorSpecializations.Surgeon,
                    PricePerVisit = 1000,
                    CreatedBy = SystemUsers.Id,
                    CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc)
                },
                new Doctor
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Dr. Shaun Murphy",
                    Specialization = DoctorSpecializations.Surgeon,
                    PricePerVisit = 200,
                    CreatedBy = SystemUsers.Id,
                    CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc)
                },
                new Doctor
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Name = "Dr. Derek Shepherd",
                    Specialization = DoctorSpecializations.Neurologist,
                    PricePerVisit = 400,
                    CreatedBy = SystemUsers.Id,
                    CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc)
                },
                new Doctor
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Name = "Dr. Meredith Grey",
                    Specialization = DoctorSpecializations.General,
                    PricePerVisit = 150,
                    CreatedBy = SystemUsers.Id,
                    CreatedAt = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
