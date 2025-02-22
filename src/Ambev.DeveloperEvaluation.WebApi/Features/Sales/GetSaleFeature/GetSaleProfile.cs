using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleFeature;

/// <summary>
/// AutoMapper profile for get sale feature
/// </summary>
public class GetSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleProfile"/> class
    /// </summary>
    public GetSaleProfile()
    {
        CreateMap<GetSaleResult, GetSaleResponse>();
        CreateMap<GetSaleItemResult, GetSaleItemResponse>()
            .ForMember(dest => dest.ProductTitle, opt => opt.MapFrom(src => src.ProductTitle));
    }
} 