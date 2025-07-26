using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Persistence.Config;

public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
{
    public void Configure(EntityTypeBuilder<Clinic> builder)
    {
        builder.HasKey(c => c.Code);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.AccCode)
            .HasMaxLength(50);

        builder.HasMany(c => c.Examinations)
            .WithOne(e => e.Clinic)
            .HasForeignKey(e => e.ClinicCode)
            .OnDelete(DeleteBehavior.Restrict);
    }
}