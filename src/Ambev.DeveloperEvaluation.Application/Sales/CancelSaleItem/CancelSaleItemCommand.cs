using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

/// <summary>
/// Command to cancel a specific item in a sale
/// </summary>
public class CancelSaleItemCommand : IRequest<CancelSaleItemResult>
{
    /// <summary>
    /// Gets or sets the ID of the sale
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the item to cancel
    /// </summary>
    public Guid ItemId { get; set; }
} 