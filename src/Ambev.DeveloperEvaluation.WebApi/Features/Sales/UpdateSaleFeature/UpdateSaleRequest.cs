namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSaleFeature;

/// <summary>
/// Request model for updating a sale
/// </summary>
public class UpdateSaleRequest
{
    /// <summary>
    /// Gets or sets the branch ID where the sale was made
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets the sale date
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets or sets the sale items
    /// </summary>
    public List<UpdateSaleItemRequest> Items { get; set; } = new();
}

/// <summary>
/// Request model for updating a sale item
/// </summary>
public class UpdateSaleItemRequest
{
    /// <summary>
    /// Gets or sets the product ID
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity sold
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price
    /// </summary>
    public decimal UnitPrice { get; set; }
} 