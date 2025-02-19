using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Command to delete an existing product
/// </summary>
public class DeleteProductCommand : IRequest<Unit>
{
    /// <summary>
    /// Gets or sets the ID of the product to delete
    /// </summary>
    public int Id { get; set; }
} 