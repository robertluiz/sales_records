using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

namespace Ambev.DeveloperEvaluation.Application.Sales.Profiles;

/// <summary>
/// Profile for mapping between sale entities and DTOs for cancellation
/// </summary>
public class CancelSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the cancel sale operation
    /// </summary>
    public CancelSaleProfile()
    {
        CreateMap<Sale, CancelSaleResult>();
        CreateMap<SaleItem, CancelSaleItemResult>();
    }
} 