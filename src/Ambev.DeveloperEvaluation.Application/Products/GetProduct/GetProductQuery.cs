using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Query to get a product by ID
/// </summary>
public class GetProductQuery : IRequest<GetProductResult>
{
    /// <summary>
    /// Gets or sets the product ID
    /// </summary>
    public int Id { get; set; }
} 