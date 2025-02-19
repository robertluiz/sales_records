using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

/// <summary>
/// Handler for listing products with pagination and filters
/// </summary>
public class ListProductsHandler : IRequestHandler<ListProductsQuery, ListProductsResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListProductsHandler"/> class.
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="mapper">The mapper</param>
    public ListProductsHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the request to list products
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The list of products with pagination info</returns>
    public async Task<ListProductsResult> Handle(ListProductsQuery request, CancellationToken cancellationToken)
    {
        var (products, totalItems) = await _productRepository.ListAsync(
            page: request.Page,
            pageSize: request.Size,
            category: request.Category,
            minPrice: request.MinPrice,
            maxPrice: request.MaxPrice,
            cancellationToken: cancellationToken);

        var totalPages = (int)Math.Ceiling(totalItems / (double)request.Size);

        return new ListProductsResult
        {
            Data = _mapper.Map<List<ProductResult>>(products),
            TotalItems = totalItems,
            CurrentPage = request.Page,
            TotalPages = totalPages
        };
    }
} 