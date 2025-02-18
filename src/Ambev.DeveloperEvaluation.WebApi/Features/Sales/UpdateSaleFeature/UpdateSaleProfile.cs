using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSaleFeature;

/// <summary>
/// AutoMapper profile for update sale feature
/// </summary>
public class UpdateSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleProfile"/> class
    /// </summary>
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleRequest, UpdateSaleCommand>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<UpdateSaleItemRequest, UpdateSaleItemCommand>();

        CreateMap<UpdateSaleResult, UpdateSaleResponse>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<UpdateSaleItemResult, UpdateSaleItemResponse>();
    }
} 