using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

/// <summary>
/// Configuration for the Product entity mapping
/// </summary>
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    /// <summary>
    /// Configures the entity mapping
    /// </summary>
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnType("integer")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.Category)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Image)
            .HasMaxLength(500);

        builder.Property(x => x.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("timestamptz");

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("timestamptz");

        builder.OwnsOne(x => x.Rating, rating =>
        {
            rating.Property(r => r.Rate)
                .HasColumnName("Rating_Rate")
                .HasPrecision(3, 2);

            rating.Property(r => r.Count)
                .HasColumnName("Rating_Count");
        });

        builder.HasMany(x => x.SaleItems)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.HasIndex(x => x.Category);
    }
}