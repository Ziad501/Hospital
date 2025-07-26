using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Persistence.Config;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(d => d.Code);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.EadaAccCode)
            .HasMaxLength(50);

        builder.Property(d => d.MName)
            .HasMaxLength(100);

        builder.Property(d => d.DocAccCode)
            .HasMaxLength(50);

        builder.HasMany(d => d.Examinations)
            .WithOne(e => e.Doctor)
            .HasForeignKey(e => e.DoctorCode)
            .OnDelete(DeleteBehavior.Restrict);
    }
}