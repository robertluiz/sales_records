using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSalesFeature;

/// <summary>
/// Request model for listing sales with filters
/// </summary>
public class ListSalesRequest
{
    /// <summary>
    /// Gets or sets the branch ID filter
    /// </summary>
    public Guid? BranchId { get; set; }

    /// <summary>
    /// Gets or sets the customer ID filter
    /// </summary>
    public Guid? CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the start date filter
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date filter
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets whether to include only cancelled sales
    /// </summary>
    public bool? IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the page number (1-based)
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the page size
    /// </summary>
    public int PageSize { get; set; } = 10;
} 