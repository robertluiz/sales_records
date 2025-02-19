using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

namespace Ambev.DeveloperEvaluation.Application.Sales.Profiles;

/// <summary>
/// Profile for mapping between sale item entities and DTOs for cancellation
/// </summary>
public class CancelSaleItemProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the cancel sale item operation
    /// </summary>
    public CancelSaleItemProfile()
    {
        CreateMap<SaleItem, CancelSaleItemResult>();
    }
} 