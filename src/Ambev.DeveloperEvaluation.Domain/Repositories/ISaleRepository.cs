using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for managing sales
/// </summary>
public interface ISaleRepository
{
    /// <summary>
    /// Gets a sale by its ID
    /// </summary>
    /// <param name="id">The sale ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a sale by its ID including all related details (Branch, Items, Products)
    /// </summary>
    /// <param name="id">The sale ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale with details if found, null otherwise</returns>
    Task<Sale?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists sales with pagination and optional filters
    /// </summary>
    /// <param name="branchId">Optional branch ID filter</param>
    /// <param name="customerId">Optional customer ID filter</param>
    /// <param name="startDate">Optional start date filter</param>
    /// <param name="endDate">Optional end date filter</param>
    /// <param name="isCancelled">Optional cancelled status filter</param>
    /// <param name="page">Page number (1-based)</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A tuple containing the list of sales and total count</returns>
    Task<(List<Sale> Sales, int TotalRecords)> ListAsync(
        Guid? branchId = null,
        Guid? customerId = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        bool? isCancelled = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new sale
    /// </summary>
    /// <param name="sale">The sale to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task AddAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing sale
    /// </summary>
    /// <param name="sale">The sale to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a sale
    /// </summary>
    /// <param name="sale">The sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DeleteAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves all pending changes
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
} 