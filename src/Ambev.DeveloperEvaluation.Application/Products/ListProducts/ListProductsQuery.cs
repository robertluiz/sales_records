using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

/// <summary>
/// Query to list products with pagination and filters
/// </summary>
public class ListProductsQuery : IRequest<ListProductsResult>
{
    /// <summary>
    /// Gets or sets the page number
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the page size
    /// </summary>
    public int Size { get; set; } = 10;

    /// <summary>
    /// Gets or sets the ordering expression
    /// </summary>
    public string? Order { get; set; }

    /// <summary>
    /// Gets or sets the category filter
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the minimum price filter
    /// </summary>
    public decimal? MinPrice { get; set; }

    /// <summary>
    /// Gets or sets the maximum price filter
    /// </summary>
    public decimal? MaxPrice { get; set; }
} 