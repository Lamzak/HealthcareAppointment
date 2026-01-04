using HealthcareAppointment.Domain.Entities;
using HealthcareAppointment.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareAppointment.Infrastructure.Configurations
{
    public class PatientConfiguration : BaseEntityConfiguration<Patient>
    {
        public override void Configure(EntityTypeBuilder<Patient> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.PasswordHash)
                .IsRequired();

            builder.HasIndex(p => p.Email)
                .IsUnique();
        }
    }
}
