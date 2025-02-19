using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleFeature;

/// <summary>
/// Perfil de mapeamento para o cancelamento de item da venda
/// </summary>
public class CancelSaleItemProfile : Profile
{
    /// <summary>
    /// Inicializa uma nova instância do perfil com os mapeamentos definidos
    /// </summary>
    public CancelSaleItemProfile()
    {
        CreateMap<CancelSaleItemResult, CancelSaleItemResponse>();
    }
} 