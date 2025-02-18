using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Query to get a sale by ID
/// </summary>
public class GetSaleQuery : IRequest<GetSaleResult>
{
    /// <summary>
    /// Gets or sets the sale ID
    /// </summary>
    public Guid Id { get; set; }
} 