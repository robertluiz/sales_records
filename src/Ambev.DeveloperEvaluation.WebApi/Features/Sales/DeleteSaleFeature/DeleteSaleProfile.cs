using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSaleFeature;

/// <summary>
/// AutoMapper profile for delete sale feature
/// </summary>
public class DeleteSaleProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of DeleteSaleProfile
    /// </summary>
    public DeleteSaleProfile()
    {
        CreateMap<DeleteSaleRequest, DeleteSaleCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
} 