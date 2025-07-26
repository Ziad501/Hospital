using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Persistence.Config;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.MiddleName)
            .HasMaxLength(50);

        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(p => p.Code)
            .IsUnique();

        builder.Property(p => p.Mobile)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(p => p.NationalId)
            .IsRequired()
            .HasMaxLength(14);

        builder.HasIndex(p => p.NationalId)
            .IsUnique();

        builder.Property(p => p.Religion)
            .HasMaxLength(50);

        builder.Property(p => p.Nationality)
            .HasMaxLength(50);

        builder.Property(p => p.PassportAddress)
            .HasMaxLength(200);

        builder.Property(p => p.ReferredFrom)
            .HasMaxLength(100);

        builder.Property(p => p.RefDoctorId)
            .HasMaxLength(20);

        builder.Property(p => p.Gender)
            .HasMaxLength(10);

        builder.HasMany(p => p.Examinations)
            .WithOne(e => e.Patient)
            .HasForeignKey(e => e.PatientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}