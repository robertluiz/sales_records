using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSalesFeature;

/// <summary>
/// Response model for listing sales
/// </summary>
public class ListSalesResponse
{
    /// <summary>
    /// Gets or sets the total number of records
    /// </summary>
    public int TotalRecords { get; set; }

    /// <summary>
    /// Gets or sets the current page number
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Gets or sets the page size
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the total number of pages
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Gets or sets the list of sales
    /// </summary>
    public List<SaleListItem> Items { get; set; } = new();
}

/// <summary>
/// Represents a sale item in the list
/// </summary>
public class SaleListItem
{
    /// <summary>
    /// Gets or sets the sale ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the branch ID where the sale was made
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets the customer ID who made the purchase
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the sale date
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets or sets the subtotal amount before discounts
    /// </summary>
    public decimal Subtotal { get; set; }

    /// <summary>
    /// Gets or sets the total discount amount
    /// </summary>
    public decimal DiscountAmount { get; set; }

    /// <summary>
    /// Gets or sets the total amount after discounts
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets whether the sale is cancelled
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets when the sale was cancelled
    /// </summary>
    public DateTime? CancelledAt { get; set; }

    /// <summary>
    /// Gets or sets when the sale was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
} 