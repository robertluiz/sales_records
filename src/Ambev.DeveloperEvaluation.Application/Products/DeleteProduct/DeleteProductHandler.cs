using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Handler to process the delete product command
/// </summary>
public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Initializes a new instance of the handler with required dependencies
    /// </summary>
    public DeleteProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    /// <summary>
    /// Processes the delete product command
    /// </summary>
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new InvalidOperationException("Product not found");

        await _productRepository.DeleteAsync(product, cancellationToken);
        await _productRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
} 