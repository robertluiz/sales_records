using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSaleFeature;

/// <summary>
/// Request model for updating a sale
/// </summary>
public class UpdateSaleRequest
{
    /// <summary>
    /// Gets or sets the sale ID
    /// </summary>
    [Required(ErrorMessage = "Sale ID is required")]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the sale observation
    /// </summary>
    [MaxLength(500, ErrorMessage = "Observation cannot be longer than 500 characters")]
    public string? Observation { get; set; }

    /// <summary>
    /// Gets or sets the sale items to be updated
    /// </summary>
    public List<UpdateSaleItemRequest>? Items { get; set; }
}

/// <summary>
/// Request model for updating a sale item
/// </summary>
public class UpdateSaleItemRequest
{
    /// <summary>
    /// Gets or sets the item ID
    /// </summary>
    [Required(ErrorMessage = "Item ID is required")]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the item observation
    /// </summary>
    [MaxLength(200, ErrorMessage = "Observation cannot be longer than 200 characters")]
    public string? Observation { get; set; }
} 