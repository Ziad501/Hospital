using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Persistence.Config;

public class ExaminationServiceConfiguration : IEntityTypeConfiguration<ExaminationService>
{
    public void Configure(EntityTypeBuilder<ExaminationService> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.ServiceCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.ServiceName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Price)
            .HasPrecision(18, 2);

        builder.Property(s => s.Total)
            .HasPrecision(18, 2);
    }
}