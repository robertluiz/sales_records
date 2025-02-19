using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Seeds;

public static class ProductSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var seedDate = DateTime.SpecifyKind(new DateTime(2024, 1, 1), DateTimeKind.Utc);

        modelBuilder.Entity<Product>().HasData(
            new 
            {
                Id = 1,
                Code = "BEER-001",
                Title = "Brahma Duplo Malte",
                Name = "Brahma Duplo Malte 350ml",
                Description = "Cerveja Brahma Duplo Malte 350ml",
                Category = "Cervejas",
                Price = 4.99M,
                Image = "brahma-duplo-malte.jpg",
                IsActive = true,
                CreatedAt = seedDate,
                UpdatedAt = (DateTime?)null
            },
            new 
            {
                Id = 2,
                Code = "BEER-002",
                Title = "Skol Puro Malte",
                Name = "Skol Puro Malte 350ml",
                Description = "Cerveja Skol Puro Malte 350ml",
                Category = "Cervejas",
                Price = 4.49M,
                Image = "skol-puro-malte.jpg",
                IsActive = true,
                CreatedAt = seedDate,
                UpdatedAt = (DateTime?)null
            },
            new 
            {
                Id = 3,
                Code = "BEER-003",
                Title = "Original",
                Name = "Cerveja Original 600ml",
                Description = "Cerveja Original 600ml",
                Category = "Cervejas",
                Price = 8.99M,
                Image = "original.jpg",
                IsActive = true,
                CreatedAt = seedDate,
                UpdatedAt = (DateTime?)null
            },
            new 
            {
                Id = 4,
                Code = "BEER-004",
                Title = "Corona Extra",
                Name = "Cerveja Corona Extra 330ml",
                Description = "Cerveja Corona Extra 330ml",
                Category = "Cervejas Premium",
                Price = 7.99M,
                Image = "corona-extra.jpg",
                IsActive = true,
                CreatedAt = seedDate,
                UpdatedAt = (DateTime?)null
            }
        );

        modelBuilder.Entity<Product>()
            .OwnsOne(p => p.Rating)
            .HasData(
                new { ProductId = 1, Rate = 4.5M, Count = 150 },
                new { ProductId = 2, Rate = 4.2M, Count = 120 },
                new { ProductId = 3, Rate = 4.8M, Count = 200 },
                new { ProductId = 4, Rate = 4.7M, Count = 180 }
            );
    }
} 