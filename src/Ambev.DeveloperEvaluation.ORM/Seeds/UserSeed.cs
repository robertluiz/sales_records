using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Seeds;

public static class UserSeed
{
    private const int WorkFactor = 11;

    public static void Seed(ModelBuilder modelBuilder)
    {
        var seedDate = DateTime.SpecifyKind(new DateTime(2024, 1, 1), DateTimeKind.Utc);

        modelBuilder.Entity<User>().HasData(
            new
            {
                Id = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae1"),
                Username = "admin",
                Email = "admin@ambev.com.br",
                Password = "$2a$11$H0s1782gZrwdI8HwPqMw9u3lmrVuCMwpLYzjJL6diAP3b3Z3wxxze",
                Phone = "(11) 99999-9999",
                Role = UserRole.Admin,
                Status = UserStatus.Active,
                CreatedAt = seedDate,
                UpdatedAt = (DateTime?)null
            },
            new
            {
                Id = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae2"),
                Username = "customer",
                Email = "customer@email.com",
                Password = "$2a$11$H0s1782gZrwdI8HwPqMw9u3lmrVuCMwpLYzjJL6diAP3b3Z3wxxze",
                Phone = "(11) 88888-8888",
                Role = UserRole.Customer,
                Status = UserStatus.Active,
                CreatedAt = seedDate,
                UpdatedAt = (DateTime?)null
            }
        );
    }
} 