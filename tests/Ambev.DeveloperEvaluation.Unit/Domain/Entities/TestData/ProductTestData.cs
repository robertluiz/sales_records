using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Models;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class ProductTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Product entities.
    /// The generated products will have:
    /// - Code (unique alphanumeric)
    /// - Title (product name)
    /// - Name (product name)
    /// - Description (optional)
    /// - Category
    /// - Image (optional URL)
    /// - Price (positive value)
    /// - Rating (0-5)
    /// - Rating count (>=0)
    /// </summary>
    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
        .RuleFor(p => p.Code, f => f.Commerce.Ean13())
        .RuleFor(p => p.Title, f => f.Commerce.ProductName())
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0])
        .RuleFor(p => p.Image, f => f.Image.PicsumUrl())
        .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
        .RuleFor(p => p.Rating, f => new ProductRating 
        { 
            Rate = f.Random.Decimal(0, 5), 
            Count = f.Random.Number(0, 1000) 
        })
        .RuleFor(p => p.IsActive, f => true)
        .RuleFor(p => p.CreatedAt, f => DateTime.UtcNow);

    /// <summary>
    /// Generates a valid Product entity with random data.
    /// The generated product will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static Product GenerateValidProduct()
    {
        return ProductFaker.Generate();
    }

    /// <summary>
    /// Generates a product code that exceeds the maximum length limit.
    /// The generated code will:
    /// - Be longer than 50 characters
    /// - Contain random alphanumeric characters
    /// This is useful for testing code length validation error cases.
    /// </summary>
    /// <returns>A product code that exceeds the maximum length limit.</returns>
    public static string GenerateLongCode()
    {
        return new Faker().Random.String2(51);
    }

    /// <summary>
    /// Generates a product title that exceeds the maximum length limit.
    /// The generated title will:
    /// - Be longer than 100 characters
    /// - Contain random text
    /// This is useful for testing title length validation error cases.
    /// </summary>
    /// <returns>A product title that exceeds the maximum length limit.</returns>
    public static string GenerateLongTitle()
    {
        return new Faker().Random.String2(101);
    }

    /// <summary>
    /// Generates a product description that exceeds the maximum length limit.
    /// The generated description will:
    /// - Be longer than 500 characters
    /// - Contain random text
    /// This is useful for testing description length validation error cases.
    /// </summary>
    /// <returns>A product description that exceeds the maximum length limit.</returns>
    public static string GenerateLongDescription()
    {
        return new Faker().Random.String2(501);
    }

    /// <summary>
    /// Generates an image URL that exceeds the maximum length limit.
    /// The generated URL will:
    /// - Be longer than 500 characters
    /// - Contain random alphanumeric characters
    /// This is useful for testing image URL length validation error cases.
    /// </summary>
    /// <returns>An image URL that exceeds the maximum length limit.</returns>
    public static string GenerateLongImageUrl()
    {
        return new Faker().Random.String2(501);
    }
} 