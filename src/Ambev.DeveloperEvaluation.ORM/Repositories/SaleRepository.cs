using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of the sale repository using Entity Framework Core
/// </summary>
public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of the SaleRepository class
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Sale?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(s => s.Branch)
            .Include(s => s.Items)
                .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<(List<Sale> Sales, int TotalRecords)> ListAsync(
        Guid? branchId = null,
        Guid? customerId = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        bool? isCancelled = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Sales
            .Include(s => s.Branch)
            .Include(s => s.Items)
                .ThenInclude(i => i.Product)
            .AsQueryable();

        if (branchId.HasValue)
            query = query.Where(s => s.BranchId == branchId.Value);

        if (customerId.HasValue)
            query = query.Where(s => s.CustomerId == customerId.Value);

        if (startDate.HasValue)
            query = query.Where(s => s.SaleDate >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(s => s.SaleDate <= endDate.Value);

        if (isCancelled.HasValue)
            query = query.Where(s => s.IsCancelled == isCancelled.Value);

        var totalRecords = await query.CountAsync(cancellationToken);

        var sales = await query
            .OrderByDescending(s => s.SaleDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (sales, totalRecords);
    }

    /// <inheritdoc/>
    public async Task AddAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
    }

    /// <inheritdoc/>
    public Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        _context.Entry(sale).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task DeleteAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        _context.Sales.Remove(sale);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
} 