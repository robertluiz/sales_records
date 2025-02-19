using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for managing products.
/// Defines CRUD operations and specialized queries for the Product entity.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Gets a product by its ID
    /// </summary>
    /// <param name="id">The product ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a product by its ID including all related details (sale items)
    /// </summary>
    /// <param name="id">The product ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product with details if found, null otherwise</returns>
    Task<Product?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new product
    /// </summary>
    /// <param name="product">The product to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task AddAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing product
    /// </summary>
    /// <param name="product">The product to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task UpdateAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a product
    /// </summary>
    /// <param name="product">The product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DeleteAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves all pending changes in the context
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists products with pagination and optional filters
    /// </summary>
    /// <param name="page">Page number (starts at 1)</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="category">Optional category filter</param>
    /// <param name="minPrice">Optional minimum price filter</param>
    /// <param name="maxPrice">Optional maximum price filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A tuple containing the list of products and total count</returns>
    Task<(List<Product> Products, int TotalCount)> ListAsync(
        int page,
        int pageSize,
        string? category = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all unique product categories
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of unique categories sorted alphabetically</returns>
    Task<List<string>> ListCategoriesAsync(CancellationToken cancellationToken = default);
} 