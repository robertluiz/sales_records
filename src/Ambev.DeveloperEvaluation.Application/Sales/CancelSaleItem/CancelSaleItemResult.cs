using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

/// <summary>
/// Result of cancelling a sale item
/// </summary>
public class CancelSaleItemResult
{
    /// <summary>
    /// Gets or sets the item ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the product ID
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity sold
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the discount percentage (0-100)
    /// </summary>
    public decimal DiscountPercentage { get; set; }

    /// <summary>
    /// Gets or sets the discount amount
    /// </summary>
    public decimal DiscountAmount { get; set; }

    /// <summary>
    /// Gets or sets the subtotal before discount (quantity * unit price)
    /// </summary>
    public decimal Subtotal { get; set; }

    /// <summary>
    /// Gets or sets the total after discount
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Gets or sets whether the item is cancelled
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets when the item was cancelled
    /// </summary>
    public DateTime? CancelledAt { get; set; }
} 