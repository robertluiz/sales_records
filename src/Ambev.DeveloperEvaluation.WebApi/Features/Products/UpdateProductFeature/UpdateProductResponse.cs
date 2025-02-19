using Ambev.DeveloperEvaluation.Domain.Models;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProductFeature;

/// <summary>
/// Response model for an updated product
/// </summary>
public class UpdateProductResponse
{
    /// <summary>
    /// Gets or sets the product ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the product code
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the product category
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product image URL
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// Gets or sets the product price
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the product rating information
    /// </summary>
    public ProductRating Rating { get; set; } = new();

    /// <summary>
    /// Gets or sets whether the product is active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the date when the product was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date when the product was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Product rating information
/// </summary>
public class ProductRating
{
    /// <summary>
    /// Gets or sets the rating value (0-5)
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// Gets or sets the number of ratings
    /// </summary>
    public int Count { get; set; }
} 