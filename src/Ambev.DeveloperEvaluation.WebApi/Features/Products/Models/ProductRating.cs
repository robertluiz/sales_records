namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Models;

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