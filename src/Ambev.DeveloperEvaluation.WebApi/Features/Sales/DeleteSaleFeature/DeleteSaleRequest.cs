namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSaleFeature;

/// <summary>
/// Request model for deleting a sale
/// </summary>
public class DeleteSaleRequest
{
    /// <summary>
    /// Gets or sets the sale ID
    /// </summary>
    public Guid Id { get; set; }
} 