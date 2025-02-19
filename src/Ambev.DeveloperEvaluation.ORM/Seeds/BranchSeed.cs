using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Seeds;

/// <summary>
/// Seed data for Branch entity
/// </summary>
public static class BranchSeed
{
    /// <summary>
    /// Configures the initial data for branches
    /// </summary>
    /// <param name="modelBuilder">The Entity Framework model builder</param>
    public static void Seed(ModelBuilder modelBuilder)
    {
        var seedDate = DateTime.SpecifyKind(new DateTime(2024, 1, 1), DateTimeKind.Utc);

        modelBuilder.Entity<Branch>().HasData(
            new Branch
            {
                Id = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                Code = "MATRIX-001",
                Name = "São Paulo Headquarters",
                Address = "Av. Paulista, 1000 - Bela Vista, São Paulo - SP",
                IsActive = true,
                CreatedAt = seedDate,
                UpdatedAt = null
            },
            new Branch
            {
                Id = Guid.Parse("9c9e6679-7425-40de-944b-e07fc1f90ae8"),
                Code = "BRANCH-RJ-001",
                Name = "Rio de Janeiro Branch",
                Address = "Av. Rio Branco, 500 - Centro, Rio de Janeiro - RJ",
                IsActive = true,
                CreatedAt = seedDate,
                UpdatedAt = null
            },
            new Branch
            {
                Id = Guid.Parse("5c9e6679-7425-40de-944b-e07fc1f90ae9"),
                Code = "BRANCH-BH-001",
                Name = "Belo Horizonte Branch",
                Address = "Av. Afonso Pena, 2000 - Centro, Belo Horizonte - MG",
                IsActive = true,
                CreatedAt = seedDate,
                UpdatedAt = null
            },
            new Branch
            {
                Id = Guid.Parse("3c9e6679-7425-40de-944b-e07fc1f90ae0"),
                Code = "BRANCH-CWB-001",
                Name = "Curitiba Branch",
                Address = "Rua XV de Novembro, 1500 - Centro, Curitiba - PR",
                IsActive = true,
                CreatedAt = seedDate,
                UpdatedAt = null
            }
        );
    }
} 