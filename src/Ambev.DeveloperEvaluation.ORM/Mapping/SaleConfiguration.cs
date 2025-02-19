using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(x => x.Number)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.SaleDate)
            .IsRequired()
            .HasColumnType("timestamptz");

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("timestamptz");

        builder.Property(x => x.Subtotal)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.DiscountAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.TotalAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.IsCancelled)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CancelledAt)
            .HasColumnType("timestamptz");

        builder.HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Branch)
            .WithMany(x => x.Sales)
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Items)
            .WithOne(x => x.Sale)
            .HasForeignKey(x => x.SaleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
} 