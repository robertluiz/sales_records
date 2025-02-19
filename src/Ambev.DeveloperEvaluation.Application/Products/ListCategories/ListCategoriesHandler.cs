using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Products.ListCategories;

/// <summary>
/// Handler for listing product categories
/// </summary>
public class ListCategoriesHandler : IRequestHandler<ListCategoriesQuery, List<string>>
{
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListCategoriesHandler"/> class.
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    public ListCategoriesHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    /// <summary>
    /// Handles the request to list product categories
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The list of unique product categories</returns>
    public async Task<List<string>> Handle(ListCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.ListCategoriesAsync(cancellationToken);
    }
} 