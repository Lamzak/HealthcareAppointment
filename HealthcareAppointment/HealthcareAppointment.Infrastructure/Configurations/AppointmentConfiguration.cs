using HealthcareAppointment.Domain.Entities;
using HealthcareAppointment.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareAppointment.Infrastructure.Configurations
{
    public class AppointmentConfiguration : BaseEntityConfiguration<Appointment>
    {
        public override void Configure(EntityTypeBuilder<Appointment> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.AppointmentTime)
                .IsRequired();

            builder.Property(a => a.Symptoms)
                .HasMaxLength(500)
                .IsRequired(false);

            Relationships(builder);
        }

        private void Relationships(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
