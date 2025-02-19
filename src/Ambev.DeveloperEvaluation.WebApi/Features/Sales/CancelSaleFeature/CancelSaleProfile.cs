using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleFeature;

/// <summary>
/// Perfil de mapeamento para o cancelamento de venda
/// </summary>
public class CancelSaleProfile : Profile
{
    /// <summary>
    /// Inicializa uma nova instância do perfil com os mapeamentos definidos
    /// </summary>
    public CancelSaleProfile()
    {
        CreateMap<CancelSaleResult, CancelSaleResponse>();
        CreateMap<CancelSaleItemResult, CancelSaleItemResponse>();
    }
} 