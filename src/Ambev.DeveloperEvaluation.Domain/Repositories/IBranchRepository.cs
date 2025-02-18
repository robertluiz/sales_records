using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for managing branches
/// </summary>
public interface IBranchRepository
{
    /// <summary>
    /// Gets a branch by its ID
    /// </summary>
    /// <param name="id">The branch ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The branch if found, null otherwise</returns>
    Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a branch by its ID including all related details
    /// </summary>
    /// <param name="id">The branch ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The branch with details if found, null otherwise</returns>
    Task<Branch?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new branch
    /// </summary>
    /// <param name="branch">The branch to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task AddAsync(Branch branch, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing branch
    /// </summary>
    /// <param name="branch">The branch to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task UpdateAsync(Branch branch, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a branch
    /// </summary>
    /// <param name="branch">The branch to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DeleteAsync(Branch branch, CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves all pending changes
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
} 