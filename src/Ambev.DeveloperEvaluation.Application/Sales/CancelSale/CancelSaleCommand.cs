using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Command to cancel an existing sale
/// </summary>
public class CancelSaleCommand : IRequest<CancelSaleResult>
{
    /// <summary>
    /// Gets or sets the ID of the sale to cancel
    /// </summary>
    public Guid Id { get; set; }
} 