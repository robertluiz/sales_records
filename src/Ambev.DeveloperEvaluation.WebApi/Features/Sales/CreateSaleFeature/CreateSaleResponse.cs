namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSaleFeature;

/// <summary>
/// Response model for a created sale
/// </summary>
public class CreateSaleResponse
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
    /// Gets or sets the sale items
    /// </summary>
    public List<SaleItemResponse> Items { get; set; } = new();
}

/// <summary>
/// Response model for a sale item
/// </summary>
public class SaleItemResponse
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
} 