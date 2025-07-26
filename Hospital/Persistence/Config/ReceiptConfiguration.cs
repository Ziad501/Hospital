using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Persistence.Config;

public class ReceiptConfiguration : IEntityTypeConfiguration<Receipt>
{
    public void Configure(EntityTypeBuilder<Receipt> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.ReceiptNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(r => r.ReceiptNumber)
            .IsUnique();

        builder.Property(r => r.TotalAmount)
            .HasPrecision(18, 2);

        builder.Property(r => r.PaidAmount)
            .HasPrecision(18, 2);

        builder.HasOne(r => r.Examination)
            .WithMany()
            .HasForeignKey(r => r.ExaminationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}