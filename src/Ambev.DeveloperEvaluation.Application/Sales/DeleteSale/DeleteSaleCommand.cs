using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Command to delete an existing sale
/// </summary>
public class DeleteSaleCommand : IRequest<Unit>
{
    /// <summary>
    /// Gets or sets the ID of the sale to delete
    /// </summary>
    public Guid Id { get; set; }
} 