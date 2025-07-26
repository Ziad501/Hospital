using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Persistence.Config;

public class ExaminationConfiguration : IEntityTypeConfiguration<Examination>
{
    public void Configure(EntityTypeBuilder<Examination> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.HospitalName)
            .HasMaxLength(100);

        builder.Property(e => e.Statement)
            .HasMaxLength(500);

        builder.Property(e => e.Amount)
            .HasPrecision(18, 2);

        builder.Property(e => e.Discount)
            .HasPrecision(18, 2);

        builder.Property(e => e.NetAmount)
            .HasPrecision(18, 2);

        builder.HasMany(e => e.Services)
            .WithOne(s => s.Examination)
            .HasForeignKey(s => s.ExaminationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}